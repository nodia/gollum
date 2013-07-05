using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;

namespace Aidon.Tools.Gollum.ReviewBoard
{
    /// <summary>
    /// A handler for posting review board tickets to review board using the Review Board REST API version 2.0
    /// </summary>
    public class ReviewBoardRestClient : GollumRestClient, IReviewBoardHandler
    {
        private readonly string _clientUrl;

        public ReviewBoardRestClient(string clientUrl) : base("GollumReviewBoard", clientUrl.TrimEnd('/') + "/api/")
        {
            _clientUrl = clientUrl.TrimEnd('/') + "/";
        }

        /// <summary>
        /// Posts a new review request to review board.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        /// <returns>
        /// Response filled with good data.
        /// </returns>
        /// <exception cref="ReviewBoardException">Thrown if the review cannot be posted.</exception>
        public async Task<ReviewBoardResponse> PostToReviewBoardAsync(ReviewBoardArguments arguments)
        {
            if (!Login(arguments))
            {
                throw new ReviewBoardException("Invalid login details.");
            }

            var reviewRequest = await PostReviewDraftAsync(arguments.Repository).ConfigureAwait(false);
            if (reviewRequest == null)
            {
                throw new ReviewBoardException("Unable to post review!");
            }

            await AddReviewDiffAsync(reviewRequest.Id, arguments.BaseDirectory, arguments.DiffFile).ConfigureAwait(false);

            reviewRequest.Summary = arguments.Summary;
            reviewRequest.Description = arguments.Description;
            reviewRequest.Groups = arguments.Group;
            reviewRequest.Public = true;
            reviewRequest.BugsClosed = arguments.Bugs;

            await UpdateReviewRequestAsync(reviewRequest).ConfigureAwait(false);

            return new ReviewBoardResponse
            {
                ReviewTicketId = reviewRequest.Id.ToString(CultureInfo.InvariantCulture),
                ReviewUrl = _clientUrl + @"r/" + reviewRequest.Id,
            };
        }

        private bool Login(ReviewBoardArguments arguments)
        {
            if (ReadCookies())
            {
                return true;
            }

            if (arguments.CredentialCallback == null)
            {
                return false;
            }
            var credentials = arguments.CredentialCallback("Review Board login");
            Client.Authenticator = new HttpBasicAuthenticator(credentials.Username, credentials.Password);
            return true;
        }

        /// <summary>
        /// Checks that response does not containt errors.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <exception cref="ReviewBoardException">Throw if the REST call was unsuccesful.</exception>
        private void CheckResponse(IRestResponse response)
        {
            if (response == null)
            {
                throw new ReviewBoardException("Null response from review board.");
            }

            if (response.Content.IndexOf("\"err\":", StringComparison.Ordinal) >= 0)
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

        private async Task UpdateReviewRequestAsync(ReviewBoardReviewRequest reviewRequest)
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

            using (var tokenSource = new CancellationTokenSource())
            {
                tokenSource.CancelAfter(DefaultTimeout);
                var response = await ExecuteAsync(request, tokenSource.Token).ConfigureAwait(false);
                CheckResponse(response);
            }
        }

        /// <summary>
        /// Adds the review diff.
        /// </summary>
        /// <param name="reviewId">The review id.</param>
        /// <param name="baseDirectory">The base directory.</param>
        /// <param name="diffFile">The diff file.</param>
        /// <returns></returns>
        private async Task AddReviewDiffAsync(int reviewId, string baseDirectory, string diffFile)
        {
            var request = new RestRequest { Resource = "review-requests/" + reviewId + "/diffs/", Method = Method.POST };
            request.AddParameter("basedir", baseDirectory);
            var filename = Path.GetFileName(diffFile);

            request.AddFile("path", FileToByteArray(diffFile), filename, "multipart/form-data");
            using (var tokenSource = new CancellationTokenSource())
            {
                tokenSource.CancelAfter(DefaultTimeout);
                var response = await ExecuteAsync(request, tokenSource.Token).ConfigureAwait(false);
                CheckResponse(response);
            }
        }

        /// <summary>
        /// Posts a new review request draft to the specified repository.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <returns>
        /// ReviewRequest containing the ID of the created request.
        /// </returns>
        /// <exception cref="ReviewBoardException">Unable to deserialize response.</exception>
        private async Task<ReviewBoardReviewRequest> PostReviewDraftAsync(string repository)
        {
            var request = new RestRequest { Resource = "review-requests/", Method = Method.POST };
            request.AddParameter("repository", repository);
            using (var tokenSource = new CancellationTokenSource())
            {
                tokenSource.CancelAfter(DefaultTimeout);
                var response = await ExecuteAsync(request, tokenSource.Token).ConfigureAwait(false);
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
                        var totalBytes = (int)new FileInfo(filename).Length;
                        return binaryReader.ReadBytes(totalBytes);
                    }
                }
            }
            catch (Exception e)
            {
                throw new ReviewBoardException("Unable to read svn diff file!", e);
            }
        }
    }
}
