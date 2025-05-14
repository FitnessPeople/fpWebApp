<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="crmnuevocontacto.aspx.cs" Inherits="fpWebApp.crmnuevocontacto" %>

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

    <title>Fitness People | Sedes</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

        <!-- Sweet Alert -->
    <link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">

    <!-- Sweet alert -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <!-- CSS de Quill -->
    <link href="https://cdn.quilljs.com/1.3.6/quill.snow.css" rel="stylesheet">
    <!-- JS de Quill -->
    <script src="https://cdn.quilljs.com/1.3.6/quill.min.js"></script>



    <%-- Editor de tecto--%>
<%--    <script>
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
    </script>--%>

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

    <%--        formato de posición en el menú--%>
    <script>
        function changeClass() {
            var element1 = document.querySelector("#crmnuevocontacto");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#crm");
            element2.classList.remove("collapse");
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

    <link href="css/plugins/footable/footable.bootstrap.css" rel="stylesheet" />

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
            var element1 = document.querySelector("#crmnuevocontacto");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#crm");
            element2.classList.remove("collapse");
            console.log(element2);
        }
    </script>

</head>

<body onload="changeClass()">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <i class="fa fa-school-flag modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para visualizar sedes</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar las sedes de manera clara y eficiente.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Crea una nueva sede</b><br />
                        Usa el formulario que está a la <b>izquierda</b> para digitar la información necesaria de la sede.<br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i><b>Agregar:</b> Guarda la información y finaliza el registro.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i><b>Cancelar:</b> Si necesitas volver atrás sin guardar cambios.
                    <br />
                        <br />
                        <b>Paso 2: Visualiza las sedes existentes</b><br />
                        Usa el buscador que está a la <b>derecha</b> para encontrar lo que buscas.<br />
                        <i class="fa-solid fa-magnifying-glass"></i>Filtra por 
                        <i class="fa-solid fa-school-flag" style="color: #0D6EFD;"></i><b>Sede</b>,
                        <i class="fa-solid fa-location-dot" style="color: #0D6EFD;"></i><b>Dirección</b> o
                        <i class="fa-solid fa-school-flag" style="color: #0D6EFD;"></i><b>Tipo de Sede</b>
                        <br />
                        <br />
                        <b>Paso 3: Gestiona las sedes</b><br />
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
                    <h2><i class="fa fa-school-flag text-success m-r-sm"></i>CRM</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Sistema</li>
                        <li class="active"><strong>Nuevo contacto</strong></li>
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
                            <div class="col-lg-4">
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
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <i class="fas fa-flag text-info"></i>
                                                    <label for="StatusLead" class="col-form-label">Status Lead:</label>
                                                    <asp:DropDownList ID="ddlStatusLead" DataTextField="NombreEstadoCRM" DataValueField="idEstadoCRM"
                                                        runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <i class="fas fa-industry text-info"></i>
                                                    <label for="Empresa" class="col-form-label">Tipo cliente:</label>
                                                    <asp:DropDownList ID="ddlTiposAfiliado" DataTextField="NombreTipoAfiliado" DataValueField="idTipoAfiliado"
                                                        runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="No aplica" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
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
                                                    <i class="fas fa-flag text-info"></i>
                                                    <label for="StatusLead" class="col-form-label">Métodos de pago:</label>
                                                    <asp:DropDownList ID="ddlTipoPago" runat="server" AppendDataBoundItems="true"
                                                        DataTextField="TipoDocumento" DataValueField="idTipoDoc" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="Efectivo" Value="Efectivo"></asp:ListItem>
                                                        <asp:ListItem Text="Transferencia" Value="Transferencia"></asp:ListItem>
                                                        <asp:ListItem Text="Datafono" Value="Datafono"></asp:ListItem>
                                                        <asp:ListItem Text="Wompi" Value="Wompi"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <i class="fa-brands fa-square-facebook text-info"></i>
                                                    <label for="Empresa" class="col-form-label">Objetivos del cliente:</label>
                                                    <asp:DropDownList ID="ddlObjetivos" DataTextField="Objetivo" DataValueField="idObjetivo"
                                                        runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="No aplica" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                         <asp:ScriptManager ID="ScriptManager1" runat="server" />
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <i class="fas fa-flag text-info"></i>
                                                    <label for="StatusLead" class="col-form-label">Canal de marketing:</label>
                                                    <asp:DropDownList ID="ddlCanalesMarketing" DataTextField="NombreCanalMarketing" DataValueField="idCanalMarketing"
                                                        runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <i class="fas fa-flag text-info"></i>
                                                    <label for="StatusLead" class="col-form-label">Planes:</label>
                                                    <asp:DropDownList ID="ddlPlanes" DataTextField="NombrePlan" DataValueField="idPlan"
                                                        runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlPlanes_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="Seleccione" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>


                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <label for="Meses" class="col-form-label">Meses del plan:</label>
                                                            <asp:RadioButtonList ID="rblMesesPlan" runat="server"
                                                                RepeatDirection="Horizontal" CssClass="form-control input-sm"
                                                                AutoPostBack="true" OnSelectedIndexChanged="rblMesesPlan_SelectedIndexChanged">
                                                                <asp:ListItem Text="&nbsp;1 &nbsp;&nbsp;&nbsp;" Value="1" style="margin-right: 5px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;2 &nbsp;&nbsp;&nbsp;" Value="2" style="margin-right: 5px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;3 &nbsp;&nbsp;&nbsp;" Value="3" style="margin-right: 5px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;4 &nbsp;&nbsp;&nbsp;" Value="4" style="margin-right: 5px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;5 &nbsp;&nbsp;&nbsp;" Value="5" style="margin-right: 5px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;6 &nbsp;&nbsp;&nbsp;" Value="6" style="margin-right: 5px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;7 &nbsp;&nbsp;&nbsp;" Value="7" style="margin-right: 5px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;8 &nbsp;&nbsp;&nbsp;" Value="8" style="margin-right: 5px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;9 &nbsp;&nbsp;&nbsp;" Value="9" style="margin-right: 5px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;11 &nbsp;&nbsp;&nbsp;" Value="11" style="margin-right: 5px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;12 &nbsp;&nbsp;&nbsp;" Value="12" style="margin-right: 5px;"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div style="height: 30px;"></div>
                                                <div class="row">
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
                                                </div>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                           <div class="form-group">
                                            <i class="fas fa-pen text-info"></i>
                                            <label for="message-text" class="col-form-label">Contexto de la negociación:</label>
                                            <textarea id="txaObservaciones" runat="server" rows="3" 
                                                cssclass="form-control input-sm" class="form-control"></textarea>


                                        </div>
<%--                                        <div class="form-group">
                                            <label>Contexto de la negociación:</label>
                                            <div id="editor" cssclass="form-control input-sm"></div>
                                            <asp:HiddenField ID="hiddenEditor" runat="server" />
                                        </div>--%>


                                        <div class="form-group">
                                            <a href="crmnuevocontacto" class="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md">Cancelar</a>
                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar"
                                                CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" ValidationGroup="agregar"
                                                OnClick="btnAgregar_Click" />
                                        </div>
                                        <br />
                                        <br />
                                        <div class="form-group">
                                            <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                        </div>
                                        <%--     </div>--%>
                                        <%-- </div>--%>
                                    </div>
                                </div>

                                <div class="ibox float-e-margins">
                                    <div class="ibox collapsed">
                                        <div class="ibox-title">
                                            <h5>
                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal></h5>
                                            <div class="ibox-tools">
                                                <a class="collapse-link collapse-link">
                                                    <i class="fa fa-chevron-up"></i>
                                                </a>
                                            </div>
                                        </div>
                                        <div class="ibox-content">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <i class="fa fa-user-tie text-info"></i>
                                                        <label for="nombreContacto" class="col-form-label">Nombre completo:</label>
                                                        <input type="text" runat="server" class="form-control" id="Text1"
                                                            placeholder="Nombre" spellcheck="false" autocomplete="off"
                                                            oninput="validarSoloLetras(this)" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-6">
                                                        <i class="fa-solid fa-phone text-info"></i>
                                                        <label for="telefonoContacto" class="col-form-label">Teléfono:</label>
                                                        <input type="text" runat="server" class="form-control" id="Text2"
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
                                                        <input type="text" runat="server" class="form-control" id="Text3"
                                                            spellcheck="false" placeholder="ej: cliente@ejemplo.com" autocomplete="off"
                                                            oninput="validarCorreo(this)">
                                                        <asp:Literal ID="Literal2" runat="server" Visible="false"></asp:Literal>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <i class="fas fa-industry text-info"></i>
                                                        <label for="Empresa" class="col-form-label">Empresa / Persona:</label>
                                                        <asp:DropDownList ID="DropDownList1" DataTextField="NombreEmpresaCRM" DataValueField="idEmpresaCRM"
                                                            runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                            <asp:ListItem Text="No aplica" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <i class="fas fa-flag text-info"></i>
                                                <label for="StatusLead" class="col-form-label">Status Lead:</label>
                                                <asp:DropDownList ID="DropDownList2" DataTextField="NombreEstadoCRM" DataValueField="idEstadoCRM"
                                                    runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <i class="fa-solid fa-hand-point-up text-info"></i>
                                                        <label for="txbFechaPrim" class="col-form-label">Primer contacto:</label>
                                                        <input type="text" runat="server" id="Text4" class="form-control input-sm datepicker" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <i class="fas fa-angle-right text-info"></i>
                                                        <label for="txbFechaProx" class="col-form-label">Próximo contacto:</label>
                                                        <input type="text" runat="server" id="Text5" class="form-control input-sm datepicker" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <i class="fa fa-dollar text-info"></i>
                                                        <label for="ValorPropuesta" class="col-form-label">Valor Propuesta:</label>
                                                        <asp:TextBox ID="TextBox1" CssClass="form-control input-sm" runat="server" placeholder="$0"
                                                            onkeyup="formatCurrency(this)" onblur="keepFormatted(this)" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="form-group">
                                                        <i class="fas fa-paperclip text-info"></i>
                                                        <label for="ArchivoPropuesta" class="col-form-label">Archivo Propuesta:</label>
                                                        <input type="file" runat="server" class="form-control" id="File1" placeholder="subir archivo">
                                                    </div>
                                                </div>
                                            </div>
                                            <%--                                        <div class="form-group">
                                            <i class="fas fa-pen text-info"></i>
                                            <label for="message-text" class="col-form-label">Contexto de la negociación:</label>
                                            <textarea id="txaObservaciones" runat="server" rows="3"
                                                cssclass="form-control input-sm" class="form-control">
                                            </textarea>
                                        </div>--%>
                                            <div class="form-group">
                                                <label>Contexto de la negociación:</label>
                                                <div id="editor" cssclass="form-control input-sm"></div>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </div>


                                            <div class="form-group">
                                                <a href="crmnuevocontacto" class="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md">Cancelar</a>
                                                <asp:Button ID="Button1" runat="server" Text="Agregar"
                                                    CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" ValidationGroup="agregar"
                                                    OnClick="btnAgregar_Click" OnClientClick="guardarContenidoEditor()" />
                                            </div>
                                            <br />
                                            <br />
                                            <div class="form-group">
                                                <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                            </div>
                                            <%--     </div>--%>
                                            <%-- </div>--%>
                                        </div>
                                    </div>
                                </div>



                                <div class="col-lg-12">
                                </div>

                            </div>
                            <div class="col-lg-8">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Lista de Contactos</h5>
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
                                                    <th data-sortable="false" data-breakpoints="xs" style="width: 200px;">Nombre</th>
                                                    <th data-breakpoints="xs">Teléfono</th>
                                                    <th data-breakpoints="xs">Correo</th>
                                                    <th data-breakpoints="all" data-title="Info"></th>
                                                    <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpContactosCRM" runat="server" OnItemDataBound="rpContactosCRM_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr class="feed-element">
                                                            <td><%# Eval("NombreContacto") %></td>
                                                            <td><%# Eval("TelefonoContacto") %></td>
                                                            <td><%# Eval("EmailContacto") %></td>
                                                            <td>
                                                                <table class="table table-bordered table-striped">
                                                                    <tr>
                                                                        <%-- <th width="25%"><i class="fa fa-city m-r-xs"></i>Ciudad</th>--%>
                                                                        <th width="25%"><i class="fa fa-mobile m-r-xs"></i>Archivo propuesta</th>
                                                                        <th width="50%" class="text-nowrap"><i class="fa fa-clock m-r-xs"></i>Historial</th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><%# Eval("ArchivoPropuesta") %></td>
                                                                        <td><%# Eval("historialHTML") %></td>
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
                        </div>
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

</body>

</html>

