﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

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
                            txbTelefonoSrio.Attributes.Add("type", "number");
                            txbCelular.Attributes.Add("type", "number");
                            txbFechaConvenio.Attributes.Add("type", "date");
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
            string strFilename = "";
            HttpPostedFile postedFile = Request.Files["fileConvenio"];

            if (postedFile != null && postedFile.ContentLength > 0)
            {
                //Save the File.
                string filePath = Server.MapPath("docs//contratos//") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                strFilename = postedFile.FileName;
            }

            clasesglobales cg = new clasesglobales();

            try
            {
                string strQuery = "INSERT INTO EmpresasAfiliadas " +
                "(DocumentoEmpresa, idTipoDocumento, NombreComercial, RazonSocial, FechaConvenio, TelefonoPpal, TelefonoSrio, " +
                "CelularEmpresa, CorreoEmpresa, DireccionEmpresa, idCiudadEmpresa, NroEmpleados, TipoNegociacion, DiasCredito, " +
                "Contrato, EstadoEmpresa) " +
                "VALUES ('" + txbDocumento.Text.ToString() + "', " + ddlTipoDocumento.SelectedItem.Value.ToString() + ", " +
                "'" + txbNombreCcial.Text.ToString() + "', '" + txbRazonSocial.Text.ToString() + "', " +
                "'" + txbFechaConvenio.Text.ToString() + "', '" + txbTelefonoPpal.Text.ToString() + "', " +
                "'" + txbTelefonoSrio.Text.ToString() + "', '" + txbCelular.Text.ToString() + "', " +
                "'" + txbCorreo.Text.ToString() + "', '" + txbDireccion.Text.ToString() + "', " +
                "" + ddlCiudadEmpresa.SelectedItem.Value.ToString() + ", " + txbNroEmpleados.Text.ToString() + ", " +
                "'" + ddlTipoNegociacion.SelectedItem.Value.ToString() + "', '" + ddlDiasCredito.SelectedItem.Value.ToString() + "', " +
                "'" + strFilename + "', 'Activo') ";
                string mensaje = cg.TraerDatosStr(strQuery);
                cg.InsertarLog(Session["idusuario"].ToString(), "empresas afiliadas", "Nuevo", "El usuario creó una nueva empresa convenio con documento: " + txbDocumento.Text.ToString() + ".", "", "");
            }
            catch (SqlException ex)
            {
                string mensaje = ex.Message;
            }

            Response.Redirect("empresasafiliadas");
        }
    }
}