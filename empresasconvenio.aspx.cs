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
                    CargarAsesores();
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetNoStore();

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
                DataTable dt = cg.ConsultarEmpresasAfiliadas();


                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["CompanyDoc"] = dt.Rows[0]["DocumentoEmpresa"].ToString();
                }


                rpEmpresas.DataSource = dt;
                rpEmpresas.DataBind();

                rpTabEmpresas.DataSource = dt;
                rpTabEmpresas.DataBind();
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

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al exportar: " + ex.Message + "');</script>");
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


        public void CargarAsesores()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                string strQuery = @"
                    SELECT u.* 
                    FROM usuarios u 
                    INNER JOIN perfiles p ON p.idPerfil = u.idPerfil 
                    WHERE p.idPerfil IN ( 10,36) "; // Perfil 10: Asesor corporativo

                
                DataTable dt = cg.TraerDatos(strQuery);            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string ConvertirACapital(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return texto;

            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            return ti.ToTitleCase(texto.ToLower());
        }

        //protected void rpEmpresas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    clasesglobales cg = new clasesglobales();

        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        if (ViewState["CrearModificar"].ToString() == "1")
        //        {

        //            string valor = ((DataRowView)e.Item.DataItem).Row[0].ToString();
        //            string cifrado = HttpUtility.UrlEncode(cg.Encrypt(valor).Replace("+", "-").Replace("/", "_").Replace("=", ""));

        //            HtmlAnchor btnEditarTab = (HtmlAnchor)e.Item.FindControl("btnEditarTab");

        //        }

        //        DataRowView row = (DataRowView)e.Item.DataItem;
        //        int idUsuario;
        //        if (row["idUsuario"] is DBNull)
        //        {
        //            idUsuario = 0;
        //        }
        //        else
        //        {
        //            idUsuario = Convert.ToInt32(row["idUsuario"]);
        //        }


        //    }
        //}

        protected void rpEmpresas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;

                // 🔥 VALIDAR PERMISOS
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    string idEmpresa = row["idEmpresaAfiliada"].ToString();

                    HtmlAnchor btnEditarEmpresa = (HtmlAnchor)e.Item.FindControl("btnEditarEmpresa");

                    if (btnEditarEmpresa != null)
                    {
                        btnEditarEmpresa.HRef = "editarempresaafiliada?editid=" + idEmpresa;
                        btnEditarEmpresa.Visible = true;
                    }
                }
            }
        }

        [WebMethod]
        public static List<object> ObtenerAsesores()
        {
            clasesglobales cg = new clasesglobales();

            string strQuery = @"
        SELECT idUsuario, NombreUsuario 
        FROM usuarios u 
        INNER JOIN perfiles p ON p.idPerfil = u.idPerfil 
        WHERE p.idPerfil IN (10,36)";

            DataTable dt = cg.TraerDatos(strQuery);

            return dt.AsEnumerable().Select(r => new {
                id = r["idUsuario"].ToString(),
                nombre = r["NombreUsuario"].ToString()
            }).Cast<object>().ToList();
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


        [WebMethod(EnableSession = true)]
        public static object ActualizarConvenioEmpresa( int idConvenio, string fechaConvenio, string fechaFinConvenio,  int nroEmpleados,
        string tipoNegociacion, int diasCredito, string descripcion,  string nombrePagador, string telefonoPagador, string correoPagador,
        string retornoAdm, string estadoConvenio)
        {
            try
            {
                if (HttpContext.Current.Session["idUsuario"] == null)
                    return new { success = false, mensaje = "Sesión expirada" };

                clasesglobales cg = new clasesglobales();

                string respuesta = cg.ActualizarConvenioEmpresa( idConvenio, fechaConvenio, fechaFinConvenio, nroEmpleados,  tipoNegociacion,
                    diasCredito,  descripcion,  nombrePagador, telefonoPagador, correoPagador, retornoAdm, estadoConvenio );

                if (respuesta != "OK")
                    return new { success = false, mensaje = respuesta };

                return new { success = true, mensaje = "Convenio actualizado correctamente" };
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

                    var convenios = cg.ListarConveniosPorEmpresa(idEmpresa); // 

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



        [WebMethod(EnableSession = true)]
        public static List<object> ObtenerDocumentosConvenio(int idConvenio)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ObtenerDocumentosConvenio(idConvenio);

            var lista = new List<object>();

            foreach (DataRow row in dt.Rows)
            {
                DateTime? fecha = null;

                // Validación segura
                if (row["fechaCarga"] != DBNull.Value)
                {
                    // Si ya es DateTime (lo ideal)
                    if (row["fechaCarga"] is DateTime)
                    {
                        fecha = (DateTime)row["fechaCarga"];
                    }
                    else
                    {
                        // Si viene como string
                        DateTime temp;
                        if (DateTime.TryParse(row["fechaCarga"].ToString(), out temp))
                        {
                            fecha = temp;
                        }
                    }
                }

                lista.Add(new
                {
                    IdDocumento = row["idDocumento"],
                    TipoDocumento = row["tipoDocumento"]?.ToString(),
                    NombreArchivo = row["nombreArchivo"]?.ToString(),
                    Fecha = fecha.HasValue ? fecha.Value.ToString("dd/MM/yyyy") : "",
                    Url = row["rutaArchivo"]?.ToString()
                });
            }

            return lista;
        }



        [WebMethod(EnableSession = true)]
        public static object SubirDocumentoConvenio()
        {
            try
            {
                var context = HttpContext.Current;

                if (context.Session["idUsuario"] == null)
                    return new { success = false, mensaje = "Sesión expirada" };

                int idUsuario = Convert.ToInt32(context.Session["idUsuario"]);
                int idConvenio = Convert.ToInt32(context.Request["idConvenio"]);
                string tipoDocumento = context.Request["tipoDocumento"];

                HttpPostedFile file = context.Request.Files["file"];

                if (file == null || file.ContentLength == 0)
                    return new { success = false, mensaje = "Archivo no válido" };

                string carpeta = context.Server.MapPath("~/docs/contratos/");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                string extension = Path.GetExtension(file.FileName);
                string nombreArchivo = idConvenio + "_" + tipoDocumento + extension;

                string rutaCompleta = Path.Combine(carpeta, nombreArchivo);

                if (File.Exists(rutaCompleta))
                    File.Delete(rutaCompleta);

                file.SaveAs(rutaCompleta);

                clasesglobales cg = new clasesglobales();

                cg.InsertarDocumentoConvenio(
                    idConvenio,
                    tipoDocumento,
                    "/docs/contratos/" + nombreArchivo,
                    nombreArchivo,
                    idUsuario
                );

                return new { success = true };
            }
            catch (Exception ex)
            {
                return new { success = false, mensaje = ex.Message };
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

        [WebMethod(EnableSession = true)]
        public static object AsignarEmpresa(int idEmpresa, int idAsesor, string estado)
        {
            clasesglobales cg = new clasesglobales();

            try
            {
               
                //  OBTENER USUARIO DE SESIÓN CORRECTAMENTE
                if (HttpContext.Current.Session["idUsuario"] == null)
                    return new { success = false, mensaje = "Sesión expirada" };

                int idUsuarioActualiza = Convert.ToInt32(HttpContext.Current.Session["idUsuario"]);

                //  VALIDACIONES
                if (idEmpresa <= 0)
                    return new { success = false, mensaje = "Empresa inválida" };

                if (idAsesor <= 0)
                    return new { success = false, mensaje = "Debe seleccionar un asesor" };

                if (string.IsNullOrEmpty(estado))
                    return new { success = false, mensaje = "Debe seleccionar un estado" };

                //  LLAMADO A MÉTODO
                bool ok = cg.ActualizarEstadoAsignacionEmpresa( idEmpresa, idAsesor, estado, idUsuarioActualiza );

                if (ok)
                    return new { success = true };
                else
                    return new { success = false, mensaje = "No se pudo actualizar" };
            }
            catch (Exception ex)
            {
                return new { success = false, mensaje = ex.Message };
            }
        }


    }
}