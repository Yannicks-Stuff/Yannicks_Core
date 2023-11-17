namespace Yannick.Crypto.Compression
{
    public abstract class CompressionAndDeCompression
    {
        internal readonly Type EnumType;


        protected CompressionAndDeCompression(Type enumType)
        {
            EnumType = enumType;
        }

        public abstract int Encode(ReadOnlySpan<byte> source, Span<byte> target, int option);
        public abstract int Decode(ReadOnlySpan<byte> source, Span<byte> target);

        public abstract byte[]? Encode(byte[] source, int option);
        public abstract byte[]? Decode(byte[] source);
    }

    public class CompressionAndDeCompression<T> where T : CompressionAndDeCompression
    {
        public static int Encode(ReadOnlySpan<byte> source, Span<byte> target, int option)
        {
            var instance = Activator.CreateInstance<T>();

            return instance.Encode(source, target, (int)Enum.Parse(instance.EnumType, "" + option));
        }

        public static int Decode(ReadOnlySpan<byte> source, Span<byte> target)
        {
            var instance = Activator.CreateInstance<T>();

            return instance.Decode(source, target);
        }

        public static byte[]? Encode(byte[] source, int option)
        {
            var instance = Activator.CreateInstance<T>();

            return instance.Encode(source, (int)Enum.Parse(instance.EnumType, "" + option));
        }

        public static byte[]? Decode(byte[] source)
        {
            var instance = Activator.CreateInstance<T>();

            return instance.Decode(source);
        }
    }
}