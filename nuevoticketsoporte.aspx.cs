using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

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
                    ValidarPermisos("Nuevo ticket soporte");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        CargarSedes();
                        CargarCategorias();
                        ddlActivosFijos.Enabled = false;
                        ddlCategoriasActivos.Enabled = false;
                        CargarTickets();
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

        private void CargarTickets()
        {
            string estado = ddlEstado.SelectedValue;
            string prioridad = ddlFiltroPrioridad.SelectedValue;

            string strQuery = "SELECT t.idTicketSoporte, af.NombreActivoFijo, af.CodigoInterno, t.DescripcionTicket, " +
                "t.EstadoTicket, t.PrioridadTicket, t.FechaCreacionTicket, u1.NombreUsuario as Responsable, " +
                "IF(t.EstadoTicket='Pendiente','warning',IF(t.EstadoTicket='En proceso','info',IF(t.EstadoTicket='Resuelto','primary','default'))) AS badge, " +
                "IF(t.PrioridadTicket='Baja','info',IF(t.PrioridadTicket='Media','warning','danger')) AS badge2 " +
                "FROM TicketSoporte t " +
                "INNER JOIN ActivosFijos af ON t.idActivoFijo = af.idActivoFijo " +
                "INNER JOIN Usuarios u ON u.idUsuario = t.idReportadoPor AND u.idUsuario = " + Session["idUsuario"].ToString() + " " +
                "LEFT JOIN AsignacionesTickets at ON at.idTicket = t.idTicketSoporte " +
                "LEFT JOIN Usuarios u1 ON at.idTecnico = u1.idUsuario " +
                "WHERE('" + estado + "' = '' OR t.EstadoTicket = '" + estado + "') " +
                "AND('" + prioridad + "' = '' OR t.PrioridadTicket = '" + prioridad + "') " +
                "ORDER BY t.FechaCreacionTicket DESC";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

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

        private void CargarCategorias()
        {
            ddlCategoriasActivos.Items.Clear();
            ListItem li = new ListItem("Seleccione", "");
            ddlCategoriasActivos.Items.Add(li);
            clasesglobales cg = new clasesglobales();
            string strQuery = "SELECT idCategoriaActivo, NombreCategoriaActivo " +
                "FROM CategoriasActivos ";

            DataTable dt = cg.TraerDatos(strQuery);

            ddlCategoriasActivos.DataSource = dt;
            ddlCategoriasActivos.DataValueField = "idCategoriaActivo";
            ddlCategoriasActivos.DataTextField = "NombreCategoriaActivo";
            ddlCategoriasActivos.DataBind();

        }

        private void CargarActivos()
        {
            clasesglobales cg = new clasesglobales();
            string strQuery = "SELECT idActivoFijo, CONCAT(NombreActivoFijo, ' ◾ ', CodigoInterno) AS NombreActivoFijo " +
                "FROM ActivosFijos " +
                "WHERE idSede = " + ddlSedes.SelectedItem.Value.ToString() + " " +
                "AND idCategoriaActivo = " + ddlCategoriasActivos.SelectedItem.Value.ToString();

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

            Response.Redirect("nuevoticketsoporte");

        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTickets();
        }

        protected void ddlFiltroPrioridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTickets();
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

        protected void rpTickets_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
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

        protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCategoriasActivos.Enabled = true;
            CargarCategorias();
        }

        protected void ddlCategoriasActivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlActivosFijos.Enabled = true;
            CargarActivos();
        }
    }
}