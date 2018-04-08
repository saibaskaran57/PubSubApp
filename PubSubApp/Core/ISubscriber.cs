namespace Core
{
    public interface ISubscriber
    {
        /// <summary>
        /// A method to subscribe from publisher.
        /// </summary>
        /// <param name="message">The dispatched message from publishers.</param>
        void Subscribe(string message);
    }
}
