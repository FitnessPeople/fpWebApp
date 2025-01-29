<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="empresasafiliadas.aspx.cs" Inherits="fpWebApp.empresasafiliadas" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/indicadores02.ascx" TagPrefix="uc1" TagName="indicadores02" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Empresas convenios</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

    <!-- Morris -->
    <link href="css/plugins/morris/morris-0.4.3.min.css" rel="stylesheet">

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
            var element1 = document.querySelector("#empresasafiliadas");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#afiliados2");
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
                    <h4 class="modal-title">Guía para ver un documento</h4>
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

    <div class="modal inmodal" id="myModal2" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <%--<i class="fa fa-file-pdf modal-icon"></i>--%>
                    <h4 class="modal-title"><span id="titulo"></span></h4>
                </div>
                <div class="modal-body">
                    
                    <div class="text-center m-t-md">
                        <object data="" type="application/pdf" width="450px" height="600px" id="objFile">
                            <embed src="" id="objEmbed">
                                <p>Este navegador no soporta archivos PDFs. Descarge el archivo para verlo: <a href="" id="objHref">Descargar PDF</a>.</p>
                            </embed>
                        </object> 
                    </div>
                        
                    
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
                    <h2><i class="fa fa-building text-success m-r-sm"></i>Empresas convenios</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Afiliados</li>
                        <li class="active"><strong>Empresas convenios</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>

            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">
                    <%--Inicio Contenido!!!!--%>

                    <uc1:indicadores02 runat="server" ID="indicadores02" />

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
                            <h5>Lista de empresas con convenio</h5>
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
                                    <label class="control-label">&nbsp;</label>
                                    <a class="btn btn-success pull-right dim m-l-md" style="font-size: 12px;" href="nuevaempresaafiliada" title="Agregar empresa" runat="server" id="btnAgregar" visible="false"><i class="fa fa-square-plus"></i> NUEVO</a>
                                    <a class="btn btn-info pull-right dim m-l-md" style="font-size: 12px;" target="_blank" runat="server" id="btnExportar" href="imprimirempresasafiliadas" visible="false" title="Exportar"><i class="fa fa-print"></i> IMPRIMIR</a>
                                    <a data-trigger="footable_expand_all" style="font-size: 12px;" class="toggle btn btn-primary pull-right dim" href="#collapse" title="Expandir todo"><i class="fa fa-square-caret-down"></i> EXPANDIR</a>
                                    <a data-trigger="footable_collapse_all" class="toggle btn btn-primary pull-right dim" style="display: none; font-size: 12px;" href="#collapse" title="Contraer todo"><i class="fa fa-square-caret-up"></i> CONTRAER</a>
                                </div>
                            </div>


                            <table class="footable table toggle-arrow-small list-group-item-text" data-page-size="10" data-filter="#filter" data-filter-minimum="3">
                                <thead>
                                    <tr>
                                        <th data-sort-ignore="true">Documento</th>
                                        <th data-sort-initial="true">Nombre Comercial</th>
                                        <th data-sort-ignore="true" data-hide="phone,tablet">Celular</th>
                                        <th data-sort-ignore="true" data-hide="phone,tablet">Correo</th>
                                        <th data-sort-ignore="true" data-hide="phone,tablet">Fecha Convenio</th>
                                        <th data-toggle="false">Estado</th>
                                        <th data-hide="all"></th>
                                        <th data-sort-ignore="true" data-toggle="false" class="text-right"
                                            style="display: flex; flex-wrap: nowrap; width: 100%;">Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rpEmpresasAfiliadas" runat="server" OnItemDataBound="rpEmpresasAfiliadas_ItemDataBound">
                                        <ItemTemplate>
                                            <tr class="feed-element">
                                                <td><%# Eval("DocumentoEmpresa") %></td>
                                                <td><%# Eval("NombreComercial") %></td>
                                                <td><i class="fab fa-whatsapp m-r-xs font-bold"></i><a href="https://wa.me/57<%# Eval("CelularEmpresa") %>" target="_blank"><%# Eval("CelularEmpresa") %></a></td>
                                                <td><i class="fa fa-envelope m-r-xs font-bold"></i><%# Eval("CorreoEmpresa") %></td>
                                                <td><%# Eval("FechaConvenio", "{0:dd MMM yyyy}") %></td>
                                                <td><span class="badge badge-<%# Eval("badge") %>"><%# Eval("EstadoEmpresa") %></span></td>
                                                <td class="table-bordered">
                                                    <table class="table table-bordered">
                                                        <thead>
                                                        <tr>
                                                            <th width="50%" colspan="2"><i class="fa fa-map-location-dot m-r-xs"></i>Dirección</th>
                                                            <th width="50%" colspan="2"><i class="fa fa-city m-r-xs"></i>Ciudad</th>
                                                        </tr>
                                                        </thead>
                                                        <tbody>
                                                        <tr>
                                                            <td colspan="2"><%# Eval("DireccionEmpresa") %></td>
                                                            <td><%# Eval("NombreCiudad") %></td>
                                                        </tr>
                                                        </tbody>
                                                        <thead>
                                                            <tr>
                                                                <th width="25%"><i class="fa fa-users m-r-xs"></i>Nro de Empleados</th>
                                                                <th width="25%"><i class="fa fa-briefcase m-r-xs"></i>Tipo de Negociación</th>
                                                                <th width="25%"><i class="fa fa-comment-dollar m-r-xs"></i>Días de crédito</th>
                                                                <th width="25%"><i class="fa fa-file m-r-xs"></i>Contrato</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td><%# Eval("NroEmpleados") %></td>
                                                                <td><%# Eval("TipoNegociacion") %></td>
                                                                <td><%# Eval("DiasCredito") %></td>
                                                                <td>
                                                                    <%--<a href='pdfviewer?url=./docs/contratos/&file=<%# Eval("Contrato") %>'><%# Eval("Contrato") %></a>--%>
                                                                    <a class="dropdown-toggle" data-toggle="modal" 
                                                                        href="#" data-target="#myModal2" 
                                                                        data-file="<%# Eval("Contrato") %>">
                                                                        <%# Eval("Contrato") %>
                                                                    </a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
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

    <!-- Peity -->
    <script src="js/plugins/peity/jquery.peity.min.js"></script>
    <script src="js/demo/peity-demo.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <script src="js/plugins/pdfjs/pdf.js"></script>

    <!-- jQuery UI -->
    <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>

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

    </script>

    <script id="script">
        //
        // If absolute URL from the remote server is provided, configure the CORS
        // header on that server.
        //

        $(document).on("click", ".dropdown-toggle", function () {
            var url = './docs/contratos/';
            url = url + $(this).data('file');

            document.getElementById('titulo').innerHTML = $(this).data('file');
            document.getElementById('objFile').data = url;
            document.getElementById('objEmbed').src = url;
            document.getElementById('objHref').src = url;
        });
        
    </script>

</body>

</html>