namespace RxBim.Tools.Autocad
{
    using System;
    using Autodesk.AutoCAD.ApplicationServices;
    using JetBrains.Annotations;
    using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

    /// <inheritdoc />
    [UsedImplicitly]
    internal class DocumentService : IDocumentService
    {
        /// <inheritdoc />
        public Document GetActiveDocument()
        {
            return Application.DocumentManager.MdiActiveDocument ??
                   throw new Exception("No active document");
        }
    }
}