namespace Yannick.Extensions.DictionaryExtensions;

public static class DictionaryExtension
{
    public static Dictionary<TValue, TKey> Swap<TKey, TValue>(this Dictionary<TKey, TValue> source)
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