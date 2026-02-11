<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresAseCom.ascx.cs" Inherits="fpWebApp.controles.indicadoresAseCom" %>

<div class="row">

    <!-- 1️⃣ Cumplimiento del Mes -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-success">
                <h5>Cumplimiento del mes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblRitmoReal" runat="server"></asp:Literal>%
                </h1>
                <small>
                    Ritmo esperado:
                    <asp:Literal ID="lblRitmoEsperado" runat="server"></asp:Literal>%
                </small>
            </div>
        </div>
    </div>

    <!-- 2️⃣ Proyección -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-primary">
                <h5>Proyección cierre mes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    $ <asp:Literal ID="lblProyeccionCierre" runat="server"></asp:Literal>
                </h1>
                <small>
                    Meta del mes:
                    $ <asp:Literal ID="lblMetaMes" runat="server"></asp:Literal>
                </small>
            </div>
        </div>
    </div>

    <!-- 3️⃣ Días restantes -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-info">
                <h5>Días restantes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblDiasHabiles" runat="server"></asp:Literal>
                </h1>
                <small>
                    Necesario por día:
                    $ <asp:Literal ID="lblVentaNecesariaDia" runat="server"></asp:Literal>
                </small>
            </div>
        </div>
    </div>

    <!-- 4️⃣ Presión -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-danger">
                <h5>Presión comercial</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblPresion" runat="server"></asp:Literal>x
                </h1>
                <small>vs promedio actual</small>
            </div>
        </div>
    </div>

</div>

<div class="row">

    <!-- 5️⃣ Ranking -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-warning">
                <h5>Ranking del mes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    #<asp:Literal ID="lblRankingActual" runat="server"></asp:Literal>
                </h1>
                <small>Posición actual</small>
            </div>
        </div>
    </div>

    <!-- 6️⃣ Ventas acumuladas -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-success">
                <h5>Ventas acumuladas</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    $ <asp:Literal ID="lblVentasAcumuladas" runat="server"></asp:Literal>
                </h1>
                <small>Total mes actual</small>
            </div>
        </div>
    </div>

    <!-- 7️⃣ Diferencia -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-danger">
                <h5>Diferencia vs meta</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    $ <asp:Literal ID="lblDiferenciaMeta" runat="server"></asp:Literal>
                </h1>
                <small>Excedente / Faltante</small>
            </div>
        </div>
    </div>

    <!-- 8️⃣ Promedio diario -->
    <div class="col-lg-3">
        <div class="ibox">
            <div class="ibox-title text-info">
                <h5>Promedio diario</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    $ <asp:Literal ID="lblPromedioDiario" runat="server"></asp:Literal>
                </h1>
                <small>Promedio actual del mes</small>
            </div>
        </div>
    </div>

</div>


