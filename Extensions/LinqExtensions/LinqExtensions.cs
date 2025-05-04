using System.Text.RegularExpressions;

namespace Yannick.Extensions.LinqExtensions;

using System;
using System.Collections.Generic;
using System.Linq;

public static partial class LinqExtensions
{
    /// <summary>
    /// Orders a sequence of elements in alphanumeric order based on the specified string selector.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="source">The sequence of elements to order.</param>
    /// <param name="selector">A function to extract the string key to order by.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> whose elements are sorted in alphanumeric order.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="source"/> or <paramref name="selector"/> is null.
    /// </exception>
    /// <example>
    /// <code>
    /// var items = new List&lt;string&gt; { "B12", "B1", "A", "B2" };
    /// var ordered = items.OrderByAlphanumeric(x => x);
    /// // Result: ["A", "B1", "B2", "B12"]
    /// </code>
    /// </example>
    public static IEnumerable<T> OrderByAlphanumeric<T>(this IEnumerable<T> source, Func<T, string> selector)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(selector);

        return source.OrderBy(item =>
        {
            var value = selector(item);
            var match = OrderByAlphanumericRegex().Match(value);
            var letterPart = match.Groups[1].Value.Trim();
            var numberPart = int.TryParse(match.Groups[2].Value, out var n) ? n : 0;

            return (letterPart, numberPart);
        });
    }

    /// <summary>
    /// Returns the index of the first element that matches the predicate, or -1 if no such element is found.
    /// </summary>
    /// <typeparam name="T">The type of elements in the source collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="predicate">The predicate to test each element.</param>
    /// <returns>The index of the first matching element, or -1 if no match is found.</returns>
    public static int FirstOrDefaultIndex<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);

        var index = 0;
        foreach (var item in source)
        {
            if (predicate(item))
                return index;
            index++;
        }

        return -1;
    }

    [GeneratedRegex(@"^([A-Za-z ]+)(\d*)$")]
    private static partial Regex OrderByAlphanumericRegex();
}