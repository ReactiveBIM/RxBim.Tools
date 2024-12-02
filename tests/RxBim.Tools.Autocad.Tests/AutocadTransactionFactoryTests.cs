namespace RxBim.Tools.Autocad.Tests
{
    using System;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class AutocadTransactionFactoryTests
    {
        private readonly IServiceProvider _container;

        public AutocadTransactionFactoryTests()
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

        [Fact]
        public void GetDatabaseContextTest()
        {
            var transactionFactory = _container.GetService<ITransactionFactory>();
            Action act = () => transactionFactory.GetDefaultContext<IDatabaseWrapper>();

            act.Should().NotThrow();
        }
    }
}