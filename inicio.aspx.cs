using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["idUsuario"] = 1;
                Session["NombreUsuario"] = "Christian Morales";
                Session["idEmpresa"] = 1;
                Session["Cargo"] = "WebMaster";
                Session["Foto"] = "chrismo.jpg";
                Session["idPerfil"] = 1;
                Session["usuario"] = "sistemas@fitnesspeoplecmd.com";
                Session["idSede"] = "11";

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


                    // ************  POSGRESSQL TEST  ************

                    //clasesglobales cg = new clasesglobales();
                    //DataTable dt = cg.TraerDatosArmatura("SELECT * FROM acc_timezone");

                    //if (dt.Rows.Count > 0)
                    //{
                    //    ltMsg.Text = dt.Rows[0]["acc_timeseg"].ToString();
                    //}
                }
                else
                {
                    Response.Redirect("logout.aspx");
                }
            }
        }
    }
}