namespace RxBim.Tools.Revit.Abstractions
{
    using System;
    using System.Threading.Tasks;
    using Autodesk.Revit.UI;
    using JetBrains.Annotations;

    /// <summary>
    /// Represents an object that is used to manage a thread on which an action is performed.
    /// </summary>
    [PublicAPI]
    public interface IRevitTask
    {
        /// <summary>
        /// Performs an action on the appropriate thread.
        /// </summary>
        /// <param name="action">An action.</param>
        public Task Run(Action<UIApplication> action);

        /// <summary>
        /// Performs an func on the appropriate thread.
        /// </summary>
        /// <param name="func">A function.</param>
        /// <typeparam name="TResult">Returned result.</typeparam>
        public Task<TResult> Run<TResult>(Func<UIApplication, TResult> func);
    }
}