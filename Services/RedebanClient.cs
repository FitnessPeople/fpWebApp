using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace fpWebApp.Services
{
    public class RedebanClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _url;
        private readonly string _baseUrlAccionSoap;
        private readonly string _codigoUnico;
        private readonly string _usuario;
        private readonly string _clave;

        public RedebanClient(HttpClient httpClient, string url, string baseUrlAccionSoap, string codigoUnico, string usuario, string clave)
        {
            _httpClient = httpClient;
            _url = url;
            _baseUrlAccionSoap = baseUrlAccionSoap;
            _codigoUnico = codigoUnico;
            _usuario = usuario;
            _clave = clave;
        }

        public async Task<string> ObtenerTokenAsync()
        {
            string accionSoap = $"{_baseUrlAccionSoap}Token";

            string soapXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                                   <soapenv:Header/>
                                   <soapenv:Body>
                                      <tem:Token>
                                         <tem:Cod_unico>{_codigoUnico}</tem:Cod_unico>
                                         <tem:usuario>{_usuario}</tem:usuario>
                                         <tem:clave>{_clave}</tem:clave>
                                      </tem:Token>
                                   </soapenv:Body>
                                </soapenv:Envelope>";

            try
            {
                using (var content = new StringContent(soapXml, Encoding.UTF8, "text/xml"))
                {
                    content.Headers.Add("SOAPAction", accionSoap);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    HttpResponseMessage response = await _httpClient.PostAsync(_url, content);
                    response.EnsureSuccessStatusCode();

                    string result = await response.Content.ReadAsStringAsync();

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(result);
                    XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
                    ns.AddNamespace("ns", _baseUrlAccionSoap);

                    XmlNode tokenNode = doc.SelectSingleNode("//ns:TokenResult", ns);
                    return tokenNode?.InnerText ?? "No se pudo leer el token";
                }
            }
            catch (Exception ex)
            {
                return $"Error al obtener token: {ex.Message}";
            }
        }

        public async Task<string> EnviarDatosCompraAsync(string idTransaccion, string token, int valor, string terminal)
        {
            string soapAction = $"{_baseUrlAccionSoap}DatosCompra";

            string sobreescribir = "S"; // Posibles Valores: N o S

            var builder = new CompraDataBuilder
            {
                Valor = valor,
                Propina = 0,
                IVA = 0,
                Factura = $"F{idTransaccion}",
                BaseDevolucionIVA = 0,
                CodigoCajero = "KIOSCO",
                ImpuestoConsumo = 0,
                MontoBaseIVA = 0,
                MontoBaseImpConsumo = 0,
                Recibo = $"R{idTransaccion}",
                Terminal = terminal,
            };

            string data = builder.GenerarCadena();

            string soapXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                                   <soapenv:Header/>
                                   <soapenv:Body>
                                      <tem:DatosCompra>
                                         <tem:Cod_Unico>{_codigoUnico}</tem:Cod_Unico>
                                         <tem:id_transaccion>{idTransaccion}</tem:id_transaccion>
                                         <tem:Data>{data}</tem:Data>
                                         <tem:token>{token}</tem:token>
                                         <tem:sobreescribir>{sobreescribir}</tem:sobreescribir>
                                      </tem:DatosCompra>
                                   </soapenv:Body>
                                </soapenv:Envelope>";

            return await EnviarSoapRequestAsync(soapAction, soapXml, "//ns:DatosCompraResult");
        }

        public async Task<string> ConsultarRespuestaAsync(string idTransaccion, string token)
        {
            string soapAction = $"{_baseUrlAccionSoap}Respuesta";

            string soapXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                                   <soapenv:Header/>
                                   <soapenv:Body>
                                      <tem:Respuesta>
                                         <tem:Cod_unico>{_codigoUnico}</tem:Cod_unico>
                                         <tem:id_transaccion>{idTransaccion}</tem:id_transaccion>
                                         <tem:token>{token}</tem:token>
                                      </tem:Respuesta>
                                   </soapenv:Body>
                                </soapenv:Envelope>";

            return await EnviarSoapRequestAsync(soapAction, soapXml, "//ns:RespuestaResult");
        }

        public async Task<string> BorrarTransaccionAsync(string idTransaccion, string token)
        {
            string soapAction = $"{_baseUrlAccionSoap}BorrarTransaccion";

            string soapXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
                                   <soapenv:Header/>
                                   <soapenv:Body>
                                      <tem:BorrarTransaccion>
                                         <tem:Cod_unico>{_codigoUnico}</tem:Cod_unico>
                                         <tem:id_transaccion>{idTransaccion}</tem:id_transaccion>
                                         <tem:token>{token}</tem:token>
                                      </tem:BorrarTransaccion>
                                   </soapenv:Body>
                                </soapenv:Envelope>";

            return await EnviarSoapRequestAsync(soapAction, soapXml, "//ns:BorrarTransaccionResult");
        }

        private async Task<string> EnviarSoapRequestAsync(string soapAction, string soapXml, string xpathResult)
        {
            try
            {
                using (var content = new StringContent(soapXml, Encoding.UTF8, "text/xml"))
                {
                    content.Headers.Add("SOAPAction", soapAction);

                    HttpResponseMessage response = await _httpClient.PostAsync(_url, content);
                    response.EnsureSuccessStatusCode();

                    string result = await response.Content.ReadAsStringAsync();

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(result);
                    XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
                    ns.AddNamespace("ns", "http://tempuri.org/");

                    XmlNode node = doc.SelectSingleNode(xpathResult, ns);
                    return node?.InnerText ?? "No se recibió respuesta.";
                }
            }
            catch (Exception ex)
            {
                return $"Error en SOAP: {ex.Message}";
            }
        }

        public class CompraDataBuilder
        {
            public int Valor { get; set; }
            public int Propina { get; set; }
            public int IVA { get; set; }
            public string Factura { get; set; }
            public int BaseDevolucionIVA { get; set; }
            public string CodigoCajero { get; set; }
            public int ImpuestoConsumo { get; set; }
            public int MontoBaseIVA { get; set; }
            public int MontoBaseImpConsumo { get; set; }
            public string Recibo { get; set; }
            public string Terminal { get; set; }
            public int Vigencia { get; set; } = 3;
            public string Persistente { get; set; } = "N";

            public string GenerarCadena()
            {
                return string.Join(",", new string[]
                {
                    Valor.ToString(),
                    Propina.ToString(),
                    IVA.ToString(),
                    Factura,
                    BaseDevolucionIVA.ToString(),
                    CodigoCajero,
                    ImpuestoConsumo.ToString(),
                    MontoBaseIVA.ToString(),
                    MontoBaseImpConsumo.ToString(),
                    Recibo,
                    Terminal,
                    Vigencia.ToString(),
                    Persistente
                });
            }
        }
    }
}