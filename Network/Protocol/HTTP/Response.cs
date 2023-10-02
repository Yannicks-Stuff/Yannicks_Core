using System.Collections.ObjectModel;
using System.Text;

namespace Yannick.Network.Protocol.HTTP;

public sealed class Response
{
    /// <summary>
    /// Gets the version of the HTTP response.
    /// </summary>
    public Version Version { get; init; } = Version.HTTP_1_1;

    /// <summary>
    /// Gets the status code of the HTTP response.
    /// </summary>
    public Status Status { get; init; }

    public byte[] Content { get; set; } = Array.Empty<byte>();
    public IReadOnlyDictionary<string, List<string>> Header { get; init; } = new Dictionary<string, List<string>>();

    /// <summary>
    /// Converts the HTTP response to a byte array.
    /// </summary>
    public byte[] ToByteArray()
    {
        var headerBuilder = new StringBuilder();
        foreach (var pair in Header)
            headerBuilder.AppendLine($"{pair.Key}: {string.Join(", ", pair.Value)}");

        var combined = Encoding.UTF8.GetBytes(headerBuilder.ToString()).Concat(Content).ToArray();
        return combined;
    }
}