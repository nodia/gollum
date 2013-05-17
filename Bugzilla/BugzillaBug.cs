using System.Xml.Linq;

namespace Aidon.Tools.Gollum.Bugzilla
{
    public class BugzillaBug
    {
        public string Resolution { get; set; }
        public string Status { get; set; }
        public string UpdateToken { get; set; }
        public string Summary { get; set; }
        public string ReviewBoardTicketLink { get; set; }

        public BugzillaBug(string xml)
        {
            XDocument document = XDocument.Parse(xml);

            var members = document.Descendants("member");

            foreach (var member in members)
            {
                var name = member.Element("name");
                if (name != null && name.Value == "summary")
                {
                    var value = member.Element("value");
                    if (value != null)
                    {
                        Summary = value.Value;
                    }
                }

                if (name != null && name.Value == "resolution")
                {
                    var value = member.Element("value");
                    if (value != null)
                    {
                        Resolution = value.Value;
                    }
                }

                if (name != null && name.Value == "status")
                {
                    var value = member.Element("value");
                    if (value != null)
                    {
                        Status = value.Value;
                    }
                }

                if (name != null && name.Value == "update_token")
                {
                    var value = member.Element("value");
                    if (value != null)
                    {
                        UpdateToken = value.Value;
                    }
                }
            }
        }
    }
}