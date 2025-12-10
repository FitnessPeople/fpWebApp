using System;
using System.Data;
using System.Web.UI;

namespace fpWebApp
{
    public partial class confirmarcodigo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblAnho.Text = DateTime.Now.Year.ToString();
                if (Request.QueryString != null)
                {
                    ltCodigo.Text = Request.QueryString["ticket"].ToString();
                }
            }
        }

        protected void btnIngresarCodigo_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            //DataTable dt = new DataTable();
            //DataTable dt = cg.RevisarCodigo(Convert.ToInt32(Session["idUsuario"].ToString()), txbCodigo.Text.ToString());

            if (Session["codigo"].ToString() == txbCodigo.Text.ToString())
            {
                // Ingresa a FP+
                int longitudCodigo = 6;
                string codigo = cg.GenerarCodigo(longitudCodigo);
                cg.ActualizarCodigoUsuario(Convert.ToInt32(Session["idUsuario"].ToString()), codigo);
                cg.InsertarLog(Session["idusuario"].ToString(), "usuarios", "Login", "El usuario inicio sesión.", "", "");
                Response.Redirect("micuenta");
            }
            else
            {
                MostrarAlerta("Código errado.", "Intente nuevamente.", "error");
            }
        }

        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            // tipo puede ser: 'success', 'error', 'warning', 'info', 'question'
            string script = $@"
            Swal.fire({{
                title: '{titulo}',
                text: '{mensaje}',
                icon: '{tipo}', 
                showCloseButton: true, 
                confirmButtonText: 'Aceptar', 
            }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
        }
    }
}