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
        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            string contenidoEditor = hiddenEditor.Value;
            litPreviewEditor.Text = $"<div style='border:1px solid #ddd; padding:10px;'>{contenidoEditor}</div>";
        }
    }
}