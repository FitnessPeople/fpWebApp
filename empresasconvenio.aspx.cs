using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class empresasconvenio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Empleados");
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
                            //lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }
                    listaEmpresasConvenio();




                    CargarCargos();
                    CargarSedes();
                    CargarCanalesVenta();
                    CargarTipoDocumento();

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



        //private void listaEmpleados()
        //{
        //    clasesglobales cg = new clasesglobales();
        //    DataTable dt = new DataTable();
        //    if (Session["idSede"].ToString() == "11") // Usuario administrativo
        //    {
        //        dt = cg.ConsultarEmpleados();
        //    }
        //    else
        //    {
        //        dt = cg.ConsultarEmpleadosPorSede(Session["idSede"].ToString());
        //    }

        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        string firstRow = dt.Rows[0]["DocumentoEmpleado"].ToString();
        //        ViewState.Add("EmployeeDoc", firstRow);
        //    }

        //    rpEmpleados.DataSource = dt;
        //    rpEmpleados.DataBind();

        //    rpTabEmpleados.DataSource = dt;
        //    rpTabEmpleados.DataBind();

        //    dt.Dispose();
        //}

        private void listaEmpresasConvenio()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = new DataTable();
                dt = cg.ConsultarEmpresasAfiliadas();

                rpEmpresas.DataSource = dt;
                rpEmpresas.DataBind();

                rpTabEmpresas.DataSource = dt;
                rpTabEmpresas.DataBind();

                if (dt != null && dt.Rows.Count > 0)
                {
                    string firstRow = dt.Rows[0]["DocumentoEmpresa"].ToString();
                    ViewState.Add("CompanyDoc", firstRow);
                }
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Código: " + idLog, "error");
            }

        }




        //protected void rpEmpleados_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    clasesglobales cg = new clasesglobales();

        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        if (ViewState["CrearModificar"].ToString() == "1")
        //        {
        //            string valor = ((DataRowView)e.Item.DataItem).Row[0].ToString();
        //            string cifrado = HttpUtility.UrlEncode(cg.Encrypt(valor).Replace("+", "-").Replace("/", "_").Replace("=", ""));

        //            HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
        //            //btnEditar.Attributes.Add("href", "editarempleado?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
        //            btnEditar.Attributes.Add("href", "editarempleado?editid=" + cifrado);
        //            btnEditar.Visible = true;
        //        }
        //        if (ViewState["Borrar"].ToString() == "1")
        //        {
        //            HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
        //            btnEliminar.Attributes.Add("href", "eliminarempleado?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
        //            btnEliminar.Visible = true;
        //        }
        //    }
        //}

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarEmpleados();

                string nombreArchivo = $"Empleados_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    cg.ExportarExcelOk(dt, nombreArchivo);
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

        protected void rpTabEmpleados_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void lkbCambiarEstado_Click(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static object ObtenerDatosEmpleado(string documento)
        {
            clasesglobales cg = new clasesglobales();

            try
            {

                DataTable dt = cg.ConsultarEmpleado(documento);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    return new
                    {
                        Cargo = row["Cargo"].ToString(),
                        Sueldo = row["Sueldo"] != DBNull.Value ? Convert.ToDecimal(row["Sueldo"]) : 0,
                        idCargo = row["idCargo"] != DBNull.Value ? Convert.ToInt32(row["idCargo"]) : 0,

                        Sede = row["NombreSede"].ToString(),
                        idSede = Convert.ToInt32(row["idSede"]),

                        CanalVenta = row["NombreCanalVenta"].ToString(),
                        idCanalVenta = row["idCanalVenta"] != DBNull.Value ? Convert.ToInt32(row["idCanalVenta"]) : 0

                    };
                }
            }
            catch (Exception ex)
            {
                return new
                {
                    error = true,
                    mensaje = ex.Message
                };
            }

            return null;
        }




        private void CargarSedes()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultaCargarSedes("Todos");
                ddlNuevaSede.DataSource = dt;

                ddlNuevaSede.DataSource = dt;
                ddlNuevaSede.DataTextField = "NombreSede";
                ddlNuevaSede.DataValueField = "idSede";
                ddlNuevaSede.DataBind();
                ddlNuevaSede.Items.Insert(0, new ListItem("Seleccione sede", ""));


                ddlSedeIngreso.DataSource = dt;
                ddlSedeIngreso.DataSource = dt;
                ddlSedeIngreso.DataTextField = "NombreSede";
                ddlSedeIngreso.DataValueField = "idSede";
                ddlSedeIngreso.DataBind();

                ddlSedeIngreso.Items.Insert(0, new ListItem("Seleccione sede", ""));


                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Código: " + idLog, "error");
            }


        }
        private void CargarCargos()
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                DataTable dt = cg.ConsultarCargos();

                ddlNuevoCargo.DataSource = dt;
                ddlNuevoCargo.DataTextField = "NombreCargo";   // texto visible
                ddlNuevoCargo.DataValueField = "idCargo";     // valor
                ddlNuevoCargo.DataBind();

                ddlNuevoCargo.Items.Insert(0, new ListItem("Seleccione cargo", ""));

                ddlCargoIngreso.DataSource = dt;
                ddlCargoIngreso.DataTextField = "NombreCargo";   // texto visible
                ddlCargoIngreso.DataValueField = "idCargo";     // valor
                ddlCargoIngreso.DataBind();

                ddlCargoIngreso.Items.Insert(0, new ListItem("Seleccione cargo", ""));


                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso",
                    "Ocurrió un inconveniente. Código: " + idLog,
                    "error");
            }
        }

        private void CargarTipoDocumento()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultartiposDocumento();
                ddlTipoDocumentoNuevo.DataSource = dt;
                ddlTipoDocumentoNuevo.DataTextField = "TipoDocumento";
                ddlTipoDocumentoNuevo.DataValueField = "idTipoDoc";
                ddlTipoDocumentoNuevo.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Código: " + idLog, "error");
            }


        }

        private void CargarCanalesVenta()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultarCanalesVenta();

                ddlNuevoCanal.DataSource = dt;
                ddlNuevoCanal.DataTextField = "NombreCanalVenta";
                ddlNuevoCanal.DataValueField = "idCanalVenta";
                ddlNuevoCanal.DataBind();

                ListItem item = ddlNuevoCanal.Items.FindByValue("15");
                if (item != null)
                {
                    ddlNuevoCanal.Items.Remove(item);
                }

                ///
                ddlCanalNuevo.DataSource = dt;
                ddlCanalNuevo.DataTextField = "NombreCanalVenta";
                ddlCanalNuevo.DataValueField = "idCanalVenta";
                ddlCanalNuevo.DataBind();

                ListItem item1 = ddlCanalNuevo.Items.FindByValue("15");
                if (item1 != null)
                {
                    ddlCanalNuevo.Items.Remove(item1);
                }

                dt.Dispose();
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, this.GetType().Name, Convert.ToInt32(Session["idUsuario"]));
                MostrarAlerta("Error de proceso", "Ocurrió un inconveniente. Si persiste, comuníquese con sistemas. Código de error:" + idLog, "error");
            }
        }



        [WebMethod(EnableSession = true)]
        public static object InsertarCambioCargoEmpleado(string documento, int idNuevoCargo)
        {
            try
            {
                clasesglobales cg = new clasesglobales();

                DataTable dt = cg.ConsultarEmpleado(documento);

                if (dt == null || dt.Rows.Count == 0)
                    return new { success = false, mensaje = "Empleado no encontrado." };

                if (HttpContext.Current.Session["idUsuario"] == null)
                    return new { success = false, mensaje = "La sesión ha expirado." };

                DataRow row = dt.Rows[0];

                int idUsuario = Convert.ToInt32(HttpContext.Current.Session["idUsuario"]);
                int idCargoActual = Convert.ToInt32(row["idCargo"]);

                if (idCargoActual == idNuevoCargo)
                    return new { success = false, mensaje = "El nuevo cargo no puede ser igual al actual." };

                string respuesta = cg.ActualizarCambioDeCargoEmpleado(documento, idNuevoCargo, idUsuario);

                if (respuesta != "OK")
                    return new { success = false, mensaje = respuesta };

                return new { success = true, mensaje = "Gestión Humana - Fitness People." };
            }
            catch (Exception ex)
            {
                return new { success = false, mensaje = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object InsertarCambioSalarialEmpleado(string documento, decimal nuevoSueldo)
        {
            try
            {
                clasesglobales cg = new clasesglobales();

                DataTable dt = cg.ConsultarEmpleado(documento);

                if (dt == null || dt.Rows.Count == 0)
                    return new { success = false, mensaje = "Empleado no encontrado." };

                if (HttpContext.Current.Session["idUsuario"] == null)
                    return new { success = false, mensaje = "La sesión ha expirado." };

                DataRow row = dt.Rows[0];

                int idUsuario = Convert.ToInt32(HttpContext.Current.Session["idUsuario"]);
                decimal sueldoActual = Convert.ToDecimal(row["Sueldo"]);

                decimal salarioMinimo = 1750000m;
                decimal medioSalarioMinimo = salarioMinimo / 2;

                if (nuevoSueldo <= 0)
                    return new { success = false, mensaje = "El salario no puede ser cero o negativo." };

                if (nuevoSueldo < medioSalarioMinimo)
                    return new { success = false, mensaje = "El salario no puede ser menor a medio salario mínimo." };

                if (nuevoSueldo == sueldoActual)
                    return new { success = false, mensaje = "El nuevo salario no puede ser igual al actual." };

                string respuesta = cg.ActualizarCambioSalarialEmpleado(documento, nuevoSueldo, idUsuario);

                if (respuesta != "OK")
                    return new { success = false, mensaje = respuesta };

                return new { success = true, mensaje = "Gestión Humana - Fitness People." };
            }
            catch (Exception ex)
            {
                return new { success = false, mensaje = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public static object InsertarTrasladoEmpleado(string documento, int idNuevaSede, int idNuevoCanal)
        {
            try
            {
                clasesglobales cg = new clasesglobales();

                DataTable dt = cg.ConsultarEmpleado(documento);

                if (dt == null || dt.Rows.Count == 0)
                    return new { success = false, mensaje = "Empleado no encontrado." };

                if (HttpContext.Current.Session["idUsuario"] == null)
                    return new { success = false, mensaje = "La sesión ha expirado. Inicie sesión nuevamente." };

                DataRow row = dt.Rows[0];

                int idUsuario = Convert.ToInt32(HttpContext.Current.Session["idUsuario"]);
                int idSedeActual = Convert.ToInt32(row["idSede"]);
                int idCanalActual = Convert.ToInt32(row["idCanalVenta"]);

                if (idNuevaSede <= 0)
                    return new { success = false, mensaje = "Seleccione una sede." };

                if (idSedeActual == idNuevaSede)
                    return new { success = false, mensaje = "El empleado ya pertenece a esa sede." };

                if (idNuevoCanal <= 0)
                    return new { success = false, mensaje = "Seleccione un canal válido." };


                string respuesta = cg.ActualizarTrasladoEmpleado(documento, idNuevaSede, idNuevoCanal, idUsuario);

                if (respuesta != "OK")
                    return new { success = false, mensaje = respuesta };

                return new { success = true, mensaje = "Gestión Humana - Fitness People" };
            }
            catch (Exception ex)
            {
                return new { success = false, mensaje = ex.Message };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object InsertarDatosBasicosEmpleado(int tipoDocumento, string documento, string nombre, string correo,
        int canal, int sede, int cargo)
        {
            try
            {
                clasesglobales cg = new clasesglobales();

                if (HttpContext.Current.Session["idUsuario"] == null)
                    return new { success = false, mensaje = "Sesión expirada." };

                int idUsuario = Convert.ToInt32(HttpContext.Current.Session["idUsuario"]);

                nombre = ConvertirACapital(nombre);

                string respuesta = cg.InsertarIngresoRapidoEmpleado(tipoDocumento, documento, nombre, correo, canal, sede, cargo, idUsuario);

                if (respuesta != "OK")
                    return new { success = false, mensaje = respuesta };

                return new { success = true, mensaje = "El colaorador deberá terminar de diligenciar los datos en la opción Mi Cuenta - Gestión Humana - Fitness People" };
            }
            catch (Exception ex)
            {
                return new { success = false, mensaje = ex.Message };
            }
        }
        private void MostrarAlerta(string titulo, string mensaje, string tipo)
        {
            clasesglobales cg = new clasesglobales();

            // tipo puede ser: 'success', 'error', 'warning', 'info', 'question'
            string script = $@"
                Swal.hideLoading();
                Swal.fire({{
                title: '{titulo}',
                text: '{mensaje}',
                icon: '{tipo}', 
                allowOutsideClick: false, 
                showCloseButton: false, 
                confirmButtonText: 'Aceptar'
            }});";

            ScriptManager.RegisterStartupScript(this, GetType(), "SweetAlert", script, true);
        }

        public static string ConvertirACapital(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return texto;

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            return ti.ToTitleCase(texto.ToLower());
        }

        protected void rpEmpresas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            clasesglobales cg = new clasesglobales();

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {

                    string valor = ((DataRowView)e.Item.DataItem).Row[0].ToString();
                    string cifrado = HttpUtility.UrlEncode(cg.Encrypt(valor).Replace("+", "-").Replace("/", "_").Replace("=", ""));

                    HtmlAnchor btnEditarTab = (HtmlAnchor)e.Item.FindControl("btnEditarTab");
                    //btnEditarTab.Attributes.Add("href", "editarempleado?editid=" + ((DataRowView)e.Item.DataItem).Row["DocumentoEmpleado"].ToString());
                    //btnEditarTab.Attributes.Add("href", "editarempleado?editid=" + cifrado);
                    //btnEditarTab.Attributes.Add("href", "editarempresaafiliada?editid=" + valor);
                    //btnEditarTab.Visible = true;

                    //HtmlAnchor btnCambiarEstado = (HtmlAnchor)e.Item.FindControl("btnCambiarEstado");
                    //btnCambiarEstado.Attributes.Add("href", "cambiarestadoempleado?id=" + ((DataRowView)e.Item.DataItem).Row["DocumentoEmpresa"].ToString());
                    //btnCambiarEstado.Visible = true;
                }

                DataRowView row = (DataRowView)e.Item.DataItem;
                int idUsuario;
                if (row["idUsuario"] is DBNull)
                {
                    idUsuario = 0;
                }
                else
                {
                    idUsuario = Convert.ToInt32(row["idUsuario"]);
                }

                //string strQuery = @"
                //    SELECT Accion, DescripcionLog, FechaHora, 
                //    CASE Accion 
                //        WHEN 'Agrega' THEN 'circle-plus' 
                //        WHEN 'Elimino' THEN 'trash' 
                //        WHEN 'Login' THEN 'lock-open' 
                //        WHEN 'Modifica' THEN 'edit' 
                //        WHEN 'Logout' THEN 'lock' 
                //        WHEN 'Nuevo' THEN 'circle-plus' 
                //        ELSE 'coffee' 
                //    END AS Label 
                //    FROM logs 
                //    WHERE idUsuario = " + idUsuario.ToString() + " ORDER BY FechaHora DESC LIMIT 10 ";
                ////clasesglobales cg = new clasesglobales();
                //DataTable dt = cg.TraerDatos(strQuery);

                //Repeater rpActividades = (Repeater)e.Item.FindControl("rpActividades");
                //rpActividades.DataSource = dt;
                //rpActividades.DataBind();
            }
        }


        [WebMethod(EnableSession = true)]
        public static object InsertarConvenioEmpresa( int idEmpresaAfiliada, string fechaConvenio, string fechaFinConvenio, string tipoNegociacion, int diasCredito, string descripcion,
            string nombrePagador, string telefonoPagador, string correoPagador, decimal retornoAdm)
        {
            try
            {
                if (HttpContext.Current.Session["idUsuario"] == null)
                    return new { success = false, mensaje = "Sesión expirada" };

                int idUsuario = Convert.ToInt32(HttpContext.Current.Session["idUsuario"]);

                clasesglobales cg = new clasesglobales();

                int idConvenio = cg.InsertarConvenioEmpresa(
                    idEmpresaAfiliada,
                    Convert.ToDateTime(fechaConvenio),
                    Convert.ToDateTime(fechaFinConvenio),
                    "", //NombreContacto
                    "", //CargoContacto
                    telefonoPagador, //NombrePagador
                    telefonoPagador, //TelefonoPagador
                    correoPagador, //CorreoPagador
                    tipoNegociacion,
                    diasCredito,
                    retornoAdm, //RetornoAdm
                    "", //Contrato
                    "", //CamaraComercio
                    "", //Rut
                    "", //CedulaRepLeg
                    descripcion,
                    idUsuario
                );

                return new
                {
                    success = true,
                    idConvenio = idConvenio
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    mensaje = ex.Message
                };
            }
        }


        protected void rpTabEmpresas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int idEmpresa = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "idEmpresaAfiliada"));

                Repeater rpConvenios = (Repeater)e.Item.FindControl("rpConvenios");

                clasesglobales cg = new clasesglobales();

                DataTable dt = cg.ListarConveniosPorEmpresa(idEmpresa); // usa tu SP

                rpConvenios.DataSource = dt;
                rpConvenios.DataBind();
            }
        }



    }
}