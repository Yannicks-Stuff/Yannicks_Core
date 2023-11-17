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

namespace Yannick.Crypto.SharpHash.Hash64
{
    internal sealed class FNV1a64 : Base.Hash, IHash64, ITransformBlock
    {
        private ulong hash;

        public FNV1a64()
            : base(8, 1)
        {
        } // end constructor

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new FNV1a64
            {
                hash = hash,
                BufferSize = BufferSize
            };

            return HashInstance;
        } // end function Clone

        public override void Initialize()
        {
            hash = 14695981039346656037;
        } // end function Initialize

        public override IHashResult TransformFinal()
        {
            IHashResult result = new HashResult(hash);

            Initialize();

            return result;
        } // end function TransformFinal

        public override void TransformBytes(byte[]? a_data, int a_index, int a_length)
        {
            var i = a_index;

            while (a_length > 0)
            {
                hash = (hash ^ a_data[i]) * 1099511628211;
                i++;
                a_length--;
            } // end while
        } // end function TransformBytes
    } // end class FNV1a64
}