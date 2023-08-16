using System.Runtime.InteropServices;

namespace Yannick.Network.Protocol.ARP;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct Header
{
    public HardwareType HardwareType;
    public ProtocolType ProtocolType;
    public byte HardwareLength;
    public byte ProtocolLength;
    public Operation Operation;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public MACAddress SenderMacAddress;

    public uint SenderIPAddress;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public MACAddress TargetMacAddress;

    public uint TargetIPAddress;


    public static Header? TryParse(byte[] rawData)
    {
        var handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);
        try
        {
            var buffer = handle.AddrOfPinnedObject();
            var o = Marshal.PtrToStructure(buffer, typeof(Header));
            return (Header?)o;
        }
        finally
        {
            handle.Free();
        }
    }
}