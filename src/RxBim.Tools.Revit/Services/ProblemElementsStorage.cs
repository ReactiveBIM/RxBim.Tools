namespace RxBim.Tools.Revit.Services
{
    using Abstractions;
    using JetBrains.Annotations;

    /// <inheritdoc cref="RxBim.Tools.Revit.Abstractions.IProblemElementsStorage" />
    [UsedImplicitly]
    internal class ProblemElementsStorage : ProblemElementsStorage<int>, IProblemElementsStorage
    {
    }
}