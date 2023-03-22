namespace RxBim.Tools.Tests
{
    using System;
    using System.Threading;
    using Di;
    using FluentAssertions;
    using Models;
    using Xunit;

    public class LogStorageTests
    {
        private readonly ILogStorage _logStorage;
        private readonly ILogMessage _testTextMessage = new TextMessage("Test message");
        private readonly ILogMessage _testTextWithIdMessage = new TextWithIdMessage("Test message", new ObjectIdWrapper(1));

        public LogStorageTests()
        {
            var di = new TestDiConfigurator();
            di.Configure(GetType().Assembly);
            var container = di.Container;

            _logStorage = container.GetService<ILogStorage>();
        }

        [Fact]
        public void LogStorage_AddTextMessage_ShouldNotThrow()
        {
            Action act = () => _logStorage.AddMessage(_testTextMessage);

            act.Should().NotThrow();
        }
        
        [Fact]
        public void LogStorage_AddTextWithIdMessage_ShouldNotThrow()
        {
            Action act = () => _logStorage.AddMessage(_testTextWithIdMessage);

            act.Should().NotThrow();
        }
        
        [Fact]
        public void LogStorage_Add2SameTextMessages_ShouldHaveEqualMessagesCount()
        {
            Action act = () =>
            {
                _logStorage.AddMessage(_testTextMessage);
                Thread.Sleep(1000);
                _logStorage.AddMessage(_testTextMessage);
            };

            act.Should().NotThrow();

            _logStorage.Count().Should().Be(2);
        }
        
        [Fact]
        public void LogStorage_Add2SameTextWithIdMessages_ShouldHaveEqualMessagesCount()
        {
            Action act = () =>
            {
                _logStorage.AddMessage(_testTextWithIdMessage);
                Thread.Sleep(1000);
                _logStorage.AddMessage(_testTextWithIdMessage);
            };

            act.Should().NotThrow();

            _logStorage.Count().Should().Be(2);
        }
        
        [Fact]
        public void LogStorage_ClearMessages_ShouldHaveZeroMessagesCount()
        {
            Action act = () =>
            {
                _logStorage.AddMessage(_testTextMessage);
                _logStorage.Clear();
            };

            act.Should().NotThrow();
            _logStorage.Count().Should().Be(0);
        }
        
        [Fact]
        public void LogStorage_HasMessages_ShouldReturnTrue()
        {
            Action act = () =>
            {
                _logStorage.AddMessage(_testTextMessage);
            };

            act.Should().NotThrow();
            _logStorage.HasMessages().Should().Be(true);
        }
    }
}