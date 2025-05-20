using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace fpWebApp
{
    public partial class nuevoproductotienda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Nuevo producto");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        txbPrecio.Attributes.Add("type", "number");
                        txbPrecio.Attributes.Add("step", "100");
                        txbPrecio.Attributes.Add("min", "1000");
                        CargarCategorias();
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

        private void CargarCategorias()
        {
            string strQuery = "SELECT idCategoria, NombreCat FROM CategoriasTienda ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlCategorias.DataSource = dt;
            ddlCategorias.DataBind();

            dt.Dispose();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            //Accede al archivo usando el nombre del archivo HTML INPUT.
            string strFilename1 = "";
            string strFilename2 = "";
            string strFilename3 = "";
            string strFilename4 = "";

            if (imgInp1.PostedFile.FileName != "")
            {
                string csvPath = Server.MapPath("img/productos/") + txbCodigo.Text.ToString().Trim() + "_" + Path.GetFileName(imgInp1.PostedFile.FileName);
                imgInp1.SaveAs(csvPath);
                strFilename1 = imgInp1.PostedFile.FileName;
            }

            if (imgInp2.PostedFile.FileName != "")
            {
                string csvPath = Server.MapPath("img/productos/") + txbCodigo.Text.ToString().Trim() + "_" + Path.GetFileName(imgInp2.PostedFile.FileName);
                imgInp2.SaveAs(csvPath);
                strFilename2 = imgInp2.PostedFile.FileName;
            }

            if (imgInp3.PostedFile.FileName != "")
            {
                string csvPath = Server.MapPath("img/productos/") + txbCodigo.Text.ToString().Trim() + "_" + Path.GetFileName(imgInp3.PostedFile.FileName);
                imgInp3.SaveAs(csvPath);
                strFilename3 = imgInp3.PostedFile.FileName;
            }

            if (imgInp4.PostedFile.FileName != "")
            {
                string csvPath = Server.MapPath("img/productos/") + txbCodigo.Text.ToString().Trim() + "_" + Path.GetFileName(imgInp4.PostedFile.FileName);
                imgInp4.SaveAs(csvPath);
                strFilename4 = imgInp4.PostedFile.FileName;
            }

            if (strFilename1 == "")
            {
                ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Debe subir al menos una imagen (Imagen 1)" +
                    "</div>";
            }
            else
            {
                string strQuery = "INSERT INTO productos " +
                    "(idCategoria, CodigoProd, NombreProd, PrecioPublicoProd, DetalleProd, DescripcionProd, " +
                    "CaracteristicasProd, BeneficiosProd, Imagen1Prod, Imagen2Prod, Imagen3Prod, Imagen4Prod, VideoProd, " +
                    "FavoritoProd, NuevoProd, MostrarProd) " +
                    "VALUES (" + ddlCategorias.SelectedItem.Value.ToString() + ", " +
                    "'" + txbCodigo.Text.ToString() + "', '" + txbNombre.Text.ToString() + "', " +
                    "" + txbPrecio.Text.ToString() + ", '" + txbDetalle.Text.ToString() + "', " +
                    "'" + txbDescripcion.Text.ToString() + "', '" + txbCaracteristicas.Text.ToString() + "', " +
                    "'" + txbBeneficios.Text.ToString() + "', '" + strFilename1 + "', '" + strFilename2 + "', " +
                    "'" + strFilename3 + "', '" + strFilename4 + "', '', 1, 1, 1) ";

                try
                {
                    string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                    using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                    {
                        mysqlConexion.Open();
                        using (MySqlCommand cmd = new MySqlCommand(strQuery, mysqlConexion))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                        }
                        mysqlConexion.Close();
                    }
                }
                catch (Exception ex)
                {
                    string respuesta = "ERROR: " + ex.Message;
                }

                clasesglobales cg = new clasesglobales();
                cg.InsertarLog(Session["idusuario"].ToString(), "productos", "Nuevo", "El usuario creó un nuevo producto con código: " + txbCodigo.Text.ToString() + ".", "", "");

            }

            

        }
    }
}