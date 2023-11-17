namespace Yannick.Crypto.Lang.Hash
{
    public struct Panama : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.Panama();

        public ushort HashSize => 32;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.Panama();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}