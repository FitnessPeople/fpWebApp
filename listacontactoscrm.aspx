<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="listacontactoscrm.aspx.cs" Inherits="fpWebApp.listacontactoscrm" EnableEventValidation="false" Culture="es-ES" UICulture="es-ES" ValidateRequest="false" %>

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

    <title>Fitness People | CRM Contactos / Empresas</title>

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

    <%--        Validación postback modal--%>
    <script>
        // Función para reabrir el modal si fue cerrado por un PostBack
        function reopenModal() {
            setTimeout(function () {
                $('#ModalContacto').modal('show');
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
            var element1 = document.querySelector("#listacontactoscrm");
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

            //const boton = document.getElementById('<%= btnAgregar.ClientID %>');

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

    <%--        Formatear telefono --%>
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

    <%--        Formatear solo letraas --%>
    <script>
        function validarSoloLetras(input) {
            // Eliminar cualquier caracter que no sea letra o espacio
            input.value = input.value.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
        }
    </script>

    <%--        Formatear solo correo --%>
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

    <!--        SweetAlert2 CDN -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <%--         Filtro de buscar--%>
    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $('#buscador').on('keyup', function () {
                var valorBusqueda = $(this).val().toLowerCase();
                $('.table tbody tr').filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(valorBusqueda) > -1);
                });
            });
        });
    </script>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</head>

<body onload="changeClass()">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <i class="fa fa-person-chalkboard modal-icon"></i>
                    <h4 class="modal-title">Guía para editar un especialista</h4>
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
                    <h2><i class="fa fa-user-tie text-success m-r-sm"></i>CRM Lista</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li class="active"><strong>CRM / Lista contactos CRM</strong></li>
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

                    <form id="form" runat="server">
                        <asp:ScriptManager ID="ScriptManager1" runat="server" />

                        <%-- Modal--%>
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
                                                                oninput="validarSoloLetras(this)" />
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
                                                                spellcheck="false" placeholder="ej: cliente@ejemplo.com" autocomplete="off"
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
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
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
                                                <%--                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <i class="fa fa-dollar text-info"></i>
                                                            <label for="ValorPropuesta" class="col-form-label">Valor Propuesta:</label>
                                                            <asp:TextBox ID="txbValorPropuesta" CssClass="form-control input-sm" runat="server" placeholder="$0"
                                                                onkeyup="formatCurrency(this)" onblur="keepFormatted(this)" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <i class="fas fa-paperclip text-info"></i>
                                                            <label for="ArchivoPropuesta" class="col-form-label">Archivo Propuesta:</label>
                                                            <input type="file" runat="server" class="form-control" id="ArchivoPropuesta" placeholder="subir archivo">
                                                        </div>
                                                    </div>
                                                </div>--%>
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
                                                    onclick="window.location.reload();">
                                                    Cerrar</button>
                                                <asp:Button ID="btnAgregar" runat="server" OnClick="btnAgregar_Click"
                                                    Text="Agregar" CssClass="btn btn-primary mb-3" Visible="false"
                                                    ValidationGroup="agregar" />
                                                <asp:Button ID="btnActualizar" runat="server" OnClick="btnActualizar_Click"
                                                    Text="Actualizar" Visible="true"
                                                    class="btn btn-primary mb-3" />



                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%-- Termina Modal --%>

                        <div class="row">
                            <div class="col-sm-8">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <span class="text-muted small pull-right">Fitness People: <i class="fa fa-clock-o"></i>
                                            <asp:Literal ID="ltFechaHoy" runat="server"></asp:Literal></span>
                                        <h2>Contactos CRM Disponibles</h2>
                                        <p>
                                            Visualiza los contactos abiertos para gestión. Solo podrás continuar una vez verificada la información del cliente.
                                        </p>

                                        <div style="display: flex; align-items: center;">
                                            <div class="input-group">
                                                <input type="text" id="buscador" placeholder="Buscar" class="input form-control">
                                                <span class="input-group-btn">
                                                    <button type="button" class="btn btn-primary">
                                                        <i class="fa fa-search"></i>Search
                                                    </button>
                                                </span>
                                            </div>
                                            <%-- <button type="button" class="btn btn-success m-l-md"
                                                data-toggle="modal" data-target="#ModalContacto" data-whatever="@fat">
                                                <i class="fa fa-plus"></i> Nuevo
                                            </button>--%>
                                        </div>
                                        <div class="clients-list">
                                            <ul class="nav nav-tabs">
                                                <span class="pull-right small text-muted">
                                                    <p id="contadorFilas"></p>
                                                </span>
                                                <li class="active"><a data-toggle="tab" href="#tab-1"><i class="fa fa-user"></i>Contactos</a></li>
                                                <li class=""><a data-toggle="tab" href="#tab-2"><i class="fa fa-briefcase"></i>Empresas</a></li>
                                            </ul>
                                            <div class="tab-content">
                                                <%--Pestaña 1 - Contactos--%>
                                                <div id="tab-1" class="tab-pane active">
                                                    <div class="full-height-scroll">
                                                        <div class="table-responsive">
                                                            <table id="tablaContactos" class="table table-striped table-hover">
                                                                <tbody>
                                                                    <asp:Repeater ID="rpContactosCRM" runat="server" OnItemDataBound="rpContactosCRM_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <tr class="feed-element">
                                                                                <td class="client-avatar">
                                                                                    <img alt="image" src='<%# Eval("Foto") %>'>
                                                                                </td>
                                                                                <td>
                                                                                    <a href='listacontactoscrm.aspx?idContacto=<%# Eval("IdContacto") %>' class="client-link">
                                                                                        <%# Eval("NombreContacto") %> <%# Eval("ApellidoContacto") %>
                                                                                    </a>
                                                                                </td>
                                                                                <td><%# GetTelefonoHTML(Eval("TelefonoContacto")) %></a></td>
                                                                                <td>
                                                                                    <%# (Eval("Edad") == DBNull.Value || Convert.ToInt32(Eval("Edad")) == 0) 
                                                                                      ? "N/D" 
                                                                                      : Eval("Edad") + " años" %>
                                                                                </td>
                                                                                <td class="contact-type"><%# Eval("NombreEstadoVenta") %></td>
                                                                                <td><%# Eval("NombrePlan") %> </td>
                                                                                <td><span class='badge badge-<%# Eval("ColorEstadoCRM")%>'>
                                                                                    <%# Eval("NombreEstadoCRM") %></span>
                                                                                </td>
                                                                                <%--<td><%# Eval("ValorPropuesta", "{0:C0}") %></td>--%>
                                                                                <td><%# Eval("NombreCanalVenta", "{0:C0}") %></td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>

                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>

                                                <%--Pestaña 2 - Empresas--%>
                                                <div id="tab-2" class="tab-pane">
                                                    <div class="full-height-scroll">
                                                        <div class="table-responsive">
                                                            <table class="table table-striped table-hover">

                                                                <asp:Repeater ID="rpEmpresaCRM" runat="server">
                                                                    <ItemTemplate>
                                                                        <tbody>
                                                                            <tr>
                                                                                <td><a href='listacontactoscrm.aspx?empresaId=<%# Eval("IdEmpresaCRM") %>#tab-2' class="client-link">
                                                                                    <%# Eval("NombreEmpresaCRM") %>
                                                                                </a></td>
                                                                                <td><%# Eval("NombreContacto") %></td>
                                                                                <td><i class="fa fa-flag"></i><%# Eval("NombreCiudad") %></td>
                                                                                <td class="client-status"><span class="label label-primary"><%# Eval("ObservacionesEmp") %></span></td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>

                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="ibox ">
                                    <div class="ibox-content">
                                        <div class="tab-content">
                                            <%-- Detalle contacto--%>
                                            <div class="tab-content">
                                                <asp:Panel ID="pnlContacto" runat="server" Visible="false">
                                                    <asp:Repeater ID="rptContenido" runat="server">
                                                        <ItemTemplate>
                                                            <div id='<%# Eval("IdContacto") %>' class='tab-pane <%# Eval("IdContacto").ToString() == Session["contactoId"]?.ToString() ? "active" : "" %>'>
                                                                <div class="row m-b-lg">
                                                                    <div class="col-lg-4 text-center">
                                                                        <h2><%# Eval("NombreContacto") %> </h2>
                                                                        <div class="m-b-sm">
                                                                            <img alt="image" class="img-circle" src='<%# Eval("Foto") %>'
                                                                                style="width: 92px">
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-lg-8">
                                                                        <strong>Acerca de mí</strong><br />
                                                                        <p>
                                                                            Edad: <%# Convert.ToInt32(Eval("Edad")) > 0 ? Eval("Edad") : "No disponible" %> - Género: <%# Eval("Genero") ?? "No especificado" %>
                                                                        </p>
                                                                        <p class="contact-type" style="display: inline-flex; align-items: center; margin-bottom: 10px;">
                                                                            <i class="fa fa-envelope" style="margin-right: 5px;"></i>
                                                                            <span><%# Eval("EmailContacto") %></span>
                                                                        </p>
                                                                        <p><%# Eval("NombreTipoAfiliado") %></p>
                                                                        <p>Mi objetivo es  <%# Eval("Objetivo") %></p>

                                                                        <div class="d-flex justify-content-end mt-2">

                                                                            <%--                                                                            <button type="button" class="btn btn-primary" data-toggle="tooltip" data-placement="top" title="Agregar información" onclick="confirmarGestion()">
                                                                                <i class="fa fa-edit"></i>Gestionar contacto
                                                                            </button>--%>

                                                                            <asp:LinkButton ID="btnGestionarContacto" runat="server" CssClass="btn btn-primary" CommandName="Gestionar" CommandArgument='<%# Eval("idContacto") %>'
                                                                                OnCommand="btnGestionarContacto_Command" data-toggle="tooltip" data-placement="top" title="Agregar información" OnClientClick="return confirmarGestion();">
                                                                                <i class="fa fa-edit"></i> Gestionar contacto
                                                                            </asp:LinkButton>


                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <!-- Contenido de detalle del contacto -->
                                                                <div class="client-detail">
                                                                    <div class="full-height-scroll">
                                                                        <ul class="list-group clear-list" runat="server"
                                                                            visible='<%# (Eval("NombreEstadoCRM") != null && Eval("idEstadoCRM").ToString() != "3" && Eval("idEstadoCRM").ToString() != "4") %>'>
                                                                            <li class="list-group-item fist-item">
                                                                                <div style="display: flex; flex-direction: column; gap: 5px;">
                                                                                    <div style="display: flex; align-items: center; flex-wrap: wrap;">
                                                                                        <i class="fa fa-phone" style="margin-right: 5px; color: green;"></i>
                                                                                        <strong>Por favor, contáctame al:</strong>
                                                                                        <span style="margin-left: 5px;"><%# GetTelefonoHTML(Eval("TelefonoContacto")) %></span>
                                                                                    </div>
                                                                                    <div style="margin-left: 20px;">
                                                                                        <%# Eval("FechaProximoCon", "{0:dddd dd MMM yyyy hh:mm tt}") %>
                                                                                    </div>
                                                                                </div>
                                                                            </li>
                                                                        </ul>
                                                                        <hr />
                                                                        <strong>Notas del cliente</strong>
                                                                        <p>
                                                                            Contacto creado el <%# ((DateTime)Eval("FechaCreacion")).ToString("dddd d 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-ES")) %><hr />
                                                                            Preferencias Entreno por las noches, fines de semana y he completado N asistencias.
                                                                        </p>

                                                                        <%-- <strong>Medio de pago sugerido</strong>
                                                                    <p>
                                                                        Efectivo
                                                                        Tarjeta.
                                                                        Pagos en linea (Wompi)
                                                                        Transferencia
                                                                    </p>--%>
                                                                        <hr />
                                                                        <strong>Historial de gestión del asesor</strong>
                                                                        <%# Eval("historialHTML2") %>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </asp:Panel>
                                            </div>

                                            <%--Detalle empresa--%>
                                            <asp:Panel ID="pnlEmpresa" runat="server" Visible="false">
                                                <asp:Repeater ID="rpContenidoEmpresaCRM" runat="server">
                                                    <ItemTemplate>
                                                        <div id='<%# Eval("IdEmpresaCRM") %>' class='tab-pane <%# Eval("IdEmpresaCRM").ToString() == Session["empresaId"]?.ToString() ? "active" : "" %>'>
                                                            <div class="m-b-lg">
                                                                <h2><%# Eval("NombreEmpresaCRM") %></h2>
                                                                <%# FormatearUbicacion(Eval("NombreCiudad"), Eval("NombreEstado")) %>
                                                                <p>
                                                                    <%# GetEnlaceWeb(Eval("ObservacionesEmp")) %>
                                                                </p>
                                                                <div>
                                                                    <small>Active project completion with: 48%</small>
                                                                    <div class="progress progress-mini">
                                                                        <div style="width: 48%;" class="progress-bar"></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="client-detail">
                                                                <div class="full-height-scroll">

                                                                    <strong>Contacto</strong>

                                                                    <ul class="list-group clear-list">
                                                                        <li class="list-group-item fist-item">
                                                                            <span class="pull-right"><span class="label label-primary">NEW</span> </span>
                                                                            <%# Eval("NombreContacto") %> 
                                                                        </li>
                                                                        <li class="list-group-item">
                                                                            <span class="pull-right"><span class="label label-warning">WAITING</span></span>
                                                                            <%# Eval("CelularEmpresa") %>
                                                                        </li>
                                                                        <%--                                                                        <li class="list-group-item">
                                                                            <span class="pull-right"><span class="label label-success">ACCEPTED</span> </span>
                                                                            Valor Propuesta:<%# FormatearCOP(Eval("ValorPropuesta")) %>
                                                                        </li>--%>
                                                                        <li class="list-group-item">
                                                                            <span class="pull-right"><span class="label label-danger">BLOCKED</span> </span>
                                                                            Asesor: <%# Eval("NombreUsuario") %>
                                                                        </li>
                                                                    </ul>
                                                                    <hr />
                                                                    <strong>Observaciones</strong>
                                                                    <div id="vertical-timeline" class="vertical-container dark-timeline">
                                                                        <div class="vertical-timeline-block">
                                                                            <div class="vertical-timeline-icon gray-bg">
                                                                                <i class="fa fa-coffee"></i>
                                                                            </div>
                                                                            <div class="vertical-timeline-content">
                                                                                <p>
                                                                                    <%# Eval("ObservacionesEmp") %>
                                                                                </p>
                                                                                <span class="vertical-date small text-muted"><%# Eval("FechaCreacion") %> </span>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>

                    <%--Fin Contenido!!!!--%>
                </div>
            </div>

            <%--         <uc1:footer runat="server" ID="footer" />--%>
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

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Validación de campos de entrada -->
    <script>

        $("#form").validate({
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
    <script type="text/javascript">
        $(document).ready(function () {
            if (window.location.href.indexOf("empresaId") > -1) {
                // Activa la pestaña 2 manualmente
                $('#tab-2-tab').click(); // Reemplaza esto con el ID real del botón o tab que activa "Empresas"
            }
        });
    </script>

    <%--Contador de filas--%>
    <script type="text/javascript">
        function actualizarContador() {
            var visibles = $('#tablaContactos tbody tr:visible').length;
            $('#contadorFilas').text(visibles + ' contactos');
        }

        Sys.Application.add_load(function () {
            // Evento al escribir en el buscador
            $('#buscador').off('keyup').on('keyup', function () {
                var valorBusqueda = $(this).val().toLowerCase();

                $('#tablaContactos tbody tr').each(function () {
                    var textoFila = $(this).text().toLowerCase();
                    var coincide = textoFila.indexOf(valorBusqueda) > -1;
                    $(this).toggle(coincide);
                });

                actualizarContador();
            });

            // Actualiza el contador al cargar
            actualizarContador();
        });
    </script>

    <script type="text/javascript">
        Sys.Application.add_load(function () {
            console.log("Script cargado");

            $('#buscador').on('keyup', function () {
                var valorBusqueda = $(this).val().toLowerCase();
                console.log("Buscando: " + valorBusqueda); // Esto debería aparecer en consola

                var filas = $('.table tbody tr');
                console.log("Cantidad de filas: " + filas.length); // ¿Cuántas encuentra?

                filas.filter(function () {
                    var texto = $(this).text().toLowerCase();
                    console.log("Fila: " + texto); // Verifica que tenga contenido
                    $(this).toggle(texto.indexOf(valorBusqueda) > -1);
                });
            });
        });
    </script>
    <script type="text/javascript">
        function confirmarGestion() {
            return confirm("¿Está seguro de gestionar el contacto seleccionado?");
        }

        // Inicializa tooltips (si usas Bootstrap)
        $(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>



</body>

</html>

