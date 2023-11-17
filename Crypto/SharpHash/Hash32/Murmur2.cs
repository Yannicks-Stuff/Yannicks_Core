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

namespace Yannick.Crypto.SharpHash.Hash32
{
    internal sealed class Murmur2 : MultipleTransformNonBlock, IHash32, IHashWithKey, ITransformBlock
    {
        private static readonly uint CKEY = 0x0;
        private static readonly uint M = 0x5BD1E995;
        private static readonly int R = 24;

        private static readonly string InvalidKeyLength = "KeyLength Must Be Equal to {0}";
        private uint key, working_key, h;

        public Murmur2()
            : base(4, 4)
        {
        } // end constructor

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Murmur2();
            HashInstance.key = key;
            HashInstance.working_key = working_key;
            HashInstance.h = h;

            HashInstance.Buffer = new MemoryStream();
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
                        fixed (byte* bPtr = &value[0])
                        {
                            key = Converters.ReadBytesAsUInt32LE((IntPtr)bPtr, 0);
                        }
                    }
                } // end else
            }
        } // end property Key

        protected override IHashResult ComputeAggregatedBytes(byte[]? a_data)
        {
            return new HashResult(InternalComputeBytes(a_data));
        } // end function ComputeAggregatedBytes

        private int InternalComputeBytes(byte[]? a_data)
        {
            int Length, current_index;
            uint k;

            if (a_data.Empty())
                return 0;

            Length = a_data.Length;

            h = working_key ^ (uint)Length;
            current_index = 0;

            unsafe
            {
                fixed (byte* ptr_a_data = a_data)
                {
                    while (Length >= 4)
                    {
                        k = Converters.ReadBytesAsUInt32LE((IntPtr)ptr_a_data, current_index);

                        TransformUInt32Fast(k);
                        current_index += 4;
                        Length -= 4;
                    } // end while

                    switch (Length)
                    {
                        case 3:
                            h = h ^ (uint)(a_data[current_index + 2] << 16);
                            h = h ^ (uint)(a_data[current_index + 1] << 8);
                            h = h ^ (a_data[current_index]);
                            h = h * M;
                            break;

                        case 2:
                            h = h ^ (uint)(a_data[current_index + 1] << 8);
                            h = h ^ (a_data[current_index]);
                            h = h * M;
                            break;

                        case 1:
                            h = h ^ (a_data[current_index]);
                            h = h * M;
                            break;
                    } // end switch
                }
            }

            h = h ^ (h >> 13);

            h = h * M;
            h = h ^ (h >> 15);

            return (int)h;
        } // end function InternalComputeBytes

        private void TransformUInt32Fast(uint a_data)
        {
            a_data = a_data * M;
            a_data = a_data ^ (a_data >> R);
            a_data = a_data * M;

            h = h * M;
            h = h ^ a_data;
        } // end function TransformUInt32Fast
    } // end class Murmur2
}