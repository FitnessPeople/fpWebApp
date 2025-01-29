using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class obteneraccesstokensiigo : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            // Generar Token Siigo
            // Metodo POST
            // Direccion: https://api.siigo.com/auth

            //var client = new HttpClient();
            //client.BaseAddress = new Uri("https://api.siigo.com/auth");

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var request = new HttpRequestMessage(HttpMethod.Post, client.BaseAddress);

            ////request.Headers.TryAddWithoutValidation("Content-Type", "application/json");

            //var content = new StringContent("{\r\n    \"username\": \"contabilidad@fitnesspeoplecmd.com\",\r\n    \"access_key\": \"YjU2NWE3YjktYjlhZS00OTRkLWE3NDgtODc0MGUyYjhmYzNlOjh9QDZyKDdwPkE=\"\r\n}", null, "application/json");
            //request.Content = content;
            //var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());



            //var baseAddress = new Uri("https://api.siigo.com/auth");

            //using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            //{
            //    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Partner-Id", "test");
            //    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

            //    using (var content = new StringContent("{ \"username\": \"contabilidad@fitnesspeoplecmd.com\", \"access_key\": \"YjU2NWE3YjktYjlhZS00OTRkLWE3NDgtODc0MGUyYjhmYzNlOjh9QDZyKDdwPkE=\" }", System.Text.Encoding.Default, "application/json"))
            //    {
            //        using (var respuesta = await httpClient.PostAsync("auth", content))
            //        {
            //            //string responseData = await respuesta.Content.ReadAsStringAsync();
            //            Response.Write(await respuesta.Content.ReadAsStringAsync() + "\r\n\r\n");
            //        }
            //    }
            //}

            //Consultar un cliente por Cédula en SIIGO
            var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Get, "https://api.siigo.com/v1/customers?identification=91491754");
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.siigo.com/v1/invoices");
            request.Headers.Add("Authorization", "eyJhbGciOiJSUzI1NiIsImtpZCI6IjExNDQzRDg2OUYxMzgwODlEREUwOTdENTNBN0YxNzVCNkQwNzIxNzdSUzI1NiIsInR5cCI6ImF0K2p3dCIsIng1dCI6IkVVUTlocDhUZ0luZDRKZlZPbjhYVzIwSElYYyJ9.eyJuYmYiOjE3Mjg0MTk1MzYsImV4cCI6MTczMTAxMTUzNiwiaXNzIjoiaHR0cDovL21zLXNlY3VyaXR5OjUwMDAiLCJhdWQiOiJodHRwOi8vbXMtc2VjdXJpdHk6NTAwMC9yZXNvdXJjZXMiLCJjbGllbnRfaWQiOiJTaWlnb0FQSSIsInN1YiI6IjUzNjc2OCIsImF1dGhfdGltZSI6MTcyODQxOTUzNiwiaWRwIjoibG9jYWwiLCJuYW1lIjoiY29udGFiaWxpZGFkQGZpdG5lc3NwZW9wbGVjbWQuY29tIiwibWFpbF9zaWlnbyI6ImNvbnRhYmlsaWRhZEBmaXRuZXNzcGVvcGxlY21kLmNvbSIsImNsb3VkX3RlbmFudF9jb21wYW55X2tleSI6IkZJVE5FU1NQRU9QTEVDRU5UUk9NRURJQ09ERVBPUlRJVk9TQVMiLCJ1c2Vyc19pZCI6IjMwODc2IiwidGVuYW50X2lkIjoiMHgwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDE4NDA2NyIsInVzZXJfbGljZW5zZV90eXBlIjoiMCIsInBsYW5fdHlwZSI6IjE0IiwidGVuYW50X3N0YXRlIjoiMSIsIm11bHRpdGVuYW50X2lkIjoiMTIzIiwiY29tcGFuaWVzIjoiMCIsImFwaV9zdWJzY3JpcHRpb25fa2V5IjoiNTA1ZGIxZDRjYjk4NDhkOTlhMTVkYWI1NDAxZWEzOGMiLCJhcGlfdXNlcl9jcmVhdGVkX2F0IjoiMTY3NDE1MDQ0NiIsImFjY291bnRhbnQiOiJmYWxzZSIsImp0aSI6IjIyN0U2MUU1RTQ0OUYwMkE2NzI3NEJCQkZCNzYxM0NBIiwiaWF0IjoxNzI4NDE5NTM2LCJzY29wZSI6WyJTaWlnb0FQSSJdLCJhbXIiOlsiY3VzdG9tIl19.UYUZ-JZXXan-Aj0bMpCwr6GcP-_W39wd5jQ_-spfiwFpoueRcOKu4_gdZfCb5hZC9zmEYewK8_48S6AQdOwLW8Pfy-zAXlO0MO1XzPS8uaGT4G1ZToty2yi8-tMzxRaLnQpGPjxQNqp6nD9qOuqA8XstDmoTl63lTa1KhOIGaODNjVWxpMWf4XYX82DwMok6RIu6Ji-aNcUIx1FlhL8lyxIKd8w-OxVrg_HfYmO5G-5eY1NkxFxn3Ll4m-0vvg_-AnPQBhPWoKINum8JwQOCIaYKO9AB3JBbOnDU2GvNV5MsDISBj8tnGoMKenJX2R4PAgmfdUc-VFw99cpgfgiGvQ");
            request.Headers.Add("Partner-Id", "test");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Response.Write(await response.Content.ReadAsStringAsync() + "\r\n\r\n");
        }
    }
}