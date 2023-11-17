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
    internal sealed class MurmurHash3_x64_128 : Base.Hash, IHash128, IHashWithKey, ITransformBlock
    {
        private static readonly uint CKEY = 0x0;

        private static readonly ulong C1 = 0x87C37B91114253D5;
        private static readonly ulong C5 = 0xFF51AFD7ED558CCD;
        private static readonly ulong C6 = 0xC4CEB9FE1A85EC53;

        private static readonly ulong C2 = 0x4CF5AD432745937F;
        private static readonly uint C3 = 0x52DCE729;
        private static readonly uint C4 = 0x38495AB5;

        private static readonly string InvalidKeyLength = "KeyLength Must Be Equal to {0}";
        private byte[]? buf;
        private ulong h1, h2, total_length;
        private int idx;
        private uint key;

        public MurmurHash3_x64_128()
            : base(16, 16)
        {
            key = CKEY;
            buf = new byte[16];
        } // end constructor

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new MurmurHash3_x64_128
            {
                h1 = h1,
                h2 = h2,
                total_length = total_length,
                key = key,
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

            total_length = 0;
            idx = 0;
        } // end function Initialize

        public override unsafe IHashResult TransformFinal()
        {
            Finish();

            ulong[] tempBufUInt64 = { h1, h2 };
            var tempBufByte = new byte[tempBufUInt64.Length * sizeof(ulong)];

            fixed (ulong* tmpPtr = tempBufUInt64)
            {
                fixed (byte* bPtr = tempBufByte)
                {
                    Converters.be64_copy((IntPtr)tmpPtr, 0, (IntPtr)bPtr, 0, tempBufByte.Length);

                    IHashResult result = new HashResult(tempBufByte);

                    Initialize();

                    return result;
                }
            }
        } // end function TransformFinal

        public override unsafe void TransformBytes(byte[]? a_data, int a_index, int a_length)
        {
            int len, nBlocks, i, offset, lIdx;
            ulong k1, k2;

            len = a_length;
            i = a_index;
            lIdx = 0;
            total_length += (uint)len;

            fixed (byte* ptr_a_data = a_data)
            {
                //consume last pending bytes
                if (idx != 0 && a_length != 0)
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
                    k1 = Converters.ReadBytesAsUInt64LE((IntPtr)ptr_a_data, a_index + lIdx);
                    lIdx += 8;

                    k2 = Converters.ReadBytesAsUInt64LE((IntPtr)ptr_a_data, a_index + lIdx);
                    lIdx += 8;

                    k1 = k1 * C1;
                    k1 = Bits.RotateLeft64(k1, 31);
                    k1 = k1 * C2;
                    h1 = h1 ^ k1;

                    h1 = Bits.RotateLeft64(h1, 27);
                    h1 = h1 + h2;
                    h1 = h1 * 5 + C3;

                    k2 = k2 * C2;
                    k2 = Bits.RotateLeft64(k2, 33);
                    k2 = k2 * C1;
                    h2 = h2 ^ k2;

                    h2 = Bits.RotateLeft64(h2, 31);
                    h2 = h2 + h1;
                    h2 = h2 * 5 + C4;

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
                    if (value.Length != KeyLength)
                        throw new ArgumentHashLibException(string.Format(InvalidKeyLength, KeyLength));

                    unsafe
                    {
                        fixed (byte* bPtr = value)
                        {
                            key = Converters.ReadBytesAsUInt32LE((IntPtr)bPtr, 0);
                        }
                    }
                } // end else
            }
        } // end property Key

        private void ByteUpdate(byte a_b)
        {
            buf[idx++] = a_b;
            ProcessPendings();
        } // end function ByteUpdate

        private unsafe void ProcessPendings()
        {
            ulong k1, k2;

            fixed (byte* ptr_Fm_buf = buf)
            {
                if (idx >= 16)
                {
                    k1 = Converters.ReadBytesAsUInt64LE((IntPtr)ptr_Fm_buf, 0);
                    k2 = Converters.ReadBytesAsUInt64LE((IntPtr)ptr_Fm_buf, 8);

                    k1 = k1 * C1;
                    k1 = Bits.RotateLeft64(k1, 31);
                    k1 = k1 * C2;
                    h1 = h1 ^ (uint)k1;

                    h1 = (uint)Bits.RotateLeft64(h1, 27);
                    h1 = h1 + h2;
                    h1 = h1 * 5 + C3;

                    k2 = k2 * C2;
                    k2 = (uint)Bits.RotateLeft64(k2, 33);
                    k2 = k2 * C1;
                    h2 = h2 ^ (uint)k2;

                    h2 = (uint)Bits.RotateLeft64(h2, 31);
                    h2 = h2 + h1;
                    h2 = h2 * 5 + C4;

                    idx = 0;
                } // end if
            }
        } // end function ProcessPendings

        private void Finish()
        {
            ulong k1, k2;
            int Length;

            // tail
            k1 = 0;
            k2 = 0;

            Length = idx;
            if (Length != 0)
            {
                switch (Length)
                {
                    case 15:
                        k2 = k2 ^ ((ulong)buf[14] << 48);
                        k2 = k2 ^ ((ulong)buf[13] << 40);
                        k2 = k2 ^ ((ulong)buf[12] << 32);
                        k2 = k2 ^ ((ulong)buf[11] << 24);
                        k2 = k2 ^ ((ulong)buf[10] << 16);
                        k2 = k2 ^ ((ulong)buf[9] << 8);
                        k2 = k2 ^ ((ulong)buf[8] << 0);
                        k2 = k2 * C2;
                        k2 = Bits.RotateLeft64(k2, 33);
                        k2 = k2 * C1;
                        h2 = h2 ^ k2;
                        break;

                    case 14:
                        k2 = k2 ^ ((ulong)buf[13] << 40);
                        k2 = k2 ^ ((ulong)buf[12] << 32);
                        k2 = k2 ^ ((ulong)buf[11] << 24);
                        k2 = k2 ^ ((ulong)buf[10] << 16);
                        k2 = k2 ^ ((ulong)buf[9] << 8);
                        k2 = k2 ^ ((ulong)buf[8] << 0);
                        k2 = k2 * C2;
                        k2 = Bits.RotateLeft64(k2, 33);
                        k2 = k2 * C1;
                        h2 = h2 ^ k2;
                        break;

                    case 13:
                        k2 = k2 ^ ((ulong)buf[12] << 32);
                        k2 = k2 ^ ((ulong)buf[11] << 24);
                        k2 = k2 ^ ((ulong)buf[10] << 16);
                        k2 = k2 ^ ((ulong)buf[9] << 8);
                        k2 = k2 ^ ((ulong)buf[8] << 0);
                        k2 = k2 * C2;
                        k2 = Bits.RotateLeft64(k2, 33);
                        k2 = k2 * C1;
                        h2 = h2 ^ k2;
                        break;

                    case 12:
                        k2 = k2 ^ ((ulong)buf[11] << 24);
                        k2 = k2 ^ ((ulong)buf[10] << 16);
                        k2 = k2 ^ ((ulong)buf[9] << 8);
                        k2 = k2 ^ ((ulong)buf[8] << 0);
                        k2 = k2 * C2;
                        k2 = Bits.RotateLeft64(k2, 33);
                        k2 = k2 * C1;
                        h2 = h2 ^ k2;
                        break;

                    case 11:
                        k2 = k2 ^ ((ulong)buf[10] << 16);
                        k2 = k2 ^ ((ulong)buf[9] << 8);
                        k2 = k2 ^ ((ulong)buf[8] << 0);
                        k2 = k2 * C2;
                        k2 = Bits.RotateLeft64(k2, 33);
                        k2 = k2 * C1;
                        h2 = h2 ^ k2;
                        break;

                    case 10:
                        k2 = k2 ^ ((ulong)buf[9] << 8);
                        k2 = k2 ^ ((ulong)buf[8] << 0);
                        k2 = k2 * C2;
                        k2 = Bits.RotateLeft64(k2, 33);
                        k2 = k2 * C1;
                        h2 = h2 ^ k2;
                        break;

                    case 9:
                        k2 = k2 ^ ((ulong)buf[8] << 0);
                        k2 = k2 * C2;
                        k2 = Bits.RotateLeft64(k2, 33);
                        k2 = k2 * C1;
                        h2 = h2 ^ k2;
                        break;
                } // end switch

                if (Length > 8)
                    Length = 8;

                switch (Length)
                {
                    case 8:
                        k1 = k1 ^ ((ulong)buf[7] << 56);
                        k1 = k1 ^ ((ulong)buf[6] << 48);
                        k1 = k1 ^ ((ulong)buf[5] << 40);
                        k1 = k1 ^ ((ulong)buf[4] << 32);
                        k1 = k1 ^ ((ulong)buf[3] << 24);
                        k1 = k1 ^ ((ulong)buf[2] << 16);
                        k1 = k1 ^ ((ulong)buf[1] << 8);
                        k1 = k1 ^ ((ulong)buf[0] << 0);
                        k1 = k1 * C1;
                        k1 = Bits.RotateLeft64(k1, 31);
                        k1 = k1 * C2;
                        h1 = h1 ^ k1;
                        break;

                    case 7:
                        k1 = k1 ^ ((ulong)buf[6] << 48);
                        k1 = k1 ^ ((ulong)buf[5] << 40);
                        k1 = k1 ^ ((ulong)buf[4] << 32);
                        k1 = k1 ^ ((ulong)buf[3] << 24);
                        k1 = k1 ^ ((ulong)buf[2] << 16);
                        k1 = k1 ^ ((ulong)buf[1] << 8);
                        k1 = k1 ^ ((ulong)buf[0] << 0);
                        k1 = k1 * C1;
                        k1 = Bits.RotateLeft64(k1, 31);
                        k1 = k1 * C2;
                        h1 = h1 ^ k1;
                        break;

                    case 6:
                        k1 = k1 ^ ((ulong)buf[5] << 40);
                        k1 = k1 ^ ((ulong)buf[4] << 32);
                        k1 = k1 ^ ((ulong)buf[3] << 24);
                        k1 = k1 ^ ((ulong)buf[2] << 16);
                        k1 = k1 ^ ((ulong)buf[1] << 8);
                        k1 = k1 ^ ((ulong)buf[0] << 0);
                        k1 = k1 * C1;
                        k1 = Bits.RotateLeft64(k1, 31);
                        k1 = k1 * C2;
                        h1 = h1 ^ k1;
                        break;

                    case 5:
                        k1 = k1 ^ ((ulong)buf[4] << 32);
                        k1 = k1 ^ ((ulong)buf[3] << 24);
                        k1 = k1 ^ ((ulong)buf[2] << 16);
                        k1 = k1 ^ ((ulong)buf[1] << 8);
                        k1 = k1 ^ ((ulong)buf[0] << 0);
                        k1 = k1 * C1;
                        k1 = Bits.RotateLeft64(k1, 31);
                        k1 = k1 * C2;
                        h1 = h1 ^ k1;
                        break;

                    case 4:
                        k1 = k1 ^ ((ulong)buf[3] << 24);
                        k1 = k1 ^ ((ulong)buf[2] << 16);
                        k1 = k1 ^ ((ulong)buf[1] << 8);
                        k1 = k1 ^ ((ulong)buf[0] << 0);
                        k1 = k1 * C1;
                        k1 = Bits.RotateLeft64(k1, 31);
                        k1 = k1 * C2;
                        h1 = h1 ^ k1;
                        break;

                    case 3:
                        k1 = k1 ^ ((ulong)buf[2] << 16);
                        k1 = k1 ^ ((ulong)buf[1] << 8);
                        k1 = k1 ^ ((ulong)buf[0] << 0);
                        k1 = k1 * C1;
                        k1 = Bits.RotateLeft64(k1, 31);
                        k1 = k1 * C2;
                        h1 = h1 ^ k1;
                        break;

                    case 2:
                        k1 = k1 ^ ((ulong)buf[1] << 8);
                        k1 = k1 ^ ((ulong)buf[0] << 0);
                        k1 = k1 * C1;
                        k1 = Bits.RotateLeft64(k1, 31);
                        k1 = k1 * C2;
                        h1 = h1 ^ k1;
                        break;

                    case 1:
                        k1 = k1 ^ ((ulong)buf[0] << 0);
                        k1 = k1 * C1;
                        k1 = Bits.RotateLeft64(k1, 31);
                        k1 = k1 * C2;
                        h1 = h1 ^ k1;
                        break;
                } // end switch
            } // end if

            // finalization

            h1 = h1 ^ total_length;
            h2 = h2 ^ total_length;

            h1 = h1 + h2;
            h2 = h2 + h1;

            h1 = h1 ^ (h1 >> 33);
            h1 = h1 * C5;
            h1 = h1 ^ (h1 >> 33);
            h1 = h1 * C6;
            h1 = h1 ^ (h1 >> 33);

            h2 = h2 ^ (h2 >> 33);
            h2 = h2 * C5;
            h2 = h2 ^ (h2 >> 33);
            h2 = h2 * C6;
            h2 = h2 ^ (h2 >> 33);

            h1 = h1 + h2;
            h2 = h2 + h1;
        } // end function Finish
    } // end class MurmurHash3_x64_128
}