namespace RxBim.Tools.Autocad.Services
{
    using Abstractions;
    using CSharpFunctionalExtensions;
    using JetBrains.Annotations;

    /// <inheritdoc />
    [UsedImplicitly]
    internal class CommandLineService : ICommandLineService
    {
        private readonly IDocumentService _documentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineService"/> class.
        /// </summary>
        /// <param name="documentService"><see cref="IDocumentService"/></param>
        public CommandLineService(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        /// <inheritdoc />
        public void WriteAsNewLine(string message)
        {
            _documentService.GetActiveDocument()
                .Tap(doc => doc.Editor.WriteMessage($"\n{message}"));
        }
    }
}