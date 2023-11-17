using System.Collections.ObjectModel;
using System.Text;
using Yannick.Extensions.EnumExtensions;
using Yannick.Lang.Attribute;

namespace Yannick.Network.Protocol.HTTP;

public sealed class Request
{
    private readonly string FullRequest;
    public readonly IReadOnlyDictionary<string, List<string>> Header;
    public readonly Method Method;
    private readonly Server.HTTP Server;
    private readonly TCP.Server.User User;
    public readonly Version Version;

    internal Request(Server.HTTP server, TCP.Server.User user, string httpMethod, string httpVersion,
        IDictionary<string, List<string>> header,
        string fullReq)
    {
        Server = server;
        User = user;
        Version = Enum.GetValues<Version>()
            .FirstOrDefault(e => e.Attribute<SuffixAttribute>()!.Suffix.Equals(httpVersion), Version.UNKNOWN);
        Method = Enum.Parse<Method>(httpMethod);
        Header = new ReadOnlyDictionary<string, List<string>>(header);
        FullRequest = fullReq;
    }

    public byte[] ReadContent(Encoding encoding)
    {
        var bodyBuilder = new StringBuilder();
        var buffer = new byte[Server.BufferSize];

        if (Header.TryGetValue("transfer-encoding", out var transferEncodingList) &&
            transferEncodingList.Contains("chunked"))
        {
            while (true)
            {
                var bytesRead = User._Receive(buffer);
                bodyBuilder.Append(Server.Encoding.GetString(buffer, 0, bytesRead));

                if (bodyBuilder.ToString().EndsWith("\r\n0\r\n\r\n"))
                {
                    break;
                }
            }
        }
        else if (Header.TryGetValue("content-length", out var contentLengthList))
        {
            var contentLength = int.Parse(contentLengthList[0]);
            var body = FullRequest[(FullRequest.IndexOf("\r\n\r\n", StringComparison.Ordinal) + 4)..];

            while (body.Length < contentLength)
            {
                var bytesRead = User._Receive(buffer);
                if (bytesRead == 0)
                    break;

                bodyBuilder.Append(Server.Encoding.GetString(buffer, 0, bytesRead));
            }
        }

        return encoding.GetBytes(bodyBuilder.ToString());
    }
}