﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
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

        public async Task<string> RegisterCustomerAsync(string documento, string nombres, string apellidos, string celular, string correo)
        {
            // 1. Obtener token
            string token = await GetTokenAsync();

            // 2. Consultar tipo de documento en BD
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCodigoSiigoPorDocumento(documento);
            string codSiigo = dt.Rows[0]["CodSiigo"].ToString();
            dt.Dispose();

            // 3. Crear el objeto Customer
            Customer oCustomer = new Customer()
            {
                person_type = "Person",
                id_type = codSiigo,
                identification = documento,
                name = new List<string> { nombres, apellidos },
                email = correo,
                phones = new List<Phone> {
                    new Phone { number = celular }
                },
                contacts = new List<Contact> {
                    new Contact
                    {
                        first_name = nombres,
                        last_name = apellidos,
                        email = correo
                    }
                }
            };

            // 4. Crear cliente en Siigo
            string respuesta = await CreateCustomerAsync(oCustomer, token);

            return respuesta;
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

        public async Task ManageCustomerAsync(string documento, string nombres, string apellidos, string celular, string correo)
        {
            // 1. Obtener token
            string token = await GetTokenAsync();

            // 2. Consultar si el cliente ya existe
            bool exists = await CustomerExistsAsync(documento, token);

            // 3. Si no existe, crearlo
            if (!exists)
            {
                await RegisterCustomerAsync(documento, nombres, apellidos, celular, correo);
            }
        }

        public async Task<bool> CustomerExistsAsync(string documento, string token)
        {
            var url = $"{_baseUrl}v1/customers?identification={documento}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("Partner-Id", _partnerId);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error al consultar cliente: {await response.Content.ReadAsStringAsync()}");

            dynamic obj = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
            return obj.pagination.total_results > 0;
        }

        public async Task<string> RegisterInvoiceAsync(string cedula, string codSiigoPlan, string nombrePlan, int precioPlan, int idSede)
        {
            // 1. Consultar información de integración en la BD
            clasesglobales cg = new clasesglobales();
            DataTable dtIntegracion = cg.ConsultarIntegracion(idSede);
            int idTipoDocumento = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? Convert.ToInt32(dtIntegracion.Rows[0]["idTipoDocumento"].ToString()) : 66444;
            int costCenterDefault = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? Convert.ToInt32(dtIntegracion.Rows[0]["costCenterDefault"].ToString()) : 13053;
            int idVendedor = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? Convert.ToInt32(dtIntegracion.Rows[0]["idVendedor"].ToString()) : 51883;
            int idPayment = dtIntegracion != null && dtIntegracion.Rows.Count > 0 ? Convert.ToInt32(dtIntegracion.Rows[0]["idPayment"].ToString()) : 59576;
            dtIntegracion.Dispose();

            // Más Datos - Pruebas
            //int idTipoDocumento = 28006;
            //int costCenterDefault = 621;
            //int idVendedor = 856;
            //int idPayment = 9438;

            // 2. Obtener token
            string token = await GetTokenAsync();

            string fechaActual = DateTime.Now.ToString("yyyy-MM-dd");

            // 3. Crear el objeto Invoice
            Invoice oInvoice = new Invoice()
            {
                document = new DocumentType { id = idTipoDocumento },
                date = fechaActual,
                customer = new Customer { identification = cedula },
                seller = idVendedor,
                items = new List<Items>
                {
                    new Items
                    {
                        code = codSiigoPlan,
                        description = nombrePlan,
                        quantity = 1,
                        price = precioPlan,
                        cost_center = new CostCenter { id = costCenterDefault }
                    }
                },
                stamp = new Stamp { send = true },
                mail = new Mail { send = true },
                payments = new List<Payments>
                {
                    new Payments
                    {
                        id = idPayment,
                        value = precioPlan
                    }
                }
            };

            // 4. Crear factura en Siigo
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

        // Clase para la estructura del cliente
        public class Customer
        {
            public string person_type { get; set; }
            public string id_type { get; set; }
            public string identification { get; set; }
            public List<string> name { get; set; }
            public string email { get; set; }
            public List<Phone> phones { get; set; }
            public List<Contact> contacts { get; set; }
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
            public int seller { get; set; }
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
            public CostCenter cost_center { get; set; }
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