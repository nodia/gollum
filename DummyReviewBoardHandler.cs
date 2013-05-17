using System;
using System.Threading;
using Aidon.Tools.Gollum.ReviewBoard;

namespace Aidon.Tools.Gollum
{
    class DummyReviewBoardHandler : IReviewBoardHandler
    {
        public ReviewBoardResponse PostToReviewBoard(ReviewBoardArguments arguments)
        {
            Thread.Sleep(3000);
            var creds = arguments.CredentialCallback.Invoke("Review Board login");

            return new ReviewBoardResponse();
        }

        public event EventHandler<ReviewIdDiscoveredEventArgs> ReviewIdDiscovered;
    }
}