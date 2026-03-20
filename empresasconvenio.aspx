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

    <script>    <!-- CSS de Quill -->
    <link href="https://cdn.quilljs.com/1.3.6/quill.snow.css" rel="stylesheet">
    <!-- JS de Quill -->
    <script src="https://cdn.quilljs.com/1.3.6/quill.min.js"></script>

    <script>
        var quill;
        document.addEventListener("DOMContentLoaded", function () {
            quill = new Quill("#editor", {
                theme: "snow",
                modules: {
                    toolbar: [
                        [{ 'header': [1, 2, 3, false] }],
                        ['bold'], // Negrita y Tachado
                        ['italic', 'underline'],
                        [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                        [{ 'align': [] }],
                    ]
                }
            });
            function ajustarAlturaEditor() {
                var editorContenido = document.querySelector(".ql-editor");
                editorContenido.style.height = "auto";
                editorContenido.style.height = editorContenido.scrollHeight + "px";
            }
            quill.on("text-change", ajustarAlturaEditor);

            var contenidoGuardado = document.getElementById('<%= hiddenEditor.ClientID %>').value;
            if (contenidoGuardado.trim() !== "") {
                quill.root.innerHTML = contenidoGuardado;
            }
        });
        function guardarContenidoEditor() {
            var contenido = quill.root.innerHTML;
            document.getElementById('<%= hiddenEditor.ClientID %>').value = contenido;
        }
    </script>

    <style>
        #editor .ql-editor {
            min-height: 80px; /* 🔥 ajusta aquí (2-3 líneas) */
        }
    </style>

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

                                                        <div class="row">

                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label>Nombre del Pagador</label>
                                                                    <input type="text" id="txbNombrePagador"
                                                                        class="form-control input-sm"
                                                                        placeholder="Pagador"
                                                                        style="text-transform: uppercase;" />
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label>Celular</label>
                                                                    <input type="text" id="txbCelularPagador"
                                                                        class="form-control input-sm"
                                                                        placeholder="Celular del Pagador" />
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label>Correo del pagador</label>
                                                                    <input type="email" id="txbCorreoPagador"
                                                                        class="form-control input-sm"
                                                                        placeholder="Correo Pagador" />
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label>Retorno administrativo</label>

                                                                    <div>
                                                                        <label class="radio-inline">
                                                                            <input type="radio" name="rblRetorno" value="1">
                                                                            Sí
                                                                        </label>
                                                                        <label class="radio-inline">
                                                                            <input type="radio" name="rblRetorno" value="0" checked>
                                                                            No
                                                                        </label>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="form-group">
                                                            <label>Descripción</label>
                                                            <div id="editor"></div>
                                                        </div>
                                                        <asp:HiddenField ID="hiddenEditor" runat="server" />
                                                        <input type="hidden" id="hdEmpresaConvenio" />
                                                        <input type="hidden" id="hdTipoConvenio" />
                                                        <input type="hidden" id="hdIdConvenio" />

                                                    </div>

                                                    <div class="modal-footer">

                                                        <button class="btn btn-secondary" data-dismiss="modal">Cancelar</button>

                                                        <button type="button"
                                                            id="btnGuardarConvenio"
                                                            class="btn btn-primary"
                                                            onclick="guardarConvenio()">
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
                                                    <th data-sortable="false">Documento</th>
                                                    <th data-sortable="true" data-type="text">Razon social</th>
                                                    <th class="text-nowrap" data-breakpoints="xs sm">Estado</th>
                                                    <th class="text-nowrap" data-breakpoints="xs sm">Asesor</th>
                                                    <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpEmpresas" runat="server" OnItemDataBound="rpEmpresas_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="client-avatar">

                                                            <td><%# Eval("DocumentoEmpresa") %></td>
                                                            <td><a data-toggle="tab" href='#contact-<%# Eval("NombreComercial").ToString().Substring(0,3).ToUpper() %><%# Eval("DocumentoEmpresa") %>' class="client-link"><%# Eval("Nombrecomercial") %></a></td>
                                                            <td><%# Eval("EstadoAsignacion") %></td>
                                                            <td><%# Eval("NombreUsuario") %></td>
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
                                                                        data-documento='<%# Eval("DocumentoEmpresa") %>'
                                                                        data-nombre='<%# Eval("NombreComercial") %>'
                                                                        data-nombrepagador='<%# Eval("NombrePagador") %>'
                                                                        data-telefono='<%# Eval("TelefonoPagador") %>'
                                                                        data-correo='<%# Eval("CorreoPagador") %>'
                                                                        data-retorno='<%# Eval("RetornoAdm") %>'>

                                                                        <i class="fa fa-file-signature m-r-xs"></i>Nuevo convenio
                                                                    </a>

                                                                    <a runat="server" id="btnCambiarEstado" href="#" visible="false"
                                                                        class='btn btn-xs btn-danger'><i class="fa fa-rotate m-r-xs"></i><%# Eval("EstadoEmpresa") %> (cambiar)
                                                                    </a>
                                                                </div>
                                                                <hr class="m-0">

                                                                <div class="table-responsive m-t-sm">
                                                                    <table class="table table-bordered table-hover">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>Convenio</th>
                                                                                <th>Fecha</th>
                                                                                <th>Acciones</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="rpConvenios" runat="server">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <span class="label label-primary">
                                                                                                <%# Eval("NombreConvenio") %>
                                                                                            </span>
                                                                                        </td>
                                                                                        <td>
                                                                                            <%# Eval("FechaConvenio", "{0:dd/MM/yyyy}") %>
                                                                                        </td>
                                                                                        <td class="text-center">
                                                                                            <div class="btn-group">
                                                                                                <!-- VER -->
                                                                                                <a href="javascript:void(0);"
                                                                                                    class="btn btn-xs btn-default m-r-xs btnVerConvenio"
                                                                                                    data-idconvenio='<%# Eval("idConvenio") %>'
                                                                                                    data-fecha='<%# Eval("FechaConvenio", "{0:yyyy-MM-dd}") %>'
                                                                                                    data-fechafin='<%# Eval("FechaFinConvenio", "{0:yyyy-MM-dd}") %>'
                                                                                                    data-tipo='<%# Eval("TipoNegociacion") %>'
                                                                                                    data-dias='<%# Eval("DiasCredito") %>'
                                                                                                    data-desc='<%# HttpUtility.HtmlEncode(Eval("Descripcion").ToString()) %>'
                                                                                                    data-nroempleados='<%# Eval("NroEmpleados") %>'
                                                                                                    data-nombrepagador='<%# Eval("NombrePagador") %>'
                                                                                                    data-telefono='<%# Eval("TelefonoPagador") %>'
                                                                                                    data-correo='<%# Eval("CorreoPagador") %>'
                                                                                                    data-retorno='<%# Eval("RetornoAdm") %>'>

                                                                                                    <i class="fa fa-eye"></i>
                                                                                                </a>

                                                                                                <!-- DOCUMENTOS -->
                                                                                                <a href="javascript:void(0);"
                                                                                                    class="btn btn-xs btn-warning m-r-xs"
                                                                                                    title="Documentos"
                                                                                                    data-toggle="tooltip"
                                                                                                    data-idconvenio='<%# Eval("idConvenio") %>'>
                                                                                                    <i class="fa fa-file-pdf"></i>
                                                                                                </a>

                                                                                                <!-- EDITAR -->
                                                                                                <a href="javascript:void(0);"
                                                                                                    class="btn btn-xs btn-primary m-r-xs btnEditarConvenio"
                                                                                                    title="Editar convenio"
                                                                                                    data-idconvenio='<%# Eval("idConvenio") %>'
                                                                                                    data-fecha='<%# Eval("FechaConvenio", "{0:yyyy-MM-dd}") %>'
                                                                                                    data-fechafin='<%# Eval("FechaFinConvenio", "{0:yyyy-MM-dd}") %>'
                                                                                                    data-tipo='<%# Eval("TipoNegociacion") %>'
                                                                                                    data-dias='<%# Eval("DiasCredito") %>'
                                                                                                    data-desc='<%# HttpUtility.HtmlEncode(Eval("Descripcion").ToString()) %>'
                                                                                                    data-nroempleados='<%# Eval("NroEmpleados") %>'
                                                                                                    data-nombrepagador='<%# Eval("NombrePagador") %>'
                                                                                                    data-telefono='<%# Eval("TelefonoPagador") %>'
                                                                                                    data-correo='<%# Eval("CorreoPagador") %>'
                                                                                                    data-retorno='<%# Eval("RetornoAdm") %>'>

                                                                                                    <i class="fa fa-edit"></i>
                                                                                                </a>

                                                                                                <!-- RENOVAR -->
                                                                                                <a href="javascript:void(0);"
                                                                                                    class="btn btn-xs btn-success m-r-xs"
                                                                                                    title="Renovar convenio"
                                                                                                    data-toggle="tooltip"
                                                                                                    data-idconvenio='<%# Eval("idConvenio") %>'>
                                                                                                    <i class="fa fa-sync"></i>
                                                                                                </a>

                                                                                                <!-- ANULAR -->
                                                                                                <a href="javascript:void(0);"
                                                                                                    class="btn btn-xs btn-danger m-r-xs"
                                                                                                    title="Anular convenio"
                                                                                                    data-toggle="tooltip"
                                                                                                    data-idconvenio='<%# Eval("idConvenio") %>'>
                                                                                                    <i class="fa fa-times"></i>
                                                                                                </a>


                                                                                            </div>

                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </tbody>
                                                                    </table>
                                                                </div>


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


    <!-- CONVENIOS -->


    <script>    
        $(document).on("click", ".btnNuevoConvenio", function () {

        console.log("CLICK NUEVO CONVENIO");

        // datos empresa
        var idEmpresa = $(this).data("idempresa");
        var documento = $(this).data("documento");
        var nombre = $(this).data("nombre");

        $("#hdEmpresaConvenio").val(idEmpresa);

        $("#lblNombreEmpresa").html(
            "<b>" + nombre + "</b><br>" +
            "<b>Documento: " + documento + "</b>"
        );

        abrirModalConvenio("NUEVO", null);
    });
    </script>

    <script>

    $(document).on("click", ".btnVerConvenio", function () {

        console.log("DATA:", $(this).data()); // debug

        var data = {
            idconvenio: $(this).data("idconvenio"),
            fecha: $(this).data("fecha"),
            fechafin: $(this).data("fechafin"),
            tipo: $(this).data("tipo"),
            dias: $(this).data("dias"),
            desc: $(this).data("desc"),

            nroempleados: $(this).data("nroempleados"),
            nombrepagador: $(this).data("nombrepagador"),
            telefono: $(this).data("telefono"),
            correo: $(this).data("correo"),
            retorno: $(this).data("retorno")
        };

        abrirModalConvenio("VER", data);
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

            var descripcion = quill.root.innerHTML;

            var nombrePagador = $("#txbNombrePagador").val();
            var telefonoPagador = $("#txbCelularPagador").val();
            var correoPagador = $("#txbCorreoPagador").val();
            var retornoAdm = $("input[name='rblRetorno']:checked").val();

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
                descripcion: descripcion,  

                nombrePagador: nombrePagador,
                telefonoPagador: telefonoPagador,
                correoPagador: correoPagador,
                retornoAdm: retornoAdm
            }),

                contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (response) {

                if (response.d.success) {

                    $("#modalConvenioEmpresa").modal("hide");

                    Swal.fire({
                        title: "Convenio creado correctamente",
                        text: "Corporativo - Fitness People",
                        icon: "success",
                        timer: 2500,
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

    <script>   
        
    function abrirModalConvenio(tipo, data) {

        $("#hdTipoConvenio").val(tipo);

        //  RESET GENERAL
        $("#modalConvenioEmpresa input, #modalConvenioEmpresa select")
            .prop("disabled", false);

        $("#editor").css("pointer-events", "auto").css("background", "#fff");
        $("#btnGuardarConvenio").show();

        //  ASEGURAR QUE QUILL EXISTE
        if (!quill) {
            quill = new Quill("#editor", {
                theme: "snow",
                modules: {
                    toolbar: [
                        [{ 'header': [1, 2, 3, false] }],
                        ['bold'],
                        ['italic', 'underline'],
                        [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                        [{ 'align': [] }]
                    ]
                }
            });
        }

        //  LIMPIAR SI ES NUEVO
        if (tipo === "NUEVO") {
            limpiarFormulario();

            if (quill) {
                quill.root.innerHTML = "";
            }
        }

        //  CARGAR DATOS
        if (data) {

            $("#hdIdConvenio").val(data.idconvenio || "");

            $("#txbFechaConvenio").val(data.fecha || "");
            $("#txbFechaFinConvenio").val(data.fechafin || "");
            $("#txbNroEmpleados").val(data.nroempleados || "");
            $("#ddlTipoNegociacion").val(data.tipo || "");
            $("#ddlDiasCredito").val(data.dias || "");

            //  QUILL CORRECTO
            if (quill && data.desc) {

                var contenido = data.desc;
               
                contenido = contenido.replace(/<div class="ql-editor[^>]*>/i, "");
                contenido = contenido.replace(/<\/div>\s*<div class="ql-clipboard[^>]*>.*?<\/div>/i, "");

                quill.root.innerHTML = contenido;
            }

            $("#txbNombrePagador").val(data.nombrepagador || "");
            $("#txbCelularPagador").val(data.telefono || "");
            $("#txbCorreoPagador").val(data.correo || "");

            var retorno = (data.retorno == "1,00" || data.retorno == "1") ? "1" : "0";

            $("input[name='rblRetorno'][value='" + retorno + "']")
                .prop("checked", true);
        }

        //  TITULO
        var titulo = "Convenio";
        if (tipo === "NUEVO") titulo = "Nuevo convenio";
        if (tipo === "EDITAR") titulo = "Editar convenio";
        if (tipo === "RENOVAR") titulo = "Renovar convenio";
        if (tipo === "VER") titulo = "Detalle del convenio";

        $("#tituloModal1").text(titulo);

        // 🔥 MODO VER
        if (tipo === "VER") {
            $("#modalConvenioEmpresa input, #modalConvenioEmpresa select")
                .prop("disabled", true);

            $("#editor").css("pointer-events", "none").css("background", "#f5f5f5");

            $("#btnGuardarConvenio").hide();
        }

        //  ABRIR MODAL
        $("#modalConvenioEmpresa").modal("show");
}
    </script>


    <script>
        function limpiarFormulario() {

            $("#txbFechaConvenio").val("");
            $("#txbFechaFinConvenio").val("");
            $("#txbNroEmpleados").val("");
            $("#ddlTipoNegociacion").val("");
            $("#ddlDiasCredito").val("");
            quill.root.innerHTML = "";

            $("#txbNombrePagador").val("");
            $("#txbCelularPagador").val("");
            $("#txbCorreoPagador").val("");
            $("input[name='rblRetorno'][value='0']").prop("checked", true);

        }
    </script>

    <script>
        function guardarConvenio() {

        var tipo = $("#hdTipoConvenio").val();
        console.log("tipo:", tipo);

        if (tipo === "NUEVO") {
            guardarNuevoConvenio();
        }

        if (tipo === "EDITAR") {
            actualizarConvenio();
        }

            if (tipo === "RENOVAR") {
                renovarConvenio();
            }
        }
    </script>


    <script>  
            $(document).on("click", ".btnEditarConvenio", function () {

            console.log("EDITAR DATA:", $(this).data());

            var data = {
                idconvenio: $(this).data("idconvenio"),
                fecha: $(this).data("fecha"),
                fechafin: $(this).data("fechafin"),
                tipo: $(this).data("tipo"),
                dias: $(this).data("dias"),
                desc: $(this).data("desc"),

                nroempleados: $(this).data("nroempleados"),
                nombrepagador: $(this).data("nombrepagador"),
                telefono: $(this).data("telefono"),
                correo: $(this).data("correo"),
                retorno: $(this).data("retorno")
            };

            abrirModalConvenio("EDITAR", data);
        });
    </script>


    <script>
        function actualizarConvenio() {

        var idConvenio = $("#hdIdConvenio").val();

        var fechaConvenio = $("#txbFechaConvenio").val();
        var fechaFin = $("#txbFechaFinConvenio").val();
        var nroEmpleados = $("#txbNroEmpleados").val();
        var tipoNegociacion = $("#ddlTipoNegociacion").val();
        var diasCredito = $("#ddlDiasCredito").val();

        var descripcion = quill.root.innerHTML;

        var nombrePagador = $("#txbNombrePagador").val();
        var telefonoPagador = $("#txbCelularPagador").val();
        var correoPagador = $("#txbCorreoPagador").val();
        var retornoAdm = $("input[name='rblRetorno']:checked").val();

        if (!fechaConvenio) {
            Swal.fire("Error", "Debe ingresar la fecha de inicio", "error");
            return;
        }

        $.ajax({
            type: "POST",
            url: "empresasconvenio.aspx/ActualizarConvenioEmpresa",

            data: JSON.stringify({
                idConvenio: idConvenio,
                fechaConvenio: fechaConvenio,
                fechaFinConvenio: fechaFin,
                nroEmpleados: nroEmpleados,
                tipoNegociacion: tipoNegociacion,
                diasCredito: diasCredito,
                descripcion: descripcion,

                nombrePagador: nombrePagador,
                telefonoPagador: telefonoPagador,
                correoPagador: correoPagador,
                retornoAdm: retornoAdm
            }),

            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (response) {

                if (response.d.success) {

                    $("#modalConvenioEmpresa").modal("hide");

                    Swal.fire({
                        title: "Convenio actualizado",
                        text: "Corporativo - Fitness People",
                        icon: "success",
                        timer: 2500,
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
                Swal.fire("Error", "Error al actualizar", "error");
            }
        });
    }
    </script>


    <script>
    function anularConvenio(id) {

        Swal.fire({
            title: "¿Anular convenio?",
            text: "Esta acción no se puede deshacer",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Sí, anular"
        }).then((result) => {

            if (result.isConfirmed) {
                // llamar AJAX
            }

        });
    }
    </script>




</body>

</html>
