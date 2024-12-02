namespace RxBim.Tools.LogStorage.Revit.Sample
{
    using Di;
    using Microsoft.Extensions.DependencyInjection;
    using Tools.Revit;

    /// <inheritdoc />
    public class Config : ICommandConfiguration
    {
        /// <inheritdoc />
        public void Configure(IServiceCollection container)
        {
            container.AddRevitHelpers();
        }
    }
}