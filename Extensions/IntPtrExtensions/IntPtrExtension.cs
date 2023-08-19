namespace Yannick.Extensions.IntPtrExtensions;

/// <summary>
/// Provides extension methods for the <see cref="System.IntPtr"/> class.
/// </summary>
public static class IntPtrExtension
{
    /// <summary>
    /// Converts the value of the specified <see cref="System.IntPtr"/> to a 32-bit signed integer.
    /// </summary>
    /// <param name="ptr">The pointer to convert.</param>
    /// <returns>
    /// A 32-bit signed integer that represents the value of the pointer.
    /// </returns>
    /// <remarks>
    /// This method safely converts the value of the specified <see cref="System.IntPtr"/> 
    /// to a 32-bit signed integer, considering the platform architecture (x86 or x64).
    /// </remarks>
    public static int ToSafeInt32(this IntPtr ptr)
    {
        return (int)((IntPtr.Size <= 4) ? ptr.ToInt32() : ptr.ToInt64());
    }
}