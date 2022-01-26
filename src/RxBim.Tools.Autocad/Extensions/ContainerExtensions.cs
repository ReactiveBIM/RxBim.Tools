namespace RxBim.Tools.Autocad.Extensions
{
    using Abstractions;
    using Autodesk.AutoCAD.DatabaseServices;
    using Di;
    using Serializers;
    using Services;
    using Tools.TableBuilder.Abstractions;

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

        /// <summary>
        /// Adds table serializer to AutoCAD table.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> object.</param>
        public static IContainer AddAutocadTableSerializer(this IContainer container)
        {
            container.AddSingleton<ITableSerializer<AutocadTableSerializerParameters, Table>, AutocadTableSerializer>();
            return container;
        }
    }
}