using NPOI.OpenXmlFormats.Spreadsheet;
using System;
using System.Data;

namespace fpWebApp
{
    public partial class cancelardebito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Ingresos");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        //No tiene acceso a esta página
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    else
                    {
                        //Si tiene acceso a esta página
                        if (ViewState["Borrar"].ToString() == "1")
                        {
                            CargarDebito();
                        }
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

        private void CargarDebito()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPagosPorId(Convert.ToInt32(Request.QueryString["idAfiliadoPlan"].ToString()));

        }

        protected void btnCancelarDebito_Click(object sender, EventArgs e)
        {
            if (txbObservaciones.Text.ToString() != "")
            {
                string strQuery = "UPDATE afiliadosplanes " +
                    "SET EstadoPlan = 'Archivado', " +
                    "ObservacionesPlan = ObservacionesPlan + ' ' " + txbObservaciones.Text.ToString() + " " +
                    "WHERE IdAfiliadoPlan = " + Request.QueryString["idAfiliadoPlan"].ToString();
                clasesglobales cg = new clasesglobales();
                string rta = cg.TraerDatosStr(strQuery);

                Response.Redirect("reportepagos");
            }
        }
    }
}