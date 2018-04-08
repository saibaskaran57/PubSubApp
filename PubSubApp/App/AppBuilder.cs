namespace App
{
    using System;
    using Core;

    internal sealed class AppBuilder
    {
        private readonly IPublisher publisher;
        private readonly IConsole console;
        private readonly string publisherName;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppBuilder"/> class.
        /// </summary>
        /// <param name="publisher">The publisher to dispatch messages to subscribers.</param>
        /// <param name="console">Console writer/reader.</param>
        /// <param name="publisherName">The publisher name to display on messages.</param>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="ArgumentNullException"/>
        public AppBuilder(IPublisher publisher, IConsole console, string publisherName)
        {
            Guard.RequiresNotNull(publisher, nameof(publisher));
            Guard.RequiresNotNull(console, nameof(console));
            Guard.RequiresNotEmpty(publisherName, nameof(publisherName));

            this.console = console;
            this.publisher = publisher;
            this.publisherName = publisherName;
        }

        /// <summary>
        /// A method to send message to subscribers via publisher.
        /// </summary>
        public void Run()
        {
            try
            {
                this.console.Write("Message to publish : ");
                var message = this.console.ReadLine();

                publisher.Publish(message, publisherName);
            }
            catch (ArgumentException)
            {
                this.console.WriteLine("Warning - Please enter valid message (e.g. Hello World!).");
            }
        }
    }
}
