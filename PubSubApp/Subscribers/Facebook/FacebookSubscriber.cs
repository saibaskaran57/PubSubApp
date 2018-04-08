namespace Facebook
{
    using Core;
    using Integration;

    public sealed class FacebookSubscriber : ISubscriber
    {
        private const string SubscriberName = "Facebook";
        private readonly IConsole console;

        public FacebookSubscriber()
            : this(new Console())
        {
        }

        public FacebookSubscriber(IConsole console)
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
