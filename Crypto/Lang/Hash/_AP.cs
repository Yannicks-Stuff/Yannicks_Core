namespace Yannick.Crypto.Lang.Hash
{
    public struct AP : IHash
    {
        public ushort Published => 1996;
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.AP();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.AP();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}