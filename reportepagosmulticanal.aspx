<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reportepagosmulticanal.aspx.cs" Inherits="fpWebApp.reportepagosmulticanal" %>

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

    <title>Fitness People | Pagos recibidos multicanal</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />

    <!-- Morris -->
    <link href="css/plugins/morris/morris-0.4.3.min.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#reportepagosmulticanal");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#reportes");
            element2.classList.remove("collapse");
        }
    </script>

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
                    <i class="fa fa-file-invoice-dollar modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para gestionar reportes de pagos multicanal</h4>
                    <small class="font-bold">Revisa y administra pagos en efectivo, datáfono, transferencia y wompi de manera fácil y rápida.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>¿Cómo funciona?</b><br />
                        El sistema muestra <b>4 tablas independientes</b>, una para cada tipo de autorización. En cada una encontrarás:<br />
                        <i class="fa-solid fa-money-bill-wave" style="color: #0D6EFD;"></i><b>Tabla de Pagos en Efectivo</b><br />
                        <i class="fa-solid fa-credit-card" style="color: #0D6EFD;"></i><b>Tabla de Pagos con Datáfono</b><br />
                        <i class="fa-solid fa-arrows-rotate" style="color: #0D6EFD;"></i><b>Tabla de Transferencias</b><br />
                        <i class="fa-solid fa-mobile" style="color: #0D6EFD;"></i><b>Tabla de Pagos con Wompi</b>
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

        <uc1:navbar runat="server" ID="navbar" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa-solid fa-coins text-success m-r-sm"></i>Pagos multicanal</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Reportes</li>
                        <li class="active"><strong>Pagos multicanal</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <form id="form1" runat="server">
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


                        <div class="ibox float-e-margins" runat="server" id="divContenido">
                            <div class="row">

                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="col-md-3">
                                            <div id="filtros">
                                                <%--<asp:RadioButtonList ID="rblValor" runat="server">
                                                    <asp:ListItem Value="0" Text="Todos" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Alguno"></asp:ListItem>
                                                </asp:RadioButtonList>--%>
                                                <asp:DropDownList ID="ddlPlanes" DataTextField="NombrePlan" DataValueField="idPlan"
                                                    runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <input type="text" runat="server" id="txbFechaIni" class="form-control input-sm datepicker" />
                                        </div>
                                        <div class="col-md-3">
                                            <input type="text" runat="server" id="txbFechaFin" class="form-control input-sm datepicker" />
                                        </div>
                                        <div class="col-md-3">
                                            <label>
                                                <br />
                                            </label>
                                            <asp:Button ID="btnFiltrar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnFiltrar_Click" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-6">
                                    <div class="ibox">
                                        <div class="ibox-title bg-info">
                                            <h5><i class="fa fa-gift m-r-xs"></i>Efectivo total : </h5>
                                            <span class="label label-success">
                                                <asp:Literal ID="ltValorTotalEfe" runat="server"></asp:Literal>
                                            </span>
                                            <div class="ibox-tools">
                                                <a class="collapse-link">
                                                    <i class="fa fa-chevron-up text-white"></i>
                                                </a>
                                                <a class="fullscreen-link">
                                                    <i class="fa fa-expand text-white"></i>
                                                </a>
                                            </div>
                                        </div>
                                        <div class="ibox-content">
                                            <div class="row">
                                                <div class="col-lg-8">
                                                    <div class="form-group">
                                                        <div class="form-group" id="filter-form-container-efectivo"></div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <asp:LinkButton ID="btnExportarEfe" runat="server" CausesValidation="false"
                                                        CssClass="btn btn-info pull-right dim m-l-md" Style="font-size: 12px;"
                                                        OnClick="btnExportarEfe_Click"><i class="fa fa-file-excel m-r-xs"></i>EXCEL                                       
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <table class="footable table table-striped list-group-item-text" data-paging-size="5"
                                                data-filter-min="3" data-filter-placeholder="Buscar"
                                                data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}" data-paging-limit="10"
                                                data-filtering="true" data-filter-container="#filter-form-container-efectivo" data-filter-delay="300"
                                                data-filter-dropdown-title="Buscar en:" data-filter-position="left" data-empty="Sin resultados">
                                                <thead>
                                                    <tr>
                                                        <th data-breakpoints="xs">Documento</th>
                                                        <th data-breakpoints="xs">Afiliado</th>
                                                        <th data-breakpoints="xs" class="text-right">Valor</th>
                                                        <th data-breakpoints="xs">Fecha Hora</th>
                                                        <th data-breakpoints="xs">Estado</th>
                                                        <th data-breakpoints="xs">Canal</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rpTipoEfectivo" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("DocumentoAfiliado") %></td>
                                                                <td><%# Eval("NombreAfiliado") %></td>
                                                                <td class="text-right"><%# Eval("Valor", "{0:C0}") %></td>
                                                                <td><%# Eval("FechaHoraPago", "{0:dd MMM yyyy HH:mm}") %></td>
                                                                <td><span class="badge badge-info"><%# Eval("EstadoPago") %></span></td>
                                                                <td><%# Eval("CanalVenta") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-6">
                                    <div class="ibox">
                                        <div class="ibox-title bg-success">
                                            <h5><i class="fa fa-right-left m-r-xs"></i>Datafono total : </h5>
                                            <span class="label label-info">
                                                <asp:Literal ID="ltValorTotalData" runat="server"></asp:Literal>
                                            </span>
                                            <div class="ibox-tools">
                                                <a class="collapse-link">
                                                    <i class="fa fa-chevron-up text-white"></i>
                                                </a>
                                                <a class="fullscreen-link">
                                                    <i class="fa fa-expand text-white"></i>
                                                </a>
                                            </div>
                                        </div>
                                        <div class="ibox-content">
                                            <div class="row">
                                                <div class="col-lg-8">
                                                    <div class="form-group">
                                                        <div class="form-group" id="filter-form-container-datafono"></div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <asp:LinkButton ID="btnExportarData" runat="server" CausesValidation="false"
                                                        CssClass="btn btn-info pull-right dim m-l-md" Style="font-size: 12px;"
                                                        OnClick="btnExportarData_Click"><i class="fa fa-file-excel m-r-xs"></i>EXCEL                                       
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <table class="footable table table-striped list-group-item-text" data-paging-size="5"
                                                data-filter-min="3" data-filter-placeholder="Buscar"
                                                data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}" data-paging-limit="10"
                                                data-filtering="true" data-filter-container="#filter-form-container-datafono" data-filter-delay="300"
                                                data-filter-dropdown-title="Buscar en:" data-filter-position="left" data-empty="Sin resultados">
                                                <thead>
                                                    <tr>
                                                        <th data-breakpoints="xs">Documento</th>
                                                        <th data-breakpoints="xs">Afiliado</th>
                                                        <th data-breakpoints="xs" class="text-right">Valor</th>
                                                        <th data-breakpoints="xs">Fecha Hora</th>
                                                        <th data-breakpoints="xs">Referencia</th>
                                                        <th data-breakpoints="xs">Estado</th>
                                                        <th data-breakpoints="xs">Canal</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rpTipoDatafono" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("DocumentoAfiliado") %></td>
                                                                <td><%# Eval("NombreAfiliado") %></td>
                                                                <td class="text-right"><%# Eval("Valor", "{0:C0}") %></td>
                                                                <td><%# Eval("FechaHoraPago", "{0:dd MMM yyyy HH:mm}") %></td>
                                                                <td><%# Eval("idReferencia") %></td>
                                                                <td><span class="badge badge-info"><%# Eval("EstadoPago") %></span></td>
                                                                <td><%# Eval("CanalVenta") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-lg-6">

                                    <div class="ibox">
                                        <div class="ibox-title bg-warning">
                                            <h5><i class="fa fa-snowflake m-r-xs"></i>Transferencia total : </h5>
                                            <span class="label label-danger">
                                                <asp:Literal ID="ltValorTotalTrans" runat="server"></asp:Literal>
                                            </span>
                                            <div class="ibox-tools">
                                                <a class="collapse-link">
                                                    <i class="fa fa-chevron-up text-white"></i>
                                                </a>
                                                <a class="fullscreen-link">
                                                    <i class="fa fa-expand text-white"></i>
                                                </a>
                                            </div>
                                        </div>
                                        <div class="ibox-content">
                                            <div class="row">
                                                <div class="col-lg-8">
                                                    <div class="form-group">
                                                        <div class="form-group" id="filter-form-container-transferencia"></div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <asp:LinkButton ID="btnExportarTrans" runat="server" CausesValidation="false"
                                                        CssClass="btn btn-info pull-right dim m-l-md" Style="font-size: 12px;"
                                                        OnClick="btnExportarTrans_Click"><i class="fa fa-file-excel m-r-xs"></i>EXCEL                                       
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <table class="footable table table-striped list-group-item-text" data-paging-size="5"
                                                data-filter-min="3" data-filter-placeholder="Buscar"
                                                data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}" data-paging-limit="10"
                                                data-filtering="true" data-filter-container="#filter-form-container-transferencia" data-filter-delay="300"
                                                data-filter-dropdown-title="Buscar en:" data-filter-position="left" data-empty="Sin resultados">
                                                <thead>
                                                    <tr>
                                                        <th data-breakpoints="xs">Documento</th>
                                                        <th data-breakpoints="xs">Afiliado</th>
                                                        <th data-breakpoints="xs" class="text-right">Valor</th>
                                                        <th data-breakpoints="xs">Fecha Hora</th>
                                                        <th data-breakpoints="xs">Banco</th>
                                                        <th data-breakpoints="xs">Estado</th>
                                                        <th data-breakpoints="xs">Canal</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rpTransferencia" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Eval("DocumentoAfiliado") %></td>
                                                                <td><%# Eval("NombreAfiliado") %></td>
                                                                <td class="text-right"><%# Eval("Valor", "{0:C0}") %></td>
                                                                <td><%# Eval("FechaHoraPago", "{0:dd MMM yyyy HH:mm}") %></td>
                                                                <td><%# Eval("Banco") %></td>
                                                                <td><span class="badge badge-info"><%# Eval("EstadoPago") %></span></td>
                                                                <td><%# Eval("CanalVenta") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-6">

                                    <div class="ibox">
                                        <div class="ibox-title bg-info">
                                            <h5><i class="fa fa-head-side-mask m-r-xs"></i>Wompi total : </h5>
                                            <span class="label label-success">
                                                <asp:Literal ID="ltValortotalWompi" runat="server"></asp:Literal>
                                            </span>
                                            <div class="ibox-tools">
                                                <a class="collapse-link">
                                                    <i class="fa fa-chevron-up text-white"></i>
                                                </a>
                                                <a class="fullscreen-link">
                                                    <i class="fa fa-expand text-white"></i>
                                                </a>
                                            </div>
                                        </div>
                                        <div class="ibox-content">
                                            <div class="row">
                                                <div class="col-lg-8">
                                                    <div class="form-group">
                                                        <div class="form-group" id="filter-form-container-wompi"></div>
                                                    </div>
                                                </div>
                                                <div class="col-lg-4">
                                                    <asp:LinkButton ID="btnExportarWompi" runat="server" CausesValidation="false"
                                                        CssClass="btn btn-info pull-right dim m-l-md" Style="font-size: 12px;"
                                                        OnClick="btnExportarWompi_Click"><i class="fa fa-file-excel m-r-xs"></i>EXCEL                                       
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            <table class="footable table table-striped list-group-item-text" data-paging-size="5"
                                                data-filter-min="3" data-filter-placeholder="Buscar"
                                                data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}" data-paging-limit="10"
                                                data-filtering="true" data-filter-container="#filter-form-container-wompi" data-filter-delay="300"
                                                data-filter-dropdown-title="Buscar en:" data-filter-position="left" data-empty="Sin resultados">
                                                <thead>
                                                    <tr>
                                                        <%--<th data-breakpoints="xs">Id</th>
                                                        <th data-breakpoints="xs">Afiliado</th>
                                                        <th data-breakpoints="xs" class="text-right">Valor</th>
                                                        <th data-breakpoints="xs">Fecha Hora</th>
                                                        <th data-breakpoints="xs">Estado</th>
                                                        <th data-breakpoints="xs">Método</th>
                                                        <th data-breakpoints="xs">Canal</th>--%>
                                                        <th data-breakpoints="xs">Documento</th>
                                                        <th data-breakpoints="xs">Afiliado</th>
                                                        <th data-breakpoints="xs" class="text-right">Valor</th>
                                                        <th data-breakpoints="xs">Fecha Hora</th>
                                                        <th data-breakpoints="xs">Referencia</th>
                                                        <th data-breakpoints="xs">Estado</th>
                                                        <th data-breakpoints="xs">Canal</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rpWompi" runat="server">
                                                        <ItemTemplate>
                                                            <%--<tr>
                                                                <td><%# Eval("id") %></td>
                                                                <td><%# Eval("full_name") %></td>
                                                                <td class="text-right">
                                                                    <%# Eval("amount_formatted", "{0:C0}") %>
                                                                </td>
                                                                <td><%# Eval("created_at", "{0:dd MMM yyyy HH:mm}") %></td>
                                                                <td>
                                                                    <span class='<%# Eval("status").ToString().ToLower() == "error" ? "badge badge-danger" : "badge badge-info" %>'>Aprobado</span>
                                                                </td>
                                                                <td class="text-center"><asp:Image 
                                                                    runat="server" 
                                                                    Width="24"
                                                                    ImageUrl='<%# 
                                                                        Eval("payment_method_type").ToString().ToLower() == "card"      ? "~/img/icons/visa.png" :
                                                                        Eval("payment_method_type").ToString().ToLower() == "bancolombia_transfer"      ? "~/img/icons/bancolombia.png" :
                                                                        Eval("payment_method_type").ToString().ToLower() == "pse"       ? "~/img/icons/pse.png" :
                                                                        Eval("payment_method_type").ToString().ToLower() == "nequi"     ? "~/img/icons/nequi.png" :
                                                                        "~/img/icons/mastercard.png"
                                                                    %>' /></td>
                                                                <td><%# Eval("Canal") %></td>
                                                            </tr>--%>
                                                            <tr>
                                                                <td><%# Eval("DocumentoAfiliado") %></td>
                                                                <td><%# Eval("NombreAfiliado") %></td>
                                                                <td class="text-right"><%# Eval("Valor", "{0:C0}") %></td>
                                                                <td><%# Eval("FechaHoraPago", "{0:dd MMM yyyy HH:mm}") %></td>
                                                                <td><%# Eval("idReferencia") %></td>
                                                                <td><span class="badge badge-info"><%# Eval("EstadoPago") %></span></td>
                                                                <td><%# Eval("CanalVenta") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr id="trError" runat="server" visible="false">
                                                        <td colspan="7" class="text-center">
                                                            <asp:Literal ID="ltError" runat="server"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--Fin Contenido!!!!--%>
                    </div>
                </div>
                <uc1:footer runat="server" ID="footer" />
            </form>
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

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- jQuery UI -->
    <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();

        $(function () {
            $("span.pie1").peity("pie", {
                fill: ['#1ab394', '#d7d7d7', '#ffffff']
            })
            $("span.pie2").peity("pie", {
                fill: ['#F8AC59', '#d7d7d7', '#ffffff']
            })
            $("span.pie3").peity("pie", {
                fill: ['#ed5565', '#d7d7d7', '#ffffff']
            })
        });

    </script>

</body>

</html>



