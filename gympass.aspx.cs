using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NPOI.OpenXmlFormats.Spreadsheet;

//using System.Web.UI.WebControls;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace fpWebApp
{
    public partial class gympass : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Gympass");
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
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            DateTime dtHoy = DateTime.Now;
                            txbFechaAgenda.Attributes.Add("type", "date");
                            txbFechaAgenda.Attributes.Add("min", dtHoy.Year.ToString() + "-" + String.Format("{0:MM}", dtHoy) + "-" + String.Format("{0:dd}", dtHoy));
                            DateTime dtHoy = DateTime.Now;
                            txbFechaAgenda.Attributes.Add("type", "date");
                            txbFechaAgenda.Attributes.Add("min", dtHoy.Year.ToString() + "-" + String.Format("{0:MM}", dtHoy) + "-" + String.Format("{0:dd}", dtHoy));

                            //btnAgregar.Visible = true;
                        }
                    }

                    rpInscritos.ItemDataBound += rpInscritos_ItemDataBound;
                    listaInscritos();
                    //ActualizarEstadoxFechaFinal();
                    //indicadores01.Visible = false;
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

        //protected void rpInscritos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        if (ViewState["CrearModificar"].ToString() == "1")
        //        {
        //            HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
        //            btnEditar.Attributes.Add("href", "editarempleado?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
        //            btnEditar.Visible = false;
        //        }
        //        if (ViewState["Borrar"].ToString() == "1")
        //        {
        //            HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
        //            btnEliminar.Attributes.Add("href", "eliminarempleado?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
        //            btnEliminar.Visible = false;
        //        }
        //    }
        //}

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

        protected void btnAgendarGymPass_Click(object sender, EventArgs e)
        {
            DateTime dtFechaAgenda = Convert.ToDateTime(txbFechaAgenda.Value.ToString() + " " + txbHoraAgenda.Value.ToString());

            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dtGymPass = cg.ConsultarGymPassPorDocumento(infoDoc.Value.ToString());
                DataTable dtAgenda = cg.ConsultarGymPassAgendaPorDocumento(infoDoc.Value.ToString());

                if (dtGymPass.Rows.Count > 0 && dtAgenda.Rows[0]["idAgenda"] == DBNull.Value)
                {
                    string id = dtGymPass.Rows[0]["idGymPass"].ToString();

                    string strQuery = @"INSERT INTO GymPassAgenda (idGymPass, FechaHora, Estado, idUsuarioCrea) " +
                                       "VALUES (" + id + ", '" + dtFechaAgenda.ToString("yyyy-MM-dd H:mm:ss") + "', 'Agendado', " + Session["idusuario"].ToString() + ")";

                    string mensaje = cg.TraerDatosStr(strQuery);
                }

                Response.Redirect("gympass");

                dtGymPass.Dispose();
                dtAgenda.Dispose();

                rpInscritos.ItemDataBound += rpInscritos_ItemDataBound;
                listaInscritos();
                LimpiarCamposAgenda();
            }
            catch (OdbcException ex)
            {
                string mensaje = ex.Message;
            }
        }

        private void LimpiarCamposAgenda()
        {
            txbFechaAgenda.Value = string.Empty;
            txbHoraAgenda.Value = "08:00";
        }

        protected void btnEliminarAgendaGymPass_Click(object sender, EventArgs eventArgs)
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dtAgenda = cg.ConsultarGymPassAgendaPorDocumento(infoDocEli.Value.ToString());

                if (dtAgenda.Rows.Count > 0)
                {
                    string id = dtAgenda.Rows[0]["idAgenda"].ToString();

                    string strQuery = @"DELETE FROM GymPassAgenda " +
                                       "WHERE idAgenda = " + int.Parse(id);

                    string mensaje = cg.TraerDatosStr(strQuery);
                }

                Response.Redirect("gympass");

                dtAgenda.Dispose();

                rpInscritos.ItemDataBound += rpInscritos_ItemDataBound;
                listaInscritos();

            }
            catch (OdbcException ex)
            {
                string mensaje = ex.Message;
            }
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
                string consultaSQL = @"SELECT CONCAT(TRIM(Nombres), ' ', TRIM(Apellidos)) AS 'Nombre',
                                       g.Email AS 'Correo', g.Celular AS 'Celular', g.NroDocumento AS 'Nro. de Documento', 
                                       c.NombreCiudadSede AS 'Ciudad', s.NombreSede AS 'Sede',
                                       g.FechaAsistencia AS 'Fecha Asistencia', g.FechaInscripcion AS 'Fecha Inscripción' 
                                       FROM GymPass g
                                       INNER JOIN sedes s 
                                       ON s.IdSede = g.idSede 
                                       INNER JOIN ciudadessedes c 
                                       ON c.idCiudadSede = s.idCiudadSede 
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

        protected void btnAgendarGymPass_Click(object sender, EventArgs e)
        {
            //txbFechaAgenda.Value
            string nroDocumento = infoDoc.Value;
            string fechaAgenda = txbFechaAgenda.Value;
            string horaAgenda = txbHoraAgenda.Value;

            string mensaje = "Guardado: " + nroDocumento + " - " + fechaAgenda + " - " + horaAgenda;
            ScriptManager.RegisterStartupScript(this, GetType(), "msg", $"console.log('{mensaje}');", true);
        }
    }
}