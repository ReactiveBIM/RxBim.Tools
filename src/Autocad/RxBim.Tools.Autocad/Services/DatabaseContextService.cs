namespace RxBim.Tools.Autocad;

using JetBrains.Annotations;

/// <summary>
/// Service for <see cref="DatabaseWrapper"/>.
/// </summary>
[UsedImplicitly]
internal class DatabaseContextService(IDocumentService documentService) : ITransactionContextService<IDatabaseWrapper>
{
    /// <inheritdoc />
    public IDatabaseWrapper GetDefaultContext() => documentService.GetActiveDocument().Database.Wrap();
}