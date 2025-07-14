<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agendacrm.aspx.cs" Inherits="fpWebApp.agendacrm" %>

<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Agenda CRM</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.3/themes/smoothness/jquery-ui.css">

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <link href="css/plugins/fullcalendar/fullcalendar.css" rel="stylesheet" />
    <link href="css/plugins/fullcalendar/fullcalendar.print.css" rel='stylesheet' media='print' />

    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet">

    <link href="css/plugins/datapicker/datepicker3.css" rel="stylesheet">

    <link href="css/plugins/clockpicker/clockpicker.css" rel="stylesheet">

    <link href="css/plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <style>
        .fc-event, .fc-event-title {
            color: white !important;
        }
    </style>
    <style>
        .modal-body {
            max-height: 70vh; /* Usa un 70% del alto de la ventana */
            overflow-y: auto;
        }
    </style>
    <style>
        #contenedorAdicional {
            margin-top: 10px;
        }

        .client-detail {
            margin-bottom: 0 !important;
            padding-bottom: 0 !important;
        }

        .modal-body textarea,
        .modal-body select {
            margin-top: 10px;
        }

        .modal-body hr {
            margin: 10px 0;
        }
    </style>

    <style>
        .client-detail {
            margin-bottom: 0 !important;
            padding-bottom: 0 !important;
        }

        .full-height-scroll {
            max-height: none !important;
            overflow: visible !important;
        }

        .tab-pane.active {
            margin-bottom: 0 !important;
            padding-bottom: 0 !important;
        }

        #datosEvento {
            margin-top: 10px;
        }
    </style>


    <script>
        function changeClass() {
            var element1 = document.querySelector("#agendacrm");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#crm");
            element2.classList.remove("collapse");
        }
    </script>
</head>

<body onload="changeClass()">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <i class="fa fa-person-chalkboard modal-icon"></i>
                    <h4 class="modal-title">Guía para agendar citas</h4>
                    <small class="font-bold">¡Bienvenido! A continuación, te ofrecemos una guía sencilla para ayudarte a completar el formulario de manera correcta y eficiente. Sigue estos pasos para asegurarte de que toda la información se registre de forma adecuada.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>1. Lee las Instrucciones</b><br />
                        Antes de comenzar, es importante que leas todas las instrucciones del formulario. Esto te ayudará a entender qué información se requiere y cómo debe ser presentada.
                        <br />
                        <br />
                        <b>2. Reúne la Información Necesaria</b><br />
                        Asegúrate de tener a mano todos los documentos e información que necesitas, como:
                        Datos personales (nombre, dirección, número de teléfono, etc.)
                        Información específica relacionada con el propósito del formulario (por ejemplo, detalles de empleo, historial médico, etc.)
                        <br />
                        <br />
                        <b>3. Completa los Campos Requeridos</b><br />
                        Campos Obligatorios: Identifica cuáles son los campos obligatorios (generalmente marcados con un asterisco *) y asegúrate de completarlos.
                        Campos Opcionales: Si hay campos opcionales, completa solo los que consideres relevantes.
                        <br />
                        <br />
                        <b>4. Confirma la Información</b><br />
                        Asegúrate de que todos los datos ingresados son correctos y actualizados. Una revisión final puede evitar errores que podrían complicar el proceso.
                        <br />
                        <br />
                        <b>5. Envía el Formulario</b><br />
                        Asegúrate de seguir el proceso de envío indicado (hacer clic en "Agregar").
                        <br />
                        <br />
                        ¡Siguiendo estos pasos, estarás listo para diligenciar tu formulario sin problemas! Si tienes dudas, no dudes en consultar con el administrador del sistema.
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <div id="wrapper">

        <uc1:navbar runat="server" ID="navbar" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa fa-calendar-days text-success m-r-sm"></i>Agenda CRM</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>CRM</li>
                        <li class="active"><strong>Agenda</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">
                    <%--Inicio Contenido!!!!--%>

                    <div class="ibox-content m-b-sm border-bottom" runat="server" id="divMensaje" visible="false">
                        <div class="p-xs">
                            <div class="pull-left m-r-md">
                                <i class="fa fa-triangle-exclamation text-danger mid-icon"></i>
                            </div>
                            <h2>Acceso Denegado</h2>
                            <span>Lamentablemente, no tienes permiso para acceder a esta página. Por favor, verifica que estás usando una cuenta con los permisos adecuados o contacta a nuestro soporte técnico para más información. Si crees que esto es un error, no dudes en ponerte en contacto con nosotros para resolver cualquier problema. Gracias por tu comprensión.</span>
                        </div>
                    </div>

                    <uc1:paginasperfil runat="server" ID="paginasperfil" Visible="false" />

                    <form runat="server" id="form">

                        <asp:HiddenField ID="hdnIdContacto" runat="server" />
                        <input type="hidden" id="hdnDocumentoAfiliado" value="" />


                        <asp:ScriptManager ID="sm1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="upAsesorCRM" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="modal-view-event" class="modal modal-top fade calendar-modal">
                                    <div class="modal-dialog modal-dialog-centered modal-lg">
                                        <div class="modal-content">

                                            <div class="modal-body">
                                                <h4 class="h4">
                                                    <span class="event-icon weight-400 mr-3"></span>
                                                    <span class="event-title"></span>
                                                </h4>

                                                <div class="row">
                                                    <!-- Columna izquierda con borde y padding -->
                                                    <div class="col-sm-6">
                                                        <div class="panel panel-default" style="padding: 15px;">
                                                            <div class="panel-body">
                                                                <asp:Repeater ID="rptContenido" runat="server">
                                                                    <ItemTemplate>
                                                                        <div id='<%# Eval("IdContacto") %>'
                                                                            class='tab-pane <%# Eval("IdContacto").ToString() == Session["contactoId"]?.ToString() ? "active" : "" %>'
                                                                            style="margin-bottom: 10px;">
                                                                            <div class="media">
                                                                                <div class="media-left">
                                                                                    <img alt="image" class="img-circle" src="img/a3.jpg" style="width: 62px">
                                                                                </div>
                                                                                <div class="media-body">
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <!-- Elementos adicionales -->
                                                    <div class="col-sm-6">
                                                        <div id="contenedorAdicional" class="well well-sm"></div>
                                                        <div id="datosEvento" class="well well-sm" style="margin-top: 10px;"></div>
                                                    </div>
                                                </div>

                                            </div>


                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-warning" data-dismiss="modal"
                                                    onclick="window.location.href='asignarcita.aspx?id=' + document.getElementById('event-id').innerHTML + '&idAfil=' + document.getElementById('hfIdAfiliado').value"
                                                    id="btnAsignar">
                                                    <i class='fa fa-calendar-plus m-r-sm'></i>Asignar
                                                </button>
                                                <button type="button" class="btn btn-danger" data-dismiss="modal">
                                                    <i class='fa fa-times m-r-sm'></i>Cerrar
                                                </button>
                                            </div>

                                            <!-- Ocultos -->
                                            <div class="event-id text-hide" id="event-id"></div>
                                            <div class="event-allday text-hide" id="event-allday"></div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row animated fadeInDown" id="divContenido" runat="server">
                                    <%-- Zona del calendario--%>
                                    <div class="col-xxl-10 col-lg-9 col-md-7 col-sm-6 col-xs-12">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <h5>Agenda
                                            <asp:Literal ID="ltSede" runat="server"></asp:Literal></h5>
                                                <div class="ibox-tools">
                                                    <span class='badge badge-info'>Primer Contacto </span>
                                                    <span class='badge badge-warning'>Propuesta en gestión </span>
                                                    <span class='badge badge-primary'>Negociación aceptada </span>
                                                    <span class='badge badge-danger'>Negociación rechazada </span>
                                                </div>
                                            </div>
                                            <div class="ibox-content">
                                                <div id="calendar"></div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-xxl-2 col-lg-3 col-md-5 col-sm-6 col-xs-12">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <h5>Información de gestión del asesor</h5>
                                            </div>
                                            <div class="ibox-content">

                                                <div class="tab-content">

                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="widget style1">
                                                                <div class="row">
                                                                    <div class="col-xs-4 text-center">
                                                                        <i class="fa fa-trophy fa-5x"></i>
                                                                    </div>
                                                                    <div class="col-xs-8 text-right">
                                                                        <span>Vendido hoy </span>
                                                                        <h2 class="font-bold">$ 456,000</h2>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="widget lazur-bg no-padding">
                                                                <div class="p-m">
                                                                    <h1 class="m-xs">$ 210,660</h1>
                                                                    <h3 class="font-bold no-margins">Meta mensual
                                                                    </h3>
                                                                    <small>Income form project Beta.</small>
                                                                </div>
                                                                <div class="flot-chart">
                                                                    <div class="flot-chart-content" id="flot-chart2"></div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <div class="widget yellw-bg no-padding">
                                                                <div class="p-m">
                                                                    <h1 class="m-xs">$ 50,992</h1>

                                                                    <h3 class="font-bold no-margins">Total venta semeste
                                                                    </h3>
                                                                    <small>Sales marketing.</small>
                                                                </div>
                                                                <div class="flot-chart">
                                                                    <div class="flot-chart-content" id="flot-chart3"></div>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="col-lg-12">
                                                            <div class="widget navy-bg no-padding">
                                                                <div class="p-m">
                                                                    <h1 class="m-xs">$ 1,540</h1>

                                                                    <h3 class="font-bold no-margins">Ventas en el año
                                                                    </h3>
                                                                    <small>Income form project Alpha.</small>
                                                                </div>
                                                                <div class="flot-chart">
                                                                    <div class="flot-chart-content" id="flot-chart1"></div>
                                                                </div>
                                                            </div>
                                                        </div>


                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </form>
                    <%--Fin Contenido!!!!--%>
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

    <!-- Full Calendar -->
    <script src="js/plugins/fullcalendar/fullcalendar.min.js"></script>

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <!-- Input Mask-->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <script type="text/javascript">  
        $(document).ready(function () {
            $("#txbAfiliado").autocomplete({
                source: function (request, response) {
                    $.getJSON("/obtenerafiliados?search=" + request.term, function (data) {
                        response($.map(data, function (item) {
                            return {
                                label: item.nombre + " " + item.apellido + " - " + item.id + ", " + item.correo,
                                value: item.id + " - " + item.nombre + " " + item.apellido,
                            };
                        }));
                    });
                },
                select: function (event, ui) {
                    if (ui.item) {
                        console.log(ui.item.value);
                        document.getElementById("txbAfiliado").value = ui.item.value;
                        let documento = ui.item.id;
                        $("#hdnDocumentoAfiliado").val(documento);
                        var btn = document.getElementById("btnAfiliado");
                        btn.click();
                    }
                },
                minLength: 3,
                delay: 100,
            });
        });
    </script>

    <script type="text/javascript">
        var estadosLead = <%= EstadosCRM_Json %>;
    </script>

    <!-- Mainly scripts -->
    <%--    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>--%>

    <!-- jquery UI -->
    <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>

    <!-- Touch Punch - Touch Event Support for jQuery UI -->
    <script src="js/plugins/touchpunch/jquery.ui.touch-punch.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js"></script>

    <!-- Jvectormap -->
    <script src="js/plugins/jvectormap/jquery-jvectormap-2.0.2.min.js"></script>
    <script src="js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"></script>

    <!-- Flot -->
    <script src="js/plugins/flot/jquery.flot.js"></script>
    <script src="js/plugins/flot/jquery.flot.tooltip.min.js"></script>
    <script src="js/plugins/flot/jquery.flot.resize.js"></script>
    <script>
        $(document).ready(function () {
            var d1 = [[1262304000000, 6], [1264982400000, 3057], [1267401600000, 20434], [1270080000000, 31982], [1272672000000, 26602], [1275350400000, 27826], [1277942400000, 24302], [1280620800000, 24237], [1283299200000, 21004], [1285891200000, 12144], [1288569600000, 10577], [1291161600000, 10295]];
            var d2 = [[1262304000000, 5], [1264982400000, 200], [1267401600000, 1605], [1270080000000, 6129], [1272672000000, 11643], [1275350400000, 19055], [1277942400000, 30062], [1280620800000, 39197], [1283299200000, 37000], [1285891200000, 27000], [1288569600000, 21000], [1291161600000, 17000]];

            var data1 = [
                { label: "Data 1", data: d1, color: '#17a084' },
                { label: "Data 2", data: d2, color: '#127e68' }
            ];
            $.plot($("#flot-chart1"), data1, {
                xaxis: {
                    tickDecimals: 0
                },
                series: {
                    lines: {
                        show: true,
                        fill: true,
                        fillColor: {
                            colors: [{
                                opacity: 1
                            }, {
                                opacity: 1
                            }]
                        }
                    },
                    points: {
                        width: 0.1,
                        show: false
                    }
                },
                grid: {
                    show: false,
                    borderWidth: 0
                },
                legend: {
                    show: false
                }
            });

            var data2 = [
                { label: "Data 1", data: d1, color: '#19a0a1' }
            ];
            $.plot($("#flot-chart2"), data2, {
                xaxis: {
                    tickDecimals: 0
                },
                series: {
                    lines: {
                        show: true,
                        fill: true,
                        fillColor: {
                            colors: [{
                                opacity: 1
                            }, {
                                opacity: 1
                            }]
                        }
                    },
                    points: {
                        width: 0.1,
                        show: false
                    }
                },
                grid: {
                    show: false,
                    borderWidth: 0
                },
                legend: {
                    show: false
                }
            });

            var data3 = [
                { label: "Data 1", data: d1, color: '#fbbe7b' },
                { label: "Data 2", data: d2, color: '#f8ac59' }
            ];
            $.plot($("#flot-chart3"), data3, {
                xaxis: {
                    tickDecimals: 0
                },
                series: {
                    lines: {
                        show: true,
                        fill: true,
                        fillColor: {
                            colors: [{
                                opacity: 1
                            }, {
                                opacity: 1
                            }]
                        }
                    },
                    points: {
                        width: 0.1,
                        show: false
                    }
                },
                grid: {
                    show: false,
                    borderWidth: 0
                },
                legend: {
                    show: false
                }
            });

            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green'
            });

            $(".todo-list").sortable({
                placeholder: "sort-highlight",
                handle: ".handle",
                forcePlaceholderSize: true,
                zIndex: 999999
            }).disableSelection();

            var mapData = {
                "US": 498,
                "SA": 200,
                "CA": 1300,
                "DE": 220,
                "FR": 540,
                "CN": 120,
                "AU": 760,
                "BR": 550,
                "IN": 200,
                "GB": 120,
                "RU": 2000
            };

            $('#world-map').vectorMap({
                map: 'world_mill_en',
                backgroundColor: "transparent",
                regionStyle: {
                    initial: {
                        fill: '#e4e4e4',
                        "fill-opacity": 1,
                        stroke: 'none',
                        "stroke-width": 0,
                        "stroke-opacity": 0
                    }
                },
                series: {
                    regions: [{
                        values: mapData,
                        scale: ["#1ab394", "#22d6b1"],
                        normalizeFunction: 'polynomial'
                    }]
                }
            });
        });
    </script>

    <script>
        $(document).ready(function () {

            /* initialize the calendar
             -----------------------------------------------------------------*/
            var date = new Date();
            var d = date.getDate();
            var m = date.getMonth();
            var y = date.getFullYear();

            $('#calendar').fullCalendar({
                firstDay: 1,
                timeFormat: 'H:mm',
                defaultView: 'month',
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sáb'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sábado'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                header: {
                    left: 'prev, next, today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay,listWeek'
                },
                businessHours: true,
                businessHours: [ // specify an array instead
                    {
                        dow: [1, 2, 3, 4, 5], // Lunes, martes, miercoles, jueves y viernes
                        start: '06:00',
                        end: '21:00'
                    },
                    {
                        dow: [6], // Sabado
                        start: '7:00',
                        end: '18:00'
                    }
                ],
                editable: false,
                droppable: false, // this allows things to be dropped onto the calendar
                drop: function () {
                    // is the "remove after drop" checkbox checked?
                    if ($('#drop-remove').is(':checked')) {
                        // if so, remove the element from the "Draggable Events" list
                        $(this).remove();
                    }
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
                <%=strEventos%>
                eventClick: function (event, jsEvent, view) {
                    $('.modal').modal('hide');
                    var documento = $('#hdnDocumentoAfiliado').val() || '';
                    window.location.href = 'crmnuevocontacto.aspx?editid=' + encodeURIComponent(event.id) + '&evento=1' + '&documento=' + encodeURIComponent(event.doc);
                },
            });
        });

    </script>





</body>

</html>


