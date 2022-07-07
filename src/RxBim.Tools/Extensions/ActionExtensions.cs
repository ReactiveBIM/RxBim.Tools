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
        public static Func<Action> ConvertToFunc(this Action action)
        {
            return () =>
            {
                action.Invoke();
                return action;
            };
        }

        /// <summary>
        /// Returns <see cref="Func{Action}"/> for action.
        /// </summary>
        /// <param name="action">Action.</param>
        public static Func<T, Action<T>> ConvertToFunc<T>(this Action<T> action)
        {
            return x =>
            {
                action.Invoke(x);
                return action;
            };
        }
    }
}