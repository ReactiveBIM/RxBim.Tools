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
#if RVT2019 || RVT2020 || RVT2021 || RVT2022 || RVT2023
        int elementId,
#else
        long elementId,
#endif
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

#if RVT2019 || RVT2020 || RVT2021 || RVT2022 || RVT2023
    /// <summary>
    /// Id of selected element.
    /// </summary>
    public int ElementId { get; }
#else
    /// <summary>
    /// Id of selected element.
    /// </summary>
    public long ElementId { get; }
#endif

    /// <summary>
    /// Category of selected element.
    /// </summary>
    public string ElementCategory { get; }
}