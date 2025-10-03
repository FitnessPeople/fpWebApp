<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nuevocontactocrm.aspx.cs" Inherits="fpWebApp.nuevocontactocrm" ValidateRequest="false" %>

<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>
<%@ Register Src="~/controles/indicadoresusucrm.ascx" TagPrefix="uc2" TagName="indicadoresusucrm" %>

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
        textarea {
            border: 2px solid #17a2b8; /* Bootstrap info color */
        }
    </style>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <!-- Mainly scripts -->
    <%--    <script src="js/jquery-3.1.1.min.js"></script>--%>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- FooTable -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

    <!-- Peity -->
    <script src="js/plugins/peity/jquery.peity.min.js"></script>
    <script src="js/demo/peity-demo.js"></script>

    <!-- CSS de Quill -->
    <link href="https://cdn.quilljs.com/1.3.6/quill.snow.css" rel="stylesheet">
    <!-- JS de Quill -->
    <script src="https://cdn.quilljs.com/1.3.6/quill.min.js"></script>


      <%--        Validación postback modal--%>
    <script>
        // Función para reabrir el modal si fue cerrado por un PostBack
        function reopenModal() {
            setTimeout(function () {
                $('#miModal').modal('show');
            }, 100);
        }

        // Detectar cuándo se actualiza el UpdatePanel y reabrir el Modal
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            reopenModal();
        });
    </script>

    <%--        formato de moneda--%>
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

    <%--        formato de posición en el menú--%>
    <script>
        function changeClass() {
            var element1 = document.querySelector("#nuevocontactocrm");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#crm");
            element2.classList.remove("collapse");
        }
    </script>

    <%--        Validar botón Agregar --%>
    <script>
        function validarFormulario() {
            const nombre = document.getElementById('txbNombreContacto').value.trim();
            const telefono = document.getElementById('txbTelefonoContacto').value.trim();
            const correo = document.getElementById('txbCorreoContacto').value.trim();
            const fechaPrim = document.getElementById('txbFechaPrim').value.trim();
            const fechaProx = document.getElementById('txbFechaProx').value.trim();
            const valor = document.getElementById('txbValorPropuesta').value.trim();
            const status = document.getElementById('ddlStatusLead').value;

            const boton = document.getElementById('<%= btnAgregar.ClientID %>');

            const camposCompletos =
                nombre !== "" &&
                telefono !== "" &&
                correo !== "" &&
                fechaPrim !== "" &&
                fechaProx !== "" &&
                valor !== "" &&
                status !== "0";

            if (camposCompletos) {
                boton.disabled = false;
                boton.classList.remove('btn-secondary');
                boton.classList.add('btn-primary');
            } else {
                boton.disabled = true;
                boton.classList.remove('btn-primary');
                boton.classList.add('btn-secondary');
            }
        }

        // Asignar eventos a cada campo
        document.addEventListener("DOMContentLoaded", function () {
            const campos = [
                'txbNombreContacto', 'txbTelefonoContacto', 'txbCorreoContacto',
                'txbFechaPrim', 'txbFechaProx', 'txbValorPropuesta',
                , 'ddlStatusLead'
            ];

            campos.forEach(id => {
                const campo = document.getElementById(id);
                if (campo) {
                    campo.addEventListener('input', validarFormulario);
                    campo.addEventListener('change', validarFormulario);
                }
            });

            validarFormulario(); // Ejecutar al cargar
        });
    </script>

    <%--        Abrir modal--%>
    <script>
        function AbrirModal() {
            $("#ModalContacto").modal("show");
        }
    </script>

        <%--    Formatear telefono --%>
    <script>
        function formatearTelefono(input) {
            let num = input.value.replace(/\D/g, ''); // Eliminar caracteres no numéricos

            // Si el número tiene 10 dígitos, es un celular
            if (num.length === 10) {
                input.value = num.substring(0, 3) + '-' + num.substring(3, 6) + '-' + num.substring(6, 10);
            }
            // Si el número tiene 7 o más dígitos, es un teléfono fijo
            else if (num.length > 6) {
                input.value = '(' + num.substring(0, 3) + ') ' + num.substring(3, 6) + '-' + num.substring(6, 10);
            } else {
                input.value = num;
            }
        }
    </script>

        <%--    Formatear solo letraas --%>
    <script>
        function validarSoloLetras(input) {
            // Eliminar cualquier caracter que no sea letra o espacio
            input.value = input.value.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
        }
    </script>

        <%--    Formatear solo correo --%>
    <script>
        function validarCorreo(input) {
            const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
            if (!emailRegex.test(input.value)) {
                input.setCustomValidity('Por favor ingrese un correo electrónico válido.');
            } else {
                input.setCustomValidity('');
            }
        }
    </script>

        <!--    SweetAlert2 CDN -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>


</head>

<body onload="changeClass()">

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
                        <li>CRM</li>
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

                    <uc2:indicadoresusucrm runat="server" ID="indicadoresusucrm" />

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


                   <%-- Div contactoss--%>
                    <div class="ibox float-e-margins" runat="server" id="divContenido">
                        <div class="ibox-title">
                            <h5><b>Contactos</b></h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>

                        <div class="ibox-content">
                            <div class="row">
                                <form id="form1" runat="server">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server" />
                                    <div class="col-lg-6 form-horizontal">
                                        <div class="form-group">
                                            <div class="form-group" <%--id="filter-form-container"--%> style="margin-left: 28px;"></div>
                                             <h5><i class="fa-solid fa-sack-dollar"></i> Total propuestas: <asp:Literal ID="ltValorTotal" runat="server"></asp:Literal></h5>
                                            
                                        </div>
                                    </div> 

                                    <asp:UpdatePanel ID="upModal" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="modal fade" id="ModalContacto" tabindex="-1" role="dialog" aria-labelledby="ModalContactoLabel" aria-hidden="false">
                                                <div class="modal-dialog" role="document">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="ModalContactoLabel">Registrar contacto</h5>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">

                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <i class="fa fa-user-tie text-info"></i>
                                                                        <label for="nombreContacto" class="col-form-label">Nombre completo:</label>
                                                                        <input type="text" runat="server" class="form-control" id="txbNombreContacto" 
                                                                            placeholder="Nombre" spellcheck="false" autocomplete="off" 
                                                                            oninput="validarSoloLetras(this)"/>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-sm-6">
                                                                        <i class="fa-solid fa-phone text-info"></i>
                                                                        <label for="telefonoContacto" class="col-form-label">Teléfono:</label>
                                                                        <input type="text" runat="server" class="form-control" id="txbTelefonoContacto"
                                                                            placeholder="ej: 310 123 4567" spellcheck="false" autocomplete="off" 
                                                                            onkeyup="formatearTelefono(this)" maxlength="14">
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <span class="glyphicon glyphicon-envelope text-info"></span>
                                                                        <label for="correoContacto" class="col-form-label">Correo electrónico:</label>
                                                                        <input type="text" runat="server" class="form-control" id="txbCorreoContacto"
                                                                            spellcheck="false" placeholder="ej: cliente@ejemplo.com"  autocomplete="off"
                                                                            oninput="validarCorreo(this)">
                                                                        <asp:Literal ID="ltError" runat="server" Visible="false"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <i class="fas fa-industry text-info"></i>
                                                                        <label for="Empresa" class="col-form-label">Empresa / Persona:</label>
                                                                        <asp:DropDownList ID="ddlEmpresa" DataTextField="NombreEmpresaCRM" DataValueField="idEmpresaCRM" 
                                                                            runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                            <asp:ListItem Text="No aplica" Value="0"></asp:ListItem>                                                                          
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <i class="fas fa-flag text-info"></i>
                                                                <label for="StatusLead" class="col-form-label">Status Lead:</label>
                                                                        <asp:DropDownList ID="ddlStatusLead" DataTextField="NombreEstadoCRM" DataValueField="idEstadoCRM" 
                                                                            runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">                                                                                                                                                 
                                                                        </asp:DropDownList>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <i class="fa-solid fa-hand-point-up text-info"></i>
                                                                        <label for="txbFechaPrim" class="col-form-label">Primer contacto:</label>
                                                                        <input type="text" runat="server" id="txbFechaPrim" class="form-control input-sm datepicker" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <i class="fas fa-angle-right text-info"></i>
                                                                        <label for="txbFechaProx" class="col-form-label">Próximo contacto:</label>
                                                                        <input type="text" runat="server" id="txbFechaProx" class="form-control input-sm datepicker" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <i class="fa fa-dollar text-info"></i>
                                                                        <label for="ValorPropuesta" class="col-form-label">Valor Propuesta:</label>
                                                                        <asp:TextBox ID="txbValorPropuesta" CssClass="form-control input-sm" runat="server" placeholder="$0"
                                                                            onkeyup="formatCurrency(this)" onblur="keepFormatted(this)"  autocomplete="off"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <div class="form-group">
                                                                        <i class="fas fa-paperclip text-info"></i>
                                                                        <label for="ArchivoPropuesta" class="col-form-label">Archivo Propuesta:</label>
                                                                        <input type="file" runat="server" class="form-control" id="ArchivoPropuesta" placeholder="subir archivo" >
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <i class="fas fa-pen text-info"></i>
                                                                <label for="message-text" class="col-form-label">Contexto de la negociación:</label>
                                                                <textarea id="txaObservaciones" runat="server" rows="3"                                                                      
                                                                     cssclass="form-control input-sm" class="form-control">
                                                                </textarea>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <asp:Literal ID="ltMensajeVal" runat="server"></asp:Literal>
                                                            <button type="button" class="btn btn-secondary" data-dismiss="modal" 
                                                            onclick="window.location.reload();">Cerrar</button>
                                                            <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click" 
                                                                Text="Agregar" CssClass="btn btn-primary mb-3"                                                               
                                                                ValidationGroup="agregar"/>
                                                            <asp:Button ID="btnActualizar" runat="server" OnClick="btnActualizar_Click"
                                                                Text="Actualizar" Visible="false"
                                                                class="btn btn-primary mb-3"/> 
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                     <%-- Termina Modal --%>

                                     <%-- Modal eliminar--%>
                                     <asp:UpdatePanel ID="upEliminar" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="modal fade" id="Modaleliminar" tabindex="-1" role="dialog" aria-labelledby="ModalEliminarLabel" aria-hidden="true">
                                                <div class="modal-dialog" role="alert">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="ModaleliminarLabel">CRM Contactos</h5>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">
                                                                <div class="col-sm-">
                                                                    <div class="form-group">                                                                                                                                            
                                                                        <asp:Literal ID="ltEliminar" runat="server"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" class="btn btn-secondary" data-dismiss="modal" onclick="window.location.reload();">Cerrar</button>
                                                            <asp:Button ID="btnAccionEliminar" runat="server" OnClick="btnAccionEliminar_Click"
                                                                Text="Eliminar" Visible="true"
                                                                class="btn btn-danger mb-3"/> 
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                     <%-- Termina Modal eliminar--%>


                                    <div class="col-lg-6 form-horizontal">
                                        <button type="button" class="btn btn-success pull-right dim m-l-md" style="font-size: 12px;" data-toggle="modal" data-target="#ModalContacto" data-whatever="@fat">Nuevo</button>
                                    </div>

                                    <asp:UpdatePanel ID="upTabla" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                                data-filter-min="3" data-filter-placeholder="Buscar"
                                                data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}"
                                                data-paging-limit="10" data-filtering="true"
                                                data-filter-container="#filter-form-container" data-filter-delay="300"
                                                data-filter-dropdown-title="Buscar en:" data-filter-position="left"
                                                data-empty="Sin resultados">
                                                <thead>
                                                    <tr>
                                                        <th data-breakpoints="xs" width="15%"><i class="fa fa-user-tie text-info"></i>Nombre </th>
                                                        <th data-breakpoints="xs" width="15%"><i class="fa-solid fa-phone text-info"></i>Teléfono</th>
                                                        <th data-breakpoints="xs"><span class="glyphicon glyphicon-envelope text-info"></span>Correo</th>
                                                        <th data-breakpoints="xs"><i class="fas fa-industry text-info"></i>Organización / Empresa</th>
                                                        <th data-breakpoints="xs"><i class="fas fa-flag text-info"></i>Staus lead</th>
                                                        <th data-breakpoints="xs"><i class="fa-solid fa-hand-point-up text-info"></i>Primer contacto</th>
                                                        <th data-breakpoints="xs"><i class="fas fa-angle-right text-success"></i>Próximo contacto</th>
                                                        <th data-breakpoints="xs"><i class="fa fa-dollar text-warning"></i>Valor propuesta</th>
                                                        <th data-breakpoints="xs"><i class="fa fa-school-flag text-info"></i>Canal</th>
                                                        <th data-breakpoints="all" data-title="Info"></th>
                                                        <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rpContactosCRM" runat="server" OnItemDataBound="rpContactosCRM_ItemDataBound1">
                                                        <ItemTemplate>
                                                            <tr class="feed-element">
                                                                <td><%# Eval("NombreContacto")%></td>
                                                                <td><%# GetTelefonoHTML(Eval("TelefonoContacto")) %></a></td>
                                                                <td><%# Eval("EmailContacto") %> </td>
                                                                <td><%# Eval("NombreEmpresaCRM") %> </td>
                                                                <td><span class='badge badge-<%# Eval("ColorEstadoCRM")%>'>
                                                                    <%# Eval("NombreEstadoCRM") %></span>
                                                                </td>
                                                                <td><%# Eval("FechaPrimerCon", "{0:yyyy-MM-dd}") %></td>
                                                                <td><%# Eval("FechaProximoCon", "{0:yyyy-MM-dd}") %></td>
                                                                <td><%# Eval("ValorPropuesta", "{0:C0}") %></td>
                                                                <td><%# Eval("NombreCanalVenta", "{0:C0}") %></td>
                                                                <td>
                                                                    <h3 class="text-info">Propuesta y observaciones</h3>
                                                                    <table class="table table-bordered table-striped">
                                                                        <tr>                                                                            
                                                                            <th width="20%"><i class="fas fa-pen text-primary"></i>Observaciones</th>
                                                                            <th width="20%"><i class="fas fa-paperclip text-primary"></i>Archivo Propuesta</th>
                                                                            <th width="20%"><i class="fas fa-paperclip text-primary"></i>Histórico</th>
                                                                            <th width="20%"><i class="fa fa-user-tie text-primary"></i>Asesor</th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td><%# Eval("Observaciones") %> </td>
                                                                            <td><%# Eval("ArchivoPropuesta") %> </td>                                                                            
                                                                            <td> </td>                                                                            
                                                                            <td><%# Eval("NombreUsuario") %> </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnEditar" runat="server"
                                                                        CssClass="btn btn-outline btn-primary pull-left m-r-xs"
                                                                        CommandArgument='<%# Eval("idContacto") %>'
                                                                        OnClick="btnEditar_Click"
                                                                        Text="&#9998;"
                                                                        ToolTip="Editar contacto"
                                                                        Style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" />
                                                              
                                                                    <asp:Button ID="btnEliminar" runat="server"
                                                                        CssClass="btn btn-outline btn-danger pull-left m-r-xs"
                                                                        CommandArgument='<%# Eval("idContacto") %>'
                                                                        OnClick="btnEliminar_Click"
                                                                        Text="&#128465;"
                                                                        ToolTip="Eliminar contacto"
                                                                        Style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" />
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </form>
                            </div>
                        </div>
                        <%--Fin Contenido!!!!--%>
                    </div>
                </div>

                <uc1:footer runat="server" ID="footer" />

            </div>
            <uc1:rightsidebar runat="server" ID="rightsidebar" />
        </div>
    </div>

 <script>
            $(document).ready(function () {
                $("#btnEditar").on("click", function () {
                    $("#EditionModal").modal("show"); // Abre la modal al hacer clic en el botón
                });
            });
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

        <!-- Custom and plugin javascript -->
<script src="js/inspinia.js"></script>
<script src="js/plugins/pace/pace.min.js"></script>

<!-- jQuery UI -->
<%--   <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>--%>

 <!-- Jquery Validate -->
<script src="js/plugins/validate/jquery.validate.min.js"></script>

<!-- Page-Level Scripts -->
<script>
    $('.footable').footable();
</script>

<!-- Validación de campos de entrada --> 
<script>

    $("#form1").validate({
        rules: {
            txbNombreContacto: {
                required: true,
                minlength: 3
            },
            txbTelefonoContacto: {
                required: true,
                minlength: 10
            },
            txbCorreoContacto: {
                required: true,
                maxlength: 150
            },
            ddlEmpresa: {
                required: true
            },
            ddlStatusLead: {
                required: true
            },
            txbFechaPrim: {
                required: true
            },
            txbFechaProx: {
                required: true,
            },
            txbValorPropuesta: {
                required: true,
            },
            txaObservaciones: {
                required: true,
            },
        }
    });

    $('.chosen-select').chosen({ width: "100%", disable_search_threshold: 10, no_results_text: "Sin resultados" });
</script>

</body>

</html>
