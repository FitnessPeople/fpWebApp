using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
	public partial class estudiafit : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Estudiafit");
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
                        divBotonesLista.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            CargarGraficaBarrasPorSede();
                            //btnAgregar.Visible = true;
                        }
                    }

                    rpInscritos.ItemDataBound += rpInscritos_ItemDataBound;
                    listaInscritos();
                }
                else
                {
                    Response.Redirect("logout.aspx");
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

        private void listaInscritos()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarGymPass();
            rpInscritos.DataSource = dt;
            rpInscritos.DataBind();
            dt.Dispose();
        }

        protected void rpInscritos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // 1. Obtén el documento del usuario en esta fila
                string nroDocumento = DataBinder.Eval(e.Item.DataItem, "NroDocumento").ToString();

                // 2. Consulta si ya tiene Agenda GymPass
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarGymPassAgendaPorDocumento(nroDocumento);

                // 3. Accede al control dentro de la fila
                HtmlAnchor btnAgendar = (HtmlAnchor)e.Item.FindControl("btnAgendar");
                HtmlAnchor btnEliminarAgenda = (HtmlAnchor)e.Item.FindControl("btnEliminarAgenda");

                // 4. Deshabilita u oculta el botón si ya tiene Agenda GymPass
                if (dt.Rows.Count > 0 && dt.Rows[0]["idAgenda"] != DBNull.Value)
                {
                    btnAgendar.Visible = false;
                    btnEliminarAgenda.Visible = true;
                    //btnAgendar.Attributes.Add("class", "btn btn-outline btn-warning pull-right m-r-xs");
                    //btnAgendar.Attributes.Add("style", "padding: 1px 2px 1px 2px; margin-bottom: 0px; pointer-events:none;");
                    //btnAgendar.InnerHtml = "<i class='fa-solid fa-calendar-check'></i>";
                }

                dt.Dispose();

                Literal litEstado = (Literal)e.Item.FindControl("litEstado");
                string estado = DataBinder.Eval(e.Item.DataItem, "Estado").ToString();
                string estadoHtml = "";

                switch (estado)
                {
                    case "Agendado":
                        estadoHtml = "<p style='margin-bottom: 0;'><span style='font-size: 1.2rem; color: #fff; font-weight: bold;' class='label label-primary'>Agendado</span></p>";
                        break;
                    case "Asistió":
                        estadoHtml = "<p style='margin-bottom: 0;'><span style='font-size: 1.2rem; color: #fff; font-weight: bold;' class='label label-success'>Asistió</span></p>";
                        break;
                    case "No Asistió":
                        estadoHtml = "<p style='margin-bottom: 0;'><span style='font-size: 1.2rem; color: #fff; font-weight: bold;' class='label label-danger'>No Asistió</span></p>";
                        break;
                    case "Cancelado":
                        estadoHtml = "<p style='margin-bottom: 0;'><span style='font-size: 1.2rem; color: #fff; font-weight: bold;' class='label label-warning'>Cancelado</span></p>";
                        break;
                    default:
                        estadoHtml = $"<p style='margin-bottom: 0;'><span style='font-size: 1.2rem; color: #fff; font-weight: bold;' class='label label-danger'>{estado}</span></p>";
                        break;
                }

                litEstado.Text = estadoHtml;
            }
        }

        private void CargarGraficaBarrasPorSede()
        {
            string query = @"SELECT CONCAT(s.NombreSede, ' - ', cs.NombreCiudadSede) AS SedeCiudad, gpa.Estado, COUNT(*) AS Cantidad
                             FROM GymPass gp
                             INNER JOIN Sedes s ON gp.idSede = s.idSede
                             INNER JOIN GymPassAgenda gpa ON gpa.idGymPass = gp.idGymPass 
                             LEFT JOIN CiudadesSedes cs ON s.idCiudadSede = cs.idCiudadSede
                             GROUP BY SedeCiudad, gpa.Estado";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(query);

            var sedes = dt.AsEnumerable().Select(r => r.Field<string>("SedeCiudad")).Distinct().ToList();

            // Inicializamos estructura
            var estados = new[] { "Agendado", "Asistió", "No Asistió", "Cancelado" };
            Dictionary<string, List<int>> estadoDatos = estados.ToDictionary(e => e, e => new List<int>());

            foreach (var sede in sedes)
            {
                foreach (var estado in estados)
                {
                    var cantidad = dt.AsEnumerable()
                        .Where(r => r.Field<string>("SedeCiudad") == sede && r.Field<string>("Estado") == estado)
                        .Select(r => Convert.ToInt32(r["Cantidad"]))
                        .FirstOrDefault();

                    estadoDatos[estado].Add(cantidad);
                }
            }

            // Cálculo del total por sede
            List<int> totalPorSede = estadoDatos.First().Value
                .Select((_, i) => estadoDatos.Sum(kvp => kvp.Value[i]))
                .ToList();

            string totalSerie = $"['Total', {string.Join(",", totalPorSede)}]";

            // Armar arrays para JS
            string columnasJS = string.Join(",", estadoDatos.Select(kvp =>
                $"['{kvp.Key}', {string.Join(",", kvp.Value)}]"
            ));

            string categoriasJS = string.Join(",", sedes.Select(s => $"\"{s}\""));

            //ClientScript.RegisterStartupScript(this.GetType(), "graficaBarrasSedes", script, true);
            ClientScript.RegisterStartupScript(this.GetType(), "setVars",
                $"var columnasJS = [{columnasJS}]; var categoriasJS = [{categoriasJS}];", true);
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string consultaSQL = @"SELECT CONCAT(TRIM(Nombres), ' ', TRIM(Apellidos)) AS 'Nombre',
                                       g.Email AS 'Correo', g.Celular AS 'Celular', g.NroDocumento AS 'Nro. de Documento', 
                                       c.NombreCiudadSede AS 'Ciudad', s.NombreSede AS 'Sede',
                                       g.FechaAsistencia AS 'Fecha Asistencia', g.FechaInscripcion AS 'Fecha Inscripción', 
                                       gpa.FechaHora AS 'Fecha Agendada', gpa.Estado AS 'Estado' 
                                       FROM GymPass g
                                       INNER JOIN sedes s 
                                       ON s.IdSede = g.idSede 
                                       INNER JOIN ciudadessedes c 
                                       ON c.idCiudadSede = s.idCiudadSede 
                                       LEFT JOIN GymPassAgenda gpa 
                                       ON gpa.idGymPass = g.idGymPass 
                                       ORDER BY FechaInscripcion DESC;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(consultaSQL);
                string nombreArchivo = $"Inscritos_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    cg.ExportarExcel(dt, nombreArchivo);
                }
                else
                {
                    Response.Write("<script>alert('No existen registros para esta consulta');</script>");
                    //Response.Redirect("gympass.aspx?mensaje=No existen registros para esta consulta");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al exportar: " + ex.Message + "');</script>");
                //Response.Redirect("gympass.aspx?mensaje=" + Server.UrlEncode(ex.Message));
            }
        }
    }
}