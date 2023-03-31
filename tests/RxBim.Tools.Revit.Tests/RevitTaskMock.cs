namespace RxBim.Tools.Revit.Tests
{
    using System;
    using System.Threading.Tasks;
    using Autodesk.Revit.UI;
    using JetBrains.Annotations;
    using RxBim.Tools.Revit.Abstractions;

    /// <inheritdoc />
    [PublicAPI]
    public class RevitTaskMock : IRevitTask
    {
        private readonly UIApplication _uiApplication;

        public RevitTaskMock(UIApplication uiApplication)
        {
            _uiApplication = uiApplication;
        }

        /// <inheritdoc />
        public Task Run(Action<UIApplication> action)
        {
            action.Invoke(_uiApplication);
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<TResult> Run<TResult>(Func<UIApplication, TResult> func)
        {
            var result = func.Invoke(_uiApplication);
            return Task.FromResult(result);
        }
    }
}