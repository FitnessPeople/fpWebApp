using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class tableroasesorcrm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            litTitulo.Text = "<span class='badge badge-primary'>Próximo Contacto</span>";
            Literal1.Text = "<span class='badge badge-warning'>Propuesta en gestión</span>";
            Literal2.Text = "<span class='badge badge-success'>Negociación aceptada</span>";
        }
    }
}