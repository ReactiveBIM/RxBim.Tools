namespace RxBim.Tools.TableBuilder
{
    using Di;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class AutocadTableBuilderContainerExtensions
    {
        /// <summary>
        /// Adds table converter to an AutoCAD table.
        /// </summary>
        /// <param name="container"><see cref="IServiceCollection"/> object.</param>
        public static IServiceCollection AddAutocadTableBuilder(this IServiceCollection container)
        {
            container.AddSingleton<IAutocadTableConverter, AutocadTableConverter>();
            return container;
        }
    }
}