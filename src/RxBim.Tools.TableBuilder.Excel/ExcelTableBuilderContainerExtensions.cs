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
        /// Adds table serialization to Excel services.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/></param>
        public static IContainer AddExcelTableBuilder(this IContainer container)
        {
            return container
                .AddSingleton<IExcelTableSerializer, ExcelTableSerializer>()
                .AddSingleton<IExcelTableDeserializer, ExcelTableDeserializer>();
        }
    }
}