using System.Runtime.Versioning;
using System.Security.Principal;

namespace Yannick.Native.OS.Windows;

public class Account
{
    [SupportedOSPlatform("windows")]
    public static bool IsRunningAsAdmin()
    {
        using var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }
}