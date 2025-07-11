<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agendacomercial.aspx.cs" Inherits="fpWebApp.agendacomercial" %>

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

    <title>Fitness People | Agenda</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">

    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet">

    <link href="css/plugins/bootstrap-tagsinput/bootstrap-tagsinput.css" rel="stylesheet">

    <link href="css/plugins/colorpicker/bootstrap-colorpicker.min.css" rel="stylesheet">

    <link href="css/plugins/cropper/cropper.min.css" rel="stylesheet">

    <link href="css/plugins/switchery/switchery.css" rel="stylesheet">

    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet">

    <link href="css/plugins/nouslider/jquery.nouislider.css" rel="stylesheet">

    <link href="css/plugins/datapicker/datepicker3.css" rel="stylesheet">

    <link href="css/plugins/ionRangeSlider/ion.rangeSlider.css" rel="stylesheet">
    <link href="css/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css" rel="stylesheet">

    <link href="css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css" rel="stylesheet">

    <link href="css/plugins/clockpicker/clockpicker.css" rel="stylesheet">

    <link href="css/plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet">

    <link href="css/plugins/select2/select2.min.css" rel="stylesheet">

    <link href="css/plugins/touchspin/jquery.bootstrap-touchspin.min.css" rel="stylesheet">

    <link href="css/plugins/dualListbox/bootstrap-duallistbox.min.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">

    <link href="https://cdn.jsdelivr.net/npm/fullcalendar@6.1.18/index.global.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/fullcalendar@6.1.18/index.global.min.js"></script>

    <!-- SweetAlert2 CDN -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <style>
        #external-events {
            top: 20px;
            left: 20px;
        }

        #external-events .fc-event {
            cursor: move;
            margin: 3px 0;
        }

        /*.fc-event-title-container {
            background: #f8ac59;
        }*/
    </style>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#agenda");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#medico");
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
                    <h2><i class="fa fa-calendar-days text-success m-r-sm"></i>Agenda comercial</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Comercial</li>
                        <li class="active"><strong>Agenda comercial</strong></li>
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
                        <div class="row animated fadeInDown" id="divContenido" runat="server">
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <%--<span class="label label-success pull-right">Monthly</span>--%>
                                                <h5>Arrastra al calendario</h5>
                                            </div>
                                            <div id='external-events' class="ibox-content">
                                                <div class='fc-event'
                                                    data-title="5%" data-value="5" data-bgcolor="#ed5565">
                                                    <div class='fc-event-main'
                                                        style="color: #fff; background: #ed5565; border: 1px solid #ed5565; border-radius: 3px; font-size: 1.5em;">
                                                        5%
                                                    </div>
                                                </div>
                                                <div class='fc-event'
                                                    data-title="10%" data-value="10" data-bgcolor="#1ab394">
                                                    <div class='fc-event-main'
                                                        style="color: #fff; background: #1ab394; border: 1px solid #1ab394; border-radius: 3px; font-size: 1.5em;">
                                                        10%
                                                    </div>
                                                </div>
                                                <div class='fc-event'
                                                    data-title="15%" data-value="15" data-bgcolor="#1c84c6">
                                                    <div class='fc-event-main'
                                                        style="color: #fff; background: #1c84c6; border: 1px solid #1c84c6; border-radius: 3px; font-size: 1.5em;">
                                                        15%
                                                    </div>
                                                </div>
                                                <div class='fc-event'
                                                    data-title="20%" data-value="20" data-bgcolor="#f8ac59">
                                                    <div class='fc-event-main'
                                                        style="color: #fff; background: #f8ac59; border: 1px solid #f8ac59; border-radius: 3px; font-size: 1.5em;">
                                                        20%
                                                    </div>
                                                </div>
                                                <div class='fc-event'
                                                    data-title="25%" data-value="25" data-bgcolor="#23c6c8">
                                                    <div class='fc-event-main'
                                                        style="color: #fff; background: #23c6c8; border: 1px solid #23c6c8; border-radius: 3px; font-size: 1.5em;">
                                                        25%
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <%--<span class="label label-info pull-right">Annual</span>--%>
                                                <h5>Meta por semana</h5>
                                            </div>
                                            <div class="ibox-content">
                                                <input type="text" id="semana1" value="0" class="dial m-r-sm" data-fgColor="#1AB394" data-width="85" data-height="85" />
                                                <div id="listaSemanas"></div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <span class="label label-primary pull-right">Today</span>
                                                <h5>Meta mensual</h5>
                                            </div>
                                            <div class="ibox-content">

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <h1 class="no-margins">$1´700.000.000</h1>
                                                        <div class="font-bold text-navy">44% <i class="fa fa-level-up"></i><small>Creciendo</small></div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <h1 class="no-margins">$700.000.000</h1>
                                                        <div class="font-bold text-navy">22% <i class="fa fa-level-up"></i><small>Creciendo</small></div>
                                                    </div>
                                                </div>


                                            </div>
                                        </div>
                                    </div>
                                    <%--<div class="col-lg-4">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <h5>Monthly income</h5>
                                                <div class="ibox-tools">
                                                    <span class="label label-primary">Updated 12.2015</span>
                                                </div>
                                            </div>
                                            <div class="ibox-content no-padding">
                                                <div class="flot-chart m-t-lg" style="height: 55px;">
                                                    <div class="flot-chart-content" id="flot-chart1"></div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Agenda
                                            <asp:Literal ID="ltSede" runat="server"></asp:Literal></h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="form-horizontal">
                                            <div class="form-group m-b-n-sm">
                                                <label class="col-sm-2 col-sm-2 control-label">Sede</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList CssClass="form-control input-sm required" ID="ddlSedes" runat="server"
                                                        OnSelectedIndexChanged="ddlSedes_SelectedIndexChanged"
                                                        DataValueField="idSede" DataTextField="NombreSede"
                                                        AutoPostBack="true" AppendDataBoundItems="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        
                                        <%--<div id='external-events'>
                                            <p>
                                                <strong>Arrastra al calendario</strong>
                                            </p>

                                            <div class='fc-event'
                                                data-title="5%" data-value="5" data-bgcolor="#ed5565">
                                                <div class='fc-event-main'
                                                    style="color: #fff; background: #ed5565; border: 1px solid #ed5565; border-radius: 3px; font-size: 1.5em;">
                                                    5%
                                                </div>
                                            </div>
                                            <div class='fc-event'
                                                data-title="10%" data-value="10" data-bgcolor="#1ab394">
                                                <div class='fc-event-main'
                                                    style="color: #fff; background: #1ab394; border: 1px solid #1ab394; border-radius: 3px; font-size: 1.5em;">
                                                    10%
                                                </div>
                                            </div>
                                            <div class='fc-event'
                                                data-title="15%" data-value="15" data-bgcolor="#1c84c6">
                                                <div class='fc-event-main'
                                                    style="color: #fff; background: #1c84c6; border: 1px solid #1c84c6; border-radius: 3px; font-size: 1.5em;">
                                                    15%
                                                </div>
                                            </div>
                                            <div class='fc-event'
                                                data-title="20%" data-value="20" data-bgcolor="#f8ac59">
                                                <div class='fc-event-main'
                                                    style="color: #fff; background: #f8ac59; border: 1px solid #f8ac59; border-radius: 3px; font-size: 1.5em;">
                                                    20%
                                                </div>
                                            </div>
                                            <div class='fc-event'
                                                data-title="25%" data-value="25" data-bgcolor="#23c6c8">
                                                <div class='fc-event-main'
                                                    style="color: #fff; background: #23c6c8; border: 1px solid #23c6c8; border-radius: 3px; font-size: 1.5em;">
                                                    25%
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div id="calendar"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
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

   <!-- JSKnob -->
   <script src="js/plugins/jsKnob/jquery.knob.js"></script>

    <!-- Date range picker -->
    <script src="js/plugins/daterangepicker/daterangepicker.js"></script>

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <!-- Input Mask-->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <!-- Data picker -->
    <script src="js/plugins/datapicker/bootstrap-datepicker.js"></script>

    <!-- Clock picker -->
    <script src="js/plugins/clockpicker/clockpicker.js"></script>

    <!-- Date range picker -->
    <script src="js/plugins/daterangepicker/daterangepicker.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    


    <script>
        $(".dial").knob();

        !(function (a) {
            a.fn.datepicker.dates.es = {
                days: ["Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"],
                daysShort: ["Dom", "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb"],
                daysMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"],
                months: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
                monthsShort: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"],
                today: "Hoy",
                monthsTitle: "Meses",
                clear: "Borrar",
                weekStart: 1,
                format: "yyyy-mm-dd",
            };
        })(jQuery);

        $(document).ready(function () {

            $('#data_1 .input-group.date').datepicker({
                language: "es",
                daysOfWeekDisabled: "0",
                todayBtn: "linked",
                todayHighlight: true,
                keyboardNavigation: false,
                forceParse: false,
                autoclose: true,
            });

            $('#data_2 .input-group.date').datepicker({
                language: "es",
                daysOfWeekDisabled: "0",
                todayBtn: "linked",
                todayHighlight: true,
                keyboardNavigation: false,
                forceParse: false,
                autoclose: true,
            });

            $('.clockpicker').clockpicker();

        });

    </script>

    <script>

        //Agrega días a una fecha
        function addDays(date, days) {
            var result = new Date(date);
            result.setDate(result.getDate() + days);
            return result;
        }

        //Obtiene la fecha inicial y final de una semana especifica de un año específico
        function obtenerFechaInicioFinSemana(weekNumber, year) {
            const simple = new Date(year, 0, 1 + (weekNumber - 1) * 7);
            const dayOfWeek = simple.getDay();
            const ISOweekStart = new Date(simple);
            if (dayOfWeek <= 4)
                ISOweekStart.setDate(simple.getDate() - simple.getDay() + 1);
            else
                ISOweekStart.setDate(simple.getDate() + 8 - simple.getDay());

            const ISOweekEnd = new Date(ISOweekStart);
            ISOweekEnd.setDate(ISOweekStart.getDate() + 6);
            return { start: ISOweekStart, end: ISOweekEnd };
        }

        //Calcula la sumatoria de porcentajes de una semana
        function sumarValoresDeSemana(weekNumber, calendar, yearNumber) {
            const { start, end } = obtenerFechaInicioFinSemana(weekNumber, yearNumber);

            const eventosSemana = calendar.getEvents().filter(evento => {
                return evento.start >= start && evento.start <= end;
            });

            const total = eventosSemana.reduce((acum, evento) => {
                return acum + (parseInt(evento.extendedProps.value) || 0);
            }, 0);

            //console.log(`Total de valores en la semana: ${total}`);
            return total;
        }

        document.addEventListener('DOMContentLoaded', function () {
            var Calendar = FullCalendar.Calendar;
            var Draggable = FullCalendar.Draggable;

            var containerEl = document.getElementById('external-events');
            var calendarEl = document.getElementById('calendar');
            var checkbox = document.getElementById('drop-remove');

            // initialize the external events
            // -----------------------------------------------------------------

            new Draggable(containerEl, {
                itemSelector: '.fc-event',
                eventData: function (eventEl) {
                    return {
                        //title: eventEl.innerText
                        title: eventEl.getAttribute('data-title'),
                        extendedProps: {
                            value: eventEl.getAttribute('data-value'),
                            bgcolor: eventEl.getAttribute('data-bgcolor')
                        }
                    };
                }
            });

            // initialize the calendar
            // -----------------------------------------------------------------

            var calendar = new Calendar(calendarEl, {
                headerToolbar: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                eventContent: function (arg) {
                    //console.log(arg);
                    let backgroundColor = arg.event.extendedProps.bgcolor || '#fff'; // Color por defecto si no se define
                    let italicEl = document.createElement('i')

                    italicEl.innerHTML = arg.event.title;
                    //italicEl.style = `background: ${backgroundColor};`;
                    italicEl.style = `font-size: 1.5em;`;
                    //console.log(backgroundColor);

                    let arrayOfDomNodes = [italicEl]
                    return { domNodes: arrayOfDomNodes }
                },
                editable: true,
                weekNumbers: true,
                fixedWeekCount: false,
                showNonCurrentDates: false,
                eventOverlap: false,
                firstDay: 1,
                allDayText: 'Todo\r\nel día',
                slotMinTime: '06:00',
                slotMaxTime: '22:00',
                moreLinkContent: function (args) {
                    return '+' + args.num + ' eventos más';
                },
                slotLabelFormat: {
                    hour: 'numeric',
                    minute: '2-digit',
                    omitZeroMinute: false,
                    meridiem: 'short'
                },
                locale: 'es',
                buttonText: {
                    prev: '',
                    next: '',
                    prevYear: 'Año anterior',
                    nextYear: 'Año siguiente',
                    year: 'Año',
                    today: 'Hoy',
                    month: 'Mes',
                    week: 'Semana',
                    day: 'Día',
                    list: 'Lista',
                    listWeek: 'Lista',
                },
                businessHours: true,
                businessHours: [ // specify an array instead
                    {
                        daysOfWeek: [1, 2, 3, 4, 5], // Lunes, martes, miercoles, jueves y viernes
                        startTime: '06:00',
                        endTime: '21:00'
                    },
                    {
                        daysOfWeek: [6], // Sabado
                        startTime: '7:00',
                        endTime: '18:00'
                    }
                ],
                dayMaxEventRows: true, // for all non-TimeGrid views
                views: {
                    timeGrid: {
                        dayMaxEventRows: 6 // adjust to 6 only for timeGridWeek/timeGridDay
                    },
                },
                eventTimeFormat: { // De este modo '14:30'
                    hour: '2-digit',
                    minute: '2-digit',
                    hour12: false
                },
                headerToolbar: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
                },
                droppable: true, // this allows things to be dropped onto the calendar
                eventDrop: function (info) {
                    // Evento que se acaba de mover
                    //console.log('Entra por el eventDrop');
                    const weekNumber = moment(addDays(info.event.start, -1)).week();
                    const yearNumber = info.event.start.getFullYear();
                    const { start, end } = obtenerFechaInicioFinSemana(weekNumber, yearNumber);

                    const fechaActual = new Date();
                    const year = fechaActual.getFullYear();
                    const month = fechaActual.getMonth();
                    const { primerDia, ultimoDia } = getPrimerYUltimoDia(year, month);

                    mostrarSemanasDelMes(primerDia, addDays(ultimoDia, 1), calendar);
                },
                eventReceive: function (info) {
                    // Aquí el evento ya ha sido agregado al calendario
                    //console.log('Entra por el eventReceive');
                    //let backgroundColor = info.event.extendedProps.bgcolor || '#fff'; // Color por defecto si no se define
                    //    let zadr = `
                    //        <div class="fc-event" style="background: ${backgroundColor}; color: #fff;">
                    //          ${info.event.title}
                    //        </div>
                    //      `;
                    //    return { html: zadr };

                    const weekNumber = moment(addDays(info.event.start, -1)).week();
                    const yearNumber = info.event.start.getFullYear();
                    const { start, end } = obtenerFechaInicioFinSemana(weekNumber, yearNumber);

                    const fechaActual = new Date();
                    const year = fechaActual.getFullYear();
                    const month = fechaActual.getMonth();
                    const { primerDia, ultimoDia } = getPrimerYUltimoDia(year, month);

                    mostrarSemanasDelMes(primerDia, addDays(ultimoDia, 1), calendar);

                },
                eventClick: function (info) {
                    console.log('Entra');
                    var eventObj = info.event;
                    if (eventObj) {
                        eventObj.remove(); // Elimina el evento
                    }

                    const fechaActual = new Date();
                    const year = fechaActual.getFullYear();
                    const month = fechaActual.getMonth();
                    const { primerDia, ultimoDia } = getPrimerYUltimoDia(year, month);
                    mostrarSemanasDelMes(primerDia, addDays(ultimoDia, 1), calendar);

                },
                datesSet: function (info) {
                    mostrarSemanasDelMes(info.start, info.end, calendar);
                }
            });

            calendar.render();
        });

        function getPrimerYUltimoDia(year, month) {
            // Primer día del mes
            const primerDia = new Date(year, month, 1);

            // Último día del mes
            const ultimoDia = new Date(year, month + 1, 0);

            return { primerDia, ultimoDia };
        }

        function mostrarSemanasDelMes(startDate, endDate, calendar) {
            //console.log(endDate);
            const semanasDiv = document.getElementById('listaSemanas');
            const knob1 = document.getElementById("semana1");
            semanasDiv.innerHTML = ''; // limpiar lista anterior
            knob1.value = 0;

            let current = new Date(startDate);
            let weekNumber = moment(addDays(startDate, -1)).week();
            const yearNumber = startDate.getFullYear();
            //let semanaIndex = 1;
            let semanaIndex = weekNumber;

            while (current < endDate) {
                const startOfWeek = new Date(current);
                const endOfWeek = new Date(current);
                endOfWeek.setDate(current.getDate() + 6);

                // Mostrar solo semanas que tengan algún día en el mes visible
                if (startOfWeek.getMonth() === endDate.getMonth() - 1 || endOfWeek.getMonth() === endDate.getMonth() - 1) {
                    const totalSemana = sumarValoresDeSemana(semanaIndex, calendar, yearNumber);
                    //console.log(`Semana ${semanaIndex}, Total semana: ${totalSemana}`);
                    //semanasDiv.innerHTML += `<div>Semana ${semanaIndex}: ${formatearFecha(startOfWeek)} - ${formatearFecha(endOfWeek)}</div>`;

                    knob1.value = parseInt(totalSemana);

                    //semanasDiv.innerHTML += `<div class="m-r-md inline"><input type="text" value="${totalSemana}" class="dial m-r-sm" data-fgColor="#1AB394" data-width="85" data-height="85" /></div>`
                    if (parseInt(totalSemana) == 0) {
                        semanasDiv.innerHTML += `<div>Semana ${semanaIndex}: <span class='badge badge-danger'>${totalSemana}%</span></div>`;
                    }
                    else {
                        if (parseInt(totalSemana) < 100) {
                            semanasDiv.innerHTML += `<div>Semana ${semanaIndex}: <span class='badge badge-warning'>${totalSemana}%</span></div>`;
                        }
                        else {
                            if (parseInt(totalSemana) == 100) {
                                semanasDiv.innerHTML += `<div>Semana ${semanaIndex}: <span class='badge badge-primary'>${totalSemana}%</span></div>`;
                            }
                            else {
                                semanasDiv.innerHTML += `<div>Semana ${semanaIndex}: <span class='badge badge-danger'>${totalSemana}%</span></div>`;
                            }
                        }
                    }

                    //semanasDiv.innerHTML += `<div>Semana ${semanaIndex}: ${totalSemana}%</div>`;
                    semanaIndex++;
                }

                current.setDate(current.getDate() + 7); // siguiente semana
            }
        }

        function formatearFecha(fecha) {
            return fecha.toISOString().split('T')[0];
        }

        
    </script>

</body>

</html>
