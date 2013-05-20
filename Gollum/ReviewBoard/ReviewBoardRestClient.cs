using System;
using System.Globalization;
using System.IO;
using RestSharp;
using RestSharp.Deserializers;

namespace Aidon.Tools.Gollum.ReviewBoard
{
    /// <summary>
    /// A handler for posting review board tickets to review board using the Review Board REST API version 2.0
    /// </summary>
    public class ReviewBoardRestClient : GollumRestClient, IReviewBoardHandler
    {
        public event EventHandler<ReviewIdDiscoveredEventArgs> ReviewIdDiscovered;

        private readonly string _clientUrl;

        public ReviewBoardRestClient(string clientUrl) : base("GollumReviewBoard", clientUrl.TrimEnd('/') + "/api/")
        {
            _clientUrl = clientUrl.TrimEnd('/') + "/";
        }

        /// <summary>
        /// Posts a new review request to review board.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>Response filled with good data.</returns>
        /// <exception cref="ReviewBoardException">Thrown if the review cannot be posted.</exception>
        public ReviewBoardResponse PostToReviewBoard(ReviewBoardArguments arguments)
        {
            return PostReviewRequest(arguments);
        }

        private ReviewBoardResponse PostReviewRequest(ReviewBoardArguments arguments)
        {
            try
            {
                if (!ReadCookies())
                {
                    if (arguments.CredentialCallback != null)
                    {
                        var credentials = arguments.CredentialCallback("Review Board login");
                        Client.Authenticator = new HttpBasicAuthenticator(credentials.Username, credentials.Password);
                    }
                }

                var reviewRequest = PostReviewDraft(arguments.Repository);
                if (reviewRequest == null)
                {
                    throw new ReviewBoardException("Unable to post review!");
                }

                OnReviewIdDiscovered(_clientUrl + @"r/" + reviewRequest.Id);

                AddReviewDiff(reviewRequest.Id, arguments.BaseDirectory, arguments.DiffFile);

                reviewRequest.Summary = arguments.Summary;
                reviewRequest.Description = arguments.Description;
                reviewRequest.Groups = arguments.Group;
                reviewRequest.Public = true;
                reviewRequest.BugsClosed = arguments.Bugs;

                UpdateReviewRequest(reviewRequest);

                var reviewBoardResponse = new ReviewBoardResponse
                    {
                        ReviewTicketId = reviewRequest.Id.ToString(CultureInfo.InvariantCulture),
                        ReviewUrl = _clientUrl + @"r/" + reviewRequest.Id
                    };

                return reviewBoardResponse;
            }
            catch (ReviewBoardAuthenticationException)
            {
                return PostReviewRequest(arguments);
            }
        }

        /// <summary>
        /// Checks that response does not containt errors.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <exception cref="ReviewBoardException">Throw if the REST call was unsuccesful.</exception>
        private void CheckResponse(RestResponse response)
        {
            if (response == null)
            {
                throw new ReviewBoardException("Null response from review board.");
            }

            if (response.Content.IndexOf("\"err\":", StringComparison.Ordinal) > 0)
            {
                ReviewBoardErrorResponse error;
                try
                {
                    var deserializer = new JsonDeserializer { RootElement = "err" };
                    error = deserializer.Deserialize<ReviewBoardErrorResponse>(response);
                }
                catch (Exception e)
                {
                    throw new ReviewBoardException("Unable to deserialize error response.", e);
                }

                if (error != null)
                {
                    switch (error.Code)
                    {
                        case "103":
                        case "101":
                        case "218": ClearCookieFile(); throw new ReviewBoardAuthenticationException();
                        default:
                            var e = new ReviewBoardException("A Review Board error occured. Code: " + error.Code + ". Message: " + error.Msg);
                            e.Data.Add("Error details", response.Content);
                            throw e;
                    }
                }
            }

            ProcessResponseCookies(response);
        }

        private void UpdateReviewRequest(ReviewBoardReviewRequest reviewRequest)
        {
            var request = new RestRequest
                {
                    Resource = "review-requests/" + reviewRequest.Id + "/draft/",
                    Method = Method.PUT
                };

            request.AddParameter("summary", reviewRequest.Summary);
            request.AddParameter("public", reviewRequest.Public.ToString());
            request.AddParameter("target_groups", reviewRequest.Groups);
            request.AddParameter("description", reviewRequest.Description);
            
            if (!String.IsNullOrEmpty(reviewRequest.BugsClosed))
            {
                request.AddParameter("bugs_closed", reviewRequest.BugsClosed);
            }

            RestResponse response = Client.Execute(request);
            CheckResponse(response);
        }

        /// <summary>
        /// Adds the review diff.
        /// </summary>
        /// <param name="reviewId">The review id.</param>
        /// <param name="baseDirectory">The base directory.</param>
        /// <param name="diffFile">The diff file.</param>
        private void AddReviewDiff(int reviewId, string baseDirectory, string diffFile)
        {
            var request = new RestRequest { Resource = "review-requests/" + reviewId + "/diffs/", Method = Method.POST };
            request.AddParameter("basedir", baseDirectory);
            string filename = Path.GetFileName(diffFile);

            request.AddFile("path", FileToByteArray(diffFile), filename, "multipart/form-data");

            RestResponse response = Client.Execute(request);
            CheckResponse(response);
        }

        /// <summary>
        /// Posts a new review request draft to the specified repository. 
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <returns>ReviewRequest containing the ID of the created request.</returns>
        private ReviewBoardReviewRequest PostReviewDraft(string repository)
        {
            var request = new RestRequest { Resource = "review-requests/", Method = Method.POST };
            request.AddParameter("repository", repository);

            RestResponse response = Client.Execute(request);

            CheckResponse(response);

            try
            {
                var deserializer = new JsonDeserializer { RootElement = "review_request" };
                return deserializer.Deserialize<ReviewBoardReviewRequest>(response);
            }
            catch (Exception e)
            {
                throw new ReviewBoardException("Unable to deserialize response.", e);
            }
        }

        /// <summary>
        /// Reads a file and converts it to byte array. Used when sending diffs to review board.
        /// </summary>
        /// <param name="filename">Parh and filename to read.</param>
        /// <returns>File in byte array.</returns>
        /// <exception cref="ReviewBoardException">Thrown if the file cannot be loaded.</exception>
        private static byte[] FileToByteArray(string filename)
        {
            try
            {
                using (var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    using (var binaryReader = new BinaryReader(fileStream))
                    {
                        long totalBytes = new FileInfo(filename).Length;
                        return binaryReader.ReadBytes((Int32)totalBytes);
                    }
                }
            }
            catch (Exception e)
            {
                throw new ReviewBoardException("Unable to read svn diff file!", e);
            }
        }

        protected virtual void OnReviewIdDiscovered(string url)
        {
            EventHandler<ReviewIdDiscoveredEventArgs> handler = ReviewIdDiscovered;
            if (handler != null)
            {
                handler(this, new ReviewIdDiscoveredEventArgs { ReviewBoardTicketLink = url });
            }
        }
    }
}
