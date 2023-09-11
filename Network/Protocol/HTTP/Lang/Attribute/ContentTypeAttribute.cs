namespace Yannick.Network.Protocol.HTTP.Lang.Attribute;

public sealed class ContentTypeAttribute : System.Attribute
{
    public required string Formatted { get; init; }
    public required string Author { get; init; }
}