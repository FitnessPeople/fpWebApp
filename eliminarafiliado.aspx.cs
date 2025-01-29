using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class eliminarafiliado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("afiliados");
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Afiliados");
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
                            CargarPlanesAfiliado();
                            ViewState["respuesta"] = "";
                            CargarPreguntaConfirmacion();
                            if (ViewState["Borrar"].ToString() == "1")
                            {
                                btnEliminar.Visible = true;
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

            string strQuery = "SELECT SinPermiso, Consulta, Exportar, CrearModificar, Borrar " +
                "FROM permisos_perfiles pp, paginas p, usuarios u " +
                "WHERE pp.idPagina = p.idPagina " +
                "AND p.Pagina = '" + strPagina + "' " +
                "AND pp.idPerfil = " + Session["idPerfil"].ToString() + " " +
                "AND u.idPerfil = pp.idPerfil " +
                "AND u.idUsuario = " + Session["idusuario"].ToString();
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

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
            string strDocumento = Request.QueryString["deleteid"].ToString();
            string strQuery = "SELECT * FROM Afiliados a " +
                "RIGHT JOIN Sedes s ON a.idSede = s.idSede " +
                "WHERE idAfiliado = " + strDocumento + " ";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                ltNombreAfiliado.Text = dt.Rows[0]["NombreAfiliado"].ToString();
                ltApellidoAfiliado.Text = dt.Rows[0]["ApellidoAfiliado"].ToString();
                ltFotoAfiliado.Text = "<img alt=\"image\" class=\"img-circle circle-border m-b-md\" src=\"img/afiliados/" + dt.Rows[0]["FotoAfiliado"].ToString() + "\" width=\"120px\">";
                ltEmailAfiliado.Text = dt.Rows[0]["EmailAfiliado"].ToString();
                ltCelularAfiliado.Text = dt.Rows[0]["CelularAfiliado"].ToString();
                ltSedeAfiliado.Text = dt.Rows[0]["NombreSede"].ToString();
                if (dt.Rows[0]["FechaNacAfiliado"].ToString() != "1900-01-00")
                {
                    ltCumpleAfiliado.Text = String.Format("{0:dd MMM}", Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"]));
                }
                else
                {
                    ltCumpleAfiliado.Text = "-";
                }
            }
            dt.Dispose();
        }

        private void CargarPlanesAfiliado()
        {
            if (Request.QueryString.Count > 0)
            {
                string strQuery = "SELECT *, " +
                    "DATEDIFF(FechaFinalPlan, CURDATE()) AS diasquefaltan, " +
                    "DATEDIFF(CURDATE(), FechaInicioPlan) AS diasconsumidos, " +
                    "DATEDIFF(FechaFinalPlan, FechaInicioPlan) AS diastotales, " +
                    "ROUND(DATEDIFF(CURDATE(), FechaInicioPlan) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje1, " +
                    "ROUND(DATEDIFF(FechaFinalPlan, CURDATE()) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje2 " +
                    "FROM afiliadosPlanes ap, Afiliados a, Planes p " +
                    "WHERE a.idAfiliado = " + Request.QueryString["deleteid"].ToString() + " " +
                    "AND ap.idAfiliado = a.idAfiliado " +
                    "AND ap.idPlan = p.idPlan " +
                    "AND ap.EstadoPlan = 'Activo'";
                clasesglobales cg1 = new clasesglobales();
                DataTable dt = cg1.TraerDatos(strQuery);

                if (dt.Rows.Count > 0)
                {
                    rpPlanesAfiliado.DataSource = dt;
                    rpPlanesAfiliado.DataBind();

                }
                dt.Dispose();
            }
        }

        private void CargarPreguntaConfirmacion()
        {
            Random aleatorio = new Random();
            int numero = aleatorio.Next(1, 3);
            switch (numero)
            {
                case 1:
                    ltPregunta.Text = "3+2=";
                    ViewState["respuesta"] = "5";
                    break;
                case 2:
                    ltPregunta.Text = "5+1=";
                    ViewState["respuesta"] = "6";
                    break;
                case 3:
                    ltPregunta.Text = "1+2=";
                    ViewState["respuesta"] = "3";
                    break;
                default:
                    break;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txbConfirmacion.Text.ToString() != "")
            {
                if (txbConfirmacion.Text.ToString() == ViewState["respuesta"].ToString())
                {
                    //Validar si el afiliado tiene un plan activo.
                    OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());

                    ltMensaje.Text = "<div class=\"ibox-content\">" +
                        "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Respuesta correcta. Afiliado eliminado." +
                        "</div></div>";
                }
                else
                {
                    ltMensaje.Text = "<div class=\"ibox-content\">" +
                        "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Respuesta incorrecta." +
                        "</div></div>";
                }
            }
            else
            {
                ltMensaje.Text = "<div class=\"ibox-content\">" +
                    "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Respuesta necesaria." +
                    "</div></div>";
            }
        }
    }
}