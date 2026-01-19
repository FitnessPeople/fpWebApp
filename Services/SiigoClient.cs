using DocumentFormat.OpenXml.Bibliography;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace fpWebApp.Services
{
    public class SiigoClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _username;
        private readonly string _accessKey;
        private readonly string _partnerId;
        private static readonly object _siigoTokenLock = new object();

        public SiigoClient(HttpClient httpClient, string baseUrl, string username, string accessKey, string partnerId)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            _username = username;
            _accessKey = accessKey;
            _partnerId = partnerId;
        }

        public async Task<string> GetTokenAsync()
        {
            // Recuperar token y hora de expiración desde Application
            var token = HttpContext.Current.Application["SiigoToken"] as string;
            var expira = HttpContext.Current.Application["SiigoTokenExp"] as DateTime?;

            // Validar si el token existe y sigue vigente
            if (!string.IsNullOrEmpty(token) && expira.HasValue && expira.Value > DateTime.Now)
                return token;

            // Si no existe o ya expiró, solicitar uno nuevo
            var url = $"{_baseUrl}auth";
            var payload = new
            {
                username = _username,
                access_key = _accessKey
            };

            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            dynamic obj = JsonConvert.DeserializeObject(jsonResponse);

            token = obj.access_token;

            // Guardar token y nueva fecha de expiración (50 minutos)
            HttpContext.Current.Application["SiigoToken"] = token;
            HttpContext.Current.Application["SiigoTokenExp"] = DateTime.Now.AddMinutes(50);

            return token;
        }

        public async Task ManageCustomerAsync(string idTipoDocSiigo, string documento, string nombres, string apellidos, string direccion, string codEstado, string codCiudad, string celular, string correo)
        {
            // 1. Obtener token
            string token = await GetTokenAsync();

            // 2. Consultar existencia de afiliado
            (bool exists, string idCustomer) = await CustomerExistsAsync(documento, token);

            Customer oCustomer = BuildCustomer(idTipoDocSiigo, documento, nombres, apellidos, direccion, codEstado, codCiudad, celular, correo);

            // 3. Si no existe, crearlo
            if (!exists)
            {
                await CreateCustomerAsync(oCustomer, token);
            }
            else
            {
                await UpdateCustomerAsync(idCustomer, oCustomer, token);
            }
        }

        public async Task<string> ManageInvoiceAsync(string idSiigoFactura)
        {
            // 1. Obtener token
            string token = await GetTokenAsync();

            // 2. Consultar si la factura existe
            string public_url = await InvoiceExistsAsync(idSiigoFactura, token);

            return public_url;
        }

        public async Task<string> InvoiceExistsAsync(string idSiigoFactura, string token)
        {
            var url = $"{_baseUrl}v1/invoices/{idSiigoFactura}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("Partner-Id", _partnerId);

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error al consultar la factura: {content}");
            }

            dynamic obj = JsonConvert.DeserializeObject(content);

            if (obj?.public_url == null)
            {
                throw new Exception("La factura existe, pero no contiene public_url.");
            }

            return obj.public_url.ToString();
        }

        public async Task<(bool, string)> CustomerExistsAsync(string documento, string token)
        {
            var url = $"{_baseUrl}v1/customers?identification={documento}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("Partner-Id", _partnerId);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error al consultar cliente: {await response.Content.ReadAsStringAsync()}");

            dynamic obj = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
            return (
                obj.pagination.total_results > 0,
                obj.pagination.total_results > 0 ? obj.results[0].id.ToString() : null
            );
        }

        public async Task<string> UpdateCustomerAsync(string customerId, Customer oCustomer, string token)
        {
            var url = $"{_baseUrl}v1/customers/{customerId}";

            var json = JsonConvert.SerializeObject(oCustomer, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("Partner-Id", _partnerId);

            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error al actualizar cliente: {await response.Content.ReadAsStringAsync()}");

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> CreateCustomerAsync(Customer oCustomer, string token)
        {
            var url = $"{_baseUrl}v1/customers";
            var json = JsonConvert.SerializeObject(oCustomer, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("Partner-Id", _partnerId);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error al crear cliente: {await response.Content.ReadAsStringAsync()}");

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> RegisterInvoiceAsync(string cedula, string codSiigoPlan, string nombrePlan, int precioPlan, string observaciones, int idVendedor, int idTipoDocumento, string fechaActual, int idCentroCosto, int idPago)
        {
            // 1. Obtener token
            string token = await GetTokenAsync();

            // 2. Crear el objeto Invoice
            Invoice oInvoice = BuildInvoice(cedula, codSiigoPlan, nombrePlan, precioPlan, observaciones, idVendedor, idTipoDocumento, fechaActual, idCentroCosto, idPago);

            // 3. Crear factura en Siigo
            string respuesta = await CreateInvoiceAsync(oInvoice, token);

            var jsonRespuesta = JsonConvert.DeserializeObject<dynamic>(respuesta);
            return jsonRespuesta.id;
        }

        public async Task<string> CreateInvoiceAsync(Invoice oInvoice, string token)
        {
            var url = $"{_baseUrl}v1/invoices";
            var json = JsonConvert.SerializeObject(oInvoice, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("Partner-Id", _partnerId);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error al crear factura: {await response.Content.ReadAsStringAsync()}");

            return await response.Content.ReadAsStringAsync();
        }

        private Customer BuildCustomer(string idTipoDocSiigo, string documento, string nombres, string apellidos, string direccion, string codEstado, string codCiudad, string celular, string correo)
        {
            return new Customer
            {
                person_type = "Person",
                id_type = idTipoDocSiigo,
                identification = documento,
                name = new List<string> { nombres, apellidos },
                address = new Address
                {
                    address = direccion,
                    city = new City
                    {
                        country_code = "Co",
                        state_code = codEstado,
                        city_code = codCiudad
                    }
                },
                email = correo,
                phones = new List<Phone>
                {
                    new Phone { number = celular }
                },
                contacts = new List<Contact>
                {
                    new Contact
                    {
                        first_name = nombres,
                        last_name = apellidos,
                        email = correo
                    }
                }
            };
        }

        private Invoice BuildInvoice(string cedula, string codSiigoPlan, string nombrePlan, int precioPlan, string observaciones, int idVendedor, int idTipoDocumento, string fechaActual, int idCentroCosto, int idPago)
        {
            return new Invoice
            {
                document = new DocumentType { id = idTipoDocumento },
                date = fechaActual,
                customer = new Customer { identification = cedula },
                cost_center = new CostCenter { id = idCentroCosto },
                seller = idVendedor,
                observations = observaciones,
                items = new List<Items>
                {
                    new Items
                    {
                        code = codSiigoPlan,
                        description = nombrePlan,
                        quantity = 1,
                        price = precioPlan
                    }
                },
                stamp = new Stamp { send = true },
                mail = new Mail { send = true },
                payments = new List<Payments>
                {
                    new Payments
                    {
                        id = idPago,
                        value = precioPlan
                    }
                }
            };
        }

        // Clase para la estructura del cliente
        public class Customer
        {
            public string person_type { get; set; }
            public string id_type { get; set; }
            public string identification { get; set; }
            public List<string> name { get; set; }
            public Address address { get; set; }
            public string email { get; set; }
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

        // Clase para la estructura de la factura
        public class Invoice
        {
            public DocumentType document { get; set; }
            public string date { get; set; }
            public Customer customer { get; set; }
            public CostCenter cost_center { get; set; }
            public int seller { get; set; }
            public string observations { get; set; }
            public List<Items> items { get; set; }
            public Stamp stamp { get; set; }
            public Mail mail { get; set; }
            public List<Payments> payments { get; set; }
        }

        public class DocumentType
        {
            public int id { get; set; }
        }

        public class Items
        {
            public string code { get; set; }
            public string description { get; set; }
            public int quantity { get; set; }
            public int price { get; set; }
        }

        public class CostCenter
        {
            public int id { get; set; }
        }

        public class Stamp
        {
            public bool send { get; set; }
        }

        public class Mail
        {
            public bool send { get; set; }
        }

        public class Payments
        {
            public int id { get; set; }
            public int value { get; set; }
        }
    }
}