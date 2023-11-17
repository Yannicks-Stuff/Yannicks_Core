using System.Collections.Specialized;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Yannick.Native.Hardware
{
    public sealed partial class Processor
    {
        private static CpuidDelegate? Cpuid;
        private static RdtscDelegate? Rdtsc;
        private static IntPtr _codeBuffer;
        private static ulong _size;


        private static readonly byte[] CpuId32 =
        {
            0x55, // push ebp
            0x8B,
            0xEC, // mov ebp, esp
            0x83,
            0xEC,
            0x10, // sub esp, 10h
            0x8B,
            0x45,
            0x08, // mov eax, dword ptr [ebp+8]
            0x8B,
            0x4D,
            0x0C, // mov ecx, dword ptr [ebp+0Ch]
            0x53, // push ebx
            0x0F,
            0xA2, // cpuid
            0x56, // push esi
            0x8D,
            0x75,
            0xF0, // lea esi, [info]
            0x89,
            0x06, // mov dword ptr [esi],eax
            0x8B,
            0x45,
            0x10, // mov eax, dword ptr [eax]
            0x89,
            0x5E,
            0x04, // mov dword ptr [esi+4], ebx
            0x89,
            0x4E,
            0x08, // mov dword ptr [esi+8], ecx
            0x89,
            0x56,
            0x0C, // mov dword ptr [esi+0Ch], edx
            0x8B,
            0x4D,
            0xF0, // mov ecx, dword ptr [info]
            0x89,
            0x08, // mov dword ptr [eax], ecx
            0x8B,
            0x45,
            0x14, // mov eax, dword ptr [ebx]
            0x8B,
            0x4D,
            0xF4, // mov ecx, dword ptr [ebp-0Ch]
            0x89,
            0x08, // mov dword ptr [eax], ecx
            0x8B,
            0x45,
            0x18, // mov eax, dword ptr [ecx]
            0x8B,
            0x4D,
            0xF8, // mov ecx, dword ptr [ebp-8]
            0x89,
            0x08, // mov dword ptr [eax], ecx
            0x8B,
            0x45,
            0x1C, // mov eax, dword ptr [edx]
            0x8B,
            0x4D,
            0xFC, // mov ecx, dword ptr [ebp-4]
            0x5E, // pop esi
            0x89,
            0x08, // mov dword ptr [eax], ecx
            0x5B, // pop ebx
            0xC9, // leave
            0xC2,
            0x18,
            0x00 // ret 18h
        };

        private static readonly byte[] CpuId64Linux =
        {
            0x49,
            0x89,
            0xD2, // mov r10, rdx
            0x49,
            0x89,
            0xCB, // mov r11, rcx
            0x53, // push rbx
            0x89,
            0xF8, // mov eax, edi
            0x89,
            0xF1, // mov ecx, esi
            0x0F,
            0xA2, // cpuid
            0x41,
            0x89,
            0x02, // mov dword ptr [r10], eax
            0x41,
            0x89,
            0x1B, // mov dword ptr [r11], ebx
            0x41,
            0x89,
            0x08, // mov dword ptr [r8], ecx
            0x41,
            0x89,
            0x11, // mov dword ptr [r9], edx
            0x5B, // pop rbx
            0xC3 // ret
        };

        private static readonly byte[] CpuId64Windows =
        {
            0x48,
            0x89,
            0x5C,
            0x24,
            0x08, // mov qword ptr [rsp+8], rbx
            0x8B,
            0xC1, // mov eax, ecx
            0x8B,
            0xCA, // mov ecx, edx
            0x0F,
            0xA2, // cpuid
            0x41,
            0x89,
            0x00, // mov dword ptr [r8], eax
            0x48,
            0x8B,
            0x44,
            0x24,
            0x28, // mov rax, qword ptr [rsp+28h]
            0x41,
            0x89,
            0x19, // mov dword ptr [r9], ebx
            0x48,
            0x8B,
            0x5C,
            0x24,
            0x08, // mov rbx, qword ptr [rsp+8]
            0x89,
            0x08, // mov dword ptr [rax], ecx
            0x48,
            0x8B,
            0x44,
            0x24,
            0x30, // mov rax, qword ptr [rsp+30h]
            0x89,
            0x10, // mov dword ptr [rax], edx
            0xC3 // ret
        };

        private static readonly byte[] Rdtsc32 =
        {
            0x0F,
            0x31, // rdtsc
            0xC3 // ret
        };

        private static readonly byte[] Rdtsc64 =
        {
            0x0F,
            0x31, // rdtsc
            0x48,
            0xC1,
            0xE2,
            0x20, // shl rdx, 20h
            0x48,
            0x0B,
            0xC2, // or rax, rdx
            0xC3 // ret
        };

        private static void OpenCpuIdWithTIME()
        {
            byte[] rdTscCode;
            byte[] cpuidCode;
            if (IntPtr.Size == 4)
            {
                rdTscCode = Rdtsc32;
                cpuidCode = CpuId32;
            }
            else
            {
                rdTscCode = Rdtsc64;

                cpuidCode = OperatingSystem.IsLinux() ? CpuId64Linux : CpuId64Windows;
            }

            _size = (ulong)(rdTscCode.Length + cpuidCode.Length);

            if (OperatingSystem.IsLinux())
            {
                var assembly = Assembly.Load("Mono.Posix, Version=2.0.0.0, Culture=neutral, " +
                                             "PublicKeyToken=0738eb9f132ed756");
                var sysCall = assembly.GetType("Mono.Unix.Native.Syscall");
                var mmap = sysCall!.GetMethod("mmap");

                var mmapProts = assembly.GetType("Mono.Unix.Native.MmapProts");
                var mmapProtsParam = Enum.ToObject(mmapProts!,
                    (int)mmapProts!.GetField("PROT_READ")!.GetValue(null)! |
                    (int)mmapProts.GetField("PROT_WRITE")!.GetValue(null)! |
                    (int)mmapProts.GetField("PROT_EXEC")!.GetValue(null)!);

                var mmapFlags = assembly.GetType("Mono.Unix.Native.MmapFlags");
                var mmapFlagsParam = Enum.ToObject(mmapFlags!,
                    (int)mmapFlags!.GetField("MAP_ANONYMOUS")!.GetValue(null)! |
                    (int)mmapFlags!.GetField("MAP_PRIVATE")!.GetValue(null)!);

                if (mmap != null)
                    _codeBuffer =
                        (IntPtr)(mmap.Invoke(null,
                                     new[] { IntPtr.Zero, _size, mmapProtsParam, mmapFlagsParam, -1, 0 }) ??
                                 throw new InvalidOperationException());
            }
            else
            {
                _codeBuffer = _w_virtualAlloc!(IntPtr.Zero,
                    _size,
                    0x1000 | 0x2000,
                    0x40);
            }

            Marshal.Copy(rdTscCode, 0, _codeBuffer, rdTscCode.Length);
            Rdtsc = Marshal.GetDelegateForFunctionPointer(_codeBuffer, typeof(RdtscDelegate)) as RdtscDelegate;
            var cpuidAddress = checked((IntPtr)((long)_codeBuffer + rdTscCode.Length));
            Marshal.Copy(cpuidCode, 0, cpuidAddress, cpuidCode.Length);
            Cpuid = Marshal.GetDelegateForFunctionPointer(cpuidAddress, typeof(CpuidDelegate)) as CpuidDelegate;
        }

        private static void CloseCpuIdWithTIME()
        {
            Rdtsc = null;
            Cpuid = null;

            if (OperatingSystem.IsLinux())
            {
                var assembly = Assembly.Load("Mono.Posix, Version=2.0.0.0, Culture=neutral, " +
                                             "PublicKeyToken=0738eb9f132ed756");
                var sysCall = assembly.GetType("Mono.Unix.Native.Syscall");
                var method = sysCall!.GetMethod("munmap");
                method?.Invoke(null, new object[] { _codeBuffer, _size });
            }
            else
            {
                _w_virtualFree!(_codeBuffer, 0ul, 0x8000);
            }
        }

        internal static Register CpuIDex(uint index, uint ecxValue, ulong mask = 0)
        {
            uint eax, ebx, ecx, edx = 0;
            CpuIdex(index, ecxValue, out eax, out ebx, out ecx, out edx, mask);
            var bufferL = new List<byte>();
            bufferL.AddRange(BitConverter.GetBytes(eax));
            bufferL.AddRange(BitConverter.GetBytes(ebx));
            bufferL.AddRange(BitConverter.GetBytes(ecx));
            bufferL.AddRange(BitConverter.GetBytes(edx));
            var buffer = bufferL.ToArray();
            var obj = new Register();
            var len = Marshal.SizeOf<Register>();
            var i = Marshal.AllocHGlobal(len);
            Marshal.Copy(buffer, 0, i, len);
            var s = Marshal.PtrToStructure(i, obj.GetType());
            Marshal.FreeHGlobal(i);
            return (Register)s!;
        }

        internal static Register CpuId(uint level, ulong mask = 0) => CpuIDex(level, 0, mask);

        public static bool CpuIdex(uint level, uint function, out uint eax, out uint ebx, out uint ecx, out uint edx,
            ulong mask = 0)
        {
            if (mask == 0)
            {
                OpenCpuIdWithTIME();
                Cpuid!(level, function, out eax, out ebx, out ecx, out edx);
                CloseCpuIdWithTIME();
                return true;
            }

            if (mask > 0 && (mask = ThreadAffinity.Set(mask)) == 0)
            {
                eax = ebx = ecx = edx = 0;
                return false;
            }

            OpenCpuIdWithTIME();
            Cpuid!(level, function, out eax, out ebx, out ecx, out edx);
            ThreadAffinity.Set(mask);
            CloseCpuIdWithTIME();
            return true;
        }

        [StructLayout(LayoutKind.Sequential)]
        public readonly struct Register
        {
            public readonly uint EAX;
            public readonly uint EBX;
            public readonly uint ECX;
            public readonly uint EDX;

            public static implicit operator byte[](Register register)
            {
                var bufferL = new List<byte>();
                bufferL.AddRange(BitConverter.GetBytes(register.EAX));
                bufferL.AddRange(BitConverter.GetBytes(register.EBX));
                bufferL.AddRange(BitConverter.GetBytes(register.ECX));
                bufferL.AddRange(BitConverter.GetBytes(register.EDX));
                return bufferL.ToArray();
            }

            public static implicit operator uint[](Register register)
            {
                return new[] { register.EAX, register.EBX, register.ECX, register.EDX };
            }

            public static implicit operator BitVector32[](Register register)
            {
                return new[]
                {
                    new BitVector32((int)register.EAX),
                    new BitVector32((int)register.EBX),
                    new BitVector32((int)register.ECX),
                    new BitVector32((int)register.EDX),
                };
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate bool CpuidDelegate(uint index, uint ecxValue, out uint eax, out uint ebx, out uint ecx,
            out uint edx);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate ulong RdtscDelegate();
    }
}