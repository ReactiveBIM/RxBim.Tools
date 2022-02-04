namespace RxBim.Command.TableBuilder.Revit.Sample
{
    using Abstractions;
    using Autodesk.Revit.Attributes;
    using Command.Revit;
    using Shared;

    /// <inheritdoc />
    [Regeneration(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    public class CreateViewScheduleCmd : RxBimCommand
    {
        /// <summary>
        /// cmd
        /// </summary>
        /// <param name="creator"><see cref="IViewScheduleCreator"/></param>
        public PluginResult ExecuteCommand(IViewScheduleCreator creator)
        {
            return creator.CreateSomeViewSchedule(
                    "TestViewSchedule", 10, 10)
                .IsSuccess ? PluginResult.Succeeded : PluginResult.Failed;
        }
    }
}