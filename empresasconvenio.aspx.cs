using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
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
            string nombrePagador, string telefonoPagador, string correoPagador, decimal retornoAdm, int nroEmpleados)
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
                    nombrePagador, //NombrePagador
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
                    idUsuario,
                    nroEmpleados
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



        [WebMethod]
        public static object ActualizarConvenioEmpresa( int idConvenio, string fechaConvenio, string fechaFinConvenio, int nroEmpleados, string tipoNegociacion,
        int diasCredito, string descripcion, string nombrePagador,  string telefonoPagador,  string correoPagador, string retornoAdm)
        {
            try
            {
                string conexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection conn = new MySqlConnection(conexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("PA_ACTUALIZAR_CONVENIO_EMPRESA", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@p_idConvenio", idConvenio);
                        cmd.Parameters.AddWithValue("@p_fechaConvenio", fechaConvenio);
                        cmd.Parameters.AddWithValue("@p_fechaFinConvenio", fechaFinConvenio);
                        cmd.Parameters.AddWithValue("@p_nroEmpleados", nroEmpleados);
                        cmd.Parameters.AddWithValue("@p_tipoNegociacion", tipoNegociacion);
                        cmd.Parameters.AddWithValue("@p_diasCredito", diasCredito);
                        cmd.Parameters.AddWithValue("@p_descripcion", descripcion);

                        cmd.Parameters.AddWithValue("@p_nombrePagador", nombrePagador);
                        cmd.Parameters.AddWithValue("@p_telefonoPagador", telefonoPagador);
                        cmd.Parameters.AddWithValue("@p_correoPagador", correoPagador);
                        cmd.Parameters.AddWithValue("@p_retornoAdm", retornoAdm);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                return new { success = true };
            }
            catch (Exception ex)
            {
                return new { success = false, mensaje = ex.Message };
            }
        }


        [WebMethod(EnableSession = true)]
        public static object RenovarConvenioEmpresa( int idConvenioAnterior, int idEmpresaAfiliada, string fechaConvenio, string fechaFinConvenio,
            string tipoNegociacion, int diasCredito, string descripcion, string nombrePagador, string telefonoPagador, string correoPagador,
            decimal retornoAdm,int nroEmpleados)
        {
            try
            {
                if (HttpContext.Current.Session["idUsuario"] == null)
                    return new { success = false, mensaje = "Sesión expirada" };

                int idUsuario = Convert.ToInt32(HttpContext.Current.Session["idUsuario"]);

                clasesglobales cg = new clasesglobales();

                // 
                int idNuevoConvenio = cg.InsertarConvenioEmpresa(
                    idEmpresaAfiliada,
                    Convert.ToDateTime(fechaConvenio),
                    Convert.ToDateTime(fechaFinConvenio),
                    "", //NombreContacto
                    "", //CargoContacto
                    nombrePagador,
                    telefonoPagador,
                    correoPagador,
                    tipoNegociacion,
                    diasCredito,
                    retornoAdm,
                    "", //Contrato
                    "", //CamaraComercio
                    "", //Rut
                    "", //CedulaRepLeg
                    descripcion,
                    idUsuario,
                    nroEmpleados
                );

                // . ACTUALIZAR ESTADO DEL ANTERIOR
                cg.ActualizarEstadoConvenio(idConvenioAnterior, "RENOVADO");

                return new
                {
                    success = true,
                    idConvenio = idNuevoConvenio
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
                // 🔥 Obtener datos de la empresa actual
                DataRowView row = (DataRowView)e.Item.DataItem;

                int idEmpresa = Convert.ToInt32(row["idEmpresaAfiliada"]);

                // 🔥 Buscar el repeater interno
                Repeater rpConvenios = (Repeater)e.Item.FindControl("rpConvenios");

                if (rpConvenios != null)
                {
                    clasesglobales cg = new clasesglobales();

                    var convenios = cg.ListarConveniosPorEmpresa(idEmpresa); // 👈 TU MÉTODO

                    rpConvenios.DataSource = convenios;
                    rpConvenios.DataBind();
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public static object AnularConvenioEmpresa(int idConvenio)
        {
            try
            {
                if (HttpContext.Current.Session["idUsuario"] == null)
                    return new { success = false, mensaje = "Sesión expirada" };

                clasesglobales cg = new clasesglobales();

                // 🔥 reutilizas tu método
                cg.ActualizarEstadoConvenio(idConvenio, "ANULADO");

                return new { success = true };
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

        [WebMethod]
        public static List<object> ObtenerDocumentosConvenio(int idConvenio)
        {
            clasesglobales cg = new clasesglobales();

            DataTable dt = cg.ObtenerDocumentosConvenio(idConvenio);

            var lista = new List<object>();

            foreach (DataRow row in dt.Rows)
            {
                lista.Add(new
                {
                    IdDocumento = row["idDocumento"],
                    TipoDocumento = row["tipoDocumento"].ToString(),
                    NombreArchivo = row["nombreArchivo"].ToString(),
                    Fecha = Convert.ToDateTime(row["fechaCarga"]).ToString("dd/MM/yyyy"),
                    Url = row["rutaArchivo"].ToString()
                });
            }

            return lista;
        }


        protected void btnSubirDocumento_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["idUsuario"] == null)
                {
                    MostrarAlerta("Error", "Sesión expirada", "error");
                    return;
                }

                if (!fileDocumento.HasFile)
                {
                    MostrarAlerta("Error", "Seleccione un archivo", "error");
                    return;
                }

                if (string.IsNullOrEmpty(ddlTipoDocumento.SelectedValue))
                {
                    MostrarAlerta("Error", "Seleccione tipo de documento", "error");
                    return;
                }

                int idConvenio = Convert.ToInt32(hdIdConvenioDoc.Value);
                int idUsuario = Convert.ToInt32(Session["idUsuario"]);
                string tipoDocumento = ddlTipoDocumento.SelectedValue;

                string carpeta = Server.MapPath("~/docs/contratos/");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                //  EXTENSIÓN ORIGINAL
                string extension = Path.GetExtension(fileDocumento.FileName);

                //  NOMBRE LIMPIO Y CONTROLADO
                string nombreArchivo = idConvenio + "_" + tipoDocumento + extension;

                string rutaCompleta = Path.Combine(carpeta, nombreArchivo);

                //  SI EXISTE, LO REEMPLAZA
                if (File.Exists(rutaCompleta))
                {
                    File.Delete(rutaCompleta);
                }

                //  GUARDAR ARCHIVO
                fileDocumento.SaveAs(rutaCompleta);

                //  GUARDAR EN BD
                clasesglobales cg = new clasesglobales();

                cg.InsertarDocumentoConvenio(
                    idConvenio,
                    tipoDocumento,
                    "/docs/contratos/" + nombreArchivo,
                    nombreArchivo,
                    idUsuario
                );

                MostrarAlerta("OK", "Documento subido correctamente", "success");

                // RECARGAR TABLA SIN RECARGAR PÁGINA
                ScriptManager.RegisterStartupScript(this, GetType(), "reload",
                    $"cargarDocumentos({idConvenio});", true);
            }
            catch (Exception ex)
            {
                MostrarAlerta("Error", ex.Message, "error");
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]

        public static object EliminarDocumentoConvenio(int idDocumento)
        {
            try
            {
                clasesglobales cg = new clasesglobales();

                DataTable dt = cg.ObtenerDocumentoPorId(idDocumento);

                if (dt.Rows.Count == 0)
                {
                    return new
                    {
                        success = false,
                        mensaje = "Documento no encontrado"
                    };
                }

                string rutaArchivo = dt.Rows[0]["rutaArchivo"].ToString();
                string ruta = HttpContext.Current.Server.MapPath(rutaArchivo);

                // eliminar archivo físico
                if (File.Exists(ruta))
                {
                    File.Delete(ruta);
                }

                // eliminar en BD
                cg.EliminarDocumentoConvenio(idDocumento);

                //  IMPORTANTE: retorno limpio
                return new
                {
                    success = true
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


    }
}