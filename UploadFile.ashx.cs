using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace fpWebApp
{
    /// <summary>
    /// Descripción breve de UploadFile
    /// </summary>
    public class UploadFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpPostedFile file = context.Request.Files["file"];

            if (file == null)
            {
                context.Response.Write("{\"error\":\"No se recibió archivo\"}");
                return;
            }

            string folder = context.Server.MapPath("~/img/correo/docs/");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string filepath = Path.Combine(folder, filename);

            file.SaveAs(filepath);

            string url = "/img/correo/docs/" + filename;

            context.Response.ContentType = "application/json";
            context.Response.Write("{\"url\":\"" + url + "\"}");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}