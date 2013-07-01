using System.Threading.Tasks;
using Aidon.Tools.Gollum.ReviewBoard;

namespace Aidon.Tools.Gollum
{
    /// <summary>
    /// Interface for posting review requests to review board.
    /// </summary>
    public interface IReviewBoardHandler
    {
        /// <summary>
        /// Posts a new review request to review board.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>
        /// Response filled with good data.
        /// </returns>
        /// <exception cref="ReviewBoardException">Thrown if the review cannot be posted.</exception>
        Task<ReviewBoardResponse> PostToReviewBoardAsync(ReviewBoardArguments arguments);
    }
}
