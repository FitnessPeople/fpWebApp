using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class eliminardisponibilidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Agenda");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        Response.Redirect("agenda");
                    }
                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
                        try
                        {
                            string strQuery = "DELETE FROM DisponibilidadEspecialistas " +
                                " WHERE idDisponibilidad = " + Request.QueryString["id"].ToString();
                            OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                            myConnection.Open();
                            command.ExecuteNonQuery();
                            command.Dispose();
                            myConnection.Close();
                        }
                        catch (OdbcException ex)
                        {
                            string mensaje = ex.Message;
                            myConnection.Close();
                        }
                    }
                    Response.Redirect("agenda");
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