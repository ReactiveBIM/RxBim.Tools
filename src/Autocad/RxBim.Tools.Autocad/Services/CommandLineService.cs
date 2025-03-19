namespace RxBim.Tools.Autocad;

using JetBrains.Annotations;

/// <inheritdoc />
[UsedImplicitly]
internal class CommandLineService(IDocumentService documentService) : ICommandLineService
{
    /// <inheritdoc />
    public void WriteAsNewLine(string message) => documentService.GetActiveDocument()
        .Editor.WriteMessage($"\n{message}");
}