namespace Yannick.Crypto.Lang.Hash
{
    /// <summary>
    /// RadioGatún is a cryptographic hash primitive created by Guido Bertoni, Joan Daemen, Michaël Peeters,
    /// and Gilles Van Assche. It was first publicly presented at the NIST Second Cryptographic Hash Workshop,
    /// held in Santa Barbara, California, on August 24–25, 2006, as part of the NIST hash function competition.
    /// </summary>
    public struct RadioGatun32 : IHash
    {
        public ushort Published => 2006;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.RadioGatun32();

        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.RadioGatun32();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    /// <summary>
    /// RadioGatún is a cryptographic hash primitive created by Guido Bertoni, Joan Daemen, Michaël Peeters,
    /// and Gilles Van Assche. It was first publicly presented at the NIST Second Cryptographic Hash Workshop,
    /// held in Santa Barbara, California, on August 24–25, 2006, as part of the NIST hash function competition.
    /// </summary>
    public struct RadioGatun64 : IHash
    {
        public ushort Published => 2006;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.RadioGatun64();

        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.RadioGatun64();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}