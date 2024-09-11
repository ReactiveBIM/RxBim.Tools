namespace RxBim.Tools.Autocad.Tests
{
    using System;
    using Di;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class AutocadTransactionFactoryTests
    {
        private readonly IContainer _container;

        public AutocadTransactionFactoryTests()
        {
            var mockDocumentContext = new Mock<ITransactionContextService<IDocumentWrapper>>();
            var mockDatabaseContext = new Mock<ITransactionContextService<IDatabaseWrapper>>();

            var di = new TestDiConfigurator();
            di.Configure(GetType().Assembly);
            _container = di.Container;

            _container.AddTransactionServices<AutocadTransactionFactory>();
            _container.AddSingleton(mockDocumentContext.Object);
            _container.AddSingleton(mockDatabaseContext.Object);
            _container.AddSingleton<ITransactionContextService<ITransactionContextWrapper>>(mockDocumentContext.Object);
        }

        [Fact]
        public void GetDefaultContextTest()
        {
            var transactionFactory = _container.GetService<ITransactionFactory>();
            Action act = () => transactionFactory.GetDefaultContext<ITransactionContextWrapper>();

            act.Should().NotThrow();
        }

        [Fact]
        public void GetDocumentContextTest()
        {
            var transactionFactory = _container.GetService<ITransactionFactory>();
            Action act = () => transactionFactory.GetDefaultContext<IDocumentWrapper>();

            act.Should().NotThrow();
        }

        [Fact]
        public void GetDatabaseContextTest()
        {
            var transactionFactory = _container.GetService<ITransactionFactory>();
            Action act = () => transactionFactory.GetDefaultContext<IDatabaseWrapper>();

            act.Should().NotThrow();
        }
    }
}