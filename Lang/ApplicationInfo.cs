using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Yannick.Lang
{
    /// <summary>
    /// Provides the absolute directory of the running executable, resolved lazily and cached.
    /// </summary>
    public static class ApplicationInfo
    {
        private static readonly Lazy<string?> _executableDirectory =
            new(DetermineExecutableDirectory, LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Absolute directory containing the application executable. Trailing directory-separator guaranteed.
        /// Can be <c>null</c> (e.g. Blazor WebAssembly).
        /// </summary>
        public static string? ExecutableDirectory => _executableDirectory.Value;

        private static string? DetermineExecutableDirectory()
        {
            // Running inside a browser (Blazor WASM) – local file-system paths don’t apply.
            if (OperatingSystem.IsBrowser())
                return null;

            foreach (var provider in new[]
                     {
                         // .NET 6+ – preferred: full path of the current process executable
                         () => Path.GetDirectoryName(Environment.ProcessPath),

                         // Standard for classic .NET apps and single-file publish
                         () => AppContext.BaseDirectory,

                         // Managed entry assembly location (may be empty in single-file publish)
                         () => Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location),

                         // Fallback: native module path of the current process
                         () =>
                         {
                             using var p = Process.GetCurrentProcess();
                             return Path.GetDirectoryName(p.MainModule?.FileName);
                         }
                     })
            {
                try
                {
                    var dir = provider();
                    if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir))
                        continue;

                    return dir.EndsWith(Path.DirectorySeparatorChar)
                        ? dir
                        : dir + Path.DirectorySeparatorChar;
                }
                catch
                {
                    // Ignore and try the next provider
                }
            }

            return null;
        }
    }
}