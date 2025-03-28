using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace fpWebApp
{
    public partial class editarproductotienda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"].ToString() != "")
                {
                    if (Session["idUsuario"] != null)
                    {
                        ValidarPermisos("Productos tienda");
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
                            CargarProductos();
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
                else
                {
                    Response.Redirect("productostienda");
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
            string strQuery = "SELECT idCategoria, NombreCat FROM Categorias ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ddlCategorias.DataSource = dt;
            ddlCategorias.DataBind();

            dt.Dispose();
        }

        private void CargarProductos()
        {
            string strQuery = "SELECT * FROM Productos p, Categorias c " +
                "WHERE p.idCategoria = c.idCategoria " +
                "AND p.idProducto = " + Request.QueryString["id"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            txbNombre.Text = dt.Rows[0]["NombreProd"].ToString();
            ltNombre.Text = dt.Rows[0]["NombreProd"].ToString();

            txbCodigo.Text = dt.Rows[0]["CodigoProd"].ToString();

            txbPrecio.Text = dt.Rows[0]["PrecioPublicoProd"].ToString();
            ltPrecio.Text = string.Format("{0:C0}", dt.Rows[0]["PrecioPublicoProd"]);

            txbDetalle.Text = dt.Rows[0]["DetalleProd"].ToString();
            ltDetalle.Text = dt.Rows[0]["DetalleProd"].ToString();

            txbDescripcion.Text = dt.Rows[0]["DescripcionProd"].ToString();
            ltDescripcion.Text = dt.Rows[0]["DescripcionProd"].ToString();

            txbCaracteristicas.Text = dt.Rows[0]["CaracteristicasProd"].ToString();
            ltCaracteristicas.Text = dt.Rows[0]["CaracteristicasProd"].ToString();

            txbBeneficios.Text = dt.Rows[0]["BeneficiosProd"].ToString();
            ltBeneficios.Text = dt.Rows[0]["BeneficiosProd"].ToString();

            ddlCategorias.SelectedIndex = Convert.ToInt32(ddlCategorias.Items.IndexOf(ddlCategorias.Items.FindByValue(dt.Rows[0]["idCategoria"].ToString())));

            ltImagen1Prod.Text = "<div><div class=\"image-imitation\" style=\"padding: initial\"><img src=\"img/productos/" + dt.Rows[0]["Imagen1Prod"].ToString() + "\" width=\"100%\" /></div></div>";
            ViewState.Add("Imagen1Prod", dt.Rows[0]["Imagen1Prod"].ToString());
            ViewState["Imagen2Prod"] = "";
            ViewState["Imagen3Prod"] = "";
            ViewState["Imagen4Prod"] = "";

            if (dt.Rows[0]["Imagen2Prod"].ToString() != "")
            {
                ltImagen2Prod.Text = "<div><div class=\"image-imitation\" style=\"padding: initial\"><img src=\"img/productos/" + dt.Rows[0]["Imagen2Prod"].ToString() + "\" width=\"100%\" /></div></div>";
                ViewState["Imagen2Prod"] = dt.Rows[0]["Imagen2Prod"].ToString();
            }
            if (dt.Rows[0]["Imagen3Prod"].ToString() != "")
            {
                ltImagen3Prod.Text = "<div><div class=\"image-imitation\" style=\"padding: initial\"><img src=\"img/productos/" + dt.Rows[0]["Imagen3Prod"].ToString() + "\" width=\"100%\" /></div></div>";
                ViewState["Imagen3Prod"] = dt.Rows[0]["Imagen3Prod"].ToString();
            }
            if (dt.Rows[0]["Imagen4Prod"].ToString() != "")
            {
                ltImagen4Prod.Text = "<div><div class=\"image-imitation\" style=\"padding: initial\"><img src=\"img/productos/" + dt.Rows[0]["Imagen4Prod"].ToString() + "\" width=\"100%\" /></div></div>";
                ViewState["Imagen4Prod"] = dt.Rows[0]["Imagen4Prod"].ToString();
            }

            dt.Dispose();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //Accede al archivo usando el nombre del archivo HTML INPUT.
            string strFilename1 = ViewState["Imagen1Prod"].ToString();
            string strFilename2 = ViewState["Imagen2Prod"].ToString();
            string strFilename3 = ViewState["Imagen3Prod"].ToString();
            string strFilename4 = ViewState["Imagen4Prod"].ToString();

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
                string strInitData = TraerData();
                try
                {
                    string strQuery = "UPDATE productos SET " +
                       "idCategoria = " + ddlCategorias.SelectedItem.Value.ToString() + ", " +
                       "CodigoProd = '" + txbCodigo.Text.ToString() + "', " +
                       "NombreProd = '" + txbNombre.Text.ToString() + "', " +
                       "PrecioPublicoProd = " + txbPrecio.Text.ToString() + ", " +
                       "DetalleProd = '" + txbDetalle.Text.ToString() + "', " +
                       "DescripcionProd = '" + txbDescripcion.Text.ToString() + "', " +
                       "CaracteristicasProd = '" + txbCaracteristicas.Text.ToString() + "', " +
                       "BeneficiosProd = '" + txbBeneficios.Text.ToString() + "', " +
                       "Imagen1Prod = '" + strFilename1 + "', " +
                       "Imagen2Prod = '" + strFilename2 + "', " +
                       "Imagen3Prod = '" + strFilename3 + "', " +
                       "Imagen4Prod = '" + strFilename4 + "'" +
                       "WHERE idProducto = " + Request.QueryString["id"].ToString();

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

                string strNewData = TraerData();
                clasesglobales cg = new clasesglobales();
                cg.InsertarLog(Session["idusuario"].ToString(), "productos", "Modifica", "El usuario modificó datos al producto con código: " + txbCodigo.Text.ToString() + ".", strInitData, strNewData);

            }
            Response.Redirect("editarproductotienda?id=" + Request.QueryString["id"].ToString());
        }

        private string TraerData()
        {
            string strQuery = "SELECT * FROM productos WHERE idProducto = " + Request.QueryString["id"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            string strData = "";
            foreach (DataColumn column in dt.Columns)
            {
                strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
            }
            dt.Dispose();

            return strData;
        }
    }
}