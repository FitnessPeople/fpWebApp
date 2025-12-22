using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class historiasclinicas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Historias clinicas");

                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        //No tiene acceso a esta página
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    else
                    {
                        //Si tiene acceso a esta página
                        btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }
                    ListaHistorias();
                    
                }
                else
                {
                    Response.Redirect("logout.aspx");
                }
            }
        }

        private void ValidarPermisos(string strPagina)
        {
            ViewState["SinPermiso"] = "1";
            ViewState["Consulta"] = "0";
            ViewState["Exportar"] = "0";
            ViewState["CrearModificar"] = "0";
            ViewState["Borrar"] = "0";

            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ValidarPermisos(strPagina, Session["idPerfil"].ToString(), Session["idusuario"].ToString());

            if (dt.Rows.Count > 0)
            {
                ViewState["SinPermiso"] = dt.Rows[0]["SinPermiso"].ToString();
                ViewState["Consulta"] = dt.Rows[0]["Consulta"].ToString();
                ViewState["Exportar"] = dt.Rows[0]["Exportar"].ToString();
                ViewState["CrearModificar"] = dt.Rows[0]["CrearModificar"].ToString();
                ViewState["Borrar"] = dt.Rows[0]["Borrar"].ToString();
            }

            dt.Dispose();
        }

        private void ListaHistorias()
        {
            string strQuery = "SELECT *, " +
                "IF(g.idGenero=1,'<i class=\"fa fa-mars text-success\"></i>',IF(g.idGenero=2,'<i class=\"fa fa-venus text-danger\"></i>','<i class=\"fa fa-venus-mars text-warning\"></i>')) AS iconGenero, " +
                "IF(TIMESTAMPDIFF(YEAR, a.FechaNacAfiliado, CURDATE()) IS NOT NULL, TIMESTAMPDIFF(YEAR, a.FechaNacAfiliado, CURDATE()),'') AS edad, " +
                "IF(Tabaquismo=0,'<i class=\"fa fa-xmark text-navy\"></i>','<i class=\"fa fa-check text-danger\"></i>') AS fuma, " +
                "IF(Alcoholismo=0,'<i class=\"fa fa-xmark text-navy\"></i>','<i class=\"fa fa-check text-danger\"></i>') AS toma, " +
                "IF(Sedentarismo=0,'<i class=\"fa fa-xmark text-navy\"></i>','<i class=\"fa fa-check text-danger\"></i>') AS sedentario, " +
                "IF(Diabetes=0,'<i class=\"fa fa-xmark text-navy\"></i>','<i class=\"fa fa-check text-danger\"></i>') AS diabetico, " +
                "IF(Colesterol=0,'<i class=\"fa fa-xmark text-navy\"></i>',IF(Colesterol=1,'<i class=\"fa fa-check text-danger\"></i>','<i class=\"fa fa-comment-slash text-primary\"></i>')) AS colesterado, " +
                "IF(Trigliceridos=0,'<i class=\"fa fa-xmark text-navy\"></i>',IF(Trigliceridos=1,'<i class=\"fa fa-check text-danger\"></i>','<i class=\"fa fa-comment-slash text-primary\"></i>')) AS triglicerado, " +
                "IF(HTA=0,'<i class=\"fa fa-xmark text-navy\"></i>',IF(HTA=1,'<i class=\"fa fa-check text-danger\"></i>','<i class=\"fa fa-comment-slash text-primary\"></i>')) AS hipertenso " +
                "FROM HistoriasClinicas hc " +
                "LEFT JOIN Afiliados a ON hc.idAfiliado = a.idAfiliado " +
                "LEFT JOIN Generos g ON a.idGenero = g.idGenero " +
                "ORDER BY FechaHora DESC";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            rpHistoriasClinicas.DataSource = dt;
            rpHistoriasClinicas.DataBind();

            dt.Dispose();
        }

        protected void rpHistoriasClinicas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlButton btnEditar = (HtmlButton)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("onClick", "window.location.href='editarhistoria?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                    btnEditar.Visible = true;

                    HtmlButton btnAgregar = (HtmlButton)e.Item.FindControl("btnAgregar");
                    //btnAgregar.Attributes.Add("onClick", "window.location.href='agregarcontrol?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                    btnAgregar.Attributes.Add("onClick", "window.location.href='verhistoriaclinica?idAfiliado=" + ((DataRowView)e.Item.DataItem).Row[1].ToString() + "'");
                    btnAgregar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlButton btnEliminar = (HtmlButton)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("onClick", "window.location.href='eliminarhistoria?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "'");
                    btnEliminar.Visible = true;
                }
                if (ViewState["Exportar"].ToString() == "1")
                {
                    HtmlButton btnImprimir = (HtmlButton)e.Item.FindControl("btnImprimir");
                    btnImprimir.Attributes.Add("onClick", "window.open('imprimirhistoriaclinica?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString() + "','_blank')");
                    btnImprimir.Visible = true;
                }
            }
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {

        }
    }
}