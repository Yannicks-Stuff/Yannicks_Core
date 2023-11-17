namespace Yannick.Crypto.Lang
{
    public interface ISymmetric
    {
        uint DefaultBlockSize { get; }
        uint DefaultKeySize { get; }
        uint[] AllowedKeySize { get; }
        byte[] Key { get; }
    }
}