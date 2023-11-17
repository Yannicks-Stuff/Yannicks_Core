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
using Yannick.Crypto.SharpHash.Interfaces.IBlake2BConfigurations;
using Yannick.Crypto.SharpHash.Utils;

namespace Yannick.Crypto.SharpHash.Crypto.Blake2BConfigurations
{
    public sealed class Blake2BConfig : IBlake2BConfig
    {
        public static readonly string InvalidHashSize = "BLAKE2B HashSize must  of the following [1 .. 64], \"{0}\"";

        public static readonly string InvalidKeyLength =
            "\"Key\" Length Must Not Be Greatebe restricted to oner Than 64, \"{0}\"";

        public static readonly string InvalidPersonalisationLength =
            "\"Personalisation\" Length Must Be Equal To 16, \"{0}\"";

        public static readonly string InvalidSaltLength = "\"Salt\" Length Must Be Equal To 16, \"{0}\"";
        private int hash_size;

        private byte[]? key;

        private byte[]? personalisation;

        private byte[]? salt;

        public Blake2BConfig(HashSizeEnum a_hash_size = HashSizeEnum.HashSize512)
        {
            ValidateHashSize((int)a_hash_size);
            hash_size = (int)a_hash_size;
        }

        public Blake2BConfig(int a_hash_size)
        {
            ValidateHashSize(a_hash_size);
            hash_size = a_hash_size;
        }

        public static Blake2BConfig DefaultConfig => new Blake2BConfig();

        public int HashSize
        {
            get => hash_size;
            set
            {
                ValidateHashSize(value);
                hash_size = value;
            }
        }

        public byte[]? Personalisation
        {
            get => personalisation;
            set
            {
                ValidatePersonalisationLength(value);
                personalisation = value;
            }
        }

        public byte[]? Salt
        {
            get => salt;
            set
            {
                ValidateSaltLength(value);
                salt = value;
            }
        }

        public byte[]? Key
        {
            get => key;
            set
            {
                ValidateKeyLength(value);
                key = value;
            }
        }

        public IBlake2BConfig Clone()
        {
            var result = new Blake2BConfig(HashSize);

            result.Key = Key.DeepCopy();
            result.Personalisation = Personalisation.DeepCopy();
            result.Salt = Salt.DeepCopy();

            return result;
        }

        public void Clear()
        {
            ArrayUtils.ZeroFill(ref key);
        }

        ~Blake2BConfig()
        {
            Clear();
        }

        private void ValidateHashSize(int a_hash_size)
        {
            if (!(a_hash_size > 0 && a_hash_size <= 64) || ((a_hash_size * 8) & 7) != 0)
                throw new ArgumentHashLibException(string.Format(InvalidHashSize, a_hash_size));
        }

        private void ValidateKeyLength(byte[]? a_Key)
        {
            int KeyLength;

            if (!a_Key.Empty())
            {
                KeyLength = a_Key.Length;
                if (KeyLength > 64)
                    throw new ArgumentOutOfRangeHashLibException(string.Format(InvalidKeyLength, KeyLength));
            }
        }

        private void ValidatePersonalisationLength(byte[]? a_Personalisation)
        {
            int PersonalisationLength;

            if (!a_Personalisation.Empty())
            {
                PersonalisationLength = a_Personalisation.Length;
                if (PersonalisationLength != 16)
                    throw new ArgumentOutOfRangeHashLibException(string.Format(InvalidPersonalisationLength,
                        PersonalisationLength));
            }
        }

        private void ValidateSaltLength(byte[]? a_Salt)
        {
            int SaltLength;

            if (!a_Salt.Empty())
            {
                SaltLength = a_Salt.Length;
                if (SaltLength != 16)
                    throw new ArgumentOutOfRangeHashLibException(string.Format(InvalidSaltLength, SaltLength));
            }
        }
    } // end class Blake2BConfig
}