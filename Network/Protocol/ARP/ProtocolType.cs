namespace Yannick.Network.Protocol.ARP;

public enum ProtocolType : ushort
{
    IPv4 = 0x0800,
    X25 = 0x0805,
    ARP = 0x0806,
    FrameRelay = 0x0808,
    IPv6 = 0x86DD,
}