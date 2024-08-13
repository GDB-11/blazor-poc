using System.Net.NetworkInformation;

namespace Gian.Basic.Helper;

public static class NetworkHelper
{
    public static string GetMacAddress()
    {
        byte[]? macAddr = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().GetAddressBytes())
            .FirstOrDefault();

        return macAddr == null
            ? throw new InvalidOperationException("No MAC address found.")
            : string.Join(":", macAddr.Select(b => b.ToString("X2")));
    }
}
