using System.Collections.Specialized;
using System.Runtime.InteropServices;
using Yannick.Native.Hardware.Lang;

namespace Yannick.Native.Hardware
{
    public sealed partial class Processor
    {
        internal static Library? Kernel32;
        internal static W_VirtualAlloc? _w_virtualAlloc;
        internal static W_VirtualFree? _w_virtualFree;
        internal static W_SetThreadAffinityMask? _w_setThreadAffinityMask;
        internal static W_GetCurrentThread? _w_getCurrentThread;

        static Processor()
        {
            if (OperatingSystem.IsWindows())
            {
                Kernel32 = new Library("kernel32.dll");
                _w_virtualAlloc = Kernel32.Link<W_VirtualAlloc>("VirtualAlloc")!;
                _w_virtualFree = Kernel32.Link<W_VirtualFree>("VirtualFree")!;
                _w_setThreadAffinityMask = Kernel32.Link<W_SetThreadAffinityMask>("SetThreadAffinityMask")!;
                _w_getCurrentThread = Kernel32.Link<W_GetCurrentThread>("GetCurrentThread")!;
            }

            //if (RuntimeInformation.IsOSPlatform(OSPlatform.WindowsLinux))
            {
                var data_ = new List<int[]>();
                var extdata_ = new List<int[]>();

                BitVector32 f_1_EAX_;
                BitVector32 f_1_ECX_;
                BitVector32 f_1_EDX_;

                BitVector32 f_7_EBX_;
                BitVector32 f_7_ECX_;
                BitVector32 f_7_EDX_;

                BitVector32 f_81_ECX_;
                BitVector32 f_81_EDX_;

                var id = CpuId(0x0);
                var nIds_ = id.EAX;
                var arra = new[] { id.EAX, id.EBX, id.ECX, id.EDX };

                for (uint i = 0; i <= nIds_; ++i)
                {
                    arra = CpuIDex(i, 0);
                    var arr = new int[4];
                    Array.Copy(arra, arr, arr.Length);
                    data_.Add(arr);
                }

                var id2 = CpuId(0x80000000);
                var nExIds_ = id2.EAX;
                for (uint i = 0x80000000; i <= nExIds_; ++i)
                {
                    arra = CpuIDex(i, 0);
                    var arr = new int[4];
                    Array.Copy(arra, arr, arr.Length);
                    extdata_.Add(arr);
                }

                var liste = new List<CPU_FLAG>();

                if (nIds_ >= 1)
                {
                    f_1_EAX_ = new BitVector32(data_[1][1]);
                    f_1_ECX_ = new BitVector32(data_[1][2]);
                    f_1_EDX_ = new BitVector32(data_[1][3]);

                    var t = 1 << 5;
                    var t1 = f_1_EAX_[t];
                    if (t1)
                        liste.Add(CPU_FLAG.AVX512BF16);

                    var k1 = 1 << 0;
                    var k = f_1_ECX_[k1];
                    if (k)
                        liste.Add(CPU_FLAG.SSE3);
                    if (f_1_ECX_[1 << 1])
                        liste.Add(CPU_FLAG.PCLMUL);
                    if (f_1_ECX_[1 << 5])
                        liste.Add(CPU_FLAG.LZCNT);
                    if (f_1_ECX_[1 << 9])
                        liste.Add(CPU_FLAG.SSSE3);
                    if (f_1_ECX_[1 << 12])
                        liste.Add(CPU_FLAG.FMA);
                    if (f_1_ECX_[1 << 13])
                        liste.Add(CPU_FLAG.CMPXCHG16B);
                    if (f_1_ECX_[1 << 19])
                        liste.Add(CPU_FLAG.SSE4_1);
                    if (f_1_ECX_[1 << 20])
                        liste.Add(CPU_FLAG.SSE4_2);
                    if (f_1_ECX_[1 << 22])
                        liste.Add(CPU_FLAG.MOVBE);
                    if (f_1_ECX_[1 << 23])
                        liste.Add(CPU_FLAG.POPCNT);
                    if (f_1_ECX_[1 << 25])
                        liste.Add(CPU_FLAG.AES);
                    if (f_1_ECX_[1 << 26])
                        liste.Add(CPU_FLAG.XSAVE);
                    if (f_1_ECX_[1 << 27])
                        liste.Add(CPU_FLAG.OSXSAVE);
                    if (f_1_ECX_[1 << 28])
                        liste.Add(CPU_FLAG.AVX);
                    if (f_1_ECX_[1 << 29])
                        liste.Add(CPU_FLAG.F16C);
                    if (f_1_ECX_[1 << 30])
                        liste.Add(CPU_FLAG.RDRND);

                    if (f_1_EDX_[1 << 8])
                        liste.Add(CPU_FLAG.CMPXCHG8B);
                    if (f_1_EDX_[1 << 15])
                        liste.Add(CPU_FLAG.CMOV);
                    if (f_1_EDX_[1 << 23])
                        liste.Add(CPU_FLAG.MMX);
                    if (f_1_EDX_[1 << 24])
                        liste.Add(CPU_FLAG.FXSAVE);
                    if (f_1_EDX_[1 << 25])
                        liste.Add(CPU_FLAG.SSE);
                    if (f_1_EDX_[1 << 26])
                        liste.Add(CPU_FLAG.SSE2);
                }

                if (nIds_ >= 7)
                {
                    f_7_EBX_ = new BitVector32(data_[7][1]);
                    f_7_ECX_ = new BitVector32(data_[7][2]);
                    f_7_EDX_ = new BitVector32(data_[7][3]);

                    if (f_7_EBX_[1 << 0])
                        liste.Add(CPU_FLAG.FSGSBASE);
                    if (f_7_EBX_[1 << 2])
                        liste.Add(CPU_FLAG.SGX);
                    if (f_7_EBX_[1 << 3])
                        liste.Add(CPU_FLAG.BMI);
                    if (f_7_EBX_[1 << 4])
                        liste.Add(CPU_FLAG.HLE);
                    if (f_7_EBX_[1 << 5])
                        liste.Add(CPU_FLAG.AVX2);
                    if (f_7_EBX_[1 << 8])
                        liste.Add(CPU_FLAG.BMI2);
                    if (f_7_EBX_[1 << 11])
                        liste.Add(CPU_FLAG.RTM);
                    if (f_7_EBX_[1 << 14])
                        liste.Add(CPU_FLAG.MPX);
                    if (f_7_EBX_[1 << 16])
                        liste.Add(CPU_FLAG.AVX512F);
                    if (f_7_EBX_[1 << 17])
                        liste.Add(CPU_FLAG.AVX512DQ);
                    if (f_7_EBX_[1 << 18])
                        liste.Add(CPU_FLAG.RDSEED);
                    if (f_7_EBX_[1 << 19])
                        liste.Add(CPU_FLAG.ADX);
                    if (f_7_EBX_[1 << 21])
                        liste.Add(CPU_FLAG.AVX512IFMA);
                    if (f_7_EBX_[1 << 23])
                        liste.Add(CPU_FLAG.CLFLUSHOPT);
                    if (f_7_EBX_[1 << 24])
                        liste.Add(CPU_FLAG.CLWB);
                    if (f_7_EBX_[1 << 26])
                        liste.Add(CPU_FLAG.AVX512PF);
                    if (f_7_EBX_[1 << 27])
                        liste.Add(CPU_FLAG.AVX512ER);
                    if (f_7_EBX_[1 << 28])
                        liste.Add(CPU_FLAG.AVX512CD);
                    if (f_7_EBX_[1 << 29])
                        liste.Add(CPU_FLAG.SHA);
                    if (f_7_EBX_[1 << 30])
                        liste.Add(CPU_FLAG.AVX512BW);
                    if (f_7_EBX_[unchecked((int)(1u << 31))])
                        liste.Add(CPU_FLAG.AVX512VL);

                    if (f_7_ECX_[1 << 0])
                        liste.Add(CPU_FLAG.PREFETCHWT1);
                    if (f_7_ECX_[1 << 1])
                        liste.Add(CPU_FLAG.AVX512VBMI);
                    if (f_7_ECX_[1 << 3])
                        liste.Add(CPU_FLAG.PKU);
                    if (f_7_ECX_[1 << 4])
                        liste.Add(CPU_FLAG.OSPKE);
                    if (f_7_ECX_[1 << 5])
                        liste.Add(CPU_FLAG.WAITPKG);
                    if (f_7_ECX_[1 << 6])
                        liste.Add(CPU_FLAG.AVX512VBMI2);
                    if (f_7_ECX_[1 << 7])
                        liste.Add(CPU_FLAG.SHSTK);
                    if (f_7_ECX_[1 << 8])
                        liste.Add(CPU_FLAG.GFNI);
                    if (f_7_ECX_[1 << 9])
                        liste.Add(CPU_FLAG.VAES);
                    if (f_7_ECX_[1 << 10])
                        liste.Add(CPU_FLAG.AVX512VNNI);
                    if (f_7_ECX_[1 << 11])
                        liste.Add(CPU_FLAG.VPCLMULQDQ);
                    if (f_7_ECX_[1 << 12])
                        liste.Add(CPU_FLAG.AVX512BITALG);
                    if (f_7_ECX_[1 << 14])
                        liste.Add(CPU_FLAG.AVX512VPOPCNTDQ);
                    if (f_7_ECX_[1 << 22])
                        liste.Add(CPU_FLAG.RDPID);
                    if (f_7_ECX_[1 << 25])
                        liste.Add(CPU_FLAG.CLDEMOTE);
                    if (f_7_ECX_[1 << 27])
                        liste.Add(CPU_FLAG.MOVDIRI);
                    if (f_7_ECX_[1 << 28])
                        liste.Add(CPU_FLAG.MOVDIR64B);

                    if (f_7_EDX_[1 << 2])
                        liste.Add(CPU_FLAG.AVX5124VNNIW);
                    if (f_7_EDX_[1 << 3])
                        liste.Add(CPU_FLAG.AVX5124FMAPS);
                    if (f_7_EDX_[1 << 18])
                        liste.Add(CPU_FLAG.PCONFIG);
                    if (f_7_EDX_[1 << 20])
                        liste.Add(CPU_FLAG.IBT);
                }

#pragma warning disable CS0652 // Der Vergleich mit einer ganzzahligen Konstante ist nutzlos. Die Konstante befindet sich außerhalb des Bereichs vom Typ "int".
                if (nExIds_ >= 0x80000001)
#pragma warning restore CS0652 // Der Vergleich mit einer ganzzahligen Konstante ist nutzlos. Die Konstante befindet sich außerhalb des Bereichs vom Typ "int".
                {
                    f_81_ECX_ = new BitVector32(extdata_[1][2]);
                    f_81_EDX_ = new BitVector32(extdata_[1][3]);

                    if (f_81_ECX_[1 << 0])
                        liste.Add(CPU_FLAG.LAHF_LM);
                    if (f_81_ECX_[1 << 5])
                        liste.Add(CPU_FLAG.ABM);
                    if (f_81_ECX_[1 << 6])
                        liste.Add(CPU_FLAG.SSE4a);
                    if (f_81_ECX_[1 << 8])
                        liste.Add(CPU_FLAG.PRFCHW);
                    if (f_81_ECX_[1 << 11])
                        liste.Add(CPU_FLAG.XOP);
                    if (f_81_ECX_[1 << 15])
                        liste.Add(CPU_FLAG.LWP);
                    if (f_81_ECX_[1 << 16])
                        liste.Add(CPU_FLAG.FMA4);
                    if (f_81_ECX_[1 << 21])
                        liste.Add(CPU_FLAG.TBM);
                    if (f_81_ECX_[1 << 29])
                        liste.Add(CPU_FLAG.MWAITX);

                    if (f_81_EDX_[1 << 22])
                        liste.Add(CPU_FLAG.MMXEXT);
                    if (f_81_EDX_[1 << 29])
                        liste.Add(CPU_FLAG.LM);
                    if (f_81_EDX_[1 << 30])
                        liste.Add(CPU_FLAG._3DNOWP);
                    if (f_81_EDX_[unchecked((int)(1u << 31))])
                        liste.Add(CPU_FLAG._3DNOW);
                }

                if (nIds_ >= 13 && data_[0][2] < 1)
                {
                    var f_13_EDX = new BitVector32(data_[13][3]);

                    if (f_13_EDX[1 << 3])
                        liste.Add(CPU_FLAG.BNDREGS);
                    if (f_13_EDX[1 << 4])
                        liste.Add(CPU_FLAG.BNDCSR);
                }
                else if (nIds_ >= 13 && data_[0][2] >= 1)
                {
                    var f_13_EDX = new BitVector32(data_[13][3]);

                    if (f_13_EDX[1 << 0])
                        liste.Add(CPU_FLAG.XSAVEOPT);
                    if (f_13_EDX[1 << 1])
                        liste.Add(CPU_FLAG.XSAVEC);
                    if (f_13_EDX[1 << 3])
                        liste.Add(CPU_FLAG.XSAVES);
                }

                if (nIds_ >= 14 && data_[0][2] < 1)
                {
                    var f_14_EBX = new BitVector32(data_[14][1]);

                    if (f_14_EBX[1 << 4])
                        liste.Add(CPU_FLAG.PTWRITE);
                }

                Flags = liste;
                CPU_LVL_0();
            }
            //else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
            }
            //else
            {
            }
        }

        public static IReadOnlyCollection<CPU_FLAG> Flags { get; private set; }


        public static IEnumerable<ProcessorData> Processoren()
        {
            for (int i = 0; i < 64; i++)
            {
                var a = new ProcessorData(i);
                if (a.MaxFunction == default)
                    break;
                yield return a;
            }
        }

        internal delegate int W_VirtualFree(IntPtr address, ulong size, uint freeType);

        internal delegate IntPtr W_VirtualAlloc(IntPtr address,
            ulong dwSize,
            uint flAllocationType,
            uint flProtect);

        internal delegate IntPtr W_SetThreadAffinityMask(IntPtr hThread, IntPtr dwThreadAffinityMask);

        internal delegate IntPtr W_GetCurrentThread();
    }
}