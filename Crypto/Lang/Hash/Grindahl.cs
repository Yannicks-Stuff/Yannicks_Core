namespace Yannick.Crypto.Lang.Hash
{
    public struct Grindahl512 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Grindahl512();

        public ushort HashSize => 64;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Grindahl512();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct Grindahl256 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Grindahl256();

        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Grindahl256();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}