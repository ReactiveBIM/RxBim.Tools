namespace RxBim.Tools
{
    using System;

    /// <summary>
    /// Extensions for <see cref="Action"/>.
    /// </summary>
    public static class ActionExtensions
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
    }
}