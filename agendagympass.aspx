<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agendagympass.aspx.cs" Inherits="fpWebApp.agendagympass" %>

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

    <title>Fitness People | Agenda Gym Pass</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet">

    <link href="css/plugins/datapicker/datepicker3.css" rel="stylesheet">

    <link href="css/plugins/clockpicker/clockpicker.css" rel="stylesheet">

    <link href="css/plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#agendagympass");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#sistema");
            element2.classList.remove("collapse");
        }
    </script>
    
    <style>
        .ibox-title {
            display: flex;
            justify-content: space-between;
            padding: 0 2rem;
            align-items: center;
            height: 5em;
        }
    </style>
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
    <div id="modal-view-event" class="modal modal-top fade calendar-modal">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="h4"><span class="event-icon mr-3 fa-2x"></span><span class="event-title"></span></h4>
                    <div class="event-body"></div>
                    <div class="event-description"></div>
                    <div class="event-id text-hide" id="event-id"></div>
                    <div class="event-idSede text-hide" id="event-idSede"></div>
                    <div class="event-allday text-hide" id="event-allday"></div>
                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-warning" onclick="window.location.href = 'addevent.aspx?id'";><i class='fa fa-edit'></i>Editar</button>--%>
                    <%--<button type="button" class="btn btn-warning" onclick="if(document.getElementById('event-allday').innerHTML == '0') { window.location.href = 'editevent.aspx?id=' + document.getElementById('event-id').innerHTML }";><i class='fa fa-edit'></i> Editar</button>--%>
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="window.location.href = 'eliminardisponibilidad.aspx?id=' + document.getElementById('event-id').innerHTML" runat="server" id="btnEliminar" visible="false"><i class='fa fa-trash m-r-sm'></i>Eliminar</button>
                    <button type="button" class="btn btn-success" data-dismiss="modal"><i class="fa fa-times m-r-sm"></i>Cerrar</button>
                    <button type="button" class="btn btn-warning" data-dismiss="modal" onclick="window.location.href = 'eliminardisponibilidad.aspx?id=' + document.getElementById('event-id').innerHTML" runat="server" id="btnEliminar" visible="false"><i class='fa fa-trash m-r-sm'></i>Eliminar</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class='fa fa-times m-r-sm'></i>Cerrar</button>
                    <button type="button" class="btn btn-success" data-dismiss="modal" onclick="window.location.href = 'asistenciaagendagympass.aspx?id=' + document.getElementById('event-id').innerHTML" runat="server" id="btnAsistencia" visible="false"><i class="fa fa-calendar-check m-r-sm"></i>Asistió</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="window.location.href = 'noasistenciaagendagympass.aspx?id=' + document.getElementById('event-id').innerHTML" runat="server" id="btnNoAsistencia" visible="false"><i class="fa fa-calendar-xmark m-r-sm"></i>No Asistió</button>
                    <button type="button" class="btn btn-warning" data-dismiss="modal" onclick="window.location.href = 'cancelaragendagympass.aspx?id=' + document.getElementById('event-id').innerHTML" runat="server" id="btnCancelar" visible="false"><i class="fa fa-calendar-minus m-r-sm"></i>Cancelar</button>
                    <button type="button" class="btn btn-info" data-dismiss="modal"><i class="fa fa-times m-r-sm"></i>Cerrar</button>
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
                    <h2><i class="fa fa-calendar-check text-success m-r-sm"></i>Agenda Gym Pass</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Sistema</li>
                        <li class="active"><strong>Agenda Gym Pass</strong></li>
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
                            <%--<div class="col-xxl-9 col-lg-8 col-md-7 col-sm-6 col-xs-12">--%>
                            <div class="col-sm-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Agenda
                                        <h5 style="margin-bottom: 0;">Agenda
                                            <asp:Literal ID="ltSede" runat="server"></asp:Literal></h5>
                                        <div class="ibox-tools">
                                            <%--<a class="collapse-link">
                                                        <i class="fa fa-chevron-up"></i>
                                                    </a>
                                                    <a class="close-link">
                                                        <i class="fa fa-times"></i>
                                                    </a>--%>
                                            <span class="label label-danger pull-right" style="color: #000;">Eliminado</span>
                                            <span class="label label-warning pull-right" style="color: #000;">Cancelado</span>
                                            <span class="label label-success pull-right" style="color: #000;">Asistió</span>
                                            <span class="label label-primary pull-right" style="color: #000;">Agendado</span>
                                            <span class="label label-success pull-right" style="color: #000;">Cita atendida</span>
                                            <span class="label label-danger pull-right" style="color: #000;">Cita cancelada</span>
                                            <span class="label label-warning pull-right" style="color: #000;">Cita asignada</span>
                                            <span class="label label-primary pull-right" style="color: #000;">Cita disponible</span>
                                            <span style="padding: 1rem; font-size: 1.2rem; color: #fff; font-weight: bold;" class="label label-warning pull-right">Cancelado</span>
                                            <span style="padding: 1rem; font-size: 1.2rem; color: #fff; font-weight: bold;" class="label label-danger pull-right">No Asistió</span>
                                            <span style="padding: 1rem; font-size: 1.2rem; color: #fff; font-weight: bold;" class="label label-success pull-right">Asistió</span>
                                            <span style="padding: 1rem; font-size: 1.2rem; color: #fff; font-weight: bold;" class="label label-primary pull-right">Agendado</span>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="form-horizontal">
                                            <div class="form-group m-b-n-sm">
                                                <label class="col-sm-2 col-sm-2 control-label">Sede</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList CssClass="form-control input-sm required" ID="ddlSedes" runat="server"
                                            <div style="display: flex; flex-direction: row; justify-content: space-between;">
                                                <div id="divFiltroSede" runat="server" style="display: flex; align-items: center; gap: 5rem;">
                                                    <label class="control-label text-center" style="padding: 0; margin: 0;">Sede</label>
                                                    <asp:DropDownList CssClass="form-control required" ID="ddlSedes" runat="server"
                                                        OnSelectedIndexChanged="ddlSedes_SelectedIndexChanged"
                                                        DataValueField="idSede" DataTextField="NombreSede"
                                                        AutoPostBack="true" AppendDataBoundItems="true">
                                                    </asp:DropDownList>
                                                                      AutoPostBack="true" AppendDataBoundItems="true" />
                                                </div>

                                                <div style="display: flex; align-items: center; gap: 5rem;">
                                                    <label class="control-label text-center" style="padding: 0; margin: 0;">Estado</label>
                                                    <asp:DropDownList CssClass="form-control required" ID="ddlEstados" runat="server"
                                                                      OnSelectedIndexChanged="ddlEstados_SelectedIndexChanged"
                                                                      DataValueField="Estados" DataTextField="Estados"
                                                                      AutoPostBack="true" AppendDataBoundItems="true" />
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
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

    <!-- Full Calendar -->
    <%--<script src="js/plugins/fullcalendar/fullcalendar.min.js"></script>--%>
    <script src="https://cdn.jsdelivr.net/npm/fullcalendar@6.1.17/index.global.min.js"></script>

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

        document.addEventListener('DOMContentLoaded', function () {
            var calendarEl = document.getElementById('calendar');

            var calendar = new FullCalendar.Calendar(calendarEl, {
                eventClick: function (info) {
                    info.jsEvent.preventDefault();

                    const fechainicial = new Date(info.event.start);
                    //fechainicial.setHours(fechainicial.getHours());

                    const formatter1 = new Intl.DateTimeFormat('en-US', { hour: '2-digit', minute: '2-digit' });
                    const formattedTime1 = formatter1.format(fechainicial);

                    const formatterdia = new Intl.DateTimeFormat('en-US', { day: '2-digit' });
                    const formatteddiaini = formatterdia.format(fechainicial);

                    const formattermes = new Intl.DateTimeFormat('es-US', { month: 'long' });
                    const formattedmesini = formattermes.format(fechainicial)[0].toUpperCase() + formattermes.format(fechainicial).substring(1);

                    if (info.event.id) {
                        console.log(info.event.extendedProps);
                        jQuery('.event-id').html(info.event.id);
                        jQuery('.event-icon').html("<i class='fa fa-" + info.event.extendedProps.icon + "'></i>");
                        jQuery('.event-title').html(info.event.title);
                        jQuery('.event-body').html(" <i class='fa fa-calendar-day'></i> " + formatteddiaini + "  " + formattedmesini + "<br /><i class='fa fa-clock'></i> " + formattedTime1 + "<br /><br />");
                        jQuery('.event-body').html(" <i class='fa fa-calendar-day'></i> " + formatteddiaini + "  " + formattedmesini + "<br /><i class='fa fa-clock'></i> " + formattedTime1 + " - " + formattedTime2 + "<br /><br />");
                        jQuery('.event-body').html(" <i class='fa fa-calendar-day'></i> " + formatteddiaini + "  " + formattedmesini + "<br /><i class='fa fa-clock'></i> " + formattedTime1 + "<br /><br />");
                        jQuery('.event-description').html(info.event.extendedProps.description);
                        var btn = document.getElementById("btnEliminar");
                        btn.style.display = info.event.extendedProps.btnEliminar;
                        var btnNoAsistencia = document.getElementById("btnNoAsistencia");
                        var btnAsistencia = document.getElementById("btnAsistencia");
                        var btnCancelar = document.getElementById("btnCancelar");
                        btnNoAsistencia.style.display = info.event.extendedProps.btnNoAsistencia;
                        btnAsistencia.style.display = info.event.extendedProps.btnAsistencia;
                        btnCancelar.style.display = info.event.extendedProps.btnCancelar;
                        jQuery('#modal-view-event').modal();
                    }
                },
                height: 700,
                initialView: 'dayGridMonth',
                firstDay: 1,
                allDayText: 'Todo\r\nel día',
                moreLinkContent: function (args) {
                    return '+' + args.num + ' eventos más';
                },
                slotMinTime: '06:00',
                slotMaxTime: '22:00',
                //weekends: false,
                //hiddenDays: [0],
                //slotDuration: '00:25:00',
                //slotLabelInterval: '00:30',
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
                eventTimeFormat: { // like '14:30'
                    hour: '2-digit',
                    minute: '2-digit',
                    hour12: false
                    hour12: true
                },
                headerToolbar: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
                },
                <%=strEventos%>
                
                <%=strEventos%>
            });

            calendar.render();
        });

    </script>

</body>

</html>
