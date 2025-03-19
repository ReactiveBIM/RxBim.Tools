namespace RxBim.Tools.Revit.Tests;

using Abstractions;
using Di;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Services;

public class TestDiConfigurator : DiConfigurator<IPluginConfiguration>
{
    /// <inheritdoc />
    protected override void ConfigureBaseDependencies()
    {
        var mockContextService = new Mock<ITransactionContextService<IDocumentWrapper>>();

        Services.AddTransactionServices<RevitTransactionFactory>();
        Services.AddSingleton(mockContextService.Object);
        Services.AddSingleton<ITransactionContextService<ITransactionContextWrapper>>(mockContextService.Object);
    }
}