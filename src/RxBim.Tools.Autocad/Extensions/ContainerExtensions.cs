namespace RxBim.Tools.Autocad.Extensions
{
    using Abstractions;
    using Di;
    using Services;

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
            container.AddSingleton<IDocumentService, DocumentService>();
            container.AddSingleton<IObjectsSelectionService, ObjectsSelectionService>();
            container.AddSingleton<ICommandLineService, CommandLineService>();
            return container;
        }
    }
}