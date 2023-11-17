namespace Yannick.Crypto.Lang.Checksum
{
    public struct CRC64_ECMA_182 : IChecksum
    {
        private SharpHash.Interfaces.IHash _hash;


        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Checksum.CRC64_ECMA_182();

        public ushort HashSize => 8;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Checksum.CRC64_ECMA_182();
            a.Initialize();
            a.TransformBytes(data);
            return a.TransformFinal().GetBytes();
        }
    }
}