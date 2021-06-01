using GSTCalculator.Common;
using GSTCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

namespace GSTCalculator.Controllers
{
    public class GSTCalculatorController : ApiController
    {
        // GET: api/GSTCalculator
        //private XmlNameTable filename;

        // GET: api/GSTCalculator
        private const String filename = "C:\\Users\\User\\Desktop\\Books.xml";

        private const String costCentreElementName = "cost_centre";
        private const String totalElementName = "total";
        private const String paymentMethodElementName = "payment_method";
        private const String venderElementName = "vender";
        private const String descriptionElementName = "description";
        private const String dateElementName = "date";
        public IActionResult Get()
        {
            XmlTextReader reader = null;
            InputText text = new InputText();
            text.Expense = new Expense();
            string[] openTag = new string[] { };
            string[] closeTag = new string[] { };
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
                            openTag.Append(elementName);
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
                            closeTag.Append(elementName);
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
            bool valid = generator.ValidateDataReceived(text, openTag, closeTag);
            if (!valid) 
            { 
                return (IActionResult)BadRequest("Request not valid"); 
            }
            
            double gst = generator.CaculateGst(text)[0];
            double totalWithoutGST = generator.CaculateGst(text)[1];
            return (IActionResult)Ok(text);
        }

        // GET: api/GSTCalculator/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GSTCalculator
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GSTCalculator/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GSTCalculator/5
        public void Delete(int id)
        {
        }
    }
}
