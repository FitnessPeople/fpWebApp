<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="agendaespecialista.aspx.cs" Inherits="fpWebApp.agendaespecialista" %>

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

    <title>Fitness People | Agenda especialista</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

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

    <script>
        function changeClass() {
            var element1 = document.querySelector("#agendaespecialista");
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
                    <button type="button" class="btn btn-warning" data-dismiss="modal" onclick="window.location.href='asignarcita.aspx?id=' + document.getElementById('event-id').innerHTML + '&idAfil=' + document.getElementById('hfIdAfiliado').value" id="btnAsignar"><i class='fa fa-calendar-plus m-r-sm'></i>Asignar</button>
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
                    <h2><i class="fa fa-calendar-days text-success m-r-sm"></i>Agenda especialista</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Médico</li>
                        <li class="active"><strong>Agenda especialista</strong></li>
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
                                    
                                    <div class="col-xxl-12 col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <h5>Agenda de <asp:Literal ID="ltEspecialista" runat="server"></asp:Literal> </h5>
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
                txbAfiliado: {
                    required: true,
                    minlength: 3
                },
            }
        });
    </script>

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
                        var btn = document.getElementById("btnAfiliado");
                        btn.click();
                    }
                },
                minLength: 3,
                delay: 100,
            });
        });
    </script>

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
                format: "yyyy-mm-d",
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

            /* initialize the calendar
             -----------------------------------------------------------------*/
            var date = new Date();
            var d = date.getDate();
            var m = date.getMonth();
            var y = date.getFullYear();

            $('#calendar').fullCalendar({
                firstDay: 1,
                timeFormat: 'H:mm',
                defaultView: 'listWeek',
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
                    const fechainicial = new Date(event.start);
                    fechainicial.setHours(fechainicial.getHours() + 5);

                    const formatter1 = new Intl.DateTimeFormat('en-US', { hour: '2-digit', minute: '2-digit' });
                    const formattedTime1 = formatter1.format(fechainicial);

                    const formatterdia = new Intl.DateTimeFormat('en-US', { day: '2-digit' });
                    const formatteddiaini = formatterdia.format(fechainicial);

                    const formattermes = new Intl.DateTimeFormat('es-US', { month: 'long' });
                    const formattedmesini = formattermes.format(fechainicial)[0].toUpperCase() + formattermes.format(fechainicial).substring(1);

                    const fechafinal = new Date(event.end);
                    fechafinal.setHours(fechafinal.getHours() + 5);
                    const formatter2 = new Intl.DateTimeFormat('en-US', { hour: '2-digit', minute: '2-digit' });
                    const formattedTime2 = formatter2.format(fechafinal);

                },
            });
        });

    </script>

</body>

</html>