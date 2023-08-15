using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Yannick.Native
{
    public sealed class Library : IDisposable
    {
        private static Delegate Close;
        private static Delegate Load;
        private static Delegate MethodFind;
        private readonly IntPtr Handle;

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

        private Library(){}
        
        /// <summary>
        /// Erstellt eine neue Instanz der <see cref="Library"/> Klasse und lädt die angegebene native Bibliothek.
        /// </summary>
        /// <param name="path">Der Pfad zur nativen Bibliotheksdatei. Wenn der Pfad nicht absolut ist, wird versucht, die Datei im Systemverzeichnis oder in standardmäßigen Bibliotheksverzeichnissen zu finden.</param>
        /// <exception cref="FileNotFoundException">Wird geworfen, wenn die angegebene Bibliotheksdatei nicht gefunden wurde.</exception>
        /// <exception cref="InvalidOperationException">Wird geworfen, wenn die Bibliothek nicht geladen werden kann.</exception>
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
        /// Ruft den angegebenen Funktionszeiger aus der nativen Bibliothek ab und wandelt ihn in einen Delegaten um.
        /// </summary>
        /// <param name="name">Der Name der Funktion in der nativen Bibliothek.</param>
        /// <param name="method">Der Delegatentyp, der die Signatur der Funktion repräsentiert.</param>
        /// <returns>Ein Delegat, das die Funktion repräsentiert, oder null, wenn die Funktion nicht gefunden wurde.</returns>
        public Delegate? this[string name, Delegate method] => _Link(name, method.GetType());
        
        private Delegate? _Link(string name, Type type)
        {
            var pointer = (IntPtr)MethodFind.DynamicInvoke(Handle, name)!;
            return pointer == IntPtr.Zero ? null : Marshal.GetDelegateForFunctionPointer(pointer, type);
        }
        
        /// <summary>
        /// Ruft den angegebenen Funktionszeiger aus der nativen Bibliothek ab und wandelt ihn in einen Delegaten vom angegebenen Typ um.
        /// </summary>
        /// <typeparam name="T">Der Typ des Delegaten, der die Signatur der Funktion repräsentiert.</typeparam>
        /// <param name="name">Der Name der Funktion in der nativen Bibliothek.</param>
        /// <returns>Ein Delegat vom angegebenen Typ, das die Funktion repräsentiert, oder null, wenn die Funktion nicht gefunden wurde.</returns>
        public T? Link<T>(string name) where T : Delegate
            => (T?) _Link(name, typeof(T));

        public void Dispose()
        {
            Close.DynamicInvoke(Handle);
        }
    }
}





























