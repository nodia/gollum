using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp.Serializers;
using System.Xml;
using RestSharp;
using RestSharp.Deserializers;

namespace Gollum
{
    /// <summary>
    /// Helper class used with GollumXmlSerializer. Hopefully we will find a way to serialize with RestSharp base XMLSerializer instead of GollumXmlSerializer.
    /// </summary>
    public class TargetProcessComment
    {
        private String description;

        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }

    /*
     * XML Serializer that handles only Comment elements atm. 
     * RestSharp internal XmlSerializer does not generate attributes in a way TargetProcess expects them.  
    */
    public class GollumXmlSerializer : ISerializer
    {
        private string rootelement;
        private string dateformat;
        private string namesp;
        private string contenttype;

        // dummy hardcoded values 
        public String RootElement { 
            get { return "Comment"; } 
            set { rootelement = value; } 
        }

        public String DateFormat { 
            get { return "dd-MM-yy"; } 
            set { dateformat = value; } 
        }

        public String Namespace { 
            get { return ""; } 
            set { namesp = value; } 
        }

        public String ContentType { 
            get { return "application/xml"; } 
            set { contenttype = value; } 
        }

        public string Serialize(object obj)
        {
            if (!(obj is TargetProcessComment)) throw new ArgumentException("Gollum eats only Comments!!");

            TargetProcessComment comment = obj as TargetProcessComment;

            XmlDocument commentDocument = new XmlDocument();

            XmlNode rootNode = commentDocument.CreateElement("Comment");
            commentDocument.AppendChild(rootNode);

            XmlNode descriptionNode = commentDocument.CreateElement("Description");
            descriptionNode.InnerText = comment.Description;

            XmlNode generalNode = commentDocument.CreateElement("General");
            XmlAttribute idAttribute = commentDocument.CreateAttribute("Id");
            idAttribute.InnerText = comment.Id.ToString();
            generalNode.Attributes.Append(idAttribute);
            
            rootNode.AppendChild(descriptionNode);
            rootNode.AppendChild(generalNode);
            return commentDocument.OuterXml;
        }
    }


    class TargetProcessHandler : ITargetProcessHandler
    {
        public void PostToTargetProcess(string taskNumber, string comment, bool changeStateToToReview)
        {
            TargetProcessComment tpComment = new TargetProcessComment();
            tpComment.Description = comment;
            tpComment.Id = Int32.Parse(taskNumber);

            RestClient client = new RestClient();
            client.BaseUrl = "http://adata/TargetProcess2/api/v1";
            client.Authenticator = new NtlmAuthenticator();
            client.ClearHandlers();
            client.AddHandler("*", new XmlDeserializer());

            RestRequest request = new RestRequest();
            request.Resource = "Comments";
            request.RequestFormat = DataFormat.Xml;
            request.XmlSerializer = new GollumXmlSerializer();
            request.AddBody(comment);
            request.Method = Method.POST;

            RestResponse response = client.Execute(request);
            if (response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                Console.WriteLine("Posting to Target Process failed.");
            }

            // TODO: update task to review state
        }
    }
}
