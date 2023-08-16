namespace Yannick.Network.Protocol.ARP;

public enum Operation : ushort
{
    Request = 1,
    Reply = 2,
    RequestReverse = 3,
    ReplyReverse = 4,
    DRARPRequest = 5,
    DRARPReply = 6,
    DRARPError = 7,
    InARPRequest = 8,
    InARPReply = 9,
    ARPNAK = 10,
}