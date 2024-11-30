namespace RxBim.Tools.TableBuilder
{
    using System.ComponentModel;
    using System.Linq;
    using Di;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Container extensions
    /// </summary>
    public static class ExcelTableBuilderContainerExtensions
    {
        /// <summary>
        /// Registers table converters to/from an Excel workbook .
        /// </summary>
        /// <param name="container"><see cref="IServiceCollection"/></param>
        public static IServiceCollection AddExcelTableBuilder(this IServiceCollection container)
        {
            return container
                .AddSingleton<IExcelTableConverter, ExcelTableConverter>()
                .AddSingleton<IFromExcelTableConverter, FromExcelTableConverter>();
        }
    }
}