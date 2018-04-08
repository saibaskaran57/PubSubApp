namespace Core
{
    public interface IWriter
    {
        /// <summary>
        /// A method to be written to target.
        /// </summary>
        /// <param name="text">Text to be written.</param>
        void Write(string text);

        /// <summary>
        /// A method to be written to target with new line.
        /// </summary>
        /// <param name="text">Text to be written.</param>
        void WriteLine(string text);
    }
}
