using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Yannick.Crypto.Lang.Hash;
using Yannick.Extensions.StringExtensions;

namespace Yannick.Crypto.Lang.Symmetric
{
    public struct Twofish_256 : Crypto.ISymmetric
    {
        public ushort? Published => 1998;
        private IBufferedCipher? DecryptionProvider;
        private IBufferedCipher? EncryptionProvider;

        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 32;

        private void Init()
        {
            if (EncryptionProvider == null)
            {
                Key = (Crypto.Hash.Decrypt<Keccak_512>(Password.Generate(32, 32).ToByteArray(Encoding.UTF8)) ??
                       throw new InvalidOperationException()).Take(KeyLenght).ToArray();
                DecryptionProvider = CipherUtilities.GetCipher(new TwofishEngine().AlgorithmName);
                DecryptionProvider.Init(false, new KeyParameter(Key));
                EncryptionProvider = CipherUtilities.GetCipher(new TwofishEngine().AlgorithmName);
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
            DecryptionProvider = CipherUtilities.GetCipher(new TwofishEngine().AlgorithmName);
            DecryptionProvider.Init(false, new KeyParameter(Key));

            return Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            EncryptionProvider = CipherUtilities.GetCipher(new TwofishEngine().AlgorithmName);
            EncryptionProvider.Init(true, new KeyParameter(Key));
            return Encrypt(data);
        }
    }

    public struct Twofish_192 : Crypto.ISymmetric
    {
        public ushort? Published => 1998;
        private IBufferedCipher? DecryptionProvider;
        private IBufferedCipher? EncryptionProvider;

        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 24;

        private void Init()
        {
            if (EncryptionProvider == null)
            {
                Key = (Crypto.Hash.Decrypt<Keccak_512>(Password.Generate(32, 32).ToByteArray(Encoding.UTF8)) ??
                       throw new InvalidOperationException()).Take(KeyLenght).ToArray();
                DecryptionProvider = CipherUtilities.GetCipher(new TwofishEngine().AlgorithmName);
                DecryptionProvider.Init(false, new KeyParameter(Key));
                EncryptionProvider = CipherUtilities.GetCipher(new TwofishEngine().AlgorithmName);
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
            DecryptionProvider = CipherUtilities.GetCipher(new TwofishEngine().AlgorithmName);
            DecryptionProvider.Init(false, new KeyParameter(Key));

            return Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            EncryptionProvider = CipherUtilities.GetCipher(new TwofishEngine().AlgorithmName);
            EncryptionProvider.Init(true, new KeyParameter(Key));
            return Encrypt(data);
        }
    }

    public struct Twofish_128 : Crypto.ISymmetric
    {
        public ushort? Published => 1998;
        private IBufferedCipher? DecryptionProvider;
        private IBufferedCipher? EncryptionProvider;

        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 16;

        private void Init()
        {
            if (EncryptionProvider == null)
            {
                Key = (Crypto.Hash.Decrypt<Keccak_512>(Password.Generate(32, 32).ToByteArray(Encoding.UTF8)) ??
                       throw new InvalidOperationException()).Take(KeyLenght).ToArray();
                DecryptionProvider = CipherUtilities.GetCipher(new TwofishEngine().AlgorithmName);
                DecryptionProvider.Init(false, new KeyParameter(Key));
                EncryptionProvider = CipherUtilities.GetCipher(new TwofishEngine().AlgorithmName);
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
            DecryptionProvider = CipherUtilities.GetCipher(new TwofishEngine().AlgorithmName);
            DecryptionProvider.Init(false, new KeyParameter(Key));

            return Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            EncryptionProvider = CipherUtilities.GetCipher(new TwofishEngine().AlgorithmName);
            EncryptionProvider.Init(true, new KeyParameter(Key));
            return Encrypt(data);
        }
    }
}