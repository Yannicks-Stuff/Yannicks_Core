///////////////////////////////////////////////////////////////////////
/// SharpHash Library
/// Copyright(c) 2019 - 2020  Mbadiwe Nnaemeka Ronald
/// Github Repository <https://github.com/ron4fun/SharpHash>
///
/// The contents of this file are subject to the
/// Mozilla Public License Version 2.0 (the "License");
/// you may not use this file except in
/// compliance with the License. You may obtain a copy of the License
/// at https://www.mozilla.org/en-US/MPL/2.0/
///
/// Software distributed under the License is distributed on an "AS IS"
/// basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See
/// the License for the specific language governing rights and
/// limitations under the License.
///
/// Acknowledgements:
///
/// Thanks to Ugochukwu Mmaduekwe (https://github.com/Xor-el) for his creative
/// development of this library in Pascal/Delphi (https://github.com/Xor-el/HashLib4Pascal).
///
/// Also, I will like to thank Udezue Chukwunwike (https://github.com/IzarchTech) for
/// his contributions to the growth and development of this library.
///
////////////////////////////////////////////////////////////////////////

using System.Runtime.CompilerServices;

namespace Yannick.Crypto.SharpHash.Utils
{
    public static class Utils
    {
        public static void Memcopy(ref byte[]? dest, byte[]? src, int n,
            int indexSrc = 0, int indexDest = 0)
        {
            Array.Copy(src, indexSrc, dest, indexDest, n);
        }

        public static void Memcopy(ref uint[] dest, uint[] src, int n,
            int indexSrc = 0, int indexDest = 0)
        {
            Array.Copy(src, indexSrc, dest, indexDest, n);
        }

        public static void Memcopy(ref ulong[] dest, ulong[] src, int n,
            int indexSrc = 0, int indexDest = 0)
        {
            Array.Copy(src, indexSrc, dest, indexDest, n);
        }

        public static void Memcopy(IntPtr dest, IntPtr src, int n)
        {
            Memmove(dest, src, n);
        }

        // A function to copy block of 'n' bytes from source
        // address 'src' to destination address 'dest'.
        public static unsafe void Memmove(IntPtr dest, IntPtr src, int n)
        {
            Unsafe.CopyBlock((IntPtr*)dest, (IntPtr*)src, (uint)n);
        }

        public static void Memmove(ref byte[]? dest, byte[]? src, int n,
            int indexSrc = 0, int indexDest = 0)
        {
            Array.Copy(src, indexSrc, dest, indexDest, n);
        } //

        public static void Memmove(ref uint[] dest, uint[] src, int n,
            int indexSrc = 0, int indexDest = 0)
        {
            Array.Copy(src, indexSrc, dest, indexDest, n);
        } //

        public static void Memmove(ref ulong[] dest, ulong[] src, int n,
            int indexSrc = 0, int indexDest = 0)
        {
            Array.Copy(src, indexSrc, dest, indexDest, n);
        } //

        public static unsafe void Memset(IntPtr dest, byte value, int n)
        {
            // Typecast src and dest address to (byte *)
            var cdest = (byte*)dest;

            // Copy data to dest[]
            for (var i = 0; i < n; i++)
                cdest[i] = value;
        } // end function memset

        public static void Memset(ref byte[]? array, byte value, int index = 0)
        {
            if (array.Empty()) return;

            int block = 32, startIndex = index, size = array.Length;
            var length = index + block < size ? index + block : size;

            // Fill the initial array
            while (index < length)
                array[index++] = value;

            length = array.Length;
            while (index < size)
            {
                Buffer.BlockCopy(array, startIndex, array, index, Math.Min(block, size - index));
                index += block;
                block *= 2;
            } // end while
        } // end function memSet

        public static unsafe void Memset(ref uint[] array, byte value, int index = 0)
        {
            if (array.Empty()) return;

            fixed (uint* ptrStart = array)
            {
                Unsafe.InitBlock((IntPtr*)(ptrStart + index), value, (uint)array.Length * sizeof(uint));
            }
        } // end function memset

        public static unsafe void Memset(ref ulong[] array, byte value, int index = 0, int n = -1)
        {
            if (array.Empty()) return;

            fixed (ulong* ptrStart = array)
            {
                Unsafe.InitBlock((IntPtr*)(ptrStart + index), value, (uint)array.Length * sizeof(ulong));
            }
        } // end function memset

        public static byte[]? Concat(byte[]? x, byte[]? y)
        {
            var result = new byte[0];
            var index = 0;

            if (x.Empty())
            {
                if (y.Empty()) return result;

                Array.Resize(ref result, y.Length);
                Memcopy(ref result, y, y.Length);

                return result;
            } // end if

            if (y.Empty())
            {
                Array.Resize(ref result, x.Length);
                Memcopy(ref result, x, x.Length);

                return result;
            } // end if

            Array.Resize(ref result, x.Length + y.Length);

            // If Lengths are equal
            if (x.Length == y.Length)
            {
                // Multi fill array
                while (index < y.Length)
                {
                    result[index] = x[index];
                    result[x.Length + index] = y[index++];
                } // end while
            } // end if
            else if (x.Length > y.Length)
            {
                // Multi fill array
                while (index < y.Length)
                {
                    result[index] = x[index];
                    result[x.Length + index] = y[index++];
                } // end while

                while (index < x.Length)
                    result[index] = x[index++];
            } // end else if
            else if (y.Length > x.Length)
            {
                // Multi fill array
                while (index < x.Length)
                {
                    result[index] = x[index];
                    result[x.Length + index] = y[index++];
                } // end while

                while (index < y.Length)
                    result[x.Length + index] = y[index++];
            } // ende else if

            return result;
        } // end function Concat

        public static uint[] Concat(uint[] x, uint[] y)
        {
            var result = new uint[0];
            var index = 0;

            if (x.Empty())
            {
                if (y.Empty()) return result;

                Array.Resize(ref result, y.Length);
                Memcopy(ref result, y, y.Length);

                return result;
            } // end if

            if (y.Empty())
            {
                Array.Resize(ref result, x.Length);
                Memcopy(ref result, x, x.Length);

                return result;
            } // end if

            Array.Resize(ref result, x.Length + y.Length);

            // If Lengths are equal
            if (x.Length == y.Length)
            {
                // Multi fill array
                while (index < y.Length)
                {
                    result[index] = x[index];
                    result[x.Length + index] = y[index++];
                } // end while
            } // end if
            else if (x.Length > y.Length)
            {
                // Multi fill array
                while (index < y.Length)
                {
                    result[index] = x[index];
                    result[x.Length + index] = y[index++];
                } // end while

                while (index < x.Length)
                    result[index] = x[index++];
            } // end else if
            else if (y.Length > x.Length)
            {
                // Multi fill array
                while (index < x.Length)
                {
                    result[index] = x[index];
                    result[x.Length + index] = y[index++];
                } // end while

                while (index < y.Length)
                    result[x.Length + index] = y[index++];
            } // ende else if

            return result;
        } // end function Concat
    }
}