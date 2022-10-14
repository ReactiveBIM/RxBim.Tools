namespace RxBim.Tools.Revit.Abstractions
{
    using Autodesk.Revit.DB;

    /// <summary>
    /// Wrapper for <see cref="Document"/>.
    /// </summary>
    public interface IDocumentWrapper : ITransactionContextWrapper
    {
    }
}