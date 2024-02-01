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
        /// <param name="container"><see cref="IContainer"/><see cref="IContainer"/></param>
        /// <param name="isTesting">Indicates that this is a test configuration.</param>
        public static IContainer AddRevitHelpers(this IContainer container, bool isTesting = false)
        {
            container.AddSingleton<IDocumentsCollector, DocumentsCollector>()
                .AddSingleton<ISheetsCollector, SheetsCollector>()
                .AddSingleton<IElementsDisplay, ElementsDisplayService>()
                .AddSingleton<ISharedParameterService, SharedParameterService>()
                //// for single instance of ScopedElementsCollector from IServiceProvider
                //// for IScopedElementsCollector and IElementsCollector.
                .AddSingleton<ScopedElementsCollector>()
                .AddSingleton<IElementsCollector>(container.GetService<ScopedElementsCollector>)
                .AddSingleton<IScopedElementsCollector>(container.GetService<ScopedElementsCollector>)
                .AddSingleton<ITransactionContextService<IDocumentWrapper>, DocumentContextService>()
                .AddSingleton<ITransactionContextService<ITransactionContextWrapper>, DocumentContextService>()
                .AddTransactionServices<RevitTransactionFactory>()
                .AddInstance(new RevitTask())
                .AddToolsServices();
            if (isTesting)
            {
                container.AddSingleton<IRevitTask, RevitTaskMock>();
            }
            else
            {
                container.AddSingleton<IRevitTask, RevitTaskAdapter>();
            }

            return container;
        }
    }
}