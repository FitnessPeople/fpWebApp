using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Media.Media3D;

namespace fpWebApp.controles
{
    public partial class indicadoresusucrm2 : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
            int idPerfil = Convert.ToInt32(Session["idPerfil"].ToString());
            int idCanalVenta = Convert.ToInt32(Session["idCanalVenta"].ToString());
            ConsultarContactosActivosPorUsuario(idUsuario);

            if (idPerfil == 21 || idPerfil == 1 || idPerfil == 37 ) // Director comercial // CEO // Director operativo // Director marketing
            {
                ObtenerGraficaVentasVsMetasDirectorComercial();
            }
            else if (idPerfil == 2 || idPerfil == 11 || idPerfil == 36 || idPerfil == 18) //Administrador sede // Líder online // Líder corporativo // Ing. Desarrollo
            {
                ObtenerGraficaVentasVsMetasCanalDeVenta();
            }
            else if(idCanalVenta == 13)
            {
                ObtenerGraficaVentasVsMetasAsesor(); // Canal de ventas web
            }
            else
            {
                ObtenerGraficaVentasVsMetasAsesor(); 
            }

        }

        private void ConsultarContactosActivosPorUsuario(int idUsuario)
        {
            clasesglobales cg = new clasesglobales();
            try
            {


                //////////////////////////////////DECLARACIÓN DE VARIABLES ///////////////////////////////
                decimal valorT = 0;
                int valor = 0;
                int idCanalVenta = Convert.ToInt32(Session["idCanalVenta"].ToString());
                int valorVendidoMes = 0;
                int valorVendidoHoy = 0;
                int valorVendidoAnnio = 0;
                int valorMetaAsesorHoy = 0;
                int valorMetaAsesorMes = 0;
                string tipoSedeUsuario = string.Empty;
                int perfilUsuario = 0;
                int cargoUsuario = 0;

                ltNumContactos.Text = "0";
                ltNumNegociacionAceptada.Text = "0";
                ltNumEnNegociacion.Text = "0";
                ltNumCaliente.Text = "0";
                ltNumTibio.Text = "0";
                ltNumFrio.Text = "0";
                ltVendidoMes.Text = "$0";
                ltVendidoDia.Text = "$0";
                ltValorMetaAsesorMes.Text = "$0";
                ltValorMetaAsesorHoy.Text = "$0";
                ltBrechaMes.Text = "$0";
                ltBrechaHoy.Text = "$0";


                DateTime hoy = DateTime.Today;
                int mes = hoy.Month;
                int anio = hoy.Year;
                string nombreMes = hoy.ToString("MMMM", new CultureInfo("es-ES"));
                nombreMes = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nombreMes);
                ltNomMesActual.Text = $"Objetivo mes {nombreMes} {anio}";
                //////////////////////////////////////////////////////////////////////////////////////////////////////


                ///////////////////////////////////////////////META CANAL DE VENTA //////////////////////////////////
                DataTable dt = cg.ConsultarMetasComerciales();

                int canalVenta = Convert.ToInt32(idCanalVenta);
                DataRow meta = ConsultarMetaCanal(dt, canalVenta);

                if (meta != null)
                {
                    valor = Convert.ToInt32(meta["Presupuesto"]);
                    string canal = meta["NombreCanalVenta"].ToString();
                }
                else
                {
                    valor = 0;
                    string canal = canalVenta.ToString();
                }

                ///////////////////////////////////////////////METAS ASESORES MES Y HOY /////////////////////////////////////////////

                DataTable dt6 = cg.ConsultarUsuarioSedePerfilPorId(idUsuario);

                if (dt6.Rows.Count > 0)
                {
                    tipoSedeUsuario = dt6.Rows[0]["TipoSede"].ToString();
                    perfilUsuario = Convert.ToInt32(dt6.Rows[0]["IdPerfil"].ToString());
                    cargoUsuario = dt6.Rows[0]["IdCargo"] == DBNull.Value ? 0: Convert.ToInt32(dt6.Rows[0]["IdCargo"]);

                    DataTable dt7 = cg.ConsultarMetaComercialMensual(idCanalVenta, mes, anio);

                    if (dt7.Rows.Count > 0)
                    {
                        DataRow filaHoy = null;
                        foreach (DataRow row in dt7.Rows)
                        {
                            DateTime fechaInicio = Convert.ToDateTime(row["FechaInicio"]);

                            if (hoy == fechaInicio)
                            {
                                filaHoy = row;
                                break;
                            }
                        }

                        if (filaHoy != null)
                        {
                            //Asesores comerciales
                            if (perfilUsuario == 4 && cargoUsuario == 48)
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["MetaAsesorSeniorMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaAsesorSeniorDia"]);
                            }

                            if (perfilUsuario == 4 && cargoUsuario == 49)
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["MetaAsesorJuniorMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaAsesorJuniorDia"]);
                            }

                            if (perfilUsuario == 4 && cargoUsuario == 3)
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["MetaAsesorEliteMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaAsesorEliteDia"]);
                            }

                            //Asesores Online
                            if (perfilUsuario == 4 && cargoUsuario == 4)
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["MetaAsesorSeniorMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaAsesorSeniorDia"]);
                            }

                            if (perfilUsuario == 4 && cargoUsuario == 57)
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["MetaAsesorJuniorMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaAsesorJuniorDia"]);
                            }

                            if (perfilUsuario == 4 && cargoUsuario == 58)
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["MetaAsesorEliteMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaAsesorEliteDia"]);
                            }

                            //Asesores Corporativo
                            if (perfilUsuario == 10 && cargoUsuario == 37)
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["MetaAsesorSeniorMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaAsesorSeniorDia"]);
                            }

                            if (perfilUsuario == 10 && cargoUsuario == 55)
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["MetaAsesorJuniorMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaAsesorJuniorDia"]);
                            }

                            if (perfilUsuario == 10 && cargoUsuario == 56)
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["MetaAsesorEliteMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaAsesorEliteDia"]);
                            }


                            //Administrador de sede / Lider Online / 
                            if (perfilUsuario == 2 || perfilUsuario == 11 || perfilUsuario == 36 || perfilUsuario == 23)
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["PresupuestoMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaSedeDia"]);
                            }

                            if (perfilUsuario == 21 || perfilUsuario == 1 || perfilUsuario == 37 || perfilUsuario == 18)
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["PresupuestoMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaSedeDia"]);
                            }

                            if (idCanalVenta == 13)
                            {
                                valorMetaAsesorMes = Convert.ToInt32(filaHoy["PresupuestoMes"]);
                                valorMetaAsesorHoy = Convert.ToInt32(filaHoy["MetaSedeDia"]);
                            }

                            ltValorMetaAsesorMes.Text = valorMetaAsesorMes.ToString("C0", new CultureInfo("es-CO"));
                            ltValorMetaAsesorHoy.Text = valorMetaAsesorHoy.ToString("C0", new CultureInfo("es-CO"));
                        }
                    }


                }

                ///////////////////////////////////////////////PANEL INDICADORES SUPERIOR DERECHC/////////////////////////

                DataTable dt2 = cg.ConsultarContactosCRMPorUsuario(idUsuario, out valorT);

                if (dt2.Rows.Count > 0)
                {
                    ltNumContactos.Text = dt2.Rows.Count.ToString();
                    //Negociación aceptada
                    DataRow[] RegistrosNegociacionAceptada = dt2.Select("idEstadoCRM = 3");
                    ltNumNegociacionAceptada.Text = RegistrosNegociacionAceptada.Length.ToString();
                    //En negociación
                    DataRow[] RegistrosEnNegociacion = dt2.Select("idEstadoCRM = 2");
                    ltNumEnNegociacion.Text = RegistrosEnNegociacion.Length.ToString();
                    //En caliente
                    DataRow[] RegistrosEnCaliente = dt2.Select("idEstadoVenta = 1");
                    ltNumCaliente.Text = RegistrosEnCaliente.Length.ToString();
                    //En tibio
                    DataRow[] RegistrosEnTibio = dt2.Select("idEstadoVenta = 2");
                    ltNumTibio.Text = RegistrosEnTibio.Length.ToString();
                    //En Frio
                    DataRow[] RegistrosEnFrio = dt2.Select("idEstadoVenta = 3");
                    ltNumFrio.Text = RegistrosEnFrio.Length.ToString();
                }

                //////////////INDICADORES DE VALORES - VENDIDO MES Y VENDIDO HOY///////////////////////

                DataTable dt3 = cg.ConsultarVentasAsesorMesVigente(idUsuario);

                valorVendidoMes = Convert.ToInt32(dt3.Rows[0]["TotalVendido"].ToString());

                if (dt3.Rows.Count > 0)
                    ltVendidoMes.Text = valorVendidoMes.ToString("C0", new CultureInfo("es-CO"));

                DataTable dt4 = cg.ConsultarVentasAsesorAnnioVigente(idUsuario);

                if (dt4.Rows.Count > 0)
                    valorVendidoAnnio = Convert.ToInt32(dt4.Rows[0]["TotalVendido"].ToString());

                DataTable dt5 = cg.ConsultarVentasAsesorDiaVigente(idUsuario);

                if (dt5.Rows.Count > 0)
                    valorVendidoHoy = Convert.ToInt32(dt5.Rows[0]["TotalVendido"].ToString());

                ltVendidoDia.Text = valorVendidoHoy.ToString("C0", new CultureInfo("es-CO"));

                /////////////////////////////////////////BRECHAS////////////////////////////////////

                int brechames = valorVendidoMes - valorMetaAsesorMes;
                ltBrechaMes.Text = brechames.ToString("C0", new CultureInfo("es-CO"));

                int brechahoy = valorVendidoHoy - valorMetaAsesorHoy;
                ltBrechaHoy.Text = brechahoy.ToString("C0", new CultureInfo("es-CO"));
                /////////////////////////////////////////////////////////////////////////////////////
            }
            catch (Exception ex)
            {
                string mensaje = ex.ToString();
            }
        }

        public DataRow ConsultarMetaCanal(DataTable dt, int idCanalVenta)
        {
            int mesActual = DateTime.Now.Month;
            int annioActual = DateTime.Now.Year;

            foreach (DataRow row in dt.Rows)
            {
                if ((int)row["idCanalVenta"] == idCanalVenta &&
                    (int)row["Mes"] == mesActual &&
                    (int)row["Annio"] == annioActual)
                {
                    return row; // devuelve el registro correcto
                }
            }

            return null;
        }


        public string labelsJson { get; set; }
        public string metasJson { get; set; }
        public string ventasJson { get; set; }

        private void ObtenerGraficaVentasVsMetasAsesor()
        {
            int idUsuario = 0;
            int idCanalVenta = 0;
            string tipoSedeUsuario = string.Empty;
            int perfilUsuario = 0;
            int cargoUsuario = 0;
            DateTime hoy = DateTime.Today;
            int _mes = hoy.Month;
            int _anio = hoy.Year;
            ltFechaHoy.Text = hoy.ToString("dd.MM.yyyy");
            int valor1 = 0;


            try
            {
                idUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
                clasesglobales cg = new clasesglobales();
                DataTable dt4 = cg.ConsultarUsuarioSedePerfilPorId(idUsuario);
                if (dt4.Rows.Count > 0)
                {
                    var row = dt4.Rows[0];

                    int.TryParse(row["idCanalVenta"]?.ToString(), out idCanalVenta);
                    tipoSedeUsuario = row["TipoSede"]?.ToString() ?? string.Empty;
                    int.TryParse(row["IdPerfil"]?.ToString(), out perfilUsuario);
                    int.TryParse(row["IdCargo"]?.ToString(), out cargoUsuario);
                }

                DataTable dt = cg.ConsultarVentasVsMetasPorUusuarioCRM(idCanalVenta, _mes, _anio, idUsuario);

                ///////////////////////////////////////////////META CANAL DE VENTA //////////////////////////////////
                DataTable dt1 = cg.ConsultarMetasComerciales();

                int canalVenta = Convert.ToInt32(idCanalVenta);
                DataRow meta = ConsultarMetaCanal(dt1, canalVenta);

                if (meta != null)
                {
                    valor1 = Convert.ToInt32(meta["Presupuesto"]);
                    string canal = meta["NombreCanalVenta"].ToString();
                }
                else
                {
                    valor1 = 0;
                    string canal = canalVenta.ToString();
                }

                var labels = new List<string>();
                var metas = new List<decimal>();
                var ventas = new List<decimal>();

                if (dt.Rows.Count > 0)
                {
                    // Cargos
                    // 3  Asesor Comercial Elite
                    //49  Asesor Comercial Junior
                    //48  Asesor Comercial Senior
                    //56  Asesor Corporativo Elite
                    //55  Asesor Corporativo Junior
                    //37  Asesor Corporativo Senior
                    //58  Asesor Online Elite
                    //57  Asesor Online Junior
                    // 4  Asesor Online Senior

                    //Perfiles
                    //2  Administrador sede
                    //4  Asesor comercial / Asesor online
                    //10  Asesor corporativo

                    var datosPorDia = dt.AsEnumerable()
                        .GroupBy(r => Convert.ToDateTime(r["Fecha"]).Day)
                        .ToDictionary(
                            g => g.Key,
                            g =>
                            {
                                decimal metaAcumulada = 0;
                                decimal ventasAcumuladas = 0;

                                foreach (var fila in g)
                                {
                                    int valorMetaHoy = 0;

                                    //if (perfilUsuario == 4 && cargoUsuario == 48)
                                    //    valorMetaHoy = Convert.ToInt32(fila["MetaAsesorSeniorDia"]);
                                    //else if (perfilUsuario == 4 && cargoUsuario == 49)
                                    //    valorMetaHoy = Convert.ToInt32(fila["MetaAsesorJuniorDia"]);
                                    //else if (perfilUsuario == 4 && cargoUsuario == 3)
                                    //    valorMetaHoy = Convert.ToInt32(fila["MetaAsesorEliteDia"]);
                                    //else if (perfilUsuario == 2)
                                    //    valorMetaHoy = Convert.ToInt32(fila["MetaDirectorSedeDia"]);                                    
                                    //else
                                    //    valorMetaHoy = Convert.ToInt32(fila["MetaSedeDia"]); 

                                    // 1️⃣ Administrador de sede (perfil manda)
                                    if (perfilUsuario == 2)
                                    {
                                        valorMetaHoy = Convert.ToInt32(fila["MetaDirectorSedeDia"]);
                                    }
                                    else
                                    {
                                        // 2️⃣ Asesores: manda el cargo, no el perfil
                                        switch (cargoUsuario)
                                        {
                                            // 🔝 ELITE
                                            case 3:   // Comercial Elite
                                            case 56:  // Corporativo Elite
                                            case 58:  // Online Elite
                                                valorMetaHoy = Convert.ToInt32(fila["MetaAsesorEliteDia"]);
                                                break;

                                            // 🧑‍💼 SENIOR
                                            case 48:  // Comercial Senior
                                            case 37:  // Corporativo Senior
                                            case 4:   // Online Senior
                                                valorMetaHoy = Convert.ToInt32(fila["MetaAsesorSeniorDia"]);
                                                break;

                                            // 🧑‍🎓 JUNIOR
                                            case 49:  // Comercial Junior
                                            case 55:  // Corporativo Junior
                                            case 57:  // Online Junior
                                                valorMetaHoy = Convert.ToInt32(fila["MetaAsesorJuniorDia"]);
                                                break;

                                            // 🏢 Otro
                                            default:
                                                valorMetaHoy = Convert.ToInt32(fila["MetaSedeDia"]);
                                                break;
                                        }
                                    }

                                    metaAcumulada += valorMetaHoy;
                                    ventasAcumuladas += Convert.ToDecimal(fila["VentaDia"]);

                                }

                                return new
                                {
                                    Metas = metaAcumulada,
                                    Ventas = ventasAcumuladas
                                };
                            }
                        );

                    int diasDelMes = DateTime.DaysInMonth(_anio, _mes);

                    for (int dia = 1; dia <= diasDelMes; dia++)
                    {
                        labels.Add(dia.ToString());

                        if (datosPorDia.ContainsKey(dia))
                        {
                            metas.Add(datosPorDia[dia].Metas);
                            ventas.Add(datosPorDia[dia].Ventas);
                        }
                        else
                        {
                            metas.Add(0);
                            ventas.Add(0);
                        }
                    }
                }
                else
                {
                    int diasDelMes = DateTime.DaysInMonth(_anio, _mes);
                    for (int dia = 1; dia <= diasDelMes; dia++)
                    {
                        labels.Add(dia.ToString());
                        metas.Add(0);
                        ventas.Add(0);
                    }
                }

                labelsJson = Newtonsoft.Json.JsonConvert.SerializeObject(labels);
                metasJson = Newtonsoft.Json.JsonConvert.SerializeObject(metas);
                ventasJson = Newtonsoft.Json.JsonConvert.SerializeObject(ventas);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }
        }

        private void ObtenerGraficaVentasVsMetasCanalDeVenta()
        {
            int idUsuario = 0;
            int idCanalVenta = 0;
            string tipoSedeUsuario = string.Empty;
            int perfilUsuario = 0;
            DateTime hoy = DateTime.Today;
            int _mes = hoy.Month;
            int _anio = hoy.Year;
            ltFechaHoy.Text = hoy.ToString("dd.MM.yyyy");


            try
            {
                idUsuario = Convert.ToInt32(Session["idUsuario"].ToString());
                clasesglobales cg = new clasesglobales();
                DataTable dt4 = cg.ConsultarUsuarioSedePerfilPorId(idUsuario);
                if (dt4.Rows.Count > 0)
                {
                    idCanalVenta = Convert.ToInt32(dt4.Rows[0]["idCanalVenta"].ToString());
                    tipoSedeUsuario = dt4.Rows[0]["TipoSede"].ToString();
                    perfilUsuario = Convert.ToInt32(dt4.Rows[0]["IdPerfil"].ToString());
                }

                DataTable dt = cg.ConsultarVentasVsMetasPorCanalVenta(idCanalVenta, _mes, _anio);

                var labels = new List<string>();
                var metas = new List<decimal>();
                var ventas = new List<decimal>();
                int _valorMetaHoy = 0;
                decimal ventasAcumuladasMes = dt.AsEnumerable()
                        .Sum(r => r.Field<decimal>("VentaDia"));
                ltVendidoMes.Text = ventasAcumuladasMes.ToString("C0", new CultureInfo("es-CO"));
                
                if (dt.Rows.Count > 0)
                {

                    var datosPorDia = dt.AsEnumerable()
                        .GroupBy(r => Convert.ToDateTime(r["Fecha"]).Day)
                        .ToDictionary(
                            g => g.Key,
                            g =>
                            {
                                decimal metaAcumulada = 0;
                                decimal ventasAcumuladas = 0;
                                decimal valorMetaSede = 0;
                             
                                foreach (var fila in g)
                                {
                                    int valorMetaHoy = 0;
                                   

                                    valorMetaHoy = Convert.ToInt32(fila["MetaSedeDia"]); //
                                    valorMetaSede = Convert.ToInt32(fila["Presupuesto"]); //
                                    _valorMetaHoy = valorMetaHoy;

                                   metaAcumulada += valorMetaHoy;
                                    ventasAcumuladas += Convert.ToDecimal(fila["VentaDia"]);

                                }

                                ltValorMetaAsesorMes.Text = valorMetaSede.ToString("C0", new CultureInfo("es-CO"));
                                ltValorMetaAsesorHoy.Text = _valorMetaHoy.ToString("C0", new CultureInfo("es-CO"));

                                /////////////////////////////////////////BRECHAS////////////////////////////////////

                                decimal brechames = ventasAcumuladas - valorMetaSede;
                                ltBrechaMes.Text = brechames.ToString("C0", new CultureInfo("es-CO"));
                                ////////////////////////////////////////////////////////////////////////////////////

                                int brechahoy = Convert.ToInt32(dt.Rows[0]["MetaSedeDia"]) - Convert.ToInt32(ventasAcumuladas.ToString());
                                ltBrechaHoy.Text = brechahoy.ToString("C0", new CultureInfo("es-CO"));
                                /////////////////////////////////////////////////////////////////////////////////////
                                return new
                                {
                                    Metas = metaAcumulada,
                                    Ventas = ventasAcumuladas
                                };
                            }
                        );                   

                    int diasDelMes = DateTime.DaysInMonth(_anio, _mes);

                    for (int dia = 1; dia <= diasDelMes; dia++)
                    {
                        labels.Add(dia.ToString());

                        if (datosPorDia.ContainsKey(dia))
                        {
                            metas.Add(datosPorDia[dia].Metas);
                            ventas.Add(datosPorDia[dia].Ventas);
                        }
                        else
                        {
                            metas.Add(0);
                            ventas.Add(0);
                        }
                    }
                }
                else
                {
                    int diasDelMes = DateTime.DaysInMonth(_anio, _mes);
                    for (int dia = 1; dia <= diasDelMes; dia++)
                    {
                        labels.Add(dia.ToString());
                        metas.Add(0);
                        ventas.Add(0);
                    }
                }

                labelsJson = Newtonsoft.Json.JsonConvert.SerializeObject(labels);
                metasJson = Newtonsoft.Json.JsonConvert.SerializeObject(metas);
                ventasJson = Newtonsoft.Json.JsonConvert.SerializeObject(ventas);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }
        }

        private void ObtenerGraficaVentasVsMetasDirectorComercial()
        {
            DateTime hoy = DateTime.Today;
            int _mes = hoy.Month;
            int _anio = hoy.Year;
            ltFechaHoy.Text = hoy.ToString("dd.MM.yyyy");

            try
            {
                clasesglobales cg = new clasesglobales();
                DataTable dt = cg.ConsultarVentasVsMetasGlobal(_mes, _anio);  

                var labels = new List<string>();
                var metas = new List<decimal>();
                var ventas = new List<decimal>();

                if (dt.Rows.Count > 0)
                {
                    decimal totalVentasMes = 0;
                    decimal totalMetaSedesMes = 0;
              

                    int diaHoy = hoy.Day;

                    // Construir el diccionario por día (igual que antes)
                    var datosPorDia = dt.AsEnumerable()
                        .GroupBy(r => Convert.ToDateTime(r["Fecha"]).Day)
                        .ToDictionary(
                            g => g.Key,
                            g =>
                            {
                                decimal metaAcumulada = 0;
                                decimal ventasAcumuladas = 0;
                                decimal valorMetaSedes = 0;
                                decimal _valorMetaHoy = 0;

                                foreach (var fila in g)
                                {
                                    decimal valorMetaHoy = Convert.ToDecimal(fila["MetaSedeDia"]);
                                    _valorMetaHoy = valorMetaHoy;
                                    valorMetaSedes = Convert.ToDecimal(fila["PresupuestoMes"]);

                                    metaAcumulada += valorMetaHoy;
                                    ventasAcumuladas += Convert.ToDecimal(fila["VentaDia"]);
                                }

                                return new
                                {
                                    Metas = metaAcumulada,
                                    Ventas = ventasAcumuladas,
                                    MetaSedeDia = _valorMetaHoy,
                                    PresupuestoMes = valorMetaSedes
                                };
                            }
                        );

                    // Calcular totales del mes (sumando todas las entradas)
                    foreach (var kv in datosPorDia)
                    {
                        totalVentasMes += kv.Value.Ventas;
                        if (totalMetaSedesMes == 0 && kv.Value.PresupuestoMes != 0)
                            totalMetaSedesMes = kv.Value.PresupuestoMes;
                    }

                    decimal ventasHoy = 0;
                    decimal metaHoy = 0;

                    if (datosPorDia.ContainsKey(diaHoy))
                    {
                        ventasHoy = datosPorDia[diaHoy].Ventas;
                        metaHoy = datosPorDia[diaHoy].MetaSedeDia; 
                    }


                    ltVendidoMes.Text = totalVentasMes.ToString("C0", new CultureInfo("es-CO"));

                    ltValorMetaAsesorMes.Text = totalMetaSedesMes.ToString("C0", new CultureInfo("es-CO"));

                    ltValorMetaAsesorHoy.Text = metaHoy.ToString("C0", new CultureInfo("es-CO"));


                    decimal brechames = totalVentasMes - totalMetaSedesMes;
                    ltBrechaMes.Text = brechames.ToString("C0", new CultureInfo("es-CO"));

                    decimal brechahoy =  ventasHoy - metaHoy;
                    ltBrechaHoy.Text = brechahoy.ToString("C0", new CultureInfo("es-CO"));

                    int diasDelMes = DateTime.DaysInMonth(_anio, _mes);

                    for (int dia = 1; dia <= diasDelMes; dia++)
                    {
                        labels.Add(dia.ToString());

                        if (datosPorDia.ContainsKey(dia))
                        {
                            metas.Add(datosPorDia[dia].Metas);
                            ventas.Add(datosPorDia[dia].Ventas);
                        }
                        else
                        {
                            metas.Add(0);
                            ventas.Add(0);
                        }
                    }
                }


                labelsJson = Newtonsoft.Json.JsonConvert.SerializeObject(labels);
                metasJson = Newtonsoft.Json.JsonConvert.SerializeObject(metas);
                ventasJson = Newtonsoft.Json.JsonConvert.SerializeObject(ventas);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();
            }
        }

    }
}
