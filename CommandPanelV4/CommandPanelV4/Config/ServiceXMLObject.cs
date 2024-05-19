using CommandPanelV4.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CommandPanelV4.Config
{
    public class ServiceXMLObject
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlIgnore]
        public bool IsInstalled { get { return SvcUtil.IsServiceInstalled(Name); } }
    }
}