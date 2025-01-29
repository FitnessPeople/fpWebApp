using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class cortesias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Cortesias");
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
                            divAfiliado.Visible = false;
                            divPlanes.Visible = false;
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

            string strQuery = "SELECT SinPermiso, Consulta, Exportar, CrearModificar, Borrar " +
                "FROM permisos_perfiles pp, paginas p, usuarios u " +
                "WHERE pp.idPagina = p.idPagina " +
                "AND p.Pagina = '" + strPagina + "' " +
                "AND pp.idPerfil = " + Session["idPerfil"].ToString() + " " +
                "AND u.idPerfil = pp.idPerfil " +
                "AND u.idUsuario = " + Session["idusuario"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

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

        protected void btn7dias_Click(object sender, EventArgs e)
        {
            ltDescripcionRegalo.Text = "Se agregarán 7 días de cortesía al final del período de su plan activo.";
            ViewState["DiasCortesia"] = 7;
            btn7dias.CssClass += " active";
            btn15dias.CssClass = btn15dias.CssClass.Replace("active", "");
            btn30dias.CssClass = btn30dias.CssClass.Replace("active", "");
            btn60dias.CssClass = btn60dias.CssClass.Replace("active", "");
        }

        protected void btn15dias_Click(object sender, EventArgs e)
        {
            ltDescripcionRegalo.Text = "Se agregarán 15 días de cortesía al final del período de su plan activo.";
            ViewState["DiasCortesia"] = 15;
            btn15dias.CssClass += " active";
            btn7dias.CssClass = btn7dias.CssClass.Replace("active", "");
            btn30dias.CssClass = btn30dias.CssClass.Replace("active", "");
            btn60dias.CssClass = btn60dias.CssClass.Replace("active", "");
        }

        protected void btn30dias_Click(object sender, EventArgs e)
        {
            ltDescripcionRegalo.Text = "Se agregarán 30 días de cortesía al final del período de su plan activo.";
            ViewState["DiasCortesia"] = 30;
            btn30dias.CssClass += " active";
            btn7dias.CssClass = btn7dias.CssClass.Replace("active", "");
            btn15dias.CssClass = btn15dias.CssClass.Replace("active", "");
            btn60dias.CssClass = btn60dias.CssClass.Replace("active", "");
        }

        protected void btn60dias_Click(object sender, EventArgs e)
        {
            ltDescripcionRegalo.Text = "Se agregarán 60 días de cortesía al final del período de su plan activo.";
            ViewState["DiasCortesia"] = 60;
            btn60dias.CssClass += " active";
            btn7dias.CssClass = btn7dias.CssClass.Replace("active", "");
            btn15dias.CssClass = btn15dias.CssClass.Replace("active", "");
            btn30dias.CssClass = btn30dias.CssClass.Replace("active", "");
        }

        protected void btnAgregarCortesia_Click(object sender, EventArgs e)
        {
            OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
            try
            {
                string strQuery = "INSERT INTO Cortesias " +
                "(idUsuario, idAfiliadoPlan, DiasCortesia, FechaHoraCortesia, ObservacionesCortesia, EstadoCortesia) " +
                "VALUES (" + Session["idUsuario"].ToString() + ", " +
                "" + ViewState["idAfiliadoPlan"].ToString() + ", " + ViewState["DiasCortesia"].ToString() + ", NOW(), " +
                "'" + txbObservaciones.Text.ToString() + "', 'Pendiente') ";
                OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                myConnection.Open();
                command.ExecuteNonQuery();
                command.Dispose();
                myConnection.Close();

                clasesglobales cg = new clasesglobales();
                cg.InsertarLog(Session["idusuario"].ToString(), "Cortesias", "Nuevo registro", "El usuario agregó una cortesia al afiliado con documento " + ViewState["DocumentoAfiliado"].ToString() + ".", "", "");
            }
            catch (OdbcException ex)
            {
                string mensaje = ex.Message;
                // ltMensaje.Text = "<div class=\"ibox-content\">" +
                // "<div class=\"alert alert-danger alert-dismissable\">" +
                // "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" + ex.Message +
                //"</div></div>";
                myConnection.Close();
            }

            Response.Redirect("afiliados");
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
                //ViewState["idAfiliado"] = dt.Rows[0]["idAfiliado"].ToString();
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

                CargarPlanesAfiliado(dt.Rows[0]["idAfiliado"].ToString());
                CargarCortesias(dt.Rows[0]["idAfiliado"].ToString());
            }
            dt.Dispose();
        }

        private void CargarPlanesAfiliado(string idAfiliado)
        {
            divPlanes.Visible = true;

            string strQuery = "SELECT *, " +
                "DATEDIFF(FechaFinalPlan, CURDATE()) AS diasquefaltan, " +
                "DATEDIFF(CURDATE(), FechaInicioPlan) AS diasconsumidos, " +
                "DATEDIFF(FechaFinalPlan, FechaInicioPlan) AS diastotales, " +
                "ROUND(DATEDIFF(CURDATE(), FechaInicioPlan) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje1, " +
                "ROUND(DATEDIFF(FechaFinalPlan, CURDATE()) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje2 " +
                "FROM afiliadosPlanes ap, Afiliados a, Planes p " +
                "WHERE a.idAfiliado = " + idAfiliado + " " +
                "AND ap.idAfiliado = a.idAfiliado " +
                "AND ap.idPlan = p.idPlan " +
                "AND ap.EstadoPlan = 'Activo'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                ViewState["idAfiliadoPlan"] = dt.Rows[0]["idAfiliadoPlan"].ToString();
                ViewState["DocumentoAfiliado"] = dt.Rows[0]["DocumentoAfiliado"].ToString();
                rpPlanesAfiliado.DataSource = dt;
                rpPlanesAfiliado.DataBind();
            }
            else
            {
                ltNoPlanes.Text = "Sin planes. No es posible agregar una cortesía.";
                txbObservaciones.Enabled = false;
                btnAgregarCortesia.Enabled = false;
            }

            dt.Dispose();
        }

        private void CargarCortesias(string idAfiliado)
        {
            string strQuery = "SELECT * " +
                "FROM Cortesias c, AfiliadosPlanes ap, Afiliados a " +
                "WHERE ap.idAfiliadoPlan = c.idAfiliadoPlan " +
                "AND ap.idAfiliado = " + idAfiliado + " " +
                "AND c.EstadoCortesia = 'Pendiente'";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                ltMensaje.Text = "<div class=\"ibox-content\">" +
                    "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Existe una cortesía en proceso. No es posible agregar otra cortesía." +
                    "</div></div>";
                txbObservaciones.Enabled = false;
                btnAgregarCortesia.Enabled = false;
            }

            dt.Dispose();
        }
    }
}