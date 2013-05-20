namespace Aidon.Tools.Gollum.ReviewBoard
{
    public class ReviewBoardReviewRequest
    {
        public int Id { get; set; }
        public string Branch { get; set; }
        public string BugsClosed { get; set; }
        public string ChangeDescription { get; set; }
        public string Description { get; set; }
        public bool Public { get; set; }
        public string Summary { get; set; }
        public string Groups { get; set; }
        public string People { get; set; }
        public string TestingDone { get; set; }
        public string Repository { get; set; }
    }
}
