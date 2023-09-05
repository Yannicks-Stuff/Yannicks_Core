namespace Yannick.Network.Protocol.HTTP.Lang.Results;

public sealed class OK : PageResult
{
    public OK(string filePath, string url)
    {
        URL = filePath;
        FilePathURL = url;
    }

    protected override bool Save { get; }
    public override Status Status => Status.OK;
    public override string? URL { get; set; }
    protected override string? FilePathURL { get; }
}