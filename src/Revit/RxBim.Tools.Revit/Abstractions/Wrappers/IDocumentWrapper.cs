namespace RxBim.Tools.Revit;

/// <summary>
/// Wrapper for Document type.
/// </summary>
public interface IDocumentWrapper : IWrapper
{
    /// <summary>
    /// Document title.
    /// </summary>
    string Title { get; }
}