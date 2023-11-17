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

using Yannick.Crypto.SharpHash.Interfaces;
using Yannick.Crypto.SharpHash.Utils;

namespace Yannick.Crypto.SharpHash.Base
{
    internal class HMACNotBuildInAdapter : Hash, IHMACNotBuiltIn, ICryptoNotBuiltIn
    {
        private Interfaces.IHash? hash;
        private byte[]? ipad;
        private byte[]? key;
        private byte[]? opad;
        private byte[]? workingKey;

        private HMACNotBuildInAdapter(Interfaces.IHash? a_underlyingHash)
            : base(a_underlyingHash.HashSize, a_underlyingHash.BlockSize)
        {
            hash = a_underlyingHash;
        } // end constructor

        private HMACNotBuildInAdapter(Interfaces.IHash a_underlyingHash, byte[]? a_HMACKey)
            : base(a_underlyingHash.HashSize, a_underlyingHash.BlockSize)
        {
            hash = a_underlyingHash.Clone();
            Key = a_HMACKey;
            ipad = new byte[hash.BlockSize];
            opad = new byte[hash.BlockSize];
        } // end constructor

        public void Clear()
        {
            ArrayUtils.ZeroFill(ref key);
            ArrayUtils.ZeroFill(ref workingKey);
        } // end function Clear

        public override Interfaces.IHash? Clone()
        {
            var hmac = new HMACNotBuildInAdapter(hash.Clone());

            hmac.opad = opad.DeepCopy();
            hmac.ipad = ipad.DeepCopy();
            hmac.key = key.DeepCopy();
            hmac.workingKey = workingKey.DeepCopy();

            hmac.BufferSize = BufferSize;

            return hmac;
        }

        public override void Initialize()
        {
            hash.Initialize();
            UpdatePads();
            hash.TransformBytes(ipad);
        } // end function Initialize

        public override IHashResult TransformFinal()
        {
            var result = hash.TransformFinal();
            hash.TransformBytes(opad);
            hash.TransformBytes(result.GetBytes());
            result = hash.TransformFinal();
            Initialize();

            return result;
        } // end function TransformFinal

        public override void TransformBytes(byte[]? a_data, int a_index, int a_length)
        {
            hash.TransformBytes(a_data, a_index, a_length);
        } // end function TransformBytes

        public override string Name => $"HMACNotBuiltIn({hash.Name})";

        public byte[]? Key
        {
            get => key.DeepCopy();
            set
            {
                if (value == null) throw new ArgumentNullHashLibException(nameof(value));
                key = value.DeepCopy();
                TransformKey();
            }
        }

        public byte[]? WorkingKey
        {
            get => workingKey.DeepCopy();
            private set => workingKey = value != null
                ? value.DeepCopy()
                : throw new ArgumentNullHashLibException(nameof(value));
        }

        public static IHMACNotBuiltIn CreateHMAC(Interfaces.IHash a_Hash, byte[]? a_HMACKey)
        {
            if (a_HMACKey == null) throw new ArgumentNullHashLibException(nameof(a_HMACKey));
            if (a_Hash == null) throw new ArgumentNullHashLibException(nameof(a_Hash));

            if (a_Hash is IHMACNotBuiltIn hmacNotBuiltIn) return (IHMACNotBuiltIn)hmacNotBuiltIn.Clone();

            return new HMACNotBuildInAdapter(a_Hash, a_HMACKey);
        } //

        public override string ToString() => Name;

        protected void UpdatePads()
        {
            var Idx = 0;
            var blockSize = hash.BlockSize;
            var length = workingKey.Length;

            ArrayUtils.Fill(ref ipad, 0, blockSize, 0x36);
            ArrayUtils.Fill(ref opad, 0, blockSize, 0x5C);

            while (Idx < length && Idx < blockSize)
            {
                ipad[Idx] = (byte)(ipad[Idx] ^ workingKey[Idx]);
                opad[Idx] = (byte)(opad[Idx] ^ workingKey[Idx]);
                Idx++;
            } // end while
        } // end function UpdatePads

        /// <summary>
        /// Computes the actual key used for hashing. This will not be the same as the
        /// original key passed to TransformKey() if the original key exceeds the <br />
        /// hash algorithm's block size. (See RFC 2104, section 2)
        /// </summary>
        private void TransformKey()
        {
            var blockSize = hash.BlockSize;
            // Perform RFC 2104, section 2 key adjustment.
            WorkingKey = key.Length > blockSize ? hash.ComputeBytes(key).GetBytes() : key;
        } // end function TransformKey
    }
}