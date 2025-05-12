using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class imprimirhistoriaclinica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strQuery = "SELECT *, " +
                "IF(g.idGenero=1,'<i class=\"fa fa-mars\"></i>',IF(g.idGenero=2,'<i class=\"fa fa-venus\"></i>','<i class=\"fa fa-venus-mars\"></i>')) AS iconGenero, " +
                "IF(TIMESTAMPDIFF(YEAR, a.FechaNacAfiliado, CURDATE()) IS NOT NULL, TIMESTAMPDIFF(YEAR, a.FechaNacAfiliado, CURDATE()),'') AS edad, " +
                "IF(Tabaquismo=0,'<i class=\"fa fa-xmark text-navy\"></i>','<i class=\"fa fa-check text-danger\"></i>') AS fuma, " +
                "IF(Alcoholismo=0,'<i class=\"fa fa-xmark text-navy\"></i>','<i class=\"fa fa-check text-danger\"></i>') AS toma, " +
                "IF(Sedentarismo=0,'<i class=\"fa fa-xmark text-navy\"></i>','<i class=\"fa fa-check text-danger\"></i>') AS sedentario, " +
                "IF(Diabetes=0,'<i class=\"fa fa-xmark text-navy\"></i>','<i class=\"fa fa-check text-danger\"></i>') AS diabetico, " +
                "IF(Colesterol=0,'<i class=\"fa fa-xmark text-navy\"></i>',IF(Colesterol=1,'<i class=\"fa fa-check text-danger\"></i>','<i class=\"fa fa-comment-slash text-primary\"></i>')) AS colesterado, " +
                "IF(Trigliceridos=0,'<i class=\"fa fa-xmark text-navy\"></i>',IF(Trigliceridos=1,'<i class=\"fa fa-check text-danger\"></i>','<i class=\"fa fa-comment-slash text-primary\"></i>')) AS triglicerado, " +
                "IF(HTA=0,'<i class=\"fa fa-xmark text-navy\"></i>',IF(HTA=1,'<i class=\"fa fa-check text-danger\"></i>','<i class=\"fa fa-comment-slash text-primary\"></i>')) AS hipertenso," +
                "IF(Gastritis=0,'<i class=\"fa fa-xmark text-navy\"></i>','<i class=\"fa fa-check text-danger\"></i>') AS gastritico, " +
                "IF(Colon=0,'<i class=\"fa fa-xmark text-navy\"></i>','<i class=\"fa fa-check text-danger\"></i>') AS colonico, " +
                "IF(Estrenimiento=0,'<i class=\"fa fa-xmark text-navy\"></i>','<i class=\"fa fa-check text-danger\"></i>') AS estrenido " +
                "FROM HistoriasClinicas hc " +
                "LEFT JOIN Afiliados a ON hc.idAfiliado = a.idAfiliado " +
                "LEFT JOIN Generos g ON a.idGenero = g.idGenero " +
                "JOIN HistoriaAlimentaria ha on hc.idHistoria = ha.idHistoria " +
                "JOIN HistoriaDeportiva hd on hc.idHistoria = hd.idHistoria " +
                "JOIN HistoriaFisioterapeuta hf on hc.idHistoria = hf.idHistoria " +
                "WHERE hc.idHistoria = " + Request.QueryString["editid"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpHistoriasClinicas.DataSource = dt;
            rpHistoriasClinicas.DataBind();

            dt.Dispose();
        }
    }
}