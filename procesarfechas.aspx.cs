
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using MySql.Data.MySqlClient;
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
        //protected async void btnProcesar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string sedeSeleccionada = ddlSede.SelectedValue;
        //        DataTable dt;

        //        if (string.IsNullOrEmpty(sedeSeleccionada))
        //            dt = new clasesglobales().ConsultarAfiliados();
        //        else
        //            dt = ConsultarAfiliadosPorSedeSeleccionada(Convert.ToInt32(sedeSeleccionada));

        //        using (HttpClient client = new HttpClient())
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                string cedula = row["DocumentoAfiliado"].ToString();
        //                string url = $"https://pqrdsuperargo.supersalud.gov.co/api/api/adres/0/{cedula}";

        //                HttpResponseMessage response = await client.GetAsync(url);
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    string json = await response.Content.ReadAsStringAsync();
        //                    JObject data = JObject.Parse(json);

        //                    string fechaNacimientoApi = data["fecha_nacimiento"]?.ToString();
        //                    string generoApi = data["sexo"]?.ToString();

        //                    string fechaNacimientoDb = row["FechaNacAfiliado"].ToString();
        //                    string generoDb = row["idGenero"].ToString();

        //                    bool actualizar = fechaNacimientoDb != fechaNacimientoApi || generoDb != generoApi;

        //                    if (actualizar)
        //                    {
        //                        ActualizarAfiliadoEnBD(Convert.ToInt32(row["IdAfiliado"]),fechaNacimientoApi, Convert.ToInt32(generoApi) );
        //                    }
        //                }
        //            }
        //        }

        //        // refrescar grid
        //        gvAfiliados.DataSource = dt;
        //        gvAfiliados.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = ex.Message;
        //    }
        //}

        //private void ActualizarAfiliadoEnBD(int idAfiliado, string fechaNacimiento, int genero)
        //{
        //    clasesglobales cg = new clasesglobales();
        //   // cg.ActualizarProcesoFechasNacimiento(idAfiliado, fechaNacimiento, genero); // tu SP
        //}

        //protected async void btnProcesar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string sedeSeleccionada = ddlSede.SelectedValue;
        //        DataTable dt;

        //        if (string.IsNullOrEmpty(sedeSeleccionada))
        //            dt = new clasesglobales().ConsultarAfiliados();
        //        else
        //            dt = ConsultarAfiliadosPorSedeSeleccionada(Convert.ToInt32(sedeSeleccionada));

        //        // Tabla para acumular actualizaciones
        //        DataTable dtUpdate = new DataTable();
        //        dtUpdate.Columns.Add("IdAfiliado", typeof(int));
        //        dtUpdate.Columns.Add("FechaNacimiento", typeof(string));
        //        dtUpdate.Columns.Add("Genero", typeof(int));

        //        using (HttpClient client = new HttpClient())
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                string cedula = row["DocumentoAfiliado"].ToString();
        //                string url = $"https://pqrdsuperargo.supersalud.gov.co/api/api/adres/0/{cedula}";

        //                HttpResponseMessage response = await client.GetAsync(url);
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    string json = await response.Content.ReadAsStringAsync();
        //                    JObject data = JObject.Parse(json);

        //                    string fechaNacimientoApi = data["fecha_nacimiento"]?.ToString();
        //                    string generoApi = data["sexo"]?.ToString();

        //                    string fechaNacimientoDb = row["FechaNacAfiliado"].ToString();
        //                    string generoDb = row["idGenero"].ToString();

        //                    bool actualizar = fechaNacimientoDb != fechaNacimientoApi || generoDb != generoApi;

        //                    if (actualizar)
        //                    {
        //                        dtUpdate.Rows.Add(
        //                            Convert.ToInt32(row["IdAfiliado"]),
        //                            fechaNacimientoApi,
        //                            Convert.ToInt32(generoApi)
        //                        );
        //                    }
        //                }
        //            }
        //        }



        //        // Ejecutar SP de actualización en lote si hay cambios
        //        if (dtUpdate.Rows.Count > 0)
        //        {
        //            using (SqlConnection conn = new SqlConnection("tu_cadena_conexion"))
        //            using (SqlCommand cmd = new SqlCommand("Pa_ACTUALIZAR_PROCESO_FECHAS_LOTE", conn))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;

        //                SqlParameter tvpParam = cmd.Parameters.AddWithValue("@Afiliados", dtUpdate);
        //                tvpParam.SqlDbType = SqlDbType.Structured;
        //                tvpParam.TypeName = "AfiliadosUpdateFechaNacTVP";

        //                conn.Open();
        //                await cmd.ExecuteNonQueryAsync();
        //            }
        //        }

        //        // refrescar grid
        //        gvAfiliados.DataSource = dt;
        //        gvAfiliados.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = ex.Message;
        //    }
        //}

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

                // Tabla para acumular actualizaciones
                DataTable dtUpdate = new DataTable();
                dtUpdate.Columns.Add("IdAfiliado", typeof(int));
                dtUpdate.Columns.Add("FechaNacimiento", typeof(string));
                dtUpdate.Columns.Add("Genero", typeof(int));

                using (HttpClient client = new HttpClient())
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string cedula = row["DocumentoAfiliado"].ToString();
                        string url = $"https://pqrdsuperargo.supersalud.gov.co/api/api/adres/0/{cedula}";

                        HttpResponseMessage response = await client.GetAsync(url);
                        if (response.IsSuccessStatusCode)
                        {
                            string json = await response.Content.ReadAsStringAsync();
                            JObject data = JObject.Parse(json);

                            string fechaNacimientoApi = data["fecha_nacimiento"]?.ToString();
                            string generoApi = data["sexo"]?.ToString();

                            string fechaNacimientoDb = row["FechaNacAfiliado"].ToString();
                            string generoDb = row["idGenero"].ToString();

                            bool actualizar = fechaNacimientoDb != fechaNacimientoApi || generoDb != generoApi;

                            if (actualizar)
                            {
                                dtUpdate.Rows.Add(Convert.ToInt32(row["IdAfiliado"]), fechaNacimientoApi, Convert.ToInt32(generoApi));
                            }
                        }
                    }
                }

                // 👉 Aquí ya no usamos SqlConnection ni TVP, sino el método MySQL
                if (dtUpdate.Rows.Count > 0)
                {
                    await ActualizarAfiliadosAsync(dtUpdate, "tu_cadena_conexion_mysql");
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
        public async Task ActualizarAfiliadosAsync(DataTable dtUpdate, string connectionString)
        {
            if (dtUpdate == null || dtUpdate.Rows.Count == 0)
                return;

            using (var conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();

                using (var cmd = new MySqlCommand("TRUNCATE TABLE AfiliadosUpdateFechaNacTmp;", conn))
                {
                    await cmd.ExecuteNonQueryAsync();
                }

                // 2. Insertar en bloque en la staging
                var sb = new StringBuilder();
                sb.Append("INSERT INTO AfiliadosUpdateFechaNacTmp (IdAfiliado, FechaNacimiento, Genero) VALUES ");

                for (int i = 0; i < dtUpdate.Rows.Count; i++)
                {
                    var row = dtUpdate.Rows[i];
                    sb.AppendFormat("({0}, '{1}', {2})",
                        row["IdAfiliado"],
                        MySqlHelper.EscapeString(row["FechaNacimiento"].ToString()),
                        string.IsNullOrEmpty(row["Genero"].ToString()) ? "NULL" : row["Genero"].ToString()
                    );

                    if (i < dtUpdate.Rows.Count - 1)
                        sb.Append(",");
                }

                sb.Append(";");

                using (var cmd = new MySqlCommand(sb.ToString(), conn))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
                               
                using (var cmd = new MySqlCommand("CALL Pa_ACTUALIZAR_PROCESO_FECHAS_LOTE();", conn))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }





    }

    //REspuesta de adres 
    //    {
    //  "numero_doc": "1000142832",
    //  "nombre": "NICOLAS",
    //  "s_nombre": "",
    //  "apellido": "BOLIVAR",
    //  "s_apellido": "VALENCIA",
    //  "fecha_nacimiento": "2000-08-02",
    //  "edad": 25,
    //  "celular": "",
    //  "telefono": "",
    //  "direccion": "",
    //  "correo": "",
    //  "municipio_id": "1",
    //  "departamento_id": "68",
    //  "eps": "EPS005",
    //  "eps_id": 85,
    //  "eps_tipo": 6,
    //  "sexo": 1,
    //  "tipo_de_afiliado": 1,
    //  "estado_afiliacion": "AC en SANITAS municipio BUCARAMANGA"
    //}
}
