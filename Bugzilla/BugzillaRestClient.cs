using System;
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

        public void PostToBugzilla(BugzillaArguments arguments)
        {
            Login(arguments);
            UpdateBug(arguments);
        }

        private void Login(BugzillaArguments arguments)
        {
            try
            {
                if (arguments.CredentialCallback == null) return;
                
                if (ReadCookies()) return;

                var credentials = arguments.CredentialCallback("Bugzilla login");
                SendLoginRequest(credentials.Username, credentials.Password);
            }
            catch (BugzillaAuthenticationException)
            {
                Login(arguments);
            }
        }

        private void UpdateBug(BugzillaArguments arguments)
        {
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

            var statusRequest = new RestRequest { Method = Method.POST, RequestFormat = DataFormat.Xml };
            statusRequest.AddParameter("text/xml", updateStatus.ToString(), ParameterType.RequestBody);
            var statusResponse = Client.Execute(statusRequest);
            CheckResponse(statusResponse);
            
            var addComment = new XmlRequest
            {
                Method = "Bug.add_comment",
                Parameters =
                        {
                            new XmlParameter {Name = "id", Value = arguments.BugId},
                            new XmlParameter {Name = "comment", Value = arguments.Comment}
                        }
            };

            var commentRequest = new RestRequest { Method = Method.POST, RequestFormat = DataFormat.Xml };
            commentRequest.AddParameter("text/xml", addComment.ToString(), ParameterType.RequestBody);
            var commentResponse = Client.Execute(commentRequest);
            CheckResponse(commentResponse);
        }

        private void CheckResponse(RestResponse response)
        {
            if (response == null)
            {
                throw new BugzillaException("Null response from Bugzilla.");
            }

            var responseXml = XDocument.Parse(response.Content);
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

        private void SendLoginRequest(string username, string password)
        {
            var xml = new XmlRequest
            {
                Method = "User.login",
                Parameters =
                        {
                            new XmlParameter {Name = "login", Value = username},
                            new XmlParameter {Name = "password", Value = password}
                        }
            };

            var request = new RestRequest { Method = Method.POST, RequestFormat = DataFormat.Xml };
            request.AddParameter("text/xml", xml.ToString(), ParameterType.RequestBody);

            var response = Client.Execute(request);
            CheckResponse(response);
        }

        public BugzillaBug GetBugInformation(BugzillaArguments arguments)
        {
            Login(arguments);
            return GetBug(arguments.UpdateToken);
        }

        private BugzillaBug GetBug(string bugId)
        {
            var xml = new XmlRequest
            {
                Method = "Bug.get",
                Parameters =
                        {
                            new XmlParameter {Name = "ids", Value = new[] { bugId }}
                        }
            };

            var request = new RestRequest { Method = Method.POST, RequestFormat = DataFormat.Xml };
            request.AddParameter("text/xml", xml.ToString(), ParameterType.RequestBody);

            var response = Client.Execute(request);
            CheckResponse(response);

            return new BugzillaBug(response.Content);
        }
    }
}
