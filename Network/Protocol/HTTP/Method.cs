namespace Yannick.Network.Protocol.HTTP;

/// <summary>
/// Represents HTTP methods.
/// </summary>
public enum Method
{
    /// <summary>
    /// Represents an HTTP GET method.
    /// </summary>
    GET,

    /// <summary>
    /// Represents an HTTP POST method.
    /// </summary>
    POST,

    /// <summary>
    /// Represents an HTTP PUT method.
    /// </summary>
    PUT,

    /// <summary>
    /// Represents an HTTP DELETE method.
    /// </summary>
    DELETE,

    /// <summary>
    /// Represents an HTTP HEAD method.
    /// </summary>
    HEAD,

    /// <summary>
    /// Represents an HTTP OPTIONS method.
    /// </summary>
    OPTIONS,

    /// <summary>
    /// Represents an HTTP PATCH method.
    /// </summary>
    PATCH,

    /// <summary>
    /// Represents an HTTP CONNECT method.
    /// </summary>
    CONNECT,

    /// <summary>
    /// Represents an HTTP TRACE method.
    /// </summary>
    TRACE
}