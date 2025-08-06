using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace fpWebApp.controles
{
    public partial class indicadoresusucrm : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CuantosContactosPorId();
        }

        private void CuantosContactosPorId()
        {
            clasesglobales cg = new clasesglobales();

            DataTable estados = cg.ConsultarEstadossCRM();

            List<Literal> literales = new List<Literal> { ltCuantos1, ltCuantos2, ltCuantos3, ltCuantos4 };
            List<Literal> literalesEtiqueta = new List<Literal> { ltEstado1, ltEstado2, ltEstado3, ltEstado4 };

            for (int i = 0; i < estados.Rows.Count && i < literales.Count; i++)
            {
                int idEstado = Convert.ToInt32(estados.Rows[i]["IdEstadoCRM"]);
                string NombreEstado = estados.Rows[i]["NombreEstadoCRM"].ToString();

                literalesEtiqueta[i].Text = NombreEstado;

                DataTable dtCantidad = cg.ConsultarCuantosPorEstadosCRM(idEstado);

                if (dtCantidad.Rows.Count > 0)
                {
                    literales[i].Text = dtCantidad.Rows[0]["cuantos"].ToString();
                }

                dtCantidad.Dispose();
            }

        }

    }
}