namespace RxBim.Tools.Revit;

using System;

/// <summary>
/// Extensions for <see cref="Type"/>
/// </summary>
internal static class TypeExtensions
{
    /// <summary>
    /// Gets base class for declare type.
    /// </summary>
    /// <param name="type">Declare type.</param>
    /// <param name="predicate">Predicate for find base class in hierarchy.</param>
    internal static Type? GetBaseClass(
        this Type type, Predicate<Type>? predicate = null)
    {
        if (predicate is null)
            return type.BaseType;
        
        while (type.BaseType != null)
        {
            type = type.BaseType;
            if (predicate(type))
                return type;
        }

        return null;
    }

    /// <summary>
    /// Gets base type of <see cref="Wrapper{T}"/>.
    /// </summary>
    /// <param name="type">Declare type.</param>
    internal static Type? GetWrapperBaseType(
        this Type type)
    {
        return type.GetBaseClass(t => t.IsGenericType
                                          && t.GetGenericTypeDefinition() == typeof(Wrapper<>));
    }
}