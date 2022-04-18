namespace RxBim.Tools.Autocad
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