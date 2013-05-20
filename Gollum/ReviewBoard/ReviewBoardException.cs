using System;

namespace Aidon.Tools.Gollum.ReviewBoard
{
    [Serializable]
    public class ReviewBoardException : Exception
    {
        public ReviewBoardException(string message) : base(message) { }
        public ReviewBoardException(string message, Exception innerException) : base(message, innerException) { }
    }
}
