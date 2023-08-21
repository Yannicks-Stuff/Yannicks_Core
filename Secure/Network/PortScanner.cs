namespace Yannick.Secure.Network;

using System;
using System.Net;
using System.Net.Sockets;

/// <summary>
/// Provides methods for scanning network ports.
/// </summary>
public class PortScanner
{
    /// <summary>
    /// Checks if a specific port is open on a given IP address using the specified protocol.
    /// </summary>
    /// <param name="address">The IP address to check.</param>
    /// <param name="port">The port number to check.</param>
    /// <param name="protocolType">The protocol to use (TCP or UDP).</param>
    /// <returns>True if the port is open, false otherwise.</returns>
    public static bool IsPortOpen(IPAddress address, ushort port, ProtocolType protocolType)
    {
        try
        {
            using var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, protocolType);
            sock.Connect(address, port);
            return sock.Connected;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if a specific TCP port is open on a given IP address.
    /// </summary>
    /// <param name="address">The IP address to check.</param>
    /// <param name="port">The port number to check.</param>
    /// <returns>True if the port is open, false otherwise.</returns>
    public static bool IsTcpPortOpen(IPAddress address, ushort port)
        => IsPortOpen(address, port, ProtocolType.Tcp);

    /// <summary>
    /// Checks if a specific UDP port is open on a given IP address.
    /// </summary>
    /// <param name="address">The IP address to check.</param>
    /// <param name="port">The port number to check.</param>
    /// <returns>True if the port is open, false otherwise.</returns>
    public static bool IsUdpPortOpen(IPAddress address, ushort port)
        => IsPortOpen(address, port, ProtocolType.Udp);

    /// <summary>
    /// Scans a range of ports on a given IP address using the specified protocol.
    /// </summary>
    /// <param name="address">The IP address to scan.</param>
    /// <param name="from">The starting port number.</param>
    /// <param name="to">The ending port number.</param>
    /// <param name="protocolType">The protocol to use (TCP or UDP).</param>
    /// <returns>A list of open ports within the specified range.</returns>
    public static async Task<List<ushort>> Scan(IPAddress address, ushort from, ushort to, ProtocolType protocolType)
    {
        var openPorts = new List<ushort>();
        var tasks = new List<Task>();

        for (var port = from; port <= to; port++)
        {
            var port1 = port;
            tasks.Add(Task.Run(() =>
            {
                if (!IsPortOpen(address, port1, protocolType)) return;
                lock (openPorts)
                {
                    openPorts.Add(port1);
                }
            }));
        }

        await Task.WhenAll(tasks);
        return openPorts;
    }
}