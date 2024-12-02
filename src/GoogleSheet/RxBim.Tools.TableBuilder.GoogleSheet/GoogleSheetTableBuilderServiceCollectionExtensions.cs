namespace RxBim.Tools.TableBuilder;

using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Container extensions.
/// </summary>
public static class GoogleSheetTableBuilderServiceCollectionExtensions
{
    /// <summary>
    /// Registers table converters from an Google Sheet workbook.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    [UsedImplicitly]
    public static IServiceCollection AddGoogleSheetTableBuilder(this IServiceCollection services)
    {
        return services
            .AddSingleton<IFromGoogleSheetTableConverter, FromGoogleSheetTableConverter>();
    }
}