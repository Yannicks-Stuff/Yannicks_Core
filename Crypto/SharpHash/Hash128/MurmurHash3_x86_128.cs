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

using Yannick.Crypto.SharpHash.Base;
using Yannick.Crypto.SharpHash.Interfaces;
using Yannick.Crypto.SharpHash.Utils;

namespace Yannick.Crypto.SharpHash.Hash128
{
    internal sealed class MurmurHash3_x86_128 : Base.Hash, IHash128, IHashWithKey, ITransformBlock
    {
        private static readonly uint CKEY = 0x0;

        private static readonly uint C1 = 0x239B961B;
        private static readonly uint C2 = 0xAB0E9789;
        private static readonly uint C3 = 0x38B34AE5;
        private static readonly uint C4 = 0xA1E38B93;
        private static readonly uint C5 = 0x85EBCA6B;
        private static readonly uint C6 = 0xC2B2AE35;

        private static readonly uint C7 = 0x561CCD1B;
        private static readonly uint C8 = 0x0BCAA747;
        private static readonly uint C9 = 0x96CD1C35;
        private static readonly uint C10 = 0x32AC3B17;

        private static readonly string InvalidKeyLength = "KeyLength Must Be Equal to {0}";
        private byte[]? buf;
        private int idx;
        private uint key, h1, h2, h3, h4, total_length;

        public MurmurHash3_x86_128()
            : base(16, 16)
        {
            key = CKEY;
            buf = new byte[16];
        } // end constructor

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new MurmurHash3_x86_128
            {
                key = key,
                h1 = h1,
                h2 = h2,
                h3 = h3,
                h4 = h4,
                total_length = total_length,
                idx = idx,
                buf = buf.DeepCopy(),
                BufferSize = BufferSize
            };

            return HashInstance;
        } // end function Clone

        public override void Initialize()
        {
            h1 = key;
            h2 = key;
            h3 = key;
            h4 = key;

            total_length = 0;
            idx = 0;
        } // end function Initialize

        public override unsafe IHashResult TransformFinal()
        {
            Finish();

            uint[] tempBufUInt32 = { h1, h2, h3, h4 };
            var tempBufByte = new byte[tempBufUInt32.Length * sizeof(uint)];

            fixed (uint* tmpPtr = tempBufUInt32)
            {
                fixed (byte* bPtr = tempBufByte)
                {
                    Converters.be32_copy((IntPtr)tmpPtr, 0, (IntPtr)bPtr, 0, tempBufByte.Length);

                    IHashResult result = new HashResult(tempBufByte);

                    Initialize();

                    return result;
                }
            }
        } // end function TransformFinal

        public override unsafe void TransformBytes(byte[]? a_data, int a_index, int a_length)
        {
            if (buf == null || a_data == null)
                throw new NullReferenceException();

            int len, nBlocks, i, offset, lIdx;
            uint k1, k2, k3, k4;

            len = a_length;
            i = a_index;
            lIdx = 0;
            total_length += (uint)len;

            fixed (byte* ptr_a_data = a_data)
            {
                //consume last pending bytes
                if (idx != 0 && len != 0)
                {
                    while (idx < 16 && len != 0)
                    {
                        buf[idx++] = *(ptr_a_data + a_index);
                        a_index++;
                        len--;
                    }

                    if (idx == 16)
                        ProcessPendings();
                }
                else
                    i = 0;

                nBlocks = len >> 4;

                // body
                while (i < nBlocks)
                {
                    k1 = Converters.ReadBytesAsUInt32LE((IntPtr)ptr_a_data, a_index + lIdx);
                    lIdx += 4;
                    k2 = Converters.ReadBytesAsUInt32LE((IntPtr)ptr_a_data, a_index + lIdx);
                    lIdx += 4;
                    k3 = Converters.ReadBytesAsUInt32LE((IntPtr)ptr_a_data, a_index + lIdx);
                    lIdx += 4;
                    k4 = Converters.ReadBytesAsUInt32LE((IntPtr)ptr_a_data, a_index + lIdx);
                    lIdx += 4;

                    k1 = k1 * C1;
                    k1 = Bits.RotateLeft32(k1, 15);
                    k1 = k1 * C2;
                    h1 = h1 ^ k1;

                    h1 = Bits.RotateLeft32(h1, 19);

                    h1 = h1 + h2;
                    h1 = h1 * 5 + C7;

                    k2 = k2 * C2;
                    k2 = Bits.RotateLeft32(k2, 16);
                    k2 = k2 * C3;
                    h2 = h2 ^ k2;

                    h2 = Bits.RotateLeft32(h2, 17);

                    h2 = h2 + h3;
                    h2 = h2 * 5 + C8;

                    k3 = k3 * C3;
                    k3 = Bits.RotateLeft32(k3, 17);
                    k3 = k3 * C4;
                    h3 = h3 ^ k3;

                    h3 = Bits.RotateLeft32(h3, 15);

                    h3 = h3 + h4;
                    h3 = h3 * 5 + C9;

                    k4 = k4 * C4;
                    k4 = Bits.RotateLeft32(k4, 18);
                    k4 = k4 * C1;
                    h4 = h4 ^ k4;

                    h4 = Bits.RotateLeft32(h4, 13);

                    h4 = h4 + h1;
                    h4 = h4 * 5 + C10;

                    i++;
                } // end if

                offset = a_index + (i * 16);
                while (offset < (a_index + len))
                {
                    ByteUpdate(a_data[offset]);
                    offset++;
                } // end while
            }
        } // end function TransformBytes

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
                    if (value == null)
                        throw new NullReferenceException();

                    if (value.Length != KeyLength)
                        throw new ArgumentHashLibException(string.Format(InvalidKeyLength, KeyLength));

                    unsafe
                    {
                        fixed (byte* bPtr = &value[0])
                        {
                            key = Converters.ReadBytesAsUInt32LE((IntPtr)bPtr, 0);
                        }
                    }
                } // end else
            }
        } // end property Key

        private void ByteUpdate(byte a_b)
        {
            if (buf == null)
                throw new NullReferenceException();

            buf[idx] = a_b;
            idx++;
            ProcessPendings();
        } // end function ByteUpdate

        private unsafe void ProcessPendings()
        {
            uint k1, k2, k3, k4;

            fixed (byte* ptr_Fm_buf = buf)
            {
                if (idx >= 16)
                {
                    k1 = Converters.ReadBytesAsUInt32LE((IntPtr)ptr_Fm_buf, 0);
                    k2 = Converters.ReadBytesAsUInt32LE((IntPtr)ptr_Fm_buf, 4);
                    k3 = Converters.ReadBytesAsUInt32LE((IntPtr)ptr_Fm_buf, 8);
                    k4 = Converters.ReadBytesAsUInt32LE((IntPtr)ptr_Fm_buf, 12);

                    k1 = k1 * C1;
                    k1 = Bits.RotateLeft32(k1, 15);
                    k1 = k1 * C2;
                    h1 = h1 ^ k1;

                    h1 = Bits.RotateLeft32(h1, 19);

                    h1 = h1 + h2;
                    h1 = h1 * 5 + C7;

                    k2 = k2 * C2;
                    k2 = Bits.RotateLeft32(k2, 16);
                    k2 = k2 * C3;
                    h2 = h2 ^ k2;

                    h2 = Bits.RotateLeft32(h2, 17);

                    h2 = h2 + h3;
                    h2 = h2 * 5 + C8;

                    k3 = k3 * C3;
                    k3 = Bits.RotateLeft32(k3, 17);
                    k3 = k3 * C4;
                    h3 = h3 ^ k3;

                    h3 = Bits.RotateLeft32(h3, 15);

                    h3 = h3 + h4;
                    h3 = h3 * 5 + C9;

                    k4 = k4 * C4;
                    k4 = Bits.RotateLeft32(k4, 18);
                    k4 = k4 * C1;
                    h4 = h4 ^ k4;

                    h4 = Bits.RotateLeft32(h4, 13);

                    h4 = h4 + h1;
                    h4 = h4 * 5 + C10;

                    idx = 0;
                } // end if
            }
        } // end function ProcessPendings

        private void Finish()
        {
            if (buf == null)
                throw new NullReferenceException();

            uint k1, k2, k3, k4;
            int Length;

            // tail
            k1 = 0;
            k2 = 0;
            k3 = 0;
            k4 = 0;

            Length = idx;
            if (Length != 0)
            {
                switch (Length)
                {
                    case 15:
                        k4 = k4 ^ (uint)(buf[14] << 16);
                        k4 = k4 ^ (uint)(buf[13] << 8);
                        k4 = k4 ^ (uint)(buf[12] << 0);

                        k4 = k4 * C4;
                        k4 = Bits.RotateLeft32(k4, 18);
                        k4 = k4 * C1;
                        h4 = h4 ^ k4;
                        break;

                    case 14:
                        k4 = k4 ^ (uint)(buf[13] << 8);
                        k4 = k4 ^ (uint)(buf[12] << 0);
                        k4 = k4 * C4;
                        k4 = Bits.RotateLeft32(k4, 18);
                        k4 = k4 * C1;
                        h4 = h4 ^ k4;
                        break;

                    case 13:
                        k4 = k4 ^ (uint)(buf[12] << 0);
                        k4 = k4 * C4;
                        k4 = Bits.RotateLeft32(k4, 18);
                        k4 = k4 * C1;
                        h4 = h4 ^ k4;
                        break;
                } // end switch

                if (Length > 12)
                    Length = 12;

                switch (Length)
                {
                    case 12:
                        k3 = k3 ^ (uint)(buf[11] << 24);
                        k3 = k3 ^ (uint)(buf[10] << 16);
                        k3 = k3 ^ (uint)(buf[9] << 8);
                        k3 = k3 ^ (uint)(buf[8] << 0);

                        k3 = k3 * C3;
                        k3 = Bits.RotateLeft32(k3, 17);
                        k3 = k3 * C4;
                        h3 = h3 ^ k3;
                        break;

                    case 11:
                        k3 = k3 ^ (uint)(buf[10] << 16);
                        k3 = k3 ^ (uint)(buf[9] << 8);
                        k3 = k3 ^ (uint)(buf[8] << 0);

                        k3 = k3 * C3;
                        k3 = Bits.RotateLeft32(k3, 17);
                        k3 = k3 * C4;
                        h3 = h3 ^ k3;
                        break;

                    case 10:
                        k3 = k3 ^ (uint)(buf[9] << 8);
                        k3 = k3 ^ (uint)(buf[8] << 0);

                        k3 = k3 * C3;
                        k3 = Bits.RotateLeft32(k3, 17);
                        k3 = k3 * C4;
                        h3 = h3 ^ k3;
                        break;

                    case 9:
                        k3 = k3 ^ (uint)(buf[8] << 0);

                        k3 = k3 * C3;
                        k3 = Bits.RotateLeft32(k3, 17);
                        k3 = k3 * C4;
                        h3 = h3 ^ k3;
                        break;
                } // end switch

                if (Length > 8)
                    Length = 8;

                switch (Length)
                {
                    case 8:
                        k2 = k2 ^ (uint)(buf[7] << 24);
                        k2 = k2 ^ (uint)(buf[6] << 16);
                        k2 = k2 ^ (uint)(buf[5] << 8);
                        k2 = k2 ^ (uint)(buf[4] << 0);

                        k2 = k2 * C2;
                        k2 = Bits.RotateLeft32(k2, 16);
                        k2 = k2 * C3;
                        h2 = h2 ^ k2;
                        break;

                    case 7:
                        k2 = k2 ^ (uint)(buf[6] << 16);
                        k2 = k2 ^ (uint)(buf[5] << 8);
                        k2 = k2 ^ (uint)(buf[4] << 0);

                        k2 = k2 * C2;
                        k2 = Bits.RotateLeft32(k2, 16);
                        k2 = k2 * C3;
                        h2 = h2 ^ k2;
                        break;

                    case 6:
                        k2 = k2 ^ (uint)(buf[5] << 8);
                        k2 = k2 ^ (uint)(buf[4] << 0);

                        k2 = k2 * C2;
                        k2 = Bits.RotateLeft32(k2, 16);
                        k2 = k2 * C3;
                        h2 = h2 ^ k2;
                        break;

                    case 5:
                        k2 = k2 ^ (uint)(buf[4] << 0);

                        k2 = k2 * C2;
                        k2 = Bits.RotateLeft32(k2, 16);
                        k2 = k2 * C3;
                        h2 = h2 ^ k2;
                        break;
                } // end switch

                if (Length > 4)
                    Length = 4;

                switch (Length)
                {
                    case 4:
                        k1 = k1 ^ (uint)(buf[3] << 24);
                        k1 = k1 ^ (uint)(buf[2] << 16);
                        k1 = k1 ^ (uint)(buf[1] << 8);
                        k1 = k1 ^ (uint)(buf[0] << 0);

                        k1 = k1 * C1;
                        k1 = Bits.RotateLeft32(k1, 15);
                        k1 = k1 * C2;
                        h1 = h1 ^ k1;
                        break;

                    case 3:
                        k1 = k1 ^ (uint)(buf[2] << 16);
                        k1 = k1 ^ (uint)(buf[1] << 8);
                        k1 = k1 ^ (uint)(buf[0] << 0);

                        k1 = k1 * C1;
                        k1 = Bits.RotateLeft32(k1, 15);
                        k1 = k1 * C2;
                        h1 = h1 ^ k1;
                        break;

                    case 2:
                        k1 = k1 ^ (uint)(buf[1] << 8);
                        k1 = k1 ^ (uint)(buf[0] << 0);

                        k1 = k1 * C1;
                        k1 = Bits.RotateLeft32(k1, 15);
                        k1 = k1 * C2;
                        h1 = h1 ^ k1;
                        break;

                    case 1:
                        k1 = k1 ^ (uint)(buf[0] << 0);

                        k1 = k1 * C1;
                        k1 = Bits.RotateLeft32(k1, 15);
                        k1 = k1 * C2;
                        h1 = h1 ^ k1;
                        break;
                } // end switch
            } // end if

            // finalization

            h1 = h1 ^ total_length;
            h2 = h2 ^ total_length;
            h3 = h3 ^ total_length;
            h4 = h4 ^ total_length;

            h1 = h1 + h2;
            h1 = h1 + h3;
            h1 = h1 + h4;
            h2 = h2 + h1;
            h3 = h3 + h1;
            h4 = h4 + h1;

            h1 = h1 ^ (h1 >> 16);
            h1 = h1 * C5;
            h1 = h1 ^ (h1 >> 13);
            h1 = h1 * C6;
            h1 = h1 ^ (h1 >> 16);

            h2 = h2 ^ (h2 >> 16);
            h2 = h2 * C5;
            h2 = h2 ^ (h2 >> 13);
            h2 = h2 * C6;
            h2 = h2 ^ (h2 >> 16);

            h3 = h3 ^ (h3 >> 16);
            h3 = h3 * C5;
            h3 = h3 ^ (h3 >> 13);
            h3 = h3 * C6;
            h3 = h3 ^ (h3 >> 16);

            h4 = h4 ^ (h4 >> 16);
            h4 = h4 * C5;
            h4 = h4 ^ (h4 >> 13);
            h4 = h4 * C6;
            h4 = h4 ^ (h4 >> 16);

            h1 = h1 + h2;
            h1 = h1 + h3;
            h1 = h1 + h4;
            h2 = h2 + h1;
            h3 = h3 + h1;
            h4 = h4 + h1;
        } // end function Finish
    } // end class MurmurHash3_x86_128
}