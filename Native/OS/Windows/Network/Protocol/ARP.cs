using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using Yannick.Extensions.GenericExtensions.ArrayExtensions;
using Yannick.Native.OS.Windows.Win32;
using Yannick.Network;
using Yannick.Network.Protocol;
using IPAddress = Yannick.Network.IPAddress;
using static Yannick.Native.OS.Windows.Lang.Constants;

namespace Yannick.Native.OS.Windows.Network.Protocol;

/// <summary>
/// Provides methods for working with the Address Resolution Protocol (ARP).
/// </summary>
public sealed class ARP
{
    public enum Type : uint
    {
        Other = 0x00000001,
        Invalid = 0x00000002,
        Dynamic = 0x00000003,
        Static = 0x00000004
    }

    /// <summary>
    /// Gets the MAC address associated with the specified IP address.
    /// </summary>
    /// <param name="ipAddress">The IP address to query.</param>
    /// <returns>The MAC address associated with the specified IP address.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided IP address is invalid.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the ARP request fails.</exception>
    public static MACAddress GetMacAddress(string ipAddress) => GetMacAddress((IPAddress)ipAddress);

    /// <summary>
    /// Gets the MAC address associated with the specified IP address.
    /// </summary>
    /// <param name="ipAddress">The IP address to query.</param>
    /// <returns>The MAC address associated with the specified IP address.</returns>
    /// <exception cref="ArgumentException">Thrown when the provided IP address is invalid.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the ARP request fails.</exception>
    public static MACAddress GetMacAddress(IPAddress ipAddress)
    {
        var dstIpAddr = BitConverter.ToInt32(ipAddress, 0);

        var macAddr = new byte[6];
        var macAddrLen = (uint)macAddr.Length;

        if (Iphlpapi.SendArp(dstIpAddr, 0, macAddr, ref macAddrLen) != 0)
            throw new InvalidOperationException("ARP request failed.");

        return BitConverter.ToString(macAddr, 0, (int)macAddrLen);
    }

    /// <summary>
    /// Retrieves the ARP table of the local computer.
    /// </summary>
    /// <returns>A list of entries in the ARP table.</returns>
    /// <exception cref="InvalidOperationException">Thrown when retrieving the ARP table fails.</exception>
    private static List<Iphlpapi.MibIpnetrow> _GetArpTable()
    {
        uint size = 0;
        Iphlpapi.GetIpNetTable(IntPtr.Zero, ref size, false);
        var pNetTable = Marshal.AllocHGlobal((int)size);

        try
        {
            if (Iphlpapi.GetIpNetTable(pNetTable, ref size, false) != 0)
                throw new InvalidOperationException("Failed to retrieve ARP table.");

            var entries = Marshal.ReadInt32(pNetTable);
            var current = new IntPtr(pNetTable.ToInt64() + Marshal.SizeOf(typeof(int)));
            var rows = new List<Iphlpapi.MibIpnetrow>();

            for (var i = 0; i < entries; i++)
            {
                var row = Marshal.PtrToStructure<Iphlpapi.MibIpnetrow>(current);
                rows.Add(row);
                current = new IntPtr(current.ToInt64() + Marshal.SizeOf(typeof(Iphlpapi.MibIpnetrow)));
            }

            return rows;
        }
        finally
        {
            Marshal.FreeHGlobal(pNetTable);
        }
    }

    /// <summary>
    /// Retrieves the ARP table of the local computer.
    /// </summary>
    /// <returns>A list of entries in the ARP table.</returns>
    /// <exception cref="InvalidOperationException">Thrown when retrieving the ARP table fails.</exception>
    public static IEnumerable<Entry> GetArpTable()
    {
        var interfaces = NetworkInterface.GetAllNetworkInterfaces()
            .ToDictionary(ni => ni.GetIPProperties().GetIPv4Properties().Index);

        foreach (var entry in _GetArpTable())
            yield return new Entry
            {
                Interface = interfaces[(int)entry.Index],
                MacAddress = new MACAddress(entry.PhysAddr.SkipLast(2)),
                IPAddress = entry.Addr,
                Type = (Type)entry.Type
            };
    }

    /// <summary>
    /// Adds a static ARP entry to the ARP table.
    /// </summary>
    /// <param name="ipAddress">The IP address of the entry to add.</param>
    /// <param name="macAddress">The MAC address of the entry to add.</param>
    /// <param name="interfaceIndex">The interface index for the ARP entry.</param>
    /// <returns>True if the entry was added successfully, false otherwise.</returns>
    public static bool AddArpEntry(string ipAddress, string macAddress, uint interfaceIndex)
        => AddArpEntry(ipAddress, (MACAddress)macAddress, interfaceIndex);

    /// <summary>
    /// Adds a static ARP entry to the ARP table.
    /// </summary>
    /// <param name="ipAddress">The IP address of the entry to add.</param>
    /// <param name="macAddress">The MAC address of the entry to add.</param>
    /// <param name="interfaceIndex">The interface index for the ARP entry.</param>
    /// <returns>True if the entry was added successfully, false otherwise.</returns>
    public static bool AddArpEntry(IPAddress ipAddress, MACAddress macAddress, uint interfaceIndex)
    {
        var arpEntry = new Iphlpapi.MibIpnetrowLh
        {
            Index = interfaceIndex,
            PhysAddrLen = 6,
            PhysAddr = macAddress.Address.Add<byte>(RESERVED, RESERVED),
            Addr = ipAddress,
            Type = 4,
            Age = 0
        };

        return Iphlpapi.CreateIpNetEntry(ref arpEntry) == 0;
    }

    /// <summary>
    /// Deletes an ARP entry from the ARP table.
    /// </summary>
    /// <param name="ipAddress">The IP address of the entry to delete.</param>
    /// <param name="interfaceIndex">The interface index for the ARP entry.</param>
    /// <returns>True if the entry was deleted successfully, false otherwise.</returns>
    public static bool DeleteArpEntry(string ipAddress, uint interfaceIndex)
        => DeleteArpEntry((IPAddress)ipAddress, interfaceIndex);

    /// <summary>
    /// Deletes an ARP entry from the ARP table.
    /// </summary>
    /// <param name="ipAddress">The IP address of the entry to delete.</param>
    /// <param name="interfaceIndex">The interface index for the ARP entry.</param>
    /// <returns>True if the entry was deleted successfully, false otherwise.</returns>
    public static bool DeleteArpEntry(IPAddress ipAddress, uint interfaceIndex)
    {
        var arpEntry = new Iphlpapi.MibIpnetrow
        {
            Index = interfaceIndex,
            PhysAddrLen = 6,
            Addr = ipAddress,
            Type = 4
        };

        return Iphlpapi.DeleteIpNetEntry(ref arpEntry) == 0;
    }

    /// <summary>
    /// Clears the ARP table for the specified interface.
    /// </summary>
    /// <param name="interfaceIndex">The interface index for which to clear the ARP table.</param>
    /// <returns>True if the table was cleared successfully, false otherwise.</returns>
    public static bool FlushArpTable(uint interfaceIndex)
    {
        return Iphlpapi.FlushIpNetTable(interfaceIndex) == 0;
    }

    public readonly struct Entry
    {
        public NetworkInterface Interface { get; init; }
        public MACAddress MacAddress { get; init; }
        public IPAddress IPAddress { get; init; }
        public Type Type { get; init; }
    }
}