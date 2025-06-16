using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class cancelaragendagympass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Agenda Gym Pass");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        Response.Redirect("agendagympass");
                    }

                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        try
                        {
                            string strQuery = "UPDATE GymPassAgenda " +
                                              "SET Estado = 'Cancelado' " +
                                              "WHERE idAgenda = " + Request.QueryString["id"].ToString();
                            clasesglobales cg = new clasesglobales();
                            string mensaje = cg.TraerDatosStr(strQuery);
                        }
                        catch (SqlException ex)
                        {
                            string mensaje = ex.Message;
                        }
                    }

                    Response.Redirect("agendagympass");
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
    }
}