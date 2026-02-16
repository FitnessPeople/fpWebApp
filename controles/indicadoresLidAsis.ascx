<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresLidAsis.ascx.cs" Inherits="fpWebApp.controles.indicadoresLidAsis" %>

<!-- Sweet Alert -->
<link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">
<!-- Sweet alert -->
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

<div class="row">

    <!-- 1. Afiliados Activos -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-primary">
                <h5>Afiliados Activos</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblTotalAfiliados" runat="server"></asp:Literal>
                </h1>
                <small>Base total activa</small>
            </div>
        </div>
    </div>

    <!-- 2. Historias del Mes -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-info">
                <h5>Historias Clínicas (Mes)</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblHistoriasMes" runat="server"></asp:Literal>
                </h1>
                <small>Valoraciones realizadas</small>
            </div>
        </div>
    </div>

    <!-- 3. Cobertura Clínica -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-success">
                <h5>% Cobertura Clínica</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblCobertura" runat="server"></asp:Literal>%
                </h1>
                <small>Afiliados con historia</small>
            </div>
        </div>
    </div>

    <!-- 4. IMC Promedio -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-warning">
                <h5>IMC Promedio</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblIMC" runat="server"></asp:Literal>
                </h1>
                <small>Estado nutricional general</small>
            </div>
        </div>
    </div>

</div>


<div class="row">

    <!-- 5. % Aptos -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-navy">
                <h5>% Aptos Actividad Física</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblAptos" runat="server"></asp:Literal>%
                </h1>
                <small>Evaluación fisioterapia</small>
            </div>
        </div>
    </div>

    <!-- 6. Incapacidades -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-danger">
                <h5>Incapacidades Activas</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblIncapacidades" runat="server"></asp:Literal>
                </h1>
                <small>Casos vigentes</small>
            </div>
        </div>
    </div>

    <!-- 7. Afiliados en Riesgo -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-muted">
                <h5>Afiliados en Riesgo</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblRiesgo" runat="server"></asp:Literal>
                </h1>
                <small>HTA / Diabetes / Sedentarismo</small>
            </div>
        </div>
    </div>

    <!-- 8. Ocupación Agenda -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-success">
                <h5>% Ocupación Agenda</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblOcupacion" runat="server"></asp:Literal>%
                </h1>
                <small>Mes actual</small>
            </div>
        </div>
    </div>

</div>
