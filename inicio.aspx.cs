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
                    ltIdUsuario.Text = Session["idUsuario"].ToString();
                    ltNombreUsuario.Text = Session["NombreUsuario"].ToString();
                    ltIdEmpresa.Text = Session["idEmpresa"].ToString();
                    ltCargo.Text = Session["CargoUsuario"].ToString();
                    ltFoto.Text = Session["Foto"].ToString();
                    ltIdPerfil.Text = Session["idPerfil"].ToString();
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

                    Control ctrInicio = new Control();

                    if (Session["idPerfil"].ToString() == "1")
                    {
                        ctrInicio = LoadControl("controles/indicadores01.ascx");
                    }
                    else
                    {
                        ctrInicio = LoadControl("controles/indicadores02.ascx");
                    }

                    phIndicadores.Controls.Add(ctrInicio);

                }
                else
                {
                    Response.Redirect("logout.aspx");
                }
            }
        }
    }
}