namespace RxBim.Tools.LogStorage.Revit.Sample.Models;

/// <summary>
/// Message that contains data of element.
/// </summary>
public class ElementDataMessage : MessageBase
{
    /// <inheritdoc />
    public ElementDataMessage(
        string message,
        string elementName,
        string elementCategory,
        int elementId,
        bool isDebugMessage = false)
        : base(message, isDebugMessage)
    {
        ElementName = elementName;
        ElementCategory = elementCategory;
        ElementId = elementId;
    }

    /// <summary>
    /// Name of selected element.
    /// </summary>
    public string ElementName { get; }

    /// <summary>
    /// Id of selected element.
    /// </summary>
    public int ElementId { get; }

    /// <summary>
    /// Category of selected element.
    /// </summary>
    public string ElementCategory { get; }
}