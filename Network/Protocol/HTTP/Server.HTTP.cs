using System.Net.Sockets;
using System.Text;

namespace Yannick.Network.Protocol.HTTP;

public static partial class Server
{
    public class HTTP : TCP.Server
    {
        public int BufferSize = 8192;
        public Encoding Encoding = Encoding.UTF8;
        public int MaxHeaderSize = 65536;


        public HTTP(IPAddress host, ushort port = 80) : base(host, port)
        {
            OnAvailable += OnRawClientRequest;
            OnConnect += OnClientConnect;
        }

        public Version Version { get; init; } = Version.HTTP_1_0;
        public event Action<User>? OnHeaderToBig;
        public event Action<User, SocketException>? OnSocketException;
        public event Action<User, IOException>? OnIOException;
        public event Action<User, Exception>? OnUnknowException;
        public event Action<User, IReadOnlyList<Request>>? OnRequest;

        private void OnClientConnect(User user)
        {
            ((Protocol.HTTP.Server.User)user).IsDecrypted = false;
        }

        private void OnRawClientRequest(User usr)
        {
            var reqL = new List<Request>();
            again:
            var buffer = new byte[BufferSize];
            var requestData = new List<byte>();

            while (true)
            {
                try
                {
                    var bytesRead = usr._Receive(buffer);
                    if (bytesRead == 0)
                    {
                        usr.Dispose();
                        return;
                    }

                    requestData.AddRange(buffer.Take(bytesRead));

                    if (requestData.Count > MaxHeaderSize)
                    {
                        OnHeaderToBig?.Invoke(usr);
                        usr.Dispose();
                        return;
                    }

                    if (requestData.Count > 3 && requestData.Skip(requestData.Count - 4)
                            .SequenceEqual("\r\n\r\n"u8.ToArray()))
                        break;
                }
                catch (SocketException ex)
                {
                    OnSocketException?.Invoke(usr, ex);
                    usr.Dispose();
                    return;
                }
                catch (IOException ex)
                {
                    OnIOException?.Invoke(usr, ex);
                    usr.Dispose();
                    return;
                }
                catch (Exception ex)
                {
                    OnUnknowException?.Invoke(usr, ex);
                    usr.Dispose();
                    return;
                }
            }

            var fullRequest = Encoding.GetString(requestData.ToArray());
            var headerLines = fullRequest[..fullRequest.IndexOf("\r\n\r\n", StringComparison.Ordinal)].Split("\r\n");

            var headers =
                new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            var requestLineParts = headerLines[0].Split(' ');
            var httpMethod = requestLineParts[0];
            var httpVersion = requestLineParts.Length > 2 ? requestLineParts[2] : "HTTP/1.0";

            for (var i = 1; i < headerLines.Length; i++)
            {
                var line = headerLines[i];
                var colonIndex = line.IndexOf(':');
                if (colonIndex == -1) continue;

                var headerName = line[..colonIndex].Trim().ToLower();
                var headerValue = line[(colonIndex + 1)..].Trim();

                if (!headers.TryGetValue(headerName, out var hv))
                {
                    hv = new List<string>();
                    headers[headerName] = hv;
                }

                hv.Add(headerValue);
            }

            reqL.Add(new Request(this, usr, httpMethod, httpVersion, headers, fullRequest));

            var remainingData = requestData.Skip(fullRequest.IndexOf("\r\n\r\n", StringComparison.Ordinal) + 4 +
                                                 (headers.TryGetValue("content-length", out var header)
                                                     ? int.Parse(header[0])
                                                     : 0)).ToArray();
            if (remainingData.Length > 0)
                goto again;

            OnRequest?.Invoke(usr, reqL);
        }
    }
}