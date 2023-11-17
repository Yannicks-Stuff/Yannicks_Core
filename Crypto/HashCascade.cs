namespace Yannick.Crypto
{
    public class HashCascade
    {
        private readonly IList<IHash> Hashes;
        public readonly uint HashSize;
        private IHash? _hash;

        public HashCascade(IEnumerable<IHash> hashes)
        {
            Hashes = new List<IHash>(hashes);
            HashSize = (uint)Hashes.Sum(e => e.HashSize);
        }

        public HashCascade(params IHash[] hashes) : this(new List<IHash>(hashes))
        {
        }

        public byte[] Decrypt(byte[]? data)
        {
            var a = new List<byte>();

            foreach (var b in Hashes)
                a.AddRange(b.Decrypt(data));

            return a.ToArray();
        }
    }
}