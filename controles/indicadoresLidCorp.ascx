<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresLidCorp.ascx.cs" Inherits="fpWebApp.controles.indicadoresLidCorp" %>

<!-- Sweet Alert -->
<link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">
<!-- Sweet alert -->
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

<div class="row">

    <!-- 1 Venta Total -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-primary"><h5>Venta Total Mes</h5></div>
            <div class="ibox-content">
                <h1><asp:Literal ID="lblVentaTotal" runat="server" /></h1>
                <small>Pagos aprobados</small>
            </div>
        </div>
    </div>

    <!-- 2 Venta Mes Anterior -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-info"><h5>Venta Mes Anterior</h5></div>
            <div class="ibox-content">
                <h1><asp:Literal ID="lblVentaMesAnterior" runat="server" /></h1>
                <small>Base comparativa</small>
            </div>
        </div>
    </div>

    <!-- 3 Variación -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-warning"><h5>Variación</h5></div>
            <div class="ibox-content">
                <h1><asp:Literal ID="lblVariacion" runat="server" />%</h1>
                <small>Vs mes anterior</small>
            </div>
        </div>
    </div>

    <!-- 4 Proyección -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-danger"><h5>Proyección Cierre</h5></div>
            <div class="ibox-content">
                <h1><asp:Literal ID="lblProyeccion" runat="server" /></h1>
                <small>Ritmo actual</small>
            </div>
        </div>
    </div>

</div>
<div class="row">

    <!-- 5 Ritmo Real -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-primary"><h5>Ritmo Real</h5></div>
            <div class="ibox-content">
                <h1><asp:Literal ID="lblRitmoReal" runat="server" />%</h1>
                <small>Sobre meta</small>
            </div>
        </div>
    </div>

    <!-- 6 Ritmo Esperado -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-info"><h5>Ritmo Esperado</h5></div>
            <div class="ibox-content">
                <h1><asp:Literal ID="lblRitmoEsperado" runat="server" />%</h1>
                <small>Según calendario</small>
            </div>
        </div>
    </div>

    <!-- 7 Días Restantes -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-warning"><h5>Días Restantes</h5></div>
            <div class="ibox-content">
                <h1><asp:Literal ID="lblDiasRestantes" runat="server" /></h1>
                <small>Para cerrar meta</small>
            </div>
        </div>
    </div>

    <!-- 8 Presión Comercial -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-danger"><h5>Presión Comercial</h5></div>
            <div class="ibox-content">
                <h1><asp:Literal ID="lblPresion" runat="server" />x</h1>
                <small>Aceleración necesaria</small>
            </div>
        </div>
    </div>

</div>
<div class="row">

    <!-- 9 Recuperación -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-success"><h5>Recuperación Cartera</h5></div>
            <div class="ibox-content">
                <h1><asp:Literal ID="lblRecuperacion" runat="server" /></h1>
                <small>Recaudo del mes</small>
            </div>
        </div>
    </div>

    <!-- 10 Visitas Efectivas -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-primary"><h5>Visitas Efectivas</h5></div>
            <div class="ibox-content">
                <h1><asp:Literal ID="lblVisitasEfectivas" runat="server" /></h1>
                <small>Atendidas</small>
            </div>
        </div>
    </div>

    <!-- 11 Tasa Efectividad -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-info"><h5>Tasa Efectividad</h5></div>
            <div class="ibox-content">
                <h1><asp:Literal ID="lblTasaEfectividad" runat="server" />%</h1>
                <small>Negociadas / Atendidas</small>
            </div>
        </div>
    </div>

    <!-- 12 Venta Directa -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-warning"><h5>Venta Directa</h5></div>
            <div class="ibox-content">
                <h1><asp:Literal ID="lblVentaDirecta" runat="server" /></h1>
                <small>Online / Efectivo</small>
            </div>
        </div>
    </div>

</div>
