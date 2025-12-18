using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class empleados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Empleados");
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
                        if (ViewState["Consulta"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = false;
                        }
                        if (ViewState["Exportar"].ToString() == "1")
                        {
                            divBotonesLista.Visible = true;
                            //lbExportarExcel.Visible = true;
                        }
                        if (ViewState["CrearModificar"].ToString() == "1")
                        {
                            btnAgregar.Visible = true;
                        }
                    }
                    listaEmpleados();
                    CantidadGenero();
                    CantidadCiudad();
                    CantidadEstadoCivil();
                    CantidadTipoContrato();
                    //ActualizarEstadoxFechaFinal();
                    //indicadores01.Visible = false;
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

        private void ActualizarEstadoxFechaFinal()
        {
            try
            {
                clasesglobales cg = new clasesglobales();
                string mensaje = cg.ActualizarEstadoEmpleadoPorFechaFinal();
            }
            catch (SqlException ex)
            {
                string mensaje = ex.Message;
            }

        }

        private void listaEmpleados()
        {
            clasesglobales cg = new clasesglobales();
            DataTable dt = new DataTable();
            if (Session["idSede"].ToString() == "11") // Usuario administrativo
            { 
                dt = cg.ConsultarEmpleados();
            }
            else
            {
                dt = cg.ConsultarEmpleadosPorSede(Session["idSede"].ToString());
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                string firstRow = dt.Rows[0]["DocumentoEmpleado"].ToString();
                ViewState.Add("EmployeeDoc", firstRow);
            }

            rpEmpleados.DataSource = dt;
            rpEmpleados.DataBind();

            rpTabEmpleados.DataSource = dt;
            rpTabEmpleados.DataBind();

            dt.Dispose();
        }

        private void CantidadGenero()
        {
            string strGeneros = @"SELECT e.idGenero, g.Genero, COUNT(*) AS cuantos  
                FROM empleados e 
                RIGHT JOIN generos g ON e.idGenero = g.idGenero 
                GROUP BY g.idGenero";
            
            clasesglobales cg = new clasesglobales();
            
            DataTable dt = cg.TraerDatos(strGeneros);

            if (dt.Rows.Count > 0)
            {
                List<string> nombres = new List<string>();
                List<int> cantidades = new List<int>();
                List<string> colores = new List<string>();
                int cuantos = 0;

                foreach (DataRow row in dt.Rows)
                {
                    cuantos += 1;
                    nombres.Add(row["Genero"].ToString());
                    cantidades.Add(Convert.ToInt32(row["cuantos"]));

                    string color = cg.GenerateColor(cuantos, Math.Max(1, Convert.ToInt32(row["cuantos"])));

                    //Random random = new Random();
                    //int randomInt = random.Next(0x1000000);
                    //string hexColor = String.Format("#{0:X6}", randomInt);
                    //colores.Add(hexColor);
                    colores.Add(color);
                }

                var serializer = new JavaScriptSerializer();
                string nombresJson = serializer.Serialize(nombres);
                string cantidadesJson = serializer.Serialize(cantidades);
                string coloresJson = serializer.Serialize(colores);

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "dataChart1",
                    $"var nombres1 = {nombresJson}; var cantidades1 = {cantidadesJson}; var colores1 = {coloresJson};",
                    true
                );
            }

            dt.Dispose();
        }

        private void CantidadCiudad()
        {
            string strCiudades = @"SELECT idCiudadEmpleado, NombreCiudad, COUNT(*) AS cuantos  
                FROM empleados e, ciudades c  
                WHERE e.idCiudadEmpleado = c.idCiudad 
                AND idCiudadEmpleado IN (
                SELECT idCiudadEmpleado 
                FROM empleados 
                WHERE idCiudadEmpleado IS NOT NULL 
                GROUP BY idCiudadEmpleado) 
                GROUP BY idCiudadEmpleado";

            clasesglobales cg = new clasesglobales();

            DataTable dt = cg.TraerDatos(strCiudades);

            if (dt.Rows.Count > 0)
            {
                List<string> nombres = new List<string>();
                List<int> cantidades = new List<int>();
                List<string> colores = new List<string>();
                int cuantos = 0;

                foreach (DataRow row in dt.Rows)
                {
                    cuantos += 1;
                    nombres.Add(row["NombreCiudad"].ToString());
                    cantidades.Add(Convert.ToInt32(row["cuantos"]));

                    string color = cg.GenerateColor(cuantos, Math.Max(1, Convert.ToInt32(row["cuantos"])));

                    //Random random = new Random();
                    //int randomInt = random.Next(0x1000000);
                    //string hexColor = String.Format("#{0:X6}", randomInt);
                    //colores.Add(hexColor);
                    colores.Add(color);
                }

                var serializer = new JavaScriptSerializer();
                string nombresJson = serializer.Serialize(nombres);
                string cantidadesJson = serializer.Serialize(cantidades);
                string coloresJson = serializer.Serialize(colores);

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "dataChart2",
                    $"var nombres2 = {nombresJson}; var cantidades2 = {cantidadesJson}; var colores2 = {coloresJson};",
                    true
                );
            }

            dt.Dispose();
        }

        private void CantidadEstadoCivil()
        {
            string strEstadoCivil = @"SELECT e.idEstadoCivil, EstadoCivil, COUNT(*) AS cuantos  
                FROM empleados e, EstadoCivil ec  
                WHERE e.idEstadoCivil = ec.idEstadoCivil 
                AND e.idEstadoCivil IN (
                SELECT idEstadoCivil 
                FROM empleados 
                WHERE idEstadoCivil IS NOT NULL 
                GROUP BY idEstadoCivil) 
                GROUP BY idEstadoCivil";

            clasesglobales cg = new clasesglobales();

            DataTable dt = cg.TraerDatos(strEstadoCivil);

            if (dt.Rows.Count > 0)
            {
                List<string> nombres = new List<string>();
                List<int> cantidades = new List<int>();
                List<string> colores = new List<string>();
                int cuantos = 0;

                foreach (DataRow row in dt.Rows)
                {
                    cuantos += 1;
                    nombres.Add(row["EstadoCivil"].ToString());
                    cantidades.Add(Convert.ToInt32(row["cuantos"]));

                    string color = cg.GenerateColor(cuantos, Math.Max(1, Convert.ToInt32(row["cuantos"])));

                    //Random random = new Random();
                    //int randomInt = random.Next(0x1000000);
                    //string hexColor = String.Format("#{0:X6}", randomInt);
                    //colores.Add(hexColor);
                    colores.Add(color);
                }

                var serializer = new JavaScriptSerializer();
                string nombresJson = serializer.Serialize(nombres);
                string cantidadesJson = serializer.Serialize(cantidades);
                string coloresJson = serializer.Serialize(colores);

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "dataChart3",
                    $"var nombres3 = {nombresJson}; var cantidades3 = {cantidadesJson}; var colores3 = {coloresJson};",
                    true
                );
            }

            dt.Dispose();
        }

        private void CantidadTipoContrato()
        {
            string strTipoContrato = @"SELECT TipoContrato, COUNT(*) AS cuantos  
                FROM empleados e  
                WHERE TipoContrato IN (
                SELECT TipoContrato 
                FROM empleados 
                WHERE TipoContrato IS NOT NULL 
                GROUP BY TipoContrato) 
                GROUP BY TipoContrato";

            clasesglobales cg = new clasesglobales();

            DataTable dt = cg.TraerDatos(strTipoContrato);

            if (dt.Rows.Count > 0)
            {
                List<string> nombres = new List<string>();
                List<int> cantidades = new List<int>();
                List<string> colores = new List<string>();
                int cuantos = 0;

                foreach (DataRow row in dt.Rows)
                {
                    cuantos += 1;
                    nombres.Add(row["TipoContrato"].ToString());
                    cantidades.Add(Convert.ToInt32(row["cuantos"]));

                    string color = cg.GenerateColor(cuantos, Math.Max(1, Convert.ToInt32(row["cuantos"])));

                    //Random random = new Random();
                    //int randomInt = random.Next(0x1000000);
                    //string hexColor = String.Format("#{0:X6}", randomInt);
                    //colores.Add(hexColor);
                    colores.Add(color);
                }

                var serializer = new JavaScriptSerializer();
                string nombresJson = serializer.Serialize(nombres);
                string cantidadesJson = serializer.Serialize(cantidades);
                string coloresJson = serializer.Serialize(colores);

                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "dataChart4",
                    $"var nombres4 = {nombresJson}; var cantidades4 = {cantidadesJson}; var colores4 = {coloresJson};",
                    true
                );
            }

            dt.Dispose();
        }

        protected void rpEmpleados_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            clasesglobales cg = new clasesglobales();
            
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    string valor = ((DataRowView)e.Item.DataItem).Row[0].ToString();
                    string cifrado = HttpUtility.UrlEncode(cg.Encrypt(valor).Replace("+", "-").Replace("/", "_").Replace("=", ""));

                    HtmlAnchor btnEditar = (HtmlAnchor)e.Item.FindControl("btnEditar");
                    //btnEditar.Attributes.Add("href", "editarempleado?editid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEditar.Attributes.Add("href", "editarempleado?editid=" + cifrado);
                    btnEditar.Visible = true;
                }
                if (ViewState["Borrar"].ToString() == "1")
                {
                    HtmlAnchor btnEliminar = (HtmlAnchor)e.Item.FindControl("btnEliminar");
                    btnEliminar.Attributes.Add("href", "eliminarempleado?deleteid=" + ((DataRowView)e.Item.DataItem).Row[0].ToString());
                    btnEliminar.Visible = true;
                }
            }
        }

        protected void lbExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string consultaSQL = @"SELECT DocumentoEmpleado AS 'Nro. de Documento', NombreEmpleado AS 'Nombre de Empleado', 
                    TelefonoEmpleado AS 'Celular Personal', TelefonoCorporativo AS 'Celular Corporativo', 
                    EmailEmpleado AS 'Correo Personal', EmailCorporativo AS 'Correo Corporativo', 
                    FechaNacEmpleado AS 'Fecha de Nacimiento', DireccionEmpleado AS 'Dirección de Residencia', 
                    NombreCiudad AS 'Ciudad', NroContrato AS 'Nro. de Contrato', 
                    TipoContrato AS 'Tipo de Contrato', cargos.NombreCargo AS 'Cargo de Empleado',
                    FechaInicio AS 'Fecha de Inicio', FechaFinal AS 'Fecha de Terminación',
                    Sueldo, GrupoNomina AS 'Grupos de Nóminas', Estado 
                    FROM Empleados e
                    LEFT JOIN ciudades ON ciudades.idCiudad = e.idCiudadEmpleado 
                    INNER JOIN cargos ON cargos.idCargo = e.idCargo
                    ORDER BY NombreEmpleado;";

                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(consultaSQL);
                string nombreArchivo = $"Empleados_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}";

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

        protected void rpTabEmpleados_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (ViewState["CrearModificar"].ToString() == "1")
                {
                    HtmlAnchor btnEditarTab = (HtmlAnchor)e.Item.FindControl("btnEditarTab");
                    btnEditarTab.Attributes.Add("href", "editarempleado?editid=" + ((DataRowView)e.Item.DataItem).Row["DocumentoEmpleado"].ToString());
                    btnEditarTab.Visible = true;

                    HtmlAnchor btnCambiarEstado = (HtmlAnchor)e.Item.FindControl("btnCambiarEstado");
                    btnCambiarEstado.Attributes.Add("href", "cambiarestadoempleado?id=" + ((DataRowView)e.Item.DataItem).Row["DocumentoEmpleado"].ToString());
                    btnCambiarEstado.Visible = true;
                }

                DataRowView row = (DataRowView)e.Item.DataItem;
                int idUsuario;
                if (row["idUsuario"] is DBNull)
                {
                    idUsuario = 0;
                }
                else
                {
                    idUsuario = Convert.ToInt32(row["idUsuario"]);
                }

                string strQuery = @"
                    SELECT Accion, DescripcionLog, FechaHora, 
                    CASE Accion 
                        WHEN 'Agrega' THEN 'circle-plus' 
                        WHEN 'Elimino' THEN 'trash' 
                        WHEN 'Login' THEN 'lock-open' 
                        WHEN 'Modifica' THEN 'edit' 
                        WHEN 'Logout' THEN 'lock' 
                        WHEN 'Nuevo' THEN 'circle-plus' 
                        ELSE 'coffee' 
                    END AS Label 
                    FROM logs 
                    WHERE idUsuario = " + idUsuario.ToString() + " ORDER BY FechaHora DESC LIMIT 10 ";
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.TraerDatos(strQuery);

                Repeater rpActividades = (Repeater)e.Item.FindControl("rpActividades");
                rpActividades.DataSource = dt;
                rpActividades.DataBind();
            }
        }

        protected void lkbCambiarEstado_Click(object sender, EventArgs e)
        {

        }
    }
}