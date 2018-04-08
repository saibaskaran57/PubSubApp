namespace Twitter
{
    using Core;
    using Integration;

    public sealed class TwitterSubscriber : ISubscriber
    {
        private const string SubscriberName = "Twitter";
        private readonly IConsole console;

        public TwitterSubscriber()
            : this(new Console())
        {
        }

        public TwitterSubscriber(IConsole console)
        {
            Guard.RequiresNotNull(console, nameof(console));

            this.console = console;
        }

        public void Subscribe(string message)
        {
            Guard.RequiresNotEmpty(message, nameof(message));

            this.console.WriteLine($"{SubscriberName} : {message}");
        }
    }
}
