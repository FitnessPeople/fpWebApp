using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class paginasperfil : System.Web.UI.UserControl
    {
        OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listaPaginas();
            }
        }

        private void listaPaginas()
        {
            string strQuery = "SELECT * FROM paginas p " +
                "INNER JOIN permisos_perfiles pp ON pp.idPagina = p.idPagina " +
                "AND pp.idPerfil = " + Session["idPerfil"].ToString() + " AND SinPermiso = 0 " +
                "ORDER BY Categoria, Pagina";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpPaginas.DataSource = dt;
            rpPaginas.DataBind();

            dt.Dispose();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string strParam = txbBuscar.Value.ToString();
            //listaAfiliados(strParam);
        }
    }
}