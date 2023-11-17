using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Yannick.Crypto.Lang.Hash;
using Yannick.Extensions.StringExtensions;

namespace Yannick.Crypto.Lang.Symmetric
{
    public struct Cast5 : Crypto.ISymmetric.ISupportKeyRange
    {
        private IBufferedCipher? DecryptionProvider;
        private IBufferedCipher? EncryptionProvider;

        public Range KeyRange => new Range(40, 128);
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght { get; private set; }

        public Cast5(ushort keyLenght = 16)
        {
            KeyLenght = keyLenght;
            Key = (Crypto.Hash.Decrypt<Keccak_512>(Password.Generate(32, 32).ToByteArray(Encoding.UTF8)) ??
                   throw new InvalidOperationException()).Take(KeyLenght).ToArray();
            DecryptionProvider = CipherUtilities.GetCipher(new Cast5Engine().AlgorithmName);
            DecryptionProvider.Init(false, new KeyParameter(Key));
            EncryptionProvider = CipherUtilities.GetCipher(new Cast5Engine().AlgorithmName);
            EncryptionProvider.Init(true, new KeyParameter(Key));
            IV = Array.Empty<byte>();
        }

        private void Init()
        {
            if (EncryptionProvider == null)
            {
                KeyLenght = 16;
                Key = (Crypto.Hash.Decrypt<Keccak_512>(Password.Generate(32, 32).ToByteArray(Encoding.UTF8)) ??
                       throw new InvalidOperationException()).Take(KeyLenght).ToArray();
                DecryptionProvider = CipherUtilities.GetCipher(new Cast5Engine().AlgorithmName);
                DecryptionProvider.Init(false, new KeyParameter(Key));
                EncryptionProvider = CipherUtilities.GetCipher(new Cast5Engine().AlgorithmName);
                EncryptionProvider.Init(true, new KeyParameter(Key));
            }
        }

        public byte[]? Decrypt(byte[]? data)
        {
            Init();
            return DecryptionProvider!.DoFinal(data);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            Init();
            return EncryptionProvider!.DoFinal(data);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            DecryptionProvider = CipherUtilities.GetCipher(new Cast5Engine().AlgorithmName);
            DecryptionProvider.Init(false, new KeyParameter(Key));

            return Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            EncryptionProvider = CipherUtilities.GetCipher(new Cast5Engine().AlgorithmName);
            EncryptionProvider.Init(true, new KeyParameter(Key));
            return Encrypt(data);
        }
    }

    public struct Cast5_40 : Crypto.ISymmetric
    {
        private Cast5? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 5;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Cast5(5);
            }
        }

        public byte[]? Decrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Encrypt(data);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Encrypt(data);
        }
    }

    public struct Cast5_64 : Crypto.ISymmetric
    {
        private Cast5? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 8;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Cast5(8);
            }
        }

        public byte[]? Decrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Encrypt(data);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Encrypt(data);
        }
    }

    public struct Cast5_80 : Crypto.ISymmetric
    {
        private Cast5? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 10;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Cast5(10);
            }
        }

        public byte[]? Decrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Encrypt(data);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Encrypt(data);
        }
    }

    public struct Cast5_128 : Crypto.ISymmetric
    {
        private Cast5? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 16;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Cast5(16);
            }
        }

        public byte[]? Decrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Encrypt(data);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Encrypt(data);
        }
    }

    public struct Cast6 : Crypto.ISymmetric.ISupportKeyRange
    {
        private IBufferedCipher? DecryptionProvider;
        private IBufferedCipher? EncryptionProvider;

        public Range KeyRange => new Range(128, 256);
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght { get; private set; }

        public Cast6(ushort keyLenght = 32)
        {
            KeyLenght = keyLenght;
            Key = (Crypto.Hash.Decrypt<Keccak_512>(Password.Generate(32, 32).ToByteArray(Encoding.UTF8)) ??
                   throw new InvalidOperationException()).Take(KeyLenght).ToArray();
            DecryptionProvider = CipherUtilities.GetCipher(new Cast6Engine().AlgorithmName);
            DecryptionProvider.Init(false, new KeyParameter(Key));
            EncryptionProvider = CipherUtilities.GetCipher(new Cast6Engine().AlgorithmName);
            EncryptionProvider.Init(true, new KeyParameter(Key));
            IV = Array.Empty<byte>();
        }

        private void Init()
        {
            if (EncryptionProvider == null)
            {
                KeyLenght = 32;
                Key = (Crypto.Hash.Decrypt<Keccak_512>(Password.Generate(32, 32).ToByteArray(Encoding.UTF8)) ??
                       throw new InvalidOperationException()).Take(KeyLenght).ToArray();
                DecryptionProvider = CipherUtilities.GetCipher(new Cast6Engine().AlgorithmName);
                DecryptionProvider.Init(false, new KeyParameter(Key));
                EncryptionProvider = CipherUtilities.GetCipher(new Cast6Engine().AlgorithmName);
                EncryptionProvider.Init(true, new KeyParameter(Key));
            }
        }

        public byte[]? Decrypt(byte[]? data)
        {
            Init();
            return DecryptionProvider!.DoFinal(data);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            Init();
            return EncryptionProvider!.DoFinal(data);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            DecryptionProvider = CipherUtilities.GetCipher(new Cast6Engine().AlgorithmName);
            DecryptionProvider.Init(false, new KeyParameter(Key));

            return Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            EncryptionProvider = CipherUtilities.GetCipher(new Cast6Engine().AlgorithmName);
            EncryptionProvider.Init(true, new KeyParameter(Key));
            return Encrypt(data);
        }
    }

    public struct Cast6_128 : Crypto.ISymmetric
    {
        private Cast6? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 16;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Cast6(16);
            }
        }

        public byte[]? Decrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Encrypt(data);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Encrypt(data);
        }
    }

    public struct Cast6_168 : Crypto.ISymmetric
    {
        private Cast6? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 21;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Cast6(21);
            }
        }

        public byte[]? Decrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Encrypt(data);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Encrypt(data);
        }
    }

    public struct Cast6_192 : Crypto.ISymmetric
    {
        private Cast6? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 24;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Cast6(24);
            }
        }

        public byte[]? Decrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Encrypt(data);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Encrypt(data);
        }
    }

    public struct Cast6_224 : Crypto.ISymmetric
    {
        private Cast6? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 28;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Cast6(28);
            }
        }

        public byte[]? Decrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Encrypt(data);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Encrypt(data);
        }
    }

    public struct Cast6_256 : Crypto.ISymmetric
    {
        private Cast6? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 32;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Cast6(32);
            }
        }

        public byte[]? Decrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            Init();
            return Provider!.Value.Encrypt(data);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            return Provider!.Value.Encrypt(data);
        }
    }
}