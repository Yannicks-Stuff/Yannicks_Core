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
    internal sealed class XXHash64 : Base.Hash, IHash64, IHashWithKey, ITransformBlock
    {
        private static readonly uint CKEY = 0x0;

        private static readonly ulong PRIME64_1 = 11400714785074694791;
        private static readonly ulong PRIME64_2 = 14029467366897019727;
        private static readonly ulong PRIME64_3 = 1609587929392839161;
        private static readonly ulong PRIME64_4 = 9650029242287828579;
        private static readonly ulong PRIME64_5 = 2870177450012600261;

        private static string InvalidKeyLength = "KeyLength Must Be Equal to {0}";
        private ulong key, hash;

        private XXH_State state;

        public XXHash64()
            : base(8, 32)
        {
            key = CKEY;
            Array.Resize(ref state.memory, 32);
        } // end constructor

        public override void Initialize()
        {
            hash = 0;
            state.v1 = key + PRIME64_1 + PRIME64_2;
            state.v2 = key + PRIME64_2;
            state.v3 = key + 0;
            state.v4 = key - PRIME64_1;
            state.total_len = 0;
            state.memsize = 0;
        } // end function Initialize

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new XXHash64
            {
                key = key,
                hash = hash,
                state = state.Clone(),
                BufferSize = BufferSize
            };

            return HashInstance;
        } // end function Clone

        public override unsafe void TransformBytes(byte[]? a_data, int a_index, int a_length)
        {
            ulong _v1, _v2, _v3, _v4;
            byte* ptrTemp;

            state.total_len = state.total_len + (ulong)a_length;

            fixed (byte* ptrAData = a_data, ptrMemory = state.memory)
            {
                var ptrBuffer = ptrAData + a_index;

                if ((state.memsize + (uint)a_length) < 32)
                {
                    ptrTemp = ptrMemory + state.memsize;

                    Utils.Utils.Memmove((IntPtr)ptrTemp, (IntPtr)ptrBuffer, a_length);

                    state.memsize = state.memsize + (uint)a_length;

                    return;
                } // end if

                var ptrEnd = ptrBuffer + (uint)a_length;

                if (state.memsize > 0)
                {
                    ptrTemp = ptrMemory + state.memsize;

                    Utils.Utils.Memmove((IntPtr)ptrTemp, (IntPtr)ptrBuffer, (int)(32 - state.memsize));

                    state.v1 = PRIME64_1 *
                               Bits.RotateLeft64(
                                   state.v1 + PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr)ptrMemory, 0), 31);
                    state.v2 = PRIME64_1 *
                               Bits.RotateLeft64(
                                   state.v2 + PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr)ptrMemory, 8), 31);
                    state.v3 = PRIME64_1 *
                               Bits.RotateLeft64(
                                   state.v3 + PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr)ptrMemory, 16), 31);
                    state.v4 = PRIME64_1 *
                               Bits.RotateLeft64(
                                   state.v4 + PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr)ptrMemory, 24), 31);

                    ptrBuffer = ptrBuffer + (32 - state.memsize);
                    state.memsize = 0;
                } // end if

                if (ptrBuffer <= (ptrEnd - 32))
                {
                    _v1 = state.v1;
                    _v2 = state.v2;
                    _v3 = state.v3;
                    _v4 = state.v4;

                    var ptrLimit = ptrEnd - 32;

                    do
                    {
                        _v1 = PRIME64_1 *
                              Bits.RotateLeft64(_v1 + PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr)ptrBuffer, 0),
                                  31);
                        _v2 = PRIME64_1 *
                              Bits.RotateLeft64(_v2 + PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr)ptrBuffer, 8),
                                  31);
                        _v3 = PRIME64_1 *
                              Bits.RotateLeft64(_v3 + PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr)ptrBuffer, 16),
                                  31);
                        _v4 = PRIME64_1 *
                              Bits.RotateLeft64(_v4 + PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr)ptrBuffer, 24),
                                  31);
                        ptrBuffer += 32;
                    } while (ptrBuffer <= ptrLimit);

                    state.v1 = _v1;
                    state.v2 = _v2;
                    state.v3 = _v3;
                    state.v4 = _v4;
                } // end if

                if (ptrBuffer < ptrEnd)
                {
                    Utils.Utils.Memmove((IntPtr)ptrMemory, (IntPtr)ptrBuffer, (int)(ptrEnd - ptrBuffer));
                    state.memsize = (uint)(ptrEnd - ptrBuffer);
                } // end if
            }
        } // end function TransformBytes

        public override unsafe IHashResult TransformFinal()
        {
            ulong _v1, _v2, _v3, _v4;
            byte* ptrEnd, ptrBuffer;

            fixed (byte* bPtr = state.memory)
            {
                if (state.total_len >= 32)
                {
                    _v1 = state.v1;
                    _v2 = state.v2;
                    _v3 = state.v3;
                    _v4 = state.v4;

                    hash = Bits.RotateLeft64(_v1, 1) + Bits.RotateLeft64(_v2, 7) + Bits.RotateLeft64(_v3, 12) +
                           Bits.RotateLeft64(_v4, 18);

                    _v1 = Bits.RotateLeft64(_v1 * PRIME64_2, 31) * PRIME64_1;
                    hash = (hash ^ _v1) * PRIME64_1 + PRIME64_4;

                    _v2 = Bits.RotateLeft64(_v2 * PRIME64_2, 31) * PRIME64_1;
                    hash = (hash ^ _v2) * PRIME64_1 + PRIME64_4;

                    _v3 = Bits.RotateLeft64(_v3 * PRIME64_2, 31) * PRIME64_1;
                    hash = (hash ^ _v3) * PRIME64_1 + PRIME64_4;

                    _v4 = Bits.RotateLeft64(_v4 * PRIME64_2, 31) * PRIME64_1;
                    hash = (hash ^ _v4) * PRIME64_1 + PRIME64_4;
                } // end if
                else
                    hash = key + PRIME64_5;

                hash += state.total_len;

                ptrBuffer = bPtr;

                ptrEnd = ptrBuffer + state.memsize;
                while ((ptrBuffer + 8) <= ptrEnd)
                {
                    hash = hash ^ (PRIME64_1 *
                                   Bits.RotateLeft64(PRIME64_2 * Converters.ReadBytesAsUInt64LE((IntPtr)ptrBuffer, 0),
                                       31));
                    hash = Bits.RotateLeft64(hash, 27) * PRIME64_1 + PRIME64_4;
                    ptrBuffer += 8;
                } // end while

                if ((ptrBuffer + 4) <= ptrEnd)
                {
                    hash = hash ^ Converters.ReadBytesAsUInt32LE((IntPtr)ptrBuffer, 0) * PRIME64_1;
                    hash = Bits.RotateLeft64(hash, 23) * PRIME64_2 + PRIME64_3;
                    ptrBuffer += 4;
                } // end if

                while (ptrBuffer < ptrEnd)
                {
                    hash = hash ^ (*ptrBuffer) * PRIME64_5;
                    hash = Bits.RotateLeft64(hash, 11) * PRIME64_1;
                    ptrBuffer++;
                } // end while

                hash = hash ^ (hash >> 33);
                hash = hash * PRIME64_2;
                hash = hash ^ (hash >> 29);
                hash = hash * PRIME64_3;
                hash = hash ^ (hash >> 32);
            }

            IHashResult result = new HashResult(hash);

            Initialize();

            return result;
        } // end function TransformFinal

        public int? KeyLength
        {
            get => 8;
        } // end property KeyLength

        public byte[]? Key
        {
            get => Converters.ReadUInt64AsBytesLE(key);

            set
            {
                if (value == null || value.Length == 0)
                    key = CKEY;
                else
                {
                    if (value.Length != KeyLength)
                        throw new ArgumentHashLibException(string.Format(InvalidKeyLength, KeyLength));

                    unsafe
                    {
                        fixed (byte* vPtr = &value[0])
                        {
                            key = Converters.ReadBytesAsUInt64LE((IntPtr)vPtr, 0);
                        }
                    }
                } // end else
            }
        } // end property GetKey

        private struct XXH_State
        {
            public ulong total_len, v1, v2, v3, v4;
            public uint memsize;
            public byte[]? memory;

            public XXH_State Clone()
            {
                var result = new XXH_State
                {
                    total_len = total_len,
                    memsize = memsize,
                    v1 = v1,
                    v2 = v2,
                    v3 = v3,
                    v4 = v4,
                    memory = memory.DeepCopy()
                };

                return result;
            } // end function Clone
        } // end struct XXH_State
    } // end class XXHash64
}