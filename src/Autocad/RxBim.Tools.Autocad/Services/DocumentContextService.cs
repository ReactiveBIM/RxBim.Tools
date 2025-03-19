namespace RxBim.Tools.Autocad;

using JetBrains.Annotations;

/// <summary>
/// Service for <see cref="DocumentWrapper"/>.
/// </summary>
[UsedImplicitly]
internal class DocumentContextService(IDocumentService documentService) : ITransactionContextService<IDocumentWrapper>
{
    /// <inheritdoc />
    public IDocumentWrapper GetDefaultContext() => documentService.GetActiveDocument().Wrap();
}