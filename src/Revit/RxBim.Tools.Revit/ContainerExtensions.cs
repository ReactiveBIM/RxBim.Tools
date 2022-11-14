namespace RxBim.Tools.Revit
{
    using Di;
    using JetBrains.Annotations;

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
        public static IContainer AddRevitTools(this IContainer container)
        {
            return container.AddSingleton<IDocumentsCollector, DocumentsCollector>()
                .AddSingleton<IDefinitionFilesCollector, DefinitionFilesCollector>()
                .AddSingleton<IElementsDisplay, ElementsDisplayService>()
                .AddSingleton<ISharedParameterService, SharedParameterService>()
                .AddSingleton<IElementsCollector, ElementsCollector>()
                .AddSingleton<IPickElementsService, PickElementsService>()
                .AddSingleton<ITransactionContextService<IDocumentWrapper>, DocumentContextService>()
                .AddSingleton<ITransactionContextService<ITransactionContextWrapper>, DocumentContextService>()
                .AddTransactionServices<RevitTransactionFactory>()
                .AddInstance(new RevitTask())
                .AddToolsServices();
        }
    }
}