using System.Text;

namespace Yannick.Native.Hardware
{
    public sealed partial class Processor
    {
        public enum Vendor
        {
            Unknown,
            Intel,
            AMD,
            Centaur,
            Cyrix,
            Hygon,
            Transmeta,
            NationalSemiconductor,
            NexGen,
            RISE,
            SiS,
            UMC,
            VIA,
            VORTEX,
            bhyve,
            KVM,
            MicrosoftHyperV,
            Parallels,
            VMware,
            XenHVM,
            ProjectACRN,
            VirtualMachine = bhyve | KVM | MicrosoftHyperV | Parallels | VMware | XenHVM | ProjectACRN
        }

        public static uint MaxFunction { get; private set; }
        public static Vendor Manufacturer { get; private set; }
        public static string ManufacturerName { get; private set; }

        private static void CPU_LVL_0()
        {
            var info = CpuId(0);
            MaxFunction = info.EAX;
            var vendorBuilder = new StringBuilder();
            AppendRegister(vendorBuilder, info.EBX);
            AppendRegister(vendorBuilder, info.EDX);
            AppendRegister(vendorBuilder, info.ECX);
            ManufacturerName = vendorBuilder.ToString();
            switch (ManufacturerName)
            {
                case "AMDisbetter!":
                case "AuthenticAMD":
                    Manufacturer = Vendor.AMD;
                    break;
                case "CentaurHauls":
                    Manufacturer = Vendor.Centaur;
                    break;
                case "CyrixInstead":
                    Manufacturer = Vendor.Cyrix;
                    break;
                case "HygonGenuine":
                    Manufacturer = Vendor.Hygon;
                    break;
                case "GenuineIntel":
                    Manufacturer = Vendor.Intel;
                    break;
                case "TransmetaCPU":
                case "GenuineTMx86":
                    Manufacturer = Vendor.Transmeta;
                    break;
                case "Geode by NSC":
                    Manufacturer = Vendor.NationalSemiconductor;
                    break;
                case "NexGenDriven":
                    Manufacturer = Vendor.NexGen;
                    break;
                case "RiseRiseRise":
                    Manufacturer = Vendor.RISE;
                    break;
                case "SiS SiS SiS ":
                    Manufacturer = Vendor.SiS;
                    break;
                case "UMC UMC UMC ":
                    Manufacturer = Vendor.UMC;
                    break;
                case "VIA VIA VIA ":
                    Manufacturer = Vendor.VIA;
                    break;
                case "Vortex86 SoC":
                    Manufacturer = Vendor.VORTEX;
                    break;
                case "bhyve bhyve ":
                    Manufacturer = Vendor.bhyve;
                    break;
                case "KVMKVMKVM":
                    Manufacturer = Vendor.KVM;
                    break;
                case "Microsoft Hv":
                    Manufacturer = Vendor.MicrosoftHyperV;
                    break;
                case " lrpepyh vr":
                    Manufacturer = Vendor.Parallels;
                    break;
                case "VMwareVMware":
                    Manufacturer = Vendor.VMware;
                    break;
                case "XenVMMXenVMM":
                    Manufacturer = Vendor.XenHVM;
                    break;
                case "ACRNACRNACRN":
                    Manufacturer = Vendor.ProjectACRN;
                    break;
                default:
                    Manufacturer = Vendor.Unknown;
                    break;
            }

            CPU_LVL_1();
        }

        internal static void AppendRegister(StringBuilder b, uint value)
        {
            b.Append((char)(value & 0xff));
            b.Append((char)((value >> 8) & 0xff));
            b.Append((char)((value >> 16) & 0xff));
            b.Append((char)((value >> 24) & 0xff));
        }
    }
}