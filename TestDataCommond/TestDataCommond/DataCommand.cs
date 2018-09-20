using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestDataCommond
{
    public class DataCommand
    {
        [XmlAttribute("Name")]
        public string CommandName;

        [XmlAttribute]
        public string Database;

        [XmlArrayItem("Parameter")]
        public List<MyCommandParameter> Parameters = new List<MyCommandParameter>();

        [XmlElement]
        public MyCDATA CommandText;
    }
}
