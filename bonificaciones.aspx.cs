using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fpWebApp
{
    public partial class bonificaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["idUsuario"] != null)
                {
                    ValidarPermisos("Sedes");
                    if (ViewState["SinPermiso"].ToString() == "1")
                    {
                        //No tiene acceso a esta página
                        divMensaje.Visible = true;
                        paginasperfil.Visible = true;
                     //   divContenido.Visible = false;
                    }
                    else
                    {
                        ObtenerPlanes();
                        ObtenerEscalas();
                        ObtenerObjetivos();
                    } 

                }
                else
                {
                    Response.Redirect("logout");
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

        #region Planes Simulador

       [WebMethod]
       public static object ObtenerPlanes()
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                DataSet ds = cg.CrudPlanesSimulador(4, 0, null, 0, 0, false);

                var lista = new List<object>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    lista.Add(new
                    {
                        Id = row["IdPlanSimulador"],
                        Nombre = row["Nombre"].ToString(),
                        Valor = row["Valor"],
                        FactorMix = row["FactorMix"],
                        EsMensual = Convert.ToBoolean(row["EsMensual"])
                    });
                }

                return lista;
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(
                    ex,
                    "bonificaciones.aspx",
                    Convert.ToInt32(HttpContext.Current.Session["idUsuario"])
                );

                return new
                {
                    ok = false,
                    errorId = idLog
                };
            }
        }

        [WebMethod(EnableSession = true)]
        public static object EliminarPlan(int id)
        {
            clasesglobales cg = new clasesglobales();

            cg.CrudPlanesSimulador(3, id, null, 0, 0, false);

            return new { ok = true };
        }

        [WebMethod]
        public static object ObtenerPlanPorId(int id)
        {
            clasesglobales cg = new clasesglobales();

            DataSet ds = cg.CrudPlanesSimulador(5, id, null, 0, 0, false);

            DataRow row = ds.Tables[0].Rows[0];

            return new
            {
                Id = row["IdPlanSimulador"],
                Nombre = row["Nombre"],
                Valor = row["Valor"],
                FactorMix = row["FactorMix"],
                EsMensual = Convert.ToBoolean(row["EsMensual"])
            };
        }

        [WebMethod(EnableSession = true)]
        public static object GuardarPlan(int accion, int id, string nombre, decimal valor, decimal factorMix, bool esMensual)
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                DataSet ds = cg.CrudPlanesSimulador(accion, id, nombre, valor, factorMix, esMensual);

                return new { ok = true };
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, "bonificaciones.aspx", Convert.ToInt32(HttpContext.Current.Session["idUsuario"]));
                return new { ok = false, errorId = idLog };
            }
        }

        #endregion


        #region Escalas

        [WebMethod]
        public static object ObtenerEscalas()
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                DataSet ds = cg.CrudEscalasSimulador(4, 0, null, 0, 0);

                var lista = new List<object>();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new
                        {
                            IdEscala = row["IdEscala"],
                            Nombre = row["Nombre"].ToString(),
                            PuntosMin = row["PuntosMin"],
                            PuntosMax = row["PuntosMax"]
                        });
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, "bonificaciones.aspx",Convert.ToInt32(HttpContext.Current.Session["idUsuario"]));

                return new
                {
                    ok = false,
                    errorId = idLog
                };
            }
        }

        [WebMethod]
        public static object ObtenerEscalaPorId(int id)
        {
            clasesglobales cg = new clasesglobales();

            DataSet ds = cg.CrudEscalasSimulador(5, id, null, 0, 0);

            DataRow row = ds.Tables[0].Rows[0];

            return new
            {
                Id = row["IdEscala"],
                Nombre = row["Nombre"],
                PuntosMin = row["PuntosMin"],
                PuntosMax = row["PuntosMax"]
            };
        }


        [WebMethod(EnableSession = true)]
        public static object GuardarEscala(int accion, int id, string nombre, decimal puntosMin, decimal puntosMax)
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                DataSet ds = cg.CrudEscalasSimulador(accion, id, nombre, puntosMin, puntosMax);

                return new { ok = true };
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, "bonificaciones.aspx",
                    Convert.ToInt32(HttpContext.Current.Session["idUsuario"]));

                return new { ok = false, errorId = idLog };
            }
        }


        [WebMethod]
        public static object EliminarEscala(int id)
        {
            clasesglobales cg = new clasesglobales();

            DataSet ds = cg.CrudEscalasSimulador(3, id, null, 0, 0);

            return new { ok = true };
        }
        #endregion

        #region Objetivos

        [WebMethod]
        public static object ObtenerObjetivos()
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                DataSet ds = cg.CrudObjetivoPlan(4, 0, 0, 0, 0, 0);

                var lista = new List<object>();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        lista.Add(new
                        {
                            IdObjetivo = row["IdObjetivo"],
                            Escala = row["Escala"].ToString(),
                            Plan = row["Plan"].ToString(),
                            CantidadObjetivo = row["CantidadObjetivo"],
                            ValorUnitarioComision = row["ValorUnitarioComision"]
                        });
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(
                    ex,
                    "bonificaciones.aspx",
                    Convert.ToInt32(HttpContext.Current.Session["idUsuario"])
                );

                return new
                {
                    ok = false,
                    errorId = idLog
                };
            }
        }

        [WebMethod]
        public static object ObtenerObjetivoPorId(int id)
        {
            clasesglobales cg = new clasesglobales();

            DataSet ds = cg.CrudObjetivoPlan(5, id, 0, 0, 0, 0);

            DataRow row = ds.Tables[0].Rows[0];

            return new
            {
                Id = row["IdObjetivo"],
                IdEscala = row["IdEscala"],
                IdPlanSimulador = row["IdPlanSimulador"],
                CantidadObjetivo = row["CantidadObjetivo"],
                ValorUnitarioComision = row["ValorUnitarioComision"]
            };
        }

        [WebMethod(EnableSession = true)]
        public static object GuardarObjetivo(int accion, int id, int idEscala, int idPlan, int cantidadObjetivo, decimal valorUnitarioComision)
        {
            clasesglobales cg = new clasesglobales();

            try
            {
                cg.CrudObjetivoPlan(accion, id, idEscala, idPlan, cantidadObjetivo, valorUnitarioComision);

                return new { ok = true };
            }
            catch (Exception ex)
            {
                int idLog = cg.ManejarError(ex, "bonificaciones.aspx",
                Convert.ToInt32(HttpContext.Current.Session["idUsuario"]));

                return new { ok = false, errorId = idLog };
            }
        }

        [WebMethod]
        public static object EliminarObjetivo(int id)
        {
            clasesglobales cg = new clasesglobales();

            cg.CrudObjetivoPlan(3, id, 0, 0, 0, 0);

            return new { ok = true };
        }

        #endregion

        [WebMethod]
        public static ResultadoSimulador CalcularComision(int anual, int semestre, int trimestre, int mensual)
        {
            decimal mix =
                (anual * 1.0m) +
                (semestre * 0.8m) +
                (trimestre * 0.5m) +
                (mensual * 0.2m);

            string escala = "Sin escala";

            if (mix >= 68)
                escala = "Full";
            else if (mix >= 54)
                escala = "Escala 2";
            else if (mix >= 41)
                escala = "Escala 1";

            decimal comision = mix * 10000; // ejemplo

            return new ResultadoSimulador
            {
                PuntosMix = mix,
                Escala = escala,
                Comision = comision
            };
        }

        public class ResultadoSimulador
        {
            public decimal PuntosMix { get; set; }
            public string Escala { get; set; }
            public decimal Comision { get; set; }
        }

  


    }
}