namespace RxBim.Tools.TableBuilder
{
    using Di;
    using Serializers;

    /// <summary>
    /// Extensions for <see cref="IContainer"/>.
    /// </summary>
    public static class AutocadTableBuilderContainerExtensions
    {
        /// <summary>
        /// Adds table serializer to AutoCAD table.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> object.</param>
        public static IContainer AddAutocadTableBuilder(this IContainer container)
        {
            container.AddSingleton<IAutocadTableSerializer, AutocadTableSerializer>();
            return container;
        }
    }
}