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

        #region Ciudades
        public DataTable ConsultarCiudades()
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

        public DataTable ValidarCiudadTablas(string idCiudad)
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

        public DataTable ConsultarDepartamentos()
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

        #endregion

        #region Arls
        public DataTable ConsultarArls()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ARLS", mysqlConexion))
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

        public DataTable ConsultarArlPorNombre(string nombreArl)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ARL_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_arl", nombreArl);

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

        public DataTable ConsultarArlPorId(int idArl)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_ARL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_arl", idArl);

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

        public string ActualizarArl(int idArl, string nombreArl)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_ARL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre_arl", nombreArl);
                        cmd.Parameters.AddWithValue("@p_id_arl", idArl);

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

        public string EliminarArl(int idArl)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_ARL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_arl", idArl);

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

        public string InsertarArl(string nombreArl)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_ARL", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_arl", nombreArl);

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

        public DataTable ValidarArlEmpleados(int idArl)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_ARL_EMPLEADOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_arl", Convert.ToInt32(idArl));

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

        #endregion

        #region Profesiones
        public DataTable ConsultarProfesiones()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PROFESIONES", mysqlConexion))
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

        public DataTable ConsultarProfesionPorNombre(string Profesion)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PROFESION_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_profesion", Profesion);

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

        public DataTable ConsultarProfesionPorId(int idProfesion)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PROFESION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_profesion", idProfesion);

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

        public string ActualizarProfesion(int idProfesion, string Profesion, string Area)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_PROFESION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_id_profesion", idProfesion);
                        cmd.Parameters.AddWithValue("@p_profesion", Profesion);
                        cmd.Parameters.AddWithValue("@p_area", Area);

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

        public string EliminarProfesion(int idProfesion)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_PROFESION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_profesion", idProfesion);

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

        public string InsertarProfesion(string Profesion, string Area)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PROFESION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_profesion", Profesion);
                        cmd.Parameters.AddWithValue("@p_area", Area);

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

        public DataTable ValidarProfesionTablas(int idProfesion)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_PROFESION_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_profesion", idProfesion);

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

        #endregion

        #region Eps
        public DataTable ConsultarEpss()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EPSS", mysqlConexion))
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

        public DataTable ConsultarEpsPorNombre(string nombreEps)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EPS_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_eps", nombreEps);

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

        public DataTable ConsultarEpsPorId(int idEps)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_EPS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_eps", idEps);

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

        public string ActualizarEps(int idEps, string nombreEps)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_EPS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre_eps", nombreEps);
                        cmd.Parameters.AddWithValue("@p_id_eps", idEps);

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

        public string EliminarEps(int idEps)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_EPS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_eps", idEps);

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

        public string InsertarEps(string nombreEps)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_EPS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_eps", nombreEps);

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

        public DataTable ValidarEpsTablas(int idEps)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_EPS_TABLAS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_eps", Convert.ToInt32(idEps));

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

        #endregion

        #region Fondos de Pension
        public DataTable ConsultarPensiones()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PENSIONES", mysqlConexion))
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

        public DataTable ConsultarPensionPorNombre(string nombrePension)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PENSION_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_pension", nombrePension);

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

        public DataTable ConsultarPensionPorId(int idPension)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_PENSION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_pension", idPension);

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

        public string ActualizarPension(int idPension, string nombrePension)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_PENSION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre_pension", nombrePension);
                        cmd.Parameters.AddWithValue("@p_id_pension", idPension);

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

        public string EliminarPension(int idPension)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_PENSION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_pension", idPension);

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

        public string InsertarPension(string nombrePension)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_PENSION", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_pension", nombrePension);

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

        public DataTable ValidarPensionEmpleados(int idPension)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_PENSION_EMPLEADOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_pension", Convert.ToInt32(idPension));

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

        #endregion

        #region Fondos de Cesantias
        public DataTable ConsultarCesantias()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CESANTIAS", mysqlConexion))
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

        public DataTable ConsultarCesantiaPorNombre(string nombreCesantia)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CESANTIA_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_cesantia", nombreCesantia);

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

        public DataTable ConsultarCesantiaPorId(int idCesantia)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CESANTIA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_cesantia", idCesantia);

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

        public string ActualizarCesantia(int idCesantia, string nombreCesantia)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open(); // Abrir conexión antes de usarla

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_CESANTIA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        cmd.Parameters.AddWithValue("@p_nombre_cesantia", nombreCesantia);
                        cmd.Parameters.AddWithValue("@p_id_cesantia", idCesantia);

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

        public string EliminarCesantia(int idCesantia)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_CESANTIA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_cesantia", idCesantia);

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

        public string InsertarCesantia(string nombreCesantia)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_CESANTIA", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_cesantia", nombreCesantia);

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

        public DataTable ValidarCesantiaEmpleados(int idPension)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_VALIDAR_CESANTIA_EMPLEADOS", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_cesantia", Convert.ToInt32(idPension));

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

        #endregion

        #region Paginas

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

        public string ActualizarPagina(int idPagina, string nombrePagina, string categoria)
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
                        cmd.Parameters.AddWithValue("@p_nombre_pagina", nombrePagina);
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

        public DataTable ConsultarPaginas()
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

        #endregion

        #region Ciudades Sedes

        public DataTable ConsultarCiudadesSedes()
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CIUDADES_SEDES", mysqlConexion))
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

        public DataTable ConsultarCiudadSedePorId(int idCiudadSede)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CIUD_SEDE_POR_ID", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_ciudad_sede", idCiudadSede);

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

        public string InsertarCiudadSede(string nombreCiudadSede)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_INSERTAR_CIUDAD_SEDE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_ciu_sede", nombreCiudadSede);
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

        public string EliminarCiudadSede(int idCiudadSede)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();

                    using (MySqlCommand cmd = new MySqlCommand("Pa_ELIMINAR_CIUDAD_SEDE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_ciudad_sede", idCiudadSede);

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

        public string ActualizarCiudadSede(int idCiudadSede, string nombreCiudadSede)
        {
            string respuesta = string.Empty;
            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    mysqlConexion.Open();
                    using (MySqlCommand cmd = new MySqlCommand("Pa_ACTUALIZAR_CIUDAD_SEDE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_id_ciudad_sede", idCiudadSede);
                        cmd.Parameters.AddWithValue("@p_nombre_ciudad_sede", nombreCiudadSede);

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

        public DataTable ConsultarCiudadSedePorNombre(string nombreCiudadSede)
        {
            DataTable dt = new DataTable();

            try
            {
                string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;
                using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Pa_CONSULTAR_CIUD_SEDE_POR_NOMBRE", mysqlConexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_nombre_ciudad_sede", nombreCiudadSede);

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


        #endregion

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

        public DataTable ValidarPermisos(string strPagina, string idPerfil, string idUsuario)
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

        public DataTable ConsultarPerfiles()
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

    }
}