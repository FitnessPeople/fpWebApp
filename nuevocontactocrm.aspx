<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nuevocontactocrm.aspx.cs" Inherits="fpWebApp.crmvista" %>

<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>
<%@ Register Src="~/controles/indicadoresusucmr.ascx" TagPrefix="uc2" TagName="indicadoresusucmr" %>


<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | CRM Contactos</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />--%>
    <link href="css/plugins/footable/footable.bootstrap.css" rel="stylesheet" />

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

    <!-- CSS de Quill -->
    <link href="https://cdn.quilljs.com/1.3.6/quill.snow.css" rel="stylesheet">
    <!-- JS de Quill -->
    <script src="https://cdn.quilljs.com/1.3.6/quill.min.js"></script>

    <%--    formato de los status--%>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var ddl = document.getElementById("<%= ddlStatusLead.ClientID %>");
            var colors = {
                "Primer contacto": "blue",
                "Propuesta enviada": "green",
                "Negociación propuesta": "orange",
                "Negociación aceptada": "lightgreen",
                "Negociación rechazada": "red"
            };

            ddl.addEventListener("change", function () {
                var selectedText = ddl.options[ddl.selectedIndex].text;
                ddl.style.backgroundColor = colors[selectedText] || "white";
            });
        });
    </script>

    <%--    formato de moneda--%>

    <script>
        function formatCurrency(input) {
            let value = input.value.replace(/\D/g, '');
            if (value === "") {
                input.value = "";
                return;
            }
            let formattedValue = new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP', minimumFractionDigits: 0 }).format(value);
            input.value = formattedValue;
        }
        function keepFormatted(input) {
            if (input.value.trim() === "") {
                input.value = "";
                return;
            }
            formatCurrency(input);
        }
        function getNumericValue(input) {
            return input.value.replace(/[^0-9]/g, '');
        }
    </script>

    <%--    formato de posición en el menú--%>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#nuevocontactocrm");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#crm");
            element2.classList.remove("collapse");
        }
    </script>

    <%--    formato de las observaciones--%>

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

    <%--    formato de fecha--%>

    <script>
        $(document).ready(function () {
            $('.datepicker').datepicker({
                format: 'yyyy-mm-dd',
                autoclose: true,
                todayHighlight: true
            });
        });
    </script>


</head>

<body onload="changeClass()">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <i class="fa fa-person-chalkboard modal-icon"></i>
                    <h4 class="modal-title">Guía para ver Historias Clínicas</h4>
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
                    <h2><i class="fa fa-notes-medical text-success m-r-sm"></i>CRM Contactos</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>CMR</li>
                        <li class="active"><strong>Contactos</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">
                    <%--Inicio Contenido!!!!--%>

                    <uc2:indicadoresusucmr runat="server" ID="indicadoresusucmr"/>

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
                            <h5>Contactos</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>

                        <div class="ibox-content">
                            <div class="row">
                                <form id="form1" runat="server">
                                    <div class="col-lg-6 form-horizontal">
                                        <div class="form-group">
                                            <div class="form-group" id="filter-form-container" style="margin-left: 28px;"></div>
                                        </div>
                                    </div>

                                    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                        <div class="modal-dialog" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title" id="exampleModalLabel">Registrar contacto</h5>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
                                                </div>
                                                <div class="modal-body">
                                                    <form>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <i class="fa fa-user-tie text-info"></i>
                                                                    <label for="recipient-name" class="col-form-label">Nombre completo:</label>
                                                                    <input type="text" class="form-control" id="recipient-name">
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-sm-6">
                                                                    <i class="fa-solid fa-phone text-info"></i>
                                                                    <label for="message-text" class="col-form-label">Teléfono:</label>
                                                                    <input type="text" class="form-control" id="telefono-text">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <span class="glyphicon glyphicon-envelope text-info"></span>
                                                                    <label for="message-text" class="col-form-label">Correo electrónico:</label>
                                                                    <input type="text" class="form-control" id="correo-text">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <i class="fas fa-industry text-info"></i>
                                                                    <label for="message-text" class="col-form-label">Empresa:</label>
                                                                    <asp:DropDownList ID="ddlEmpresa" runat="server"
                                                                        AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                        <asp:ListItem Text="Empresa 1" Value="Empresa 1"></asp:ListItem>
                                                                        <asp:ListItem Text="Empresa 2" Value="Empresa 2"></asp:ListItem>
                                                                        <asp:ListItem Text="Empresa 3" Value="Empresa 3"></asp:ListItem>
                                                                        <asp:ListItem Text="Empresa 4" Value="Empresa 4"></asp:ListItem>
                                                                        <asp:ListItem Text="Empresa 5" Value="Empresa 5"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <i class="fas fa-flag text-info"></i>
                                                            <label for="message-text" class="col-form-label">Status Lead:</label>
                                                            <asp:DropDownList ID="ddlStatusLead" runat="server"
                                                                AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="Primer contacto" Value="Primer contacto" Class="text-dark"></asp:ListItem>
                                                                <asp:ListItem Text="Propuesta enviada" Value="Propuesta enviada" Class="text-success"></asp:ListItem>
                                                                <asp:ListItem Text="Negociación propuesta" Value="Negociación propuesta" Class="text-warning"></asp:ListItem>
                                                                <asp:ListItem Text="Negociación aceptada" Value="Negociación aceptada" Class="text-primary"></asp:ListItem>
                                                                <asp:ListItem Text="Negociación rechazada" Value="Negociación rechazada" Class="text-danger"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <i class="fa-solid fa-hand-point-up text-info"></i>
                                                                    <label for="message-text" class="col-form-label">Primer contacto:</label>
                                                                    <input type="text" runat="server" id="txbFechaPrim" class="form-control input-sm datepicker" />
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <i class="fas fa-angle-right text-info"></i>
                                                                    <label for="message-text" class="col-form-label">Próximo contacto:</label>
                                                                    <input type="text" runat="server" id="txbFechaProx" class="form-control input-sm datepicker" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <i class="fa fa-dollar text-info"></i>
                                                                    <label for="message-text" class="col-form-label">Valor Propuesta:</label>
                                                                    <asp:TextBox ID="txbValorPropuesta" CssClass="form-control input-sm" runat="server" placeholder="Valor"
                                                                        onkeyup="formatCurrency(this)" onblur="keepFormatted(this)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <i class="fas fa-paperclip text-info"></i>
                                                                    <label for="message-text" class="col-form-label">Archivo Propuesta:</label>
                                                                    <input type="file" class="form-control" id="archivo-text" placeholder="subir archivo">
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <i class="fas fa-pen text-info"></i>
                                                            <label for="message-text" class="col-form-label">Observaciones:</label>
                                                            <div id="editor" cssclass="form-control input-sm"></div>
                                                            <asp:HiddenField ID="hiddenEditor" runat="server" />
                                                        </div>
                                                    </form>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                                    <button type="submit" class="btn btn-primary mb-3">Guardar</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 form-horizontal">
                                        <button type="button" class="btn btn-success pull-right dim m-l-md" data-toggle="modal" data-target="#exampleModal" data-whatever="@fat">Nuevo</button>
                                    </div>
                                </form>
                            </div>

                            <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                data-filter-min="3" data-filter-placeholder="Buscar"
                                data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}"
                                data-paging-limit="10" data-filtering="true"
                                data-filter-container="#filter-form-container" data-filter-delay="300"
                                data-filter-dropdown-title="Buscar en:" data-filter-position="left"
                                data-empty="Sin resultados">
                                <thead>
                                    <tr>
                                        <th data-breakpoints="xs" width="15%"><i class="fa fa-user-tie text-info"></i> Nombre </th>
                                        <th data-breakpoints="xs"><i class="fa-solid fa-phone text-info"></i> Teléfono</th>
                                        <th data-breakpoints="xs"><span class="glyphicon glyphicon-envelope text-info"></span> Correo</th>
                                        <th data-breakpoints="xs"><i class="fas fa-industry text-info"></i> Organización / Empresa</th>
                                        <th data-breakpoints="xs"><i class="fas fa-flag text-info"></i> Staus lead</th>
                                        <th data-breakpoints="xs"><i class="fa-solid fa-hand-point-up text-info"></i> Primer contacto</th>
                                        <th data-breakpoints="xs"><i class="fas fa-angle-right text-success"></i> Próximo contacto</th>
                                        <th data-breakpoints="xs"><i class="fa fa-dollar text-warning"></i> Valor propuesta</th>
                                        <th data-breakpoints="all" data-title="Info"></th>
                                        <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rpContactosCMR" runat="server" OnItemDataBound="rpContactosCMR_ItemDataBound">
                                       <ItemTemplate>
                                            <tr class="feed-element">                                              
                                                <td><%# Eval("NombreContacto") %></td>
                                                <td><a href="https://wa.me/57<%# Eval("TelefonoContacto") %>" target="_blank"><i class="fab fa-whatsapp m-r-xs font-bold"></i> <%# Eval("TelefonoContacto") %></a></td>
                                                <td><%# Eval("EmailContacto") %> </td>
                                                <td><%# Eval("idEmpresa") %> </td>
                                                <td><%# Eval("idstatusLead") %> </td>
                                                <td><%# Eval("FechaPrimerCon", "{0:yyyy-MM-dd}") %></td>
                                                <td><%# Eval("FechaProximoCon", "{0:yyyy-MM-dd}") %></td>
                                                <td><%# Eval("ValorPropuesta", "{0:C0}") %></td>
                                                <td>
                                                    <h3 class="text-info">Propuesta y observaciones</h3>
                                                    <table class="table table-bordered table-striped">
                                                        <tr>
                                                            <th width="20%"><i class="fas fa-paperclip text-primary"></i> Archivo Propuesta</th>
                                                            <th width="20%"><i class="fas fa-pen text-primary"></i> Observaciones</th>
                                                            <th width="20%"><i class="fa fa-user-tie text-primary"></i>Usuario</th>
                                                        </tr>
                                                        <tr>
                                                            <td><%# Eval("ArchivoPropuesta") %> </td>
                                                            <td><%# Eval("Observaciones") %> </td>
                                                            <td><%# Eval("idUsuario") %> </td>
                                                        </tr>
                                                    </table>

                                                </td>
                                                <td>
                                                    <button runat="server" id="btnAgregar" class="btn btn-outline btn-success pull-right"
                                                        style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false" title="Agregar control">
                                                        <i class="fa fa-notes-medical"></i>
                                                    </button>
                                                    <a runat="server" id="btnEditar" class="btn btn-outline btn-success pull-right"
                                                        style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false" >
                                                        <i class="fa fa-edit"></i>
                                                    </a>
                                                     <a runat="server" id="btnEliminar" class="btn btn-outline btn-danger pull-right"
                                                        style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false" >
                                                        <i class="fa fa-trash"></i>
                                                    </a>

<%--                                                    <button runat="server" id="btnEliminar" class="btn btn-outline btn-danger pull-right m-r-xs"
                                                        style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false" title="Eliminar contacto">
                                                        <i class="fa fa-trash"></i>
                                                    </button>--%>
<%--                                                    <button runat="server" id="btnEditar" class="btn btn-outline btn-primary pull-right m-r-xs"
                                                        style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false" title="Editar contacto">
                                                        <i class="fa fa-edit"></i>
                                                    </button>--%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <td colspan="6">
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
