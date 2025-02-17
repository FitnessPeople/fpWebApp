﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sedes.aspx.cs" Inherits="fpWebApp.sedes" ValidateRequest="false" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/indicadores01.ascx" TagPrefix="uc1" TagName="indicadores01" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Sedes</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <%--<link href="css/plugins/footable/footable.core.css" rel="stylesheet" />--%>
    <link href="css/plugins/footable/footable.bootstrap.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <style type="text/css" media="print">
        body { visibility: hidden; display: none }
    </style>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#sedes");
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
                    <i class="fa fa-person-chalkboard modal-icon"></i>
                    <h4 class="modal-title">Guía para administrar sedes</h4>
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

        <uc1:navbar runat="server" ID="navbar1" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header1" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa fa-school-flag text-success m-r-sm"></i>Sedes</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Sistema</li>
                        <li class="active"><strong>Sedes</strong></li>
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

                    <form role="form" id="form" runat="server">
                        <div class="row" id="divContenido" runat="server">
                            <div class="col-lg-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>
                                            <asp:Literal ID="ltTitulo" runat="server"></asp:Literal></h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <label>Nombre de la sede:</label>
                                                    <asp:TextBox ID="txbSede" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Dirección:</label>
                                                    <asp:TextBox ID="txbDireccion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Ciudad:</label>
                                                            <asp:DropDownList ID="ddlCiudadSede" runat="server" CssClass="form-control input-sm" 
                                                                DataTextField="NombreCiudadSede" DataValueField="idCiudadSede" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Teléfono:</label>
                                                            <asp:TextBox ID="txbTelefono" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label>Horario:</label>
                                                    <textarea id="summernote" name="editordata" class="form-control input-sm" runat="server"></textarea>
                                                </div>
                                                <div class="form-group">
                                               <a href="sedes" class="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md">Cancelar</a>
                                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" 
                                                        CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" 
                                                        OnClick="btnAgregar_Click" />
                                                </div>
                                                <br />
                                                <br />
                                                <div class="form-group">
                                                    <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-8">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Lista de Sedes</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">

                                        <div class="row" style="font-size: 12px;" runat="server" id="divBotonesLista">
                                            <div class="col-lg-4 form-horizontal">
                                                <div class="form-group">
                                                    <div class="form-group" id="filter-form-container" style="margin-left: 28px;"></div>                                                </div>
                                            </div>
                                            <div class="col-lg-4 form-horizontal" style="text-align: center;">
                                                <label class="control-label">Mostrar </label>

                                                <a href="#" class="data-page-size" data-page-size="10">10</a> | 
                                                <a href="#" class="data-page-size" data-page-size="20">20</a>

                                                <label class="control-label">registros</label>
                                            </div>
                                            <div class="col-lg-4 form-horizontal">
                                                <a class="btn btn-info pull-right dim m-l-md" style="font-size: 12px; padding: 6px 8px;" 
                                                    target="_blank" runat="server" id="btnImprimir" href="imprimirprofesiones" 
                                                    title="Imprimir"><i class="fa fa-print"></i> IMPRIMIR</a>
                                                <a data-trigger="footable_expand_all" style="font-size: 12px; padding: 6px 8px;" 
                                                    class="toggle btn btn-primary pull-right dim" href="#collapse" 
                                                    title="Expandir todo"><i class="fa fa-square-caret-down"></i> EXPANDIR</a>
                                                <a data-trigger="footable_collapse_all" class="toggle btn btn-primary pull-right dim" 
                                                    style="display: none; font-size: 12px; padding: 6px 8px;" href="#collapse" 
                                                    title="Contraer todo"><i class="fa fa-square-caret-up"></i> CONTRAER</a>
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
                                                    <th data-sort-initial="true">Sede</th>
                                                    <th data-sort-ignore="true">Dirección</th>
                                                    <%--<th data-hide="phone,tablet">Ciudad</th>
                                                    <th data-sort-ignore="true" data-hide="phone,tablet">Teléfono</th>
                                                    <th data-hide="phone,tablet">Horario</th>--%>
                                                    <th data-hide="all"></th>
                                                    <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpSedes" runat="server" OnItemDataBound="rpSedes_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr class="feed-element">
                                                            <td><%# Eval("NombreSede") %></td>
                                                            <td><%# Eval("DireccionSede") %></td>
                                                            <%--<td><%# Eval("CiudadSede") %></td>
                                                            <td><%# Eval("TelefonoSede") %></td>
                                                            <td><%# Eval("HorarioSede") %></td>--%>
                                                            <td class="table-bordered">
                                                                <table class="table table-bordered">
                                                                    <thead>
                                                                    <tr> 
                                                                        <th width="25%"><i class="fa fa-city m-r-xs"></i>Ciudad</th>
                                                                        <th width="25%"><i class="fa fa-mobile m-r-xs"></i>Teléfono</th>
                                                                        <th width="50%" class="text-nowrap"><i class="fa fa-clock m-r-xs"></i>Horario</th>
                                                                    </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                    <tr>
                                                                        <td><%# Eval("NombreCiudadSede") %></td>
                                                                        <td><%# Eval("TelefonoSede") %></td>
                                                                        <td><%# Eval("HorarioSede") %></td>
                                                                    </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                            <td style="display: flex; flex-wrap: nowrap; width: 100%;">
                                                                <button runat="server" id="btnEditar" class="btn btn-outline btn-primary pull-left m-r-xs" 
                                                                    style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false"><i class="fa fa-edit"></i></button>
                                                                <button runat="server" id="btnEliminar" class="btn btn-outline btn-danger pull-right" 
                                                                    style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false"><i class="fa fa-trash"></i></button>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>

                    <%--Fin Contenido!!!!--%>
                </div>
            </div>

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

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();
    </script>

</body>

</html>