namespace RxBim.Tools.Autocad.Abstractions
{
    using Autodesk.AutoCAD.ApplicationServices;
    using CSharpFunctionalExtensions;

    /// <summary>
    /// AutoCAD documents service
    /// </summary>
    public interface IDocumentService
    {
        /// <summary>
        /// Gets the current document
        /// </summary>
        Result<Document> GetActiveDocument();
    }
}