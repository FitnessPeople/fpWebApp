﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usuarios.aspx.cs" Inherits="fpWebApp.usuarios" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>
<%@ Register Src="~/controles/indicadores04.ascx" TagPrefix="uc1" TagName="indicadores04" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Usuarios</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/dataTables/datatables.min.css" rel="stylesheet" />
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

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
            var element1 = document.querySelector("#usuarios");
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
                    <h4 class="modal-title">Guía para administrar usuarios</h4>
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
                    <h2><i class="fa fa-circle-user text-success m-r-sm"></i>Usuarios</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Sistema</li>
                        <li class="active"><strong>Usuarios</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">

                    <uc1:indicadores04 runat="server" ID="indicadores04" />

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

                    <div class="row" id="divContenido" runat="server">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5>Lista de usuarios</h5>
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
                                                <label class="col-lg-4 control-label" style="text-align: left;">Buscador:</label>
                                                <div class="col-lg-8">
                                                    <input type="text" placeholder="Buscar..." class="form-control input-sm m-b-xs" id="filter">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 form-horizontal" style="text-align: center;">
                                            <label class="control-label">Mostrar </label>

                                            <a href="#" class="data-page-size" data-page-size="10">10</a> | 
                                                    <a href="#" class="data-page-size" data-page-size="20">20</a> | 
                                                    <a href="#" class="data-page-size" data-page-size="50">50</a> | 
                                                    <a href="#" class="data-page-size" data-page-size="100">100</a>

                                            <label class="control-label">registros</label>
                                        </div>

                                        <div class="col-lg-4 form-horizontal">
                                            <a class="btn btn-success btn-sm m-l-lg pull-right dim" href="nuevousuario.aspx" 
                                                title="Agregar usuario" runat="server" id="btnAgregar" style="font-size: 12px; padding: 6px 8px;" 
                                                visible="false"><i class="fa fa-square-plus"></i> NUEVO</a>
                                            <a class="btn btn-info btn-sm pull-right dim" target="_blank" style="font-size: 12px; padding: 6px 8px;" 
                                                runat="server" id="btnImprimir" href="imprimirusuarios" visible="false"><i class="fa fa-print"></i> IMPRIMIR</a>
                                        </div>
                                    </div>

                                    <table class="footable table toggle-arrow-small list-group-item-text" data-page-size="10" data-filter="#filter" data-filter-minimum="3">
                                        <thead>
                                            <tr>
                                                <th data-sort-initial="true">Nombre</th>
                                                <th data-sort-ignore="true" data-hide="phone,tablet">Empleado</th>
                                                <th data-hide="phone,tablet">Correo</th>
                                                <th data-sort-ignore="true" data-hide="phone,tablet">Clave</th>
                                                <%--<th data-sort-ignore="true">Cargo</th>--%>
                                                <th data-hide="phone,tablet">Perfil</th>
                                                <th class="text-nowrap">Estado</th>
                                                <th data-sort-ignore="true" data-toggle="false" class="text-right"
                                                    style="display: flex; flex-wrap: nowrap; width: 100%;">Acciones</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rpUsuarios" runat="server" OnItemDataBound="rpUsuarios_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr class="feed-element">
                                                        <td><%# Eval("NombreUsuario") %></td>
                                                        <td><span class='badge badge-<%# Eval("label") %>'><%# Eval("Empleado") %></span></td>
                                                        <td><i class="fa fa-circle-user m-r-xs font-bold"></i><%# Eval("EmailUsuario") %></td>
                                                        <td><i class="fa fa-unlock-keyhole m-r-xs font-bold"></i><%# Eval("ClaveUsuario") %></td>
                                                        <%--<td><%# Eval("CargoUsuario") %></td>--%>
                                                        <td><i class="fa fa-user-shield m-r-xs font-bold"></i><%# Eval("Perfil") %></td>
                                                        <td><a runat="server" id="cambiaestado" visible="false"><span class='badge badge-<%# Eval("estatus") %>'><%# Eval("EstadoUsuario") %></span></a></td>
                                                        <td style="display: flex; flex-wrap: nowrap; width: 100%;">
                                                            <button runat="server" id="btnEditar" class="btn btn-outline btn-primary pull-left m-r-xs"
                                                                style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false">
                                                                <i class="fa fa-edit"></i>
                                                            </button>
                                                            <button runat="server" id="btnEliminar" class="btn btn-outline btn-danger pull-right"
                                                                style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false">
                                                                <i class="fa fa-trash"></i>
                                                            </button>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="7">
                                                    <ul class="pagination"></ul>
                                                </td>
                                            </tr>
                                        </tfoot>
                                    </table>

                                </div>
                            </div>
                        </div>
                    </div>
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
    <script src="js/plugins/footable/footable.all.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();

        $('.data-page-size').on('click', function (e) {
            e.preventDefault();
            var newSize = $(this).data('pageSize');
            $('.footable').data('page-size', newSize);
            $('.footable').trigger('footable_initialized');
        });

        $('.toggle').click(function (e) {
            e.preventDefault();
            $('.toggle').toggle();
            $('.footable').trigger($(this).data('trigger')).trigger('footable_redraw');
        });

        $('.chosen-select').chosen({ width: "100%" });

    </script>

</body>

</html>
