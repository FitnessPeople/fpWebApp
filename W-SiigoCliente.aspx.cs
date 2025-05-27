using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
	public partial class W_SiigoCliente : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            //string Token = "YjU2NWE3YjktYjlhZS00OTRkLWE3NDgtODc0MGUyYjhmYzNlOjh9QDZyKDdwPkE=";
        }

        private void CrearCliente()
        {
            string URLCrearCliente = "https://api.siigo.com/v1/customers";

            Cliente oCliente = new Cliente()
            {
                person_type = "Person",
                id_type = "13",
                identification = "100513710578",
                name = new List<string> { "Brayan", "Rojas" },
                address = new Address
                {
                    address = "CLL 52 12 09",
                    city = new City
                    {
                        country_code = "Co",
                        state_code = "68",
                        city_code = "68001"
                    }
                },
                phones = new List<Phone> {
                    new Phone { number = "3156325002" }
                },
                contacts = new List<Contact> {
                    new Contact
                    {
                        first_name = "Brayan",
                        last_name = "Rojas",
                        email = "brayan.rojas@correo.com"
                    }
                }
            };

            string respuesta = GetPostCliente(URLCrearCliente, oCliente);

            Console.WriteLine(respuesta);
        }

        public static string GetPostCliente(string url, Cliente oCliente)
        {
            string result = "";
            WebRequest wRequest = WebRequest.Create(url);
            wRequest.Method = "post";
            wRequest.ContentType = "application/json;charset=UTF-8";
            wRequest.Headers.Add("Partner-Id", "SandboxSiigoApi");
            wRequest.Headers.Add("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6IjExNDQzRDg2OUYxMzgwODlEREUwOTdENTNBN0YxNzVCNkQwNzIxNzdSUzI1NiIsInR5cCI6ImF0K2p3dCIsIng1dCI6IkVVUTlocDhUZ0luZDRKZlZPbjhYVzIwSElYYyJ9.eyJuYmYiOjE3NDYzNzY0ODYsImV4cCI6MTc0NjQ2Mjg4NiwiaXNzIjoiaHR0cDovL21zLXNlY3VyaXR5OjUwMDAiLCJhdWQiOiJodHRwOi8vbXMtc2VjdXJpdHk6NTAwMC9yZXNvdXJjZXMiLCJjbGllbnRfaWQiOiJTaWlnb0FQSSIsInN1YiI6IjUzNjc2OCIsImF1dGhfdGltZSI6MTc0NjM3NjQ4NSwiaWRwIjoibG9jYWwiLCJuYW1lIjoiY29udGFiaWxpZGFkQGZpdG5lc3NwZW9wbGVjbWQuY29tIiwibWFpbF9zaWlnbyI6ImNvbnRhYmlsaWRhZEBmaXRuZXNzcGVvcGxlY21kLmNvbSIsImNsb3VkX3RlbmFudF9jb21wYW55X2tleSI6IkZJVE5FU1NQRU9QTEVDRU5UUk9NRURJQ09ERVBPUlRJVk9TQVMiLCJ1c2Vyc19pZCI6IjMwODc2IiwidGVuYW50X2lkIjoiMHgwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDE4NDA2NyIsInVzZXJfbGljZW5zZV90eXBlIjoiMCIsInBsYW5fdHlwZSI6IjE0IiwidGVuYW50X3N0YXRlIjoiMSIsIm11bHRpdGVuYW50X2lkIjoiMTIzIiwiY29tcGFuaWVzIjoiMCIsImFwaV9zdWJzY3JpcHRpb25fa2V5IjoiNTA1ZGIxZDRjYjk4NDhkOTlhMTVkYWI1NDAxZWEzOGMiLCJhcGlfdXNlcl9jcmVhdGVkX2F0IjoiMTY3NDE1MDQ0NiIsImFjY291bnRhbnQiOiJmYWxzZSIsImp0aSI6IjU3NjEwREIzNEI2MDJEMzUxM0QzRUMyRUFDODYzNTlDIiwiaWF0IjoxNzQ2Mzc2NDg2LCJzY29wZSI6WyJTaWlnb0FQSSJdLCJhbXIiOlsiY3VzdG9tIl19.GvAw2gdCy7ZF35v7KDHvCH3LkWZapnn6Nd_eBsuaux22v5_cyJIvJ3W0H7aE3JrKdXT4DBrudA-laWnynd0d7cxWHINw43dJ36oskgaybfsFXBemzgu7iCOoF3EeHzplRVZLSfWR6LzeU14yH93jd25w2z7ldC8Q_1j_aAkTdKwN5wb9yhOeAPNnyJwliXcNe2LVadVxQjbbAp_aLG0MImw7Tsq59d0n8HK1147aHxiRbCuWFaMIbzxAABTNJ6eH6xFKqTqnJctF8gdHDgd10AzKTSUBADihSYiTWvazsfWnSzxVxbpZ9C5XCzSVpeWlgYZLfRNNG-W461I8mZLmDA");

            using (var oSW = new StreamWriter(wRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(oCliente);
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

        protected void btnCrearCliente_Click(object sender, EventArgs e)
        {
            CrearCliente();
        }

        public class Cliente
        {
            public string person_type { get; set; }

            public string id_type { get; set; }

            public string identification { get; set; }

            public List<string> name { get; set; }

            public Address address { get; set; }

            public List<Phone> phones { get; set; }

            public List<Contact> contacts { get; set; }
        }

        public class Address
        {
            public string address { get; set; }

            public City city { get; set; }
        }

        public class City
        {
            public string country_code { get; set; }

            public string state_code { get; set; }

            public string city_code { get; set; }
        }

        public class Phone
        {
            public string number { get; set; }
        }

        public class Contact
        {
            public string first_name { get; set; }

            public string last_name { get; set; }

            public string email { get; set; }
        }
    }
}