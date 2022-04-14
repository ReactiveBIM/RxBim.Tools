namespace RxBim.Tools.TableBuilder
{
    using Di;
    using Services;

    /// <summary>
    /// Container extensions
    /// </summary>
    public static class ExcelTableBuilderContainerExtensions
    {
        /// <summary>
        /// Registers table converters to/from an Excel workbook .
        /// </summary>
        /// <param name="container"><see cref="IContainer"/></param>
        public static IContainer AddExcelTableBuilder(this IContainer container)
        {
            return container
                .AddSingleton<IExcelTableConverter, ExcelTableConverter>()
                .AddSingleton<IFromExcelTableConverter, FromExcelTableConverter>();
        }
    }
}