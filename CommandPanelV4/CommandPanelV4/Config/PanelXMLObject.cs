using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CommandPanelV4.Config
{
    public class PanelXMLObject
    {
        public PanelXMLObject() { Services = new List<ServiceXMLObject>(); }
        [XmlArray]
        [XmlArrayItem("Service")]
        public List<ServiceXMLObject> Services { get; set; }
    }
}
