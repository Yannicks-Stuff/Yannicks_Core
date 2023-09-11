namespace Yannick.Extensions.DictionaryExtensions;

public static class IReadOnlyDictionaryExtension
{
    public static IReadOnlyDictionary<TValue, TKey> Swap<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> source)
        where TValue : notnull where TKey : notnull
    {
        var swapped = new Dictionary<TValue, TKey>();
        foreach (var entry in source)
        {
            swapped[entry.Value] = entry.Key;
        }

        return swapped;
    }
}