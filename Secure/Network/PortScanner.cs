using System.Net.Sockets;
using Yannick.Network;
using IPAddress = System.Net.IPAddress;

namespace Yannick.Secure.Network;

/// <summary>
/// Provides methods for scanning network ports.
/// </summary>
public class PortScanner
{
    /// <summary>
    /// Scans the specified IP address for open ports within the given range.
    /// </summary>
    /// <param name="address">The IP address to scan.</param>
    /// <param name="from">The starting port for the scan (inclusive). Default is 1.</param>
    /// <param name="to">The ending port for the scan (inclusive). Default is 65535.</param>
    /// <param name="timeout">The timeout for each port check. Default is 2 seconds.</param>
    /// <param name="protocolType">The protocol type to use for scanning (TCP/UDP). Default is TCP.</param>
    /// <param name="maxDegreeOfParallelism">The maximum number of concurrent tasks for the scan. If not provided, it defaults to the number of processor cores.</param>
    /// <returns>A list of open ports found during the scan.</returns>
    public static async Task<List<ushort>> Scan(IPAddress address, ushort? from = null, ushort? to = null,
        TimeSpan? timeout = null, ProtocolType protocolType = ProtocolType.Tcp, int? maxDegreeOfParallelism = null)
    {
        from ??= 1;
        to ??= 65535;

        var availablePorts = new List<ushort>();
        var allPorts = Enumerable.Range(from.Value, to.Value - from.Value + 1).ToList();

        maxDegreeOfParallelism ??= Environment.ProcessorCount;

        await Task.Run(() =>
        {
            Parallel.ForEach(allPorts, new ParallelOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism.Value },
                port =>
                {
                    if (!PortInfo.IsPortOpen((ushort)port, address, protocolType, timeout)) return;
                    lock (availablePorts)
                    {
                        availablePorts.Add((ushort)port);
                    }
                });
        });

        return availablePorts;
    }
}