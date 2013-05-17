using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Aidon.Tools.Gollum.Bugzilla
{
    public class XmlRequest
    {
        public string Method { get; set; }
        public List<XmlParameter> Parameters { get; set; }

        public XmlRequest()
        {
            Parameters = new List<XmlParameter>();
        }

        public override string ToString()
        {
            var xml = new XDocument();
            var methodCall = new XElement("methodCall");
            methodCall.Add(new XElement("methodName", Method));
            xml.AddFirst(methodCall);

            if (Parameters.Count > 0)
            {
                var structi = new XElement("struct");
                var paramss = new XElement("params",
                                           new XElement("param",
                                                        new XElement("value", structi)));

                foreach (var xmlParameter in Parameters)
                {
                    var member = new XElement("member", new XElement("name", xmlParameter.Name), GetParameterValue(xmlParameter.Value));
                    structi.Add(member);
                }

                methodCall.Add(paramss);
            }

            return xml.ToString();
        }

        private XElement GetParameterValue(object value)
        {
            if (value is int)
            {
                return new XElement("value", new XElement("int", value));
            }
            if (value is string)
            {
                return new XElement("value", new XElement("string", value));
            }
            if (value is IEnumerable)
            {
                return new XElement("value", ToArrayElement(value));
            }

            var commentHash = value as XmlCommentHash;
            if (commentHash != null)
            {
                var hash = new XElement("hash");
                hash.Add(new XElement("body", new XElement("string", commentHash.Body)));
                hash.Add(new XElement("is_private", new XElement("boolean", commentHash.IsPrivate)));
                return hash;
            }

            return null;
        }

        private XElement ToArrayElement(object array)
        {
            var list = (IEnumerable)array;

            var data = new XElement("data");
            foreach (var value in list)
            {
                data.Add(GetParameterValue(value));
            }

            return new XElement("array", data);
        }
    }
}