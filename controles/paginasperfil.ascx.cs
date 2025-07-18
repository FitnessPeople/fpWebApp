﻿using System;
using System.Data;

namespace fpWebApp.controles
{
    public partial class paginasperfil : System.Web.UI.UserControl
    {
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
                "INNER JOIN CategoriasPaginas cp " +
                "ON p.idCategoria = cp.idCategoriaPagina " + 
                "INNER JOIN permisos_perfiles pp ON pp.idPagina = p.idPagina " +
                "AND pp.idPerfil = " + Session["idPerfil"].ToString() + " AND SinPermiso = 0 " +
                "ORDER BY cp.NombreCategoriaPagina, p.Pagina";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpPaginas.DataSource = dt;
            rpPaginas.DataBind();

            dt.Dispose();
        }
    }
}