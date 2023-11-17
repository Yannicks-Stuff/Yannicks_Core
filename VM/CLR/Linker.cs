using System.Collections.Concurrent;
using System.Reflection;

namespace Yannick.VM.CLR;

/// <summary>
/// Provides functionality to dynamically link methods, properties, and fields at runtime.
/// </summary>
public class Linker
{
    private readonly ConcurrentDictionary<string, FieldInfo> _fieldCache = new();
    private readonly ConcurrentDictionary<string, MethodInfo> _methodCache = new();
    private readonly ConcurrentDictionary<string, PropertyInfo> _propertyCache = new();
    private readonly Type? _targetType;

    /// <summary>
    /// Initializes a new instance of the Linker class with a specified class name and optional namespace.
    /// </summary>
    /// <param name="className">The name of the class to link.</param>
    /// <param name="namespaceName">The namespace of the class, if any.</param>
    /// <exception cref="Exception">Thrown when the specified class is not found in the given namespace.</exception>
    public Linker(string className, string? namespaceName = null)
    {
        _targetType = Assembly.GetExecutingAssembly().GetTypes()
            .FirstOrDefault(t => t.Name == className && (namespaceName == null || t.Namespace == namespaceName));

        if (_targetType == null)
        {
            throw new Exception($"Class {className} not found in namespace {namespaceName}");
        }
    }

    /// <summary>
    /// Initializes a new instance of the Linker class with a specified Type.
    /// </summary>
    /// <param name="type">The Type to link.</param>
    public Linker(Type type)
    {
        _targetType = type;
    }

    public object? Instance { get; set; }

    /// <summary>
    /// Links a static method with the specified delegate type. the names is from T
    /// </summary>
    /// <typeparam name="T">The delegate type to link to.</typeparam>
    /// <param name="onlyPublic">Whether to only consider public methods.</param>
    /// <returns>A delegate linked to the specified method, or null if not found.</returns>
    public T? LinkStatic<T>(bool onlyPublic = true) where T : Delegate => LinkStatic<T>(typeof(T).Name, onlyPublic);

    /// <summary>
    /// Links a static method with the specified delegate type.
    /// </summary>
    /// <typeparam name="T">The delegate type to link to.</typeparam>
    /// <param name="methodName">The name of the method to link.</param>
    /// <param name="onlyPublic">Whether to only consider public methods.</param>
    /// <returns>A delegate linked to the specified method, or null if not found.</returns>
    public T? LinkStatic<T>(string methodName, bool onlyPublic = true) where T : Delegate
    {
        if (_targetType == null)
            return null;

        if (_methodCache.TryGetValue(methodName, out var cachedMethod))
        {
            return (T)Delegate.CreateDelegate(typeof(T), cachedMethod);
        }

        var bindingFlags = BindingFlags.Static | (onlyPublic ? BindingFlags.Public : BindingFlags.NonPublic);

        var methodInfo = _targetType.GetMethod(methodName, bindingFlags);

        if (methodInfo != null)
        {
            _methodCache[methodName] = methodInfo;
            return (T)Delegate.CreateDelegate(typeof(T), methodInfo);
        }

        return null;
    }

    /// <summary>
    /// Links an instance method with the specified delegate type. the names is from T
    /// </summary>
    /// <typeparam name="T">The delegate type to link to.</typeparam>
    /// <param name="onlyPublic">Whether to only consider public methods.</param>
    /// <returns>A delegate linked to the specified method, or null if not found.</returns>
    public T? LinkInstance<T>(bool onlyPublic = true) where T : Delegate => LinkInstance<T>(typeof(T).Name, onlyPublic);

    /// <summary>
    /// Links an instance method with the specified delegate type.
    /// </summary>
    /// <typeparam name="T">The delegate type to link to.</typeparam>
    /// <param name="methodName">The name of the method to link.</param>
    /// <param name="onlyPublic">Whether to only consider public methods.</param>
    /// <returns>A delegate linked to the specified method, or null if not found.</returns>
    public T? LinkInstance<T>(string methodName, bool onlyPublic = true) where T : Delegate
    {
        if (_targetType == null || Instance == null)
            return null;

        if (_methodCache.TryGetValue(methodName, out var cachedMethod))
        {
            return (T)Delegate.CreateDelegate(typeof(T), Instance, cachedMethod);
        }

        var bindingFlags = BindingFlags.Instance | (onlyPublic ? BindingFlags.Public : BindingFlags.NonPublic);

        var methodInfo = _targetType.GetMethod(methodName, bindingFlags);

        if (methodInfo != null)
        {
            _methodCache[methodName] = methodInfo;
            return (T)Delegate.CreateDelegate(typeof(T), Instance, methodInfo);
        }

        return null;
    }


    /// <summary>
    /// Links a property with the specified name and type. the names is from T
    /// </summary>
    /// <typeparam name="T">The type of the property's value.</typeparam>
    /// <param name="onlyPublic">Whether to only consider public properties.</param>
    /// <returns>A LinkedProperty representing the linked property.</returns>
    public LinkedProperty<T> LinkProperty<T>(bool onlyPublic = true) where T : Delegate =>
        LinkProperty<T>(typeof(T).Name, onlyPublic);

    /// <summary>
    /// Links a property with the specified name and type.
    /// </summary>
    /// <typeparam name="T">The type of the property's value.</typeparam>
    /// <param name="propertyName">The name of the property to link.</param>
    /// <param name="onlyPublic">Whether to only consider public properties.</param>
    /// <returns>A LinkedProperty representing the linked property.</returns>
    public LinkedProperty<T> LinkProperty<T>(string propertyName, bool onlyPublic = true) where T : Delegate
    {
        if (_targetType == null)
            return default;

        if (!_propertyCache.TryGetValue(propertyName, out var propertyInfo))
        {
            var bindingFlags = (onlyPublic ? BindingFlags.Public : BindingFlags.NonPublic) | BindingFlags.Instance |
                               BindingFlags.Static;
            propertyInfo = _targetType.GetProperty(propertyName, bindingFlags);

            if (propertyInfo != null)
            {
                _propertyCache[propertyName] = propertyInfo;
            }
        }

        return new LinkedProperty<T>(propertyInfo, Instance);
    }

    /// <summary>
    /// Links a field with the specified name and type. the names is from T
    /// </summary>
    /// <typeparam name="T">The type of the field's value.</typeparam>
    /// <param name="onlyPublic">Whether to only consider public fields.</param>
    /// <returns>A LinkedField representing the linked field.</returns>
    public LinkedField<T> LinkField<T>(bool onlyPublic = true) where T : Delegate =>
        LinkField<T>(typeof(T).Name, onlyPublic);

    /// <summary>
    /// Links a field with the specified name and type.
    /// </summary>
    /// <typeparam name="T">The type of the field's value.</typeparam>
    /// <param name="fieldName">The name of the field to link.</param>
    /// <param name="onlyPublic">Whether to only consider public fields.</param>
    /// <returns>A LinkedField representing the linked field.</returns>
    public LinkedField<T> LinkField<T>(string fieldName, bool onlyPublic = true) where T : Delegate
    {
        if (_targetType == null)
            return default;

        if (!_fieldCache.TryGetValue(fieldName, out var fieldInfo))
        {
            var bindingFlags = (onlyPublic ? BindingFlags.Public : BindingFlags.NonPublic) | BindingFlags.Instance |
                               BindingFlags.Static;
            fieldInfo = _targetType.GetField(fieldName, bindingFlags);

            if (fieldInfo != null)
            {
                _fieldCache[fieldName] = fieldInfo;
            }
        }

        return new LinkedField<T>(fieldInfo, Instance);
    }

    /// <summary>
    /// Creates an instance of the specified type.
    /// </summary>
    /// <typeparam name="T1">The type of object to return. This type must be compatible with the specified type.</typeparam>
    /// <param name="type">The type of object to create. This should be assignable to type T1.</param>
    /// <param name="args">An array of arguments that match in number, order, and type the parameters of the constructor to invoke. If args is an empty array or null, the constructor that takes no parameters (the default constructor) is invoked.</param>
    /// <returns>An instance of the specified type, cast to type T1.</returns>
    public static T1? CreateInstance<T1>(Type type, params object?[]? args)
    {
        try
        {
            var instance = Activator.CreateInstance(type, args);

            return (T1?)instance;
        }
        catch (Exception)
        {
            return default;
        }
    }

    /// <summary>
    /// Creates an instance of the type specified by the type name, optionally using the assembly name.
    /// </summary>
    /// <typeparam name="T1">The type of object to return. The created object must be compatible with this type.</typeparam>
    /// <param name="typeName">The fully qualified name of the type to create.</param>
    /// <param name="assemblyName">Optional. The name of the assembly where the type is defined.</param>
    /// <param name="args">An array of arguments that match the parameters of the constructor to invoke, if any.</param>
    /// <returns>An instance of the specified type, cast to type T1.</returns>
    public static T1? CreateInstance<T1>(string typeName, string? assemblyName = null, params object?[]? args)
    {
        var fullTypeName = string.IsNullOrEmpty(assemblyName) ? typeName : $"{typeName}, {assemblyName}";

        var type = Type.GetType(fullTypeName);
        if (type == null)
            throw new InvalidOperationException($"Type '{fullTypeName}' could not be found.");

        try
        {
            var instance = Activator.CreateInstance(type, args);
            return (T1?)instance;
        }
        catch (Exception)
        {
            return default;
        }
    }


    /// <summary>
    /// Represents a linked property and provides methods to get and set its value.
    /// </summary>
    /// <typeparam name="T">The type of the property's value.</typeparam>
    public readonly struct LinkedProperty<T>
    {
        private readonly PropertyInfo? _propertyInfo;
        private readonly object? _instance;

        /// <summary>
        /// Initializes a new instance of the LinkedProperty struct.
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        /// <param name="instance">The instance of the object containing the property.</param>
        public LinkedProperty(PropertyInfo? propertyInfo, object? instance)
        {
            _propertyInfo = propertyInfo;
            _instance = instance;
        }

        /// <summary>
        /// Gets the value of the linked property.
        /// </summary>
        /// <returns>The value of the property.</returns>
        public T? GetValue()
        {
            return (T?)_propertyInfo?.GetValue(_instance);
        }

        /// <summary>
        /// Sets the value of the linked property.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetValue(T? value)
        {
            _propertyInfo?.SetValue(_instance, value);
        }
    }

    /// <summary>
    /// Represents a linked field and provides methods to get and set its value.
    /// </summary>
    /// <typeparam name="T">The type of the field's value.</typeparam>
    public readonly struct LinkedField<T>
    {
        private readonly FieldInfo? _fieldInfo;
        private readonly object? _instance;

        /// <summary>
        /// Initializes a new instance of the LinkedField struct.
        /// </summary>
        /// <param name="fieldInfo">The field information.</param>
        /// <param name="instance">The instance of the object containing the field.</param>
        public LinkedField(FieldInfo? fieldInfo, object? instance)
        {
            _fieldInfo = fieldInfo;
            _instance = instance;
        }

        /// <summary>
        /// Gets the value of the linked field.
        /// </summary>
        /// <returns>The value of the field.</returns>
        public T? GetValue()
        {
            return (T?)_fieldInfo?.GetValue(_instance);
        }

        /// <summary>
        /// Sets the value of the linked field.
        /// </summary>
        /// <param name="value">The value to set.</param>
        public void SetValue(T? value)
        {
            _fieldInfo?.SetValue(_instance, value);
        }
    }
}