using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fpWebApp
{
    public class pagoswompidet
    {
        public string Id { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaFinalizacion { get; set; }
        public string Valor { get; set; }
        public string Moneda { get; set; }
        public string MetodoPago { get; set; }
        public string Estado { get; set; }
        public string Referencia { get; set; }
        public string NombreTarjeta { get; set; }
        public string UltimosDigitos { get; set; }
        public string MarcaTarjeta { get; set; }
        public string TipoTarjeta { get; set; }
        public string NombreComercio { get; set; }
        public string ContactoComercio { get; set; }
        public string TelefonoComercio { get; set; }
        public string URLRedireccion { get; set; }
        public string PaymentLinkId { get; set; }
        public string PublicKeyComercio { get; set; }
        public string EmailComercio { get; set; } 
        public string Estado3DS { get; set; }
    }
}