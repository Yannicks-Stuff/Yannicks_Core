using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Yannick.Crypto.Lang.Hash;
using Yannick.Extensions.StringExtensions;

namespace Yannick.Crypto.Lang.Symmetric
{
    public struct Rc2 : Crypto.ISymmetric.ISupportKeyRange
    {
        private IBufferedCipher? DecryptionProvider;
        private IBufferedCipher? EncryptionProvider;

        public Range KeyRange => new Range(1, 1024);
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght { get; private set; }

        public Rc2(ushort keyLenght = 128)
        {
            KeyLenght = keyLenght;

            if (keyLenght < 64)
                Key = (Crypto.Hash.Decrypt<Keccak_512>(Password.Generate(32, 32).ToByteArray(Encoding.UTF8)) ??
                       throw new InvalidOperationException()).Take(KeyLenght).ToArray();
            else
            {
                var list = new List<byte>();
                list.AddRange(Crypto.Hash.Decrypt<Keccak_512>(Password.Generate(32, 32).ToByteArray(Encoding.UTF8)) ??
                              throw new InvalidOperationException());
                list.AddRange((Crypto.Hash.Decrypt<Keccak_512>(Password.Generate(32, 32).ToByteArray(Encoding.UTF8)) ??
                               throw new InvalidOperationException()).Take(KeyLenght - 64));
                Key = list.ToArray();
            }

            DecryptionProvider = CipherUtilities.GetCipher(new RC2Engine().AlgorithmName);
            DecryptionProvider.Init(false, new KeyParameter(Key));
            EncryptionProvider = CipherUtilities.GetCipher(new RC2Engine().AlgorithmName);
            EncryptionProvider.Init(true, new KeyParameter(Key));
            IV = new byte[0];
        }

        private void Init()
        {
            if (EncryptionProvider == null)
            {
                KeyLenght = 128;
                Key = (Crypto.Hash.Decrypt<Keccak_512>(Password.Generate(32, 32).ToByteArray(Encoding.UTF8)) ??
                       throw new InvalidOperationException()).Take(KeyLenght).ToArray();
                DecryptionProvider = CipherUtilities.GetCipher(new RC2Engine().AlgorithmName);
                DecryptionProvider.Init(false, new KeyParameter(Key));
                EncryptionProvider = CipherUtilities.GetCipher(new RC2Engine().AlgorithmName);
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
            DecryptionProvider = CipherUtilities.GetCipher(new RC2Engine().AlgorithmName);
            DecryptionProvider.Init(false, new KeyParameter(Key));

            return Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            EncryptionProvider = CipherUtilities.GetCipher(new RC2Engine().AlgorithmName);
            EncryptionProvider.Init(true, new KeyParameter(Key));
            return Encrypt(data);
        }
    }

    public struct Rc2_1024 : Crypto.ISymmetric
    {
        private Rc2? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 128;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Rc2(128);
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

    public struct Rc2_512 : Crypto.ISymmetric
    {
        private Rc2? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 64;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Rc2(64);
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

    public struct Rc2_256 : Crypto.ISymmetric
    {
        private Rc2? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 32;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Rc2(32);
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

    public struct Rc2_128 : Crypto.ISymmetric
    {
        private Rc2? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 16;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Rc2(16);
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

    public struct Rc2_64 : Crypto.ISymmetric
    {
        private Rc2? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 8;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Rc2(8);
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

    public struct Rc5 : Crypto.ISymmetric.ISupportKeyRange
    {
        private IBufferedCipher? DecryptionProvider;
        private IBufferedCipher? EncryptionProvider;

        public Range KeyRange => new Range(1, 128);
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght { get; private set; }

        public Rc5(ushort keyLenght = 16)
        {
            KeyLenght = keyLenght;


            Key = (Crypto.Hash.Decrypt<Keccak_512>(Password.Generate(32, 32).ToByteArray(Encoding.UTF8)) ??
                   throw new InvalidOperationException()).Take(KeyLenght).ToArray();


            DecryptionProvider = CipherUtilities.GetCipher(new RC564Engine().AlgorithmName);
            DecryptionProvider.Init(false, new KeyParameter(Key));
            EncryptionProvider = CipherUtilities.GetCipher(new RC564Engine().AlgorithmName);
            EncryptionProvider.Init(true, new KeyParameter(Key));
            IV = new byte[0];
        }

        private void Init()
        {
            if (EncryptionProvider == null)
            {
                KeyLenght = 16;
                Key = (Crypto.Hash.Decrypt<Keccak_512>(Password.Generate(32, 32).ToByteArray(Encoding.UTF8)) ??
                       throw new InvalidOperationException()).Take(KeyLenght).ToArray();
                DecryptionProvider = CipherUtilities.GetCipher(new RC564Engine().AlgorithmName);
                DecryptionProvider.Init(false, new KeyParameter(Key));
                EncryptionProvider = CipherUtilities.GetCipher(new RC564Engine().AlgorithmName);
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
            DecryptionProvider = CipherUtilities.GetCipher(new RC564Engine().AlgorithmName);
            DecryptionProvider.Init(false, new KeyParameter(Key));

            return Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            EncryptionProvider = CipherUtilities.GetCipher(new RC564Engine().AlgorithmName);
            EncryptionProvider.Init(true, new KeyParameter(Key));
            return Encrypt(data);
        }
    }

    public struct Rc5_128 : Crypto.ISymmetric
    {
        private Rc5? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 16;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Rc5(16);
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

    public struct Rc5_64 : Crypto.ISymmetric
    {
        private Rc5? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 8;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Rc5(8);
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

    public struct Rc6 : Crypto.ISymmetric.ISupportKeyRange
    {
        private IBufferedCipher? DecryptionProvider;
        private IBufferedCipher? EncryptionProvider;

        public Range KeyRange => new Range(1, 256);
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght { get; private set; }

        public Rc6(ushort keyLenght = 32)
        {
            KeyLenght = keyLenght;


            Key = (Crypto.Hash.Decrypt<Keccak_512>(Password.Generate(32, 32).ToByteArray(Encoding.UTF8)) ??
                   throw new InvalidOperationException()).Take(KeyLenght).ToArray();


            DecryptionProvider = CipherUtilities.GetCipher(new RC6Engine().AlgorithmName);
            DecryptionProvider.Init(false, new KeyParameter(Key));
            EncryptionProvider = CipherUtilities.GetCipher(new RC6Engine().AlgorithmName);
            EncryptionProvider.Init(true, new KeyParameter(Key));
            IV = new byte[0];
        }

        private void Init()
        {
            if (EncryptionProvider == null)
            {
                KeyLenght = 32;
                Key = (Crypto.Hash.Decrypt<Keccak_512>(Password.Generate(32, 32).ToByteArray(Encoding.UTF8)) ??
                       throw new InvalidOperationException()).Take(KeyLenght).ToArray();
                DecryptionProvider = CipherUtilities.GetCipher(new RC6Engine().AlgorithmName);
                DecryptionProvider.Init(false, new KeyParameter(Key));
                EncryptionProvider = CipherUtilities.GetCipher(new RC6Engine().AlgorithmName);
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
            DecryptionProvider = CipherUtilities.GetCipher(new RC6Engine().AlgorithmName);
            DecryptionProvider.Init(false, new KeyParameter(Key));

            return Decrypt(data);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            Init();
            IV = iv;
            Key = key;
            EncryptionProvider = CipherUtilities.GetCipher(new RC6Engine().AlgorithmName);
            EncryptionProvider.Init(true, new KeyParameter(Key));
            return Encrypt(data);
        }
    }

    public struct Rc6_256 : Crypto.ISymmetric
    {
        private Rc6? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 32;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Rc6(32);
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

    public struct Rc6_128 : Crypto.ISymmetric
    {
        private Rc6? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 16;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Rc6(16);
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

    public struct Rc6_64 : Crypto.ISymmetric
    {
        private Rc6? Provider;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 8;

        private void Init()
        {
            if (Provider == null)
            {
                Provider = new Rc6(8);
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