namespace App.Tests
{
    using System;
    using App;
    using Core;
    using Moq;
    using Xunit;

    public class AppBuilderTests
    {
        private const string PublisherName = "YourAppName";
        private readonly Mock<IPublisher> mockedPublisher;
        private readonly Mock<IConsole> mockedConsole;

        public AppBuilderTests()
        {
            this.mockedPublisher = new Mock<IPublisher>();
            this.mockedConsole = new Mock<IConsole>();
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenPublisherIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AppBuilder(null, null, null));
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenConsoleIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AppBuilder(this.mockedPublisher.Object, null, null));
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentException_WhenPublisherNameIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => new AppBuilder(this.mockedPublisher.Object, this.mockedConsole.Object, string.Empty));
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenPublisherNameIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AppBuilder(this.mockedPublisher.Object, this.mockedConsole.Object, null));
        }

        [Fact]
        public void Start_ShouldSuccessfullyPublish()
        {
            var message = "Hello World";

            this.mockedConsole.Setup(m => m.Write("Message to publish : "));
            this.mockedConsole.Setup(m => m.WriteLine("Warning - Please enter valid message (e.g. Hello World!)."));
            this.mockedConsole.Setup(m => m.ReadLine()).Returns(message);
            this.mockedPublisher.Setup(m => m.Publish(message, PublisherName));

            var app = new AppBuilder(this.mockedPublisher.Object, this.mockedConsole.Object, PublisherName);
            app.Run();

            AssertApp(message, Times.Once(), Times.Never(), Times.Once(), Times.Once());
        }

        [Fact]
        public void Start_ShouldWriteWarningToConsole_WhenMessageIsEmpty()
        {
            var message = string.Empty;

            this.mockedConsole.Setup(m => m.Write("Message to publish : "));
            this.mockedConsole.Setup(m => m.WriteLine("Warning - Please enter valid message (e.g. Hello World!)."));
            this.mockedConsole.Setup(m => m.ReadLine()).Returns(message);
            this.mockedPublisher.Setup(m => m.Publish(message, PublisherName)).Throws(new ArgumentException());

            var app = new AppBuilder(this.mockedPublisher.Object, this.mockedConsole.Object, PublisherName);
            app.Run();

            AssertApp(message, Times.Once(), Times.Once(), Times.Once(), Times.Once());
        }

        private void AssertApp(string message, Times publishMessageCount, Times warningCount, Times readCount, Times publishCount)
        {
            this.mockedConsole.Verify(m => m.Write("Message to publish : "), publishMessageCount);
            this.mockedConsole.Verify(m => m.WriteLine("Warning - Please enter valid message (e.g. Hello World!)."), warningCount);
            this.mockedConsole.Verify(m => m.ReadLine(), readCount);
            this.mockedPublisher.Verify(m => m.Publish(message, PublisherName), publishCount);
        }
    }
}
