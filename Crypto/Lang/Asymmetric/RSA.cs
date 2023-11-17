using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Yannick.Crypto.Lang.Asymmetric
{
    /// <summary>
    /// RSA (Rivest–Shamir–Adleman) is one of the first public-key cryptosystems and is widely used for secure data transmission. In such a cryptosystem, the encryption key is public and distinct from the decryption key which is kept secret (private). In RSA, this asymmetry is based on the practical difficulty of factoring the product of two large prime numbers, the "factoring problem". The acronym RSA is the initial letters of the surnames of Ron Rivest, Adi Shamir, and Leonard Adleman, who publicly described the algorithm in 1977. Clifford Cocks, an English mathematician working for the British intelligence agency Government Communications Headquarters (GCHQ), had developed an equivalent system in 1973, which was not declassified until 1997.[1]
    /// </summary>
    public struct RSA : IAsymmetric, IDisposable
    {
        private readonly dynamic?[] Pkcs1Encoding = new dynamic?[2];

        public ushort? Published => 1977;
        public byte[] PublicKey { get; private set; }
        public byte[] PrivateKey { get; private set; }
        private AsymmetricCipherKeyPair? AsymmetricCipherKeyPair;
        private AsymmetricKeyParameter? _PublicKey = null;
        private AsymmetricKeyParameter? _PrivateKey = null;
        private readonly bool UsePkcs1;
        private readonly IDigest OaepDigest;


        public RSA(int keySize = 2048, bool useSafePrimes = true, bool usePkcs1 = true) : this(keySize, useSafePrimes,
            usePkcs1, null)
        {
        }

        public RSA(IDigest oaepDigest, int keySize = 2048, bool useSafePrimes = true) : this(keySize, useSafePrimes,
            false, oaepDigest)
        {
        }

        public RSA(int keySize, bool useSafePrimes, bool usePkcs1, IDigest? oaepDigest)
        {
            oaepDigest ??= new Sha256Digest();
            UsePkcs1 = usePkcs1;
            OaepDigest = oaepDigest;
            if (keySize == 0)
            {
                PublicKey = Array.Empty<byte>();
                PrivateKey = Array.Empty<byte>();
                AsymmetricCipherKeyPair = null;
            }
            else
            {
                if (useSafePrimes)
                {
                    var randomGenerator = new CryptoApiRandomGenerator();
                    var secureRandom = new SecureRandom(randomGenerator);
                    var keyGenerationParameters = new KeyGenerationParameters(secureRandom, keySize);

                    var keyPairGenerator = new RsaKeyPairGenerator();
                    keyPairGenerator.Init(keyGenerationParameters);
                    AsymmetricCipherKeyPair = keyPairGenerator.GenerateKeyPair();
                }
                else
                {
                    using var rsaProvider = new RSACryptoServiceProvider(keySize);
                    var rsaKeyInfo = rsaProvider.ExportParameters(true);
                    AsymmetricCipherKeyPair = DotNetUtilities.GetRsaKeyPair(rsaKeyInfo);
                }

                Pkcs1Encoding[0] = UsePkcs1
                    ? new Pkcs1Encoding(new RsaBlindedEngine())
                    : new OaepEncoding(new RsaBlindedEngine(), OaepDigest);
                Pkcs1Encoding[1] = UsePkcs1
                    ? new Pkcs1Encoding(new RsaBlindedEngine())
                    : new OaepEncoding(new RsaBlindedEngine(), OaepDigest);

                PublicKey = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(AsymmetricCipherKeyPair.Public)
                    .ToAsn1Object().GetDerEncoded();
                PrivateKey = PrivateKeyInfoFactory.CreatePrivateKeyInfo(AsymmetricCipherKeyPair.Private)
                    .ToAsn1Object().GetDerEncoded();

                Pkcs1Encoding[0]!.Init(true, AsymmetricCipherKeyPair.Public);
                Pkcs1Encoding[1]!.Init(false, AsymmetricCipherKeyPair.Private);
            }
        }


        public void ImportPublicKey(byte[] key)
        {
            //RsaPrivateKeyStructure.GetInstance(Asn1Object.FromByteArray(key));
            throw new NotImplementedException();
        }

        public void ImportPrivateKey(byte[] key)
        {
            throw new NotImplementedException();
            //PrivateKeyFactory.CreateKey()
            // AsymmetricCipherKeyPair = new AsymmetricCipherKeyPair());
        }

        public byte[] Decrypt(byte[] data)
            => Pkcs1Encoding[1]!.ProcessBlock(data, 0, data.Length);

        public byte[] Encrypt(byte[] data)
            => Pkcs1Encoding[0]!.ProcessBlock(data, 0, data.Length);


        public string ExportPrivateKeyToPem()
        {
            if (AsymmetricCipherKeyPair == null)
                return string.Empty;

            var textWriter = new StringWriter();
            var pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(AsymmetricCipherKeyPair.Private);
            pemWriter.Writer.Flush();
            return textWriter.ToString();
        }

        public string ExportPublicKeyToPem()
        {
            if (AsymmetricCipherKeyPair == null)
                return string.Empty;

            var textWriter = new StringWriter();
            var pemWriter = new PemWriter(textWriter);
            pemWriter.WriteObject(AsymmetricCipherKeyPair.Public);
            pemWriter.Writer.Flush();
            return textWriter.ToString();
        }

        public void ImportPrivateKeyFromPem(string pem)
        {
            AsymmetricCipherKeyPair = null;
            _PrivateKey = null;

            using var sr = new StringReader(pem);
            var pemReader = new PemReader(sr);
            var pemObject = pemReader.ReadObject();

            if (pemObject is AsymmetricCipherKeyPair ack)
            {
                AsymmetricCipherKeyPair = ack;
                PrivateKey = PrivateKeyInfoFactory.CreatePrivateKeyInfo(AsymmetricCipherKeyPair.Private)
                    .ToAsn1Object().GetDerEncoded();
            }
            else if (pemObject is RsaKeyParameters rsaKeyParameters)
            {
                _PrivateKey = rsaKeyParameters;
                PrivateKey = PrivateKeyInfoFactory.CreatePrivateKeyInfo(rsaKeyParameters)
                    .ToAsn1Object().GetDerEncoded();
            }

            Pkcs1Encoding[1] = UsePkcs1
                ? new Pkcs1Encoding(new RsaBlindedEngine())
                : new OaepEncoding(new RsaBlindedEngine(), OaepDigest);
            Pkcs1Encoding[1]!.Init(false, _PrivateKey ?? AsymmetricCipherKeyPair!.Private);
        }

        public void ImportPublicKeyFromPem(string pem)
        {
            AsymmetricCipherKeyPair = null;
            _PublicKey = null;

            using var sr = new StringReader(pem);
            var pemReader = new PemReader(sr);
            var pemObject = pemReader.ReadObject();
            if (pemObject is AsymmetricCipherKeyPair ack)
            {
                AsymmetricCipherKeyPair = ack;
                PublicKey = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(AsymmetricCipherKeyPair.Public)
                    .ToAsn1Object().GetDerEncoded();
            }
            else if (pemObject is RsaKeyParameters rsaKeyParameters)
            {
                _PublicKey = rsaKeyParameters;
                PublicKey = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(rsaKeyParameters)
                    .ToAsn1Object().GetDerEncoded();
            }

            Pkcs1Encoding[0] = UsePkcs1
                ? new Pkcs1Encoding(new RsaBlindedEngine())
                : new OaepEncoding(new RsaBlindedEngine(), OaepDigest);
            Pkcs1Encoding[0]!.Init(true, _PublicKey ?? AsymmetricCipherKeyPair!.Public);
        }

        public void Dispose()
        {
        }
    }
}