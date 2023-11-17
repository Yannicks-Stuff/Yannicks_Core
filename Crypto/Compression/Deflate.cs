using System.IO.Compression;

namespace Yannick.Crypto.Compression
{
    public class Deflate : CompressionAndDeCompression
    {
        public enum CompressionLevel
        {
            Optimal,
            Fastest,
            NoCompression,
        }

        public Deflate() : base(typeof(CompressionLevel))
        {
        }


        public override int Encode(ReadOnlySpan<byte> source, Span<byte> target, int option)
        {
            target = Encode(source.ToArray(), option);
            return 1;
        }

        public override int Decode(ReadOnlySpan<byte> source, Span<byte> target)
        {
            target = Decode(source.ToArray());
            return 1;
        }

        public byte[]? Encode(byte[] source, System.IO.Compression.CompressionLevel option) =>
            Encode(source, (int)option);

        public override byte[]? Encode(byte[] source, int option)
        {
            using (var r = new MemoryStream(source))
            using (var w = new MemoryStream())
            using (var d = new DeflateStream(w,
                       option <= 0
                           ? System.IO.Compression.CompressionLevel.Optimal
                           : Enum.Parse<System.IO.Compression.CompressionLevel>(option + "")))
            {
                r.CopyTo(d);
                d.Close();
                return w.GetBuffer();
            }
        }

        public override byte[]? Decode(byte[] source)
        {
            using (var r = new MemoryStream(source))
            using (var w = new MemoryStream())
            using (var ds = new DeflateStream(r, CompressionMode.Decompress))
            {
                ds.CopyTo(w, 4096);
                return w.ToArray();
            }
        }
    }
}