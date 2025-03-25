<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="blogs.aspx.cs" Inherits="fpWebApp.blogs" %>

<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
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

    <title>Fitness People | Blogs</title>

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
            var element1 = document.querySelector("#blog");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#webpage");
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
                    <h4 class="modal-title">Guía para modificar el blog de la página web</h4>
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
                    <h2><i class="fab fa-blogger text-success m-r-sm"></i>Blogs</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Página Web</li>
                        <li class="active"><strong>Blogs</strong></li>
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


                            <div class="ibox float-e-margins" runat="server" id="divContenido">
                                <div class="ibox-title">
                                    <h5>Lista de artículos</h5>
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
                                            <label class="control-label">&nbsp;</label>
                                            <a class="btn btn-success pull-right dim m-l-md" href="nuevoarticulo" title="Agregar artículo" runat="server" id="btnAgregar" visible="false"><i class="fa fa-square-plus"></i></a>
                                            <a class="btn btn-info pull-right dim m-l-md" target="_blank" runat="server" id="btnExportar" href="imprimirarticulos" visible="false" title="Exportar"><i class="fa fa-print"></i></a>
                                            <a data-trigger="footable_expand_all" class="toggle btn btn-primary pull-right dim" href="#collapse" title="Expandir todo"><i class="fa fa-square-caret-down"></i></a>
                                            <a data-trigger="footable_collapse_all" class="toggle btn btn-primary pull-right dim" style="display: none" href="#collapse" title="Contraer todo"><i class="fa fa-square-caret-up"></i></a>
                                        </div>
                                    </div>


                                    <table class="footable table toggle-arrow-small" data-page-size="10" data-filter="#filter" data-filter-minimum="3">
                                        <thead>
                                            <tr>
                                                <th data-sort-ignore="true">Título del artículo</th>
                                                <th data-sort-initial="true">Categoría</th>
                                                <th data-sort-ignore="true" data-hide="all">Artículo</th>
                                                <th data-sort-ignore="true" data-hide="phone,tablet">Etiquetas</th>
                                                <th data-sort-ignore="true" data-hide="phone,tablet">Usuario</th>
                                                <th data-sort-ignore="true" data-toggle="false" class="text-right"
                                                    style="display: flex; flex-wrap: nowrap; width: 100%;">Acciones</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rpArticulosBlog" runat="server">
                                                <ItemTemplate>
                                                    <tr class="feed-element">
                                                        <td><%# Eval("DocumentoEmpleado") %></td>
                                                        <td>
                                                            <img class="img-sm" src="img/empleados/<%# Eval("FotoEmpleado") %>" />
                                                            <%# Eval("NombreEmpleado") %></td>
                                                        <td><i class="fab fa-whatsapp m-r-xs font-bold"></i><a href="https://wa.me/57<%# Eval("TelefonoEmpleado") %>" target="_blank"><%# Eval("TelefonoEmpleado") %></a></td>
                                                        <td><i class="fa fa-envelope m-r-xs font-bold"></i><%# Eval("EmailEmpleado") %></td>
                                                        <td><i class="fa fa-user-tie m-r-xs font-bold"></i><%# Eval("CargoEmpleado") %></td>
                                                        <td><%# Eval("icono") %><%# Eval("FechaNacEmpleado", "{0:dd MMM}") %></td>
                                                        <td><span class="badge badge-<%# Eval("label") %>"><%# Eval("Estado") %></span></td>
                                                        <td class="table-bordered">
                                                            <table class="table table-bordered">
                                                                <thead>
                                                                    <tr>
                                                                        <th width="50%" colspan="2"><i class="fa fa-map-location-dot m-r-xs"></i>Dirección</th>
                                                                        <th width="50%"><i class="fa fa-city m-r-xs"></i>Ciudad</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td colspan="2"><%# Eval("DireccionEmpleado") %></td>
                                                                        <td><%# Eval("CiudadEmpleado") %></td>
                                                                    </tr>
                                                                </tbody>
                                                                <thead>
                                                                    <tr>
                                                                        <th width="27%"><i class="fa fa-hashtag m-r-xs"></i>Nro Contrato</th>
                                                                        <th width="27%"><i class="fa fa-file-lines m-r-xs"></i>Tipo Contrato</th>
                                                                        <th width="46%"><i class="fa fa-clock m-r-xs"></i>Duración</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <tr>
                                                                        <td><%# Eval("NroContrato") %></td>
                                                                        <td><%# Eval("TipoContrato") %></td>
                                                                        <td style="vertical-align: central;">
                                                                            <span class="pie"><%# Eval("diastrabajados") %>/<%# Eval("diastotales") %></span>
                                                                            <%# Eval("FechaInicio", "{0:dd MMM yyyy}") %> - <%# Eval("FechaFinal", "{0:dd MMM yyyy}") %>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td style="display: flex; flex-wrap: nowrap; width: 100%;">
                                                            <button runat="server" id="btnEditar" class="btn btn-outline btn-primary pull-left m-r-xs"
                                                                style="padding: 3px 6px 3px 6px; margin-bottom: 0px;" visible="false">
                                                                <i class="fa fa-edit"></i>
                                                            </button>
                                                            <button runat="server" id="btnEliminar" class="btn btn-outline btn-danger pull-right"
                                                                style="padding: 3px 6px 3px 6px; margin-bottom: 0px;" visible="false">
                                                                <i class="fa fa-trash"></i>
                                                            </button>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="8">
                                                    <ul class="pagination"></ul>
                                                </td>
                                            </tr>
                                        </tfoot>
                                    </table>

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

    <!-- Page-Level Scripts -->

</body>

</html>
