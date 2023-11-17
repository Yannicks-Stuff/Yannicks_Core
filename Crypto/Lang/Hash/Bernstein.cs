namespace Yannick.Crypto.Lang.Hash
{
    public struct Bernstein1 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.Bernstein1();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.Bernstein1();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Bernstein : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.Bernstein();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.Bernstein();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}