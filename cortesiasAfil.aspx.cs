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
    public partial class cortesiasAfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarDiasCortesia();
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
                            CargarAfiliado();
                            CargarCortesias();
                            CargarPlanesAfiliado();
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
            if (Request.QueryString.Count > 0)
            {
                string strQuery = "SELECT * " +
                    "FROM afiliados a, sedes s " +
                    "WHERE a.idAfiliado = " + Request.QueryString["id"].ToString() + " " +
                    "AND a.idSede = s.idSede ";
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(strQuery);

                if (dt.Rows.Count > 0)
                {
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
                }
                dt.Dispose();
            }
        }

        private void CargarCortesias()
        {
            string strQuery = "SELECT * " +
                "FROM Cortesias c, AfiliadosPlanes ap " +
                "WHERE ap.idAfiliadoPlan = c.idAfiliadoPlan " +
                "AND ap.idAfiliado = " + Request.QueryString["id"].ToString() + " " +
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

        private void CargarPlanesAfiliado()
        {
            if (Request.QueryString.Count > 0)
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.CargarPlanesAfiliado(Request.QueryString["id"].ToString(), "Activo");

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
        }

        private void CargarDiasCortesia()
        {
            string strQuery = "SELECT * " +
                "FROM DiasCortesia " +
                "WHERE Estado = 1";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                PlaceHolder ph = ((PlaceHolder)this.FindControl("phDiasCortesia"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Button btn = new Button();
                    btn.Attributes.Add("style", "width: 70px; font-size: 30px; height: 70px;");
                    btn.Text = dt.Rows[i]["DiasCortesia"].ToString();
                    btn.CssClass = "btn btn-info dim btn-large-dim btn-outline";
                    btn.ToolTip = dt.Rows[i]["DiasCortesia"].ToString();
                    //btn.Style = "";
                    btn.Command += new CommandEventHandler(btn_Click);
                    btn.CommandArgument = dt.Rows[i]["idDiasCortesia"].ToString();
                    btn.ID = dt.Rows[i]["idDiasCortesia"].ToString();
                    ph.Controls.Add(btn);
                }
            }
            dt.Dispose();
        }

        private void btn_Click(object sender, CommandEventArgs e)
        {
            string strQuery = "SELECT * " +
                "FROM DiasCortesia " +
                "WHERE idDiasCortesia = " + e.CommandArgument;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ltDescripcionRegalo.Text = "Se agregarán " + dt.Rows[0]["DiasCortesia"].ToString() + " días de cortesía al final del período de su plan activo.";
            ViewState["DiasCortesia"] = dt.Rows[0]["DiasCortesia"].ToString();
            
        }

        protected void btnAgregarCortesia_Click(object sender, EventArgs e)
        {
            if (ViewState["DiasCortesia"] == null)
            {
                ltMensaje.Text = "<div class=\"ibox-content\">" +
                    "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Elija cuantos días de cortesía se van a otorgar al afiliado." +
                    "</div></div>";
            }
            else
            {
                if (txbObservaciones.Text.ToString() == "")
                {
                    ltMensaje.Text = "<div class=\"ibox-content\">" +
                        "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Escriba las observaciones de la cortesía." +
                        "</div></div>";
                }
                else
                {
                    try
                    {
                        string strQuery = "INSERT INTO Cortesias " +
                        "(idUsuario, idAfiliadoPlan, DiasCortesia, FechaHoraCortesia, ObservacionesCortesia, EstadoCortesia) " +
                        "VALUES (" + Session["idUsuario"].ToString() + ", " +
                        "" + ViewState["idAfiliadoPlan"].ToString() + ", " + ViewState["DiasCortesia"].ToString() + ", NOW(), " +
                        "'" + txbObservaciones.Text.ToString() + "', 'Pendiente') ";

                        clasesglobales cg = new clasesglobales();
                        string mensaje = cg.TraerDatosStr(strQuery);

                        cg.InsertarLog(Session["idusuario"].ToString(), "Cortesias", "Nuevo registro", "El usuario agregó una cortesia al afiliado con documento " + ViewState["DocumentoAfiliado"].ToString() + ".", "", "");

                        Response.Redirect("afiliados");
                    }
                    catch (OdbcException ex)
                    {
                        ltMensaje.Text = "<div class=\"ibox-content\">" +
                            "<div class=\"alert alert-danger alert-dismissable\">" +
                            "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" + ex.Message +
                            "</div></div>";
                    }
                }
            }
        }
    }
}