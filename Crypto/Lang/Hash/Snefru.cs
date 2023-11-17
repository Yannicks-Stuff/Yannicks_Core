using Yannick.Crypto.SharpHash.Crypto;

namespace Yannick.Crypto.Lang.Hash
{
    /// <param name="aSharpHash.Crypto.Security_level">Any Integer value greater than 0. Standard is 8. </param>
    /// <param name="a_hashSharpHash.Crypto.Size">128bit, 256bit</param>
    //public struct Snefru: IHash
    public struct Snefru_1_128 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(1, 128);

        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(1, 128);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Snefru_2_128 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(2, 128);
        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(2, 128);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Snefru_3_128 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(3, 128);
        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(3, 128);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Snefru_4_128 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(4, 128);
        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(4, 128);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Snefru_5_128 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(5, 128);
        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(5, 128);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Snefru_6_128 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(6, 128);
        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(6, 128);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Snefru_7_128 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(7, 128);
        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(7, 128);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Snefru_8_128 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(8, 128);
        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(8, 128);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Snefru_1_256 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(1, 256);
        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(1, 256);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Snefru_2_256 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(2, 256);
        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(2, 256);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Snefru_3_256 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(3, 256);
        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(3, 256);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Snefru_4_256 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(4, 256);
        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(4, 256);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Snefru_5_256 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(5, 256);
        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(5, 256);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Snefru_6_256 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(6, 256);
        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(6, 256);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Snefru_7_256 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(7, 256);
        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(7, 256);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Snefru_8_256 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new Snefru(8, 256);
        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new Snefru(8, 256);
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}