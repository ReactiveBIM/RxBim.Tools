namespace RxBim.Tools.Autocad.Tests;

using Di;
using Microsoft.Extensions.DependencyInjection;
using Moq;

public class TestDiConfigurator : DiConfigurator<IPluginConfiguration>
{
    /// <inheritdoc />
    protected override void ConfigureBaseDependencies()
    {
        var mockDocumentContext = new Mock<ITransactionContextService<IDocumentWrapper>>();
        var mockDatabaseContext = new Mock<ITransactionContextService<IDatabaseWrapper>>();

        Services.AddTransactionServices<AutocadTransactionFactory>();
        Services.AddSingleton(mockDocumentContext.Object);
        Services.AddSingleton(mockDatabaseContext.Object);
        Services.AddSingleton<ITransactionContextService<ITransactionContextWrapper>>(mockDocumentContext.Object);
    }
}