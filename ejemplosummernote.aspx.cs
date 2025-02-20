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
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Recupera el contenido de los editores Summernote
            string contenido1 = Request.Form["descripcion1"];
            string contenido2 = Request.Form["descripcion2"];

            // Muestra el contenido en la página (puedes guardarlo en una BD)
            litContenido.Text = "<b>Artículo 1:</b><br>" + contenido1 + "<br><br>";
            litContenido.Text += "<b>Artículo 2:</b><br>" + contenido2;
        }
    }
}