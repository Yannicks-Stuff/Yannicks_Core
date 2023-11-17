namespace Yannick.Crypto.Lang.Hash
{
    public struct OneAtTime : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.OneAtTime();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.OneAtTime();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}