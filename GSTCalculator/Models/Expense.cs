using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace GSTCalculator.Models
{
    [XmlRoot(ElementName = "expense")]
    public class Expense
    {
        [XmlElement("cost_centre")]
        public string CostCentre { get; set; }

        [XmlElement("total")]
        public double Total { get; set; }

        [XmlElement("payment_method")]
        public string PaymentMethod { get; set; }
    }
}