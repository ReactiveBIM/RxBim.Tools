namespace RxBim.Tools.Autocad;

using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extensions for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds AutoCAD services to the container.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> object.</param>
    public static IServiceCollection AddAutocadHelpers(this IServiceCollection services)
    {
        return services
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