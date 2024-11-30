namespace RxBim.Tools.TableBuilder
{
    using Di;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Container extensions
    /// </summary>
    public static class RevitTableBuilderContainerExtensions
    {
        /// <summary>
        /// Register table converters.
        /// </summary>
        /// <param name="container"><see cref="IServiceCollection"/></param>
        public static IServiceCollection AddRevitTableBuilder(this IServiceCollection container)
        {
            return container
                .AddSingleton<IViewScheduleTableConverter, ViewScheduleTableConverter>();
        }
    }
}