using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace WebPage.Services
{
    public class UrlEncryptor
    {
        // Cambia si quieres separar propósitos para distintos tokens dentro de la app
        private static readonly string Purpose = "WompiPay_v1";

        // Serializador simple (incluido en .NET Framework)
        private static readonly JavaScriptSerializer Js = new JavaScriptSerializer();

        // ----- ENCRYPT -----
        // payload: cadena tipo query "k1=v1&k2=v2" (puedes usar HttpUtility.UrlEncode en valores)
        // ttl: tiempo de vida opcional; si es null => sin expiración
        public static string Encrypt(string payload, TimeSpan? ttl = null)
        {
            var wrapper = new Dictionary<string, object>
            {
                ["v"] = 1, // versión (por si en el futuro cambias formato)
                ["d"] = payload
            };

            if (ttl.HasValue)
            {
                // guardamos la expiración en UTC ticks
                wrapper["exp"] = DateTime.UtcNow.Add(ttl.Value).Ticks;
            }

            string json = Js.Serialize(wrapper);
            byte[] plain = Encoding.UTF8.GetBytes(json);
            byte[] protectedBytes = MachineKey.Protect(plain, Purpose);
            return HttpServerUtility.UrlTokenEncode(protectedBytes); // url-safe
        }

        // Helper: convertir NameValueCollection a query-string (sin ?)
        public static string ToQueryString(NameValueCollection nvc)
        {
            var list = new List<string>();
            foreach (string key in nvc)
            {
                string[] values = nvc.GetValues(key);
                if (values == null) continue;
                foreach (var v in values)
                {
                    // NO hagas doble encode aquí si ya encodificaste valores manualmente.
                    list.Add($"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(v)}");
                }
            }
            return string.Join("&", list);
        }

        // Overload: crear token desde NameValueCollection
        public static string Encrypt(NameValueCollection nvc, TimeSpan? ttl = null)
        {
            return Encrypt(ToQueryString(nvc), ttl);
        }

        // ----- TRY DECRYPT -----
        // Devuelve: true si ok (no modificado, propósito correcto y no expirado)
        // out payload: la cadena original "k1=v1&k2=v2"
        // out expiresUtc: fecha de expiración si existía (UTC), null si no tenía TTL
        public static bool TryDecrypt(string token, out string payload, out DateTime? expiresUtc)
        {
            payload = null;
            expiresUtc = null;

            try
            {
                byte[] protectedBytes = HttpServerUtility.UrlTokenDecode(token);
                if (protectedBytes == null) return false;

                byte[] plain = MachineKey.Unprotect(protectedBytes, Purpose);
                if (plain == null || plain.Length == 0) return false;

                string json = Encoding.UTF8.GetString(plain);
                var dict = Js.Deserialize<Dictionary<string, object>>(json);

                if (dict == null || !dict.ContainsKey("d")) return false;

                payload = Convert.ToString(dict["d"]);

                if (dict.ContainsKey("exp") && dict["exp"] != null)
                {
                    // Js devuelve números como double/int32/long dependiendo; normalizamos
                    long ticks;
                    try
                    {
                        // intentar varias conversiones
                        if (dict["exp"] is long) ticks = (long)dict["exp"];
                        else if (dict["exp"] is int) ticks = (int)dict["exp"];
                        else ticks = Convert.ToInt64(dict["exp"]);
                    }
                    catch
                    {
                        return false;
                    }

                    expiresUtc = new DateTime(ticks, DateTimeKind.Utc);
                    if (DateTime.UtcNow > expiresUtc.Value)
                    {
                        // token expirado
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                return false; // on any error, treat as invalid
            }
        }

        // Overload: desencriptar y devolver NameValueCollection
        public static bool TryDecryptToCollection(string token, out NameValueCollection nvc, out DateTime? expiresUtc)
        {
            nvc = null;
            expiresUtc = null;

            if (!TryDecrypt(token, out string payload, out expiresUtc)) return false;

            // payload formato "k1=v1&k2=v2"
            nvc = HttpUtility.ParseQueryString(payload);
            return true;
        }
    }
}