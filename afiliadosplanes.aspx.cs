using System;
using System.Data;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class afiliadosplanes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Afiliados planes");
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
                        divBotonesLista.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            ListarAfiliadosPlanes();
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            ListarAfiliadosPlanes();
                            lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            ListarAfiliadosPlanes();
                            //CargarPlanes();
                        }
                    }
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

        private void ListarAfiliadosPlanes()
        {
            clasesglobales cg = new clasesglobales();
            string strQuery = @"
                SELECT 
	                ap.*, 
	                p.*, 
	                a.*, 
	                IF(p.DebitoAutomatico = 1,'Si', 'No') da, 
	                IF(p.DebitoAutomatico = 1,'info', 'warning') badge, 
	                IFNULL(hcr_cnt.Intentos, 0) AS Intentos 
                FROM AfiliadosPlanes ap 
                INNER JOIN Planes p ON p.idPlan = ap.idPlan 
                INNER JOIN Afiliados a ON a.idAfiliado = ap.idAfiliado 
                LEFT JOIN (
	                SELECT 
		                hcr.idAfiliadoPlan, 
		                COUNT(*) AS Intentos 
	                FROM HistorialCobrosRechazados hcr 
	                GROUP BY hcr.idAfiliadoPlan
                ) hcr_cnt ON hcr_cnt.idAfiliadoPlan = ap.idAfiliadoPlan 
                WHERE ap.EstadoPlan <> 'Archivado'";
            DataTable dt = cg.TraerDatos(strQuery);

            rpAfiliadosPlanes.DataSource = dt;
            rpAfiliadosPlanes.DataBind();
            dt.Dispose();
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {

        }

        protected void rpAfiliadosPlanes_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["Borrar"].ToString() == "1")
                {
                    Button btnCancelar = (Button)e.Item.FindControl("btnCancelar");
                    btnCancelar.Visible = true;
                }
            }
        }

        protected void btnCancelar_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "cancelarDebito")
            {
                int idAfiliadoPlan = int.Parse(e.CommandArgument.ToString());

                Response.Redirect("cancelardebito?idAfiliadoPlan=" + idAfiliadoPlan.ToString());
            }
        }
    }
}