using System.Collections;

namespace Yannick.Lang
{
    /// <summary>
    /// Represents an alphabet class that provides an enumeration of characters.
    /// </summary>
    public sealed class Alphabet : IEnumerable<char>, IAsyncEnumerable<char>
    {
        public static readonly Alphabet Russian = new(new[]
        {
            'А', 'а', 'Б', 'б', 'В', 'в', 'Г', 'г', 'Д', 'д', 'Е', 'е', 'Ё', 'ё', 'Ж', 'ж', 'З', 'з', 'И', 'и', 'Й',
            'й', 'К', 'к',
            'Л', 'л', 'М', 'м', 'Н', 'н', 'О', 'о', 'П', 'п', 'Р', 'р', 'С', 'с', 'Т', 'т', 'У', 'у', 'Ф', 'ф', 'Х',
            'х', 'Ц', 'ц',
            'Ч', 'ч', 'Ш', 'ш', 'Щ', 'щ', 'Ъ', 'ъ', 'Ы', 'ы', 'Ь', 'ь', 'Э', 'э', 'Ю', 'ю', 'Я', 'я'
        });

        public static readonly Alphabet Ukrainian = new(new[]
        {
            'А', 'а', 'Б', 'б', 'В', 'в', 'Г', 'г', 'Ґ', 'ґ', 'Д', 'д', 'Е', 'е', 'Є', 'є', 'Ж', 'ж', 'З', 'з', 'И',
            'и', 'І', 'і',
            'Ї', 'ї', 'Й', 'й', 'К', 'к', 'Л', 'л', 'М', 'м', 'Н', 'н', 'О', 'о', 'П', 'п', 'Р', 'р', 'С', 'с', 'Т',
            'т', 'У', 'у',
            'Ф', 'ф', 'Х', 'х', 'Ц', 'ц', 'Ч', 'ч', 'Ш', 'ш', 'Щ', 'щ', 'Ь', 'ь', 'Ю', 'ю', 'Я', 'я'
        });

        public static readonly Alphabet Spanish = new(new[]
        {
            'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', 'G', 'g', 'H', 'h', 'I', 'i', 'J', 'j', 'K',
            'k',
            'L', 'l', 'M', 'm', 'N', 'n', 'Ñ', 'ñ', 'O', 'o', 'P', 'p', 'Q', 'q', 'R', 'r', 'S', 's', 'T', 't', 'U',
            'u',
            'V', 'v', 'W', 'w', 'X', 'x', 'Y', 'y', 'Z', 'z'
        });

        public static readonly Alphabet Arabic = new(new[]
        {
            'أ', 'ا', 'ب', 'ت', 'ث', 'ج', 'ح', 'خ', 'د', 'ذ', 'ر', 'ز', 'س', 'ش', 'ص', 'ض', 'ط', 'ظ', 'ع', 'غ', 'ف',
            'ق', 'ك', 'ل', 'م', 'ن', 'ه', 'و', 'ي'
        });

        public static readonly Alphabet Hindi = new(new[]
        {
            'अ', 'आ', 'इ', 'ई', 'उ', 'ऊ', 'ए', 'ऐ', 'ओ', 'औ', 'क', 'ख', 'ग', 'घ', 'च', 'छ', 'ज', 'झ', 'ट', 'ठ', 'ड',
            'ढ', 'त', 'थ', 'द', 'ध', 'न', 'प', 'फ', 'ब', 'भ', 'म', 'य', 'र', 'ल', 'व', 'श', 'ष', 'स', 'ह'
        });

        public static readonly Alphabet Portuguese = new(new[]
        {
            'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', 'G', 'g', 'H', 'h', 'I', 'i', 'J', 'j', 'K',
            'k',
            'L', 'l', 'M', 'm', 'N', 'n', 'O', 'o', 'P', 'p', 'Q', 'q', 'R', 'r', 'S', 's', 'T', 't', 'U', 'u', 'V',
            'v',
            'W', 'w', 'X', 'x', 'Y', 'y', 'Z', 'z', 'Ç', 'ç'
        });

        public static readonly Alphabet Japanese = new(new[]
        {
            'ア', 'イ', 'ウ', 'エ', 'オ', 'カ', 'キ', 'ク', 'ケ', 'コ', 'サ', 'シ', 'ス', 'セ', 'ソ', 'タ', 'チ', 'ツ', 'テ', 'ト',
            'ナ', 'ニ', 'ヌ', 'ネ', 'ノ', 'ハ', 'ヒ', 'フ', 'ヘ', 'ホ', 'マ', 'ミ', 'ム', 'メ', 'モ', 'ヤ', 'ユ', 'ヨ', 'ラ', 'リ',
            'ル', 'レ', 'ロ', 'ワ', 'ヲ', 'ン'
        });

        public static readonly Alphabet Korean = new(new[]
        {
            'ㄱ', 'ㄴ', 'ㄷ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅅ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ', 'ㄲ', 'ㄸ', 'ㅃ', 'ㅆ', 'ㅉ', 'ㅏ',
            'ㅑ', 'ㅓ', 'ㅕ', 'ㅗ', 'ㅛ', 'ㅜ', 'ㅠ', 'ㅡ', 'ㅣ', 'ㅐ', 'ㅒ', 'ㅔ', 'ㅖ', 'ㅘ', 'ㅙ', 'ㅚ', 'ㅝ', 'ㅞ', 'ㅟ', 'ㅢ'
        });

        /// <summary>
        /// Represents the German alphabet.
        /// </summary>
        public static Alphabet German = new[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E',
            'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
            'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'ä', 'ö', 'ü', 'Ä', 'Ü', 'Ö'
        };

        /// <summary>
        /// Represents the English alphabet.
        /// </summary>
        public static Alphabet English = new[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E',
            'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
            'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        /// <summary>
        /// Represents the numeric digits.
        /// </summary>
        public static Alphabet Numbers = new[]
        {
            '1', '2', '3', '4', '5',
            '6', '7', '8', '9', '0'
        };

        /// <summary>
        /// Represents special characters.
        /// </summary>
        public static Alphabet SpecialKeys = new[]
        {
            '!', '"', '$', '$', '%',
            '&', '/', '(', ')', '=', '?', '`', '´', '#',
            ',', '.', '-', '_', ':', ';'
        };

        private readonly char[] _a;
        public readonly int Count;
        public readonly uint Length;

        /// <summary>
        /// Initializes a new instance of the <see cref="Alphabet"/> class with a specified character array.
        /// </summary>
        /// <param name="alpha">The character array representing the alphabet.</param>
        public Alphabet(char[] alpha)
        {
            _a = alpha;
            Length = (uint)_a.Length;
            Count = _a.Length;
        }

        /// <summary>
        /// Gets the character at a specified offset.
        /// </summary>
        /// <param name="offset">The offset of the character in the alphabet.</param>
        /// <returns>The character at the specified offset.</returns>
        public char this[int offset] => _a[offset];

        /// <summary>
        /// Returns an enumerator that iterates asynchronously through the collection.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the iteration.</param>
        /// <returns>An <see cref="IAsyncEnumerator{T}"/> that can be used to iterate through the collection.</returns>
        public async IAsyncEnumerator<char> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            foreach (var c in _a)
            {
                if (cancellationToken.IsCancellationRequested)
                    yield break;

                yield return c;
                await Task.Delay(1, cancellationToken);
            }
        }

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<char> GetEnumerator()
        {
            foreach (var c in _a)
                yield return c;
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Converts an array of characters to an instance of the <see cref="Alphabet"/> class.
        /// </summary>
        public static implicit operator Alphabet(char[] a) => new Alphabet(a);

        /// <summary>
        /// Converts an instance of the <see cref="Alphabet"/> class to an array of characters.
        /// </summary>
        public static implicit operator char[](Alphabet a) => a._a;

        /// <summary>
        /// Converts an instance of the <see cref="Alphabet"/> class to an integer representing the count.
        /// </summary>
        public static implicit operator int(Alphabet a) => a.Count;

        /// <summary>
        /// Converts an instance of the <see cref="Alphabet"/> class to an unsigned integer representing the length.
        /// </summary>
        public static implicit operator uint(Alphabet a) => a.Length;

        /// <summary>
        /// Combines two alphabets into a new alphabet containing all characters from both alphabets.
        /// </summary>
        /// <param name="a">The first alphabet to combine.</param>
        /// <param name="b">The second alphabet to combine.</param>
        /// <returns>A new alphabet containing all characters from both <paramref name="a"/> and <paramref name="b"/>.</returns>
        public static Alphabet operator +(Alphabet a, Alphabet b)
        {
            var c = new char[a.Length + b.Length];
            Array.Copy(a._a, 0, c, 0, a.Length);
            Array.Copy(b._a, 0, c, a.Length, b.Length);
            return c;
        }

        /// <summary>
        /// Produces a new alphabet by removing characters in the second alphabet from the first alphabet.
        /// </summary>
        /// <param name="a">The alphabet to subtract from.</param>
        /// <param name="b">The alphabet whose characters will be removed from <paramref name="a"/>.</param>
        /// <returns>A new alphabet with characters from <paramref name="b"/> removed from <paramref name="a"/>.</returns>
        public static Alphabet operator -(Alphabet a, Alphabet b)
        {
            return (
                    from c in a
                    let found = b.Any(r => r == c)
                    where !found
                    select c)
                .ToArray();
        }

        /// <summary>
        /// Implicitly converts an <see cref="Alphabet"/> instance to a byte array.
        /// </summary>
        /// <param name="a">The <see cref="Alphabet"/> instance to convert.</param>
        /// <returns>A byte array representing the characters in the Alphabet.</returns>
        public static implicit operator byte[](Alphabet a)
        {
            var byteArray = new byte[a._a.Length * sizeof(char)];
            Buffer.BlockCopy(a._a, 0, byteArray, 0, byteArray.Length);
            return byteArray;
        }
    }
}