namespace RxBim.Tools.Revit.Services;

using System;
using System.Threading.Tasks;
using Abstractions;
using Autodesk.Revit.UI;

/// <summary>
/// Адаптер для использования <see cref="RevitTask"/> с интерфейсом <see cref="IRevitTask"/>.
/// </summary>
public class RevitTaskAdapter : IRevitTask
{
    private readonly RevitTask _revitTask;

    /// <summary>
    /// Initializes a new instance of the <see cref="RevitTaskAdapter"/> class.
    /// </summary>
    /// <param name="revitTask"><see cref="RevitTask"/></param>
    public RevitTaskAdapter(RevitTask revitTask)
    {
        _revitTask = revitTask;
    }

    /// <inheritdoc />
    public Task Run(Action<UIApplication> action)
    {
        return _revitTask.Run(action);
    }

    /// <inheritdoc />
    public Task<TResult> Run<TResult>(Func<UIApplication, TResult> func)
    {
        return _revitTask.Run(func);
    }
}