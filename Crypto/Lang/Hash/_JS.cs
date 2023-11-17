namespace Yannick.Crypto.Lang.Hash
{
    public struct JS : IHash
    {
        public ushort Published => 1996;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.JS();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.JS();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}