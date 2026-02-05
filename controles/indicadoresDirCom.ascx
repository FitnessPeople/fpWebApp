<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresDirCom.ascx.cs" Inherits="fpWebApp.controles.indicadoresDirCom" %>
<%--
    ****************
    Indicadores: 
        Ventas del mes
        Nuevas afiliaciones
        Tasa de conversión (%)
        Meta del mes
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-warning">
                <h5>Ritmo vs tiempo</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblRitmoReal" runat="server"></asp:Literal>%
                </h1>
                <small>Esperado:
                 <asp:Literal ID="lblRitmoEsperado" runat="server"></asp:Literal>%
                </small>
            </div>
        </div>
    </div>

    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-danger">
                <h5>Proyección cierre mes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblProyeccionCierre" runat="server"></asp:Literal>
                </h1>
                <small>Meta:
                 <asp:Literal ID="lblMetaMes" runat="server"></asp:Literal>
                </small>
            </div>
        </div>
    </div>

    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-info">
                <h5>Días hábiles restantes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblDiasHabiles" runat="server"></asp:Literal>
                </h1>
                <small>Necesario por día:
              <asp:Literal ID="lblVentaNecesariaDia" runat="server"></asp:Literal>
                </small>
            </div>
        </div>
    </div>

    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-danger">
                <h5>Presión comercial</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblPresion" runat="server"></asp:Literal>x
                </h1>
                <small>vs promedio diario actual
                </small>
            </div>
        </div>
    </div>
    <%--  --%>
</div>

<div class="row">
    <div class="col-lg-3">
    <div class="ibox float-e-margins">
        <div class="ibox-title text-primary">
            <h5>Presupuesto total del período</h5>
        </div>
        <div class="ibox-content">
            <h1 class="no-margins">
                <asp:Literal ID="lblPresupuestoTotal" runat="server"></asp:Literal>
            </h1>
            <small>Total canales / sedes</small>
        </div>
    </div>
</div>

    <div class="col-lg-3">
    <div class="ibox float-e-margins">
        <div class="ibox-title text-info">
            <h5>Variación vs mes anterior</h5>
        </div>
        <div class="ibox-content">
            <h1 class="no-margins">
                <asp:Literal ID="lblVariacionMes" runat="server"></asp:Literal>%
            </h1>
            <small>
                Presupuesto comparativo
            </small>
        </div>
    </div>
</div>

    <div class="col-lg-3">
    <div class="ibox float-e-margins">
        <div class="ibox-title text-warning">
            <h5>Canal con mayor meta</h5>
        </div>
        <div class="ibox-content">
            <h1 class="no-margins">
                <asp:Literal ID="lblCanalTop" runat="server"></asp:Literal>
            </h1>
            <small>
                <asp:Literal ID="lblMetaCanalTop" runat="server"></asp:Literal>
            </small>
        </div>
    </div>
</div>

    <div class="col-lg-3">
    <div class="ibox float-e-margins">
        <div class="ibox-title text-danger">
            <h5>Concentración de metas</h5>
        </div>
        <div class="ibox-content">
            <h1 class="no-margins">
                <asp:Literal ID="lblConcentracion" runat="server"></asp:Literal>%
            </h1>
            <small>
                Top 3 canales
            </small>
        </div>
    </div>
</div>


</div>
