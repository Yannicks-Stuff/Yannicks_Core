using System.Security.Cryptography;

namespace Yannick.Crypto.Lang.Symmetric
{
    public struct AES_CBC_128 : Crypto.ISymmetric, IDisposable
    {
        private ICryptoTransform Decryptor;
        private ICryptoTransform Encryptor;
        private Aes? Provider;

        public ushort? Published => 1998;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 16;


        public byte[]? Decrypt(byte[]? data)
        {
            if (data == null)
                return null;
            Init();
            return Decryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            if (data == null)
                return null;
            Init();
            return Encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            if (data == null)
                return null;
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            Decryptor = Provider.CreateDecryptor();
            return Decryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            if (data == null || key == null || iv == null)
                return null;
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            Encryptor = Provider.CreateEncryptor();
            return Encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public void Init()
        {
            if (Provider == null)
            {
                Provider = Aes.Create();
                Provider.Mode = CipherMode.CBC;
                Provider.KeySize = 128;
                IV = Provider.IV;
                Key = Provider.Key;
                Decryptor = Provider.CreateDecryptor();
                Encryptor = Provider.CreateEncryptor();
            }
        }


        public void Dispose()
        {
            Provider?.Dispose();
            Decryptor?.Dispose();
            Encryptor?.Dispose();
        }
    }

    public struct AES_CBC_192 : Crypto.ISymmetric, IDisposable
    {
        private ICryptoTransform Decryptor;
        private ICryptoTransform Encryptor;
        private Aes? Provider;

        public ushort? Published => 1998;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 24;


        public byte[]? Decrypt(byte[]? data)
        {
            if (data == null)
                return null;
            Init();
            return Decryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            if (data == null)
                return null;
            Init();
            return Encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            if (data == null || key == null || iv == null)
                return null;
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            Decryptor = Provider.CreateDecryptor();
            return Decryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            if (data == null || key == null || iv == null)
                return null;
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            Encryptor = Provider.CreateEncryptor();
            return Encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public void Init()
        {
            if (Provider == null)
            {
                Provider = Aes.Create();
                Provider.Mode = CipherMode.CBC;
                Provider.KeySize = 192;
                IV = Provider.IV;
                Key = Provider.Key;
                Decryptor = Provider.CreateDecryptor();
                Encryptor = Provider.CreateEncryptor();
            }
        }


        public void Dispose()
        {
            Provider?.Dispose();
            Decryptor?.Dispose();
            Encryptor?.Dispose();
        }
    }

    public struct AES_CBC_256 : Crypto.ISymmetric, IDisposable
    {
        private ICryptoTransform Decryptor;
        private ICryptoTransform Encryptor;
        private Aes? Provider;

        public ushort? Published => 1998;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 32;


        public byte[]? Decrypt(byte[]? data)
        {
            if (data == null)
                return null;
            Init();
            return Decryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            if (data == null)
                return null;
            Init();
            return Encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            if (data == null || key == null || iv == null)
                return null;
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            Decryptor = Provider.CreateDecryptor();
            return Decryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            if (data == null || key == null || iv == null)
                return null;
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            Encryptor = Provider.CreateEncryptor();
            return Encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public void Init()
        {
            if (Provider == null)
            {
                Provider = Aes.Create();
                Provider.Mode = CipherMode.CBC;
                Provider.KeySize = 256;
                IV = Provider.IV;
                Key = Provider.Key;
                Decryptor = Provider.CreateDecryptor();
                Encryptor = Provider.CreateEncryptor();
            }
        }


        public void Dispose()
        {
            Provider?.Dispose();
            Decryptor?.Dispose();
            Encryptor?.Dispose();
        }
    }

    /*
    public struct AES_CFB_128 : ISymmetric, IDisposable
    {
        private Aes? Provider;
        private ICryptoTransform PublicProvider;
        private ICryptoTransform PrivateProvider;
        public ushort KeyLenght => 16;

        public void Init()
        {
            if (Provider == null)
            {
                Provider = Aes.Create();
                Provider.Mode = CipherMode.CFB;
                Provider.BlockSize = KeyLenght;
                PublicProvider = Provider.CreateEncryptor();
                PrivateProvider = Provider.CreateDecryptor();
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            Init();
            return PrivateProvider.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Encrypt(byte[] data)
        {
            Init();
            return PublicProvider.TransformFinalBlock(data, 0, data.Length);
        }

        public void Dispose()
        {
            Provider?.Dispose();
            PublicProvider?.Dispose();
            PrivateProvider?.Dispose();
        }
    }
    public struct AES_CFB_192 : ISymmetric, IDisposable
    {
        private Aes? Provider;
        private ICryptoTransform PublicProvider;
        private ICryptoTransform PrivateProvider;
        public ushort KeyLenght => 24;

        public void Init()
        {
            if (Provider == null)
            {
                Provider = Aes.Create();
                Provider.Mode = CipherMode.CFB;
                Provider.BlockSize = KeyLenght;
                PublicProvider = Provider.CreateEncryptor();
                PrivateProvider = Provider.CreateDecryptor();
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            Init();
            return PrivateProvider.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Encrypt(byte[] data)
        {
            Init();
            return PublicProvider.TransformFinalBlock(data, 0, data.Length);
        }

        public void Dispose()
        {
            Provider?.Dispose();
            PublicProvider?.Dispose();
            PrivateProvider?.Dispose();
        }
    }
    public struct AES_CFB_256 : ISymmetric, IDisposable
    {
        private Aes? Provider;
        private ICryptoTransform PublicProvider;
        private ICryptoTransform PrivateProvider;
        public ushort KeyLenght => 32;

        public void Init()
        {
            if (Provider == null)
            {
                Provider = Aes.Create();
                Provider.Mode = CipherMode.CFB;
                Provider.BlockSize = KeyLenght;
                PublicProvider = Provider.CreateEncryptor();
                PrivateProvider = Provider.CreateDecryptor();
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            Init();
            return PrivateProvider.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Encrypt(byte[] data)
        {
            Init();
            return PublicProvider.TransformFinalBlock(data, 0, data.Length);
        }

        public void Dispose()
        {
            Provider?.Dispose();
            PublicProvider?.Dispose();
            PrivateProvider?.Dispose();
        }
    }
    */
    /*
    public struct AES_CTS_128 : ISymmetric, IDisposable
    {
        private Aes? Provider;
        private ICryptoTransform PublicProvider;
        private ICryptoTransform PrivateProvider;
        public ushort KeyLenght => 16;

        public void Init()
        {
            if (Provider == null)
            {
                Provider = Aes.Create();
                Provider.Mode = CipherMode.CTS;
                Provider.BlockSize = KeyLenght;
                PublicProvider = Provider.CreateEncryptor();
                PrivateProvider = Provider.CreateDecryptor();
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            Init();
            return PrivateProvider.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Encrypt(byte[] data)
        {
            Init();
            return PublicProvider.TransformFinalBlock(data, 0, data.Length);
        }

        public void Dispose()
        {
            Provider?.Dispose();
            PublicProvider?.Dispose();
            PrivateProvider?.Dispose();
        }
    }
    public struct AES_CTS_192 : ISymmetric, IDisposable
    {
        private Aes? Provider;
        private ICryptoTransform PublicProvider;
        private ICryptoTransform PrivateProvider;
        public ushort KeyLenght => 24;

        public void Init()
        {
            if (Provider == null)
            {
                Provider = Aes.Create();
                Provider.Mode = CipherMode.CTS;
                Provider.BlockSize = KeyLenght;
                PublicProvider = Provider.CreateEncryptor();
                PrivateProvider = Provider.CreateDecryptor();
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            Init();
            return PrivateProvider.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Encrypt(byte[] data)
        {
            Init();
            return PublicProvider.TransformFinalBlock(data, 0, data.Length);
        }

        public void Dispose()
        {
            Provider?.Dispose();
            PublicProvider?.Dispose();
            PrivateProvider?.Dispose();
        }
    }
    public struct AES_CTS_256 : ISymmetric, IDisposable
    {
        private Aes? Provider;
        private ICryptoTransform PublicProvider;
        private ICryptoTransform PrivateProvider;
        public ushort KeyLenght => 32;

        public void Init()
        {
            if (Provider == null)
            {
                Provider = Aes.Create();
                Provider.Mode = CipherMode.CTS;
                Provider.BlockSize = KeyLenght;
                PublicProvider = Provider.CreateEncryptor();
                PrivateProvider = Provider.CreateDecryptor();
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            Init();
            return PrivateProvider.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Encrypt(byte[] data)
        {
            Init();
            return PublicProvider.TransformFinalBlock(data, 0, data.Length);
        }

        public void Dispose()
        {
            Provider?.Dispose();
            PublicProvider?.Dispose();
            PrivateProvider?.Dispose();
        }
    }
    */

    public struct AES_ECB_128 : Crypto.ISymmetric, IDisposable
    {
        private ICryptoTransform Decryptor;
        private ICryptoTransform Encryptor;
        private Aes? Provider;

        public ushort? Published => 1998;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 16;


        public byte[]? Decrypt(byte[]? data)
        {
            if (data == null)
                return null;
            Init();
            return Decryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            if (data == null)
                return null;
            Init();
            return Encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            if (data == null || key == null || iv == null)
                return null;
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            Decryptor = Provider.CreateDecryptor();
            return Decryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            if (data == null || key == null || iv == null)
                return null;
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            Encryptor = Provider.CreateEncryptor();
            return Encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public void Init()
        {
            if (Provider == null)
            {
                Provider = Aes.Create();
                Provider.Mode = CipherMode.ECB;
                Provider.KeySize = 128;
                IV = Provider.IV;
                Key = Provider.Key;
                Decryptor = Provider.CreateDecryptor();
                Encryptor = Provider.CreateEncryptor();
            }
        }


        public void Dispose()
        {
            Provider?.Dispose();
            Decryptor?.Dispose();
            Encryptor?.Dispose();
        }
    }

    public struct AES_ECB_192 : Crypto.ISymmetric, IDisposable
    {
        private ICryptoTransform Decryptor;
        private ICryptoTransform Encryptor;
        private Aes? Provider;

        public ushort? Published => 1998;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 24;


        public byte[]? Decrypt(byte[]? data)
        {
            if (data == null)
                return null;
            Init();
            return Decryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            if (data == null)
                return null;
            Init();
            return Encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            if (data == null || key == null || iv == null)
                return null;
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            Decryptor = Provider.CreateDecryptor();
            return Decryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            if (data == null || key == null || iv == null)
                return null;
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            Encryptor = Provider.CreateEncryptor();
            return Encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public void Init()
        {
            if (Provider == null)
            {
                Provider = Aes.Create();
                Provider.Mode = CipherMode.ECB;
                Provider.KeySize = 192;
                IV = Provider.IV;
                Key = Provider.Key;
                Decryptor = Provider.CreateDecryptor();
                Encryptor = Provider.CreateEncryptor();
            }
        }


        public void Dispose()
        {
            Provider?.Dispose();
            Decryptor?.Dispose();
            Encryptor?.Dispose();
        }
    }

    public struct AES_ECB_256 : Crypto.ISymmetric, IDisposable
    {
        private ICryptoTransform Decryptor;
        private ICryptoTransform Encryptor;
        private Aes? Provider;

        public ushort? Published => 1998;
        public byte[]? Key { get; private set; }
        public byte[]? IV { get; private set; }
        public ushort KeyLenght => 32;


        public byte[]? Decrypt(byte[]? data)
        {
            if (data == null)
                return null;
            Init();
            return Decryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Encrypt(byte[]? data)
        {
            if (data == null)
                return null;
            Init();
            return Encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Decrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            if (data == null || key == null || iv == null)
                return null;
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            Decryptor = Provider.CreateDecryptor();
            return Decryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[]? Encrypt(byte[]? key, byte[]? iv, byte[]? data)
        {
            if (data == null || key == null || iv == null)
                return null;
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            Encryptor = Provider.CreateEncryptor();
            return Encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public void Init()
        {
            if (Provider == null)
            {
                Provider = Aes.Create();
                Provider.Mode = CipherMode.ECB;
                Provider.KeySize = 256;
                IV = Provider.IV;
                Key = Provider.Key;
                Decryptor = Provider.CreateDecryptor();
                Encryptor = Provider.CreateEncryptor();
            }
        }


        public void Dispose()
        {
            Provider?.Dispose();
            Decryptor?.Dispose();
            Encryptor?.Dispose();
        }
    }

    /*
    public struct AES_OFB_128 : ISymmetric, IDisposable
    {
        private ICryptoTransform Decryptor;
        private ICryptoTransform Encryptor;
        private Aes? Provider;

        public byte[] Key { get; private set; }
        public byte[] IV { get; private set;}
        public ushort KeyLenght => 16;


        public byte[] Decrypt(byte[] data)
        {
            Init();
            return Decryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Encrypt(byte[] data)
        {
            Init();
            return Encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Decrypt(byte[] key, byte[] iv, byte[] data)
        {
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            using var a = Provider.CreateDecryptor();
            return a.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Encrypt(byte[] key, byte[] iv, byte[] data)
        {
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            using var a = Provider.CreateEncryptor();
            return a.TransformFinalBlock(data, 0, data.Length);
        }

        public void Init()
        {
            if (Provider == null)
            {
                Provider = Aes.Create();
                Provider.Mode = CipherMode.OFB;
                Provider.KeySize = 128;
                IV = Provider.IV;
                Key = Provider.Key;
                Decryptor = Provider.CreateDecryptor();
                Encryptor = Provider.CreateEncryptor();
            }
        }



        public void Dispose()
        {
            Provider?.Dispose();
            Decryptor?.Dispose();
            Encryptor?.Dispose();
        }
    }
    public struct AES_OFB_192 : ISymmetric, IDisposable
    {
        private ICryptoTransform Decryptor;
        private ICryptoTransform Encryptor;
        private Aes? Provider;

        public byte[] Key { get; private set; }
        public byte[] IV { get; private set;}
        public ushort KeyLenght => 24;


        public byte[] Decrypt(byte[] data)
        {
            Init();
            return Decryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Encrypt(byte[] data)
        {
            Init();
            return Encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Decrypt(byte[] key, byte[] iv, byte[] data)
        {
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            using var a = Provider.CreateDecryptor();
            return a.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Encrypt(byte[] key, byte[] iv, byte[] data)
        {
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            using var a = Provider.CreateEncryptor();
            return a.TransformFinalBlock(data, 0, data.Length);
        }

        public void Init()
        {
            if (Provider == null)
            {
                Provider = Aes.Create();
                Provider.Mode = CipherMode.OFB;
                Provider.KeySize = 192;
                IV = Provider.IV;
                Key = Provider.Key;
                Decryptor = Provider.CreateDecryptor();
                Encryptor = Provider.CreateEncryptor();
            }
        }



        public void Dispose()
        {
            Provider?.Dispose();
            Decryptor?.Dispose();
            Encryptor?.Dispose();
        }
    }
    public struct AES_OFB_256 : ISymmetric, IDisposable
    {
        private ICryptoTransform Decryptor;
        private ICryptoTransform Encryptor;
        private Aes? Provider;

        public byte[] Key { get; private set; }
        public byte[] IV { get; private set;}
        public ushort KeyLenght => 32;


        public byte[] Decrypt(byte[] data)
        {
            Init();
            return Decryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Encrypt(byte[] data)
        {
            Init();
            return Encryptor.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Decrypt(byte[] key, byte[] iv, byte[] data)
        {
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            using var a = Provider.CreateDecryptor();
            return a.TransformFinalBlock(data, 0, data.Length);
        }

        public byte[] Encrypt(byte[] key, byte[] iv, byte[] data)
        {
            Init();
            Provider!.Key = key;
            Provider!.IV = iv;
            using var a = Provider.CreateEncryptor();
            return a.TransformFinalBlock(data, 0, data.Length);
        }

        public void Init()
        {
            if (Provider == null)
            {
                Provider = Aes.Create();
                Provider.Mode = CipherMode.OFB;
                Provider.KeySize = 256;
                IV = Provider.IV;
                Key = Provider.Key;
                Decryptor = Provider.CreateDecryptor();
                Encryptor = Provider.CreateEncryptor();
            }
        }



        public void Dispose()
        {
            Provider?.Dispose();
            Decryptor?.Dispose();
            Encryptor?.Dispose();
        }
    }
    */
}