using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease.Activities;

namespace fpWebApp
{
    public partial class nuevaempresaafiliada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Empresas afiliadas");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            txbTelefonoPpal.Attributes.Add("type", "number");
                            txbCorreoPagador.Attributes.Add("type", "email");
                            txbCelularPagador.Attributes.Add("type", "number");
                            txbFechaConvenio.Attributes.Add("type", "date");
                            txbFechaFinConvenio.Attributes.Add("type", "date");
                            txbNroEmpleados.Attributes.Add("type", "number");
                            CargarTipoDocumento();
                            CargarCiudad();
                            btnAgregar.Visible = true;
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

        private void CargarTipoDocumento()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultartiposDocumento();

            ddlTipoDocumento.DataSource = dt;
            ddlTipoDocumento.DataBind();

            dt.Dispose();
        }

        private void CargarCiudad()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCiudadesCol();

            ddlCiudadEmpresa.DataSource = dt;
            ddlCiudadEmpresa.DataBind();

            dt.Dispose();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string contenidoEditor = hiddenEditor.Value;
            string carpeta = Server.MapPath("~/docs/contratos/");
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            string strFilenameContrato = "";
            string strFilenameCamara = "";
            string strFilenameRut = "";
            string strFilenameCedRep = "";

            HttpPostedFile postedFileContrato = Request.Files["fileConvenio"];
            HttpPostedFile postedFileCamara = Request.Files["fileCamara"];
            HttpPostedFile postedFileRut = Request.Files["fileRut"];
            HttpPostedFile postedFileCedRep = Request.Files["fileCedulaRepLeg"];


            if (postedFileContrato != null && postedFileContrato.ContentLength > 0)
            {
                string nombre = Path.GetFileName(postedFileContrato.FileName);
                postedFileContrato.SaveAs(Path.Combine(carpeta, nombre));
                strFilenameContrato = nombre;
            }

            if (postedFileCamara != null && postedFileCamara.ContentLength > 0)
            {
                string nombre = txbDocumento.Text.Trim() + "_CamaraComercio.pdf";
                postedFileCamara.SaveAs(Path.Combine(carpeta, nombre));
                strFilenameCamara = nombre;
            }

            if (postedFileRut != null && postedFileRut.ContentLength > 0)
            {
                string nombre = txbDocumento.Text.Trim() + "_Rut.pdf";
                postedFileRut.SaveAs(Path.Combine(carpeta, nombre));
                strFilenameRut = nombre;
            }

            if (postedFileCedRep != null && postedFileCedRep.ContentLength > 0)
            {
                string nombre = txbDocumento.Text.Trim() + "_CedulaRepLegal.pdf";
                postedFileCedRep.SaveAs(Path.Combine(carpeta, nombre));
                strFilenameCedRep = nombre;
            }

            contenidoEditor = hiddenEditor.Value;

            clasesglobales cg = new clasesglobales();
            bool respuesta;
            string mensaje;
            try
            {
                mensaje = cg.InsertarEmpresaAfiliada( txbDocumento.Text.Trim(), txbDV.Text.Trim(), Convert.ToInt32(ddlTipoDocumento.SelectedValue),
                    txbNombreCcial.Text.Trim().ToUpper(), txbRazonSocial.Text.Trim().ToUpper(), Convert.ToDateTime(txbFechaConvenio.Text), 
                    string.IsNullOrEmpty(txbFechaFinConvenio.Text) ? (DateTime?)null: Convert.ToDateTime(txbFechaFinConvenio.Text),
                    txbNombreContacto.Text.Trim().ToUpper(), txbCargoContacto.Text.Trim().ToUpper(), txbTelefonoPpal.Text.Trim(), txbCorreo.Text.Trim(),
                    txbNombrepagador.Text.Trim().ToUpper(),txbCelularPagador.Text.Trim(),txbCorreoPagador.Text.Trim(), txbDireccion.Text.Trim(),
                    Convert.ToInt32(ddlCiudadEmpresa.SelectedValue), Convert.ToInt32(txbNroEmpleados.Text), ddlTipoNegociacion.SelectedValue,
                    Convert.ToInt32(ddlDiasCredito.SelectedValue), strFilenameContrato, "", strFilenameCamara, strFilenameRut, strFilenameCedRep,
                    rblActivo.SelectedValue, Convert.ToInt32(Session["IdUsuario"]), out respuesta,out mensaje);

                if (!respuesta)
                {                    
                    string script = @"
                            Swal.fire({
                                title: 'Error',
                                text: '" + mensaje.Replace("'", "\\'") + @"',
                                icon: 'error'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                            
                                }
                            });
                        ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);                  
                }
                else
                {
                    string script = @"
                            Swal.fire({
                                title: 'La empresa convenio se creó de forma exitosa',
                                text: '',
                                icon: 'success',
                                timer: 3000, // 3 segundos
                                showConfirmButton: false,
                                timerProgressBar: true
                            }).then(() => {
                                window.location.href = 'empresasafiliadas';
                            });
                            ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);

                    cg.InsertarLog(Session["idusuario"].ToString(), "empresas afiliadas", "Nuevo", "El usuario creó una nueva empresa convenio con documento: " + txbDocumento.Text.ToString() + ".", "", "");
                }
            }
            catch (SqlException ex)
            {
                mensaje = ex.Message;
            }
        }
    }
}