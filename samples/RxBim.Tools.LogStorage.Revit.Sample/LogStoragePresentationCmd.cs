namespace RxBim.Tools.LogStorage.Revit.Sample;

using System.Linq;
using System.Text;
using Autodesk.Revit.Attributes;
using Autodesk.Windows;
using Command.Revit;
using JetBrains.Annotations;
using Models;
using Shared;
using Tools.Revit.Abstractions;
using Tools.Revit.Extensions;

/// <inheritdoc />
[Regeneration(RegenerationOption.Manual)]
[Transaction(TransactionMode.Manual)]
public class LogStoragePresentationCmd : RxBimCommand
{
    /// <summary>
    /// cmd
    /// </summary>
    /// <param name="logStorage"><see cref="ILogStorage"/></param>
    /// <param name="collector"><see cref="IScopedElementsCollector"/></param>
    [UsedImplicitly]
    public PluginResult ExecuteCommand(ILogStorage logStorage, IScopedElementsCollector collector)
    {
        var elements = collector.PickElements();
        foreach (var element in elements)
        {
            var message = new ElementDataMessage(
                "Data of element:",
                element.Name,
                element.Category?.Name ?? "No category",
                element.Id.GetIdValue());
            logStorage.AddMessage(message);
        }

        var resultMessage = new StringBuilder();
        resultMessage.AppendLine($"Selected elements count: {elements.Count}");
        var messages = logStorage.GetMessages().ToList();
        resultMessage.AppendLine($"Log storage messages count: {messages.Count}");
        foreach (var message in messages.OfType<ElementDataMessage>())
        {
            resultMessage.AppendLine(
                $"{message.Text} Category - {message.ElementCategory}, Name - {message.ElementName}, Id - {message.ElementId}");
        }

        var dialog = new TaskDialog
        {
            ContentText = resultMessage.ToString()
        };
        dialog.Show();
        return PluginResult.Succeeded;
    }
}