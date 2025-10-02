<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresusucrm2.ascx.cs" Inherits="fpWebApp.controles.indicadoresusucrm2" %>

<style>
    .valor-numero {
        font-size: 22px;
        font-family: 'Helvetica', monospace;
        text-align: left; /*  cambia aquí */
        line-height: 1.2;
        white-space: nowrap;
    }

    h2.valor-numero {
        font-size: 22px !important;
        margin: 0;
    }
</style>


<div class="row d-flex">
     
   <%-- Gráfica--%>
    <div class="col-lg-6 d-flex">
        <div class="ibox flex-fill w-100">
            <div class="ibox-content">
                <div>
                    <span class="pull-right text-right">
                        <small>
                             <asp:Literal ID="ltFechaHoy" runat="server"></asp:Literal><br />
                            Objetivo hoy:
                            <asp:Literal ID="ltValorMetaAsesorHoy" runat="server"></asp:Literal></small>
                            <br />
                            <small>Vendido hoy:
                            <asp:Literal ID="ltVendidoDia" runat="server"></asp:Literal></small> <br />                           
                    </span>



                    <h1 class="m-b-xs">
                        <asp:Literal ID="ltValorMetaAsesorMes" runat="server"></asp:Literal>
                    </h1>
                    <h3 class="font-bold no-margins">
                        <asp:Literal ID="ltNomMesActual" runat="server"></asp:Literal>
                    </h3>
                    <small>..</small>
                </div>

                <div>
                    <canvas id="CRMlineChart" height="75"></canvas>
                </div>

            </div>
        </div>
    </div>

    <!-- Widgets -->
    <div class="col-lg-6 d-flex flex-column">
        <!-- Fila 1 de widgets -->
        <div class="row flex-fill mb-2">
            <div class="col-lg-4 d-flex">
                <div class="widget style1 navy-bg flex-fill w-100"
                    title="Cantidad de contactos">
                    <div class="row vertical-align">
                        <div class="col-xs-3">
                            <i class="fa fa-user fa-3x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <h2 class="font-bold">
                                <asp:Literal ID="ltNumContactos" runat="server"></asp:Literal>
                            </h2>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 d-flex">
                <div class="widget style1 navy-bg flex-fill w-100"
                    title="Cantidad de negociaciones aceptadas">
                    <div class="row vertical-align">
                        <div class="col-xs-3">
                            <i class="fa fa-handshake fa-3x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <h2 class="font-bold">
                                <asp:Literal ID="ltNumNegociacionAceptada" runat="server"></asp:Literal>

                            </h2>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 d-flex">
                <div class="widget style1 yellow-bg flex-fill w-100"
                    title="Cantidad de propuestas en gestión">
                    <div class="row vertical-align">
                        <div class="col-xs-3">
                            <i class="fa fa-paper-plane fa-3x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <h2 class="font-bold">
                                <asp:Literal ID="ltNumEnNegociacion" runat="server"></asp:Literal>
                            </h2>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <!-- Fila 2 de widgets -->
        <div class="row flex-fill">
            <div class="col-lg-4 d-flex">
                <div class="widget style1 lazur-bg flex-fill w-100"
                    title="Cantidad de leads en caliente">
                    <div class="row vertical-align">
                        <div class="col-xs-3">
                            <i class="fa-solid fa-fire fa-3x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <h2 class="font-bold">
                                <asp:Literal ID="ltNumCaliente" runat="server"></asp:Literal></h2>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 d-flex">
                <div class="widget style1 lazur-bg flex-fill w-100"
                    title="Cantidad de leads en tibio">
                    <div class="row vertical-align">
                        <div class="col-xs-3">
                            <i class="fa-solid fa-mug-hot fa-3x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <h2 class="font-bold">
                                <asp:Literal ID="ltNumTibio" runat="server"></asp:Literal></h2>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-4 d-flex">
                <div class="widget style1 navy-bg flex-fill w-100"
                    title="Cantidad de leads en frío">
                    <div class="row vertical-align">
                        <div class="col-xs-3">
                            <i class="fa-solid fa-snowflake fa-3x"></i>
                        </div>
                        <div class="col-xs-9 text-right">
                            <h2 class="font-bold">
                                <asp:Literal ID="ltNumFrio" runat="server"></asp:Literal></h2>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Otros ibox -->
    <div class="col-lg-2 d-flex mt-3">
        <div class="ibox flex-fill w-100">
            <div class="ibox-content">
                <h5>Vendido del mes</h5>
                <div class="valor-numero">
                    <asp:Literal ID="ltVendidoMes" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>

    <div class="col-lg-2 d-flex mt-3">
        <div class="ibox flex-fill w-100">
            <div class="ibox-content">
                <h5>Brecha mes</h5>
                <div class="valor-numero">
                    <asp:Literal ID="ltBrechaMes" runat="server"></asp:Literal>
                </div>
                <div class="stat-percent font-bold text-danger">
                    12% <i class="fa fa-level-down"></i>
                    <small>Otros</small>
                </div>
            </div>
        </div>
    </div>

        <div class="col-lg-2 d-flex mt-3">
        <div class="ibox flex-fill w-100">
            <div class="ibox-content">
                <h5>Brecha hoy</h5>
                <div class="valor-numero">
                    <asp:Literal ID="ltBrechaHoy" runat="server"></asp:Literal>
                </div>
                <div class="stat-percent font-bold text-danger">
                    12% <i class="fa fa-level-down"></i>
                </div>
            </div>
        </div>
    </div>

</div>


<!-- EayPIE -->
<script src="js/plugins/easypiechart/jquery.easypiechart.js"></script>

<!-- Sparkline -->
<script src="js/plugins/sparkline/jquery.sparkline.min.js"></script>


<!-- ChartJS-->
<script src="js/plugins/chartJs/Chart.min.js"></script>

<!-- Toastr -->
<script src="js/plugins/toastr/toastr.min.js"></script>

<script>
    $(document).ready(function () {
        var lineData = {
            labels: <%= labelsJson %>,
            datasets: [
                {
                    label: "Ventas",
                    backgroundColor: "rgba(26,179,148,0.5)",
                    borderColor: "rgba(26,179,148,0.7)",
                    pointBackgroundColor: "rgba(26,179,148,1)",
                    pointBorderColor: "#fff",
                    data: <%= ventasJson %>
                },
                    {
                        label: "Metas",
                        backgroundColor: "rgba(220,220,220,0.5)",
                        borderColor: "rgba(220,220,220,1)",
                        pointBackgroundColor: "rgba(220,220,220,1)",
                        pointBorderColor: "#fff",
                        data: <%= metasJson %>
                }
            ]
        };

        var lineOptions = { responsive: true };
        var ctx = document.getElementById("CRMlineChart").getContext("2d");
        new Chart(ctx, { type: 'line', data: lineData, options: lineOptions });
    });
</script>

<script>
    $(document).ready(function () {
        var lineData = {
            labels: <%= labelsJson %>,
                datasets: [
                    {
                        label: "Ventas",
                        backgroundColor: "rgba(26,179,148,0.5)",
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: <%= ventasJson %> // números, no texto
                },
                {
                    label: "Metas",
                    backgroundColor: "rgba(220,220,220,0.5)",
                    borderColor: "rgba(220,220,220,1)",
                    pointBackgroundColor: "rgba(220,220,220,1)",
                    pointBorderColor: "#fff",
                    data: <%= metasJson %> // números, no texto
                }
            ]
        };

        var lineOptions = {
            responsive: true,
            maintainAspectRatio: false,
            tooltips: {
                callbacks: {
                    label: function (tooltipItem, data) {
                        var value = tooltipItem.yLabel;
                        return value.toLocaleString('es-CO', {
                            style: 'currency',
                            currency: 'COP',
                            minimumFractionDigits: 0
                        });
                    }
                }
            },
            scales: {
                yAxes: [{
                    ticks: {
                        callback: function (value) {
                            return value.toLocaleString('es-CO', {
                                style: 'currency',
                                currency: 'COP',
                                minimumFractionDigits: 0
                            });
                        }
                    }
                }]
            }
        };
        var ctx = document.getElementById("CRMlineChart").getContext("2d");
        new Chart(ctx, { type: 'line', data: lineData, options: lineOptions });
    });
</script>




