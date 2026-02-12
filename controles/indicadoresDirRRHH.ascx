<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresDirRRHH.ascx.cs" Inherits="fpWebApp.controles.indicadoresDirRRHH" %>

<!-- Sweet Alert -->
<link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">
<!-- Sweet alert -->
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

<div class="row">

    <!-- 1 -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-primary">
                <h5>Empleados Activos</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblTotalActivos" runat="server"></asp:Literal>
                </h1>
                <small>Personal actual activo</small>
            </div>
        </div>
    </div>

    <!-- 2 -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-danger">
                <h5>Rotación del Mes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblRotacionMes" runat="server"></asp:Literal>%
                </h1>
                <small>Salidas vs total activo</small>
            </div>
        </div>
    </div>

    <!-- 3 -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-success">
                <h5>Nuevas Contrataciones</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblIngresosMes" runat="server"></asp:Literal>
                </h1>
                <small>Ingresos del mes</small>
            </div>
        </div>
    </div>

    <!-- 4 -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-warning">
                <h5>Costo Nómina</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    $<asp:Literal ID="lblNomina" runat="server"></asp:Literal>
                </h1>
                <small>Total mensual actual</small>
            </div>
        </div>
    </div>

</div>

<div class="row">

    <!-- 5 -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-info">
                <h5>Antigüedad Promedio</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblAntiguedad" runat="server"></asp:Literal>
                </h1>
                <small>Años promedio</small>
            </div>
        </div>
    </div>

    <!-- 6 -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-success">
                <h5>Índice Profesionalización</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblProfesionalizacion" runat="server"></asp:Literal>%
                </h1>
                <small>Nivel educativo alto</small>
            </div>
        </div>
    </div>

    <!-- 7 -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-primary">
                <h5>🔥 Índice de Estabilidad</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblEstabilidad" runat="server"></asp:Literal>%
                </h1>
                <small>Más de 1 año en empresa</small>
            </div>
        </div>
    </div>

    <!-- 8 -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-info">
                <h5>Sedes con Personal</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblSedesActivas" runat="server"></asp:Literal>
                </h1>
                <small>Sedes con empleados activos</small>
            </div>
        </div>
    </div>

</div>
