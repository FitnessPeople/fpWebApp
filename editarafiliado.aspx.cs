using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace fpWebApp
{
    public partial class editarafiliado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Especialistas");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        txbDocumento.Attributes.Add("type", "number");
                        txbTelefono.Attributes.Add("type", "number");
                        txbFechaNac.Attributes.Add("type", "date");
                        txbTelefonoContacto.Attributes.Add("type", "number");
                        CargarTipoDocumento();
                        CargarCiudad();
                        CargarEmpresas();
                        CargarEstadoCivil();
                        CargarEps();
                        CargarProfesiones();
                        CargarSedes();
                        CargarGeneros();
                        CargarAfiliado();
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
            string strQuery = "SELECT * FROM tiposDocumento";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlTipoDocumento.DataSource = dt;
            ddlTipoDocumento.DataBind();

            dt.Dispose();
        }

        private void CargarCiudad()
        {
            string strQuery = "SELECT * FROM Ciudades WHERE CodigoPais = 'Co' " +
                "AND CodigoEstado = 68 " +
                "ORDER BY NombreCiudad";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlCiudadAfiliado.DataSource = dt;
            ddlCiudadAfiliado.DataBind();

            dt.Dispose();
        }

        private void CargarEmpresas()
        {
            string strQuery = "SELECT idEmpresaAfiliada, RazonSocial FROM EmpresasAfiliadas " +
                "ORDER BY RazonSocial";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlEmpresaConvenio.DataSource = dt;
            ddlEmpresaConvenio.DataBind();

            dt.Dispose();
        }

        private void CargarEstadoCivil()
        {
            string strQuery = "SELECT * FROM estadocivil";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlEstadoCivil.DataSource = dt;
            ddlEstadoCivil.DataBind();

            dt.Dispose();
        }

        private void CargarEps()
        {
            string strQuery = "SELECT * FROM eps";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlEps.DataSource = dt;
            ddlEps.DataBind();

            dt.Dispose();
        }

        private void CargarProfesiones()
        {
            string strQuery = "SELECT * FROM profesiones";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlProfesiones.DataSource = dt;
            ddlProfesiones.DataBind();

            dt.Dispose();
        }

        private void CargarSedes()
        {
            string strQuery = "SELECT s.idSede, CONCAT(s.NombreSede, \" - \", cs.NombreCiudadSede) AS NombreSede " +
                "FROM sedes s " +
                "LEFT JOIN CiudadesSedes cs ON s.idCiudadSede = cs.idCiudadSede " +
                "ORDER BY s.idCiudadSede, NombreSede";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlSedes.DataSource = dt;
            ddlSedes.DataBind();

            dt.Dispose();
        }

        private void CargarGeneros()
        {
            string strQuery = "SELECT * FROM generos ORDER BY idGenero";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlGenero.DataSource = dt;
            ddlGenero.DataBind();

            dt.Dispose();
        }

        private void CargarAfiliado()
        {
            if (Request.QueryString.Count > 0)
            {
                string strQuery = "SELECT * FROM afiliados WHERE idAfiliado = " + Request.QueryString["editid"].ToString();
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(strQuery);

                if (dt.Rows.Count > 0)
                {
                    txbNombre.Text = dt.Rows[0]["NombreAfiliado"].ToString();
                    txbApellido.Text = dt.Rows[0]["ApellidoAfiliado"].ToString();
                    txbDocumento.Text = dt.Rows[0]["DocumentoAfiliado"].ToString();
                    ddlTipoDocumento.SelectedIndex = Convert.ToInt16(dt.Rows[0]["idTipoDocumento"].ToString());
                    txbTelefono.Text = dt.Rows[0]["CelularAfiliado"].ToString();
                    txbEmail.Text = dt.Rows[0]["EmailAfiliado"].ToString();
                    txbDireccion.Text = dt.Rows[0]["DireccionAfiliado"].ToString();
                    ddlCiudadAfiliado.SelectedIndex = Convert.ToInt16(ddlCiudadAfiliado.Items.IndexOf(ddlCiudadAfiliado.Items.FindByValue(dt.Rows[0]["idCiudadAfiliado"].ToString())));
                    ddlEmpresaConvenio.SelectedIndex = Convert.ToInt16(ddlEmpresaConvenio.Items.IndexOf(ddlEmpresaConvenio.Items.FindByValue(dt.Rows[0]["idEmpresaAfil"].ToString())));
                    txbFechaNac.Attributes.Add("type", "date");

                    DateTime dt14 = DateTime.Now.AddYears(-14);
                    DateTime dt100 = DateTime.Now.AddYears(-100);
                    txbFechaNac.Attributes.Add("min", dt100.Year.ToString() + "-" + String.Format("{0:MM}", dt100) + "-" + String.Format("{0:dd}", dt100));
                    txbFechaNac.Attributes.Add("max", dt14.Year.ToString() + "-" + String.Format("{0:MM}", dt14) + "-" + String.Format("{0:dd}", dt14));
                    DateTime dtFecha = new DateTime();
                    if (dt.Rows[0]["FechaNacAfiliado"].ToString() != "1900-01-00")
                    {
                        dtFecha = Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"].ToString());
                    }

                    txbFechaNac.Text = dtFecha.ToString("yyyy-MM-dd");

                    if (dt.Rows[0]["FotoAfiliado"].ToString() != "")
                    {
                        imgFoto.ImageUrl = "img/afiliados/" + dt.Rows[0]["FotoAfiliado"].ToString();
                        ViewState["FotoAfiliado"] = dt.Rows[0]["FotoAfiliado"].ToString();
                    }
                    if (dt.Rows[0]["idGenero"].ToString() != "")
                    {
                        ddlGenero.SelectedIndex = Convert.ToInt16(ddlGenero.Items.IndexOf(ddlGenero.Items.FindByValue(dt.Rows[0]["idGenero"].ToString())));
                    }
                    if (dt.Rows[0]["idEstadoCivilAfiliado"].ToString() != "")
                    {
                        ddlEstadoCivil.SelectedIndex = Convert.ToInt16(ddlEstadoCivil.Items.IndexOf(ddlEstadoCivil.Items.FindByValue(dt.Rows[0]["idEstadoCivilAfiliado"].ToString())));
                    }
                    if (dt.Rows[0]["idProfesion"].ToString() != "")
                    {
                        ddlProfesiones.SelectedIndex = Convert.ToInt16(ddlProfesiones.Items.IndexOf(ddlProfesiones.Items.FindByValue(dt.Rows[0]["idProfesion"].ToString())));
                    }
                    if (dt.Rows[0]["idEps"].ToString() != "")
                    {
                        ddlEps.SelectedIndex = Convert.ToInt16(ddlEps.Items.IndexOf(ddlEps.Items.FindByValue(dt.Rows[0]["idEps"].ToString())));
                    }
                    if (dt.Rows[0]["idSede"].ToString() != "")
                    {
                        ddlSedes.SelectedIndex = Convert.ToInt16(ddlSedes.Items.IndexOf(ddlSedes.Items.FindByValue(dt.Rows[0]["idSede"].ToString())));
                    }
                    if (dt.Rows[0]["Parentesco"].ToString() != "")
                    {
                        ddlParentesco.SelectedIndex = Convert.ToInt16(ddlParentesco.Items.IndexOf(ddlParentesco.Items.FindByText(dt.Rows[0]["Parentesco"].ToString())));
                    }
                    txbResponsable.Text = dt.Rows[0]["ResponsableAfiliado"].ToString();
                    txbTelefonoContacto.Text = dt.Rows[0]["ContactoAfiliado"].ToString();
                    rblEstado.Items.FindByValue(dt.Rows[0]["EstadoAfiliado"].ToString()).Selected = true;
                }
                else
                {
                    divMensaje1.Visible = true;
                    btnActualizar.Visible = false;
                }

                dt.Dispose();
            }
            else
            {
                divMensaje1.Visible = true;
                btnActualizar.Visible = false;
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            string strFilename = "";
            // Actualiza la tabla Afiliados
            if (ViewState["FotoAfiliado"] != null)
            {
                strFilename = ViewState["FotoAfiliado"].ToString();
            }
            else
            {
                strFilename = "nofoto.png";
            }

            HttpPostedFile postedFile = Request.Files["fileFoto"];

            if (postedFile != null && postedFile.ContentLength > 0)
            {
                //Borrar la foto del afiliado si tiene una.
                if (ViewState["FotoAfiliado"] != null)
                {
                    if (ViewState["FotoAfiliado"].ToString() != "nofoto.png")
                    {
                        string strPhysicalFolder = Server.MapPath("img//afiliados//");
                        string strFileFullPath = strPhysicalFolder + ViewState["FotoAfiliado"].ToString();

                        if (File.Exists(strFileFullPath))
                        {
                            File.Delete(strFileFullPath);
                        }
                    }
                }

                //Guardar la foto del afiliado
                string filePath = Server.MapPath("img//afiliados//") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                strFilename = postedFile.FileName;
            }

            OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
            string strInitData = TraerData();
            try
            {
                string strQuery = "UPDATE afiliados SET " +
                "idTipoDocumento = " + ddlTipoDocumento.SelectedItem.Value.ToString() + ", NombreAfiliado = '" + txbNombre.Text.ToString().Replace("'", "").Replace("<", "").Replace(">", "").Trim() + "', " +
                "ApellidoAfiliado = '" + txbApellido.Text.ToString() + "', CelularAfiliado = '" + txbTelefono.Text.ToString() + "', " +
                "EmailAfiliado = '" + txbEmail.Text.ToString() + "', DireccionAfiliado = '" + txbDireccion.Text.ToString() + "', " +
                "idCiudadAfiliado = " + ddlCiudadAfiliado.SelectedItem.Value.ToString() + ", FechaNacAfiliado = '" + txbFechaNac.Text.ToString() + "', " +
                "FotoAfiliado = '" + strFilename + "', idGenero = " + ddlGenero.SelectedItem.Value.ToString() + ", " +
                "idEstadoCivilAfiliado = " + ddlEstadoCivil.SelectedItem.Value.ToString() + ", idProfesion = " + ddlProfesiones.SelectedItem.Value.ToString() + ", " +
                "idEmpresaAfil = " + ddlEmpresaConvenio.SelectedItem.Value.ToString() + ", " +
                "idEps = " + ddlEps.SelectedItem.Value.ToString() + ", idSede = " + ddlSedes.SelectedItem.Value.ToString() + ", " +
                "ResponsableAfiliado = '" + txbResponsable.Text.ToString() + "', Parentesco = '" + ddlParentesco.SelectedItem.Value.ToString() + "', " +
                "ContactoAfiliado = '" + txbTelefonoContacto.Text.ToString() + "' " +
                "WHERE idAfiliado = " + Request.QueryString["editid"].ToString();
                OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                myConnection.Open();
                command.ExecuteNonQuery();
                command.Dispose();
                myConnection.Close();

                string strNewData = TraerData();

                clasesglobales cg = new clasesglobales();
                cg.InsertarLog(Session["idusuario"].ToString(), "afiliados", "Modifica", "El usuario modificó datos al afiliado con documento " + txbDocumento.Text.ToString() + ".", strInitData, strNewData);
            }
            catch (OdbcException ex)
            {
                string mensaje = ex.Message;
                myConnection.Close();
            }

            Response.Redirect("afiliados");
        }

        private string TraerData()
        {
            string strQuery = "SELECT * FROM afiliados WHERE idAfiliado = " + Request.QueryString["editid"].ToString();
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
