namespace Integration
{
    using Core;

    internal sealed class EventPublisher : IPublisher
    {
        private readonly IBus<string> bus;

        /// <inheritdoc />
        public EventPublisher(IBus<string> bus)
        {
            Guard.RequiresNotNull(bus, nameof(bus));

            this.bus = bus;
        }

        /// <inheritdoc />
        public void Publish(string message, string publisherName)
        {
            Guard.RequiresNotEmpty(message, nameof(message));
            Guard.RequiresNotEmpty(publisherName, nameof(publisherName));

            this.bus.Publish($"{message} via {publisherName}");
        }
    }
}
