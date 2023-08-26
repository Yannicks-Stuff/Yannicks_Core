using System.Net.Sockets;

namespace Yannick.Network.Protocol.HTTP;

public abstract class HttpServer
{
    private readonly Socket Socket;
}