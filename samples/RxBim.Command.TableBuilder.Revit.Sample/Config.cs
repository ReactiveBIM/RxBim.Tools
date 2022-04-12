namespace RxBim.Command.TableBuilder.Revit.Sample
{
    using Abstractions;
    using Autodesk.Revit.DB;
    using Di;
    using Services;
    using Tools.Revit;
    using Tools.Revit.Serializers;
    using Tools.TableBuilder;
    using Tools.TableBuilder.Abstractions;

    /// <inheritdoc />
    public class Config : ICommandConfiguration
    {
        /// <inheritdoc />
        public void Configure(IContainer container)
        {
            container.AddRevitHelpers();

            container.AddTransient<IViewScheduleCreator, ViewScheduleCreator>()
                .AddTransient<ITableSerializer<ViewScheduleTableSerializerParameters, ViewSchedule>,
                    ViewScheduleTableSerializer<ViewSchedule>>();
        }
    }
}