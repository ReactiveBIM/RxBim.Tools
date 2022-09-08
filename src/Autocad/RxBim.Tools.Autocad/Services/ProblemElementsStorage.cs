namespace RxBim.Tools.Autocad
{
    using Autodesk.AutoCAD.DatabaseServices;
    using JetBrains.Annotations;

    /// <inheritdoc cref="RxBim.Tools.Autocad.IProblemElementsStorage" />
    [UsedImplicitly]
    internal class ProblemElementsStorage : ProblemElementsStorage<ObjectId>, IProblemElementsStorage
    {
    }
}