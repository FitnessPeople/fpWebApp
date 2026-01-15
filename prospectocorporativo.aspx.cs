using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class prospectocorporativo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Prospecto corporativo");
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
                    CargarTipoDocumento();
                    ListaProspectosEmpresas();
                    CargarCiudad();

                    ltTitulo.Text = "Agregar empresa prospecto";
                    if (Request.QueryString.Count > 0)
                    {
                        rpEmpresasCRM.Visible = false;
                        bool respuesta = false;
                        if (Request.QueryString["editid"] != null)
                        {
                            //Editar
                            clasesglobales cg = new clasesglobales();
                            DataTable dt = new DataTable();

                            dt = cg.ConsultarEmpresaCRMPorId(int.Parse(Request.QueryString["editid"].ToString()), out respuesta);
                            if (dt.Rows.Count > 0 && respuesta)
                            {
                                if (dt.Rows[0]["idTipoDocumento"].ToString() != "")
                                    ddlTipoDocumento.SelectedIndex = Convert.ToInt32(ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dt.Rows[0]["idTipoDocumento"].ToString())));
                                else
                                    ddlTipoDocumento.SelectedItem.Value = "0";

                                ddlTipoDocumento.SelectedIndex = Convert.ToInt32(ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dt.Rows[0]["idTipoDocumento"].ToString())));
                                txbDocumento.Text = dt.Rows[0]["DocumentoEmpresa"].ToString();
                                txbDigitoVerificacion.Text = dt.Rows[0]["digitoverificacion"].ToString();
                                txbRazonSocial.Value = dt.Rows[0]["NombreEmpresaCRM"].ToString();
                                txbNombreComercialEmpresa.Value = dt.Rows[0]["NombreComercial"].ToString();
                                txbNombreContacto.Value = dt.Rows[0]["NombreContacto"].ToString();
                                txbCargoContacto.Value = dt.Rows[0]["CargoContacto"].ToString();
                                txbCelularEmpresa.Value = dt.Rows[0]["CelularEmpresa"].ToString();
                                txbCorreoEmpresa.Value = dt.Rows[0]["CorreoEmpresa"].ToString();

                                if (dt.Rows[0]["idCiudad"].ToString() != "")
                                    ddlCiudades.SelectedIndex = Convert.ToInt32(ddlCiudades.Items.IndexOf(ddlCiudades.Items.FindByValue(dt.Rows[0]["idCiudad"].ToString())));
                                else
                                    ddlCiudades.SelectedItem.Value = "0";

                                txaObservaciones.Value = dt.Rows[0]["ObservacionesEmp"].ToString();

                                btnAgregar.Text = "Actualizar";
                                ltTitulo.Text = "Actualizar empresa prospecto";
                            }
                        }
                        if (Request.QueryString["deleteid"] != null)
                        {
                            clasesglobales cg = new clasesglobales();
                            string mensaje = string.Empty;

                            DataTable dt = cg.ValidarEmpresaCRMPorId(Convert.ToInt32(Request.QueryString["deleteid"].ToString()), out respuesta, out mensaje);
                            if (!respuesta)
                            {
                                ltMensaje.Text = "<div class=\"ibox-content\">" +
                                    "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                    mensaje +
                                    "</div></div>";

                                DataTable dt1 = new DataTable();
                                dt1 = cg.ConsultarEmpresaCRMPorId(int.Parse(Request.QueryString["deleteid"].ToString()), out respuesta);
                                if (dt.Rows.Count > 0 && respuesta)
                                {
                                    if (dt1.Rows[0]["idTipoDocumento"].ToString() != "")
                                        ddlTipoDocumento.SelectedIndex = Convert.ToInt32(ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dt1.Rows[0]["idTipoDocumento"].ToString())));
                                    else
                                        ddlTipoDocumento.SelectedItem.Value = "0";
                                    ddlTipoDocumento.SelectedIndex = Convert.ToInt32(ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dt1.Rows[0]["idTipoDocumento"].ToString())));
                                    txbDocumento.Text = dt1.Rows[0]["DocumentoEmpresa"].ToString();
                                    txbDigitoVerificacion.Text = dt1.Rows[0]["digitoverificacion"].ToString();
                                    txbRazonSocial.Value = dt1.Rows[0]["NombreEmpresaCRM"].ToString();
                                    txbNombreComercialEmpresa.Value = dt1.Rows[0]["NombreComercial"].ToString();
                                    txbNombreContacto.Value = dt1.Rows[0]["NombreContacto"].ToString();
                                    txbCargoContacto.Value = dt1.Rows[0]["CargoContacto"].ToString();
                                    txbCelularEmpresa.Value = dt1.Rows[0]["CelularEmpresa"].ToString();
                                    txbCorreoEmpresa.Value = dt1.Rows[0]["CorreoEmpresa"].ToString();
                                    if (dt1.Rows[0]["idCiudad"].ToString() != "")
                                        ddlCiudades.SelectedIndex = Convert.ToInt32(ddlCiudades.Items.IndexOf(ddlCiudades.Items.FindByValue(dt1.Rows[0]["idCiudad"].ToString())));
                                    else
                                        ddlCiudades.SelectedItem.Value = "0";
                                    txaObservaciones.Value = dt1.Rows[0]["ObservacionesEmp"].ToString();

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
                                dt1 = cg.ConsultarEmpresaCRMPorId(int.Parse(Request.QueryString["deleteid"].ToString()), out respuesta);
                                if (dt1.Rows.Count > 0)
                                {
                                    if (dt1.Rows[0]["idTipoDocumento"].ToString() != "")
                                        ddlTipoDocumento.SelectedIndex = Convert.ToInt32(ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dt1.Rows[0]["idTipoDocumento"].ToString())));
                                    else
                                        ddlTipoDocumento.SelectedItem.Value = "0";
                                    ddlTipoDocumento.SelectedIndex = Convert.ToInt32(ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dt1.Rows[0]["idTipoDocumento"].ToString())));
                                    txbDocumento.Text = dt1.Rows[0]["DocumentoEmpresa"].ToString();
                                    txbDigitoVerificacion.Text = dt1.Rows[0]["digitoverificacion"].ToString();
                                    txbRazonSocial.Value = dt1.Rows[0]["NombreEmpresaCRM"].ToString();
                                    txbNombreComercialEmpresa.Value = dt1.Rows[0]["NombreComercial"].ToString();
                                    txbNombreContacto.Value = dt1.Rows[0]["NombreContacto"].ToString();
                                    txbCargoContacto.Value = dt1.Rows[0]["CargoContacto"].ToString();
                                    txbCelularEmpresa.Value = dt1.Rows[0]["CelularEmpresa"].ToString();
                                    txbCorreoEmpresa.Value = dt1.Rows[0]["CorreoEmpresa"].ToString();
                                    if (dt1.Rows[0]["idCiudad"].ToString() != "")
                                        ddlCiudades.SelectedIndex = Convert.ToInt32(ddlCiudades.Items.IndexOf(ddlCiudades.Items.FindByValue(dt1.Rows[0]["idCiudad"].ToString())));
                                    else
                                        ddlCiudades.SelectedItem.Value = "0";
                                    txaObservaciones.Value = dt1.Rows[0]["ObservacionesEmp"].ToString();

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

        private void CargarCiudad()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarCiudadesCol();

            ddlCiudades.DataSource = dt;
            ddlCiudades.DataBind();

            dt.Dispose();
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
        private void CargarTipoDocumento()
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultartiposDocumento();

                DataView dv = new DataView(dt);
                dv.RowFilter = "idTipoDoc = 7";

                ddlTipoDocumento.DataSource = dv;
                ddlTipoDocumento.DataBind();

                dt.Dispose();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }

        }

        private void ListaProspectosEmpresas()
        {
            try
            {
                DataTable dt = new DataTable();
                clasesglobales cg = new clasesglobales();

                int idUsuario = Convert.ToInt32(Session["idUsuario"]);
                DataTable dt1 = cg.ConsultarUsuarioSedePerfilPorId(idUsuario);
                int idPerfil = Convert.ToInt32(dt1.Rows[0]["idPerfil"]);

                if (idPerfil == 36 || idPerfil == 1 || idPerfil == 37) // 36: Líder corporativo, 1: CEO, 37: Director operativo
                {
                    phAsesorHeader.Visible = true;
                    rpEmpresasCRM.ItemDataBound += (s, e) =>
                    {
                        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                        {
                            PlaceHolder phCol = (PlaceHolder)e.Item.FindControl("phAsesorCol");
                            if (phCol != null) phCol.Visible = true;
                        }
                    };

                    dt = cg.ConsultarEmpresasCRM();
                }
                else
                {
                    dt = cg.ConsultarEmpresasCRMPorUsuario(idUsuario);
                }

                rpEmpresasCRM.DataSource = dt;
                rpEmpresasCRM.DataBind();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            bool salida = false;
            string mensaje = string.Empty;

            if (Request.QueryString.Count > 0)
            {
                string strInitData = TraerData();

                if (Request.QueryString["editid"] != null)
                {
                    try
                    {
                        string respuesta = cg.ActualizarEmpresaCRM(int.Parse(Request.QueryString["editid"].ToString()), txbRazonSocial.Value.ToString().Trim().ToUpper(),
                        Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value.ToString()), txbDocumento.Text, txbDigitoVerificacion.Text, txbCelularEmpresa.Value.ToString(),
                        txbCorreoEmpresa.Value.ToString().Trim().ToLower(), Convert.ToInt32(ddlCiudades.SelectedItem.Value.ToString()), txaObservaciones.Value,
                        Convert.ToInt32(Session["idUsuario".ToString()]), txbNombreComercialEmpresa.Value.ToString().Trim().ToUpper(), txbNombreContacto.Value.ToString().Trim().ToUpper(),
                        txbCargoContacto.Value.ToString().Trim().ToUpper(), out salida, out mensaje);

                        if (salida)
                        {
                            string strNewData = TraerData();
                            cg.InsertarLog(Session["idusuario"].ToString(), "prospecto empresa", "Modifica", "El usuario modificó  el prospecto empresa: " + "" + ".", strInitData, strNewData);


                            string script = @"
                                Swal.fire({
                                    title: 'Registro actualizado correctamente.',
                                    text: '" + mensaje.Replace("'", "\\'") + @"',
                                    icon: 'success',
                                    timer: 3000, // 3 segundos
                                    showConfirmButton: false,
                                    timerProgressBar: true
                                }).then(() => {
                                    window.location.href = 'prospectosempresas';
                                });
                                ";

                            ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                        }
                        else
                        {
                            string script = @"
                            Swal.fire({
                                title: 'Error',
                                text: '" + mensaje.Replace("'", "\\'") + @"',
                                icon: 'error'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                  window.location.href = 'prospectosempresas';
                                }
                            });
                        ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        string script = @"
                        Swal.fire({
                        title: 'Error',
                        text: 'Ha ocurrido un error inesperado." + ex.Message.ToString() + @"',
                        icon: 'error'
                    });
                    ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);

                    }
                }

                if (Request.QueryString["deleteid"] != null)
                {
                    try
                    {
                        string respuesta = cg.EliminarEmpresaCRM(int.Parse(Request.QueryString["deleteid"].ToString()),
                        Convert.ToInt32(Session["idUsuario"].ToString()), Session["NombreUsuario"].ToString(), out salida, out mensaje);

                        if (salida)
                        {
                            string strNewData = TraerData();
                            cg.InsertarLog(Session["idusuario"].ToString(), "prospecto empresa", "Modifica", "El usuario modificó  el prospecto empresa: " + "" + ".", strInitData, strNewData);


                            string script = @"
                                Swal.fire({
                                    title: 'Registro eliminado correctamente.',
                                    text: '" + mensaje.Replace("'", "\\'") + @"',
                                    icon: 'success',
                                    timer: 3000, // 3 segundos
                                    showConfirmButton: false,
                                    timerProgressBar: true
                                }).then(() => {
                                    window.location.href = 'prospectosempresas';
                                });
                                ";

                            ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                        }
                        else
                        {
                            string script = @"
                            Swal.fire({
                                title: 'Error',
                                text: '" + mensaje.Replace("'", "\\'") + @"',
                                icon: 'error'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                  window.location.href = 'prospectosempresas';
                                }
                            });
                        ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        string script = @"
                        Swal.fire({
                        title: 'Error',
                        text: 'Ha ocurrido un error inesperado." + ex.Message.ToString() + @"',
                        icon: 'error'
                    });
                    ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                    }

                }

            }
            else
            {
                DataTable dt3 = new DataTable();
                dt3 = cg.ConsultarEmpresasCRMPorNombre(txbRazonSocial.Value.ToString());
                if (dt3.Rows.Count == 0)
                {
                    try
                    {
                        string respuesta = cg.InsertarEmpresaCRM(txbRazonSocial.Value.ToString().Trim().ToUpper(), Convert.ToInt32(ddlTipoDocumento.SelectedItem.Value.ToString()),
                        txbDocumento.Text, txbDigitoVerificacion.Text, txbCelularEmpresa.Value.ToString(), txbCorreoEmpresa.Value.ToString().Trim().ToLower(),
                        Convert.ToInt32(ddlCiudades.SelectedItem.Value.ToString()), txaObservaciones.Value, Convert.ToInt32(Session["idUsuario".ToString()]),
                        txbNombreComercialEmpresa.Value.ToString().Trim().ToUpper(), txbNombreContacto.Value.ToString().Trim().ToUpper(),
                        txbCargoContacto.Value.ToString().Trim().ToUpper(), out salida, out mensaje);

                        cg.InsertarLog(Session["idusuario"].ToString(), "prospectos empresas", "Agrega", "El usuario agregó un nuevo prospecto empresa crm: " + "" + ".", "", "");

                        if (salida)
                        {
                            string script = @"
                                Swal.fire({
                                    title: 'La empresa prospecto se creó correctamente.',
                                    text: '" + mensaje.Replace("'", "\\'") + @"',
                                    icon: 'success',
                                    timer: 3000, // 3 segundos
                                    showConfirmButton: false,
                                    timerProgressBar: true
                                }).then(() => {
                                    window.location.href = 'prospectosempresas';
                                });
                                ";

                            ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                        }
                        else
                        {
                            string script = @"
                            Swal.fire({
                                title: 'Warning',
                                text: '" + mensaje.Replace("'", "\\'") + @"',
                                icon: 'error'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                  window.location.href = 'prospectosempresas';
                                }
                            });
                        ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        string script = @"
                        Swal.fire({
                        title: 'Error',
                        text: 'Ha ocurrido un error inesperado." + ex.Message.ToString() + @"',
                        icon: 'error'
                    });
                    ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ErrorCatch", script, true);
                    }
                    // Response.Redirect("empresasprospectos");
                }
                else
                {
                    ltMensaje.Text = "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Ya existe un prospecto empresa con ese nombre." +
                    "</div>";
                }
            }
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarEmpresasCRM();
                string nombreArchivo = $"Empresas_prospecto{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

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
            bool respuesta = false;
            clasesglobales cg = new clasesglobales();

            int idEmpresaProspecto = 0;


            if (!string.IsNullOrEmpty(Request.QueryString["editid"]))
            {
                idEmpresaProspecto = Convert.ToInt32(Request.QueryString["editid"].ToString());
            }
            else if (!string.IsNullOrEmpty(Request.QueryString["deleteid"]))
            {
                idEmpresaProspecto = Convert.ToInt32(Request.QueryString["deleteid"].ToString());
            }

            DataTable dt = cg.ConsultarEmpresaCRMPorId(idEmpresaProspecto, out respuesta);

            string strData = "";
            if (dt.Rows.Count > 0 && respuesta)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    strData += column.ColumnName + ": " + dt.Rows[0][column] + "\r\n";
                }
                dt.Dispose();
            }
            return strData;
        }





        protected void rpEmpresasCRM_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView row = (DataRowView)e.Item.DataItem;

                // 👉 Configuración de botones
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    btnEditar.Attributes.Add("href", "prospectosempresas?editid=" + row.Row[0].ToString());
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "prospectosempresas?deleteid=" + row.Row[0].ToString());
                    btnEliminar.Visible = true;
                }

                // 👉 Cálculo de "Hace cuánto"
                if (row["FechaCreacion"] != DBNull.Value)
                {
                    DateTime fechaCreacion = Convert.ToDateTime(row["FechaCreacion"]);
                    TimeSpan diferencia = DateTime.Now - fechaCreacion;

                    string leyenda;
                    if (diferencia.TotalMinutes < 1)
                    {
                        leyenda = "Hace menos de un minuto";
                    }
                    else if (diferencia.TotalMinutes < 60)
                    {
                        leyenda = $"Hace {(int)diferencia.TotalMinutes} minuto{((int)diferencia.TotalMinutes == 1 ? "" : "s")}";
                    }
                    else if (diferencia.TotalHours < 24)
                    {
                        leyenda = $"Hace {(int)diferencia.TotalHours} hora{((int)diferencia.TotalHours == 1 ? "" : "s")}";
                    }
                    else if (diferencia.TotalDays < 30)
                    {
                        leyenda = $"Hace {(int)diferencia.TotalDays} día{((int)diferencia.TotalDays == 1 ? "" : "s")}";
                    }
                    else if (diferencia.TotalDays < 365)
                    {
                        int meses = (int)(diferencia.TotalDays / 30);
                        leyenda = $"Hace {meses} mes{(meses == 1 ? "" : "es")}";
                    }
                    else
                    {
                        int años = (int)(diferencia.TotalDays / 365);
                        leyenda = $"Hace {años} año{(años == 1 ? "" : "s")}";
                    }

                    Literal ltTiempo = (Literal)e.Item.FindControl("ltTiempoTranscurrido");
                    if (ltTiempo != null)
                        ltTiempo.Text = leyenda;
                }
            }
        }



    }
}