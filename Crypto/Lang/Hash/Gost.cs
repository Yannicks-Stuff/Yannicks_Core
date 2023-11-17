namespace Yannick.Crypto.Lang.Hash
{
    /// <summary>
    /// The GOST block cipher (Magma), defined in the standard GOST 28147-89 (RFC 5830),
    /// is a Soviet and Russian government standard symmetric key block cipher with a block size of 64 bits.
    /// The original standard, published in 1989, did not give the cipher any name, but the most recent revision of
    /// the standard, GOST R 34.12-2015, specifies that it may be referred to as Magma.[1] The GOST hash function
    /// is based on this cipher. The new standard also specifies a new 128-bit block cipher called Kuznyechik.
    /// </summary>
    public struct Gost : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Gost();

        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Gost();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    /// <summary>
    /// The GOST block cipher (Magma), defined in the standard GOST 28147-89 (RFC 5830),
    /// is a Soviet and Russian government standard symmetric key block cipher with a block size of 64 bits.
    /// The original standard, published in 1989, did not give the cipher any name, but the most recent revision of
    /// the standard, GOST R 34.12-2015, specifies that it may be referred to as Magma.[1] The GOST hash function
    /// is based on this cipher. The new standard also specifies a new 128-bit block cipher called Kuznyechik.
    /// </summary>
    public struct GOST3411_2012_256 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.GOST3411_2012_256();

        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.GOST3411_2012_256();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    /// <summary>
    /// The GOST block cipher (Magma), defined in the standard GOST 28147-89 (RFC 5830),
    /// is a Soviet and Russian government standard symmetric key block cipher with a block size of 64 bits.
    /// The original standard, published in 1989, did not give the cipher any name, but the most recent revision of
    /// the standard, GOST R 34.12-2015, specifies that it may be referred to as Magma.[1] The GOST hash function
    /// is based on this cipher. The new standard also specifies a new 128-bit block cipher called Kuznyechik.
    /// </summary>
    public struct GOST3411_2012_512
    {
        public ushort HashSize => 64;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.GOST3411_2012_512();

            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}