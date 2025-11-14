using NPOI.OpenXmlFormats.Spreadsheet;
using System;
using System.Data;

namespace fpWebApp
{
    public partial class detallecorreo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
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

                    if (!string.IsNullOrEmpty(Request.QueryString["idCorreo"]))
                    {
                        string idCorreo = Request.QueryString["idCorreo"];
                        strQuery = @"SELECT *  
                            FROM correointerno ci 
                            INNER JOIN usuarios u ON u.idUsuario = ci.idUsuarioDe 
                            WHERE ci.idUsuarioPara = " + Session["idUsuario"].ToString() + @" 
                            AND ci.idCorreo = " + idCorreo;

                        DataTable dt = cg.TraerDatos(strQuery);

                        ltAsunto.Text = dt.Rows[0]["Asunto"].ToString();
                        ltFechaHora.Text = Convert.ToDateTime(dt.Rows[0]["FechaHora"]).ToString("dd 'de' MM 'de' yyyy, HH:mm:ss");
                        ltRemitente.Text = dt.Rows[0]["NombreUsuario"].ToString();
                        ltMensaje.Text = dt.Rows[0]["Mensaje"].ToString();

                        dt.Dispose();

                        MarcarComoLeido(idCorreo);
                    }
                }
                else
                {
                    Response.Redirect("logout");
                }
            }
        }

        private void CargarCategorias()
        {
            string strQuery = @"SELECT * 
                FROM categoriascorreo ";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpCategorias.DataSource = dt;
            rpCategorias.DataBind();

            dt.Dispose();
        }

        private void MarcarComoLeido(string idCorreo)
        {

            string strQuery = @"UPDATE correointerno SET Leido = 1 WHERE idCorreo = " + idCorreo;

            clasesglobales cg = new clasesglobales();
            cg.TraerDatosStr(strQuery);
        }

    }
}