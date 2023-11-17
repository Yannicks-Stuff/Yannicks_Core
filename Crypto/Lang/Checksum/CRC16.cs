namespace Yannick.Crypto.Lang.Checksum
{
    public struct CRC16 : IChecksum
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Checksum.CRC16(0x8005, 0x0000, false, false, 0x0000,
            0xFEE8, new[] { "CRC-16/BUYPASS", "CRC-16/VERIFONE" });

        public ushort HashSize => 2;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Checksum.CRC16(0x8005, 0x0000, false, false, 0x0000, 0xFEE8,
                new[] { "CRC-16/BUYPASS", "CRC-16/VERIFONE" });
            a.Initialize();
            a.TransformBytes(data);
            return a.TransformFinal().GetBytes();
        }
    }
}