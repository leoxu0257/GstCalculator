using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace GSTCalculator.Models
{
    public class DataReceived
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