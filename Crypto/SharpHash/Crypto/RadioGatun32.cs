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

namespace Yannick.Crypto.SharpHash.Crypto
{
    internal sealed class RadioGatun32 : BlockHash, ICryptoNotBuiltIn, ITransformBlock
    {
        private uint[][] belt;
        private uint[] mill;

        public RadioGatun32()
            : base(32, 12)
        {
            mill = new uint[19];

            Array.Resize(ref belt, 13);
            for (var i = 0; i < 13; i++)
                belt[i] = new uint[3];
        } // end constructor

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new RadioGatun32();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.mill = mill.DeepCopy();

            Array.Resize(ref belt, 13);
            for (var i = 0; i < 13; i++)
                Utils.Utils.Memcopy(ref HashInstance.belt[i], belt[i], belt[i].Length);

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone

        public override void Initialize()
        {
            ArrayUtils.ZeroFill(ref mill);

            for (var i = 0; i < 13; i++)
                ArrayUtils.ZeroFill(ref belt[i]);

            base.Initialize();
        } // end function Initialize

        protected override unsafe byte[]? GetResult()
        {
            var tempRes = new uint[8];
            var result = new byte[8 * sizeof(uint)];

            fixed (uint* tPtr = tempRes, millPtr = mill)
            {
                fixed (byte* resultPtr = result)
                {
                    for (var i = 0; i < 4; i++)
                    {
                        RoundFunction();
                        Utils.Utils.Memmove((IntPtr)(tPtr + (i * 2)), (IntPtr)(millPtr + 1), 2 * sizeof(uint));
                    } // end for

                    Converters.le32_copy((IntPtr)tPtr, 0, (IntPtr)resultPtr, 0,
                        result.Length);
                }
            }

            return result;
        } // end function GetResult

        protected override void Finish()
        {
            var padding_size = 12 - (int)(processed_bytes % 12);

            var pad = new byte[padding_size];

            pad[0] = 0x01;

            TransformBytes(pad, 0, padding_size);

            for (var i = 0; i < 16; i++)
                RoundFunction();
        } // end function Finish

        protected override unsafe void TransformBlock(IntPtr a_data,
            int a_data_length, int a_index)
        {
            var data = new uint[3];

            fixed (uint* dataPtr = data)
            {
                Converters.le32_copy(a_data, a_index, (IntPtr)dataPtr, 0, 12);

                var i = 0;
                while (i < 3)
                {
                    mill[i + 16] = mill[i + 16] ^ data[i];
                    belt[0][i] = belt[0][i] ^ data[i];
                    i++;
                } // end while

                RoundFunction();

                ArrayUtils.ZeroFill(ref data);
            }
        } // end function TransformBlock

        private void RoundFunction()
        {
            var a = new uint[19];
            var q = belt[12];

            var i = 12;
            while (i > 0)
            {
                belt[i] = belt[i - 1];
                i--;
            } // end while

            belt[0] = q;

            i = 0;
            while (i < 12)
            {
                belt[i + 1][i % 3] = belt[i + 1][i % 3] ^ mill[i + 1];
                i++;
            } // end while

            i = 0;
            while (i < 19)
            {
                a[i] = mill[i] ^ (mill[(i + 1) % 19] | ~mill[(i + 2) % 19]);
                i++;
            } // end while

            i = 0;
            while (i < 19)
            {
                mill[i] = Bits.RotateRight32(a[(7 * i) % 19], (i * (i + 1)) >> 1);
                i++;
            } // end while

            i = 0;
            while (i < 19)
            {
                a[i] = mill[i] ^ mill[(i + 1) % 19] ^ mill[(i + 4) % 19];
                i++;
            } // end while

            a[0] = a[0] ^ 1;

            i = 0;
            while (i < 19)
            {
                mill[i] = a[i];
                i++;
            } // end while

            i = 0;
            while (i < 3)
            {
                mill[i + 13] = mill[i + 13] ^ q[i];
                i++;
            } // end while
        } // end function RoundFunction
    } // end class RadioGatun32
}