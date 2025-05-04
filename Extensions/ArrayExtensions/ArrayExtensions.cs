namespace Yannick.Extensions.ArrayExtensions;

public static class ArrayExtensions
{
    public static bool IsEmpty<T>(this T[] array) => array.Length == 0;
    public static bool IsEmptyOrNull<T>(this T[]? array) => array == null || array.IsEmpty();
}