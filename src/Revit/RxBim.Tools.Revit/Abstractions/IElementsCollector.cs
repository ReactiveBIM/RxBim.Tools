namespace RxBim.Tools.Revit
{
    /// <summary>
    /// Интерфейс коллектора элементов
    /// </summary>
    public interface IElementsCollector
        : IFilteredElementScopeManager, ISelectedElementsManager
    {
        /// <summary>
        /// Gets <see cref="IFilteredElementCollectorWrapper"/>.
        /// </summary>
        /// <param name="document"><see cref="IDocumentWrapper"/>.
        /// If null, <see cref="IDocumentWrapper"/> gets from current document.</param>
        /// <param name="scope"><see cref="FilteredElementScope"/>.
        /// If null, <see cref="FilteredElementScope"/> gets from <see cref="IElementsCollector.Scope"/>.</param>
        IFilteredElementCollectorWrapper GetCollector(
            IDocumentWrapper? document = null,
            FilteredElementScope? scope = null);
    }
}
