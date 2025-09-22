using System.Text;
using System.Web;
using System.Web.Security;

namespace WebPage.Services
{
    public class UrlEncryptor
    {
        private static string Purpose = "WompiPlan";

        public static string Encrypt(string payload)
        {
            byte[] plain = Encoding.UTF8.GetBytes(payload);
            byte[] protectedBytes = MachineKey.Protect(plain, Purpose);
            return HttpServerUtility.UrlTokenEncode(protectedBytes); // url-safe
        }

        public static bool TryDecrypt(string token, out string payload)
        {
            payload = null;
            try
            {
                byte[] protectedBytes = HttpServerUtility.UrlTokenDecode(token);
                if (protectedBytes == null) return false;
                byte[] plain = MachineKey.Unprotect(protectedBytes, Purpose);
                if (plain == null) return false;
                payload = Encoding.UTF8.GetString(plain);
                return true;
            }
            catch { return false; }
        }
    }
}