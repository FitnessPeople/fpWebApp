using System;
using System.Data;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

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
                        divBotonesLista.Visible = false;
                        btnAgregar.Visible = false;
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
                            txbFechaIngreso.Attributes.Add("type", "date");
                            CargarSedes();
                            CargarCategorias();
                            CargarActivos();
                            btnAgregar.Visible = true;
                        }
                    }

                    ltTitulo.Text = "Agregar sede";

                    if (Request.QueryString.Count > 0)
                    {
                        rpActivosFijos.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            string strQuery = "SELECT * FROM ActivosFijos WHERE idActivoFijo = " + Request.QueryString["editid"].ToString();
                            //DataTable dt = cg.ConsultarSedePorId(int.Parse(Request.QueryString["editid"].ToString()));
                            DataTable dt = cg.TraerDatos(strQuery);
                            if (dt.Rows.Count > 0)
                            {
                                txbActivo.Text = dt.Rows[0]["NombreActivoFijo"].ToString();
                                txbCodigoInterno.Text = dt.Rows[0]["CodigoInterno"].ToString();
                                txbMarca.Text = dt.Rows[0]["Marca"].ToString();
                                txbProveedor.Text = dt.Rows[0]["Proveedor"].ToString();
                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar sede";
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            //Borrar
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

        private void CargarActivos()
        {
            string sede = ddlSedes.SelectedValue;
            string categoria = ddlCategorias.SelectedValue;

            clasesglobales cg = new clasesglobales();
            string strQuery = "SELECT * " +
                "FROM ActivosFijos af " +
                "INNER JOIN Sedes s ON s.idSede = af.idSede " +
                "INNER JOIN CategoriasActivos ca ON ca.idCategoriaActivo = af.idCategoriaActivo " +
                "WHERE ('" + sede + "' = '' OR af.idSede = '" + sede + "') " +
                "AND ('" + categoria + "' = '' OR af.idCategoriaActivo = '" + categoria + "') ";

            DataTable dt = cg.TraerDatos(strQuery);

            rpActivosFijos.DataSource = dt;
            rpActivosFijos.DataBind();
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

            ddlSede.DataSource = dt;
            ddlSede.DataValueField = "idSede";
            ddlSede.DataTextField = "NombreSedeCiudad";
            ddlSede.DataBind();
        }

        private void CargarCategorias()
        {
            clasesglobales cg = new clasesglobales();
            string strQuery = "SELECT * " +
                "FROM CategoriasActivos ";

            DataTable dt = cg.TraerDatos(strQuery);

            ddlCategorias.DataSource = dt;
            ddlCategorias.DataValueField = "idCategoriaActivo";
            ddlCategorias.DataTextField = "NombreCategoriaActivo";
            ddlCategorias.DataBind();

            ddlCategoria.DataSource = dt;
            ddlCategoria.DataValueField = "idCategoriaActivo";
            ddlCategoria.DataTextField = "NombreCategoriaActivo";
            ddlCategoria.DataBind();
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

        protected void ddlSedes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarActivos();
        }

        protected void ddlCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarActivos();
        }

        protected void rpActivosFijos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "activosfijos?editid=" + ((DataRowView)e.Item.DataItem).Row["idActivoFijo"].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "activosfijos?deleteid=" + ((DataRowView)e.Item.DataItem).Row["idActivoFijo"].ToString());
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            string strFechaIngreseso = "2000-01-01";

            if (txbFechaIngreso.Text.ToString() != "")
            {
                strFechaIngreseso = txbFechaIngreso.Text.ToString();
            }

            string strFilename = "no-image.jpg";
            HttpPostedFile postedFile = Request.Files["fileFoto"];
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                string filePath = Server.MapPath("img//activos//") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                strFilename = postedFile.FileName;
            }

            clasesglobales cg = new clasesglobales();

            try
            {
                mensaje = cg.InsertarActivo(Convert.ToInt32(ddlSede.SelectedItem.Value.ToString()),
                    Convert.ToInt32(ddlCategoria.SelectedItem.Value.ToString()), 
                    txbActivo.Text.ToString(), "Activo", txbCodigoInterno.Text.ToString(), txbMarca.Text.ToString(), 
                    txbProveedor.Text.ToString(), strFechaIngreseso, strFilename);

                if (mensaje == "OK")
                {
                    cg.InsertarLog(Session["idusuario"].ToString(), "activosfijos", "Nuevo",
                        "El usuario creó un nuevo activo fijo: " + txbActivo.Text.ToString() + " - " + txbCodigoInterno.Text.ToString(), "", "");

                    string script = @"
                        Swal.fire({
                            title: 'Activo registrado',
                            text: '',
                            icon: 'success',
                            timer: 5000,
                            showConfirmButton: false,
                            timerProgressBar: true
                        }).then(() => {
                            window.location.href = 'activosfijos';
                        });
                    ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                }
                else
                {
                    string script = @"
                        Swal.fire({
                            title: 'Error',
                            text: 'No se pudo registrar. Detalle: " + mensaje.Replace("'", "\\'") + @"',
                            icon: 'error'
                        });
                    ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                }
            }
            catch (Exception ex)
            {
                string script = @"
                    Swal.fire({
                        title: 'Error',
                        text: 'Ocurrió un error inesperado. Detalle: " + ex.Message.Replace("'", "\\'") + @"',
                        icon: 'error'
                    });
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
            }
        }
    }
}