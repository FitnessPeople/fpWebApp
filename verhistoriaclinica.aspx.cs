using Microsoft.Ajax.Utilities;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class verhistoriaclinica : System.Web.UI.Page
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
                            CargarObjetivos();
                            if (Request.QueryString.Count > 0)
                            {
                                MostrarDatosAfiliado(Request.QueryString["idAfiliado"].ToString());
                                CargarHistoriasClinicas(Request.QueryString["idAfiliado"].ToString());
                            }

                            txbFum.Attributes.Add("type", "date");
                            txbCigarrillos.Attributes.Add("type", "number");
                            txbBebidas.Attributes.Add("type", "number");
                            
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

        private void CargarObjetivos()
        {
            string strQuery = "SELECT * FROM ObjetivosAfiliado";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlObjetivo.DataSource = dt;
            ddlObjetivo.DataBind();

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

                LlenarHistoriasClinicas(Request.QueryString["idAfiliado"].ToString());
                btnContinuar.Visible = true;
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

        private void LlenarHistoriasClinicas(string idAfiliado)
        {
            string strQuery = "SELECT * " +
                "FROM HistoriasClinicas hc " +
                "LEFT JOIN ObjetivosAfiliado oa ON hc.idObjetivoIngreso = oa.idObjetivo " +
                "WHERE idAfiliado = " + idAfiliado + " " +
                "ORDER BY FechaHora DESC " +
                "LIMIT 1";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            txbMedicinaPrepagada.Text = dt.Rows[0]["MedicinaPrepagada"].ToString();
            ddlObjetivo.SelectedIndex = Convert.ToInt32(ddlObjetivo.Items.IndexOf(ddlObjetivo.Items.FindByValue(dt.Rows[0]["idObjetivoIngreso"].ToString())));
            txbDescripcionObjetivo.Text = dt.Rows[0]["DescripcionObjetivoIngreso"].ToString();

            //Antecedentes
            txbAnteFamiliares.Text = dt.Rows[0]["AnteFamiliar"].ToString();
            txbAntePatologico.Text = dt.Rows[0]["AntePatologico"].ToString();
            txbAnteQuirurgico.Text = dt.Rows[0]["AnteQuirurgico"].ToString();
            txbAnteTraumatologico.Text = dt.Rows[0]["AnteTraumatologico"].ToString();
            txbAnteFarmacologico.Text = dt.Rows[0]["AnteFarmacologico"].ToString();
            txbAnteActividadFisica.Text = dt.Rows[0]["AnteActividadFisica"].ToString();
            txbAnteToxicologico.Text = dt.Rows[0]["AnteToxicologico"].ToString();
            txbAnteHospitalario.Text = dt.Rows[0]["AnteHospitalario"].ToString();
            txbAnteGinecoObstetricio.Text = dt.Rows[0]["AnteGineco"].ToString();

            if (dt.Rows[0]["AnteFUM"].ToString() != "")
            {
                DateTime dtFecha = Convert.ToDateTime(dt.Rows[0]["AnteFUM"].ToString());
                txbFum.Text = dtFecha.ToString("yyyy-MM-dd");
            }

            //Factores de Riesgo Cardiovascular
            rblFuma.SelectedIndex = Convert.ToInt32(rblFuma.Items.IndexOf(rblFuma.Items.FindByValue(Convert.ToInt16(dt.Rows[0]["Tabaquismo"]).ToString())));
            txbCigarrillos.Text = dt.Rows[0]["Cigarrillos"].ToString();
            rblToma.SelectedIndex = Convert.ToInt32(rblToma.Items.IndexOf(rblToma.Items.FindByValue(Convert.ToInt16(dt.Rows[0]["Alcoholismo"]).ToString())));
            txbBebidas.Text = dt.Rows[0]["Bebidas"].ToString();
            rblSedentarismo.SelectedIndex = Convert.ToInt32(rblSedentarismo.Items.IndexOf(rblSedentarismo.Items.FindByValue(Convert.ToInt16(dt.Rows[0]["Sedentarismo"]).ToString())));
            rblDiabetes.SelectedIndex = Convert.ToInt32(rblDiabetes.Items.IndexOf(rblDiabetes.Items.FindByValue(Convert.ToInt16(dt.Rows[0]["Diabetes"]).ToString())));
            rblColesterol.SelectedIndex = Convert.ToInt32(rblColesterol.Items.IndexOf(rblColesterol.Items.FindByValue(Convert.ToInt16(dt.Rows[0]["Colesterol"]).ToString())));
            rblTrigliceridos.SelectedIndex = Convert.ToInt32(rblTrigliceridos.Items.IndexOf(rblTrigliceridos.Items.FindByValue(Convert.ToInt16(dt.Rows[0]["Trigliceridos"]).ToString())));
            rblHTA.SelectedIndex = Convert.ToInt32(rblHTA.Items.IndexOf(rblHTA.Items.FindByValue(Convert.ToInt16(dt.Rows[0]["HTA"]).ToString())));

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
            //Inserta datos en la tabla HistoriasClinicas
            try
            {
                string strQuery = "INSERT INTO HistoriasClinicas " +
                "(idAfiliado, FechaHora, MedicinaPrepagada, idObjetivoIngreso, DescripcionObjetivoIngreso, AnteFamiliar, AntePatologico, " +
                "AnteQuirurgico, AnteToxicologico, AnteHospitalario, AnteTraumatologico, AnteFarmacologico, AnteActividadFisica, AnteGineco, " +
                "AnteFUM, Tabaquismo, Cigarrillos, Alcoholismo, Bebidas, Sedentarismo, Diabetes, Colesterol, Trigliceridos, HTA) " +
                "VALUES (" + Request.QueryString["idAfiliado"].ToString() + ", CURRENT_TIMESTAMP(), '" + txbMedicinaPrepagada.Text.ToString() + "', " +
                "" + ddlObjetivo.SelectedItem.Value.ToString() + ", '" + txbDescripcionObjetivo.Text.ToString() + "', " +
                "'" + txbAnteFamiliares.Text.ToString() + "', '" + txbAntePatologico.Text.ToString() + "', " +
                "'" + txbAnteQuirurgico.Text.ToString() + "', '" + txbAnteToxicologico.Text.ToString() + "', " +
                "'" + txbAnteHospitalario.Text.ToString() + "', '" + txbAnteTraumatologico.Text.ToString() + "', " +
                "'" + txbAnteFarmacologico.Text.ToString() + "', '" + txbAnteActividadFisica.Text.ToString() + "', " +
                "'" + txbAnteGinecoObstetricio.Text.ToString() + "', '" + txbFum.Text.ToString() + "', " +
                "" + rblFuma.SelectedItem.Value.ToString() + ", " + txbCigarrillos.Text.ToString() + ", " +
                "" + rblToma.SelectedItem.Value.ToString() + ", " + txbBebidas.Text.ToString() + ", " +
                "" + rblSedentarismo.SelectedItem.Value.ToString() + ", " + rblDiabetes.SelectedItem.Value.ToString() + ", " +
                "" + rblColesterol.SelectedItem.Value.ToString() + ", " + rblTrigliceridos.SelectedItem.Value.ToString() + ", " +
                "" + rblHTA.SelectedItem.Value.ToString() + ") ";
                clasesglobales cg = new clasesglobales();
                string mensaje = cg.TraerDatosStr(strQuery);

                strQuery = "SELECT idHistoria FROM HistoriasClinicas WHERE idAfiliado = " + Request.QueryString["idAfiliado"].ToString() + " ORDER BY idHistoria DESC LIMIT 1";
                DataTable dt = cg.TraerDatos(strQuery);
                string idHistoria = dt.Rows[0]["idHistoria"].ToString();
                dt.Dispose();

                if (mensaje == "OK")
                {
                    //Avanzamos según el perfil
                    if (Session["idPerfil"].ToString() == "5") //Medico deportologo
                    {
                        string script = @"
                            Swal.fire({
                                title: 'La historia clínica se creo de forma exitosa',
                                text: '',
                                icon: 'success',
                                timer: 2000, // 2 segundos
                                showConfirmButton: false,
                                timerProgressBar: true
                            }).then(() => {
                                window.location.href = 'histclideporte01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + @"&idHistoria=" + idHistoria + @"';
                            });
                            ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                        //Response.Redirect("histclideporte01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + "&idHistoria=" + idHistoria);
                    }
                    if (Session["idPerfil"].ToString() == "8") //Fisioterapeuta
                    {
                        string script = @"
                            Swal.fire({
                                title: 'La historia clínica se creo de forma exitosa',
                                text: '',
                                icon: 'success',
                                timer: 2000, // 2 segundos
                                showConfirmButton: false,
                                timerProgressBar: true
                            }).then(() => {
                                window.location.href = 'histclifisio01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + @"&idHistoria=" + idHistoria + @"';
                            });
                            ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                        //Response.Redirect("histclifisio01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + "&idHistoria=" + idHistoria);
                    }
                    if (Session["idPerfil"].ToString() == "9") //Nutricionista
                    {
                        string script = @"
                            Swal.fire({
                                title: 'La historia clínica se creo de forma exitosa',
                                text: '',
                                icon: 'success',
                                timer: 2000, // 2 segundos
                                showConfirmButton: false,
                                timerProgressBar: true
                            }).then(() => {
                                window.location.href = 'histclinutricion01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + @"&idHistoria=" + idHistoria + @"';
                            });
                            ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                        //Response.Redirect("histclinutricion01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + "&idHistoria=" + idHistoria);
                    }
                    if (Session["idPerfil"].ToString() == "1") //OJO comentar esta condición
                    {
                        string script = @"
                            Swal.fire({
                                title: 'La historia clínica se creo de forma exitosa',
                                text: '',
                                icon: 'success',
                                timer: 2000, // 2 segundos
                                showConfirmButton: false,
                                timerProgressBar: true
                            }).then(() => {
                                window.location.href = 'histclifisio01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + @"&idHistoria=" + idHistoria + @"';
                            });
                            ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                        //Response.Redirect("histclinutricion01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + "&idHistoria=" + idHistoria);
                    }
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
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            string strQuery = "SELECT idHistoria FROM HistoriasClinicas WHERE idAfiliado = " + Request.QueryString["idAfiliado"].ToString() + " ORDER BY idHistoria DESC LIMIT 1";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);
            string idHistoria = dt.Rows[0]["idHistoria"].ToString();
            dt.Dispose();

            //Avanzamos según el perfil
            if (Session["idPerfil"].ToString() == "5") //Medico deportologo
            {
                string script = @"
                    Swal.fire({
                        title: 'Siguiente paso...',
                        text: 'Riesgo cardiovascular y signos vitales',
                        icon: 'success',
                        timer: 2000, // 2 segundos
                        showConfirmButton: false,
                        timerProgressBar: true
                    }).then(() => {
                        window.location.href = 'histclideporte01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + @"&idHistoria=" + idHistoria + @"';
                    });
                    ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                //Response.Redirect("histclideporte01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + "&idHistoria=" + idHistoria);
            }
            if (Session["idPerfil"].ToString() == "8") //Fisioterapeuta
            {
                string script = @"
                    Swal.fire({
                        title: 'Siguiente paso...',
                        text: '',
                        icon: 'success',
                        timer: 2000, // 2 segundos
                        showConfirmButton: false,
                        timerProgressBar: true
                    }).then(() => {
                        window.location.href = 'histclifisio01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + @"&idHistoria=" + idHistoria + @"';
                    });
                    ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                //Response.Redirect("histclifisio01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + "&idHistoria=" + idHistoria);
            }
            if (Session["idPerfil"].ToString() == "9") //Nutricionista
            {
                string script = @"
                    Swal.fire({
                        title: 'Siguiente paso...',
                        text: 'Historia alimentaria',
                        icon: 'success',
                        timer: 2000, // 2 segundos
                        showConfirmButton: false,
                        timerProgressBar: true
                    }).then(() => {
                        window.location.href = 'histclinutricion01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + @"&idHistoria=" + idHistoria + @"';
                    });
                    ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                //Response.Redirect("histclinutricion01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + "&idHistoria=" + idHistoria);
            }
            if (Session["idPerfil"].ToString() == "1") //OJO comentar esta condición
            {
                string script = @"
                    Swal.fire({
                        title: 'Siguiente paso...',
                        text: 'Historia alimentaria',
                        icon: 'success',
                        timer: 2000, // 2 segundos
                        showConfirmButton: false,
                        timerProgressBar: true
                    }).then(() => {
                        window.location.href = 'histclifisio01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + @"&idHistoria=" + idHistoria + @"';
                    });
                    ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ExitoMensaje", script, true);
                //Response.Redirect("histclinutricion01?idAfiliado=" + Request.QueryString["idAfiliado"].ToString() + "&idHistoria=" + idHistoria);
            }
        }
    }
}