using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class cambiaestadousuario : System.Web.UI.Page
    {
        OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            string strQuery = "SELECT EstadoUsuario FROM Usuarios WHERE idUsuario = " + Request.QueryString["id"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (dt.Rows[0]["EstadoUsuario"].ToString() == "Activo")
                    {
                        strQuery = "UPDATE Usuarios SET " +
                            "EstadoUsuario = 'Inactivo' " +
                            "WHERE idUsuario = " + Request.QueryString["id"].ToString();
                        OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                        myConnection.Open();
                        command.ExecuteNonQuery();
                        command.Dispose();
                        myConnection.Close();
                    }
                    if (dt.Rows[0]["EstadoUsuario"].ToString() == "Inactivo")
                    {
                        strQuery = "UPDATE Usuarios SET " +
                            "EstadoUsuario = 'Activo' " +
                            "WHERE idUsuario = " + Request.QueryString["id"].ToString();
                        OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                        myConnection.Open();
                        command.ExecuteNonQuery();
                        command.Dispose();
                        myConnection.Close();
                    }
                }
                catch (OdbcException ex)
                {
                    string mensaje = ex.Message;
                    myConnection.Close();
                }
            }

            dt.Dispose();

            Response.Redirect("usuarios");
        }
    }
}