namespace RxBim.Tools.Autocad
{
    using Abstractions;
    using Autodesk.AutoCAD.DatabaseServices;

    /// <inheritdoc />
    public interface IProblemElementsStorage : IProblemElementsStorage<ObjectId>
    {
    }
}