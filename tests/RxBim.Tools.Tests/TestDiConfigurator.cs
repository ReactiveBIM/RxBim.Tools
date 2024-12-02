namespace RxBim.Tools.Tests
{
    using Di;
    using Microsoft.Extensions.DependencyInjection;

    public class TestDiConfigurator : DiConfigurator<IPluginConfiguration>
    {
        /// <inheritdoc />
        protected override void ConfigureBaseDependencies()
        {
            Services.AddTransient<ILogStorage, LogStorage>();
        }
    }
}