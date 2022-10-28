namespace RxBim.Tools.Autocad
{
    using Di;

    /// <summary>
    /// Extensions for <see cref="IContainer"/>.
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Adds AutoCAD services to the container.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> object.</param>
        public static IContainer AddAutocadHelpers(this IContainer container)
        {
            return container
                .AddSingleton<IDocumentService, DocumentService>()
                .AddSingleton<IObjectsSelectionService, ObjectsSelectionService>()
                .AddSingleton<ICommandLineService, CommandLineService>()
                .AddSingleton<IElementsDisplay, ElementsDisplayService>()
                .AddSingleton<ITransactionContextService<IDatabaseWrapper>, DatabaseContextService>()
                .AddSingleton<ITransactionContextService<IDocumentWrapper>, DocumentContextService>()
                .AddSingleton<ITransactionContextService<ITransactionContextWrapper>, DocumentContextService>()
                .AddTransactionServices<AutocadTransactionFactory>()
                .AddToolsServices();
        }
    }
}