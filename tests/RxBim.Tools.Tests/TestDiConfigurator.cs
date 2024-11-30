namespace RxBim.Tools.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using RxBim.Di;

    public class TestDiConfigurator : DiConfigurator<IPluginConfiguration>
    {
        /// <inheritdoc />
        protected override void ConfigureBaseDependencies()
        {
            Services.AddTransient<ILogStorage, LogStorage>();
        }
    }
}