namespace Yannick.Crypto.Lang.Hash
{
    public struct SDBM : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.SDBM();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.SDBM();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}