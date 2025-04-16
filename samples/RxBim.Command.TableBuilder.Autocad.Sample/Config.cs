namespace RxBim.Command.TableBuilder.Autocad.Sample;

using Abstractions;
using Di;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Tools.Autocad;
using Tools.TableBuilder;

/// <inheritdoc />
public class Config : ICommandConfiguration
{
    /// <inheritdoc />
    public void Configure(IServiceCollection container)
    {
        container
            .AddAutocadHelpers()
            .AddAutocadTableBuilder()
            .AddSingleton<ITableDataService, TableDataService>();
    }
}