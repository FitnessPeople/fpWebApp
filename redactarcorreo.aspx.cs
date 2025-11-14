using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class redactarcorreo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    CargarUsuarios();
                    CargarCategorias();
                    clasesglobales cg = new clasesglobales();
                    string strQuery = @"SELECT * 
                        FROM correointerno ci 
                        INNER JOIN usuarios u ON u.idUsuario = ci.idUsuarioDe 
                        WHERE ci.idUsuarioPara = " + Session["idUsuario"].ToString() + @" 
                        AND ci.Leido = 0 
                        ORDER BY FechaHora DESC";

                    DataTable dt1 = cg.TraerDatos(strQuery);

                    ltNroMensajes1.Text = dt1.Rows.Count.ToString();

                    dt1.Dispose();
                }
                else
                {
                    Response.Redirect("logout");
                }
            }
        }

        private void CargarUsuarios()
        {
            clasesglobales cg = new clasesglobales();
            string strQuery = "SELECT * " +
                "FROM usuarios " +
                "WHERE idUsuario <> " + Session["idUsuario"].ToString() + " ORDER BY NombreUsuario ";

            DataTable dt = cg.TraerDatos(strQuery);

            ddlUsuarios.DataSource = dt;
            ddlUsuarios.DataValueField = "idUsuario";
            ddlUsuarios.DataTextField = "NombreUsuario";
            ddlUsuarios.DataBind();
        }

        private void CargarCategorias()
        {
            string strQuery = @"SELECT * 
                FROM categoriascorreo ";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpCategorias.DataSource = dt;
            rpCategorias.DataBind();

            ddlCategorias.DataSource = dt;
            ddlCategorias.DataBind();

            dt.Dispose();
        }

        protected void lbEnviar_Click(object sender, EventArgs e)
        {
            List<string> seleccionados = new List<string>();

            foreach (ListItem item in ddlUsuarios.Items)
            {
                if (item.Selected)
                {
                    seleccionados.Add(item.Value);
                }
            }

            foreach (string id in seleccionados)
            {
                string strQuery = "INSERT INTO correointerno (idUsuarioDe, idUsuarioPara, idCategoriaCorreo, Asunto, Mensaje, FechaHora) " +
                    "VALUES (" + Session["idUsuario"].ToString() + ", " + id + ", " + ddlCategorias.SelectedItem.Value.ToString() + ", " +
                    "'" + txbAsunto.Text.ToString() + "', '" + hiddenEditor.Value.ToString() + "', NOW())";
                clasesglobales cg = new clasesglobales();
                cg.TraerDatosStr(strQuery);
            }

            Response.Redirect("correointerno");
        }
    }
}