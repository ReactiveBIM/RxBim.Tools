namespace RxBim.Tools.Autocad.Sample;

using Abstractions;
using Di;
using Microsoft.Extensions.DependencyInjection;
using Services;

/// <inheritdoc />
public class Config : ICommandConfiguration
{
    /// <inheritdoc />
    public void Configure(IServiceCollection container)
    {
        container.AddAutocadHelpers();
        container.AddSingleton<ICircleService, CircleService>();
        container.AddSingleton<IEntityService, EntityService>();
    }
}