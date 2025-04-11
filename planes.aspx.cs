using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace fpWebApp
{
    public partial class planes : System.Web.UI.Page
    {
        private string _strData;
        protected string strData { get { return this._strData; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                {
                    if (Session["idUsuario"] != null)
                    {
                        ValidarPermisos("Planes");
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
                        ListaPlanes();
                        ltTitulo.Text = "Agregar un plan";
                        txbPrecio.Attributes.Add("type", "number");
                        txbDiasCongelamiento.Attributes.Add("type", "number");
                        txbDiasCongelamiento.Attributes.Add("step", "0.1");
                        txbDiasCongelamiento.Attributes.Add("max", "10");
                        txbDescuentoMensual.Attributes.Add("type", "number");
                        txbDescuentoMensual.Attributes.Add("step", "0.1");
                        txbMesesMaximo.Attributes.Add("type", "number");
                        txbMesesMaximo.Attributes.Add("min", "1");
                        txbMesesMaximo.Attributes.Add("max", "12");
                        txbFechaInicio.Attributes.Add("type", "date");
                        txbFechaFinal.Attributes.Add("type", "date");

                        if (Request.QueryString.Count > 0)
                        {
                            rpPlanes.Visible = false;
                            if (Request.QueryString["editid"] != null)
                            {
                                //Editar
                                clasesglobales cg = new clasesglobales();
                                DataTable dt = cg.ConsultarPlanPorId(int.Parse(Request.QueryString["editid"].ToString()));
                                if (dt.Rows.Count > 0)
                                {
                                    txbPlan.Text = dt.Rows[0]["NombrePlan"].ToString();
                                    txbDescripcion.Text = dt.Rows[0]["DescripcionPlan"].ToString();
                                    txbPrecio.Text = dt.Rows[0]["PrecioBase"].ToString();
                                    txbDiasCongelamiento.Text = dt.Rows[0]["DiasCongelamientoMes"].ToString().Replace(',','.');
                                    txbDescuentoMensual.Text = dt.Rows[0]["DescuentoMensual"].ToString();
                                    txbMesesMaximo.Text = dt.Rows[0]["MesesMaximo"].ToString();
                                    txbFechaInicio.Text = dt.Rows[0]["FechaInicial"].ToString();
                                    txbFechaFinal.Text = dt.Rows[0]["FechaFinal"].ToString();
                                    btnAgregar.Text = "Actualizar";
                                    ltTitulo.Text = "Actualizar Plan";
                                }
                            }
                            if (Request.QueryString["deleteid"] != null)
                            {
                                clasesglobales cg = new clasesglobales();
                                DataTable dt = cg.ValidarPlanAfiliados(int.Parse(Request.QueryString["deleteid"].ToString()));
                                if (dt.Rows.Count > 0)
                                {
                                    ltMensaje.Text = "<div class=\"ibox-content\">" +
                                        "<div class=\"alert alert-danger alert-dismissable\">" +
                                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                        "Este Plan no se puede borrar, hay afiliados asociados a éste." +
                                        "</div></div>";

                                    DataTable dt1 = new DataTable();
                                    dt1 = cg.ConsultarPlanPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                    if (dt1.Rows.Count > 0)
                                    {
                                        txbPlan.Text = dt1.Rows[0]["NombrePlan"].ToString();
                                        txbDescripcion.Text = dt1.Rows[0]["DescripcionPlan"].ToString();
                                        txbPrecio.Text = dt1.Rows[0]["PrecioBase"].ToString();
                                        txbDescuentoMensual.Text = dt.Rows[0]["DescuentoMensual"].ToString();
                                        txbMesesMaximo.Text = dt.Rows[0]["MesesMaximo"].ToString();
                                        txbDiasCongelamiento.Text = dt1.Rows[0]["DiasCongelamientoMes"].ToString().Replace(',', '.');
                                        txbFechaInicio.Text = dt1.Rows[0]["FechaInicial"].ToString();
                                        txbFechaFinal.Text = dt1.Rows[0]["FechaFinal"].ToString();
                                        txbPlan.Enabled = false;
                                        txbDescripcion.Enabled = false;
                                        txbPrecio.Enabled = false;
                                        txbDescuentoMensual.Enabled = false;
                                        txbMesesMaximo.Enabled = false;
                                        txbDiasCongelamiento.Enabled = false;
                                        txbFechaInicio.Enabled = false;
                                        txbFechaFinal.Enabled = false;
                                        btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                        btnAgregar.Enabled = false;
                                        ltTitulo.Text = "Borrar Plan";
                                    }
                                    dt1.Dispose();
                                }
                                else
                                {
                                    //Borrar
                                    DataTable dt1 = new DataTable();
                                    dt1 = cg.ConsultarPlanPorId(int.Parse(Request.QueryString["deleteid"].ToString()));
                                    if (dt1.Rows.Count > 0)
                                    {
                                        txbPlan.Text = dt1.Rows[0]["NombrePlan"].ToString();
                                        txbDescripcion.Text = dt1.Rows[0]["DescripcionPlan"].ToString();
                                        txbPrecio.Text = dt1.Rows[0]["PrecioBase"].ToString();
                                        txbDescuentoMensual.Text = dt.Rows[0]["DescuentoMensual"].ToString();
                                        txbMesesMaximo.Text = dt.Rows[0]["MesesMaximo"].ToString();
                                        txbDiasCongelamiento.Text = dt1.Rows[0]["DiasCongelamientoMes"].ToString().Replace(',', '.');
                                        txbFechaInicio.Text = dt1.Rows[0]["FechaInicial"].ToString();
                                        txbFechaFinal.Text = dt1.Rows[0]["FechaFinal"].ToString();
                                        txbPlan.Enabled = false;
                                        txbDescripcion.Enabled = false;
                                        txbPrecio.Enabled = false;
                                        txbDescuentoMensual.Enabled = false;
                                        txbMesesMaximo.Enabled = false;
                                        txbDiasCongelamiento.Enabled = false;
                                        txbFechaInicio.Enabled = false;
                                        txbFechaFinal.Enabled = false;
                                        btnAgregar.Text = "⚠ Confirmar borrado ❗";
                                        ltTitulo.Text = "Borrar Plan";
                                    }
                                    dt1.Dispose();
                                }
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("logout");
                    }
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

        private void ListaPlanes()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanes();
            rpPlanes.DataSource = dt;
            rpPlanes.DataBind();
            dt.Dispose();
        }

        protected void rpPlanes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "planes?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "planes?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }
            }
        }

        private bool ValidarPlan(string strNombre)
        {
            bool bExiste = false;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanPorNombre(strNombre);
            if (dt.Rows.Count > 0)
            {
                bExiste = true;
            }
            dt.Dispose();
            return bExiste;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["editid"] != null)
                {
                    string respuesta = cg.ActualizarPlan(int.Parse(Request.QueryString["editid"].ToString()), 
                        txbPlan.Text.ToString().Trim(), 
                        txbDescripcion.Text.ToString(), 
                        int.Parse(txbPrecio.Text.ToString()),
                        double.Parse(txbDescuentoMensual.Text.ToString()),
                        int.Parse(txbMesesMaximo.Text.ToString()),
                        rblColor.SelectedItem.Value.ToString(),
                        double.Parse(txbDiasCongelamiento.Text.ToString()), 
                        txbFechaInicio.Text.ToString(), 
                        txbFechaFinal.Text.ToString());
                }
                if (Request.QueryString["deleteid"] != null)
                {
                    string respuesta = cg.EliminarPlan(int.Parse(Request.QueryString["deleteid"].ToString()));
                }
                Response.Redirect("planes");
            }
            else
            {
                if (!ValidarPlan(txbPlan.Text.ToString()))
                {
                    try
                    {
                        string respuesta = cg.InsertarPlan(txbPlan.Text.ToString().Trim(),
                        txbDescripcion.Text.ToString(),
                        int.Parse(txbPrecio.Text.ToString()),
                        double.Parse(txbDescuentoMensual.Text.ToString()),
                        int.Parse(txbMesesMaximo.Text.ToString()),
                        rblColor.SelectedItem.Value.ToString(),
                        int.Parse(Session["idusuario"].ToString()),
                        double.Parse(txbDiasCongelamiento.Text.ToString()),
                        txbFechaInicio.Text.ToString(),
                        txbFechaFinal.Text.ToString());
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
                    Response.Redirect("planes");
                }
                else
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Ya existe un Plan con ese nombre." +
                        "</div>";
                }
            }
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarPlanes();
                string nombreArchivo = $"Planes_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

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

        protected void btnSimular_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanes();

            Random rnd = new Random();

            if (dt.Rows.Count > 0)
            {
                int intPrecioBase;
                double dobDescuentoMensual;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _strData += "{\r\n";
                    _strData += "label: \"" + dt.Rows[i]["NombrePlan"].ToString() + "\",\r\n";
                    _strData += "backgroundColor: 'rgba(" + rnd.Next(255) + "," + rnd.Next(255) + "," + rnd.Next(255) + ",0)',\r\n";
                    _strData += "borderColor: '" + dt.Rows[i]["ColorPlan"].ToString() + "',\r\n";
                    _strData += "pointBackgroundColor: '" + dt.Rows[i]["ColorPlan"].ToString() + "',\r\n";
                    _strData += "pointBorderColor: \"#fff\",\r\n";
                    _strData += "data: [";

                    intPrecioBase = Convert.ToInt32(dt.Rows[i]["PrecioBase"].ToString());
                    dobDescuentoMensual = Convert.ToInt32(dt.Rows[i]["DescuentoMensual"].ToString());
                    for (int j = 0; j < 12; j++)
                    {
                        double dobDescuento = j * dobDescuentoMensual;
                        double dobTotal = (intPrecioBase - ((intPrecioBase * dobDescuento) / 100)) * (j + 1);
                        
                        _strData += dobTotal + ",";
                    }
                    _strData = _strData.Substring(0, _strData.Length - 1);
                    _strData += "]\r\n";
                    _strData += "},\r\n";
                }

                _strData += "{\r\n";
                _strData += "label: \"" + txbPlan.Text.ToString() + "\",\r\n";
                _strData += "backgroundColor: 'rgba(" + rnd.Next(255) + "," + rnd.Next(255) + "," + rnd.Next(255) + ",0)',\r\n";
                _strData += "borderColor: 'rgba(" + rnd.Next(255) + "," + rnd.Next(255) + "," + rnd.Next(255) + ",0.7)',\r\n";
                _strData += "pointBackgroundColor: 'rgba(" + rnd.Next(255) + "," + rnd.Next(255) + "," + rnd.Next(255) + ",1)',\r\n";
                _strData += "pointBorderColor: \"#fff\",\r\n";
                _strData += "data: [";

                intPrecioBase = Convert.ToInt32(txbPrecio.Text.ToString());
                dobDescuentoMensual = Convert.ToDouble(txbDescuentoMensual.Text.ToString());
                for (int j = 0; j < 12; j++)
                {
                    double dobDescuento = j * dobDescuentoMensual;
                    double dobTotal = (intPrecioBase - ((intPrecioBase * dobDescuento) / 100)) * (j + 1);

                    _strData += dobTotal + ",";
                }
                _strData = _strData.Substring(0, _strData.Length - 1);
                _strData += "]\r\n";
                _strData += "}\r\n";
            }
            //_strData = _strData.Substring(0, _strData.Length - 1);
            dt.Dispose();
        }
    }
}