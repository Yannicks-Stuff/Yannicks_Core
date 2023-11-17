namespace Yannick.Crypto
{
    public interface ISymmetric
    {
        byte[]? Key { get; }
        byte[]? IV { get; }
        ushort KeyLenght { get; }
        ushort? Published => null;

        byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data);
        byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data);

        byte[]? Decrypt(byte[]? key, byte[]? data) => Decrypt(key, IV, data);
        byte[]? Encrypt(byte[]? key, byte[]? data) => Encrypt(key, IV, data);

        byte[]? Decrypt(byte[]? data) => Decrypt(Key, IV, data);
        byte[]? Encrypt(byte[]? data) => Encrypt(Key, IV, data);

        public interface ISupportKeyRange : ISymmetric
        {
            Range KeyRange { get; }
        }
    }
}