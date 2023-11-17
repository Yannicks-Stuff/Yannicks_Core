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
    internal class Blake3 : Base.Hash, ICryptoNotBuiltIn, ITransformBlock
    {
        private const int ChunkSize = 1024;
        private const int BlockSizeInBytes = 64;
        internal const int KeyLengthInBytes = 32;

        private const uint flagChunkStart = 1 << 0;
        private const uint flagChunkEnd = 1 << 1;
        private const uint flagParent = 1 << 2;
        private const uint flagRoot = 1 << 3;
        protected const uint flagKeyedHash = 1 << 4;
        private const uint flagDeriveKeyContext = 1 << 5;
        private const uint flagDeriveKeyMaterial = 1 << 6;

        // maximum size in bytes this digest output reader can produce
        private const ulong MaxDigestLengthInBytes = ulong.MaxValue;

        public static readonly string InvalidXOFSize =
            "XOFSize in Bits must be Multiples of 8 and be Greater than Zero Bytes";

        public static readonly string InvalidKeyLength = "\"Key\" Length Must Not Be Greater Than {0}, \"{1}\"";
        public static readonly string MaximumOutputLengthExceeded = "Maximum Output Length is 2^64 Bytes";
        public static readonly string OutputBufferTooShort = "Output Buffer Too Short";
        public static readonly string OutputLengthInvalid = "Output Length is above the Digest Length";
        public static readonly string WritetoXofAfterReadError = "\"{0}\" Write to Xof after Read not Allowed";

        internal static readonly uint[] IV =
        {
            0x6A09E667, 0xBB67AE85, 0x3C6EF372, 0xA54FF53A,
            0x510E527F, 0x9B05688C, 0x1F83D9AB, 0x5BE0CD19
        };


        protected Blake3ChunkState CS;
        protected uint Flags;
        protected uint[] Key;
        protected Blake3OutputReader OutputReader;

        // log(n) set of Merkle subtree roots, at most one per height.
        // stack [54][8]uint32
        protected uint[][] Stack; // 2^54 * chunkSize = 2^64

        // bit vector indicating which stack elems are valid; also number of chunks added
        protected ulong Used;

        public Blake3(int a_HashSize, uint[] a_KeyWords, uint a_Flags)
            : base(a_HashSize, BlockSizeInBytes)
        {
            int LIdx;

            Key = a_KeyWords.DeepCopy();
            Flags = a_Flags;

            Stack = new uint[54][];
            for (LIdx = 0; LIdx < Stack.Length; LIdx++)
                Stack[LIdx] = new uint[8];
        } // end cctr

        public override string Name
        {
            get => string.Format("{0}_{1}", GetType().Name, HashSize * 8);
        } // end property Name

        public override void Initialize()
        {
            CS = Blake3ChunkState.CreateBlake3ChunkState(Key, 0, Flags);
            OutputReader = Blake3OutputReader.DefaultBlake3OutputReader();

            for (var i = 0; i < Stack.Length; i++)
                ArrayUtils.ZeroFill(ref Stack[i]);

            Used = 0;
        } // end function Initialize

        public override unsafe void TransformBytes(byte[]? a_data, int a_index, int a_length)
        {
            byte* LPtrAData;
            int LCount;
            var LCV = new uint[8];

            fixed (uint* LPtrCV = LCV)
            {
                fixed (byte* dataPtr = a_data)
                {
                    LPtrAData = dataPtr + a_index;

                    while (a_length > 0)
                    {
                        // If the current chunk is complete, finalize it and add it to the tree,
                        // then reset the chunk state (but keep incrementing the counter across
                        // chunks).
                        if (CS.Complete())
                        {
                            CS.Node().ChainingValue(ref LCV);
                            AddChunkChainingValue(LCV);
                            CS = Blake3ChunkState.CreateBlake3ChunkState(Key, CS.ChunkCounter() + 1, Flags);
                        }

                        // Compress input bytes into the current chunk state.
                        LCount = Math.Min(ChunkSize - CS.BytesConsumed, a_length);
                        CS.Update(LPtrAData, LCount);

                        LPtrAData += LCount;
                        a_length -= LCount;
                    }
                }
            }
        } // end function TransformBytes

        public override IHashResult TransformFinal()
        {
            Finish();

            var Buffer = new byte[HashSize];

            InternalDoOutput(ref Buffer, 0, (ulong)Buffer.Length);

            IHashResult result = new HashResult(Buffer);

            Initialize();

            return result;
        } // end function TransformFinal

        public override Interfaces.IHash? Clone()
        {
            var blake = new Blake3(HashSize, Key, Flags);

            blake.CS = CS.Clone();
            blake.OutputReader = OutputReader.Clone();

            for (var i = 0; i < Stack.Length; i++)
                blake.Stack[i] = Stack[i].DeepCopy();

            blake.Used = Used;

            blake.BufferSize = BufferSize;

            return blake;
        } // end function Clone

        private Blake3Node RootNode()
        {
            int LIdx, LTrailingZeros64, LLen64;

            var result = CS.Node();
            var LTemp = new uint[8];

            LTrailingZeros64 = TrailingZeros64(Used);
            LLen64 = Len64(Used);

            for (LIdx = LTrailingZeros64; LIdx < LLen64; LIdx++)
            {
                if (HasSubTreeAtHeight(LIdx))
                {
                    result.ChainingValue(ref LTemp);
                    result = Blake3Node.ParentNode(Stack[LIdx], LTemp, Key, Flags);
                }
            }

            result.Flags = result.Flags | flagRoot;

            return result;
        } // end function RootNode

        private bool HasSubTreeAtHeight(int a_Idx)
        {
            return (Used & ((uint)1 << a_Idx)) != 0;
        } // end function HasSubTreeAtHeight

        // AddChunkChainingValue appends a chunk to the right edge of the Merkle tree.
        private void AddChunkChainingValue(uint[] a_CV)
        {
            // seek to first open stack slot, merging subtrees as we go
            var LIdx = 0;
            while (HasSubTreeAtHeight(LIdx))
            {
                Blake3Node.ParentNode(Stack[LIdx], a_CV, Key, Flags).ChainingValue(ref a_CV);
                LIdx++;
            }

            Stack[LIdx] = a_CV.DeepCopy();
            Used++;
        } // end function AddChunkChainingValue

        // Len64 returns the minimum number of bits required to represent x; the result is 0 for x == 0.
        private static int Len64(ulong a_Value)
        {
            var result = 0;
            if (a_Value >= (1 << 32))
            {
                a_Value = a_Value >> 32;
                result = 32;
            }

            if (a_Value >= (1 << 16))
            {
                a_Value = a_Value >> 16;
                result = result + 16;
            }

            if (a_Value >= (1 << 8))
            {
                a_Value = a_Value >> 8;
                result = result + 8;
            }

            return result + Len8((byte)a_Value);
        } // end function Len64

        private static byte Len8(byte a_Value)
        {
            byte result = 0;
            while (a_Value != 0)
            {
                a_Value = (byte)(a_Value >> 1);
                result++;
            }

            return result;
        } // end function Len8

        private static int TrailingZeros64(ulong a_Value)
        {
            if (a_Value == 0) return 64;

            var result = 0;
            while ((a_Value & 1) == 0)
            {
                a_Value = a_Value >> 1;
                result++;
            }

            return result;
        } // end function TrailingZeros64

        protected void InternalDoOutput(ref byte[]? a_Destination, ulong a_DestinationOffset, ulong a_OutputLength)
        {
            OutputReader.Read(ref a_Destination, a_DestinationOffset, a_OutputLength);
        } // end function InternalDoOutput

        protected void Finish()
        {
            OutputReader.N = RootNode();
        } // end function Finish

        public static unsafe Blake3 CreateBlake3(int a_HashSize, byte[] a_Key)
        {
            int LKeyLength;
            Blake3 blake3 = null;
            var LKeyWords = new uint[8];

            if (a_Key.Empty())
            {
                LKeyWords = IV.DeepCopy();
                blake3 = new Blake3(a_HashSize, LKeyWords, 0);
            }
            else
            {
                LKeyLength = a_Key.Length;
                if (LKeyLength != KeyLengthInBytes)
                    throw new ArgumentOutOfRangeHashLibException(
                        string.Format(InvalidKeyLength, KeyLengthInBytes, LKeyLength));

                fixed (byte* keyPtr = a_Key)
                {
                    fixed (uint* keywordPtr = LKeyWords)
                    {
                        Converters.le32_copy((IntPtr)keyPtr, 0, (IntPtr)keywordPtr, 0, LKeyLength);
                    }
                }

                blake3 = new Blake3(a_HashSize, LKeyWords, flagKeyedHash);
            }

            return blake3;
        } // end cctr

        public static Blake3 CreateBlake3(HashSizeEnum a_HashSize = HashSizeEnum.HashSize256, byte[]? a_Key = null)
        {
            return CreateBlake3((int)a_HashSize, a_Key);
        } // end cctr

        // DeriveKey derives a subkey from ctx and srcKey. ctx should be hardcoded,
        // globally unique, and application-specific. A good format for ctx strings is:
        //
        // [application] [commit timestamp] [purpose]
        //
        // e.g.:
        //
        // example.com 2019-12-25 16:18:03 session tokens v1
        //
        // The purpose of these requirements is to ensure that an attacker cannot trick
        // two different applications into using the same context string.
        public static unsafe void DeriveKey(byte[]? a_SrcKey, byte[]? a_Ctx, byte[]? a_SubKey)
        {
            const int derivationIVLen = 32;
            IXOF LXof;

            var LIVWords = IV.DeepCopy();

            // construct the derivation Hasher and get the DerivationIV
            var LDerivationIV = (new Blake3(derivationIVLen, LIVWords, flagDeriveKeyContext) as Interfaces.IHash)
                .ComputeBytes(a_Ctx).GetBytes();

            fixed (byte* derivePtr = LDerivationIV)
            {
                fixed (uint* wordPtr = LIVWords)
                {
                    Converters.le32_copy((IntPtr)derivePtr, 0, (IntPtr)wordPtr, 0, KeyLengthInBytes);
                }
            }

            // derive the SubKey
            LXof = new Blake3XOF(32, LIVWords, flagDeriveKeyMaterial);
            LXof.XOFSizeInBits = (ulong)a_SubKey.Length * 8;
            LXof.Initialize();
            LXof.TransformBytes(a_SrcKey);
            LXof.DoOutput(ref a_SubKey, 0, (ulong)a_SubKey.Length);
            LXof.Initialize();
        } // end function DeriveKey

        // A Blake3Node represents a chunk or parent in the BLAKE3 Merkle tree. In BLAKE3
        // terminology, the elements of the bottom layer (aka "leaves") of the tree are
        // called chunk nodes, and the elements of upper layers (aka "interior nodes")
        // are called parent nodes.
        //
        // Computing a BLAKE3 hash involves splitting the input into chunk nodes, then
        // repeatedly merging these nodes into parent nodes, until only a single "root"
        // node remains. The root node can then be used to generate up to 2^64 - 1 bytes
        // of pseudorandom output.
        protected struct Blake3Node
        {
            // the chaining value from the previous state
            public uint[] CV;

            // the current state
            public uint[] Block;
            public ulong Counter;
            public uint BlockLen, Flags;

            public Blake3Node Clone()
            {
                var result = DefaultBlake3Node();

                result.CV = CV.DeepCopy();
                result.Block = Block.DeepCopy();
                result.Counter = Counter;
                result.BlockLen = BlockLen;
                result.Flags = Flags;

                return result;
            } // end function Clone

            // ChainingValue returns the first 8 words of the compressed node. This is used
            // in two places. First, when a chunk node is being constructed, its cv is
            // overwritten with this value after each block of input is processed. Second,
            // when two nodes are merged into a parent, each of their chaining values
            // supplies half of the new node's block.
            public void ChainingValue(ref uint[] a_Result)
            {
                var LFull = new uint[16];
                Compress(ref LFull);
                Utils.Utils.Memmove(ref a_Result, LFull, 8);
            } // end function 

            // compress is the core hash function, generating 16 pseudorandom words from a
            // node.
            // NOTE: we unroll all of the rounds, as well as the permutations that occur
            // between rounds.
            public void Compress(ref uint[] a_PtrState)
            {
                // initializes state here (in this case, a_PtrState)
                a_PtrState[0] = CV[0];
                a_PtrState[1] = CV[1];
                a_PtrState[2] = CV[2];
                a_PtrState[3] = CV[3];
                a_PtrState[4] = CV[4];
                a_PtrState[5] = CV[5];
                a_PtrState[6] = CV[6];
                a_PtrState[7] = CV[7];
                a_PtrState[8] = IV[0];
                a_PtrState[9] = IV[1];
                a_PtrState[10] = IV[2];
                a_PtrState[11] = IV[3];
                a_PtrState[12] = (uint)Counter;
                a_PtrState[13] = (uint)(Counter >> 32);
                a_PtrState[14] = BlockLen;
                a_PtrState[15] = Flags;

                // NOTE: we unroll all of the rounds, as well as the permutations that occur
                // between rounds.
                // Round 0
                // Mix the columns.
                G(ref a_PtrState, 0, 4, 8, 12, Block[0], Block[1]);
                G(ref a_PtrState, 1, 5, 9, 13, Block[2], Block[3]);
                G(ref a_PtrState, 2, 6, 10, 14, Block[4], Block[5]);
                G(ref a_PtrState, 3, 7, 11, 15, Block[6], Block[7]);

                // Mix the rows.
                G(ref a_PtrState, 0, 5, 10, 15, Block[8], Block[9]);
                G(ref a_PtrState, 1, 6, 11, 12, Block[10], Block[11]);
                G(ref a_PtrState, 2, 7, 8, 13, Block[12], Block[13]);
                G(ref a_PtrState, 3, 4, 9, 14, Block[14], Block[15]);

                // Round 1
                // Mix the columns.
                G(ref a_PtrState, 0, 4, 8, 12, Block[2], Block[6]);
                G(ref a_PtrState, 1, 5, 9, 13, Block[3], Block[10]);
                G(ref a_PtrState, 2, 6, 10, 14, Block[7], Block[0]);
                G(ref a_PtrState, 3, 7, 11, 15, Block[4], Block[13]);

                // Mix the rows.
                G(ref a_PtrState, 0, 5, 10, 15, Block[1], Block[11]);
                G(ref a_PtrState, 1, 6, 11, 12, Block[12], Block[5]);
                G(ref a_PtrState, 2, 7, 8, 13, Block[9], Block[14]);
                G(ref a_PtrState, 3, 4, 9, 14, Block[15], Block[8]);

                // Round 2
                // Mix the columns.
                G(ref a_PtrState, 0, 4, 8, 12, Block[3], Block[4]);
                G(ref a_PtrState, 1, 5, 9, 13, Block[10], Block[12]);
                G(ref a_PtrState, 2, 6, 10, 14, Block[13], Block[2]);
                G(ref a_PtrState, 3, 7, 11, 15, Block[7], Block[14]);

                // Mix the rows.
                G(ref a_PtrState, 0, 5, 10, 15, Block[6], Block[5]);
                G(ref a_PtrState, 1, 6, 11, 12, Block[9], Block[0]);
                G(ref a_PtrState, 2, 7, 8, 13, Block[11], Block[15]);
                G(ref a_PtrState, 3, 4, 9, 14, Block[8], Block[1]);

                // Round 3
                // Mix the columns.
                G(ref a_PtrState, 0, 4, 8, 12, Block[10], Block[7]);
                G(ref a_PtrState, 1, 5, 9, 13, Block[12], Block[9]);
                G(ref a_PtrState, 2, 6, 10, 14, Block[14], Block[3]);
                G(ref a_PtrState, 3, 7, 11, 15, Block[13], Block[15]);

                // Mix the rows.
                G(ref a_PtrState, 0, 5, 10, 15, Block[4], Block[0]);
                G(ref a_PtrState, 1, 6, 11, 12, Block[11], Block[2]);
                G(ref a_PtrState, 2, 7, 8, 13, Block[5], Block[8]);
                G(ref a_PtrState, 3, 4, 9, 14, Block[1], Block[6]);

                // Round 4
                // Mix the columns.
                G(ref a_PtrState, 0, 4, 8, 12, Block[12], Block[13]);
                G(ref a_PtrState, 1, 5, 9, 13, Block[9], Block[11]);
                G(ref a_PtrState, 2, 6, 10, 14, Block[15], Block[10]);
                G(ref a_PtrState, 3, 7, 11, 15, Block[14], Block[8]);

                // Mix the rows.
                G(ref a_PtrState, 0, 5, 10, 15, Block[7], Block[2]);
                G(ref a_PtrState, 1, 6, 11, 12, Block[5], Block[3]);
                G(ref a_PtrState, 2, 7, 8, 13, Block[0], Block[1]);
                G(ref a_PtrState, 3, 4, 9, 14, Block[6], Block[4]);

                // Round 5
                // Mix the columns.
                G(ref a_PtrState, 0, 4, 8, 12, Block[9], Block[14]);
                G(ref a_PtrState, 1, 5, 9, 13, Block[11], Block[5]);
                G(ref a_PtrState, 2, 6, 10, 14, Block[8], Block[12]);
                G(ref a_PtrState, 3, 7, 11, 15, Block[15], Block[1]);

                // Mix the rows.
                G(ref a_PtrState, 0, 5, 10, 15, Block[13], Block[3]);
                G(ref a_PtrState, 1, 6, 11, 12, Block[0], Block[10]);
                G(ref a_PtrState, 2, 7, 8, 13, Block[2], Block[6]);
                G(ref a_PtrState, 3, 4, 9, 14, Block[4], Block[7]);

                // Round 6
                // Mix the columns.
                G(ref a_PtrState, 0, 4, 8, 12, Block[11], Block[15]);
                G(ref a_PtrState, 1, 5, 9, 13, Block[5], Block[0]);
                G(ref a_PtrState, 2, 6, 10, 14, Block[1], Block[9]);
                G(ref a_PtrState, 3, 7, 11, 15, Block[8], Block[6]);

                // Mix the rows.
                G(ref a_PtrState, 0, 5, 10, 15, Block[14], Block[10]);
                G(ref a_PtrState, 1, 6, 11, 12, Block[2], Block[12]);
                G(ref a_PtrState, 2, 7, 8, 13, Block[3], Block[4]);
                G(ref a_PtrState, 3, 4, 9, 14, Block[7], Block[13]);

                // compression finalization

                a_PtrState[0] = a_PtrState[0] ^ a_PtrState[8];
                a_PtrState[1] = a_PtrState[1] ^ a_PtrState[9];
                a_PtrState[2] = a_PtrState[2] ^ a_PtrState[10];
                a_PtrState[3] = a_PtrState[3] ^ a_PtrState[11];
                a_PtrState[4] = a_PtrState[4] ^ a_PtrState[12];
                a_PtrState[5] = a_PtrState[5] ^ a_PtrState[13];
                a_PtrState[6] = a_PtrState[6] ^ a_PtrState[14];
                a_PtrState[7] = a_PtrState[7] ^ a_PtrState[15];
                a_PtrState[8] = a_PtrState[8] ^ CV[0];
                a_PtrState[9] = a_PtrState[9] ^ CV[1];
                a_PtrState[10] = a_PtrState[10] ^ CV[2];
                a_PtrState[11] = a_PtrState[11] ^ CV[3];
                a_PtrState[12] = a_PtrState[12] ^ CV[4];
                a_PtrState[13] = a_PtrState[13] ^ CV[5];
                a_PtrState[14] = a_PtrState[14] ^ CV[6];
                a_PtrState[15] = a_PtrState[15] ^ CV[7];
            } // end function Compress

            private void G(ref uint[] a_PtrState, uint A, uint B, uint C, uint D, uint X, uint Y)
            {
                uint LA, LB, LC, LD;

                LA = a_PtrState[A];
                LB = a_PtrState[B];
                LC = a_PtrState[C];
                LD = a_PtrState[D];

                LA = LA + LB + X;
                LD = Bits.RotateRight32(LD ^ LA, 16);
                LC = LC + LD;
                LB = Bits.RotateRight32(LB ^ LC, 12);
                LA = LA + LB + Y;
                LD = Bits.RotateRight32(LD ^ LA, 8);
                LC = LC + LD;
                LB = Bits.RotateRight32(LB ^ LC, 7);

                a_PtrState[A] = LA;
                a_PtrState[B] = LB;
                a_PtrState[C] = LC;
                a_PtrState[D] = LD;
            } // end function G

            public static Blake3Node DefaultBlake3Node()
            {
                var result = new Blake3Node();

                result.CV = new uint[8];
                result.Block = new uint[16];
                result.Counter = 0;
                result.BlockLen = 0;
                result.Flags = 0;

                return result;
            } // end function DefaultBlake3Node

            public static Blake3Node CreateBlake3Node(uint[] a_CV, uint[] a_Block,
                ulong a_Counter, uint a_BlockLen, uint a_Flags)
            {
                var result = DefaultBlake3Node();

                result.CV = a_CV.DeepCopy();
                result.Block = a_Block.DeepCopy();
                result.Counter = a_Counter;
                result.BlockLen = a_BlockLen;
                result.Flags = a_Flags;

                return result;
            } // end function CreateBlake3Node

            public static Blake3Node ParentNode(uint[] a_Left, uint[] a_Right, uint[] a_Key, uint a_Flags)
            {
                var LBlockWords = Utils.Utils.Concat(a_Left, a_Right);
                return CreateBlake3Node(a_Key, LBlockWords, 0, BlockSizeInBytes, a_Flags | flagParent);
            } // end funtion ParentNode
        } // end struct Blake3Node

        // Blake3ChunkState manages the state involved in hashing a single chunk of input.
        protected struct Blake3ChunkState
        {
            private Blake3Node N;
            private byte[]? Block;
            public int BlockLen, BytesConsumed;

            public Blake3ChunkState Clone()
            {
                var result = DefaultBlake3ChunkState();

                result.N = N.Clone();
                result.Block = Block.DeepCopy();
                result.BlockLen = BlockLen;
                result.BytesConsumed = BytesConsumed;

                return result;
            } // end function Clone

            // ChunkCounter is the index of this chunk, i.e. the number of chunks that have
            // been processed prior to this one.
            public ulong ChunkCounter()
            {
                return N.Counter;
            } // end function ChunkCounter

            public bool Complete()
            {
                return BytesConsumed == ChunkSize;
            } // end function Complete

            // node returns a node containing the chunkState's current state, with the
            // ChunkEnd flag set.
            public unsafe Blake3Node Node()
            {
                var result = N.Clone();

                fixed (byte* blockPtr = Block)
                {
                    fixed (uint* resultPtr = result.Block)
                    {
                        // pad the remaining space in the block with zeros
                        Utils.Utils.Memset((IntPtr)blockPtr + BlockLen, 0, (Block.Length - BlockLen) * sizeof(byte));
                        Converters.le32_copy((IntPtr)blockPtr, 0, (IntPtr)resultPtr, 0, BlockSizeInBytes);
                    }
                }

                result.BlockLen = (uint)BlockLen;
                result.Flags = result.Flags | flagChunkEnd;

                return result;
            } // end function Node

            // update incorporates input into the chunkState.
            public unsafe void Update(byte* dataPtr, int a_DataLength)
            {
                int LCount, LIndex;

                LIndex = 0;

                fixed (byte* LBytePtr = Block)
                {
                    fixed (uint* LCardinalPtr = N.Block)
                    {
                        fixed (uint* LCVPtr = N.CV)
                        {
                            while (a_DataLength > 0)
                            {
                                // If the block buffer is full, compress it and clear it. More
                                // input is coming, so this compression is not flagChunkEnd.
                                if (BlockLen == BlockSizeInBytes)
                                {
                                    // copy the chunk block (bytes) into the node block and chain it.
                                    Converters.le32_copy((IntPtr)LBytePtr, 0, (IntPtr)LCardinalPtr, 0,
                                        BlockSizeInBytes);
                                    N.ChainingValue(ref N.CV);
                                    // clear the start flag for all but the first block
                                    N.Flags = N.Flags & (N.Flags ^ flagChunkStart);
                                    BlockLen = 0;
                                } // end if

                                // Copy input bytes into the chunk block.
                                LCount = Math.Min(BlockSizeInBytes - BlockLen, a_DataLength);
                                Utils.Utils.Memmove((IntPtr)(LBytePtr + BlockLen), (IntPtr)(dataPtr + LIndex), LCount);

                                BlockLen += LCount;
                                BytesConsumed += LCount;
                                LIndex += LCount;
                                a_DataLength -= LCount;
                            } // end while
                        }
                    }
                }
            } // end function Update

            public static Blake3ChunkState DefaultBlake3ChunkState()
            {
                var result = new Blake3ChunkState();

                result.N = Blake3Node.DefaultBlake3Node();
                result.Block = new byte[BlockSizeInBytes];
                result.BlockLen = 0;
                result.BytesConsumed = 0;

                return result;
            } // end function DefaultBlake3ChunkState

            public static Blake3ChunkState CreateBlake3ChunkState(uint[] a_IV, ulong a_ChunkCounter, uint a_Flags)
            {
                var result = DefaultBlake3ChunkState();

                result.N.CV = a_IV.DeepCopy();
                result.N.Counter = a_ChunkCounter;
                result.N.BlockLen = BlockSizeInBytes;
                // compress the first block with the start flag set
                result.N.Flags = a_Flags | flagChunkStart;

                return result;
            } // end function CreateBlake3ChunkState
        } // end struct Blake3ChunkState

        protected struct Blake3OutputReader
        {
            public Blake3Node N;
            public byte[]? Block;
            public ulong Offset;

            public Blake3OutputReader Clone()
            {
                var result = DefaultBlake3OutputReader();

                result.N = N.Clone();
                result.Block = Block.DeepCopy();
                result.Offset = Offset;

                return result;
            } // end function  Clone

            public unsafe void Read(ref byte[]? a_Destination, ulong a_DestinationOffset, ulong a_OutputLength)
            {
                ulong LRemainder, LBlockOffset, LDiff;
                int LCount;

                var LWords = new uint[16];

                if (Offset == MaxDigestLengthInBytes)
                    throw new ArgumentOutOfRangeHashLibException(MaximumOutputLengthExceeded);
                LRemainder = MaxDigestLengthInBytes - Offset;
                if (a_OutputLength > LRemainder)
                    a_OutputLength = LRemainder;

                // end else
                fixed (uint* LPtrCardinal = LWords)
                {
                    fixed (byte* LPtrByte = Block)
                    {
                        while (a_OutputLength > 0)
                        {
                            if ((Offset & (BlockSizeInBytes - 1)) == 0)
                            {
                                N.Counter = Offset / BlockSizeInBytes;
                                N.Compress(ref LWords);
                                Converters.le32_copy((IntPtr)LPtrCardinal, 0, (IntPtr)LPtrByte, 0, BlockSizeInBytes);
                            }

                            LBlockOffset = Offset & (BlockSizeInBytes - 1);

                            LDiff = (ulong)Block.Length - LBlockOffset;

                            LCount = (int)Math.Min(a_OutputLength, LDiff);

                            Utils.Utils.Memmove(ref a_Destination, Block, LCount, (int)LBlockOffset,
                                (int)a_DestinationOffset);

                            a_OutputLength -= (ulong)LCount;
                            a_DestinationOffset += (ulong)LCount;
                            Offset += (ulong)LCount;
                        }
                    }
                }
            } // end function Read

            public static Blake3OutputReader DefaultBlake3OutputReader()
            {
                var result = new Blake3OutputReader();

                result.Block = new byte[BlockSizeInBytes];
                result.N = Blake3Node.DefaultBlake3Node();
                result.Offset = 0;

                return result;
            } // end function Blake3OutputReader
        } // end struct Blake3OutputReader
    } // end class Blake3

    internal sealed class Blake3XOF : Blake3, IXOF
    {
        private ulong _XofSizeInBits;
        private bool Finalized;

        public Blake3XOF(int a_HashSize, uint[] a_KeyWords, uint a_Flags)
            : base(a_HashSize, a_KeyWords, a_Flags)
        {
            Finalized = false;
        } // end cctr

        public ulong XOFSizeInBits
        {
            get => _XofSizeInBits;
            set => SetXOFSizeInBitsInternal(value);
        }

        public override string Name
        {
            get => GetType().Name;
        } // end property Name

        public override void Initialize()
        {
            Finalized = false;
            base.Initialize();
        } // end function Initialize

        public override Interfaces.IHash? Clone()
        {
            // Xof Cloning
            IXOF LXof = CreateBlake3XOF(HashSize, null);
            LXof.XOFSizeInBits = this.XOFSizeInBits;

            // Blake3XOF Cloning
            var HashInstance = (LXof as Blake3XOF);
            HashInstance.Finalized = Finalized;

            // Internal Blake3 Cloning
            HashInstance.CS = CS.Clone();
            HashInstance.OutputReader = OutputReader.Clone();

            for (var i = 0; i < Stack.Length; i++)
                HashInstance.Stack[i] = Stack[i].DeepCopy();

            HashInstance.Used = Used;
            HashInstance.Flags = Flags;
            HashInstance.Key = Key.DeepCopy();

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        } // end function Clone

        public override void TransformBytes(byte[]? a_data, int a_index, int a_length)
        {
            if (Finalized)
                throw new InvalidOperationHashLibException(
                    string.Format(WritetoXofAfterReadError, Name));

            base.TransformBytes(a_data, a_index, a_length);
        } // end function TransformBytes

        public override IHashResult TransformFinal()
        {
            var buffer = GetResult();

            Initialize();

            IHashResult result = new HashResult(buffer);

            return result;
        } // end function TransformFinal

        public void DoOutput(ref byte[]? a_destination, ulong a_destinationOffset, ulong a_outputLength)
        {
            if ((ulong)a_destination.Length - a_destinationOffset < a_outputLength)
                throw new ArgumentOutOfRangeHashLibException(OutputBufferTooShort);

            if ((OutputReader.Offset + a_outputLength) > (XOFSizeInBits >> 3))
                throw new ArgumentOutOfRangeHashLibException(OutputLengthInvalid);

            if (!Finalized)
            {
                Finish();
                Finalized = true;
            }

            InternalDoOutput(ref a_destination, a_destinationOffset, a_outputLength);
        } // end function DoOutput

        public static unsafe Blake3XOF CreateBlake3XOF(int a_HashSize, byte[] a_Key)
        {
            Blake3XOF blake = null;
            var LKeyWords = new uint[8];

            if (a_Key.Empty())
            {
                LKeyWords = IV.DeepCopy();
                blake = new Blake3XOF(a_HashSize, LKeyWords, 0);
            }
            else
            {
                var LKeyLength = a_Key.Length;
                if (LKeyLength != KeyLengthInBytes)
                    throw new ArgumentOutOfRangeHashLibException(
                        string.Format(InvalidKeyLength, KeyLengthInBytes, LKeyLength));

                fixed (byte* keyPtr = a_Key)
                {
                    fixed (uint* keywordPtr = LKeyWords)
                    {
                        Converters.le32_copy((IntPtr)keyPtr, 0, (IntPtr)keywordPtr, 0, LKeyLength);
                    }
                }

                blake = new Blake3XOF(a_HashSize, LKeyWords, flagKeyedHash);
            }

            blake.Finalized = false;

            return blake;
        } // end cctr

        private IXOF SetXOFSizeInBitsInternal(ulong a_XofSizeInBits)
        {
            var xofSizeInBytes = a_XofSizeInBits >> 3;
            if (((a_XofSizeInBits & 0x7) != 0) || (xofSizeInBytes < 1))
                throw new ArgumentInvalidHashLibException(InvalidXOFSize);

            _XofSizeInBits = a_XofSizeInBits;

            return this;
        }

        private byte[]? GetResult()
        {
            var xofSizeInBytes = XOFSizeInBits >> 3;

            var result = new byte[xofSizeInBytes];

            DoOutput(ref result, 0, xofSizeInBytes);

            return result;
        }
    } // end class Blake3XOF
}