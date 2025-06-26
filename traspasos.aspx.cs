using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class traspasos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    btnTraspasar.Visible = false;
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
                            CargarAfiliadosOrigen();
                            txbFechaInicio.Attributes.Add("type", "date");
                            divAfiliadoOrigen.Visible = false;
                            divPlanes.Visible = false;
                            btnTraspasar.Visible = true;
                            btnTraspasar.Enabled = false;
                        }
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

        private void CargarAfiliadosOrigen()
        {
            string strQuery = @"SELECT a.idAfiliado, 
                CONCAT(a.NombreAfiliado, ' ', a.ApellidoAfiliado, ' - ', a.DocumentoAfiliado) AS DocNombreAfiliado 
                FROM afiliados_copia_normalizada a 
                INNER JOIN AfiliadosPlanes ap ON ap.idAfiliado = a.idAfiliado AND ap.EstadoPlan = 'Activo' 
                WHERE EstadoAfiliado = 'Activo' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlAfiliadoOrigen.DataSource = dt;
            ddlAfiliadoOrigen.DataBind();

            dt.Dispose();
        }

        protected void btnTraspasar_Click(object sender, EventArgs e)
        {
            try
            {
                string strFilename = "";
                HttpPostedFile postedFile = Request.Files["documento"];

                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    //Save the File.
                    string filePath = Server.MapPath("docs//traspasos//") + "Ori_" + ViewState["idAfiliadoOrigen"].ToString() + "_Des_" + ViewState["idAfiliadoDestino"].ToString() + "_" + Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    strFilename = "Ori_" + ViewState["idAfiliadoOrigen"].ToString() + "_Des_" + ViewState["idAfiliadoDestino"].ToString() + "_" + postedFile.FileName;
                }

                string strQuery = "INSERT INTO TraspasoPlanes " +
                "(idAfiliadoOrigen, idAfiliadoDestino, idAfiliadoPlan, FechaTraspaso, FechaInicioTraspaso, " +
                "DocumentoRespaldo, Observaciones, idUsuario, EstadoTraspaso) " +
                "VALUES (" + ViewState["idAfiliadoOrigen"].ToString() + ", " + ViewState["idAfiliadoDestino"].ToString() + ", " +
                "" + ViewState["idAfiliadoPlan"].ToString() + ", NOW(), '" + txbFechaInicio.Text.ToString() + "', '" + strFilename + "', " +
                "'" + txbObservaciones.Text.ToString() + "', " + Session["idUsuario"].ToString() + ", 'En proceso') ";
                clasesglobales cg = new clasesglobales();
                string mensaje = cg.TraerDatosStr(strQuery);
                cg.InsertarLog(Session["idusuario"].ToString(), "traspasos planes", "Agrega", "El usuario agregó un traspaso del afiliado con documento: " + ViewState["DocumentoAfiliadoOrigen"].ToString() + " al afiliado con documento: " + ViewState["DocumentoAfiliadoDestino"].ToString() + ".", "", "");

                Response.Redirect("afiliados");
            }
            catch (SqlException ex)
            {
                string mensaje = ex.Message;
                string script = @"
                    Swal.fire({
                        title: 'Error',
                        text: 'Ha ocurrido un error inesperado. " + mensaje + @"',
                        icon: 'error'
                    }).then(() => {
                    });
                    ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                //ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                //    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" + ex.Message +
                //    "</div>";
            }
        }

        private void CargarPlanesAfiliado()
        {
            //ltNoPlanes.Text = "";
            //string strQuery = "SELECT *, " +
            //    "DATEDIFF(FechaFinalPlan, CURDATE()) AS diasquefaltan, " +
            //    "DATEDIFF(CURDATE(), FechaInicioPlan) AS diasconsumidos, " +
            //    "DATEDIFF(FechaFinalPlan, FechaInicioPlan) AS diastotales, " +
            //    "ROUND(DATEDIFF(CURDATE(), FechaInicioPlan) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje1, " +
            //    "ROUND(DATEDIFF(FechaFinalPlan, CURDATE()) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje2 " +
            //    "FROM afiliadosPlanes ap, Afiliados a, Planes p " +
            //    "WHERE a.idAfiliado = " + ViewState["idAfiliadoOrigen"] + " " +
            //    "AND ap.idAfiliado = a.idAfiliado " +
            //    "AND ap.idPlan = p.idPlan " +
            //    "AND ap.EstadoPlan = 'Activo'";
            //clasesglobales cg = new clasesglobales();
            //DataTable dt = cg.TraerDatos(strQuery);

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.CargarPlanesAfiliado(ViewState["idAfiliadoOrigen"].ToString(), "Activo");

            if (dt.Rows.Count > 0)
            {
                ViewState["idAfiliadoPlan"] = dt.Rows[0]["idAfiliadoPlan"].ToString();
                ViewState["DocumentoAfiliadoOrigen"] = dt.Rows[0]["DocumentoAfiliado"].ToString();
                rpPlanesAfiliado.DataSource = dt;
                rpPlanesAfiliado.DataBind();
                //txbAfiliadoDestino.Enabled = true;
            }
            else
            {
                string script = @"
                    Swal.fire({
                        title: 'Error',
                        text: 'Este afiliado no tiene planes activos para realizar traspasos.',
                        icon: 'error'
                    }).then(() => {
                    });
                    ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                //ltNoPlanes.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                //"<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                //"Este afiliado no tiene planes activos para realizar traspasos." +
                //"</div>";
                //txbAfiliadoDestino.Enabled = false;
                btnTraspasar.Enabled = false;
            }
            dt.Dispose();
        }

        private void CargarTraspasos()
        {
            string strQuery = "SELECT * FROM TraspasoPlanes " +
                "WHERE idAfiliadoOrigen = " + ViewState["idAfiliadoOrigen"] + " " +
                "AND EstadoTraspaso = 'En proceso'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                string script = @"
                    Swal.fire({
                        title: 'Error',
                        text: 'Este afiliado ya tiene un traspaso en proceso.',
                        icon: 'error'
                    }).then(() => {
                    });
                    ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                //ltNoPlanes.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                //"<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                //"Este afiliado ya tiene un traspaso en proceso." +
                //"</div>";
                //txbAfiliadoDestino.Enabled = false;
                btnTraspasar.Enabled = false;
            }
            dt.Dispose();
        }

        protected void ddlAfiliadoOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAfiliadoOrigen.SelectedItem.Value.ToString() != "")
            {
                string strQuery = @"SELECT a.idAfiliado, a.NombreAfiliado, a.ApellidoAfiliado, a.EmailAfiliado, 
                a.CelularAfiliado, s.NombreSede, a.FotoAfiliado, a.idGenero, ap.idPlan, p.NombrePlan, a.FechaNacAfiliado  
                FROM Afiliados_Copia_Normalizada a 
                RIGHT JOIN Sedes s ON a.idSede = s.idSede 
                LEFT JOIN AfiliadosPlanes ap ON ap.idAfiliado = a.idAfiliado AND ap.EstadoPlan = 'Activo' 
                LEFT JOIN Planes p ON ap.idPlan = p.idPlan 
                WHERE a.idAfiliado = " + ddlAfiliadoOrigen.SelectedItem.Value.ToString() + " ";
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(strQuery);
                if (dt.Rows.Count > 0)
                {
                    ViewState["idAfiliadoOrigen"] = dt.Rows[0]["idAfiliado"].ToString();
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

                    divAfiliadoOrigen.Visible = true;
                    divPlanes.Visible = true;
                    CargarPlanesAfiliado();
                    CargarTraspasos();
                }
                dt.Dispose();

                CargarAfiliadosDestino();
            }
        }

        private void CargarAfiliadosDestino()
        {
            ddlAfiliadoDestino.Items.Clear();
            ListItem li = new ListItem("Seleccione", "");
            ddlAfiliadoDestino.Items.Add(li);
            string strQuery = @"SELECT a.idAfiliado, 
                CONCAT(a.NombreAfiliado, ' ', a.ApellidoAfiliado, ' - ', a.DocumentoAfiliado) AS DocNombreAfiliado 
                FROM afiliados_copia_normalizada a 
                INNER JOIN AfiliadosPlanes ap ON ap.idAfiliado = a.idAfiliado 
                WHERE EstadoAfiliado = 'Activo' 
                AND a.idAfiliado <> " + ddlAfiliadoOrigen.SelectedItem.Value.ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlAfiliadoDestino.DataSource = dt;
            ddlAfiliadoDestino.DataBind();

            dt.Dispose();
        }

        protected void ddlAfiliadoDestino_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAfiliadoDestino.SelectedItem.Value.ToString() != "")
            {
                string strQuery = @"SELECT a.idAfiliado, a.DocumentoAfiliado, a.NombreAfiliado, a.ApellidoAfiliado, a.EmailAfiliado, 
                a.CelularAfiliado, s.NombreSede, a.FotoAfiliado, a.idGenero, ap.idPlan, p.NombrePlan, a.FechaNacAfiliado  
                FROM afiliados_copia_normalizada a 
                RIGHT JOIN Sedes s ON a.idSede = s.idSede 
                LEFT JOIN AfiliadosPlanes ap ON ap.idAfiliado = a.idAfiliado AND ap.EstadoPlan = 'Activo' 
                LEFT JOIN Planes p ON ap.idPlan = p.idPlan 
                WHERE a.IdAfiliado = " + ddlAfiliadoDestino.SelectedItem.Value.ToString() + " ";
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(strQuery);
                if (dt.Rows.Count > 0)
                {
                    divAfiliadoDestino.Visible = true;
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

                    if (dt.Rows[0]["idPlan"] is DBNull)
                    {
                        //ltPlanActivo.Text = "Ninguno";
                        btnTraspasar.Enabled = true;
                    }
                    else
                    {
                        //ltPlanActivo.Text = dt.Rows[0]["NombrePlan"].ToString();
                        string script = @"
                            Swal.fire({
                                title: 'Error',
                                text: 'Este afiliado ya tiene un plan activo (" + dt.Rows[0]["NombrePlan"].ToString() + @"). No se puede realizar el traspaso.',
                                icon: 'error'
                            }).then(() => {
                            });
                            ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);

                        //ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        //    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        //    "Este afiliado ya tiene un plan activo (" + dt.Rows[0]["NombrePlan"].ToString() + "). No se puede realizar el traspaso." +
                        //    "</div>";
                    }
                }
                dt.Dispose();
            }
        }
    }
}