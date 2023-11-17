using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Yannick.Crypto.Lang.Asymmetric
{
    /// <summary>
    /// In cryptography, the ElGamal encryption system is an asymmetric key encryption algorithm for public-key cryptography which is based on the Diffie–Hellman key exchange. It was described by Taher Elgamal in 1985.[1] ElGamal encryption is used in the free GNU Privacy Guard software, recent versions of PGP, and other cryptosystems. The Digital Signature Algorithm (DSA) is a variant of the ElGamal signature scheme, which should not be confused with ElGamal encryption.
    /// </summary>
    public struct ElGamal : IAsymmetric
    {
        private ElGamalEngine? PublicKeyProvider;
        private ElGamalEngine? PrivateKeyProvider;
        private ElGamalPublicKeyParameters? _PublicKey;
        private ElGamalPrivateKeyParameters? _PrivateKey;
        public byte[] PublicKey { get; private set; }
        public byte[] PrivateKey { get; private set; }
        public ushort? Published => 1985;

        private void Init()
        {
            if (PublicKeyProvider == null)
            {
                var g = new BigInteger(256, 10, new SecureRandom());
                var p = new BigInteger(256, 10, new SecureRandom());

                var dhParams = new ElGamalParameters(p, g, 0);
                var @params = new ElGamalKeyGenerationParameters(new SecureRandom(), dhParams);
                var kpGen = new ElGamalKeyPairGenerator();

                kpGen.Init(@params);

                var pair = kpGen.GenerateKeyPair();
                _PublicKey = (ElGamalPublicKeyParameters)pair.Public;
                _PrivateKey = (ElGamalPrivateKeyParameters)pair.Private;

                var k = PrivateKeyInfoFactory.CreatePrivateKeyInfo(_PrivateKey);
                PrivateKey = k.ToAsn1Object().GetDerEncoded();

                var publicK = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(_PublicKey);
                PublicKey = publicK.ToAsn1Object().GetDerEncoded();

                PublicKeyProvider = new ElGamalEngine();
                PublicKeyProvider.Init(true, _PublicKey);

                PrivateKeyProvider = new ElGamalEngine();
                PrivateKeyProvider.Init(false, _PrivateKey);
            }
        }

        public void ImportPublicKey(byte[] key)
        {
            Init();
            _PublicKey = (ElGamalPublicKeyParameters)PublicKeyFactory.CreateKey(key);
            var publicK = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(_PublicKey);
            PublicKey = publicK.ToAsn1Object().GetDerEncoded();
            PublicKeyProvider = new ElGamalEngine();
            PublicKeyProvider.Init(true, _PublicKey);
        }

        public void ImportPrivateKey(byte[] key)
        {
            Init();
            _PrivateKey = (ElGamalPrivateKeyParameters)PrivateKeyFactory.CreateKey(key);
            var k = PrivateKeyInfoFactory.CreatePrivateKeyInfo(_PrivateKey);
            PrivateKey = k.ToAsn1Object().GetDerEncoded();
            PrivateKeyProvider = new ElGamalEngine();
            PrivateKeyProvider.Init(false, _PrivateKey);
        }

        public byte[] Decrypt(byte[] data)
        {
            Init();
            return PrivateKeyProvider!.ProcessBlock(data, 0, data.Length);
        }

        public byte[] Encrypt(byte[] data)
        {
            Init();
            return PublicKeyProvider!.ProcessBlock(data, 0, data.Length);
        }
    }
}