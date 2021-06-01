using GSTCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSTCalculator.Common
{
    public class GstGenerator
    {
        public double[] CaculateGst(InputText text)
        {

            double total = (double)text.Expense.Total;

            double gst = Math.Round(total * 0.15);

            double totalWithoutGst = Math.Round(total - gst);

            double[] results = { gst, totalWithoutGst };

            return results;

        }

        public bool ValidateDataReceived(InputText text, Stack<string> openTag)
        {
            bool valid = true;

            if (text.Expense.Total == null)
            {
                valid = false;
            }
            if(openTag.Count == 0)
            {
                valid = false;
            }
            
          
            
            return valid;
        }
    }
}