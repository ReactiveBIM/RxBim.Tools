namespace RxBim.Command.TableBuilder.Revit.Sample
{
    using Abstractions;
    using Di;
    using Services;
    using Tools.Revit;
    using Tools.TableBuilder;

    /// <inheritdoc />
    public class Config : ICommandConfiguration
    {
        /// <inheritdoc />
        public void Configure(IContainer container)
        {
            container.AddRevitHelpers();
            container.AddRevitTableBuilder();
            container.AddTransient<IViewScheduleCreator, ViewScheduleCreator>();
        }
    }
}