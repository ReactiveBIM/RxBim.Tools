namespace RxBim.Tools.Revit.Tests
{
    using System;
    using Abstractions;
    using Di;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Services;
    using Xunit;

    public class RevitTransactionFactoryTests
    {
        private readonly IServiceProvider _container;

        public RevitTransactionFactoryTests()
        {
            var di = new TestDiConfigurator();
            di.Configure(GetType().Assembly);
            _container = di.Build();
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