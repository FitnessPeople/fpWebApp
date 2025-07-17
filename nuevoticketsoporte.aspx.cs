using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace fpWebApp
{
    public partial class nuevoticketsoporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Usuarios");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        CargarEquipos();
                    }
                    else
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("logout");
                }
            }
        }

        private void ValidarPermisos(string strPagina)
        {
            ViewState["SinPermiso"] = "1";
            ViewState["Consulta"] = "0";
            ViewState["Exportar"] = "0";
            ViewState["CrearModificar"] = "0";
            ViewState["Borrar"] = "0";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ValidarPermisos(strPagina, Session["idPerfil"].ToString(), Session["idusuario"].ToString());

            if (dt.Rows.Count > 0)
            {
                ViewState["SinPermiso"] = dt.Rows[0]["SinPermiso"].ToString();
                ViewState["Consulta"] = dt.Rows[0]["Consulta"].ToString();
                ViewState["Exportar"] = dt.Rows[0]["Exportar"].ToString();
                ViewState["CrearModificar"] = dt.Rows[0]["CrearModificar"].ToString();
                ViewState["Borrar"] = dt.Rows[0]["Borrar"].ToString();
            }

            dt.Dispose();
        }

        private void CargarEquipos()
        {
            clasesglobales cg = new clasesglobales();
            string strQuery = "SELECT * FROM ActivosFijos";

            DataTable dt = cg.TraerDatos(strQuery);

            ddlActivosFijos.DataSource = dt;
            ddlActivosFijos.DataValueField = "idActivoFijo";
            ddlActivosFijos.DataTextField = "NombreActivoFijo";
            ddlActivosFijos.DataBind();

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string descripcion = txtDescripcion.Text;
            int idActivoFijo = int.Parse(ddlActivosFijos.SelectedValue);
            string prioridad = ddlPrioridad.SelectedValue;
            string idUsuario = Session["idUsuario"].ToString(); // Aquí usarías el ID del usuario logueado
            string strQuery = "INSERT INTO TicketSoporte " +
                "(idActivoFijo, idReportadoPor, FechaCreacionTicket, DescripcionTicket, PrioridadTicket) " +
                "VALUES (" + idActivoFijo + ", " + idUsuario + ", NOW(), '" + descripcion + "', '" + prioridad + "')";

            clasesglobales cg = new clasesglobales();
            cg.TraerDatosStr(strQuery);

            ltMensaje.Text = "<div class=\"ibox-content\">" +
                    "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Ticket creado correctamente." +
                    "</div></div>";
        }
    }
}