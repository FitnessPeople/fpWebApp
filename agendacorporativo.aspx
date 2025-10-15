<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agendacorporativo.aspx.cs" Inherits="fpWebApp.agendacorporativo" %>

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

    <title>Fitness People | Agenda corporativa</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet">

    <link href="css/plugins/datapicker/datepicker3.css" rel="stylesheet">

    <link href="css/plugins/clockpicker/clockpicker.css" rel="stylesheet">

    <link href="css/plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet">

    <!-- SweetAlert2 CDN -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#agendacorporativo");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#corporativo");
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
    <div id="modal-view-event" class="modal modal-top fade calendar-modal">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body">
                    <h4 class="h4"><span class="event-icon mr-3 fa-2x"></span><span class="event-title"></span></h4>
                    <div class="event-body"></div>
                    <div class="event-description"></div>
                    <div class="event-id text-hide" id="event-id"></div>
                    <div class="event-allday text-hide" id="event-allday"></div>
                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-warning" onclick="window.location.href = 'addevent.aspx?id'";><i class='fa fa-edit'></i>Editar</button>--%>
                    <%--<button type="button" class="btn btn-warning" onclick="if(document.getElementById('event-allday').innerHTML == '0') { window.location.href = 'editevent.aspx?id=' + document.getElementById('event-id').innerHTML }";><i class='fa fa-edit'></i> Editar</button>--%>
                    <button type="button" class="btn btn-warning" data-dismiss="modal" onclick="window.location.href = 'agenda.aspx?deleteid=' + document.getElementById('event-id').innerHTML" runat="server" id="btnEliminar" visible="false"><i class='fa fa-trash m-r-sm'></i>Eliminar</button>
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
                    <h2><i class="fa fa-calendar-days text-success m-r-sm"></i>Agenda corporativa</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Corporativo</li>
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
                        <div class="row animated fadeInDown" id="divContenido" runat="server">
                            <asp:Label ID="lblMensaje" runat="server" Text="Label"></asp:Label>
                            <%--<div class="col-xxl-3 col-lg-4 col-md-5 col-sm-6 col-xs-12">--%>
                            <div class="col-sm-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Agregar visita</h5>
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
                                                <label>Empresa:</label>
                                                <div class="form-group">
                                                     <asp:DropDownList CssClass="form-control input-sm required" ID="ddlEmpresas" runat="server"
                                                        OnSelectedIndexChanged="ddlEmpresas_SelectedIndexChanged" DataValueField="idEmpresa" DataTextField="NombreEmpresa"
                                                        AutoPostBack="true" AppendDataBoundItems="true">
                                                         <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>                                                     
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <label>Lugar:</label>
                                                <div class="form-group">
                                                    <input type="text" class="form-control input-sm" id="txbLugar" name="txbLugar" runat="server" />
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label>Fecha Inicial:</label>
                                                <div class="form-group">
                                                    <input type="text" class="form-control input-sm" id="txbFechaIni" name="txbFechaIni" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <label>Fecha Final:</label>
                                                <div class="form-group">
                                                    <%--<span class="input-group-addon"><i class="fa fa-calendar-day"></i></span>--%>
                                                    <input type="text" class="form-control input-sm" id="txbFechaFin" name="txbFechaFin" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">

                                            <div class="col-sm-6 m-b-md">
                                                <label>Hora inicio:</label>
                                                <div class="input-group clockpicker" data-autoclose="true">
                                                    <input type="text" class="form-control input-sm" value="08:00" id="txbHoraIni" name="txbHoraIni" runat="server" />
                                                    <span class="input-group-addon">
                                                        <span class="fa fa-clock"></span>
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <label>Hora final:</label>
                                                <div class="input-group clockpicker" data-autoclose="true">
                                                    <input type="text" class="form-control input-sm" value="12:00" id="txbHoraFin" name="txbHoraFin" runat="server" />
                                                    <span class="input-group-addon">
                                                        <span class="fa fa-clock"></span>
                                                    </span>
                                                </div>
                                            </div>
                                        </div>

<%--                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-horizontal">
                                                    <div class="form-group m-b-mb">
                                                        <label class="col-sm-4">Duración cita:</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList CssClass="form-control input-sm required" ID="ddlDuracion" runat="server"
                                                                AppendDataBoundItems="true">
                                                                <asp:ListItem Text="20 minutos" Value="20"></asp:ListItem>
                                                                <asp:ListItem Text="30 minutos" Value="30"></asp:ListItem>
                                                                <asp:ListItem Text="40 minutos" Value="40"></asp:ListItem>
                                                                <asp:ListItem Text="45 minutos" Value="45"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>--%>
                                        <div class="row">
                                            <div class="form-group">
                                                <i class="fas fa-pen text-info"></i>
                                                <label for="message-text" class="col-form-label">Contexto de la negociación:</label>
                                                <textarea id="txaObservaciones" runat="server" rows="3"
                                                    cssclass="form-control input-sm" class="form-control" placeholder="Escribe tu comentario…"></textarea>
                                                <asp:RequiredFieldValidator ID="rfvObservaciones" runat="server" ControlToValidate="txaObservaciones"
                                                    ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                            <%--<div class="col-xxl-9 col-lg-8 col-md-7 col-sm-6 col-xs-12">--%>
                            <div class="col-sm-8">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Agenda
                                            <asp:Literal ID="ltSede" runat="server"></asp:Literal></h5>
                                        <div class="ibox-tools">
                                            <span class="label label-success pull-right" style="color: #000;">Cita atendida</span>
                                            <span class="label label-danger pull-right" style="color: #000;">Cita cancelada</span>
                                            <span class="label label-warning pull-right" style="color: #000;">Cita asignada</span>
                                            <span class="label label-primary pull-right" style="color: #000;">Cita disponible</span>
                                        </div>
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
                ddlEspecialistas: {
                    required: true,
                },
                ddlSedesCita: {
                    required: true,
                },
                txbFechaIni: {
                    required: true
                },
                txbFechaFin: {
                    required: true
                },
                txbHoraIni: {
                    required: true,
                },
                txbHoraFin: {
                    required: true,
                },
                txbDuracion: {
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
                    console.log(info.event.id);
                    info.jsEvent.preventDefault();

                    const fechainicial = new Date(info.event.start);
                    //fechainicial.setHours(fechainicial.getHours() + 5);

                    const formatter1 = new Intl.DateTimeFormat('en-US', { hour: '2-digit', minute: '2-digit' });
                    const formattedTime1 = formatter1.format(fechainicial);

                    const formatterdia = new Intl.DateTimeFormat('en-US', { day: '2-digit' });
                    const formatteddiaini = formatterdia.format(fechainicial);

                    const formattermes = new Intl.DateTimeFormat('es-US', { month: 'long' });
                    const formattedmesini = formattermes.format(fechainicial)[0].toUpperCase() + formattermes.format(fechainicial).substring(1);

                    const fechafinal = new Date(info.event.end);
                    //fechafinal.setHours(fechafinal.getHours() + 5);
                    const formatter2 = new Intl.DateTimeFormat('en-US', { hour: '2-digit', minute: '2-digit' });
                    const formattedTime2 = formatter2.format(fechafinal);

                    if (info.event.id) {
                        console.log(info.event.extendedProps);
                        jQuery('.event-id').html(info.event.id);
                        jQuery('.event-icon').html("<i class='fa fa-" + info.event.extendedProps.icon + "'></i>");
                        jQuery('.event-title').html(info.event.title);
                        jQuery('.event-body').html(" <i class='fa fa-calendar-day'></i> " + formatteddiaini + "  " + formattedmesini + "<br /><i class='fa fa-clock'></i> " + formattedTime1 + " - " + formattedTime2 + "<br /><br />");
                        jQuery('.event-description').html(info.event.extendedProps.description);
                        var btn = document.getElementById("btnEliminar");
                        btn.style.display = info.event.extendedProps.btnEliminar;
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
