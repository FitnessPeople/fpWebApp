using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class nuevahistoriaclinica : System.Web.UI.Page
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
                            txbFum.Attributes.Add("type", "date");
                            txbCigarrillos.Attributes.Add("type", "number");
                            txbBebidas.Attributes.Add("type", "number");
                            CargarObjetivos();
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

        private void CargarObjetivos()
        {
            string strQuery = "SELECT * FROM ObjetivosAfiliado";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            ddlObjetivo.DataSource = dt;
            ddlObjetivo.DataBind();

            dt.Dispose();
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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            OdbcConnection myConnection = new OdbcConnection(ConfigurationManager.AppSettings["sConn"].ToString());
            try
            {
                string strQuery = "INSERT INTO HistoriasClinicas " +
                "(idAfiliado, FechaHora, MedicinaPrepagada, idObjetivoIngreso, DescripcionObjetivoIngreso, AnteFamiliar, AntePatologico, " +
                "AnteQuirurgico, AnteToxicologico, AnteHospitalario, AnteTraumatologico, AnteFarmacologico, AnteActividadFisica, AnteGineco, " +
                "AnteFUM, Tabaquismo, Cigarrillos, Alcoholismo, Bebidas, Sedentarismo, Diabetes, Colesterol, Trigliceridos, HTA) " +
                "VALUES (" + hfIdAfiliado.Value.ToString() + ", CURRENT_TIMESTAMP(), '" + txbMedicinaPrepagada.Text.ToString() + "', " +
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
                OdbcCommand command = new OdbcCommand(strQuery, myConnection);
                myConnection.Open();
                command.ExecuteNonQuery();
                command.Dispose();
                myConnection.Close();
            }
            catch (OdbcException ex)
            {
                string mensaje = ex.Message;
                myConnection.Close();
            }

            Response.Redirect("historiasclinicas");
        }

        protected void btnAfiliado_Click(object sender, EventArgs e)
        {
            string strDocumento = txbAfiliado.Text.ToString();
            string strQuery = "SELECT * FROM Afiliados a " +
                "LEFT JOIN Profesiones p ON a.idProfesion = p.idProfesion " +
                "LEFT JOIN Eps ON a.idEps = Eps.idEps " +
                "LEFT JOIN HistoriasClinicas hc ON a.idAfiliado = hc.IdAfiliado " +
                "WHERE DocumentoAfiliado = '" + strDocumento + "' ";
            clasesglobales cg1 = new clasesglobales();
            DataTable dt = cg1.TraerDatos(strQuery);

            if (dt.Rows.Count > 0)
            {
                txbNombreAfiliado.Text = dt.Rows[0]["NombreAfiliado"].ToString() + " " + dt.Rows[0]["ApellidoAfiliado"].ToString();
                txbProfesion.Text = dt.Rows[0]["Profesion"].ToString();
                txbEps.Text = dt.Rows[0]["NombreEps"].ToString();
                hfGenero.Value = dt.Rows[0]["idGenero"].ToString();
                hfIdAfiliado.Value = dt.Rows[0]["idAfiliado"].ToString();

                if (dt.Rows[0]["idHistoria"].ToString() != "")
                {
                    btnAgregar.Visible = false;
                    //Muestra mensaje para llevarlo a crear un control de ese afiliado.
                }
            }
            dt.Dispose();
        }
    }
}