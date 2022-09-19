namespace RxBim.Tools.TableBuilder.Revit.Sample
{
    using Di;
    using Tools.Revit;
    using Tools.TableBuilder;

    /// <inheritdoc />
    public class Config : ICommandConfiguration
    {
        /// <inheritdoc />
        public void Configure(IContainer container)
        {
            container.AddRevitTools();
            container.AddRevitTableBuilder();
        }
    }
}