using System.Runtime.InteropServices;

namespace Yannick.Native.OS.Windows.Win32;

public static partial class Kernel32
{
    [Flags]
    public enum AllocationType
    {
        Commit = 0x1000,
        Reserve = 0x2000,
        Decommit = 0x4000,
        Release = 0x8000,
        Reset = 0x80000,
        Physical = 0x400000,
        TopDown = 0x100000,
        WriteWatch = 0x200000,
        LargePages = 0x20000000
    }


    [Flags]
    public enum MemoryProtection
    {
        Execute = 0x10,
        ExecuteRead = 0x20,
        ExecuteReadWrite = 0x40,
        ExecuteWriteCopy = 0x80,
        NoAccess = 0x01,
        ReadOnly = 0x02,
        ReadWrite = 0x04,
        WriteCopy = 0x08,
        GuardModifierflag = 0x100,
        NoCacheModifierflag = 0x200,
        WriteCombineModifierflag = 0x400
    }

    /// <summary>
    /// Releases, decommits, or releases and decommits a region of pages within the virtual address space of the calling process.
    /// To free memory allocated in another process by the VirtualAllocEx function, use the VirtualFreeEx function.
    /// </summary>
    /// <param name="address">A pointer to the base address of the region of pages to be freed</param>
    /// <param name="size">The function frees the entire region that is reserved in the initial allocation call to VirtualAlloc</param>
    /// <param name="freeType">The type of free operation</param>
    /// <returns>If the function succeeds, the return value is nonzero</returns>
    [DllImport(DLL)]
    public static extern unsafe int VirtualFree(void* address, ulong size, uint freeType);

    /// <summary>
    /// Releases, decommits, or releases and decommits a region of pages within the virtual address space of the calling process.
    /// To free memory allocated in another process by the VirtualAllocEx function, use the VirtualFreeEx function.
    /// </summary>
    /// <param name="address">A pointer to the base address of the region of pages to be freed</param>
    /// <param name="size">The function frees the entire region that is reserved in the initial allocation call to VirtualAlloc</param>
    /// <param name="freeType">The type of free operation</param>
    /// <returns>If the function succeeds, the return value is nonzero</returns>
    [DllImport(DLL)]
    public static extern unsafe int VirtualFree(void* address, ulong size, AllocationType freeType);

    /// <summary>
    /// Releases, decommits, or releases and decommits a region of pages within the virtual address space of the calling process.
    /// To free memory allocated in another process by the VirtualAllocEx function, use the VirtualFreeEx function.
    /// </summary>
    /// <param name="address">A pointer to the base address of the region of pages to be freed</param>
    /// <param name="size">The function frees the entire region that is reserved in the initial allocation call to VirtualAlloc</param>
    /// <param name="freeType">The type of free operation</param>
    /// <returns>If the function succeeds, the return value is nonzero</returns>
    [DllImport(DLL)]
    public static extern int VirtualFree(IntPtr address, ulong size, uint freeType);

    /// <summary>
    /// Releases, decommits, or releases and decommits a region of pages within the virtual address space of the calling process.
    /// To free memory allocated in another process by the VirtualAllocEx function, use the VirtualFreeEx function.
    /// </summary>
    /// <param name="address">A pointer to the base address of the region of pages to be freed</param>
    /// <param name="size">The function frees the entire region that is reserved in the initial allocation call to VirtualAlloc</param>
    /// <param name="freeType">The type of free operation</param>
    /// <returns>If the function succeeds, the return value is nonzero</returns>
    [DllImport(DLL)]
    public static extern int VirtualFree(IntPtr address, ulong size, AllocationType freeType);

    /// <summary>
    /// Allocates, commits, or reserves a region of pages within the virtual address space of the calling process.
    /// </summary>
    /// <param name="address">
    /// A pointer to the base address of the region of pages to be allocated.
    /// </param>
    /// <param name="dwSize">
    /// The size of the region, in bytes. The region size must be a multiple of the page size.
    /// </param>
    /// <param name="flAllocationType">
    /// The type of memory allocation.
    /// </param>
    /// <param name="flProtect">
    /// The memory protection for the region of pages to be allocated. If the pages are being committed, you can specify any one of the memory protection constants.
    /// </param>
    /// <returns>
    /// If the function succeeds, the return value is the base address of the allocated region of pages.
    /// </returns>
    /// <remarks>
    /// This function is used to allocate memory either in the address space of the calling process or in the address space of a specified process.
    /// </remarks>
    [DllImport(DLL)]
    public static extern IntPtr VirtualAlloc(IntPtr address,
        ulong dwSize,
        AllocationType flAllocationType,
        MemoryProtection flProtect);

    /// <summary>
    /// Allocates, commits, or reserves a region of pages within the virtual address space of the calling process.
    /// </summary>
    /// <param name="address">
    /// A pointer to the base address of the region of pages to be allocated.
    /// </param>
    /// <param name="dwSize">
    /// The size of the region, in bytes. The region size must be a multiple of the page size.
    /// </param>
    /// <param name="flAllocationType">
    /// The type of memory allocation.
    /// </param>
    /// <param name="flProtect">
    /// The memory protection for the region of pages to be allocated. If the pages are being committed, you can specify any one of the memory protection constants.
    /// </param>
    /// <returns>
    /// If the function succeeds, the return value is the base address of the allocated region of pages.
    /// </returns>
    /// <remarks>
    /// This function is used to allocate memory either in the address space of the calling process or in the address space of a specified process.
    /// </remarks>
    [DllImport(DLL)]
    public static extern IntPtr VirtualAlloc(IntPtr address,
        ulong dwSize,
        uint flAllocationType,
        uint flProtect);

    /// <summary>
    /// Allocates, commits, or reserves a region of pages within the virtual address space of the calling process.
    /// </summary>
    /// <param name="address">
    /// A pointer to the base address of the region of pages to be allocated.
    /// </param>
    /// <param name="dwSize">
    /// The size of the region, in bytes. The region size must be a multiple of the page size.
    /// </param>
    /// <param name="flAllocationType">
    /// The type of memory allocation.
    /// </param>
    /// <param name="flProtect">
    /// The memory protection for the region of pages to be allocated. If the pages are being committed, you can specify any one of the memory protection constants.
    /// </param>
    /// <returns>
    /// If the function succeeds, the return value is the base address of the allocated region of pages.
    /// </returns>
    /// <remarks>
    /// This function is used to allocate memory either in the address space of the calling process or in the address space of a specified process.
    /// </remarks>
    [DllImport(DLL)]
    public static extern unsafe void* VirtualAllocEx(void* process, void* address, ulong dwSize,
        uint flAllocationType, uint flProtect);
}