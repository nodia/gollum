namespace Aidon.Tools.Gollum
{
    public class CopyToClipboardEventArgs
    {
        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CopyToClipboardEventArgs"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CopyToClipboardEventArgs(string message)
        {
            Message = message;
        }

    }
}
