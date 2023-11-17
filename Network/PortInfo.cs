using System.Net;
using System.Net.Sockets;

namespace Yannick.Network
{
    /// <summary>
    /// Provides utility methods for working with network ports.
    /// </summary>
    public static class PortInfo
    {
        /// <summary>
        /// Determines whether the specified port number is valid.
        /// </summary>
        /// <param name="port">The port number to validate.</param>
        /// <returns><c>true</c> if the port number is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValidPort(int port) => port is > 0 and <= 65535;

        /// <summary>
        /// Determines whether the specified port is open on the given IP address.
        /// </summary>
        /// <param name="port">The port number to check.</param>
        /// <param name="address">The IP address to check. If not specified, the local IP address is used.</param>
        /// <param name="transportType">The transport protocol to use. Can be either <see cref="ProtocolType.Tcp"/> or <see cref="ProtocolType.Udp"/>.</param>
        /// <param name="timeout">The maximum amount of time to wait for a response. If not specified, a default timeout of 2 seconds is used.</param>
        /// <returns><c>true</c> if the port is open; otherwise, <c>false</c>.</returns>
        public static bool IsPortOpen(ushort port, IPAddress? address = null,
            ProtocolType transportType = ProtocolType.Tcp, TimeSpan? timeout = null)
        {
            address ??= IPAddress.Loopback;
            timeout ??= TimeSpan.FromSeconds(2);

            using var socket = new Socket(AddressFamily.InterNetwork,
                transportType == ProtocolType.Tcp ? SocketType.Stream : SocketType.Dgram, transportType);
            try
            {
                socket.ReceiveTimeout = (int)timeout.Value.TotalMilliseconds;
                socket.SendTimeout = (int)timeout.Value.TotalMilliseconds;

                if (transportType == ProtocolType.Tcp)
                    socket.Connect(new IPEndPoint(address, port));
                else
                {
                    socket.SendTo(new byte[1], new IPEndPoint(address, port));
                    return socket.Poll(timeout.Value.Milliseconds, SelectMode.SelectRead);
                }

                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }
    }
}