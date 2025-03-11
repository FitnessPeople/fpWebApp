using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class planesAfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarPlanes();
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Afiliados");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                        divContenido.Visible = false;
                    }
                    if (ViewState["CrearModificar"].ToString() == "1")
                    {
                        txbFechaInicio.Attributes.Add("type", "date");

                        DateTime dtHoy = DateTime.Now;
                        DateTime dt60 = DateTime.Now.AddMonths(2);
                        txbFechaInicio.Attributes.Add("min", dtHoy.Year.ToString() + "-" + String.Format("{0:MM}", dtHoy) + "-" + String.Format("{0:dd}", dtHoy));
                        txbFechaInicio.Attributes.Add("max", dt60.Year.ToString() + "-" + String.Format("{0:MM}", dt60) + "-" + String.Format("{0:dd}", dt60));
                        txbFechaInicio.Text = String.Format("{0:yyyy-MM-dd}", dtHoy);

                        txbWompi.Attributes.Add("type", "number");
                        txbWompi.Attributes.Add("min", "0");
                        txbWompi.Attributes.Add("max", "10000000");
                        txbWompi.Attributes.Add("step", "100");
                        txbWompi.Text = "0";

                        txbDatafono.Attributes.Add("type", "number");
                        txbDatafono.Attributes.Add("min", "0");
                        txbDatafono.Attributes.Add("max", "10000000");
                        txbDatafono.Attributes.Add("step", "100");
                        txbDatafono.Text = "0";

                        txbEfectivo.Attributes.Add("type", "number");
                        txbEfectivo.Attributes.Add("min", "0");
                        txbEfectivo.Attributes.Add("max", "10000000");
                        txbEfectivo.Attributes.Add("step", "100");
                        txbEfectivo.Text = "0";

                        txbTransferencia.Attributes.Add("type", "number");
                        txbTransferencia.Attributes.Add("min", "0");
                        txbTransferencia.Attributes.Add("max", "10000000");
                        txbTransferencia.Attributes.Add("step", "100");
                        txbTransferencia.Text = "0";

                        ViewState.Add("precioBase", 0);
                        ltPrecioBase.Text = "$0";
                        ltDescuento.Text = "0%";
                        ltPrecioFinal.Text = "$0";
                        ltAhorro.Text = "$0";
                        ltConDescuento.Text = "$0";

                        ltNombrePlan.Text = "Nombre del plan";

                        //btnMes1.Attributes.Add("style", "padding: 6px 9px;");

                        CargarAfiliado();
                        CargarPlanesAfiliado();
                        MesesDisabled();

                        string strData = listarDetalle();
                        ltDetalleWompi.Text = strData;
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

        private void MesesDisabled()
        {
            btnMes1.Enabled = false;
            btnMes2.Enabled = false;
            btnMes3.Enabled = false;
            btnMes4.Enabled = false;
            btnMes5.Enabled = false;
            btnMes6.Enabled = false;
            btnMes7.Enabled = false;
            btnMes8.Enabled = false;
            btnMes9.Enabled = false;
            btnMes10.Enabled = false;
            btnMes11.Enabled = false;
            btnMes12.Enabled = false;
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

        private void CargarAfiliado()
        {
            if (Request.QueryString.Count > 0)
            {
                string strQuery = "SELECT *, " +
                    "IF(EstadoAfiliado='Activo','info',IF(EstadoAfiliado='Inactivo','danger','warning')) AS label " +
                    "FROM afiliados a " +
                    "RIGHT JOIN Sedes s ON a.idSede = s.idSede " +
                    "LEFT JOIN ciudades ON ciudades.idCiudad = a.idCiudadAfiliado " +
                    "WHERE a.idAfiliado = " + Request.QueryString["id"].ToString() + " ";
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(strQuery);

                if (dt.Rows.Count > 0)
                {
                    ViewState["DocumentoAfiliado"] = dt.Rows[0]["DocumentoAfiliado"].ToString();
                    ltNombre.Text = dt.Rows[0]["NombreAfiliado"].ToString();
                    ltApellido.Text = dt.Rows[0]["ApellidoAfiliado"].ToString();
                    ltEmail.Text = dt.Rows[0]["EmailAfiliado"].ToString();
                    ViewState["EmailAfiliado"] = dt.Rows[0]["EmailAfiliado"].ToString();
                    ltCelular.Text = "<a href=\"https://wa.me/57" + dt.Rows[0]["CelularAfiliado"].ToString() + "\" target=\"_blank\">" + dt.Rows[0]["CelularAfiliado"].ToString() + "</a>";
                    ltSede.Text = dt.Rows[0]["NombreSede"].ToString();
                    ltDireccion.Text = dt.Rows[0]["DireccionAfiliado"].ToString();
                    ltCiudad.Text = dt.Rows[0]["NombreCiudad"].ToString();
                    ltCumple.Text = String.Format("{0:dd MMM}", Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"]));
                    ltEstado.Text = "<span class=\"label label-" + dt.Rows[0]["label"].ToString() + "\">" + dt.Rows[0]["EstadoAfiliado"].ToString() + "</span>";
                    ViewState["EstadoAfiliado"] = dt.Rows[0]["EstadoAfiliado"].ToString();

                    if (dt.Rows[0]["FechaNacAfiliado"].ToString() != "1900-01-00")
                    {
                        ltCumple.Text = String.Format("{0:dd MMM}", Convert.ToDateTime(dt.Rows[0]["FechaNacAfiliado"]));
                    }
                    else
                    {
                        ltCumple.Text = "-";
                    }

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
                dt.Dispose();
            }
        }

        private void CargarPlanesAfiliado()
        {
            if (Request.QueryString.Count > 0)
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.CargarPlanesAfiliado(Request.QueryString["id"].ToString(), "Activo");

                //string strQuery = "SELECT *, " +
                //    "DATEDIFF(FechaFinalPlan, CURDATE()) AS diasquefaltan, " +
                //    "DATEDIFF(CURDATE(), FechaInicioPlan) AS diasconsumidos, " +
                //    "DATEDIFF(FechaFinalPlan, FechaInicioPlan) AS diastotales, " +
                //    "ROUND(DATEDIFF(CURDATE(), FechaInicioPlan) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje1, " +
                //    "ROUND(DATEDIFF(FechaFinalPlan, CURDATE()) / DATEDIFF(FechaFinalPlan, FechaInicioPlan) * 100) AS Porcentaje2 " +
                //    "FROM afiliadosPlanes ap, Afiliados a, Planes p " +
                //    "WHERE a.idAfiliado = " + Request.QueryString["id"].ToString() + " " +
                //    "AND ap.idAfiliado = a.idAfiliado " +
                //    "AND ap.idPlan = p.idPlan " +
                //    "AND ap.EstadoPlan = 'Activo'";
                //clasesglobales cg = new clasesglobales();
                //DataTable dt = cg.TraerDatos(strQuery);

                //if (dt.Rows.Count > 0)
                //{
                    rpPlanesAfiliado.DataSource = dt;
                    rpPlanesAfiliado.DataBind();
                //}
                dt.Dispose();
            }
        }

        private void CargarPlanes()
        {
            string strQuery = "SELECT * " +
                "FROM Planes " +
                "WHERE EstadoPlan = 'Activo' " +
                "AND (FechaInicial IS NULL OR FechaInicial <= CURDATE()) " +
                "AND (FechaFinal IS NULL OR FechaFinal >= CURDATE())";
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                PlaceHolder ph = ((PlaceHolder)this.FindControl("phPlanes"));
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Button btn = new Button();
                    btn.Text = dt.Rows[i]["NombrePlan"].ToString();
                    btn.CssClass = "btn btn-" + dt.Rows[i]["NombreColorPlan"].ToString() + " btn-outline btn-block btn-lg font-bold";
                    btn.ToolTip = dt.Rows[i]["NombrePlan"].ToString();
                    btn.Command += new CommandEventHandler(btn_Click);
                    btn.CommandArgument = dt.Rows[i]["idPlan"].ToString();
                    btn.ID = dt.Rows[i]["idPlan"].ToString();
                    ph.Controls.Add(btn);
                }
            }
            dt.Dispose();
        }

        private void btn_Click(object sender, CommandEventArgs e)
        {
            string strQuery = "SELECT * " +
                "FROM planes " +
                "WHERE idPlan = " + e.CommandArgument;
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

            ViewState["idPlan"] = dt.Rows[0]["idPlan"].ToString();
            ViewState["nombrePlan"] = dt.Rows[0]["NombrePlan"].ToString();
            ViewState["precioBase"] = Convert.ToInt32(dt.Rows[0]["PrecioBase"].ToString());
            ViewState["descuentoMensual"] = Convert.ToDouble(dt.Rows[0]["DescuentoMensual"].ToString());
            ViewState["mesesMaximo"] = Convert.ToDouble(dt.Rows[0]["MesesMaximo"].ToString());

            //divPanelResumen.Attributes.Remove("class");
            //divPanelResumen.Attributes.Add("class", "panel panel-" + dt.Rows[0]["NombreColorPlan"].ToString());

            ltPrecioBase.Text = "$" + String.Format("{0:N0}", ViewState["precioBase"]);
            ltPrecioFinal.Text = ltPrecioBase.Text;

            CalculoPrecios("1");
            ActivarBotones("1");
            ActivarCortesia("0");

            ltDescuento.Text = "0%";
            ltAhorro.Text = "$0";
            ltConDescuento.Text = "$0";
            ltDescripcion.Text = "<b>Características</b>: " + dt.Rows[0]["DescripcionPlan"].ToString() + "<br />";

            ltNombrePlan.Text = "<b>Plan " + ViewState["nombrePlan"].ToString() + "</b>";

            MesesEnabled();
        }

        private void MesesEnabled()
        {
            MesesDisabled();
            if (Convert.ToInt32(btnMes1.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
            {
                btnMes1.Enabled = true;
            }
            if (Convert.ToInt32(btnMes2.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
            {
                btnMes2.Enabled = true;
            }
            if (Convert.ToInt32(btnMes3.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
            {
                btnMes3.Enabled = true;
            }
            if (Convert.ToInt32(btnMes4.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
            {
                btnMes4.Enabled = true;
            }
            if (Convert.ToInt32(btnMes5.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
            {
                btnMes5.Enabled = true;
            }
            if (Convert.ToInt32(btnMes6.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
            {
                btnMes6.Enabled = true;
            }
            if (Convert.ToInt32(btnMes7.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
            {
                btnMes7.Enabled = true;
            }
            if (Convert.ToInt32(btnMes8.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
            {
                btnMes8.Enabled = true;
            }
            if (Convert.ToInt32(btnMes9.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
            {
                btnMes9.Enabled = true;
            }
            if (Convert.ToInt32(btnMes10.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
            {
                btnMes10.Enabled = true;
            }
            if (Convert.ToInt32(btnMes11.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
            {
                btnMes11.Enabled = true;
            }
            if (Convert.ToInt32(btnMes12.Text.ToString()) <= Convert.ToInt32(ViewState["mesesMaximo"].ToString()))
            {
                btnMes12.Enabled = true;
            }

        }

        //protected void btnAgregarPlan_Click(object sender, EventArgs e)
        //{
        //    if (ltEstado.Text.ToString() != "Activo")
        //    {
        //        ltMensaje.Text = "<div class=\"ibox-content\">" +
        //            "<div class=\"alert alert-danger alert-dismissable\">" +
        //            "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
        //            "El afiliado no se encuentra activo." +
        //            "</div></div>";
        //    }
        //    else
        //    {
        //        if (ViewState["nombrePlan"] != null)
        //        {
        //            if (txbFechaInicio.Text != "")
        //            {
        //                // Consultar si este usuario tiene un plan activo y cual es su fecha de inicio y fecha final.
        //                string strQuery = "SELECT * FROM AfiliadosPlanes " +
        //                    "WHERE idAfiliado = " + Request.QueryString["id"].ToString() + " " +
        //                    "AND EstadoPlan = 'Activo'";
        //                clasesglobales cg = new clasesglobales();
        //                DataTable dt = cg.TraerDatos(strQuery);
        //                if (dt.Rows.Count > 0)
        //                {
        //                    ltMensaje.Text = "<div class=\"ibox-content\">" +
        //                        "<div class=\"alert alert-danger alert-dismissable\">" +
        //                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
        //                        "Este afiliado ya tiene un plan activo, hasta " + dt.Rows[0]["FechaFinalPlan"].ToString() +
        //                        "</div></div>";
        //                }
        //                else
        //                {
        //                    try
        //                    {
        //                        DateTime fechainicio = Convert.ToDateTime(txbFechaInicio.Text.ToString());
        //                        DateTime fechafinal = fechainicio.AddMonths(Convert.ToInt16(ViewState["meses"].ToString()));
        //                        strQuery = "INSERT INTO AfiliadosPlanes " +
        //                            "(idAfiliado, idPlan, FechaInicioPlan, FechaFinalPlan, EstadoPlan, Meses, Valor, ObservacionesPlan) " +
        //                            "VALUES (" + Request.QueryString["id"].ToString() + ", " + ViewState["idPlan"].ToString() + ", " +
        //                            "'" + txbFechaInicio.Text.ToString() + "', '" + String.Format("{0:yyyy-MM-dd}", fechafinal) + "', 'Inactivo', " +
        //                            "" + ViewState["meses"].ToString() + ", " + ViewState["precio"].ToString() + ",  " +
        //                            "'" + ViewState["observaciones"].ToString() + "') ";

        //                        try
        //                        {
        //                            string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

        //                            using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
        //                            {
        //                                mysqlConexion.Open();
        //                                using (MySqlCommand cmd = new MySqlCommand(strQuery, mysqlConexion))
        //                                {
        //                                    cmd.CommandType = CommandType.Text;
        //                                    cmd.ExecuteNonQuery();
        //                                }
        //                                mysqlConexion.Close();
        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            string respuesta = "ERROR: " + ex.Message;
        //                        }

        //                        string strString = Convert.ToBase64String(Encoding.Unicode.GetBytes(ViewState["DocumentoAfiliado"].ToString() + "_" + ViewState["precio"].ToString()));

        //                        string strMensaje = "Se ha creado un Plan para ud. en Fitness People \r\n\r\n";
        //                        strMensaje += "Descripción del plan.\r\n\r\n";
        //                        strMensaje += "Por favor, agradecemos realice el pago a través del siguiente enlace: \r\n";
        //                        strMensaje += "https://fitnesspeoplecolombia.com/wompiplan?code=" + strString;

        //                        cg.EnviarCorreo("afiliaciones@fitnesspeoplecolombia.com", ViewState["EmailAfiliado"].ToString(), "Plan Fitness People", strMensaje);

        //                        // Enviar correo electrónico al afiliado para que pague.
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        ltMensaje.Text = "<div class=\"ibox-content\">" +
        //                        "<div class=\"alert alert-danger alert-dismissable\">" +
        //                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" + ex.Message.ToString() +
        //                        "</div></div>";
        //                        throw;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                ltMensaje.Text = "<div class=\"ibox-content\">" +
        //                    "<div class=\"alert alert-danger alert-dismissable\">" +
        //                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
        //                    "Elija la fecha de inicio del plan." +
        //                    "</div></div>";
        //            }
        //        }
        //        else
        //        {
        //            ltMensaje.Text = "<div class=\"ibox-content\">" +
        //                "<div class=\"alert alert-danger alert-dismissable\">" +
        //                "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
        //                "Elija el tipo de plan." +
        //                "</div></div>";
        //        }
        //    }
        //}

        private void LimpiarFormulario()
        {
            ltCortesias.Text = "";
            ltRegalos.Text = "";
            btn7dias.CssClass += btn7dias.CssClass.Replace("active", "");
            btn15dias.CssClass = btn15dias.CssClass.Replace("active", "");
            btn30dias.CssClass = btn30dias.CssClass.Replace("active", "");
            btn60dias.CssClass = btn60dias.CssClass.Replace("active", "");
            btnRegalo1.CssClass = btnRegalo1.CssClass.Replace("active", "");
            btnRegalo2.CssClass = btnRegalo2.CssClass.Replace("active", "");
            btnRegalo3.CssClass = btnRegalo3.CssClass.Replace("active", "");
        }

        protected void btnMes1_Click(object sender, EventArgs e)
        {
            CalculoPrecios(btnMes1.Text.ToString());
            ActivarBotones(btnMes1.Text.ToString());
            ActivarCortesia("1");
            ActivarRegalo("0");
            LimpiarFormulario();
        }

        protected void btnMes2_Click(object sender, EventArgs e)
        {
            CalculoPrecios(btnMes2.Text.ToString());
            ActivarBotones(btnMes2.Text.ToString());
            ActivarCortesia("1");
            ActivarRegalo("0");
            LimpiarFormulario();
        }

        protected void btnMes3_Click(object sender, EventArgs e)
        {
            CalculoPrecios(btnMes3.Text.ToString());
            ActivarBotones(btnMes3.Text.ToString());
            ActivarCortesia("2");
            ActivarRegalo("0");
            LimpiarFormulario();
        }

        protected void btnMes4_Click(object sender, EventArgs e)
        {
            CalculoPrecios(btnMes4.Text.ToString());
            ActivarBotones(btnMes4.Text.ToString());
            ActivarCortesia("3");
            ActivarRegalo("1");
            LimpiarFormulario();
        }

        protected void btnMes5_Click(object sender, EventArgs e)
        {
            CalculoPrecios(btnMes5.Text.ToString());
            ActivarBotones(btnMes5.Text.ToString());
            ActivarCortesia("3");
            ActivarRegalo("1");
            LimpiarFormulario();
        }

        protected void btnMes6_Click(object sender, EventArgs e)
        {
            CalculoPrecios(btnMes6.Text.ToString());
            ActivarBotones(btnMes6.Text.ToString());
            ActivarCortesia("3");
            ActivarRegalo("1");
            LimpiarFormulario();
        }

        protected void btnMes7_Click(object sender, EventArgs e)
        {
            CalculoPrecios(btnMes7.Text.ToString());
            ActivarBotones(btnMes7.Text.ToString());
            ActivarCortesia("3");
            ActivarRegalo("1");
            LimpiarFormulario();
        }

        protected void btnMes8_Click(object sender, EventArgs e)
        {
            CalculoPrecios(btnMes8.Text.ToString());
            ActivarBotones(btnMes8.Text.ToString());
            ActivarCortesia("3");
            ActivarRegalo("2");
            LimpiarFormulario();
        }

        protected void btnMes9_Click(object sender, EventArgs e)
        {
            CalculoPrecios(btnMes9.Text.ToString());
            ActivarBotones(btnMes9.Text.ToString());
            ActivarCortesia("3");
            ActivarRegalo("2");
            LimpiarFormulario();
        }

        protected void btnMes10_Click(object sender, EventArgs e)
        {
            CalculoPrecios(btnMes10.Text.ToString());
            ActivarBotones(btnMes10.Text.ToString());
            ActivarCortesia("4");
            ActivarRegalo("2");
            LimpiarFormulario();
        }

        protected void btnMes11_Click(object sender, EventArgs e)
        {
            CalculoPrecios(btnMes11.Text.ToString());
            ActivarBotones(btnMes11.Text.ToString());
            ActivarCortesia("4");
            ActivarRegalo("2");
            LimpiarFormulario();
        }

        protected void btnMes12_Click(object sender, EventArgs e)
        {
            CalculoPrecios(btnMes12.Text.ToString());
            ActivarBotones(btnMes12.Text.ToString());
            ActivarCortesia("4");
            ActivarRegalo("3");
            LimpiarFormulario();
        }

        private void CalculoPrecios(string strMes)
        {
            int intPrecioBase = Convert.ToInt32(ViewState["precioBase"]);
            double dobDescuento = (Convert.ToInt32(strMes) - 1) * Convert.ToDouble(ViewState["descuentoMensual"]);
            int intMeses = Convert.ToInt32(strMes);
            ViewState["meses"] = intMeses;
            double dobTotal = (intPrecioBase - ((intPrecioBase * dobDescuento) / 100)) * intMeses;
            ViewState["precio"] = Convert.ToString(Convert.ToInt32(dobTotal));
            double dobAhorro = ((intPrecioBase * dobDescuento) / 100) * intMeses;
            double dobConDescuento = (intPrecioBase - ((intPrecioBase * dobDescuento) / 100));

            ltPrecioBase.Text = "$" + String.Format("{0:N0}", intPrecioBase);
            ltDescuento.Text = dobDescuento.ToString() + "%";
            //ltPrecioFinal.Text = String.Format("{0:C0}", intTotal);
            ltPrecioFinal.Text = "$" + String.Format("{0:N0}", dobTotal);
            //ltAhorro.Text = String.Format("{0:C0}", intAhorro);
            ltAhorro.Text = "$" + String.Format("{0:N0}", dobAhorro);
            //ltConDescuento.Text = String.Format("{0:C0}", intConDescuento);
            ltConDescuento.Text = "$" + String.Format("{0:N0}", dobConDescuento);

            ltObservaciones.Text = "Valor sin descuento: $" + string.Format("{0:N0}", intPrecioBase) + "<br /><br />";
            ltObservaciones.Text += "<b>Meses</b>: " + intMeses.ToString() + ".<br />";
            ltObservaciones.Text += "<b>Descuento</b>: " + dobDescuento.ToString() + "%.<br />";
            ltObservaciones.Text += "<b>Valor del mes con descuento</b>: $" + string.Format("{0:N0}", dobConDescuento) + ".<br />";
            ltObservaciones.Text += "<b>Ahorro</b>: $" + string.Format("{0:N0}", dobAhorro) + ".<br />";
            ltObservaciones.Text += "<b>Valor Total</b>: $" + string.Format("{0:N0}", dobTotal) + ".<br />";

            ViewState["observaciones"] = ltObservaciones.Text.ToString().Replace("<b>","").Replace("</b>", "").Replace("<br />", "\r\n");
            ltValorTotal.Text = "($" + string.Format("{0:N0}", dobTotal) + ")";

            string strDataWompi = Convert.ToBase64String(Encoding.Unicode.GetBytes(ViewState["DocumentoAfiliado"].ToString() + "_" + ViewState["precio"].ToString()));
            //lbEnlaceWompi.Text = "https://fitnesspeoplecolombia.com/wompiplan?code=" + strDataWompi;
            lbEnlaceWompi.Text = "<b>Enlace de pago Wompi:</b> <br />";
            lbEnlaceWompi.Text += MakeTinyUrl("https://fitnesspeoplecolombia.com/wompiplan?code=" + strDataWompi);
            hdEnlaceWompi.Value = MakeTinyUrl("https://fitnesspeoplecolombia.com/wompiplan?code=" + strDataWompi);
            btnPortapaleles.Visible = true;
        }

        public static string MakeTinyUrl(string url)
        {
            try
            {
                if (url.Length <= 30)
                {
                    return url;
                }
                
                var request = WebRequest.Create("http://tinyurl.com/api-create.php?url=" + url);
                var res = request.GetResponse();
                string text;
                using (var reader = new StreamReader(res.GetResponseStream()))
                {
                    text = reader.ReadToEnd();
                }
                return text;
            }
            catch (Exception)
            {
                return url;
            }
        }

        private void ActivarCortesia(string strCortesia)
        {
            switch (strCortesia)
            {
                case "0":
                    btn7dias.Enabled = false;
                    btn15dias.Enabled = false;
                    btn30dias.Enabled = false;
                    btn60dias.Enabled = false;
                    break;
                case "1":
                    btn7dias.Enabled = true;
                    btn15dias.Enabled = false;
                    btn30dias.Enabled = false;
                    btn60dias.Enabled = false;
                    break;
                case "2":
                    btn7dias.Enabled = true;
                    btn15dias.Enabled = true;
                    btn30dias.Enabled = false;
                    btn60dias.Enabled = false;
                    break;
                case "3":
                    btn7dias.Enabled = true;
                    btn15dias.Enabled = true;
                    btn30dias.Enabled = true;
                    btn60dias.Enabled = false;
                    break;
                case "4":
                    btn7dias.Enabled = true;
                    btn15dias.Enabled = true;
                    btn30dias.Enabled = true;
                    btn60dias.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        private void ActivarRegalo(string strRegalo)
        {
            switch (strRegalo)
            {
                case "0":
                    btnRegalo1.CssClass += " disabled";
                    btnRegalo2.CssClass += " disabled";
                    btnRegalo3.CssClass += " disabled";
                    break;
                case "1":
                    btnRegalo1.CssClass = btnRegalo1.CssClass.Replace("disabled", "");
                    btnRegalo2.CssClass += " disabled";
                    btnRegalo3.CssClass += " disabled";
                    break;
                case "2":
                    btnRegalo1.CssClass = btnRegalo1.CssClass.Replace("disabled", "");
                    btnRegalo2.CssClass = btnRegalo2.CssClass.Replace("disabled", "");
                    btnRegalo3.CssClass += " disabled";
                    break;
                case "3":
                    btnRegalo1.CssClass = btnRegalo1.CssClass.Replace("disabled", "");
                    btnRegalo2.CssClass = btnRegalo2.CssClass.Replace("disabled", "");
                    btnRegalo3.CssClass = btnRegalo3.CssClass.Replace("disabled", "");
                    break;
                default:
                    break;
            }
        }

        private void ActivarBotones(string strMes)
        {
            btnMes1.CssClass = btnMes1.CssClass.Replace("active", "");
            btnMes2.CssClass = btnMes2.CssClass.Replace("active", "");
            btnMes3.CssClass = btnMes3.CssClass.Replace("active", "");
            btnMes4.CssClass = btnMes4.CssClass.Replace("active", "");
            btnMes5.CssClass = btnMes5.CssClass.Replace("active", "");
            btnMes6.CssClass = btnMes6.CssClass.Replace("active", "");
            btnMes7.CssClass = btnMes7.CssClass.Replace("active", "");
            btnMes8.CssClass = btnMes8.CssClass.Replace("active", "");
            btnMes9.CssClass = btnMes9.CssClass.Replace("active", "");
            btnMes10.CssClass = btnMes10.CssClass.Replace("active", "");
            btnMes11.CssClass = btnMes11.CssClass.Replace("active", "");
            btnMes12.CssClass = btnMes12.CssClass.Replace("active", "");

            switch (strMes)
            {
                case "1":
                    btnMes1.CssClass += " active";
                    break;
                case "2":
                    btnMes2.CssClass += " active";
                    break;
                case "3":
                    btnMes3.CssClass += " active";
                    break;
                case "4":
                    btnMes4.CssClass += " active";
                    break;
                case "5":
                    btnMes5.CssClass += " active";
                    break;
                case "6":
                    btnMes6.CssClass += " active";
                    break;
                case "7":
                    btnMes7.CssClass += " active";
                    break;
                case "8":
                    btnMes8.CssClass += " active";
                    break;
                case "9":
                    btnMes9.CssClass += " active";
                    break;
                case "10":
                    btnMes10.CssClass += " active";
                    break;
                case "11":
                    btnMes11.CssClass += " active";
                    break;
                case "12":
                    btnMes12.CssClass += " active";
                    break;
                default:
                    break;
            }
        }

        protected void btn7dias_Click(object sender, EventArgs e)
        {
            btn7dias.CssClass += " active";
            btn15dias.CssClass = btn15dias.CssClass.Replace("active", "");
            btn30dias.CssClass = btn30dias.CssClass.Replace("active", "");
            btn60dias.CssClass = btn60dias.CssClass.Replace("active", "");
            ltCortesias.Text = "<b>Cortesía: </b>7 días adicionales al plan.<br />";
        }

        protected void btn15dias_Click(object sender, EventArgs e)
        {
            btn15dias.CssClass += " active";
            btn7dias.CssClass = btn7dias.CssClass.Replace("active", "");
            btn30dias.CssClass = btn30dias.CssClass.Replace("active", "");
            btn60dias.CssClass = btn60dias.CssClass.Replace("active", "");
            ltCortesias.Text = "<b>Cortesía: </b>15 días adicionales al plan.<br />";
        }

        protected void btn30dias_Click(object sender, EventArgs e)
        {
            btn30dias.CssClass += " active";
            btn7dias.CssClass = btn7dias.CssClass.Replace("active", "");
            btn15dias.CssClass = btn15dias.CssClass.Replace("active", "");
            btn60dias.CssClass = btn60dias.CssClass.Replace("active", "");
            ltCortesias.Text = "<b>Cortesía: </b>30 días adicionales al plan.<br />";
        }

        protected void btn60dias_Click(object sender, EventArgs e)
        {
            btn60dias.CssClass += " active";
            btn7dias.CssClass = btn7dias.CssClass.Replace("active", "");
            btn15dias.CssClass = btn15dias.CssClass.Replace("active", "");
            btn30dias.CssClass = btn30dias.CssClass.Replace("active", "");
            ltCortesias.Text = "<b>Cortesía: </b>60 días adicionales al plan.<br />";
        }

        protected void btnRegalo1_Click(object sender, EventArgs e)
        {
            btnRegalo1.CssClass += " active";
            btnRegalo2.CssClass = btnRegalo2.CssClass.Replace("active", "");
            btnRegalo3.CssClass = btnRegalo3.CssClass.Replace("active", "");
            ltRegalos.Text = "<b>Regalo: </b>Termo Fitness People.<br />";
        }

        protected void btnRegalo2_Click(object sender, EventArgs e)
        {
            btnRegalo2.CssClass += " active";
            btnRegalo1.CssClass = btnRegalo1.CssClass.Replace("active", "");
            btnRegalo3.CssClass = btnRegalo3.CssClass.Replace("active", "");
            ltRegalos.Text = "<b>Regalo: </b>Camiseta Fitness People.<br />";
        }

        protected void btnRegalo3_Click(object sender, EventArgs e)
        {
            btnRegalo3.CssClass += " active";
            btnRegalo1.CssClass = btnRegalo1.CssClass.Replace("active", "");
            btnRegalo2.CssClass = btnRegalo2.CssClass.Replace("active", "");
            ltRegalos.Text = "<b>Regalo: </b>Cita con nutricionista.<br />";
        }

        protected void rpPlanesAfiliado_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater rptPlanes = sender as Repeater; // Get the Repeater control object.

            // If the Repeater contains no data.
            if (rpPlanesAfiliado.Items.Count < 1)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    // Show the Error Label (if no data is present).
                    Label lblSinRegistros = e.Item.FindControl("lblSinRegistros") as Label;
                    if (lblSinRegistros != null)
                    {
                        lblSinRegistros.Visible = true;
                    }
                }
            }
        }

        protected void lbAgregarPlan_Click(object sender, EventArgs e)
        {
            if (ViewState["EstadoAfiliado"].ToString() != "Activo")
            {
                ltMensaje.Text = "<div class=\"ibox-content\">" +
                    "<div class=\"alert alert-danger alert-dismissable\">" +
                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                    "El afiliado no se encuentra activo." +
                    "</div></div>";
            }
            else
            {
                if (ViewState["nombrePlan"] != null)
                {
                    if (txbFechaInicio.Text != "")
                    {
                        // Consultar si este usuario tiene un plan activo y cual es su fecha de inicio y fecha final.
                        string strQuery = "SELECT * FROM AfiliadosPlanes " +
                            "WHERE idAfiliado = " + Request.QueryString["id"].ToString() + " " +
                            "AND EstadoPlan = 'Activo'";
                        clasesglobales cg = new clasesglobales();
                        DataTable dt = cg.TraerDatos(strQuery);
                        if (dt.Rows.Count > 0)
                        {
                            ltMensaje.Text = "<div class=\"ibox-content\">" +
                                "<div class=\"alert alert-danger alert-dismissable\">" +
                                "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                "Este afiliado ya tiene un plan activo, hasta el " + string.Format("{0:dd MMM yyyy}", dt.Rows[0]["FechaFinalPlan"]) +
                                "</div></div>";
                        }
                        else
                        {
                            if (ViewState["precio"].ToString() != txbTotal.Text.ToString())
                            {
                                ltMensaje.Text = "<div class=\"ibox-content\">" +
                                "<div class=\"alert alert-danger alert-dismissable\">" +
                                "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                                "El precio del plan es diferente al precio a pagar." +
                                "</div></div>";
                            }
                            else
                            {
                                try
                                {
                                    DateTime fechainicio = Convert.ToDateTime(txbFechaInicio.Text.ToString());
                                    DateTime fechafinal = fechainicio.AddMonths(Convert.ToInt16(ViewState["meses"].ToString()));
                                    strQuery = "INSERT INTO AfiliadosPlanes " +
                                        "(idAfiliado, idPlan, FechaInicioPlan, FechaFinalPlan, EstadoPlan, Meses, Valor, ObservacionesPlan) " +
                                        "VALUES (" + Request.QueryString["id"].ToString() + ", " + ViewState["idPlan"].ToString() + ", " +
                                        "'" + txbFechaInicio.Text.ToString() + "', '" + String.Format("{0:yyyy-MM-dd}", fechafinal) + "', 'Inactivo', " +
                                        "" + ViewState["meses"].ToString() + ", " + ViewState["precio"].ToString() + ",  " +
                                        "'" + ViewState["observaciones"].ToString() + "') ";

                                    try
                                    {
                                        string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                                        using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                                        {
                                            mysqlConexion.Open();
                                            using (MySqlCommand cmd = new MySqlCommand(strQuery, mysqlConexion))
                                            {
                                                cmd.CommandType = CommandType.Text;
                                                //cmd.ExecuteNonQuery();
                                            }
                                            mysqlConexion.Close();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string respuesta = "ERROR: " + ex.Message;
                                    }

                                    //if (txbWompi.Text.ToString() != "0")
                                    //{
                                    //    string strString = Convert.ToBase64String(Encoding.Unicode.GetBytes(ViewState["DocumentoAfiliado"].ToString() + "_" + ViewState["precio"].ToString()));

                                    //    string strMensaje = "Se ha creado un Plan para ud. en Fitness People \r\n\r\n";
                                    //    strMensaje += "Descripción del plan.\r\n\r\n";
                                    //    strMensaje += "Por favor, agradecemos realice el pago a través del siguiente enlace: \r\n";
                                    //    strMensaje += "https://fitnesspeoplecolombia.com/wompiplan?code=" + strString;

                                    //    cg.EnviarCorreo("afiliaciones@fitnesspeoplecolombia.com", ViewState["EmailAfiliado"].ToString(), "Plan Fitness People", strMensaje);
                                    //}

                                    strQuery = "SELECT * FROM AfiliadosPlanes ORDER BY idAfiliadoPlan DESC LIMIT 1";
                                    DataTable dt1 = cg.TraerDatos(strQuery);

                                    string strTipoPago = string.Empty;
                                    string strReferencia = string.Empty;
                                    string strBanco = string.Empty;

                                    if (txbWompi.Text.ToString() != "0")
                                    {
                                        strTipoPago = "Wompi";
                                    }
                                    if (txbDatafono.Text.ToString() != "0")
                                    {
                                        strTipoPago = "Datafono";
                                        strReferencia = txbNroAprobacion.Text.ToString();
                                    }
                                    if (txbTransferencia.Text.ToString() != "0")
                                    {
                                        strTipoPago = "Transferencia";
                                    }
                                    if (txbEfectivo.Text.ToString() != "0")
                                    {
                                        strTipoPago = "Efectivo";
                                    }

                                    if (rblBancos.SelectedItem == null)
                                    {
                                        strBanco = "(NULL)";
                                    }
                                    else
                                    {
                                        strBanco = rblBancos.SelectedItem.Value.ToString();
                                    }

                                    strQuery = "INSERT INTO PagosPlanAfiliado (idAfiliadoPlan, Valor, TipoPago, idReferencia, Banco, FechaHoraPago) " +
                                    "VALUES (" + dt1.Rows[0]["idAfiliadoPlan"].ToString() + ", " +
                                    "" + txbTotal.Text.ToString() + ", " +
                                    "'" + strTipoPago + "', " +
                                    "'" + strReferencia + "', " +
                                    "'" + strBanco + "', " +
                                    "NOW()) ";

                                    try
                                    {
                                        string strConexion = WebConfigurationManager.ConnectionStrings["ConnectionFP"].ConnectionString;

                                        using (MySqlConnection mysqlConexion = new MySqlConnection(strConexion))
                                        {
                                            mysqlConexion.Open();
                                            using (MySqlCommand cmd = new MySqlCommand(strQuery, mysqlConexion))
                                            {
                                                cmd.CommandType = CommandType.Text;
                                                //cmd.ExecuteNonQuery();
                                            }
                                            mysqlConexion.Close();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string respuesta = "ERROR: " + ex.Message;
                                    }

                                }
                                catch (Exception ex)
                                {
                                    ltMensaje.Text = "<div class=\"ibox-content\">" +
                                    "<div class=\"alert alert-danger alert-dismissable\">" +
                                    "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" + ex.Message.ToString() +
                                    "</div></div>";
                                    throw;
                                }
                            }
                        }
                        dt.Dispose();
                    }
                    else
                    {
                        ltMensaje.Text = "<div class=\"ibox-content\">" +
                            "<div class=\"alert alert-danger alert-dismissable\">" +
                            "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                            "Elija la fecha de inicio del plan." +
                            "</div></div>";
                    }
                }
                else
                {
                    ltMensaje.Text = "<div class=\"ibox-content\">" +
                        "<div class=\"alert alert-danger alert-dismissable\">" +
                        "<button aria-hidden=\"true\" data-dismiss=\"alert\" class=\"close\" type=\"button\">×</button>" +
                        "Elija el tipo de plan." +
                        "</div></div>";
                }
            }
        }

        protected void txbWompi_TextChanged(object sender, EventArgs e)
        {
            if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
            {
                txbTotal.Text = Convert.ToString(Convert.ToInt32(txbWompi.Text) + Convert.ToInt32(txbDatafono.Text) + Convert.ToInt32(txbEfectivo.Text) + Convert.ToInt32(txbTransferencia.Text));
            }
        }

        protected void txbDatafono_TextChanged(object sender, EventArgs e)
        {
            if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
            {
                txbTotal.Text = Convert.ToString(Convert.ToInt32(txbWompi.Text) + Convert.ToInt32(txbDatafono.Text) + Convert.ToInt32(txbEfectivo.Text) + Convert.ToInt32(txbTransferencia.Text));
            }
        }

        protected void txbEfectivo_TextChanged(object sender, EventArgs e)
        {
            if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
            {
                txbTotal.Text = Convert.ToString(Convert.ToInt32(txbWompi.Text) + Convert.ToInt32(txbDatafono.Text) + Convert.ToInt32(txbEfectivo.Text) + Convert.ToInt32(txbTransferencia.Text));
            }
        }

        protected void txbTransferencia_TextChanged(object sender, EventArgs e)
        {
            if (txbWompi.Text != "" && txbDatafono.Text != "" && txbEfectivo.Text != "" && txbTransferencia.Text != "")
            {
                txbTotal.Text = Convert.ToString(Convert.ToInt32(txbWompi.Text) + Convert.ToInt32(txbDatafono.Text) + Convert.ToInt32(txbEfectivo.Text) + Convert.ToInt32(txbTransferencia.Text));
            }
        }

        private string listarDetalle()
        {
            string parametro = string.Empty;
            string tester = string.Empty;
            string mensaje = string.Empty;
            clasesglobales cg = new clasesglobales();
            DataTable dti = cg.ConsultarUrl(4);

            string strFechaHoy = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            parametro = "?from_date=2025-01-01&until_date=2025-03-11&page=1&page_size=50&order_by=created_at&order=DESC";
            //parametro = "?from_date=" + strFechaHoy + "&until_date=" + strFechaHoy + "&page=1&page_size=10&order_by=created_at&order=DESC";

            string url = dti.Rows[0]["urlTest"].ToString() + parametro;
            string[] respuesta = cg.EnviarPeticionGet(url, out mensaje);
            JToken token = JToken.Parse(respuesta[0]);
            string prettyJson = token.ToString(Formatting.Indented);

            if (mensaje == "Ok") //Verifica respuesta ok
            {
                JObject jsonData = JObject.Parse(prettyJson);

                List<Datum> listaDatos = new List<Datum>();

                foreach (var item in jsonData["data"])
                {
                    listaDatos.Add(new Datum
                    {
                        //id = item["id"]?.ToString(),
                        created_at = item["created_at"]?.ToString(),
                        //finalized_at = item["finalized_at"]?.ToString(),
                        amount_in_cents = ((item["amount_in_cents"]?.Value<int>() ?? 0) / 100).ToString("N0"),
                        reference = item["reference"]?.ToString(),
                        customer_email = item["customer_email"]?.ToString(),
                        currency = item["currency"]?.ToString(),
                        payment_method_type = item["payment_method_type"]?.ToString(),
                        status = item["status"]?.ToString(),
                        status_message = item["status_message"]?.ToString(),
                        device_id = item["customer_data"]?["device_id"]?.ToString(),
                        full_name = item["customer_data"]?["full_name"]?.ToString(),
                        phone_number = item["customer_data"]?["phone_number"]?.ToString()
                    });
                }

                StringBuilder sb = new StringBuilder();

                sb.Append("<table class=\"table table-bordered table-striped\">");
                sb.Append("<tr>");
                sb.Append("<th>Afiliado</th><th>Teléfono</th><th>Fecha creación</th><th>Valor</th>");
                sb.Append("<th>Método pago</th><th>Estado</th><th>Referencia</th>");
                sb.Append("</tr>");

                foreach (var pago in listaDatos)
                {
                    string strStatus = string.Empty;
                    sb.Append("<tr>");
                    sb.Append($"<td>{pago.full_name}</td>");
                    sb.Append($"<td>{pago.phone_number}</td>");
                    sb.Append($"<td>" + String.Format("{0:dd MMM yyyy HH:mm}", Convert.ToDateTime(pago.created_at)) + "</td>");
                    sb.Append($"<td>{pago.amount_in_cents}</td>");
                    sb.Append($"<td>{pago.payment_method_type}</td>");

                    if (pago.status.ToString() == "APPROVED")
                    {
                        strStatus = "<span class=\"badge badge-info\">Aprobado</span>";
                    }
                    else
                    {
                        strStatus = "<span class=\"badge badge-danger\">Rechazado</span>";
                    }

                    sb.Append($"<td>" + strStatus + "</td>");
                    sb.Append($"<td>{pago.reference}</td>");
                    sb.Append("</tr>");
                }

                sb.Append("</table>");

                return sb.ToString();

            }
            else
            {
                return prettyJson;
            }
        }

        public class Datum
        {
            public string id { get; set; }
            public string created_at { get; set; }
            public string finalized_at { get; set; }
            public string amount_in_cents { get; set; }
            public string reference { get; set; }
            public string customer_email { get; set; }
            public string currency { get; set; }
            public string payment_method_type { get; set; }
            public string status { get; set; }
            public string status_message { get; set; }
            public string device_id { get; set; }
            public string full_name { get; set; }
            public string phone_number { get; set; }
        }

    }
}