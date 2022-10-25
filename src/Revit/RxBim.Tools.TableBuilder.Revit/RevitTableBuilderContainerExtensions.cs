namespace RxBim.Tools.TableBuilder
{
    using Di;

    /// <summary>
    /// Container extensions
    /// </summary>
    public static class RevitTableBuilderContainerExtensions
    {
        /// <summary>
        /// Register table converters.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/></param>
        public static IContainer AddRevitTableBuilder(this IContainer container)
        {
            return container
                .AddSingleton<IViewScheduleTableConverter, ViewScheduleTableConverter>();
        }
    }
}