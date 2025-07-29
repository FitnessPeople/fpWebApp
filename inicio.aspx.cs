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
                //Session["idUsuario"] = 147;
                //Session["NombreUsuario"] = "Christian Morales";
                //Session["idEmpresa"] = 1;
                //Session["Cargo"] = "WebMaster";
                //Session["Foto"] = "chrismo.jpg";
                //Session["idPerfil"] = 1;
                //Session["usuario"] = "sistemas@fitnesspeoplecmd.com";
                //Session["idSede"] = "11";

                if (Request.QueryString["idPerfil"] != null)
                {
                    Session["idPerfil"] = Convert.ToInt16(Request.QueryString["idPerfil"].ToString());
                }
                if (Request.QueryString["idUsuario"] != null)
                {
                    Session["idUsuario"] = Convert.ToInt16(Request.QueryString["idUsuario"].ToString());
                }

                DateTime fechaActual = DateTime.Now;
                DateTime fechaDestino = new DateTime(2025, 8, 29);
                TimeSpan diferencia = fechaDestino - fechaActual;
                _strDiaZero = Convert.ToInt32(diferencia.TotalDays).ToString();

                Control ctrInicio = new Control();
                if (Session["idUsuario"] != null)
                {
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