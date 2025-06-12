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
    public partial class traspasosAfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Traspasos");
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
                            if (Request.QueryString["id"] == null)
                            {
                                Response.Redirect("afiliados");
                            }
                            else
                            {
                                CargarAfiliado();
                                CargarPlanesAfiliado();
                                CargarTraspasos();
                            }
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

        private void CargarAfiliado()
        {
            string strQuery = "SELECT * " +
                "FROM afiliados a, sedes s " +
                "WHERE a.idAfiliado = " + Request.QueryString["id"].ToString() + " " +
                "AND a.idSede = s.idSede ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                ViewState["DocumentoAfiliadoOrigen"] = dt.Rows[0]["DocumentoAfiliado"].ToString();
                ltNombreAfiliadoOrigen.Text = dt.Rows[0]["NombreAfiliado"].ToString();
                ltApellidoAfiliadoOrigen.Text = dt.Rows[0]["ApellidoAfiliado"].ToString();
                ltEmailAfiliadoOrigen.Text = dt.Rows[0]["EmailAfiliado"].ToString();
                ltCelularAfiliadoOrigen.Text = dt.Rows[0]["CelularAfiliado"].ToString();
                ltSedeAfiliadoOrigen.Text = dt.Rows[0]["NombreSede"].ToString();

                if (dt.Rows[0]["FechaNacAfiliado"].ToString() != "1900-01-00")
                {
                    ltCumpleAfiliadoOrigen.Text = String.Format("{0:dd MMM}", Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"]));
                }
                else
                {
                    ltCumpleAfiliadoOrigen.Text = "-";
                }

                if (dt.Rows[0]["FotoAfiliado"].ToString() != "")
                {
                    ltFotoAfiliadoOrigen.Text = "<img src=\"img/afiliados/" + dt.Rows[0]["FotoAfiliado"].ToString() + "\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                }
                else
                {
                    if (dt.Rows[0]["idGenero"].ToString() == "1" || dt.Rows[0]["idGenero"].ToString() == "3")
                    {
                        ltFotoAfiliadoOrigen.Text = "<img src=\"img/afiliados/avatar_male.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                    }
                    if (dt.Rows[0]["idGenero"].ToString() == "2")
                    {
                        ltFotoAfiliadoOrigen.Text = "<img src=\"img/afiliados/avatar_female.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                    }
                }
            }
            else
            {
                Response.Redirect("afiliados");
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
            //    "ROUND(DATEDIFF(FechaFinalPlan, CURDATE()) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje2 " +
            //    "FROM afiliadosPlanes ap, Afiliados a, Planes p " +
            //    "WHERE a.idAfiliado = " + Request.QueryString["id"].ToString() + " " +
            //    "AND ap.idAfiliado = a.idAfiliado " +
            //    "AND ap.idPlan = p.idPlan " +
            //    "AND ap.EstadoPlan = 'Activo'";
            //clasesglobales cg = new clasesglobales();
            //DataTable dt = cg.TraerDatos(strQuery);

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.CargarPlanesAfiliado(Request.QueryString["id"].ToString(), "Activo");

            if (dt.Rows.Count > 0)
            {
                ViewState["idAfiliadoPlan"] = dt.Rows[0]["idAfiliadoPlan"].ToString();
                rpPlanesAfiliado.DataSource = dt;
                rpPlanesAfiliado.DataBind();
            }
            else
            {
                ltNoPlanes.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                "Este afiliado no tiene planes activos para realizar traspasos." +
                "</div>";
                txbAfiliadoDestino.Enabled = false;
                btnTraspasar.Enabled = false;
            }
            dt.Dispose();
        }

        /// <summary>
        /// Verifica si el afiliado actual tiene un traspaso de planes en proceso.
        /// Si existe un traspaso pendiente, deshabilita los controles asociados y muestra una alerta.
        /// </summary>
        /// <remarks>
        /// - Consulta la tabla TraspasoPlanes filtrando por el ID del afiliado y estado "En proceso".
        /// - Utiliza la clase clasesglobales para ejecutar la consulta SQL.
        /// - Muestra un mensaje de alerta en el control ltNoPlanes si hay registros.
        /// - Deshabilita el campo txbAfiliadoDestino y el botón 'btnTraspasar' en caso de traspaso pendiente.
        /// </remarks>
        private void CargarTraspasos()
        {
            string strQuery = "SELECT * FROM TraspasoPlanes " +
                "WHERE idAfiliadoOrigen = " + Request.QueryString["id"].ToString() + " " +
                "AND EstadoTraspaso = 'En proceso'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                ltNoPlanes.Text = "<div class=\"ibox-content\">" +
                        "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Este afiliado ya tiene un traspaso en proceso." +
                        "</div></div>";
                txbAfiliadoDestino.Enabled = false;
                btnTraspasar.Enabled = false;
            }
            dt.Dispose();
        }

        /// <summary>
        /// Procesa la solicitud de traspaso de planes entre afiliados, validando los requisitos y almacenando la información en la base de datos.
        /// </summary>
        /// <remarks>
        /// Realiza las siguientes validaciones antes de ejecutar el traspaso:
        /// 1. Verifica que se haya adjuntado un documento de respaldo
        /// 2. Valida que las observaciones tengan al menos 20 caracteres
        /// 3. Comprueba que se haya especificado una fecha de inicio
        /// 4. Asegura que el afiliado destino sea diferente al origen
        /// 
        /// Si todas las validaciones son exitosas:
        /// - Guarda el documento adjunto en el servidor
        /// - Registra el traspaso en la tabla TraspasoPlanes
        /// - Crea un registro de log de la operación
        /// - Redirige a la página de afiliados
        /// 
        /// En caso de error, muestra mensajes de alerta al usuario.
        /// </remarks>
        /// <param name="sender">Objeto que generó el evento</param>
        /// <param name="e">Argumentos del evento</param>
        /// <exception cref="OdbcException">Captura y muestra errores de base de datos</exception>
        protected void btnTraspasar_Click(object sender, EventArgs e)
        {
            if (Request.Files["documento"] == null)
            {
                ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Debe elegir un documento de respaldo para el traspaso." +
                    "</div>";
            }
            else
            {
                if (txbObservaciones.Text.Length < 20)
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Debe escribir las observaciones del traspaso (mínimo 20 caracteres)." +
                        "</div>";
                }
                else
                {
                    if (txbFechaInicio.Text.ToString() == "")
                    {
                        ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                            "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                            "Debe elegir una fecha de inicio." +
                            "</div>";
                    }
                    else
                    {
                        if (Request.QueryString["id"].ToString() == ViewState["idAfiliadoDestino"].ToString())
                        {
                            ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                                "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                "El afiliado destino no puede ser el mismo afiliado de origen." +
                                "</div>";
                        }
                        else
                        {
                            try
                            {
                                string strFilename = "";
                                HttpPostedFile postedFile = Request.Files["documento"];

                                if (postedFile != null && postedFile.ContentLength > 0)
                                {
                                    //Save the File.
                                    string filePath = Server.MapPath("docs//traspasos//") + "Ori_" + Request.QueryString["id"].ToString() + "_Des_" + ViewState["idAfiliadoDestino"].ToString() + "_" + Path.GetFileName(postedFile.FileName);
                                    postedFile.SaveAs(filePath);
                                    strFilename = "Ori_" + Request.QueryString["id"].ToString() + "_Des_" + ViewState["idAfiliadoDestino"].ToString() + "_" + postedFile.FileName;
                                }

                                string strQuery = "INSERT INTO TraspasoPlanes " +
                                "(idAfiliadoOrigen, idAfiliadoDestino, idAfiliadoPlan, FechaTraspaso, FechaInicioTraspaso, " +
                                "DocumentoRespaldo, Observaciones, idUsuario, EstadoTraspaso) " +
                                "VALUES (" + Request.QueryString["id"].ToString() + ", " + ViewState["idAfiliadoDestino"].ToString() + ", " +
                                "" + ViewState["idAfiliadoPlan"].ToString() + ", NOW(), '" + txbFechaInicio.Text.ToString() + "', '" + strFilename + "', " +
                                "'" + txbObservaciones.Text.ToString() + "', " + Session["idUsuario"].ToString() + ", 'En proceso') ";
                                clasesglobales cg = new clasesglobales();
                                string mensaje = cg.TraerDatosStr(strQuery);
                                cg.InsertarLog(Session["idusuario"].ToString(), "traspasoplanes", "Agrega", "El usuario agregó un traspaso del afiliado con documento " + ViewState["DocumentoAfiliadoOrigen"].ToString() + " al afiliado con documento " + ViewState["DocumentoAfiliadoDestino"].ToString() + ".", "", "");

                                Response.Redirect("afiliados");
                            }
                            catch (OdbcException ex)
                            {
                                string mensaje = ex.Message;
                                ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" + ex.Message +
                                    "</div>";
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Busca y muestra la información de un afiliado destino basado en el documento ingresado.
        /// </summary>
        /// <remarks>
        /// Esta función:
        /// 1. Realiza una búsqueda en la base de datos usando el número de documento del afiliado
        /// 2. Muestra los datos del afiliado encontrado en la interfaz
        /// 3. Configura el campo de fecha de inicio con restricciones de fecha mínima
        /// 4. Establece valores en ViewState para uso posterior
        /// 5. Maneja la visualización de la foto de perfil (personalizada o por defecto según género)
        /// 
        /// Se espera que el texto de entrada esté en formato "documento - nombre" (se procesa solo la parte del documento)
        /// </remarks>
        /// <param name="sender">Objeto que disparó el evento</param>
        /// <param name="e">Argumentos del evento</param>
        protected void btnAfiliadoDestino_Click(object sender, EventArgs e)
        {
            string[] strDocumento = txbAfiliadoDestino.Text.ToString().Split('-');
            string strQuery = "SELECT * FROM Afiliados a " +
                "RIGHT JOIN Sedes s ON a.idSede = s.idSede " +
                "WHERE DocumentoAfiliado = '" + strDocumento[0].Trim() + "' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                divAfilDestino.Visible = true;
                DateTime dtHoy = DateTime.Now;
                txbFechaInicio.Attributes.Add("type", "date");
                txbFechaInicio.Attributes.Add("min", dtHoy.Year.ToString() + "-" + String.Format("{0:MM}", dtHoy) + "-" + String.Format("{0:dd}", dtHoy));

                ViewState["idAfiliadoDestino"] = dt.Rows[0]["idAfiliado"].ToString();
                ViewState["DocumentoAfiliadoDestino"] = dt.Rows[0]["DocumentoAfiliado"].ToString();
                ltNombreAfiliadoDestino.Text = dt.Rows[0]["NombreAfiliado"].ToString();
                ltApellidoAfiliadoDestino.Text = dt.Rows[0]["ApellidoAfiliado"].ToString();
                ltEmailAfiliadoDestino.Text = dt.Rows[0]["EmailAfiliado"].ToString();
                ltCelularAfiliadoDestino.Text = dt.Rows[0]["CelularAfiliado"].ToString();
                ltSedeAfiliadoDestino.Text = dt.Rows[0]["NombreSede"].ToString();

                if (dt.Rows[0]["FechaNacAfiliado"].ToString() != "1900-01-00")
                {
                    ltCumpleAfiliadoDestino.Text = String.Format("{0:dd MMM}", Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"]));
                }
                else
                {
                    ltCumpleAfiliadoDestino.Text = "-";
                }

                if (dt.Rows[0]["FotoAfiliado"].ToString() != "")
                {
                    ltFotoAfiliadoDestino.Text = "<img src=\"img/afiliados/" + dt.Rows[0]["FotoAfiliado"].ToString() + "\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                }
                else
                {
                    if (dt.Rows[0]["idGenero"].ToString() == "1" || dt.Rows[0]["idGenero"].ToString() == "3")
                    {
                        ltFotoAfiliadoDestino.Text = "<img src=\"img/afiliados/avatar_male.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                    }
                    if (dt.Rows[0]["idGenero"].ToString() == "2")
                    {
                        ltFotoAfiliadoDestino.Text = "<img src=\"img/afiliados/avatar_female.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                    }
                }
            }
            dt.Dispose();
        }
    }
}