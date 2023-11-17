namespace Yannick.Crypto.Lang.Hash
{
    public struct ShiftAndXor : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Hash32.ShiftAndXor();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Hash32.ShiftAndXor();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}