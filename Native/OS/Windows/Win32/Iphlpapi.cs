using System.Runtime.InteropServices;

namespace Yannick.Native.OS.Windows.Win32;

public static partial class Iphlpapi
{
    public const string Dll = "iphlpapi.dll";

    #region ARP

    /// <summary>
    /// Sends an ARP request to obtain the physical address that corresponds to the specified IPv4 address.
    /// </summary>
    /// <param name="destIp">The destination IPv4 address.</param>
    /// <param name="srcIp">The source IPv4 address of the sender. This parameter is not used.</param>
    /// <param name="macAddress">A pointer to an array of bytes that receives the physical address.</param>
    /// <param name="macAddressLength">A pointer to an int variable that specifies the maximum buffer size.</param>
    /// <returns>If the function succeeds, the return value is NO_ERROR. Otherwise, the return value is an error code.</returns>
    [LibraryImport(Dll)]
    public static partial int SendArp(int destIp, int srcIp, byte[] macAddress, ref uint macAddressLength);

    /// <summary>
    /// Retrieves the ARP table of the local computer.
    /// </summary>
    /// <param name="pNetTable">A pointer to a buffer that receives the ARP table.</param>
    /// <param name="pdwSize">The size of the buffer pointed to by the pNetTable parameter.</param>
    /// <param name="bOrder">If this parameter is TRUE, the function sorts the ARP entries by IP address.</param>
    /// <returns>If the function succeeds, the return value is NO_ERROR. Otherwise, the return value is an error code.</returns>
    [LibraryImport(Dll, SetLastError = true)]
    public static partial int GetIpNetTable(IntPtr pNetTable, ref uint pdwSize,
        [MarshalAs(UnmanagedType.Bool)] bool bOrder);

    /// <summary>
    /// Adds a static ARP entry to the ARP cache.
    /// </summary>
    /// <param name="pArpEntry">A reference to an MIB_IPNETROW_LH structure that specifies the ARP entry to add.</param>
    /// <returns>If the function succeeds, the return value is NO_ERROR.</returns>
    [DllImport(Dll, SetLastError = true)]
    public static extern uint CreateIpNetEntry(ref MibIpnetrowLh pArpEntry);

    /// <summary>
    /// Deletes an ARP entry from the ARP cache.
    /// </summary>
    /// <param name="pArpEntry">A reference to an MIB_IPNETROW structure that specifies the ARP entry to delete.</param>
    /// <returns>If the function succeeds, the return value is NO_ERROR.</returns>
    [DllImport(Dll, SetLastError = true)]
    public static extern uint DeleteIpNetEntry(ref MibIpnetrow pArpEntry);

    /// <summary>
    /// Removes all ARP entries for the specified interface from the ARP table.
    /// </summary>
    /// <param name="dwIfIndex">The index of the interface for which to remove ARP entries.</param>
    /// <returns>The status of the operation. A return value of 0 indicates success, otherwise it indicates an error code.</returns>
    [DllImport(Dll, SetLastError = true)]
    public static extern uint FlushIpNetTable(uint dwIfIndex);

    #region STRUCTS

    [StructLayout(LayoutKind.Sequential)]
    public struct MibIpnetrow
    {
        public uint Index;
        public uint PhysAddrLen;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] PhysAddr;

        public uint Addr;
        public uint Type;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct MibIpnetrowLh
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

    #endregion

    #region ICMP

    #region STRUCTS

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct IpOptionInformation
    {
        public byte Ttl;
        public byte Tos;
        public byte Flags;
        public byte OptionsSize;
        public IntPtr OptionsData;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct IcmpEchoReply
    {
        public uint Address;
        public uint Status;
        public uint RoundTripTime;
        public ushort DataSize;
        public ushort Reserved;
        public IntPtr Data;
        public IpOptionInformation Options;
    }

    #endregion

    /// <summary>
    /// Sends an IPv4 ICMP Echo Request to the specified destination.
    /// </summary>
    /// <param name="destinationAddress">IPv4 address of the destination.</param>
    /// <param name="requestData">Pointer to a buffer that contains the data to send in the request.</param>
    /// <param name="requestSize">Size, in bytes, of the request data buffer.</param>
    /// <param name="requestOptions">Pointer to the IP header options for the request, in the form of an IP_OPTION_INFORMATION structure.</param>
    /// <param name="replyBuffer">Pointer to the buffer to hold any replies. The buffer must be large enough to hold at least one ICMP_ECHO_REPLY structure plus RequestSize bytes of data.</param>
    /// <param name="replySize">Size, in bytes, of the reply buffer.</param>
    /// <param name="timeout">Time, in milliseconds, to wait for replies.</param>
    /// <returns>If the function succeeds, the return value is the number of ICMP_ECHO_REPLY structures stored in the ReplyBuffer. If the function fails, the return value is zero.</returns>
    [DllImport(Dll, SetLastError = true)]
    public static extern uint IcmpSendEcho(IntPtr icmpHandle, uint destinationAddress, byte[] requestData,
        short requestSize, IntPtr requestOptions, IntPtr replyBuffer, uint replySize, uint timeout);

    /// <summary>
    /// Sends an IPv4 ICMP Echo Request to the specified destination.
    /// </summary>
    /// <param name="destinationAddress">IPv4 address of the destination.</param>
    /// <param name="requestData">Pointer to a buffer that contains the data to send in the request.</param>
    /// <param name="requestSize">Size, in bytes, of the request data buffer.</param>
    /// <param name="requestOptions">Pointer to the IP header options for the request, in the form of an IP_OPTION_INFORMATION structure.</param>
    /// <param name="replyBuffer">Pointer to the buffer to hold any replies. The buffer must be large enough to hold at least one ICMP_ECHO_REPLY structure plus RequestSize bytes of data.</param>
    /// <param name="replySize">Size, in bytes, of the reply buffer.</param>
    /// <param name="timeout">Time, in milliseconds, to wait for replies.</param>
    /// <returns>If the function succeeds, the return value is the number of ICMP_ECHO_REPLY structures stored in the ReplyBuffer. If the function fails, the return value is zero.</returns>
    [DllImport(Dll, SetLastError = true)]
    public static extern uint IcmpSendEcho(IntPtr icmpHandle, uint destinationAddress, byte[] requestData,
        short requestSize, ref IpOptionInformation requestOptions, IntPtr replyBuffer, uint replySize, uint timeout);

    /// <summary>
    /// Opens a handle on which ICMP Echo Requests can be issued.
    /// </summary>
    /// <returns>If the function succeeds, the return value is a valid handle to the ICMP request.
    /// If the function fails, the return value is INVALID_HANDLE_VALUE.</returns>
    [DllImport(Dll, SetLastError = true)]
    public static extern IntPtr IcmpCreateFile();

    /// <summary>
    /// Closes the ICMP handle.
    /// </summary>
    /// <param name="icmpHandle">ICMP handle to close.</param>
    /// <returns>If the function succeeds, the return value is true. If the function fails, the return value is false.</returns>
    [DllImport(Dll, SetLastError = true)]
    public static extern bool IcmpCloseHandle(IntPtr icmpHandle);

    #endregion
}