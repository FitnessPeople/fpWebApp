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
    public partial class editarespecialista : System.Web.UI.Page
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
                        CargarTipoDocumento();
                        CargarCiudad();
                        CargarEstadoCivil();
                        CargarEps();
                        CargarProfesiones();
                        CargarSedes();
                        CargarGeneros();
                        CargarEspecialista();
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
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlTipoDocumento.DataSource = dt;
            ddlTipoDocumento.DataBind();

            dt.Dispose();
        }

        private void CargarCiudad()
        {
            string strQuery = "SELECT idCiudad, CONCAT(NombreCiudad, ' - ', NombreEstado) AS NombreCiudad FROM Ciudades " +
                "WHERE CodigoPais = 'Co' " +
                "ORDER BY NombreCiudad";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlCiudadEspecialista.DataSource = dt;
            ddlCiudadEspecialista.DataBind();

            dt.Dispose();
        }

        private void CargarEstadoCivil()
        {
            string strQuery = "SELECT * FROM estadocivil";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlEstadoCivil.DataSource = dt;
            ddlEstadoCivil.DataBind();

            dt.Dispose();
        }

        private void CargarEps()
        {
            string strQuery = "SELECT * FROM eps";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlEps.DataSource = dt;
            ddlEps.DataBind();

            dt.Dispose();
        }

        private void CargarProfesiones()
        {
            string strQuery = "SELECT * FROM profesiones WHERE area = 'Salud'";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlProfesiones.DataSource = dt;
            ddlProfesiones.DataBind();

            dt.Dispose();
        }

        private void CargarSedes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultaCargarSedes("Gimnasio");

            ddlSedes.DataSource = dt;
            ddlSedes.DataBind();

            dt.Dispose();
        }

        private void CargarGeneros()
        {
            string strQuery = "SELECT * FROM generos ORDER BY idGenero";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlGenero.DataSource = dt;
            ddlGenero.DataBind();

            dt.Dispose();
        }

        private void CargarEspecialista()
        {
            if (Request.QueryString.Count > 0)
            {
                string strQuery = "SELECT * FROM Especialistas WHERE idEspecialista = " + Request.QueryString["editid"].ToString();
                clasesglobales cg1 = new clasesglobales();
                DataTable dt = cg1.TraerDatos(strQuery);

                if (dt.Rows.Count > 0)
                {
                    txbNombre.Text = dt.Rows[0]["NombreEspecialista"].ToString();
                    txbApellido.Text = dt.Rows[0]["ApellidoEspecialista"].ToString();
                    txbDocumento.Text = dt.Rows[0]["DocumentoEspecialista"].ToString();
                    ddlTipoDocumento.SelectedIndex = Convert.ToInt16(dt.Rows[0]["idTipoDocumento"].ToString());
                    txbTelefono.Text = dt.Rows[0]["CelularEspecialista"].ToString();
                    txbEmail.Text = dt.Rows[0]["EmailEspecialista"].ToString();
                    txbDireccion.Text = dt.Rows[0]["DireccionEspecialista"].ToString();
                    ddlCiudadEspecialista.SelectedIndex = Convert.ToInt16(ddlCiudadEspecialista.Items.IndexOf(ddlCiudadEspecialista.Items.FindByValue(dt.Rows[0]["idCiudadEspecialista"].ToString())));
                    txbFechaNac.Attributes.Add("type", "date");

                    DateTime dtFecha = Convert.ToDateTime(dt.Rows[0]["FechaNacEspecialista"].ToString());
                    txbFechaNac.Text = dtFecha.ToString("yyyy-MM-dd");

                    if (dt.Rows[0]["FotoEspecialista"].ToString() != "")
                    {
                        imgFoto.ImageUrl = "img/especialistas/" + dt.Rows[0]["FotoEspecialista"].ToString();
                        ViewState["FotoEspecialista"] = dt.Rows[0]["FotoEspecialista"].ToString();
                    }
                    if (dt.Rows[0]["idGenero"].ToString() != "")
                    {
                        ddlGenero.SelectedIndex = Convert.ToInt16(ddlGenero.Items.IndexOf(ddlGenero.Items.FindByValue(dt.Rows[0]["idGenero"].ToString())));
                    }
                    if (dt.Rows[0]["idEstadoCivilEspecialista"].ToString() != "")
                    {
                        ddlEstadoCivil.SelectedIndex = Convert.ToInt16(ddlEstadoCivil.Items.IndexOf(ddlEstadoCivil.Items.FindByValue(dt.Rows[0]["idEstadoCivilEspecialista"].ToString())));
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
                    rblEstado.Items.FindByValue(dt.Rows[0]["EstadoEspecialista"].ToString()).Selected = true;
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
            // Actualiza la tabla Especialistas
            if (ViewState["FotoEspecialista"] != null)
            {
                strFilename = ViewState["FotoEspecialista"].ToString();
            }
            else
            {
                strFilename = "nofoto.png";
            }

            HttpPostedFile postedFile = Request.Files["fileFoto"];

            if (postedFile != null && postedFile.ContentLength > 0)
            {
                //Save the File.
                string filePath = Server.MapPath("img//especialistas//") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                strFilename = postedFile.FileName;
            }

            try
            {
                string strQuery = "UPDATE Especialistas SET " +
                "idTipoDocumento = " + ddlTipoDocumento.SelectedItem.Value.ToString() + ", NombreEspecialista = '" + txbNombre.Text.ToString().Replace("'", "").Replace("<", "").Replace(">", "").Trim() + "', " +
                "ApellidoEspecialista = '" + txbApellido.Text.ToString() + "', CelularEspecialista = '" + txbTelefono.Text.ToString() + "', " +
                "EmailEspecialista = '" + txbEmail.Text.ToString() + "', DireccionEspecialista = '" + txbDireccion.Text.ToString() + "', " +
                "idCiudadEspecialista = " + ddlCiudadEspecialista.SelectedItem.Value.ToString() + ", FechaNacEspecialista = '" + txbFechaNac.Text.ToString() + "', " +
                "FotoEspecialista = '" + strFilename + "', idGenero = " + ddlGenero.SelectedItem.Value.ToString() + ", " +
                "idEstadoCivilEspecialista = " + ddlEstadoCivil.SelectedItem.Value.ToString() + ", idProfesion = " + ddlProfesiones.SelectedItem.Value.ToString() + ", " +
                "idEps = " + ddlEps.SelectedItem.Value.ToString() + ", idSede = " + ddlSedes.SelectedItem.Value.ToString() + ", " +
                "EstadoEspecialista = '" + rblEstado.Text.ToString() + "' " +
                "WHERE idEspecialista = " + Request.QueryString["editid"].ToString();

                clasesglobales cg = new clasesglobales();
                string mensaje = cg.TraerDatosStr(strQuery);
            }
            catch (OdbcException ex)
            {
                string mensaje = ex.Message;               
            }

            Response.Redirect("especialistas");
        }
    }
}