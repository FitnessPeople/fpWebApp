using System;
using System.Data;

namespace fpWebApp
{
    public partial class imprimirhistoriaclinica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strQuery = "SELECT *, " +
                "IF(TIMESTAMPDIFF(YEAR, a.FechaNacAfiliado, CURDATE()) IS NOT NULL, TIMESTAMPDIFF(YEAR, a.FechaNacAfiliado, CURDATE()),'') AS edad " +
                "FROM HistoriasClinicas hc " +
                "LEFT JOIN Afiliados a ON hc.idAfiliado = a.idAfiliado " +
                "LEFT JOIN Generos g ON a.idGenero = g.idGenero " +
                "LEFT JOIN HistoriaAlimentaria ha on hc.idHistoria = ha.idHistoria " +
                "LEFT JOIN HistoriaDeportiva hd on hc.idHistoria = hd.idHistoria " +
                "LEFT JOIN HistoriaFisioterapeuta hf on hc.idHistoria = hf.idHistoria " +
                "WHERE hc.idHistoria = " + Request.QueryString["editid"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpHistoriasClinicas.DataSource = dt;
            rpHistoriasClinicas.DataBind();

            dt.Dispose();
        }
    }
}