using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class pruebapagowompi : System.Web.UI.Page
    {
        OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form.Count != 0)
            {
                string strDocumento = Request.Form["txbDocumento"].ToString();
                //Buscamos el documento en la tabla afiliados. Si no existe, creamos el afiliado. Si existe, actualizamos Correo, Celular, Ciudad, Sede y Plan
                if (ExisteAfiliado(strDocumento))
                {
                    string strQuery = "UPDATE Afiliados SET " +
                        "CelularAfiliado = '" + Request.Form["txbCelular"].ToString() + "', " +
                        "EmailAfiliado = '" + Request.Form["txbCorreo"].ToString() + "', DireccionAfiliado = '" + Request.Form["txbDireccion"].ToString() + "', " +
                        "CiudadAfiliado = '" + Request.Form["txbCiudad"].ToString() + "' " +
                        "WHERE DocumentoAfiliado = '" + strDocumento + "' ";
                    OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                    myConnection.Open();
                    command.ExecuteNonQuery();
                    command.Dispose();
                    myConnection.Close();
                }
                else
                {
                    //Si no existe el documento del afiliado, lo creamos como nuevo.
                    string strQuery = "INSERT INTO Afiliados " +
                        "(DocumentoAfiliado, idTipoDocumento, NombreAfiliado, ApellidoAfiliado, CelularAfiliado, EmailAfiliado, " +
                        "DireccionAfiliado, CiudadAfiliado, EstadoAfiliado) " +
                        "VALUES ('" + strDocumento + "', " + Request.Form["ddlTipoDoc"].ToString() + ", " +
                        "'" + Request.Form["txbNombre"].ToString() + "', '" + Request.Form["txbApellido"].ToString() + "', " +
                        "'" + Request.Form["txbCelular"].ToString() + "', '" + Request.Form["txbCorreo"].ToString() + "', " +
                        "'" + Request.Form["txbDireccion"].ToString() + "', '" + Request.Form["txbCiudad"].ToString() + "', 'Pendiente') ";
                    OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                    myConnection.Open();
                    command.ExecuteNonQuery();
                    command.Dispose();
                    myConnection.Close();

                    //EnviarCorreoBienvenida();
                }

                //Insertamos el Plan elegido


                //Mostramos el formulario de Wompi con los datos.


                //Referencia unica para el pago.
                string strReferencia = strDocumento + "-" + DateTime.Now.ToString("yyyyMMddHHmmss");

                //Hash Sha256 para Wompi
                string monto = Request.Form["txbPrecio"].ToString() + "00";
                string moneda = "COP";
                string integrity_secret = "test_integrity_ECI40KcjCePVzQFu1rlkqQDWxwnQ6lAD";

                string concatenado = strReferencia + monto + moneda + integrity_secret;
                string strHash = ComputeSha256Hash(concatenado);

                //form1.Visible = true;
                //divIbox.Visible = false;

                //ltScript.Text = "<script src=\"https://checkout.wompi.co/widget.js\"" + "\n\r";
                //ltScript.Text += "data-render=\"button\"" + "\n\r";
                //ltScript.Text += "data-public-key=\"pub_test_Mp5JzDLXitLu7W0I3Gea5OXotOExpFjv\"" + "\n\r";
                //ltScript.Text += "data-currency=\"COP\"" + "\n\r";
                //ltScript.Text += "data-amount-in-cents=\"" + monto + "\"" + "\n\r";
                //ltScript.Text += "data-reference=\"" + strReferencia + "\"" + "\n\r";
                //ltScript.Text += "data-signature:integrity=\"" + strHash + "\"" + "\n\r";
                //ltScript.Text += "data-redirect-url=\"https://fitnesspeoplecolombia.com/planes\"" + "\n\r";
                //ltScript.Text += "data-customer-data:email=\"" + Request.Form["txbCorreo"].ToString() + "\"" + "\n\r";
                //ltScript.Text += "data-customer-data:full-name=\"" + Request.Form["txbNombre"].ToString() + " " + Request.Form["txbApellido"].ToString() + "\"" + "\n\r";
                //ltScript.Text += "data-customer-data:phone-number=\"" + Request.Form["txbCelular"].ToString() + "\"" + "\n\r";
                //ltScript.Text += "data-customer-data:phone-number-prefix=\"+57\"" + "\n\r";
                //ltScript.Text += "data-customer-data:legal-id=\"" + strDocumento + "\"" + "\n\r";
                //ltScript.Text += "data-customer-data:legal-id-type=\"CC\">" + "\n\r";

                //ltScript.Text += "</script>";

                //amount_in_cents.Value = monto;
                //reference.Value = strReferencia;
                //signature_integrity.Value = strHash;
                //correopagador.Value = Request.Form["txbCorreo"].ToString();
                //nombrecompletopagador.Value = Request.Form["txbNombre"].ToString() + " " + Request.Form["txbApellido"].ToString();
                //celularpagador.Value = Request.Form["txbCelular"].ToString();
                //documentopagador.Value = strDocumento;
            }
            //txbBuscar.Attributes.Add("type", "number");
            //txbCelular.Attributes.Add("type", "number");
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Crea un SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - devuelve una matriz de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convierte una matriz de bytes en una cadena
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool ExisteAfiliado(string strDocumento)
        {
            string strQuery = "SELECT * FROM Afiliados WHERE DocumentoAfiliado = " + strDocumento;
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //protected void txbBuscar_TextChanged(object sender, EventArgs e)
        //{
        //    limpiarTxb();
        //    string strDocumento = "";//txbBuscar.Text.ToString();
        //    string strQuery = "SELECT * FROM Afiliados a " +
        //        "WHERE DocumentoAfiliado = '" + strDocumento + "' ";
        //    DataTable dt = TraerDatos(strQuery);

        //    if (dt.Rows.Count > 0)
        //    {
        //        txbNombreAfiliado.Text = dt.Rows[0]["NombreAfiliado"].ToString() + " " + dt.Rows[0]["ApellidoAfiliado"].ToString();
        //        txbDireccion.Text = dt.Rows[0]["DireccionAfiliado"].ToString();
        //        txbCorreo.Text = dt.Rows[0]["EmailAfiliado"].ToString();
        //        correopagador.Value = dt.Rows[0]["EmailAfiliado"].ToString();
        //        txbCelular.Text = dt.Rows[0]["CelularAfiliado"].ToString();
        //        hfIdAfiliado.Value = dt.Rows[0]["idAfiliado"].ToString();

        //        txbNombreAfiliado.Enabled = false;
        //        txbDireccion.Enabled = false;
        //        txbCorreo.Enabled = false;
        //        txbCelular.Enabled = false;

        //        ltPublicKey.Text = "pub_test_Mp5JzDLXitLu7W0I3Gea5OXotOExpFjv";
        //        hfPublicKey.Value = "pub_test_Mp5JzDLXitLu7W0I3Gea5OXotOExpFjv";

        //        //ltBotonWompi.Text = "<script src=\"https://checkout.wompi.co/widget.js\"" + "\n\r";
        //        //ltBotonWompi.Text += "data-render=\"button\"" + "\n\r";
        //        //ltBotonWompi.Text += "data-public-key=\"pub_test_Mp5JzDLXitLu7W0I3Gea5OXotOExpFjv\"" + "\n\r";
        //        //ltBotonWompi.Text += "data-currency=\"COP\"" + "\n\r";
        //        //ltBotonWompi.Text += "data-amount-in-cents=\"2490000\"" + "\n\r";
        //        //ltBotonWompi.Text += "data-reference=\"sk8-438k4-xmxm392-6655\"" + "\n\r";
        //        //ltBotonWompi.Text += "data-signature:integrity=\"59cdd8ebbaaef3ff6859cf4826e6a6b576147205424e56af7e0a08efd151a953\"" + "\n\r";
        //        //ltBotonWompi.Text += "data-redirect-url=\"https://fitnesspeoplecolombia.com/planes\"></script>";
        //    }
        //    else
        //    {
        //        txbNombreAfiliado.Enabled = true;
        //        txbDireccion.Enabled = true;
        //        txbCorreo.Enabled = true;
        //        txbCelular.Enabled = true;
        //    }
            
        //    dt.Dispose();
        //}

        //protected void limpiarTxb()
        //{
        //    txbNombreAfiliado.Text = "";
        //    txbDireccion.Text = "";
        //    txbCorreo.Text = "";
        //}
    }
}