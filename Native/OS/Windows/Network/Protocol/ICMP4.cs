using System.ComponentModel;
using System.Runtime.InteropServices;
using Yannick.Native.OS.Windows.Win32;
using Yannick.Network;

namespace Yannick.Native.OS.Windows.Network.Protocol;

/// <summary>
/// Provides methods for working with the Internet Control Message Protocol (ICMP) for IPV4.
/// </summary>
public static class ICMP4
{
    /// <summary>
    /// Represents options for IP packets.
    /// </summary>
    public enum Flags : byte
    {
        /// <summary>
        /// This value causes the IP packet to add in an IP routing header with the source.
        /// <remarks>This value is only applicable on Windows Vista and later.</remarks>
        /// </summary>
        AddRoutingHeader = 0x01,

        /// <summary>
        /// This value indicates that the packet should not be fragmented.
        /// </summary>
        NotFragment = 0x2
    }

    /// <summary>
    /// Represents the Type of Service (TOS) options for an ICMP packet.
    /// </summary>
    public enum Tos : byte
    {
        /// <summary>
        /// Indicates low delay for the ICMP packet.
        /// </summary>
        LowDelay = 1,

        /// <summary>
        /// Indicates high throughput for the ICMP packet.
        /// </summary>
        HighThroughput = 2,

        /// <summary>
        /// Indicates high reliability for the ICMP packet.
        /// </summary>
        HighReliability = 4
    }

    /// <summary>
    /// Sends an ICMP echo request (ping) to the specified IP address.
    /// </summary>
    /// <param name="ipAddress">IPv4 address of the destination.</param>
    /// <param name="timeout">The timeout for the echo request.</param>
    /// <returns>Returns the diff</returns>
    /// <exception cref="System.ComponentModel.Win32Exception">Thrown when an error occurs during the echo request.</exception>
    public static TimeSpan SendEchoRequest(IPAddress ipAddress, TimeSpan timeout)
    {
        var requestData = new byte[]
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
            30, 31, 32
        };
        var a = SendEchoRequest(ipAddress, requestData, new Options(), timeout);
        return TimeSpan.FromMilliseconds(a.RoundTripTime);
    }

    /// <summary>
    /// Sends an ICMP echo request to the specified IP address and returns the result.
    /// </summary>
    /// <param name="ipAddress">The IP address of the target host.</param>
    /// <param name="requestData">The data to send in the echo request.</param>
    /// <param name="options">The IP options for the echo request.</param>
    /// <param name="timeout">The timeout for the echo request.</param>
    /// <returns>A <see cref="Result"/> object containing the results of the echo request.</returns>
    /// <exception cref="System.ComponentModel.Win32Exception">Thrown when an error occurs during the echo request.</exception>
    public static Result SendEchoRequest(IPAddress ipAddress, byte[] requestData, Options options, TimeSpan timeout)
    {
        uint destinationAddress = ipAddress;
        var requestSize = (short)requestData.Length;

        var ipOptions = new Iphlpapi.IpOptionInformation
        {
            Ttl = options.Ttl,
            Tos = (byte)options.Tos,
            Flags = (byte)options.Flags,
            OptionsSize = 0,
            OptionsData = IntPtr.Zero
        };

        var replyBuffer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Iphlpapi.IcmpEchoReply)) + requestSize);
        var replySize = (uint)(Marshal.SizeOf(typeof(Iphlpapi.IcmpEchoReply)) + requestSize);

        var icmpHandle = Iphlpapi.IcmpCreateFile();
        if (icmpHandle == IntPtr.Zero)
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        try
        {
            var result = Iphlpapi.IcmpSendEcho(icmpHandle, destinationAddress, requestData, requestSize, ref ipOptions,
                replyBuffer, replySize,
                (uint)timeout.TotalMilliseconds);

            var lastError = Marshal.GetLastWin32Error();
            if (result == 0 && lastError != 0)
            {
                throw new Win32Exception(lastError);
            }

            var echoReply = Marshal.PtrToStructure<Iphlpapi.IcmpEchoReply>(replyBuffer);

            return new Result
            {
                Address = new IPAddress(echoReply.Address),
                Status = echoReply.Status,
                RoundTripTime = echoReply.RoundTripTime,
                Data = new byte[echoReply.DataSize],
                OptionsInformation = echoReply.Options
            };
        }
        finally
        {
            Iphlpapi.IcmpCloseHandle(icmpHandle);
            Marshal.FreeHGlobal(replyBuffer);
        }
    }

    /// <summary>
    /// Represents the options for an Internet Control Message Protocol (ICMP) request.
    /// </summary>
    public readonly struct Options
    {
        public Options()
        {
            Ttl = 128;
            Tos = Tos.LowDelay;
            Flags = Flags.NotFragment;
        }

        /// <summary>
        /// Gets or sets the Time-to-Live (TTL) value for the ICMP request.
        /// The TTL is used to limit the lifespan of a packet in a network.
        /// </summary>
        public byte Ttl { get; init; }

        /// <summary>
        /// Gets or sets the Type of Service (TOS) value for the ICMP request.
        /// The TOS is used to determine the priority of the request.
        /// </summary>
        public Tos Tos { get; init; }

        /// <summary>
        /// Gets or sets the Flags value for the ICMP request.
        /// The Flags can be used to control various aspects of the request,
        /// such as whether or not the packet should be fragmented.
        /// </summary>
        public Flags Flags { get; init; }
    }

    /// <summary>
    /// Represents the result of an Internet Control Message Protocol (ICMP) request, such as a ping.
    /// </summary>
    public readonly struct Result
    {
        /// <summary>
        /// Gets or sets the IP address of the destination host.
        /// </summary>
        public IPAddress Address { get; init; }

        /// <summary>
        /// Gets or sets the status of the ICMP request. This could be a response code or error code
        /// depending on the outcome of the request.
        /// </summary>
        public uint Status { get; init; }

        /// <summary>
        /// Gets or sets the round-trip time, in milliseconds, for the ICMP request. This is the time
        /// taken for the request to reach the destination and for the response to come back.
        /// </summary>
        public uint RoundTripTime { get; init; }

        /// <summary>
        /// Gets or sets the data payload of the ICMP response. This could be the data sent in the request
        /// or additional data provided in the response.
        /// </summary>
        public byte[] Data { get; init; }

        /// <summary>
        /// Gets or sets the options for the ICMP request. This includes options such as the Time-to-Live (TTL)
        /// and the Type of Service (TOS) for the request.
        /// </summary>
        public Iphlpapi.IpOptionInformation OptionsInformation { get; init; }
    }
}