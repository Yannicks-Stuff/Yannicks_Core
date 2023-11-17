using Yannick.Crypto.SharpHash.Hash128;

namespace Yannick.Crypto.Lang.Hash
{
    public struct MurmurHash3_x86_128 : IHash
    {
        public ushort Published => 1996;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash128.MurmurHash3_x86_128();

        public ushort HashSize => 16;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash128.MurmurHash3_x86_128();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct MurmurHash3_X64_128 : IHash
    {
        public ushort Published => 1996;

        SharpHash.Interfaces.IHash IHash.Hash => new MurmurHash3_x64_128();

        public ushort HashSize => 16;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new MurmurHash3_x64_128();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Murmur2 : IHash
    {
        public ushort Published => 1996;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.Murmur2();

        public ushort HashSize => 16;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.Murmur2();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct MurmurHash3_x86_32 : IHash
    {
        public ushort Published => 1996;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.MurmurHash3_x86_32();

        public ushort HashSize => 16;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.MurmurHash3_x86_32();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Murmur2_64 : IHash
    {
        public ushort Published => 1996;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash64.Murmur2_64();

        public ushort HashSize => 8;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash64.Murmur2_64();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}