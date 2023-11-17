namespace Yannick.Crypto.Lang.Hash
{
    public struct MD2 : IHash
    {
        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.MD2();

        public ushort HashSize => 16;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.MD2();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }

        public ushort Published => 1989;
    }

    public struct MD4 : IHash
    {
        public ushort Published => 1990;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.MD4();

        public ushort HashSize => 16;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.MD4();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }

    public struct MD5 : IHash, IHackable
    {
        public ushort Published => 1992;

        SharpHash.Interfaces.IHash IHash.Hash => new SharpHash.Crypto.MD5();

        public ushort HashSize => 16;

        public byte[]? Decrypt(byte[]? data)
        {
            var a = new SharpHash.Crypto.MD5();
            a.Initialize();
            return a.ComputeBytes(data).GetBytes();
        }
    }
}