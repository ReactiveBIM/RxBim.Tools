namespace RxBim.Tools.Revit
{
    using Di;

    /// <summary>
    /// Расширения для контейнера
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Добавляет сервисы работы с Revit в контейнер
        /// </summary>
        /// <param name="container">контейнер</param>
        public static void AddRevitTools(this IContainer container)
        {
            container.AddSingleton<IProblemElementsStorage, ProblemElementsStorage>();
            container.AddSingleton<IDocumentsCollector, DocumentsCollector>();
            container.AddSingleton<IDefinitionFilesCollector, DefinitionFilesCollector>();
            container.AddSingleton<IElementsDisplay, ElementsDisplayService>();
            container.AddSingleton<ISharedParameterService, SharedParameterService>();
            container.AddSingleton<IElementsCollector, ElementsCollector>();
            container.AddSingleton<IPickElementsService, PickElementsService>();
            container.AddSingleton<ITransactionContextService<DocumentContext>, DocumentContextService>();
            container.AddTransactionServices<RevitTransactionFactory>();
            container.AddInstance(new RevitTask());
        }
    }
}