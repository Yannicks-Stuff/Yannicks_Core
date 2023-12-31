﻿///////////////////////////////////////////////////////////////////////
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

using Yannick.Crypto.SharpHash.Base;
using Yannick.Crypto.SharpHash.Interfaces;
using Yannick.Crypto.SharpHash.Utils;

namespace Yannick.Crypto.SharpHash.Hash32
{
    internal sealed class MurmurHash3_x86_32 : Base.Hash, IHash32, IHashWithKey, ITransformBlock
    {
        private static readonly uint CKEY = 0x0;

        private static readonly uint C1 = 0xCC9E2D51;
        private static readonly uint C2 = 0x1B873593;
        private static readonly uint C3 = 0xE6546B64;
        private static readonly uint C4 = 0x85EBCA6B;
        private static readonly uint C5 = 0xC2B2AE35;

        private static readonly string InvalidKeyLength = "KeyLength Must Be Equal to {0}";
        private byte[]? buf;
        private int idx;
        private uint key, h, total_length;

        public MurmurHash3_x86_32()
            : base(4, 4)
        {
            key = CKEY;
            buf = new byte[4];
        } // end constructor

        public override void Initialize()
        {
            h = key;
            total_length = 0;
            idx = 0;
        } // end function Initialize

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new MurmurHash3_x86_32();

            HashInstance.key = key;
            HashInstance.h = h;
            HashInstance.total_length = total_length;
            HashInstance.idx = idx;

            HashInstance.buf = buf.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone

        public override void TransformBytes(byte[]? a_data, int a_index, int a_length)
        {
            int len, nBlocks, i, offset;
            uint k;

            len = a_length;
            i = a_index;
            total_length += (uint)len;

            unsafe
            {
                fixed (byte* ptr_a_data = a_data, ptr_Fm_buf = buf)
                {
                    //consume last pending bytes
                    if (idx != 0 && a_length != 0)
                    {
                        while (idx < 4 && len != 0)
                        {
                            buf[idx++] = *(ptr_a_data + a_index);
                            a_index++;
                            len--;
                        }

                        if (idx == 4)
                        {
                            k = Converters.ReadBytesAsUInt32LE((IntPtr)ptr_Fm_buf, 0);
                            TransformUInt32Fast(k);
                            idx = 0;
                        }
                    } // end if
                    else
                    {
                        i = 0;
                    } // end else

                    nBlocks = (len) >> 2;
                    offset = 0;

                    // body
                    while (i < nBlocks)
                    {
                        k = Converters.ReadBytesAsUInt32LE((IntPtr)ptr_a_data, a_index + (i * 4));
                        TransformUInt32Fast(k);
                        i++;
                    } // end while

                    //save pending end bytes
                    offset = a_index + (i * 4);
                    while (offset < (len + a_index))
                    {
                        ByteUpdate(a_data[offset]);
                        offset++;
                    } // end while
                }
            }
        } // end function TransformBytes

        public override IHashResult TransformFinal()
        {
            Finish();

            IHashResult result = new HashResult(h);

            Initialize();

            return result;
        } // end function TransformFinal

        public int? KeyLength
        {
            get => 4;
        } // end property KeyLength

        public byte[]? Key
        {
            get => Converters.ReadUInt32AsBytesLE(key);

            set
            {
                if (value.Empty())
                    key = CKEY;
                else
                {
                    if (value.Length != KeyLength)
                        throw new ArgumentHashLibException(string.Format(InvalidKeyLength, KeyLength));

                    unsafe
                    {
                        fixed (byte* vPtr = &value[0])
                        {
                            key = Converters.ReadBytesAsUInt32LE((IntPtr)vPtr, 0);
                        }
                    }
                } // end else
            }
        } // end property Key

        private void TransformUInt32Fast(uint a_data)
        {
            var k = a_data;

            k = k * C1;
            k = Bits.RotateLeft32(k, 15);
            k = k * C2;

            h = h ^ k;
            h = Bits.RotateLeft32(h, 13);
            h = (h * 5) + C3;
        } // end function TransformUInt32Fast

        private void ByteUpdate(byte a_b)
        {
            uint k = 0;

            buf[idx] = a_b;
            idx++;
            if (idx >= 4)
            {
                unsafe
                {
                    fixed (byte* ptr_Fm_buf = &buf[0])
                    {
                        k = Converters.ReadBytesAsUInt32LE((IntPtr)ptr_Fm_buf, 0);
                    }
                }

                TransformUInt32Fast(k);
                idx = 0;
            } // end if
        } // end function ByteUpdate

        private void Finish()
        {
            uint k = 0;

            // tail
            if (idx != 0)
            {
                switch (idx)
                {
                    case 3:
                        k = k ^ (uint)(buf[2] << 16);
                        k = k ^ (uint)(buf[1] << 8);
                        k = k ^ buf[0];
                        k = k * C1;
                        k = Bits.RotateLeft32(k, 15);
                        k = k * C2;
                        h = h ^ k;
                        break;

                    case 2:
                        k = k ^ (uint)(buf[1] << 8);
                        k = k ^ buf[0];
                        k = k * C1;
                        k = Bits.RotateLeft32(k, 15);
                        k = k * C2;
                        h = h ^ k;
                        break;

                    case 1:
                        k = k ^ buf[0];
                        k = k * C1;
                        k = Bits.RotateLeft32(k, 15);
                        k = k * C2;
                        h = h ^ k;
                        break;
                } // end switch
            } // end if

            // finalization
            h = h ^ total_length;
            h = h ^ (h >> 16);
            h = h * C4;
            h = h ^ (h >> 13);
            h = h * C5;
            h = h ^ (h >> 16);
        } // end function Finish
    } // end class MurmurHash3_x86_32
}