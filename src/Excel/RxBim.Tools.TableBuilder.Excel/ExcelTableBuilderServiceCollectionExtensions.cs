namespace RxBim.Tools.TableBuilder
{
    using JetBrains.Annotations;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Container extensions
    /// </summary>
    public static class ExcelTableBuilderServiceCollectionExtensions
    {
        /// <summary>
        /// Registers table converters to/from an Excel workbook.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        [UsedImplicitly]
        public static IServiceCollection AddExcelTableBuilder(this IServiceCollection services)
        {
            return services
                .AddSingleton<IExcelTableConverter, ExcelTableConverter>()
                .AddSingleton<IFromExcelTableConverter, FromExcelTableConverter>();
        }
    }
}