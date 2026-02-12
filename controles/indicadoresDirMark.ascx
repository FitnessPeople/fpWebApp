<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresDirMark.ascx.cs" Inherits="fpWebApp.controles.indicadoresDirMark" %>

<!-- Sweet Alert -->
<link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">
<!-- Sweet alert -->
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

<div class="row">

    <!-- CARD 1: Ventas Marketing -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-primary">
                <h5>Ventas Marketing (Mes)</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    $<asp:Literal ID="lblVentasMarketingMes" runat="server"></asp:Literal>
                </h1>
                <small>Ventas durante campañas activas</small>
            </div>
        </div>
    </div>

    <!-- CARD 2: Campaña Top -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-info">
                <h5>Campaña Top del Mes</h5>
            </div>
            <div class="ibox-content">
                <h2 class="no-margins">
                    <asp:Literal ID="lblCampaniaTop" runat="server"></asp:Literal>
                </h2>
                <small>Mayor impacto en ventas</small>
            </div>
        </div>
    </div>

    <!-- CARD 3: ROI Marketing -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-success">
                <h5>ROI Marketing</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblRoiMarketingMes" runat="server"></asp:Literal>%
                </h1>
                <small>Retorno sobre inversión</small>
            </div>
        </div>
    </div>

    <!-- CARD 4: Crecimiento -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-warning">
                <h5>Crecimiento vs Mes Anterior</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblCrecimientoMarketing" runat="server"></asp:Literal>%
                </h1>
                <small>Evolución mensual</small>
            </div>
        </div>
    </div>

</div>

