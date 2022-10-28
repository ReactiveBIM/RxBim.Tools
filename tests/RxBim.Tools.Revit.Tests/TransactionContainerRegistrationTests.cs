namespace RxBim.Tools.Revit.Tests
{
    using Abstractions;
    using Di;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class TransactionContainerRegistrationTests
    {
        [Fact]
        public void DefaultContextRegistrationTest()
        {
            var mockContextService = new Mock<ITransactionContextService<IDocumentWrapper>>();

            var di = new TestDiConfigurator();
            di.Configure(GetType().Assembly);
            var container = di.Container;

            container.AddSingleton<ITransactionContextService<IDocumentWrapper>>(mockContextService.Object);
            container.AddSingleton<ITransactionContextService<ITransactionContextWrapper>>(mockContextService.Object);

            var result = container.GetService<ITransactionContextService<ITransactionContextWrapper>>();

            result.Should().Be(mockContextService.Object);
        }
    }
}