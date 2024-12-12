namespace RxBim.Command.TableBuilder.Revit.Sample
{
    using Abstractions;
    using Di;
    using Microsoft.Extensions.DependencyInjection;
    using Services;
    using Tools.Revit;
    using Tools.TableBuilder;

    /// <inheritdoc />
    public class Config : ICommandConfiguration
    {
        /// <inheritdoc />
        public void Configure(IServiceCollection container)
        {
            container.AddRevitHelpers();
            container.AddRevitTableBuilder();
            container.AddTransient<IViewScheduleCreator, ViewScheduleCreator>();
        }
    }
}