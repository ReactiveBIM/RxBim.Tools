namespace RxBim.Tools.Revit;

using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

/// <summary>
/// Wrapped <see cref="Document"/>.
/// </summary>
public class DocumentWrapper
    : Wrapper<Document>, IDocumentWrapper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentWrapper"/> class.
    /// </summary>
    /// <param name="wrappedObject"><see cref="Document"/></param>
    public DocumentWrapper(Document wrappedObject)
        : base(wrappedObject)
    {
    }

    /// <inheritdoc />
    public string Title
        => Object.Title;

    /// <inheritdoc />
    public IViewWrapper ActiveView
        => Object.ActiveView.Wrap();

    /// <inheritdoc />
    public IEnumerable<IViewSheetWrapper> ViewSheets
        => new FilteredElementCollector(Object)
            .WhereElementIsNotElementType()
            .OfClass<ViewSheet>()
            .Where(sheet => !sheet.IsPlaceholder
                            && sheet.CanBePrinted)
            .Select(viewSheet => viewSheet.Wrap());

    /// <inheritdoc />
    public void Regenerate()
        => Object.Regenerate();
}