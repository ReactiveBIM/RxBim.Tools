namespace RxBim.Tools.Revit.Tests
{
    using System;
    using Abstractions;
    using Di;
    using FluentAssertions;
    using Moq;
    using Services;
    using Xunit;

    public class RevitTransactionFactoryTests
    {
        private readonly IContainer _container;

        private RevitTransactionFactoryTests()
        {
            var mockContextService = new Mock<ITransactionContextService<IDocumentWrapper>>();

            var di = new TestDiConfigurator();
            di.Configure(GetType().Assembly);
            _container = di.Container;

            _container.AddTransactionServices<RevitTransactionFactory>();
            _container.AddSingleton(mockContextService.Object);
            _container.AddSingleton<ITransactionContextService<ITransactionContextWrapper>>(mockContextService.Object);
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
    }
}