<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="calendariofpadmin.aspx.cs" Inherits="fpWebApp.calendariofpadmin" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Usuarios</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/fullcalendar/fullcalendar.css" rel="stylesheet">
    <link href="css/plugins/fullcalendar/fullcalendar.print.css" rel='stylesheet' media='print'>

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
</head>

<body class="">

    <div id="wrapper">
        <uc1:navbar runat="server" ID="navbar" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">
                <div class="col-sm-4">
                    <h2><i class="fa fa-calendar text-success m-r-sm"></i>Calendario de Actividades</h2>
                    <ol class="breadcrumb">
                        <li>
                            <a href="inicio.aspx">Inicio</a>
                        </li>
                        <li class="active">
                            <strong>Calendario</strong>
                        </li>
                    </ol>
                </div>
                <div class="col-sm-8">
                    <%--<div class="title-action">
                        <a href="" class="btn btn-primary">This is action area</a>
                    </div>--%>
                </div>
            </div>

            <div class="wrapper wrapper-content animated fadeInRight">

                <div class="row animated fadeInDown">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Calendario </h5>
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
                                <div id="calendar"></div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <uc1:footer runat="server" ID="footer" />
        </div>
        <uc1:rightsidebar runat="server" ID="rightsidebar" />
    </div>

    <!-- Mainly scripts -->
    <script src="js/plugins/fullcalendar/moment.min.js"></script>
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- jQuery UI  -->
    <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>

    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js"></script>

    <!-- Full Calendar -->
    <script src="js/plugins/fullcalendar/fullcalendar.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@fullcalendar/core@6.1.15/locales-all.global.min.js"></script>

    <script>

        $(document).ready(function () {

            /* initialize the calendar
             -----------------------------------------------------------------*/
            var initialLocaleCode = 'es';
            var date = new Date();
            var d = date.getDate();
            var m = date.getMonth();
            var y = date.getFullYear();

            $('#calendar').fullCalendar({
                locale: initialLocaleCode,
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                allDayText: 'Todo\r\nel día',
                buttonText: {
                    prev: '',
                    next: '',
                    prevYear: 'Año anterior',
                    nextYear: 'Año siguiente',
                    year: 'Año',
                    today: 'Hoy',
                    month: 'Mes',
                    week: 'Semana',
                    day: 'Dia',
                    list: 'Lista',
                    listWeek: 'Lista',
                },
                editable: false,
                droppable: false, // this allows things to be dropped onto the calendar
                drop: function () {
                    // is the "remove after drop" checkbox checked?
                    if ($('#drop-remove').is(':checked')) {
                        // if so, remove the element from the "Draggable Events" list
                        $(this).remove();
                    }
                },
                events: [
                    {
                        title: 'Reunión Equipo Sistemas',
                        start: new Date(2025, 3, 7, 8, 0),
                        end: new Date(2025, 3, 7, 8, 30),
                        color: '#DBADFF'
                    },
                    {
                        title: 'Inducción Carlos Rivera',
                        start: new Date(2025, 3, 7, 8, 30),
                        end: new Date(2025, 3, 7, 9, 0)
                    },
                    {
                        title: 'Instalación equipo Carlos Rivera',
                        start: new Date(2025, 3, 7, 9, 0),
                        end: new Date(2025, 3, 7, 11, 0)
                    },
                    {
                        title: 'Revisión de avances',
                        start: new Date(2025, 3, 11, 11, 0),
                        end: new Date(2025, 3, 11, 12, 0),
                        color: '#DBADFF'
                    },
                    {
                        title: 'Revisión de avances',
                        start: new Date(2025, 3, 16, 11, 0),
                        end: new Date(2025, 3, 16, 12, 0),
                        color: '#DBADFF'
                    },
                    {
                        title: 'Revisión de avances',
                        start: new Date(2025, 3, 25, 11, 0),
                        end: new Date(2025, 3, 25, 12, 0),
                        color: '#DBADFF'
                    },
                    {
                        title: 'Revisión de avances',
                        start: new Date(2025, 3, 30, 11, 0),
                        end: new Date(2025, 3, 30, 12, 0),
                        color: '#DBADFF'
                    },

                    {
                        start: '2025-04-17',
                        end: '2025-04-17',
                        rendering: 'background',
                        color: '#ff9f89',
                        allDay: true,
                    },
                    {
                        start: '2025-04-18',
                        end: '2025-04-18',
                        rendering: 'background',
                        color: '#ff9f89',
                        allDay: true,
                    },
                    {
                        start: '2025-05-01',
                        end: '2025-05-01',
                        rendering: 'background',
                        color: '#ff9f89',
                        allDay: true,
                    },
                ]
            });


        });

    </script>


</body>

</html>
