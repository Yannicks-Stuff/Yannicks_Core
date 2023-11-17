namespace Yannick.Crypto.Lang.Hash
{
    public struct SipHash2_4 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash64.SipHash2_4();

        public ushort HashSize => 8;
        public ushort Published => 2003;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash64.SipHash2_4();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}