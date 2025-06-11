<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="crmnuevocontacto.aspx.cs" Inherits="fpWebApp.crmnuevocontacto" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/indicadores01.ascx" TagPrefix="uc1" TagName="indicadores01" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>
<%@ Register Src="~/controles/indicadoresusucrm.ascx" TagPrefix="uc2" TagName="indicadoresusucrm" %>


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

    <link href="css/plugins/clockpicker/clockpicker.css" rel="stylesheet">

    <!-- Sweet alert -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <!-- CSS de Quill -->
    <link href="https://cdn.quilljs.com/1.3.6/quill.snow.css" rel="stylesheet">
    <!-- JS de Quill -->
    <script src="https://cdn.quilljs.com/1.3.6/quill.min.js"></script>

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
            var element1 = document.querySelector("#crmnuevocontacto");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#crm");
            element2.classList.remove("collapse");
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

    <%--    Formatear solo letraas --%>
    <script>
        function validarSoloLetras(input) {
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
    <!-- Select2 -->
    <link href="css/plugins/select2/select2.min.css" rel="stylesheet">




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

                    <form role="form" id="form" runat="server">
                        <asp:ScriptManager ID="ScriptManager1" runat="server" />
                        <div class="row" id="divContenido" runat="server">

                            <div class="col-lg-12">

                                <div class="tabs-container">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a data-toggle="tab" href="#tab-1"><i class="fas fa-user-plus"></i>Contactos</a></li>
                                        <li class=""><a data-toggle="tab" href="#tab-2"><i class="fas fa-industry text-info"></i>Empresas</a></li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="tab-1" class="tab-pane active">
                                            <%--Inicia contenido formulario Nuevo contacto--%>
                                            <div class="panel-body">
                                                <%--Zona lateral izquierda sup --%>
                                                <div class="col-lg-5">
                                                    <div class="ibox-content">
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <i class="fa fa-id-card text-info"></i>
                                                                    <label>Tipo de Documento</label>
                                                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" AppendDataBoundItems="true"
                                                                        DataTextField="TipoDocumento" DataValueField="idTipoDoc" CssClass="form-control input-sm">
                                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label>Nro. de Documento</label>
                                                                    <asp:TextBox ID="txbDocumento" CssClass="form-control input-sm" runat="server" placeholder="#"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvNumDoc" runat="server" ErrorMessage="* Campo requerido"
                                                                        ControlToValidate="txbDocumento" ValidationGroup="agregar"
                                                                        CssClass="font-bold text-danger" InitialValue="">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <i class="fa fa-user-tie text-info"></i>
                                                                    <label for="nombreContacto" class="col-form-label">Nombre completo:</label>
                                                                    <input type="text" runat="server" id="txbNombreContacto" class="form-control"
                                                                        oninput="validarSoloLetras(this)" style="text-transform: uppercase;" spellcheck="false" autocomplete="off" />
                                                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txbNombreContacto"
                                                                        ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-sm-6">
                                                                    <i class="fa-solid fa-phone text-info"></i>
                                                                    <label for="txbTelefonoContacto" class="col-form-label">Teléfono:</label>

                                                                    <input type="text" runat="server" id="txbTelefonoContacto"
                                                                        class="form-control"
                                                                        placeholder="ej: 310 123 4567"
                                                                        spellcheck="false"
                                                                        autocomplete="off"
                                                                        onkeyup="formatearTelefono(this)"
                                                                        maxlength="14" />

                                                                    <asp:RequiredFieldValidator ID="rfvTelefono" runat="server"
                                                                        ControlToValidate="txbTelefonoContacto"
                                                                        ErrorMessage="* Campo requerido"
                                                                        CssClass="text-danger"
                                                                        Display="Dynamic" />
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
                                                                        oninput="validarCorreo(this)" style="text-transform: lowercase;">
                                                                    <asp:Literal ID="ltError" runat="server" Visible="false"></asp:Literal>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <i class="fas fa-industry text-info"></i>
                                                                    <label for="Empresa" class="col-form-label">Empresa / Persona:</label>
                                                                    <asp:DropDownList ID="ddlEmpresa" DataTextField="NombreEmpresaCRM" DataValueField="idEmpresaCRM"
                                                                        runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                        <asp:ListItem Text="No aplica" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvEmpresa" runat="server" ErrorMessage="* Campo requerido"
                                                                        ControlToValidate="ddlEmpresa" ValidationGroup="agregar"
                                                                        CssClass="font-bold text-danger" InitialValue="">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <i class="fas fa-flag text-info"></i>
                                                                    <label for="StatusLead" class="col-form-label">Status Lead:</label>
                                                                    <asp:DropDownList ID="ddlStatusLead" runat="server" CssClass="select2_demo_1 form-control input-sm" AppendDataBoundItems="true">
                                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>


                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Campo requerido"
                                                                        ControlToValidate="ddlStatusLead" ValidationGroup="agregar"
                                                                        CssClass="font-bold text-danger" InitialValue="">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <i class="fas fa-industry text-info"></i>
                                                                    <label for="TiposAfiliado" class="col-form-label">Tipo cliente:</label>
                                                                    <asp:DropDownList ID="ddlTiposAfiliado" DataTextField="NombreTipoAfiliado" DataValueField="idTipoAfiliado"
                                                                        runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvTipoAfiliado" runat="server" ErrorMessage="* Campo requerido"
                                                                        ControlToValidate="ddlTiposAfiliado" ValidationGroup="agregar"
                                                                        CssClass="font-bold text-danger" InitialValue="">
                                                                    </asp:RequiredFieldValidator>
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
                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <i class="fas fa-angle-right"></i>
                                                                    <label for="txbFechaProx" class="col-form-label">Llamada</label>
                                                                    <input type="text" runat="server" id="txbFechaProx" class="form-control input-sm datepicker" />
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-3">
                                                                <div class="form-group">
                                                                    <label for="txbHoraIni" class="col-form-label">Hora:</label>
                                                                    <div class="input-group clockpicker" data-autoclose="true">
                                                                        <input type="text" class="form-control input-sm" value="08:00" id="txbHoraIni" name="txbHoraIni" runat="server" />
                                                                        <span class="input-group-addon">
                                                                            <span class="fa fa-clock"></span>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <i class="fa-solid fa-coins text-info"></i>
                                                                    <label for="TipoPago" class="col-form-label">Métodos de pago:</label>
                                                                    <asp:DropDownList ID="ddlTipoPago" runat="server" AppendDataBoundItems="true"
                                                                        DataTextField="TipoPago" DataValueField="idTipoDoc" CssClass="form-control input-sm">
                                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                        <asp:ListItem Text="Efectivo" Value="Efectivo"></asp:ListItem>
                                                                        <asp:ListItem Text="Transferencia" Value="Transferencia"></asp:ListItem>
                                                                        <asp:ListItem Text="Datafono" Value="Datafono"></asp:ListItem>
                                                                        <asp:ListItem Text="Wompi" Value="Wompi"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvTipoPago" runat="server" ErrorMessage="* Campo requerido"
                                                                        ControlToValidate="ddlTipoPago" ValidationGroup="agregar"
                                                                        CssClass="font-bold text-danger" InitialValue="">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <i class="fa-solid fa-bullseye text-info"></i>
                                                                    <label for="Objetivos" class="col-form-label">Objetivos del cliente:</label>
                                                                    <asp:DropDownList ID="ddlObjetivos" DataTextField="Objetivo" DataValueField="idObjetivo"
                                                                        runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvObjetios" runat="server" ErrorMessage="* Campo requerido"
                                                                        ControlToValidate="ddlObjetivos" ValidationGroup="agregar"
                                                                        CssClass="font-bold text-danger" InitialValue="">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="row">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fa-brands fa-square-facebook text-info"></i>
                                                                            <label for="Canales" class="col-form-label">Canal de marketing:</label>
                                                                            <asp:DropDownList ID="ddlCanalesMarketing" DataTextField="NombreCanalMarketing" DataValueField="idCanalMarketing"
                                                                                runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvCanalesMarketing" runat="server" ErrorMessage="* Campo requerido"
                                                                                ControlToValidate="ddlCanalesMarketing" ValidationGroup="agregar"
                                                                                CssClass="font-bold text-danger" InitialValue="">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fa fa-ticket text-info"></i>
                                                                            <label for="Planes" class="col-form-label">Planes:</label>
                                                                            <asp:DropDownList ID="ddlPlanes" DataTextField="NombrePlan" DataValueField="idPlan"
                                                                                runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlPlanes_SelectedIndexChanged" AutoPostBack="true">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="form-group">
                                                                            <i class="fa-regular fa-calendar text-info"></i>
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
                                                                                <asp:ListItem Text="&nbsp;10 &nbsp;&nbsp;&nbsp;" Value="10" style="margin-right: 5px;"></asp:ListItem>
                                                                                <asp:ListItem Text="&nbsp;11 &nbsp;&nbsp;&nbsp;" Value="11" style="margin-right: 5px;"></asp:ListItem>
                                                                                <asp:ListItem Text="&nbsp;12 &nbsp;&nbsp;&nbsp;" Value="12" style="margin-right: 5px;"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div style="height: 30px;"></div>
                                                                <div class="row">
                                                                    <div class="col-sm-4">
                                                                        <div class="form-group">
                                                                            <i class="fa fa-dollar text-info"></i>
                                                                            <label for="ValorPropuesta" class="col-form-label">Valor Propuesta:</label>
                                                                            <asp:TextBox ID="txbValorPropuesta" CssClass="form-control input-sm" runat="server" placeholder="$0"
                                                                                onkeyup="formatCurrency(this)" onblur="keepFormatted(this)" autocomplete="off"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvValorPropuesta" runat="server" ErrorMessage="* Campo requerido"
                                                                                ControlToValidate="txbValorPropuesta" ValidationGroup="agregar"
                                                                                CssClass="font-bold text-danger" InitialValue="">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <div class="form-group">
                                                                            <i class="fa fa-dollar text-info"></i>
                                                                            <label for="ValorMes" class="col-form-label">Valor mes:</label>
                                                                            <asp:TextBox ID="txbValorMes" CssClass="form-control input-sm" runat="server" placeholder="$0"
                                                                                onkeyup="formatCurrency(this)" onblur="keepFormatted(this)" autocomplete="off" Style="background-color: #e3ff00;"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <div class="form-group">
                                                                            <i class="fas fa-paperclip text-info"></i>
                                                                            <label for="ArchivoPropuesta" class="col-form-label">Archivo Propuesta:</label>
                                                                            <input type="file" runat="server" class="form-control" id="ArchivoPropuesta" placeholder="subir archivo">
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group">
                                                                    <i class="fas fa-pen text-info"></i>
                                                                    <label for="message-text" class="col-form-label">Contexto de la negociación:</label>
                                                                    <textarea id="txaObservaciones" runat="server" rows="3"
                                                                        cssclass="form-control input-sm" class="form-control"></textarea>
                                                                    <asp:RequiredFieldValidator ID="rfvObservaciones" runat="server" ErrorMessage="* Campo requerido"
                                                                        ControlToValidate="txbValorPropuesta" ValidationGroup="agregar"
                                                                        CssClass="font-bold text-danger" InitialValue="">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>

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

                                                <%--Zona lateral derecha sup --%>
                                                <div class="col-lg-7">
                                                    <div class="row" style="font-size: 12px;" runat="server" id="divBotonesLista">
                                                        <div class="col-lg-6 form-horizontal">
                                                            <div class="form-group">
                                                                <div class="form-group" id="filter-form-container1" style="margin-left: 28px;"></div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                                        data-filter-min="3" data-filter-placeholder="Buscar"
                                                        data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}"
                                                        data-paging-limit="10" data-filtering="true"
                                                        data-filter-container="#filter-form-container1" data-filter-delay="300"
                                                        data-filter-dropdown-title="Buscar en:" data-filter-position="left"
                                                        data-empty="Sin resultados">
                                                        <thead>
                                                            <tr>
                                                                <th data-sortable="false" data-breakpoints="xs" style="width: 200px;">Nombre</th>
                                                                <th data-breakpoints="xs">Teléfono</th>
                                                                <th data-breakpoints="xs">Correo</th>
                                                                <th data-breakpoints="xs">Lead</th>
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
                                                                            <span title='<%# Eval("NombreEstadoCRM") %>' style='color: <%# Eval("ColorHexaCRM") %>'>
                                                                                <%# Eval("IconoMinEstadoCRM") %>
                                                                            </span>
                                                                        </td>

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
                                                    <%-- </div>--%>
                                                    <%--  </div>--%>
                                                </div>
                                                <%--                                                    </ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                            </div>
                                        </div>
                                        <div id="tab-2" class="tab-pane">
                                            <div class="panel-body">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <%--Zona lateral izquierda inf --%>
                                                        <div class="col-lg-5">
                                                            <div class="ibox-content">
                                                                <div class="row">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fa fa-user-tie text-info"></i>
                                                                            <label for="nombreEmpresaCRM" class="col-form-label">Nombre empresa:</label>
                                                                            <input type="text" runat="server" class="form-control" id="txbNombreEmpresaCRM"
                                                                                spellcheck="false" autocomplete="off"
                                                                                oninput="validarSoloLetras(this)" style="text-transform: uppercase;" />
                                                                            <asp:RequiredFieldValidator ID="rfvNombreEmpresa" runat="server" ControlToValidate="txbNombreEmpresaCRM"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-sm-6">
                                                                            <i class="fa-solid fa-phone text-info"></i>
                                                                            <label for="paginaWeb" class="col-form-label">Página web:</label>
                                                                            <input type="text" runat="server" class="form-control" id="txbPaginaWeb"
                                                                                placeholder="ej: www.fitnesspeoplecolombia.com" spellcheck="false" autocomplete="off"
                                                                                style="text-transform: lowercase;">
                                                                            <asp:RequiredFieldValidator ID="fvPaginaWeb" runat="server" ControlToValidate="txbPaginaWeb"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" ValidationGroup="agregarE" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fa fa-city text-info"></i>
                                                                            <label>Ciudad:</label>
                                                                            <asp:DropDownList ID="ddlCiudad" runat="server"
                                                                                AppendDataBoundItems="true" DataTextField="NombreCiudad"
                                                                                DataValueField="idCiudad" CssClass="chosen-select form-control input-sm">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvCiudad" runat="server" ErrorMessage="* Campo requerido"
                                                                                ControlToValidate="ddlCiudad" ValidationGroup="agregarE"
                                                                                CssClass="font-bold text-danger" InitialValue="">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fas fa-flag text-info"></i>
                                                                            <label for="Contacto" class="col-form-label">Contacto:</label>
                                                                            <asp:DropDownList ID="ddlContactos" DataTextField="NombreContacto" DataValueField="idContacto"
                                                                                runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <label>Estado:</label>
                                                                            <asp:RadioButtonList ID="rblEstado" runat="server" RepeatDirection="Horizontal"
                                                                                CssClass="form-control input-sm">
                                                                                <asp:ListItem Text="&nbsp;Activo&nbsp;&nbsp;&nbsp;&nbsp;" Value="Activo"></asp:ListItem>
                                                                                <asp:ListItem Text="&nbsp;Inactivo&nbsp;&nbsp;&nbsp;&nbsp;" Value="Inactivo"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                            <asp:RequiredFieldValidator ID="rfvEstado" runat="server" ErrorMessage="* Campo requerido"
                                                                                ControlToValidate="rblEstado" ValidationGroup="agregarE"
                                                                                CssClass="font-bold text-danger" InitialValue="">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fas fa-pen text-info"></i>
                                                                            <label for="message-text" class="col-form-label">Observaciones:</label>
                                                                            <textarea id="txaObservacionesEmp" runat="server" rows="3"
                                                                                cssclass="form-control input-sm" class="form-control"></textarea>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <a href="crmnuevocontacto" class="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md">Cancelar</a>
                                                                    <asp:Button ID="btnAgregarEmp" runat="server" Text="Agregar"
                                                                        CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" ValidationGroup="agregarE"
                                                                        OnClick="btnAgregarEmp_Click" />
                                                                </div>
                                                                <br />
                                                                <br />
                                                                <div class="form-group">
                                                                    <asp:Literal ID="Literal3" runat="server"></asp:Literal>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <%--Zona lateral derecha inf --%>
                                                        <div class="col-lg-7">
                                                            <div class="row" style="font-size: 12px;" runat="server" id="divBotonesLista1">
                                                                <div class="col-lg-6 form-horizontal">
                                                                    <div class="form-group">
                                                                        <div class="form-group" id="filter-form-container2" style="margin-left: 28px;"></div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                                                data-filter-min="3" data-filter-placeholder="Buscar"
                                                                data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}"
                                                                data-paging-limit="10" data-filtering="true"
                                                                data-filter-container="#filter-form-container2" data-filter-delay="300"
                                                                data-filter-dropdown-title="Buscar en:" data-filter-position="left"
                                                                data-empty="Sin resultados">
                                                                <thead>
                                                                    <tr>
                                                                        <th data-sortable="false" data-breakpoints="xs" style="width: 200px;">Nombre empresa</th>
                                                                        <th data-breakpoints="xs">Ciudad</th>
                                                                        <th data-breakpoints="xs">Página web</th>
                                                                        <th data-breakpoints="xs">Contacto</th>
                                                                        <th data-breakpoints="all" data-title="Info"></th>
                                                                        <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Repeater ID="rpEmpresasCRM" runat="server" OnItemCommand="rpEmpresasCRM_ItemCommand" OnItemDataBound="rpEmpresasCRM_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <tr class="feed-element">
                                                                                <td><%# Eval("NombreEmpresaCRM") %></td>
                                                                                <td><%# Eval("NombreCiudad") %></td>
                                                                                <td><%# Eval("paginaWeb") %></td>
                                                                                <td><%# Eval("idContacto") %></td>

                                                                                <td>
                                                                                    <table class="table table-bordered table-striped">
                                                                                        <tr>
                                                                                            <th width="25%"><i class="fa fa-mobile m-r-xs"></i>Archivo propuesta</th>
                                                                                            <th width="50%" class="text-nowrap"><i class="fa fa-clock m-r-xs"></i>Historial</th>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td><%# Eval("ArchivoPropuesta") %></td>
                                                                                            <td><%# Eval("ObservacionesEmp") %></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                                <td style="display: flex; flex-wrap: nowrap;">
                                                                                    <a runat="server" id="btnEliminarEmp" href="#" class="btn btn-outline btn-danger pull-right m-r-xs"
                                                                                        style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false"><i class="fa fa-trash"></i></a>
                                                                                    <asp:LinkButton runat="server" ID="btnEditarEmp" CommandArgument='<%# Eval("idEmpresaCRM") %>'
                                                                                        CommandName="EditarEmpresa" CssClass="btn btn-outline btn-primary pull-right m-r-xs"
                                                                                        Style="padding: 1px 2px 1px 2px; margin-bottom: 0px;">                                                                                                                        
                                                                                        <i class="fa fa-edit"></i>
                                                                                    </asp:LinkButton>



                                                                                    <%--                                                                                    <asp:Button ID="btnEditarEmp" runat="server" Text="E" CssClass="btn btn-outline btn-primary pull-right m-r-xs"
                                                                                        Style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" CausesValidation="false" OnClick="btnEditarEmp_Click" CommandArgument='<%# Eval("idEmpresaCRM") %>' />--%>
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>


                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
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
    <script src="js/plugins/clockpicker/clockpicker.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-beta.1/dist/js/select2.min.js"></script>

    <script>
        $('.footable').footable();
        $('.clockpicker').clockpicker();
    </script>
    <script>
        $('#acordeonZonaLateralIzqSup').collapse('hide');
    </script>

    <script>
        $(document).ready(function () {
            $('#<%= ddlStatusLead.ClientID %>').select2({
                templateResult: formatOption,
                templateSelection: formatOption,
                escapeMarkup: function (m) { return m; } // Permite HTML
            });

            function formatOption(state) {
                if (!state.id) return state.text;

                var color = $(state.element).data('color');
                var icon = $(state.element).data('icon');

                // Aplica color solo al icono
                return "<span><span style='color:" + color + ";'>" + icon + "</span> " + state.text + "</span>";
            }
        });
    </script>

</body>

</html>

