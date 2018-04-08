namespace Integration
{
    using System.Collections.Generic;
    using System.Reactive;
    using System.Reactive.Subjects;
    using Core;

    internal sealed class ReactiveBus : IBus<string>
    {
        private readonly ISubject<string    > queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveBus"/> class.
        /// </summary>
        public ReactiveBus()
            : this(new Subject<string>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveBus"/> class.
        /// </summary>
        /// <param name="queue">The queue to be observed.</param>
        public ReactiveBus(ISubject<string> queue)
        {
            Guard.RequiresNotNull(queue, nameof(queue));

            this.queue = queue;
        }

        /// <inheritdoc />
        public void Publish(string item)
        {
            Guard.RequiresNotEmpty(item, nameof(item));

            this.queue.OnNext(item);
        }

        /// <inheritdoc />
        public void Subscribe(IReadOnlyList<ISubscriber> subscribers)
        {
            Guard.RequiresNotNull(subscribers, nameof(subscribers));
            Guard.RequiresPredicate(subscribers.Count > 0, nameof(subscribers));

            foreach (var subscriber in subscribers)
            {
                this.queue.Subscribe(Observer.Create<string>(feed => subscriber.Subscribe(feed)));
            }
        }
    }
}