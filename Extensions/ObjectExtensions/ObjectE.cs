namespace Yannick.Extensions.ObjectExtensions;

public static class ObjectE
{
    /// <summary>
    /// Applies a transformation function to an object.
    /// </summary>
    /// <typeparam name="T">The return type of the transformation function.</typeparam>
    /// <param name="o">The object to transform.</param>
    /// <param name="cb">The transformation function.</param>
    /// <returns>The result of the transformation function.</returns>
    public static T Apply<T>(this object o, Func<T, T> cb) => cb((T)o);

    /// <summary>
    /// Applies a transformation function to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T">The return type of the transformation function.</typeparam>
    /// <typeparam name="T0">The type of the additional argument.</typeparam>
    /// <param name="o">The object to transform.</param>
    /// <param name="cb">The transformation function.</param>
    /// <param name="arg1">The additional argument.</param>
    /// <returns>The result of the transformation function.</returns>
    public static T Apply<T, T0>(this object o, Func<T, T0, T> cb, T0 arg1) => cb((T)o, arg1);

    /// <summary>
    /// Applies a transformation function to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T">The return type of the transformation function.</typeparam>
    /// <typeparam name="T0">The type of the additional argument for arg 1</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 2</typeparam>
    /// <param name="o">The object to transform.</param>
    /// <param name="cb">The transformation function.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <param name="arg2">The additional argument for arg 2</param>
    /// <returns>The result of the transformation function.</returns>
    public static T Apply<T, T0, T1>(this object o, Func<T, T0, T1, T> cb, T0 arg1, T1 arg2) => cb((T)o, arg1, arg2);

    /// <summary>
    /// Applies a transformation function to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T">The return type of the transformation function.</typeparam>
    /// <typeparam name="T0">The type of the additional argument for arg 1</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 2</typeparam>
    /// <typeparam name="T2">The type of the additional argument for arg 3</typeparam>
    /// <param name="o">The object to transform.</param>
    /// <param name="cb">The transformation function.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <param name="arg2">The additional argument for arg 2</param>
    /// <param name="arg3">The additional argument for arg 3</param>
    /// <returns>The result of the transformation function.</returns>
    public static T Apply<T, T0, T1, T2>(this object o, Func<T, T0, T1, T2, T> cb, T0 arg1, T1 arg2, T2 arg3) =>
        cb((T)o, arg1, arg2, arg3);

    /// <summary>
    /// Applies a transformation function to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T">The return type of the transformation function.</typeparam>
    /// <typeparam name="T0">The type of the additional argument for arg 1</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 2</typeparam>
    /// <typeparam name="T2">The type of the additional argument for arg 3</typeparam>
    /// <typeparam name="T3">The type of the additional argument for arg 4</typeparam>
    /// <param name="o">The object to transform.</param>
    /// <param name="cb">The transformation function.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <param name="arg2">The additional argument for arg 2</param>
    /// <param name="arg3">The additional argument for arg 3</param>
    /// <param name="arg4">The additional argument for arg 4</param>
    /// <returns>The result of the transformation function.</returns>
    public static T Apply<T, T0, T1, T2, T3>(this object o, Func<T, T0, T1, T2, T3, T> cb, T0 arg1, T1 arg2, T2 arg3,
        T3 arg4) => cb((T)o, arg1, arg2, arg3, arg4);

    /// <summary>
    /// Applies a transformation function to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T">The return type of the transformation function.</typeparam>
    /// <typeparam name="T0">The type of the additional argument for arg 1</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 2</typeparam>
    /// <typeparam name="T2">The type of the additional argument for arg 3</typeparam>
    /// <typeparam name="T3">The type of the additional argument for arg 4</typeparam>
    /// <typeparam name="T4">The type of the additional argument for arg 5</typeparam>
    /// <param name="o">The object to transform.</param>
    /// <param name="cb">The transformation function.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <param name="arg2">The additional argument for arg 2</param>
    /// <param name="arg3">The additional argument for arg 3</param>
    /// <param name="arg4">The additional argument for arg 4</param>
    /// <param name="arg5">The additional argument for arg 5</param>
    /// <returns>The result of the transformation function.</returns>
    public static T Apply<T, T0, T1, T2, T3, T4>(this object o, Func<T, T0, T1, T2, T3, T4, T> cb, T0 arg1, T1 arg2,
        T2 arg3, T3 arg4, T4 arg5) => cb((T)o, arg1, arg2, arg3, arg4, arg5);

    /// <summary>
    /// Applies a transformation function to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T">The return type of the transformation function.</typeparam>
    /// <typeparam name="T0">The type of the additional argument for arg 1</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 2</typeparam>
    /// <typeparam name="T2">The type of the additional argument for arg 3</typeparam>
    /// <typeparam name="T3">The type of the additional argument for arg 4</typeparam>
    /// <typeparam name="T4">The type of the additional argument for arg 5</typeparam>
    /// <typeparam name="T5">The type of the additional argument for arg 6</typeparam>
    /// <param name="o">The object to transform.</param>
    /// <param name="cb">The transformation function.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <param name="arg2">The additional argument for arg 2</param>
    /// <param name="arg3">The additional argument for arg 3</param>
    /// <param name="arg4">The additional argument for arg 4</param>
    /// <param name="arg5">The additional argument for arg 5</param>
    /// <param name="arg6">The additional argument for arg 6</param>
    /// <returns>The result of the transformation function.</returns>
    public static T Apply<T, T0, T1, T2, T3, T4, T5>(this object o, Func<T, T0, T1, T2, T3, T4, T5, T> cb, T0 arg1,
        T1 arg2, T2 arg3, T3 arg4, T4 arg5, T5 arg6) => cb((T)o, arg1, arg2, arg3, arg4, arg5, arg6);

    /// <summary>
    /// Applies a transformation function to an object and returns a result of a specified type.
    /// </summary>
    /// <typeparam name="T">The return type of the transformation function.</typeparam>
    /// <typeparam name="T0">The type of the object being transformed.</typeparam>
    /// <param name="o">The object to transform.</param>
    /// <param name="cb">The transformation function.</param>
    /// <returns>The result of the transformation function.</returns>
    public static T ApplyTo<T, T0>(this object o, Func<T0, T> cb) => cb((T0)o);

    /// <summary>
    /// Applies a transformation function to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T">The return type of the transformation function.</typeparam>
    /// <typeparam name="T0">The type of the object being transformed.</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 1</typeparam>
    /// <param name="o">The object to transform.</param>
    /// <param name="cb">The transformation function.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <returns>The result of the transformation function.</returns>
    public static T ApplyTo<T, T0, T1>(this object o, Func<T0, T1, T> cb, T1 arg1) => cb((T0)o, arg1);

    /// <summary>
    /// Applies a transformation function to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T">The return type of the transformation function.</typeparam>
    /// <typeparam name="T0">The type of the object being transformed.</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 1</typeparam>
    /// <typeparam name="T2">The type of the additional argument for arg 2</typeparam>
    /// <param name="o">The object to transform.</param>
    /// <param name="cb">The transformation function.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <param name="arg2">The additional argument for arg 2</param>
    /// <returns>The result of the transformation function.</returns>
    public static T ApplyTo<T, T0, T1, T2>(this object o, Func<T0, T1, T2, T> cb, T1 arg1, T2 arg2) =>
        cb((T0)o, arg1, arg2);

    /// <summary>
    /// Applies a transformation function to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T">The return type of the transformation function.</typeparam>
    /// <typeparam name="T0">The type of the object being transformed.</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 1</typeparam>
    /// <typeparam name="T2">The type of the additional argument for arg 2</typeparam>
    /// <typeparam name="T3">The type of the additional argument for arg 3</typeparam>
    /// <param name="o">The object to transform.</param>
    /// <param name="cb">The transformation function.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <param name="arg2">The additional argument for arg 2</param>
    /// <param name="arg3">The additional argument for arg 3</param>
    /// <returns>The result of the transformation function.</returns>
    public static T ApplyTo<T, T0, T1, T2, T3>(this object o, Func<T0, T1, T2, T3, T> cb, T1 arg1, T2 arg2, T3 arg3) =>
        cb((T0)o, arg1, arg2, arg3);

    /// <summary>
    /// Applies a transformation function to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T">The return type of the transformation function.</typeparam>
    /// <typeparam name="T0">The type of the object being transformed.</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 1</typeparam>
    /// <typeparam name="T2">The type of the additional argument for arg 2</typeparam>
    /// <typeparam name="T3">The type of the additional argument for arg 3</typeparam>
    /// <typeparam name="T4">The type of the additional argument for arg 4</typeparam>
    /// <param name="o">The object to transform.</param>
    /// <param name="cb">The transformation function.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <param name="arg2">The additional argument for arg 2</param>
    /// <param name="arg3">The additional argument for arg 3</param>
    /// <param name="arg4">The additional argument for arg 4</param>
    /// <returns>The result of the transformation function.</returns>
    public static T ApplyTo<T, T0, T1, T2, T3, T4>(this object o, Func<T0, T1, T2, T3, T4, T> cb, T1 arg1, T2 arg2,
        T3 arg3, T4 arg4) => cb((T0)o, arg1, arg2, arg3, arg4);

    /// <summary>
    /// Applies a transformation function to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T">The return type of the transformation function.</typeparam>
    /// <typeparam name="T0">The type of the object being transformed.</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 1</typeparam>
    /// <typeparam name="T2">The type of the additional argument for arg 2</typeparam>
    /// <typeparam name="T3">The type of the additional argument for arg 3</typeparam>
    /// <typeparam name="T4">The type of the additional argument for arg 4</typeparam>
    /// <typeparam name="T5">The type of the additional argument for arg 5</typeparam>
    /// <param name="o">The object to transform.</param>
    /// <param name="cb">The transformation function.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <param name="arg2">The additional argument for arg 2</param>
    /// <param name="arg3">The additional argument for arg 3</param>
    /// <param name="arg4">The additional argument for arg 4</param>
    /// <param name="arg5">The additional argument for arg 5</param>
    /// <returns>The result of the transformation function.</returns>
    public static T ApplyTo<T, T0, T1, T2, T3, T4, T5>(this object o, Func<T0, T1, T2, T3, T4, T5, T> cb, T1 arg1,
        T2 arg2, T3 arg3, T4 arg4, T5 arg5) => cb((T0)o, arg1, arg2, arg3, arg4, arg5);

    /// <summary>
    /// Applies a transformation function to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T">The return type of the transformation function.</typeparam>
    /// <typeparam name="T0">The type of the object being transformed.</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 1</typeparam>
    /// <typeparam name="T2">The type of the additional argument for arg 2</typeparam>
    /// <typeparam name="T3">The type of the additional argument for arg 3</typeparam>
    /// <typeparam name="T4">The type of the additional argument for arg 4</typeparam>
    /// <typeparam name="T5">The type of the additional argument for arg 5</typeparam>
    /// <typeparam name="T6">The type of the additional argument for arg 6</typeparam>
    /// <param name="o">The object to transform.</param>
    /// <param name="cb">The transformation function.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <param name="arg2">The additional argument for arg 2</param>
    /// <param name="arg3">The additional argument for arg 3</param>
    /// <param name="arg4">The additional argument for arg 4</param>
    /// <param name="arg5">The additional argument for arg 5</param>
    /// <param name="arg6">The additional argument for arg 6</param>
    /// <returns>The result of the transformation function.</returns>
    public static T ApplyTo<T, T0, T1, T2, T3, T4, T5, T6>(this object o, Func<T0, T1, T2, T3, T4, T5, T6, T> cb,
        T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) => cb((T0)o, arg1, arg2, arg3, arg4, arg5, arg6);

    /// <summary>
    /// Applies an action to an object.
    /// </summary>
    /// <typeparam name="T0">The type of the object.</typeparam>
    /// <param name="o">The object to apply the action to.</param>
    /// <param name="cb">The action to apply.</param>
    public static void ApplyOnly<T0>(this object o, Action<T0> cb) => cb((T0)o);

    /// <summary>
    /// Applies an action to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T0">The type of the object.</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 1</typeparam>
    /// <param name="o">The object to apply the action to.</param>
    /// <param name="cb">The action to apply.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    public static void ApplyOnly<T0, T1>(this object o, Action<T0, T1> cb, T1 arg1) => cb((T0)o, arg1);

    /// <summary>
    /// Applies an action to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T0">The type of the object.</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 1</typeparam>
    /// <typeparam name="T2">The type of the additional argument for arg 2</typeparam>
    /// <param name="o">The object to apply the action to.</param>
    /// <param name="cb">The action to apply.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <param name="arg2">The additional argument for arg 2</param>
    public static void ApplyOnly<T0, T1, T2>(this object o, Action<T0, T1, T2> cb, T1 arg1, T2 arg2) =>
        cb((T0)o, arg1, arg2);

    /// <summary>
    /// Applies an action to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T0">The type of the object.</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 1</typeparam>
    /// <typeparam name="T2">The type of the additional argument for arg 2</typeparam>
    /// <typeparam name="T3">The type of the additional argument for arg 3</typeparam>
    /// <param name="o">The object to apply the action to.</param>
    /// <param name="cb">The action to apply.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <param name="arg2">The additional argument for arg 2</param>
    /// <param name="arg3">The additional argument for arg 3</param>
    public static void ApplyOnly<T0, T1, T2, T3>(this object o, Action<T0, T1, T2, T3> cb, T1 arg1, T2 arg2, T3 arg3) =>
        cb((T0)o, arg1, arg2, arg3);

    /// <summary>
    /// Applies an action to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T0">The type of the object.</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 1</typeparam>
    /// <typeparam name="T2">The type of the additional argument for arg 2</typeparam>
    /// <typeparam name="T3">The type of the additional argument for arg 3</typeparam>
    /// <typeparam name="T4">The type of the additional argument for arg 4</typeparam>
    /// <param name="o">The object to apply the action to.</param>
    /// <param name="cb">The action to apply.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <param name="arg2">The additional argument for arg 2</param>
    /// <param name="arg3">The additional argument for arg 3</param>
    /// <param name="arg4">The additional argument for arg 4</param>
    public static void ApplyOnly<T0, T1, T2, T3, T4>(this object o, Action<T0, T1, T2, T3, T4> cb, T1 arg1, T2 arg2,
        T3 arg3, T4 arg4) => cb((T0)o, arg1, arg2, arg3, arg4);

    /// <summary>
    /// Applies an action to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T0">The type of the object.</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 1</typeparam>
    /// <typeparam name="T2">The type of the additional argument for arg 2</typeparam>
    /// <typeparam name="T3">The type of the additional argument for arg 3</typeparam>
    /// <typeparam name="T4">The type of the additional argument for arg 4</typeparam>
    /// <typeparam name="T5">The type of the additional argument for arg 5</typeparam>
    /// <param name="o">The object to apply the action to.</param>
    /// <param name="cb">The action to apply.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <param name="arg2">The additional argument for arg 2</param>
    /// <param name="arg3">The additional argument for arg 3</param>
    /// <param name="arg4">The additional argument for arg 4</param>
    /// <param name="arg5">The additional argument for arg 5</param>
    public static void ApplyOnly<T0, T1, T2, T3, T4, T5>(this object o, Action<T0, T1, T2, T3, T4, T5> cb, T1 arg1,
        T2 arg2, T3 arg3, T4 arg4, T5 arg5) => cb((T0)o, arg1, arg2, arg3, arg4, arg5);

    /// <summary>
    /// Applies an action to an object with one additional argument.
    /// </summary>
    /// <typeparam name="T0">The type of the object.</typeparam>
    /// <typeparam name="T1">The type of the additional argument for arg 1</typeparam>
    /// <typeparam name="T2">The type of the additional argument for arg 2</typeparam>
    /// <typeparam name="T3">The type of the additional argument for arg 3</typeparam>
    /// <typeparam name="T4">The type of the additional argument for arg 4</typeparam>
    /// <typeparam name="T5">The type of the additional argument for arg 5</typeparam>
    /// <typeparam name="T6">The type of the additional argument for arg 6</typeparam>
    /// <param name="o">The object to apply the action to.</param>
    /// <param name="cb">The action to apply.</param>
    /// <param name="arg1">The additional argument for arg 1</param>
    /// <param name="arg2">The additional argument for arg 2</param>
    /// <param name="arg3">The additional argument for arg 3</param>
    /// <param name="arg4">The additional argument for arg 4</param>
    /// <param name="arg5">The additional argument for arg 5</param>
    /// <param name="arg6">The additional argument for arg 6</param>
    public static void ApplyOnly<T0, T1, T2, T3, T4, T5, T6>(this object o, Action<T0, T1, T2, T3, T4, T5, T6> cb,
        T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) => cb((T0)o, arg1, arg2, arg3, arg4, arg5, arg6);
}