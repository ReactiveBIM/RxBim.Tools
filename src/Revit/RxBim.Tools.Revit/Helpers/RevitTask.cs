namespace RxBim.Tools.Revit.Helpers;

using System;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using JetBrains.Annotations;

/// <summary>
/// Класс для запуска задач в контексте Revit.
/// </summary>
[PublicAPI]
public class RevitTask
{
    private readonly DefaultEventHandler _handler;
    private readonly ExternalEvent _externalEvent;
    private TaskCompletionSource<object>? _tcs;

    /// <inheritdoc cref="RevitTask" />
    public RevitTask()
    {
        _handler = new DefaultEventHandler();
        _handler.ExternalEventCompleted += OnExternalEventCompleted;
        _externalEvent = ExternalEvent.Create(_handler);
    }

    /// <summary>
    /// Устанавливает указанный делегат как тело метода <see cref="IExternalEventHandler.Execute(UIApplication)"/>
    /// и вызывает связанный с ним <see cref="Autodesk.Revit.UI.ExternalEvent"/>.
    /// </summary>
    /// <param name="func">Делегат, зависящий от <see cref="Autodesk.Revit.UI.UIApplication"/>
    /// и возвращающий объект типа <typeparamref name="TResult"/>.</param>
    public Task<TResult> Run<TResult>(Func<UIApplication, TResult> func)
    {
        _tcs = new TaskCompletionSource<object>();

        var task = Task.Run(async () => (TResult)await _tcs.Task);

        _handler.ExternalEventBody = app => func(app)!;

        _externalEvent.Raise();

        return task;
    }

    /// <summary>
    /// Устанавливает указанный делегат как тело метода <see cref="IExternalEventHandler.Execute(UIApplication)"/>
    /// и вызывает связанный с ним <see cref="Autodesk.Revit.UI.ExternalEvent"/>.
    /// </summary>
    /// <param name="act">Делегат, зависящий от <see cref="Autodesk.Revit.UI.UIApplication"/>.</param>
    public Task Run(Action<UIApplication> act)
    {
        _tcs = new TaskCompletionSource<object>();

        _handler.ExternalEventBody = (app) =>
        {
            act(app);
            return new object();
        };

        _externalEvent.Raise();

        return _tcs.Task;
    }

    /// <summary>
    /// Возникает при завершении <see cref="IExternalEventHandler.Execute(UIApplication)"/> и передаёт результат в
    /// <see cref="TaskCompletionSource{TResult}"/>.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="result">Результат.</param>
    private void OnExternalEventCompleted(object sender, object result)
    {
        if (_handler.Exception == null)
        {
            _tcs?.TrySetResult(result);
        }
        else
        {
            _tcs?.TrySetException(_handler.Exception);
        }
    }
}