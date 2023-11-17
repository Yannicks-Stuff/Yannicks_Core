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

namespace Yannick.Crypto.SharpHash.Utils
{
    internal static class Bits
    {
        public static unsafe void ReverseByteArray(IntPtr Source, IntPtr Dest, long size)
        {
            var ptr_src = (byte*)Source;
            var ptr_dest = (byte*)Dest;

            ptr_dest = ptr_dest + (size - 1);
            while (size > 0)
            {
                *ptr_dest = *ptr_src;
                ptr_src += 1;
                ptr_dest -= 1;
                size -= 1;
            } // end while
        } // end function ReverseByteArray

        public static int ReverseBytesInt32(int value)
        {
            var i1 = value & 0xFF;
            var i2 = Asr32(value, 8) & 0xFF;
            var i3 = Asr32(value, 16) & 0xFF;
            var i4 = Asr32(value, 24) & 0xFF;

            return (i1 << 24) | (i2 << 16) | (i3 << 8) | (i4 << 0);
        } // end function ReverseBytesInt32

        public static byte ReverseBitsUInt8(byte value)
        {
            var result = (byte)(((value >> 1) & 0x55) | ((value << 1) & 0xAA));
            result = (byte)(((result >> 2) & 0x33) | ((result << 2) & 0xCC));
            return (byte)(((result >> 4) & 0x0F) | ((result << 4) & 0xF0));
        } // end function ReverseBitsUInt8

        public static ushort ReverseBytesUInt16(ushort value)
        {
            return (ushort)(((value & (uint)(0xFF)) << 8 | (value & (uint)(0xFF00)) >> 8));
        } // end function ReverseBytesUInt16

        public static uint ReverseBytesUInt32(uint value)
        {
            return (value & 0x000000FF) << 24 |
                   (value & 0x0000FF00) << 8 |
                   (value & 0x00FF0000) >> 8 |
                   (value & 0xFF000000) >> 24;
        } // end function ReverseBytesUInt32

        public static ulong ReverseBytesUInt64(ulong value)
        {
            return (value & 0x00000000000000FF) << 56 |
                   (value & 0x000000000000FF00) << 40 |
                   (value & 0x0000000000FF0000) << 24 |
                   (value & 0x00000000FF000000) << 8 |
                   (value & 0x000000FF00000000) >> 8 |
                   (value & 0x0000FF0000000000) >> 24 |
                   (value & 0x00FF000000000000) >> 40 |
                   (value & 0xFF00000000000000) >> 56;
        } // end function ReverseBytesUInt64

        public static int Asr32(int value, int ShiftBits)
        {
            return (int)((uint)(value) >> (ShiftBits & 31) |
                         ((uint)((int)(0 - ((uint)(value) >> 31) &
                                       (uint)(0 - (Convert.ToInt32((ShiftBits & 31) != 0))))) <<
                          (32 - (ShiftBits & 31))));
        } // end function Asr32

        public static long Asr64(long value, long ShiftBits)
        {
            return (long)((ulong)(value) >> (int)(ShiftBits & 63) |
                          ((0 - ((ulong)(value) >> 63) &
                            (ulong)(0 - (Convert.ToInt32((ShiftBits & 63) != 0)))) << (int)(64 - (ShiftBits & 63))));
        } // end function Asr64

        public static uint RotateLeft32(uint a_value, int a_n)
        {
            a_n = a_n & 31;
            return (a_value << a_n) | (a_value >> (32 - a_n));
        } // end function RotateLeft32

        public static ulong RotateLeft64(ulong a_value, int a_n)
        {
            a_n = a_n & 63;
            return (a_value << a_n) | (a_value >> (64 - a_n));
        } // end function RotateLeft64

        public static uint RotateRight32(uint a_value, int a_n)
        {
            a_n = a_n & 31;
            return (a_value >> a_n) | (a_value << (32 - a_n));
        } // end function RotateRight32

        public static ulong RotateRight64(ulong a_value, int a_n)
        {
            a_n = a_n & 63;
            return (a_value >> a_n) | (a_value << (64 - a_n));
        } // end function RotateRight64
    }
}