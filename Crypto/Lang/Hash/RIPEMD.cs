namespace Yannick.Crypto.Lang.Hash
{
    /// <summary>
    /// /RIPEMD (RIPE Message Digest) is a family of cryptographic hash functions developed in 1992 (the original RIPEMD)
    /// and 1996 (other variants).
    /// </summary>
    public struct RIPEMD128 : IHash
    {
        public ushort Published => 1992;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.RIPEMD128();

        public ushort HashSize => 16;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.RIPEMD128();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    /// <summary>
    /// /RIPEMD (RIPE Message Digest) is a family of cryptographic hash functions developed in 1992 (the original RIPEMD)
    /// and 1996 (other variants).
    /// </summary>
    public struct RIPEMD160 : IHash
    {
        public ushort Published => 1992;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.RIPEMD160();

        public ushort HashSize => 20;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.RIPEMD160();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    /// <summary>
    /// /RIPEMD (RIPE Message Digest) is a family of cryptographic hash functions developed in 1992 (the original RIPEMD)
    /// and 1996 (other variants).
    /// </summary>
    public struct RIPEMD256 : IHash
    {
        public ushort Published => 1992;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.RIPEMD256();

        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.RIPEMD256();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    /// <summary>
    /// /RIPEMD (RIPE Message Digest) is a family of cryptographic hash functions developed in 1992 (the original RIPEMD)
    /// and 1996 (other variants).
    /// </summary>
    public struct RIPEMD320 : IHash
    {
        public ushort Published => 1992;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.RIPEMD320();

        public ushort HashSize => 40;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.RIPEMD320();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}