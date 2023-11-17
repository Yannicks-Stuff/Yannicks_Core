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
    internal sealed class Murmur2_64 : MultipleTransformNonBlock, IHash64, IHashWithKey, ITransformBlock
    {
        private static readonly ulong CKEY = 0x0;
        private static readonly ulong M = 0xC6A4A7935BD1E995;
        private static readonly int R = 47;

        private static readonly string InvalidKeyLength = "KeyLength Must Be Equal to {0}";
        private ulong key, working_key;

        public Murmur2_64()
            : base(8, 8)
        {
            key = CKEY;
        } // end constructor

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Murmur2_64
            {
                key = key,
                working_key = working_key,
                Buffer = new MemoryStream()
            };

            var buf = Buffer.ToArray();
            HashInstance.Buffer.Write(buf, 0, buf.Length);
            HashInstance.Buffer.Position = Buffer.Position;

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone

        public override void Initialize()
        {
            working_key = key;
            base.Initialize();
        } // end function Initialize

        public int? KeyLength => 8;

        public byte[]? Key
        {
            get => Converters.ReadUInt64AsBytesLE(key);

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
                        fixed (byte* bPtr = &value[0])
                        {
                            key = Converters.ReadBytesAsUInt64LE((IntPtr)bPtr, 0);
                        }
                    }
                } // end else
            }
        } // end property Key

        protected override unsafe IHashResult ComputeAggregatedBytes(byte[]? a_data)
        {
            int Length, current_index;
            ulong k, h;

            if (a_data.Empty())
                return new HashResult((ulong)0);

            Length = a_data.Length;

            fixed (byte* ptr_a_data = a_data)
            {
                h = working_key ^ ((ulong)Length * M);
                current_index = 0;

                while (Length >= 8)
                {
                    k = Converters.ReadBytesAsUInt64LE((IntPtr)ptr_a_data, current_index);

                    k = k * M;
                    k = k ^ (k >> R);
                    k = k * M;

                    h = h ^ k;
                    h = h * M;

                    current_index += 8;
                    Length -= 8;
                } // end while

                switch (Length)
                {
                    case 7:
                        h = h ^ (((ulong)(a_data[current_index + 6]) << 48));

                        h = h ^ ((ulong)(a_data[current_index + 5]) << 40);

                        h = h ^ ((ulong)(a_data[current_index + 4]) << 32);

                        h = h ^ ((ulong)(a_data[current_index + 3]) << 24);

                        h = h ^ ((ulong)(a_data[current_index + 2]) << 16);

                        h = h ^ ((ulong)(a_data[current_index + 1]) << 8);

                        h = h ^ a_data[current_index];

                        h = h * M;
                        break;

                    case 6:
                        h = h ^ ((ulong)(a_data[current_index + 5]) << 40);

                        h = h ^ ((ulong)(a_data[current_index + 4]) << 32);

                        h = h ^ ((ulong)(a_data[current_index + 3]) << 24);

                        h = h ^ ((ulong)(a_data[current_index + 2]) << 16);

                        h = h ^ ((ulong)(a_data[current_index + 1]) << 8);

                        h = h ^ a_data[current_index];

                        h = h * M;
                        break;

                    case 5:
                        h = h ^ ((ulong)(a_data[current_index + 4]) << 32);

                        h = h ^ ((ulong)(a_data[current_index + 3]) << 24);

                        h = h ^ ((ulong)(a_data[current_index + 2]) << 16);

                        h = h ^ ((ulong)(a_data[current_index + 1]) << 8);

                        h = h ^ a_data[current_index];

                        h = h * M;
                        break;

                    case 4:
                        h = h ^ ((ulong)(a_data[current_index + 3]) << 24);

                        h = h ^ ((ulong)(a_data[current_index + 2]) << 16);

                        h = h ^ ((ulong)(a_data[current_index + 1]) << 8);

                        h = h ^ a_data[current_index];

                        h = h * M;
                        break;

                    case 3:
                        h = h ^ ((ulong)(a_data[current_index + 2]) << 16);

                        h = h ^ ((ulong)(a_data[current_index + 1]) << 8);

                        h = h ^ a_data[current_index];

                        h = h * M;
                        break;

                    case 2:
                        h = h ^ ((ulong)(a_data[current_index + 1]) << 8);

                        h = h ^ a_data[current_index];

                        h = h * M;
                        break;

                    case 1:
                        h = h ^ a_data[current_index];

                        h = h * M;
                        break;
                } // end switch

                h = h ^ (h >> R);
                h = h * M;
                h = h ^ (h >> R);
            }

            return new HashResult(h);
        } // end function ComputeAggregatedBytes
    } // end class Murmur2_64
}