﻿using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class ticketsoporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Tickets soporte");
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
                            ddlUsuarios.Enabled = false;
                            divAsignacion.Visible = false;

                            CargarSedes();
                            CargarTickets();

                            if (Request.QueryString.Count > 0)
                            {
                                bool boolMostrarResponsable = false;
                                if (Request.QueryString["asignarid"] != null)
                                {
                                    divAsignacion.Visible = true;
                                    boolMostrarResponsable = true;
                                    CargarAsignacion(boolMostrarResponsable, Request.QueryString["asignarid"].ToString());
                                }
                                if (Request.QueryString["detailid"] != null)
                                {
                                    divAsignacion.Visible = false;
                                    boolMostrarResponsable = false;
                                    CargarAsignacion(boolMostrarResponsable, Request.QueryString["detailid"].ToString());
                                }
                            }
                        }
                    }
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

        private void CargarTickets()
        {
            string estado = ddlEstado.SelectedValue;
            string prioridad = ddlFiltroPrioridad.SelectedValue;
            string sede = ddlSedes.SelectedValue;
            //string activo = ddlActivos.SelectedValue;

            string strQuery = "SELECT t.idTicketSoporte, af.NombreActivoFijo, af.CodigoInterno, af.ImagenActivo, " +
                "t.DescripcionTicket, t.EstadoTicket, t.PrioridadTicket, t.FechaCreacionTicket, ca.NombreCategoriaActivo, " +
                "u.NombreUsuario, s.NombreSede, u1.NombreUsuario as Responsable, " +
                "IF(t.EstadoTicket='Pendiente','warning',IF(t.EstadoTicket='En proceso','info',IF(t.EstadoTicket='Resuelto','primary','default'))) AS badge, " +
                "IF(t.PrioridadTicket='Baja','info',IF(t.PrioridadTicket='Media','warning','danger')) AS badge2 " +
                "FROM TicketSoporte t " +
                "INNER JOIN ActivosFijos af ON t.idActivoFijo = af.idActivoFijo " +
                "INNER JOIN CategoriasActivos ca ON af.idCategoriaActivo = ca.idCategoriaActivo " +
                "INNER JOIN Usuarios u ON t.idReportadoPor = u.idUsuario " +
                "INNER JOIN Sedes s ON af.idSede = s.idSede " +
                "LEFT JOIN AsignacionesTickets at ON at.idTicket = t.idTicketSoporte " +
                "LEFT JOIN Usuarios u1 ON at.idTecnico = u1.idUsuario " +
                "WHERE ('" + estado + "' = '' OR t.EstadoTicket = '" + estado + "') " +
                "AND ('" + prioridad + "' = '' OR t.PrioridadTicket = '" + prioridad + "') " +
                "AND ('" + sede + "' = '' OR af.idSede = '" + sede + "') " +
                "ORDER BY t.FechaCreacionTicket DESC";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpTickets.DataSource = dt;
            rpTickets.DataBind();   

            dt.Dispose();
        }

        private void CargarSedes()
        {
            clasesglobales cg = new clasesglobales();
            string strQuery = "SELECT s.idSede, CONCAT(s.NombreSede, ' - ', cs.NombreCiudadSede) AS NombreSedeCiudad " +
                "FROM Sedes s, CiudadesSedes cs " +
                "WHERE s.idCiudadSede = cs.idCiudadSede ";

            DataTable dt = cg.TraerDatos(strQuery);

            ddlSedes.DataSource = dt;
            ddlSedes.DataValueField = "idSede";
            ddlSedes.DataTextField = "NombreSedeCiudad";
            ddlSedes.DataBind();
        }

        private void CargarAsignacion(bool mostrarResponsable, string idTicketSoporte)
        {
            string strQuery = "SELECT t.idTicketSoporte, af.NombreActivoFijo, af.CodigoInterno, af.ImagenActivo, " +
                "t.DescripcionTicket, t.EstadoTicket, t.PrioridadTicket, t.FechaCreacionTicket, ca.NombreCategoriaActivo, " +
                "u.NombreUsuario, s.NombreSede, " +
                "IF(t.EstadoTicket='Pendiente','warning',IF(t.EstadoTicket='En proceso','info',IF(t.EstadoTicket='Resuelto','primary','default'))) AS badge, " +
                "IF(t.PrioridadTicket='Baja','info',IF(t.PrioridadTicket='Media','warning','danger')) AS badge2 " +
                "FROM TicketSoporte t " +
                "INNER JOIN ActivosFijos af ON t.idActivoFijo = af.idActivoFijo " +
                "INNER JOIN CategoriasActivos ca ON af.idCategoriaActivo = ca.idCategoriaActivo " +
                "INNER JOIN Usuarios u ON t.idReportadoPor = u.idUsuario " +
                "INNER JOIN Sedes s ON af.idSede = s.idSede " +
                "WHERE t.idTicketSoporte = " + idTicketSoporte;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                ViewState.Add("idTicket", dt.Rows[0]["idTicketSoporte"].ToString());
                ltActivo.Text = dt.Rows[0]["NombreActivoFijo"].ToString();
                ltCodigo.Text = dt.Rows[0]["CodigoInterno"].ToString();
                ltDescripcion.Text = dt.Rows[0]["DescripcionTicket"].ToString();
                ltPrioridad.Text = dt.Rows[0]["PrioridadTicket"].ToString();
                ltCirculoPrioridad.Text = "<i class=\"fa fa-circle text-" + dt.Rows[0]["badge2"].ToString() + " m-r-sm\"></i>";
            }

            dt.Dispose();

            if (mostrarResponsable)
            {
                ddlUsuarios.Enabled = true;
                CargarTecnicos();
            }

        }

        private void CargarTecnicos()
        {
            string strQuery = "SELECT * " +
                "FROM usuarios " +
                "WHERE idPerfil = 22 " +
                "AND EstadoUsuario = 'Activo' " +
                "ORDER BY NombreUsuario " ;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlUsuarios.DataSource = dt;
            ddlUsuarios.DataValueField = "idUsuario";
            ddlUsuarios.DataTextField = "NombreUsuario";
            ddlUsuarios.DataBind();

            dt.Dispose();
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string consultaSQL = @"SELECT NombreUsuario AS 'Nombre de Usuario', EmailUsuario AS 'Correo de Usuario', ClaveUsuario AS 'Contraseña', 
                    CargoUsuario AS 'Cargo de Usuario', EstadoUsuario AS 'Estado de Usuario', DocumentoEmpleado AS 'Nro. de Documento', 
                    IF(NombreEmpleado IS NULL, '-Sin asociar-', NombreEmpleado) AS 'Nombre de Empleado', TelefonoEmpleado AS 'Celular', EmailEmpleado AS 'Correo de Empleado',
                    FechaNacEmpleado AS 'Fecha de Nacimiento', DireccionEmpleado AS 'Dirección de Residencia', NombreCiudad AS 'Ciudad', 
                    NroContrato AS 'Nro. de Contrato', TipoContrato AS 'Tipo de Contrato', CargoEmpleado AS 'Cargo de Empleado', 
                    FechaInicio AS 'Fecha de Inicio', FechaFinal AS 'Fecha de Terminación',
                    Sueldo, GrupoNomina AS 'Grupo de Nómina', Estado, Perfil 
                    FROM Usuarios u 
                    LEFT JOIN Empleados e ON u.idEmpleado = e.DocumentoEmpleado 
				    LEFT JOIN Ciudades c ON c.idCiudad = e.idCiudadEmpleado                                       
                    INNER JOIN Perfiles pf ON u.idPerfil = pf.idPerfil 
                    ORDER BY NombreUsuario;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(consultaSQL);
                string nombreArchivo = $"Usuarios_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    cg.ExportarExcel(dt, nombreArchivo);
                }
                else
                {
                    Response.Write("<script>alert('No existen registros para esta consulta');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al exportar: " + ex.Message + "');</script>");
            }
        }

        protected void rpTickets_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlButton btnAsignar = (HtmlButton)e.Item.FindControl("btnAsignar");
                    btnAsignar.Attributes.Add("onClick", "window.location.href='ticketsoporte?asignarid=" + ((DataRowView)e.Item.DataItem).Row["idTicketSoporte"].ToString() + "'");

                    if (((DataRowView)e.Item.DataItem).Row["EstadoTicket"].ToString() == "En Proceso")
                    {
                        btnAsignar.Visible = false;
                    }
                    else
                    {
                        btnAsignar.Visible = true;
                    }
                }

                DataRowView row = (DataRowView)e.Item.DataItem;
                //  ltTiempoTranscurrido: Hace X minutos
                if (row["FechaCreacionTicket"] != DBNull.Value)
                {
                    DateTime FechaCreacionTicket = Convert.ToDateTime(row["FechaCreacionTicket"]);
                    TimeSpan diferencia = DateTime.Now - FechaCreacionTicket;

                    string leyenda = "";
                    if (diferencia.TotalMinutes < 1)
                    {
                        leyenda = "<i class=\"fa fa-hourglass-half m-r-sm\"></i>Hace menos de un minuto";
                    }
                    else if (diferencia.TotalMinutes < 60)
                    {
                        int min = (int)Math.Floor(diferencia.TotalMinutes);
                        leyenda = $"Hace {min} minuto" + (min == 1 ? "" : "s");
                        leyenda = "<i class=\"fa fa-hourglass-half m-r-sm\"></i>" + leyenda;
                    }
                    else if (diferencia.TotalHours < 24)
                    {
                        int hrs = (int)Math.Floor(diferencia.TotalHours);
                        leyenda = $"Hace {hrs} hora" + (hrs == 1 ? "" : "s");
                        leyenda = "<i class=\"fa fa-clock m-r-sm\"></i>" + leyenda;
                    }
                    else
                    {
                        int dias = (int)Math.Floor(diferencia.TotalDays);
                        leyenda = $"Hace {dias} día" + (dias == 1 ? "" : "s");
                        leyenda = "<i class=\"fa fa-calendar-days m-r-sm\"></i>" + leyenda;
                    }

                    Literal ltTiempo = (Literal)e.Item.FindControl("ltTiempoTranscurrido");
                    if (ltTiempo != null)
                    {
                        ltTiempo.Text = leyenda;
                    }
                }

            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            // Inserta la asignación de responsable en la tabla AsignacionesTickets
            string strResponsable = ddlUsuarios.SelectedItem.Value.ToString();
            string strQuery = "INSERT INTO AsignacionesTickets (idTicket, idTecnico, FechaAsignacion) " +
                "VALUES (" + ViewState["idTicket"].ToString() + ", " + strResponsable + ", NOW())";
            clasesglobales cg = new clasesglobales();
            cg.TraerDatosStr(strQuery);

            // Actualiza el estado del ticket en la tabla TicketSoporte
            strQuery = "UPDATE TicketSoporte SET EstadoTicket = 'En Proceso' WHERE idTicketSoporte = " + ViewState["idTicket"].ToString();
            cg.TraerDatosStr(strQuery);

            Response.Redirect("ticketsoporte");
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTickets();
        }

        protected void ddlFiltroPrioridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTickets();
        }

        protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTickets();
        }
    }
}