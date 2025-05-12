using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Npgsql;

namespace fpWebApp
{
    public partial class inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["idUsuario"] = 1;
                //Session["NombreUsuario"] = "Christian Morales";
                //Session["idEmpresa"] = 1;
                //Session["Cargo"] = "WebMaster";
                //Session["Foto"] = "chrismo.jpg";
                //Session["idPerfil"] = 1;

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

                    //string connString = ConfigurationManager.ConnectionStrings["PSIPlatformBoot"].ConnectionString;

                    //using (var conn = new NpgsqlConnection(connString))
                    //{
                    //    conn.Open();
                    //    Console.WriteLine("Conexión exitosa a PostgreSQL 9.6!");

                    //    using (var cmd = new NpgsqlCommand("SELECT version()", conn))
                    //    using (var reader = cmd.ExecuteReader())
                    //    {
                    //        if (reader.Read())
                    //            ltMsg.Text = reader.GetString(0);
                    //    }
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