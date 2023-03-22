namespace RxBim.Tools.Tests
{
    using RxBim.Di;

    public class TestDiConfigurator : DiConfigurator<IPluginConfiguration>
    {
        /// <inheritdoc />
        protected override void ConfigureBaseDependencies()
        {
            Container.AddTransient<ILogStorage, LogStorage>();
        }
    }
}