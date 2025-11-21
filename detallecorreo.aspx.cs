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
                    string strQuery = @"
                        SELECT * 
                        FROM CorreoInterno 
                        WHERE IdUsuarioPara = " + Session["idUsuario"].ToString() + @" 
                          AND LeidoPara = 0
                          AND PapeleraPara = 0";

                    DataTable dt1 = cg.TraerDatos(strQuery);

                    ltNroMensajesSinLeer.Text = dt1.Rows.Count.ToString();

                    dt1.Dispose();

                    strQuery = @"
                        SELECT * 
                        FROM CorreoInterno 
                        WHERE IdUsuarioPara = " + Session["idUsuario"].ToString() + @" 
                          AND PapeleraPara = 0";

                    DataTable dt2 = cg.TraerDatos(strQuery);

                    ltNroMensajesTotal.Text = dt2.Rows.Count.ToString();

                    dt2.Dispose();

                    strQuery = @"
                    SELECT * 
                    FROM CorreoInterno
                    WHERE (idUsuarioPara = " + Session["idUsuario"].ToString() + @" AND PapeleraPara = 1)
                       OR (idUsuarioDe = " + Session["idUsuario"].ToString() + @" AND PapeleraDe = 1);";

                    DataTable dt3 = cg.TraerDatos(strQuery);

                    ltNroMensajesPapelera.Text = dt3.Rows.Count.ToString();

                    dt3.Dispose();

                    strQuery = @"
                    SELECT * 
                    FROM CorreoInterno
                    WHERE idUsuarioDe = " + Session["idUsuario"].ToString() + @" 
                      AND PapeleraDe = 0";

                    DataTable dt4 = cg.TraerDatos(strQuery);

                    ltNroMensajesEnviados.Text = dt4.Rows.Count.ToString();

                    dt4.Dispose();

                    if (!string.IsNullOrEmpty(Request.QueryString["idCorreo"]))
                    {
                        string idCorreo = Request.QueryString["idCorreo"];
                        strQuery = @"
                            SELECT ci.*, cc.*, 
                                u1.NombreUsuario AS Remitente, u2.NombreUsuario AS Destinatario 
                            FROM correointerno ci
                            INNER JOIN usuarios u1 ON u1.idUsuario = ci.idUsuarioDe 
                            INNER JOIN usuarios u2 ON u2.idUsuario = ci.idUsuarioPara 
                            INNER JOIN categoriasCorreo cc ON cc.idCategoriaCorreo = ci.idCategoriaCorreo 
                            WHERE ci.idCorreo = " + idCorreo;

                        DataTable dt = cg.TraerDatos(strQuery);

                        ltAsunto.Text = dt.Rows[0]["Asunto"].ToString();
                        ltFechaHora.Text = Convert.ToDateTime(dt.Rows[0]["FechaHora"]).ToString("dd 'de' MMM 'de' yyyy, HH:mm:ss");
                        ltRemitente.Text = dt.Rows[0]["Remitente"].ToString();
                        ltDestinatarios.Text = dt.Rows[0]["Destinatario"].ToString();
                        ltMensaje.Text = dt.Rows[0]["Mensaje"].ToString();

                        if (dt.Rows[0]["idUsuarioDe"].ToString() == Session["idUsuario"].ToString() || !string.IsNullOrEmpty(Request.QueryString["trash"]))
                        {
                            // No puede borrar el mensaje
                            lnkEliminarTop.Visible = false;
                            lkbEliminar.Visible = false;
                        }
                        else
                        {

                            MarcarComoLeido(idCorreo);
                        }

                        dt.Dispose();
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

            string strQuery = @"
                UPDATE CorreoInterno
                SET LeidoDe = 1
                WHERE idCorreo = " + idCorreo + @" 
                AND idUsuarioDe = " + Session["idUsuario"].ToString();

            clasesglobales cg = new clasesglobales();
            cg.TraerDatosStr(strQuery);

            strQuery = @"
                UPDATE CorreoInterno
                SET LeidoPara = 1
                WHERE idCorreo = " + idCorreo + @" 
                AND idUsuarioPara = " + Session["idUsuario"].ToString();

            cg.TraerDatosStr(strQuery);
        }

        protected void lkbEliminar_Click(object sender, EventArgs e)
        {
            string idCorreo = Request.QueryString["idCorreo"];
            string strQuery = @"
                UPDATE CorreoInterno
                SET PapeleraDe = 1
                WHERE idCorreo = " + idCorreo + @" 
                AND idUsuarioDe = " + Session["idUsuario"].ToString();

            clasesglobales cg = new clasesglobales();
            cg.TraerDatosStr(strQuery);

            strQuery = @"
                UPDATE CorreoInterno
                SET PapeleraPara = 1
                WHERE idCorreo = " + idCorreo + @" 
                AND idUsuarioPara = " + Session["idUsuario"].ToString();

            cg.TraerDatosStr(strQuery);

            Response.Redirect("correointerno");
        }
    }
}