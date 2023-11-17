using System.IO.Compression;

namespace Yannick.Crypto.Compression
{
    public class GZip : CompressionAndDeCompression
    {
        public enum Option
        {
            Fast
        }

        public GZip() : base(typeof(Option))
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

        public override byte[]? Encode(byte[] source, int option)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gZipStream.Write(source);
                }

                return memoryStream.ToArray();
            }
        }

        public override byte[]? Decode(byte[] source)
        {
            using (var ms = new MemoryStream())
            using (var memoryStream = new MemoryStream(source))
            {
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.CopyTo(ms);

                    return ms.ToArray();
                }
            }
        }
    }
}