using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace fpWebApp.controles
{
    /// <summary>
    /// Descripción breve de HandlerDocumentos
    /// </summary>
    public class HandlerDocumentos : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (context.Session["idUsuario"] == null)
                    throw new Exception("Sesión expirada");

                HttpPostedFile file = context.Request.Files["file"];

                if (file.ContentLength > (2 * 1024 * 1024))
                    throw new Exception("El archivo no debe superar los 2MB");

                string[] permitidos = { ".pdf", ".jpeg", ".jpg", ".png" };
                string extension = Path.GetExtension(file.FileName).ToLower();

                if (!permitidos.Contains(extension))
                    throw new Exception("Tipo de archivo no permitido");

                int idUsuario = Convert.ToInt32(context.Session["idUsuario"]);
                int idConvenio = Convert.ToInt32(context.Request["idConvenio"]);
                string tipoDocumento = context.Request["tipoDocumento"];

                

                if (file == null || file.ContentLength == 0)
                    throw new Exception("Archivo no válido");

                string carpeta = context.Server.MapPath("~/docs/contratos/");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                extension = Path.GetExtension(file.FileName);
                string nombreArchivo = idConvenio + "_" + tipoDocumento + extension;

                string rutaCompleta = Path.Combine(carpeta, nombreArchivo);

                if (File.Exists(rutaCompleta))
                    File.Delete(rutaCompleta);

                file.SaveAs(rutaCompleta);

                clasesglobales cg = new clasesglobales();

                cg.InsertarDocumentoConvenio(
                    idConvenio,
                    tipoDocumento,
                    "/docs/contratos/" + nombreArchivo,
                    nombreArchivo,
                    idUsuario
                );

                context.Response.ContentType = "application/json";
                context.Response.Write("{\"success\":true}");
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.Write("{\"success\":false,\"mensaje\":\"" + ex.Message + "\"}");
            }

        }


        public bool IsReusable
        {
            get { return false; }
        }
    }

}




