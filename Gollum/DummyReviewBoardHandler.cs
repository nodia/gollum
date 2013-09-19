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
            return new ReviewBoardResponse { ReviewTicketId = "121", ReviewUrl = "http://intra/reviewboard/121/" };
        }

        public async Task UploadDiffAsync(ReviewBoardReviewRequest reviewRequest, ReviewBoardArguments arguments)
        {
            await Task.Delay(2000).ConfigureAwait(false);
        }
    }
}