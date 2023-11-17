namespace Yannick.Crypto.Lang.Hash
{
    public struct Jenkins3 : IHash
    {
        public ushort Published => 1996;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.Jenkins3();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.Jenkins3();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}