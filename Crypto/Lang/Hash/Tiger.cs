using Yannick.Crypto.SharpHash.Crypto;

namespace Yannick.Crypto.Lang.Hash
{
    public struct Tiger192 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => Tiger_192.CreateRound5();

        public ushort HashSize => 24;
        public ushort Published => 1996;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = Tiger_192.CreateRound5();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Tiger160 : IHash
    {
        public ushort Published => 1996;

        SharpHash.Interfaces.IHash IHash.Hash => Tiger_160.CreateRound5();

        public ushort HashSize => 20;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = Tiger_160.CreateRound5();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Tiger128 : IHash
    {
        public ushort Published => 1996;

        SharpHash.Interfaces.IHash IHash.Hash => Tiger_128.CreateRound5();

        public ushort HashSize => 16;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = Tiger_128.CreateRound5();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Tiger_2_192 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => Tiger2_192.CreateRound5();

        public ushort HashSize => 24;
        public ushort Published => 1996;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = Tiger2_192.CreateRound5();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Tiger_2_160 : IHash
    {
        public ushort Published => 1996;

        SharpHash.Interfaces.IHash IHash.Hash => Tiger2_160.CreateRound5();

        public ushort HashSize => 20;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = Tiger2_160.CreateRound5();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Tiger_2_128 : IHash
    {
        public ushort Published => 1996;

        SharpHash.Interfaces.IHash IHash.Hash => Tiger2_128.CreateRound5();

        public ushort HashSize => 16;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = Tiger2_128.CreateRound5();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}