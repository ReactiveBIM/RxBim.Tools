namespace RxBim.Tools.Revit.TestablePlugin.Sample
{
    using Abstractions;
    using Di;
    using Microsoft.Extensions.Configuration;
    using Models;
    using Revit;
    using Services;

    /// <inheritdoc />
    public class Config : ICommandConfiguration
    {
        /// <inheritdoc />
        public void Configure(IContainer container)
        {
            // framework
            container.AddRevitTools();

            // configurations and settings
            container.AddTransient(() => container.GetService<IConfiguration>()
                .GetSection(nameof(PluginSettings))
                .Get<PluginSettings>());

            // services
            container.AddTransient<ICollapseSteelConsumptionStatementService, CollapseSteelConsumptionStatementService>();
        }
    }
}
