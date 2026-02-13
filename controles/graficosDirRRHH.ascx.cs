using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class graficosDirRRHH : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Gráficos
            CantidadGenero();
            CantidadCiudad();
            CantidadEstadoCivil();
            CantidadTipoContrato();
            CantidadNivelEstudio();
            CantidadTipoVivienda();
            CantidadActividadExtra();
            CantidadConsumoLicor();

            CantidadEdades();
            CantidadMedioTransporte();
            CantidadTipoSangre();
        }

        private void CantidadGenero()
        {
            string strGeneros = "";
            if (Session["idSede"].ToString() == "11") // Usuario administrativo
            {
                strGeneros = @"SELECT e.idGenero, 
                    IF(g.Genero = 'Masculino', '🙍‍♂️ Masc','🙍‍♀️ Fem') AS Genero, 
                    COUNT(*) AS cuantos  
                    FROM empleados e 
                    LEFT JOIN generos g ON g.idGenero = e.idGenero 
                    GROUP BY e.idGenero";
            }
            else
            {
                strGeneros = @"SELECT e.idGenero, g.Genero, COUNT(*) AS cuantos  
                    FROM empleados e 
                    LEFT JOIN generos g ON g.idGenero = e.idGenero 
                    WHERE e.idSede = " + Session["idSede"].ToString() + @" 
                    GROUP BY e.idGenero";
            }

            clasesglobales cg = new clasesglobales();

            DataTable dt = cg.TraerDatos(strGeneros);

            if (dt.Rows.Count > 0)
            {
                List<string> nombres = new List<string>();
                List<int> cantidades = new List<int>();

                foreach (DataRow row in dt.Rows)
                {
                    nombres.Add(row["Genero"].ToString());
                    cantidades.Add(Convert.ToInt32(row["cuantos"]));
                }

                var serializer = new JavaScriptSerializer();
                string nombresJson = serializer.Serialize(nombres);
                string cantidadesJson = serializer.Serialize(cantidades);

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "dataChart1",
                    $"var nombres1 = {nombresJson}; var cantidades1 = {cantidadesJson};",
                    true
                );

            }

            dt.Dispose();
        }

        private void CantidadCiudad()
        {
            string strCiudades = "";
            if (Session["idSede"].ToString() == "11") // Usuario administrativo
            {
                strCiudades = @"SELECT idCiudadEmpleado, NombreCiudad, COUNT(*) AS cuantos  
                    FROM empleados e, ciudades c  
                    WHERE e.idCiudadEmpleado = c.idCiudad 
                    AND idCiudadEmpleado IS NOT NULL
                    AND idCiudadEmpleado IN (
                    SELECT idCiudadEmpleado 
                    FROM empleados 
                    WHERE idCiudadEmpleado IS NOT NULL 
                    GROUP BY idCiudadEmpleado) 
                    GROUP BY idCiudadEmpleado";
            }
            else
            {
                strCiudades = @"SELECT idCiudadEmpleado, NombreCiudad, COUNT(*) AS cuantos  
                    FROM empleados e, ciudades c  
                    WHERE e.idCiudadEmpleado = c.idCiudad 
                    AND idCiudadEmpleado IS NOT NULL 
                    AND e.idSede = " + Session["idSede"].ToString() + @" 
                    AND idCiudadEmpleado IN (
                    SELECT idCiudadEmpleado 
                    FROM empleados 
                    WHERE idCiudadEmpleado IS NOT NULL 
                    GROUP BY idCiudadEmpleado) 
                    GROUP BY idCiudadEmpleado";
            }

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

                ScriptManager.RegisterStartupScript(
                    this,
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
            string strEstadoCivil = "";
            if (Session["idSede"].ToString() == "11") // Usuario administrativo
            {
                strEstadoCivil = @"SELECT e.idEstadoCivil, EstadoCivil, COUNT(*) AS cuantos  
                    FROM empleados e, EstadoCivil ec  
                    WHERE e.idEstadoCivil = ec.idEstadoCivil 
                    AND e.idEstadoCivil IN (
                    SELECT idEstadoCivil 
                    FROM empleados 
                    WHERE idEstadoCivil IS NOT NULL 
                    GROUP BY idEstadoCivil) 
                    GROUP BY idEstadoCivil";
            }
            else
            {
                strEstadoCivil = @"SELECT e.idEstadoCivil, EstadoCivil, COUNT(*) AS cuantos  
                    FROM empleados e, EstadoCivil ec  
                    WHERE e.idEstadoCivil = ec.idEstadoCivil 
                    AND e.idSede = " + Session["idSede"].ToString() + @" 
                    AND e.idEstadoCivil IN (
                    SELECT idEstadoCivil 
                    FROM empleados 
                    WHERE idEstadoCivil IS NOT NULL 
                    GROUP BY idEstadoCivil) 
                    GROUP BY idEstadoCivil";
            }

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

                ScriptManager.RegisterStartupScript(
                    this,
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
            string strTipoContrato = "";
            if (Session["idSede"].ToString() == "11") // Usuario administrativo
            {
                strTipoContrato = @"SELECT TipoContrato, COUNT(*) AS cuantos  
                    FROM empleados e  
                    WHERE TipoContrato IN (
                    SELECT TipoContrato 
                    FROM empleados 
                    WHERE TipoContrato IS NOT NULL 
                    GROUP BY TipoContrato) 
                    GROUP BY TipoContrato";
            }
            else
            {
                strTipoContrato = @"SELECT TipoContrato, COUNT(*) AS cuantos  
                    FROM empleados e  
                    WHERE TipoContrato IN (
                    SELECT TipoContrato 
                    FROM empleados 
                    WHERE TipoContrato IS NOT NULL 
                    AND e.idSede = " + Session["idSede"].ToString() + @" 
                    GROUP BY TipoContrato) 
                    GROUP BY TipoContrato";
            }

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

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "dataChart4",
                    $"var nombres4 = {nombresJson}; var cantidades4 = {cantidadesJson}; var colores4 = {coloresJson};",
                    true
                );

            }

            dt.Dispose();
        }

        private void CantidadNivelEstudio()
        {
            string strNivelEstudio = "";
            if (Session["idSede"].ToString() == "11") // Usuario administrativo
            {
                strNivelEstudio = @"SELECT NivelEstudio, COUNT(*) AS cuantos  
                    FROM empleados e 
                    WHERE NivelEstudio IS NOT NULL
                    GROUP BY NivelEstudio
                    ORDER BY NivelEstudio";
            }
            else
            {
                strNivelEstudio = @"SELECT NivelEstudio, COUNT(*) AS cuantos  
                    FROM empleados e 
                    WHERE NivelEstudio IS NOT NULL
                    AND e.idSede = " + Session["idSede"].ToString() + @" 
                    GROUP BY NivelEstudio
                    ORDER BY NivelEstudio";
            }

            clasesglobales cg = new clasesglobales();

            DataTable dt = cg.TraerDatos(strNivelEstudio);

            if (dt.Rows.Count > 0)
            {
                List<string> nombres = new List<string>();
                List<int> cantidades = new List<int>();
                List<string> colores = new List<string>();
                int cuantos = 0;

                foreach (DataRow row in dt.Rows)
                {
                    cuantos += 1;
                    nombres.Add(row["NivelEstudio"].ToString());
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

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "dataChart5",
                    $"var nombres5 = {nombresJson}; var cantidades5 = {cantidadesJson}; var colores5 = {coloresJson};",
                    true
                );

            }

            dt.Dispose();
        }

        private void CantidadTipoVivienda()
        {
            string strTipoVivienda = "";
            if (Session["idSede"].ToString() == "11") // Usuario administrativo
            {
                strTipoVivienda = @"SELECT TipoVivienda, COUNT(*) AS cuantos  
                    FROM empleados e 
                    WHERE TipoVivienda IS NOT NULL
                    GROUP BY TipoVivienda";
            }
            else
            {
                strTipoVivienda = @"SELECT TipoVivienda, COUNT(*) AS cuantos  
                    FROM empleados e 
                    WHERE TipoVivienda IS NOT NULL 
                    AND e.idSede = " + Session["idSede"].ToString() + @" 
                    GROUP BY TipoVivienda";
            }

            clasesglobales cg = new clasesglobales();

            DataTable dt = cg.TraerDatos(strTipoVivienda);

            if (dt.Rows.Count > 0)
            {
                List<string> nombres = new List<string>();
                List<int> cantidades = new List<int>();
                List<string> colores = new List<string>();
                int cuantos = 0;

                foreach (DataRow row in dt.Rows)
                {
                    cuantos += 1;
                    nombres.Add(row["TipoVivienda"].ToString());
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

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "dataChart6",
                    $"var nombres6 = {nombresJson}; var cantidades6 = {cantidadesJson}; var colores6 = {coloresJson};",
                    true
                );

            }

            dt.Dispose();
        }

        private void CantidadActividadExtra()
        {
            string strActividadExtra = "";
            if (Session["idSede"].ToString() == "11") // Usuario administrativo
            {
                strActividadExtra = @"SELECT ActividadExtra, COUNT(*) AS cuantos  
                    FROM empleados e 
                    WHERE ActividadExtra IS NOT NULL 
                    GROUP BY ActividadExtra";
            }
            else
            {
                strActividadExtra = @"SELECT ActividadExtra, COUNT(*) AS cuantos  
                    FROM empleados e 
                    WHERE ActividadExtra IS NOT NULL 
                    AND e.idSede = " + Session["idSede"].ToString() + @" 
                    GROUP BY ActividadExtra";
            }

            clasesglobales cg = new clasesglobales();

            DataTable dt = cg.TraerDatos(strActividadExtra);

            if (dt.Rows.Count > 0)
            {
                List<string> nombres = new List<string>();
                List<int> cantidades = new List<int>();
                List<string> colores = new List<string>();
                int cuantos = 0;

                foreach (DataRow row in dt.Rows)
                {
                    cuantos += 1;
                    nombres.Add(row["ActividadExtra"].ToString());
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

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "dataChart7",
                    $"var nombres7 = {nombresJson}; var cantidades7 = {cantidadesJson}; var colores7 = {coloresJson};",
                    true
                );

            }

            dt.Dispose();
        }

        private void CantidadConsumoLicor()
        {
            string strConsumeLicor = "";
            if (Session["idSede"].ToString() == "11") // Usuario administrativo
            {
                strConsumeLicor = @"SELECT ConsumeLicor, COUNT(*) AS cuantos  
                    FROM empleados e 
                    WHERE ConsumeLicor IS NOT NULL 
                    GROUP BY ConsumeLicor";
            }
            else
            {
                strConsumeLicor = @"SELECT ConsumeLicor, COUNT(*) AS cuantos  
                    FROM empleados e 
                    WHERE ConsumeLicor IS NOT NULL 
                    AND e.idSede = " + Session["idSede"].ToString() + @" 
                    GROUP BY ConsumeLicor";
            }

            clasesglobales cg = new clasesglobales();

            DataTable dt = cg.TraerDatos(strConsumeLicor);

            if (dt.Rows.Count > 0)
            {
                List<string> nombres = new List<string>();
                List<int> cantidades = new List<int>();
                List<string> colores = new List<string>();
                int cuantos = 0;

                foreach (DataRow row in dt.Rows)
                {
                    cuantos += 1;
                    nombres.Add(row["ConsumeLicor"].ToString());
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

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "dataChart8",
                    $"var nombres8 = {nombresJson}; var cantidades8 = {cantidadesJson}; var colores8 = {coloresJson};",
                    true
                );

            }

            dt.Dispose();
        }

        private void CantidadEdades()
        {
            string strEdades = "";
            if (Session["idSede"].ToString() == "11") // Usuario administrativo
            {
                strEdades = @"SELECT 
                    CASE 
                        WHEN TIMESTAMPDIFF(YEAR, e.FechaNacEmpleado, CURDATE()) < 25 THEN '<25 años'
                        WHEN TIMESTAMPDIFF(YEAR, e.FechaNacEmpleado, CURDATE()) BETWEEN 25 AND 35 THEN '25–35 años'
                        WHEN TIMESTAMPDIFF(YEAR, e.FechaNacEmpleado, CURDATE()) BETWEEN 36 AND 45 THEN '36–45 años'
                        WHEN TIMESTAMPDIFF(YEAR, e.FechaNacEmpleado, CURDATE()) BETWEEN 46 AND 60 THEN '46–60 años'
                        ELSE '>60 años'
                    END AS RangoEdad,
                    COUNT(*) AS cuantos 
                    FROM Empleados e 
                    WHERE e.FechaNacEmpleado IS NOT NULL
                    GROUP BY RangoEdad;";
            }
            else
            {
                strEdades = @"SELECT 
                    CASE 
                        WHEN TIMESTAMPDIFF(YEAR, e.FechaNacEmpleado, CURDATE()) < 25 THEN '<25 años'
                        WHEN TIMESTAMPDIFF(YEAR, e.FechaNacEmpleado, CURDATE()) BETWEEN 25 AND 35 THEN '25–35 años'
                        WHEN TIMESTAMPDIFF(YEAR, e.FechaNacEmpleado, CURDATE()) BETWEEN 36 AND 45 THEN '36–45 años'
                        WHEN TIMESTAMPDIFF(YEAR, e.FechaNacEmpleado, CURDATE()) BETWEEN 46 AND 60 THEN '46–60 años'
                        ELSE '>60 años'
                    END AS RangoEdad,
                    COUNT(*) AS cuantos 
                    FROM Empleados e 
                    WHERE e.FechaNacEmpleado IS NOT NULL 
                    AND e.idSede = " + Session["idSede"].ToString() + @" 
                    GROUP BY RangoEdad;";
            }

            clasesglobales cg = new clasesglobales();

            DataTable dt = cg.TraerDatos(strEdades);

            if (dt.Rows.Count > 0)
            {
                List<string> nombres = new List<string>();
                List<int> cantidades = new List<int>();
                List<string> colores = new List<string>();
                int cuantos = 0;

                foreach (DataRow row in dt.Rows)
                {
                    cuantos += 1;
                    nombres.Add(row["RangoEdad"].ToString());
                    cantidades.Add(Convert.ToInt32(row["cuantos"]));

                    string color = cg.GenerateColor(cuantos, Math.Max(1, Convert.ToInt32(row["cuantos"])));
                    colores.Add(color);
                }

                var serializer = new JavaScriptSerializer();
                string nombresJson = serializer.Serialize(nombres);
                string cantidadesJson = serializer.Serialize(cantidades);
                string coloresJson = serializer.Serialize(colores);

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "dataChart9",
                    $"var nombres9 = {nombresJson}; var cantidades9 = {cantidadesJson}; var colores9 = {coloresJson};",
                    true
                );

            }

            dt.Dispose();
        }

        private void CantidadMedioTransporte()
        {
            string strMedioTransporte = "";
            if (Session["idSede"].ToString() == "11") // Usuario administrativo
            {
                strMedioTransporte = @"SELECT MedioTransporte, COUNT(*) AS cuantos  
                    FROM empleados e 
                    WHERE MedioTransporte IS NOT NULL 
                    GROUP BY MedioTransporte
                    ORDER BY MedioTransporte";
            }
            else
            {
                strMedioTransporte = @"SELECT MedioTransporte, COUNT(*) AS cuantos  
                    FROM empleados e 
                    WHERE MedioTransporte IS NOT NULL 
                    AND e.idSede = " + Session["idSede"].ToString() + @" 
                    GROUP BY MedioTransporte
                    ORDER BY MedioTransporte";
            }

            clasesglobales cg = new clasesglobales();

            DataTable dt = cg.TraerDatos(strMedioTransporte);

            if (dt.Rows.Count > 0)
            {
                List<string> nombres = new List<string>();
                List<int> cantidades = new List<int>();
                List<string> colores = new List<string>();
                int cuantos = 0;

                foreach (DataRow row in dt.Rows)
                {
                    cuantos += 1;
                    nombres.Add(row["MedioTransporte"].ToString());
                    cantidades.Add(Convert.ToInt32(row["cuantos"]));

                    string color = cg.GenerateColor(cuantos, Math.Max(1, Convert.ToInt32(row["cuantos"])));
                    colores.Add(color);
                }

                var serializer = new JavaScriptSerializer();
                string nombresJson = serializer.Serialize(nombres);
                string cantidadesJson = serializer.Serialize(cantidades);
                string coloresJson = serializer.Serialize(colores);

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "dataChart10",
                    $"var nombres10 = {nombresJson}; var cantidades10 = {cantidadesJson}; var colores10 = {coloresJson};",
                    true
                );

            }

            dt.Dispose();
        }

        private void CantidadTipoSangre()
        {
            string strTipoSangre = "";
            if (Session["idSede"].ToString() == "11") // Usuario administrativo
            {
                strTipoSangre = @"SELECT TipoSangre, COUNT(*) AS cuantos  
                    FROM empleados e 
                    WHERE TipoSangre IS NOT NULL 
                    GROUP BY TipoSangre
                    ORDER BY TipoSangre";
            }
            else
            {
                strTipoSangre = @"SELECT TipoSangre, COUNT(*) AS cuantos  
                    FROM empleados e 
                    WHERE TipoSangre IS NOT NULL 
                    AND e.idSede = " + Session["idSede"].ToString() + @" 
                    GROUP BY TipoSangre
                    ORDER BY TipoSangre";
            }

            clasesglobales cg = new clasesglobales();

            DataTable dt = cg.TraerDatos(strTipoSangre);

            if (dt.Rows.Count > 0)
            {
                List<string> nombres = new List<string>();
                List<int> cantidades = new List<int>();
                List<string> colores = new List<string>();
                int cuantos = 0;

                foreach (DataRow row in dt.Rows)
                {
                    cuantos += 1;
                    nombres.Add(row["TipoSangre"].ToString());
                    cantidades.Add(Convert.ToInt32(row["cuantos"]));

                    string color = cg.GenerateColor(cuantos, Math.Max(1, Convert.ToInt32(row["cuantos"])));
                    colores.Add(color);
                }

                var serializer = new JavaScriptSerializer();
                string nombresJson = serializer.Serialize(nombres);
                string cantidadesJson = serializer.Serialize(cantidades);
                string coloresJson = serializer.Serialize(colores);

                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "dataChart11",
                    $"var nombres11 = {nombresJson}; var cantidades11 = {cantidadesJson}; var colores11 = {coloresJson};",
                    true
                );

            }

            dt.Dispose();
        }



    }
}