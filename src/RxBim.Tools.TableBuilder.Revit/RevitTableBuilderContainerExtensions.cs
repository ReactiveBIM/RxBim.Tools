namespace RxBim.Tools.TableBuilder
{
    using Di;

    /// <summary>
    /// Container extensions
    /// </summary>
    public static class RevitTableBuilderContainerExtensions
    {
        /// <summary>
        /// Adds table serialization to Excel services.
        /// </summary>
        /// <param name="container"><see cref="IContainer"/></param>
        public static IContainer AddRevitTableBuilder(this IContainer container)
        {
            return container
                .AddSingleton<IViewScheduleTableSerializer, ViewScheduleTableSerializer>();
        }
    }
}