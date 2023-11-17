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

namespace Yannick.Crypto.SharpHash.Hash32
{
    internal sealed class DJB : Base.Hash, IHash32, ITransformBlock
    {
        private uint hash;

        public DJB()
            : base(4, 1)
        {
        } // end constructor

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new DJB();
            HashInstance.hash = hash;

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone

        public override void Initialize()
        {
            hash = 5381;
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
                hash = ((hash << 5) + hash) + a_data[i];
                i++;
                a_length--;
            } // end while
        } // end function TransformBytes
    } // end class
}