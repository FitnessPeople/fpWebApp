using System;
using System.Web.UI;

namespace fpWebApp
{
    public partial class inicio : System.Web.UI.Page
    {
        private string _strDiaZero;
        protected string strDiaZero { get { return this._strDiaZero; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    clasesglobales cg = new clasesglobales();
                    string ipLogin = cg.ObtenerIPReal();
                    ltIdUsuario.Text = Session["idUsuario"].ToString();
                    ltNombreUsuario.Text = Session["NombreUsuario"].ToString() + " IP: " + ipLogin;
                    ltIdEmpresa.Text = Session["idEmpresa"].ToString();
                    ltCargo.Text = Session["CargoUsuario"].ToString();
                    ltFoto.Text = Session["Foto"].ToString();
                    ltIdPerfil.Text = Session["idPerfil"].ToString() + " - " + Session["Perfil"].ToString();
                    ltEmailUsuario.Text = Session["emailUsuario"].ToString();
                    ltFechaNac.Text = Session["fechaNac"].ToString();
                    ltIdSede.Text = Session["idSede"].ToString();
                    ltIdCanalVenta.Text = Session["idCanalVenta"].ToString();
                    ltIdEmpleado.Text = Session["idEmpleado"].ToString();

                    //Session["idUsuario"] = 147;
                    //Session["NombreUsuario"] = "Christian Morales";
                    //Session["idEmpresa"] = 1;
                    //Session["Cargo"] = "WebMaster";
                    //Session["Foto"] = "chrismo.jpg";
                    //Session["idPerfil"] = 1;
                    //Session["usuario"] = "sistemas@fitnesspeoplecmd.com";
                    //Session[""] = "11";

                    DateTime fechaObjetivo = Convert.ToDateTime(Session["fechaNac"]);
                    int diaObjetivo = fechaObjetivo.Day;
                    int mesObjetivo = fechaObjetivo.Month;

                    DateTime hoy = DateTime.Now;

                    if (hoy.Day == diaObjetivo && hoy.Month == mesObjetivo)
                    {
                        // Ejecutar el script en el navegador
                        ScriptManager.RegisterStartupScript(this, GetType(),
                            "confettiScript", "lanzarConfetti();", true);
                    }

                    if (Request.QueryString["idPerfil"] != null)
                    {
                        Session["idPerfil"] = Convert.ToInt16(Request.QueryString["idPerfil"].ToString());
                    }
                    if (Request.QueryString["idUsuario"] != null)
                    {
                        Session["idUsuario"] = Convert.ToInt16(Request.QueryString["idUsuario"].ToString());
                    }

                    DateTime fechaDestino = new DateTime(2026, 01, 31);
                    TimeSpan diferencia = fechaDestino - hoy;
                    _strDiaZero = Convert.ToInt32(diferencia.TotalDays).ToString();

                    Control ctrIndicadores;
                    Control ctrGraficos;

                    switch (Session["Perfil"].ToString())
                    {
                        case "CEO":
                            ctrIndicadores = LoadControl("controles/indicadoresCEO.ascx");
                            ctrGraficos = LoadControl("controles/graficosCEO.ascx");
                            break;
                        case "Director operativo":
                            ctrIndicadores = LoadControl("controles/indicadoresDirOpe.ascx");
                            ctrGraficos = LoadControl("controles/graficosDirOpe.ascx");
                            break;
                        case "Director comercial":
                            ctrIndicadores = LoadControl("controles/indicadoresDirCom.ascx");
                            ctrGraficos = LoadControl("controles/graficosCEO.ascx");
                            break;
                        case "Administrador sede":
                            ctrIndicadores = LoadControl("controles/indicadoresAdmSede.ascx");
                            ctrGraficos = LoadControl("controles/graficosAdmSede.ascx");
                            break;
                        case "Líder online":
                            ctrIndicadores = LoadControl("controles/indicadoresAdmSede.ascx");
                            ctrGraficos = LoadControl("controles/graficosAdmSede.ascx");
                            break;
                        case "Líder corporativo":
                            ctrIndicadores = LoadControl("controles/indicadoresAseCom.ascx");
                            ctrGraficos = LoadControl("controles/graficosAseCom.ascx");
                            break;
                        case "Asesor comercial":
                            ctrIndicadores = LoadControl("controles/indicadoresAseCom.ascx");
                            ctrGraficos = LoadControl("controles/graficosAseCom.ascx");
                            break;
                        case "Asesor corporativo":
                            ctrIndicadores = LoadControl("controles/indicadoresAseCom.ascx");
                            ctrGraficos = LoadControl("controles/graficosAseCom.ascx");
                            break;
                        case "Líder asistencial":
                            ctrIndicadores = LoadControl("controles/indicadoresLidAsis.ascx");
                            ctrGraficos = LoadControl("controles/graficosLidAsis.ascx");
                            break;
                        case "Director financiero y administrativo":
                            ctrIndicadores = LoadControl("controles/indicadoresDirFin.ascx");
                            ctrGraficos = LoadControl("controles/graficosDirFin.ascx");
                            break;
                        default:
                            ctrIndicadores = LoadControl("controles/indicadores02.ascx");
                            ctrGraficos = LoadControl("controles/graficosCEO.ascx");
                            break;
                    }

                    phIndicadores.Controls.Add(ctrIndicadores);
                    phGraficos.Controls.Add(ctrGraficos);

                }
                else
                {
                    Response.Redirect("logout.aspx");
                }
            }
        }
    }
}