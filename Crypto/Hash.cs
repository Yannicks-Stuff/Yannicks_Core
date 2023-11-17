using System.Text;
using Yannick.Extensions.StringExtensions;
using Yannick.Lang;

namespace Yannick.Crypto
{
    public static class Hash
    {
        public static byte[]? Decrypt<T>(byte[]? array) where T : IHash => Activator.CreateInstance<T>().Decrypt(array);

        public static string? FindKey<T>(byte[] dKey, byte max,
            char[]? alphabet = null,
            Encoding? encoding = null) where T : IHash
        {
            encoding ??= Encoding.UTF8;
            max = max <= 0 ? (byte)1 : max;
            alphabet ??= Alphabet.German + Alphabet.Numbers + Alphabet.SpecialKeys;
            var isFound = false;
            var pwToF = "";
            var instance = Activator.CreateInstance<T>();
            var keyChars = CreateCharArray(max, alphabet[0]);
            var indexOfLastChar = max - 1;
            CreateNewKey(0, keyChars, indexOfLastChar);

            void CreateNewKey(int currentCharPosition, char[] keys, int indexOfLastChar)
            {
                var nextCharPosition = currentCharPosition + 1;
                foreach (var t in alphabet)
                {
                    if (isFound)
                        break;
                    keys[currentCharPosition] = t;
                    if (currentCharPosition < indexOfLastChar)
                        CreateNewKey(nextCharPosition, keys, indexOfLastChar);
                    else
                    {
                        pwToF = new string(keys);
                        if (instance.Decrypt(pwToF.ToByteArray(encoding)).SequenceEqual(dKey))
                        {
                            isFound = true;
                        }
                    }
                }
            }

            static char[] CreateCharArray(int length, char defaultChar)
                => (from c in new char[length] select defaultChar).ToArray();

            return pwToF;
        }

        public static string? DictionaryFinder<T>(string filePath, byte[] dkey, Encoding? encoding = null)
            where T : IHash
        {
            encoding ??= Encoding.UTF8;
            if (!File.Exists(filePath))
                return null;

            return File.ReadLines(filePath, encoding).AsParallel().FirstOrDefault(e =>
                Activator.CreateInstance<T>().Decrypt(e.ToByteArray(encoding)).SequenceEqual(dkey));
        }

        public static string[] DeterminePossibleHashAlgorithms(string input)
        {
            List<string> possibleAlgorithms = new List<string>();
            if (input.Length == 32)
            {
                possibleAlgorithms.Add("MD5");
                possibleAlgorithms.Add("FNV-32");
                possibleAlgorithms.Add("HAVAL");
            }
            else if (input.Length == 40)
            {
                possibleAlgorithms.Add("SHA-1");
            }
            else if (input.Length == 64)
            {
                possibleAlgorithms.Add("SHA-256");
                possibleAlgorithms.Add("FNV-64");
                possibleAlgorithms.Add("Snefru");
            }
            else if (input.Length == 96)
            {
                possibleAlgorithms.Add("SHA-384");
            }
            else if (input.Length == 128)
            {
                possibleAlgorithms.Add("SHA-512");
                possibleAlgorithms.Add("FNV-128");
                possibleAlgorithms.Add("Whirlpool");
            }
            else if (input.Length == 136)
            {
                possibleAlgorithms.Add("SHA-3");
            }
            else if (input.Length == 28)
            {
                possibleAlgorithms.Add("MD4");
            }
            else if (input.Length == 16)
            {
                possibleAlgorithms.Add("CRC-16");
                possibleAlgorithms.Add("FNV-16");
                possibleAlgorithms.Add("GOST");
            }
            else if (input.Length == 24)
            {
                possibleAlgorithms.Add("CRC-24");
            }
            else if (input.Length == 56)
            {
                possibleAlgorithms.Add("BLAKE2b");
            }
            else if (input.Length == 48)
            {
                possibleAlgorithms.Add("Tiger");
            }
            else if (input.Length == 20)
            {
                possibleAlgorithms.Add("RIPEMD-160");
            }
            else if (input.Length == 4)
            {
                possibleAlgorithms.Add("CRC-4");
            }
            else if (input.Length == 8)
            {
                possibleAlgorithms.Add("CRC-8");
            }
            else if (input.Length == 10)
            {
                possibleAlgorithms.Add("CRC-10");
            }
            else if (input.Length == 12)
            {
                possibleAlgorithms.Add("CRC-12");
            }
            else if (input.Length == 14)
            {
                possibleAlgorithms.Add("CRC-14");
            }
            else if (input.Length == 15)
            {
                possibleAlgorithms.Add("CRC-15");
            }
            else if (input.Length == 16)
            {
                possibleAlgorithms.Add("CRC-16");
                possibleAlgorithms.Add("FNV-16");
                possibleAlgorithms.Add("GOST");
            }
            else if (input.Length == 20)
            {
                possibleAlgorithms.Add("CRC-20");
                possibleAlgorithms.Add("RIPEMD-160");
            }
            else if (input.Length == 24)
            {
                possibleAlgorithms.Add("CRC-24");
            }
            else if (input.Length == 28)
            {
                possibleAlgorithms.Add("CRC-28");
                possibleAlgorithms.Add("MD4");
            }
            else if (input.Length == 32)
            {
                possibleAlgorithms.Add("CRC-32");
                possibleAlgorithms.Add("MD5");
                possibleAlgorithms.Add("FNV-32");
                possibleAlgorithms.Add("HAVAL");
            }
            else if (input.Length == 40)
            {
                possibleAlgorithms.Add("CRC-40");
                possibleAlgorithms.Add("SHA-1");
            }
            else if (input.Length == 48)
            {
                possibleAlgorithms.Add("CRC-48");
                possibleAlgorithms.Add("Tiger");
            }
            else if (input.Length == 56)
            {
                possibleAlgorithms.Add("CRC-56");
                possibleAlgorithms.Add("BLAKE2b");
            }
            else if (input.Length == 64)
            {
                possibleAlgorithms.Add("CRC-64");
                possibleAlgorithms.Add("SHA-256");
                possibleAlgorithms.Add("FNV-64");
                possibleAlgorithms.Add("Snefru");
            }
            else if (input.Length == 72)
            {
                possibleAlgorithms.Add("CRC-72");
            }
            else if (input.Length == 80)
            {
                possibleAlgorithms.Add("CRC-80");
            }
            else if (input.Length == 84)
            {
                possibleAlgorithms.Add("CRC-84");
            }
            else if (input.Length == 96)
            {
                possibleAlgorithms.Add("CRC-96");
                possibleAlgorithms.Add("SHA-384");
            }
            else if (input.Length == 128)
            {
                possibleAlgorithms.Add("CRC-128");
                possibleAlgorithms.Add("SHA-512");
                possibleAlgorithms.Add("FNV-128");
                possibleAlgorithms.Add("Whirlpool");
            }
            else if (input.Length == 136)
            {
                possibleAlgorithms.Add("CRC-136");
                possibleAlgorithms.Add("SHA-3");
            }
            else
            {
                possibleAlgorithms.Add("Unbekannter Algorithmus");
            }

            return possibleAlgorithms.ToArray();
        }
    }

    public struct Hash<T>
    {
        public Hash(byte count)
        {
        }
    }
}