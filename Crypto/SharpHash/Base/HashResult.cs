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
    public sealed class HashResult : IHashResult
    {
        private static readonly string ImpossibleRepresentationInt32 =
            "Current Data Structure cannot be Represented as an 'Int32' Type.";

        private static readonly string ImpossibleRepresentationUInt8 =
            "Current Data Structure cannot be Represented as an 'UInt8' Type.";

        private static readonly string ImpossibleRepresentationUInt16 =
            "Current Data Structure cannot be Represented as an 'UInt16' Type.";

        private static readonly string ImpossibleRepresentationUInt32 =
            "Current Data Structure cannot be Represented as an 'UInt32' Type.";

        private static readonly string ImpossibleRepresentationUInt64 =
            "Current Data Structure cannot be Represented as an 'UInt64' Type.";

        private byte[]? hash;

        public HashResult()
        {
            hash = new byte[0];
        } // end constructor

        public HashResult(ulong a_hash)
        {
            hash = new byte[8];

            hash[0] = (byte)(a_hash >> 56);
            hash[1] = (byte)(a_hash >> 48);
            hash[2] = (byte)(a_hash >> 40);
            hash[3] = (byte)(a_hash >> 32);
            hash[4] = (byte)(a_hash >> 24);
            hash[5] = (byte)(a_hash >> 16);
            hash[6] = (byte)(a_hash >> 8);
            hash[7] = (byte)(a_hash);
        } // end constructor

        public HashResult(byte[]? a_hash)
        {
            hash = a_hash.DeepCopy();
        } // end constructor

        public HashResult(uint a_hash)
        {
            hash = new byte[4];

            hash[0] = (byte)(a_hash >> 24);
            hash[1] = (byte)(a_hash >> 16);
            hash[2] = (byte)(a_hash >> 8);
            hash[3] = (byte)(a_hash);
        } // end constructor

        public HashResult(byte a_hash)
        {
            hash = new byte[1];
            hash[0] = a_hash;
        } // end constructor

        public HashResult(ushort a_hash)
        {
            hash = new byte[2];

            hash[0] = (byte)(a_hash >> 8);
            hash[1] = (byte)(a_hash);
        } // end constructor

        public HashResult(int a_hash)
        {
            hash = new byte[4];

            hash[0] = (byte)(Bits.Asr32(a_hash, 24));
            hash[1] = (byte)(Bits.Asr32(a_hash, 16));
            hash[2] = (byte)(Bits.Asr32(a_hash, 8));
            hash[3] = (byte)(a_hash);
        } // end constructor

        // Copy Constructor
        public HashResult(HashResult right)
        {
            hash = right.hash.DeepCopy();
        }

        public bool CompareTo(IHashResult a_hashResult)
        {
            return SlowEquals(a_hashResult.GetBytes(), hash);
        } // end function CompareTo

        public byte[]? GetBytes()
        {
            return hash.DeepCopy();
        } // end function GetBytes

        public override int GetHashCode()
        {
            var Temp = Convert.ToBase64String(hash);

            uint LResult = 0;
            int I = 0, Top = Temp.Length;

            while (I < Top)
            {
                LResult = Bits.RotateLeft32(LResult, 5);
                LResult = (LResult ^ Temp[I]);
                I += 1;
            } // end while

            return (int)LResult;
        } // end function GetHashCode

        public int GetInt32()
        {
            if (hash.Length != 4)
                throw new InvalidOperationHashLibException(ImpossibleRepresentationInt32);

            return (hash[0] << 24) | (hash[1] << 16) | (hash[2] << 8) | hash[3];
        } // end function GetInt32

        public byte GetUInt8()
        {
            if (hash.Length != 1)
                throw new InvalidOperationHashLibException(ImpossibleRepresentationUInt8);

            return hash[0];
        } // end function GetUInt8

        public ushort GetUInt16()
        {
            if (hash.Length != 2)
                throw new InvalidOperationHashLibException(ImpossibleRepresentationUInt16);

            return (ushort)((hash[0] << 8) | hash[1]);
        } // end function GetUInt16

        public uint GetUInt32()
        {
            if (hash.Length != 4)
                throw new InvalidOperationHashLibException(ImpossibleRepresentationUInt32);

            return (uint)((hash[0] << 24) | (hash[1] << 16) | (hash[2] << 8) | hash[3]);
        } // end function GetUInt32

        public ulong GetUInt64()
        {
            if (hash.Length != 8)
                throw new InvalidOperationHashLibException(ImpossibleRepresentationUInt64);

            return ((ulong)(hash[0]) << 56) | ((ulong)(hash[1]) << 48) | ((ulong)(hash[2]) << 40) |
                   ((ulong)(hash[3]) << 32) |
                   ((ulong)(hash[4]) << 24) | ((ulong)(hash[5]) << 16) | ((ulong)(hash[6]) << 8) | hash[7];
        } // end function GetUInt64

        public string ToString(bool a_group = false)
        {
            return Converters.ConvertBytesToHexString(hash, a_group);
        } // end function ToString

        private static bool SlowEquals(byte[]? a_ar1, byte[]? a_ar2)
        {
            uint diff = (uint)(a_ar1?.Length ^ a_ar2?.Length), I = 0;

            while (I <= (a_ar1?.Length - 1) && I <= (a_ar2?.Length - 1))
            {
                diff = diff | (uint)(a_ar1[I] ^ a_ar2[I]);
                I += 1;
            } // end while

            return diff == 0;
        } // end function SlowEquals
    }
}