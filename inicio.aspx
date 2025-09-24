<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="inicio.aspx.cs" Inherits="fpWebApp.inicio" %>

<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%--<%@ Register Src="~/controles/indicadores02.ascx" TagPrefix="uc1" TagName="indicadores02" %>--%>
<%@ Register Src="~/controles/grafico01.ascx" TagPrefix="uc1" TagName="grafico01" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Dashboard</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <!-- jQuery UI -->
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>

    <!-- Toastr style -->
    <link href="css/plugins/toastr/toastr.min.css" rel="stylesheet">

    <!-- Flot -->
    <script src="js/plugins/flot/jquery.flot.js"></script>
    <script src="js/plugins/flot/jquery.flot.tooltip.min.js"></script>
    <script src="js/plugins/flot/jquery.flot.spline.js"></script>
    <script src="js/plugins/flot/jquery.flot.resize.js"></script>
    <script src="js/plugins/flot/jquery.flot.pie.js"></script>
    <script src="js/plugins/flot/jquery.flot.symbol.js"></script>
    <script src="js/plugins/flot/jquery.flot.time.js"></script>

    <script>
        function changeClass() {
            var element = document.querySelector("#inicio");
            element.classList.replace("old", "active");
        }
    </script>
</head>

<body onload="changeClass()">

    <div id="wrapper">

        <uc1:navbar runat="server" ID="navbar1" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header1" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">
                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa fa-gauge text-success m-r-sm"></i>Dashboard</h2>
                    <ol class="breadcrumb">
                        <li>
                            <a href="inicio">Inicio</a>
                        </li>
                        <li class="active">
                            <strong>Dashboard</strong><asp:Literal runat="server" ID="ltMsg"></asp:Literal>
                        </li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row animated fadeInDown">
            <%--<div class="fh-breadcrumb">
                <div class="full-height">
                    <div class="full-height-scroll border-left">
                        <div class="element-detail-box">--%>
                            <%--Inicio Contenido!!!!--%>
                            <asp:PlaceHolder ID="phIndicadores" runat="server"></asp:PlaceHolder>
                            <%--<uc1:indicadores02 runat="server" ID="indicadores02" />--%>

                            <uc1:grafico01 runat="server" ID="grafico01" />

                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="ibox float-e-margins">
                                        <div class="ibox-title">
                                            <h5>Mensajes</h5>
                                            <div class="ibox-tools">
                                                <a class="collapse-link">
                                                    <i class="fa fa-chevron-up"></i>
                                                </a>
                                                <a class="close-link">
                                                    <i class="fa fa-times"></i>
                                                </a>
                                            </div>
                                        </div>
                                        <div class="ibox-content ibox-heading">
                                            <h3><i class="fa fa-envelope-o"></i>Nuevos mensajes</h3>
                                            <small><i class="fa fa-tim"></i>Tienes 22 mensajes nuevos y 16 en espera en la carpeta de borradores.</small>
                                        </div>
                                        <div class="ibox-content">
                                            <div class="feed-activity-list">

                                                <div class="feed-element">
                                                    <div>
                                                        <small class="pull-right text-navy">Hace 1m</small>
                                                        <strong>Monica Smith</strong>
                                                        <div>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum</div>
                                                        <small class="text-muted">Hoy 5:60 pm - 12.06.2024</small>
                                                    </div>
                                                </div>

                                                <div class="feed-element">
                                                    <div>
                                                        <small class="pull-right">Hace 2m</small>
                                                        <strong>Jogn Angel</strong>
                                                        <div>There are many variations of passages of Lorem Ipsum available</div>
                                                        <small class="text-muted">Hoy 2:23 pm - 11.06.2024</small>
                                                    </div>
                                                </div>

                                                <div class="feed-element">
                                                    <div>
                                                        <small class="pull-right">Hace 5m</small>
                                                        <strong>Jesica Ocean</strong>
                                                        <div>Contrary to popular belief, Lorem Ipsum</div>
                                                        <small class="text-muted">Hoy 1:00 pm - 08.06.2024</small>
                                                    </div>
                                                </div>

                                                <div class="feed-element">
                                                    <div>
                                                        <small class="pull-right">Hace 5m</small>
                                                        <strong>Monica Jackson</strong>
                                                        <div>The generated Lorem Ipsum is therefore </div>
                                                        <small class="text-muted">Ayer 8:48 pm - 10.06.2024</small>
                                                    </div>
                                                </div>


                                                <div class="feed-element">
                                                    <div>
                                                        <small class="pull-right">Hace 5m</small>
                                                        <strong>Anna Legend</strong>
                                                        <div>All the Lorem Ipsum generators on the Internet tend to repeat </div>
                                                        <small class="text-muted">Ayer 8:48 pm - 10.06.2024</small>
                                                    </div>
                                                </div>
                                                <div class="feed-element">
                                                    <div>
                                                        <small class="pull-right">Hace 5m</small>
                                                        <strong>Damian Nowak</strong>
                                                        <div>The standard chunk of Lorem Ipsum used </div>
                                                        <small class="text-muted">Ayer 8:48 pm - 10.06.2024</small>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-8">

                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="ibox float-e-margins">
                                                <div class="ibox-title">
                                                    <span class="label label-warning pull-right">Datos actualizados</span>
                                                    <h5>Actividad del sistema</h5>
                                                </div>
                                                <div class="ibox-content">
                                                    <div class="row">
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Páginas / Visitas</small>
                                                            <h4>236 321.80</h4>
                                                        </div>

                                                        <div class="col-xs-4">
                                                            <small class="stats-label">% Nuevas Visitas</small>
                                                            <h4>46.11%</h4>
                                                        </div>
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Última semana</small>
                                                            <h4>432.021</h4>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="ibox-content">
                                                    <div class="row">
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Páginas / Visitas</small>
                                                            <h4>643 321.10</h4>
                                                        </div>

                                                        <div class="col-xs-4">
                                                            <small class="stats-label">% Nuevas Visitas</small>
                                                            <h4>92.43%</h4>
                                                        </div>
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Última semana</small>
                                                            <h4>564.554</h4>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="ibox-content">
                                                    <div class="row">
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Páginas / Visitas</small>
                                                            <h4>436 547.20</h4>
                                                        </div>

                                                        <div class="col-xs-4">
                                                            <small class="stats-label">% Nuevas Visitas</small>
                                                            <h4>150.23%</h4>
                                                        </div>
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Última semana</small>
                                                            <h4>124.990</h4>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="ibox float-e-margins">
                                                <div class="ibox-title">
                                                    <h5>Lista de Actividades</h5>
                                                    <div class="ibox-tools">
                                                        <a class="collapse-link">
                                                            <i class="fa fa-chevron-up"></i>
                                                        </a>
                                                        <a class="close-link">
                                                            <i class="fa fa-times"></i>
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="ibox-content">
                                                    <ul class="todo-list m-t small-list">
                                                        <li>
                                                            <a href="#" class="check-link"><i class="fa fa-check-square"></i></a>
                                                            <span class="m-l-xs todo-completed">Lanzamiento promociones.</span>

                                                        </li>
                                                        <li>
                                                            <a href="#" class="check-link"><i class="fa fa-square-o"></i></a>
                                                            <span class="m-l-xs">Agregar planes comerciales.</span>

                                                        </li>
                                                        <li>
                                                            <a href="#" class="check-link"><i class="fa fa-square-o"></i></a>
                                                            <span class="m-l-xs">Send documents to Mike</span>
                                                            <small class="label label-primary"><i class="fa fa-clock-o"></i>1 mins</small>
                                                        </li>
                                                        <li>
                                                            <a href="#" class="check-link"><i class="fa fa-square-o"></i></a>
                                                            <span class="m-l-xs">Go to the doctor dr Smith</span>
                                                        </li>
                                                        <li>
                                                            <a href="#" class="check-link"><i class="fa fa-check-square"></i></a>
                                                            <span class="m-l-xs todo-completed">Plan vacation</span>
                                                        </li>
                                                        <li>
                                                            <a href="#" class="check-link"><i class="fa fa-square-o"></i></a>
                                                            <span class="m-l-xs">Create Nuevas stuff</span>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="ibox float-e-margins">
                                                <div class="ibox-content">
                                                    <div>
                                                        <span class="pull-right text-right">
                                                            <small>Valor promedio de las ventas del ultimo mes: <strong>FP</strong></small>
                                                            <br />
                                                            Ventas totales: 162,862
                        </span>
                                                        <h3 class="font-bold no-margins">Margen de ingresos semestral
            </h3>
                                                        <small>Marketing de ventas.</small>
                                                    </div>

                                                    <div class="m-t-sm">

                                                        <div class="row">
                                                            <div class="col-md-8">
                                                                <div>
                                                                    <canvas id="lineChart" height="114"></canvas>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <ul class="stat-list m-t-lg">
                                                                    <li>
                                                                        <h2 class="no-margins">2,346</h2>
                                                                        <small>Solicitudes del periodo</small>
                                                                        <div class="progress progress-mini">
                                                                            <div class="progress-bar" style="width: 48%;"></div>
                                                                        </div>
                                                                    </li>
                                                                    <li>
                                                                        <h2 class="no-margins ">4,422</h2>
                                                                        <small>Solicitudes ultimo mes</small>
                                                                        <div class="progress progress-mini">
                                                                            <div class="progress-bar" style="width: 60%;"></div>
                                                                        </div>
                                                                    </li>
                                                                </ul>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="m-t-md">
                                                        <small class="pull-right">
                                                            <i class="fa fa-clock-o"></i>
                                                            Actualizado el 03.10.2024
            </small>
                                                        <small>
                                                            <strong>Analisis de venta:</strong> The value has been changed over time, and last month reached a level over $50,000.
            </small>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--Fin Contenido!!!!--%>
                        <%--</div>
                    </div>--%>
                </div>
            </div>

            <uc1:footer runat="server" ID="footer1" />

        </div>
        <uc1:rightsidebar runat="server" ID="rightsidebar1" />
    </div>

    <!-- Mainly scripts -->
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- Peity -->
    <script src="js/plugins/peity/jquery.peity.min.js"></script>
    <script src="js/demo/peity-demo.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- EayPIE -->
    <script src="js/plugins/easypiechart/jquery.easypiechart.js"></script>

    <!-- Sparkline -->
    <script src="js/plugins/sparkline/jquery.sparkline.min.js"></script>

    <!-- Sparkline demo data  -->
    <script src="js/demo/sparkline-demo.js"></script>

    <!-- ChartJS-->
    <script src="js/plugins/chartJs/Chart.min.js"></script>

    <!-- Toastr -->
    <script src="js/plugins/toastr/toastr.min.js"></script>

    <script>
        $('.chart').easyPieChart({
            barColor: '#f8ac59',
            //scaleColor: false,
            scaleLength: 5,
            liNuevasidth: 4,
            size: 80
        });

        $('.chart2').easyPieChart({
            barColor: '#1c84c6',
            //scaleColor: false,
            scaleLength: 5,
            liNuevasidth: 4,
            size: 80
        });

        $(document).ready(function () {
            var lineData = {
                labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio"],
                datasets: [
                    {
                        label: "Afiliados",
                        backgroundColor: "rgba(26,179,148,0.5)",
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: [48, 48, 60, 39, 56, 37, 30]
                    },
                    {
                        label: "Empresas",
                        backgroundColor: "rgba(220,220,220,0.5)",
                        borderColor: "rgba(220,220,220,1)",
                        pointBackgroundColor: "rgba(220,220,220,1)",
                        pointBorderColor: "#fff",
                        data: [65, 59, 40, 51, 36, 25, 40]
                    }
                ]
            };

            var lineOptions = {
                responsive: true
            };


            var ctx = document.getElementById("lineChart").getContext("2d");
            new Chart(ctx, { type: 'line', data: lineData, options: lineOptions });

            setTimeout(function () {
                toastr.options = {
                    closeButton: true,
                    progressBar: true,
                    showMethod: 'slideDown',
                    timeOut: 1000
                };
                //toastr.error('<%=strDiaZero%> días y contando...', 'DIA ZERO');
            }, 1300);

            setTimeout(function () {
                toastr.options = {
                    closeButton: true,
                    progressBar: true,
                    showMethod: 'slideDown',
                    timeOut: 3000
                };
                toastr.success('Puedes usar CTRL+L para bloquear la aplicación', 'Píldora');
            }, 2600);
        });
    </script>
    <script src="https://cdn.jsdelivr.net/npm/js-confetti@latest/dist/js-confetti.browser.js"></script>
    <script type="text/javascript">
        const jsConfetti = new JSConfetti()
        //jsConfetti.addConfetti()
    </script>

</body>

</html>