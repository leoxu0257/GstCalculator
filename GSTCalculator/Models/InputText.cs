using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace GSTCalculator.Models
{
    [XmlRoot(ElementName = "input_text")]
    public class InputText
    {
        [XmlElement("expense")]
        public Expense Expense { get; set; }

        [XmlElement("vender")]
        public string Vender { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("date")]
        public string Date { get; set; }
    }
}