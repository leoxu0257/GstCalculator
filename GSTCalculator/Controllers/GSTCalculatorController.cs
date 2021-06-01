using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GSTCalculator.Controllers
{
    public class GSTCalculatorController : ApiController
    {
        // GET: api/GSTCalculator
        public class GSTCalculatorController : ApiController
        {
            //private XmlNameTable filename;

            // GET: api/GSTCalculator
            private const String filename = "C:\\Users\\User\\Desktop\\Books.xml";

            private const String costCentreElementName = "cost_centre";
            private const String totalElementName = "total";
            private const String paymentMethodElementName = "payment_method";
            private const String venderElementName = "vender";
            private const String descriptionElementName = "description";
            private const String dateElementName = "date";
            public Received Get()
            {
                XmlTextReader reader = null;
                Received received = new Received();
                received.Expense = new Expense();

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
                                break;
                            case XmlNodeType.Text:
                                Console.Write(reader.Value);
                                if (elementName == costCentreElementName)
                                {
                                    received.Expense.CostCentre = reader.Value;
                                }
                                else if (elementName == totalElementName)
                                {
                                    received.Expense.Total = Convert.ToDouble(reader.Value);
                                }
                                else if (elementName == paymentMethodElementName)
                                {
                                    received.Expense.PaymentMethod = reader.Value;
                                }
                                else if (elementName == venderElementName)
                                {
                                    received.Vender = reader.Value;
                                }
                                else if (elementName == descriptionElementName)
                                {
                                    received.Description = reader.Value;
                                }
                                else if (elementName == dateElementName)
                                {
                                    received.Date = reader.Value;
                                }
                                break;
                            case XmlNodeType.EndElement: // can use for validation
                                Console.Write("</{0}>", reader.Name);
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
                double gst = generator.CaculateGst(received)[0];
                double totalWithoutGST = generator.CaculateGst(received)[1];
                return received;
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
