﻿using System.Collections;

namespace Yannick.Lang
{
    /// <summary>
    /// Represents an alphabet class that provides an enumeration of characters.
    /// </summary>
    public sealed class Alphabet : IEnumerable<char>
    {
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
        /// Combines two alphabets.
        /// </summary>
        public static Alphabet operator +(Alphabet a, Alphabet b)
        {
            var c = new char[a.Length + b.Length];
            Array.Copy(a._a, 0, c, 0, a.Length);
            Array.Copy(b._a, 0, c, a.Length, b.Length);
            return c;
        }
    }
}