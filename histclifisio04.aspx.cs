using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace fpWebApp
{
    public partial class histclifisio04 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Historias clinicas");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["Consulta"].ToString() == "1")
                    {
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            if (Request.QueryString.Count > 0)
                            {
                                MostrarDatosAfiliado(Request.QueryString["idAfiliado"].ToString());
                                CargarHistoriasClinicas(Request.QueryString["idAfiliado"].ToString());
                                CargarCie10();

                                //Consulta si tiene datos de fisioterapia asociado a la historia del afiliado
                                clasesglobales cg = new clasesglobales();
                                DataTable dtHistorias = cg.ConsultarHistoriaClinicaPorId(Convert.ToInt32(Request.QueryString["idHistoria"].ToString()));

                                if (dtHistorias.Rows.Count > 0)
                                {
                                    if (dtHistorias.Rows[0]["idHistoriaFisio"].ToString() != "")
                                    {
                                        //Llena la historia clinica con los datos tomados por el fisioterapeuta.
                                        btnAgregar.Text = "Actualizar y continuar";
                                        txbCabezaAdelantada.Text = dtHistorias.Rows[0]["CabezaAdelantada"].ToString();
                                        txbHombrosDesalineados.Text = dtHistorias.Rows[0]["HombrosDesalineados"].ToString();
                                        txbHipercifosisDorsal.Text = dtHistorias.Rows[0]["HipercifosisDorsal"].ToString();
                                        txbEscoliosis.Text = dtHistorias.Rows[0]["Escoliosis"].ToString();
                                        txbDismetrias.Text = dtHistorias.Rows[0]["Dismetrias"].ToString();
                                        txbGenuValgus.Text = dtHistorias.Rows[0]["GenuValgus"].ToString();
                                        txbGenuVarus.Text = dtHistorias.Rows[0]["GenuVarus"].ToString();
                                        txbGenuRecurbatum.Text = dtHistorias.Rows[0]["GenuRecurbatum"].ToString();
                                        txbGenuAntecurbatum.Text = dtHistorias.Rows[0]["GenuAntecurbatum"].ToString();
                                        txbPiePlano.Text = dtHistorias.Rows[0]["PiePlano"].ToString();
                                        txbPieCavus.Text = dtHistorias.Rows[0]["PieCavus"].ToString();
                                        ddlApto.SelectedIndex = Convert.ToInt32(ddlApto.Items.IndexOf(ddlApto.Items.FindByValue(Convert.ToInt16(dtHistorias.Rows[0]["Apto"]).ToString())));
                                        txbRestricciones.Text = dtHistorias.Rows[0]["Restricciones"].ToString();
                                        txbDiagnostico.Text = dtHistorias.Rows[0]["Diagnostico"].ToString();
                                        txbRecomendaciones.Text = dtHistorias.Rows[0]["Observaciones"].ToString();
                                        txbObservaciones.Text = dtHistorias.Rows[0]["Recomendaciones"].ToString();
                                        ddlCie10.SelectedIndex = Convert.ToInt32(ddlCie10.Items.IndexOf(ddlCie10.Items.FindByValue(Convert.ToInt16(dtHistorias.Rows[0]["idCie10"]).ToString())));
                                    }
                                }
                            }

                            btnAgregar.Visible = true;
                        }
                    }
                }
                else
                {
                    Response.Redirect("logout.aspx");
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

        private void MostrarDatosAfiliado(string idAfiliado)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarAfiliadoPorIdEncabezado(Convert.ToInt32(idAfiliado));

            ltNombre.Text = dt.Rows[0]["NombreAfiliado"].ToString();
            ltApellido.Text = dt.Rows[0]["ApellidoAfiliado"].ToString();
            ltEmail.Text = dt.Rows[0]["EmailAfiliado"].ToString();
            ltCelular.Text = dt.Rows[0]["CelularAfiliado"].ToString();
            ltSede.Text = dt.Rows[0]["NombreSede"].ToString();
            ltDireccion.Text = dt.Rows[0]["DireccionAfiliado"].ToString();
            ltCiudad.Text = dt.Rows[0]["NombreCiudad"].ToString();
            ltCumple.Text = String.Format("{0:dd MMM yyyy}", Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"])) + " (" + dt.Rows[0]["edad"].ToString() + " años)";
            ltGenero.Text = dt.Rows[0]["Genero"].ToString();
            ltEPS.Text = dt.Rows[0]["NombreEps"].ToString();
            hfGenero.Value = dt.Rows[0]["idGenero"].ToString();
            hfEdad.Value = dt.Rows[0]["edad"].ToString();

            string label = "warning";
            if (dt.Rows[0]["EstadoAfiliado"].ToString() == "Activo")
            {
                label = "info";
            }
            else
            {
                if (dt.Rows[0]["EstadoAfiliado"].ToString() == "Inactivo")
                {
                    label = "danger";
                }
            }
            ltEstado.Text = "<span class=\"label label-" + label + "\">" + dt.Rows[0]["EstadoAfiliado"].ToString() + "</span>";

            if (dt.Rows[0]["FotoAfiliado"].ToString() != "")
            {
                ltFoto.Text = "<img src=\"img/afiliados/" + dt.Rows[0]["FotoAfiliado"].ToString() + "\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
            }
            else
            {
                if (dt.Rows[0]["idGenero"].ToString() == "1" || dt.Rows[0]["idGenero"].ToString() == "3")
                {
                    ltFoto.Text = "<img src=\"img/afiliados/avatar_male.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                }
                if (dt.Rows[0]["idGenero"].ToString() == "2")
                {
                    ltFoto.Text = "<img src=\"img/afiliados/avatar_female.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";
                }
            }
        }

        private void CargarHistoriasClinicas(string idAfiliado)
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.ConsultarHistoriaClinicaPorIdAfiliado(Convert.ToInt32(idAfiliado));

            if (dt.Rows.Count > 0)
            {
                rpHistorias.DataSource = dt;
                rpHistorias.DataBind();
            }
            else
            {
                ltMensaje.Text = "<div class=\"ibox-content\">" +
                    "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "Afiliado sin historias clínicas." +
                    "</div></div>";
            }

            dt.Dispose();
        }

        private void CargarCie10()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.CargarCie10();
            ddlCie10.DataSource = dt;
            ddlCie10.DataBind();
            dt.Dispose();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            //Actualiza datos en la tabla HistoriaFisioterapeuta
            try
            {
                clasesglobales cg = new clasesglobales();
                string mensaje = cg.ActualizarHistoriaFisioterapeuta4(
                    Convert.ToInt32(Request.QueryString["idHistoria"].ToString()),
                    txbCabezaAdelantada.Text.ToString(),
                    txbHombrosDesalineados.Text.ToString(),
                    txbHipercifosisDorsal.Text.ToString(),
                    txbEscoliosis.Text.ToString(),
                    txbDismetrias.Text.ToString(),
                    txbGenuValgus.Text.ToString(),
                    txbGenuVarus.Text.ToString(),
                    txbGenuRecurbatum.Text.ToString(),
                    txbGenuAntecurbatum.Text.ToString(),
                    txbPiePlano.Text.ToString(),
                    txbPieCavus.Text.ToString(),
                    ddlApto.SelectedItem.Value.ToString(),
                    txbRestricciones.Text.ToString(),
                    txbDiagnostico.Text.ToString(),
                    txbObservaciones.Text.ToString(),
                    txbRecomendaciones.Text.ToString(),
                    Convert.ToInt32(ddlCie10.SelectedItem.Value.ToString())
                    );

                if (mensaje == "OK")
                {
                    string script = @"
                    Swal.fire({
                        title: 'Datos fisioterapéuticos guardados con exito',
                        text: '',
                        icon: 'success',
                        timer: 2000, // 2 segundos
                        showConfirmButton: false,
                        timerProgressBar: true
                    }).then(() => {
                        window.location.href = 'historiasclinicas';
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
                                            
                            }
                        });
                    ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
                }
            }
            catch (SqlException ex)
            {
                string mensaje = ex.Message;
                string script = @"
                    Swal.fire({
                        title: 'Error',
                        text: '" + mensaje.Replace("'", "\\'") + @"',
                        icon: 'error'
                    }).then((result) => {
                        if (result.isConfirmed) {
                                            
                        }
                    });
                ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ErrorMensajeModal", script, true);
            }
        }
    }
}