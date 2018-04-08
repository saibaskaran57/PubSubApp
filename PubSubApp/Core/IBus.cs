namespace Core
{
    using System.Collections.Generic;

    public interface IBus<T> where T : class
    {
        /// <summary> 
        /// A method to publishe new item to the sequence.
        /// </summary>
        /// <param name="item">The item to be posted to the sequence.</param>
        void Publish(T item);

        /// <summary>
        /// A method to dispatch items to subscribers.
        /// </summary>
        /// <param name="subscribers"></param>
        void Subscribe(IReadOnlyList<ISubscriber> subscribers);
    }
}