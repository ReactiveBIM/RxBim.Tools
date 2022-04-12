namespace RxBim.Tools.TableBuilder.Excel.Extensions
{
    using ClosedXML.Excel;
    using Di;
    using Models;
    using Services;
    using TableBuilder.Abstractions;

    /// <summary>
    /// Container extensions
    /// </summary>
    public static class ContainerExtensions
    {
        /// <summary>
        /// Adds a table serialization service to Excel
        /// </summary>
        /// <param name="container"><see cref="IContainer"/></param>
        public static IContainer AddExcelSerializer(this IContainer container)
        {
            return container
                .AddSingleton<ITableSerializer<ExcelTableSerializerParameters, IXLWorkbook>, ExcelTableSerializer>();
        }

        /// <summary>
        /// Adds a table deserialization service from Excel
        /// </summary>
        /// <param name="container"><see cref="IContainer"/></param>
        public static IContainer AddExcelDeserializer(this IContainer container)
        {
            return container.AddSingleton<ITableDeserializer<IXLWorksheet>, ExcelTableDeserializer>();
        }
    }
}