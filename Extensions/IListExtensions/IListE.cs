using System.Collections.Immutable;

namespace Yannick.Extensions.IListExtensions;

public static class IListE
{
    public static T[] ToArray<T>(this IList<T> list, object lockObj)
    {
        T[] arr;

        lock (lockObj)
            arr = list.ToArray();

        return arr;
    }

    public static ImmutableArray<T> ToImmutableArray<T>(this IList<T> list, object lockObj)
    {
        ImmutableArray<T> arr;

        lock (lockObj)
            arr = [..list];

        return arr;
    }
}