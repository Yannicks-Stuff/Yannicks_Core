using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace Yannick.Network.Protocol.ICMP;

/// <summary>
/// Provides functionality to send and receive ICMPv4 messages
/// </summary>
public sealed partial class ICMP4 : IDisposable
{
    /// <summary>
    /// Maximum size of buffer, as specified by the Maximum Transmission Unit (MTU).
    /// </summary>
    public const uint MTUBufferSize = 65507;

    /// <summary>
    /// Socket used for sending and receiving ICMPv4 messages.
    /// </summary>
    private readonly Socket _socket = new(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);

    /// <summary>
    /// Releases the resources used by the <see cref="ICMP4"/> instance.
    /// </summary>
    public void Dispose()
    {
        _socket.Dispose();
    }

    /// <summary>
    /// Sends an ICMPv4 message to a specified address.
    /// </summary>
    /// <param name="package">The ICMPv4 package to send.</param>
    /// <param name="address">The destination address as a string.</param>
    public void Send(Package package, string address) => Send(package, IPAddress.Parse(address));

    /// <summary>
    /// Sends an ICMPv4 message to a specified address.
    /// </summary>
    /// <param name="package">The ICMPv4 package to send.</param>
    /// <param name="address">The destination IP address.</param>
    public void Send(Package package, IPAddress address)
    {
        if (address.AddressFamily != AddressFamily.InterNetwork)
            throw new InvalidOperationException("Use IPV4");

        var headerBytes = new[]
        {
            (byte)package.Header.Type,
            (byte)package.Header.Code,
            (byte)(package.Header.Checksum >> 8),
            (byte)(package.Header.Checksum & 0xFF),
            (byte)(package.Header.Identifier >> 8),
            (byte)(package.Header.Identifier & 0xFF),
            (byte)(package.Header.SequenceNumber >> 8),
            (byte)(package.Header.SequenceNumber & 0xFF),
        };

        Array.Reverse(headerBytes, 2, 2);
        Array.Reverse(headerBytes, 4, 2);
        Array.Reverse(headerBytes, 6, 2);


        var packetBytes = new byte[headerBytes.Length + package.Data.Length];
        Buffer.BlockCopy(headerBytes, 0, packetBytes, 0, headerBytes.Length);
        Buffer.BlockCopy(package.Data, 0, packetBytes, headerBytes.Length, package.Data.Length);

        _socket.SendTo(packetBytes, new IPEndPoint(address, 0));
    }

    /// <summary>
    /// Receives an ICMPv4 message.
    /// </summary>
    /// <param name="bufferSize">The buffer size in bytes. Defaults to 64.</param>
    /// <returns>The received ICMPv4 package.</returns>
    public Package Receive(uint bufferSize = 512)
    {
        if (bufferSize > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(bufferSize),
                "Der Buffer darf nicht größer als ein signed int sein.");
        if (bufferSize > 65507)
            bufferSize = MTUBufferSize;
        if (bufferSize == 0)
            bufferSize = 64;
        if (bufferSize < 32)
            bufferSize = 32;

        var buffer = new byte[bufferSize];

        EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
        var receivedBytes = _socket.ReceiveFrom(buffer, ref remoteEndPoint);

        var receivedData = new byte[receivedBytes];
        Array.Copy(buffer, receivedData, receivedBytes);

        return new Package(receivedData);
    }

    /// <summary>
    /// Asynchronously receives an ICMPv4 message.
    /// </summary>
    /// <param name="bufferSize">The buffer size in bytes. Defaults to 64.</param>
    /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
    /// <returns>A task representing the received ICMPv4 package.</returns>
    public async Task<Package> ReceiveAsync(uint bufferSize = 512, CancellationToken cancellationToken = default)
    {
        if (bufferSize > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(bufferSize),
                "Der Buffer darf nicht größer als ein signed int sein.");
        if (bufferSize > 65507)
            bufferSize = MTUBufferSize;
        if (bufferSize == 0)
            bufferSize = 64;
        if (bufferSize < 32)
            bufferSize = 32;

        var buffer = new byte[bufferSize];

        EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
        var receivedBytes = (await _socket.ReceiveFromAsync(buffer, remoteEndPoint, cancellationToken)).ReceivedBytes;

        var receivedData = new byte[receivedBytes];
        Array.Copy(buffer, receivedData, receivedBytes);

        return new Package(receivedData);
    }

    /// <summary>
    /// Sends an ICMP Echo Request (ping) to the specified address and measures the time it takes to receive a reply.
    /// </summary>
    /// <param name="address">The destination IP address.</param>
    /// <returns>The round-trip time it took to receive a reply.</returns>
    public static TimeSpan Ping(IPAddress address)
    {
        const byte ip = 20;
        const byte header = 8;

        var requestData = new byte[]
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
            30, 31, 32
        };
        var p = Package.Create(MessageType.EchoRequest, Code.NoCode, 1, 1, requestData);
        using var i = new ICMP4();
        var t = Stopwatch.StartNew();
        i.Send(p, address);
        var r = i.Receive((uint)(ip + header + requestData.Length));
        t.Stop();
        return t.Elapsed;
    }

    /// <summary>
    /// Asynchronously sends an ICMP Echo Request (ping) to the specified address and measures the time it takes to receive a reply.
    /// </summary>
    /// <param name="address">The destination IP address.</param>
    /// <param name="token">The cancellation token to cancel the operation.</param>
    /// <returns>A task representing the round-trip time it took to receive a reply.</returns>
    public static async Task<TimeSpan> PingAsync(IPAddress address, CancellationToken token = default)
    {
        const byte ip = 20;
        const byte header = 8;

        var requestData = new byte[]
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
            30, 31, 32
        };
        var p = Package.Create(MessageType.EchoRequest, Code.NoCode, 1, 1, requestData);
        using var i = new ICMP4();
        var t = Stopwatch.StartNew();
        i.Send(p, address);
        var r = await i.ReceiveAsync((uint)(ip + header + requestData.Length), token);
        t.Stop();
        return t.Elapsed;
    }
}