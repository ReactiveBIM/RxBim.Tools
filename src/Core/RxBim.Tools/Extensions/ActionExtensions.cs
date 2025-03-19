namespace RxBim.Tools;

using System;

/// <summary>
/// Extensions for <see cref="Action"/>.
/// </summary>
internal static class ActionExtensions
{
    /// <summary>
    /// Returns <see cref="Func{Action}"/> for action.
    /// </summary>
    /// <param name="action">Action.</param>
    /// <param name="returnValue">The value to return the function.</param>
    public static Func<object?> ToFunc(this Action action, object? returnValue = null)
    {
        return () =>
        {
            action.Invoke();
            return returnValue;
        };
    }

    /// <summary>
    /// Returns <see cref="Func{Action}"/> for action.
    /// </summary>
    /// <param name="action">Action.</param>
    /// <param name="returnValue">The value to return the function.</param>
    public static Func<T, object?> ToFunc<T>(this Action<T> action, object? returnValue = null)
    {
        return x =>
        {
            action.Invoke(x);
            return returnValue;
        };
    }

    /// <summary>
    /// Returns <see cref="Func{Action}"/> for action.
    /// </summary>
    /// <param name="action">Action.</param>
    /// <param name="returnValue">The value to return the function.</param>
    public static Func<T1, T2, object?> ToFunc<T1, T2>(this Action<T1, T2> action, object? returnValue = null)
    {
        return (x1, x2) =>
        {
            action.Invoke(x1, x2);
            return returnValue;
        };
    }
}