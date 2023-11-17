namespace Yannick.Crypto.Lang.Hash
{
    public struct XXHash32 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.XXHash32();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.XXHash32();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct XXHash64 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash64.XXHash64();

        public ushort HashSize => 8;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash64.XXHash64();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}