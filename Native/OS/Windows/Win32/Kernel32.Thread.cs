using System.Runtime.InteropServices;

namespace Yannick.Native.OS.Windows.Win32;

public static partial class Kernel32
{
    /// <summary>
    /// Sets a processor affinity mask for the specified thread.
    /// </summary>
    /// <param name="hThread">
    /// A handle to the thread for which the affinity mask is to be set.
    /// This handle must have been created with the THREAD_SET_INFORMATION access right.
    /// </param>
    /// <param name="dwThreadAffinityMask">
    /// The affinity mask for the thread. On a system with more than 64 processors, this function sets the thread's ideal processor to a logical processor in the processor group to which the thread is assigned.
    /// </param>
    /// <returns>
    /// If the function succeeds, the return value is the thread's previous affinity mask.
    /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
    /// </returns>
    [DllImport(DLL)]
    public static extern IntPtr SetThreadAffinityMask(IntPtr hThread, IntPtr dwThreadAffinityMask);

    /// <summary>
    /// Sets a processor affinity mask for the specified thread.
    /// </summary>
    /// <param name="hThread">
    /// A handle to the thread for which the affinity mask is to be set.
    /// This handle must have been created with the THREAD_SET_INFORMATION access right.
    /// </param>
    /// <param name="dwThreadAffinityMask">
    /// The affinity mask for the thread. On a system with more than 64 processors, this function sets the thread's ideal processor to a logical processor in the processor group to which the thread is assigned.
    /// </param>
    /// <returns>
    /// If the function succeeds, the return value is the thread's previous affinity mask.
    /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
    /// </returns>
    public static IntPtr SetThreadAffinityMask(IntPtr hThread, ulong dwThreadAffinityMask)
    {
        unsafe
        {
            return SetThreadAffinityMask(hThread, new IntPtr(new UIntPtr(dwThreadAffinityMask).ToPointer()));
        }
    }

    /// <summary>
    /// Retrieves a pseudo handle for the calling thread.
    /// </summary>
    /// <returns>
    /// The return value is a pseudo handle to the current thread.
    /// </returns>
    /// <remarks>
    /// A pseudo handle is a special constant that is interpreted as the current thread handle.
    /// The calling thread can use this handle to specify itself whenever a thread handle is required.
    /// Pseudo handles are not inherited by child processes.
    /// 
    /// This handle has the THREAD_QUERY_INFORMATION and THREAD_QUERY_LIMITED_INFORMATION access rights to the thread object.
    /// 
    /// Windows Server 2003 and Windows XP:  This handle has the maximum access allowed by the security descriptor of the thread to the primary token of the process.
    /// 
    /// If the function succeeds, the return value is a handle to the current thread.
    /// 
    /// Note that you should not close this handle with the CloseHandle function. It is not necessary and doing so can cause the handle to become invalid.
    /// </remarks>
    [DllImport(DLL)]
    public static extern IntPtr GetCurrentThread();
}