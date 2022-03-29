namespace RxBim.Tools.Serializer.Excel.Extensions
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
        /// Add excel serializer service
        /// </summary>
        /// <param name="container"><see cref="IContainer"/></param>
        public static IContainer AddExcelSerializer(this IContainer container)
        {
            return container
                .AddSingleton<ITableSerializer<ExcelTableSerializerParameters, IXLWorkbook>, ExcelTableSerializer>();
        }

        /// <summary>
        /// Add excel deserializer service
        /// </summary>
        /// <param name="container"><see cref="IContainer"/></param>
        public static IContainer AddExcelDeserializer(this IContainer container)
        {
            return container.AddSingleton<ITableDeserializer<IXLWorksheet>, ExcelTableDeserializer>();
        }
    }
}