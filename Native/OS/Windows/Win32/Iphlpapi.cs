using System.Runtime.InteropServices;

namespace Yannick.Native.OS.Windows.Win32;

public static partial class Iphlpapi
{
    public const string DLL = "iphlpapi.dll";

    /// <summary>
    /// Sends an ARP request to obtain the physical address that corresponds to the specified IPv4 address.
    /// </summary>
    /// <param name="destIP">The destination IPv4 address.</param>
    /// <param name="srcIP">The source IPv4 address of the sender. This parameter is not used.</param>
    /// <param name="macAddress">A pointer to an array of bytes that receives the physical address.</param>
    /// <param name="macAddressLength">A pointer to an int variable that specifies the maximum buffer size.</param>
    /// <returns>If the function succeeds, the return value is NO_ERROR. Otherwise, the return value is an error code.</returns>
    [LibraryImport(DLL)]
    public static partial int SendARP(int destIP, int srcIP, byte[] macAddress, ref uint macAddressLength);

    /// <summary>
    /// Retrieves the ARP table of the local computer.
    /// </summary>
    /// <param name="pNetTable">A pointer to a buffer that receives the ARP table.</param>
    /// <param name="pdwSize">The size of the buffer pointed to by the pNetTable parameter.</param>
    /// <param name="bOrder">If this parameter is TRUE, the function sorts the ARP entries by IP address.</param>
    /// <returns>If the function succeeds, the return value is NO_ERROR. Otherwise, the return value is an error code.</returns>
    [LibraryImport(DLL, SetLastError = true)]
    public static partial int GetIpNetTable(IntPtr pNetTable, ref uint pdwSize,
        [MarshalAs(UnmanagedType.Bool)] bool bOrder);

    /// <summary>
    /// Adds a static ARP entry to the ARP cache.
    /// </summary>
    /// <param name="pArpEntry">A reference to an MIB_IPNETROW_LH structure that specifies the ARP entry to add.</param>
    /// <returns>If the function succeeds, the return value is NO_ERROR.</returns>
    [DllImport(DLL, SetLastError = true)]
    public static extern uint CreateIpNetEntry(ref MIB_IPNETROW_LH pArpEntry);

    /// <summary>
    /// Deletes an ARP entry from the ARP cache.
    /// </summary>
    /// <param name="pArpEntry">A reference to an MIB_IPNETROW structure that specifies the ARP entry to delete.</param>
    /// <returns>If the function succeeds, the return value is NO_ERROR.</returns>
    [DllImport(DLL, SetLastError = true)]
    public static extern uint DeleteIpNetEntry(ref MIB_IPNETROW pArpEntry);

    /// <summary>
    /// Removes all ARP entries for the specified interface from the ARP table.
    /// </summary>
    /// <param name="dwIfIndex">The index of the interface for which to remove ARP entries.</param>
    /// <returns>The status of the operation. A return value of 0 indicates success, otherwise it indicates an error code.</returns>
    [DllImport(DLL, SetLastError = true)]
    public static extern uint FlushIpNetTable(uint dwIfIndex);

    #region STRUCTS

    [StructLayout(LayoutKind.Sequential)]
    public struct MIB_IPNETROW
    {
        public uint Index;
        public uint PhysAddrLen;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] PhysAddr;

        public uint Addr;
        public uint Type;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct MIB_IPNETROW_LH
    {
        [FieldOffset(0)] public uint Index;
        [FieldOffset(4)] public uint PhysAddrLen;

        [FieldOffset(8)] [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] PhysAddr;

        [FieldOffset(16)] public uint Addr;
        [FieldOffset(20)] public uint Type;
        [FieldOffset(24)] public uint Age;
        [FieldOffset(24)] public uint LastReachable;
    }

    #endregion
}