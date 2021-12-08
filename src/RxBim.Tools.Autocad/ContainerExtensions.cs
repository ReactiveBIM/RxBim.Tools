namespace RxBim.Tools.Autocad
{
    using Abstractions;
    using Autodesk.AutoCAD.DatabaseServices;
    using Di;
    using Serializers;
    using Services;
    using TableBuilder.Abstractions;

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
            container.AddSingleton<IObjectsSelectionService, ObjectsSelectionService>();
            container.AddSingleton<ICommandLineService, CommandLineService>();
            return container;
        }

        /// <summary>
        /// Adds table serializer to AutoCAD.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> object.</param>
        public static IContainer AddTableSerializer(this IContainer container)
        {
            container.AddSingleton<ITableSerializer<AutocadTableSerializerParameters, Table>, AutocadTableSerializer>();
            return container;
        }
    }
}