namespace Yannick.Extensions.GenericExtensions.ArrayExtensions;

/// <summary>
/// Provides extension methods for generic arrays.
/// </summary>
public static class GenericArrayExtension
{
    /// <summary>
    /// Combines two arrays of the same type into a single array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the arrays.</typeparam>
    /// <param name="firstArray">The first array to combine.</param>
    /// <param name="secondArray">The second array to combine.</param>
    /// <returns>A new array that contains the elements of both input arrays.</returns>
    public static T[] Combine<T>(this T[] firstArray, T[] secondArray)
    {
        if (firstArray == null)
            throw new ArgumentNullException(nameof(firstArray), "The first array cannot be null.");

        if (secondArray == null)
            throw new ArgumentNullException(nameof(secondArray), "The second array cannot be null.");

        var result = new T[firstArray.Length + secondArray.Length];
        Array.Copy(firstArray, result, firstArray.Length);
        Array.Copy(secondArray, 0, result, firstArray.Length, secondArray.Length);

        return result;
    }

    /// <summary>
    /// Adds specified items to the end of the source array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="source">The source array to which items should be added.</param>
    /// <param name="items">The items to add to the source array.</param>
    /// <returns>A new array that contains the elements of the source array followed by the specified items.</returns>
    /// <exception cref="ArgumentNullException">Thrown when source or items is null.</exception>
    public static T[] Add<T>(this T[] source, params T[] items)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source), "The source array cannot be null.");

        if (items == null)
            throw new ArgumentNullException(nameof(items), "The items array cannot be null.");

        var sourceLength = source.Length;
        var itemsLength = items.Length;
        var result = new T[sourceLength + itemsLength];
        Array.Copy(source, result, sourceLength);
        Array.Copy(items, 0, result, sourceLength, itemsLength);

        return result;
    }

    /// <summary>
    /// Adds multiple items to an existing array.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the array.</typeparam>
    /// <param name="source">The original array to which the items should be added.</param>
    /// <param name="item">The item to be added to the array.</param>
    /// <param name="count">The number of items to be added.</param>
    /// <returns>A new array that includes the elements of the original array plus the added items.</returns>
    /// <exception cref="ArgumentNullException">Thrown when source or items is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the value of 'count' is greater than Int32.MaxValue
    /// or the length of the resulting array would be greater than Int32.MaxValue.</exception>
    public static T[] Add<T>(this T[] source, T item, uint count)
    {
        if (count > int.MaxValue || source.Length + count > int.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(count), "your over int max");

        return Add(source, Enumerable.Repeat(item, (int)count).ToArray());
    }
}