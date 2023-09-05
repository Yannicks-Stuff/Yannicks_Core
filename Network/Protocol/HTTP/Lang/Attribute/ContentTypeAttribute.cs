namespace Yannick.Network.Protocol.HTTP.Lang.Attribute;

public sealed class ContentTypeAttribute : System.Attribute
{
    public string Formatted { get; init; }
    public string Author { get; init; }
}