using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                    ValidarPermisos("Empresas afiliadas");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        txbTelefonoPpal.Attributes.Add("type", "number");
                        txbTelefonoSrio.Attributes.Add("type", "number");
                        txbCelular.Attributes.Add("type", "number");
                        txbFechaConvenio.Attributes.Add("type", "date");
                        txbNroEmpleados.Attributes.Add("type", "number");
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
                string strQuery = "SELECT * FROM EmpresasAfiliadas WHERE idEmpresaAfiliada = " + Request.QueryString["editid"].ToString();
                clasesglobales cg1 = new clasesglobales();
                DataTable dt = cg1.TraerDatos(strQuery);

                if (dt.Rows.Count > 0)
                {
                    txbNombreCcial.Text = dt.Rows[0]["NombreComercial"].ToString();
                    txbRazonSocial.Text = dt.Rows[0]["RazonSocial"].ToString();
                    txbDocumento.Text = dt.Rows[0]["DocumentoEmpresa"].ToString();
                    ddlTipoDocumento.SelectedIndex = Convert.ToInt16(dt.Rows[0]["idTipoDocumento"].ToString());
                    txbTelefonoPpal.Text = dt.Rows[0]["TelefonoPpal"].ToString();
                    txbTelefonoSrio.Text = dt.Rows[0]["TelefonoSrio"].ToString();
                    txbCelular.Text = dt.Rows[0]["CelularEmpresa"].ToString();
                    txbCorreo.Text = dt.Rows[0]["CorreoEmpresa"].ToString();
                    txbDireccion.Text = dt.Rows[0]["DireccionEmpresa"].ToString();
                    ddlCiudadEmpresa.SelectedIndex = Convert.ToInt16(ddlCiudadEmpresa.Items.IndexOf(ddlCiudadEmpresa.Items.FindByValue(dt.Rows[0]["idCiudadEmpresa"].ToString())));
                    txbFechaConvenio.Attributes.Add("type", "date");

                    DateTime dtFecha = Convert.ToDateTime(dt.Rows[0]["FechaConvenio"].ToString());
                    txbFechaConvenio.Text = dtFecha.ToString("yyyy-MM-dd");
                    txbNroEmpleados.Text = dt.Rows[0]["NroEmpleados"].ToString();
                    ddlTipoNegociacion.SelectedIndex = Convert.ToInt16(ddlTipoNegociacion.Items.IndexOf(ddlTipoNegociacion.Items.FindByText(dt.Rows[0]["TipoNegociacion"].ToString())));
                    ddlDiasCredito.SelectedIndex = Convert.ToInt16(ddlDiasCredito.Items.IndexOf(ddlDiasCredito.Items.FindByText(dt.Rows[0]["DiasCredito"].ToString())));

                    if (dt.Rows[0]["Contrato"].ToString() != "")
                    {
                        //imgFoto.ImageUrl = "img/afiliados/" + dt.Rows[0]["FotoAfiliado"].ToString();
                        ViewState["Contrato"] = dt.Rows[0]["Contrato"].ToString();
                        ltContrato.Text = "<a class=\"btn btn-block btn-social btn-reddit dropdown-toggle\" data-toggle=\"modal\" " +
                            "href=\"#\" data-target=\"#myModal2\" " +
                            "data-file=\"" + ViewState["Contrato"].ToString() + "\">" +
                            "<span class=\"fa fa-file-pdf\"></span> " + ViewState["Contrato"].ToString() + " " +
                            "<a>";
                    }
                    rblEstado.Items.FindByValue(dt.Rows[0]["EstadoEmpresa"].ToString()).Selected = true;
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
            string strFilename = "";
            // Actualiza la tabla EmpresasAfiliadas
            if (ViewState["Contrato"] != null)
            {
                strFilename = ViewState["Contrato"].ToString();
            }

            HttpPostedFile postedFile = Request.Files["fileConvenio"];

            if (postedFile != null && postedFile.ContentLength > 0)
            {
                //Save the File.
                string filePath = Server.MapPath("img//contratos//") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                strFilename = postedFile.FileName;
            }

            string strInitData = TraerData();
            try
            {
                string strQuery = "UPDATE EmpresasAfiliadas SET " +
                    "NombreComercial = '" + txbNombreCcial.Text.ToString().Replace("'", "").Replace("<", "").Replace(">", "").Replace("=", "").Trim() + "', " +
                    "RazonSocial = '" + txbRazonSocial.Text.ToString().Replace("'", "").Replace("<", "").Replace(">", "").Replace("=", "").Trim() + "', " +
                    "DocumentoEmpresa = '" + txbDocumento.Text.ToString().Trim() + "', " +
                    "idTipoDocumento = " + ddlTipoDocumento.SelectedItem.Value.ToString() + ", " +
                    "TelefonoPpal = '" + txbTelefonoPpal.Text.ToString().Replace("e", "").Replace("+", "").Replace("-", "").Replace(".", "") + "', " +
                    "TelefonoSrio = '" + txbTelefonoSrio.Text.ToString().Replace("e", "").Replace("+", "").Replace("-", "").Replace(".", "") + "', " +
                    "CelularEmpresa = '" + txbCelular.Text.ToString().Replace("e", "").Replace("+", "").Replace("-", "").Replace(".", "") + "', " +
                    "CorreoEmpresa = '" + txbCorreo.Text.ToString().Trim() + "', " +
                    "DireccionEmpresa = '" + txbDireccion.Text.ToString().Trim() + "', " +
                    "idCiudadEmpresa = '" + ddlCiudadEmpresa.SelectedItem.Value.ToString() + "', " +
                    "FechaConvenio = '" + txbFechaConvenio.Text.ToString() + "', " +
                    "NroEmpleados = " + txbNroEmpleados.Text.ToString().Replace("e", "").Replace("+", "").Replace("-", "").Replace(".", "") + ", " +
                    "Contrato = '" + strFilename + "', " +
                    "TipoNegociacion = '" + ddlTipoNegociacion.SelectedItem.Value.ToString() + "', " +
                    "DiasCredito = '" + ddlDiasCredito.SelectedItem.Value.ToString() + "', " +
                    "EstadoEmpresa = '" + rblEstado.Text.ToString() + "' " +
                    "WHERE idEmpresaAfiliada = " + Request.QueryString["editid"].ToString();

                clasesglobales cg = new clasesglobales();
                string mensaje = cg.TraerDatosStr(strQuery);
                
                string strNewData = TraerData();

                
                cg.InsertarLog(Session["idusuario"].ToString(), "empresas afiliadas", "Modifica", "El usuario modificó datos a la empresa afiliada con documento: " + txbDocumento.Text.ToString() + ".", strInitData, strNewData);
            }
            catch (OdbcException ex)
            {
                string mensaje = ex.Message;               
            }

            Response.Redirect("empresasafiliadas");
        }

        private string TraerData()
        {
            string strQuery = "SELECT * FROM EmpresasAfiliadas WHERE idEmpresaAfiliada = " + Request.QueryString["editid"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

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