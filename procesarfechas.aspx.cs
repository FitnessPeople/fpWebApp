//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace fpWebApp
//{
//    public partial class procesarfechas : System.Web.UI.Page
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            CargarSedes();
//        }



//        private void CargarSedes()
//        {
//            clasesglobales cg = new clasesglobales();
//            try
//            {
//                DataTable dt = cg.ConsultaCargarSedes("Todos");

//                ddlSede.DataSource = dt;
//                ddlSede.DataValueField = "idSede";
//                ddlSede.DataTextField = "NombreSede";
//                ddlSede.DataBind();
//            }
//            catch (Exception ex)
//            {
//                string mensaje = ex.Message.ToString();
//            }
//        }

//        private DataTable ConsultarAfiliadosPorSedeSeleccionada(int idSede)
//        {
//            clasesglobales cg = new clasesglobales();
//            DataTable dt = cg.ConsultarAfiliados();

//            DataRow[] filasFiltradas = dt.Select("idSede = " + idSede);

//            if (filasFiltradas.Length > 0)
//                return filasFiltradas.CopyToDataTable();
//            else
//                return dt.Clone(); // devuelve la estructura vacía
//        }

//        protected void btnConsultar_Click(object sender, EventArgs e)
//        {
//            try
//            {                
//                string sedeSeleccionada = ddlSede.SelectedValue;


//                if (string.IsNullOrEmpty(sedeSeleccionada))
//                {

//                    DataTable dt = new clasesglobales().ConsultarAfiliados();
//                    gvAfiliados.DataSource = dt; 
//                    gvAfiliados.DataBind();
//                }
//                else
//                {                    
//                    int idSede = Convert.ToInt32(sedeSeleccionada);
//                    DataTable dt = ConsultarAfiliadosPorSedeSeleccionada(idSede);

//                    gvAfiliados.DataSource = dt; 
//                    gvAfiliados.DataBind();
//                }
//            }
//            catch (Exception ex)
//            {
//                string mensaje = ex.Message;          
//            }
//        }


//    }
//}

using System;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using Newtonsoft.Json.Linq;

namespace fpWebApp
{
    public partial class procesarfechas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Para que no recargue siempre
                CargarSedes();
        }

        private void CargarSedes()
        {
            clasesglobales cg = new clasesglobales();
            try
            {
                DataTable dt = cg.ConsultaCargarSedes("Todos");
                ddlSede.DataSource = dt;
                ddlSede.DataValueField = "idSede";
                ddlSede.DataTextField = "NombreSede";
                ddlSede.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
        }

        private DataTable ConsultarAfiliadosPorSedeSeleccionada(int idSede)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarAfiliados();

            DataRow[] filasFiltradas = dt.Select("idSede = " + idSede);
            if (filasFiltradas.Length > 0)
                return filasFiltradas.CopyToDataTable();
            else
                return dt.Clone();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                string sedeSeleccionada = ddlSede.SelectedValue;
                DataTable dt;

                if (string.IsNullOrEmpty(sedeSeleccionada))
                    dt = new clasesglobales().ConsultarAfiliados();
                else
                    dt = ConsultarAfiliadosPorSedeSeleccionada(Convert.ToInt32(sedeSeleccionada));

                gvAfiliados.DataSource = dt;
                gvAfiliados.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
        }

        // 🔹 Aquí está el nuevo botón asincrónico
        protected async void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                string sedeSeleccionada = ddlSede.SelectedValue;
                DataTable dt;

                if (string.IsNullOrEmpty(sedeSeleccionada))
                    dt = new clasesglobales().ConsultarAfiliados();
                else
                    dt = ConsultarAfiliadosPorSedeSeleccionada(Convert.ToInt32(sedeSeleccionada));

                using (HttpClient client = new HttpClient())
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string cedula = row["Cedula"].ToString();
                        string url = $"https://pqrdsuperargo.supersalud.gov.co/api/api/adres/0/{cedula}";

                        HttpResponseMessage response = await client.GetAsync(url);
                        if (response.IsSuccessStatusCode)
                        {
                            string json = await response.Content.ReadAsStringAsync();
                            JObject data = JObject.Parse(json);

                            string fechaNacimientoApi = data["fecha_nacimiento"]?.ToString();
                            string generoApi = data["sexo"]?.ToString();

                            string fechaNacimientoDb = row["FechaNacimiento"].ToString();
                            string generoDb = row["Genero"].ToString();

                            bool actualizar = fechaNacimientoDb != fechaNacimientoApi || generoDb != generoApi;

                            if (actualizar)
                            {
                                ActualizarAfiliadoEnBD(
                                    Convert.ToInt32(row["IdAfiliado"]),
                                    fechaNacimientoApi,
                                    generoApi
                                );
                            }
                        }
                    }
                }

                // refrescar grid
                gvAfiliados.DataSource = dt;
                gvAfiliados.DataBind();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
        }

        private void ActualizarAfiliadoEnBD(int idAfiliado, string fechaNacimiento, int genero)
        {
            clasesglobales cg = new clasesglobales();
            cg.ActualizarProcesoFechasNacimiento(idAfiliado, fechaNacimiento, genero); // tu SP
        }
    }
}
