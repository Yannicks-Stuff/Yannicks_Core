using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Yannick.Network.Protocol.ARP;

/// <summary>
/// Represents a client for sending and receiving ARP (Address Resolution Protocol) requests and responses.
/// </summary>
public sealed class ArpClient : IDisposable
{
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
    private Socket _socket;
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value

    public ArpClient()
    {
        throw new NotImplementedException("NO direct to OSI 2");
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ArpClient"/> class.
    /// </summary>
    public ArpClient(IPEndPoint endPoint) : this()
    {
        //_socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, SocketType.Raw);
        _socket.Connect(endPoint); //NO SUPPORT
    }

    /// <summary>
    /// Release socket
    /// </summary>
    public void Dispose()
    {
        _socket.Dispose();
    }

    /// <summary>
    /// Sends an ARP request to a remote host.
    /// </summary>
    /// <param name="header">The ARP header of the request to be sent.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="header"/> is null.</exception>
    /// <exception cref="SocketException">Thrown when an error occurs while accessing the socket.</exception>
    public void Request(Header header)
    {
        var buffer = new byte[Marshal.SizeOf(header)];
        var ptr = Marshal.AllocHGlobal(buffer.Length);

        try
        {
            Marshal.StructureToPtr(header, ptr, false);
            Marshal.Copy(ptr, buffer, 0, buffer.Length);
            _socket.Send(buffer);
        }
        finally
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    /// <summary>
    /// Receives an ARP response from a remote host.
    /// </summary>
    /// <returns>The ARP header of the received response.</returns>
    /// <exception cref="SocketException">Thrown when an error occurs while accessing the socket.</exception>
    public Header? Response()
    {
        var buffer = new byte[Marshal.SizeOf(typeof(Header))];
        _socket.Receive(buffer);

        return Header.TryParse(buffer);
    }
}