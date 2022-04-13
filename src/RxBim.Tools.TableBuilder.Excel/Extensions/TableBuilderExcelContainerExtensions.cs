namespace RxBim.Tools.TableBuilder.Extensions
{
    using Abstractions;
    using Di;
    using Services;

    /// <summary>
    /// Container extensions
    /// </summary>
    public static class TableBuilderExcelContainerExtensions
    {
        /// <summary>
        /// Adds table serialization to Excel services.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/></param>
        public static IContainer AddExcelSerialization(this IContainer container)
        {
            return container
                .AddSingleton<IExcelTableSerializer, ExcelTableSerializer>()
                .AddSingleton<IExcelTableDeserializer, ExcelTableDeserializer>();
        }
    }
}