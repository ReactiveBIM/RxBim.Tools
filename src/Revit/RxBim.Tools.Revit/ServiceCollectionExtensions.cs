namespace RxBim.Tools.Revit
{
    using Abstractions;
    using Collectors;
    using Helpers;
    using JetBrains.Annotations;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    [PublicAPI]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Revit services to the container.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/><see cref="IServiceCollection"/></param>
        /// <param name="isTesting">Indicates that this is a test configuration.</param>
        public static IServiceCollection AddRevitHelpers(this IServiceCollection services, bool isTesting = false)
        {
            services.AddSingleton<IDocumentsCollector, DocumentsCollector>()
                .AddSingleton<ISheetsCollector, SheetsCollector>()
                .AddSingleton<IElementsDisplay, ElementsDisplayService>()
                .AddSingleton<ISharedParameterService, SharedParameterService>()
                .AddSingleton<ScopedElementsCollector>()
                .AddSingleton<IElementsCollector>(sp => sp.GetService<ScopedElementsCollector>())
                .AddSingleton<IScopedElementsCollector>(sp => sp.GetService<ScopedElementsCollector>())
                .AddSingleton<ITransactionContextService<IDocumentWrapper>, DocumentContextService>()
                .AddSingleton<ITransactionContextService<ITransactionContextWrapper>, DocumentContextService>()
                .AddTransactionServices<RevitTransactionFactory>()
                .AddSingleton(new RevitTask())
                .AddToolsServices();
            if (isTesting)
            {
                services.AddSingleton<IRevitTask, RevitTaskMock>();
            }
            else
            {
                services.AddSingleton<IRevitTask, RevitTaskAdapter>();
            }

            return services;
        }
    }
}