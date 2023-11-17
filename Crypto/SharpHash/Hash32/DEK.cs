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
    internal sealed class DEK : MultipleTransformNonBlock, IHash32, ITransformBlock
    {
        public DEK()
            : base(4, 1)
        {
        } // end constructor

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new DEK();

            HashInstance.Buffer = new MemoryStream();
            var buf = Buffer.ToArray();
            HashInstance.Buffer.Write(buf, 0, buf.Length);
            HashInstance.Buffer.Position = Buffer.Position;

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone

        protected override IHashResult ComputeAggregatedBytes(byte[]? a_data)
        {
            uint hash = 0;

            if (!a_data.Empty())
            {
                hash = (uint)a_data.Length;

                for (var i = 0; i < a_data.Length; i++)
                    hash = Bits.RotateLeft32(hash, 5) ^ a_data[i];
            } // end if

            return new HashResult(hash);
        } // end function ComputeAggregatedBytes
    } // end class DEK
}