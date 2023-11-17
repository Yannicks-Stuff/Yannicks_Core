namespace Yannick.Crypto.Lang.Hash
{
    public struct Haval_5_256 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Haval_5_256();

        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Haval_5_256();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Haval_4_256 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Haval_4_256();

        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Haval_4_256();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Haval_3_256 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Haval_3_256();

        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Haval_3_256();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Haval_5_224 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Haval_5_224();

        public ushort HashSize => 28;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Haval_5_224();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Haval_4_224 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Haval_4_224();

        public ushort HashSize => 28;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Haval_4_224();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Haval_3_224 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Haval_3_224();

        public ushort HashSize => 28;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Haval_3_224();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Haval_5_192 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Haval_5_192();

        public ushort HashSize => 24;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Haval_5_192();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Haval_4_192 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Haval_4_192();

        public ushort HashSize => 24;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Haval_4_192();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Haval_3_192 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Haval_3_192();

        public ushort HashSize => 24;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Haval_3_192();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Haval_5_160 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Haval_5_160();

        public ushort HashSize => 20;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Haval_5_160();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Haval_4_160 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Haval_4_160();

        public ushort HashSize => 20;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Haval_4_160();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Haval_3_160 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Haval_3_160();

        public ushort HashSize => 20;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Haval_3_160();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Haval_5_128 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Haval_5_128();

        public ushort HashSize => 16;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Haval_5_128();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Haval_4_128 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Haval_4_128();

        public ushort HashSize => 16;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Haval_4_128();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Haval_3_128 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Haval_3_128();

        public ushort HashSize => 16;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Haval_3_128();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}