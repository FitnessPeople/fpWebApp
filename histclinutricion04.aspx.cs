using DocumentFormat.OpenXml.Vml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class histclinutricion04 : System.Web.UI.Page
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
                                txbGastoCalorico.Attributes.Add("readonly", "readonly");
                                txbGastoTotal.Attributes.Add("readonly", "readonly");

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
                                txbPliegueMuslo.Attributes.Add ("type", "number");
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
            string strQuery = "SELECT *, " +
                "IF(EstadoAfiliado='Activo','info',IF(EstadoAfiliado='Inactivo','danger','warning')) AS label, " +
                "IF(TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()) IS NOT NULL, TIMESTAMPDIFF(YEAR, FechaNacAfiliado, CURDATE()),'') AS edad " +
                "FROM Afiliados a " +
                "RIGHT JOIN Sedes s ON a.idSede = s.idSede " +
                "LEFT JOIN ciudades c ON c.idCiudad = a.idCiudadAfiliado " +
                "LEFT JOIN generos g ON g.idGenero = a.idGenero " +
                "LEFT JOIN eps ON eps.idEps = a.idEps " +
                "WHERE idAfiliado = '" + idAfiliado + "' ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            //ViewState["DocumentoAfiliado"] = dt.Rows[0]["DocumentoAfiliado"].ToString();
            ltNombre.Text = dt.Rows[0]["NombreAfiliado"].ToString();
            ltApellido.Text = dt.Rows[0]["ApellidoAfiliado"].ToString();
            ltEmail.Text = dt.Rows[0]["EmailAfiliado"].ToString();
            ltCelular.Text = dt.Rows[0]["CelularAfiliado"].ToString();
            ltSede.Text = dt.Rows[0]["NombreSede"].ToString();
            ltDireccion.Text = dt.Rows[0]["DireccionAfiliado"].ToString();
            ltCiudad.Text = dt.Rows[0]["NombreCiudad"].ToString();
            ltCumple.Text = String.Format("{0:dd MMM yyyy}", Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"])) + " (" + dt.Rows[0]["edad"].ToString() + " años)";
            ltGenero.Text = dt.Rows[0]["Genero"].ToString();
            hfGenero.Value = dt.Rows[0]["idGenero"].ToString();
            hfEdad.Value = dt.Rows[0]["edad"].ToString();
            ltEPS.Text = dt.Rows[0]["NombreEps"].ToString();
            ltEstado.Text = "<span class=\"label label-" + dt.Rows[0]["label"].ToString() + "\">" + dt.Rows[0]["EstadoAfiliado"].ToString() + "</span>";
            //ltFoto.Text = "<img src=\"img/afiliados/nofoto.png\" class=\"img-circle circle-border m-b-md\" width=\"120px\" alt=\"profile\">";

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
            string strQuery = "SELECT *, " +
                "IF(Tabaquismo=0,'<i class=\"fa fa-xmark text-navy\"></i>','<i class=\"fa fa-check text-danger\"></i>') AS fuma, " +
                "IF(Alcoholismo=0,'<i class=\"fa fa-xmark text-navy\"></i>','<i class=\"fa fa-check text-danger\"></i>') AS toma, " +
                "IF(Sedentarismo=0,'<i class=\"fa fa-xmark text-navy\"></i>','<i class=\"fa fa-check text-danger\"></i>') AS sedentario, " +
                "IF(Diabetes=0,'<i class=\"fa fa-xmark text-navy\"></i>','<i class=\"fa fa-check text-danger\"></i>') AS diabetico, " +
                "IF(Colesterol=0,'<i class=\"fa fa-xmark text-navy\"></i>',IF(Colesterol=1,'<i class=\"fa fa-check text-danger\"></i>','<i class=\"fa fa-comment-slash text-primary\"></i>')) AS colesterado, " +
                "IF(Trigliceridos=0,'<i class=\"fa fa-xmark text-navy\"></i>',IF(Trigliceridos=1,'<i class=\"fa fa-check text-danger\"></i>','<i class=\"fa fa-comment-slash text-primary\"></i>')) AS triglicerado, " +
                "IF(HTA=0,'<i class=\"fa fa-xmark text-navy\"></i>',IF(HTA=1,'<i class=\"fa fa-check text-danger\"></i>','<i class=\"fa fa-comment-slash text-primary\"></i>')) AS hipertenso, " +
                "(@rownum := @rownum + 1) as nro_fila, " +
                "IF(@rownum=1,'in','') AS clase " +
                "FROM HistoriasClinicas hc " +
                "LEFT JOIN ObjetivosAfiliado oa ON hc.idObjetivoIngreso = oa.idObjetivo " +
                "CROSS JOIN (SELECT @rownum := 0) r " +
                "WHERE idAfiliado = " + idAfiliado + " " +
                "ORDER BY FechaHora DESC ";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

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
                //ltMensaje.Text = "Afiliado sin historias clínicas.";
            }

            dt.Dispose();
        }

        public class HtmlTemplate : ITemplate
        {
            private string _html;

            public HtmlTemplate(string html)
            {
                _html = html;
            }

            public void InstantiateIn(Control container)
            {
                container.Controls.Add(new LiteralControl(_html));
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            //Actualiza datos en la tabla HistoriaAlimentaria
            try
            {
                string strQuery = "UPDATE HistoriaAlimentaria SET " +
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
                    "PliegueMuslo = '" + txbPliegueMuslo.Text.ToString() + "', " +
                    "PlieguePantorrilla = '" + txbPlieguePantorrilla.Text.ToString() + "', " +
                    "PorcGrasa = '" + txbPorcGrasa.Text.ToString() + "', " +
                    "PorcMuscular = '" + txbPorcMuscular.Text.ToString() + "', " +
                    "FCETanaka = '" + txbFCETanaka.Text.ToString() + "', " +
                    "PesoEsperado = '" + txbPesoEsperado.Text.ToString() + "', " +
                    "PesoGraso = '" + txbPesoGraso.Text.ToString() + "', " +
                    "PesoMagro = '" + txbPesoMagro.Text.ToString() + "', " +
                    "GastoCalorico = '" + txbGastoCalorico.Text.ToString() + "', " +
                    "ActividadFisica = '" + ddlActividadFisica.SelectedItem.Value.ToString() + "', " +
                    "GastoTotal = '" + txbGastoTotal.Text.ToString() + "', " +
                    "Diagnostico = '" + txbDiagnostico.Text.ToString() + "', " +
                    "PlanManejo = '" + txbPlanManejo.Text.ToString() + "', " +
                    "Recomendaciones = '" + txbRecomendaciones.Text.ToString() + "', " +
                    "Observaciones = '" + txbObservaciones.Text.ToString() + "' " +
                    "WHERE idHistoria = " + Request.QueryString["idHistoria"].ToString();
                clasesglobales cg = new clasesglobales();
                string mensaje = cg.TraerDatosStr(strQuery);

                if (mensaje == "OK")
                {
                    string script = @"
                    Swal.fire({
                        title: 'Datos nutricionales guardados con exito',
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
            catch (OdbcException ex)
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

            //Response.Redirect("historiasclinicas");
        }
    }
}