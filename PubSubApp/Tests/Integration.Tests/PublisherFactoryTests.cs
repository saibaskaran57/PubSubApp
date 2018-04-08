namespace Integration.Tests
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Moq;
    using Xunit;

    public class PublisherFactoryTests
    {
        private IReadOnlyList<ISubscriber> mockedSubscribers;
        private readonly Mock<IBus<string>> mockedBus;

        public PublisherFactoryTests()
        {
            this.mockedSubscribers = GetMockedSubscribers();
            this.mockedBus = new Mock<IBus<string>>();
        }

        [Fact]
        public void Register_ShouldThrowArgumentNullException_WhenSubscribersIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => PublisherFactory.Register(null));
        }

        [Fact]
        public void Register_ShouldThrowArgumentException_WhenSubscribersEmpty()
        {
            Assert.Throws<ArgumentException>(() => PublisherFactory.Register(new List<ISubscriber>()));
        }

        [Fact]
        public void Register_ShouldRegisterAsReactiveBus()
        {
            var bus = PublisherFactory.Register(this.mockedSubscribers);
           
            Assert.NotNull(bus);
            Assert.IsType<ReactiveBus>(bus);
        }

        [Fact]
        public void Build_ShouldThrowArgumentNullException_WhenBusIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => PublisherFactory.Build(null));
        }

        [Fact]
        public void Build_ShouldReturnEventPublisher()
        {
            var publisher = PublisherFactory.Build(this.mockedBus.Object);

            Assert.NotNull(publisher);
            Assert.IsType<EventPublisher>(publisher);
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
