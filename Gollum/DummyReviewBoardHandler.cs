using System;
using System.Threading;
using System.Threading.Tasks;
using Aidon.Tools.Gollum.ReviewBoard;

namespace Aidon.Tools.Gollum
{
    class DummyReviewBoardHandler : IReviewBoardHandler
    {
        public async Task<ReviewBoardResponse> PostToReviewBoardAsync(ReviewBoardArguments arguments)
        {
            await Task.Delay(1000);
            arguments.CredentialCallback("Review Board login");
            await Task.Delay(2000).ConfigureAwait(false);
            return new ReviewBoardResponse();
        }

        public event EventHandler<ReviewIdDiscoveredEventArgs> ReviewIdDiscovered;
    }
}