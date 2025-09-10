<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresusucrm2.ascx.cs" Inherits="fpWebApp.controles.indicadoresusucrm2" %>




            <div class="row d-flex">
                <!-- Gráfica -->
                <div class="col-lg-6 d-flex">
                    <div class="ibox flex-fill w-100">
                        <div class="ibox-content">
                            <div>
                                <span class="pull-right text-right">
                                    <small>Valor promedio de ventas del último mes en: <strong>Online</strong></small>
                                    <br />
                                    Total ventas: 19'162,862
                                </span>
                                <h1 class="m-b-xs">$ 5'098,992</h1>
                                <h3 class="font-bold no-margins">Objetivo mes julio</h3>
                                <small>..</small>
                            </div>

                            <div>
                                <canvas id="lineChart" height="70"></canvas>
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
                            <h5>Vendido hasta hoy</h5>
                            <h1 class="no-margins">
                                <asp:Literal ID="ltVendidoMes" runat="server"></asp:Literal>
                            </h1>
                            <div class="stat-percent font-bold text-navy">98% <i class="fa fa-bolt"></i></div>
                            <small>Cumplimiento</small>
                        </div>
                    </div>
                </div>
                <div class="col-lg-2 d-flex mt-3">
                    <div class="ibox flex-fill w-100">
                        <div class="ibox-content">
                            <h5>Brecha hoy</h5>
                            <h1 class="no-margins">-200,100</h1>
                            <div class="stat-percent font-bold text-danger">12% <i class="fa fa-level-down"></i></div>
                            <small>Proyectado</small>
                        </div>
                    </div>
                </div>
                <div class="col-lg-2 d-flex mt-3">
                    <div class="ibox flex-fill w-100">
                        <div class="ibox-content">
                            <h5>Brecha mes</h5>
                            <h1 class="no-margins">-200,100</h1>
                            <div class="stat-percent font-bold text-danger">12% <i class="fa fa-level-down"></i></div>
                            <small>Otros</small>
                        </div>
                    </div>
                </div>
            </div>



            <!-- jQuery UI -->
            <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>

            <!-- Jvectormap -->
            <script src="js/plugins/jvectormap/jquery-jvectormap-2.0.2.min.js"></script>
            <script src="js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"></script>

            <!-- Sparkline -->
            <script src="js/plugins/sparkline/jquery.sparkline.min.js"></script>

            <!-- Sparkline demo data  -->
            <script src="js/demo/sparkline-demo.js"></script>

            <!-- ChartJS-->
            <script src="js/plugins/chartJs/Chart.min.js"></script>

            <script>
                $(document).ready(function () {

                    var lineData = {
                        labels: ["January", "February", "March", "April", "May", "June", "July"],
                        datasets: [
                            {
                                label: "Ventas completadas",
                                backgroundColor: "rgba(26,179,148,0.5)",
                                borderColor: "rgba(26,179,148,0.7)",
                                pointBackgroundColor: "rgba(26,179,148,1)",
                                pointBorderColor: "#fff",
                                data: [280, 480, 400, 190, 860, 270, 900]
                            },
                            {
                                label: "Meta propuesta",
                                backgroundColor: "rgba(220,220,220,0.5)",
                                borderColor: "rgba(220,220,220,1)",
                                pointBackgroundColor: "rgba(220,220,220,1)",
                                pointBorderColor: "#fff",
                                data: [650, 590, 800, 810, 560, 550, 400]
                            }
                        ]
                    };

                    var lineOptions = {
                        responsive: true
                    };


                    var ctx = document.getElementById("lineChart").getContext("2d");
                    new Chart(ctx, { type: 'line', data: lineData, options: lineOptions });

                });
            </script>

