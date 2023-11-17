namespace Yannick.Crypto.Lang.Hash
{
    public struct WhirlPool : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.WhirlPool();

        public ushort HashSize => 64;
        public ushort Published => 2003;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.WhirlPool();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}