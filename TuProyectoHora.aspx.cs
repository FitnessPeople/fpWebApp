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
	public partial class TuProyectoHora : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            string strPrivateKeyProduction = "prv_prod_h7JHlOIL6EjCzotPnupYSbzy16ulQ5DO";
        }

        public static string GetPost(string person_type, string id_type, string identification, string name, string acceptance_token, string accept_personal_auth)
        {
            Cliente oCliente = new Cliente() { person_type = "" + person_type + "", id_type = "" + id_type + "", identification = "" + identification + "", name = "" + name + "", accept_personal_auth = "" + accept_personal_auth + "" };

            string result = "";
            WebRequest wRequest = WebRequest.Create(url);
            wRequest.Method = "post";
            wRequest.ContentType = "application/json;charset=UTF-8";
            wRequest.Headers.Add("Authorization", "Bearer prv_test_GWPWL8e9md24zYyTuF5KojJmH7Y4Sez2");

            using (var oSW = new StreamWriter(wRequest.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(oFuentePago);
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