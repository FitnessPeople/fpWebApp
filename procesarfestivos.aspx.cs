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
                    ValidarPermisos("Días festivos");
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
                   
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                           
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;                            
                        }
                    }

                    CargarAnios();
                    ListaFestivos();
                    ltTitulo.Text = "Insertar festivos";
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
            try
            {
                clasesglobales cg = new clasesglobales();
                string url = string.Empty;
                string urlBase = string.Empty;

                DataTable dt = cg.ConsultarUrl(7);

                urlBase = dt.Rows[0]["urlTest"].ToString();
                url = urlBase + ano;

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<List<HolidayApi>>(json);
                }
            }
            catch (Exception ex)
            {
                ltMensaje.Text = "<div class='alert alert-danger'>Error: " + ex.Message + "</div>";
                return new List<HolidayApi>();
            }
        }


        public async Task ActualizarFestivos(int ano)
        {
            try
            {
                clasesglobales cg = new clasesglobales();

                List<HolidayApi> festivos = await ObtenerFestivosPorAno(ano);

                foreach (var f in festivos)
                {
                    DateTime fecha = DateTime.Parse(f.date);
                    cg.InsertarFestivo(f.name, fecha);
                }
            }
            catch (Exception ex)
            {
                ltMensaje.Text = "<div class='alert alert-danger'>Error: " + ex.Message + "</div>";                
            }
        }

        protected async void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlAnio.SelectedValue) || ddlAnio.SelectedValue == "0")
                {
                    string warning = "Debe seleccionar un año válido.";
                    string scriptWarning = $@"Swal.fire({{ title: 'Año no válido', text: '{HttpUtility.JavaScriptStringEncode(warning)}', icon: 'warning' }});";

                    // Si tienes ScriptManager:
                    if (ScriptManager.GetCurrent(this.Page) != null)
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert1", scriptWarning, true);
                    else
                        ClientScript.RegisterStartupScript(this.GetType(), "alert1", scriptWarning, true);

                    return;
                }

                int ano = int.Parse(ddlAnio.SelectedValue);

                await ActualizarFestivos(ano);


                string mensaje = $"Festivos del año {ano} actualizados correctamente.";
                string scriptSuccess = $@"
                Swal.fire({{
                    title: 'Proceso completado',
                    text: '{HttpUtility.JavaScriptStringEncode(mensaje)}',
                    icon: 'success'
                }}).then(() => {{

                    var btn = document.getElementById('{btnRefrescar.ClientID}');
                    if(btn) btn.click();
                    else __doPostBack('{btnRefrescar.UniqueID}','');
                }});";

                if (ScriptManager.GetCurrent(this.Page) != null)
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertPostBack", scriptSuccess, true);
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "alertPostBack", scriptSuccess, true);
            }
            catch (Exception ex)
            {
                string err = HttpUtility.JavaScriptStringEncode(ex.Message);
                string scriptError = $@"Swal.fire({{ title: 'Error', text: '{err}', icon: 'error' }});";

                if (ScriptManager.GetCurrent(this.Page) != null)
                    ScriptManager.RegisterStartupScript(this, GetType(), "errorAlert", scriptError, true);
                else
                    ClientScript.RegisterStartupScript(this.GetType(), "errorAlert", scriptError, true);
            }
        }


        private void CargarAnios()
        {
            try
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
            catch (Exception ex)
            {
                ltMensaje.Text = "<div class='alert alert-danger'>Error: " + ex.Message + "</div>";
            }
        }

        protected async void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAnio.SelectedValue != "0")  // Ignora "Todos"
                {
                    int ano = int.Parse(ddlAnio.SelectedValue);
                    await ActualizarFestivos(ano);

                    ltMensaje.Text = $"Festivos del año {ano} procesados.";
                }
            }
            catch (Exception ex)
            {
                ltMensaje.Text = "<div class='alert alert-danger'>Error: " + ex.Message + "</div>";
            }
        }

        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            try
            {
                ListaFestivos(); 
            }
            catch (Exception ex)
            {
                ltMensaje.Text = "<div class='alert alert-danger'>Error: " + ex.Message + "</div>";
            }
           
        }
    }
}