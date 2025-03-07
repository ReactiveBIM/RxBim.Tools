namespace RxBim.Tools.TableBuilder;

using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Container extensions
/// </summary>
public static class RevitTableBuilderServiceCollectionExtensions
{
    /// <summary>
    /// Register table converters.
    /// </summary>
    /// <param name="container"><see cref="IServiceCollection"/>.</param>
    [UsedImplicitly]
    public static IServiceCollection AddRevitTableBuilder(this IServiceCollection container)
    {
        return container
            .AddSingleton<IViewScheduleTableConverter, ViewScheduleTableConverter>();
    }
}