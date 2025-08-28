
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
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
            
            DataRow[] filasFiltradas = dt.Select("idSede = " + idSede + " AND ProcesoActFecNac = 0");

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

                // Tabla para crear actualizaciones
                DataTable dtUpdate = new DataTable();
                dtUpdate.Columns.Add("IdAfiliado", typeof(int));
                dtUpdate.Columns.Add("FechaNacimiento", typeof(string));
                dtUpdate.Columns.Add("Genero", typeof(int));

                string nombre, s_nombre, apellidoApi, s_apellidoApi, fechaNacimientoApi, generoApi, fechaNacimientoDb, generoDb;

                using (HttpClient client = new HttpClient())
                {
                    foreach (DataRow row in dt.Rows)
                    {

                        string cedula = row["DocumentoAfiliado"].ToString();
                        string url = $"https://pqrdsuperargo.supersalud.gov.co/api/api/adres/0/{cedula}";

                        apellidoApi = string.Empty;
                        s_apellidoApi = string.Empty;
                        fechaNacimientoApi = string.Empty;
                        generoApi = string.Empty;
                        fechaNacimientoDb =string.Empty;
                        generoDb = string.Empty;

                        HttpResponseMessage response = await client.GetAsync(url);
                        if (response.IsSuccessStatusCode)
                        {
                            string json = await response.Content.ReadAsStringAsync();
                            JObject data = JObject.Parse(json);

                            nombre = NormalizarTexto(data["nombre"]?.ToString());
                            s_nombre = NormalizarTexto(data["s_nombre"]?.ToString());
                            apellidoApi = NormalizarTexto(data["apellido"]?.ToString());
                            s_apellidoApi = NormalizarTexto(data["s_apellido"]?.ToString());

                            // valores BD
                            fechaNacimientoDb = row["FechaNacAfiliado"].ToString();
                            generoDb = row["idGenero"].ToString();
                            string nombreDb = row["Nombre"].ToString();
                            string sNombreDb = row["SegundoNombre"].ToString();
                            string apellidoDb = row["Apellido"].ToString();
                            string sApellidoDb = row["SegundoApellido"].ToString();

                            // validar fecha y género
                            if (!string.IsNullOrEmpty(fechaNacimientoApi) && !string.IsNullOrEmpty(generoApi))
                            {
                                bool actualizarFechaGenero = fechaNacimientoDb != fechaNacimientoApi || generoDb != generoApi;

                                // validar nombres/apellidos
                                bool actualizarNombre = nombreDb != nombre || sNombreDb != s_nombre || apellidoDb != apellidoApi || sApellidoDb != s_apellidoApi;

                                if (actualizarFechaGenero || actualizarNombre)
                                {
                                    dtUpdate.Rows.Add(
                                        Convert.ToInt32(row["IdAfiliado"]),
                                        fechaNacimientoApi,
                                        Convert.ToInt32(generoApi),
                                        nombre,
                                        s_nombre,
                                        apellidoApi,
                                        s_apellidoApi
                                    );
                                }
                            }
                        }
                    }
                }

                if (dtUpdate.Rows.Count > 0)
                {
                    string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                    await ActualizarAfiliadosAsync(dtUpdate, strConexion);
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

        //public async Task ActualizarAfiliadosAsync(DataTable dtUpdate, string connectionString)
        //{
        //    if (dtUpdate == null || dtUpdate.Rows.Count == 0)
        //        return;

        //    using (var conn = new MySqlConnection(connectionString))
        //    {
        //        await conn.OpenAsync();

        //        using (var cmd = new MySqlCommand("TRUNCATE TABLE AfiliadosUpdateFechaNacTmp;", conn))
        //        {
        //            await cmd.ExecuteNonQueryAsync();
        //        }

        //        // 2. Insertar en bloque en la staging
        //        var sb = new StringBuilder();
        //        sb.Append("INSERT INTO AfiliadosUpdateFechaNacTmp (IdAfiliado, FechaNacimiento, Genero) VALUES ");

        //        for (int i = 0; i < dtUpdate.Rows.Count; i++)
        //        {
        //            var row = dtUpdate.Rows[i];
        //            sb.AppendFormat("({0}, '{1}', {2})",
        //                row["IdAfiliado"],
        //                MySqlHelper.EscapeString(row["FechaNacimiento"].ToString()),
        //                string.IsNullOrEmpty(row["Genero"].ToString()) ? "NULL" : row["Genero"].ToString()
        //            );

        //            if (i < dtUpdate.Rows.Count - 1)
        //                sb.Append(",");
        //        }

        //        sb.Append(";");

        //        using (var cmd = new MySqlCommand(sb.ToString(), conn))
        //        {
        //            await cmd.ExecuteNonQueryAsync();
        //        }

        //        using (var cmd = new MySqlCommand("CALL Pa_ACTUALIZAR_PROCESO_FECHAS_LOTE();", conn))
        //        {
        //            await cmd.ExecuteNonQueryAsync();
        //        }
        //    }
        //}

        public async Task ActualizarAfiliadosAsync(DataTable dtUpdate, string connectionString)
        {
            if (dtUpdate == null || dtUpdate.Rows.Count == 0)
                return;

            using (var conn = new MySqlConnection(connectionString))
            {
                await conn.OpenAsync();

                // limpiar tabla temporal
                await new MySqlCommand("TRUNCATE TABLE AfiliadosUpdateFechaNacTmp;", conn).ExecuteNonQueryAsync();

                var sb = new StringBuilder();
                sb.Append("INSERT INTO AfiliadosUpdateFechaNacTmp (IdAfiliado, FechaNacimiento, Genero, Nombre, S_Nombre, Apellido, S_Apellido) VALUES ");

                for (int i = 0; i < dtUpdate.Rows.Count; i++)
                {
                    var row = dtUpdate.Rows[i];

                    sb.AppendFormat("({0}, '{1}', {2}, '{3}', '{4}', '{5}', '{6}')",
                        row["IdAfiliado"],
                        MySqlHelper.EscapeString(row["FechaNacimiento"].ToString()),
                        string.IsNullOrEmpty(row["Genero"].ToString()) ? "NULL" : row["Genero"].ToString(),
                        MySqlHelper.EscapeString(row["Nombre"].ToString()),
                        MySqlHelper.EscapeString(row["S_Nombre"].ToString()),
                        MySqlHelper.EscapeString(row["Apellido"].ToString()),
                        MySqlHelper.EscapeString(row["S_Apellido"].ToString())
                    );

                    if (i < dtUpdate.Rows.Count - 1)
                        sb.Append(",");
                }

                sb.Append(";");

                using (var cmd = new MySqlCommand(sb.ToString(), conn))
                {
                    await cmd.ExecuteNonQueryAsync();
                }

                // ejecutar SP
                using (var cmd = new MySqlCommand("CALL Pa_ACTUALIZAR_PROCESO_FECHAS_LOTE();", conn))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


        private string NormalizarTexto(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return texto;

            Dictionary<string, string> mapaReemplazos = new Dictionary<string, string>
            {
                { "¥", "Ñ" }, { "Ý", "Í" }, { "ý", "í" },
                { "¨", "Ü" }, { "¸", "ü" },
                { "Û", "Ó" }, { "û", "ó" },
                { "Õ", "Á" }, { "õ", "á" },
                { "Â", "É" }, { "â", "é" },
                { "Ê", "Ú" }, { "ê", "ú" }
                
            };

            foreach (var kvp in mapaReemplazos)
            {
                texto = texto.Replace(kvp.Key, kvp.Value);
            }

            return texto;
        }




    }

    //Respuesta de adres 
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
