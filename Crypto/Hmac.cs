using Yannick.Crypto.SharpHash.Base;

namespace Yannick.Crypto
{
    public struct Hmac<T> where T : IHash
    {
        private SharpHash.Interfaces.IHash Hash;
        public ushort HashSize { get; }

        public Hmac(byte[]? key) : this()
        {
            var a = Activator.CreateInstance<T>();
            Hash = HMACNotBuildInAdapter.CreateHMAC(a.Hash, key);
            HashSize = a.HashSize;
        }

        public byte[]? Decrypt(byte[]? data)
            => Hash.ComputeBytes(data).GetBytes();

        public byte[]? Decrypt(byte[]? key, byte[]? data)
            => new Hmac<T>(key).Decrypt(data);
    }

    public static class Hmac
    {
        public static byte[]? Decrypt<T>(byte[]? key, byte[]? data) where T : IHash
            => new Hmac<T>(key).Decrypt(data);
    }
}