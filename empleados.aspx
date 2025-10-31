<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="empleados.aspx.cs" Inherits="fpWebApp.empleados" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/indicadores01.ascx" TagPrefix="uc1" TagName="indicadores01" %>
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
                    <i class="fa fa-id-card modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para visualizar los empleados registrados</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar el listado de forma rápida y sencilla.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Busca y filtra afiliados</b><br />
                        Usa el buscador para encontrar afiliados específicos.<br />
                        <i class="fa-solid fa-magnifying-glass"></i>Filtra por: 
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i><b>Nombre</b>, 
                        <i class="fa-solid fa-address-card" style="color: #0D6EFD;"></i><b>Cédula</b>, 
                        <i class="fa-solid fa-envelope" style="color: #0D6EFD;"></i><b>Correo</b>, 
                        <i class="fa-solid fa-mobile" style="color: #0D6EFD;"></i><b>Celular</b>,
                        <i class="fa-solid fa-user-tie" style="color: #0D6EFD;"></i><b>Cargo</b> o 
                        <i class="fa-solid fa-circle" style="color: #0D6EFD;"></i><b>Estado</b><br />
                        <i class="fa-solid fa-star" style="color: #FECE32;"></i>Tip: ¡Combina filtros para resultados más precisos!
                    <br />
                        <br />
                        <b>Paso 2: Revisa la tabla de resultados</b><br />
                        La tabla muestra toda la información clave de cada afiliado.<br />
                        En la columna "Acciones" encontrarás estas opciones:<br />
                        <i class="fa fa-edit" style="color: #1AB394;"></i><b>Editar:</b> Modifica los datos del afiliado.<br />
                        <i class="fa fa-trash" style="color: #DC3545;"></i><b>Eliminar:</b> Da de baja al afiliado.
                    <br />
                        <br />
                        <b>Paso 3: Acciones adicionales</b><br />
                        Al lado opuesto del buscador encontrarás dos botones útiles:<br />
                        <i class="fa-solid fa-file-export" style="color: #212529;"></i><b>Exportar a Excel:</b>
                        Genera un archivo Excel con los datos visibles en la tabla.<br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i><b>Crear Nuevo Empleado:</b>
                        Te lleva a un formulario para registrar un nuevo empleado.
                   <br />
                        <br />
                        <i class="fa fa-exclamation-circle mr-2"></i>Si tienes dudas, no dudes en consultar con el administrador del sistema.
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
                                                <asp:LinkButton ID="lbExportarExcel" runat="server" CausesValidation="false"
                                                    CssClass="btn btn-info pull-right dim m-l-md" Style="font-size: 12px;"
                                                    OnClick="lbExportarExcel_Click">
                                                    <i class="fa fa-file-excel m-r-xs"></i>EXCEL
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </form>

                                    <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                        data-filter-min="3" data-filter-placeholder="Buscar"
                                        data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}"
                                        data-paging-limit="10" data-filtering="true"
                                        data-filter-container="#filter-form-container" data-filter-delay="300"
                                        data-filter-dropdown-title="Buscar en:" data-filter-position="left"
                                        data-empty="Sin resultados">
                                        <thead>
                                            <tr>
                                                <th data-breakpoints="xs"></th>
                                                <th>Nombre</th>
                                                <th data-sortable="false">Documento</th>
                                                <th data-breakpoints="xs">Teléfono corporativo</th>
                                                <%--<th data-breakpoints="xs sm">Teléfono personal</th>--%>
                                                <%--<th data-breakpoints="xs sm md">Correo</th>--%>
                                                <%--<th data-breakpoints="xs sm">Cargo</th>--%>
                                                <%--<th data-breakpoints="xs sm">Sede</th>--%>
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
                                                        <td><a data-toggle="tab" href='#contact-<%# Eval("DocumentoEmpleado") %>' class="client-link"><%# Eval("NombreEmpleado") %></a></td>
                                                        <td><%# Eval("DocumentoEmpleado") %></td>
                                                        <td><i class="fab fa-whatsapp m-r-xs font-bold"></i><a href="https://wa.me/57<%# Eval("TelefonoCorporativo") %>" target="_blank"><%# Eval("TelefonoCorporativo") %></a></td>
                                                        <%--<td><i class="fab fa-whatsapp m-r-xs font-bold"></i><a href="https://wa.me/57<%# Eval("TelefonoEmpleado") %>" target="_blank"><%# Eval("TelefonoEmpleado") %></a></td>--%>
                                                        <%--<td><i class="fa fa-envelope m-r-xs font-bold"></i><a href="mailto:<%# Eval("EmailCorporativo") %>" title="Enviar correo"><%# Eval("EmailCorporativo") %></a></td>--%>
                                                        <%--<td><a href="cargos" title="Ir a Cargos"><i class="fa fa-user-nurse m-r-xs font-bold"></i><%# Eval("Cargo") %></a></td>--%>
                                                        <%--<td><%# Eval("NombreSede") %></td>--%>
                                                        <%--<td><i class="fa fa-user-tie m-r-xs font-bold"></i><%# Eval("CargoEmpleado") %></td>--%>
                                                        <td><%# Eval("icono") %><%# Eval("FechaNacEmpleado", "{0:dd MMM}") %></td>
                                                        <td><span class="badge badge-<%# Eval("label") %>"><%# Eval("Estado") %></span></td>
                                                        <%--<td class="table-bordered">
                                                            <table class="table table-bordered">
                                                                <tr>
                                                                    <th width="34%"><i class="fa fa-map-location-dot m-r-xs"></i>Dirección</th>
                                                                    <th width="33%"><i class="fa fa-city m-r-xs"></i>Ciudad</th>
                                                                    <th width="33%"><i class="fa fa-user-tie m-r-xs"></i>Cargo</th>
                                                                </tr>
                                                                <tr>
                                                                    <td><%# Eval("DireccionEmpleado") %></td>
                                                                    <td><%# Eval("NombreCiudad") %> (<%# Eval("NombreEstado") %>)</td>
                                                                    <td><%# Eval("Cargo") %></td>
                                                                </tr>
                                                                <tr>
                                                                    <th><i class="fa fa-hashtag m-r-xs"></i>Nro Contrato</th>
                                                                    <th><i class="fa fa-file-lines m-r-xs"></i>Tipo Contrato</th>
                                                                    <th><i class="fa fa-clock m-r-xs"></i>Duración</th>
                                                                </tr>
                                                                <tr>
                                                                    <td><%# Eval("NroContrato") %></td>
                                                                    <td><%# Eval("TipoContrato") %></td>
                                                                    <td style="vertical-align: central;">
                                                                        <span class="pie"><%# Eval("diastrabajados") %>/<%# Eval("diastotales") %></span>
                                                                        <%# Eval("FechaInicio", "{0:dd MMM yyyy}") %> - <%# Eval("FechaFinal", "{0:dd MMM yyyy}") %>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>--%>
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
                                                <div id='contact-<%# Eval("DocumentoEmpleado") %>' class='tab-pane <%# Eval("DocumentoEmpleado").ToString() == ViewState["EmployeeDoc"]?.ToString() ? "active" : "" %>'>
                                                    <div class="row m-b-lg">
                                                        <div class="ibox-content text-center">
                                                            <h2><%# Eval("NombreEmpleado") %></h2>
                                                            <div class="m-b-sm">
                                                                <img alt="image" class="img-circle" src="img/empleados/<%# Eval("FotoEmpleado") %>" width="120">
                                                            </div>
                                                            <p class="font-bold"><%# Eval("Cargo") %></p>

                                                            <div class="text-center">
                                                                <a class="btn btn-xs btn-white"><i class="fa fa-thumbs-up"></i>Like </a>
                                                                <a class="btn btn-xs btn-primary"><i class="fa fa-heart"></i>Love</a>
                                                            </div>
                                                        </div>
                                                        <%--<div class="col-lg-4 text-center">
                                                            <h3><%# Eval("NombreEmpleado") %></h3>
                                                            <div class="m-b-sm">
                                                                <img alt="image" class="img-circle" src="img/empleados/<%# Eval("FotoEmpleado") %>" style="width: 62px" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-8">
                                                            <strong>Acerca de mi</strong>
                                                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
                                                            <div class="user-button">
                                                                <div class="row">
                                                                    <div class="col-md-6">
                                                                        <button type="button" class="btn btn-primary btn-sm btn-block"><i class="fa fa-envelope"></i> Send Message</button>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <button type="button" class="btn btn-default btn-sm btn-block"><i class="fa fa-coffee"></i> Buy a coffee</button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                    </div>
                                                    <div class="client-detail">
                                                        <div class="full-height-scroll">
                                                            <%--<strong>Detalles</strong>--%>

                                                            <%--<ul class="list-group clear-list">
                                                                <li class="list-group-item fist-item">
                                                                    <span class="pull-right"><%# Eval("DireccionEmpleado") %></span>
                                                                    <i class="fa fa-map-marker m-r-xs"></i>Dirección
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <span class="pull-right"><%# Eval("NombreCiudad") %></span>
                                                                    <i class="fa fa-city m-r-xs"></i>Ciudad
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <span class="pull-right"><%# Eval("TelefonoEmpleado") %></span>
                                                                    <i class="fab fa-whatsapp m-r-xs"></i>Teléfono personal
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <span class="pull-right"><%# Eval("EmailCorporativo") %></span>
                                                                    <i class="fa fa-envelope m-r-xs"></i>Email corporativo
                                                                </li>
                                                                <li class="list-group-item">
                                                                    <span class="pull-right"><%# Eval("EmailEmpleado") %></span>
                                                                    <i class="fa fa-envelope m-r-xs"></i>Correo personal
                                                                </li>
                                                            </ul>--%>

                                                            <div class="feed-activity-list">

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-map-marker m-r-xs"></i>Dirección</strong>
                                                                        <div><%# Eval("DireccionEmpleado") %></div>
                                                                        <small class="text-muted"><%# Eval("NombreCiudad") %></small>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fab fa-whatsapp m-r-xs"></i>Teléfono personal</strong>
                                                                        <div><%# Eval("TelefonoEmpleado") %></div>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-envelope m-r-xs"></i>Email corporativo</strong>
                                                                        <div><%# Eval("EmailCorporativo") %></div>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-envelope m-r-xs"></i>Email personal</strong>
                                                                        <div><%# Eval("EmailEmpleado") %></div>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-ring m-r-xs"></i>Estado civil</strong>
                                                                        <div><%# Eval("EstadoCivil") %></div>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-file m-r-xs"></i>Tipo de contrato</strong>
                                                                        <div><%# Eval("TipoContrato") %></div>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-graduation-cap m-r-xs"></i>Nivel estudio</strong>
                                                                        <div><%# Eval("NivelEstudio") %></div>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <small class="pull-right text-navy">Estrato: <%# Eval("EstratoSocioeconomico") %></small>
                                                                        <strong><i class="fa fa-house m-r-xs"></i>Tipo de vivienda</strong>
                                                                        <div><%# Eval("TipoVivienda") %></div>
                                                                        <small class="text-muted">Personas nucleo familiar: <%# Eval("PersonasNucleoFamiliar") %></small>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-person-rays m-r-xs"></i>Actividad extra</strong>
                                                                        <div><%# Eval("ActividadExtra") %></div>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-martini-glass m-r-xs"></i>Consume licor</strong>
                                                                        <div><%# Eval("ConsumeLicor") %></div>
                                                                    </div>
                                                                </div>

                                                                <div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-car m-r-xs"></i>Medio de transporte</strong>
                                                                        <div><%# Eval("MedioTransporte") %></div>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                            <strong>Notas</strong>
                                                            <p>
                                                                Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod
    tempor incididunt ut labore et dolore magna aliqua.
                                       
                                                            </p>
                                                            <hr />
                                                            <strong>Última actividad</strong>
                                                            <div id="vertical-timeline" class="vertical-container dark-timeline">

                                                                <asp:Repeater ID="rpActividades" runat="server">
                                                                    <ItemTemplate>
                                                                        <div class="vertical-timeline-block">
                                                                            <div class="vertical-timeline-icon gray-bg">
                                                                                <i class="fa fa-coffee"></i>
                                                                            </div>
                                                                            <div class="vertical-timeline-content">
                                                                                <p><%# Eval("Accion") %></p>
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

                    <div class="wrapper wrapper-content animated fadeInRight">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Estadísticas</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row text-center">
                                            <div class="col-lg-3">
                                                <canvas id="doughnutChart1" width="250" height="150" style="margin: 18px auto 0"></canvas>
                                                <h5>Géneros</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="doughnutChart2" width="250" height="150" style="margin: 18px auto 0"></canvas>
                                                <h5>Ciudades</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="doughnutChart3" width="250" height="150" style="margin: 18px auto 0"></canvas>
                                                <h5>Estado civil</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="doughnutChart4" width="250" height="150" style="margin: 18px auto 0"></canvas>
                                                <h5>Tipo de contrato</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="wrapper wrapper-content animated fadeInRight">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Estadísticas</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row text-center">
                                            <div class="col-lg-3">
                                                <canvas id="barChart" height="140"></canvas>
                                                <h5>Nivel estudio</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="doughnutChart6" width="150" height="80" style="margin: 18px auto 0"></canvas>
                                                <h5>Tipo de vivienda</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="doughnutChart7" width="150" height="80" style="margin: 18px auto 0"></canvas>
                                                <h5>Actividad extra</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="doughnutChart8" width="150" height="80" style="margin: 18px auto 0"></canvas>
                                                <h5>Consumo licor</h5>
                                            </div>
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

    <!-- FooTable -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

    <!-- Peity -->
    <script src="js/plugins/peity/jquery.peity.min.js"></script>
    <script src="js/demo/peity-demo.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- jQuery UI -->
    <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>

    <!-- Gráficas -->
    <!-- ChartJS-->
    <script src="js/plugins/chartJs/Chart.min.js"></script>
    <script src="js/demo/chartjs-demo.js"></script>

    <!-- d3 and c3 charts -->
    <script src="js/plugins/d3/d3.min.js"></script>
    <script src="js/plugins/c3/c3.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();
    </script>

    <script>

        $(function () {

            var doughnutOptions = {
                responsive: false,
                legend: {
                    display: false
                }
            };


            // Grafica Generos
            var doughnutData = {
                labels: nombres1,
                datasets: [{
                    data: cantidades1,
                    backgroundColor: colores1
                }]
            };

            var ctx4 = document.getElementById("doughnutChart1").getContext("2d");
            new Chart(ctx4, { type: 'doughnut', data: doughnutData, options: doughnutOptions });


            // Grafica Ciudades
            var doughnutData = {
                labels: nombres2,
                datasets: [{
                    data: cantidades2,
                    backgroundColor: colores2
                }]
            };

            var ctx4 = document.getElementById("doughnutChart2").getContext("2d");
            new Chart(ctx4, { type: 'doughnut', data: doughnutData, options: doughnutOptions });


            // Grafica Estado civil
            var doughnutData = {
                labels: nombres3,
                datasets: [{
                    data: cantidades3,
                    backgroundColor: colores3
                }]
            };

            var ctx4 = document.getElementById("doughnutChart3").getContext("2d");
            new Chart(ctx4, { type: 'doughnut', data: doughnutData, options: doughnutOptions });


            // Grafica TipoContrato
            var doughnutData = {
                labels: nombres4,
                datasets: [{
                    data: cantidades4,
                    backgroundColor: colores4
                }]
            };

            var ctx4 = document.getElementById("doughnutChart4").getContext("2d");
            new Chart(ctx4, { type: 'doughnut', data: doughnutData, options: doughnutOptions });


            var barData = {
                labels: ["January", "February", "March", "April", "May", "June", "July"],
                datasets: [
                    {
                        label: "Data 1",
                        backgroundColor: 'rgba(220, 220, 220, 0.5)',
                        pointBorderColor: "#fff",
                        data: [65, 59, 80, 81, 56, 55, 40]
                    },
                    {
                        label: "Data 2",
                        backgroundColor: 'rgba(26,179,148,0.5)',
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: [28, 48, 40, 19, 86, 27, 90]
                    }
                ]
            };

            var barOptions = {
                responsive: true
            };


            var ctx2 = document.getElementById("barChart").getContext("2d");
            new Chart(ctx2, { type: 'bar', data: barData, options: barOptions });
        });

    </script>

</body>

</html>
