namespace Yannick.Crypto.Lang.Hash
{
    /// <summary>
    /// The Secure Hash Algorithms are a family of cryptographic hash functions published by the National Institute of Standards and Technology (NIST) as a U.S. Federal Information Processing Standard (FIPS), including:
    /// A retronym applied to the original version of the 160-bit hash function published in 1993 under the name "SHA". It was withdrawn shortly after publication due to an undisclosed "significant flaw" and replaced by the slightly revised version SHA-1.
    /// </summary>
    public struct SHA0 : IHash
    {
        ushort? Published => 1993;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.SHA0();

        public ushort HashSize => 20;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.SHA0();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    /// <summary>
    /// The Secure Hash Algorithms are a family of cryptographic hash functions published by the National Institute of Standards and Technology (NIST) as a U.S. Federal Information Processing Standard (FIPS), including:
    /// A 160-bit hash function which resembles the earlier MD5 algorithm. This was designed by the National Security Agency (NSA) to be part of the Digital Signature Algorithm. Cryptographic weaknesses were discovered in SHA-1, and the standard was no longer approved for most cryptographic uses after 2010.
    /// </summary>
    public struct SHA1 : IHash
    {
        ushort? Published => 1995;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.SHA1();

        public ushort HashSize => 20;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.SHA1();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    /// <summary>
    /// These were also designed by the NSA.
    /// </summary>
    public struct SHA2_224 : IHash
    {
        ushort? Published => 2004;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.SHA2_224();

        public ushort HashSize => 28;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.SHA2_224();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    /// <summary>
    /// These were also designed by the NSA.
    /// </summary>
    public struct SHA2_256 : IHash
    {
        ushort? Published => 2001;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.SHA2_256();

        public ushort HashSize => 28;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.SHA2_256();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    /// <summary>
    /// These were also designed by the NSA.
    /// </summary>
    public struct SHA2_384 : IHash
    {
        ushort? Published => 2001;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.SHA2_384();

        public ushort HashSize => 48;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.SHA2_384();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    /// <summary>
    /// These were also designed by the NSA.
    /// </summary>
    public struct SHA2_512 : IHash
    {
        ushort? Published => 2001;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.SHA2_512();

        public ushort HashSize => 64;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.SHA2_512();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public struct SHA2_512_224 : IHash
    {
        ushort? Published => 2012;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.SHA2_512_224();

        public ushort HashSize => 64;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.SHA2_512_224();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public struct SHA2_512_256 : IHash
    {
        ushort? Published => 2012;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.SHA2_512_256();

        public ushort HashSize => 64;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.SHA2_512_256();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct SHA3_224 : IHash
    {
        ushort? Published => 2015;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.SHA3_224();

        public ushort HashSize => 28;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.SHA3_224();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct SHA3_256 : IHash
    {
        ushort? Published => 2015;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.SHA3_256();

        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.SHA3_256();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct SHA3_384 : IHash
    {
        ushort? Published => 2015;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.SHA3_384();

        public ushort HashSize => 48;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.SHA3_384();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct SHA3_512 : IHash
    {
        ushort? Published => 2015;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.SHA3_512();

        public ushort HashSize => 64;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.SHA3_512();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Keccak_224 : IHash
    {
        ushort? Published => 2015;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Keccak_224();

        public ushort HashSize => 28;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Keccak_224();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Keccak_256 : IHash
    {
        ushort? Published => 2015;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Keccak_256();

        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Keccak_256();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Keccak_288 : IHash
    {
        ushort? Published => 2015;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Keccak_288();

        public ushort HashSize => 36;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Keccak_288();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Keccak_384 : IHash
    {
        ushort? Published => 2015;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Keccak_384();

        public ushort HashSize => 48;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Keccak_384();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Keccak_512 : IHash
    {
        ushort? Published => 2015;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Keccak_512();

        public ushort HashSize => 64;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Keccak_512();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}