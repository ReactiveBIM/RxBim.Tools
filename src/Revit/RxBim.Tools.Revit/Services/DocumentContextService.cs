namespace RxBim.Tools.Revit.Services;

using Abstractions;
using Autodesk.Revit.UI;
using Extensions;
using JetBrains.Annotations;
using Models;

/// <summary>
/// The service for <see cref="DocumentWrapper"/>.
/// </summary>
[UsedImplicitly]
internal class DocumentContextService : ITransactionContextService<IDocumentWrapper>
{
    private readonly UIApplication _application;

    /// <summary>
    /// Initializes a new instance of the <see cref="DocumentContextService"/> class.
    /// </summary>
    /// <param name="application"><see cref="UIApplication"/> instance.</param>
    public DocumentContextService(UIApplication application)
    {
        _application = application;
    }

    /// <inheritdoc />
    public IDocumentWrapper GetDefaultContext()
    {
        return _application.ActiveUIDocument.Document.Wrap();
    }
}