<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="agendaespecialista.ascx.cs" Inherits="fpWebApp.controles.agendaespecialista" %>
<div class="row animated fadeInDown" id="divContenido" runat="server">
    
    <div class="col-xxl-12 col-lg-12 col-md-12 col-sm-12 col-xs-12">
        <div class="ibox float-e-margins">
            <div class="ibox-title">
                <h5>Agenda de <asp:Literal ID="ltEspecialista" runat="server"></asp:Literal> </h5>
                <div class="ibox-tools">
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

<script>

    document.addEventListener('DOMContentLoaded', function () {
        var calendarEl = document.getElementById('calendar');

        var calendar = new FullCalendar.Calendar(calendarEl, {
            eventClick: function (info) {
                //info.jsEvent.preventDefault();

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

                console.log(info.event.id);

                if (info.event.id) {
                    //console.log(info.event.title);
                    jQuery('.event-id').html(info.event.id);
                    jQuery('.event-icon').html("<i class='fa fa-" + info.event.icon + "'></i>");
                    jQuery('.event-title').html(info.event.title);
                    jQuery('.event-body').html(" <i class='fa fa-calendar-day'></i> " + formatteddiaini + "  " + formattedmesini + "<br /><i class='fa fa-clock'></i> " + formattedTime1 + " - " + formattedTime2 + "<br /><br />");
                    jQuery('.event-description').html(info.event.description);
                    var btn = document.getElementById("btnEliminar");
                    btn.style.display = info.event.btnEliminar;
                    jQuery('#modal-view-event').modal();
                }
            },
            height: 700,
            initialView: 'listWeek',
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