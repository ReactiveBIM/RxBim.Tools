namespace RxBim.Command.TableBuilder.Autocad.Sample
{
    using Abstractions;
    using Di;
    using Services;
    using Tools.Autocad;

    /// <inheritdoc />
    public class Config : ICommandConfiguration
    {
        /// <inheritdoc />
        public void Configure(IContainer container)
        {
            container
                .AddAutocadHelpers()
                .AddTableSerializer()
                .AddSingleton<ITableDataService, TableDataService>();
        }
    }
}