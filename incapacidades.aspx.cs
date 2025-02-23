using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace fpWebApp
{
    public partial class incapacidades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Incapacidades");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1" && ViewState["CrearModificar"].ToString() == "1")
                    {
                        divAfiliado.Visible = false;
                        divPlanes.Visible = false;

                        ddlTipoIncapacidad.Enabled = false;
                        txbObservaciones.Enabled = false;
                        txbFechaInicio.Enabled = false;
                        btnSolicitarIncapacidad.Enabled = false;
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

        protected void btnAfiliado_Click(object sender, EventArgs e)
        {
            string[] strDocumento = txbAfiliado.Text.ToString().Split('-');
            string strQuery = "SELECT * FROM Afiliados a " +
                "RIGHT JOIN Sedes s ON a.idSede = s.idSede " +
                "WHERE DocumentoAfiliado = '" + strDocumento[0].Trim() + "' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                divAfiliado.Visible = true;
                ViewState["idAfiliado"] = dt.Rows[0]["idAfiliado"].ToString();
                ViewState["DocumentoAfiliado"] = dt.Rows[0]["DocumentoAfiliado"].ToString();
                ltNombre.Text = dt.Rows[0]["NombreAfiliado"].ToString();
                ltApellido.Text = dt.Rows[0]["ApellidoAfiliado"].ToString();
                ltEmail.Text = dt.Rows[0]["EmailAfiliado"].ToString();
                ltCelular.Text = dt.Rows[0]["CelularAfiliado"].ToString();
                ltSede.Text = dt.Rows[0]["NombreSede"].ToString();

                if (dt.Rows[0]["FechaNacAfiliado"].ToString() != "1900-01-00")
                {
                    ltCumple.Text = String.Format("{0:dd MMM}", Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"]));
                }
                else
                {
                    ltCumple.Text = "-";
                }

                if (dt.Rows[0]["FotoAfiliado"].ToString() != "")
                {
                    ltFoto.Text = "<img src=\"img/afiliados/" + dt.Rows[0]["FotoAfiliado"].ToString() + "\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                }
                else
                {
                    if (dt.Rows[0]["idGenero"].ToString() == "1" || dt.Rows[0]["idGenero"].ToString() == "3")
                    {
                        ltFoto.Text = "<img src=\"img/afiliados/avatar_male.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                    }
                    if (dt.Rows[0]["idGenero"].ToString() == "2")
                    {
                        ltFoto.Text = "<img src=\"img/afiliados/avatar_female.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                    }
                }

                divPlanes.Visible = true;
                CargarIncapacidades();
                CargarPlanesAfiliado();
                CargarTiposIncapacidades();
            }
            dt.Dispose();
        }

        private void CargarIncapacidades()
        {
            string strQuery = "SELECT * " +
                "FROM incapacidades i, afiliadosplanes ap " +
                "WHERE ap.idAfiliado = " + ViewState["idAfiliado"].ToString() + " " +
                "AND ap.idAfiliadoPlan = i.idAfiliadoPlan " +
                "AND i.Estado = 'En proceso'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                //ltNoPlanes.Text = "Existe una incapacidad en proceso. No es posible agregar otra incapacidad.";
                ltMensaje.Text = "<div class=\"ibox-content\">" +
                    "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Existe una incapacidad en proceso. No es posible agregar otra incapacidad." +
                    "</div></div>";
                txbFechaInicio.Enabled = false;
                btnSolicitarIncapacidad.Enabled = false;
            }

            dt.Dispose();
        }

        private void CargarPlanesAfiliado()
        {
            //string strQuery = "SELECT *, " +
            //    "DATEDIFF(FechaFinalPlan, CURDATE()) AS diasquefaltan, " +
            //    "DATEDIFF(CURDATE(), FechaInicioPlan) AS diasconsumidos, " +
            //    "DATEDIFF(FechaFinalPlan, FechaInicioPlan) AS diastotales, " +
            //    "ROUND(DATEDIFF(CURDATE(), FechaInicioPlan) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje1, " +
            //    "ROUND(DATEDIFF(FechaFinalPlan, CURDATE()) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje2, " +
            //    "ROUND(ap.Meses * p.DiasCongelamientoMes) as DiasCongelamiento " +
            //    "FROM AfiliadosPlanes ap, Afiliados a, Planes p " +
            //    "WHERE a.idAfiliado = " + ViewState["idAfiliado"].ToString() + " " +
            //    "AND ap.idAfiliado = a.idAfiliado " +
            //    "AND ap.idPlan = p.idPlan " +
            //    "AND ap.EstadoPlan = 'Activo'";
            //clasesglobales cg = new clasesglobales();
            //DataTable dt = cg.TraerDatos(strQuery);

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.CargarPlanesAfiliado(ViewState["idAfiliado"].ToString(), "Activo");

            if (dt.Rows.Count > 0)
            {
                ViewState["idAfiliadoPlan"] = dt.Rows[0]["idAfiliadoPlan"].ToString();

                rpPlanesAfiliado.DataSource = dt;
                rpPlanesAfiliado.DataBind();

                txbFechaInicio.Enabled = true;
                hfDiasAfiliado.Value = dt.Rows[0]["DiasCongelamiento"].ToString();

                DateTime dtHoy = DateTime.Now;
                DateTime dtFechaFinal = (DateTime)dt.Rows[0]["FechaFinalPlan"];
                txbFechaInicio.Attributes.Add("min", dtHoy.Year.ToString() + "-" + String.Format("{0:MM}", dtHoy) + "-" + String.Format("{0:dd}", dtHoy));
                txbFechaInicio.Attributes.Add("max", dtFechaFinal.Year.ToString() + "-" + String.Format("{0:MM}", dtFechaFinal) + "-" + String.Format("{0:dd}", dtFechaFinal));

                txbFechaInicio.Attributes.Add("type", "date");
                txbFechaInicio.Enabled = true;

                ddlTipoIncapacidad.Enabled = true;
                txbObservaciones.Enabled = true;
                txbFechaInicio.Enabled = true;
                btnSolicitarIncapacidad.Enabled = true;
            }
            else
            {
                ltNoPlanes.Text = "Sin planes. No es posible agregar una incapacidad.";
                ddlTipoIncapacidad.Enabled = false;
                txbObservaciones.Enabled = false;
                txbFechaInicio.Enabled = false;
                btnSolicitarIncapacidad.Enabled = false;
            }

            dt.Dispose();
        }

        private void CargarTiposIncapacidades()
        {
            string strQuery = "SELECT * FROM TiposIncapacidad WHERE idTipoIncapacidad = 1 ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlTipoIncapacidad.DataSource = dt;
            ddlTipoIncapacidad.DataBind();

            dt.Dispose();
        }

        protected void btnSolicitarIncapacidad_Click(object sender, EventArgs e)
        {
            if (ddlTipoIncapacidad.SelectedItem.Value.ToString() == "")
            {
                ltValidacion.Text = "<div class=\"ibox-content\">" +
                    "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Seleccione el tipo de congelación." +
                    "</div></div>";
            }
            else
            {
                if (txbFechaInicio.Text.ToString() == "")
                {
                    ltValidacion.Text = "<div class=\"ibox-content\">" +
                        "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Seleccione la fecha de inicio de la congelación." +
                        "</div></div>";
                }
                else
                {
                    if (documento.Value.ToString() == "")
                    {
                        ltValidacion.Text = "<div class=\"ibox-content\">" +
                            "<div class=\"alert alert-danger alert-dismissable\">" +
                            "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                            "Debe incluir un documento de soporte." +
                            "</div></div>";
                    }
                    else
                    {
                        if (txbObservaciones.Text.ToString() == "")
                        {
                            ltValidacion.Text = "<div class=\"ibox-content\">" +
                                "<div class=\"alert alert-danger alert-dismissable\">" +
                                "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                "Debe escribir las observaciones." +
                                "</div></div>";
                        }
                        else
                        {
                            string strDias = hfDias.Value.ToString();

                            OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
                            try
                            {
                                string strFilename = "";
                                HttpPostedFile postedFile = Request.Files["documento"];

                                if (postedFile != null && postedFile.ContentLength > 0)
                                {
                                    //Save the File.
                                    string filePath = Server.MapPath("docs//incapacidades//") + ViewState["idAfiliadoPlan"].ToString() + "_" + Path.GetFileName(postedFile.FileName);
                                    postedFile.SaveAs(filePath);
                                    strFilename = ViewState["idAfiliadoPlan"].ToString() + "_" + postedFile.FileName;
                                }

                                string strQuery = "INSERT INTO Incapacidades " +
                                "(idAfiliadoPlan, idTipoIncapacidad, idUsuario, FechaInicio, Dias, DocumentoIncapacidad, Observaciones, Estado, Fecha) " +
                                "VALUES (" + ViewState["idAfiliadoPlan"].ToString() + ", " + ddlTipoIncapacidad.SelectedItem.Value.ToString() + ", " +
                                "" + Session["idUsuario"].ToString() + ", '" + txbFechaInicio.Text.ToString() + "', " + strDias + ", " +
                                "'" + strFilename + "', '" + txbObservaciones.Text.ToString() + "', 'En proceso', Now()) ";
                                OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                                myConnection.Open();
                                command.ExecuteNonQuery();
                                command.Dispose();
                                myConnection.Close();

                                clasesglobales cg = new clasesglobales();
                                cg.InsertarLog(Session["idusuario"].ToString(), "Incapacidades", "Nuevo registro", "El usuario agregó una incapacidad al afiliado con documento " + ViewState["DocumentoAfiliado"].ToString() + ".", "", "");

                                Response.Redirect("afiliados");
                            }
                            catch (OdbcException ex)
                            {
                                string mensaje = ex.Message;
                                ltMensaje.Text = "<div class=\"ibox-content\">" +
                                    "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" + ex.Message +
                                    "</div></div>";
                                myConnection.Close();
                            }
                        }
                    }
                }
            }
        }
    }
}