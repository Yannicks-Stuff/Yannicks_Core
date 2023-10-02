using System.Diagnostics;
using System.Runtime.InteropServices;
using Yannick.Native.OS.Windows.Win32;
using static Yannick.Native.OS.Windows.Win32.Kernel32;

namespace Yannick.Native.OS.Windows;

public class Memory
{
    public enum DataType
    {
        Int32,
        UInt32,
        Int16,
        UInt16,
        Byte,
        SByte,
        Float,
        Double,
        Long,
        ULong
    }

    private List<IntPtr> _currentAddresses = new();


    private IntPtr _processHandle;

    public Memory(string processName)
    {
        _processHandle = Memory.GetProcessHandle(processName);
        if (_processHandle == IntPtr.Zero)
        {
            throw new Exception($"Could not open process {processName}");
        }
    }

    public object Read(IntPtr address, DataType dataType)
    {
        switch (dataType)
        {
            case DataType.Int32:
                return BitConverter.ToInt32(ReadMemory(_processHandle, address, 4), 0);
            case DataType.UInt32:
                return BitConverter.ToUInt32(ReadMemory(_processHandle, address, 4), 0);
            case DataType.Int16:
                return BitConverter.ToInt16(ReadMemory(_processHandle, address, 2), 0);
            case DataType.UInt16:
                return BitConverter.ToUInt16(ReadMemory(_processHandle, address, 2), 0);
            case DataType.Byte:
                return ReadMemory(_processHandle, address, 1)[0];
            case DataType.SByte:
                return (sbyte)ReadMemory(_processHandle, address, 1)[0];
            case DataType.Float:
                return BitConverter.ToSingle(ReadMemory(_processHandle, address, 4), 0);
            case DataType.Double:
                return BitConverter.ToDouble(ReadMemory(_processHandle, address, 8), 0);
            case DataType.Long:
                return BitConverter.ToInt64(ReadMemory(_processHandle, address, 8), 0);
            case DataType.ULong:
                return BitConverter.ToUInt64(ReadMemory(_processHandle, address, 8), 0);
            default:
                throw new ArgumentException("Unsupported data type", nameof(dataType));
        }
    }

    public void Write(IntPtr address, object value, DataType dataType)
    {
        byte[] data;
        switch (dataType)
        {
            case DataType.Int32:
                data = BitConverter.GetBytes((int)value);
                break;
            case DataType.UInt32:
                data = BitConverter.GetBytes((uint)value);
                break;
            case DataType.Int16:
                data = BitConverter.GetBytes((short)value);
                break;
            case DataType.UInt16:
                data = BitConverter.GetBytes((ushort)value);
                break;
            case DataType.Byte:
                data = new byte[] { (byte)value };
                break;
            case DataType.SByte:
                data = new byte[] { (byte)(sbyte)value };
                break;
            case DataType.Float:
                data = BitConverter.GetBytes((float)value);
                break;
            case DataType.Double:
                data = BitConverter.GetBytes((double)value);
                break;
            case DataType.Long:
                data = BitConverter.GetBytes((long)value);
                break;
            case DataType.ULong:
                data = BitConverter.GetBytes((ulong)value);
                break;
            default:
                throw new ArgumentException("Unsupported data type", nameof(dataType));
        }

        WriteMemory(_processHandle, address, data);
    }


    public IntPtr FindPattern(byte[] pattern, byte?[] mask)
    {
        var currentAddress = IntPtr.Zero;
        var mbi = new MEMORY_BASIC_INFORMATION();

        while (VirtualQueryEx(_processHandle, currentAddress, out mbi,
                   (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))))
        {
            if (mbi is { State: PROCESS_WM_READ, Protect: 0x04 or PROCESS_VM_WRITE })
            {
                var block = new byte[mbi.RegionSize.ToInt32()];
                ReadMemory(_processHandle, mbi.BaseAddress, (uint)mbi.RegionSize.ToInt32());

                for (var i = 0; i < block.Length - pattern.Length; i++)
                {
                    var isMatch = true;
                    for (var j = 0; j < pattern.Length; j++)
                    {
                        if (!mask[j].HasValue || mask[j].Value == block[i + j]) continue;
                        isMatch = false;
                        break;
                    }

                    if (isMatch)
                    {
                        return new IntPtr(mbi.BaseAddress.ToInt64() + i);
                    }
                }
            }

            currentAddress = new IntPtr(mbi.BaseAddress.ToInt64() + mbi.RegionSize.ToInt64());
        }

        return IntPtr.Zero;
    }

    public void ResetSearch() => _currentAddresses.Clear();

    public List<IntPtr> InitialSearch<T>(T value) where T : struct
    {
        _currentAddresses.Clear();

        var searchData = new byte[Marshal.SizeOf(typeof(T))];
        var handle = GCHandle.Alloc(searchData, GCHandleType.Pinned);
        try
        {
            Marshal.StructureToPtr(value, handle.AddrOfPinnedObject(), false);
        }
        finally
        {
            handle.Free();
        }

        var currentAddress = IntPtr.Zero;
        var mbi = new MEMORY_BASIC_INFORMATION();

        while (VirtualQueryEx(_processHandle, currentAddress, out mbi,
                   (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))))
        {
            if (mbi.State == 0x1000 && mbi.Protect is 0x04 or 0x20)
            {
                var block = new byte[mbi.RegionSize.ToInt32()];
                ReadMemory(_processHandle, mbi.BaseAddress, (uint)mbi.RegionSize.ToInt32());

                for (var i = 0; i < block.Length - searchData.Length; i++)
                {
                    var isMatch = true;
                    for (var j = 0; j < searchData.Length; j++)
                    {
                        if (searchData[j] != block[i + j])
                        {
                            isMatch = false;
                            break;
                        }
                    }

                    if (isMatch)
                    {
                        _currentAddresses.Add(new IntPtr(mbi.BaseAddress.ToInt64() + i));
                    }
                }
            }

            currentAddress = new IntPtr(mbi.BaseAddress.ToInt64() + mbi.RegionSize.ToInt64());
        }

        return _currentAddresses;
    }

    public List<IntPtr> NextSearch<T>(T value) where T : struct
    {
        var searchData = new byte[Marshal.SizeOf(typeof(T))];
        var handle = GCHandle.Alloc(searchData, GCHandleType.Pinned);
        try
        {
            Marshal.StructureToPtr(value, handle.AddrOfPinnedObject(), false);
        }
        finally
        {
            handle.Free();
        }

        _currentAddresses = _currentAddresses.Where(address =>
        {
            var currentData = ReadMemory(_processHandle, address, (uint)searchData.Length);
            return currentData.SequenceEqual(searchData);
        }).ToList();

        return _currentAddresses;
    }

    public static IntPtr GetProcessHandle(string processName)
    {
        var processes = Process.GetProcessesByName(processName);
        return processes.Length == 0
            ? IntPtr.Zero
            : OpenProcess(PROCESS_WM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION, false, processes[0].Id);
    }

    public static byte[] ReadMemory(IntPtr processHandle, IntPtr address, uint size)
    {
        var buffer = new byte[size];
        ReadProcessMemory(processHandle, address, buffer, size, out _);
        return buffer;
    }

    public static void WriteMemory(IntPtr processHandle, IntPtr address, byte[] data)
    {
        WriteProcessMemory(processHandle, address, data, (uint)data.Length, out _);
    }
}