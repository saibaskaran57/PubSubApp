namespace Integration
{
    using System.Collections.Generic;
    using Core;

    public static class PublisherFactory
    {
        /// <inheritdoc />
        public static IBus<string> Register(IReadOnlyList<ISubscriber> subscribers)
        {
            Guard.RequiresNotNull(subscribers, nameof(subscribers));
            Guard.RequiresPredicate(subscribers.Count > 0, nameof(subscribers));

            var bus = new ReactiveBus();
            bus.Subscribe(subscribers);

            return bus;
        }

        /// <inheritdoc />
        public static IPublisher Build(this IBus<string> bus)
        {
            Guard.RequiresNotNull(bus, nameof(bus));

            return new EventPublisher(bus);
        }
    }
}
