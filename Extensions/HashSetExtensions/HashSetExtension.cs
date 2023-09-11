namespace Yannick.Extensions.HashSetExtensions;

public static class HashSetExtension
{
    public static void AddRange<T>(this HashSet<T> hs, IEnumerable<T> args)
    {
        foreach (var a in args)
            hs.Add(a);
    }
}