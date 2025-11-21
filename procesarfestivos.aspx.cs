using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class procesarfestivos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Páginas");
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
                        btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = false;
                           // CargarCategorias();
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                            //CargarCategorias();
                        }
                    }

                    CargarAnios();
                    ListaFestivos();
                    ltTitulo.Text = "Insertar festivos";

                    //if (Request.QueryString.Count > 0)
                    //{
                    //    rpPaginas.Visible = false;
                    //    if (Request.QueryString["editid"] != null)
                    //    {
                    //        //Editar
                    //        clasesglobales cg = new clasesglobales();
                    //        DataTable dt = cg.ConsultarPaginaPorId(int.Parse(Request.QueryString["editid"].ToString()));
                    //        if (dt.Rows.Count > 0)
                    //        {
                    //            txbPagina.Text = dt.Rows[0]["Pagina"].ToString();
                    //            txbAspx.Text = dt.Rows[0]["NombreAspx"].ToString();
                    //            txbIconoFA.Text = dt.Rows[0]["IconoFA"].ToString();
                    //            ddlCategorias.SelectedIndex = Convert.ToInt16(ddlCategorias.Items.IndexOf(ddlCategorias.Items.FindByValue(dt.Rows[0]["idCategoria"].ToString())));

                    //            btnAgregar.Text = "Actualizar";
                    //            ltTitulo.Text = "Actualizar Página";
                    //        }
                    //        dt.Dispose();
                    //    }
                    //    if (Request.QueryString["deleteid"] != null)
                    //    {
                    //        //Las paginas no se pueden borrar
                    //        ltMensaje.Text = "<div class=\"ibox-content\">" +
                    //            "<div class=\"alert alert-danger alert-dismissable\">" +
                    //            "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    //            "Esta página no se puede borrar." +
                    //            "</div></div>";

                    //        clasesglobales cg = new clasesglobales();
                    //        DataTable dt = cg.ConsultarPaginaPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                    //        if (dt.Rows.Count > 0)
                    //        {
                    //            txbPagina.Text = dt.Rows[0]["Pagina"].ToString();
                    //            txbPagina.Enabled = false;
                    //            txbAspx.Text = dt.Rows[0]["NombreAspx"].ToString();
                    //            txbAspx.Enabled = false;
                    //            txbIconoFA.Text = dt.Rows[0]["IconoFA"].ToString();
                    //            txbIconoFA.Enabled = false;
                    //            ddlCategorias.SelectedIndex = Convert.ToInt16(ddlCategorias.Items.IndexOf(ddlCategorias.Items.FindByValue(dt.Rows[0]["idCategoria"].ToString())));
                    //            ddlCategorias.Enabled = false;
                    //            btnAgregar.Text = "⚠ Confirmar borrado ❗";
                    //            btnAgregar.Enabled = false;
                    //            ltTitulo.Text = "Borrar Página";
                    //        }
                    //        dt.Dispose();
                    //    }
                    //}
                }
                else
                {
                    Response.Redirect("logout");
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

        private void ListaFestivos()
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarDiasFestivos();
                rpFestivos.DataSource = dt;
                rpFestivos.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                ltMensaje.Text =
                    "<div class='alert alert-danger alert-dismissable'>" +
                    "<button aria-hidden='true' data-dismiss='alert' class='close' type='button'>×</button>" +
                    "<strong>Error:</strong> " + ex.Message.ToString() +
                    "</div>";
            }
        }

        public class HolidayApi
        {
            public string date { get; set; }
            public string name { get; set; }
        }
        public async Task<List<HolidayApi>> ObtenerFestivosPorAno(int ano)
        {
            clasesglobales cg = new clasesglobales();
            string url = string.Empty;
 
            DataTable dt = cg.ConsultarUrl(7);
            url = dt.Rows[0]["urlTest"].ToString();
            //url = $"https://api-colombia.com/api/v1/Holiday/year/{ano}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<HolidayApi>>(json);
            }
        }

        public async Task ActualizarFestivos()
        {
            clasesglobales cg = new clasesglobales();   

            for (int ano = 2025; ano <= 2026; ano++)
            {
                List<HolidayApi> festivos = await ObtenerFestivosPorAno(ano);

                foreach (var f in festivos)
                {
                    DateTime fecha = DateTime.Parse(f.date);

                    cg.InsertarFestivo(f.name, fecha);
                }
            }
        }
        protected async void btnActualizarFestivos_Click(object sender, EventArgs e)
        {
            try
            {
                await ActualizarFestivos();

                ltMensaje.Text =
                    "<div class='alert alert-success'>Festivos actualizados correctamente.</div>";
            }
            catch (Exception ex)
            {
                ltMensaje.Text =
                    "<div class='alert alert-danger'>Error: " + ex.Message + "</div>";
            }
        }

        private void CargarAnios()
        {
            int anioActual = DateTime.Now.Year;
            int anioFinal = 2051;

            ddlAnio.Items.Clear();
            ddlAnio.Items.Add(new ListItem("Todos", "0"));

            for (int anio = anioActual; anio <= anioFinal; anio++)
            {
                ddlAnio.Items.Add(new ListItem(anio.ToString(), anio.ToString()));
            }
        }




        protected void rpPaginas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        private bool ValidarPagina(string strNombre)
        {
            bool bExiste = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPaginaPorNombre(strNombre);
            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }
            dt.Dispose();
            return bExiste;
        }

        protected async void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                await ActualizarFestivos(); // <-- Llama el método que carga 2025–2050

                ltMensaje.Text =
                    "<div class='alert alert-success alert-dismissable'>" +
                    "<button aria-hidden='true' data-dismiss='alert' class='close' type='button'>×</button>" +
                    "Los festivos fueron actualizados correctamente." +
                    "</div>";
            }
            catch (Exception ex)
            {
                ltMensaje.Text =
                    "<div class='alert alert-danger alert-dismissable'>" +
                    "<button aria-hidden='true' data-dismiss='alert' class='close' type='button'>×</button>" +
                    "Error: " + ex.Message +
                    "</div>";
            }
        }




        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarPaginas();
                string nombreArchivo = $"Paginas_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    cg.ExportarExcel(dt, nombreArchivo);
                }
                else
                {
                    Response.Write("<script>alert('No existen registros para esta consulta');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al exportar: " + ex.Message + "');</script>");
            }
        }

        private string TraerData()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPaginaPorId(int.Parse(Request.QueryString["editid"].ToString()));

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();

            return strData;
        }

        protected void rpFestivos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "procesarfestivos?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "procesarfestivos?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}