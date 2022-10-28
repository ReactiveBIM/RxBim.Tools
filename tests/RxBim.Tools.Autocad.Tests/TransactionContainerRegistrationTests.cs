namespace RxBim.Tools.Autocad.Tests
{
    using Di;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class TransactionContainerRegistrationTests
    {
        [Fact]
        public void DefaultContextRegistrationTest()
        {
            var mockDocumentContext = new Mock<ITransactionContextService<IDocumentWrapper>>();
            var mockDatabaseContext = new Mock<ITransactionContextService<IDatabaseWrapper>>();

            var di = new TestDiConfigurator();
            di.Configure(GetType().Assembly);
            var container = di.Container;

            container.AddSingleton<ITransactionContextService<IDocumentWrapper>>(mockDocumentContext.Object);
            container.AddSingleton<ITransactionContextService<IDatabaseWrapper>>(mockDatabaseContext.Object);
            container.AddSingleton<ITransactionContextService<ITransactionContextWrapper>>(mockDocumentContext.Object);

            var result = container.GetService<ITransactionContextService<ITransactionContextWrapper>>();

            result.Should().Be(mockDocumentContext.Object);
        }
    }
}