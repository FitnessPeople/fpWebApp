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
            var element1 = document.querySelector("#empresasafiliadas");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#corporativo");
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
                    <i class="fa fa-building modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para visualizar empresas con convenio</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar el listado de forma rápida y sencilla.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Busca y filtra empresas</b><br />
                        Usa el buscador para encontrar empresas específicas. Puedes filtrar por:<br />
                        <i class="fa-solid fa-address-card" style="color: #0D6EFD;"></i><b>Documento (CC, TI, etc.).<br />
                            <i class="fa-solid fa-tag" style="color: #0D6EFD;"></i>Nombre Comercial.<br />
                            <i class="fa-solid fa-phone" style="color: #0D6EFD;"></i>Celular o Correo.<br />
                            <i class="fa-solid fa-calendar-days" style="color: #0D6EFD;"></i>Fecha de Convenio.<br />
                            <i class="fa-solid fa-circle" style="color: #0D6EFD;"></i>Estado (Activo/Inactivo).<br />
                            <i class="fa-solid fa-circle-info" style="color: #0D6EFD;"></i>Info (datos adicionales).</b><br />
                        <i class="fa-solid fa-star" style="color: #FECE32;"></i>Tip: ¡Combina filtros para resultados más exactos!
                    <br />
                        <br />
                        <b>Paso 2: Revisa la tabla de resultados</b><br />
                        La tabla muestra toda la información de las empresas.<br />
                        <i class="fa fa-edit" style="color: #1AB394;"></i><b>Editar:</b> Haz clic para modificar datos.<br />
                        <i class="fa fa-trash" style="color: #DC3545;"></i><b>Eliminar:</b> Elimina el convenio (sistema pedirá confirmación).
                    <br />
                        <br />
                        <b>Paso 3: Acciones adicionales</b><br />
                        Al lado opuesto del buscador encontrarás dos botones útiles:<br />
                        <i class="fa-solid fa-file-export" style="color: #212529;"></i><b>Exportar a Excel:</b>
                        Genera un archivo Excel con los datos visibles en la tabla.<br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i><b>Crear Nueva Empresa:</b>
                        Te lleva a un formulario para registrar un nuevo convenio.
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


    <div class="modal inmodal" id="myModal2" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <i class="fa fa-file modal-icon"></i>
                    <h4 class="modal-title">
                        <span id="titulo"></span>
                    </h4>
                </div>

                <div class="modal-body">
                    <div id="viewer"
                        class="text-center"
                        style="min-height: 600px;">
                    </div>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">
                        Cerrar
                    </button>
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
                            <form runat="server" id="form1">
                                <div class="row" style="font-size: 12px;" runat="server" id="divBotonesLista">
                                    <div class="col-lg-6 form-horizontal">
                                        <div class="form-group">
                                            <div class="form-group" id="filter-form-container" style="margin-left: 28px;"></div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 form-horizontal">
                                        <a class="btn btn-success pull-right dim m-l-md" style="font-size: 12px;"
                                            href="nuevaempresaafiliada" title="Agregar empresa afiliada"
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
                                        <th data-sortable="false" data-breakpoints="xs">Nombre Comercial</th>
                                        <th data-breakpoints="xs sm md">Celular</th>
                                        <th data-breakpoints="xs sm md">Correo</th>
                                        <th data-type="date" data-breakpoints="xs sm md">Fecha Inicio Convenio</th>
                                        <th data-type="date" data-breakpoints="xs sm md">Fecha Fin Convenio</th>
                                        <th class="text-nowrap" data-breakpoints="xs">Estado</th>
                                        <th data-breakpoints="all" data-title="Info"></th>
                                        <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rpEmpresasAfiliadas" runat="server" OnItemDataBound="rpEmpresasAfiliadas_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Eval("DocumentoEmpresa") %></td>
                                                <td><%# Eval("NombreComercial") %></td>
                                                <td><i class="fab fa-whatsapp m-r-xs font-bold"></i><a href="https://wa.me/57<%# Eval("CelularEmpresa") %>" target="_blank"><%# Eval("CelularEmpresa") %></a></td>
                                                <td><i class="fa fa-envelope m-r-xs font-bold"></i><%# Eval("CorreoEmpresa") %></td>
                                                <td><%# Eval("FechaConvenio", "{0:dd MMM yyyy}") %></td>
                                                <td><%# Eval("FechaFinConvenio", "{0:dd MMM yyyy}") %></td>
                                                <td><span class="badge badge-<%# Eval("badge") %>"><%# Eval("EstadoEmpresa") %></span></td>
                                                <td>
                                                    <table class="table table-bordered">
                                                        <tr>
                                                            <th width="50%" colspan="2"><i class="fa fa-map-location-dot m-r-xs"></i>Dirección</th>
                                                            <th width="50%" colspan="2"><i class="fa fa-city m-r-xs"></i>Ciudad</th>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2"><%# Eval("DireccionEmpresa") %></td>
                                                            <td><%# Eval("NombreCiudad") %></td>
                                                        </tr>
                                                        <tr>
                                                            <th width="25%"><i class="fa fa-users m-r-xs"></i>Nro de Empleados</th>
                                                            <th width="25%"><i class="fa fa-briefcase m-r-xs"></i>Tipo de Negociación</th>
                                                            <th width="25%"><i class="fa fa-comment-dollar m-r-xs"></i>Días de crédito</th>
                                                            <th width="25%"><i class="fa fa-file m-r-xs"></i>Contrato</th>
                                                        </tr>
                                                        <tr>
                                                            <td><%# Eval("NroEmpleados") %></td>
                                                            <td><%# Eval("TipoNegociacion") %></td>
                                                            <td><%# Eval("DiasCredito") %></td>
                                                            <td>
                                                                <a href="#"
                                                                    class="ver-documento"
                                                                    data-file="<%# Eval("Contrato") %>">
                                                                    <%# Eval("Contrato") %>
                                                                </a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th width="25%"><i class="fa fa-file m-r-xs"></i>Camara de comercio</th>
                                                            <th width="25%"><i class="fa fa-file m-r-xs"></i>Rut</th>
                                                            <th width="25%"><i class="fa fa-file m-r-xs"></i>Cédula representante legal</th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <a href="#"
                                                                    class="ver-documento"
                                                                    data-file="<%# Eval("CamaraComercio") %>">
                                                                    <%# Eval("CamaraComercio") %>
                                                                </a>

                                                            </td>
                                                            <td>
                                                                <a href="#"
                                                                    class="ver-documento"
                                                                    data-file="<%# Eval("Rut") %>">
                                                                    <%# Eval("Rut") %>
                                                                </a>
                                                            </td>
                                                            <td>
                                                                <a href="#"
                                                                    class="ver-documento"
                                                                    data-file="<%# Eval("CedulaRepLeg") %>">
                                                                    <%# Eval("CedulaRepLeg") %>
                                                                </a>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
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

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();
    </script>

    <!-- pdfJS -->
    <script src="js/plugins/pdfjs/pdf.js"></script>

    <script>
        $(document).on('click', '.ver-documento', function (e) {
            e.preventDefault();

            var archivo = $(this).data('file');

            console.log('Archivo recibido:', archivo);

            if (!archivo) {
                alert('No hay archivo');
                return;
            }

            archivo = archivo.toString().trim();

            var url = './docs/contratos/' + archivo;
            console.log('URL final:', url);

            $('#titulo').text(archivo);

            $('#viewer').html(
                '<p><i class="fa fa-spinner fa-spin"></i> Cargando documento...</p>'
            );

            var ext = archivo.split('.').pop().toLowerCase();
            var html = '';

            if (ext === 'pdf') {
                html =
                    '<object data="' + url + '" type="application/pdf" width="100%" height="600px">' +
                    '<p>No se puede mostrar el PDF. ' +
                    '<a href="' + url + '" target="_blank">Descargar</a></p>' +
                    '</object>';
            }
            else if (ext === 'jpg' || ext === 'jpeg' || ext === 'png') {
                html =
                    '<img src="' + url + '" style="max-width:100%;max-height:600px;" />';
            }
            else {
                html = '<p class="text-danger">Formato no soportado</p>';
            }

            $('#viewer').html(html);
            $('#myModal2').modal('show');
        });
    </script>


</body>

</html>
