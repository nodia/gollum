using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using RestSharp;

namespace Aidon.Tools.Gollum.Bugzilla
{
    public class BugzillaRestClient : GollumRestClient, IBugzillaHandler
    {
        public BugzillaRestClient(string clientUrl)
            : base("GollumBugzilla", clientUrl.TrimEnd('/') + "/xmlrpc.cgi")
        {
        }

        public async Task PostToBugzillaAsync(BugzillaArguments arguments)
        {
            await Login(arguments).ConfigureAwait(false);

            var updateStatus = new XmlRequest
            {
                Method = "Bug.update",
                Parameters =
                {
                    new XmlParameter {Name = "ids", Value = new[] { arguments.BugId  }},
                    new XmlParameter {Name = "status", Value = arguments.Status},
                    new XmlParameter {Name = "resolution", Value = arguments.Resolution}
                }
            };

            using (var tokenSource = new CancellationTokenSource())
            {
                var statusRequest = new RestRequest { Method = Method.POST, RequestFormat = DataFormat.Xml };
                statusRequest.AddParameter("text/xml", updateStatus.ToString(), ParameterType.RequestBody);

                tokenSource.CancelAfter(DefaultTimeout);
                var statusResponse = await ExecuteAsync(statusRequest, tokenSource.Token).ConfigureAwait(false);
                CheckResponse(statusResponse);

                var addComment = new XmlRequest
                {
                    Method = "Bug.add_comment",
                    Parameters =
                    {
                        new XmlParameter { Name = "id", Value = arguments.BugId },
                        new XmlParameter { Name = "comment", Value = arguments.Comment }
                    }
                };

                var commentRequest = new RestRequest { Method = Method.POST, RequestFormat = DataFormat.Xml };
                commentRequest.AddParameter("text/xml", addComment.ToString(), ParameterType.RequestBody);

                var commentResponse = await ExecuteAsync(commentRequest, tokenSource.Token).ConfigureAwait(false);
                CheckResponse(commentResponse);
            }
        }

        private async Task Login(BugzillaArguments arguments)
        {
            if (ReadCookies())
            {
                return;
            }

            if (arguments.CredentialCallback == null)
            {
                throw new BugzillaAuthenticationException();
            }
            var credentials = arguments.CredentialCallback("Bugzilla login");
            await SendLoginRequestAsync(credentials.Username, credentials.Password).ConfigureAwait(false);
        }

        private void CheckResponse(IRestResponse response)
        {
            if (response == null)
            {
                throw new BugzillaException("Null response from Bugzilla.");
            }

            XDocument responseXml;
            try
            {
                responseXml = XDocument.Parse(response.Content);
            }
            catch (Exception)
            {
                throw new BugzillaException("Invalid bugzilla address " + response.ResponseUri + ".");
            }

            var members = responseXml.Descendants("member");

            string faultCode = String.Empty;
            string faultString = String.Empty;
            foreach (var member in members)
            {
                var name = member.Element("name");
                if (name == null) continue;

                if (name.Value == "faultCode")
                {
                    var value = member.Element("value");
                    if (value != null)
                    {
                        faultCode = value.Value;
                    }
                }

                if (name.Value == "faultString")
                {
                    var value = member.Element("value");
                    if (value != null)
                    {
                        faultString = value.Value;
                    }
                }
            }

            switch (faultCode)
            {
                case "": break;
                case "300":
                case "301":
                case "305":
                case "50":
                    ClearCookieFile();
                    throw new BugzillaAuthenticationException();
                default:
                    var e = new BugzillaException("A Bugzilla error occured. Code: " + faultCode + ". Message: " + faultString);
                    e.Data.Add("Error details", response.Content);
                    throw e;
            }

            ProcessResponseCookies(response);
        }

        private async Task SendLoginRequestAsync(string username, string password)
        {
            var xml = new XmlRequest
            {
                Method = "User.login",
                Parameters =
                {
                    new XmlParameter { Name = "login", Value = username },
                    new XmlParameter { Name = "password", Value = password }
                }
            };

            var request = new RestRequest { Method = Method.POST, RequestFormat = DataFormat.Xml };
            request.AddParameter("text/xml", xml.ToString(), ParameterType.RequestBody);

            using (var tokenSource = new CancellationTokenSource())
            {
                tokenSource.CancelAfter(DefaultTimeout);
                var response = await ExecuteAsync(request, tokenSource.Token).ConfigureAwait(false);
                CheckResponse(response);
            }
        }

        public async Task<BugzillaBug> GetBugInformationAsync(BugzillaArguments arguments, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            await Login(arguments).ConfigureAwait(false);

            token.ThrowIfCancellationRequested();

            var xml = new XmlRequest
            {
                Method = "Bug.get",
                Parameters =
                {
                    new XmlParameter {Name = "ids", Value = new[] { arguments.UpdateToken }}
                }
            };

            var request = new RestRequest { Method = Method.POST, RequestFormat = DataFormat.Xml };
            request.AddParameter("text/xml", xml.ToString(), ParameterType.RequestBody);

            var response = await ExecuteAsync(request, token).ConfigureAwait(false);
            CheckResponse(response);
            return new BugzillaBug(response.Content);
            
        }
    }
}
