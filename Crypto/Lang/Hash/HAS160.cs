namespace Yannick.Crypto.Lang.Hash
{
    public struct HAS160 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.HAS160();

        public ushort HashSize => 20;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.HAS160();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}