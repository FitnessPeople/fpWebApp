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
                                                <th data-sortable="false">Documento</th>
                                                <th data-breakpoints="xs"></th>
                                                <th>Nombre</th>
                                                <th data-breakpoints="xs">Teléfono corporativo</th>
                                                <th data-breakpoints="xs sm">Teléfono personal</th>
                                                <%--<th data-breakpoints="xs sm md">Correo</th>--%>
                                                <th data-breakpoints="xs sm">Cargo</th>
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
                                                        <td><%# Eval("DocumentoEmpleado") %></td>
                                                        <td class="client-avatar">
                                                            <img alt="image" src="img/empleados/<%# Eval("FotoEmpleado") %>"></td>
                                                        <td><%# Eval("NombreEmpleado") %></td>
                                                        <td><i class="fab fa-whatsapp m-r-xs font-bold"></i><a href="https://wa.me/57<%# Eval("TelefonoCorporativo") %>" target="_blank"><%# Eval("TelefonoCorporativo") %></a></td>
                                                        <td><i class="fab fa-whatsapp m-r-xs font-bold"></i><a href="https://wa.me/57<%# Eval("TelefonoEmpleado") %>" target="_blank"><%# Eval("TelefonoEmpleado") %></a></td>
                                                        <%--<td><i class="fa fa-envelope m-r-xs font-bold"></i><a href="mailto:<%# Eval("EmailCorporativo") %>" title="Enviar correo"><%# Eval("EmailCorporativo") %></a></td>--%>
                                                        <td><a href="cargos" title="Ir a Cargos"><i class="fa fa-user-nurse m-r-xs font-bold"></i><%# Eval("Cargo") %></a></td>
                                                        <td><%# Eval("NombreSede") %></td>
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
                                                                style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false"><i class="fa fa-trash"></i></a>
                                                            <a runat="server" id="btnEditar" href="#" class="btn btn-outline btn-primary pull-right m-r-xs"
                                                                style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false"><i class="fa fa-edit"></i></a>
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
                                        <div id="contact-1" class="tab-pane active">
                                            <div class="row m-b-lg">
                                                <div class="col-lg-4 text-center">
                                                    <h2>Nicki Smith</h2>

                                                    <div class="m-b-sm">
                                                        <img alt="image" class="img-circle" src="img/a2.jpg"
                                                            style="width: 62px">
                                                    </div>
                                                </div>
                                                <div class="col-lg-8">
                                                    <strong>About me
                                            </strong>

                                                    <p>
                                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod
                                                tempor incididunt ut labore et dolore magna aliqua.
                                           
                                                    </p>
                                                    <button type="button" class="btn btn-primary btn-sm btn-block">
                                                        <i
                                                            class="fa fa-envelope"></i>Send Message
                                           
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="client-detail">
                                                <div class="full-height-scroll">

                                                    <strong>Last activity</strong>

                                                    <ul class="list-group clear-list">
                                                        <li class="list-group-item fist-item">
                                                            <span class="pull-right">09:00 pm </span>
                                                            Please contact me
                                            </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">10:16 am </span>
                                                            Sign a contract
                                            </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">08:22 pm </span>
                                                            Open new shop
                                            </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">11:06 pm </span>
                                                            Call back to Sylvia
                                            </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">12:00 am </span>
                                                            Write a letter to Sandra
                                            </li>
                                                    </ul>
                                                    <strong>Notes</strong>
                                                    <p>
                                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod
                                            tempor incididunt ut labore et dolore magna aliqua.
                                       
                                                    </p>
                                                    <hr />
                                                    <strong>Timeline activity</strong>
                                                    <div id="vertical-timeline" class="vertical-container dark-timeline">
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                   
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                   
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-bolt"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    There are many variations of passages of Lorem Ipsum available.
                                                   
                                                                </p>
                                                                <span class="vertical-date small text-muted">06:10 pm - 11.03.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon navy-bg">
                                                                <i class="fa fa-warning"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    The generated Lorem Ipsum is therefore.
                                                   
                                                                </p>
                                                                <span class="vertical-date small text-muted">02:50 pm - 03.10.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                   
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                   
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="contact-2" class="tab-pane">
                                            <div class="row m-b-lg">
                                                <div class="col-lg-4 text-center">
                                                    <h2>Edan Randall</h2>

                                                    <div class="m-b-sm">
                                                        <img alt="image" class="img-circle" src="img/a3.jpg"
                                                            style="width: 62px">
                                                    </div>
                                                </div>
                                                <div class="col-lg-8">
                                                    <strong>About me
                                            </strong>

                                                    <p>
                                                        Many desktop publishing packages and web page editors now use Lorem Ipsum as their default tempor incididunt model text.
                                           
                                                    </p>
                                                    <button type="button" class="btn btn-primary btn-sm btn-block">
                                                        <i
                                                            class="fa fa-envelope"></i>Send Message
                                           
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="client-detail">
                                                <div class="full-height-scroll">

                                                    <strong>Last activity</strong>

                                                    <ul class="list-group clear-list">
                                                        <li class="list-group-item fist-item">
                                                            <span class="pull-right">09:00 pm </span>
                                                            Lorem Ipsum available
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">10:16 am </span>
                                                            Latin words, combined
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">08:22 pm </span>
                                                            Open new shop
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">11:06 pm </span>
                                                            The generated Lorem Ipsum
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">12:00 am </span>
                                                            Content here, content here
                                                </li>
                                                    </ul>
                                                    <strong>Notes</strong>
                                                    <p>
                                                        There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words.
                                           
                                                    </p>
                                                    <hr />
                                                    <strong>Timeline activity</strong>
                                                    <div id="vertical-timeline" class="vertical-container dark-timeline">
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-bolt"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    There are many variations of passages of Lorem Ipsum available.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">06:10 pm - 11.03.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon navy-bg">
                                                                <i class="fa fa-warning"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    The generated Lorem Ipsum is therefore.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">02:50 pm - 03.10.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="contact-3" class="tab-pane">
                                            <div class="row m-b-lg">
                                                <div class="col-lg-4 text-center">
                                                    <h2>Jasper Carson</h2>

                                                    <div class="m-b-sm">
                                                        <img alt="image" class="img-circle" src="img/a4.jpg"
                                                            style="width: 62px">
                                                    </div>
                                                </div>
                                                <div class="col-lg-8">
                                                    <strong>About me
                                            </strong>

                                                    <p>
                                                        Latin professor at Hampden-Sydney College in Virginia, looked  embarrassing hidden in the middle.
                                           
                                                    </p>
                                                    <button type="button" class="btn btn-primary btn-sm btn-block">
                                                        <i
                                                            class="fa fa-envelope"></i>Send Message
                                           
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="client-detail">
                                                <div class="full-height-scroll">

                                                    <strong>Last activity</strong>

                                                    <ul class="list-group clear-list">
                                                        <li class="list-group-item fist-item">
                                                            <span class="pull-right">09:00 pm </span>
                                                            Aldus PageMaker including
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">10:16 am </span>
                                                            Finibus Bonorum et Malorum
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">08:22 pm </span>
                                                            Write a letter to Sandra
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">11:06 pm </span>
                                                            Standard chunk of Lorem
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">12:00 am </span>
                                                            Open new shop
                                                </li>
                                                    </ul>
                                                    <strong>Notes</strong>
                                                    <p>
                                                        Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source.
                                           
                                                    </p>
                                                    <hr />
                                                    <strong>Timeline activity</strong>
                                                    <div id="vertical-timeline" class="vertical-container dark-timeline">
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-bolt"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    There are many variations of passages of Lorem Ipsum available.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">06:10 pm - 11.03.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon navy-bg">
                                                                <i class="fa fa-warning"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    The generated Lorem Ipsum is therefore.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">02:50 pm - 03.10.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="contact-4" class="tab-pane">
                                            <div class="row m-b-lg">
                                                <div class="col-lg-4 text-center">
                                                    <h2>Reuben Pacheco</h2>

                                                    <div class="m-b-sm">
                                                        <img alt="image" class="img-circle" src="img/a5.jpg"
                                                            style="width: 62px">
                                                    </div>
                                                </div>
                                                <div class="col-lg-8">
                                                    <strong>About me
                                            </strong>

                                                    <p>
                                                        Finibus Bonorum et Malorum" (The Extremes of Good and Evil) by Cicero,written in 45 BC. This book is a treatise on.
                                           
                                                    </p>
                                                    <button type="button" class="btn btn-primary btn-sm btn-block">
                                                        <i
                                                            class="fa fa-envelope"></i>Send Message
                                           
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="client-detail">
                                                <div class="full-height-scroll">

                                                    <strong>Last activity</strong>

                                                    <ul class="list-group clear-list">
                                                        <li class="list-group-item fist-item">
                                                            <span class="pull-right">09:00 pm </span>
                                                            The point of using
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">10:16 am </span>
                                                            Lorem Ipsum is that it has
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">08:22 pm </span>
                                                            Text, and a search for 'lorem ipsum'
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">11:06 pm </span>
                                                            Passages of Lorem Ipsum
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">12:00 am </span>
                                                            If you are going
                                                </li>
                                                    </ul>
                                                    <strong>Notes</strong>
                                                    <p>
                                                        Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.
                                           
                                                    </p>
                                                    <hr />
                                                    <strong>Timeline activity</strong>
                                                    <div id="vertical-timeline" class="vertical-container dark-timeline">
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-bolt"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    There are many variations of passages of Lorem Ipsum available.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">06:10 pm - 11.03.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon navy-bg">
                                                                <i class="fa fa-warning"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    The generated Lorem Ipsum is therefore.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">02:50 pm - 03.10.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="company-1" class="tab-pane">
                                            <div class="m-b-lg">
                                                <h2>Tellus Institute</h2>

                                                <p>
                                                    Finibus Bonorum et Malorum" (The Extremes of Good and Evil) by Cicero,written in 45 BC. This book is a treatise on.
                                           
                                                </p>
                                                <div>
                                                    <small>Active project completion with: 48%</small>
                                                    <div class="progress progress-mini">
                                                        <div style="width: 48%;" class="progress-bar"></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="client-detail">
                                                <div class="full-height-scroll">

                                                    <strong>Last activity</strong>

                                                    <ul class="list-group clear-list">
                                                        <li class="list-group-item fist-item">
                                                            <span class="pull-right"><span class="label label-primary">NEW</span> </span>
                                                            The point of using
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right"><span class="label label-warning">WAITING</span></span>
                                                            Lorem Ipsum is that it has
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right"><span class="label label-danger">BLOCKED</span> </span>
                                                            If you are going
                                                </li>
                                                    </ul>
                                                    <strong>Notes</strong>
                                                    <p>
                                                        Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.
                                           
                                                    </p>
                                                    <hr />
                                                    <strong>Timeline activity</strong>
                                                    <div id="vertical-timeline" class="vertical-container dark-timeline">
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-bolt"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    There are many variations of passages of Lorem Ipsum available.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">06:10 pm - 11.03.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon navy-bg">
                                                                <i class="fa fa-warning"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    The generated Lorem Ipsum is therefore.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">02:50 pm - 03.10.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="company-2" class="tab-pane">
                                            <div class="m-b-lg">
                                                <h2>Penatibus Consulting</h2>

                                                <p>
                                                    There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some.
                                       
                                                </p>
                                                <div>
                                                    <small>Active project completion with: 22%</small>
                                                    <div class="progress progress-mini">
                                                        <div style="width: 22%;" class="progress-bar"></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="client-detail">
                                                <div class="full-height-scroll">

                                                    <strong>Last activity</strong>

                                                    <ul class="list-group clear-list">
                                                        <li class="list-group-item fist-item">
                                                            <span class="pull-right"><span class="label label-warning">WAITING</span> </span>
                                                            Aldus PageMaker
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right"><span class="label label-primary">NEW</span> </span>
                                                            Lorem Ipsum, you need to be sure
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right"><span class="label label-danger">BLOCKED</span> </span>
                                                            The generated Lorem Ipsum
                                                </li>
                                                    </ul>
                                                    <strong>Notes</strong>
                                                    <p>
                                                        Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.
                                           
                                                    </p>
                                                    <hr />
                                                    <strong>Timeline activity</strong>
                                                    <div id="vertical-timeline" class="vertical-container dark-timeline">
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-bolt"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    There are many variations of passages of Lorem Ipsum available.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">06:10 pm - 11.03.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon navy-bg">
                                                                <i class="fa fa-warning"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    The generated Lorem Ipsum is therefore.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">02:50 pm - 03.10.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="company-3" class="tab-pane">
                                            <div class="m-b-lg">
                                                <h2>Ultrices Incorporated</h2>

                                                <p>
                                                    Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text.
                                       
                                                </p>
                                                <div>
                                                    <small>Active project completion with: 72%</small>
                                                    <div class="progress progress-mini">
                                                        <div style="width: 72%;" class="progress-bar"></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="client-detail">
                                                <div class="full-height-scroll">

                                                    <strong>Last activity</strong>

                                                    <ul class="list-group clear-list">
                                                        <li class="list-group-item fist-item">
                                                            <span class="pull-right"><span class="label label-danger">BLOCKED</span> </span>
                                                            Hidden in the middle of text
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right"><span class="label label-primary">NEW</span> </span>
                                                            Non-characteristic words etc.
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right"><span class="label label-warning">WAITING</span> </span>
                                                            Bonorum et Malorum
                                                </li>
                                                    </ul>
                                                    <strong>Notes</strong>
                                                    <p>
                                                        There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour.
                                           
                                                    </p>
                                                    <hr />
                                                    <strong>Timeline activity</strong>
                                                    <div id="vertical-timeline" class="vertical-container dark-timeline">
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-bolt"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    There are many variations of passages of Lorem Ipsum available.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">06:10 pm - 11.03.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon navy-bg">
                                                                <i class="fa fa-warning"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    The generated Lorem Ipsum is therefore.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">02:50 pm - 03.10.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
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

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();
    </script>

</body>

</html>
