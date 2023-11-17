using System.Text;
using Yannick.Extensions.ArrayExtensions.ByteArrayExtensions;

namespace Yannick.Crypto
{
    public interface IChecksum : IHash
    {
    }

    public interface IHash
    {
        internal SharpHash.Interfaces.IHash Hash { get; }

        /// <summary>
        /// Byte array length
        /// </summary>
        ushort HashSize { get; }

        ushort? Published => null;

        /// <summary>
        /// Size in bytes
        /// </summary>
        uint BlockSize => 8u * HashSize;

        byte[]? Decrypt(byte[]? data);

        byte[] Decrypt(object? data, Encoding? encoding = null) => data == null
            ? Array.Empty<byte>()
            : (encoding ?? Encoding.UTF8).GetBytes(Decrypt(data.ToString(), encoding));

        string Decrypt(string data, Encoding? encoding = null) =>
            (encoding ?? Encoding.UTF8).GetString(Decrypt((encoding ?? Encoding.UTF8).GetBytes(data)));

        string DecryptToBase64(byte[]? data) => Decrypt(data).ToBase64String();
    }
}