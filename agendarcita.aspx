<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agendarcita.aspx.cs" Inherits="fpWebApp.agendarcita" %>

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
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.3/themes/smoothness/jquery-ui.css">

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet">
    <link href="css/plugins/datapicker/datepicker3.css" rel="stylesheet">
    <link href="css/plugins/clockpicker/clockpicker.css" rel="stylesheet">
    <link href="css/plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#agendarcita");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#medico");
            element2.classList.remove("collapse");
        }
    </script>
</head>

<body onload="changeClass()">
    <form runat="server" id="form">
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
                    <h4 class="h4"><span class="event-icon weight-400 mr-3"></span><span class="event-title"></span></h4>
                    <div class="event-body"></div>
                    <div class="event-description"></div>
                    <div class="event-id text-hide" id="event-id"></div>
                    <div class="event-allday text-hide" id="event-allday"></div>
                    <div class="form-group" id="divAfil" runat="server" visible="false">
                        <label>Afiliado</label>
                        <asp:DropDownList ID="ddlAfiliados" runat="server" DataTextField="DocNombreAfiliado" 
                            DataValueField="idAfiliado" CssClass="chosen-select input-sm" AppendDataBoundItems="true" >
                            <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-warning" data-dismiss="modal" 
                        onclick="window.location.href='asignarcita.aspx?id=' + document.getElementById('event-id').innerHTML + '&idAfil=' + document.getElementById('ddlAfiliados').value" 
                        id="btnAsignar" runat="server" visible="false"><i class='fa fa-calendar-plus m-r-sm'></i>Asignar</button>
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
                    <h2><i class="fa fa-calendar-days text-success m-r-sm"></i>Agenda</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Asistencial</li>
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

                        <%--<asp:ScriptManager ID="sm1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="upAgendarCita" runat="server">
                            <ContentTemplate>--%>
                                <div class="row animated fadeInDown" id="divContenido" runat="server">
                                    <div class="col-xs-12">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <h5>Agendamiento de citas</h5>
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
                                                <div class="row">
                                                    <div class="col-xs-6">
                                                        <div class="form-group">
                                                            <label>Sede:</label>
                                                            <asp:DropDownList CssClass="form-control input-sm required" ID="ddlSedes" runat="server"
                                                                OnSelectedIndexChanged="ddlSedes_SelectedIndexChanged"
                                                                DataValueField="idSede" DataTextField="NombreSede"
                                                                AutoPostBack="true" AppendDataBoundItems="true">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-xs-6">
                                                        <div class="form-group">
                                                            <label>Especialidad:</label>
                                                            <asp:DropDownList CssClass="form-control input-sm required" ID="ddlEspecialidad" runat="server"
                                                                OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged"
                                                                AutoPostBack="true">
                                                                <asp:ListItem Text="Médico deportólogo" Value="29"></asp:ListItem>
                                                                <asp:ListItem Text="Fisioterapeuta" Value="18"></asp:ListItem>
                                                                <asp:ListItem Text="Nutricionista" Value="30"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <%--<asp:DropDownList ID="ddlAfiliados" runat="server" DataTextField="DocNombreAfiliado" 
                                                                DataValueField="idAfiliado" CssClass="select2 input-sm" AppendDataBoundItems="true" >
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-12">
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
                                                    <span class="label label-success pull-right" style="color: #000;">Cita atendida</span>
                                                    <span class="label label-danger pull-right" style="color: #000;">Cita cancelada</span>
                                                    <span class="label label-warning pull-right" style="color: #000;">Cita asignada</span>
                                                    <span class="label label-primary pull-right" style="color: #000;">Cita disponible</span>
                                                </div>
                                            </div>
                                            <div class="ibox-content">
                                                <div id="calendar"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    
                    <%--Fin Contenido!!!!--%>
                </div>
            </div>

            <uc1:footer runat="server" ID="footer" />

        </div>
        <uc1:rightsidebar runat="server" ID="rightsidebar" />
    </div>
    </form>
    <!-- Mainly scripts -->
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- jQuery UI  -->
    <%--<script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>--%>

    <!-- Full Calendar -->
    <script src="https://cdn.jsdelivr.net/npm/fullcalendar@6.1.17/index.global.min.js"></script>

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <!-- Input Mask-->
    <%--<script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>--%>

    <!-- Jquery Validate -->
    <%--<script src="js/plugins/validate/jquery.validate.min.js"></script>--%>

    <script>
        $('.chosen-select').chosen({ width: "100%", disable_search_threshold: 10, no_results_text: "Sin resultados" });

        document.addEventListener('DOMContentLoaded', function () {
            var calendarEl = document.getElementById('calendar');

            var calendar = new FullCalendar.Calendar(calendarEl, {
                eventClick: function (info) {
                    info.jsEvent.preventDefault();

                    const fechainicial = new Date(info.event.start);
                    //fechainicial.setHours(fechainicial.getHours() + 5);
                    fechainicial.setHours(fechainicial.getHours());

                    const formatter1 = new Intl.DateTimeFormat('en-US', { hour: '2-digit', minute: '2-digit' });
                    const formattedTime1 = formatter1.format(fechainicial);

                    const formatterdia = new Intl.DateTimeFormat('en-US', { day: '2-digit' });
                    const formatteddiaini = formatterdia.format(fechainicial);

                    const formattermes = new Intl.DateTimeFormat('es-US', { month: 'long' });
                    const formattedmesini = formattermes.format(fechainicial)[0].toUpperCase() + formattermes.format(fechainicial).substring(1);

                    const fechafinal = new Date(info.event.end);
                    //fechafinal.setHours(fechafinal.getHours() + 5);
                    fechafinal.setHours(fechafinal.getHours());
                    const formatter2 = new Intl.DateTimeFormat('en-US', { hour: '2-digit', minute: '2-digit' });
                    const formattedTime2 = formatter2.format(fechafinal);

                    if (info.event.id) {
                        console.log(info.event.extendedProps);
                        jQuery('.event-id').html(info.event.id);
                        jQuery('.event-icon').html("<i class='fa fa-" + info.event.extendedProps.icon + " fa-3x text-success m-r-xs'></i>");
                        jQuery('.event-title').html(info.event.title);
                        jQuery('.event-body').html(" <i class='fa fa-calendar-day m-r-xs'></i>" + formatteddiaini + "  " + formattedmesini + "<br /><i class='fa fa-clock m-r-xs'></i>" + formattedTime1 + " - " + formattedTime2 + "<br /><br />");
                        jQuery('.event-description').html(info.event.extendedProps.description);
                        var btn = document.getElementById("btnAsignar");
                        btn.style.display = info.event.extendedProps.btnAsignar;
                        var seleccion = document.getElementById("divAfil");
                        seleccion.style.display = info.event.extendedProps.divAfil;
                        jQuery('#modal-view-event').modal();
                    }
                },
                height: 700,
                initialView: 'timeGridWeek',
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
