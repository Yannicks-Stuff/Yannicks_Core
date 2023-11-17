namespace Yannick.Crypto.Lang.Hash
{
    public struct Rotating : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.DJB();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.DJB();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}