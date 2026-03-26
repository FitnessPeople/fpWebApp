using iTextSharp.text.pdf.codec.wmf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;

namespace fpWebApp
{
    public partial class editarempresaafiliada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Empresas convenio");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        txbTelefonoPpal.Attributes.Add("type", "number");
                        //txbTelefonoSrio.Attributes.Add("type", "number");
                        //txbCelular.Attributes.Add("type", "number");
                        //txbFechaConvenio.Attributes.Add("type", "date");
                        //txbFechaFinConvenio.Attributes.Add("type", "date");
                        //txbNroEmpleados.Attributes.Add("type", "number");
                        CargarTipoDocumento();
                        CargarCiudad();
                        CargarEmpresa();
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

        private void CargarEmpresa()
        {
            if (Request.QueryString.Count > 0)
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarEmpresaAfiliadaPorId(Convert.ToInt32(Request.QueryString["editid"].ToString()));
                string contenidoEditor = hiddenEditor.Value;

                if (dt.Rows.Count > 0)
                {
                    txbNombreCcial.Text = dt.Rows[0]["NombreComercial"].ToString();
                    txbRazonSocial.Text = dt.Rows[0]["RazonSocial"].ToString();
                    txbDocumento.Text = dt.Rows[0]["DocumentoEmpresa"].ToString();
                    ddlTipoDocumento.SelectedIndex = Convert.ToInt16(dt.Rows[0]["idTipoDocumento"].ToString());
                    txbDV.Text = dt.Rows[0]["digitoverificacion"].ToString();
                    txbNombreContacto.Text = dt.Rows[0]["NombreContacto"].ToString();
                    txbCargoContacto.Text = dt.Rows[0]["CargoContacto"].ToString();
                    txbTelefonoPpal.Text = dt.Rows[0]["CelularEmpresa"].ToString();
                    txbCorreo.Text = dt.Rows[0]["CorreoEmpresa"].ToString();

                    hiddenEditor.Value = dt.Rows[0]["Descripcion"].ToString();
                    txbDireccion.Text = dt.Rows[0]["DireccionEmpresa"].ToString();
                    ddlCiudadEmpresa.SelectedIndex = Convert.ToInt16(ddlCiudadEmpresa.Items.IndexOf(ddlCiudadEmpresa.Items.FindByValue(dt.Rows[0]["idCiudadEmpresa"].ToString())));

                    string estado = dt.Rows[0]["EstadoEmpresa"].ToString();

                    if (rblEstado.Items.FindByValue(estado) != null)
                    {
                        rblEstado.SelectedValue = estado;
                    }




                }
                else
                {
                    //divMensaje1.Visible = true;
                    btnActualizar.Visible = false;
                }

                dt.Dispose();
            }
            else
            {
                //divMensaje1.Visible = true;
                btnActualizar.Visible = false;
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {


            clasesglobales cg1 = new clasesglobales();
            string respuesta = string.Empty;
            try
            {
                DataTable dt = cg1.ConsultarEmpresaAfiliadaPorId(Convert.ToInt32(Request.QueryString["editid"].ToString()));
                string strInitData = TraerData();

                DateTime hoy = DateTime.Now;

                string estado = rblEstado.SelectedValue;



                clasesglobales cg = new clasesglobales();
                respuesta = cg.EditarEmpresaAfiliada(Convert.ToInt32(Request.QueryString["editid"].ToString()), txbDocumento.Text.Trim(), Convert.ToInt32(ddlTipoDocumento.SelectedValue),
                    txbNombreCcial.Text.Trim().ToUpper(), txbRazonSocial.Text.Trim().ToUpper(), txbNombreContacto.Text.Trim().ToUpper(),  txbCargoContacto.Text.Trim().ToUpper(),
                    txbTelefonoPpal.Text.Trim(), txbCorreo.Text.Trim(), txbDireccion.Text.Trim(), Convert.ToInt32(ddlCiudadEmpresa.SelectedValue), "", estado,
                    txbDV.Text.Trim(), hiddenEditor.Value, Convert.ToInt32(Session["idUsuario"]) );

                string strNewData = TraerData();

                cg.InsertarLog(Session["idusuario"].ToString(), "empresas convenio", "Modifica", "El usuario modificó datos a la empresa afiliada con documento: " + txbDocumento.Text.ToString() + ".", strInitData, strNewData);

                if (respuesta != "OK")
                {
                    string script = @"
                            Swal.fire({
                                title: 'Error',
                                text: '" + respuesta.Replace("'", "\\'") + @"',
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
                                title: 'La empresa se actualizó de forma exitosa',
                                text: 'Corporativo - Fitness People',
                                icon: 'success',
                                timer: 3000, // 3 segundos
                                showConfirmButton: false,
                                timerProgressBar: true
                            }).then(() => {
                                window.location.href = 'empresasconvenio';
                            });
                            ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);

                    cg.InsertarLog(Session["idusuario"].ToString(), "empresas convenio", "Nuevo", "El usuario creó una nueva empresa convenio con documento: " + txbDocumento.Text.ToString() + ".", "", "");
                }
            }
            catch (SqlException ex)
            {
                respuesta = ex.Message;
            }
        }


        private string TraerData()
        {

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarEmpresaAfiliadaPorId(Convert.ToInt32(Request.QueryString["editid"].ToString()));

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();

            return strData;
        }
    }
}