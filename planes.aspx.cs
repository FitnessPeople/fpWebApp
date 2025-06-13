using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
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
                        txbDiasCongelamiento.Attributes.Add("type", "number");
                        txbDiasCongelamiento.Attributes.Add("step", "0.1");
                        txbDiasCongelamiento.Attributes.Add("max", "10");
                        txbMeses.Attributes.Add("type", "number");
                        txbMeses.Attributes.Add("min", "1");
                        txbMeses.Attributes.Add("max", "12");
                        txbFechaInicial.Attributes.Add("type", "date");
                        txbFechaFinal.Attributes.Add("type", "date");

                        txbFechaInicial.Attributes.Add("value", DateTime.Now.ToString("yyyy-MM-dd"));
                        txbFechaFinal.Attributes.Add("value", DateTime.Now.ToString("yyyy-MM-dd"));

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
                                    if (dt.Rows[0]["FechaInicial"] == DBNull.Value || string.IsNullOrWhiteSpace(dt.Rows[0]["FechaInicial"].ToString()) &&
                                        dt.Rows[0]["FechaFinal"] == DBNull.Value || string.IsNullOrWhiteSpace(dt.Rows[0]["FechaFinal"].ToString()))
                                    {
                                        txbFechaInicial.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                        txbFechaFinal.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                    }
                                    else
                                    {
                                        txbFechaInicial.Text = Convert.ToDateTime(dt.Rows[0]["FechaInicial"]).ToString("yyyy-MM-dd");
                                        txbFechaFinal.Text = Convert.ToDateTime(dt.Rows[0]["FechaFinal"]).ToString("yyyy-MM-dd");
                                    }

                                    txbPlan.Text = dt.Rows[0]["NombrePlan"].ToString();
                                    //txbTituloPlan.Text = dt.Rows[0]["TituloPlan"].ToString();
                                    txbDescripcion.Text = dt.Rows[0]["DescripcionPlan"].ToString();
                                    int intPrecioBase = Convert.ToInt32(dt.Rows[0]["PrecioBase"]);
                                    txbPrecioBase.Text = intPrecioBase.ToString("C0", new CultureInfo("es-CO"));
                                    txbDiasCongelamiento.Text = dt.Rows[0]["DiasCongelamientoMes"].ToString().Replace(',','.');
                                    int intPrecioTotal = Convert.ToInt32(dt.Rows[0]["PrecioTotal"]);
                                    txbPrecioTotal.Text = intPrecioTotal.ToString("C0", new CultureInfo("es-CO"));
                                    txbMeses.Text = dt.Rows[0]["Meses"].ToString();
                                    txbMesesCortesia.Text = dt.Rows[0]["MesesCortesia"].ToString();
                                    ddlColor.SelectedIndex = Convert.ToInt16(ddlColor.Items.IndexOf(ddlColor.Items.FindByValue(dt.Rows[0]["NombreColorPlan"].ToString())));
                                    cbPermanente.Checked = Convert.ToBoolean(dt.Rows[0]["Permanente"]);
                                    btnAgregar.Text = "Actualizar";
                                    ltTitulo.Text = "Actualizar Plan";

                                    //if (dt.Rows[0]["BannerWeb"].ToString() != "")
                                    //{
                                    //    ltBanner.Text = "<img src=\"img/banners/" + dt.Rows[0]["BannerWeb"].ToString() + "\" class=\"img responsive\" />";
                                    //}
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
                                        if (dt1.Rows[0]["FechaInicial"] == DBNull.Value || string.IsNullOrWhiteSpace(dt1.Rows[0]["FechaInicial"].ToString()) &&
                                        dt1.Rows[0]["FechaFinal"] == DBNull.Value || string.IsNullOrWhiteSpace(dt1.Rows[0]["FechaFinal"].ToString()))
                                        {
                                            txbFechaInicial.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                            txbFechaFinal.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                        }
                                        else
                                        {
                                            txbFechaInicial.Text = Convert.ToDateTime(dt.Rows[0]["FechaInicial"]).ToString("yyyy-MM-dd");
                                            txbFechaFinal.Text = Convert.ToDateTime(dt.Rows[0]["FechaFinal"]).ToString("yyyy-MM-dd");
                                        }

                                        txbPlan.Text = dt1.Rows[0]["NombrePlan"].ToString();
                                        //txbTituloPlan.Text = dt1.Rows[0]["TituloPlan"].ToString();
                                        txbDescripcion.Text = dt1.Rows[0]["DescripcionPlan"].ToString();
                                        txbPrecioBase.Text = dt1.Rows[0]["PrecioBase"].ToString();
                                        txbPrecioTotal.Text = dt.Rows[0]["PrecioTotal"].ToString();
                                        txbMeses.Text = dt.Rows[0]["Meses"].ToString();
                                        txbDiasCongelamiento.Text = dt1.Rows[0]["DiasCongelamientoMes"].ToString().Replace(',', '.');
                                        ddlColor.SelectedValue = dt1.Rows[0]["NombreColorPlan"].ToString();
                                        //ddlColor.SelectedIndex = Convert.ToInt16(ddlColor.Items.IndexOf(ddlColor.Items.FindByValue(dt1.Rows[0]["NombreColorPlan"].ToString())));
                                        cbPermanente.Checked = Convert.ToBoolean(dt.Rows[0]["Permanente"]);
                                        txbPlan.Enabled = false;
                                        txbDescripcion.Enabled = false;
                                        txbPrecioBase.Enabled = false;
                                        txbPrecioTotal.Enabled = false;
                                        txbMeses.Enabled = false;
                                        txbMesesCortesia.Enabled = false;
                                        txbDiasCongelamiento.Enabled = false;
                                        ddlColor.Enabled = false;
                                        txbFechaInicial.Enabled = false;
                                        txbFechaFinal.Enabled = false;
                                        cbPermanente.Enabled = false;
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
                                        if (dt1.Rows[0]["FechaInicial"] == DBNull.Value || string.IsNullOrWhiteSpace(dt1.Rows[0]["FechaInicial"].ToString()) &&
                                        dt1.Rows[0]["FechaFinal"] == DBNull.Value || string.IsNullOrWhiteSpace(dt1.Rows[0]["FechaFinal"].ToString()))
                                        {
                                            txbFechaInicial.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                            txbFechaFinal.Text = DateTime.Now.ToString("yyyy-MM-dd");
                                        }
                                        else
                                        {
                                            txbFechaInicial.Text = Convert.ToDateTime(dt.Rows[0]["FechaInicial"]).ToString("yyyy-MM-dd");
                                            txbFechaFinal.Text = Convert.ToDateTime(dt.Rows[0]["FechaFinal"]).ToString("yyyy-MM-dd");
                                        }

                                        txbPlan.Text = dt1.Rows[0]["NombrePlan"].ToString();
                                        //txbTituloPlan.Text = dt1.Rows[0]["TituloPlan"].ToString();
                                        txbDescripcion.Text = dt1.Rows[0]["DescripcionPlan"].ToString();
                                        txbPrecioBase.Text = dt1.Rows[0]["PrecioBase"].ToString();
                                        txbPrecioTotal.Text = dt1.Rows[0]["PrecioTotal"].ToString();
                                        txbMeses.Text = dt1.Rows[0]["Meses"].ToString();
                                        txbDiasCongelamiento.Text = dt1.Rows[0]["DiasCongelamientoMes"].ToString().Replace(',', '.');
                                        ddlColor.SelectedValue = dt1.Rows[0]["NombreColorPlan"].ToString();
                                        //ddlColor.SelectedIndex = Convert.ToInt16(ddlColor.Items.IndexOf(ddlColor.Items.FindByValue(dt.Rows[0]["NombreColorPlan"].ToString())));
                                        cbPermanente.Checked = Convert.ToBoolean(dt1.Rows[0]["Permanente"]);
                                        txbPlan.Enabled = false;
                                        txbDescripcion.Enabled = false;
                                        txbPrecioBase.Enabled = false;
                                        txbPrecioTotal.Enabled = false;
                                        txbMeses.Enabled = false;
                                        txbMesesCortesia.Enabled = false;
                                        txbDiasCongelamiento.Enabled = false;
                                        ddlColor.Enabled = false;
                                        txbFechaInicial.Enabled = false;
                                        txbFechaFinal.Enabled = false;
                                        cbPermanente.Enabled = false;
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

        /// <summary>
        /// Valida los permisos del usuario en la pagina visitada.
        /// Contiene un parametro.
        /// </summary>
        /// <param name="strPagina"></param>
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

        /// <summary>
        /// Lista todos los planes 
        /// </summary>
        private void ListaPlanes()
        {
            clasesglobales cg = new clasesglobales();
            //DataTable dt = cg.ConsultarPlanes();
            string strQuery = "SELECT *, IF(pm.EstadoPlan='Activo','primary','danger') AS label " +
                "FROM Planes pm " +
                "LEFT JOIN Usuarios u ON pm.idUsuario = u.idUsuario ";
            DataTable dt = cg.TraerDatos(strQuery);
            rpPlanes.DataSource = dt;

            if (!dt.Columns.Contains("TotalMeses"))
                dt.Columns.Add("TotalMeses", typeof(int));

            foreach (DataRow row in dt.Rows)
            {
                int meses = row["Meses"] != DBNull.Value ? Convert.ToInt32(row["Meses"]) : 0;
                int cortesia = row["MesesCortesia"] != DBNull.Value ? Convert.ToInt32(row["MesesCortesia"]) : 0;
                row["TotalMeses"] = meses + cortesia;
            }

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

        /// <summary>
        /// Valida si un plan ya existe con ese mismo nombre
        /// </summary>
        /// <param name="strNombre"></param>
        /// <returns>Devuelve 'true' si existe o 'false' si no existe.</returns>
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

        /// <summary>
        /// Agrega un nuevo plan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            int intPermanente = cbPermanente.Checked ? 1 : 0;
            string fechaInicial = cbPermanente.Checked ? null : txbFechaInicial.Text.ToString();
            string fechaFinal = cbPermanente.Checked ? null : txbFechaFinal.Text.ToString();

            clasesglobales cg = new clasesglobales();
            if (Request.QueryString.Count > 0)
            {
                string strInitData = TraerData();

                string strFilename = "";
                HttpPostedFile postedFile = Request.Files["fileConvenio"];

                if (postedFile != null && postedFile.ContentLength > 0)
                {
                    //Save the File.
                    string filePath = Server.MapPath("~//img//banners//") + Path.GetFileName(postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    strFilename = postedFile.FileName;
                }

                if (Request.QueryString["editid"] != null)
                {
                    string respuesta = cg.ActualizarPlan(int.Parse(Request.QueryString["editid"].ToString()), 
                        txbPlan.Text.ToString().Trim(), 
                        txbDescripcion.Text.ToString(),
                        Convert.ToInt32(Regex.Replace(txbPrecioTotal.Text, @"[^\d]", "")),
                        Convert.ToInt32(Regex.Replace(txbPrecioBase.Text, @"[^\d]", "")),
                        int.Parse(txbMeses.Text.ToString()),
                        int.Parse(txbMesesCortesia.Text.ToString()),
                        ddlColor.SelectedItem.Value.ToString(),
                        int.Parse(Session["idusuario"].ToString()),
                        int.Parse(txbDiasCongelamiento.Text.ToString()),
                        fechaInicial,
                        fechaFinal,
                        intPermanente);

                    string strNewData = TraerData();

                    cg.InsertarLog(Session["idusuario"].ToString(), "planes", "Modifica", "El usuario modificó el plan: " + txbPlan.Text.ToString() + ".", strInitData, strNewData);
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
                        Convert.ToInt32(Regex.Replace(txbPrecioTotal.Text, @"[^\d]", "")),
                        Convert.ToInt32(Regex.Replace(txbPrecioBase.Text, @"[^\d]", "")),
                        int.Parse(txbMeses.Text.ToString()),
                        int.Parse(txbMesesCortesia.Text.ToString()),
                        ddlColor.SelectedItem.Value.ToString(),
                        int.Parse(Session["idusuario"].ToString()),
                        double.Parse(txbDiasCongelamiento.Text.ToString()),
                        txbFechaInicio.Text.ToString(),
                        txbFechaFinal.Text.ToString(), 
                        intPermanente,
                        txbTituloPlan.Text.ToString().Trim());

                        cg.InsertarLog(Session["idusuario"].ToString(), "planes", "Agrega", "El usuario agregó un nuevo plan: " + txbPlan.Text.ToString() + ".", "", "");
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
                string consultaSQL = @"SELECT NombrePlan AS 'Nombre de Plan', 
                    DescripcionPlan AS 'Descripción', 
                    PrecioTotal AS 'Precio Total', 
                    PrecioBase AS 'Precio Base', 
                    Meses AS 'Meses', 
                    MesesCortesia AS 'Meses de Cortesía', 
                    EstadoPlan AS 'Estado', 
                    DiasCongelamientoMes AS 'Cantidad de Días de Congelamiento', 
                    FechaInicial AS 'Fecha de Inicio', 
                    FechaFinal AS 'Fecha de Terminación', 
                    IF(Permanente = 1, 'Si', 'No') AS Permanente,
                    IF(Permanente = 1,'Sin caducidad', CONCAT('Hasta el ', DAY(FechaFinal), ' de ', MONTHNAME(FechaFinal))) AS Vigencia, 
                    IF(Recurrente = 1, 'Si', 'No') AS Recurrente,
                    NombreUsuario AS 'Nombre de Usuario Creador', 
                    EmailUsuario AS 'Correo de Usuario Creador'
                    FROM Planes p 
                    LEFT JOIN Usuarios u ON p.idusuario = u.idUsuario 
                    ORDER BY NombrePlan;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(consultaSQL);
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

        //protected void btnSimular_Click(object sender, EventArgs e)
        //{
        //    clasesglobales cg = new clasesglobales();
        //    DataTable dt = cg.ConsultarPlanes();

        //    Random rnd = new Random();

        //    if (dt.Rows.Count > 0)
        //    {
        //        int intPrecioBase;
        //        int intPrecioTotal;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            _strData += "{\r\n";
        //            _strData += "label: \"" + dt.Rows[i]["NombrePlan"].ToString() + "\",\r\n";
        //            //_strData += "backgroundColor: 'rgba(" + rnd.Next(255) + "," + rnd.Next(255) + "," + rnd.Next(255) + ",1)',\r\n";
        //            //_strData += "borderColor: '" + dt.Rows[i]["ColorPlan"].ToString() + "',\r\n";
        //            _strData += "borderColor: 'rgba(" + rnd.Next(255) + "," + rnd.Next(255) + "," + rnd.Next(255) + ",1)',\r\n";
        //            //_strData += "pointBackgroundColor: '" + dt.Rows[i]["ColorPlan"].ToString() + "',\r\n";
        //            _strData += "pointBackgroundColor: 'rgba(" + rnd.Next(255) + "," + rnd.Next(255) + "," + rnd.Next(255) + ",1)',\r\n";
        //            _strData += "pointBorderColor: \"#fff\",\r\n";
        //            _strData += "data: [";

        //            intPrecioBase = Convert.ToInt32(dt.Rows[i]["PrecioBase"].ToString());
        //            intPrecioTotal = Convert.ToInt32(dt.Rows[i]["PrecioTotal"].ToString());
        //            for (int j = 0; j < 12; j++)
        //            {
        //                if (j < Convert.ToInt16(dt.Rows[i]["Meses"].ToString()))
        //                {
        //                    _strData += intPrecioTotal + ",";
        //                }
        //                else
        //                {
        //                    _strData += "0,";
        //                }
        //                //_strData += intPrecioTotal + ",";
        //            }
        //            _strData = _strData.Substring(0, _strData.Length - 1);
        //            _strData += "]\r\n";
        //            _strData += "},\r\n";
        //        }

        //        _strData += "{\r\n";
        //        _strData += "label: \"" + txbPlan.Text.ToString() + "\",\r\n";
        //        _strData += "backgroundColor: 'rgba(" + rnd.Next(255) + "," + rnd.Next(255) + "," + rnd.Next(255) + ",1',\r\n";
        //        _strData += "borderColor: 'rgba(" + rnd.Next(255) + "," + rnd.Next(255) + "," + rnd.Next(255) + ",0.7)',\r\n";
        //        _strData += "pointBackgroundColor: 'rgba(" + rnd.Next(255) + "," + rnd.Next(255) + "," + rnd.Next(255) + ",1)',\r\n";
        //        _strData += "pointBorderColor: \"#fff\",\r\n";
        //        _strData += "data: [";

        //        intPrecioBase = Convert.ToInt32(txbPrecioBase.Text.ToString());
        //        intPrecioTotal = Convert.ToInt32(txbPrecioTotal.Text.ToString());
        //        for (int j = 0; j < 12; j++)
        //        {
        //            //double dobDescuento = j * dobDescuentoMensual;
        //            //double dobTotal = (intPrecioBase - ((intPrecioBase * dobDescuento) / 100)) * (j + 1);

        //            _strData += intPrecioTotal + ",";
        //        }
        //        _strData = _strData.Substring(0, _strData.Length - 1);
        //        _strData += "]\r\n";
        //        _strData += "}\r\n";
        //    }
        //    //_strData = _strData.Substring(0, _strData.Length - 1);
        //    dt.Dispose();
        //}

        private string TraerData()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarPlanPorId(int.Parse(Request.QueryString["editid"].ToString()));

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