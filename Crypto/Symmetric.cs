using System.Text;

namespace Yannick.Crypto
{
    public struct Symmetric<T> where T : ISymmetric
    {
        private readonly T Instance;
        private readonly byte[] IV;
        private byte[] Key;

        public Symmetric(byte[] key) : this()
        {
            Key = key;
            Instance = Activator.CreateInstance<T>();
        }

        public void GeneratePasswort<H>(Encoding? encoding = null) where H : IHash
        {
            var ins = Activator.CreateInstance<H>();
            var pw = (encoding ?? Encoding.UTF8).GetBytes(Password.Generate(Instance.KeyLenght * 8,
                Instance.KeyLenght * 8 / 2));
            var l = new List<byte>();
            again:
            if (l.Count < pw.Length)
            {
                l.AddRange(ins.Decrypt(pw));
                goto again;
            }

            Key = l.ToArray().Take(Instance.KeyLenght).ToArray();
        }

        public byte[]? Decrypt(byte[]? key, byte[]? data)
            => Instance.Decrypt(key, data);

        public byte[]? Encrypt(byte[]? key, byte[]? data)
            => Instance.Encrypt(key, data);

        public byte[]? Decrypt(byte[]? data) => Instance.Decrypt(data);
        public byte[]? Encrypt(byte[]? data) => Instance.Encrypt(data);
    }

    public static class Symmetric
    {
        public static byte[]? Decrypt<T>(byte[]? key, byte[]? data) where T : ISymmetric
            => Activator.CreateInstance<T>().Decrypt(key, data);

        public static byte[]? Encrypt<T>(byte[]? key, byte[]? data) where T : ISymmetric
            => Activator.CreateInstance<T>().Encrypt(key, data);

        public static byte[]? Decrypt<T, H>(string key, byte[]? data, Encoding? keyEncoding = null)
            where T : ISymmetric where H : IHash
            => Activator.CreateInstance<T>()
                .Decrypt(Hash.Decrypt<H>((keyEncoding ?? Encoding.UTF8).GetBytes(key)), data);

        public static byte[]? Encrypt<T, H>(string key, byte[]? data, Encoding? keyEncoding = null)
            where T : ISymmetric where H : IHash
            => Activator.CreateInstance<T>()
                .Encrypt(Hash.Decrypt<H>((keyEncoding ?? Encoding.UTF8).GetBytes(key)), data);
    }
}