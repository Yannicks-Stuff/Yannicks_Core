using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Yannick.Native
{
    /// <summary>
    /// Represents a utility for loading native libraries and retrieving function pointers.
    /// </summary>
    /// <remarks>
    /// This class provides cross-platform support for loading native libraries on Windows, Linux, and macOS. 
    /// It also allows retrieving function pointers from the loaded libraries and converting them to delegates.
    /// </remarks>
    public sealed class Library : IDisposable
    {
        private static Delegate Close;
        private static Delegate Load;
        private static Delegate MethodFind;
        private readonly IntPtr Handle;

        static Library()
        {
            if (OperatingSystem.IsWindows())
            {
                Load = Windows.LoadLibrary;
                Close = Windows.FreeLibrary;
                MethodFind = Windows.GetProcAddress;
            }
            else
            {
                Load = Linux.dlopen;
                Close = Linux.dlclose;
                MethodFind = Linux.dlsym;
            }
        }

        private Library()
        {
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Library"/> class and loads the specified native library.
        /// </summary>
        /// <param name="path">The path to the native library file. If the path is not absolute, it will try to find the file in the system directory or in default library directories.</param>
        /// <exception cref="FileNotFoundException">Thrown if the specified library file is not found.</exception>.
        /// <exception cref="InvalidOperationException">Will be thrown if the library cannot be loaded.</exception>
        public Library(string path)
        {
            if (!File.Exists(path))
            {
                if (OperatingSystem.IsWindows())
                {
                    path = Path.Combine(Environment.SystemDirectory, path);
                }
                else if (OperatingSystem.IsLinux())
                {
                    path = Path.Combine("/usr/lib", path);
                }
                else if (OperatingSystem.IsMacOS())
                {
                    path = Path.Combine("/usr/lib", path);
                }
            }

            if (!File.Exists(path))
                throw new FileNotFoundException("Die angegebene Bibliotheksdatei wurde nicht gefunden.", path);

            Handle = (IntPtr)(Load.DynamicInvoke(path) ?? throw new InvalidOperationException());
        }

        /// <summary>
        /// Retrieves the specified function pointer from the native library and converts it to a delegate.
        /// </summary>
        /// <param name="name">The name of the function in the native library.</param>
        /// <param name="method">The delegate type representing the function's signature.</param>.
        /// <returns>A delegate representing the function, or null if the function was not found.</returns>.
        public Delegate? this[string name, Delegate method] => _Link(name, method.GetType());

        /// <summary>
        /// Disposes the native library, releasing the resources associated with it.
        /// </summary>
        public void Dispose()
        {
            Close.DynamicInvoke(Handle);
        }

        /// <summary>
        /// Retrieves the specified function pointer from the native library and converts it to a delegate of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the delegate representing the function's signature.</typeparam>
        /// <param name="name">The name of the function in the native library.</param>
        /// <param name="type">Represent the delegate type</param>
        /// <returns>A delegate of the specified type representing the function, or null if the function was not found.</returns>.
        private Delegate? _Link(string name, Type type)
        {
            var pointer = (IntPtr)MethodFind.DynamicInvoke(Handle, name)!;
            return pointer == IntPtr.Zero ? null : Marshal.GetDelegateForFunctionPointer(pointer, type);
        }

        /// <summary>
        /// Retrieves the specified function pointer from the native library and converts it to a delegate of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the delegate representing the function's signature.</typeparam>
        /// <param name="name">The name of the function in the native library.</param>
        /// <returns>A delegate of the specified type representing the function, or null if the function was not found.</returns>.
        public T? Link<T>(string name) where T : Delegate
            => (T?)_Link(name, typeof(T));

        /// <summary>
        /// Provides native methods for working with libraries on Windows.
        /// </summary>
        private static class Windows
        {
            private const string Library = "kernel32.dll";

            [DllImport(Library, CharSet = CharSet.Auto)]
            public static extern IntPtr LoadLibrary(string filename);

            [DllImport(Library, SetLastError = true)]
            public static extern void FreeLibrary(IntPtr module);

            [DllImport(Library, CharSet = CharSet.Auto)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string procname);
        }

        /// <summary>
        /// Provides native methods for working with libraries on Linux and macOS.
        /// </summary>
        private static class Linux
        {
            private const string Library = "libdl.so";

            [DllImport(Library, CharSet = CharSet.Auto)]
            public static extern IntPtr dlopen(string filename, int flags);

            [DllImport(Library, CharSet = CharSet.Auto)]
            public static extern IntPtr dlsym(IntPtr handle, string symbol);

            [DllImport(Library)]
            public static extern int dlclose(IntPtr handle);
        }
    }
}