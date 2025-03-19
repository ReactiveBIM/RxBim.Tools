namespace RxBim.Tools.Revit.Services;

using System;
using System.Threading.Tasks;
using Abstractions;
using Autodesk.Revit.UI;
using JetBrains.Annotations;

/// <inheritdoc />
[PublicAPI]
public class RevitTaskMock : IRevitTask
{
    private readonly UIApplication _uiApplication;

    /// <summary>
    /// Initializes a new instance of the <see cref="RevitTaskMock"/> class.
    /// </summary>
    /// <param name="uiApplication">The UI app.</param>
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