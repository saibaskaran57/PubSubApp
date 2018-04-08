namespace Integration.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Subjects;
    using Core;
    using Moq;
    using Xunit;

    public class ReactiveBusTests
    {
        private const string Message = "Hello World";
        private Mock<ISubject<string>> mockedQueue;

        public ReactiveBusTests()
        {
            this.mockedQueue = new Mock<ISubject<string>>();
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenQueueIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ReactiveBus(null));
        }

        [Fact]
        public void Publish_ShouldThrowArgumentException_WhenItemIsEmpty()
        {
            var bus = new ReactiveBus(this.mockedQueue.Object);

            Assert.NotNull(bus);
            Assert.Throws<ArgumentException>(() => bus.Publish(string.Empty));
        }

        [Fact]
        public void Publish_ShouldThrowArgumentNullException_WhenItemIsNull()
        {
            var bus = new ReactiveBus(this.mockedQueue.Object);

            Assert.NotNull(bus);
            Assert.Throws<ArgumentNullException>(() => bus.Publish(null));
        }

        [Fact]
        public void Publish_ShouldSuccessfullyPublishItem()
        {
            this.mockedQueue.Setup(m => m.OnNext(Message));

            var bus = new ReactiveBus(this.mockedQueue.Object);

            bus.Publish(Message);

            Assert.NotNull(bus);
            this.mockedQueue.Verify(m => m.OnNext(Message), Times.Once);
        }

        [Fact]
        public void Subscribe_ShouldThrowArgumentNullException_WhenSubscribersIsNull()
        {
            var bus = new ReactiveBus(this.mockedQueue.Object);

            Assert.NotNull(bus);
            Assert.Throws<ArgumentNullException>(() => bus.Subscribe(null));
        }

        [Fact]
        public void Subscribe_ShouldThrowArgumentException_WhenSubscribersIsEmpty()
        {
            var bus = new ReactiveBus(this.mockedQueue.Object);

            Assert.NotNull(bus);
            Assert.Throws<ArgumentException>(() => bus.Subscribe(new List<ISubscriber>()));
        }

        [Fact]
        public void Subscribe_ShouldSuccessfullySubscribeSubscribers()
        {
            this.mockedQueue.Setup(m => m.Subscribe(It.IsAny<IObserver<string>>()));

            var bus = new ReactiveBus(this.mockedQueue.Object);

            bus.Subscribe(GetMockedSubscribers());

            this.mockedQueue.Verify(m => m.Subscribe(It.IsAny<IObserver<string>>()), Times.Exactly(2));
        }

        [Fact]
        public void ShouldSuccessfullyPublishAndSubscribe()
        {
            var subject = new Subject<string>();
            var mockedSubscriber = new Mock<ISubscriber>();
            var bus = new ReactiveBus(subject);

            bus.Subscribe(new List<ISubscriber> { mockedSubscriber.Object });
            bus.Publish(Message);

            mockedSubscriber.Verify(m => m.Subscribe(Message), Times.Once);
        }

        private IReadOnlyList<ISubscriber> GetMockedSubscribers()
        {
            return new List<ISubscriber>
            {
                new Mock<ISubscriber>().Object,
                new Mock<ISubscriber>().Object,
            };
        }
    }
}
