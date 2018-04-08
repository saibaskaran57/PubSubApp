namespace Integration.Tests
{
    using System;
    using Core;
    using Moq;
    using Xunit;

    public class EventPublisherTests
    {
        private const string Message = "Hello World";
        private const string PublisherName = "YourAppName";

        private readonly Mock<IBus<string>> mockedBus;

        public EventPublisherTests()
        {
            this.mockedBus = new Mock<IBus<string>>();
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenBusIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new EventPublisher(null));
        }

        [Fact]
        public void Publish_ShouldThrowArgumentException_WhenMessageIsEmpty()
        {
            var publisher = new EventPublisher(this.mockedBus.Object);

            Assert.NotNull(publisher);
            Assert.Throws<ArgumentException>(() => publisher.Publish(string.Empty, string.Empty));
        }

        [Fact]
        public void Publish_ShouldThrowArgumentNullException_WhenMessageIsNull()
        {
            var publisher = new EventPublisher(this.mockedBus.Object);

            Assert.NotNull(publisher);
            Assert.Throws<ArgumentNullException>(() => publisher.Publish(null, string.Empty));
        }

        [Fact]
        public void Publish_ShouldThrowArgumentException_WhenPublisherNameIsEmpty()
        {
            var publisher = new EventPublisher(this.mockedBus.Object);

            Assert.NotNull(publisher);
            Assert.Throws<ArgumentException>(() => publisher.Publish(Message, string.Empty));
        }

        [Fact]
        public void Publish_ShouldThrowArgumentNullException_WhenPublisherNameIsNull()
        {
            var publisher = new EventPublisher(this.mockedBus.Object);

            Assert.NotNull(publisher);
            Assert.Throws<ArgumentNullException>(() => publisher.Publish(Message, null));
        }

        [Fact]
        public void Publish_ShouldPublishSuccessfully()
        {
            var message = $"{Message} via {PublisherName}";
            this.mockedBus.Setup(m => m.Publish(message));

            var publisher = new EventPublisher(this.mockedBus.Object);

            publisher.Publish(Message, PublisherName);

            this.mockedBus.Verify(m => m.Publish(message), Times.Once);
        }
    }
}
