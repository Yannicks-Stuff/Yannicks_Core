namespace Yannick.Network.Protocol.HTTP;

public class Client
{
    public static string GetAndContentAsString(string url, bool useHttpClient = true)
    {
        if (!useHttpClient)
            throw new NotImplementedException();

        using var client = new HttpClient();
        var s = client.GetStringAsync(url);
        s.Wait();
        return s.Result;
    }
}