using System;
using System.Collections.Generic;
using System.Linq;
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

		}

        [WebMethod]
        public static string GetCurrentTime()
        {
            // Retornar la hora actual del servidor en formato 'hh:mm:ss tt'
            return DateTime.Now.ToString("hh:mm:ss tt");
        }
    }
}