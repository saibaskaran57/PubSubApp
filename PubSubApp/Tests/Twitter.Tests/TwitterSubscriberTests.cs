namespace Twitter.Tests
{
    using System;
    using Core;
    using Moq;
    using Xunit;

    public class TwitterSubscriberTests
    {
        private const string SubscriberName = "Twitter";

        private readonly Mock<IConsole> mockedConsole;
        private readonly ISubscriber mockedSubscriber;

        public TwitterSubscriberTests()
        {
            this.mockedConsole = new Mock<IConsole>();
            this.mockedSubscriber = new TwitterSubscriber(this.mockedConsole.Object);
        }

        [Fact]
        public void Subscribe_ShouldThrowArgumentNullException_WhenConsoleIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TwitterSubscriber(null));
        }

        [Fact]
        public void Subscribe_ShouldThrowArgumentNullException_WhenMessageIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => this.mockedSubscriber.Subscribe(null));
        }

        [Fact]
        public void Subscribe_ShouldThrowArgumentException_WhenMessageIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => this.mockedSubscriber.Subscribe(string.Empty));
        }

        [Fact]
        public void Subscribe_ShouldConsistSubscriberNameAndMessage()
        {
            var message = "Hello World!";

            this.mockedConsole.Setup(m => m.WriteLine($"{SubscriberName} : {message}"));

            this.mockedSubscriber.Subscribe(message);

            this.mockedConsole.Verify(m => m.WriteLine($"{SubscriberName} : {message}"), Times.Once);
        }
    }
}