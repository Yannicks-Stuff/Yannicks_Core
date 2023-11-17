namespace Yannick.Crypto
{
    public interface IAsymmetric
    {
        ushort? Published => null;

        byte[] PublicKey { get; }
        byte[] PrivateKey { get; }

        void ImportPublicKey(byte[] key);
        void ImportPrivateKey(byte[] key);
        byte[] Decrypt(byte[] data);
        byte[] Encrypt(byte[] data);
    }
}