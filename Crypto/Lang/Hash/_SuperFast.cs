namespace Yannick.Crypto.Lang.Hash
{
    public struct SuperFast : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.SuperFast();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.SuperFast();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}