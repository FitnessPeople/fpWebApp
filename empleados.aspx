<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="empleados.aspx.cs" Inherits="fpWebApp.empleados" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/indicadoresCEO.ascx" TagPrefix="uc1" TagName="indicadores01" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Empleados</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <style type="text/css" media="print">
        body {
            visibility: hidden;
            display: none
        }
    </style>

    <style type="text/css">
        /* Imagen cuando el empleado está activo (a color) */
        .img-activo {
            filter: none;
        }

        /* Imagen cuando el empleado está inactivo (en blanco y negro) */
        .img-inactivo {
            filter: grayscale(100%);
        }
    </style>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#empleados");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#sistema");
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
                    <i class="fa fa-user-tie modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Ayuda para la administración de Empleados</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar el listado de forma rápida y sencilla.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Busca y filtra empleados</b><br />
                        Usa el buscador para encontrar empleados específicos.<br />
                        <i class="fa-solid fa-magnifying-glass m-r-xs"></i>Filtra por: 
                        <i class="fa-solid fa-user m-r-xs" style="color: #0D6EFD;"></i><b>Nombre</b>, 
                        <i class="fa-solid fa-address-card m-r-xs" style="color: #0D6EFD;"></i><b>Cédula</b>, 
                        <i class="fa-solid fa-envelope m-r-xs" style="color: #0D6EFD;"></i><b>Correo</b>, 
                        <i class="fa-solid fa-mobile m-r-xs" style="color: #0D6EFD;"></i><b>Celular</b>,
                        <i class="fa-solid fa-user-tie m-r-xs" style="color: #0D6EFD;"></i><b>Cargo</b> o 
                        <i class="fa-solid fa-circle m-r-xs" style="color: #0D6EFD;"></i><b>Estado</b><br />
                        <i class="fa-solid fa-star m-r-xs" style="color: #FECE32;"></i>Tip: ¡Combina filtros para resultados más precisos!
                    <br />
                        <br />
                        <b>Paso 2: Revisa la tabla de resultados</b><br />
                        La tabla muestra toda la información clave de cada empleado.<br />
                        En la columna "Acciones" encontrarás estas opciones:<br />
                        <i class="fa fa-edit m-r-xs" style="color: #1AB394;"></i><b>Editar:</b> Modifica los datos del empleado.<br />
                        <i class="fa fa-trash m-r-xs" style="color: #DC3545;"></i><s><b>Eliminar:</b> Da de baja al empleado.</s>
                    <br />
                        <br />
                        <b>Paso 3: Acciones adicionales</b><br />
                        Al lado opuesto del buscador encontrarás dos botones útiles:<br />
                        <i class="fa-solid fa-file-export m-r-xs" style="color: #212529;"></i><b>Exportar a Excel:</b>
                        Genera un archivo Excel con los datos visibles en la tabla.<br />
                        <i class="fa-solid fa-square-check fa-lg m-r-xs" style="color: #18A689;"></i><b>Crear Nuevo Empleado:</b>
                        Te lleva a un formulario para registrar un nuevo empleado.
                   <br />
                        <br />
                        <i class="fa fa-exclamation-circle mr-2 m-r-xs"></i>Si tienes dudas, consulta con el administrador del sistema.
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
                    <h2><i class="fa fa-user-tie text-success m-r-sm"></i>Empleados</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Sistema</li>
                        <li class="active"><strong>Empleados</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">
                    <%--Inicio Contenido!!!!--%>

                    <uc1:indicadores01 runat="server" ID="indicadores01" />

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

                    <div class="row">
                        <div class="col-sm-8">
                            <div class="ibox float-e-margins" runat="server" id="divContenido">
                                <div class="ibox-title">
                                    <h5>Lista de empleados</h5>
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
                                                <a class="btn btn-success pull-right dim m-l-md" style="font-size: 12px;"
                                                    href="nuevoempleado" title="Agregar empleado"
                                                    runat="server" id="btnAgregar" visible="false"><i class="fa fa-square-plus m-r-xs"></i>NUEVO
                                                </a>
                                                <%--<asp:LinkButton ID="lbExportarExcel" runat="server" CausesValidation="false"
                                                    CssClass="btn btn-info pull-right dim m-l-md" Style="font-size: 12px;"
                                                    OnClick="lbExportarExcel_Click">
                                                    <i class="fa fa-file-excel m-r-xs"></i>EXCEL
                                                </asp:LinkButton>--%>
                                            </div>
                                        </div>
                                    </form>

                                    <table id="tabla" class="footable table table-striped list-group-item-text" data-paging-size="15"
                                        data-filter-min="3" data-filter-placeholder="Buscar"
                                        data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}"
                                        data-paging-limit="10" data-filtering="true"
                                        data-filter-container="#filter-form-container" data-filter-delay="300"
                                        data-filter-dropdown-title="Buscar en:" data-filter-position="left"
                                        data-empty="Sin resultados">
                                        <thead>
                                            <tr>
                                                <th data-breakpoints="xs"></th>
                                                <th data-breakpoints="xs" data-sortable="true" data-type="text">Nombre</th>
                                                <th data-sortable="false">Documento</th>
                                                <th data-breakpoints="xs">Teléfono corporativo</th>
                                                <%--<th data-breakpoints="xs sm">Teléfono personal</th>--%>
                                                <%--<th data-breakpoints="xs sm md">Correo</th>--%>
                                                <%--<th data-breakpoints="xs sm">Cargo</th>--%>
                                                <th data-breakpoints="xs sm">Sede</th>
                                                <%--<th data-hide="phone,tablet">Cargo</th>--%>
                                                <th data-type="date" data-breakpoints="xs sm">Cumpleaños</th>
                                                <th class="text-nowrap" data-breakpoints="xs sm">Estado</th>
                                                <%--<th data-breakpoints="all" data-title="Info"></th>--%>
                                                <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rpEmpleados" runat="server" OnItemDataBound="rpEmpleados_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="client-avatar">
                                                            <img alt="image" src="img/empleados/<%# Eval("FotoEmpleado") %>"></td>
                                                        <td><a data-toggle="tab" href='#contact-<%# Eval("NombreEmpleado").ToString().Substring(0,3).ToUpper() %><%# Eval("DocumentoEmpleado") %>' class="client-link"><%# Eval("NombreEmpleado") %></a></td>
                                                        <td><%# Eval("DocumentoEmpleado") %></td>
                                                        <td><i class="fab fa-whatsapp m-r-xs font-bold"></i><a href="https://wa.me/57<%# Eval("TelefonoCorporativo") %>" target="_blank"><%# Eval("TelefonoCorporativo") %></a></td>
                                                        <%--<td><i class="fab fa-whatsapp m-r-xs font-bold"></i><a href="https://wa.me/57<%# Eval("TelefonoEmpleado") %>" target="_blank"><%# Eval("TelefonoEmpleado") %></a></td>--%>
                                                        <%--<td><i class="fa fa-envelope m-r-xs font-bold"></i><a href="mailto:<%# Eval("EmailCorporativo") %>" title="Enviar correo"><%# Eval("EmailCorporativo") %></a></td>--%>
                                                        <%--<td><a href="cargos" title="Ir a Cargos"><i class="fa fa-user-nurse m-r-xs font-bold"></i><%# Eval("Cargo") %></a></td>--%>
                                                        <td><%# Eval("NombreSede") %></td>
                                                        <%--<td><i class="fa fa-user-tie m-r-xs font-bold"></i><%# Eval("CargoEmpleado") %></td>--%>
                                                        <td><%# Eval("icono") %><%# Eval("FechaNacEmpleado", "{0:dd MMM}") %></td>
                                                        <td><span class="badge badge-<%# Eval("label") %>"><%# Eval("Estado") %></span></td>
                                                        <td>
                                                            <a runat="server" id="btnEliminar" href="#" class="btn btn-outline btn-danger pull-right m-r-xs"
                                                                style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false" title="Eliminar"><i class="fa fa-trash"></i></a>
                                                            <a runat="server" id="btnEditar" href="#" class="btn btn-outline btn-primary pull-right m-r-xs"
                                                                style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false" title="Editar"><i class="fa fa-edit"></i></a>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="ibox ">

                                <div class="ibox-content">
                                    <div class="tab-content">
                                        <asp:Repeater ID="rpTabEmpleados" runat="server" OnItemDataBound="rpTabEmpleados_ItemDataBound">
                                            <ItemTemplate>
                                                <div id='contact-<%# Eval("NombreEmpleado").ToString().Substring(0,3).ToUpper() %><%# Eval("DocumentoEmpleado") %>' class='tab-pane <%# Eval("DocumentoEmpleado").ToString() == ViewState["EmployeeDoc"]?.ToString() ? "active" : "" %>'>
                                                    <div class="row m-b-lg">
                                                        <div class="ibox-content text-center">
                                                            <h2><%# Eval("NombreEmpleado") %></h2>
                                                            <div class="m-b-sm">
                                                                <img alt="image" 
                                                                 class='img-circle <%# Eval("Estado").ToString() == "Inactivo" ? "img-inactivo" : "img-activo" %>' 
                                                                 src='img/empleados/<%# Eval("FotoEmpleado") %>' 
                                                                 width="120">
                                                                <span class="label label-danger">Rh: <%# Eval("TipoSangre") %></span>
                                                            </div>
                                                            <p class="font-bold"><%# Eval("Cargo") %></p>

                                                            <div class="text-center">
                                                                <a runat="server" id="btnEditarTab" href="#" class="btn btn-xs btn-primary"><i class="fa fa-edit m-r-xs" visible="false"></i>Editar</a>
                                                                <%--<asp:LinkButton ID="lkbCambiarEstado" runat="server" 
                                                                    CssClass="btn btn-xs btn-warning" OnClick="lkbCambiarEstado_Click">
                                                                    <i class="fa fa-rotate m-r-xs"></i>Cambiar estado
                                                                </asp:LinkButton>--%>
                                                                <a runat="server" id="btnCambiarEstado" href="#" visible="false" 
                                                                    class='btn btn-xs btn-danger'><i class="fa fa-rotate m-r-xs"></i><%# Eval("Estado") %> (cambiar)
                                                                </a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="client-detail">
                                                        <div class="full-height-scroll">

                                                            <div class="feed-activity-list">

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-map-marker m-r-xs"></i>Dirección</strong>
                                                                        <div class="text-info"><%# Eval("DireccionEmpleado") %></div>
                                                                        <small class="text-muted"><i class="fa fa-city m-r-xs"></i><%# Eval("NombreCiudad") %></small>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fab fa-whatsapp m-r-xs"></i>Teléfono corporativo</strong>
                                                                        <div><a href="https://wa.me/57<%# Eval("TelefonoCorporativo") %>" target="_blank"><%# Eval("TelefonoCorporativo") %></a></div>
                                                                        <small class="text-muted"><i class="fa fa-envelope m-r-xs"></i>Email corporativo: <%# Eval("EmailCorporativo") %></small>
                                                                    </div>
                                                                </div>

                                                                <%--<div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-envelope m-r-xs"></i>Email corporativo</strong>
                                                                        <div><%# Eval("EmailCorporativo") %></div>
                                                                    </div>
                                                                </div>--%>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fab fa-whatsapp m-r-xs"></i>Teléfono personal</strong>
                                                                        <div><a href="https://wa.me/57<%# Eval("TelefonoEmpleado") %>" target="_blank"><%# Eval("TelefonoEmpleado") %></a></div>
                                                                        <small class="text-muted"><i class="fa fa-envelope m-r-xs"></i>Email personal: <%# Eval("EmailEmpleado") %></small>
                                                                    </div>
                                                                </div>

                                                                <%--<div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-envelope m-r-xs"></i>Email personal</strong>
                                                                        <div><%# Eval("EmailEmpleado") %></div>
                                                                    </div>
                                                                </div>--%>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-ring m-r-xs"></i>Estado civil</strong>
                                                                        <div class="text-info"><%# Eval("EstadoCivil") %></div>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-file-lines m-r-xs"></i>Tipo de contrato</strong>
                                                                        <div class="text-info"><%# Eval("TipoContrato") %></div>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-graduation-cap m-r-xs"></i>Nivel estudio</strong>
                                                                        <div class="text-info"><%# Eval("NivelEstudio") %></div>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <small class="pull-right text-navy">Estrato: <%# Eval("EstratoSocioeconomico") %></small>
                                                                        <strong><i class="fa fa-house m-r-xs"></i>Tipo de vivienda</strong>
                                                                        <div class="text-info"><%# Eval("TipoVivienda") %></div>
                                                                        <small class="text-muted">Personas nucleo familiar: <%# Eval("PersonasNucleoFamiliar") %></small>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-person-rays m-r-xs"></i>Actividad extra</strong>
                                                                        <div class="text-info"><%# Eval("ActividadExtra") %></div>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-martini-glass m-r-xs"></i>Consume licor</strong>
                                                                        <div class="text-info"><%# Eval("ConsumeLicor") %></div>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-car m-r-xs"></i>Medio de transporte</strong>
                                                                        <div class="text-info"><%# Eval("MedioTransporte") %></div>
                                                                    </div>
                                                                </div>

                                                                <%--<div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-car m-r-xs"></i>Notas</strong>
                                                                        <div>
                                                                            <p>
                                                                                Lorem ipsum dolor sit amet, consectetur adipisicing elit, 
                                                                                sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                                                                            </p>

                                                                        </div>
                                                                    </div>
                                                                </div>--%>

                                                            </div>

                                                            <hr />
                                                            <strong>Última actividad</strong>
                                                            <div id="vertical-timeline" class="vertical-container dark-timeline">

                                                                <asp:Repeater ID="rpActividades" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="vertical-timeline-block">
                                                                            <div class="vertical-timeline-icon gray-bg">
                                                                                <i class="fa fa-<%# Eval("Label") %>"></i>
                                                                            </div>
                                                                            <div class="vertical-timeline-content">
                                                                                <p><%# Eval("Accion") %></p>
                                                                                <p class="text-info"><%# Eval("DescripcionLog") %></p>
                                                                                <span class="vertical-date small text-muted"><%# Eval("FechaHora") %></span>
                                                                            </div>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="wrapper wrapper-content animated fadeInRight" style="padding: 20px 10px 0px;">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Estadísticas</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row text-center">
                                            <div class="col-lg-3">
                                                <canvas id="barChart1" height="150"></canvas>
                                                <h5><i class="fa fa-venus-mars fa-2x text-navy m-r-xs"></i>Géneros</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="barChart2" height="150"></canvas>
                                                <h5><i class="fa fa-city text-navy fa-2x m-r-xs"></i>Ciudades</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="barChart3" height="150"></canvas>
                                                <h5><i class="fa fa-ring text-navy fa-2x m-r-xs"></i>Estado civil</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="barChart4" height="150"></canvas>
                                                <h5><i class="fa fa-file-lines fa-2x text-navy m-r-xs"></i>Tipo de contrato</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="wrapper wrapper-content animated fadeInRight" style="padding: 20px 10px 0px;">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Estadísticas</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row text-center">
                                            <div class="col-lg-3">
                                                <canvas id="barChart" height="150"></canvas>
                                                <h5><i class="fa fa-graduation-cap fa-2x text-navy m-r-xs"></i>Nivel de estudio</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="doughnutChart6" height="150"></canvas>
                                                <h5><i class="fa fa-house fa-2x text-navy m-r-xs"></i>Tipo de vivienda</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="doughnutChart7" height="150"></canvas>
                                                <h5><i class="fa fa-person-rays fa-2x text-navy m-r-xs"></i>Actividad extra</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="doughnutChart8" height="150"></canvas>
                                                <h5><i class="fa fa-martini-glass fa-2x text-navy m-r-xs"></i>Consumo de licor</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="wrapper wrapper-content animated fadeInRight" style="padding: 20px 10px 0px;">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Estadísticas</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row text-center">
                                            <div class="col-lg-3">
                                                <canvas id="barChart9" height="150"></canvas>
                                                <h5><i class="fa fa-person-cane fa-2x text-navy m-r-xs"></i>Edades</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="barChart10" height="150"></canvas>
                                                <h5><i class="fa fa-car fa-2x text-navy m-r-xs"></i>Medio de transporte</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="barChart11" height="150"></canvas>
                                                <h5><i class="fa fa-droplet fa-2x text-navy m-r-xs"></i>Tipo de sangre</h5>
                                            </div>
                                            <%--<div class="col-lg-3">
                                                <canvas id="doughnutChart8" height="150"></canvas>
                                                <h5>Consumo licor</h5>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
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

    <!-- Peity -->
    <script src="js/plugins/peity/jquery.peity.min.js"></script>
    <script src="js/demo/peity-demo.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- jQuery UI -->
    <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>

    <!-- ChartJS-->
    <script src="js/plugins/chartJs/Chart.min.js"></script>

    <!-- FooTable -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $(function () {
            $('#tabla').footable();
        });
    </script>

    <script>
        // Gráfico de Géneros

        $(function () {

            const colores1 = cantidades1.map((_, index) => {
                const hue = (index * 360) / cantidades1.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres1,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores1,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades1
                    }
                ]
            };

            var barOptions = {
                responsive: true,
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                animation: {
                    onComplete: function () {
                        var chartInstance = this.chart;
                        var ctx = chartInstance.ctx;

                        ctx.font = "10px Arial";
                        ctx.fillStyle = "#000";
                        ctx.textAlign = "center";
                        ctx.textBaseline = "bottom";

                        this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            meta.data.forEach(function (bar, index) {
                                var value = dataset.data[index];
                                ctx.fillText(value, bar._model.x, bar._model.y - 5);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("barChart1").getContext("2d");
            new Chart(ctx4, { type: 'bar', data: barData, options: barOptions });


            // Grafica Ciudades

            const colores2 = cantidades2.map((_, index) => {
                const hue = (index * 360) / cantidades2.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres2,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores2,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades2
                    }
                ]
            };

            var barOptions = {
                responsive: true,
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                animation: {
                    onComplete: function () {
                        var chartInstance = this.chart;
                        var ctx = chartInstance.ctx;

                        ctx.font = "10px Arial";
                        ctx.fillStyle = "#000";
                        ctx.textAlign = "center";
                        ctx.textBaseline = "bottom";

                        this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            meta.data.forEach(function (bar, index) {
                                var value = dataset.data[index];
                                ctx.fillText(value, bar._model.x + 10, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("barChart2").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });



            // Grafica Estado civil

            const colores3 = cantidades3.map((_, index) => {
                const hue = (index * 360) / cantidades3.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres3,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores3,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades3
                    }
                ]
            };

            var barOptions = {
                responsive: true,
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                animation: {
                    onComplete: function () {
                        var chartInstance = this.chart;
                        var ctx = chartInstance.ctx;

                        ctx.font = "10px Arial";
                        ctx.fillStyle = "#000";
                        ctx.textAlign = "center";
                        ctx.textBaseline = "bottom";

                        this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            meta.data.forEach(function (bar, index) {
                                var value = dataset.data[index];
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("barChart3").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Grafica TipoContrato

            const colores4 = cantidades4.map((_, index) => {
                const hue = (index * 360) / cantidades4.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres4,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores4,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades4
                    }
                ]
            };

            var barOptions = {
                responsive: true,
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                animation: {
                    onComplete: function () {
                        var chartInstance = this.chart;
                        var ctx = chartInstance.ctx;

                        ctx.font = "10px Arial";
                        ctx.fillStyle = "#000";
                        ctx.textAlign = "center";
                        ctx.textBaseline = "bottom";

                        this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            meta.data.forEach(function (bar, index) {
                                var value = dataset.data[index];
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("barChart4").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Gráfica Nivel de Estudio

            const colores5 = cantidades5.map((_, index) => {
                const hue = (index * 360) / cantidades5.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres5,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores5,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades5
                    }
                ]
            };

            var barOptions = {
                responsive: true,
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                animation: {
                    onComplete: function () {
                        var chartInstance = this.chart;
                        var ctx = chartInstance.ctx;

                        ctx.font = "10px Arial";
                        ctx.fillStyle = "#000";
                        ctx.textAlign = "center";
                        ctx.textBaseline = "bottom";

                        this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            meta.data.forEach(function (bar, index) {
                                var value = dataset.data[index];
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("barChart").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Gráfica Tipo de Vivienda

            const colores6 = cantidades6.map((_, index) => {
                const hue = (index * 360) / cantidades6.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });
            
            var barData = {
                labels: nombres6,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores6,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades6
                    }
                ]
            };

            var barOptions = {
                responsive: true,
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                animation: {
                    onComplete: function () {
                        var chartInstance = this.chart;
                        var ctx = chartInstance.ctx;

                        ctx.font = "10px Arial";
                        ctx.fillStyle = "#000";
                        ctx.textAlign = "center";
                        ctx.textBaseline = "bottom";

                        this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            meta.data.forEach(function (bar, index) {
                                var value = dataset.data[index];
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("doughnutChart6").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Gráfica Actividad Extra

            const colores7 = cantidades7.map((_, index) => {
                const hue = (index * 360) / cantidades7.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });
            
            var barData = {
                labels: nombres7,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores7,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades7
                    }
                ]
            };

            var barOptions = {
                responsive: true,
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                animation: {
                    onComplete: function () {
                        var chartInstance = this.chart;
                        var ctx = chartInstance.ctx;

                        ctx.font = "10px Arial";
                        ctx.fillStyle = "#000";
                        ctx.textAlign = "center";
                        ctx.textBaseline = "bottom";

                        this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            meta.data.forEach(function (bar, index) {
                                var value = dataset.data[index];
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("doughnutChart7").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Gráfica Consume Licor

            const colores8 = cantidades8.map((_, index) => {
                const hue = (index * 360) / cantidades8.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });
            
            var barData = {
                labels: nombres8,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores8,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades8
                    }
                ]
            };

            var barOptions = {
                responsive: true,
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                animation: {
                    onComplete: function () {
                        var chartInstance = this.chart;
                        var ctx = chartInstance.ctx;

                        ctx.font = "10px Arial";
                        ctx.fillStyle = "#000";
                        ctx.textAlign = "center";
                        ctx.textBaseline = "bottom";

                        this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            meta.data.forEach(function (bar, index) {
                                var value = dataset.data[index];
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("doughnutChart8").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Gráfico de edades

            const colores9 = cantidades9.map((_, index) => {
                const hue = (index * 360) / cantidades9.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres9,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores9,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades9
                    }
                ]
            };

            var barOptions = {
                responsive: true,
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                animation: {
                    onComplete: function () {
                        var chartInstance = this.chart;
                        var ctx = chartInstance.ctx;

                        ctx.font = "10px Arial";
                        ctx.fillStyle = "#000";
                        ctx.textAlign = "center";
                        ctx.textBaseline = "bottom";

                        this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            meta.data.forEach(function (bar, index) {
                                var value = dataset.data[index];
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("barChart9").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Gráfico de Medio de Transporte

            const colores10 = cantidades10.map((_, index) => {
                const hue = (index * 360) / cantidades10.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres10,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores10,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades10
                    }
                ]
            };

            var barOptions = {
                responsive: true,
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                animation: {
                    onComplete: function () {
                        var chartInstance = this.chart;
                        var ctx = chartInstance.ctx;

                        ctx.font = "10px Arial";
                        ctx.fillStyle = "#000";
                        ctx.textAlign = "center";
                        ctx.textBaseline = "bottom";

                        this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            meta.data.forEach(function (bar, index) {
                                var value = dataset.data[index];
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("barChart10").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Gráfico de Tipo de Sangre

            const colores11 = cantidades11.map((_, index) => {
                const hue = (index * 360) / cantidades11.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres11,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores11,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades11
                    }
                ]
            };

            var barOptions = {
                responsive: true,
                legend: {
                    display: false
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                animation: {
                    onComplete: function () {
                        var chartInstance = this.chart;
                        var ctx = chartInstance.ctx;

                        ctx.font = "10px Arial";
                        ctx.fillStyle = "#000";
                        ctx.textAlign = "center";
                        ctx.textBaseline = "bottom";

                        this.data.datasets.forEach(function (dataset, i) {
                            var meta = chartInstance.controller.getDatasetMeta(i);
                            meta.data.forEach(function (bar, index) {
                                var value = dataset.data[index];
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("barChart11").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });
        });

    </script>

</body>

</html>
