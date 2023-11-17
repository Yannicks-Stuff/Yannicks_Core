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
    public static class ArrayUtils
    {
        public static bool Empty(this byte[]? array)
        {
            return ((array == null) || (array.Length == 0));
        }

        public static bool Empty(this uint[] array)
        {
            return ((array == null) || (array.Length == 0));
        }

        public static bool Empty(this ulong[] array)
        {
            return ((array == null) || (array.Length == 0));
        }

        public static byte[]? DeepCopy(this byte[]? array)
        {
            var newArray = new byte[array?.Length ?? 0];
            if (newArray.Length != 0)
                Utils.Memcopy(ref newArray, array, newArray.Length);

            return newArray;
        }

        public static uint[] DeepCopy(this uint[] array)
        {
            var newArray = new uint[array?.Length ?? 0];
            if (newArray.Length != 0)
                Utils.Memcopy(ref newArray, array, newArray.Length);

            return newArray;
        }

        public static ulong[] DeepCopy(this ulong[] array)
        {
            var newArray = new ulong[array?.Length ?? 0];
            if (newArray.Length != 0)
                Utils.Memcopy(ref newArray, array, newArray.Length);

            return newArray;
        }

        public static bool ConstantTimeAreEqual(byte[] buffer1, byte[] buffer2)
        {
            int Idx;
            uint Diff;

            Diff = (uint)(buffer1.Length ^ buffer2.Length);

            Idx = 0;
            while (Idx <= buffer1.Length && Idx <= buffer2.Length)
            {
                Diff = Diff | (uint)(buffer1[Idx] ^ buffer2[Idx]);
                Idx++;
            }

            return Diff == 0;
        } // end function ConstantTimeAreEqual

        public static unsafe void Fill(ref byte[]? buffer, int from, int to, byte filler)
        {
            if (!buffer.Empty())
            {
                fixed (byte* ptrStart = buffer)
                {
                    Unsafe.InitBlock((IntPtr*)(ptrStart + from), filler, (uint)(to - from) * sizeof(byte));
                }
            }
        } // end function fill

        public static void Fill(ref uint[] buffer, int from, int to, uint filler)
        {
            if (!buffer.Empty())
            {
                var count = from;
                while (count < to)
                {
                    buffer[count] = filler;
                    count++;
                }
            }
        } // end funtion fill

        public static void Fill(ref ulong[] buffer, int from, int to, ulong filler)
        {
            if (!buffer.Empty())
            {
                var count = from;
                while (count < to)
                {
                    buffer[count] = filler;
                    count++;
                }
            }
        } // end function fill

        public static void ZeroFill(ref byte[]? buffer)
        {
            Fill(ref buffer, 0, buffer?.Length ?? 0, 0);
        } // end function zeroFill

        public static void ZeroFill(ref uint[] buffer)
        {
            Fill(ref buffer, 0, buffer?.Length ?? 0, 0);
        } // end function zeroFill

        public static void ZeroFill(ref ulong[] buffer)
        {
            Fill(ref buffer, 0, buffer?.Length ?? 0, 0);
        } // end function zeroFill
    } // end class ArrayUtils
}