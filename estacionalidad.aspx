﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="estacionalidad.aspx.cs" Inherits="fpWebApp.estacionalidad" %>

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

    <title>Fitness People | Estacionalidad</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/datapicker/datepicker3.css" rel="stylesheet">

    <link href="css/plugins/ionRangeSlider/ion.rangeSlider.css" rel="stylesheet">
    <link href="css/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">

    <%--<link href="https://cdn.jsdelivr.net/npm/fullcalendar@6.1.18/index.global.min.css" rel="stylesheet" />--%>
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

        /* Fuerza que todas las celdas tengan igual alto */
        .fc .fc-daygrid-day {
            height: 60px; /* ajusta este valor según tu diseño */
            vertical-align: top;
            overflow: hidden;
        }

        /* Asegura que el contenido interno no estire la celda */
        .fc .fc-daygrid-day-frame {
            height: 100%;
            /*display: flex;*/
            flex-direction: column;
            justify-content: flex-start;

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
                    <h2><i class="fa fa-calendar-days text-success m-r-sm"></i>Estacionalidad</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Comercial</li>
                        <li class="active"><strong>Estacionalidad</strong></li>
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

                    <form runat="server" id="form" onsubmit="return false;">
                        <div id="divContenido" runat="server">
                            <div class="col-lg-3">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5><i class="fa fa-arrow-turn-down text-danger"></i> Arrastra al calendario</h5>
                                    </div>
                                    <div id='external-events' class="ibox-content">
                                        <div class='fc-event' data-title="5%" data-bgcolor="#ed5565">
                                            <div class='fc-event-main'
                                                style="color: #fff; background: #ed5565; border: 1px solid #ed5565; border-radius: 3px; font-size: 1.5em; padding-left: 10px;">
                                                5%
                                            </div>
                                        </div>
                                        <div class='fc-event' data-title="10%" data-bgcolor="#1ab394">
                                            <div class='fc-event-main'
                                                style="color: #fff; background: #1ab394; border: 1px solid #1ab394; border-radius: 3px; font-size: 1.5em; padding-left: 10px;">
                                                10%
                                            </div>
                                        </div>
                                        <div class='fc-event' data-title="15%" data-bgcolor="#1c84c6">
                                            <div class='fc-event-main'
                                                style="color: #fff; background: #1c84c6; border: 1px solid #1c84c6; border-radius: 3px; font-size: 1.5em; padding-left: 10px;">
                                                15%
                                            </div>
                                        </div>
                                        <div class='fc-event' data-title="20%" data-bgcolor="#f8ac59">
                                            <div class='fc-event-main'
                                                style="color: #fff; background: #f8ac59; border: 1px solid #f8ac59; border-radius: 3px; font-size: 1.5em; padding-left: 10px;">
                                                20%
                                            </div>
                                        </div>
                                        <div class='fc-event' data-title="25%" data-bgcolor="#23c6c8">
                                            <div class='fc-event-main'
                                                style="color: #fff; background: #23c6c8; border: 1px solid #23c6c8; border-radius: 3px; font-size: 1.5em; padding-left: 10px;">
                                                25%
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="ibox float-e-margins">
                                    <div class="ibox-content">
                                        <h3><i class="fa fa-chart-simple text-danger"></i> Total por semana</h3>
                                        <div id="listaSemanas"></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-9">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5><i class="fa fa-percent text-danger"></i> Estacionalidad 
                                            <asp:Literal ID="ltSede" runat="server"></asp:Literal></h5>
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

    <script>
        var calendar; // declarada globalmente

        function realizarCalculosConExtendedProps(eventos) {
            const semanasDiv = document.getElementById('listaSemanas');
            semanasDiv.innerHTML = ''; // limpiar lista anterior


            let sumaPorSemana = {};

            eventos.forEach(evento => {
                const fecha = new Date(evento.start);
                const valor = parseFloat(evento.title);

                if (isNaN(valor)) return;

                // Calcular el año y número de semana
                const semanaKey = obtenerClaveSemana(fecha);

                if (!sumaPorSemana[semanaKey]) {
                    sumaPorSemana[semanaKey] = 0;
                }

                sumaPorSemana[semanaKey] += valor;
            });

            let claves = Object.keys(sumaPorSemana); // claves = ["nombre", "color", "macho", "edad"]
            for (let i = 0; i < claves.length; i++) {
                let clave = claves[i];
                //console.log(sumaPorSemana[clave]);
                const totalSemana = sumaPorSemana[clave];
                //semanasDiv.innerHTML += "<div>Semana " + clave + ": <span class='badge badge-danger'>" + sumaPorSemana[clave] + "%</span></div>";

                if (parseInt(totalSemana) == 0) {
                    semanasDiv.innerHTML += `<h5>Semana ${clave}</h5><div class="progress progress-striped active"><div style="width: ${totalSemana}%" class="progress-bar progress-bar-danger" aria-valuemax="100" aria-valuemin="0" aria-valuenow="${totalSemana}" role="progressbar"><span class="font-bold">${totalSemana}%</span></div></div>`;
                }
                else {
                    if (parseInt(totalSemana) < 100) {
                        //semanasDiv.innerHTML += `<div>Semana ${clave}: <span class='badge badge-warning' style='font-size: 1.5rem; margin-bottom: 2px;' >${totalSemana}%</span></div>`;
                        semanasDiv.innerHTML += `<h5>Semana ${clave}</h5><div class="progress progress-striped active"><div style="width: ${totalSemana}%" class="progress-bar progress-bar-warning" aria-valuemax="100" aria-valuemin="0" aria-valuenow="${totalSemana}" role="progressbar"><span class="font-bold">${totalSemana}%</span></div></div>`;
                    }
                    else {
                        if (parseInt(totalSemana) == 100) {
                            semanasDiv.innerHTML += `<h5>Semana ${clave}</h5><div class="progress progress-striped active"><div style="width: ${totalSemana}%" class="progress-bar progress-bar-primary" aria-valuemax="100" aria-valuemin="0" aria-valuenow="${totalSemana}" role="progressbar"><span class="font-bold">${totalSemana}%</span></div></div>`;
                        }
                        else {
                            semanasDiv.innerHTML += `<h5>Semana ${clave}</h5><div class="progress progress-striped active"><div style="width: ${totalSemana}%" class="progress-bar progress-bar-danger" aria-valuemax="100" aria-valuemin="0" aria-valuenow="${totalSemana}" role="progressbar"><span class="font-bold">${totalSemana}%</span></div></div>`;
                        }
                    }
                }
            }
        }

        function obtenerClaveSemana(fecha) {
            const temp = new Date(fecha.getTime());
            temp.setHours(0, 0, 0, 0);
            temp.setDate(temp.getDate() + 4 - (temp.getDay() || 7)); // Mover al jueves de la semana ISO

            const yearStart = new Date(temp.getFullYear(), 0, 1);
            const weekNo = Math.ceil((((temp - yearStart) / 86400000) + 1) / 7);

            return `${String(weekNo).padStart(2, '0')}`;
        }

        function eliminarEvento(evento) {
            let data = evento;

            // Enviar a servidor por AJAX
            fetch("estacionalidad.aspx/EliminarEvento", {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            })
                .then(response => response.text())
                .then(data => {
                    //console.log('Respuesta del servidor:', data);
                })
                .catch(error => {
                    console.error('Error al eliminar evento:', error);
                });
        }

        let feriados = {};

        function cargarFeriados() {
            return fetch('estacionalidad.aspx/ObtenerFeriados', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({})
            })
                .then(response => response.json())
                .then(data => {
                // Guardamos los feriados en formato { 'yyyy-mm-dd': 'Nombre del feriado' }
                data.d.forEach(item => {
                    feriados[item.fecha] = item.descripcion;
                });
            });
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
                            //value: eventEl.getAttribute('data-value'),
                            bgcolor: eventEl.getAttribute('data-bgcolor')
                        }
                    };
                }
            });

            cargarFeriados().then(() => {

            // initialize the calendar
            // -----------------------------------------------------------------

            calendar = new Calendar(calendarEl, {
                headerToolbar: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'dayGridMonth'
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
                editable: false,
                //events: 'obtenerestacionalidad.aspx',
                eventSources: [{
                    url: 'obtenerestacionalidad.aspx',
                    method: 'GET',
                    failure: function () {
                        alert('Error al cargar eventos.');
                    },
                    success: function (events) {
                        // Aquí puedes hacer tu cálculo cuando todos los eventos hayan llegado
                        realizarCalculosConExtendedProps(events);
                    }
                }],
                weekNumbers: true,
                fixedWeekCount: false,
                showNonCurrentDates: false,
                eventOverlap: false,
                firstDay: 1,
                allDayText: 'Todo\r\nel día',
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
                    right: 'dayGridMonth'
                },
                droppable: true, // this allows things to be dropped onto the calendar
                eventReceive: function (info) {
                    // Aquí el evento ya ha sido agregado al calendario
                    //console.log('Entra por el eventReceive');
                    let color = info.event.extendedProps.bgcolor;
                    info.event.setProp('backgroundColor', color);
                    const evento = {
                        title: info.event.title,
                        start: info.event.start.toISOString(),
                        allDay: info.event.allDay,
                        bgcolor: info.event.backgroundColor
                    };
                    console.log(evento);

                    fetch('estacionalidad.aspx/GuardarEvento', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(evento)
                    })
                        .then(response => response.json())
                        .then(result => {
                            const idInsertado = result.d;
                            if (idInsertado > 0) {
                                // Asignar el id retornado al evento
                                info.event.setProp('id', idInsertado);
                            } else {
                                alert('Error al guardar el evento.');
                                info.revert(); // Revierte el evento si falla
                            }
                        })
                        .catch(error => {
                            console.error('Error:', error);
                            info.revert();
                        });

                    realizarCalculosConExtendedProps(calendar.getEvents());

                },
                eventClick: function (info) {
                    // Evento que se activa cuando hacemos clic en un evento
                    if (info.event.rendering === 'background' || info.event.display === 'background') {
                        return; // Ignora el clic
                    }

                    const evento = {
                        id: info.event.id
                    };
                    
                    eliminarEvento(evento); // Elimina el evento de la base de datos

                    var eventObj = info.event;
                    if (eventObj) {
                        eventObj.remove(); // Elimina el evento del calendario
                    }

                    realizarCalculosConExtendedProps(calendar.getEvents());

                },
                dayCellDidMount: function(info) {
                    //console.log(info);
                    const fechaCelda = info.date.toISOString().split('T')[0];
                    const diaSemana = info.date.getUTCDay(); // 0 = Domingo

                    // Resaltar domingos
                    if (diaSemana === 0) {
                        info.el.style.backgroundColor = '#F2F2F2'; // Color gris claro
                    }

                    if (feriados[fechaCelda]) {
                        // Cambiar fondo
                        info.el.style.backgroundColor = '#FFE2DB';

                        // Buscar el contenedor interno (donde aparece el número del día)
                        const contenedor = info.el.querySelector('.fc-daygrid-day-frame');

                        if (contenedor) {
                            // Crear un div para mostrar el texto del feriado
                            const textoFeriado = document.createElement('div');
                            textoFeriado.innerText = feriados[fechaCelda];
                            textoFeriado.style.fontSize = '0.75rem';
                            textoFeriado.style.color = '#000';
                            textoFeriado.style.marginTop = '2px';

                            // Agregar el texto dentro de la celda
                            contenedor.appendChild(textoFeriado);
                        }
                    }
                }
            });

            calendar.render();
            });
        });

    </script>

</body>

</html>
