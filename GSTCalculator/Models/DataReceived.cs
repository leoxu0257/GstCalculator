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
        public string Vender { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
    }
}