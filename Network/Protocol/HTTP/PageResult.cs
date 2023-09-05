namespace Yannick.Network.Protocol.HTTP;

public abstract class PageResult
{
    private Dictionary<string, byte[]> _content = new();

    protected abstract bool Save { get; }

    public abstract Status Status { get; }
    public abstract string? URL { get; set; }
    protected abstract string? FilePathURL { get; }


    protected virtual Response ToResponse(Version version)
    {
        var data = Array.Empty<byte>();
        if (FilePathURL != null)
            if (Save && _content.ContainsKey(FilePathURL!))
                data = _content[FilePathURL];
            else if (File.Exists(FilePathURL))
                data = File.ReadAllBytes(FilePathURL);

        return new Response
        {
            Version = version,
            Status = Status,
            Header = new Dictionary<string, List<string>>
            {
            },
            Content = data
        };
    }
}