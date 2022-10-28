namespace RxBim.Tools.Autocad.Tests
{
    using Di;

    public class TestDiConfigurator : DiConfigurator<IPluginConfiguration>
    {
        /// <inheritdoc />
        protected override void ConfigureBaseDependencies()
        {
            // Container.AddTransient<IBaseService, BaseService>();
        }
    }
}