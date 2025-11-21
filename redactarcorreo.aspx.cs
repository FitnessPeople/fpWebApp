using MySql.Data.MySqlClient;
using NPOI.OpenXmlFormats.Spreadsheet;
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

                    string strQuery = @"
                        SELECT *  
                        FROM CorreoInterno 
                        WHERE idUsuarioPara = " + Session["idUsuario"].ToString() + @" 
                          AND LeidoPara = 0 
                          AND PapeleraPara = 0";

                    DataTable dt1 = cg.TraerDatos(strQuery);

                    ltNroMensajesSinLeer.Text = dt1.Rows.Count.ToString();

                    dt1.Dispose();

                    strQuery = @"
                        SELECT * 
                        FROM CorreoInterno
                        WHERE (idUsuarioPara = " + Session["idUsuario"].ToString() + @" AND PapeleraPara = 1)
                           OR (idUsuarioDe = " + Session["idUsuario"].ToString() + @" AND PapeleraDe = 1);";

                    DataTable dt2 = cg.TraerDatos(strQuery);

                    ltNroMensajesPapelera.Text = dt2.Rows.Count.ToString();

                    dt2.Dispose();

                    strQuery = @"
                        SELECT * 
                        FROM CorreoInterno
                        WHERE idUsuarioDe = " + Session["idUsuario"].ToString() + @" 
                          AND PapeleraDe = 0";

                    DataTable dt3 = cg.TraerDatos(strQuery);

                    ltNroMensajesEnviados.Text = dt3.Rows.Count.ToString();

                    dt3.Dispose();

                    strQuery = @"
                        SELECT * 
                        FROM CorreoInterno 
                        WHERE IdUsuarioPara = " + Session["idUsuario"].ToString() + @" 
                          AND PapeleraPara = 0";

                    DataTable dt4 = cg.TraerDatos(strQuery);

                    ltNroMensajesTotal.Text = dt4.Rows.Count.ToString();

                    dt4.Dispose();

                    ltNombreUsuario.Text = Session["NombreUsuario"].ToString();
                    ltCargo.Text = Session["CargoUsuario"].ToString();
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

            if (seleccionados.Count != 0 && txbAsunto.Text.ToString() != "")
            {
                //string valoresComoString = string.Join(",", seleccionados);

                //string contenidoEditor = hiddenEditor.Value;
                //string strQuery = "INSERT INTO correointerno (idUsuarioDe, idsPara, idCategoriaCorreo, Asunto, Mensaje, FechaHora) " +
                //    "VALUES (" + Session["idUsuario"].ToString() + ", '" + valoresComoString + "', " + ddlCategorias.SelectedItem.Value.ToString() + ", " +
                //    "'" + txbAsunto.Text.ToString() + "', '" + contenidoEditor + "', NOW())";
                //clasesglobales cg = new clasesglobales();
                //cg.TraerDatosStr(strQuery);

                string contenidoEditor = hiddenEditor.Value;
                foreach (string id in seleccionados)
                {
                    string strQuery = "INSERT INTO correointerno (idUsuarioDe, idUsuarioPara, idCategoriaCorreo, Asunto, Mensaje, FechaHora) " +
                        "VALUES (" + Session["idUsuario"].ToString() + ", " + id + ", " + ddlCategorias.SelectedItem.Value.ToString() + ", " +
                        "'" + txbAsunto.Text.ToString() + "', '" + contenidoEditor + "', NOW())";
                    clasesglobales cg = new clasesglobales();
                    cg.TraerDatosStr(strQuery);
                }

                Response.Redirect("correointerno");
            }

        }
    }
}