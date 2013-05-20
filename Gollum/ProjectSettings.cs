using System.IO;
using System.Xml.Serialization;

namespace Aidon.Tools.Gollum
{
    /// <summary>
    /// Settings local to a project.
    /// </summary>
    public class ProjectSettings
    {
        public const string DefaultFileName = "gollum.xml";

        public string ReviewBoardGroup { get; set; }
        public string RepositoryBasePath { get; set; }
        public string ReviewBoardRepositoryName { get; set; }

        public ProjectSettings()
        {
            ReviewBoardGroup = "";
            RepositoryBasePath = "";
            ReviewBoardRepositoryName = "";
        }

        public static ProjectSettings Load(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var serializer = new XmlSerializer(typeof (ProjectSettings));
                return (ProjectSettings) serializer.Deserialize(reader);
            }
        }

        public static void Save(ProjectSettings settings, string path)
        {
            using (var writer = new StreamWriter(path))
            {
                var serializer = new XmlSerializer(typeof(ProjectSettings));
                serializer.Serialize(writer, settings);
            }
        }
    }
}
