using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;

namespace Yannick.Network;

/// <summary>
/// wrapper class for <see cref="System.Net.IPAddress"/>
/// </summary>
public sealed class IPAddress : IEquatable<IPAddress>
{
    private readonly System.Net.IPAddress _ipAddress;

    /// <summary>
    /// Initializes a new instance of the <see cref="IPAddress"/> class with the specified <see cref="System.Net.IPAddress"/>.
    /// </summary>
    /// <param name="ipAddress">The underlying <see cref="System.Net.IPAddress"/> to wrap.</param>
    public IPAddress(System.Net.IPAddress ipAddress)
    {
        _ipAddress = ipAddress;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref='System.Net.IPAddress'/>
    ///     class with the specified address.
    /// </summary>
    /// <param name="newAddress">newAddress – The long value of the IP address. For example,
    /// the value 0x2414188f in big-endian format would be the IP address "143.24.20.36".</param>
    /// <exception cref="ArgumentOutOfRangeException">– newAddress  0 or newAddress  0x00000000FFFFFFFF</exception>
    public IPAddress(long newAddress) : this(new System.Net.IPAddress(newAddress))
    {
    }

    /// <summary>
    /// Provides the IP address of the loopback interface.
    /// </summary>
    public static IPAddress Loopback => System.Net.IPAddress.Loopback;

    /// <summary>
    /// Provides the IP address of the broadcast address.
    /// </summary>
    public static IPAddress Broadcast => System.Net.IPAddress.Broadcast;

    /// <summary>
    /// Provides the IP address indicating that no network interface should be used.
    /// </summary>
    public static IPAddress None => System.Net.IPAddress.None;

    /// <summary>
    /// Provides the IP address indicating that the server must listen for client activity on all network interfaces for IPv6.
    /// </summary>
    public static IPAddress IPv6Any => System.Net.IPAddress.IPv6Any;

    /// <summary>
    /// Provides the IP address of the loopback interface for IPv6.
    /// </summary>
    public static IPAddress IPv6Loopback => System.Net.IPAddress.IPv6Loopback;

    /// <summary>
    /// Provides the IP address indicating that no network interface should be used for IPv6.
    /// </summary>
    public static IPAddress IPv6None => System.Net.IPAddress.IPv6None;

    /// <summary>
    /// Gets the address family of the IP address.
    /// </summary>
    public AddressFamily AddressFamily => _ipAddress.AddressFamily;

    /// <summary>
    /// IPv6 Scope identifier. This is really a uint32, but that isn't CLS compliant
    /// </summary>
    public long ScopeId => _ipAddress.ScopeId;

    /// <summary>
    /// Determines if an address is an IPv6 Multicast address
    /// </summary>
    public bool IsIPv6Multicast => _ipAddress.IsIPv6Multicast;

    /// <summary>
    /// Determines if an address is an IPv6 Link Local address
    /// </summary>
    public bool IsIPv6LinkLocal => _ipAddress.IsIPv6LinkLocal;

    /// <summary>
    /// Determines if an address is an IPv6 Site Local address
    /// </summary>
    public bool IsIPv6SiteLocal => _ipAddress.IsIPv6SiteLocal;

    /// <summary>
    /// Gets whether the address is an IPv6 Teredo address.
    /// <returns>true if the IP address is an IPv6 Teredo address; otherwise, false.</returns>
    /// </summary>
    public bool IsIPv6Teredo => _ipAddress.IsIPv6Teredo;

    /// <summary>
    /// Gets whether the address is an IPv6 Unique Local address
    /// </summary>
    public bool IsIPv6UniqueLocal => _ipAddress.IsIPv6UniqueLocal;

    /// <summary>
    /// Gets whether the IP address is an IPv4-mapped IPv6 address.
    /// <returns>Returns Boolean. true if the IP address is an IPv4-mapped IPv6 address; otherwise, false.</returns>
    /// </summary>
    public bool IsIPv4MappedToIPv6 => _ipAddress.IsIPv4MappedToIPv6;

    public bool Equals(IPAddress? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || _ipAddress.Equals(other._ipAddress);
    }

    /// <summary>
    /// Provides a copy of the IPAddress internals as an array of bytes.
    /// </summary>
    /// <returns>Include the internals copy of the IPAddress array of bytes</returns>
    public byte[] GetAddressBytes() => _ipAddress.GetAddressBytes();

    /// <summary>
    /// Converts an IP address from its host byte order representation to its network byte order representation.
    /// </summary>
    /// <param name="host">A 16-bit signed integer.</param>
    /// <returns>A 16-bit signed integer.</returns>
    public static short HostToNetworkOrder(short host) => System.Net.IPAddress.HostToNetworkOrder(host);

    /// <summary>
    /// Converts an IP address from its host byte order representation to its network byte order representation.
    /// </summary>
    /// <param name="host">A 32-bit signed integer.</param>
    /// <returns>A 32-bit signed integer.</returns>
    public static int HostToNetworkOrder(int host) => System.Net.IPAddress.HostToNetworkOrder(host);

    /// <summary>
    /// Converts an IP address from its host byte order representation to its network byte order representation.
    /// </summary>
    /// <param name="host">A 64-bit signed integer.</param>
    /// <returns>A 64-bit signed integer.</returns>
    public static long HostToNetworkOrder(long host) => System.Net.IPAddress.HostToNetworkOrder(host);

    /// <summary>
    /// Converts an IP address from its network byte order representation to its host byte order representation.
    /// </summary>
    /// <param name="host">A 16-bit signed integer.</param>
    /// <returns>A 16-bit signed integer.</returns>
    public static short NetworkToHostOrder(short host) => System.Net.IPAddress.NetworkToHostOrder(host);

    /// <summary>
    /// Converts an IP address from its network byte order representation to its host byte order representation.
    /// </summary>
    /// <param name="host">A 32-bit signed integer.</param>
    /// <returns>A 32-bit signed integer.</returns>
    public static int NetworkToHostOrder(int host) => System.Net.IPAddress.NetworkToHostOrder(host);

    /// <summary>
    /// Converts an IP address from its network byte order representation to its host byte order representation.
    /// </summary>
    /// <param name="host">A 64-bit signed integer.</param>
    /// <returns>A 64-bit signed integer.</returns>
    public static long NetworkToHostOrder(long host) => System.Net.IPAddress.NetworkToHostOrder(host);

    /// <summary>
    /// Indicates whether the specified IP address is the loopback address.
    /// </summary>
    /// <param name="address">An IP address.</param>
    /// <returns>true if <paramref name="address"/> is the loopback address; otherwise, false.</returns>
    public static bool IsLoopback(IPAddress address) => System.Net.IPAddress.IsLoopback(address._ipAddress);

    /// <summary>
    /// Determines whether the specified string is a valid IP address and converts it to an <see cref="IPAddress"/> object if it is valid.
    /// </summary>
    /// <param name="ipString">A string that represents an IP address.</param>
    /// <param name="address">When this method returns, contains the <see cref="IPAddress"/> object represented by <paramref name="ipString"/>, if the conversion succeeded, or the value of <see cref="None"/> if the conversion failed.</param>
    /// <returns>true if <paramref name="ipString"/> was able to be parsed into an <see cref="IPAddress"/>; otherwise, false.</returns>
    public static bool TryParse(string ipString, out IPAddress address)
    {
        var rs = System.Net.IPAddress.TryParse(ipString, out var adr);
        address = adr ?? None;
        return rs;
    }

    /// <summary>
    /// Determines whether the specified character span is a valid IP address and converts it to an <see cref="IPAddress"/> object if it is valid.
    /// </summary>
    /// <param name="ipSpan">A character span that represents an IP address.</param>
    /// <param name="address">When this method returns, contains the <see cref="IPAddress"/> object represented by <paramref name="ipSpan"/>, if the conversion succeeded, or the value of <see cref="None"/> if the conversion failed.</param>
    /// <returns>true if <paramref name="ipSpan"/> was able to be parsed into an <see cref="IPAddress"/>; otherwise, false.</returns>
    public static bool TryParse(ReadOnlySpan<char> ipSpan, out IPAddress address)
    {
        var rs = System.Net.IPAddress.TryParse(ipSpan, out var adr);
        address = adr ?? None;
        return rs;
    }

    /// <summary>
    /// Converts a character span that represents an IP address to an <see cref="IPAddress"/> object.
    /// </summary>
    /// <param name="ipSpan">A character span that represents an IP address.</param>
    /// <returns>An <see cref="IPAddress"/> object.</returns>
    public static IPAddress Parse(ReadOnlySpan<char> ipSpan) => new(System.Net.IPAddress.Parse(ipSpan));

    /// <summary>
    /// Converts a string that represents an IP address to an <see cref="IPAddress"/> object.
    /// </summary>
    /// <param name="ipString">A string that represents an IP address.</param>
    /// <returns>An <see cref="IPAddress"/> object.</returns>
    public static IPAddress Parse(string ipString) => new(System.Net.IPAddress.Parse(ipString));

    /// <summary>
    /// Defines an implicit conversion of an <see cref="System.Net.IPAddress"/> instance to an <see cref="IPAddress"/> instance.
    /// </summary>
    public static implicit operator IPAddress(System.Net.IPAddress ip) => new(ip);

    /// <summary>
    /// Defines an implicit conversion of an <see cref="IPAddress"/> instance to a byte array.
    /// </summary>
    public static implicit operator byte[](IPAddress ip) => ip._ipAddress.GetAddressBytes();

    /// <summary>
    /// Defines an implicit conversion of an <see cref="IPAddress"/> instance to a 32-bit unsigned integer.
    /// </summary>
    public static implicit operator uint(IPAddress ip) =>
        ip._ipAddress.AddressFamily != AddressFamily.InterNetwork
            ? throw new ArgumentOutOfRangeException(nameof(ip), "Only IPV4 is support to unsigned int32")
            : BitConverter.ToUInt32(ip._ipAddress.GetAddressBytes(), 0);


    /// <summary>
    /// Defines an implicit conversion of a string to an <see cref="IPAddress"/> instance.
    /// </summary>
    public static implicit operator IPAddress(string adr) => Parse(adr);

    /// <summary>
    /// Defines an implicit conversion of a uint to an <see cref="IPAddress"/> IPV4 instance.
    /// </summary>
    public static implicit operator IPAddress(uint adr) => new IPAddress(adr);

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is IPAddress other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _ipAddress.GetHashCode();
    }

    public override string ToString() => _ipAddress.ToString();
}