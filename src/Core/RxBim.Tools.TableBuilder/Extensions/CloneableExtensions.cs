﻿namespace RxBim.Tools.TableBuilder;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

/// <summary>
/// Extensions for clone objects.
/// </summary>
public static class CloneableExtensions
{
    private static readonly MethodInfo CloneMethod = typeof(object)
        .GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance)!;

    /// <summary>
    /// Gets true if type is primitive.
    /// </summary>
    /// <param name="type"><see cref="Type"/>.</param>
    public static bool IsPrimitive(this Type type)
    {
        if (type == typeof(string))
            return true;

        return type.IsValueType & type.IsPrimitive;
    }

    /// <summary>
    /// Copies object.
    /// </summary>
    /// <param name="originalObject">Original object.</param>
    public static object Copy(this object originalObject)
    {
        return InternalCopy(originalObject, new Dictionary<object, object>(new ReferenceEqualityComparer()))!;
    }

    /// <summary>
    /// Copies object.
    /// </summary>
    /// <param name="original">Object.</param>
    /// <typeparam name="T">Type of object.</typeparam>
    public static T Copy<T>(this T original)
    {
        return (T)Copy((object)original!);
    }

    private static object? InternalCopy(object? originalObject, IDictionary<object, object> visited)
    {
        if (originalObject is null)
            return null;
        Debug.Print($"{originalObject.GetType()} {originalObject} [{(originalObject as Cell)?.GetRowIndex()} {(originalObject as Cell)?.GetColumnIndex()}]");
        var typeToReflect = originalObject.GetType();
        if (IsPrimitive(typeToReflect))
            return originalObject;
        if (visited.ContainsKey(originalObject))
            return visited[originalObject];
        if (typeof(Delegate).IsAssignableFrom(typeToReflect))
            throw new InvalidCastException();

        var cloneObject = CloneMethod.Invoke(originalObject, null);
        if (typeToReflect.IsArray)
        {
            var arrayType = typeToReflect.GetElementType();
            if (IsPrimitive(arrayType!) == false)
            {
                var clonedArray = (Array)cloneObject;
                clonedArray.ForEach((array, indices) =>
                    array.SetValue(InternalCopy(clonedArray.GetValue(indices), visited), indices));
            }
        }

        visited.Add(originalObject, cloneObject);
        CopyFields(originalObject, visited, cloneObject, typeToReflect);
        RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);

        return cloneObject;
    }

    private static void RecursiveCopyBaseTypePrivateFields(
        object originalObject,
        IDictionary<object, object> visited,
        object cloneObject,
        Type typeToReflect)
    {
        if (typeToReflect.BaseType == null)
            return;

        RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
        CopyFields(
            originalObject,
            visited,
            cloneObject,
            typeToReflect.BaseType,
            BindingFlags.Instance | BindingFlags.NonPublic,
            info => info.IsPrivate);
    }

    private static void CopyFields(
        object originalObject,
        IDictionary<object, object> visited,
        object cloneObject,
        IReflect typeToReflect,
        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public |
                                    BindingFlags.FlattenHierarchy,
        Func<FieldInfo, bool>? filter = null)
    {
        foreach (var fieldInfo in typeToReflect.GetFields(bindingFlags))
        {
            if (filter != null && filter(fieldInfo) == false)
                continue;
            if (IsPrimitive(fieldInfo.FieldType))
                continue;

            var originalFieldValue = fieldInfo.GetValue(originalObject);
            var clonedFieldValue = InternalCopy(originalFieldValue, visited);
            fieldInfo.SetValue(cloneObject, clonedFieldValue);
        }
    }
}