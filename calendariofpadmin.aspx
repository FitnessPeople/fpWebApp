<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="calendariofpadmin.aspx.cs" Inherits="fpWebApp.calendariofpadmin" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Avances FP+</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet">

    <link href="css/plugins/datapicker/datepicker3.css" rel="stylesheet">

    <link href="css/plugins/clockpicker/clockpicker.css" rel="stylesheet">

    <link href="css/plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

</head>

<body>
    <div id="modal-view-event" class="modal modal-top fade calendar-modal">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body">
                    <h4 class="h4"><span class="event-icon weight-400 mr-3"></span><span class="event-title"></span></h4>
                    <div class="event-body"></div>
                    <div class="event-description"></div>
                    <div class="event-id text-hide" id="event-id"></div>
                    <div class="event-allday text-hide" id="event-allday"></div>
                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-warning" onclick="window.location.href = 'addevent.aspx?id'";><i class='fa fa-edit'></i>Editar</button>--%>
                    <%--<button type="button" class="btn btn-warning" onclick="if(document.getElementById('event-allday').innerHTML == '0') { window.location.href = 'editevent.aspx?id=' + document.getElementById('event-id').innerHTML }";><i class='fa fa-edit'></i> Editar</button>--%>
                    <button type="button" class="btn btn-warning" data-dismiss="modal" onclick="window.location.href='asignarcita.aspx?id=' + document.getElementById('event-id').innerHTML + '&idAfil=' + document.getElementById('hfIdAfiliado').value" id="btnAsignar" runat="server" visible="false"><i class='fa fa-calendar-plus m-r-sm'></i>Asignar</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal"><i class='fa fa-times m-r-sm'></i>Cerrar</button>
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
                    <h2><i class="fa fa-calendar-days text-success m-r-sm"></i>Calendario avances</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li class="active"><strong>Calendario avances</strong></li>
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
                            <div class="col-xxl-2 col-lg-3 col-md-5 col-sm-6 col-xs-12">
                            <%--<div class="col-sm-12">--%>
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Agregar avance o reunión</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                            <a class="close-link">
                                                <i class="fa fa-times"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content" id="divCrear" runat="server" visible="false">

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <label>Descripción:</label>
                                                <div class="form-group">
                                                    <textarea class="form-control input-sm" id="txbDescripcion" name="txbDescripcion" runat="server" rows="5" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label>Fecha Inicial:</label>
                                                <div class="form-group">
                                                    <input type="text" class="form-control input-sm" id="txbFechaIni" name="txbFechaIni" runat="server">
                                                </div>
                                            </div>

                                            <div class="col-sm-6 m-b-md">
                                                <label>Hora inicio:</label>
                                                <div class="input-group clockpicker" data-autoclose="true">
                                                    <input type="text" class="form-control input-sm" value="08:00" id="txbHoraIni" name="txbHoraIni" runat="server">
                                                    <span class="input-group-addon">
                                                        <span class="fa fa-clock"></span>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <label>Tipo:</label>
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Avance" Value="Avance"></asp:ListItem>
                                                        <asp:ListItem Text="Reunión" Value="Reunión"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                                <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs pull-right" Text="Agregar" OnClick="btnAgregar_Click" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="col-xxl-10 col-lg-9 col-md-7 col-sm-6 col-xs-12">
                            <%--<div class="col-sm-12">--%>
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Agenda
                                            <asp:Literal ID="ltSede" runat="server"></asp:Literal></h5>
                                        <div class="ibox-tools">
                                            <%--<a class="collapse-link">
                                                        <i class="fa fa-chevron-up"></i>
                                                    </a>
                                                    <a class="close-link">
                                                        <i class="fa fa-times"></i>
                                                    </a>--%>
                                            <span class="label label-warning pull-right" style="color: #000;">Avance</span>
                                            <span class="label label-danger pull-right" style="color: #000;">Reunión</span>
                                            <%--<span class="label label-warning pull-right" style="color: #000;">Cita asignada</span>
                                            <span class="label label-primary pull-right" style="color: #000;">Cita disponible</span>--%>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
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

        $("#form").validate({
            rules: {
                txbFechaIni: {
                    required: true
                },
                txbHoraIni: {
                    required: true,
                },
            }
        });

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
                    fechainicial.setHours(fechainicial.getHours() + 5);

                    const formatter1 = new Intl.DateTimeFormat('en-US', { hour: '2-digit', minute: '2-digit' });
                    const formattedTime1 = formatter1.format(fechainicial);

                    const formatterdia = new Intl.DateTimeFormat('en-US', { day: '2-digit' });
                    const formatteddiaini = formatterdia.format(fechainicial);

                    const formattermes = new Intl.DateTimeFormat('es-US', { month: 'long' });
                    const formattedmesini = formattermes.format(fechainicial)[0].toUpperCase() + formattermes.format(fechainicial).substring(1);

                    const fechafinal = new Date(info.event.end);
                    fechafinal.setHours(fechafinal.getHours() + 5);
                    const formatter2 = new Intl.DateTimeFormat('en-US', { hour: '2-digit', minute: '2-digit' });
                    const formattedTime2 = formatter2.format(fechafinal);

                    if (info.event.id) {
                        console.log(info.event);
                        jQuery('.event-id').html(info.event.id);
                        jQuery('.event-icon').html("<i class='fa fa-" + info.event.extendedProps.icon + "'></i> ");
                        jQuery('.event-title').html(info.event.title);
                        jQuery('.event-body').html(" <i class='fa fa-calendar-day'></i> " + formatteddiaini + "  " + formattedmesini + "<br /><i class='fa fa-clock'></i> " + formattedTime1 + " - " + formattedTime2 + "<br /><br />");
                        jQuery('.event-description').html(info.event.extendedProps.description);
                        var btn = document.getElementById("btnEliminar");
                        //btn.style.display = info.event.extendedProps.btnEliminar;
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
                },
                headerToolbar: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
                },
                <%=strEventos%>
            });

            calendar.render();
        });

    </script>

</body>

</html>
