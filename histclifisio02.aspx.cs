using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace fpWebApp
{
    public partial class histclifisio02 : System.Web.UI.Page
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
                                txbIMC.Attributes.Add("readonly", "readonly");
                                txbPorcGrasa.Attributes.Add("readonly", "readonly");
                                txbPorcMuscular.Attributes.Add("readonly", "readonly");
                                txbFCETanaka.Attributes.Add("readonly", "readonly");
                                txbPesoEsperado.Attributes.Add("readonly", "readonly");
                                txbPesoGraso.Attributes.Add("readonly", "readonly");
                                txbPesoMagro.Attributes.Add("readonly", "readonly");

                                txbPeso.Attributes.Add("type", "number");
                                txbPeso.Attributes.Add("min", "20");
                                txbTalla.Attributes.Add("type", "number");
                                txbTalla.Attributes.Add("min", "80");

                                txbPerimCintura.Attributes.Add("type", "number");
                                txbPerimCadera.Attributes.Add("type", "number");
                                txbPerimAbdomen.Attributes.Add("type", "number");
                                txbPerimPecho.Attributes.Add("type", "number");

                                txbPerimMuslo.Attributes.Add("type", "number");
                                txbPerimPantorrilla.Attributes.Add("type", "number");
                                txbPerimBrazo.Attributes.Add("type", "number");

                                txbPliegueTricipital.Attributes.Add("type", "number");
                                txbPliegueIliocrestal.Attributes.Add("type", "number");
                                txbPliegueAbdominal.Attributes.Add("type", "number");
                                txbPliegueSubescapular.Attributes.Add("type", "number");
                                txbPliegueMuslo.Attributes.Add("type", "number");
                                txbPlieguePantorrilla.Attributes.Add("type", "number");

                                MostrarDatosAfiliado(Request.QueryString["idAfiliado"].ToString());
                                CargarHistoriasClinicas(Request.QueryString["idAfiliado"].ToString());
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            //Actualiza datos en la tabla HistoriaFisioterapeuta
            try
            {
                string strQuery = "UPDATE HistoriaFisioterapeuta SET " +
                    "Peso = " + txbPeso.Text.ToString() + ", " +
                    "Talla = " + txbTalla.Text.ToString() + ", " +
                    "IMC = " + txbIMC.Text.ToString() + ", " +
                    "PerimCintura = '" + txbPerimCintura.Text.ToString() + "', " +
                    "PerimCadera = '" + txbPerimCadera.Text.ToString() + "', " +
                    "PerimAbdomen = '" + txbPerimAbdomen.Text.ToString() + "', " +
                    "PerimPecho = '" + txbPerimPecho.Text.ToString() + "', " +
                    "PerimMuslo = '" + txbPerimMuslo.Text.ToString() + "', " +
                    "PerimPantorrilla = '" + txbPerimPantorrilla.Text.ToString() + "', " +
                    "PerimBrazo = '" + txbPerimBrazo.Text.ToString() + "', " +
                    "PliegueTricipital = '" + txbPliegueTricipital.Text.ToString() + "', " +
                    "PliegueIliocrestal = '" + txbPliegueIliocrestal.Text.ToString() + "', " +
                    "PliegueAbdominal = '" + txbPliegueAbdominal.Text.ToString() + "', " +
                    "PliegueSubescapular = '" + txbPliegueSubescapular.Text.ToString() + "', " +
                    "PliegueMuslo = '" + txbPliegueMuslo.Text.ToString() + "', " +
                    "PlieguePantorrilla = '" + txbPlieguePantorrilla.Text.ToString() + "', " +
                    "PorcGrasa = '" + txbPorcGrasa.Text.ToString() + "', " +
                    "PorcMuscular = '" + txbPorcMuscular.Text.ToString() + "', " +
                    "FCETanaka = '" + txbFCETanaka.Text.ToString() + "', " +
                    "PesoEsperado = '" + txbPesoEsperado.Text.ToString() + "', " +
                    "PesoGraso = '" + txbPesoGraso.Text.ToString() + "', " +
                    "PesoMagro = '" + txbPesoMagro.Text.ToString() + "' " +
                    "WHERE idHistoria = " + Request.QueryString["idHistoria"].ToString();
                clasesglobales cg = new clasesglobales();
                string mensaje = cg.TraerDatosStr(strQuery);

                if (mensaje == "OK")
                {
                    string script = @"
                    Swal.fire({
                        title: 'Siguiente paso...',
                        text: 'Flexibilidad, Pruebas de Fuerza y de Resistencia Cardiovascular',
                        icon: 'success',
                        timer: 2000, // 2 segundos
                        showConfirmButton: false,
                        timerProgressBar: true
                    }).then(() => {
                        window.location.href = 'histclifisio03?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + @"&idHistoria=" + Request.QueryString["idHistoria"].ToString() + @"';
                    });
                    ";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                    //Response.Redirect("histclinutricion02?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + "&idHistoria=" + Request.QueryString["idHistoria"].ToString());
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