using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSTCalculator.Models
{
    public class Expense
    {
        public string CostCentre { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
    }
}