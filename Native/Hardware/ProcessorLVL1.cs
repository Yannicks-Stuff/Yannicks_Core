using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Text;
using Yannick.Extensions.IntPtrExtensions;
using Yannick.Native.Hardware.Lang;

namespace Yannick.Native.Hardware
{
    public readonly struct ProcessorData
    {
        private uint[,] Data { get; }
        private uint[,] ExtData { get; }
        public uint MaxFunction { get; }
        public Processor.Vendor Manufacturer { get; }
        public string ManufacturerName { get; }
        public uint ApicId { get; }
        public string BrandString { get; }
        public uint CoreId { get; }
        public uint Family { get; }
        public uint Model { get; }
        public string Name { get; }
        public uint ProcessorId { get; }
        public uint Stepping { get; }
        public uint ThreadId { get; }
        public int Thread { get; }
        public IEnumerable<CPU_FLAG> Flags { get; }

        /*
        public float CoreLoad
        {
            get
            {
                long[] _idleTimes;
                float _totalLoad;
                long[] _totalTimes;

                Processor.GetTimes(out _idleTimes, out _totalTimes);

                if (_idleTimes == null)
                    return -1;

                if (!Processor.GetTimes(out long[] newIdleTimes, out long[] newTotalTimes))
                    return -1;


                for (int i = 0; i < Math.Min(newTotalTimes.Length, _totalTimes.Length); i++)
                {
                    if (newTotalTimes[i] - _totalTimes[i] < 100000)
                        return -1;
                }

                if (newIdleTimes == null)
                    return -1;


                float total = 0;
                int count = 0;
                //for (int i = 0; i < _cpuid.Length; i++)
                {
                    float value = 0;
                    //for (int j = 0; j < _cpuid[i].Length; j++)
                    {
                        long index = Thread;
                        if (index < newIdleTimes.Length && index < _totalTimes.Length)
                        {
                            float idle = (newIdleTimes[index] - _idleTimes[index]) /
                                         (float) (newTotalTimes[index] - _totalTimes[index]);
                            value += idle;
                            total += idle;
                            count++;
                        }
                    }

                    value = 1.0f - value / 1;
                    value = value < 0 ? 0 : value;
                    return value * 100;
                }
            }
        }*/

        internal ProcessorData(int thread)
        {
            Thread = thread;
            ulong mask = 1UL << thread;
            uint threadMaskWith, coreMaskWith;
            uint eax, ebx, ecx, edx = 0;
            Processor.CpuIdex(0, 0, out eax, out ebx, out ecx, out edx, mask);
            MaxFunction = eax;
            var maxCpuid = Math.Min(MaxFunction, 1024);
            var maxCpuidExt = Math.Min(MaxFunction - Processor.CPUID_EXT, 1024);

            Data = new uint[maxCpuid + 1, 4];
            for (uint i = 0; i < maxCpuid + 1; i++)
            {
                Processor.CpuIdex(0 + i, 0, out Data[i, 0], out Data[i, 1], out Data[i, 2], out Data[i, 3], mask);
            }

            ExtData = new uint[maxCpuidExt + 1, 4];
            for (uint i = 0; i < maxCpuidExt + 1; i++)
            {
                Processor.CpuIdex(Processor.CPUID_EXT + i, 0, out ExtData[i, 0], out ExtData[i, 1], out ExtData[i, 2],
                    out ExtData[i, 3], mask);
            }

            if (MaxFunction == 0)
            {
                ApicId = default;
                BrandString = string.Empty;
                CoreId = default;
                Family = default;
                Flags = new List<CPU_FLAG>();
                Manufacturer = default;
                ManufacturerName = string.Empty;
                Model = default;
                Stepping = default;
                ThreadId = default;
                Name = string.Empty;
                ProcessorId = default;
                return;
            }

            var info = Processor.CpuId(0, mask);
            var vendorBuilder = new StringBuilder();
            Processor.AppendRegister(vendorBuilder, info.EBX);
            Processor.AppendRegister(vendorBuilder, info.EDX);
            Processor.AppendRegister(vendorBuilder, info.ECX);
            ManufacturerName = vendorBuilder.ToString();
            switch (ManufacturerName)
            {
                case "AMDisbetter!":
                case "AuthenticAMD":
                    Manufacturer = Processor.Vendor.AMD;
                    break;
                case "CentaurHauls":
                    Manufacturer = Processor.Vendor.Centaur;
                    break;
                case "CyrixInstead":
                    Manufacturer = Processor.Vendor.Cyrix;
                    break;
                case "HygonGenuine":
                    Manufacturer = Processor.Vendor.Hygon;
                    break;
                case "GenuineIntel":
                    Manufacturer = Processor.Vendor.Intel;
                    break;
                case "TransmetaCPU":
                case "GenuineTMx86":
                    Manufacturer = Processor.Vendor.Transmeta;
                    break;
                case "Geode by NSC":
                    Manufacturer = Processor.Vendor.NationalSemiconductor;
                    break;
                case "NexGenDriven":
                    Manufacturer = Processor.Vendor.NexGen;
                    break;
                case "RiseRiseRise":
                    Manufacturer = Processor.Vendor.RISE;
                    break;
                case "SiS SiS SiS ":
                    Manufacturer = Processor.Vendor.SiS;
                    break;
                case "UMC UMC UMC ":
                    Manufacturer = Processor.Vendor.UMC;
                    break;
                case "VIA VIA VIA ":
                    Manufacturer = Processor.Vendor.VIA;
                    break;
                case "Vortex86 SoC":
                    Manufacturer = Processor.Vendor.VORTEX;
                    break;
                case "bhyve bhyve ":
                    Manufacturer = Processor.Vendor.bhyve;
                    break;
                case "KVMKVMKVM":
                    Manufacturer = Processor.Vendor.KVM;
                    break;
                case "Microsoft Hv":
                    Manufacturer = Processor.Vendor.MicrosoftHyperV;
                    break;
                case " lrpepyh vr":
                    Manufacturer = Processor.Vendor.Parallels;
                    break;
                case "VMwareVMware":
                    Manufacturer = Processor.Vendor.VMware;
                    break;
                case "XenVMMXenVMM":
                    Manufacturer = Processor.Vendor.XenHVM;
                    break;
                case "ACRNACRNACRN":
                    Manufacturer = Processor.Vendor.ProjectACRN;
                    break;
                default:
                    Manufacturer = Processor.Vendor.Unknown;
                    break;
            }

            var nameBuilder = new StringBuilder();
            for (uint i = 2; i <= 4; i++)
            {
                if (Processor.CpuIdex(Processor.CPUID_EXT + i, 0, out eax, out ebx, out ecx, out edx, mask))
                {
                    Processor.AppendRegister(nameBuilder, eax);
                    Processor.AppendRegister(nameBuilder, ebx);
                    Processor.AppendRegister(nameBuilder, ecx);
                    Processor.AppendRegister(nameBuilder, edx);
                }
            }

            nameBuilder.Replace('\0', ' ');
            BrandString = nameBuilder.ToString().Trim();
            nameBuilder.Replace("(R)", string.Empty);
            nameBuilder.Replace("(TM)", string.Empty);
            nameBuilder.Replace("(tm)", string.Empty);
            nameBuilder.Replace("CPU", string.Empty);
            nameBuilder.Replace("Quad-Core Processor", string.Empty);
            nameBuilder.Replace("Six-Core Processor", string.Empty);
            nameBuilder.Replace("Eight-Core Processor", string.Empty);

            for (int i = 0; i < 10; i++)
                nameBuilder.Replace("  ", " ");

            Name = nameBuilder.ToString();
            if (Name.Contains("@"))
                Name = Name.Remove(Name.LastIndexOf('@'));

            Name = Name.Trim();
            Family = ((Data[1, 0] & 0x0FF00000) >> 20) + ((Data[1, 0] & 0x0F00) >> 8);
            Model = ((Data[1, 0] & 0x0F0000) >> 12) + ((Data[1, 0] & 0xF0) >> 4);
            Stepping = Data[1, 0] & 0x0F;
            ApicId = (Data[1, 1] >> 24) & 0xFF;

            switch (Manufacturer)
            {
                case Processor.Vendor.Intel:
                    uint maxCoreAndThreadIdPerPackage = (Data[1, 1] >> 16) & 0xFF;
                    uint maxCoreIdPerPackage;
                    if (maxCpuid >= 4)
                        maxCoreIdPerPackage = ((Data[4, 0] >> 26) & 0x3F) + 1;
                    else
                        maxCoreIdPerPackage = 1;

                    threadMaskWith = Processor.NextLog2(maxCoreAndThreadIdPerPackage / maxCoreIdPerPackage);
                    coreMaskWith = Processor.NextLog2(maxCoreIdPerPackage);
                    break;
                case Processor.Vendor.AMD:
                    uint corePerPackage;
                    if (maxCpuidExt >= 8)
                        corePerPackage = (ExtData[8, 2] & 0xFF) + 1;
                    else
                        corePerPackage = 1;

                    threadMaskWith = 0;
                    coreMaskWith = Processor.NextLog2(corePerPackage);

                    if (Family == 0x17)
                    {
                        // ApicIdCoreIdSize: APIC ID size.
                        // cores per DIE
                        // we need this for Ryzen 5 (4 cores, 8 threads) ans Ryzen 6 (6 cores, 12 threads)
                        // Ryzen 5: [core0][core1][dummy][dummy][core2][core3] (Core0 EBX = 00080800, Core2 EBX = 08080800)
                        uint maxCoresPerDie = (ExtData[8, 2] >> 12) & 0xF;
                        switch (maxCoresPerDie)
                        {
                            case 0x04: // Ryzen
                                coreMaskWith = Processor.NextLog2(16);
                                break;
                            case 0x05: // Threadripper
                                coreMaskWith = Processor.NextLog2(32);
                                break;
                            case 0x06: // Epic
                                coreMaskWith = Processor.NextLog2(64);
                                break;
                        }
                    }

                    break;
                default:
                    threadMaskWith = 0;
                    coreMaskWith = 0;
                    break;
            }

            ProcessorId = ApicId >> (int)(coreMaskWith + threadMaskWith);
            CoreId = (ApicId >> (int)threadMaskWith) - (ProcessorId << (int)coreMaskWith);
            ThreadId = ApicId - (ProcessorId << (int)(coreMaskWith + threadMaskWith)) -
                       (CoreId << (int)threadMaskWith);

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

            var id = Processor.CpuId(0x0, mask);
            var nIds_ = id.EAX;
            var arra = new[] { id.EAX, id.EBX, id.ECX, id.EDX };

            for (uint i = 0; i <= nIds_; ++i)
            {
                arra = Processor.CpuIDex(i, 0, mask);
                var arr = new int[4];
                Array.Copy(arra, arr, arr.Length);
                data_.Add(arr);
            }

            var id2 = Processor.CpuId(0x80000000, mask);
            var nExIds_ = id2.EAX;
            for (uint i = 0x80000000; i <= nExIds_; ++i)
            {
                arra = Processor.CpuIDex(i, 0, mask);
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
        }
    }

    public sealed partial class Processor
    {
        internal const uint CPUID_EXT = 0x80000000;

        /*
        internal static bool GetTimes(out long[] idle, out long[] total)
        {

            unsafe
            {
                Winternl._SYSTEM_PERFORMANCE_INFORMATION[] information =
                    new Winternl._SYSTEM_PERFORMANCE_INFORMATION[64];
                idle = null;
                total = null;

                if (!Winternl.NtQuerySystemInformation(Winternl.SYSTEM_INFORMATION_CLASS.SystemPerformanceInformation, ref information, out var returnLength))
                {
                    return false;
                }


                idle = new long[(int) returnLength / information.Length];
                total = new long[(int) returnLength / information.Length];

                for (int i = 0; i < idle.Length; i++)
                {
                    idle[i] = information[i].IdleProcessTime.QuadPart;
                    total[i] = information[i].KernelTime + information[i].TUserTime;
                }

                return true;
            }
        }
*/
        private static uint[,] Data { get; set; } = new uint[0, 0];
        private static uint[,] ExtData { get; set; } = new uint[0, 0];
        public static uint ApicId { get; private set; }
        public static string BrandString { get; private set; } = string.Empty;
        public static uint CoreId { get; private set; }
        public static uint Family { get; private set; }

        public static uint Model { get; private set; }

        public static string Name { get; private set; } = string.Empty;

        public static uint ProcessorId { get; private set; }

        public static uint Stepping { get; private set; }

        public static uint ThreadId { get; private set; }

        private static void CPU_LVL_1()
        {
            uint threadMaskWith, coreMaskWith;
            uint eax, ebx, ecx, edx = 0;
            CpuIdex(0, 0, out eax, out ebx, out ecx, out edx);
            var maxCpuid = Math.Min(MaxFunction, 1024);
            var maxCpuidExt = Math.Min(MaxFunction - CPUID_EXT, 1024);

            Data = new uint[maxCpuid + 1, 4];
            for (uint i = 0; i < maxCpuid + 1; i++)
            {
                CpuIdex(0 + i, 0, out Data[i, 0], out Data[i, 1], out Data[i, 2], out Data[i, 3]);
            }

            ExtData = new uint[maxCpuidExt + 1, 4];
            for (uint i = 0; i < maxCpuidExt + 1; i++)
            {
                CpuIdex(CPUID_EXT + i, 0, out ExtData[i, 0], out ExtData[i, 1], out ExtData[i, 2], out ExtData[i, 3]);
            }

            var nameBuilder = new StringBuilder();
            for (uint i = 2; i <= 4; i++)
            {
                if (CpuIdex(CPUID_EXT + i, 0, out eax, out ebx, out ecx, out edx))
                {
                    AppendRegister(nameBuilder, eax);
                    AppendRegister(nameBuilder, ebx);
                    AppendRegister(nameBuilder, ecx);
                    AppendRegister(nameBuilder, edx);
                }
            }

            nameBuilder.Replace('\0', ' ');
            BrandString = nameBuilder.ToString().Trim();
            nameBuilder.Replace("(R)", string.Empty);
            nameBuilder.Replace("(TM)", string.Empty);
            nameBuilder.Replace("(tm)", string.Empty);
            nameBuilder.Replace("CPU", string.Empty);
            nameBuilder.Replace("Quad-Core Processor", string.Empty);
            nameBuilder.Replace("Six-Core Processor", string.Empty);
            nameBuilder.Replace("Eight-Core Processor", string.Empty);

            for (int i = 0; i < 10; i++)
                nameBuilder.Replace("  ", " ");

            Name = nameBuilder.ToString();
            if (Name.Contains("@"))
                Name = Name.Remove(Name.LastIndexOf('@'));

            Name = Name.Trim();
            Family = ((Data[1, 0] & 0x0FF00000) >> 20) + ((Data[1, 0] & 0x0F00) >> 8);
            Model = ((Data[1, 0] & 0x0F0000) >> 12) + ((Data[1, 0] & 0xF0) >> 4);
            Stepping = Data[1, 0] & 0x0F;
            ApicId = (Data[1, 1] >> 24) & 0xFF;

            switch (Manufacturer)
            {
                case Vendor.Intel:
                    uint maxCoreAndThreadIdPerPackage = (Data[1, 1] >> 16) & 0xFF;
                    uint maxCoreIdPerPackage;
                    if (maxCpuid >= 4)
                        maxCoreIdPerPackage = ((Data[4, 0] >> 26) & 0x3F) + 1;
                    else
                        maxCoreIdPerPackage = 1;

                    threadMaskWith = NextLog2(maxCoreAndThreadIdPerPackage / maxCoreIdPerPackage);
                    coreMaskWith = NextLog2(maxCoreIdPerPackage);
                    break;
                case Vendor.AMD:
                    uint corePerPackage;
                    if (maxCpuidExt >= 8)
                        corePerPackage = (ExtData[8, 2] & 0xFF) + 1;
                    else
                        corePerPackage = 1;

                    threadMaskWith = 0;
                    coreMaskWith = NextLog2(corePerPackage);

                    if (Family == 0x17)
                    {
                        // ApicIdCoreIdSize: APIC ID size.
                        // cores per DIE
                        // we need this for Ryzen 5 (4 cores, 8 threads) ans Ryzen 6 (6 cores, 12 threads)
                        // Ryzen 5: [core0][core1][dummy][dummy][core2][core3] (Core0 EBX = 00080800, Core2 EBX = 08080800)
                        uint maxCoresPerDie = (ExtData[8, 2] >> 12) & 0xF;
                        switch (maxCoresPerDie)
                        {
                            case 0x04: // Ryzen
                                coreMaskWith = NextLog2(16);
                                break;
                            case 0x05: // Threadripper
                                coreMaskWith = NextLog2(32);
                                break;
                            case 0x06: // Epic
                                coreMaskWith = NextLog2(64);
                                break;
                        }
                    }

                    break;
                default:
                    threadMaskWith = 0;
                    coreMaskWith = 0;
                    break;
            }

            ProcessorId = ApicId >> (int)(coreMaskWith + threadMaskWith);
            CoreId = (ApicId >> (int)threadMaskWith) - (ProcessorId << (int)coreMaskWith);
            ThreadId = ApicId - (ProcessorId << (int)(coreMaskWith + threadMaskWith)) -
                       (CoreId << (int)threadMaskWith);
        }


        internal static uint NextLog2(long x)
        {
            if (x <= 0)
                return 0;


            x--;
            uint count = 0;
            while (x > 0)
            {
                x >>= 1;
                count++;
            }

            return count;
        }
    }

    internal static class ThreadAffinity
    {
        public static ulong Set(ulong mask)
        {
            unsafe
            {
                if (mask == 0)
                    return 0;


                if (OperatingSystem.IsLinux())
                {
                    ulong result = 0;
                    if (LibC.sched_getaffinity(0, (IntPtr)Marshal.SizeOf(result), ref result) != 0)
                        return 0;

                    return LibC.sched_setaffinity(0, (IntPtr)Marshal.SizeOf(mask), ref mask) != 0 ? (ulong)0 : result;
                }

                UIntPtr uIntPtrMask;
                try
                {
                    uIntPtrMask = (UIntPtr)mask;
                }
                catch (OverflowException)
                {
                    throw new ArgumentOutOfRangeException(nameof(mask));
                }

                var a = uIntPtrMask.ToUInt32();

                return Processor._w_setThreadAffinityMask!(Processor._w_getCurrentThread!(),
                    new IntPtr(new UIntPtr(mask).ToPointer())).ToSafeUint64();
            }
        }
    }

    internal class LibC
    {
        private const string DllName = "libc";

        [DllImport(DllName)]
        internal static extern int sched_getaffinity(int pid, IntPtr maskSize, ref ulong mask);

        [DllImport(DllName)]
        internal static extern int sched_setaffinity(int pid, IntPtr maskSize, ref ulong mask);
    }
}