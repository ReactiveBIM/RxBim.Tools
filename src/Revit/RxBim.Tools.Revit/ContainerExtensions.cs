namespace RxBim.Tools.Revit
{
    using Abstractions;
    using Collectors;
    using Di;
    using JetBrains.Annotations;
    using Services;

    /// <summary>
    /// Extensions for <see cref="IContainer"/>.
    /// </summary>
    [PublicAPI]
    public static class ContainerExtensions
    {
        /// <summary>
        /// Adds Revit services to the container.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> object.</param>
        public static IContainer AddRevitHelpers(this IContainer container)
        {
            return container.AddSingleton<IDocumentsCollector, DocumentsCollector>()
                .AddSingleton<ISheetsCollector, SheetsCollector>()
                .AddSingleton<IElementsDisplay, ElementsDisplayService>()
                .AddSingleton<ISharedParameterService, SharedParameterService>()
                .AddSingleton<IElementsCollector, ScopedElementsCollector>()
                .AddSingleton<IScopedElementsCollector, ScopedElementsCollector>()
                .AddSingleton<ITransactionContextService<IDocumentWrapper>, DocumentContextService>()
                .AddSingleton<ITransactionContextService<ITransactionContextWrapper>, DocumentContextService>()
                .AddTransactionServices<RevitTransactionFactory>()
                .AddInstance(new RevitTask())
                .AddToolsServices();
        }
    }
}