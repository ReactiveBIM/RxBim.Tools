namespace RxBim.Tools.Autocad.Services
{
    using Abstractions;
    using Autodesk.AutoCAD.ApplicationServices;
    using CSharpFunctionalExtensions;
    using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

    /// <inheritdoc />
    internal class DocumentService : IDocumentService
    {
        /// <inheritdoc />
        public Result<Document> GetActiveDocument()
        {
            return Application.DocumentManager.MdiActiveDocument ??
                   Result.Failure<Document>("No active document");
        }
    }
}