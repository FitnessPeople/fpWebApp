using System;
using System.Data;

namespace fpWebApp
{
    public partial class activosfijos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Activos fijos");
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
                        //divBotonesLista.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            //divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            //divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            //CargarActivos();
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
    }
}