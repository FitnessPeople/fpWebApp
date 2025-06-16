using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class estadoscrm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Ciudades sedes");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        //No tiene acceso a esta página
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    else
                    {
                        //Si tiene acceso a esta página
                        divBotonesLista.Visible = false;
                        btnAgregar.Visible = false;
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }

                    ListaEstadosCRM();
                    ListaColoresCRM();
                    ListaIconosCRM();

                    ltTitulo.Text = "Agregar Estado CRM";
                    if (Request.QueryString.Count > 0)
                    {
                        rpEstadosCRM.Visible = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = new DataTable();
                            dt = cg.ConsultarCiudadSedePorId(int.Parse(Request.QueryString["editid"].ToString()));
                            if (dt.Rows.Count > 0)
                            {
                                txbCiudadSede.Text = dt.Rows[0]["NombreCiudadSede"].ToString();
                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar Estado CRM";
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = cg.ValidarCiudadSedesTablas(Request.QueryString["deleteid"].ToString());
                            if (dt.Rows.Count > 0)
                            {
                                ltMensaje.Text = "<div class=\"ibox-content\">" +
                                    "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                    "Esta Ciudad Sede no se puede borrar, hay empleados asociados a ella." +
                                    "</div></div>";

                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarCiudadSedePorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt.Rows.Count > 0)
                                {
                                    txbCiudadSede.Text = dt1.Rows[0]["NombreCiudadSede"].ToString();
                                    txbCiudadSede.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    btnAgregar.Enabled = false;
                                    ltTitulo.Text = "Borrar Estado CRM";
                                }
                                dt1.Dispose();
                            }
                            else
                            {
                                //Borrar
                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarCiudadSedePorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt1.Rows.Count > 0)
                                {
                                    txbCiudadSede.Text = dt1.Rows[0]["NombreCiudadSede"].ToString();
                                    txbCiudadSede.Enabled = false;
                                    btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                    ltTitulo.Text = "Borrar Estado CRM";
                                }
                                dt1.Dispose();
                            }
                            dt.Dispose();
                        }
                    }
                }
                else
                {
                    Response.Redirect("logout");
                }
            }
        }

        private bool ValidarCiudad(string strNombre)
        {
            bool bExiste = false;
            DataTable dt = new DataTable();
            clasesglobales cg = new clasesglobales();
            dt = cg.ConsultarCiudadSedePorNombre(strNombre);
            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }
            return bExiste;
        }

        private void ValidarPermisos(string strPagina)
        {
            ViewState["SinPermiso"] = "1";
            ViewState["Consulta"] = "0";
            ViewState["Exportar"] = "0";
            ViewState["CrearModificar"] = "0";
            ViewState["Borrar"] = "0";

            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.ValidarPermisos(strPagina, Session["idPerfil"].ToString(), Session["idusuario"].ToString());

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

        private void ListaColoresCRM()
        {            
            DataTable dt = new DataTable();
            dt.Columns.Add("NombreEstadoCRM", typeof(string));
            dt.Columns.Add("ColorHexaCRM", typeof(string));
            dt.Columns.Add("IconoMinEstadoCRM", typeof(string));
            dt.Columns.Add("idEstadoCRM", typeof(string));

            dt.Rows.Add("Primary", "#1c84c6", "fa fa-circle", "1");
            dt.Rows.Add("Info", "#23c6c8", "fa fa-circle", "2");
            dt.Rows.Add("Success", "#1ab394", "fa fa-circle", "3");
            dt.Rows.Add("Warning", "#f8ac59", "fa fa-circle", "4");
            dt.Rows.Add("Danger", "#ed5565", "fa fa-circle", "5");
            dt.Rows.Add("Pink", "#e91e63", "fa fa-circle", "6");
            dt.Rows.Add("Violet", "#8e44ad", "fa fa-circle", "7");
            dt.Rows.Add("Brown", "#795548", "fa fa-circle", "8");
            dt.Rows.Add("Silver", "#bdc3c7", "fa fa-circle", "9");

            // Cargar al DropDownList
            ddlColores.Items.Clear();
            ddlColores.Items.Add(new ListItem("Seleccione", ""));

            foreach (DataRow row in dt.Rows)
            {
                ListItem item = new ListItem
                {
                    Text = row["NombreEstadoCRM"].ToString(), // solo el texto
                    Value = row["idEstadoCRM"].ToString()
                };

                item.Attributes["style"] = $"color: {row["ColorHexaCRM"]};";
                item.Attributes["data-icon"] = row["IconoMinEstadoCRM"].ToString();
                item.Attributes["data-color"] = row["ColorHexaCRM"].ToString();

                ddlColores.Items.Add(item);
            }
        }

        private void ListaIconosCRM()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NombreEstadoCRM", typeof(string));
            dt.Columns.Add("ColorHexaCRM", typeof(string)); // Puedes usar un color genérico o fijo
            dt.Columns.Add("IconoMinEstadoCRM", typeof(string));
            dt.Columns.Add("idEstadoCRM", typeof(string));

            string colorNeutro = "#6c757d"; // Gris Bootstrap

            dt.Rows.Add("Apuntar", colorNeutro, "fa-solid fa-hand-point-up", "1");
            dt.Rows.Add("Enviar", colorNeutro, "fa-solid fa-paper-plane", "2");
            dt.Rows.Add("Acuerdo", colorNeutro, "fa-solid fa-handshake", "3");
            dt.Rows.Add("Sin acuerdo", colorNeutro, "fa-solid fa-handshake-slash", "4");

            ddlIconos.Items.Clear();
            ddlIconos.Items.Add(new ListItem("Seleccione", ""));

            foreach (DataRow row in dt.Rows)
            {
                ListItem item = new ListItem
                {
                    Text = row["NombreEstadoCRM"].ToString(), // solo el texto visible
                    Value = row["idEstadoCRM"].ToString()
                };

                item.Attributes["style"] = $"color: {row["ColorHexaCRM"]};";
                item.Attributes["data-icon"] = row["IconoMinEstadoCRM"].ToString();
                item.Attributes["data-color"] = row["ColorHexaCRM"].ToString();

                ddlIconos.Items.Add(item);
            }
        }

        private void ListaEstadosCRM()
        {
            DataTable dt = new DataTable();
            clasesglobales cg = new clasesglobales();
            dt = cg.ConsultarEstadossCRM();
            rpEstadosCRM.DataSource = dt;
            rpEstadosCRM.DataBind();
            dt.Dispose();
        }


        protected void rpEstadosCRM_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "estadoscrm?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "estadoscrm?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            if (Request.QueryString.Count > 0)
            {
                string strInitData = TraerData();

                if (Request.QueryString["editid"] != null)
                {
                    string respuesta = cg.ActualizarCiudadSede(int.Parse(Request.QueryString["editid"].ToString()), txbCiudadSede.Text.ToString().Trim());

                    string strNewData = TraerData();
                    cg.InsertarLog(Session["idusuario"].ToString(), "ciudades sedes", "Modifica", "El usuario modificó la ciudad sede: " + txbCiudadSede.Text.ToString() + ".", strInitData, strNewData);
                }

                if (Request.QueryString["deleteid"] != null)
                {
                    string respuesta = cg.EliminarCiudadSede(int.Parse(Request.QueryString["deleteid"].ToString()));
                }
                Response.Redirect("ciudadessedes");
            }
            else
            {
                if (!ValidarCiudad(txbCiudadSede.Text.ToString()))
                {
                    try
                    {
                        string respuesta = cg.InsertarCiudadSede(txbCiudadSede.Text.ToString().Trim());

                        cg.InsertarLog(Session["idusuario"].ToString(), "ciudades sedes", "Agrega", "El usuario agregó una nueva ciudad sede: " + txbCiudadSede.Text.ToString() + ".", "", "");
                    }
                    catch (Exception ex)
                    {
                        string mensajeExcepcionInterna = string.Empty;
                        Console.WriteLine(ex.Message);
                        if (ex.InnerException != null)
                        {
                            mensajeExcepcionInterna = ex.InnerException.Message;
                            Console.WriteLine("Mensaje de la excepción interna: " + mensajeExcepcionInterna);
                        }
                        ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Excepción interna." +
                        "</div>";
                    }
                    Response.Redirect("ciudadessedes");
                }
                else
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Ya existe una Ciudad Sede con ese nombre." +
                    "</div>";
                }
            }
        }
        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string consultaSQL = @"SELECT NombreCiudadSede AS 'Sedes en Ciudades'
		                               FROM ciudadessedes
		                               ORDER BY NombreCiudadSede;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(consultaSQL);
                string nombreArchivo = $"CiudadesSedes_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

                if (dt.Rows.Count > 0)
                {
                    cg.ExportarExcel(dt, nombreArchivo);
                }
                else
                {
                    Response.Write("<script>alert('No existen registros para esta consulta');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error al exportar: " + ex.Message + "');</script>");
            }
        }

        private string TraerData()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCiudadSedePorId(int.Parse(Request.QueryString["editid"].ToString()));

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