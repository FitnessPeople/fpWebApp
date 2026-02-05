<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresAdmSede.ascx.cs" Inherits="fpWebApp.controles.indicadoresAdmSede" %>
<div class="row">

    <!-- CARD 1: Cumplimiento -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-success">
                <h5>Cumplimiento del mes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblCumplimientoMes" runat="server"></asp:Literal>%
                </h1>
                <small>Ventas vs Meta</small>
            </div>
        </div>
    </div>

    <!-- CARD 2: Ventas -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-primary">
                <h5>Ventas del mes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    $<asp:Literal ID="lblVentasMes" runat="server"></asp:Literal>
                </h1>
                <small>Acumulado a la fecha</small>
            </div>
        </div>
    </div>

    <!-- CARD 3: Meta -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-warning">
                <h5>Meta del mes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    $<asp:Literal ID="lblMetaMes" runat="server"></asp:Literal>
                </h1>
                <small>Meta asignada a la sede</small>
            </div>
        </div>
    </div>

    <!-- CARD 4: Ticket promedio -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-info">
                <h5>Ticket promedio</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    $<asp:Literal ID="lblTicketPromedio" runat="server"></asp:Literal>
                </h1>
                <small>Promedio por venta</small>
            </div>
        </div>
    </div>

</div>
