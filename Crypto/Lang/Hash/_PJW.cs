namespace Yannick.Crypto.Lang.Hash
{
    public struct PJW : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.PJW();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.PJW();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}