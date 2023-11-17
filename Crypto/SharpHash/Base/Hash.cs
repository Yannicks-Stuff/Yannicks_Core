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

using System.Text;
using Yannick.Crypto.SharpHash.Interfaces;
using Yannick.Crypto.SharpHash.Utils;

namespace Yannick.Crypto.SharpHash.Base
{
    internal abstract class Hash : Interfaces.IHash
    {
        protected static int BUFFER_SIZE = 64 * 1024; // 64Kb

        private static string IndexOutOfRange = "Current Index Is Out Of Range";
        private static string InvalidBufferSize = "\"BufferSize\" Must Be Greater Than Zero";
        private static string UnAssignedStream = "Input Stream Is Unassigned";
        private static string FileNotExist = "Specified File Not Found";
        private static string CloneNotYetImplemented = "Clone Not Yet Implemented For \"{0}\"";
        protected int block_size;
        private int buffer_size;
        protected int hash_size;

        public Hash(int a_hash_size, int a_block_size)
        {
            block_size = a_block_size;
            hash_size = a_hash_size;
            buffer_size = BUFFER_SIZE;
        } // end constructor

        public virtual string Name => GetType().Name;

        public virtual int BufferSize
        {
            get => buffer_size;
            set
            {
                if (value > 0)
                {
                    buffer_size = value;
                } // end if
                else
                {
                    throw new ArgumentHashLibException(InvalidBufferSize);
                } // end else
            }
        } // end property BufferSize

        public virtual int BlockSize
        {
            get => block_size;
            set => block_size = value;
        }

        public virtual int HashSize
        {
            get => hash_size;
            set => hash_size = value;
        }

        public abstract void Initialize();

        public virtual Interfaces.IHash? Clone()
        {
            throw new NotImplementedHashLibException(string.Format(CloneNotYetImplemented, Name));
        } // end function Clone

        public virtual IHashResult ComputeString(string a_data, Encoding encoding)
        {
            return ComputeBytes(Converters.ConvertStringToBytes(a_data, encoding));
        } // end function ComputeString

        public virtual IHashResult ComputeUntyped(IntPtr a_data, long a_length)
        {
            Initialize();
            TransformUntyped(a_data, a_length);
            return TransformFinal();
        } // end function ComputeUntyped

        public virtual void TransformUntyped(IntPtr a_data, long a_length)
        {
            unsafe
            {
                byte* PtrBuffer, PtrEnd;
                byte[]? ArrBuffer = { };
                int LBufferSize;

                PtrBuffer = (byte*)a_data;

                LBufferSize = buffer_size > a_length ? BUFFER_SIZE : buffer_size;

                if (PtrBuffer != null)
                {
                    Array.Resize(ref ArrBuffer, LBufferSize);
                    PtrEnd = (PtrBuffer) + a_length;

                    while (PtrBuffer < PtrEnd)
                    {
                        if ((PtrEnd - PtrBuffer) >= LBufferSize)
                        {
                            fixed (byte* bDest = ArrBuffer)
                            {
                                Utils.Utils.Memmove((IntPtr)bDest, (IntPtr)PtrBuffer, LBufferSize);
                            }

                            TransformBytes(ArrBuffer);
                            PtrBuffer += LBufferSize;
                        } // end if
                        else
                        {
                            Array.Resize(ref ArrBuffer, (int)(PtrEnd - PtrBuffer));
                            fixed (byte* bDest = ArrBuffer)
                            {
                                Utils.Utils.Memmove((IntPtr)bDest, (IntPtr)PtrBuffer, ArrBuffer.Length);
                            }

                            TransformBytes(ArrBuffer);
                            break;
                        } // end else
                    } // end while
                } // end if
            }
        } // end function TransformUntyped

        public virtual IHashResult ComputeStream(Stream a_stream, long a_length = -1)
        {
            Initialize();
            TransformStream(a_stream, a_length);
            return TransformFinal();
        } // end function ComputeStream

        public virtual IHashResult ComputeFile(string a_file_name,
            long a_from = 0, long a_length = -1)
        {
            Initialize();
            TransformFile(a_file_name, a_from, a_length);
            return TransformFinal();
        } // end function ComputeFile

        public virtual IHashResult ComputeBytes(byte[]? a_data)
        {
            Initialize();
            TransformBytes(a_data);
            return TransformFinal();
        } // end function ComputeBytes

        public virtual void TransformString(string a_data, Encoding encoding)
        {
            TransformBytes(Converters.ConvertStringToBytes(a_data, encoding));
        } // end function TransformString

        public virtual void TransformBytes(byte[]? a_data)
        {
            TransformBytes(a_data, 0, a_data?.Length ?? 0);
        } // end function TransformBytes

        public virtual void TransformBytes(byte[]? a_data, int a_index)
        {
            if (a_data.Empty()) return;
            TransformBytes(a_data, a_index, a_data.Length - a_index);
        } // end function TransformBytes

        public abstract void TransformBytes(byte[]? a_data, int a_index, int a_length);

        public virtual void TransformStream(Stream a_stream, long a_length = -1)
        {
            int readed = 0, LBufferSize;
            ulong size, new_size;
            long total;

            total = 0;
            size = (ulong)(a_stream?.Length ?? 0);

            if (a_stream != null)
            {
                if (a_length > -1)
                {
                    if ((ulong)(a_stream.Position + a_length) > size)
                        throw new IndexOutOfRangeHashLibException(IndexOutOfRange);
                } // end if

                if (a_stream.Position >= (int)size)
                    return;
            } // end if
            else
                throw new ArgumentNullHashLibException(UnAssignedStream);

            if ((int)size > BUFFER_SIZE)
            {
                if (a_length == -1) LBufferSize = BUFFER_SIZE;
                else
                {
                    LBufferSize = (int)(a_length > BUFFER_SIZE ? BUFFER_SIZE : a_length);
                }
            }
            else
            {
                LBufferSize = (int)(a_length == -1 ? (int)size : a_length);
            }

            var data = new byte[LBufferSize];

            if (LBufferSize == BUFFER_SIZE)
            {
                while (true)
                {
                    readed = a_stream.Read(data, 0, LBufferSize);

                    if (readed != BUFFER_SIZE)
                    {
                        Array.Resize(ref data, readed);

                        TransformBytes(data, 0, readed);

                        break;
                    }

                    if (readed == 0) break;

                    total = total + readed;

                    TransformBytes(data, 0, readed);

                    if (a_length != -1 && a_length - total <= BUFFER_SIZE)
                    {
                        new_size = (ulong)(a_length - total);
                        Array.Resize(ref data, (int)new_size);

                        a_stream.Read(data, 0, (int)new_size);

                        TransformBytes(data, 0, (int)new_size);
                        break;
                    }
                } // end while
            }
            else
            {
                a_stream.Read(data, 0, LBufferSize);

                TransformBytes(data, 0, LBufferSize);
            }
        } // end function TransformStream

        public virtual void TransformFile(string a_file_name,
            long a_from = 0, long a_length = -1)
        {
            Stream ReadFile = File.OpenRead(a_file_name);

            if (!ReadFile.CanRead)
                throw new ArgumentHashLibException(FileNotExist);

            ReadFile.Position = 0;

            TransformStream(ReadFile, a_length);

            ReadFile.Close();
        } // end function TransformFile

        public abstract IHashResult TransformFinal();
    }
}