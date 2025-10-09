<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="crmnuevocontacto.aspx.cs" Inherits="fpWebApp.crmnuevocontacto" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/indicadores01.ascx" TagPrefix="uc1" TagName="indicadores01" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>
<%@ Register Src="~/controles/indicadoresusucrm.ascx" TagPrefix="uc2" TagName="indicadoresusucrm" %>
<%@ Register Src="~/controles/indicadoresusucrm2.ascx" TagPrefix="uc1" TagName="indicadoresusucrm2" %>



<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Contacto CRM</title>

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

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.3/themes/smoothness/jquery-ui.css">

    <link href="css/plugins/dropzone/basic.css" rel="stylesheet" />
    <link href="css/plugins/dropzone/dropzone.css" rel="stylesheet" />
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet" />
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />
    <link href="css/plugins/ionRangeSlider/ion.rangeSlider.css" rel="stylesheet" />
    <link href="css/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.bootstrap.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <style>
        .crm-align-row {
            display: table;
            width: 100%;
        }

        .crm-align-cell {
            display: table-cell;
            vertical-align: bottom;
            padding-right: 15px;
        }

        .crm-spinner {
            text-align: center;
        }
    </style>

    <style>
        #historialHTMLVisual table {
            max-width: 100%;
            width: auto;
            table-layout: auto;
            word-break: break-word;
        }

        #historialHTMLVisual td,
        #historialHTMLVisual th {
            word-break: break-word;
            white-space: normal;
            font-size: 12px;
        }
    </style>


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

    <!-- Select2 -->
    <%--   <link href="css/plugins/select2/select2.min.css" rel="stylesheet">--%>

    <script src="js/jquery-3.1.1.min.js"></script>
    <!-- Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" rel="stylesheet" />

    <!-- Select2 -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.1.0-beta.1/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.1.0-beta.1/js/select2.min.js"></script>

    <script>
        $(document).ready(function () {
            $('.select2_demo_1').select2();
        });
    </script>

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
            // Activa el menú principal
            var element1 = document.querySelector("#crmnuevocontacto");
            if (element1) {
                element1.classList.add("active");
            }

            // Despliega el submenú
            var element2 = document.querySelector("#crm");
            if (element2) {
                element2.classList.add("show"); // en Bootstrap el desplegado es con "show"
                element2.classList.remove("collapse");
            }
        }


    </script>

</head>

<body onload="changeClass(); iniciarContador()">
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
                    <uc1:indicadoresusucrm2 runat="server" id="indicadoresusucrm2" />
                    <%-- //////////////////////////////////GRÁFICAS//////////////////////////////////////////////////--%>

                    <%--Gráfica asesor--%>
<%--                    <div class="row d-flex">
                        <!-- Gráfica -->
                        <div class="col-lg-6 d-flex">
                            <div class="ibox flex-fill w-100">
                                <div class="ibox-content">
                                    <div>
                                        <span class="pull-right text-right">
                                            <small>Valor promedio de ventas del último mes en: <strong>Online</strong></small>
                                            <br />
                                            Total ventas: 19'162,862
                                        </span>
                                        <h1 class="m-b-xs">$ 5'098,992</h1>
                                        <h3 class="font-bold no-margins">Objetivo mes julio</h3>
                                        <small>..</small>
                                    </div>

                                    <div>
                                        <canvas id="lineChart" height="70"></canvas>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Widgets -->
                        <div class="col-lg-6 d-flex flex-column">
                            <!-- Fila 1 de widgets -->
                            <div class="row flex-fill mb-2">
                                <div class="col-lg-4 d-flex">
                                    <div class="widget style1 navy-bg flex-fill w-100">
                                        <div class="row vertical-align">
                                            <div class="col-xs-3">
                                                <i class="fa fa-user fa-3x"></i>
                                            </div>
                                            <div class="col-xs-9 text-right">
                                                <h2 class="font-bold">217</h2>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 d-flex">
                                    <div class="widget style1 navy-bg flex-fill w-100">
                                        <div class="row vertical-align">
                                            <div class="col-xs-3">
                                                <i class="fa fa-thumbs-up fa-3x"></i>
                                            </div>
                                            <div class="col-xs-9 text-right">
                                                <h2 class="font-bold">400</h2>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 d-flex">
                                    <div class="widget style1 navy-bg flex-fill w-100">
                                        <div class="row vertical-align">
                                            <div class="col-xs-3">
                                                <i class="fa fa-rss fa-3x"></i>
                                            </div>
                                            <div class="col-xs-9 text-right">
                                                <h2 class="font-bold">10</h2>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Fila 2 de widgets -->
                            <div class="row flex-fill">
                                <div class="col-lg-4 d-flex">
                                    <div class="widget style1 lazur-bg flex-fill w-100">
                                        <div class="row vertical-align">
                                            <div class="col-xs-3">
                                                <i class="fa fa-phone fa-3x"></i>
                                            </div>
                                            <div class="col-xs-9 text-right">
                                                <h2 class="font-bold">120</h2>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 d-flex">
                                    <div class="widget style1 lazur-bg flex-fill w-100">
                                        <div class="row vertical-align">
                                            <div class="col-xs-3">
                                                <i class="fa fa-euro fa-3x"></i>
                                            </div>
                                            <div class="col-xs-9 text-right">
                                                <h2 class="font-bold">462</h2>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 d-flex">
                                    <div class="widget style1 yellow-bg flex-fill w-100">
                                        <div class="row vertical-align">
                                            <div class="col-xs-3">
                                                <i class="fa fa-shield fa-3x"></i>
                                            </div>
                                            <div class="col-xs-9 text-right">
                                                <h2 class="font-bold">610</h2>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Otros ibox -->
                        <div class="col-lg-2 d-flex mt-3">
                            <div class="ibox flex-fill w-100">
                                <div class="ibox-content">
                                    <h5>Vendido hasta hoy</h5>
                                    <h1 class="no-margins">1 738,200</h1>
                                    <div class="stat-percent font-bold text-navy">98% <i class="fa fa-bolt"></i></div>
                                    <small>Cumplimiento</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 d-flex mt-3">
                            <div class="ibox flex-fill w-100">
                                <div class="ibox-content">
                                    <h5>Brecha</h5>
                                    <h1 class="no-margins">-200,100</h1>
                                    <div class="stat-percent font-bold text-danger">12% <i class="fa fa-level-down"></i></div>
                                    <small>Proyectado</small>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 d-flex mt-3">
                            <div class="ibox flex-fill w-100">
                                <div class="ibox-content">
                                    <h5>Brecha</h5>
                                    <h1 class="no-margins">-200,100</h1>
                                    <div class="stat-percent font-bold text-danger">12% <i class="fa fa-level-down"></i></div>
                                    <small>Otros</small>
                                </div>
                            </div>
                        </div>
                    </div>--%>

                    <%-- ////////////////////////////////////////////////////////////////////////////////////////////--%>


                    <%--     <uc2:indicadoresusucrm runat="server" ID="indicadoresusucrm" />--%>
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
                                            <div class="form-group">
                                                <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                            </div>
                                            <%--Inicia contenido formulario Nuevo contacto--%>
                                            <div class="panel-body">
                                                <%--Zona lateral izquierda sup --%>
                                                <div class="col-lg-5">
                                                    <div class="ibox-content">

                                                        <asp:UpdatePanel ID="upAfiliado" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>

<%--                                                                <div class="crm-align-row">
                                                                    <div class="crm-align-cell" style="width: 80%;" id="btnAfiliadoBus">
                                                                        <div class="form-group">
                                                                            <label>Afiliado origen</label>
                                                                            <asp:DropDownList ID="ddlAfiliadoOrigen" name="ddlAfiliadoOrigen" runat="server"
                                                                                DataTextField="DocNombreAfiliado" AppendDataBoundItems="true"
                                                                                DataValueField="DocumentoAfiliado" CssClass="chosen-select form-control input-sm"
                                                                                OnSelectedIndexChanged="ddlAfiliadoOrigen_SelectedIndexChanged"
                                                                                AutoPostBack="true">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <!-- Contador -->
                                                                    <div class="crm-align-cell" style="width: 10%;">
                                                                        <div class="form-group">
                                                                            <label>Contador:</label>
                                                                            <div id="reloj" style="font-size: 20px; font-family: monospace;"></div>
                                                                            <asp:HiddenField ID="hfContador" runat="server" />
                                                                        </div>
                                                                    </div>

                                                                    <!-- Spinner -->
                                                                    <div class="crm-align-cell" style="width: 10%;">
                                                                        <div class="form-group crm-spinner">
                                                                            <div class="sk-spinner sk-spinner-wave">
                                                                                <div class="sk-rect1"></div>
                                                                                <div class="sk-rect2"></div>
                                                                                <div class="sk-rect3"></div>
                                                                                <div class="sk-rect4"></div>
                                                                                <div class="sk-rect5"></div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>--%>

                                                                  <div class="crm-align-row">
                                                                    <div class="crm-align-cell" style="width: 80%;" id="btnAfiliadoBus">
                                                                        <div class="form-group">
                                                                            <label>Contactos asignados</label>
                                                                            <asp:DropDownList ID="ddlAfiliadoOrigen" name="ddlAfiliadoOrigen" runat="server"
                                                                                DataTextField="NombreCompleto" AppendDataBoundItems="true"
                                                                                DataValueField="DocumentoContacto" CssClass="chosen-select form-control input-sm"
                                                                                OnSelectedIndexChanged="ddlAfiliadoOrigen_SelectedIndexChanged"
                                                                                AutoPostBack="true">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <!-- Contador -->
                                                                    <div class="crm-align-cell" style="width: 10%;">
                                                                        <div class="form-group">
                                                                            <label>Contador:</label>
                                                                            <div id="reloj" style="font-size: 20px; font-family: monospace;"></div>
                                                                            <asp:HiddenField ID="hfContador" runat="server" />
                                                                        </div>
                                                                    </div>

                                                                    <!-- Spinner -->
                                                                    <div class="crm-align-cell" style="width: 10%;">
                                                                        <div class="form-group crm-spinner">
                                                                            <div class="sk-spinner sk-spinner-wave">
                                                                                <div class="sk-rect1"></div>
                                                                                <div class="sk-rect2"></div>
                                                                                <div class="sk-rect3"></div>
                                                                                <div class="sk-rect4"></div>
                                                                                <div class="sk-rect5"></div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <asp:Panel ID="pnlPlanesAfiliado" runat="server" Visible="false">
                                                                    <div class="row">
                                                                        <div class="ibox-content">
                                                                            <ul class="todo-list small-list">
                                                                                <asp:Repeater ID="rpPlanesAfiliado" runat="server">
                                                                                    <ItemTemplate>
                                                                                        <li>
                                                                                            <div class="i-checks">
                                                                                                <small class="label label-<%# Eval("label1") %> pull-right"><%# Eval("DiasQueFaltan") %></small>
                                                                                                <label>
                                                                                                    <%# Eval("NombrePlan") %>, <%# Eval("Meses") %> mes(es)
                                                                                                </label>
                                                                                                <br />
                                                                                                <div class="progress progress-striped active">
                                                                                                    <div style='width: <%# Eval("Porcentaje1") %>%' class="progress-bar progress-bar-success"></div>
                                                                                                    <div style='width: <%# Eval("Porcentaje2") %>%' class="progress-bar progress-bar-warning"></div>
                                                                                                </div>
                                                                                                <small class="text-muted"><%# Eval("FechaInicioPlan", "{0:dd MMM yyyy}") %> - <%# Eval("FechaFinalPlan", "{0:dd MMM yyyy}") %></small>
                                                                                            </div>
                                                                                        </li>
                                                                                    </ItemTemplate>
                                                                                </asp:Repeater>
                                                                            </ul>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>


                                                                <div class="row" id="tipoDocYDoc">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fa fa-id-card text-info"></i>
                                                                            <label>Tipo de Documento</label>
                                                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" AppendDataBoundItems="true"
                                                                                DataTextField="TipoDocumento" DataValueField="idTipoDoc" CssClass="form-control input-sm">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvTipoDoc" runat="server" ControlToValidate="ddlTipoDocumento"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <label>Nro. de Documento</label>
                                                                            <asp:TextBox ID="txbDocumento" ClientIDMode="Static" CssClass="form-control input-sm" runat="server" placeholder="#"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvNumDoc" runat="server" ControlToValidate="txbDocumento"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic">
                                                                            </asp:RequiredFieldValidator>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row" id="nombres">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fa fa-user-tie text-info"></i>
                                                                            <label for="nombreContacto" class="col-form-label">Nombres:</label>
                                                                            <input type="text" runat="server" id="txbNombreContacto" class="form-control"
                                                                                oninput="validarSoloLetras(this)" style="text-transform: uppercase;" spellcheck="false" autocomplete="off" />
                                                                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txbNombreContacto"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fa fa-user-tie text-info"></i>
                                                                            <label for="apellidoContacto" class="col-form-label">Apellidos:</label>
                                                                            <input type="text" runat="server" id="txbApellidoContacto" class="form-control"
                                                                                oninput="validarSoloLetras(this)" style="text-transform: uppercase;" spellcheck="false" autocomplete="off" />
                                                                            <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txbApellidoContacto"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row" id="generoyfechanac">
                                                                    <div class="col-sm-4" >
                                                                        <div class="form-group">
                                                                            <i class="fa-solid fa-user-group text-info"></i></i>
                                                                            <label for="Genero" class="col-form-label">Género:</label>
                                                                            <asp:DropDownList ID="ddlGenero" DataTextField="Genero" DataValueField="idGenero"
                                                                                runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvGenero" runat="server" ControlToValidate="ddlEstadoVenta"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-3">
                                                                        <div class="form-group">
                                                                            <i class="fa-solid fa-arrow-up-9-1 text-info"></i>
                                                                            <label for="Edad" class="col-form-label">Edad:</label>
                                                                              <asp:TextBox ID="txbEdad" ClientIDMode="Static" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <div class="form-group">
                                                                            <i class="fa-solid fa-calendar-days text-info"></i>
                                                                            <label for="FechaNac" class="col-form-label">F. Nac.:</label>
                                                                            <asp:TextBox ID="txbFecNac" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>                                                                           
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row" id="telefonoYCorreo">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">

                                                                            <i class="fa-solid fa-phone text-info"></i>
                                                                            <label for="txbTelefonoContacto" class="col-form-label">Teléfono:</label>
                                                                            <input type="text" runat="server" id="txbTelefonoContacto" class="form-control"
                                                                                placeholder="ej: 310 123 4567" spellcheck="false" autocomplete="off"
                                                                                onkeyup="formatearTelefono(this)" maxlength="14" />
                                                                            <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txbTelefonoContacto"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fa-solid fa-envelope text-info"></i>
                                                                            <label for="correoContacto" class="col-form-label">Correo electrónico:</label>
                                                                            <input type="text" runat="server" class="form-control" id="txbCorreoContacto"
                                                                                spellcheck="false" placeholder="ej: cliente@ejemplo.com" autocomplete="off"
                                                                                oninput="validarCorreo(this)" style="text-transform: lowercase;">
                                                                            <asp:Literal ID="ltError" runat="server" Visible="false"></asp:Literal>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txbCorreoContacto"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-6" id="empresa">
                                                                        <div class="form-group">
                                                                            <i class="fas fa-industry text-info"></i>
                                                                            <label for="Empresa" class="col-form-label">Empresa / Persona:</label>
                                                                            <asp:DropDownList ID="ddlEmpresa" DataTextField="NombreEmpresaCRM" DataValueField="idEmpresaCRM"
                                                                                runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                                <asp:ListItem Text="No aplica" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvEmpresa" runat="server" ControlToValidate="ddlEmpresa"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fas fa-flag text-info"></i>
                                                                            <label for="StatusLead" class="col-form-label">Status Lead:</label>
                                                                            <asp:DropDownList ID="ddlStatusLead" runat="server" CssClass="select2_demo_1 form-control input-sm"
                                                                                AppendDataBoundItems="true">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvStatusLead" runat="server" ControlToValidate="ddlStatusLead"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row">
                                                                    <div class="col-sm-6" id="estadoventa">
                                                                        <div class="form-group">
                                                                            <i class="fa-solid fa-fire text-info"></i>
                                                                            <label for="EstadoVenta" class="col-form-label">Estado de la venta:</label>
                                                                            <asp:DropDownList ID="ddlEstadoVenta" DataTextField="NombreEstadoVenta" DataValueField="idEstadoVenta"
                                                                                runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvEstadoVenta" runat="server" ControlToValidate="ddlEstadoVenta"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6" id="estrategia">
                                                                        <div class="form-group">
                                                                            <i class="fa-solid fa-arrows-to-eye text-info"></i>
                                                                            <label for="Estrategia" class="col-form-label">Estrategia de marketing:</label>
                                                                            <asp:DropDownList ID="ddlEstrategia" DataTextField="NombreEstrategia" DataValueField="idEstrategia"
                                                                                runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvEstrategia" runat="server" ControlToValidate="ddlEstrategia"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="row" id="canalMarkYTipoCli">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fa-brands fa-square-facebook text-info"></i>
                                                                            <label for="Canales" class="col-form-label">Canal de marketing:</label>
                                                                            <asp:DropDownList ID="ddlCanalesMarketing" DataTextField="NombreCanalMarketing" DataValueField="idCanalMarketing"
                                                                                runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvCanalesMarketing" runat="server" ControlToValidate="ddlCanalesMarketing"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fa-solid fa-person text-info"></i>
                                                                            <label for="TiposAfiliado" class="col-form-label">Tipo cliente:</label>
                                                                            <asp:DropDownList ID="ddlTiposAfiliado" DataTextField="NombreTipoAfiliado" DataValueField="idTipoAfiliado"
                                                                                runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="rfvTipoAfiliado" runat="server" ControlToValidate="ddlTiposAfiliado"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <div class="row">
                                                            <div class="col-sm-4" id="primerContacto">
                                                                <div class="form-group">
                                                                    <i class="fa-solid fa-hand-point-up text-info"></i>
                                                                    <label for="txbFechaPrim" class="col-form-label">1er contacto:</label>
                                                                    <input type="text" runat="server" id="txbFechaPrim" class="form-control input-sm datepicker" />
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <div class="form-group">
                                                                    <i class="fas fa-angle-right"></i>
                                                                    <label for="txbFechaProx" class="col-form-label">Llamada</label>
                                                                    <input type="text" runat="server" id="txbFechaProx" class="form-control input-sm datepicker" />
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-4">
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

                                                        <div class="row" id="metodosPagoYObjetivos">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <i class="fa-solid fa-coins text-info"></i>
                                                                    <label for="TipoPago" class="col-form-label">Métodos de pago:</label>
                                                                    <asp:DropDownList ID="ddlTipoPago" runat="server" AppendDataBoundItems="true"
                                                                        DataTextField="NombreMedioPago" DataValueField="idMedioPago" CssClass="form-control input-sm">
                                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvTipoPago" runat="server" ControlToValidate="ddlTipoPago"
                                                                        ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
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
                                                                    <asp:RequiredFieldValidator ID="rfvObjetios" runat="server" ControlToValidate="ddlObjetivos"
                                                                        ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="row" id="planesYValorMes">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fa fa-ticket text-info"></i>
                                                                            <label for="Planes" class="col-form-label">Planes:</label>
                                                                            <asp:DropDownList ID="ddlPlanes" DataTextField="NombrePlan" DataValueField="idPlan"
                                                                                runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlPlanes_SelectedIndexChanged" AutoPostBack="true">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPlanes"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fa fa-dollar text-info"></i>
                                                                            <label for="ValorMes" class="col-form-label">Valor mes:</label>
                                                                            <asp:TextBox ID="txbValorMes" CssClass="form-control input-sm" runat="server" placeholder="$0"
                                                                                onkeyup="formatCurrency(this)" onblur="keepFormatted(this)" autocomplete="off" Style="background-color: #e3ff00;"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row" id="valorPropuestaYArchivo">
                                                                    <div class="col-sm-6">
                                                                        <div class="form-group">
                                                                            <i class="fa fa-dollar text-info"></i>
                                                                            <label for="ValorPropuesta" class="col-form-label">Valor Propuesta:</label>
                                                                            <asp:TextBox ID="txbValorPropuesta" CssClass="form-control input-sm" runat="server" placeholder="$0"
                                                                                onkeyup="formatCurrency(this)" onblur="keepFormatted(this)" autocomplete="off"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvValorPropuesta" runat="server" ControlToValidate="txbValorPropuesta"
                                                                                ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
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

                                                                <div class="form-group">
                                                                    <i class="fas fa-pen text-info"></i>
                                                                    <label for="message-text" class="col-form-label">Contexto de la negociación:</label>
                                                                    <textarea id="txaObservaciones" runat="server" rows="3"
                                                                        cssclass="form-control input-sm" class="form-control" placeholder="Escribe tu comentario…"></textarea>
                                                                    <asp:RequiredFieldValidator ID="rfvObservaciones" runat="server" ControlToValidate="txaObservaciones"
                                                                        ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" />
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>

                                                        <div class="form-group">
                                                            <a href="crmnuevocontacto" class="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md" id="btnCancelar_">Cancelar</a>
                                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar"
                                                                CssClass="btn btn-sm btn-primary pull-right m-t-n-xs m-l-md"
                                                                OnClick="btnAgregar_Click" />
                                                            <a href="agendacrm" class="btn btn-sm btn-info pull-right m-t-n-xs m-l-md" id="btnVolverAgenda" style="display: none;">Volver</a>
                                                        </div>
                                                        <asp:Literal ID="itBotonConfirmar" runat="server" Visible="false"></asp:Literal>
                                                        <br />
                                                        <br />

                                                        <%--     </div>--%>
                                                        <%-- </div>--%>
                                                    </div>
                                                </div>
                                                <%--Zona lateral derecha sup --%>
                                                <div class="col-lg-7" id="divTabla">
                                                    <div class="row" style="font-size: 12px;" runat="server" id="divBotonesLista">
                                                        <div class="col-lg-6 form-horizontal">
                                                            <div class="form-group">
                                                                <div class="form-group" id="filter-form-container1" style="margin-left: 28px;"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <table id="tablaContactos" class="footable table table-striped list-group-item-text" data-paging-size="10"
                                                        data-filter-min="3" data-filter-placeholder="Buscar"
                                                        data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}"
                                                        data-paging-limit="10" data-filtering="true"
                                                        data-filter-container="#filter-form-container1" data-filter-delay="300"
                                                        data-filter-dropdown-title="Buscar en:" data-filter-position="left"
                                                        data-empty="Sin resultados">
                                                        <thead>
                                                            <tr>
                                                                <th data-sortable="false" data-breakpoints="xs" style="width: 300px;">Nombre</th>
                                                                <th data-breakpoints="xs">Teléfono</th>
                                                                <th data-breakpoints="xs">Estado</th>
                                                                <th data-breakpoints="xs">Plan</th>
                                                                <th data-breakpoints="xs">HaceCuanto</th>
                                                                <th data-breakpoints="xs">Lead</th>
                                                                <th data-breakpoints="all" data-title="Info"></th>
                                                                <th data-sortable="false" data-filterable="false" class="text-left" style="width: 120px;">Acciones</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rpContactosCRM" runat="server" OnItemDataBound="rpContactosCRM_ItemDataBound">
                                                                <ItemTemplate>
                                                                    <tr class="feed-element">
                                                                        <td><%# Eval("NombreContacto") %> <%# Eval("ApellidoContacto") %></td>
                                                                        <td><%# Eval("TelefonoContacto") %></td>
                                                                        <td>Caliente</td>
                                                                        <td><%# Eval("NombrePlan") %></td>
                                                                        <td>
                                                                            <asp:Literal ID="ltTiempoTranscurrido" runat="server"></asp:Literal></td>
                                                                        <td>
                                                                            <span title='<%# Eval("NombreEstadoCRM") %>' style='color: <%# Eval("ColorHexaCRM") %>'>
                                                                                <%# Eval("IconoMinEstadoCRM") %>
                                                                            </span><%# Eval("NombreEstadoCRM") %>
                                                                        </td>

                                                                        <td>
                                                                            <table class="table table-bordered table-striped">
                                                                                <tr>
                                                                                    <%-- <th width="25%"><i class="fa fa-city m-r-xs"></i>Ciudad</th>--%>
                                                                                    <th width="25%"><i class="fa fa-mobile m-r-xs"></i>Datos del contacto</th>
                                                                                    <th width="50%" class="text-nowrap"><i class="fa fa-clock m-r-xs"></i>Historial</th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>Identificación:<%# Eval("DocumentoAfiliado") %></td>
                                                                                    <td><%# Eval("HistorialHTML2") %></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="display: flex; flex-wrap: nowrap;">

                                                                            <a runat="server" id="btnEditar" href="#" class="btn btn-outline btn-primary pull-left m-r-xs"
                                                                                style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false" onclientclick="ocultarContador(); return true;">
                                                                                <i class="fa fa-edit"></i></a>
                                                                            <a runat="server" id="btnEliminar" href="#" class="btn btn-outline btn-danger pull-left m-r-xs"
                                                                                style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false"><i class="fa fa-trash"></i></a>
                                                                            <a runat="server" id="btnNuevoAfiliado" href="#" class="btn btn-outline btn-success pull-left"
                                                                                style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" data-idcrm='<%# Eval("idContacto") %>'
                                                                                data-documento='<%# Eval("DocumentoAfiliado") %>' onclick="redirigirNuevoAfiliado(this, event)">
                                                                                <i class="fa fa-id-card"></i></a>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>

                                                <div class="col-lg-7" id="divDatos" style="display: none;">
                                                    <div id="contact-1" class="tab-pane active">
                                                        <div class="row m-b-lg">
                                                            <div class="col-lg-4 text-center">
                                                                <h2>
                                                                    <asp:Literal ID="ltNombreContacto" runat="server"></asp:Literal></h2>

                                                                <div class="m-b-sm">
                                                                    <asp:Image ID="imgFoto" runat="server" CssClass="img-circle m-b-sm" Width="92px" />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-8">
                                                                <strong>Acerca del afiliado
                                                                </strong>
                                                                <br>Tipo de cliente:
                                                                    <asp:Literal ID="ltTipoAfiliado" runat="server"></asp:Literal></br>
                                                                Objetivo del afiliado:
                                                                <asp:Literal ID="ltObjetivo" runat="server"></asp:Literal>
                                                                </p>                                                                
                                                             
                                                                <div class="text-left">
                                                                    <asp:Button ID="btnActualizarYRedirigir" runat="server" Text="Comenzar Venta"
                                                                        OnClick="btnActualizarYRedirigir_Click" CssClass="btn btn-success btn-sm" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="client-detail">
                                                            <div class="full-height-scroll">

                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <strong>Actividad</strong>

                                                                        <ul class="list-group clear-list">
                                                                            <li class="list-group-item fist-item">
                                                                                <span class="pull-right">
                                                                                    <div style="margin-left: 20px;">
                                                                                        <asp:Literal ID="ltProximoContacto" runat="server"></asp:Literal>
                                                                                    </div>
                                                                                </span>
                                                                                <i class="fa fa-phone" style="margin-right: 5px; color: green;"></i>Por favor, contáctame al:
                                                                                <asp:Literal ID="ltTelefono" runat="server"></asp:Literal>
                                                                            </li>
                                                                            <li class="list-group-item">
                                                                                <span class="pull-right">
                                                                                    <asp:Literal ID="ltPlan" runat="server"></asp:Literal>
                                                                                </span>
                                                                                Plan Sugerido
                                                                            </li>
                                                                            <li class="list-group-item">
                                                                                <span class="pull-right">Tonificar </span>
                                                                                Objetivo del afiliado
                                                                            </li>
                                                                            <li class="list-group-item">
                                                                                <span class="pull-right">Pago en linea </span>
                                                                                Método de pago sugerido
                                                                            </li>
                                                                            <li class="list-group-item">
                                                                                <span class="pull-right">Whatsapp </span>
                                                                                Canal de marketing
                                                                            </li>
                                                                            <li class="list-group-item">
                                                                                <span class="pull-right">
                                                                                    <asp:Literal ID="ltPrimerContacto" runat="server"></asp:Literal>
                                                                                </span>
                                                                                Registrado en el CRM 
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                                <hr />
                                                                <strong>Historial de actividades</strong>
                                                                <div style="overflow-x: auto;">
                                                                    <asp:Literal ID="litHistorialHTML" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>


                                                <!-- Zona lateral izquierda para mostrar historial -->
                                                <!-- Contenedor para mostrar historial -->
                                                <%--                                                <div id="contenedorHistorial" style="border: 1px solid #ccc; padding: 10px; max-height: 400px; overflow-y: auto; overflow-x: hidden;">
                                                    <h5>Historial del contacto</h5>
                                                    <div id="historialHTMLVisual" style="font-size: 12px; word-wrap: break-word;"></div>
                                                </div>



                                                <div style="overflow-x: auto;">
                                                    <asp:Literal ID="litHistorialHTML" runat="server" />
                                                </div>--%>
                                            </div>
                                        </div>
                                        <div id="tab-2" class="tab-pane">
                                            <div class="panel-body">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <%--Zona lateral izquierda inf --%>
                                                        <%--   <div class="col-lg-5">
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
                                                        </div>--%>
                                                        <%--Zona lateral derecha inf --%>
                                                        <%--   <div class="col-lg-7">
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
                                                                                        Style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" CausesValidation="false" OnClick="btnEditarEmp_Click" CommandArgument='<%# Eval("idEmpresaCRM") %>' />
                                                                                </td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tbody>
                                                            </table>


                                                        </div>--%>
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
    <%--<script src="js/jquery-3.1.1.min.js"></script>--%>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>


    <!-- FooTable -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>


    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <!-- Page-Level Scripts -->
    <script src="js/plugins/clockpicker/clockpicker.js"></script>
    <%--    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-beta.1/dist/js/select2.min.js"></script>--%>

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <script>
        function pageLoad() {
            $('.chosen-select').chosen({ width: "100%", disable_search_threshold: 10, no_results_text: "Sin resultados" });
        }
    </script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>


    <script>
        $('.footable').footable();
        $('.clockpicker').clockpicker();
    </script>
    <script>
        $('#acordeonZonaLateralIzqSup').collapse('hide');
    </script>


    <script>
        let segundos = 0;
        let contadorIniciado = false; // <--- control para evitar doble setInterval

        function iniciarContador() {
            if (contadorIniciado) return; // ya se inició
            contadorIniciado = true;

            setInterval(() => {
                segundos++;

                const min = Math.floor(segundos / 60).toString().padStart(2, '0');
                const sec = (segundos % 60).toString().padStart(2, '0');

                const reloj = document.getElementById("reloj");
                if (reloj) reloj.textContent = `${min}:${sec}`;

                const hiddenField = document.getElementById("<%= hfContador.ClientID %>");
                if (hiddenField) hiddenField.value = segundos;

                if (segundos >= 300) reloj.style.color = '#1AB394';
                if (segundos >= 600) reloj.style.color = '#ED5565';

            }, 1000); // cada segundo
        }

        window.addEventListener("load", iniciarContador);
    </script>






    <%--    <script>
    function mueveReloj() {
        const ahora = new Date();
        let h = ahora.getHours().toString().padStart(2, '0');
        let m = ahora.getMinutes().toString().padStart(2, '0');
        let s = ahora.getSeconds().toString().padStart(2, '0');

        document.getElementById("reloj").textContent = `${h} : ${m} : ${s}`;

        setTimeout(mueveReloj, 1000);
    }
    </script>--%>





    <script>

        $.validator.setDefaults({ ignore: ":hidden:not(.chosen-select)" })

        $("#form").validate({
            rules: {



                txbTelefonoContacto: {
                    required: true,
                    minlength: 3
                },
                txbCorreoContacto: {
                    required: true,
                    minlength: 3
                },
                ddlEmpresa: {
                    required: true
                },
                ddlStatusLead: {
                    required: true
                },
                ddlTiposAfiliado: {
                    required: true
                },
                txbFechaPrim: {
                    required: true
                },
                txbFechaProx: {
                    required: true,
                },
                txbHoraIni: {
                    required: true,
                },
                ddlTipoPago: {
                    required: true,
                },
                ddlObjetivos: {
                    required: true,
                },
                ddlCanalesMarketing: {
                    required: true
                },
                ddlPlanes: {
                    required: true,
                },
                txbValorPropuesta: {
                    required: true,
                    minlength: 3
                },
            },
            messages: {
                ddlCiudadAfiliado: "*",
                ddlProfesiones: "*",
            }
        });

        $('.chosen-select').chosen({ width: "100%", disable_search_threshold: 10, no_results_text: "Sin resultados" });
    </script>


    <script type="text/javascript">
        function redirigirNuevoAfiliado(anchor, event) {
            event.preventDefault();

            const idcrm = anchor.getAttribute("data-idcrm");
            const documento = anchor.getAttribute("data-documento");
            console.log("IDCRM:", idcrm, "DOCUMENTO:", documento);

            if (!documento) {
                const url = `nuevoafiliado.aspx?idcrm=${encodeURIComponent(idcrm)}`;
                window.location.href = url;
                return;
            }

            $.getJSON("/obtenerafiliados?search=" + encodeURIComponent(documento), function (data) {
                console.log("Datos devueltos:", data);

                // ✅ Usa la propiedad id porque así se llama en tu JSON
                const existe = data.some(item => String(item.id) === String(documento));

                console.log("¿Existe?:", existe);

                const destino = existe ? "editarafiliado.aspx" : "nuevoafiliado.aspx";
                const url = `${destino}?idcrm=${encodeURIComponent(idcrm)}`;
                window.location.href = url;
            }).fail(function () {
                alert("Error al consultar los afiliados.");
            });
        }
    </script>




    <script>
        $(document).ready(function () {

            $(document).on("click", ".btnVerHistorial", function () {
                var historial = $(this).data("historial");
                $("#historialHTMLVisual").html(historial);
            });
        });
    </script>



    <script>
        $(document).ready(function () {
            const params = new URLSearchParams(window.location.search);
            const evento = params.get("evento");

            if (evento === "1") {
                $("#divTabla").hide();
                $("#divDatos").show();
                $("#tipoDocYDoc").hide();
                $("#btnAfiliadoBus").hide();
                $("#nombres").hide();
                $("#telefonoYCorreo").hide();
                $("#empresa").hide();
                $("#primerContacto").hide();
                $("#canalMarkYTipoCli").hide();
                $("#metodosPagoYObjetivos").hide();
                $("#planesYValorMes").show();
                $("#valorPropuestaYArchivo").hide();
                $("#btnCancelar_").hide();
                $("#btnVolverAgenda").show(); // Muestra el botón solo si viene del evento
                $("#generoyfechanac").hide(); 
                $("#estrategia").hide(); 

                const hoy = new Date().toLocaleDateString("es-CO"); // Formato local
                const mensaje = '✏️...';
                ;
                $("#txaObservaciones")
                    .val(mensaje)
                    .css({
                        "font-family": "Segoe UI, sans-serif",
                        "font-size": "14px",
                        "padding": "8px",
                        "border-radius": "6px",
                        "border": "1px solid #ccc",
                        "background-color": "#fdfdfd",
                        "color": "#333"
                    })
                    .attr("rows", 10) // 
                    .show();

            }
        });
    </script>

    <script>
        $(document).ready(function () {
            const params = new URLSearchParams(window.location.search);
            const idcrm = params.get("editid");
            const documento = params.get("documento");

            if (idcrm) {
                $("#btnNuevoAfiliado").attr("data-idcrm", idcrm);
            }
            if (documento) {
                $("#btnNuevoAfiliado").attr("data-documento", documento);
            }

            // Mostrar el botón solo si llegó desde evento
            if (params.get("evento") === "1") {
                $("#btnNuevoAfiliado").show();
            } else {
                $("#btnNuevoAfiliado").hide();
            }
        });
    </script>

    <script>
        $(document).ready(function () {
            // Obtener el valor del literal (HTML plano insertado)
            var documento = $("#ltDocumento").text().trim();

            // Asignarlo al botón
            $("#btnNuevoAfiliado").attr("data-documento", documento);
        });
    </script>

    <!-- Mainly scripts -->
    <%--    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>--%>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- Flot -->
    <script src="js/plugins/flot/jquery.flot.js"></script>
    <script src="js/plugins/flot/jquery.flot.tooltip.min.js"></script>
    <script src="js/plugins/flot/jquery.flot.spline.js"></script>
    <script src="js/plugins/flot/jquery.flot.resize.js"></script>
    <script src="js/plugins/flot/jquery.flot.pie.js"></script>
    <script src="js/plugins/flot/jquery.flot.symbol.js"></script>
    <script src="js/plugins/flot/curvedLines.js"></script>

    <!-- Peity -->
    <script src="js/plugins/peity/jquery.peity.min.js"></script>
    <script src="js/demo/peity-demo.js"></script>

    <%--    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>--%>

    <!-- jQuery UI -->
    <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>

    <!-- Jvectormap -->
    <script src="js/plugins/jvectormap/jquery-jvectormap-2.0.2.min.js"></script>
    <script src="js/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"></script>

    <!-- Sparkline -->
    <script src="js/plugins/sparkline/jquery.sparkline.min.js"></script>

    <!-- Sparkline demo data  -->
    <script src="js/demo/sparkline-demo.js"></script>

    <!-- ChartJS-->
    <script src="js/plugins/chartJs/Chart.min.js"></script>

    <script>
        $(document).ready(function () {

            var lineData = {
                labels: ["January", "February", "March", "April", "May", "June", "July"],
                datasets: [
                    {
                        label: "Ventas completadas",
                        backgroundColor: "rgba(26,179,148,0.5)",
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: [280, 480, 400, 190, 860, 270, 900]
                    },
                    {
                        label: "Meta propuesta",
                        backgroundColor: "rgba(220,220,220,0.5)",
                        borderColor: "rgba(220,220,220,1)",
                        pointBackgroundColor: "rgba(220,220,220,1)",
                        pointBorderColor: "#fff",
                        data: [650, 590, 800, 810, 560, 550, 400]
                    }
                ]
            };

            var lineOptions = {
                responsive: true
            };


            var ctx = document.getElementById("lineChart").getContext("2d");
            new Chart(ctx, { type: 'line', data: lineData, options: lineOptions });

        });
    </script>

    <script>
        $(document).ready(function () {
            $('#<%= ddlStatusLead.ClientID %>').select2({
                templateResult: formatOptionWithIcon,
                templateSelection: formatOptionWithIcon,
                width: '100%'
            });

            function formatOptionWithIcon(option) {
                if (!option.id) return option.text;

                const $option = $(option.element);
                const iconClass = $option.data('icon') || '';
                const color = $option.data('color') || '#000';

                return $(
                    `<span style="color:${color};">
          <i class="${iconClass}" style="margin-right: 6px;"></i>${option.text}
        </span>`
                );
            }
        });
    </script>

    <script>
        $(document).ready(function () {
            $('#txbDocumento').on('change blur', function () {
                var documento = $(this).val().trim();
                if (documento.length === 0) return;

                var url = 'https://pqrdsuperargo.supersalud.gov.co/api/api/adres/0/' + documento;

                // Limpia primero los campos
                $('#txbNombreContacto').val('');
                $('#txbApellidoContacto').val('');
                // $('#txaObservaciones').val('Consultando...');

                $.ajax({
                    url: url,
                    method: 'GET',
                    success: function (data) {
                        // Nombres
                        var nombreCompleto = [data.nombre, data.s_nombre].filter(Boolean).join(' ').toUpperCase();
                        var apellidoCompleto = [data.apellido, data.s_apellido].filter(Boolean).join(' ').toUpperCase();

                        $('#txbNombreContacto').val(nombreCompleto);
                        $('#txbApellidoContacto').val(apellidoCompleto);
                        $('#txbEdad').val((data.edad != null ? data.edad + ' años' : ''));
                        $('#txbFecNac').val((data.fecha_nacimiento));
                        $('#ddlGenero').val(data.sexo);

                        // Campos restantes (omitimos nombre y apellidos)
                        //    var observaciones = `
                        //    Nro. Documento: ${data.numero_doc}
                        //    Sexo: ${data.sexo == 1 ? 'Masculino' : 'Femenino'}
                        //    Correo: ${data.correo || '-'}
                        //    Municipio ID: ${data.municipio_id}
                        //    Departamento ID: ${data.departamento_id}
                        //    EPS: ${data.eps}
                        //    Tipo de Afiliado: ${data.tipo_de_afiliado}
                        //    Estado Afiliación: ${data.estado_afiliacion}
                        //`   .trim();

                        //    $('#txaObservaciones').val(observaciones);
                    },
                    error: function (xhr, status, error) {
                        $('#txaObservaciones').val('Error al consultar la información.');
                        console.error("Error:", error);
                    }
                });
            });
        });
    </script>

    <script type="text/javascript">
        var idUsuario = '<%= Session["idUsuario"] %>';
    </script>

    <script>
        $('#txbDocumento').on('change blur', function () {
            var documento = $(this).val().trim();
            if (documento.length === 0) return;

            $.ajax({
                type: "POST",
                url: "crmnuevocontacto.aspx/ValidarContacto",
                data: JSON.stringify({ documento: documento, idUsuario: parseInt(idUsuario) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    const result = response.d;

                    function limpiarCamposContacto() {
                        $('#txbNombreContacto').val('');
                        $('#txbApellidoContacto').val('');
                        $('#txbCorreoContacto').val('');
                        $('#txbTelefonoContacto').val('');
                    }

                    if (result === "bloqueado") {
                        Swal.fire({
                            icon: 'warning',
                            title: 'Atención',
                            text: 'El contacto está siendo gestionado por otro asesor.'
                        });
                        limpiarCamposContacto();
                    }
                    else if (result === "propio") {
                        Swal.fire({
                            icon: 'info',
                            title: 'CRM en gestión por usted',
                            text: 'Ya tienes un contacto creado para este documento.'
                        });
                    }
                    else if (result === "planVendido") {
                        Swal.fire({
                            icon: 'success',
                            title: 'Contacto con plan activo',
                            text: 'El contacto ya tiene un plan vendido o una negociación aceptada.'
                        });
                        limpiarCamposContacto();
                    }
                    else if (result === "ok") {
                        console.log("Contacto libre, puede continuar");
                    }
                },
                error: function () {
                    console.error("Error al validar el contacto");
                }
            });
        });
    </script>



</body>

</html>

