<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="negociarconvenio.aspx.cs" Inherits="fpWebApp.negociarconvenio" ValidateRequest="false" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/indicadoresCEO.ascx" TagPrefix="uc1" TagName="indicadores01" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Negociar convenio</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- CSS de Quill -->
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

    <!-- Sweet Alert -->
    <link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">

    <!-- Sweet alert -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <%--<link href="css/plugins/footable/footable.bootstrap.css" rel="stylesheet" />--%>
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

    <style>
        #tablaPlanes .inputDescuento {
            width: 80px !important; /* o prueba 100px si quieres más ancho */
            text-align: center;
            margin: 0 auto;
        }
    </style>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#negociarconvenio");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#corporativo");
            element2.classList.remove("collapse");
        }
    </script>

    <style>
        .table-scroll-x {
            width: 100%;
            overflow-x: auto;
            white-space: nowrap;
        }
    </style>

</head>

<body onload="changeClass()">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <i class="fa fa-school-flag modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para visualizar Estrategias Marketing</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar las estrategias de markeing de manera clara y eficiente.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Crea una nueva estrategia</b><br />
                        Usa el formulario que está a la <b>izquierda</b> para digitar la información necesaria de la estrategia.<br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i><b>Agregar:</b> Guarda la información y finaliza el registro.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i><b>Cancelar:</b> Si necesitas volver atrás sin guardar cambios.
                    <br />
                        <br />
                        <b>Paso 2: Visualiza las estrategias existentes</b><br />
                        Usa el buscador que está a la <b>derecha</b> para encontrar lo que buscas.<br />
                        <i class="fa-solid fa-magnifying-glass"></i>Filtra por 
                        <i class="fa-solid fa-school-flag" style="color: #0D6EFD;"></i><b>Sede</b>,
                        <i class="fa-solid fa-location-dot" style="color: #0D6EFD;"></i><b>Dirección</b> o
                        <i class="fa-solid fa-school-flag" style="color: #0D6EFD;"></i><b>Tipo de Sede</b>
                        <br />
                        <br />
                        <b>Paso 3: Gestiona las estrategias</b><br />
                        En la columna "Acciones" encontrarás estas opciones:<br />
                        <i class="fa fa-edit" style="color: #1AB394;"></i><b>Editar:</b> Modifica los datos necesarios.<br />
                        <i class="fa fa-trash" style="color: #DC3545;"></i><b>Eliminar:</b> Borra lo que creas innecesario.
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
                    <h2><i class="fa fa-handshake text-success m-r-sm"></i>Negociación</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Corporativo</li>
                        <li class="active"><strong>Negociar convenio</strong></li>
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
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                            <div class="col-lg-5">
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
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="form-group">
                                                            <label>Empresa:</label>
                                                            <asp:DropDownList ID="ddlEmpresas" CssClass="form-control input-sm required" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlEmpresas_SelectedIndexChanged">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>

                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="form-group">
                                                            <label>Prospecto:</label>
                                                            <asp:DropDownList CssClass="form-control input-sm required" ID="ddlProspectos" runat="server"
                                                                DataValueField="DocumentoContacto" DataTextField="NombreContacto" AppendDataBoundItems="false">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDocumento" runat="server"
                                                                ErrorMessage="* Campo requerido" ControlToValidate="ddlProspectos"
                                                                CssClass="text-danger font-bold" ValidationGroup="agregar"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="form-group">
                                                    <label>Descripción de la negociación / oferta y beneficios:</label>
                                                    <div id="editor" cssclass="form-control input-sm"></div>
                                                    <asp:HiddenField ID="hiddenEditor" runat="server" />
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <i class="fa-solid fa-calendar-days text-info"></i>
                                                            <label for="txbFechaIni" class="col-form-label">Fecha inicio:</label>
                                                            <input type="text" runat="server" id="txbFechaIni" class="form-control input-sm datepicker" />
                                                            <asp:RequiredFieldValidator ID="rfvFechaIni" runat="server" ControlToValidate="txbFechaIni"
                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <i class="fas fa-calendar-days text-info"></i>
                                                            <label for="txbFechaFin" class="col-form-label">Fecha final:</label>
                                                            <input type="text" runat="server" id="txbFechaFin" class="form-control input-sm datepicker" />
                                                            <asp:RequiredFieldValidator ID="rfvFechaFin" runat="server" ControlToValidate="txbFechaFin"
                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>



                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="ibox">
                                                            <div class="ibox-title">
                                                                <h5>Planes comerciales</h5>
                                                            </div>

                                                            <!-- AQUÍ VA LA TABLA -->
                                                            <div class="ibox-content">
                                                                <div class="table-responsive">
                                                                    <table class="table table-bordered text-center" id="tablaPlanes">
                                                                        <thead class="thead-light">
                                                                            <tr>
                                                                                <th>#</th>
                                                                                <th>Plan</th>
                                                                                <th>Valor ($)</th>
                                                                                <th>Aplicar</th>
                                                                                <th>Dscto.(%)</th>
                                                                                <th>Valor (%)</th>
                                                                            </tr>
                                                                        </thead>
                                                                        <tbody>
                                                                            <asp:Repeater ID="rpPlanesVigentes" runat="server" OnItemDataBound="rpPlanesVigentes_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td><%# Eval("idPlan") %></td>
                                                                                        <td><%# Eval("NombrePlan") %></td>
                                                                                        <td class="valor"><%# String.Format(new System.Globalization.CultureInfo("es-CO"), "{0:C0}", Eval("PrecioTotal")) %></td>
                                                                                        <td>
                                                                                            <input type="radio" class="rdDescuento" name="planSeleccionado" />
                                                                                        <td>
                                                                                            <input type="number" class="form-control inputDescuento" min="0" max="100" disabled value="0"></td>
                                                                                        <td class="valorConDescuento">—</td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>

                                                                        </tbody>
                                                                    </table>


                                                                </div>
                                                            </div>
                                                            <!-- FIN TABLA -->
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <a href="negociarconvenio" class="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md">Cancelar</a>
                                                    <asp:HiddenField ID="hfIdPlan" runat="server" />
                                                    <asp:HiddenField ID="hfDescuento" runat="server" />
                                                    <asp:HiddenField ID="hfValorNegociacion" runat="server" />
                                                    <asp:HiddenField ID="hfModo" runat="server" />
                                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar"
                                                        CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" ValidationGroup="agregar" OnClick="btnAgregar_Click"
                                                        OnClientClick="return validarYConfirmar();" />
                                                </div>
                                                <asp:HiddenField ID="hfAccion" runat="server" />
                                                <br />
                                                <br />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-7">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Negociación generada</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">

                                        <div class="row" style="font-size: 12px;" runat="server" id="divBotonesLista">
                                            <div class="col-lg-6 form-horizontal">
                                                <div class="form-group">
                                                    <div class="form-group" id="filter-form-container" style="margin-left: 28px;"></div>
                                                </div>
                                            </div>

                                            <div class="col-lg-6 form-horizontal">
                                                <asp:LinkButton ID="lbExportarExcel" runat="server" CausesValidation="false"
                                                    CssClass="btn btn-info pull-right dim m-l-md" Style="font-size: 12px;"
                                                    OnClick="lbExportarExcel_Click">
                                                    <i class="fa fa-file-excel m-r-xs"></i>EXCEL
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="table-scroll-x">
                                            <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                                data-filter-min="3" data-filter-placeholder="Buscar"
                                                data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}"
                                                data-paging-limit="10" data-filtering="true"
                                                data-filter-container="#filter-form-container" data-filter-delay="300"
                                                data-filter-dropdown-title="Buscar en:" data-filter-position="left"
                                                data-empty="Sin resultados">
                                                <thead>
                                                    <tr>
                                                        <th data-breakpoints="xs">id</th>
                                                        <th data-breakpoints="xs">Nit</th>
                                                        <th data-breakpoints="xs">Cliente</th>
                                                        <th data-breakpoints="xs">Plan</th>
                                                        <th data-breakpoints="xs">Fecha Ini</th>
                                                        <th data-breakpoints="xs">Fecha Fin</th>
                                                        <th data-breakpoints="xs">Valor</th>
                                                        <th data-breakpoints="xs">Dscto.</th>
                                                        <th data-breakpoints="xs">Usuario.</th>
                                                        <th data-breakpoints="xs">Creado.</th>
                                                        <th data-breakpoints="all" data-title="Info"></th>
                                                        <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rpNegociaciones" runat="server" OnItemDataBound="rpNegociaciones_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr class="feed-element">
                                                                <td><%# Eval("idNegociacion") %></td>
                                                                <td><%# Eval("DocumentoEmpresa") %></td>
                                                                <td><%# Eval("NombreContacto") %> <%# Eval("ApellidoContacto") %></td>
                                                                <td><%# Eval("NombrePlan") %></td>
                                                                <td><%# Convert.ToDateTime(Eval("FechaIni")).ToString("dd/MM/yyyy") %></td>
                                                                <td><%# Convert.ToDateTime(Eval("FechaFin")).ToString("dd/MM/yyyy") %></td>
                                                                <td><%# string.Format(new System.Globalization.CultureInfo("es-CO"), "{0:C0}", Eval("ValorNegociacion")) %></td>
                                                                <td><%# Eval("Descuento") %>%</td>
                                                                <td><%# Eval("NombreUsuario") %></td>
                                                                <td><%# Eval("FechaCreacion") %></td>
                                                                <td>
                                                                    <table class="table table-bordered table-striped">
                                                                        <tr>
                                                                            <th width="50%"><i class="fa fa-city m-r-xs"></i>Razón social</th>
                                                                            <th width="50%"><i class="fa fa-city m-r-xs"></i>Descripción</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><%# Eval("NombreEmpresa") %></td>
                                                                            <td><%# Eval("Descripcion") %></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="display: flex; flex-wrap: nowrap;">
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

                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Negociaciones en cartera</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">

                                        <div class="row" style="font-size: 12px;" runat="server" id="div1">
                                            <div class="col-lg-6 form-horizontal">
                                                <div class="form-group">
                                                    <div class="form-group" id="filter-form-container1" style="margin-left: 28px;"></div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="table-scroll-x">
                                            <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                                data-filter-min="3" data-filter-placeholder="Buscar"
                                                data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}"
                                                data-paging-limit="10" data-filtering="true"
                                                data-filter-container="#filter-form-container1" data-filter-delay="300"
                                                data-filter-dropdown-title="Buscar en:" data-filter-position="left"
                                                data-empty="Sin resultados">
                                                <thead>
                                                    <tr>
                                                        <th data-breakpoints="xs">id</th>
                                                        <th data-breakpoints="xs">Nit</th>
                                                        <th data-breakpoints="xs">Cliente</th>
                                                        <th data-breakpoints="xs">Plan</th>
                                                        <th data-breakpoints="xs">Fecha Ini</th>
                                                        <th data-breakpoints="xs">Fecha Fin</th>
                                                        <th data-breakpoints="xs">Valor</th>
                                                        <th data-breakpoints="xs">Dscto.</th>
                                                        <th data-breakpoints="xs">Usuario.</th>
                                                        <th data-breakpoints="xs">Creado.</th>
                                                        <th data-breakpoints="all" data-title="Info"></th>
                                                        <%--                                                        <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rpNegociacionesCerradas" runat="server" OnItemDataBound="rpNegociacionesCerradas_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr class="feed-element">
                                                                <td><%# Eval("idNegociacion") %></td>
                                                                <td><%# Eval("DocumentoEmpresa") %></td>
                                                                <td><%# Eval("NombreContacto") %> <%# Eval("ApellidoContacto") %></td>
                                                                <td><%# Eval("NombrePlan") %></td>
                                                                <td><%# Convert.ToDateTime(Eval("FechaIni")).ToString("dd/MM/yyyy") %></td>
                                                                <td><%# Convert.ToDateTime(Eval("FechaFin")).ToString("dd/MM/yyyy") %></td>
                                                                <td><%# string.Format(new System.Globalization.CultureInfo("es-CO"), "{0:C0}", Eval("ValorNegociacion")) %></td>
                                                                <td><%# Eval("Descuento") %>%</td>
                                                                <td><%# Eval("NombreUsuario") %></td>
                                                                <td><%# Eval("FechaCreacion") %></td>
                                                                <td>
                                                                    <table class="table table-bordered table-striped">
                                                                        <tr>
                                                                            <th width="50%"><i class="fa fa-city m-r-xs"></i>Razón social</th>
                                                                            <th width="50%"><i class="fa fa-city m-r-xs"></i>Descripción</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><%# Eval("NombreEmpresa") %></td>
                                                                            <td><%# Eval("Descripcion") %></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <%--                                          <td style="display: flex; flex-wrap: nowrap;">
                                                                    <a runat="server" id="btnDeshacer" href="#" class="btn btn-outline btn-danger pull-right m-r-xs"
                                                                        style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false"><i class="fa fa-trash"></i></a>
                                                                </td>--%>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--                      <script>
                            function inicializarDescuentos() {
                                // Formateo de moneda colombiana
                                function formatCOP(valor) {
                                    return new Intl.NumberFormat("es-CO", {
                                        style: "currency",
                                        currency: "COP",
                                        minimumFractionDigits: 0
                                    }).format(valor);
                                }

                                // Convierte "$120.000" → 120000
                                function parseCOP(texto) {
                                    return Number(texto.replace(/[^0-9,-]+/g, '').replace(',', '.')) || 0;
                                }

                                document.querySelectorAll("#tablaPlanes tbody tr").forEach(fila => {
                                    const chk = fila.querySelector(".rdDescuento");
                                    const inputDesc = fila.querySelector(".inputDescuento");
                                    const celdaValor = fila.querySelector(".valor");
                                    const celdaValorConDesc = fila.querySelector(".valorConDescuento");
                                    if (!chk || !inputDesc || !celdaValor || !celdaValorConDesc) return;

                                    const valorBase = parseCOP(celdaValor.textContent.trim());
                                    celdaValorConDesc.textContent = formatCOP(valorBase);

                                    // Inicia deshabilitado
                                    inputDesc.disabled = true;

                                    chk.addEventListener("change", function () {
                                        if (this.checked) {
                                            inputDesc.disabled = false;
                                            inputDesc.focus();
                                        } else {
                                            inputDesc.disabled = true;
                                            inputDesc.value = "";
                                            celdaValorConDesc.textContent = formatCOP(valorBase);
                                        }
                                    });

                                    inputDesc.addEventListener("input", function () {
                                        let porcentaje = parseFloat(this.value) || 0;
                                        if (porcentaje < 0) porcentaje = 0;
                                        if (porcentaje > 100) porcentaje = 100;
                                        const nuevoValor = valorBase - (valorBase * porcentaje / 100);
                                        celdaValorConDesc.textContent = formatCOP(nuevoValor);
                                    });
                                });
                            }

                            // Ejecutar al cargar completamente el DOM
                            document.addEventListener("DOMContentLoaded", inicializarDescuentos);

                            // Soporte para UpdatePanel (ScriptManager)
                            if (typeof Sys !== "undefined" && Sys.WebForms) {
                                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(inicializarDescuentos);
                            }
                        </script>

                        <script>
                            function obtenerSeleccionPlan() {

                                // Buscar el radio seleccionado
                                const radio = document.querySelector("input.rdDescuento:checked");
                                if (!radio) {
                                    alert("Debe seleccionar un plan.");
                                    return false; // Evita el postback
                                }

                                const fila = radio.closest("tr");

                                // Obtener idPlan (primera celda)
                                const idPlan = fila.children[0].textContent.trim();

                                // Obtener descuento del input
                                const descuentoInput = fila.querySelector(".inputDescuento");
                                const descuento = parseFloat(descuentoInput.value) || 0;

                                // Obtener valor final formateado
                                const celdaValorFinal = fila.querySelector(".valorConDescuento");
                                const valorFinalTexto = celdaValorFinal.textContent.trim();

                                // Convertir "$120.000" → 120000
                                const valorFinal = Number(valorFinalTexto.replace(/[^0-9,-]+/g, '').replace(',', '.')) || 0;

                                // Pasar valores al server (HiddenFields)
                                document.getElementById("<%= hfIdPlan.ClientID %>").value = idPlan;
                                document.getElementById("<%= hfDescuento.ClientID %>").value = descuento;
                                document.getElementById("<%= hfValorNegociacion.ClientID %>").value = valorFinal;

                                return true; // permite el postback
                            }
                        </script>
                        <script>
                            function mostrarPlanSoloLectura() {

                                var idPlan = document.getElementById('<%= hfIdPlan.ClientID %>').value;
                                var descuento = document.getElementById('<%= hfDescuento.ClientID %>').value;
                                var valor = document.getElementById('<%= hfValorNegociacion.ClientID %>').value;

                                if (!idPlan) return;

                                // 🔴 BUSCAR TODOS LOS TR DEL REPEATER
                                var filas = document.querySelectorAll(".rdDescuento");

                                filas.forEach(function (radio) {

                                    var fila = radio.closest("tr");
                                    var idPlanFila = fila.cells[0].innerText.trim();

                                    var inputDesc = fila.querySelector(".inputDescuento");
                                    var lblValor = fila.querySelector(".valorConDescuento");

                                    // 🔒 bloquear todo
                                    radio.disabled = true;
                                    if (inputDesc) inputDesc.disabled = true;

                                    // 🎯 plan correcto
                                    if (idPlanFila === idPlan) {

                                        radio.checked = true;

                                        if (inputDesc) {
                                            inputDesc.value = descuento;
                                        }

                                        if (lblValor) {
                                            lblValor.innerText =
                                                new Intl.NumberFormat('es-CO', {
                                                    style: 'currency',
                                                    currency: 'COP',
                                                    minimumFractionDigits: 0
                                                }).format(valor);
                                        }
                                    }
                                });
                            }
                        </script>--%>

                        <%--                    <script>
                        function inicializarPlanes() {

                            const modo = document.getElementById('<%= hfModo.ClientID %>').value;

                        const hfIdPlan = document.getElementById('<%= hfIdPlan.ClientID %>').value;
                        const hfDescuento = document.getElementById('<%= hfDescuento.ClientID %>').value;
                        const hfValor = document.getElementById('<%= hfValorNegociacion.ClientID %>').value;

                            function formatCOP(valor) {
                                return new Intl.NumberFormat("es-CO", {
                                    style: "currency",
                                    currency: "COP",
                                    minimumFractionDigits: 0
                                }).format(valor);
                            }

                            function parseCOP(texto) {
                                return Number(texto.replace(/[^0-9]+/g, '')) || 0;
                            }

                            document.querySelectorAll(".rdDescuento").forEach(radio => {

                                const fila = radio.closest("tr");
                                const idPlanFila = fila.cells[0].innerText.trim();

                                const inputDesc = fila.querySelector(".inputDescuento");
                                const celdaValor = fila.querySelector(".valor");
                                const celdaValorFinal = fila.querySelector(".valorConDescuento");

                                const valorBase = parseCOP(celdaValor.innerText);

                                // 🔹 valor inicial
                                celdaValorFinal.innerText = formatCOP(valorBase);

                                // 🔒 MODO SOLO LECTURA
                                if (modo === "lectura") {

                                    radio.disabled = true;
                                    inputDesc.disabled = true;

                                    if (idPlanFila === hfIdPlan) {
                                        radio.checked = true;
                                        inputDesc.value = hfDescuento;
                                        celdaValorFinal.innerText = formatCOP(hfValor);
                                    }

                                    return; // ❌ no eventos
                                }

                                // ✏️ MODO EDICIÓN
                                inputDesc.disabled = true;

                                radio.addEventListener("change", function () {
                                    document.querySelectorAll(".inputDescuento").forEach(i => {
                                        i.disabled = true;
                                        i.value = "";
                                    });

                                    document.querySelectorAll(".valorConDescuento").forEach(v => {
                                        v.innerText = formatCOP(valorBase);
                                    });

                                    inputDesc.disabled = false;
                                    inputDesc.focus();
                                });

                                inputDesc.addEventListener("input", function () {
                                    let p = parseFloat(this.value) || 0;
                                    if (p < 0) p = 0;
                                    if (p > 100) p = 100;

                                    const nuevoValor = valorBase - (valorBase * p / 100);
                                    celdaValorFinal.innerText = formatCOP(nuevoValor);
                                });
                            });
                        }

                        // DOM + UpdatePanel
                        Sys.Application.add_load(inicializarPlanes);
                        //document.addEventListener("DOMContentLoaded", inicializarPlanes);
                        if (typeof Sys !== "undefined" && Sys.WebForms) {
                            Sys.WebForms.PageRequestManager.getInstance()
                                .add_endRequest(inicializarPlanes);
                        }
                    </script>--%>

                        <script>
                            function inicializarPlanes() {

                                const modo = document.getElementById('<%= hfModo.ClientID %>').value;
                                const hfIdPlan = document.getElementById('<%= hfIdPlan.ClientID %>').value;
                                const hfDescuento = document.getElementById('<%= hfDescuento.ClientID %>').value;
                                const hfValor = document.getElementById('<%= hfValorNegociacion.ClientID %>').value;

                                function formatCOP(valor) {
                                    return new Intl.NumberFormat("es-CO", {
                                        style: "currency",
                                        currency: "COP",
                                        minimumFractionDigits: 0
                                    }).format(valor);
                                }

                                function parseCOP(texto) {
                                    return Number(texto.replace(/[^0-9]+/g, '')) || 0;
                                }

                                document.querySelectorAll(".rdDescuento").forEach(radio => {

                                    const fila = radio.closest("tr");
                                    const idPlanFila = fila.cells[0].innerText.trim();

                                    const inputDesc = fila.querySelector(".inputDescuento");
                                    const celdaValor = fila.querySelector(".valor");
                                    const celdaValorFinal = fila.querySelector(".valorConDescuento");

                                    const valorBase = parseCOP(celdaValor.innerText);
                                    celdaValorFinal.innerText = formatCOP(valorBase);

                                    if (idPlanFila === hfIdPlan) {
                                        radio.checked = true;
                                        inputDesc.value = hfDescuento;
                                        celdaValorFinal.innerText = formatCOP(hfValor);

                                        if (modo === "edicion") {
                                            inputDesc.disabled = false;
                                        }
                                    }

                                    //// 🔒 SOLO LECTURA
                                    //if (modo === "lectura") {
                                    //    radio.disabled = true;
                                    //    inputDesc.disabled = true;
                                    //    return;
                                    //}

                                    // edición
                                    inputDesc.disabled = true;

                                    radio.addEventListener("change", function () {
                                        document.querySelectorAll(".inputDescuento").forEach(i => {
                                            i.disabled = true;
                                            i.value = "";
                                        });

                                        inputDesc.disabled = false;
                                        inputDesc.focus();
                                    });

                                    inputDesc.addEventListener("input", function () {
                                        let p = parseFloat(this.value) || 0;
                                        if (p > 100) p = 100;
                                        const nuevoValor = valorBase - (valorBase * p / 100);
                                        celdaValorFinal.innerText = formatCOP(nuevoValor);
                                    });
                                });
                            }

                            // ✅ EL ÚNICO LUGAR CORRECTO EN WEBFORMS
                            Sys.Application.add_load(inicializarPlanes);
                        </script>

                        <script>
                            function obtenerSeleccionPlan() {

                                const radio = document.querySelector(".rdDescuento:checked");
                                if (!radio) {
                                    alert("Debe seleccionar un plan.");
                                    return false;
                                }

                                const fila = radio.closest("tr");
                                const idPlan = fila.cells[0].innerText.trim();

                                const descuentoInput = fila.querySelector(".inputDescuento");
                                const descuento = parseFloat(descuentoInput.value) || 0;

                                const celdaValorFinal = fila.querySelector(".valorConDescuento");
                                const valorFinal = Number(
                                    celdaValorFinal.innerText.replace(/[^0-9]/g, '')
                                );

                                document.getElementById("<%= hfIdPlan.ClientID %>").value = idPlan;
                                document.getElementById("<%= hfDescuento.ClientID %>").value = descuento;
                                document.getElementById("<%= hfValorNegociacion.ClientID %>").value = valorFinal;

                                return true;
                            }
                        </script>
                        <script>
                            function confirmarEliminacion() {

                                Swal.fire({
                                    title: '¿Eliminar negociación?',
                                    text: 'Esta acción no se puede deshacer.',
                                    icon: 'warning',
                                    showCancelButton: true,
                                    confirmButtonText: 'Sí, eliminar',
                                    cancelButtonText: 'Cancelar',
                                    confirmButtonColor: '#d33'
                                }).then((result) => {
                                    if (result.isConfirmed) {

                                        // Redirige pasando el deleteid
                                        window.location.href = 'negociarconvenio.aspx?deleteid=<%= Request.QueryString["editid"] %>';
                                    }
                                });

                                return false; // 
                            }
                        </script>
                        <script>
                            function validarYConfirmar() {

                                const accion = document.getElementById('<%= hfAccion.ClientID %>').value;

                                // ✔ Guardar editor siempre
                                guardarContenidoEditor();

                                // ✔ Validar plan siempre
                                if (!obtenerSeleccionPlan()) {
                                    return false;
                                }

                                // 🟢 SI NO ES ELIMINAR → POSTBACK NORMAL
                                if (accion !== "eliminar") {
                                    return true;
                                }

                                // 🔴 SOLO SI ES ELIMINAR
                                Swal.fire({
                                    title: '¿Desea eliminar esta negociación?',
                                    text: 'Esta acción no se puede deshacer.',
                                    icon: 'warning',
                                    showCancelButton: true,
                                    confirmButtonText: 'Sí, eliminar',
                                    cancelButtonText: 'Cancelar',
                                    confirmButtonColor: '#d33'
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        __doPostBack('<%= btnAgregar.UniqueID %>', '');
                                }
                            });

                                return false; // ⛔ detener postback automático
                            }
                        </script>




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

    <script type="text/javascript">
        function setEditorContent(texto) {
            document.getElementById("editor").innerHTML = texto || "";
        }
    </script>




</body>

</html>

