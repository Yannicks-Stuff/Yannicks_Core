namespace Yannick.Crypto.Lang.Hash
{
    public struct RS : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.RS();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.RS();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}