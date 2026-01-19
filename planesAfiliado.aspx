<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="planesAfiliado.aspx.cs" Inherits="fpWebApp.planesAfiliado" Async="true" %>

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

    <title>Fitness People | Plan afiliado</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />
    <link href="css/plugins/ionRangeSlider/ion.rangeSlider.css" rel="stylesheet">
    <link href="css/plugins/ionRangeSlider/ion.rangeSlider.skinNice.css" rel="stylesheet">

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <link href="css/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css" rel="stylesheet" />

    <!-- Sweet Alert -->
    <link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">

    <!-- Sweet alert -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#afiliados");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#afiliados2");
            element2.classList.remove("collapse");
        }
    </script>
</head>

<body onload="changeClass()" class="pace-done mini-navbar">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <i class="fa fa-ticket modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para asignar plan</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar los planes de manera clara y eficiente.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Visualiza la información del afiliado</b><br />
                        <i class="fa-solid fa-magnifying-glass"></i>Revisa los datos:
                        <br />
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i><b>Nombre, </b>
                        <i class="fa-solid fa-envelope" style="color: #0D6EFD;"></i><b>Correo, </b>
                        <i class="fa-solid fa-city" style="color: #0D6EFD;"></i><b>Ciudad, </b>
                        <i class="fa-solid fa-mobile" style="color: #0D6EFD;"></i><b>Teléfono, </b>
                        <i class="fa-solid fa-building" style="color: #0D6EFD;"></i><b>Sede, </b>
                        <i class="fa-solid fa-cake" style="color: #0D6EFD;"></i><b>Cumpleaños, </b>
                        <br />
                        <i class="fa-solid fa-shield" style="color: #0D6EFD;"></i><b>Estado, </b>
                        <i class="fa-solid fa-calendar-day" style="color: #0D6EFD;"></i><b>Días asistidos, Congelaciones y</b>
                        <i class="fa-solid fa-ticket" style="color: #0D6EFD;"></i><b>Planes registrados</b>,
                    <br />
                        <br />
                        <b>Paso 2: Asigna un plan al afiliado</b><br />
                        <i class="fa-solid fa-ticket" style="color: #21B9BB;"></i>Selecciona el <b>plan</b>.<br />
                        <i class="fa-solid fa-ticket" style="color: #EC4758;"></i>Selecciona la <b>cantidad de meses</b> a la que se registrará el plan.
                    <br />
                        <br />
                        <b>Paso 3: Visualiza los precios del plan</b><br />
                        Puedes ver estos <b>precios</b> en:<br />
                        <i class="fa-solid fa-money-bill-wave" style="color: #23C6C8;"></i><b>Valor x mes</b><br />
                        <i class="fa-solid fa-tag" style="color: #ED5565;"></i><b>Descuento en % y $</b><br />
                        <i class="fa-solid fa-cart-shopping" style="color: #F8AC59;"></i><b>Valor total</b><br />
                        <i class="fa-solid fa-hand-holding-dollar" style="color: #1C84C6;"></i><b>Ahorro</b>
                        <br />
                        <br />
                        <b>Paso 4: Termina el proceso</b><br />
                        <i class="fa-solid fa-gift" style="color: #21B9BB;"></i>Selecciona los <b>días de cortesía</b>.<br />
                        <i class="fa-solid fa-gift" style="color: #EC4758;"></i>Selecciona el <b>regalo</b>.<br />
                        <i class="fa-solid fa-magnifying-glass"></i>Revisa que los <b>detalles del plan</b> sean los correctos.<br />
                        <i class="fa-solid fa-money-bill"></i>Completa los campos del <b>método de pago</b>.
                    <br />
                        <br />
                        <b>Paso 5: Confirma o cancela</b><br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i><b>Agregar Plan:</b> Guarda la información y <b>registra</b> el plan al afiliado.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i><b>Cancelar:</b> Si necesitas volver <b>atrás</b> sin guardar cambios.
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

    <div class="modal fade" id="ModalDetalleWompi" tabindex="-1" aria-labelledby="miModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <h5 class="modal-title">Detalle de la transacción</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>
                        <asp:Literal ID="ltDetalleWompi" runat="server"></asp:Literal>
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
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
                    <h2><i class="fa fa-ticket text-success m-r-sm"></i>Plan afiliado</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Afiliados</li>
                        <li class="active"><strong>Plan afiliado</strong></li>
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

                    <div class="row m-b-lg m-t-lg">
                        <div class="col-md-6">

                            <div class="profile-image">
                                <asp:Literal ID="ltFoto" runat="server"></asp:Literal>
                            </div>
                            <div class="profile-info">
                                <div class="">
                                    <div>
                                        <h2 class="no-margins">
                                            <asp:Literal ID="ltNombre" runat="server"></asp:Literal>
                                            <asp:Literal ID="ltApellido" runat="server"></asp:Literal>
                                        </h2>
                                        <h4>
                                            <asp:Literal ID="ltEmail" runat="server"></asp:Literal></h4>
                                        <small class="text-capitalize">
                                            <asp:Literal ID="ltDireccion" runat="server"></asp:Literal>, 
                                            <asp:Literal ID="ltCiudad" runat="server"></asp:Literal></small>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <table class="table small m-b-xs">
                                <tbody>
                                    <tr>
                                        <td><strong><i class="fab fa-whatsapp m-r-sm"></i></strong>
                                            <asp:Literal ID="ltCelular" runat="server"></asp:Literal></td>
                                        <td><strong><i class="fa fa-shield m-r-sm"></i></strong>Estado: 
                                            <asp:Literal ID="ltEstado" runat="server"></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td><strong><i class="fa fa-building m-r-sm"></i></strong>Sede: 
                                            <asp:Literal ID="ltSede" runat="server"></asp:Literal></td>
                                        <td><strong>
                                            <asp:Literal ID="ltAsistencias" runat="server"></asp:Literal></strong> Asistencias</td>
                                    </tr>
                                    <tr>
                                        <td><strong><i class="fa fa-cake m-r-sm"></i></strong>
                                            <asp:Literal ID="ltCumple" runat="server"></asp:Literal></td>
                                        <td><strong>
                                            <asp:Literal ID="ltCongelaciones" runat="server"></asp:Literal></strong> Congelaciones</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5><i class="fa fa-ticket m-r-xs"></i>Planes del afiliado</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <ul class="todo-list small-list">
                                <asp:Repeater ID="rpPlanesAfiliado" runat="server" OnItemDataBound="rpPlanesAfiliado_ItemDataBound">
                                    <ItemTemplate>
                                        <li>
                                            <div class="i-checks">
                                                <small class="label label-primary pull-right"><%# Eval("DiasQueFaltan") %> días disponibles</small>
                                                <label>
                                                    <%# Eval("NombrePlan") %>
                                                </label>
                                                <br />
                                                <div class="progress progress-striped active" style="margin-bottom: 0px;">
                                                    <div style='width: <%# Eval("Porcentaje1") %>%' class="progress-bar progress-bar-success"></div>
                                                    <div style='width: <%# Eval("Porcentaje2") %>%' class="progress-bar progress-bar-warning"></div>
                                                </div>
                                                <small class="text-muted"><%# Eval("FechaInicioPlan", "{0:dd MMM yyyy}") %> - <%# Eval("FechaFinalPlan", "{0:dd MMM yyyy}") %></small>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <%-- Etiqueta para mostrar que no hay registros --%>
                                        <asp:Label ID="lblSinRegistros" runat="server" Text="El afiliado no tiene planes asignados." Visible="false">
                                        </asp:Label>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>

                    <div class="ibox float-e-margins" runat="server" id="divContenido">
                        <div class="ibox-title">
                            <h5>Agregar un plan al afiliado</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">

                            <div class="row">
                                <form role="form" id="form" runat="server">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <div class="col-sm-12 m-t-xs">
                                        <asp:UpdatePanel ID="upPlanes" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-sm-8 b-r">
                                                        <div class="form-group">
                                                            <%--<label>Tipo de plan:</label>--%>
                                                            <div class="form-group">
                                                                <asp:Timer ID="tmrRespuesta" runat="server" Interval="4000" Enabled="false" OnTick="tmrRespuesta_Tick" />
                                                                <%--<asp:PlaceHolder ID="phPlanes" runat="server"></asp:PlaceHolder>--%>
                                                                <table class="footable table table-striped" data-paging-size="10"
                                                                    data-filter-min="3" data-filter-placeholder="Buscar"
                                                                    data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}"
                                                                    data-paging-limit="10" data-filtering="true"
                                                                    data-filter-container="#filter-form-container" data-filter-delay="300"
                                                                    data-filter-dropdown-title="Buscar en:" data-filter-position="left"
                                                                    data-empty="Sin resultados">
                                                                    <thead>
                                                                        <tr>
                                                                            <th style="width: 100px;">Nombre</th>
                                                                            <th data-breakpoints="xs" style="width: 500px;">Descripción</th>
                                                                            <th data-breakpoints="xs">Vigencia</th>
                                                                            <th data-breakpoints="xs" class="text-right">Precio</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:Repeater ID="rpPlanes" runat="server" OnItemCommand="rpPlanes_ItemCommand" OnItemDataBound="rpPlanes_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <tr class="feed-element">
                                                                                    <td>
                                                                                        <asp:LinkButton runat="server" ID="btnSeleccionarPlan" CommandArgument='<%# Eval("idPlan") %>'
                                                                                            CommandName="SeleccionarPlan"><%# Eval("NombrePlan") %></asp:LinkButton>
                                                                                    <td><i class="fa fa-note-sticky m-r-xs font-bold"></i><%# Eval("DescripcionPlan") %></td>
                                                                                    <td><%# Eval("Vigencia") %></td>
                                                                                    <td style="text-align: right;">$<%# Eval("PrecioTotal","{0:N0}") %></td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label>Días de cortesía:</label>
                                                            <div class="form-group">
                                                                <asp:Button ID="btn7dias" runat="server" Text="7" CssClass="btn btn-info dim btn-large-dim btn-outline"
                                                                    Style="width: 70px; font-size: 30px; height: 70px;" OnClick="btn7dias_Click" Enabled="false" />
                                                                <asp:Button ID="btn15dias" runat="server" Text="15" CssClass="btn btn-info dim btn-large-dim btn-outline"
                                                                    Style="width: 70px; font-size: 30px; height: 70px;" OnClick="btn15dias_Click" Enabled="false" />
                                                                <asp:Button ID="btn30dias" runat="server" Text="30" CssClass="btn btn-info dim btn-large-dim btn-outline"
                                                                    Style="width: 70px; font-size: 30px; height: 70px;" OnClick="btn30dias_Click" Enabled="false" />
                                                                <asp:Button ID="btn60dias" runat="server" Text="60" CssClass="btn btn-info dim btn-large-dim btn-outline"
                                                                    Style="width: 70px; font-size: 30px; height: 70px;" OnClick="btn60dias_Click" Enabled="false" />
                                                                <asp:Button ID="btn90dias" runat="server" Text="90" CssClass="btn btn-info dim btn-large-dim btn-outline"
                                                                    Style="width: 70px; font-size: 30px; height: 70px;" OnClick="btn90dias_Click" Enabled="false" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--<div class="col-sm-5">
                                                        <div class="form-group">
                                                            <label for="ddlRegalos">Regalos:</label>
                                                            <div class="form-group">
                                                                <asp:LinkButton ID="btnRegalo1" runat="server" 
                                                                    CssClass="btn btn-danger dim btn-large-dim btn-outline disabled"
                                                                    Style="width: 70px; font-size: 30px; height: 70px;"><i class="fa fa-bottle-water"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="btnRegalo2" runat="server" 
                                                                    CssClass="btn btn-danger dim btn-large-dim btn-outline disabled"
                                                                    Style="width: 70px; font-size: 30px; height: 70px;"><i class="fa fa-tshirt"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="btnRegalo3" runat="server" 
                                                                    CssClass="btn btn-danger dim btn-large-dim btn-outline disabled"
                                                                    Style="width: 70px; font-size: 30px; height: 70px;"><i class="fa fa-laptop-medical"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-lg-12">
                                                                        <div class="widget style1 lazur-bg">
                                                                            <div class="row vertical-align">
                                                                                <div class="col-xs-3">
                                                                                    <i class="fa fa-money-bill-wave fa-3x" style="font-size: 2.3em"></i>
                                                                                </div>
                                                                                <div class="col-xs-9 text-right">
                                                                                    <span>Valor mes base</span>
                                                                                    <h2 class="font-bold">
                                                                                        <asp:Literal ID="ltPrecioBase" runat="server"></asp:Literal></h2>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-12">
                                                                        <div class="widget style1 bg-danger">
                                                                            <div class="row vertical-align">
                                                                                <div class="col-xs-3">
                                                                                    <i class="fa fa-tag fa-3x" style="font-size: 2.3em"></i>
                                                                                </div>
                                                                                <div class="col-xs-9 text-right">
                                                                                    <span>Descuento
                                                                                        <asp:Literal ID="ltDescuento" runat="server"></asp:Literal></span>
                                                                                    <h2 class="font-bold">
                                                                                        <asp:Literal ID="ltConDescuento" runat="server"></asp:Literal></h2>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-12">
                                                                        <div class="widget style1 bg-success">
                                                                            <div class="row vertical-align">
                                                                                <div class="col-xs-3">
                                                                                    <i class="fa fa-cart-shopping fa-3x" style="font-size: 2.3em"></i>
                                                                                </div>
                                                                                <div class="col-xs-9 text-right">
                                                                                    <span>Total </span>
                                                                                    <h2 class="font-bold">
                                                                                        <asp:Literal ID="ltPrecioFinal" runat="server"></asp:Literal></h2>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-12">
                                                                        <div class="widget style1 yellow-bg">
                                                                            <div class="row vertical-align">
                                                                                <div class="col-xs-3">
                                                                                    <i class="fa fa-hand-holding-dollar fa-3x" style="font-size: 2.3em"></i>
                                                                                </div>
                                                                                <div class="col-xs-9 text-right">
                                                                                    <span>Ahorro </span>
                                                                                    <h2 class="font-bold">
                                                                                        <asp:Literal ID="ltAhorro" runat="server"></asp:Literal></h2>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-lg-12">
                                                                        <div class="widget style1 bg-primary">
                                                                            <div class="row vertical-align">
                                                                                <div class="col-xs-3">
                                                                                    <i class="fa fa-credit-card fa-3x" style="font-size: 2.3em"></i>
                                                                                </div>
                                                                                <div class="col-xs-9 text-right">
                                                                                    <span>Tipo de plan </span>
                                                                                    <h2 class="font-bold">
                                                                                        <asp:Literal ID="ltTipoPlan" runat="server"></asp:Literal></h2>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr />

                                                <hr />
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <div class="panel panel-default" runat="server" id="divPanelResumen">
                                                                <div class="panel-heading">
                                                                    <i class="fa fa-ticket m-r-sm"></i>
                                                                    <asp:Literal ID="ltNombrePlan" runat="server"></asp:Literal>
                                                                </div>
                                                                <div class="panel-body">
                                                                    <p>
                                                                        <asp:Literal ID="ltDescripcion" runat="server"></asp:Literal>
                                                                        <asp:Literal ID="ltObservaciones" runat="server"></asp:Literal>
                                                                        <hr />
                                                                        <asp:Literal ID="ltCortesias" runat="server"></asp:Literal>
                                                                        <asp:Literal ID="ltRegalos" runat="server"></asp:Literal>
                                                                    </p>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="form-group" style="margin-bottom: 5px;">
                                                            <label>Fecha de inicio:</label>
                                                            <asp:TextBox ID="txbFechaInicio" CssClass="form-control input-sm" runat="server" name="txbFechaInicio"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-8">

                                                        <%--<div class="form-group">
                                                            <label><strong>Seleccione un método de pago:</strong></label>
                                                            <asp:RadioButtonList ID="rblMetodoPago" runat="server" RepeatDirection="Horizontal" CssClass="form-control input-sm" onchange="mostrarMetodoSeleccionado(this)" Visible="false">
                                                                <asp:ListItem Text="&nbsp;Pago en línea&nbsp;&nbsp;&nbsp;" Value="wompi" />
                                                                <asp:ListItem Text="&nbsp;Datafono&nbsp;&nbsp;&nbsp;" Value="datafono" />
                                                                <asp:ListItem Text="&nbsp;Efectivo&nbsp;&nbsp;&nbsp;" Value="efectivo" />
                                                                <asp:ListItem Text="&nbsp;Transferencia" Value="transferencia" />
                                                                <%--<asp:ListItem Text="Pago mixto" Value="combinado" />
                                                            </asp:RadioButtonList>
                                                        </div>--%>

                                                        <div>
                                                            <table class="table">
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <button type="button" class="btn btn-warning m-r-sm">
                                                                                <i class="fa fa-laptop"></i>
                                                                            </button>
                                                                            <span class="font-bold">Online</span>
                                                                        </td>
                                                                        <td>
                                                                            <h4><small>Enlace de pago:</small></h4>
                                                                            <br />
                                                                            <asp:Label ID="lbEnlaceWompi" runat="server"></asp:Label>
                                                                            <asp:HiddenField ID="hdEnlaceWompi" runat="server" />
                                                                            <button class="btn btn-success btn-circle" visible="false" id="btnPortapaleles"
                                                                                onclick="copyToClipboard()" runat="server" title="Copiar enlace">
                                                                                <i class="fa fa-copy"></i>
                                                                            </button>
                                                                        </td>
                                                                        <td>
                                                                            <a class="dropdown-toggle count-info btn btn-info btn-xs" data-toggle="modal" href="#" data-target="#ModalDetalleWompi"><i class="fa fa-search m-r-xs"></i>Verificar pago</a>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txbWompi" CssClass="form-control input-sm"
                                                                                runat="server" OnTextChanged="txbWompi_TextChanged" Text="$0"
                                                                                onclick="if(this.value === '$0') this.value=''" placeholder="$0"
                                                                                onkeyup="formatCurrency(this)" autocomplete="off"
                                                                                onblur="if(this.value.replace(/\D/g, '') === '') this.value = '$0'; else keepFormatted(this);"
                                                                                AutoPostBack="true" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <button type="button" class="btn btn-primary m-r-sm">
                                                                                <i class="fa fa-credit-card"></i>
                                                                            </button>
                                                                            <span class="font-bold">Datafono</span>
                                                                        </td>
                                                                        <%--<td></td>--%>
                                                                        <td colspan="2">
                                                                            <asp:TextBox ID="txbNroAprobacion" CssClass="form-control input-sm"
                                                                                runat="server" placeholder="Ref."></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txbDatafono" CssClass="form-control input-sm"
                                                                                runat="server" OnTextChanged="txbDatafono_TextChanged" Text="$0"
                                                                                onclick="if(this.value === '$0') this.value=''" placeholder="$0"
                                                                                onkeyup="formatCurrency(this)" autocomplete="off"
                                                                                onblur="if(this.value.replace(/\D/g, '') === '') this.value = '$0'; else keepFormatted(this);"
                                                                                AutoPostBack="true" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            <button type="button" class="btn btn-success m-r-sm">
                                                                                <i class="fa fa-money-bill-wave"></i>
                                                                            </button>
                                                                            <span class="font-bold">Efectivo</span>
                                                                        </td>
                                                                        <%--<td></td>
                                                                        <td></td>--%>
                                                                        <td>
                                                                            <asp:TextBox ID="txbEfectivo" CssClass="form-control input-sm"
                                                                                runat="server" OnTextChanged="txbEfectivo_TextChanged" Text="$0"
                                                                                onclick="if(this.value === '$0') this.value=''" placeholder="$0"
                                                                                onkeyup="formatCurrency(this)" autocomplete="off"
                                                                                onblur="if(this.value.replace(/\D/g, '') === '') this.value = '$0'; else keepFormatted(this);"
                                                                                AutoPostBack="true" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="25%">
                                                                            <button type="button" class="btn btn-danger m-r-sm">
                                                                                <i class="fa fa-university"></i>
                                                                            </button>
                                                                            <span class="font-bold">Transferencia</span>
                                                                        </td>
                                                                        <%--<td></td>--%>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rblBancos" runat="server"
                                                                                RepeatDirection="Horizontal" CssClass="form-control input-sm" Visible="false">
                                                                                <asp:ListItem Text="&nbsp;&nbsp;Bancolombia" Value="Bancolombia" style="margin-right: 5px; font-size: 10px;"></asp:ListItem>
                                                                                <asp:ListItem Text="&nbsp;&nbsp;Davivienda" Value="Davivienda" style="margin-right: 5px; font-size: 10px;"></asp:ListItem>
                                                                                <asp:ListItem Text="&nbsp;&nbsp;BBVA" Value="BBVA" style="margin-right: 5px; font-size: 10px;"></asp:ListItem>
                                                                                <asp:ListItem Text="&nbsp;&nbsp;Bogotá" Value="Bogota" style="margin-right: 5px; font-size: 10px;"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                            <asp:Button ID="btnBancolombia" runat="server" Text="Bancolombia" CssClass="btn btn-info btn-outline"
                                                                                Style="font-size: 10px;" OnClick="btnBancolombia_Click" />
                                                                            <asp:Button ID="btnDavivienda" runat="server" Text="Davivienda" CssClass="btn btn-danger btn-outline"
                                                                                Style="font-size: 10px;" OnClick="btnDavivienda_Click"/>
                                                                            <asp:Button ID="btnBBVA" runat="server" Text="BBVA" CssClass="btn btn-warning btn-outline"
                                                                                Style="font-size: 10px;" OnClick="btnBBVA_Click" />
                                                                            <asp:Button ID="btnBogota" runat="server" Text="Bogotá" CssClass="btn btn-success btn-outline"
                                                                                Style="font-size: 10px;" OnClick="btnBogota_Click" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txbReferencia" CssClass="form-control input-sm"
                                                                                runat="server" placeholder="Ref."></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txbTransferencia" CssClass="form-control input-sm"
                                                                                runat="server" OnTextChanged="txbTransferencia_TextChanged" Text="$0"
                                                                                onclick="if(this.value === '$0') this.value=''" placeholder="$0"
                                                                                onkeyup="formatCurrency(this)" autocomplete="off"
                                                                                onblur="if(this.value.replace(/\D/g, '') === '') this.value = '$0'; else keepFormatted(this);"
                                                                                AutoPostBack="true" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <button type="button" class="btn btn-secondary m-r-sm">
                                                                                <i class="fa fa-coins"></i>
                                                                            </button>
                                                                            <span class="font-bold">Crédito</span>
                                                                        </td>
                                                                        <%--<td></td>--%>
                                                                        <td colspan="2">
                                                                            <asp:DropDownList ID="ddlEmpresa" DataTextField="NombreEmpresaCRM" DataValueField="idEmpresaCRM"
                                                                                runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                                <asp:ListItem Text="N/A" Value="0"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txbCredito" CssClass="form-control input-sm"
                                                                                runat="server" Text="$0" 
                                                                                onclick="if(this.value === '$0') this.value=''" placeholder="$0"
                                                                                onkeyup="formatCurrency(this)" autocomplete="off"
                                                                                onblur="if(this.value.replace(/\D/g, '') === '') this.value = '$0'; else keepFormatted(this);"
                                                                                AutoPostBack="true" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>

                                                        <hr />

                                                        <!-- Total -->
                                                        <div class="form-group" style="margin-bottom: 5px;">
                                                            <h1>
                                                                <label class="col-lg-5 control-label">
                                                                    TOTAL:
                                                                    <asp:Literal ID="ltValorTotal" runat="server"></asp:Literal>
                                                                </label>
                                                            </h1>
                                                            <div class="col-lg-2">
                                                                <asp:CheckBox ID="cbPagaCounter" runat="server" Text="&nbsp;Paga en counter" OnCheckedChanged="cbPagaCounter_CheckedChanged" AutoPostBack="true" />
                                                            </div>
                                                            <div class="col-lg-5">
                                                                <asp:TextBox ID="txbTotal" CssClass="form-control input-sm"
                                                                    runat="server" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div>
                                                    <button id="btnCancelar" class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button" onclick="window.location.href='afiliados'" runat="server"><strong>Cancelar</strong></button>
                                                    <button id="btnVolver" runat="server" type="button" class="btn btn-sm btn-info pull-right m-t-n-xs" onclick="window.location.href='agendacrm.aspx';">
                                                        Regresar a Agenda CRM</button>
                                                    <asp:LinkButton ID="lbAgregarPlan" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right"
                                                        OnClick="lbAgregarPlan_Click" OnClientClick="return confirmarVenta(event);">
                                                        <i class="fa fa-ticket"></i> Agregar plan
                                                    </asp:LinkButton>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12">
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

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- IonRangeSlider -->
    <script src="js/plugins/ionRangeSlider/ion.rangeSlider.min.js"></script>

    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Peity -->
    <script src="js/plugins/peity/jquery.peity.min.js"></script>
    <script src="js/demo/peity-demo.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <script>

        function copyToClipboard() {
            // Get the text field
            var copyText = document.getElementById("hdEnlaceWompi");

            // Select the text field
            copyText.select();
            //copyText.setSelectionRange(0, 99999); // For mobile devices

            // Copy the text inside the text field
            navigator.clipboard.writeText(copyText.value);
        }

        //var ddlRegalos = document.getElementById("ddlRegalos");
        //var check15 = document.getElementById("check15");
        //ddlRegalos.setAttribute("disabled", true);
        //check15.setAttribute("checked", false);



        //$("#ionrange_1").ionRangeSlider({
        //    grid: true,
        //    values: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"],
        //    onChange: function (data) {
        //        // fired on every range slider update
        //        console.dir(data.fromNumber);
        //        if (data.fromNumber >= 6) {
        //            console.log(data.fromNumber);
        //            ddlRegalos.removeAttribute('disabled');
        //            check15.setAttribute("checked", true);
        //        }
        //        else {
        //            ddlRegalos.setAttribute("disabled", true);
        //            check15.setAttribute("checked", false);
        //        }
        //    },
        //});

        $(document).ready(function () {
            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
            });
        });

        $("#form").validate({
            rules: {
                txbFechaInicio: {
                    required: true,
                },
            }
        });

    </script>

    <script type="text/javascript">
        function mostrarMetodoSeleccionado(radioList) {
            // Obtener el valor seleccionado
            const seleccion = radioList.querySelector('input[type=radio]:checked').value;

            // Ocultar todos los contenedores de método de pago
            document.querySelectorAll('.metodo-pago').forEach(div => {
                div.style.display = 'none';
            });

            // Mostrar el contenedor correspondiente
            const divId = 'div' + seleccion.charAt(0).toUpperCase() + seleccion.slice(1); // ej: 'combinado' -> 'divCombinado'
            const selectedDiv = document.getElementById(divId);
            if (selectedDiv) {
                selectedDiv.style.display = 'block';
            }
        }

        // Ejecutar al cargar la página
        <%--window.onload = function () {
            const rbl = document.getElementById('<%= rblMetodoPago.ClientID %>');
            if (rbl) mostrarMetodoSeleccionado(rbl);
        };--%>
    </script>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script>
        function confirmarVenta(e) {

            // Detener el postback por defecto
            if (e) e.preventDefault();

            Swal.fire({
                title: 'Confirmar venta',
                text: 'Esta acción generará la venta. ¿Desea continuar?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, continuar',
                cancelButtonText: 'Cancelar'
            }).then((result) => {

                if (result.isConfirmed) {
                    // FORZAR el postback manualmente
                    __doPostBack('<%= lbAgregarPlan.UniqueID %>', '');
                }
            });

            return false; // Evita que se ejecute el postback inmediato
        }
    </script>







</body>

</html>
