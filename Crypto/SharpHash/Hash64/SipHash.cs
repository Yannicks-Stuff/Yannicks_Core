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

namespace Yannick.Crypto.SharpHash.Hash64
{
    internal abstract class SipHash : Base.Hash, IHash64, IHashWithKey, ITransformBlock
    {
        private static readonly ulong V0 = 0x736F6D6570736575;
        private static readonly ulong V1 = 0x646F72616E646F6D;
        private static readonly ulong V2 = 0x6C7967656E657261;
        private static readonly ulong V3 = 0x7465646279746573;
        private static readonly ulong KEY0 = 0x0706050403020100;
        private static readonly ulong KEY1 = 0x0F0E0D0C0B0A0908;

        private static readonly string InvalidKeyLength = "KeyLength Must Be Equal to {0}";
        protected byte[]? buf;
        protected int cr, fr, idx;
        protected ulong v0, v1, v2, v3, key0, key1, total_length, m;

        public SipHash(int a_compression_rounds = 2, int a_finalization_rounds = 4)
            : base(8, 8)
        {
            key0 = KEY0;
            key1 = KEY1;
            cr = a_compression_rounds;
            fr = a_finalization_rounds;
            Array.Resize(ref buf, 8);
        } // end constructor

        public override void Initialize()
        {
            v0 = V0;
            v1 = V1;
            v2 = V2;
            v3 = V3;
            total_length = 0;
            idx = 0;

            v3 = v3 ^ key1;
            v2 = v2 ^ key0;
            v1 = v1 ^ key1;
            v0 = v0 ^ key0;
        } // end function Initialize

        public override unsafe void TransformBytes(byte[]? a_data, int a_index, int a_length)
        {
            if (buf == null || a_data == null)
                throw new NullReferenceException();

            int i, Length, iter, offset;

            Length = a_length;
            i = a_index;

            total_length += (uint)Length;

            fixed (byte* ptr_a_data = a_data, ptr_Fm_buf = buf)
            {
                // consume last pending bytes

                if ((idx != 0) && (a_length != 0))
                {
                    while ((idx < 8) && (Length != 0))
                    {
                        buf[idx] = *(ptr_a_data + a_index);
                        idx++;
                        a_index++;
                        Length--;
                    } // end while

                    if (idx == 8)
                    {
                        m = Converters.ReadBytesAsUInt64LE((IntPtr)ptr_Fm_buf, 0);
                        ProcessBlock(m);
                        idx = 0;
                    } // end if
                } // end if
                else
                {
                    i = 0;
                } // end else

                iter = Length >> 3;

                // body

                while (i < iter)
                {
                    m = Converters.ReadBytesAsUInt64LE((IntPtr)ptr_a_data, a_index + (i * 8));
                    ProcessBlock(m);
                    i++;
                } // end while

                // save pending end bytes
                offset = a_index + (i * 8);

                while (offset < (Length + a_index))
                {
                    ByteUpdate(a_data[offset]);
                    offset++;
                } // end while
            }
        } // end function TransformBytes

        public override IHashResult TransformFinal()
        {
            Finish();

            var BufferByte = new byte[HashSize];
            Converters.ReadUInt64AsBytesLE(v0 ^ v1 ^ v2 ^ v3, ref BufferByte, 0);

            IHashResult result = new HashResult(BufferByte);

            Initialize();

            return result;
        } // end function TransformFinal

        public virtual int? KeyLength
        {
            get => 16;
        } // end property KeyLength

        public virtual unsafe byte[]? Key
        {
            get
            {
                if (KeyLength == null)
                    return null;

                var LKey = new byte[(int)KeyLength];

                Converters.ReadUInt64AsBytesLE(key0, ref LKey, 0);
                Converters.ReadUInt64AsBytesLE(key1, ref LKey, 8);

                return LKey;
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    key0 = KEY0;
                    key1 = KEY1;
                } // end if
                else
                {
                    if (value.Length != KeyLength)
                        throw new ArgumentHashLibException(string.Format(InvalidKeyLength, KeyLength));

                    fixed (byte* bPtr = &value[0])
                    {
                        key0 = Converters.ReadBytesAsUInt64LE((IntPtr)bPtr, 0);
                        key1 = Converters.ReadBytesAsUInt64LE((IntPtr)bPtr, 8);
                    }
                } // end else
            }
        } // end property Key

        private void Compress()
        {
            v0 = v0 + v1;
            v2 = v2 + v3;
            v1 = Bits.RotateLeft64(v1, 13);
            v3 = Bits.RotateLeft64(v3, 16);
            v1 = v1 ^ v0;
            v3 = v3 ^ v2;
            v0 = Bits.RotateLeft64(v0, 32);
            v2 = v2 + v1;
            v0 = v0 + v3;
            v1 = Bits.RotateLeft64(v1, 17);
            v3 = Bits.RotateLeft64(v3, 21);
            v1 = v1 ^ v2;
            v3 = v3 ^ v0;
            v2 = Bits.RotateLeft64(v2, 32);
        } // end function Compress

        private void CompressTimes(int a_times)
        {
            var i = 0;

            while (i < a_times)
            {
                Compress();
                i++;
            } // end while
        } // end function CompressTimes

        private void ProcessBlock(ulong a_m)
        {
            v3 = v3 ^ a_m;
            CompressTimes(cr);
            v0 = v0 ^ a_m;
        } // end function ProcessBlock

        private unsafe void ByteUpdate(byte a_b)
        {
            if (buf == null)
                throw new NullReferenceException();

            buf[idx] = a_b;
            idx++;
            if (idx >= 8)
            {
                fixed (byte* ptr_Fm_buf = buf)
                {
                    var m = Converters.ReadBytesAsUInt64LE((IntPtr)ptr_Fm_buf, 0);
                    ProcessBlock(m);
                    idx = 0;
                }
            } // end if
        } // end function ByteUpdate

        private void Finish()
        {
            if (buf == null)
                throw new NullReferenceException();

            var b = (total_length & 0xFF) << 56;

            if (idx != 0)
            {
                switch (idx)
                {
                    case 7:
                        b = b | ((ulong)(buf[6]) << 48);
                        b = b | ((ulong)(buf[5]) << 40);
                        b = b | ((ulong)(buf[4]) << 32);
                        b = b | ((ulong)(buf[3]) << 24);
                        b = b | ((ulong)(buf[2]) << 16);
                        b = b | ((ulong)(buf[1]) << 8);
                        b = b | buf[0];
                        break;

                    case 6:
                        b = b | ((ulong)(buf[5]) << 40);
                        b = b | ((ulong)(buf[4]) << 32);
                        b = b | ((ulong)(buf[3]) << 24);
                        b = b | ((ulong)(buf[2]) << 16);
                        b = b | ((ulong)(buf[1]) << 8);
                        b = b | buf[0];
                        break;

                    case 5:
                        b = b | ((ulong)(buf[4]) << 32);
                        b = b | ((ulong)(buf[3]) << 24);
                        b = b | ((ulong)(buf[2]) << 16);
                        b = b | ((ulong)(buf[1]) << 8);
                        b = b | buf[0];
                        break;

                    case 4:
                        b = b | ((ulong)(buf[3]) << 24);
                        b = b | ((ulong)(buf[2]) << 16);
                        b = b | ((ulong)(buf[1]) << 8);
                        b = b | buf[0];
                        break;

                    case 3:
                        b = b | ((ulong)(buf[2]) << 16);
                        b = b | ((ulong)(buf[1]) << 8);
                        b = b | buf[0];
                        break;

                    case 2:
                        b = b | ((ulong)(buf[1]) << 8);
                        b = b | buf[0];
                        break;

                    case 1:
                        b = b | buf[0];
                        break;
                } // end switch
            } // end if

            v3 = v3 ^ b;
            CompressTimes(cr);
            v0 = v0 ^ b;
            v2 = v2 ^ 0xFF;
            CompressTimes(fr);
        } // end function Finish
    } // end class SipHash

    /// <summary>
    /// SipHash 2 - 4 algorithm.
    /// <summary>
    internal sealed class SipHash2_4 : SipHash
    {
        // end constructor

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new SipHash2_4
            {
                v0 = v0,
                v1 = v1,
                v2 = v2,
                v3 = v3,
                key0 = key0,
                key1 = key1,
                total_length = total_length,
                cr = cr,
                fr = fr,
                idx = idx,
                buf = buf.DeepCopy(),
                BufferSize = BufferSize
            };

            return HashInstance;
        } // end function Clone
    } // end class SipHash2_4
}