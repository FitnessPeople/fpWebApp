using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class comprobantetransferencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Afiliados");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {

                    }
                    else
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileFoto.PostedFile != null && fileFoto.PostedFile.ContentLength > 0)
            {
                string extension = Path.GetExtension(fileFoto.PostedFile.FileName).ToLower();
                if (extension != ".jpg" && extension != ".jpeg" && extension != ".png")
                {
                    //lblMensaje.Text = "Formato no permitido";
                    return;
                }

                // Nombre único
                string nuevoNombre = DateTime.Now.Ticks + ".jpg";
                string rutaServidor = Server.MapPath("~/img/comprobantes/" + nuevoNombre);

                // Procesar imagen
                using (System.Drawing.Image imagenOriginal = System.Drawing.Image.FromStream(fileFoto.PostedFile.InputStream))
                {
                    // Redimensionar (manteniendo proporción)
                    int maxWidth = 1024;
                    int maxHeight = 1024;

                    int newWidth = imagenOriginal.Width;
                    int newHeight = imagenOriginal.Height;

                    if (newWidth > maxWidth || newHeight > maxHeight)
                    {
                        double ratioX = (double)maxWidth / newWidth;
                        double ratioY = (double)maxHeight / newHeight;
                        double ratio = Math.Min(ratioX, ratioY);

                        newWidth = (int)(newWidth * ratio);
                        newHeight = (int)(newHeight * ratio);
                    }

                    using (Bitmap imagenRedimensionada = new Bitmap(imagenOriginal, newWidth, newHeight))
                    {
                        // Configurar compresión JPG al 70%
                        var codec = GetEncoder(ImageFormat.Jpeg);
                        var encoder = System.Drawing.Imaging.Encoder.Quality;
                        var encoderParams = new EncoderParameters(1);
                        encoderParams.Param[0] = new EncoderParameter(encoder, 70L);

                        // Guardar comprimida
                        imagenRedimensionada.Save(rutaServidor, codec, encoderParams);
                    }
                }

                // Guardar URL en BD
                string urlFoto = "img/comprobantes/" + nuevoNombre;
                GuardarFotoEnBD(urlFoto);

                //lblMensaje.Text = "Imagen optimizada y guardada.";
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }


        public void GuardarFotoEnBD(string url)
        {
            clasesglobales cg = new clasesglobales();
            int idComprobante = cg.InsertarComprobanteTransferencia(url);

            imgFoto.ImageUrl = url;
            ltReferencia.Text = "Código de Referencia: <b>" + idComprobante.ToString() + "</b>";
        }
    }
}