namespace RxBim.Tools.TableBuilder
{
    using System.Linq;
    using Di;

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
                .AddSingletonIfNotRegistered<IExcelTableConverter, ExcelTableConverter>()
                .AddSingletonIfNotRegistered<IFromExcelTableConverter, FromExcelTableConverter>();
        }

        /// <summary>
        /// Adds a singleton service to the container if it hasn't been already registered.
        /// </summary>
        /// <param name="container">DI container abstraction.</param>
        /// <typeparam name="TService">Service type.</typeparam>
        /// <typeparam name="TImplementation">Service implementation type.</typeparam>
        /// <returns>A reference to the <see cref="IContainer"/> instance after the operation has completed.</returns>
        private static IContainer AddSingletonIfNotRegistered<TService, TImplementation>(
            this IContainer container)
            where TService : class
            where TImplementation : class, TService
        {
            if (!container.GetCurrentRegistrations().Select(x => x.ServiceType).Contains(typeof(TService)))
                container.AddSingleton<TService, TImplementation>();

            return container;
        }
    }
}