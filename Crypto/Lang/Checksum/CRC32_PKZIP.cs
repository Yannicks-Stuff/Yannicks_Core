namespace Yannick.Crypto.Lang.Checksum
{
    public struct CRC32_PKZIP : IChecksum
    {
        private SharpHash.Interfaces.IHash _hash;


        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Checksum.CRC32_PKZIP();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Checksum.CRC32_PKZIP();
            a.Initialize();
            a.TransformBytes(data);
            return a.TransformFinal().GetBytes();
        }
    }
}