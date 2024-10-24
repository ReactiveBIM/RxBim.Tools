#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace RxBim.Tools.Revit.Helpers;

using System;
using Autodesk.Revit.UI;

/// <inheritdoc />
internal class DefaultEventHandler : IExternalEventHandler
{
    /// <summary>
    /// Событие завершения выполнения задачи.
    /// </summary>
    public event EventHandler<object> ExternalEventCompleted;

    /// <summary>
    /// Исключенире, возникшее в результате выполнения задачи.
    /// </summary>
    public Exception? Exception { get; private set; }

    /// <summary>
    /// Делегат, который выполняется в методе <see cref="IExternalEventHandler.Execute"/>.
    /// </summary>
    public Func<UIApplication, object> ExternalEventBody { get; set; }

    /// <inheritdoc />
    public void Execute(UIApplication app)
    {
        object? result = null;

        try
        {
            result = ExternalEventBody.Invoke(app);
        }
        catch (Exception ex)
        {
            Exception = ex;
        }

        ExternalEventCompleted.Invoke(this, result);
    }

    /// <inheritdoc />
    public string GetName()
    {
        return nameof(DefaultEventHandler);
    }
}