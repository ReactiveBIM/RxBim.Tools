namespace RxBim.Tools.TableBuilder
{
    using Di;

    /// <summary>
    /// Extensions for <see cref="IContainer"/>.
    /// </summary>
    public static class AutocadTableBuilderContainerExtensions
    {
        /// <summary>
        /// Adds table converter to an AutoCAD table.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/> object.</param>
        public static IContainer AddAutocadTableBuilder(this IContainer container)
        {
            container.AddSingleton<IAutocadTableConverter, AutocadTableConverter>();
            return container;
        }
    }
}