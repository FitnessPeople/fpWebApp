<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gympass.aspx.cs" Inherits="fpWebApp.gympass" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/indicadoresgympass.ascx" TagPrefix="uc1" TagName="indicadoresgympass" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Gym Pass</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <%--<link href="css/plugins/footable/footable.core.css" rel="stylesheet" />--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />

    <!-- Morris -->
    <link href="css/plugins/morris/morris-0.4.3.min.css" rel="stylesheet" />

    <link href="css/plugins/clockpicker/clockpicker.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <style type="text/css" media="print">
        body {
            visibility: hidden;
            display: none
        }
    </style>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#gympass");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#sistema");
            element2.classList.remove("collapse");
        }
    </script>

    <style>
        .clockpicker-popover {
            z-index: 9999 !important;
        }

        .float-e-margins .btn {
            margin-bottom: 0;
        }
    </style>
</head>

<body onload="changeClass()">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <i class="fa fa-id-card modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para visualizar los usuarios inscritos en Gym Pass</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar el listado de forma rápida y sencilla.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Busca y filtra afiliados</b><br />
                        Usa el buscador para encontrar afiliados específicos.<br />
                        <i class="fa-solid fa-magnifying-glass"></i> Filtra por: 
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i> <b>Nombre</b>, 
                        <i class="fa-solid fa-address-card" style="color: #0D6EFD;"></i> <b>Cédula</b>, 
                        <i class="fa-solid fa-envelope" style="color: #0D6EFD;"></i> <b>Correo</b>, 
                        <i class="fa-solid fa-mobile" style="color: #0D6EFD;"></i> <b>Celular</b>,
                        <i class="fa-solid fa-user-tie" style="color: #0D6EFD;"></i> <b>Cargo</b> o 
                        <i class="fa-solid fa-circle" style="color: #0D6EFD;"></i> <b>Estado</b><br />
                        <i class="fa-solid fa-star" style="color: #FECE32;"></i> Tip: ¡Combina filtros para resultados más precisos!
                    <br />
                        <br />
                        <b>Paso 2: Revisa la tabla de resultados</b><br />
                        La tabla muestra toda la información clave de cada afiliado.<br />
                        En la columna "Acciones" encontrarás estas opciones:<br />
                        <i class="fa fa-edit" style="color: #1AB394;"></i> <b>Editar:</b> Modifica los datos del afiliado.<br />
                        <i class="fa fa-trash" style="color: #DC3545;"></i> <b>Eliminar:</b> Da de baja al afiliado.
                    <br />
                        <br />
                        <b>Paso 3: Acciones adicionales</b><br />
                        Al lado opuesto del buscador encontrarás dos botones útiles:<br />
                        <i class="fa-solid fa-file-export" style="color: #212529;"></i> <b>Exportar a Excel:</b>
                        Genera un archivo Excel con los datos visibles en la tabla.<br />
                        <br />
                        <br />
                        <i class="fa fa-exclamation-circle mr-2"></i> Si tienes dudas, no dudes en consultar con el administrador del sistema.
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <div id="wrapper">

        <uc1:navbar runat="server" ID="navbar1" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header1" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa fa-user-tag text-success m-r-sm"></i>Gym Pass</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Sistema</li>
                        <li class="active"><strong>Gym Pass</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">
                    <%--Inicio Contenido!!!!--%>

                    <uc1:indicadoresgympass runat="server" ID="indicadoresgympass" />

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


                    <div class="ibox float-e-margins" runat="server" id="divContenido">
                        <div class="ibox-title">
                            <h5>Lista de inscritos</h5>
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
                            <form runat="server" id="form1">
                                <div class="row" style="font-size: 12px;" runat="server" id="divBotonesLista">
                                    <div class="col-lg-6 form-horizontal">
                                        <div class="form-group">
                                            <div class="form-group" id="filter-form-container" style="margin-left: 28px;"></div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 form-horizontal">
                                        <%--<a class="btn btn-success pull-right dim m-l-md" style="font-size: 12px;" 
                                                    href="nuevoempleado" title="Agregar empleado" 
                                                    runat="server" id="btnAgregar" visible="false"><i class="fa fa-square-plus"></i> NUEVO
                                                </a>--%>
                                        <asp:LinkButton ID="lbExportarExcel" runat="server" CausesValidation="false"
                                            CssClass="btn btn-info pull-right dim m-l-md" style="font-size: 12px;"
                                            OnClick="lbExportarExcel_Click">
                                                    <i class="fa fa-file-excel"></i> EXCEL
                                        </asp:LinkButton>
                                    </div>
                                </div>

                                <table class="footable table table-striped" data-paging-size="10"
                                    data-filter-min="3" data-filter-placeholder="Buscar"
                                    data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}"
                                    data-paging-limit="10" data-filtering="true"
                                    data-filter-container="#filter-form-container" data-filter-delay="300"
                                    data-filter-dropdown-title="Buscar en:" data-filter-position="left"
                                    data-empty="Sin resultados">
                                    <thead>
                                        <tr>
                                            <th data-sortable="false" data-breakpoints="xs">Documento</th>
                                            <th data-breakpoints="xs">Nombre</th>
                                            <th data-breakpoints="xs">Apellidos</th>
                                            <th data-breakpoints="xs">Email</th>
                                            <th data-breakpoints="xs">Celular</th>
                                            <th data-breakpoints="xs">Ciudad</th>
                                            <th data-breakpoints="xs">Sede</th>
                                            <th data-type="date" data-breakpoints="xs">Fecha asistencia</th>
                                            <th data-type="date" data-breakpoints="xs">Fecha inscripción</th>
                                            <th data-type="date" data-breakpoints="xs">Fecha agendada</th>
                                            <th data-breakpoints="xs">Estado</th>
                                            <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rpInscritos" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("NroDocumento") %></td>
                                                    <td><%# Eval("Nombres") %></td>
                                                    <td><%# Eval("Apellidos") %></td>
                                                    <td><%# Eval("Email") %></td>
                                                    <td><i class="fab fa-whatsapp m-r-xs font-bold"></i><a href="https://wa.me/57<%# Eval("Celular") %>" target="_blank"><%# Eval("Celular") %></a></td>
                                                    <td><%# Eval("NombreCiudadSede") %></td>
                                                    <td><%# Eval("NombreSede") %></td>
                                                    <td><%# Eval("FechaAsistencia", "{0:dd MMM yyyy}") %></td>
                                                    <td><%# Eval("FechaInscripcion", "{0:dd MMM yyyy, HH:mm}") %></td>
                                                    <td><%# Eval("FechaHora", "{0:dd MMM yyyy, HH:mm}") %></td>
                                                    <td>
                                                        <asp:Literal ID="litEstado" runat="server"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <a runat="server" id="btnEliminar" href="#" class="btn btn-outline btn-danger pull-right m-r-xs"
                                                            style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false"><i class="fa fa-trash"></i></a>
                                                        <a runat="server" id="btnEditar" href="#" class="btn btn-outline btn-primary pull-right m-r-xs"
                                                            style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false"><i class="fa fa-edit"></i></a>
                                                        <a runat="server" id="btnAgendar" href="#" title="Agendar" class="btn btn-outline btn-success pull-right m-r-xs"
                                                            style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="true"><i class="fa fa-calendar-day"></i></a>
                                                        <a runat="server" id="btnEliminarAgenda" href="#" title="Eliminar Agenda" class="btn btn-outline btn-danger pull-right m-r-xs"
                                                           style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false"><i class="fa fa-calendar-check"></i></a>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>

                                <div class="modal fade" id="modal-agendar-info" tabindex="-1" role="dialog" aria-labelledby="modalLabel">
                                    <div class="modal-dialog modal-dialog-centered">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="modal-title" id="modalLabelAgendar">Agendar Gym Pass</h4>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <label>Nro. de Documento</label>
                                                        <div class="form-group">
                                                            <input type="text" class="form-control input-sm" id="infoDoc" name="txbNroDocumento" runat="server" readonly /> 
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 m-b-md">
                                                        <label>Nombre</label>
                                                        <div class="form-groupp">
                                                            <input type="text" class="form-control input-sm" id="infoNombre" name="txbNombres" runat="server" readonly />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <label>Fecha</label>
                                                        <div class="form-group">
                                                            <input type="text" class="form-control input-sm" id="txbFechaAgenda" name="txbFechaAgenda" runat="server" />
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 m-b-md">
                                                        <label>Hora</label>
                                                        <div class="input-group clockpicker" data-autoclose="true">
                                                            <input type="text" class="form-control input-sm" value="08:00" id="txbHoraAgenda" name="txbHoraAgenda" runat="server" />
                                                            <span class="input-group-addon">
                                                                <span class="fa fa-clock"></span>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button ID="btnAgendarGymPass" class="btn btn-success m-b-none" runat="server" Text="Agendar" OnClick="btnAgendarGymPass_Click" />
                                                <%--<button type="submit" class="btn btn-primary">Agendar</button>--%>
                                                <%--<button type="button" class="btn btn-warning" onclick="window.location.href = 'addevent.aspx?id'";><i class='fa fa-edit'></i>Editar</button>--%>
                                                <%--<button type="button" class="btn btn-warning" onclick="if(document.getElementById('event-allday').innerHTML == '0') { window.location.href = 'editevent.aspx?id=' + document.getElementById('event-id').innerHTML }";><i class='fa fa-edit'></i> Editar</button>--%>
                                                <%--<button type="button" class="btn btn-warning" data-dismiss="modal" onclick="window.location.href = 'eliminardisponibilidad.aspx?id=' + document.getElementById('event-id').innerHTML" runat="server" id="btnEliminar" visible="false"><i class='fa fa-trash m-r-sm'></i>Eliminar</button>--%>
                                                <button type="button" class="btn btn-danger" data-dismiss="modal"><i class='fa fa-times m-r-sm'></i>Cerrar</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                <div class="modal fade" id="modal-eliminar-agenda" tabindex="-1" role="dialog" aria-labelledby="modalLabel">
                                    <div class="modal-dialog modal-dialog-centered">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="modal-title" id="modalLabelEliminar">Eliminar Agenda Gym Pass</h4>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <p>¿Deseas eliminar la Agenda Gym Pass de...?</p>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <label>Nro. de Documento</label>
                                                        <div class="form-group">
                                                            <input type="text" class="form-control input-sm" id="infoDocEli" name="txbNroDocumento" runat="server" readonly /> 
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 m-b-md">
                                                        <label>Nombre</label>
                                                        <div class="form-groupp">
                                                            <input type="text" class="form-control input-sm" id="infoNombreEli" name="txbNombres" runat="server" readonly />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button ID="btnEliminarAgendaGymPass" class="btn btn-danger" runat="server" Text="Aceptar" OnClick="btnEliminarAgendaGymPass_Click" />
                                                <%--<button type="submit" class="btn btn-primary">Agendar</button>--%>
                                                <%--<button type="button" class="btn btn-warning" onclick="window.location.href = 'addevent.aspx?id'";><i class='fa fa-edit'></i>Editar</button>--%>
                                                <%--<button type="button" class="btn btn-warning" onclick="if(document.getElementById('event-allday').innerHTML == '0') { window.location.href = 'editevent.aspx?id=' + document.getElementById('event-id').innerHTML }";><i class='fa fa-edit'></i> Editar</button>--%>
                                                <%--<button type="button" class="btn btn-warning" data-dismiss="modal" onclick="window.location.href = 'eliminardisponibilidad.aspx?id=' + document.getElementById('event-id').innerHTML" runat="server" id="btnEliminar" visible="false"><i class='fa fa-trash m-r-sm'></i>Eliminar</button>--%>
                                                <button type="button" class="btn btn-success" data-dismiss="modal"><i class='fa fa-times m-r-sm'></i>Cancelar</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>

                    <%--Fin Contenido!!!!--%>
                </div>
            </div>

            <uc1:footer runat="server" ID="footer1" />

        </div>
        <uc1:rightsidebar runat="server" ID="rightsidebar1" />
    </div>

    <!-- Mainly scripts -->
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- FooTable -->
    <%--<script src="js/plugins/footable/footable.all.min.js"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

    <!-- Peity -->
    <script src="js/plugins/peity/jquery.peity.min.js"></script>
    <script src="js/demo/peity-demo.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- jQuery UI -->
    <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>

    <!-- Data picker -->
    <script src="js/plugins/datapicker/bootstrap-datepicker.js"></script>

    <!-- Clock picker -->
    <script src="js/plugins/clockpicker/clockpicker.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();
    </script>

    <%--Modal para Agendar Gym Pass--%>
    <script>
        $(document).ready(function () {
            // Delegación para manejar clics incluso si los botones se generan dinámicamente
            $('table').on('click', 'a[id*="btnAgendar"]', function (e) {
                e.preventDefault();

                // Encuentra la fila donde se hizo clic
                var row = $(this).closest('tr');
                var cells = row.find('td');

                // Extrae el texto de cada celda y lo coloca en el modal
                $('#infoDoc').val(cells.eq(0).text().trim());
                $('#infoNombre').val(cells.eq(1).text().trim());
                $('#infoSede').val(cells.eq(6).text().trim());

                // Muestra el modal con Bootstrap 3
                $('#modal-agendar-info').modal('show');
            });

            $('#data_1 .input-group.date').datepicker({
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

<%--Eliminar Agenda Gym Pass--%>
<script>
    $(document).ready(function () {
        // Delegación para manejar clics incluso si los botones se generan dinámicamente
        $('table').on('click', 'a[id*="btnEliminarAgenda"]', function (e) {
            e.preventDefault();

            // Encuentra la fila donde se hizo clic
            var row = $(this).closest('tr');
            var cells = row.find('td');

            // Extrae el texto de cada celda y lo coloca en el modal
            $('#infoDocEli').val(cells.eq(0).text().trim());
            $('#infoNombreEli').val(cells.eq(1).text().trim());

            // Muestra el modal con Bootstrap 3
            $('#modal-eliminar-agenda').modal('show');
        });
    });
</script>

</body>

</html>