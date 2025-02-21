using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class ejemplosummernote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                // Cargar contenido guardado en el editor si existe
                hiddenEditor.Value = HttpUtility.HtmlDecode(hiddenEditor.Value);
            }
        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            // Guardar el contenido del editor en el campo oculto
            hiddenEditor.Value = HttpUtility.HtmlEncode(Request.Unvalidated[hiddenEditor.ClientID]);
        }
    }
}