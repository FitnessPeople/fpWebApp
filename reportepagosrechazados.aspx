<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reportepagosrechazados.aspx.cs" Inherits="fpWebApp.reportepagosrechazados" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/indicadores01.ascx" TagPrefix="uc1" TagName="indicadores01" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Reporte Débitos Rechazados</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
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
            var element1 = document.querySelector("#reportepagosrechazados");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#reportes");
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
                    <i class="fa fa-hand-holding-usd modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para visualizar ventas realizadas</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo consultar y filtrar las ventas registradas en el sistema de manera fácil y rápida.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Filtra las ventas</b><br />
                        Usa el buscador para encontrar pagos específicos.<br />
                        <i class="fa-solid fa-magnifying-glass"></i>Filtra por: 
                        <i class="fa-solid fa-hashtag" style="color: #0D6EFD;"></i><b>ID Pago</b>, 
                        <i class="fa-solid fa-address-card" style="color: #0D6EFD;"></i><b>Documento</b>, 
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i><b>Afiliado</b> o 
                        <i class="fa-solid fa-sack-dollar" style="color: #0D6EFD;"></i><b>Valor</b><br />
                        <i class="fa-solid fa-money-check-dollar" style="color: #0D6EFD;"></i><b>Tipo de Pago</b>
                        <i class="fa-solid fa-money-check-dollar" style="color: #0D6EFD;"></i><b>Referencia</b>
                        <i class="fa-solid fa-calendar-days" style="color: #0D6EFD;"></i><b>Fecha</b>
                        <i class="fa-solid fa-circle" style="color: #0D6EFD;"></i><b>Estado </b>
                        <i class="fa-solid fa-user-group" style="color: #0D6EFD;"></i><b>Usuario</b>
                        <i class="fa-solid fa-phone" style="color: #0D6EFD;"></i><b>Canal</b>
                        <i class="fa-solid fa-receipt" style="color: #0D6EFD;"></i><b>Detalle.</b><br />
                        <i class="fa-solid fa-magnifying-glass"></i>Filtrar por: <b>Tipo de pago</b><br />
                        En el menú desplegable, selecciona:<br />
                        <i class="fa-solid fa-money-bill" style="color: #0D6EFD;"></i><b>Efectivo</b>
                        <i class="fa-solid fa-money-bill-transfer" style="color: #0D6EFD;"></i><b>Transferencia</b>
                        <i class="fa-solid fa-money-check-dollar" style="color: #0D6EFD;"></i><b>Datáfono</b>
                        <i class="fa-solid fa-mobile" style="color: #0D6EFD;"></i><b>Wompi</b><br />
                        <i class="fa-solid fa-magnifying-glass"></i>Filtrar por: <b>Rango de fechas</b><br />
                        Usa los calendarios para seleccionar:<br />
                        <i class="fa-solid fa-calendar-days" style="color: #0D6EFD;"></i><b>Desde:</b> Fecha inicial - <b>Hasta:</b> Fecha final
                    <br />
                        <br />
                        <b>Paso 2: Revisa los resultados</b><br />
                        La tabla muestra toda la información de los pagos realizados.
                    <br />
                        <br />
                        <b>Paso 3: Exporta a excel</b><br />
                        Al lado opuesto del buscador encontrarás un botón útil.<br />
                        <i class="fa-solid fa-file-export" style="color: #212529;"></i><b>Exportar a Excel:</b>
                        Genera un archivo Excel con los datos visibles en la tabla.
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
                    <h2><i class="fas fa-ban text-success m-r-sm"></i>Reporte débitos rechazados</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Reportes</li>
                        <li class="active"><strong>Reporte débitos rechazados</strong></li>
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

                    <form id="form1" runat="server">
                        <asp:ScriptManager ID="ScriptManager1" runat="server" />

                        <!-- Modal de Ver Detalle -->
                        <div class="modal fade" id="ModalDetalle" tabindex="-1" aria-labelledby="miModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content animated bounceInRight">
                                    <div class="modal-header">
                                        <h5 class="modal-title">Detalle de pagos del afiliado</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Literal ID="ltDetalleModal" runat="server"></asp:Literal>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row" id="divContenido" runat="server">
                            <div class="col-lg-12">

                                <div class="ibox float-e-margins" id="divPagosRechazados" runat="server" visible="false">
                                    <div class="ibox-title">
                                        <h5>Reporte de pagos rechazados (<asp:Literal ID="ltCuantos" runat="server"></asp:Literal> registros por valor de  <asp:Literal ID="ltTotalPorRecuadar" runat="server"></asp:Literal>):</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">

                                        <div class="row" style="font-size: 12px;" runat="server" id="div1">
                                            <div class="col-lg-10">
                                                <div class="form-group">
                                                    <div class="form-group" id="filter-form-container1"></div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:LinkButton ID="lkbExcel1" runat="server"
                                                    CausesValidation="false"
                                                    CssClass="btn btn-info pull-right dim m-l-md" Style="font-size: 12px;"
                                                     OnClick="lkbExcel1_Click">
                                                    <i class="fa fa-file-excel"></i> EXCEL
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                        <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                            data-filter-min="3" data-filter-placeholder="Buscar"
                                            data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}" data-paging-limit="10"
                                            data-filtering="true" data-filter-container="#filter-form-container1" data-filter-delay="300"
                                            data-filter-dropdown-title="Buscar en:" data-filter-position="left" data-empty="Sin resultados">
                                            <thead>
                                                <tr>
                                                    <th data-sortable="false" data-breakpoints="xs" style="width: 80px;">ID</th>
                                                    <th>Documento</th>
                                                    <th>Afiliado</th>
                                                    <th class="text-right">Valor</th>
                                                    <th data-type="number" data-breakpoints="xs sm md" class="text-center">Intentos</th>
                                                    <th data-breakpoints="xs sm md">Último intento</th>
                                                    <th data-breakpoints="xs sm md">Mensaje</th>
                                                    <%--<th data-breakpoints="xs sm md">Detalle</th>--%>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpHistorialCobrosRechazados" runat="server">
                                                    <ItemTemplate>
                                                        <tr class="feed-element">
                                                            <td><%# Eval("idAfiliadoPlan") %></td>
                                                            <td><%# Eval("DocumentoAfiliado") %></td>
                                                            <td><%# Eval("NombreCompletoAfiliado") %></td>
                                                            <td class="text-right"><%# Eval("PrecioBase", "{0:C0}") %></td>
                                                            <td class="text-center"><%# Eval("Intentos") %></td>
                                                            <td><%# Eval("UltimoIntento") %></td>
                                                            <td><%# Eval("Mensaje") %></td>
                                                            <%--<td>
                                                                <asp:Button ID="btnDetalle" runat="server" Text="Ver"
                                                                    CssClass="btn btn-primary"
                                                                    CommandArgument='<%# Eval("idAfiliadoPlan") %>'
                                                                    OnCommand="btnDetalle_Command"
                                                                    CommandName="mostrarDetalle"
                                                                    Visible='<%# Eval("NombreMedioPago").ToString() == "Pago en línea" %>' />
                                                            </td>--%>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>

                                        <%--<p>Total registros: <span id="totalRegistros"></span></p>
                                        <p>Registros visibles: <span id="registrosVisibles"></span></p>--%>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </form>
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

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();

    </script>


</body>

</html>
