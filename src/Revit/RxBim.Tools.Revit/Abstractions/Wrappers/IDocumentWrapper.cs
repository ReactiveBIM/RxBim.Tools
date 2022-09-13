namespace RxBim.Tools.Revit;

using System.Collections.Generic;
using JetBrains.Annotations;

/// <summary>
/// Wrapper for Document type.
/// </summary>
[PublicAPI]
public interface IDocumentWrapper : IWrapper
{
    /// <summary>
    /// Document title.
    /// </summary>
    string Title { get; }
    
    /// <summary>
    /// Collection of <see cref="IViewSheetWrapper"/> for this document.
    /// </summary>
    IEnumerable<IViewSheetWrapper> ViewSheets { get; }
}