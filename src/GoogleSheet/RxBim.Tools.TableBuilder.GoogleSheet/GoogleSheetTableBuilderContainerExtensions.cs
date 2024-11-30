namespace RxBim.Tools.TableBuilder;

using Di;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Container extensions.
/// </summary>
public static class GoogleSheetTableBuilderContainerExtensions
{
    /// <summary>
    /// Registers table converters from an Google Sheet workbook.
    /// </summary>
    /// <param name="container"><see cref="IServiceCollection"/></param>
    public static IServiceCollection AddGoogleSheetTableBuilder(this IServiceCollection container)
    {
        return container
            .AddSingleton<IFromGoogleSheetTableConverter, FromGoogleSheetTableConverter>();
    }
}