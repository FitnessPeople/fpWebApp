using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Configuration;
using System.Text;
using System.Net.Mail;

namespace fpWebApp
{
    public class clasesglobales
    {
        public DataTable TraerDatos(string strQuery)
        {
            OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
            myConnection.Open();
            DataTable dt = new DataTable();

            OdbcCommand sqlCmd = new OdbcCommand(strQuery, myConnection);
            OdbcDataAdapter sqlDA = new OdbcDataAdapter(sqlCmd);
            sqlDA.Fill(dt);
            myConnection.Close();

            return dt;
        }

        public void InsertarLog(string idUsuario, string tabla, string accion, string descripcion, string datosAnteriores, string datosNuevos)
        {
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
    }
}