namespace Yannick.Crypto.Lang.Checksum
{
    public struct CRC32_CASTAGNOLI : IChecksum
    {
        private SharpHash.Interfaces.IHash _hash;


        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Checksum.CRC32_CASTAGNOLI();

        public ushort HashSize => 4;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Checksum.CRC32_CASTAGNOLI();
            a.Initialize();
            a.TransformBytes(data);
            return a.TransformFinal().GetBytes();
        }
    }
}