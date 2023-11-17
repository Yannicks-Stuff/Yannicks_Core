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

using Yannick.Crypto.SharpHash.Crypto.Blake2BConfigurations;
using Yannick.Crypto.SharpHash.Interfaces;
using Yannick.Crypto.SharpHash.Interfaces.IBlake2BConfigurations;
using Yannick.Crypto.SharpHash.Utils;

namespace Yannick.Crypto.SharpHash.Crypto
{
    internal sealed class Blake2BP : Base.Hash, ICryptoNotBuiltIn, ITransformBlock
    {
        private static readonly int BlockSizeInBytes = 128;
        private static readonly int OutSizeInBytes = 64;
        private static readonly int ParallelismDegree = 4;
        private byte[]? Buffer;
        private byte[]? Key;
        private Blake2B[] LeafHashes;

        public Blake2BP(int a_HashSize, byte[] a_Key)
            : base(a_HashSize, BlockSizeInBytes)
        {
            Buffer = new byte[ParallelismDegree * BlockSizeInBytes];
            LeafHashes = new Blake2B[ParallelismDegree];

            Key = a_Key.DeepCopy();

            RootHash = Blake2BPCreateRoot();

            for (var i = 0; i < ParallelismDegree; i++)
                LeafHashes[i] = Blake2BPCreateLeaf((ulong)i);
        }

        private Blake2BP(int a_HashSize)
            : base(a_HashSize, BlockSizeInBytes)
        {
        }

        // had to use the classes directly for performance purposes
        private Blake2B RootHash { get; set; }
        private ulong BufferLength { get; set; }

        public override Interfaces.IHash? Clone()
        {
            var HashInstance = new Blake2BP(HashSize);

            HashInstance.Key = Key.DeepCopy();

            HashInstance.RootHash = (Blake2B)RootHash?.Clone();

            if (LeafHashes != null)
            {
                HashInstance.LeafHashes = new Blake2B[LeafHashes.Length];
                for (var i = 0; i < LeafHashes.Length; i++)
                {
                    HashInstance.LeafHashes[i] = (Blake2B)LeafHashes[i].Clone();
                }
            }

            HashInstance.Buffer = Buffer.DeepCopy();

            HashInstance.BufferLength = BufferLength;

            HashInstance.BufferSize = BufferSize;

            return HashInstance;
        }

        public override void Initialize()
        {
            RootHash.Initialize();
            for (var i = 0; i < ParallelismDegree; i++)
            {
                LeafHashes[i].Initialize();
                LeafHashes[i].HashSize = OutSizeInBytes;
            }

            ArrayUtils.ZeroFill(ref Buffer);
            BufferLength = 0;
        }

        public override unsafe void TransformBytes(byte[]? a_data, int a_index, int a_length)
        {
            ulong left, fill, dataLength;
            byte* ptrData = null;
            int i;
            var ptrDataContainer = new DataContainer();

            if (a_data.Empty()) return;

            dataLength = (ulong)a_length;

            fixed (byte* ptr = a_data)
            {
                fixed (byte* bufferPtr = Buffer)
                {
                    ptrData = ptr + a_index;

                    left = BufferLength;
                    fill = (ulong)Buffer.Length - left;

                    if ((left > 0) && (dataLength >= fill))
                    {
                        Utils.Utils.Memmove((IntPtr)(bufferPtr + left), (IntPtr)ptrData, (int)fill);

                        for (i = 0; i < ParallelismDegree; i++)
                        {
                            LeafHashes[i].TransformBytes(Buffer, i * BlockSizeInBytes, BlockSizeInBytes);
                        }

                        ptrData += fill;
                        dataLength = dataLength - fill;
                        left = 0;
                    }

                    try
                    {
                        ptrDataContainer.PtrData = (IntPtr)ptrData;
                        ptrDataContainer.Counter = dataLength;
                        DoParallelComputation(ref ptrDataContainer);
                    }
                    catch (Exception)
                    {
                        /* pass */
                    }

                    ptrData += (dataLength - (dataLength % (ulong)(ParallelismDegree * BlockSizeInBytes)));
                    dataLength = dataLength % (ulong)(ParallelismDegree * BlockSizeInBytes);

                    if (dataLength > 0)
                        Utils.Utils.Memmove((IntPtr)(bufferPtr + left), (IntPtr)ptrData, (int)dataLength);

                    BufferLength = (uint)left + (uint)dataLength;
                }
            }
        }

        public override IHashResult TransformFinal()
        {
            int i;
            ulong left;

            byte[]?[] hash = new byte[ParallelismDegree][];

            for (i = 0; i < hash.Length; i++)
            {
                hash[i] = new byte[OutSizeInBytes];
            }

            for (i = 0; i < ParallelismDegree; i++)
            {
                if (BufferLength > (ulong)(i * BlockSizeInBytes))
                {
                    left = BufferLength - (ulong)(i * BlockSizeInBytes);
                    if (left > (ulong)BlockSizeInBytes)
                        left = (ulong)BlockSizeInBytes;

                    LeafHashes[i].TransformBytes(Buffer, i * BlockSizeInBytes, (int)left);
                }

                hash[i] = LeafHashes[i].TransformFinal().GetBytes();
            }

            for (i = 0; i < ParallelismDegree; i++)
                RootHash.TransformBytes(hash[i], 0, OutSizeInBytes);

            var result = RootHash.TransformFinal();

            Initialize();

            return result;
        }

        public override string Name
        {
            get { return string.Format("{0}_{1}", GetType().Name, HashSize * 8); }
        } // end property Name

        ~Blake2BP()
        {
            Clear();
        }

        /// <summary>
        /// <br />Blake2B defaults to setting the expected output length <br />
        /// from the <c>HashSize</c> in the <c>Blake2BConfig</c> class. <br />In
        /// some cases, however, we do not want this, as the output length <br />
        /// of these instances is given by <c>Blake2BTreeConfig.InnerSize</c>
        /// instead. <br />
        /// </summary>
        private Blake2B Blake2BPCreateLeafParam(IBlake2BConfig a_Blake2BConfig, IBlake2BTreeConfig? a_Blake2BTreeConfig)
        {
            return new Blake2B(a_Blake2BConfig, a_Blake2BTreeConfig);
        }

        private Blake2B Blake2BPCreateLeaf(ulong a_Offset)
        {
            IBlake2BConfig blake2BConfig = new Blake2BConfig(HashSize);

            blake2BConfig.Key = Key.DeepCopy();

            IBlake2BTreeConfig? blake2BTreeConfig = new Blake2BTreeConfig();
            blake2BTreeConfig.FanOut = (byte)ParallelismDegree;
            blake2BTreeConfig.MaxDepth = 2;
            blake2BTreeConfig.NodeDepth = 0;
            blake2BTreeConfig.LeafSize = 0;
            blake2BTreeConfig.NodeOffset = a_Offset;
            blake2BTreeConfig.InnerHashSize = (byte)OutSizeInBytes;

            if (a_Offset == (ulong)(ParallelismDegree - 1))
                blake2BTreeConfig.IsLastNode = true;

            return Blake2BPCreateLeafParam(blake2BConfig, blake2BTreeConfig);
        }

        private Blake2B Blake2BPCreateRoot()
        {
            IBlake2BConfig blake2BConfig = new Blake2BConfig(HashSize);

            blake2BConfig.Key = Key.DeepCopy();

            IBlake2BTreeConfig? blake2BTreeConfig = new Blake2BTreeConfig();
            blake2BTreeConfig.FanOut = (byte)ParallelismDegree;
            blake2BTreeConfig.MaxDepth = 2;
            blake2BTreeConfig.NodeDepth = 1;
            blake2BTreeConfig.LeafSize = 0;
            blake2BTreeConfig.NodeOffset = 0;
            blake2BTreeConfig.InnerHashSize = (byte)OutSizeInBytes;
            blake2BTreeConfig.IsLastNode = true;

            return new Blake2B(blake2BConfig, blake2BTreeConfig, false);
        }

        private unsafe void ParallelComputation(int Idx, ref DataContainer a_DataContainer)
        {
            var temp = new byte[BlockSizeInBytes];

            var ptrData = (byte*)a_DataContainer.PtrData;

            var counter = a_DataContainer.Counter;

            ptrData += (Idx * BlockSizeInBytes);

            while (counter >= (ulong)(ParallelismDegree * BlockSizeInBytes))
            {
                fixed (byte* tempPtr = temp)
                {
                    Utils.Utils.Memmove((IntPtr)tempPtr, (IntPtr)ptrData, BlockSizeInBytes);

                    LeafHashes[Idx].TransformBytes(temp, 0, BlockSizeInBytes);

                    ptrData += ((ulong)(ParallelismDegree * BlockSizeInBytes));
                    counter = counter - (ulong)(ParallelismDegree * BlockSizeInBytes);
                }
            }
        }

        private void DoParallelComputation(ref DataContainer a_DataContainer)
        {
            for (var i = 0; i < ParallelismDegree; i++)
                ParallelComputation(i, ref a_DataContainer);
        }

        private void Clear()
        {
            ArrayUtils.ZeroFill(ref Key);
        }

        private struct DataContainer
        {
            public IntPtr PtrData;
            public ulong Counter;
        } // end struct DataContainer
    } // end class Blake2BP
}