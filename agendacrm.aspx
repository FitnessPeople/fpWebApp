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

                        <asp:ScriptManager ID="sm1" runat="server" ></asp:ScriptManager>
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
                                                    <!-- Columna izquierda: contenido original -->
                                                    <div class="col-md-6 border-right">
                                                        <div class="tab-content">
                                                            <asp:Repeater ID="rptContenido" runat="server">
                                                                <ItemTemplate>
                                                                    <div id='<%# Eval("IdContacto") %>' class='tab-pane <%# Eval("IdContacto").ToString() == Session["contactoId"]?.ToString() ? "active" : "" %>' style="margin-bottom: 0; padding-bottom: 0;">
                                                                        <div class="m-b-sm">
                                                                            <img alt="image" class="img-circle" src="img/a3.jpg" style="width: 62px">
                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </div>

                                                        <!-- Elementos dinámicos -->
                                                        <div id="contenedorAdicional"></div>
                                                        <div id="datosEvento" style="margin-top: 10px;"></div>
                                                    </div>

                                                    <!-- Columna derecha: zona de planes -->
                                                    <div class="col-md-6">
                                                        <h6><strong>Planes</strong></h6>

                                                        <div class="mb-3">
                                                            <label>Tipo de plan:</label>
                                                            <asp:PlaceHolder ID="phPlanes" runat="server"></asp:PlaceHolder>
                                                        </div>

                                                        <div class="mb-3">
                                                            <label>Meses del plan:</label>
                                                            <%--    <div class="container-fluid">--%>
                                                            <div class="row">
                                                                <div class="col-sm-3 col-xs-3 col-xs-3">
                                                                    <asp:Button ID="btnMes1" runat="server" Text="1"
                                                                        CssClass="btn btn-warning btn-outline btn-block font-bold active"
                                                                        OnClick="btnMes1_Click" />
                                                                </div>
                                                                <div class="col-sm-3 col-xs-3">
                                                                    <asp:Button ID="btnMes2" runat="server" Text="2"
                                                                        CssClass="btn btn-warning btn-outline btn-block font-bold"
                                                                        OnClick="btnMes2_Click" />
                                                                </div>
                                                                <div class="col-sm-3 col-xs-3">
                                                                    <asp:Button ID="btnMes3" runat="server" Text="3"
                                                                        CssClass="btn btn-info btn-outline btn-block font-bold"
                                                                        OnClick="btnMes3_Click" />
                                                                </div>
                                                                <div class="col-sm-3 col-xs-3">
                                                                    <asp:Button ID="btnMes4" runat="server" Text="4"
                                                                        CssClass="btn btn-danger btn-outline btn-block font-bold"
                                                                        OnClick="btnMes4_Click" />
                                                                </div>
                                                                <div class="col-sm-3 col-xs-3">
                                                                    <asp:Button ID="btnMes5" runat="server" Text="5"
                                                                        CssClass="btn btn-warning btn-outline btn-block font-bold"
                                                                        OnClick="btnMes5_Click" />
                                                                </div>

                                                                <div class="col-sm-3 col-xs-3">
                                                                    <asp:Button ID="btnMes6" runat="server" Text="6"
                                                                        CssClass="btn btn-info btn-outline btn-block font-bold"
                                                                        OnClick="btnMes6_Click" />
                                                                </div>
                                                                <div class="col-sm-3 col-xs-3">
                                                                    <asp:Button ID="btnMes7" runat="server" Text="7"
                                                                        CssClass="btn btn-warning btn-outline btn-block font-bold"
                                                                        OnClick="btnMes7_Click" />
                                                                </div>
                                                                <div class="col-sm-3 col-xs-3">
                                                                    <asp:Button ID="btnMes8" runat="server" Text="8"
                                                                        CssClass="btn btn-danger btn-outline btn-block font-bold"
                                                                        OnClick="btnMes8_Click" />
                                                                </div>
                                                                <div class="col-sm-3 col-xs-3">
                                                                    <asp:Button ID="btnMes9" runat="server" Text="9"
                                                                        CssClass="btn btn-info btn-outline btn-block font-bold"
                                                                        OnClick="btnMes9_Click" />
                                                                </div>
                                                                <div class="col-sm-3 col-xs-3">
                                                                    <asp:Button ID="btnMes10" runat="server" Text="10"
                                                                        CssClass="btn btn-warning btn-outline btn-block font-bold"
                                                                        OnClick="btnMes10_Click" />
                                                                </div>
                                                                <div class="col-sm-3 col-xs-3">
                                                                    <asp:Button ID="btnMes11" runat="server" Text="11"
                                                                        CssClass="btn btn-warning btn-outline btn-block font-bold"
                                                                        OnClick="btnMes11_Click" />
                                                                </div>
                                                                <div class="col-sm-3 col-xs-3">
                                                                    <asp:Button ID="btnMes12" runat="server" Text="12"
                                                                        CssClass="btn btn-danger btn-outline btn-block font-bold"
                                                                        OnClick="btnMes12_Click" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <hr />

                                                        <%-- Widgets apilados --%>
                                                        <div class="row">
                                                            <div class="col-12 mb-3 widget style1 bg-warning rounded">
                                                                <div class="" style="display: flex; justify-content: space-between">
                                                                    <i class="fa fa-money-bill-wave fa-3x" style="font-size: 2em; align-content: center"></i>
                                                                    <span style="align-content: center">Mes
                                                                        <asp:Literal ID="ltPrecioBase" runat="server"></asp:Literal></span>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 mb-3 widget style1 bg-danger">
                                                                <div class="" style="display: flex; justify-content: space-between">
                                                                    <i class="fa fa-tag fa-3x" style="font-size: 2em; align-content: center"></i>
                                                                    <span style="align-content: center">Dcto.
                                                                        <asp:Literal ID="ltDescuento" runat="server"></asp:Literal></span>
                                                                </div>

                                                                <div class="col-6 text-right">
                                                                    <h3 class="font-bold text-sm">
                                                                        <asp:Literal ID="ltConDescuento" runat="server"></asp:Literal>
                                                                    </h3>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 mb-3 widget style1 bg-success p-3 rounded text-white">
                                                                <div class="" style="display: flex; justify-content: space-between">
                                                                    <i class="fa fa-cart-shopping fa-3x" style="font-size: 2em; align-content: center"></i>
                                                                    <span style="align-content: center">Total
                                                                        <asp:Literal ID="ltPrecioFinal" runat="server"></asp:Literal></span>
                                                                </div>
                                                            </div>

                                                            <div class="col-12 mb-3 widget style1 lazur-bg">
                                                                <div class="" style="display: flex; justify-content: space-between">
                                                                    <i class="fa fa-hand-holding-dollar fa-3x" style="font-size: 2em; align-content: center"></i>
                                                                    <span style="align-content: center">Ahorro
                                                                        <asp:Literal ID="ltAhorro" runat="server"></asp:Literal></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <hr />
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

                                    <%-- zona de detalle del afuliado bienvenido--%>
                                    <div class="col-xxl-2 col-lg-3 col-md-5 col-sm-6 col-xs-12">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <h5>Planes para afiliado</h5>
                                            </div>
                                            <div class="ibox-content">

                                                <div class="tab-content">
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
                    // ...
                    jQuery('.event-id').html(event.id);
                    jQuery('.event-title').html('Contacto: ' + event.title);

                    var selectHtml = "<label for='ddlStatusLead'>Estatus Lead</label><select id='ddlStatusLead' class='form-control'>";
                    estadosLead.forEach(function (estado) {
                        var selected = (estado.id == event.idEstadoCRM) ? "selected" : "";
                        selectHtml += "<option value='" + estado.id + "' " + selected + ">" + estado.nombre + "</option>";
                    });
                    selectHtml += "</select><br />";

                    var textareaHtml = "<label for='txtContexto'>Contexto de la negociación</label>" +
                        "<textarea id='txtContexto' class='form-control' rows='3'></textarea><br />";

                    const fechainicial = new Date(event.start);
                    fechainicial.setHours(fechainicial.getHours() + 5);
                    const fechafinal = new Date(event.end);
                    fechafinal.setHours(fechafinal.getHours() + 5);

                    const formatter1 = new Intl.DateTimeFormat('es-CO', { hour: '2-digit', minute: '2-digit' });
                    const formatterdia = new Intl.DateTimeFormat('es-CO', { day: '2-digit' });
                    const formattermes = new Intl.DateTimeFormat('es-CO', { month: 'long' });

                    const formatteddiaini = formatterdia.format(fechainicial);
                    const formattedmesini = formattermes.format(fechainicial)[0].toUpperCase() + formattermes.format(fechainicial).substring(1);
                    const formattedTime1 = formatter1.format(fechainicial);
                    const formattedTime2 = formatter1.format(fechafinal);

                    jQuery('.event-id').html(event.id);
                    jQuery('.event-icon').html("<i class='fa fa-" + event.icon + "'></i>");
                    jQuery('.event-title').html('Contacto: ' + event.title);

                    // Insertar contenido en #contenedorAdicional
                    const contenedor = document.getElementById('contenedorAdicional');
                    if (contenedor) {
                        contenedor.innerHTML =
                            selectHtml +
                            textareaHtml +
                            "<label><strong>Historial del contacto</strong></label><br />" +
                            (event.historialHTML2 || '') +
                            "<br />";
                    }

                    document.getElementById("btnAsignar").style.display = event.btnAsignar;


                    //__doPostBack('MostrarDetalleContacto', event.id);
                    jQuery('#modal-view-event').modal();

                },
            });
        });

    </script>
<script>
    $('#modal-view-event').on('hidden.bs.modal', function () {
        location.reload();
    });
</script>
</body>

</html>


