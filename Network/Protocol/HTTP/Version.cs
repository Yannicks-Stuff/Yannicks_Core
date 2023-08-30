using Yannick.Lang.Attribute;

namespace Yannick.Network.Protocol.HTTP;

/// <summary>
/// Represents the various versions of the HTTP protocol.
/// </summary>
public enum Version
{
    /// <summary>
    /// Represents a unknown value
    /// </summary>
    UNKNOWN = 0,

    /// <summary>
    /// Represents the HTTP/0.9 version, which was the first version of the HTTP protocol.
    /// </summary>
    [Suffix("HTTP/0.9")] HTTP_0_9,

    /// <summary>
    /// Represents the HTTP/1.0 version, which introduced status codes and the HTTP header.
    /// </summary>
    [Suffix("HTTP/1.0")] HTTP_1_0,

    /// <summary>
    /// Represents the HTTP/1.1 version, which added persistent connections, chunked transfer coding, and more.
    /// </summary>
    [Suffix("HTTP/1.1")] HTTP_1_1,

    /// <summary>
    /// Represents the HTTP/2 version, which brought performance improvements, multiplexing, and header compression.
    /// </summary>
    [Suffix("HTTP/2")] HTTP_2,

    /// <summary>
    /// Represents the HTTP/3 version, which uses the QUIC protocol for transport and offers further performance improvements.
    /// </summary>
    [Suffix("HTTP/3")] HTTP_3
}