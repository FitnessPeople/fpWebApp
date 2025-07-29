using System;
using System.Data;
using System.Data.SqlClient;

namespace fpWebApp
{
    public partial class cambiarpermiso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strQuery = "SELECT * FROM permisos_perfiles " +
                "WHERE idPerfil = " + Request.QueryString["idPer"].ToString() + " " +
                "AND idPagina = " + Request.QueryString["idPag"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                switch (Request.QueryString["perm"].ToString())
                {
                    case "1":
                        if (dt.Rows[0]["SinPermiso"].ToString() == "1")
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "SinPermiso = 0 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        else
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "SinPermiso = 1 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        break;
                    case "2":
                        if (dt.Rows[0]["Consulta"].ToString() == "1")
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Consulta = 0 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        else
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Consulta = 1 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        break;
                    case "3":
                        if (dt.Rows[0]["Exportar"].ToString() == "1")
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Exportar = 0 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        else
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Exportar = 1 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        break;
                    case "4":
                        if (dt.Rows[0]["CrearModificar"].ToString() == "1")
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "CrearModificar = 0 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        else
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "CrearModificar = 1 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        break;
                    case "5":
                        if (dt.Rows[0]["Borrar"].ToString() == "1")
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Borrar = 0 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        else
                        {
                            strQuery = "UPDATE permisos_perfiles SET " +
                                "Borrar = 1 " +
                                "WHERE idPerfil = " + dt.Rows[0]["idPerfil"].ToString() + " " +
                                "AND idPagina = " + dt.Rows[0]["idPagina"].ToString();
                        }
                        break;
                    default:
                        break;
                }
                
                try
                {
                    string mensaje = cg.TraerDatosStr(strQuery);
                }
                catch (SqlException ex)
                {
                    string mensaje = ex.Message;
                }
            }

            dt.Dispose();

            Response.Redirect("perfiles");
        }
    }
}