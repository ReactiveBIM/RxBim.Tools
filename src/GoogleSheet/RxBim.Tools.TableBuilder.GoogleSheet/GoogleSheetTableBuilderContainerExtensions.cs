namespace RxBim.Tools.TableBuilder;

using Converters;
using Di;

/// <summary>
/// Container extensions.
/// </summary>
public static class GoogleSheetTableBuilderContainerExtensions
{
    /// <summary>
    /// Registers table converters from an Google Sheet workbook.
    /// </summary>
    /// <param name="container"><see cref="IContainer"/></param>
    public static IContainer AddGoogleSheetTableBuilder(this IContainer container)
    {
        return container
            .AddSingleton<IFromGoogleSheetTableConverter, FromGoogleSheetTableConverter>();
    }
}