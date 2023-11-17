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

namespace Yannick.Crypto.SharpHash.NullDigest
{
    internal sealed class NullDigest : Base.Hash, ITransformBlock
    {
        private static readonly string HashSizeNotImplemented = "HashSize Not Implemented For \"{0}\"";
        private static readonly string BlockSizeNotImplemented = "BlockSize Not Implemented For \"{0}\"";
        private MemoryStream Out;

        public NullDigest() : base(-1, -1) // Dummy State
        {
            Out = new MemoryStream();
        } // end constructor

        public override int BlockSize
        {
            get { throw new NotImplementedHashLibException(string.Format(BlockSizeNotImplemented, Name)); }
        } // end property BlockSize

        public override int HashSize
        {
            get { throw new NotImplementedHashLibException(string.Format(HashSizeNotImplemented, Name)); }
        } // end property HashSize

        ~NullDigest()
        {
            Out.Flush();
            Out.Close();
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new NullDigest();

            var buf = Out.ToArray();
            HashInstance.Out.Write(buf, 0, buf.Length);

            HashInstance.Out.Position = Out.Position;

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        }

        public override void Initialize()
        {
            Out.Flush();
            Out.SetLength(0); // Reset stream
        } // end function Initialize

        public override IHashResult TransformFinal()
        {
            var size = (int)Out.Length;

            var res = new byte[size];

            try
            {
                Out.Position = 0;
                if (!(res.Length == 0))
                    Out.Read(res, 0, size);
            } // end try
            finally
            {
                Initialize();
            } // end finally

            IHashResult result = new HashResult(res);

            return result;
        } // end function TransformFinal

        public override void TransformBytes(byte[]? a_data, int a_index, int a_length)
        {
            if (!a_data.Empty())
            {
                Out.Write(a_data, a_index, a_length);
            } // end if
        } // end function TransformBytes
    }
}