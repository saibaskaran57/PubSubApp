namespace App
{
    using System.Collections.Generic;
    using Core;
    using Facebook;
    using Integration;
    using Twitter;

    internal static class Program
    {
        private const string PublisherName = "YourAppName";

        public static void Main(string[] args)
        {
            var publisher = PublisherFactory
                .Register(GetSubscribers())
                .Build();

            var console = new Console();

            var app = new AppBuilder(publisher, console, PublisherName);

            while (true)
            {
                app.Run();
            }
        }

        private static IReadOnlyList<ISubscriber> GetSubscribers()
        {
            return new List<ISubscriber>
            {
                new FacebookSubscriber(),
                new TwitterSubscriber()
            };
        }
    }
}
