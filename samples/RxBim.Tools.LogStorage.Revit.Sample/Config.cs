namespace RxBim.Tools.LogStorage.Revit.Sample
{
    using Microsoft.Extensions.DependencyInjection;
    using RxBim.Di;
    using RxBim.Tools.Revit;

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