﻿///////////////////////////////////////////////////////////////////////
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
    internal abstract class Haval : BlockHash, ICryptoNotBuiltIn, ITransformBlock
    {
        public const int HAVAL_VERSION = 1;

        public static readonly string InvalidHavalRound = "Haval Round Must be 3, 4 | 5";

        public static readonly string InvalidHavalHashSize =
            "Haval HashSize Must be Either 128 bit(16 byte), 160 bit(20 byte), 192 bit(24 byte), 224 bit(28 byte) | 256 bit(32 byte)";

        protected uint[] hash;
        protected int rounds;

        protected Haval(HashRounds a_rounds, HashSizeEnum a_hash_size)
            : base((int)a_hash_size, 128)
        {
            rounds = (int)a_rounds;
            hash = new uint[8];
        } // end constructor

        public override string Name => string.Format("Haval_{0}_{1}", rounds, HashSize * 8);

        public override void Initialize()
        {
            hash[0] = 0x243F6A88;
            hash[1] = 0x85A308D3;
            hash[2] = 0x13198A2E;
            hash[3] = 0x03707344;
            hash[4] = 0xA4093822;
            hash[5] = 0x299F31D0;
            hash[6] = 0x082EFA98;
            hash[7] = 0xEC4E6C89;

            base.Initialize();
        } // end function Initialize

        protected override unsafe byte[]? GetResult()
        {
            TailorDigestBits();

            var result = new byte[(HashSize >> 2) * sizeof(uint)];

            fixed (uint* hashPtr = hash)
            {
                fixed (byte* resultPtr = result)
                {
                    Converters.le32_copy((IntPtr)hashPtr, 0, (IntPtr)resultPtr, 0,
                        result.Length);
                }
            }

            return result;
        } // end function GetResult

        protected override void Finish()
        {
            int padindex;

            var bits = processed_bytes * 8;
            if (buffer.Position < 118)
                padindex = 118 - buffer.Position;
            else
                padindex = 246 - buffer.Position;

            var pad = new byte[padindex + 10];

            pad[0] = 0x01;

            pad[padindex] = (byte)((rounds << 3) | (HAVAL_VERSION & 0x07));
            padindex++;
            pad[padindex] = (byte)(HashSize << 1);
            padindex++;

            bits = Converters.le2me_64(bits);

            Converters.ReadUInt64AsBytesLE(bits, ref pad, padindex);

            padindex = padindex + 8;

            TransformBytes(pad, 0, padindex);
        } // end function Finish

        private void TailorDigestBits()
        {
            uint t;

            if (HashSize == 16)
            {
                t = (hash[7] & 0x000000FF) | (hash[6] & 0xFF000000) |
                    (hash[5] & 0x00FF0000) | (hash[4] & 0x0000FF00);
                hash[0] = hash[0] + Bits.RotateRight32(t, 8);
                t = (hash[7] & 0x0000FF00) | (hash[6] & 0x000000FF) |
                    (hash[5] & 0xFF000000) | (hash[4] & 0x00FF0000);
                hash[1] = hash[1] + Bits.RotateRight32(t, 16);
                t = (hash[7] & 0x00FF0000) | (hash[6] & 0x0000FF00) |
                    (hash[5] & 0x000000FF) | (hash[4] & 0xFF000000);
                hash[2] = hash[2] + Bits.RotateRight32(t, 24);
                t = (hash[7] & 0xFF000000) | (hash[6] & 0x00FF0000) |
                    (hash[5] & 0x0000FF00) | (hash[4] & 0x000000FF);
                hash[3] = hash[3] + t;
            } // end if
            else if (HashSize == 20)
            {
                t = hash[7] & 0x3F | (uint)(hash[6] & (0x7F << 25))
                                   | hash[5] & (0x3F << 19);
                hash[0] = hash[0] + Bits.RotateRight32(t, 19);
                t = hash[7] & (0x3F << 6) | hash[6] & 0x3F |
                    (uint)(hash[5] & (0x7F << 25));
                hash[1] = hash[1] + Bits.RotateRight32(t, 25);
                t = (hash[7] & (0x7F << 12)) | (hash[6] & (0x3F << 6)) |
                    (hash[5] & 0x3F);
                hash[2] = hash[2] + t;
                t = (hash[7] & (0x3F << 19)) | (hash[6] & (0x7F << 12)) |
                    (hash[5] & (0x3F << 6));
                hash[3] = hash[3] + (t >> 6);
                t = (hash[7] & ((uint)(0x7F) << 25)) |
                    hash[6] & (0x3F << 19) |
                    hash[5] & (0x7F << 12);
                hash[4] = hash[4] + (t >> 12);
            } // end else if
            else if (HashSize == 24)
            {
                t = hash[7] & 0x1F | (uint)(hash[6] & (0x3F << 26));
                hash[0] = hash[0] + Bits.RotateRight32(t, 26);
                t = (hash[7] & (0x1F << 5)) | (hash[6] & 0x1F);
                hash[1] = hash[1] + t;
                t = (hash[7] & (0x3F << 10)) | (hash[6] & (0x1F << 5));
                hash[2] = hash[2] + (t >> 5);
                t = (hash[7] & (0x1F << 16)) | (hash[6] & (0x3F << 10));
                hash[3] = hash[3] + (t >> 10);
                t = (hash[7] & (0x1F << 21)) | (hash[6] & (0x1F << 16));
                hash[4] = hash[4] + (t >> 16);
                t = (uint)(hash[7] & (0x3F << 26)) |
                    hash[6] & (0x1F << 21);
                hash[5] = hash[5] + (t >> 21);
            } // end else if
            else if (HashSize == 28)
            {
                hash[0] = hash[0] + ((hash[7] >> 27) & 0x1F);
                hash[1] = hash[1] + ((hash[7] >> 22) & 0x1F);
                hash[2] = hash[2] + ((hash[7] >> 18) & 0x0F);
                hash[3] = hash[3] + ((hash[7] >> 13) & 0x1F);
                hash[4] = hash[4] + ((hash[7] >> 9) & 0x0F);
                hash[5] = hash[5] + ((hash[7] >> 4) & 0x1F);
                hash[6] = hash[6] + (hash[7] & 0x0F);
            } // end else if
        } // end function TailorDigestBits
    } // end class Haval

    internal abstract class Haval3 : Haval
    {
        public Haval3(HashSizeEnum a_hash_size)
            : base(HashRounds.Rounds3, a_hash_size)
        {
        }

        protected override unsafe void TransformBlock(IntPtr a_data,
            int a_data_length, int a_index)
        {
            uint a, b, c, d, e, f, g, h, t;
            var temp = new uint[32];

            fixed (uint* tempPtr = temp)
            {
                Converters.le32_copy(a_data, a_index, (IntPtr)tempPtr, 0, 128);

                a = hash[0];
                b = hash[1];
                c = hash[2];
                d = hash[3];
                e = hash[4];
                f = hash[5];
                g = hash[6];
                h = hash[7];

                t = c & (e ^ d) ^ g & a ^ f & b ^ e;
                h = temp[0] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(h, 11);

                t = b & (d ^ c) ^ f & h ^ e & a ^ d;
                g = temp[1] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(g, 11);

                t = a & (c ^ b) ^ e & g ^ d & h ^ c;
                f = temp[2] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(f, 11);

                t = h & (b ^ a) ^ d & f ^ c & g ^ b;
                e = temp[3] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(e, 11);

                t = g & (a ^ h) ^ c & e ^ b & f ^ a;
                d = temp[4] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(d, 11);

                t = f & (h ^ g) ^ b & d ^ a & e ^ h;
                c = temp[5] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(c, 11);

                t = e & (g ^ f) ^ a & c ^ h & d ^ g;
                b = temp[6] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(b, 11);

                t = d & (f ^ e) ^ h & b ^ g & c ^ f;
                a = temp[7] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(a, 11);

                t = c & (e ^ d) ^ g & a ^ f & b ^ e;
                h = temp[8] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(h, 11);

                t = b & (d ^ c) ^ f & h ^ e & a ^ d;
                g = temp[9] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(g, 11);

                t = a & (c ^ b) ^ e & g ^ d & h ^ c;
                f = temp[10] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(f, 11);

                t = h & (b ^ a) ^ d & f ^ c & g ^ b;
                e = temp[11] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(e, 11);

                t = g & (a ^ h) ^ c & e ^ b & f ^ a;
                d = temp[12] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(d, 11);

                t = f & (h ^ g) ^ b & d ^ a & e ^ h;
                c = temp[13] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(c, 11);

                t = e & (g ^ f) ^ a & c ^ h & d ^ g;
                b = temp[14] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(b, 11);

                t = d & (f ^ e) ^ h & b ^ g & c ^ f;
                a = temp[15] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(a, 11);

                t = c & (e ^ d) ^ g & a ^ f & b ^ e;
                h = temp[16] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(h, 11);

                t = b & (d ^ c) ^ f & h ^ e & a ^ d;
                g = temp[17] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(g, 11);

                t = a & (c ^ b) ^ e & g ^ d & h ^ c;
                f = temp[18] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(f, 11);

                t = h & (b ^ a) ^ d & f ^ c & g ^ b;
                e = temp[19] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(e, 11);

                t = g & (a ^ h) ^ c & e ^ b & f ^ a;
                d = temp[20] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(d, 11);

                t = f & (h ^ g) ^ b & d ^ a & e ^ h;
                c = temp[21] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(c, 11);

                t = e & (g ^ f) ^ a & c ^ h & d ^ g;
                b = temp[22] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(b, 11);

                t = d & (f ^ e) ^ h & b ^ g & c ^ f;
                a = temp[23] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(a, 11);

                t = c & (e ^ d) ^ g & a ^ f & b ^ e;
                h = temp[24] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(h, 11);

                t = b & (d ^ c) ^ f & h ^ e & a ^ d;
                g = temp[25] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(g, 11);

                t = a & (c ^ b) ^ e & g ^ d & h ^ c;
                f = temp[26] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(f, 11);

                t = h & (b ^ a) ^ d & f ^ c & g ^ b;
                e = temp[27] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(e, 11);

                t = g & (a ^ h) ^ c & e ^ b & f ^ a;
                d = temp[28] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(d, 11);

                t = f & (h ^ g) ^ b & d ^ a & e ^ h;
                c = temp[29] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(c, 11);

                t = e & (g ^ f) ^ a & c ^ h & d ^ g;
                b = temp[30] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(b, 11);

                t = d & (f ^ e) ^ h & b ^ g & c ^ f;
                a = temp[31] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(a, 11);

                t = f & (d & ~a ^ b & c ^ e ^ g) ^ b & (d ^ c)
                                                 ^ a & c ^ g;
                h = temp[5] + 0x452821E6 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = e & (c & ~h ^ a & b ^ d ^ f) ^ a & (c ^ b)
                                                 ^ h & b ^ f;
                g = temp[14] + 0x38D01377 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = d & (b & ~g ^ h & a ^ c ^ e) ^ h & (b ^ a)
                                                 ^ g & a ^ e;
                f = temp[26] + 0xBE5466CF + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = c & (a & ~f ^ g & h ^ b ^ d) ^ g & (a ^ h)
                                                 ^ f & h ^ d;
                e = temp[18] + 0x34E90C6C + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = b & (h & ~e ^ f & g ^ a ^ c) ^ f & (h ^ g)
                                                 ^ e & g ^ c;
                d = temp[11] + 0xC0AC29B7 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = a & (g & ~d ^ e & f ^ h ^ b) ^ e & (g ^ f)
                                                 ^ d & f ^ b;
                c = temp[28] + 0xC97C50DD + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = h & (f & ~c ^ d & e ^ g ^ a) ^ d & (f ^ e)
                                                 ^ c & e ^ a;
                b = temp[7] + 0x3F84D5B5 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = g & (e & ~b ^ c & d ^ f ^ h) ^ c & (e ^ d)
                                                 ^ b & d ^ h;
                a = temp[16] + 0xB5470917 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = f & (d & ~a ^ b & c ^ e ^ g) ^ b & (d ^ c)
                                                 ^ a & c ^ g;
                h = temp[0] + 0x9216D5D9 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = e & (c & ~h ^ a & b ^ d ^ f) ^ a & (c ^ b)
                                                 ^ h & b ^ f;
                g = temp[23] + 0x8979FB1B + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = d & (b & ~g ^ h & a ^ c ^ e) ^ h & (b ^ a)
                                                 ^ g & a ^ e;
                f = temp[20] + 0xD1310BA6 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = c & (a & ~f ^ g & h ^ b ^ d) ^ g & (a ^ h)
                                                 ^ f & h ^ d;
                e = temp[22] + 0x98DFB5AC + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = b & (h & ~e ^ f & g ^ a ^ c) ^ f & (h ^ g)
                                                 ^ e & g ^ c;
                d = temp[1] + 0x2FFD72DB + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = a & (g & ~d ^ e & f ^ h ^ b) ^ e & (g ^ f)
                                                 ^ d & f ^ b;
                c = temp[10] + 0xD01ADFB7 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = h & (f & ~c ^ d & e ^ g ^ a) ^ d & (f ^ e)
                                                 ^ c & e ^ a;
                b = temp[4] + 0xB8E1AFED + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = g & (e & ~b ^ c & d ^ f ^ h) ^ c & (e ^ d)
                                                 ^ b & d ^ h;
                a = temp[8] + 0x6A267E96 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = f & (d & ~a ^ b & c ^ e ^ g) ^ b & (d ^ c)
                                                 ^ a & c ^ g;
                h = temp[30] + 0xBA7C9045 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = e & (c & ~h ^ a & b ^ d ^ f) ^ a & (c ^ b)
                                                 ^ h & b ^ f;
                g = temp[3] + 0xF12C7F99 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = d & (b & ~g ^ h & a ^ c ^ e) ^ h & (b ^ a)
                                                 ^ g & a ^ e;
                f = temp[21] + 0x24A19947 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = c & (a & ~f ^ g & h ^ b ^ d) ^ g & (a ^ h)
                                                 ^ f & h ^ d;
                e = temp[9] + 0xB3916CF7 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = b & (h & ~e ^ f & g ^ a ^ c) ^ f & (h ^ g)
                                                 ^ e & g ^ c;
                d = temp[17] + 0x0801F2E2 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = a & (g & ~d ^ e & f ^ h ^ b) ^ e & (g ^ f)
                                                 ^ d & f ^ b;
                c = temp[24] + 0x858EFC16 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = h & (f & ~c ^ d & e ^ g ^ a) ^ d & (f ^ e)
                                                 ^ c & e ^ a;
                b = temp[29] + 0x636920D8 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = g & (e & ~b ^ c & d ^ f ^ h) ^ c & (e ^ d)
                                                 ^ b & d ^ h;
                a = temp[6] + 0x71574E69 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = f & (d & ~a ^ b & c ^ e ^ g) ^ b & (d ^ c)
                                                 ^ a & c ^ g;
                h = temp[19] + 0xA458FEA3 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = e & (c & ~h ^ a & b ^ d ^ f) ^ a & (c ^ b)
                                                 ^ h & b ^ f;
                g = temp[12] + 0xF4933D7E + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = d & (b & ~g ^ h & a ^ c ^ e) ^ h & (b ^ a)
                                                 ^ g & a ^ e;
                f = temp[15] + 0x0D95748F + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = c & (a & ~f ^ g & h ^ b ^ d) ^ g & (a ^ h)
                                                 ^ f & h ^ d;
                e = temp[13] + 0x728EB658 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = b & (h & ~e ^ f & g ^ a ^ c) ^ f & (h ^ g)
                                                 ^ e & g ^ c;
                d = temp[2] + 0x718BCD58 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = a & (g & ~d ^ e & f ^ h ^ b) ^ e & (g ^ f)
                                                 ^ d & f ^ b;
                c = temp[25] + 0x82154AEE + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = h & (f & ~c ^ d & e ^ g ^ a) ^ d & (f ^ e)
                                                 ^ c & e ^ a;
                b = temp[31] + 0x7B54A41D + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = g & (e & ~b ^ c & d ^ f ^ h) ^ c & (e ^ d)
                                                 ^ b & d ^ h;
                a = temp[27] + 0xC25A59B5 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = d & (f & e ^ g ^ a) ^ f & c ^ e & b ^ a;
                h = temp[19] + 0x9C30D539 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = c & (e & d ^ f ^ h) ^ e & b ^ d & a ^ h;
                g = temp[9] + 0x2AF26013 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = b & (d & c ^ e ^ g) ^ d & a ^ c & h ^ g;
                f = temp[4] + 0xC5D1B023 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = a & (c & b ^ d ^ f) ^ c & h ^ b & g ^ f;
                e = temp[20] + 0x286085F0 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = h & (b & a ^ c ^ e) ^ b & g ^ a & f ^ e;
                d = temp[28] + 0xCA417918 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = g & (a & h ^ b ^ d) ^ a & f ^ h & e ^ d;
                c = temp[17] + 0xB8DB38EF + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = f & (h & g ^ a ^ c) ^ h & e ^ g & d ^ c;
                b = temp[8] + 0x8E79DCB0 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = e & (g & f ^ h ^ b) ^ g & d ^ f & c ^ b;
                a = temp[22] + 0x603A180E + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = d & (f & e ^ g ^ a) ^ f & c ^ e & b ^ a;
                h = temp[29] + 0x6C9E0E8B + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = c & (e & d ^ f ^ h) ^ e & b ^ d & a ^ h;
                g = temp[14] + 0xB01E8A3E + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = b & (d & c ^ e ^ g) ^ d & a ^ c & h ^ g;
                f = temp[25] + 0xD71577C1 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = a & (c & b ^ d ^ f) ^ c & h ^ b & g ^ f;
                e = temp[12] + 0xBD314B27 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = h & (b & a ^ c ^ e) ^ b & g ^ a & f ^ e;
                d = temp[24] + 0x78AF2FDA + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = g & (a & h ^ b ^ d) ^ a & f ^ h & e ^ d;
                c = temp[30] + 0x55605C60 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = f & (h & g ^ a ^ c) ^ h & e ^ g & d ^ c;
                b = temp[16] + 0xE65525F3 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = e & (g & f ^ h ^ b) ^ g & d ^ f & c ^ b;
                a = temp[26] + 0xAA55AB94 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = d & (f & e ^ g ^ a) ^ f & c ^ e & b ^ a;
                h = temp[31] + 0x57489862 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = c & (e & d ^ f ^ h) ^ e & b ^ d & a ^ h;
                g = temp[15] + 0x63E81440 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = b & (d & c ^ e ^ g) ^ d & a ^ c & h ^ g;
                f = temp[7] + 0x55CA396A + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = a & (c & b ^ d ^ f) ^ c & h ^ b & g ^ f;
                e = temp[3] + 0x2AAB10B6 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = h & (b & a ^ c ^ e) ^ b & g ^ a & f ^ e;
                d = temp[1] + 0xB4CC5C34 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = g & (a & h ^ b ^ d) ^ a & f ^ h & e ^ d;
                c = temp[0] + 0x1141E8CE + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = f & (h & g ^ a ^ c) ^ h & e ^ g & d ^ c;
                b = temp[18] + 0xA15486AF + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = e & (g & f ^ h ^ b) ^ g & d ^ f & c ^ b;
                a = temp[27] + 0x7C72E993 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = d & (f & e ^ g ^ a) ^ f & c ^ e & b ^ a;
                h = temp[13] + 0xB3EE1411 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = c & (e & d ^ f ^ h) ^ e & b ^ d & a ^ h;
                g = temp[6] + 0x636FBC2A + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = b & (d & c ^ e ^ g) ^ d & a ^ c & h ^ g;
                f = temp[21] + 0x2BA9C55D + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = a & (c & b ^ d ^ f) ^ c & h ^ b & g ^ f;
                e = temp[10] + 0x741831F6 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = h & (b & a ^ c ^ e) ^ b & g ^ a & f ^ e;
                d = temp[23] + 0xCE5C3E16 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = g & (a & h ^ b ^ d) ^ a & f ^ h & e ^ d;
                c = temp[11] + 0x9B87931E + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = f & (h & g ^ a ^ c) ^ h & e ^ g & d ^ c;
                b = temp[5] + 0xAFD6BA33 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = e & (g & f ^ h ^ b) ^ g & d ^ f & c ^ b;
                a = temp[2] + 0x6C24CF5C + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                hash[0] = hash[0] + a;
                hash[1] = hash[1] + b;
                hash[2] = hash[2] + c;
                hash[3] = hash[3] + d;
                hash[4] = hash[4] + e;
                hash[5] = hash[5] + f;
                hash[6] = hash[6] + g;
                hash[7] = hash[7] + h;

                Utils.Utils.Memset(ref temp, 0);
            }
        } // end function TransformBlock
    } // end class Haval3

    internal abstract class Haval4 : Haval
    {
        public Haval4(HashSizeEnum a_hash_size)
            : base(HashRounds.Rounds4, a_hash_size)
        {
        }

        protected override unsafe void TransformBlock(IntPtr a_data,
            int a_data_length, int a_index)
        {
            uint a, b, c, d, e, f, g, h, t;
            var temp = new uint[32];

            fixed (uint* tempPtr = temp)
            {
                Converters.le32_copy(a_data, a_index, (IntPtr)tempPtr, 0, 128);

                a = hash[0];
                b = hash[1];
                c = hash[2];
                d = hash[3];
                e = hash[4];
                f = hash[5];
                g = hash[6];
                h = hash[7];

                t = d & (a ^ b) ^ f & g ^ e & c ^ a;
                h = temp[0] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(h, 11);

                t = c & (h ^ a) ^ e & f ^ d & b ^ h;
                g = temp[1] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(g, 11);

                t = b & (g ^ h) ^ d & e ^ c & a ^ g;
                f = temp[2] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(f, 11);

                t = a & (f ^ g) ^ c & d ^ b & h ^ f;
                e = temp[3] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(e, 11);

                t = h & (e ^ f) ^ b & c ^ a & g ^ e;
                d = temp[4] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(d, 11);

                t = g & (d ^ e) ^ a & b ^ h & f ^ d;
                c = temp[5] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(c, 11);

                t = f & (c ^ d) ^ h & a ^ g & e ^ c;
                b = temp[6] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(b, 11);

                t = e & (b ^ c) ^ g & h ^ f & d ^ b;
                a = temp[7] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(a, 11);

                t = d & (a ^ b) ^ f & g ^ e & c ^ a;
                h = temp[8] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(h, 11);

                t = c & (h ^ a) ^ e & f ^ d & b ^ h;
                g = temp[9] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(g, 11);

                t = b & (g ^ h) ^ d & e ^ c & a ^ g;
                f = temp[10] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(f, 11);

                t = a & (f ^ g) ^ c & d ^ b & h ^ f;
                e = temp[11] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(e, 11);

                t = h & (e ^ f) ^ b & c ^ a & g ^ e;
                d = temp[12] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(d, 11);

                t = g & (d ^ e) ^ a & b ^ h & f ^ d;
                c = temp[13] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(c, 11);

                t = f & (c ^ d) ^ h & a ^ g & e ^ c;
                b = temp[14] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(b, 11);

                t = e & (b ^ c) ^ g & h ^ f & d ^ b;
                a = temp[15] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(a, 11);

                t = d & (a ^ b) ^ f & g ^ e & c ^ a;
                h = temp[16] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(h, 11);

                t = c & (h ^ a) ^ e & f ^ d & b ^ h;
                g = temp[17] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(g, 11);

                t = b & (g ^ h) ^ d & e ^ c & a ^ g;
                f = temp[18] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(f, 11);

                t = a & (f ^ g) ^ c & d ^ b & h ^ f;
                e = temp[19] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(e, 11);

                t = h & (e ^ f) ^ b & c ^ a & g ^ e;
                d = temp[20] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(d, 11);

                t = g & (d ^ e) ^ a & b ^ h & f ^ d;
                c = temp[21] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(c, 11);

                t = f & (c ^ d) ^ h & a ^ g & e ^ c;
                b = temp[22] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(b, 11);

                t = e & (b ^ c) ^ g & h ^ f & d ^ b;
                a = temp[23] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(a, 11);

                t = d & (a ^ b) ^ f & g ^ e & c ^ a;
                h = temp[24] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(h, 11);

                t = c & (h ^ a) ^ e & f ^ d & b ^ h;
                g = temp[25] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(g, 11);

                t = b & (g ^ h) ^ d & e ^ c & a ^ g;
                f = temp[26] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(f, 11);

                t = a & (f ^ g) ^ c & d ^ b & h ^ f;
                e = temp[27] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(e, 11);

                t = h & (e ^ f) ^ b & c ^ a & g ^ e;
                d = temp[28] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(d, 11);

                t = g & (d ^ e) ^ a & b ^ h & f ^ d;
                c = temp[29] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(c, 11);

                t = f & (c ^ d) ^ h & a ^ g & e ^ c;
                b = temp[30] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(b, 11);

                t = e & (b ^ c) ^ g & h ^ f & d ^ b;
                a = temp[31] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(a, 11);

                t = b & (g & ~a ^ c & f ^ d ^ e) ^ c & (g ^ f)
                                                 ^ a & f ^ e;
                h = temp[5] + 0x452821E6 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = a & (f & ~h ^ b & e ^ c ^ d) ^ b & (f ^ e)
                                                 ^ h & e ^ d;
                g = temp[14] + 0x38D01377 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = h & (e & ~g ^ a & d ^ b ^ c) ^ a & (e ^ d)
                                                 ^ g & d ^ c;
                f = temp[26] + 0xBE5466CF + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = g & (d & ~f ^ h & c ^ a ^ b) ^ h & (d ^ c)
                                                 ^ f & c ^ b;
                e = temp[18] + 0x34E90C6C + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = f & (c & ~e ^ g & b ^ h ^ a) ^ g & (c ^ b)
                                                 ^ e & b ^ a;
                d = temp[11] + 0xC0AC29B7 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = e & (b & ~d ^ f & a ^ g ^ h) ^ f & (b ^ a)
                                                 ^ d & a ^ h;
                c = temp[28] + 0xC97C50DD + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = d & (a & ~c ^ e & h ^ f ^ g) ^ e & (a ^ h)
                                                 ^ c & h ^ g;
                b = temp[7] + 0x3F84D5B5 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = c & (h & ~b ^ d & g ^ e ^ f) ^ d & (h ^ g)
                                                 ^ b & g ^ f;
                a = temp[16] + 0xB5470917 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = b & (g & ~a ^ c & f ^ d ^ e) ^ c & (g ^ f)
                                                 ^ a & f ^ e;
                h = temp[0] + 0x9216D5D9 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = a & (f & ~h ^ b & e ^ c ^ d) ^ b & (f ^ e)
                                                 ^ h & e ^ d;
                g = temp[23] + 0x8979FB1B + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = h & (e & ~g ^ a & d ^ b ^ c) ^ a & (e ^ d)
                                                 ^ g & d ^ c;
                f = temp[20] + 0xD1310BA6 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = g & (d & ~f ^ h & c ^ a ^ b) ^ h & (d ^ c)
                                                 ^ f & c ^ b;
                e = temp[22] + 0x98DFB5AC + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = f & (c & ~e ^ g & b ^ h ^ a) ^ g & (c ^ b)
                                                 ^ e & b ^ a;
                d = temp[1] + 0x2FFD72DB + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = e & (b & ~d ^ f & a ^ g ^ h) ^ f & (b ^ a)
                                                 ^ d & a ^ h;
                c = temp[10] + 0xD01ADFB7 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = d & (a & ~c ^ e & h ^ f ^ g) ^ e & (a ^ h)
                                                 ^ c & h ^ g;
                b = temp[4] + 0xB8E1AFED + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = c & (h & ~b ^ d & g ^ e ^ f) ^ d & (h ^ g)
                                                 ^ b & g ^ f;
                a = temp[8] + 0x6A267E96 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = b & (g & ~a ^ c & f ^ d ^ e) ^ c & (g ^ f)
                                                 ^ a & f ^ e;
                h = temp[30] + 0xBA7C9045 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = a & (f & ~h ^ b & e ^ c ^ d) ^ b & (f ^ e)
                                                 ^ h & e ^ d;
                g = temp[3] + 0xF12C7F99 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = h & (e & ~g ^ a & d ^ b ^ c) ^ a & (e ^ d)
                                                 ^ g & d ^ c;
                f = temp[21] + 0x24A19947 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = g & (d & ~f ^ h & c ^ a ^ b) ^ h & (d ^ c)
                                                 ^ f & c ^ b;
                e = temp[9] + 0xB3916CF7 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = f & (c & ~e ^ g & b ^ h ^ a) ^ g & (c ^ b)
                                                 ^ e & b ^ a;
                d = temp[17] + 0x0801F2E2 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = e & (b & ~d ^ f & a ^ g ^ h) ^ f & (b ^ a)
                                                 ^ d & a ^ h;
                c = temp[24] + 0x858EFC16 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = d & (a & ~c ^ e & h ^ f ^ g) ^ e & (a ^ h)
                                                 ^ c & h ^ g;
                b = temp[29] + 0x636920D8 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = c & (h & ~b ^ d & g ^ e ^ f) ^ d & (h ^ g)
                                                 ^ b & g ^ f;
                a = temp[6] + 0x71574E69 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = b & (g & ~a ^ c & f ^ d ^ e) ^ c & (g ^ f)
                                                 ^ a & f ^ e;
                h = temp[19] + 0xA458FEA3 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = a & (f & ~h ^ b & e ^ c ^ d) ^ b & (f ^ e)
                                                 ^ h & e ^ d;
                g = temp[12] + 0xF4933D7E + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = h & (e & ~g ^ a & d ^ b ^ c) ^ a & (e ^ d)
                                                 ^ g & d ^ c;
                f = temp[15] + 0x0D95748F + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = g & (d & ~f ^ h & c ^ a ^ b) ^ h & (d ^ c)
                                                 ^ f & c ^ b;
                e = temp[13] + 0x728EB658 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = f & (c & ~e ^ g & b ^ h ^ a) ^ g & (c ^ b)
                                                 ^ e & b ^ a;
                d = temp[2] + 0x718BCD58 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = e & (b & ~d ^ f & a ^ g ^ h) ^ f & (b ^ a)
                                                 ^ d & a ^ h;
                c = temp[25] + 0x82154AEE + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = d & (a & ~c ^ e & h ^ f ^ g) ^ e & (a ^ h)
                                                 ^ c & h ^ g;
                b = temp[31] + 0x7B54A41D + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = c & (h & ~b ^ d & g ^ e ^ f) ^ d & (h ^ g)
                                                 ^ b & g ^ f;
                a = temp[27] + 0xC25A59B5 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = g & (c & a ^ b ^ f) ^ c & d ^ a & e ^ f;
                h = temp[19] + 0x9C30D539 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = f & (b & h ^ a ^ e) ^ b & c ^ h & d ^ e;
                g = temp[9] + 0x2AF26013 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = e & (a & g ^ h ^ d) ^ a & b ^ g & c ^ d;
                f = temp[4] + 0xC5D1B023 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = d & (h & f ^ g ^ c) ^ h & a ^ f & b ^ c;
                e = temp[20] + 0x286085F0 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = c & (g & e ^ f ^ b) ^ g & h ^ e & a ^ b;
                d = temp[28] + 0xCA417918 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = b & (f & d ^ e ^ a) ^ f & g ^ d & h ^ a;
                c = temp[17] + 0xB8DB38EF + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = a & (e & c ^ d ^ h) ^ e & f ^ c & g ^ h;
                b = temp[8] + 0x8E79DCB0 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = h & (d & b ^ c ^ g) ^ d & e ^ b & f ^ g;
                a = temp[22] + 0x603A180E + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = g & (c & a ^ b ^ f) ^ c & d ^ a & e ^ f;
                h = temp[29] + 0x6C9E0E8B + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = f & (b & h ^ a ^ e) ^ b & c ^ h & d ^ e;
                g = temp[14] + 0xB01E8A3E + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = e & (a & g ^ h ^ d) ^ a & b ^ g & c ^ d;
                f = temp[25] + 0xD71577C1 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = d & (h & f ^ g ^ c) ^ h & a ^ f & b ^ c;
                e = temp[12] + 0xBD314B27 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = c & (g & e ^ f ^ b) ^ g & h ^ e & a ^ b;
                d = temp[24] + 0x78AF2FDA + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = b & (f & d ^ e ^ a) ^ f & g ^ d & h ^ a;
                c = temp[30] + 0x55605C60 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = a & (e & c ^ d ^ h) ^ e & f ^ c & g ^ h;
                b = temp[16] + 0xE65525F3 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = h & (d & b ^ c ^ g) ^ d & e ^ b & f ^ g;
                a = temp[26] + 0xAA55AB94 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = g & (c & a ^ b ^ f) ^ c & d ^ a & e ^ f;
                h = temp[31] + 0x57489862 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = f & (b & h ^ a ^ e) ^ b & c ^ h & d ^ e;
                g = temp[15] + 0x63E81440 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = e & (a & g ^ h ^ d) ^ a & b ^ g & c ^ d;
                f = temp[7] + 0x55CA396A + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = d & (h & f ^ g ^ c) ^ h & a ^ f & b ^ c;
                e = temp[3] + 0x2AAB10B6 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = c & (g & e ^ f ^ b) ^ g & h ^ e & a ^ b;
                d = temp[1] + 0xB4CC5C34 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = b & (f & d ^ e ^ a) ^ f & g ^ d & h ^ a;
                c = temp[0] + 0x1141E8CE + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = a & (e & c ^ d ^ h) ^ e & f ^ c & g ^ h;
                b = temp[18] + 0xA15486AF + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = h & (d & b ^ c ^ g) ^ d & e ^ b & f ^ g;
                a = temp[27] + 0x7C72E993 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = g & (c & a ^ b ^ f) ^ c & d ^ a & e ^ f;
                h = temp[13] + 0xB3EE1411 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = f & (b & h ^ a ^ e) ^ b & c ^ h & d ^ e;
                g = temp[6] + 0x636FBC2A + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = e & (a & g ^ h ^ d) ^ a & b ^ g & c ^ d;
                f = temp[21] + 0x2BA9C55D + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = d & (h & f ^ g ^ c) ^ h & a ^ f & b ^ c;
                e = temp[10] + 0x741831F6 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = c & (g & e ^ f ^ b) ^ g & h ^ e & a ^ b;
                d = temp[23] + 0xCE5C3E16 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = b & (f & d ^ e ^ a) ^ f & g ^ d & h ^ a;
                c = temp[11] + 0x9B87931E + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = a & (e & c ^ d ^ h) ^ e & f ^ c & g ^ h;
                b = temp[5] + 0xAFD6BA33 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = h & (d & b ^ c ^ g) ^ d & e ^ b & f ^ g;
                a = temp[2] + 0x6C24CF5C + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = a & (e & ~c ^ f & ~g ^ b ^ g ^ d) ^ f &
                    (b & c ^ e ^ g) ^ c & g ^ d;
                h = temp[24] + 0x7A325381 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = h & (d & ~b ^ e & ~f ^ a ^ f ^ c) ^ e &
                    (a & b ^ d ^ f) ^ b & f ^ c;
                g = temp[4] + 0x28958677 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = g & (c & ~a ^ d & ~e ^ h ^ e ^ b) ^ d &
                    (h & a ^ c ^ e) ^ a & e ^ b;
                f = temp[0] + 0x3B8F4898 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = f & (b & ~h ^ c & ~d ^ g ^ d ^ a) ^ c &
                    (g & h ^ b ^ d) ^ h & d ^ a;
                e = temp[14] + 0x6B4BB9AF + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = e & (a & ~g ^ b & ~c ^ f ^ c ^ h) ^ b &
                    (f & g ^ a ^ c) ^ g & c ^ h;
                d = temp[2] + 0xC4BFE81B + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = d & (h & ~f ^ a & ~b ^ e ^ b ^ g) ^ a &
                    (e & f ^ h ^ b) ^ f & b ^ g;
                c = temp[7] + 0x66282193 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = c & (g & ~e ^ h & ~a ^ d ^ a ^ f) ^ h &
                    (d & e ^ g ^ a) ^ e & a ^ f;
                b = temp[28] + 0x61D809CC + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = b & (f & ~d ^ g & ~h ^ c ^ h ^ e) ^ g &
                    (c & d ^ f ^ h) ^ d & h ^ e;
                a = temp[23] + 0xFB21A991 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = a & (e & ~c ^ f & ~g ^ b ^ g ^ d) ^ f &
                    (b & c ^ e ^ g) ^ c & g ^ d;
                h = temp[26] + 0x487CAC60 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = h & (d & ~b ^ e & ~f ^ a ^ f ^ c) ^ e &
                    (a & b ^ d ^ f) ^ b & f ^ c;
                g = temp[6] + 0x5DEC8032 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = g & (c & ~a ^ d & ~e ^ h ^ e ^ b) ^ d &
                    (h & a ^ c ^ e) ^ a & e ^ b;
                f = temp[30] + 0xEF845D5D + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = f & (b & ~h ^ c & ~d ^ g ^ d ^ a) ^ c &
                    (g & h ^ b ^ d) ^ h & d ^ a;
                e = temp[20] + 0xE98575B1 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = e & (a & ~g ^ b & ~c ^ f ^ c ^ h) ^ b &
                    (f & g ^ a ^ c) ^ g & c ^ h;
                d = temp[18] + 0xDC262302 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = d & (h & ~f ^ a & ~b ^ e ^ b ^ g) ^ a &
                    (e & f ^ h ^ b) ^ f & b ^ g;
                c = temp[25] + 0xEB651B88 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = c & (g & ~e ^ h & ~a ^ d ^ a ^ f) ^ h &
                    (d & e ^ g ^ a) ^ e & a ^ f;
                b = temp[19] + 0x23893E81 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = b & (f & ~d ^ g & ~h ^ c ^ h ^ e) ^ g &
                    (c & d ^ f ^ h) ^ d & h ^ e;
                a = temp[3] + 0xD396ACC5 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = a & (e & ~c ^ f & ~g ^ b ^ g ^ d) ^ f &
                    (b & c ^ e ^ g) ^ c & g ^ d;
                h = temp[22] + 0x0F6D6FF3 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = h & (d & ~b ^ e & ~f ^ a ^ f ^ c) ^ e &
                    (a & b ^ d ^ f) ^ b & f ^ c;
                g = temp[11] + 0x83F44239 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = g & (c & ~a ^ d & ~e ^ h ^ e ^ b) ^ d &
                    (h & a ^ c ^ e) ^ a & e ^ b;
                f = temp[31] + 0x2E0B4482 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = f & (b & ~h ^ c & ~d ^ g ^ d ^ a) ^ c &
                    (g & h ^ b ^ d) ^ h & d ^ a;
                e = temp[21] + 0xA4842004 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = e & (a & ~g ^ b & ~c ^ f ^ c ^ h) ^ b &
                    (f & g ^ a ^ c) ^ g & c ^ h;
                d = temp[8] + 0x69C8F04A + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = d & (h & ~f ^ a & ~b ^ e ^ b ^ g) ^ a &
                    (e & f ^ h ^ b) ^ f & b ^ g;
                c = temp[27] + 0x9E1F9B5E + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = c & (g & ~e ^ h & ~a ^ d ^ a ^ f) ^ h &
                    (d & e ^ g ^ a) ^ e & a ^ f;
                b = temp[12] + 0x21C66842 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = b & (f & ~d ^ g & ~h ^ c ^ h ^ e) ^ g &
                    (c & d ^ f ^ h) ^ d & h ^ e;
                a = temp[9] + 0xF6E96C9A + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = a & (e & ~c ^ f & ~g ^ b ^ g ^ d) ^ f &
                    (b & c ^ e ^ g) ^ c & g ^ d;
                h = temp[1] + 0x670C9C61 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = h & (d & ~b ^ e & ~f ^ a ^ f ^ c) ^ e &
                    (a & b ^ d ^ f) ^ b & f ^ c;
                g = temp[29] + 0xABD388F0 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = g & (c & ~a ^ d & ~e ^ h ^ e ^ b) ^ d &
                    (h & a ^ c ^ e) ^ a & e ^ b;
                f = temp[5] + 0x6A51A0D2 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = f & (b & ~h ^ c & ~d ^ g ^ d ^ a) ^ c &
                    (g & h ^ b ^ d) ^ h & d ^ a;
                e = temp[15] + 0xD8542F68 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = e & (a & ~g ^ b & ~c ^ f ^ c ^ h) ^ b &
                    (f & g ^ a ^ c) ^ g & c ^ h;
                d = temp[17] + 0x960FA728 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = d & (h & ~f ^ a & ~b ^ e ^ b ^ g) ^ a &
                    (e & f ^ h ^ b) ^ f & b ^ g;
                c = temp[10] + 0xAB5133A3 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = c & (g & ~e ^ h & ~a ^ d ^ a ^ f) ^ h &
                    (d & e ^ g ^ a) ^ e & a ^ f;
                b = temp[16] + 0x6EEF0B6C + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = b & (f & ~d ^ g & ~h ^ c ^ h ^ e) ^ g &
                    (c & d ^ f ^ h) ^ d & h ^ e;
                a = temp[13] + 0x137A3BE4 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                hash[0] = hash[0] + a;
                hash[1] = hash[1] + b;
                hash[2] = hash[2] + c;
                hash[3] = hash[3] + d;
                hash[4] = hash[4] + e;
                hash[5] = hash[5] + f;
                hash[6] = hash[6] + g;
                hash[7] = hash[7] + h;

                Utils.Utils.Memset(ref temp, 0);
            }
        } // end function TransformBlock
    } // end class Haval4

    internal abstract class Haval5 : Haval
    {
        public Haval5(HashSizeEnum a_hash_size)
            : base(HashRounds.Rounds5, a_hash_size)
        {
        }

        protected override unsafe void TransformBlock(IntPtr a_data,
            int a_data_length, int a_index)
        {
            uint a, b, c, d, e, f, g, h, t;
            var temp = new uint[32];

            fixed (uint* tempPtr = temp)
            {
                Converters.le32_copy(a_data, a_index, (IntPtr)tempPtr, 0, 128);

                a = hash[0];
                b = hash[1];
                c = hash[2];
                d = hash[3];
                e = hash[4];
                f = hash[5];
                g = hash[6];
                h = hash[7];

                t = c & (g ^ b) ^ f & e ^ a & d ^ g;
                h = temp[0] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(h, 11);
                t = b & (f ^ a) ^ e & d ^ h & c ^ f;
                g = temp[1] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(g, 11);

                t = a & (e ^ h) ^ d & c ^ g & b ^ e;
                f = temp[2] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(f, 11);

                t = h & (d ^ g) ^ c & b ^ f & a ^ d;
                e = temp[3] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(e, 11);

                t = g & (c ^ f) ^ b & a ^ e & h ^ c;
                d = temp[4] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(d, 11);

                t = f & (b ^ e) ^ a & h ^ d & g ^ b;
                c = temp[5] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(c, 11);

                t = e & (a ^ d) ^ h & g ^ c & f ^ a;
                b = temp[6] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(b, 11);

                t = d & (h ^ c) ^ g & f ^ b & e ^ h;
                a = temp[7] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(a, 11);

                t = c & (g ^ b) ^ f & e ^ a & d ^ g;
                h = temp[8] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(h, 11);

                t = b & (f ^ a) ^ e & d ^ h & c ^ f;
                g = temp[9] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(g, 11);

                t = a & (e ^ h) ^ d & c ^ g & b ^ e;
                f = temp[10] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(f, 11);

                t = h & (d ^ g) ^ c & b ^ f & a ^ d;
                e = temp[11] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(e, 11);

                t = g & (c ^ f) ^ b & a ^ e & h ^ c;
                d = temp[12] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(d, 11);

                t = f & (b ^ e) ^ a & h ^ d & g ^ b;
                c = temp[13] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(c, 11);

                t = e & (a ^ d) ^ h & g ^ c & f ^ a;
                b = temp[14] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(b, 11);

                t = d & (h ^ c) ^ g & f ^ b & e ^ h;
                a = temp[15] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(a, 11);

                t = c & (g ^ b) ^ f & e ^ a & d ^ g;
                h = temp[16] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(h, 11);

                t = b & (f ^ a) ^ e & d ^ h & c ^ f;
                g = temp[17] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(g, 11);

                t = a & (e ^ h) ^ d & c ^ g & b ^ e;
                f = temp[18] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(f, 11);

                t = h & (d ^ g) ^ c & b ^ f & a ^ d;
                e = temp[19] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(e, 11);

                t = g & (c ^ f) ^ b & a ^ e & h ^ c;
                d = temp[20] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(d, 11);

                t = f & (b ^ e) ^ a & h ^ d & g ^ b;
                c = temp[21] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(c, 11);

                t = e & (a ^ d) ^ h & g ^ c & f ^ a;
                b = temp[22] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(b, 11);

                t = d & (h ^ c) ^ g & f ^ b & e ^ h;
                a = temp[23] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(a, 11);

                t = c & (g ^ b) ^ f & e ^ a & d ^ g;
                h = temp[24] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(h, 11);

                t = b & (f ^ a) ^ e & d ^ h & c ^ f;
                g = temp[25] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(g, 11);

                t = a & (e ^ h) ^ d & c ^ g & b ^ e;
                f = temp[26] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(f, 11);

                t = h & (d ^ g) ^ c & b ^ f & a ^ d;
                e = temp[27] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(e, 11);

                t = g & (c ^ f) ^ b & a ^ e & h ^ c;
                d = temp[28] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(d, 11);

                t = f & (b ^ e) ^ a & h ^ d & g ^ b;
                c = temp[29] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(c, 11);

                t = e & (a ^ d) ^ h & g ^ c & f ^ a;
                b = temp[30] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(b, 11);

                t = d & (h ^ c) ^ g & f ^ b & e ^ h;
                a = temp[31] + Bits.RotateRight32(t, 7) + Bits.RotateRight32(a, 11);

                t = d & (e & ~a ^ b & c ^ g ^ f) ^ b & (e ^ c)
                                                 ^ a & c ^ f;
                h = temp[5] + 0x452821E6 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = c & (d & ~h ^ a & b ^ f ^ e) ^ a & (d ^ b)
                                                 ^ h & b ^ e;
                g = temp[14] + 0x38D01377 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = b & (c & ~g ^ h & a ^ e ^ d) ^ h & (c ^ a)
                                                 ^ g & a ^ d;
                f = temp[26] + 0xBE5466CF + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = a & (b & ~f ^ g & h ^ d ^ c) ^ g & (b ^ h)
                                                 ^ f & h ^ c;
                e = temp[18] + 0x34E90C6C + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = h & (a & ~e ^ f & g ^ c ^ b) ^ f & (a ^ g)
                                                 ^ e & g ^ b;
                d = temp[11] + 0xC0AC29B7 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = g & (h & ~d ^ e & f ^ b ^ a) ^ e & (h ^ f)
                                                 ^ d & f ^ a;
                c = temp[28] + 0xC97C50DD + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = f & (g & ~c ^ d & e ^ a ^ h) ^ d & (g ^ e)
                                                 ^ c & e ^ h;
                b = temp[7] + 0x3F84D5B5 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = e & (f & ~b ^ c & d ^ h ^ g) ^ c & (f ^ d)
                                                 ^ b & d ^ g;
                a = temp[16] + 0xB5470917 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = d & (e & ~a ^ b & c ^ g ^ f) ^ b & (e ^ c)
                                                 ^ a & c ^ f;
                h = temp[0] + 0x9216D5D9 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = c & (d & ~h ^ a & b ^ f ^ e) ^ a & (d ^ b)
                                                 ^ h & b ^ e;
                g = temp[23] + 0x8979FB1B + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = b & (c & ~g ^ h & a ^ e ^ d) ^ h & (c ^ a)
                                                 ^ g & a ^ d;
                f = temp[20] + 0xD1310BA6 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = a & (b & ~f ^ g & h ^ d ^ c) ^ g & (b ^ h)
                                                 ^ f & h ^ c;
                e = temp[22] + 0x98DFB5AC + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = h & (a & ~e ^ f & g ^ c ^ b) ^ f & (a ^ g)
                                                 ^ e & g ^ b;
                d = temp[1] + 0x2FFD72DB + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = g & (h & ~d ^ e & f ^ b ^ a) ^ e & (h ^ f)
                                                 ^ d & f ^ a;
                c = temp[10] + 0xD01ADFB7 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = f & (g & ~c ^ d & e ^ a ^ h) ^ d & (g ^ e)
                                                 ^ c & e ^ h;
                b = temp[4] + 0xB8E1AFED + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = e & (f & ~b ^ c & d ^ h ^ g) ^ c & (f ^ d)
                                                 ^ b & d ^ g;
                a = temp[8] + 0x6A267E96 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = d & (e & ~a ^ b & c ^ g ^ f) ^ b & (e ^ c)
                                                 ^ a & c ^ f;
                h = temp[30] + 0xBA7C9045 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = c & (d & ~h ^ a & b ^ f ^ e) ^ a & (d ^ b)
                                                 ^ h & b ^ e;
                g = temp[3] + 0xF12C7F99 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = b & (c & ~g ^ h & a ^ e ^ d) ^ h & (c ^ a)
                                                 ^ g & a ^ d;
                f = temp[21] + 0x24A19947 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = a & (b & ~f ^ g & h ^ d ^ c) ^ g & (b ^ h)
                                                 ^ f & h ^ c;
                e = temp[9] + 0xB3916CF7 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = h & (a & ~e ^ f & g ^ c ^ b) ^ f & (a ^ g)
                                                 ^ e & g ^ b;
                d = temp[17] + 0x0801F2E2 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = g & (h & ~d ^ e & f ^ b ^ a) ^ e & (h ^ f)
                                                 ^ d & f ^ a;
                c = temp[24] + 0x858EFC16 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = f & (g & ~c ^ d & e ^ a ^ h) ^ d & (g ^ e)
                                                 ^ c & e ^ h;
                b = temp[29] + 0x636920D8 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = e & (f & ~b ^ c & d ^ h ^ g) ^ c & (f ^ d)
                                                 ^ b & d ^ g;
                a = temp[6] + 0x71574E69 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = d & (e & ~a ^ b & c ^ g ^ f) ^ b & (e ^ c)
                                                 ^ a & c ^ f;
                h = temp[19] + 0xA458FEA3 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = c & (d & ~h ^ a & b ^ f ^ e) ^ a & (d ^ b)
                                                 ^ h & b ^ e;
                g = temp[12] + 0xF4933D7E + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = b & (c & ~g ^ h & a ^ e ^ d) ^ h & (c ^ a)
                                                 ^ g & a ^ d;
                f = temp[15] + 0x0D95748F + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = a & (b & ~f ^ g & h ^ d ^ c) ^ g & (b ^ h)
                                                 ^ f & h ^ c;
                e = temp[13] + 0x728EB658 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = h & (a & ~e ^ f & g ^ c ^ b) ^ f & (a ^ g)
                                                 ^ e & g ^ b;
                d = temp[2] + 0x718BCD58 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = g & (h & ~d ^ e & f ^ b ^ a) ^ e & (h ^ f)
                                                 ^ d & f ^ a;
                c = temp[25] + 0x82154AEE + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = f & (g & ~c ^ d & e ^ a ^ h) ^ d & (g ^ e)
                                                 ^ c & e ^ h;
                b = temp[31] + 0x7B54A41D + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = e & (f & ~b ^ c & d ^ h ^ g) ^ c & (f ^ d)
                                                 ^ b & d ^ g;
                a = temp[27] + 0xC25A59B5 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = e & (b & d ^ c ^ f) ^ b & a ^ d & g ^ f;
                h = temp[19] + 0x9C30D539 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = d & (a & c ^ b ^ e) ^ a & h ^ c & f ^ e;
                g = temp[9] + 0x2AF26013 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = c & (h & b ^ a ^ d) ^ h & g ^ b & e ^ d;
                f = temp[4] + 0xC5D1B023 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = b & (g & a ^ h ^ c) ^ g & f ^ a & d ^ c;
                e = temp[20] + 0x286085F0 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = a & (f & h ^ g ^ b) ^ f & e ^ h & c ^ b;
                d = temp[28] + 0xCA417918 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = h & (e & g ^ f ^ a) ^ e & d ^ g & b ^ a;
                c = temp[17] + 0xB8DB38EF + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = g & (d & f ^ e ^ h) ^ d & c ^ f & a ^ h;
                b = temp[8] + 0x8E79DCB0 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = f & (c & e ^ d ^ g) ^ c & b ^ e & h ^ g;
                a = temp[22] + 0x603A180E + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = e & (b & d ^ c ^ f) ^ b & a ^ d & g ^ f;
                h = temp[29] + 0x6C9E0E8B + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = d & (a & c ^ b ^ e) ^ a & h ^ c & f ^ e;
                g = temp[14] + 0xB01E8A3E + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = c & (h & b ^ a ^ d) ^ h & g ^ b & e ^ d;
                f = temp[25] + 0xD71577C1 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = b & (g & a ^ h ^ c) ^ g & f ^ a & d ^ c;
                e = temp[12] + 0xBD314B27 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = a & (f & h ^ g ^ b) ^ f & e ^ h & c ^ b;
                d = temp[24] + 0x78AF2FDA + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = h & (e & g ^ f ^ a) ^ e & d ^ g & b ^ a;
                c = temp[30] + 0x55605C60 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = g & (d & f ^ e ^ h) ^ d & c ^ f & a ^ h;
                b = temp[16] + 0xE65525F3 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = f & (c & e ^ d ^ g) ^ c & b ^ e & h ^ g;
                a = temp[26] + 0xAA55AB94 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = e & (b & d ^ c ^ f) ^ b & a ^ d & g ^ f;
                h = temp[31] + 0x57489862 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = d & (a & c ^ b ^ e) ^ a & h ^ c & f ^ e;
                g = temp[15] + 0x63E81440 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = c & (h & b ^ a ^ d) ^ h & g ^ b & e ^ d;
                f = temp[7] + 0x55CA396A + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = b & (g & a ^ h ^ c) ^ g & f ^ a & d ^ c;
                e = temp[3] + 0x2AAB10B6 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = a & (f & h ^ g ^ b) ^ f & e ^ h & c ^ b;
                d = temp[1] + 0xB4CC5C34 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = h & (e & g ^ f ^ a) ^ e & d ^ g & b ^ a;
                c = temp[0] + 0x1141E8CE + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = g & (d & f ^ e ^ h) ^ d & c ^ f & a ^ h;
                b = temp[18] + 0xA15486AF + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = f & (c & e ^ d ^ g) ^ c & b ^ e & h ^ g;
                a = temp[27] + 0x7C72E993 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = e & (b & d ^ c ^ f) ^ b & a ^ d & g ^ f;
                h = temp[13] + 0xB3EE1411 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = d & (a & c ^ b ^ e) ^ a & h ^ c & f ^ e;
                g = temp[6] + 0x636FBC2A + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = c & (h & b ^ a ^ d) ^ h & g ^ b & e ^ d;
                f = temp[21] + 0x2BA9C55D + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = b & (g & a ^ h ^ c) ^ g & f ^ a & d ^ c;
                e = temp[10] + 0x741831F6 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = a & (f & h ^ g ^ b) ^ f & e ^ h & c ^ b;
                d = temp[23] + 0xCE5C3E16 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = h & (e & g ^ f ^ a) ^ e & d ^ g & b ^ a;
                c = temp[11] + 0x9B87931E + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = g & (d & f ^ e ^ h) ^ d & c ^ f & a ^ h;
                b = temp[5] + 0xAFD6BA33 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = f & (c & e ^ d ^ g) ^ c & b ^ e & h ^ g;
                a = temp[2] + 0x6C24CF5C + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = d & (f & ~a ^ c & ~b ^ e ^ b ^ g) ^ c &
                    (e & a ^ f ^ b) ^ a & b ^ g;
                h = temp[24] + 0x7A325381 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = c & (e & ~h ^ b & ~a ^ d ^ a ^ f) ^ b &
                    (d & h ^ e ^ a) ^ h & a ^ f;
                g = temp[4] + 0x28958677 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = b & (d & ~g ^ a & ~h ^ c ^ h ^ e) ^ a &
                    (c & g ^ d ^ h) ^ g & h ^ e;
                f = temp[0] + 0x3B8F4898 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = a & (c & ~f ^ h & ~g ^ b ^ g ^ d) ^ h &
                    (b & f ^ c ^ g) ^ f & g ^ d;
                e = temp[14] + 0x6B4BB9AF + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = h & (b & ~e ^ g & ~f ^ a ^ f ^ c) ^ g &
                    (a & e ^ b ^ f) ^ e & f ^ c;
                d = temp[2] + 0xC4BFE81B + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = g & (a & ~d ^ f & ~e ^ h ^ e ^ b) ^ f &
                    (h & d ^ a ^ e) ^ d & e ^ b;
                c = temp[7] + 0x66282193 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = f & (h & ~c ^ e & ~d ^ g ^ d ^ a) ^ e &
                    (g & c ^ h ^ d) ^ c & d ^ a;
                b = temp[28] + 0x61D809CC + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = e & (g & ~b ^ d & ~c ^ f ^ c ^ h) ^ d &
                    (f & b ^ g ^ c) ^ b & c ^ h;
                a = temp[23] + 0xFB21A991 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = d & (f & ~a ^ c & ~b ^ e ^ b ^ g) ^ c &
                    (e & a ^ f ^ b) ^ a & b ^ g;
                h = temp[26] + 0x487CAC60 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = c & (e & ~h ^ b & ~a ^ d ^ a ^ f) ^ b &
                    (d & h ^ e ^ a) ^ h & a ^ f;
                g = temp[6] + 0x5DEC8032 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = b & (d & ~g ^ a & ~h ^ c ^ h ^ e) ^ a &
                    (c & g ^ d ^ h) ^ g & h ^ e;
                f = temp[30] + 0xEF845D5D + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = a & (c & ~f ^ h & ~g ^ b ^ g ^ d) ^ h &
                    (b & f ^ c ^ g) ^ f & g ^ d;
                e = temp[20] + 0xE98575B1 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = h & (b & ~e ^ g & ~f ^ a ^ f ^ c) ^ g &
                    (a & e ^ b ^ f) ^ e & f ^ c;
                d = temp[18] + 0xDC262302 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = g & (a & ~d ^ f & ~e ^ h ^ e ^ b) ^ f &
                    (h & d ^ a ^ e) ^ d & e ^ b;
                c = temp[25] + 0xEB651B88 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = f & (h & ~c ^ e & ~d ^ g ^ d ^ a) ^ e &
                    (g & c ^ h ^ d) ^ c & d ^ a;
                b = temp[19] + 0x23893E81 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = e & (g & ~b ^ d & ~c ^ f ^ c ^ h) ^ d &
                    (f & b ^ g ^ c) ^ b & c ^ h;
                a = temp[3] + 0xD396ACC5 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = d & (f & ~a ^ c & ~b ^ e ^ b ^ g) ^ c &
                    (e & a ^ f ^ b) ^ a & b ^ g;
                h = temp[22] + 0x0F6D6FF3 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = c & (e & ~h ^ b & ~a ^ d ^ a ^ f) ^ b &
                    (d & h ^ e ^ a) ^ h & a ^ f;
                g = temp[11] + 0x83F44239 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = b & (d & ~g ^ a & ~h ^ c ^ h ^ e) ^ a &
                    (c & g ^ d ^ h) ^ g & h ^ e;
                f = temp[31] + 0x2E0B4482 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = a & (c & ~f ^ h & ~g ^ b ^ g ^ d) ^ h &
                    (b & f ^ c ^ g) ^ f & g ^ d;
                e = temp[21] + 0xA4842004 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = h & (b & ~e ^ g & ~f ^ a ^ f ^ c) ^ g &
                    (a & e ^ b ^ f) ^ e & f ^ c;
                d = temp[8] + 0x69C8F04A + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = g & (a & ~d ^ f & ~e ^ h ^ e ^ b) ^ f &
                    (h & d ^ a ^ e) ^ d & e ^ b;
                c = temp[27] + 0x9E1F9B5E + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = f & (h & ~c ^ e & ~d ^ g ^ d ^ a) ^ e &
                    (g & c ^ h ^ d) ^ c & d ^ a;
                b = temp[12] + 0x21C66842 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = e & (g & ~b ^ d & ~c ^ f ^ c ^ h) ^ d &
                    (f & b ^ g ^ c) ^ b & c ^ h;
                a = temp[9] + 0xF6E96C9A + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = d & (f & ~a ^ c & ~b ^ e ^ b ^ g) ^ c &
                    (e & a ^ f ^ b) ^ a & b ^ g;
                h = temp[1] + 0x670C9C61 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = c & (e & ~h ^ b & ~a ^ d ^ a ^ f) ^ b &
                    (d & h ^ e ^ a) ^ h & a ^ f;
                g = temp[29] + 0xABD388F0 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = b & (d & ~g ^ a & ~h ^ c ^ h ^ e) ^ a &
                    (c & g ^ d ^ h) ^ g & h ^ e;
                f = temp[5] + 0x6A51A0D2 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = a & (c & ~f ^ h & ~g ^ b ^ g ^ d) ^ h &
                    (b & f ^ c ^ g) ^ f & g ^ d;
                e = temp[15] + 0xD8542F68 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = h & (b & ~e ^ g & ~f ^ a ^ f ^ c) ^ g &
                    (a & e ^ b ^ f) ^ e & f ^ c;
                d = temp[17] + 0x960FA728 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = g & (a & ~d ^ f & ~e ^ h ^ e ^ b) ^ f &
                    (h & d ^ a ^ e) ^ d & e ^ b;
                c = temp[10] + 0xAB5133A3 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = f & (h & ~c ^ e & ~d ^ g ^ d ^ a) ^ e &
                    (g & c ^ h ^ d) ^ c & d ^ a;
                b = temp[16] + 0x6EEF0B6C + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = e & (g & ~b ^ d & ~c ^ f ^ c ^ h) ^ d &
                    (f & b ^ g ^ c) ^ b & c ^ h;
                a = temp[13] + 0x137A3BE4 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = b & (d & e & g ^ ~f) ^ d & a ^ e & f ^ g & c;
                h = temp[27] + 0xBA3BF050 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = a & (c & d & f ^ ~e) ^ c & h ^ d & e ^ f & b;
                g = temp[3] + 0x7EFB2A98 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = h & (b & c & e ^ ~d) ^ b & g ^ c & d ^ e & a;
                f = temp[21] + 0xA1F1651D + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = g & (a & b & d ^ ~c) ^ a & f ^ b & c ^ d & h;
                e = temp[26] + 0x39AF0176 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = f & (h & a & c ^ ~b) ^ h & e ^ a & b ^ c & g;
                d = temp[17] + 0x66CA593E + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = e & (g & h & b ^ ~a) ^ g & d ^ h & a ^ b & f;
                c = temp[11] + 0x82430E88 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = d & (f & g & a ^ ~h) ^ f & c ^ g & h ^ a & e;
                b = temp[20] + 0x8CEE8619 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = c & (e & f & h ^ ~g) ^ e & b ^ f & g ^ h & d;
                a = temp[29] + 0x456F9FB4 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = b & (d & e & g ^ ~f) ^ d & a ^ e & f ^ g & c;
                h = temp[19] + 0x7D84A5C3 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = a & (c & d & f ^ ~e) ^ c & h ^ d & e ^ f & b;
                g = temp[0] + 0x3B8B5EBE + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = h & (b & c & e ^ ~d) ^ b & g ^ c & d ^ e & a;
                f = temp[12] + 0xE06F75D8 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = g & (a & b & d ^ ~c) ^ a & f ^ b & c ^ d & h;
                e = temp[7] + 0x85C12073 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = f & (h & a & c ^ ~b) ^ h & e ^ a & b ^ c & g;
                d = temp[13] + 0x401A449F + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = e & (g & h & b ^ ~a) ^ g & d ^ h & a ^ b & f;
                c = temp[8] + 0x56C16AA6 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = d & (f & g & a ^ ~h) ^ f & c ^ g & h ^ a & e;
                b = temp[31] + 0x4ED3AA62 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = c & (e & f & h ^ ~g) ^ e & b ^ f & g ^ h & d;
                a = temp[10] + 0x363F7706 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = b & (d & e & g ^ ~f) ^ d & a ^ e & f ^ g & c;
                h = temp[5] + 0x1BFEDF72 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = a & (c & d & f ^ ~e) ^ c & h ^ d & e ^ f & b;
                g = temp[9] + 0x429B023D + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = h & (b & c & e ^ ~d) ^ b & g ^ c & d ^ e & a;
                f = temp[14] + 0x37D0D724 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = g & (a & b & d ^ ~c) ^ a & f ^ b & c ^ d & h;
                e = temp[30] + 0xD00A1248 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = f & (h & a & c ^ ~b) ^ h & e ^ a & b ^ c & g;
                d = temp[18] + 0xDB0FEAD3 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = e & (g & h & b ^ ~a) ^ g & d ^ h & a ^ b & f;
                c = temp[6] + 0x49F1C09B + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = d & (f & g & a ^ ~h) ^ f & c ^ g & h ^ a & e;
                b = temp[28] + 0x075372C9 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = c & (e & f & h ^ ~g) ^ e & b ^ f & g ^ h & d;
                a = temp[24] + 0x80991B7B + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                t = b & (d & e & g ^ ~f) ^ d & a ^ e & f ^ g & c;
                h = temp[2] + 0x25D479D8 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(h, 11);

                t = a & (c & d & f ^ ~e) ^ c & h ^ d & e ^ f & b;
                g = temp[23] + 0xF6E8DEF7 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(g, 11);

                t = h & (b & c & e ^ ~d) ^ b & g ^ c & d ^ e & a;
                f = temp[16] + 0xE3FE501A + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(f, 11);

                t = g & (a & b & d ^ ~c) ^ a & f ^ b & c ^ d & h;
                e = temp[22] + 0xB6794C3B + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(e, 11);

                t = f & (h & a & c ^ ~b) ^ h & e ^ a & b ^ c & g;
                d = temp[4] + 0x976CE0BD + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(d, 11);

                t = e & (g & h & b ^ ~a) ^ g & d ^ h & a ^ b & f;
                c = temp[1] + 0x04C006BA + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(c, 11);

                t = d & (f & g & a ^ ~h) ^ f & c ^ g & h ^ a & e;
                b = temp[25] + 0xC1A94FB6 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(b, 11);

                t = c & (e & f & h ^ ~g) ^ e & b ^ f & g ^ h & d;
                a = temp[15] + 0x409F60C4 + Bits.RotateRight32(t, 7) +
                    Bits.RotateRight32(a, 11);

                hash[0] = hash[0] + a;
                hash[1] = hash[1] + b;
                hash[2] = hash[2] + c;
                hash[3] = hash[3] + d;
                hash[4] = hash[4] + e;
                hash[5] = hash[5] + f;
                hash[6] = hash[6] + g;
                hash[7] = hash[7] + h;

                Utils.Utils.Memset(ref temp, 0);
            }
        } // end function TransformBlock
    } // end class Haval5

    internal sealed class Haval_3_128 : Haval3
    {
        public Haval_3_128()
            : base(HashSizeEnum.HashSize128)
        {
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Haval_3_128();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.rounds = rounds;

            HashInstance.hash = hash.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone
    } // end class Haval_3_128

    internal sealed class Haval_4_128 : Haval4
    {
        public Haval_4_128()
            : base(HashSizeEnum.HashSize128)
        {
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Haval_4_128();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.rounds = rounds;

            HashInstance.hash = hash.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone
    } // end class Haval_4_128

    internal sealed class Haval_5_128 : Haval5
    {
        public Haval_5_128()
            : base(HashSizeEnum.HashSize128)
        {
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Haval_5_128();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.rounds = rounds;

            HashInstance.hash = hash.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone
    } // end class Haval_5_128

    internal sealed class Haval_3_160 : Haval3
    {
        public Haval_3_160()
            : base(HashSizeEnum.HashSize160)
        {
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Haval_3_160();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.rounds = rounds;

            HashInstance.hash = hash.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone
    } // end class Haval_3_160

    internal sealed class Haval_4_160 : Haval4
    {
        public Haval_4_160()
            : base(HashSizeEnum.HashSize160)
        {
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Haval_4_160();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.rounds = rounds;

            HashInstance.hash = hash.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone
    } // end class Haval_4_160

    internal sealed class Haval_5_160 : Haval5
    {
        public Haval_5_160()
            : base(HashSizeEnum.HashSize160)
        {
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Haval_5_160();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.rounds = rounds;

            HashInstance.hash = hash.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone
    } // end class Haval_5_160

    internal sealed class Haval_3_192 : Haval3
    {
        public Haval_3_192()
            : base(HashSizeEnum.HashSize192)
        {
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Haval_3_192();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.rounds = rounds;

            HashInstance.hash = hash.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone
    } // end class Haval_3_192

    internal sealed class Haval_4_192 : Haval4
    {
        public Haval_4_192()
            : base(HashSizeEnum.HashSize192)
        {
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Haval_4_192();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.rounds = rounds;

            HashInstance.hash = hash.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone
    } // end class Haval_4_192

    internal sealed class Haval_5_192 : Haval5
    {
        public Haval_5_192()
            : base(HashSizeEnum.HashSize192)
        {
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Haval_5_192();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.rounds = rounds;

            HashInstance.hash = hash.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone
    } // end class Haval_5_192

    internal sealed class Haval_3_224 : Haval3
    {
        public Haval_3_224()
            : base(HashSizeEnum.HashSize224)
        {
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Haval_3_224();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.rounds = rounds;

            HashInstance.hash = hash.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone
    } // end class Haval_3_224

    internal sealed class Haval_4_224 : Haval4
    {
        public Haval_4_224()
            : base(HashSizeEnum.HashSize224)
        {
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Haval_4_224();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.rounds = rounds;

            HashInstance.hash = hash.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone
    } // end class Haval_4_224

    internal sealed class Haval_5_224 : Haval5
    {
        public Haval_5_224()
            : base(HashSizeEnum.HashSize224)
        {
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Haval_5_224();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.rounds = rounds;

            HashInstance.hash = hash.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone
    } // end class Haval_3_224

    internal sealed class Haval_3_256 : Haval3
    {
        public Haval_3_256()
            : base(HashSizeEnum.HashSize256)
        {
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Haval_3_256();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.rounds = rounds;

            HashInstance.hash = hash.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone
    } // end class Haval_3_256

    internal sealed class Haval_4_256 : Haval4
    {
        public Haval_4_256()
            : base(HashSizeEnum.HashSize256)
        {
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Haval_4_256();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.rounds = rounds;

            HashInstance.hash = hash.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone
    } // end class Haval_4_256

    internal sealed class Haval_5_256 : Haval5
    {
        public Haval_5_256()
            : base(HashSizeEnum.HashSize256)
        {
        }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Haval_5_256();
            HashInstance.buffer = buffer.Clone();
            HashInstance.processed_bytes = processed_bytes;

            HashInstance.rounds = rounds;

            HashInstance.hash = hash.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone
    } // end class Haval_5_256
}