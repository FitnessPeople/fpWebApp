<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="planesAfil.aspx.cs" Inherits="fpWebApp.planesAfil" %>

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
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
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

    <script>
        function changeClass() {
            var element1 = document.querySelector("#afiliados1");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#afiliados2");
            element2.classList.remove("collapse");
        }
    </script>
</head>

<body onload="changeClass()">
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
                        <i class="fa-solid fa-magnifying-glass"></i> Revisa los datos: <br />
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i> <b>Nombre, </b>
                        <i class="fa-solid fa-envelope" style="color: #0D6EFD;"></i> <b>Correo, </b>
                        <i class="fa-solid fa-city" style="color: #0D6EFD;"></i> <b>Ciudad, </b>
                        <i class="fa-solid fa-mobile" style="color: #0D6EFD;"></i> <b>Teléfono, </b>
                        <i class="fa-solid fa-building" style="color: #0D6EFD;"></i> <b>Sede, </b>
                        <i class="fa-solid fa-cake" style="color: #0D6EFD;"></i> <b>Cumpleaños, </b><br />
                        <i class="fa-solid fa-shield" style="color: #0D6EFD;"></i> <b>Estado, </b>
                        <i class="fa-solid fa-calendar-day" style="color: #0D6EFD;"></i> <b>Días asistidos, Congelaciones y</b>
                        <i class="fa-solid fa-ticket" style="color: #0D6EFD;"></i> <b>Planes registrados</b>,
                    <br />
                        <br />
                        <b>Paso 2: Asigna un plan al afiliado</b><br />
                        <i class="fa-solid fa-ticket" style="color: #21B9BB;"></i> Selecciona el <b>plan</b>.<br />
                        <i class="fa-solid fa-ticket" style="color: #EC4758;"></i> Selecciona la <b>cantidad de meses</b> a la que se registrará el plan.
                    <br />
                        <br />
                        <b>Paso 3: Visualiza los precios del plan</b><br />
                        Puedes ver estos <b>precios</b> en:<br />
                        <i class="fa-solid fa-money-bill-wave" style="color: #23C6C8;"></i> <b>Valor x mes</b><br />
                        <i class="fa-solid fa-tag" style="color: #ED5565;"></i> <b>Descuento en % y $</b><br />
                        <i class="fa-solid fa-cart-shopping" style="color: #F8AC59;"></i> <b>Valor total</b><br />
                        <i class="fa-solid fa-hand-holding-dollar" style="color: #1C84C6;"></i> <b>Ahorro</b>
                    <br />
                        <br />
                        <b>Paso 4: Termina el proceso</b><br />
                        <i class="fa-solid fa-gift" style="color: #21B9BB;"></i> Selecciona los <b>días de cortesía</b>.<br />
                        <i class="fa-solid fa-gift" style="color: #EC4758;"></i> Selecciona el <b>regalo</b>.<br />
                        <i class="fa-solid fa-magnifying-glass"></i> Revisa que los <b>detalles del plan</b> sean los correctos.<br />
                        <i class="fa-solid fa-money-bill"></i> Completa los campos del <b>método de pago</b>.
                    <br />
                        <br />
                        <b>Paso 5: Confirma o cancela</b><br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i> <b>Agregar Plan:</b> Guarda la información y <b>registra</b> el plan al afiliado.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i> <b>Cancelar:</b> Si necesitas volver <b>atrás</b> sin guardar cambios.
                   <br />
                        <br />
                        <i class="fa fa-exclamation-circle mr-2"></i> Si tienes dudas, no dudes en consultar con el administrador del sistema.
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
                    <p><asp:Literal ID="ltDetalleWompi" runat="server"></asp:Literal></p>
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
                        <div class="col-md-5">

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
                                        <small>
                                            <asp:Literal ID="ltDireccion" runat="server"></asp:Literal>,
                                            <asp:Literal ID="ltCiudad" runat="server"></asp:Literal></small>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <table class="table small m-b-xs">
                                <tbody>
                                    <tr>
                                        <td><strong><i class="fab fa-whatsapp"></i></strong>
                                            <asp:Literal ID="ltCelular" runat="server"></asp:Literal></td>
                                        <td><strong><i class="fa fa-shield"></i></strong> Estado: 
                                            <asp:Literal ID="ltEstado" runat="server"></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td><strong><i class="fa fa-building"></i></strong> Sede:
                                            <asp:Literal ID="ltSede" runat="server"></asp:Literal></td>
                                        <td><strong>54</strong> Días asistidos</td>
                                    </tr>
                                    <tr>
                                        <td><strong><i class="fa fa-cake"></i></strong>
                                            <asp:Literal ID="ltCumple" runat="server"></asp:Literal></td>
                                        <td><strong>2</strong> Congelaciones</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="col-md-4">
                            <ul class="todo-list small-list">
                                <asp:Repeater ID="rpPlanesAfiliado" runat="server" OnItemDataBound="rpPlanesAfiliado_ItemDataBound">
                                    <ItemTemplate>
                                        <li>
                                            <div class="i-checks">
                                                <small class="label label-primary pull-right"><%# Eval("DiasQueFaltan") %> días disponibles</small>
                                                <label>
                                                    Plan Activo: <%# Eval("NombrePlan") %>, <%# Eval("Meses") %> mes(es)
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
                                                    <div class="col-sm-3 b-r">
                                                        <div class="form-group">
                                                            <label>Tipo de plan:</label>
                                                            <div class="form-group">
                                                                <%--<asp:Button ID="btnDeluxe" runat="server" Text="Deluxe"
                                                                    CssClass="btn btn-primary btn-outline btn-block btn-lg font-bold"
                                                                    OnClick="btnDeluxe_Click" />
                                                                <asp:Button ID="btnPremium" runat="server" Text="Premium"
                                                                    CssClass="btn btn-danger btn-outline btn-block btn-lg font-bold"
                                                                    OnClick="btnPremium_Click" />--%>
                                                                <asp:PlaceHolder ID="phPlanes" runat="server"></asp:PlaceHolder>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-9">
                                                        <div class="form-group">
                                                            <label>Meses del plan:</label>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-sm-3 col-xs-3 col-xs-3">
                                                                        <asp:Button ID="btnMes1" runat="server" Text="1"
                                                                            CssClass="btn btn-warning btn-outline btn-block font-bold active"
                                                                            OnClick="btnMes1_Click" />
                                                                    </div>
                                                                    <div class="col-sm-3 col-xs-3">
                                                                        <asp:Button ID="btnMes2" runat="server" Text="2"
                                                                            CssClass="btn btn-warning btn-outline btn-block font-bold"
                                                                            OnClick="btnMes2_Click" />
                                                                    </div>
                                                                    <div class="col-sm-3 col-xs-3">
                                                                        <asp:Button ID="btnMes3" runat="server" Text="3"
                                                                            CssClass="btn btn-info btn-outline btn-block font-bold"
                                                                            OnClick="btnMes3_Click" />
                                                                    </div>
                                                                    <div class="col-sm-3 col-xs-3">
                                                                        <asp:Button ID="btnMes4" runat="server" Text="4"
                                                                            CssClass="btn btn-danger btn-outline btn-block font-bold"
                                                                            OnClick="btnMes4_Click" />
                                                                    </div>
                                                                    <div class="col-sm-3 col-xs-3">
                                                                        <asp:Button ID="btnMes5" runat="server" Text="5"
                                                                            CssClass="btn btn-warning btn-outline btn-block font-bold"
                                                                            OnClick="btnMes5_Click" />
                                                                    </div>
                                                                    <div class="col-sm-3 col-xs-3">
                                                                        <asp:Button ID="btnMes6" runat="server" Text="6"
                                                                            CssClass="btn btn-info btn-outline btn-block font-bold"
                                                                            OnClick="btnMes6_Click" />
                                                                    </div>
                                                                    <div class="col-sm-3 col-xs-3">
                                                                        <asp:Button ID="btnMes7" runat="server" Text="7"
                                                                            CssClass="btn btn-warning btn-outline btn-block font-bold"
                                                                            OnClick="btnMes7_Click" />
                                                                    </div>
                                                                    <div class="col-sm-3 col-xs-3">
                                                                        <asp:Button ID="btnMes8" runat="server" Text="8"
                                                                            CssClass="btn btn-danger btn-outline btn-block font-bold"
                                                                            OnClick="btnMes8_Click" />
                                                                    </div>
                                                                    <div class="col-sm-3 col-xs-3">
                                                                        <asp:Button ID="btnMes9" runat="server" Text="9"
                                                                            CssClass="btn btn-info btn-outline btn-block font-bold"
                                                                            OnClick="btnMes9_Click" />
                                                                    </div>
                                                                    <div class="col-sm-3 col-xs-3">
                                                                        <asp:Button ID="btnMes10" runat="server" Text="10"
                                                                            CssClass="btn btn-warning btn-outline btn-block font-bold"
                                                                            OnClick="btnMes10_Click" />
                                                                    </div>
                                                                    <div class="col-sm-3 col-xs-3">
                                                                        <asp:Button ID="btnMes11" runat="server" Text="11"
                                                                            CssClass="btn btn-warning btn-outline btn-block font-bold"
                                                                            OnClick="btnMes11_Click" />
                                                                    </div>
                                                                    <div class="col-sm-3 col-xs-3">
                                                                        <asp:Button ID="btnMes12" runat="server" Text="12"
                                                                            CssClass="btn btn-danger btn-outline btn-block font-bold"
                                                                            OnClick="btnMes12_Click" />

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-lg-3">
                                                        <div class="widget style1 lazur-bg">
                                                            <div class="row vertical-align">
                                                                <div class="col-xs-3">
                                                                    <i class="fa fa-money-bill-wave fa-3x" style="font-size: 2.3em"></i>
                                                                </div>
                                                                <div class="col-xs-9 text-right">
                                                                    <span>Valor mes </span>
                                                                    <h2 class="font-bold">
                                                                        <asp:Literal ID="ltPrecioBase" runat="server"></asp:Literal></h2>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-3">
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
                                                    <div class="col-lg-3">
                                                        <div class="widget style1 yellow-bg">
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
                                                    <div class="col-lg-3">
                                                        <div class="widget style1 bg-success">
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
                                                </div>
                                                <hr />
                                                <div class="row">
                                                    <div class="col-sm-4">
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

                                                                <%--<div class="i-checks"><label for="check15"> <input type="checkbox" id="check15" name="check15"> 15 dias </label></div>--%>
                                                                <%--<asp:CheckBox ID="check15" runat="server" />--%>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="ddlRegalos">Regalos:</label>
                                                            <%--<asp:DropDownList ID="ddlRegalos" runat="server" AppendDataBoundItems="true" CssClass="form-control m-b">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="Camiseta" Value="Camiseta"></asp:ListItem>
                                                                <asp:ListItem Text="Termo" Value="Termo"></asp:ListItem>
                                                            </asp:DropDownList>--%>
                                                            <div class="form-group">
                                                                <asp:LinkButton ID="btnRegalo1" runat="server" OnClick="btnRegalo1_Click"
                                                                    CssClass="btn btn-danger dim btn-large-dim btn-outline disabled"
                                                                    Style="width: 70px; font-size: 30px; height: 70px;"><i class="fa fa-bottle-water"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="btnRegalo2" runat="server" OnClick="btnRegalo2_Click"
                                                                    CssClass="btn btn-danger dim btn-large-dim btn-outline disabled"
                                                                    Style="width: 70px; font-size: 30px; height: 70px;"><i class="fa fa-tshirt"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="btnRegalo3" runat="server" OnClick="btnRegalo3_Click"
                                                                    CssClass="btn btn-danger dim btn-large-dim btn-outline disabled"
                                                                    Style="width: 70px; font-size: 30px; height: 70px;"><i class="fa fa-laptop-medical"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <div class="panel panel-default" runat="server" id="divPanelResumen">
                                                                <div class="panel-heading">
                                                                    <i class="fa fa-ticket"></i>
                                                                    <asp:Literal ID="ltNombrePlan" runat="server"></asp:Literal>
                                                                </div>
                                                                <div class="panel-body">
                                                                    <p>
                                                                        <asp:Literal ID="ltDescripcion" runat="server"></asp:Literal>
                                                                        <asp:Literal ID="ltObservaciones" runat="server"></asp:Literal>
                                                                        <hr />
                                                                        <asp:Literal ID="ltCortesias" runat="server"></asp:Literal>
                                                                        <asp:Literal ID="ltRegalos" runat="server"></asp:Literal>
                                                                        <asp:Label ID="lbEnlaceWompi" runat="server" ></asp:Label>
                                                                        <asp:HiddenField ID="hdEnlaceWompi" runat="server" />
                                                                        <button class="btn btn-success btn-circle" visible="false" id="btnPortapaleles" 
                                                                            onclick="copyToClipboard()" runat="server" title="Copiar enlace"><i class="fa fa-copy"></i></button>
                                                                    </p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-5">
                                                        <div class="form-group">
                                                            <div class="panel panel-default" runat="server" id="div1">
                                                                <div class="panel-heading">
                                                                    <i class="fa fa-money-bill"></i> Pago
                                                                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                                                </div>
                                                                <div class="panel-body">
                                                                    <div class="form-group" style="margin-bottom: 5px;">
                                                                        <label>Fecha de inicio:</label>
                                                                        <%--<div class="col-lg-7">--%>
                                                                            <asp:TextBox ID="txbFechaInicio" CssClass="form-control input-sm" runat="server" name="txbFechaInicio"></asp:TextBox>
                                                                        <%--</div>--%>
                                                                    </div>

                                                                    <div class="form-group" style="margin-bottom: 5px;">
                                                                        <label>Pago por Wompi:</label>
                                                                        <div class="row">
                                                                            <div class="col-lg-8">
                                                                                <a class="dropdown-toggle count-info" data-toggle="modal" href="#" data-target="#ModalDetalleWompi">Verificar pago...</a>
                                                                                <%--<asp:LinkButton ID="lkVerificarPago" 
                                                                                    runat="server" OnClick="lkVerificarPago_Click">Verificar pago...</asp:LinkButton>--%>
                                                                            </div>
                                                                            <div class="col-lg-4">
                                                                                <asp:TextBox ID="txbWompi" CssClass="form-control input-sm" 
                                                                                    runat="server" OnTextChanged="txbWompi_TextChanged" 
                                                                                    AutoPostBack="true" style="text-align: right;"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group" style="margin-bottom: 5px;">
                                                                        <label>Pago por Datafono:</label>
                                                                        <div class="row">
                                                                            <div class="col-lg-8">
                                                                                <asp:TextBox ID="txbNroAprobacion" CssClass="form-control input-sm" 
                                                                                    runat="server" placeholder="Ref."></asp:TextBox>
                                                                            </div>
                                                                            <div class="col-lg-4">
                                                                                <asp:TextBox ID="txbDatafono" CssClass="form-control input-sm" 
                                                                                    runat="server" OnTextChanged="txbDatafono_TextChanged" 
                                                                                    AutoPostBack="true" style="text-align: right;"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group" style="margin-bottom: 5px;">
                                                                        <label>Pago en Efectivo:</label>
                                                                        <div class="row">
                                                                            <div class="col-lg-8">
                                                                                
                                                                            </div>
                                                                            <div class="col-lg-4">
                                                                                <asp:TextBox ID="txbEfectivo" CssClass="form-control input-sm" 
                                                                                    runat="server" OnTextChanged="txbEfectivo_TextChanged" 
                                                                                    AutoPostBack="true" style="text-align: right;"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group" style="margin-bottom: 5px;">
                                                                        <label>Pago por Transferencia:</label>
                                                                        <div class="row">
                                                                            <div class="col-lg-8">
                                                                                <asp:RadioButtonList ID="rblBancos" runat="server" 
                                                                                    RepeatDirection="Horizontal" CssClass="form-control input-sm">
                                                                                    <asp:ListItem Text="Bancolombia" Value="Bancolombia" style="margin-right: 5px; font-size: 10px;"></asp:ListItem>
                                                                                    <asp:ListItem Text="Davivienda" Value="Davivienda" style="margin-right: 5px; font-size: 10px;"></asp:ListItem>
                                                                                    <asp:ListItem Text="BBVA" Value="BBVA" style="margin-right: 5px; font-size: 10px;"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                            <div class="col-lg-4">
                                                                                <asp:TextBox ID="txbTransferencia" CssClass="form-control input-sm" 
                                                                                    runat="server" OnTextChanged="txbTransferencia_TextChanged" 
                                                                                    AutoPostBack="true" style="text-align: right;"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <hr />  
                                                                    <div class="form-group" style="margin-bottom: 5px;">
                                                                        <label class="col-lg-7 control-label">TOTAL <asp:Literal ID="ltValorTotal" runat="server"></asp:Literal>:</label>
                                                                        <div class="col-lg-5">
                                                                            <asp:TextBox ID="txbTotal" CssClass="form-control input-sm" 
                                                                                runat="server" ReadOnly style="text-align: right;"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--<asp:AsyncPostBackTrigger ControlID="lkVerificarPago" EventName="Click" />--%>
                                                <%--<asp:AsyncPostBackTrigger ControlID="btnPremium" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnRegalo1" EventName="Click" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>

                                        <div>
                                            
                                            <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button"
                                                onclick="window.location.href='afiliados'">
                                                <strong>Cancelar</strong></button>
                                            <asp:LinkButton ID="lbAgregarPlan" runat="server"
                                                CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right" 
                                                OnClick="lbAgregarPlan_Click">
                                                <i class="fa fa-ticket"></i> Agregar plan</asp:LinkButton>
                                            <%--<asp:Button ID="btnAgregarPlan" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right"
                                                Text="Agregar Plan" OnClick="btnAgregarPlan_Click" />--%>
                                        </div>
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

        var ddlRegalos = document.getElementById("ddlRegalos");
        var check15 = document.getElementById("check15");
        ddlRegalos.setAttribute("disabled", true);
        check15.setAttribute("checked", false);

        $("#ionrange_1").ionRangeSlider({
            grid: true,
            values: ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"],
            onChange: function (data) {
                // fired on every range slider update
                console.dir(data.fromNumber);
                if (data.fromNumber >= 6) {
                    console.log(data.fromNumber);
                    ddlRegalos.removeAttribute('disabled');
                    check15.setAttribute("checked", true);
                }
                else {
                    ddlRegalos.setAttribute("disabled", true);
                    check15.setAttribute("checked", false);
                }
            },
        });

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

</body>

</html>
