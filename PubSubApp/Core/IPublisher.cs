namespace Core
{
    public interface IPublisher
    {
        /// <summary>
        /// A method to publish new item to subscribers.
        /// </summary>
        /// <param name="message">Message to be published.</param>
        /// <param name="publisherName">The publisher name.</param>
        void Publish(string message, string publisherName);
    }
}
