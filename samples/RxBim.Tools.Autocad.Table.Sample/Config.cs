namespace RxBim.Tools.Autocad.Table.Sample
{
    using Abstractions;
    using Di;
    using Services;

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