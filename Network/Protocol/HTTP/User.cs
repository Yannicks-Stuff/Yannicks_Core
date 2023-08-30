using System.Net.Sockets;

namespace Yannick.Network.Protocol.HTTP;

public partial class Server
{
    public class User : TCP.Server.User
    {
        public User(Socket socket, TCP.Server server) : base(socket, server)
        {
            ReceiveTimeout = TimeSpan.FromSeconds(10);
            BufferSize = 0;
            Listen = true;
        }

        public bool IsDecrypted { get; internal set; }
    }
}