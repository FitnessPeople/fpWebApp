using fpWebApp.Services;
using System;
using System.Collections.Generic;

namespace fpWebApp.controles
{
    public partial class header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuariosOnline();
            }
        }

        private void CargarUsuariosOnline()
        {
            var lista = Application["ListaUsuarios"] as List<UsuarioOnline>;
            ltNroUsuarios.Text = lista != null ? lista.Count.ToString() : "0";

            var listaUsuarios = (List<UsuarioOnline>)Application["ListaUsuarios"];
            rpUsuariosEnLinea.DataSource = listaUsuarios;
            rpUsuariosEnLinea.DataBind();
        }

        protected void tmRefrescarUsuarios_Tick(object sender, EventArgs e)
        {
            CargarUsuariosOnline();
        }
    }
}