using System;
using System.Data;

namespace fpWebApp
{
    public partial class editarhistoria : System.Web.UI.Page
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
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            CargarHistoria();
                            txbFum.Attributes.Add("type", "date");
                            txbCigarrillos.Attributes.Add("type", "number");
                            txbBebidas.Attributes.Add("type", "number");
                            CargarObjetivos();
                            btnAgregar.Visible = true;
                        }
                    }
                }
                else
                {
                    Response.Redirect("logout.aspx");
                }
            }
        }

        private void CargarObjetivos()
        {
            string strQuery = "SELECT * FROM ObjetivosAfiliado";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlObjetivo.DataSource = dt;
            ddlObjetivo.DataBind();

            dt.Dispose();
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

        private void CargarHistoria()
        {
            if (Request.QueryString.Count > 0)
            {
                string strQuery = "SELECT * FROM HistoriasClinicas WHERE idHistoria = " + Request.QueryString["editId"].ToString();
                clasesglobales cg1 = new clasesglobales();
                DataTable dt = cg1.TraerDatos(strQuery);
                if (dt.Rows.Count > 0)
                {
                    txbFum.Text = dt.Rows[0]["AnteFUM"].ToString();
                    txbCigarrillos.Text = dt.Rows[0]["Cigarrillos"].ToString();
                    txbBebidas.Text = dt.Rows[0]["Bebidas"].ToString();
                    ddlObjetivo.SelectedValue = dt.Rows[0]["idObjetivoIngreso"].ToString();
                }
                dt.Dispose();
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
}