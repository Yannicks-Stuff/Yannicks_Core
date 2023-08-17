using System.Runtime.InteropServices;

namespace Yannick.Network;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct MACAddress
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public byte[] Address;

    public MACAddress(byte[] address)
    {
        Address = address;
    }

    public MACAddress(IEnumerable<byte> address)
    {
        Address = address.ToArray();
    }

    public static implicit operator byte[](MACAddress address) => address.Address;

    public static implicit operator MACAddress(string adr)
    {
        var bytes = new byte[6];
        var parts = adr.Split('-');

        for (var i = 0; i < 6; i++)
        {
            bytes[i] = Convert.ToByte(parts[i], 16);
        }

        return new MACAddress(bytes);
    }

    public override string ToString()
        => string.Join(":", Address.Select(b => b.ToString("X2")));
}