namespace RxBim.Tools.TableBuilder;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/>.
/// </summary>
public static class AutocadTableBuilderServiceCollectionExtensions
{
    /// <summary>
    /// Adds table converter to an AutoCAD table.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> object.</param>
    public static IServiceCollection AddAutocadTableBuilder(this IServiceCollection services)
    {
        services.AddSingleton<IAutocadTableConverter, AutocadTableConverter>();
        return services;
    }
}