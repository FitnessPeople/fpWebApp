using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;

namespace fpWebApp
{
    public partial class estacionalidad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Estacionalidad");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        //CargarSedes();
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        DateTime dtHoy = DateTime.Now;
                    }
                    if (ViewState["Borrar"].ToString() == "1")
                    {
                        //btnEliminar.Visible = true;
                    }
                    //indicadores01.Visible = false;
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

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static int GuardarEvento(string title, string start, bool allDay, string bgcolor)
        {
            string dtInicio = Convert.ToDateTime(start).ToString("yyyy-MM-dd");
            string dtFin = dtInicio;
            string strQuery = "INSERT INTO estacionalidad (Titulo, FechaInicio, FechaFin, TodoElDia, Color) " +
            "VALUES ('" + title + "', '" + dtInicio + "', '" + dtFin + "', " + allDay + ", '" + bgcolor + "')";
            clasesglobales cg = new clasesglobales();
            cg.TraerDatosStr(strQuery);

            strQuery = "SELECT LAST_INSERT_ID()";
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string EliminarEvento(string id)
        {
            string strQuery = "DELETE FROM estacionalidad WHERE idEstacionalidad = " + id;
            clasesglobales cg = new clasesglobales();
            cg.TraerDatosStr(strQuery);

            return "Ok";
        }

        [WebMethod]
        public static List<Feriado> ObtenerFeriados()
        {
            List<Feriado> lista = new List<Feriado>();

            //string strQuery = "SELECT * FROM festivos";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarDiasFestivos();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lista.Add(new Feriado
                {
                    fecha = Convert.ToDateTime(dt.Rows[i]["Fecha"]).ToString("yyyy-MM-dd"),
                    descripcion = dt.Rows[i]["Titulo"].ToString()
                });
            }

            return lista;
        }

        public class Feriado
        {
            public string fecha { get; set; }
            public string descripcion { get; set; }
        }

        //protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlSedes.SelectedItem.Value.ToString() != "")
        //    {
        //        ltSede.Text = ddlSedes.SelectedItem.Text.ToString();
        //        CargarAgenda();
        //    }
        //}
    }
}