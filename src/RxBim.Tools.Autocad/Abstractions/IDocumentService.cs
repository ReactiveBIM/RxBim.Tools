namespace RxBim.Tools.Autocad.Abstractions
{
    using Autodesk.AutoCAD.ApplicationServices;
    using CSharpFunctionalExtensions;

    /// <summary>
    /// Сервис документа
    /// </summary>
    public interface IDocumentService
    {
        /// <summary>
        /// Возвращает текущий документ
        /// </summary>
        Result<Document> GetActiveDocument();
    }
}