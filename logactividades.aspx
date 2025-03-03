<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="logactividades.aspx.cs" Inherits="fpWebApp.logactividades" %>

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

    <title>Fitness People | Flujo de actividades</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#logactividades");
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
                    <h4 class="modal-title">Guía para consultar el flujo de actividades</h4>
                    <small class="font-bold">¡Bienvenido! A continuación, te ofrecemos una guía sencilla para ayudarte a completar el formulario de manera correcta y eficiente. Sigue estos pasos para asegurarte de que toda la información se registre de forma adecuada.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>1. Lee las Instrucciones</b><br />
                        Antes de comenzar, es importante que leas todas las instrucciones del formulario. Esto te ayudará a entender qué información se requiere y cómo debe ser presentada.
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
                    <h2><i class="fa fa-location-crosshairs text-success m-r-sm"></i>Flujo de actividades</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Sistema</li>
                        <li class="active"><strong>Flujo de actividades</strong></li>
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

                    <div class="row" id="divContenido" runat="server">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5>Flujo de actividades</h5>
                                    <div class="ibox-tools">
                                        <a class="collapse-link">
                                            <i class="fa fa-chevron-up"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="ibox-content">
                                    <div class="row">
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
                                            <a class="btn btn-info pull-right dim m-l-md"
                                                style="font-size: 12px;"
                                                target="_blank"
                                                runat="server"
                                                id="btnExportar"
                                                href="imprimirafiliados"
                                                visible="false"
                                                title="Exportar">
                                                <i class="fa fa-print"></i> IMPRIMIR
                                            </a>
                                            <a data-trigger="footable_expand_all"
                                                style="font-size: 12px;"
                                                class="toggle btn btn-primary pull-right dim"
                                                href="#collapse"
                                                title="Expandir todo">
                                                <i class="fa fa-square-caret-down"></i> EXPANDIR
                                            </a>
                                            <a data-trigger="footable_collapse_all"
                                                class="toggle btn btn-primary pull-right dim"
                                                style="display: none; font-size: 12px;"
                                                href="#collapse"
                                                title="Contraer todo">
                                                <i class="fa fa-square-caret-up"></i> CONTRAER
                                            </a>
                                        </div>
                                    </div>

                                    <table class="footable table toggle-arrow-small list-group-item-text" data-page-size="10" data-filter="#filter" data-filter-minimum="3">
                                        <thead>
                                            <tr>
                                                <th>Acción</th>
                                                <th data-hide="phone,tablet">Usuario</th>
                                                <th data-hide="phone,tablet">Fecha-Hora</th>
                                                <th data-hide="phone,tablet">Tabla</th>
                                                <th data-hide="phone,tablet">Actividad</th>
                                                <th data-hide="all"></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rpLogs" runat="server">
                                                <ItemTemplate>
                                                    <tr class="feed-element">
                                                        <td><span class='label label-<%# Eval("label") %>'><%# Eval("Accion") %></span></td>
                                                        <td><%# Eval("NombreUsuario") %></td>
                                                        <td><%# Eval("FechaHora", "{0:yyyy/MM/dd HH:mm}") %></td>
                                                        <td><%# Eval("Tabla") %></td>
                                                        <td><%# Eval("DescripcionLog") %></td>
                                                        <td class="table-bordered">
                                                            <table class="table table-bordered">
                                                                <thead>
                                                                    <tr>
                                                                        <th width="33%"><i class="fa fa-file-shield m-r-xs"></i>Datos Anteriores</th>
                                                                        <th width="33%"><i class="fa fa-file-pen m-r-xs"></i>Datos Nuevos</th>
                                                                        <th width="34%"><i class="fa fa-file-circle-check m-r-xs"></i>Diferencias</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr class="diff-wrapper">
                                                                        <td>
                                                                            <div class="original">
                                                                                <pre><%# Eval("DatosAnteriores") %></pre>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div class="changed">
                                                                                <pre><%# Eval("DatosNuevos") %></pre>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <div class="diff1"></div>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
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

                    <%--Fin Contenido!!!!--%>
                </div>
            </div>

            <uc1:footer runat="server" ID="footer" />

        </div>
        <uc1:rightsidebar runat="server" ID="rightsidebar" />
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

    <!-- Diff march patch -->
    <script src="js/plugins/diff_match_patch/javascript/diff_match_patch.js"></script>

    <!-- jQuery pretty text diff -->
    <script src="js/plugins/preetyTextDiff/jquery.pretty-text-diff.min.js"></script>

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

        $(document).ready(function () {

            // Initial diff1
            $(".diff-wrapper").prettyTextDiff({
                diffContainer: ".diff1"
            });

        });
    </script>

</body>

</html>
