using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using static fpWebApp.W_SiigoCliente;

namespace fpWebApp
{
    public partial class W_SiigoFactura : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void CrearFactura()
        {
            string URLCrearFactura = "https://api.siigo.com/v1/invoices";

            Factura oFactura = new Factura()
            {
                document = new TipoFactura { id = 65140 },
                date = "2025-05-05",
                customer = new Cliente
                {
                    identification = "100513710578",
                    branch_office = "0"
                },
                seller = 51881,
                items = new List<Elementos>
                {
                    new Elementos
                    {
                        code = "Code-1",
                        description = "Producto de prueba",
                        quantity = 1,
                        taxed_price = 0,
                        taxes = new List<Impuesto>
                        {
                            new Impuesto { id = 1270 },
                        }
                    }
                },
                stamp = new Estampilla { send = true },
                mail = new Correo { send = true },
                observations = "Observaciones",
                payments = new List<MetodosPago>
                {
                    new MetodosPago
                    {
                        id = 542,
                        value = 0,
                        due_date = "2023-05-31"
                    }
                }
            };

            string respuesta = GetPostFactura(URLCrearFactura, oFactura);

            Console.WriteLine(respuesta);
        }

        public static string GetPostFactura(string url, Factura oFactura)
        {
            string result = "";
            WebRequest wRequest = WebRequest.Create(url);
            wRequest.Method = "post";
            wRequest.ContentType = "application/json;charset=UTF-8";
            wRequest.Headers.Add("Partner-Id", "SandboxSiigoApi");
            wRequest.Headers.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjExNDQzRDg2OUYxMzgwODlEREUwOTdENTNBN0YxNzVCNkQwNzIxNzdSUzI1NiIsInR5cCI6ImF0K2p3dCIsIng1dCI6IkVVUTlocDhUZ0luZDRKZlZPbjhYVzIwSElYYyJ9.eyJuYmYiOjE3NDYzNzY0ODYsImV4cCI6MTc0NjQ2Mjg4NiwiaXNzIjoiaHR0cDovL21zLXNlY3VyaXR5OjUwMDAiLCJhdWQiOiJodHRwOi8vbXMtc2VjdXJpdHk6NTAwMC9yZXNvdXJjZXMiLCJjbGllbnRfaWQiOiJTaWlnb0FQSSIsInN1YiI6IjUzNjc2OCIsImF1dGhfdGltZSI6MTc0NjM3NjQ4NSwiaWRwIjoibG9jYWwiLCJuYW1lIjoiY29udGFiaWxpZGFkQGZpdG5lc3NwZW9wbGVjbWQuY29tIiwibWFpbF9zaWlnbyI6ImNvbnRhYmlsaWRhZEBmaXRuZXNzcGVvcGxlY21kLmNvbSIsImNsb3VkX3RlbmFudF9jb21wYW55X2tleSI6IkZJVE5FU1NQRU9QTEVDRU5UUk9NRURJQ09ERVBPUlRJVk9TQVMiLCJ1c2Vyc19pZCI6IjMwODc2IiwidGVuYW50X2lkIjoiMHgwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDE4NDA2NyIsInVzZXJfbGljZW5zZV90eXBlIjoiMCIsInBsYW5fdHlwZSI6IjE0IiwidGVuYW50X3N0YXRlIjoiMSIsIm11bHRpdGVuYW50X2lkIjoiMTIzIiwiY29tcGFuaWVzIjoiMCIsImFwaV9zdWJzY3JpcHRpb25fa2V5IjoiNTA1ZGIxZDRjYjk4NDhkOTlhMTVkYWI1NDAxZWEzOGMiLCJhcGlfdXNlcl9jcmVhdGVkX2F0IjoiMTY3NDE1MDQ0NiIsImFjY291bnRhbnQiOiJmYWxzZSIsImp0aSI6IjU3NjEwREIzNEI2MDJEMzUxM0QzRUMyRUFDODYzNTlDIiwiaWF0IjoxNzQ2Mzc2NDg2LCJzY29wZSI6WyJTaWlnb0FQSSJdLCJhbXIiOlsiY3VzdG9tIl19.GvAw2gdCy7ZF35v7KDHvCH3LkWZapnn6Nd_eBsuaux22v5_cyJIvJ3W0H7aE3JrKdXT4DBrudA-laWnynd0d7cxWHINw43dJ36oskgaybfsFXBemzgu7iCOoF3EeHzplRVZLSfWR6LzeU14yH93jd25w2z7ldC8Q_1j_aAkTdKwN5wb9yhOeAPNnyJwliXcNe2LVadVxQjbbAp_aLG0MImw7Tsq59d0n8HK1147aHxiRbCuWFaMIbzxAABTNJ6eH6xFKqTqnJctF8gdHDgd10AzKTSUBADihSYiTWvazsfWnSzxVxbpZ9C5XCzSVpeWlgYZLfRNNG-W461I8mZLmDA");

            using (var oSW = new StreamWriter(wRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(oFactura);
                oSW.Write(json);
                oSW.Flush();
                oSW.Close();
            }

            WebResponse wResponse = wRequest.GetResponse();

            using (var oSR = new StreamReader(wResponse.GetResponseStream()))
            {
                result = oSR.ReadToEnd().Trim();
            }

            return result;
        }

        protected void btnCrearFactura_Click(object sender, EventArgs e)
        {
            CrearFactura();
        }

        public class Factura
        {
            public TipoFactura document { get; set; }
            public string date { get; set; }
            public Cliente customer { get; set; }

            public int seller { get; set; }

            public List<Elementos> items { get; set; }

            public Estampilla stamp { get; set; }

            public Correo mail { get; set; }

            public string observations { get; set; }

            public List<MetodosPago> payments { get; set; }
        }

        public class TipoFactura
        {
            public int id { get; set; }
        }

        public class Cliente
        {
            public string identification { get; set; }
            public string branch_office { get; set; }
        }

        public class Elementos
        {
            public string code { get; set; }
            public string description { get; set; }
            public int quantity { get; set; }
            public List<Impuesto> taxes { get; set; }
            public int taxed_price { get; set; }
        }

        public class Impuesto
        {
            public int id { get; set; }
        }

        public class Estampilla
        {
            public bool send { get; set; }
        }

        public class Correo
        {
            public bool send { get; set; }
        }

        public class MetodosPago
        {
            public int id { get; set; }
            public int value { get; set; }
            public string due_date { get; set; }
        }
    }
}