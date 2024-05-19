using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace CommandPanelV4.Config
{
    public class ConfigUtil
    {
        public const string CONFIG_PATH = "Config.xml";
        private static ConfigUtil? Instance;
        private PanelXMLObject? _config;
        public PanelXMLObject Config
        {
            get
            {
                return _config;
            }
            set
            {
                _config = value;
            }
        }

        public static PanelXMLObject GetConfig()
        {
            if (Instance == null)
            {
                Init();
            }
            return Instance.Config;
        }
        private static void Init()
        {
            Instance = new ConfigUtil();
            if (!File.Exists(CONFIG_PATH))
            {
                PanelXMLObject panelXMLObject = new PanelXMLObject();
                panelXMLObject.Services.Add(new ServiceXMLObject() { Name = "TestSvc1" });
                File.WriteAllText(CONFIG_PATH,SerializeXMLObject<PanelXMLObject>(panelXMLObject));
            }
            try
            {
                Instance._config = DeserializeXMLObjectFromFile<PanelXMLObject>(CONFIG_PATH);
            }
            catch (Exception exc)
            {
                MessageBox.Show($"Error while loading config {exc.Message}");
            }
            
        }

        public static XMLObject DeserializeXMLObjectFromFile<XMLObject>(string path)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(XMLObject));
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                XMLObject result = (XMLObject)serializer.Deserialize(fileStream);
                return result;
            }
        }

        public static XMLObject DeserializeXMLObjectFromString<XMLObject>(string XML)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(XMLObject));
            using (var clipboardString = GenerateStreamFromString(XML))
            {
                XMLObject result = (XMLObject)serializer.Deserialize(clipboardString);
                if (result != null)
                {
                    return result;
                }

            }
            return default(XMLObject);
        }
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public static string SerializeXMLObject<XMLObjectType>(XMLObjectType obj, bool includeNamespace = false)
        {
            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(XMLObjectType));
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            //settings.Encoding = Encoding.Unicode;
            if (!includeNamespace)
            {
                settings.OmitXmlDeclaration = true;
            }
            using (var stream = new UTF8StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, obj, emptyNamespaces);
                return stream.ToString();
            }
        }

        public sealed class UTF8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
    }
}
