namespace RxBim.Tools.TableBuilder.Autocad.Sample
{
    using Abstractions;
    using Di;
    using Services;
    using Tools.Autocad;
    using Tools.TableBuilder;

    /// <inheritdoc />
    public class Config : ICommandConfiguration
    {
        /// <inheritdoc />
        public void Configure(IContainer container)
        {
            container
                .AddAutocadTools()
                .AddAutocadTableBuilder()
                .AddSingleton<ITableDataService, TableDataService>();
        }
    }
}