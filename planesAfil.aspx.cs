using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;
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

                        ViewState.Add("precioBase", 0);
                        ltPrecioBase.Text = "$0";
                        ltDescuento.Text = "0%";
                        ltPrecioFinal.Text = "$0";
                        ltAhorro.Text = "$0";
                        ltConDescuento.Text = "$0";

                        //btnMes1.Attributes.Add("style", "padding: 6px 9px;");

                        CargarAfiliado();
                        CargarPlanesAfiliado();
                        MesesDisabled();
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

            string strQuery = "SELECT SinPermiso, Consulta, Exportar, CrearModificar, Borrar " +
                "FROM permisos_perfiles pp, paginas p, usuarios u " +
                "WHERE pp.idPagina = p.idPagina " +
                "AND p.Pagina = '" + strPagina + "' " +
                "AND pp.idPerfil = " + Session["idPerfil"].ToString() + " " +
                "AND u.idPerfil = pp.idPerfil " +
                "AND u.idUsuario = " + Session["idusuario"].ToString();
            clasesglobales cg = new clasesglobales();
            DataTable dt = cg.TraerDatos(strQuery);

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
                string strQuery = "SELECT * " +
                    "FROM afiliados a, sedes s " +
                    "WHERE a.idAfiliado = " + Request.QueryString["id"].ToString() + " " +
                    "AND a.idSede = s.idSede ";
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(strQuery);

                if (dt.Rows.Count > 0)
                {
                    ltNombre.Text = dt.Rows[0]["NombreAfiliado"].ToString();
                    ltApellido.Text = dt.Rows[0]["ApellidoAfiliado"].ToString();
                    ltEmail.Text = dt.Rows[0]["EmailAfiliado"].ToString();
                    ltCelular.Text = dt.Rows[0]["CelularAfiliado"].ToString();
                    ltSede.Text = dt.Rows[0]["NombreSede"].ToString();
                    ltEstado.Text = dt.Rows[0]["EstadoAfiliado"].ToString();

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

                if (dt.Rows.Count > 0)
                {
                    rpPlanesAfiliado.DataSource = dt;
                    rpPlanesAfiliado.DataBind();

                }
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
                    btn.CssClass = "btn btn-" + dt.Rows[i]["ColorPlan"].ToString() + " btn-outline btn-block btn-lg font-bold";
                    btn.ToolTip = dt.Rows[i]["NombrePlan"].ToString();
                    btn.Command += new CommandEventHandler(btn_Click);
                    //btn.CommandName = dt.Rows[i]["NombrePlan"].ToString();
                    btn.CommandArgument = dt.Rows[i]["idPlan"].ToString();
                    //btn.Click += new EventHandler(btn_Click);
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

            divPanelResumen.Attributes.Remove("class");
            divPanelResumen.Attributes.Add("class", "panel panel-" + dt.Rows[0]["ColorPlan"].ToString());

            ltPrecioBase.Text = "$" + String.Format("{0:N0}", ViewState["precioBase"]);
            ltPrecioFinal.Text = ltPrecioBase.Text;
            ActivarBotones("1");
            ActivarCortesia("0");

            ltDescuento.Text = "0%";
            ltAhorro.Text = "$0";
            ltConDescuento.Text = "$0";
            ltDescripcion.Text = "<b>Caracteristicas</b>: " + dt.Rows[0]["DescripcionPlan"].ToString() + "<br />";

            ltTituloRegalo.Text = "<b>Plan " + ViewState["nombrePlan"].ToString() + "</b>";

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

        protected void btnAgregarPlan_Click(object sender, EventArgs e)
        {
            if (ltEstado.Text.ToString() != "Activo")
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
                                "Este afiliado ya tiene un plan activo, hasta " + dt.Rows[0]["FechaFinalPlan"].ToString() +
                                "</div></div>";
                        }
                        else
                        {
                            OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
                            try
                            {
                                DateTime fechainicio = Convert.ToDateTime(txbFechaInicio.Text.ToString());
                                DateTime fechafinal = fechainicio.AddMonths(Convert.ToInt16(ViewState["meses"].ToString()));
                                strQuery = "INSERT INTO AfiliadosPlanes " +
                                    "(idAfiliado, idPlan, FechaInicioPlan, FechaFinalPlan, EstadoPlan, Meses, ObservacionesPlan) " +
                                    "VALUES (" + Request.QueryString["id"].ToString() + ", " + ViewState["idPlan"].ToString() + ", " +
                                    "'" + txbFechaInicio.Text.ToString() + "', '" + String.Format("{0:yyyy-MM-dd}", fechafinal) + "', 'Inactivo', " +
                                    "" + ViewState["meses"].ToString() + ", 'Algo') ";
                                OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                                myConnection.Open();
                                command.ExecuteNonQuery();
                                command.Dispose();
                                myConnection.Close();

                                // Enviar correo electrónico al afiliado para que pague.
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
    }
}