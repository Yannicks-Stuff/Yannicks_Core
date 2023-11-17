namespace Yannick.Crypto.Lang
{
    /*
    public abstract class Symmetric : ISymmetric
    {
        public abstract uint DefaultBlockSize { get; }
        public abstract uint DefaultKeySize { get; }
        public abstract uint[] AllowedKeySize { get; }
        public abstract byte[] Key { get; protected set; }


        public abstract byte[] Encode(byte[] array, int offset, int length);
        public byte[] Encode(byte[] array, int offset) => Encode(array, offset, array.Length - offset);
        public byte[] Encode(byte[] array) => Encode(array, 0, array.Length);
        public abstract byte[] Decode(byte[] array, int offset, int length);
        public byte[] Decode(byte[] array, int offset) => Decode(array, offset, array.Length - offset);
        public byte[] Decode(byte[] array) => Decode(array, 0, array.Length);


        public void RandomKey(Alphabet? alphabet = null, Encoding? encoding = null)
        {
            Key = RandomKey(DefaultKeySize, alphabet, encoding);
        }
        public static byte[] RandomKey(uint keyLength , Alphabet? alphabet = null, Encoding? encoding = null)
        {
            alphabet ??= Alphabet.GermanWithNumbers;
            encoding ??= Encoding.UTF8;

            var ar = Password.Generate(alphabet, Convert.ToInt32(keyLength > int.MaxValue ? int.MaxValue : keyLength));

            return encoding.GetBytes(ar);
        }
    }*/
}