namespace RxBim.Command.TableBuilder.Autocad.Sample
{
    using Abstractions;
    using Di;
    using Services;
    using Tools.Autocad;
    using Tools.Autocad.Extensions;

    /// <inheritdoc />
    public class Config : ICommandConfiguration
    {
        /// <inheritdoc />
        public void Configure(IContainer container)
        {
            container
                .AddAutocadHelpers()
                .AddAutocadTableSerializer()
                .AddSingleton<ITableDataService, TableDataService>();
        }
    }
}