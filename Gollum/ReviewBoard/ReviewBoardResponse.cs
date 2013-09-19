namespace Aidon.Tools.Gollum.ReviewBoard
{
    public class ReviewBoardResponse
    {
        public string ReviewUrl { get; set; }
        public string ReviewTicketId { get; set; }
        public ReviewBoardReviewRequest ReviewRequest { get; set; }
    }
}