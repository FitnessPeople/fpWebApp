<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresDirOpe.ascx.cs" Inherits="fpWebApp.controles.indicadoresDirOpe" %>
<%--
    ****************
    Indicadores: 
        Asistencia promedio diaria
        Clases realizadas vs programadas
        Ocupación promedio por sede
        Incidentes operativos abiertos
    ****************
--%>
<div class="row">

    <!-- 1. Ingresos reales -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-primary">
                <h5>Ingresos reales</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblIngresos" runat="server"></asp:Literal>
                </h1>
                <small>Ingresos del mes</small>
            </div>
        </div>
    </div>

    <!-- 2. Meta mensual -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-info">
                <h5>Meta mensual</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblMetaMensual" runat="server"></asp:Literal>
                </h1>
                <small>Objetivo operativo</small>
            </div>
        </div>
    </div>

    <!-- 3. Cumplimiento ingresos -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-success">
                <h5>Cumplimiento ingresos</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblCumplimiento" runat="server"></asp:Literal>%
                </h1>
                <small>Ingresos vs meta</small>
            </div>
        </div>
    </div>

    <!-- 4. Tasa de retención -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-success">
                <h5>Tasa de retención</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblRetencion" runat="server"></asp:Literal>%
                </h1>
                <small>Clientes activos</small>
            </div>
        </div>
    </div>

</div>

<div class="row">

    <!-- 5. Churn -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-danger">
                <h5>Churn</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblChurn" runat="server"></asp:Literal>%
                </h1>
                <small>Cancelaciones del mes</small>
            </div>
        </div>
    </div>

    <!-- 6. Ocupación -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-warning">
                <h5>Ocupación promedio</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblOcupacion" runat="server"></asp:Literal>%
                </h1>
                <small>Capacidad utilizada</small>
            </div>
        </div>
    </div>

    <!-- 7. Ticket promedio -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-info">
                <h5>Ticket promedio</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblTicketPromedio" runat="server"></asp:Literal>
                </h1>
                <small>Ingreso por cliente</small>
            </div>
        </div>
    </div>

    <!-- 8. Cumplimiento por sede -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-primary">
                <h5>Cumplimiento por sede</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblCumplimientoSede" runat="server"></asp:Literal>%
                </h1>
                <small>Promedio general</small>
            </div>
        </div>
    </div>

</div>
