namespace Yannick.Crypto.Lang.Hash
{
    public struct FNV1a : IHash
    {
        public ushort Published => 1996;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.FNV1a();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.FNV1a();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct FNV1a64 : IHash
    {
        public ushort Published => 1996;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash64.FNV1a64();

        public ushort HashSize => 8;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash64.FNV1a64();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct FNV64 : IHash
    {
        public ushort Published => 1996;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash64.FNV64();

        public ushort HashSize => 8;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash64.FNV64();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct FNV : IHash
    {
        public ushort Published => 1996;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.FNV();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.FNV();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}