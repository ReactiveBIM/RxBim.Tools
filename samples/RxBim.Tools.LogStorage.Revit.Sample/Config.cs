namespace RxBim.Tools.LogStorage.Revit.Sample
{
    using RxBim.Di;
    using RxBim.Tools.Revit;

    /// <inheritdoc />
    public class Config : ICommandConfiguration
    {
        /// <inheritdoc />
        public void Configure(IContainer container)
        {
            container.AddRevitHelpers();
        }
    }
}