<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="empresasconvenio.aspx.cs" Inherits="fpWebApp.empresasconvenio" %>


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

    <title>Fitness People | Empresas convenio</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />


    <!-- CSS de Quill -->
    <link href="https://cdn.quilljs.com/1.3.6/quill.snow.css" rel="stylesheet">
    <!-- JS de Quill -->
    <script src="https://cdn.quilljs.com/1.3.6/quill.min.js"></script>

    <!-- Sweet Alert -->
    <link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">
    <!-- Sweet alert -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

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

    <form id="form1" runat="server">

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


        <input type="hidden" id="hdEmpresaConvenio" />

        <asp:HiddenField ID="hdDocumentoMovimiento" runat="server" />
        <asp:HiddenField ID="hdTipoMovimiento" runat="server" />
        <div id="wrapper">

            <uc1:navbar runat="server" ID="navbar1" />

            <div id="page-wrapper" class="gray-bg">
                <div class="row border-bottom">
                    <uc1:header runat="server" ID="header1" />
                </div>
                <div class="row wrapper border-bottom white-bg page-heading">

                    <%--Inicio Breadcrumb!!!--%>
                    <div class="col-sm-10">
                        <h2><i class="fa fa-user-tie text-success m-r-sm"></i>Empresas convenio</h2>
                        <ol class="breadcrumb">
                            <li><a href="inicio">Inicio</a></li>
                            <li>Corporativo</li>
                            <li class="active"><strong>Empresas convenio</strong></li>
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
                                        <h5>Lista de empresas</h5>
                                    </div>

                                    <div class="ibox-content">

                                        <div class="row" style="font-size: 12px;" runat="server" id="divBotonesLista">
                                            <div class="col-lg-6 form-horizontal">
                                                <div class="form-group">
                                                    <div class="form-group" id="filter-form-container" style="margin-left: 28px;"></div>
                                                </div>
                                            </div>

                                            <div class="col-lg-6 form-horizontal">
                                                <a class="btn btn-success pull-right dim m-l-md" style="font-size: 12px;"
                                                    href="nuevaempresaafiliada" title="Agregar empresa"
                                                    runat="server" id="btnAgregar" visible="false"><i class="fa fa-square-plus m-r-xs"></i>NUEVO
                                                </a>
                                                <asp:LinkButton ID="lbExportarExcel" runat="server" CausesValidation="false"
                                                    CssClass="btn btn-info pull-right dim m-l-md" Style="font-size: 12px;"
                                                    OnClick="lbExportarExcel_Click">
                                                    <i class="fa fa-file-excel m-r-xs"></i>EXCEL
                                                </asp:LinkButton>
                                            </div>
                                        </div>


                                        <div class="modal fade" id="modalMovimientoEmpleado" tabindex="-1">
                                            <div class="modal-dialog">
                                                <div class="modal-content">

                                                    <div class="modal-header text-white bg-primary">
                                                        <h4 class="modal-title mb-0">
                                                            <span id="tituloModal"></span>
                                                            <br />
                                                            <div id="lblNombreEmpleado" class="h5 font-weight-bold mt-1"></div>
                                                        </h4>
                                                        <button type="button" class="close text-white" data-dismiss="modal"></button>
                                                    </div>

                                                    <div class="modal-body">

                                                        <div id="seccionCargoActual" class="form-group">
                                                            <label>Cargo actual</label>
                                                            <input type="text" id="txtCargoActual" class="form-control" disabled />
                                                        </div>
                                                        <!-- SECCION SALARIO ACTUAL -->
                                                        <div id="seccionSalarioActual" class="seccionMovimiento">
                                                            <div class="form-group">
                                                                <label>Salario actual</label>
                                                                <input type="text" id="txtSalarioActual" class="form-control" disabled />
                                                            </div>
                                                        </div>

                                                        <!-- SECCION SEDE ACTUAL -->
                                                        <div id="seccionSedeActual" class="seccionMovimiento" style="display: none;">
                                                            <div class="form-group">
                                                                <label>Sede actual</label>
                                                                <input type="text" id="txtSedeActual" class="form-control" disabled />
                                                            </div>
                                                        </div>

                                                        <!-- SECCION ASCENSO -->
                                                        <div id="seccionCargo" class="seccionMovimiento">
                                                            <div class="form-group">
                                                                <label>Nuevo cargo</label>
                                                                <asp:DropDownList ID="ddlNuevoCargo" runat="server"
                                                                    CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        <div id="seccionSalario" class="seccionMovimiento">
                                                            <div class="form-group">
                                                                <label>Nuevo salario</label>
                                                                <input type="text" id="txtNuevoSalario"
                                                                    class="form-control"
                                                                    onkeyup="formatCurrency(this)"
                                                                    onblur="keepFormatted(this)"
                                                                    autocomplete="off" />
                                                            </div>
                                                        </div>

                                                        <!-- SECCION TRASLADO -->
                                                        <div id="seccionTraslado" class="seccionMovimiento" style="display: none;">



                                                            <div class="form-group">
                                                                <label>Canal de venta actual</label>
                                                                <input type="text" id="txtCanalActual" class="form-control" disabled />
                                                            </div>


                                                            <div class="form-group">
                                                                <label>Nueva sede</label>
                                                                <asp:DropDownList ID="ddlNuevaSede" runat="server"
                                                                    CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>


                                                            <div class="form-group">
                                                                <label>Nuevo canal de venta</label>
                                                                <asp:DropDownList ID="ddlNuevoCanal" runat="server"
                                                                    CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>

                                                        <!-- SECCION INGRESO RAPIDO -->
                                                        <div id="seccionIngresoRapido" class="seccionMovimiento" style="display: none;">

                                                            <div class="form-group">
                                                                <label>Tipo documento</label>
                                                                <asp:DropDownList ID="ddlTipoDocumentoNuevo" runat="server"
                                                                    CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group">
                                                                <label>Documento</label>
                                                                <input type="text" id="txtDocumentoNuevo" class="form-control" />
                                                            </div>

                                                            <div class="form-group">
                                                                <label>Nombre completo</label>
                                                                <input type="text" id="txtNombreNuevo" class="form-control" />
                                                            </div>

                                                            <div class="form-group">
                                                                <label>Correo electrónico</label>
                                                                <input type="email" id="txtCorreoNuevo" class="form-control"
                                                                    oninput="this.value = this.value.toLowerCase();"
                                                                    pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$"
                                                                    title="Ingrese un correo válido (ej: usuario@correo.com)" />
                                                            </div>

                                                            <div class="form-group">
                                                                <label>Sede</label>
                                                                <asp:DropDownList ID="ddlSedeIngreso" runat="server"
                                                                    CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group">
                                                                <label>Canal de venta</label>
                                                                <asp:DropDownList ID="ddlCanalNuevo" runat="server"
                                                                    CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="form-group">
                                                                <label>Cargo</label>
                                                                <asp:DropDownList ID="ddlCargoIngreso" runat="server"
                                                                    CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>



                                                    </div>

                                                    <div class="modal-footer">
                                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">
                                                            Cancelar
                                                        </button>
                                                        <button type="button"
                                                            id="btnGuardarMovimiento"
                                                            class="btn btn-primary"
                                                            onclick="guardarMovimiento()">
                                                            Guardar
                                                        </button>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>


                                        <div class="modal fade" id="modalConvenioEmpresa" tabindex="-1">
                                            <div class="modal-dialog modal-lg">
                                                <div class="modal-content">

                                                    <div class="modal-header bg-primary text-white">
                                                        <h4 class="modal-title">
                                                            <span id="tituloModal1"></span>
                                                            <br />
                                                            <div id="lblNombreEmpresa" class="h5 font-weight-bold"></div>
                                                        </h4>
                                                        <button type="button" class="close text-white" data-dismiss="modal"></button>
                                                    </div>

                                                    <div class="modal-body">

                                                        <div class="row">

                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <label>Fecha inicio convenio</label>
                                                                    <input type="date" id="txbFechaConvenio" class="form-control input-sm" />
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <label>Fecha fin convenio</label>
                                                                    <input type="date" id="txbFechaFinConvenio" class="form-control input-sm" />
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <label>Nro de empleados</label>
                                                                    <input type="number" id="txbNroEmpleados" class="form-control input-sm" />
                                                                </div>
                                                            </div>

                                                        </div>


                                                        <div class="row">

                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label>Tipo negociación</label>
                                                                    <select id="ddlTipoNegociacion" class="form-control input-sm">
                                                                        <option value="">Seleccione</option>
                                                                        <option value="Alianza">Alianza</option>
                                                                        <option value="Convenio">Convenio</option>
                                                                    </select>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label>Días de crédito</label>
                                                                    <select id="ddlDiasCredito" class="form-control input-sm">
                                                                        <option value="">Seleccione</option>
                                                                        <option value="30">30</option>
                                                                        <option value="60">60</option>
                                                                        <option value="90">90</option>
                                                                    </select>
                                                                </div>
                                                            </div>

                                                        </div>


                                                        <div class="form-group">
                                                            <label>Descripción</label>
                                                            <div id="editor"></div>
                                                        </div>

                                                    </div>

                                                    <div class="modal-footer">

                                                        <button class="btn btn-secondary" data-dismiss="modal">Cancelar</button>

                                                        <button type="button"
                                                            class="btn btn-primary"
                                                            onclick="guardarNuevoConvenio()">
                                                            Guardar convenio
                                                        </button>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>


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
                                                    <th data-sortable="true" data-type="text">Razon social</th>
                                                    <th data-sortable="false">Documento</th>
                                                    <th class="text-nowrap" data-breakpoints="xs sm">Estado</th>
                                                    <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpEmpresas" runat="server" OnItemDataBound="rpEmpresas_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="client-avatar">

                                                            <td><a data-toggle="tab" href='#contact-<%# Eval("NombreComercial").ToString().Substring(0,3).ToUpper() %><%# Eval("DocumentoEmpresa") %>' class="client-link"><%# Eval("Nombrecomercial") %></a></td>
                                                            <td><%# Eval("DocumentoEmpresa") %></td>
                                                            <td><%# Eval("EstadoEmpresa") %></td>
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
                                            <asp:Repeater ID="rpTabEmpresas" runat="server" OnItemDataBound="rpTabEmpresas_ItemDataBound">
                                                <ItemTemplate>
                                                    <div id='contact-<%# Eval("NombreComercial").ToString().Substring(0,3).ToUpper() %><%# Eval("DocumentoEmpresa") %>' class='tab-pane <%# Eval("DocumentoEmpresa").ToString() == ViewState["CompanyDoc"]?.ToString() ? "active" : "" %>'>
                                                        <div class="row m-b-lg">
                                                            <div class="ibox-content text-center">
                                                                <h2><%# Eval("NombreComercial") %></h2>

                                                                <hr class="my-2">
                                                                <p class="font-bold text-center">Gestión de convenio</p>

                                                                <div class="text-center">

                                                                    <a href="javascript:void(0);"
                                                                        class="btn btn-xs btn-primary btnNuevoConvenio"
                                                                        data-idempresa='<%# Eval("idEmpresaAfiliada") %>'
                                                                        data-nombre='<%# Eval("NombreComercial") %>'>
                                                                        <i class="fa fa-file-signature m-r-xs"></i>Nuevo convenio
                                                                    </a>

                                                                    <a href="javascript:void(0);"
                                                                        class="btn btn-xs btn-warning btnMovimiento"
                                                                        data-doc='<%# Eval("DocumentoEmpresa") %>'
                                                                        data-nombre='<%# Eval("NombreComercial") %>'
                                                                        data-tipo="CAMBIO_CARGO">
                                                                        <i class="fa fa-user-edit m-r-xs"></i>Editar convenio
                                                                    </a>


                                                                    </a>


                                                                    <a runat="server" id="btnCambioContrato" href="#" class="btn btn-xs btn-warning"><i class="fa fa-person-running m-r-xs" visible="false"></i>Anular convenio</a>
                                                                    <a runat="server" id="btnRetiro" href="#" class="btn btn-xs btn-warning"><i class="fa fa-person-running m-r-xs" visible="false"></i>Renovar convenio</a>

                                                                    <a runat="server" id="btnEditarTab" href="#" class="btn btn-xs btn-primary"><i class="fa fa-edit m-r-xs" visible="false"></i>Editar</a>
                                                                    <%--<asp:LinkButton ID="lkbCambiarEstado" runat="server" 
                                                                    CssClass="btn btn-xs btn-warning" OnClick="lkbCambiarEstado_Click">
                                                                    <i class="fa fa-rotate m-r-xs"></i>Cambiar estado
                                                                </asp:LinkButton>--%>
                                                                    <a runat="server" id="btnCambiarEstado" href="#" visible="false"
                                                                        class='btn btn-xs btn-danger'><i class="fa fa-rotate m-r-xs"></i><%# Eval("EstadoEmpresa") %> (cambiar)
                                                                    </a>
                                                                </div>
                                                                <hr class="m-0">
                                                            </div>
                                                        </div>
                                                        <div class="client-detail">
                                                            <div class="full-height-scroll">

                                                                <div class="feed-activity-list">

                                                                    <%--                                                                    <div class="feed-element">
                                                                        <div>
                                                                            <strong><i class="fa fa-map-marker m-r-xs"></i>Dirección</strong>
                                                                            <div class="text-info"><%# Eval("DireccionEmpleado") %></div>
                                                                            <small class="text-muted"><i class="fa fa-city m-r-xs"></i><%# Eval("NombreCiudad") %></small>
                                                                        </div>
                                                                    </div>--%>

                                                                    <%--                                                                 <div class="feed-element">
                                                                        <div>
                                                                            <strong><i class="fab fa-whatsapp m-r-xs"></i>Teléfono corporativo</strong>
                                                                            <div><a href="https://wa.me/57<%# Eval("TelefonoCorporativo") %>" target="_blank"><%# Eval("TelefonoCorporativo") %></a></div>
                                                                            <small class="text-muted"><i class="fa fa-envelope m-r-xs"></i>Email corporativo: <%# Eval("EmailCorporativo") %></small>
                                                                        </div>
                                                                    </div>--%>

                                                                    <%--<div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-envelope m-r-xs"></i>Email corporativo</strong>
                                                                        <div><%# Eval("EmailCorporativo") %></div>
                                                                    </div>
                                                                </div>--%>

                                                                    <%--                                                                    <div class="feed-element">
                                                                        <div>
                                                                            <strong><i class="fab fa-whatsapp m-r-xs"></i>Teléfono personal</strong>
                                                                            <div><a href="https://wa.me/57<%# Eval("TelefonoEmpleado") %>" target="_blank"><%# Eval("TelefonoEmpleado") %></a></div>
                                                                            <small class="text-muted"><i class="fa fa-envelope m-r-xs"></i>Email personal: <%# Eval("EmailEmpleado") %></small>
                                                                        </div>
                                                                    </div>--%>

                                                                    <%--<div class="feed-element">
                                                                    <div>
                                                                        <strong><i class="fa fa-envelope m-r-xs"></i>Email personal</strong>
                                                                        <div><%# Eval("EmailEmpleado") %></div>
                                                                    </div>
                                                                </div>--%>

                                                                    <%--                                                                    <div class="feed-element">
                                                                        <div>
                                                                            <strong><i class="fa fa-ring m-r-xs"></i>Estado civil</strong>
                                                                            <div class="text-info"><%# Eval("EstadoCivil") %></div>
                                                                        </div>
                                                                    </div>--%>

                                                                    <%--                                                                    <div class="feed-element">
                                                                        <div>
                                                                            <strong><i class="fa fa-file-lines m-r-xs"></i>Tipo de contrato</strong>
                                                                            <div class="text-info"><%# Eval("TipoContrato") %></div>
                                                                        </div>
                                                                    </div>--%>

                                                                    <%--                                                                    <div class="feed-element">
                                                                        <div>
                                                                            <strong><i class="fa fa-graduation-cap m-r-xs"></i>Nivel estudio</strong>
                                                                            <div class="text-info"><%# Eval("NivelEstudio") %></div>
                                                                        </div>
                                                                    </div>--%>

                                                                    <%--                                                                    <div class="feed-element">
                                                                        <div>
                                                                            <small class="pull-right text-navy">Estrato: <%# Eval("EstratoSocioeconomico") %></small>
                                                                            <strong><i class="fa fa-house m-r-xs"></i>Tipo de vivienda</strong>
                                                                            <div class="text-info"><%# Eval("TipoVivienda") %></div>
                                                                            <small class="text-muted">Personas nucleo familiar: <%# Eval("PersonasNucleoFamiliar") %></small>
                                                                        </div>
                                                                    </div>--%>

                                                                    <%--                                                                    <div class="feed-element">
                                                                        <div>
                                                                            <strong><i class="fa fa-person-rays m-r-xs"></i>Actividad extra</strong>
                                                                            <div class="text-info"><%# Eval("ActividadExtra") %></div>
                                                                        </div>
                                                                    </div>--%>

                                                                    <%--                                                                    <div class="feed-element">
                                                                        <div>
                                                                            <strong><i class="fa fa-martini-glass m-r-xs"></i>Consume licor</strong>
                                                                            <div class="text-info"><%# Eval("ConsumeLicor") %></div>
                                                                        </div>
                                                                    </div>--%>

                                                                    <%--                                                                    <div class="feed-element">
                                                                        <div>
                                                                            <strong><i class="fa fa-car m-r-xs"></i>Medio de transporte</strong>
                                                                            <div class="text-info"><%# Eval("MedioTransporte") %></div>
                                                                        </div>
                                                                    </div>--%>

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
                                                                <%--                                                                <div id="vertical-timeline" class="vertical-container dark-timeline">

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

                                                                </div>--%>
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


                        <%--Fin Contenido!!!!--%>
                    </div>
                </div>

                <uc1:footer runat="server" ID="footer1" />

            </div>
            <uc1:rightsidebar runat="server" ID="rightsidebar1" />
        </div>


        <%--        <script>

        var documentoSeleccionado = "";

        function abrirAscenso(doc) {

            documentoSeleccionado = doc;

            console.log("Documento recibido:", documentoSeleccionado);

            $("#modalAscenso").modal("show");
        }

        </script>--%>
    </form>

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

    <!-- Gráficas -->

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

    <script>
        $(document).on("click", ".btnAscenso", function () {

            var documento = $(this).data("doc");
            var nombreEmpleado = $(this).data("nombre");
            $("#lblNombreEmpleado").text(nombreEmpleado);

            $("#txtDocumentoAscenso").val(documento);

            // Llamar método para traer info actual
            $.ajax({
                type: "POST",
                url: "Empleados.aspx/ObtenerDatosEmpleado",
                data: JSON.stringify({ documento: documento }),
                contentType: "application/json; charset=utf-8",
                success: function (response) {

                    var data = response.d;

                    $("#txtCargoActual").val(data.Cargo);
                    $("#txtSalarioActual").val(data.Sueldo);
                    $("#txtNuevoSalario").val(data.Sueldo);

                    $("#modalAscenso").modal("show");
                }
            });

        });

    </script>

    <script>
        $(document).on("click", ".btnAscenso", function () {

            var documento = $(this).data("doc");

            console.log("Documento detectado:", documento);

            $("#hdDocumentoAscenso").val(documento);

            $.ajax({
                type: "POST",
                url: "Empleados.aspx/ObtenerDatosEmpleado",
                data: JSON.stringify({ documento: documento }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    var data = response.d;

                    $("#txtCargoActual").val(data.Cargo);
                    var sueldoActual = parseFloat(data.Sueldo);

                    $("#txtSalarioActual").val(
                        "$ " + sueldoActual.toLocaleString("es-CO")
                    );
                    $("#txtNuevoSalario").val("$ 0");

                    $("#modalAscenso").modal("show");
                }
            });
        });
    </script>


    <script>
        function guardarCambioCargo() {

            var documento = $("#hdDocumentoMovimiento").val();
            var idNuevoCargo = parseInt($("#ddlNuevoCargo").val());

            if (!idNuevoCargo || isNaN(idNuevoCargo)) {
                alert("Seleccione un nuevo cargo.");
                return;
            }

            $.ajax({
                type: "POST",
                url: "Empleados.aspx/InsertarCambioCargoEmpleado",
                data: JSON.stringify({
                    documento: documento,
                    idNuevoCargo: idNuevoCargo
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.d.success) {

                        $("#modalMovimientoEmpleado").modal("hide");

                        Swal.fire({
                            title: 'Cambio de cargo registrado',
                            text: response.d.mensaje,
                            icon: 'success',
                            timer: 2500,
                            showConfirmButton: false
                        }).then(() => {
                            location.reload();
                        });

                    } else {
                        alert(response.d.mensaje);
                    }
                }
            });
        }
    </script>

    <script>
        function guardarCambioSalarial() {

            var documento = $("#hdDocumentoMovimiento").val();

            var sueldoTexto = $("#txtNuevoSalario").val() || "";
            sueldoTexto = sueldoTexto.replace(/\$/g, "").replace(/\./g, "").trim();

            var nuevoSueldo = parseFloat(sueldoTexto);

            if (isNaN(nuevoSueldo) || nuevoSueldo <= 0) {
                alert("Ingrese un salario válido.");
                return;
            }

            $.ajax({
                type: "POST",
                url: "Empleados.aspx/InsertarCambioSalarialEmpleado",
                data: JSON.stringify({
                    documento: documento,
                    nuevoSueldo: nuevoSueldo
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.d.success) {

                        $("#modalMovimientoEmpleado").modal("hide");

                        Swal.fire({
                            title: 'Cambio salarial registrado',
                            text: response.d.mensaje,
                            icon: 'success',
                            timer: 2500,
                            showConfirmButton: false
                        }).then(() => {
                            location.reload();
                        });

                    } else {
                        alert(response.d.mensaje);
                    }
                }
            });
        }
    </script>



    <script>
        function guardarTraslado() {

            var documento = $("#hdDocumentoMovimiento").val();
            var idNuevaSede = parseInt($("#ddlNuevaSede").val());
            var idNuevoCanal = parseInt($("#ddlNuevoCanal").val());

            if (!idNuevaSede || isNaN(idNuevaSede)) {
                alert("Seleccione una sede válida.");
                return;
            }

            if (!idNuevoCanal || isNaN(idNuevoCanal)) {
                alert("Seleccione un canal de venta.");
                return;
            }

            $.ajax({
                type: "POST",
                url: "Empleados.aspx/InsertarTrasladoEmpleado",
                data: JSON.stringify({
                    documento: documento,
                    idNuevaSede: idNuevaSede,
                    idNuevoCanal: idNuevoCanal
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.d.success) {

                        $("#modalMovimientoEmpleado").modal("hide");

                        Swal.fire({
                            title: 'Traslado registrado',
                            text: response.d.mensaje,
                            icon: 'success',
                            timer: 2500,
                            showConfirmButton: false
                        }).then(() => {
                            location.reload();
                        });

                    } else {
                        alert(response.d.mensaje);
                    }
                }
            });
        }
    </script>

    <script>
        function guardarIngresoRapido() {

            var tipoDocumento = $("#ddlTipoDocumentoNuevo").val();
            var documento = $("#txtDocumentoNuevo").val();
            var nombre = $("#txtNombreNuevo").val();
            var correo = $("#txtCorreoNuevo").val();
            var sede = $("#ddlSedeIngreso").val();
            var canal = $("#ddlCanalNuevo").val();
            var cargo = $("#ddlCargoIngreso").val();

            if (!documento || !nombre) {
                alert("Documento y nombre son obligatorios");
                return;
            }

            $.ajax({
                type: "POST",
                url: "Empleados.aspx/InsertarDatosBasicosEmpleado",
                data: JSON.stringify({
                    tipoDocumento: tipoDocumento,
                    documento: documento,
                    nombre: nombre,
                    correo: correo,
                    canal: canal,
                    sede: sede,
                    cargo: cargo
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.d.success) {

                        $("#modalMovimientoEmpleado").modal("hide");

                        Swal.fire({
                            title: "Empleado creado",
                            text: response.d.mensaje,
                            icon: "success",
                            timer: 2500,
                            showConfirmButton: false
                        }).then(() => {
                            location.reload();
                        });

                    } else {
                        alert(response.d.mensaje);
                    }
                }
            });
        }
    </script>

    <script>
        $(document).on("click", ".btnMovimiento", function () {

            var documento = $(this).data("doc");
            var nombreEmpleado = $(this).data("nombre");
            var tipo = $(this).data("tipo");

            $("#hdDocumentoMovimiento").val(documento);
            $("#hdTipoMovimiento").val(tipo);
            $("#lblNombreEmpleado").text(nombreEmpleado);

            cargarDatosEmpleado(documento, tipo);
        });
    </script>

    <script>
        function cargarDatosEmpleado(documento, tipo) {

            $.ajax({
                type: "POST",
                url: "Empleados.aspx/ObtenerDatosEmpleado",
                data: JSON.stringify({ documento: documento }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    var data = response.d;

                    $("#txtCargoActual").val(data.Cargo);

                    var sueldoActual = parseFloat(data.Sueldo);
                    $("#txtSalarioActual").val(
                        "$ " + sueldoActual.toLocaleString("es-CO")
                    );

                    $("#txtSedeActual").val(data.Sede);

                    $("#txtCanalActual").val(data.CanalVenta);

                    configurarModalSegunTipo(tipo);

                    $("#modalMovimientoEmpleado").modal("show");
                }
            });
        }
    </script>

    <script>
        function configurarModalSegunTipo(tipo) {

            $(".seccionMovimiento").hide();

            if (tipo === "CAMBIO_CARGO") {

                $("#tituloModal").text("Cambio de cargo");

                $("#btnGuardarMovimiento")
                    .removeClass()
                    .addClass("btn btn-warning");

                $("#seccionCargo").show();
                $("#seccionSalarioActual").show();
            }

            if (tipo === "CAMBIO_SALARIAL") {

                $("#tituloModal").text("Cambio salarial");

                $("#btnGuardarMovimiento")
                    .removeClass()
                    .addClass("btn btn-success");

                $("#seccionSalarioActual").show();
                $("#seccionSalario").show();

                $("#txtNuevoSalario").val("$ 0");
            }

            if (tipo === "TRASLADO") {

                $("#tituloModal").text("Traslado de empleado");

                $("#btnGuardarMovimiento")
                    .removeClass()
                    .addClass("btn btn-info");

                $("#seccionSedeActual").show();
                $("#seccionTraslado").show();
            }

            if (tipo === "INGRESO_RAPIDO") {

                $("#tituloModal").text("Nuevo ingreso rápido");

                $("#btnGuardarMovimiento")
                    .removeClass()
                    .addClass("btn btn-primary");
                $("#seccionCargoActual").hide();
                $("#seccionIngresoRapido").show();

            }
        }
    </script>

    <script>
        function guardarMovimiento() {

            var tipo = $("#hdTipoMovimiento").val();

            if (tipo === "CAMBIO_CARGO") {
                guardarCambioCargo();
            }

            if (tipo === "CAMBIO_SALARIAL") {
                guardarCambioSalarial();
            }

            if (tipo === "TRASLADO") {
                guardarTraslado();
            }
            if (tipo === "INGRESO_RAPIDO") {
                guardarIngresoRapido();
            }

        }
    </script>

    <script>
        function formatCurrency(input) {

            let value = input.value.replace(/\D/g, "");

            if (value === "") {
                input.value = "";
                return;
            }

            let number = parseInt(value, 10);

            input.value = "$ " + number.toLocaleString("es-CO");
        }

        function keepFormatted(input) {

            if (input.value === "") return;

            let value = input.value.replace(/\D/g, "");
            let number = parseInt(value, 10);

            input.value = "$ " + number.toLocaleString("es-CO");
        }
    </script>

    <script>
        function abrirIngresoRapido() {

            $("#hdTipoMovimiento").val("INGRESO_RAPIDO");

            configurarModalSegunTipo("INGRESO_RAPIDO");

            $("#modalMovimientoEmpleado").modal("show");
        }
    </script>

    <script>
        $(document).on("click", ".btnIngresoRapido", function () {

            $("#hdTipoMovimiento").val("INGRESO_RAPIDO");

            configurarModalSegunTipo("INGRESO_RAPIDO");
            $("#txtDocumentoNuevo").val("");
            $("#txtNombreNuevo").val("");
            $("#txtCorreoNuevo").val("");
            txtCargoActual


            $("#modalMovimientoEmpleado").modal("show");
        });
    </script>

    <script>
        $(document).ready(function () {

            $('#txtDocumentoNuevo').on('change blur', function () {

                var documento = $(this).val().trim();
                if (documento.length === 0) return;

                var url = 'https://pqrdsuperargo.supersalud.gov.co/api/api/adres/0/' + documento;

                // limpiar campo nombre
                $('#txtNombreNuevo').val('');

                $.ajax({
                    url: url,
                    method: 'GET',

                    success: function (data) {

                        var nombreCompleto =
                            [data.nombre, data.s_nombre].filter(Boolean).join(' ') + ' ' +
                            [data.apellido, data.s_apellido].filter(Boolean).join(' ');

                        $('#txtNombreNuevo').val(nombreCompleto.toUpperCase());

                    },

                    error: function () {

                        console.log("No se encontró información en ADRES");

                    }
                });

            });

        });
    </script>

    <script>

        $(document).on("click", ".btnNuevoConvenio", function () {

            var idEmpresa = $(this).data("idempresa");
            var nombreEmpresa = $(this).data("nombre");

            $("#hdEmpresaConvenio").val(idEmpresa);

            $("#tituloModal").text("Nuevo Convenio");

            $("#lblNombreEmpleado").text(nombreEmpresa);

            $("#modalConvenioEmpresa").modal("show");

        });

    </script>


    <script>
        function guardarNuevoConvenio() {

            var idEmpresa = $("#hdEmpresaConvenio").val();
            var fechaConvenio = $("#txbFechaConvenio").val();
            var fechaFin = $("#txbFechaFinConvenio").val();
            var nroEmpleados = $("#txbNroEmpleados").val();
            var tipoNegociacion = $("#ddlTipoNegociacion").val();
            var diasCredito = $("#ddlDiasCredito").val();

            var descripcion = $("#editor").html();

            if (!fechaConvenio) {
                alert("Debe ingresar la fecha de inicio del convenio");
                return;
            }

            if (!tipoNegociacion) {
                alert("Seleccione el tipo de negociación");
                return;
            }

            $.ajax({

                type: "POST",
                url: "empresasconvenio.aspx/InsertarConvenioEmpresa",

                data: JSON.stringify({
                    idEmpresaAfiliada: idEmpresa,
                    fechaConvenio: fechaConvenio,
                    fechaFinConvenio: fechaFin,
                    nroEmpleados: nroEmpleados,
                    tipoNegociacion: tipoNegociacion,
                    diasCredito: diasCredito,
                    descripcion: descripcion
                }),

                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    if (response.d.success) {

                        $("#modalConvenioEmpresa").modal("hide");

                        Swal.fire({
                            title: "Convenio creado",
                            text: "Registro guardado correctamente",
                            icon: "success",
                            timer: 2000,
                            showConfirmButton: false
                        }).then(() => {
                            location.reload();
                        });

                    } else {

                        Swal.fire("Error", response.d.mensaje, "error");

                    }

                },

                error: function (xhr) {

                    console.log(xhr.responseText);

                    Swal.fire(
                        "Error",
                        "Error al guardar el convenio",
                        "error"
                    );

                }

            });

        }
    </script>





</body>

</html>
