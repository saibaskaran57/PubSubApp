namespace Integration
{
    using Core;

    public class Console : IConsole
    {
        /// <inheritdoc />
        public string ReadLine() => System.Console.ReadLine();

        /// <inheritdoc />
        public void Write(string text) => System.Console.Write(text);

        /// <inheritdoc />
        public void WriteLine(string text) => System.Console.WriteLine(text);
    }
}
