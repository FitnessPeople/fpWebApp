
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
            ltCantidadRegistros.Text = filasFiltradas.Length.ToString() + "registros";

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
                dtUpdate.Columns.Add("Nombre", typeof(string));
                dtUpdate.Columns.Add("SegundoNombre", typeof(string));
                dtUpdate.Columns.Add("Apellido", typeof(string));
                dtUpdate.Columns.Add("SegundoApellido", typeof(string));


                string nombre, s_nombre, apellidoApi, s_apellidoApi, fechaNacimientoApi, generoApi, fechaNacimientoDb, generoDb;
                bool cambioNombre;
                bool cambio_sNombre;
                bool cambioApellido;
                bool cambio_sApellido;

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
                        fechaNacimientoDb = string.Empty;
                        generoDb = string.Empty;
                        cambioNombre = false;
                        cambio_sNombre = false;
                        cambioApellido = false;
                        cambio_sApellido = false;

                        HttpResponseMessage response = await client.GetAsync(url);
                        if (response.IsSuccessStatusCode)
                        {
                            string json = await response.Content.ReadAsStringAsync();
                            JObject data = JObject.Parse(json);

                            fechaNacimientoApi = data["fecha_nacimiento"]?.ToString();
                            generoApi = data["sexo"]?.ToString();

                            nombre = NormalizarTexto(data["nombre"]?.ToString(), out cambioNombre);
                            s_nombre = NormalizarTexto(data["s_nombre"]?.ToString(), out cambio_sNombre);
                            apellidoApi = NormalizarTexto(data["apellido"]?.ToString(), out cambioApellido);
                            s_apellidoApi = NormalizarTexto(data["s_apellido"]?.ToString(), out cambio_sApellido);


                            // valores BD
                            fechaNacimientoDb = row["FechaNacAfiliado"].ToString();
                            generoDb = row["idGenero"].ToString();
                            string nombreDb = row["NombreAfiliado"].ToString();
                            string apellidoDb = row["ApellidoAfiliado"].ToString();

                            // Detectar cambios en nombre y apellidos
                            bool actualizarNombreApellido1 = cambioNombre || cambio_sNombre || cambioApellido || cambio_sApellido;

                            // Detectar cambios en fecha de nacimiento o género
                            bool actualizarFechaGenero = (!string.IsNullOrEmpty(fechaNacimientoApi) && !string.IsNullOrEmpty(generoApi)) &&
                                                         (fechaNacimientoDb != fechaNacimientoApi || generoDb != generoApi);

                            string nombreCompletoApi = nombre;
                            string apellidoCompletoApi = apellidoApi;

                            if (!string.IsNullOrEmpty(nombreCompletoApi) && !string.IsNullOrEmpty(apellidoCompletoApi))
                            {
                                nombreCompletoApi = nombre + " " + s_nombre;
                                apellidoCompletoApi = apellidoApi + " " + s_apellidoApi;
                            }

                            bool actualizarNombreApellido2 = (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(apellidoApi)) && 
                                                             (nombreCompletoApi != nombreDb || apellidoCompletoApi != apellidoDb);

                            // Si hay cambios en cualquiera, se agrega el registro
                            if (actualizarNombreApellido1 || actualizarNombreApellido2 || actualizarFechaGenero)
                            {
                                dtUpdate.Rows.Add(
                                    Convert.ToInt32(row["IdAfiliado"]),
                                    string.IsNullOrEmpty(fechaNacimientoApi) ? fechaNacimientoDb : fechaNacimientoApi,
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

                var sb = new StringBuilder();
                sb.Append("INSERT INTO AfiliadosUpdateFechaNacTmp (IdAfiliado, FechaNacimiento, Genero, Nombre, SegundoNombre, Apellido, SegundoApellido) VALUES ");

                for (int i = 0; i < dtUpdate.Rows.Count; i++)
                {
                    var row = dtUpdate.Rows[i];

                    sb.AppendFormat("({0}, '{1}', {2}, '{3}', '{4}', '{5}', '{6}')",
                        row["IdAfiliado"],
                        MySqlHelper.EscapeString(row["FechaNacimiento"].ToString()),
                        string.IsNullOrEmpty(row["Genero"].ToString()) ? "NULL" : row["Genero"].ToString(),
                        MySqlHelper.EscapeString(row["Nombre"].ToString()),
                        MySqlHelper.EscapeString(row["SegundoNombre"].ToString()),
                        MySqlHelper.EscapeString(row["Apellido"].ToString()),
                        MySqlHelper.EscapeString(row["SegundoApellido"].ToString())
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

        private string NormalizarTexto(string texto, out bool modificado)
        {
            modificado = false;

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

            string original = texto;

            foreach (var kvp in mapaReemplazos)
            {
                if (texto.Contains(kvp.Key))
                {
                    texto = texto.Replace(kvp.Key, kvp.Value);
                    modificado = true;
                }
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
