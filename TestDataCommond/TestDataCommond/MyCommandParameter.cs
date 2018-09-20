using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestDataCommond
{
    public class MyCommandParameter
    {
        [XmlAttribute("Name")]
        public string ParamName;

        [XmlAttribute("Type")]
        public string ParamType;
    }
}
