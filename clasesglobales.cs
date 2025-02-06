using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Configuration;
using System.Text;
using System.Net.Mail;
using MySql.Data.MySqlClient;
using System.Web.Configuration;

namespace fpWebApp
{
    public class clasesglobales
    {
        public DataTable consultarCiudades()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CIUDADES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@p_id_ciudad", codigoCiudad);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCiudadesPorId(int codigoCiudad)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CIUDAD", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_ciudad", codigoCiudad);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarCiudadesPorNombre(string nombreCiudad)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CIUDAD_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_ciudad", nombreCiudad);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public string ActualizarCiudad(int idCiudad, string nombreCiudad, string nombreEstado, string codigoEstado)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_CIUDAD", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre_ciudad", nombreCiudad);
                        cmd.Parameters.AddWithValue("@p_id_ciudad", idCiudad);
                        cmd.Parameters.AddWithValue("@p_nombre_estado", nombreEstado);
                        cmd.Parameters.AddWithValue("@p_codigo_estado", codigoEstado);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string EliminarCiudad(int idCiudad)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_CIUDAD", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_ciudad", idCiudad);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string InsertarCiudad(string nombreCiudad, string codigoCiudad, string nombreEstado, string codigoEstado, string nombrePais, string CodigoPais)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_CIUDAD", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_ciudad", nombreCiudad);
                        cmd.Parameters.AddWithValue("@p_codigo_ciudad", codigoCiudad);
                        cmd.Parameters.AddWithValue("@p_nombre_estado", nombreEstado);
                        cmd.Parameters.AddWithValue("@p_codigo_estado", codigoEstado);
                        cmd.Parameters.AddWithValue("@p_nombre_pais", nombrePais);
                        cmd.Parameters.AddWithValue("@p_codigo_pais", CodigoPais);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable consultarDepartamentos()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_DEPARTAMENTOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;                     
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable CargarPlanesAfiliado(string idAfiliado, string estado)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CARGAR_PLANES_AFILIADO", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_afiliado", idAfiliado);
                        cmd.Parameters.AddWithValue("@p_estado", estado);
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable validarPermisos(string strPagina, string idPerfil, string idUsuario)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                    using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                    {
                        using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_PERMISOS", mysqlConexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@p_pagina", strPagina);
                            cmd.Parameters.AddWithValue("@p_id_perfil", Convert.ToInt32(idPerfil));
                            cmd.Parameters.AddWithValue("@p_id_usuario", Convert.ToInt32(idUsuario));

                            using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                            {
                                mysqlConexion.Open();
                                dataAdapter.Fill(dt);
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable validarCiudadTablas(string idCiudad)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_CIUDAD_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_ciudad", Convert.ToInt32(idCiudad));

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable TraerDatos(string strQuery)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, mysqlConexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public void InsertarLog(string idUsuario, string tabla, string accion, string descripcion, string datosAnteriores, string datosNuevos)
        {
            // Tarea de Javier
            OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
            try
            {
                string strQuery = "INSERT INTO logs " +
                    "(idUsuario, FechaHora, Tabla, Accion, DatosAnteriores, DatosNuevos, DescripcionLog) " +
                    "VALUES (" + idUsuario + ", now(), '" + tabla + "', '" + accion + "', '" + datosAnteriores + "', '" + datosNuevos + "', '" + descripcion + "') ";
                //"VALUES (" + idUsuario + ", DATE_SUB(NOW(), INTERVAL 5 HOUR), '" + tabla + "', '" + accion + "', '" + datosAnteriores + "', '" + datosNuevos + "', '" + descripcion + "') ";
                OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                myConnection.Open();
                command.ExecuteNonQuery();
                command.Dispose();
                myConnection.Close();
            }
            catch (OdbcException ex)
            {
                string mensaje = ex.Message;
                myConnection.Close();
            }
        }

        public string CreatePassword(int length)
        {
            const string valid = "abcdefghkmnpqrstuvwxyzABCDEFGHKMNPQRSTUVWXYZ123456789*%$_-@#&?";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                var unused = res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public void EnviarCorreo(string strRemitente, string strDestinatario, string strAsunto, string strMensaje)
        {
            MailMessage objeto_mail = new MailMessage();
            objeto_mail.From = new MailAddress(strRemitente);
            MailAddress maTo = new MailAddress(strDestinatario);
            objeto_mail.To.Add(maTo);
            objeto_mail.Subject = strAsunto;
            objeto_mail.Body = strMensaje;

            SmtpClient client = new SmtpClient();
            client.Host = "localhost";
            client.Port = 25;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("contabilidad@fitnesspeoplecolombia.com", "K)961558128719os");

            try
            {
                client.Send(objeto_mail);
                objeto_mail.Dispose();
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
            }

        }

        public string InsertarPagina(string Pagina, string Categoria)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PAGINA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_pagina", Pagina);
                        cmd.Parameters.AddWithValue("@p_categoria", Categoria);
                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public string InsertarPermisosPerfiles(int perfil, int pagina, int sinPermiso, int consulta, int Exportar, int CrearModificar, int Borrar)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PERMISOS_PERFILES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_idPerfil", perfil);
                        cmd.Parameters.AddWithValue("@p_idPagina", pagina);
                        cmd.Parameters.AddWithValue("@p_SinPermiso", sinPermiso);
                        cmd.Parameters.AddWithValue("@p_Consulta", consulta);
                        cmd.Parameters.AddWithValue("@p_Exportar", Exportar);
                        cmd.Parameters.AddWithValue("@p_CrearModificar", CrearModificar);
                        cmd.Parameters.AddWithValue("@p_Borrar", Borrar);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable consultarPerfiles()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PERFILES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }


        public DataTable ConsultarPermisosPerfiles(string nombre_pagina, int id_perfil, int id_usuario)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PERMISOS_PERFILES", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_pagina", nombre_pagina);
                        cmd.Parameters.AddWithValue("@p_id_perfil", id_perfil);
                        cmd.Parameters.AddWithValue("@p_id_usuario", id_usuario);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

        public DataTable ConsultarUltimaPagina()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ULTIMA_PAGINA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;                      

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public string ActualizarPagina(string nomnbrePagina, string categoria, int idPagina)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_PAGINA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre_pagina", nomnbrePagina);
                        cmd.Parameters.AddWithValue("@p_categoria", categoria);
                        cmd.Parameters.AddWithValue("@p_id_pagina", idPagina);

                        cmd.ExecuteNonQuery();
                        respuesta = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = "ERROR: " + ex.Message;
            }

            return respuesta;
        }

        public DataTable ConsultarPaginaPorNombre(string nombre_pagina)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PAGINA_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_pagina", nombre_pagina);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable consultarPaginas()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PAGINAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;                        
                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }
            return dt;
        }

        public DataTable ConsultarPaginaPorId(int idPagina)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PAGINA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_pagina", idPagina);

                        using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                        {
                            mysqlConexion.Open();
                            dataAdapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = new DataTable();
                dt.Columns.Add("Error", typeof(string));
                dt.Rows.Add(ex.Message);
            }

            return dt;
        }

    }
}