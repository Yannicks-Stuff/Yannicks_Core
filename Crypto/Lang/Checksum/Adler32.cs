namespace Yannick.Crypto.Lang.Checksum
{
    public struct Adler32 : IChecksum
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Checksum.Adler32();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Checksum.Adler32();
            a.Initialize();
            a.TransformBytes(data);
            return a.TransformFinal().GetBytes();
        }
    }
}