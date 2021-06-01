using GSTCalculator.Common;
using GSTCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using System;
using System.Xml;
using System.Linq;
using System.Collections.Generic;

namespace GSTCalculator.Controllers
{
    public class GSTCalculatorController : ApiController
    {
        private const String filename = "C:\\Users\\User\\Desktop\\Books.xml";

        private const String costCentreElementName = "cost_centre";
        private const String totalElementName = "total";
        private const String paymentMethodElementName = "payment_method";
        private const String venderElementName = "vender";
        private const String descriptionElementName = "description";
        private const String dateElementName = "date";

        [HttpPost]
        public IActionResult Post()
        {
            XmlTextReader reader = null;
            InputText text = new InputText();
            text.Expense = new Expense();
            Stack<string> openTag = new Stack<string>();
            try
            {
                // Load the reader with the data file and ignore all white space nodes.

                reader = new XmlTextReader(filename);
                reader.WhitespaceHandling = WhitespaceHandling.None;
                String elementName = String.Empty;

                // Parse the file and display each of the nodes.
                
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            Console.Write("<{0}>", reader.Name);
                            elementName = reader.Name;
                            openTag.Push(elementName);
                            break;
                        case XmlNodeType.Text:
                            Console.Write(reader.Value);
                            if (elementName == costCentreElementName)
                            {
                                text.Expense.CostCentre = reader.Value;
                            }
                            else if (elementName == totalElementName)
                            {
                                text.Expense.Total = Convert.ToDouble(reader.Value);
                            }
                            else if (elementName == paymentMethodElementName)
                            {
                                text.Expense.PaymentMethod = reader.Value;
                            }
                            else if (elementName == venderElementName)
                            {
                                text.Vender = reader.Value;
                            }
                            else if (elementName == descriptionElementName)
                            {
                                text.Description = reader.Value;
                            }
                            else if (elementName == dateElementName)
                            {
                                text.Date = reader.Value;
                            }
                            break;

                        case XmlNodeType.EndElement: // can use for validation
                            Console.Write("</{0}>", reader.Name);
                            if(openTag.Peek() == elementName)
                            {
                                openTag.Pop();
                            }
                            break;
                    }
                }
            }

            finally
            {
                if (reader != null)
                    reader.Close();
            }

            GstGenerator generator = new GstGenerator();
            bool valid = generator.ValidateDataReceived(text, openTag, closeTag);// check validation
            if (!valid) 
            {
                return BadRequest("Request not valid");
            }
            
            double gst = generator.CaculateGst(text)[0];
            double totalWithoutGST = generator.CaculateGst(text)[1];
            return Ok(text);
        }
    }
}
