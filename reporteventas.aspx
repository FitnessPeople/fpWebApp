<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteventas.aspx.cs" Inherits="fpWebApp.reporteventas" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/indicadoresCEO.ascx" TagPrefix="uc1" TagName="indicadores01" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Ventas</title>

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
            var element1 = document.querySelector("#reporteventas");
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
                    <h2><i class="fas fa-money-bill-trend-up text-success m-r-sm"></i>Ventas</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Reportes</li>
                        <li class="active"><strong>Ventas</strong></li>
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
                                <%--<uc1:indicadoresreportespagos runat="server" ID="indicadoresreportespagos"/>--%>

                                <!-- INDICADORES INICIO -->

                                <%--
                                    ****************
                                    Indicadores: 
                                        Total ventas
                                        Total ventas por web
                                        Total ventas por counter
                                        Total ventas totales
                                    ****************
                                --%>
                                <div class="row">
                                    <div class="col-lg-3">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <%--<span class="label label-success pull-right">Mes actual</span>--%>
                                                <h5>Ventas totales
                                                    <asp:Literal ID="ltMes1" runat="server"></asp:Literal></h5>
                                            </div>
                                            <div class="ibox-content">
                                                <h1 class="no-margins">
                                                    <asp:Literal ID="ltCuantos1" runat="server"></asp:Literal></h1>
                                                <div class="stat-percent font-bold text-success">
                                                    <asp:Literal ID="ltRegistros1" runat="server"></asp:Literal>
                                                    registros
                                                </div>
                                                <small>&nbsp;</small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <%--<span class="label label-info pull-right">Mes actual</span>--%>
                                                <h5>Ventas Web
                                                    <asp:Literal ID="ltMes2" runat="server"></asp:Literal></h5>
                                            </div>
                                            <div class="ibox-content">
                                                <h1 class="no-margins">
                                                    <asp:Literal ID="ltCuantos2" runat="server"></asp:Literal></h1>
                                                <div class="stat-percent font-bold text-success">
                                                    <asp:Literal ID="ltRegistros2" runat="server"></asp:Literal>
                                                    registros
                                                </div>
                                                <small>&nbsp;</small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <%--<span class="label label-primary pull-right">Mes actual</span>--%>
                                                <h5>Ventas Counter
                                                    <asp:Literal ID="ltMes3" runat="server"></asp:Literal></h5>
                                            </div>
                                            <div class="ibox-content">
                                                <h1 class="no-margins">
                                                    <asp:Literal ID="ltCuantos3" runat="server"></asp:Literal></h1>
                                                <div class="stat-percent font-bold text-success">
                                                    <asp:Literal ID="ltRegistros3" runat="server"></asp:Literal>
                                                    registros
                                                </div>
                                                <small>&nbsp;</small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <%--<span class="label label-danger pull-right">Mes actual</span>--%>
                                                <h5>Ventas Totales Acumuladas</h5>
                                            </div>
                                            <div class="ibox-content">
                                                <h1 class="no-margins">
                                                    <asp:Literal ID="ltCuantos4" runat="server"></asp:Literal></h1>
                                                <div class="stat-percent font-bold text-success">
                                                    <asp:Literal ID="ltRegistros4" runat="server"></asp:Literal>
                                                    registros
                                                </div>
                                                <small>&nbsp;</small>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- INDICADORES FINAL -->

                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Reporte de pagos
                                            <asp:Literal ID="ltMes4" runat="server"></asp:Literal>:</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row" style="font-size: 12px;" runat="server" id="divBotonesLista">
                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                    <div class="form-group" id="filter-form-container"></div>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div>
                                                    <asp:DropDownList ID="ddlPlanes" DataTextField="NombrePlan" DataValueField="idPlan"
                                                        runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlTipoPago" runat="server" AppendDataBoundItems="true"
                                                        DataTextField="TipoDocumento" DataValueField="idTipoDoc" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Todos" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Pago en línea" Value="4" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Efectivo" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Transferencia" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Datafono" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:DropDownList ID="ddlMes" runat="server" AppendDataBoundItems="true"
                                                    CssClass="form-control input-sm">
                                                    <asp:ListItem Text="Enero" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Febrero" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Marzo" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
                                                    <asp:ListItem Text="Mayo" Value="5"></asp:ListItem>
                                                    <asp:ListItem Text="Junio" Value="6"></asp:ListItem>
                                                    <asp:ListItem Text="Julio" Value="7"></asp:ListItem>
                                                    <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
                                                    <asp:ListItem Text="Septiembre" Value="9"></asp:ListItem>
                                                    <asp:ListItem Text="Octubre" Value="10"></asp:ListItem>
                                                    <asp:ListItem Text="Noviembre" Value="11"></asp:ListItem>
                                                    <asp:ListItem Text="Diciembre" Value="12"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-2">
                                                <asp:DropDownList ID="ddlAnnio" runat="server" AppendDataBoundItems="true"
                                                    CssClass="form-control input-sm">
                                                    <%--<asp:ListItem Text="Seleccione" Value=""></asp:ListItem>--%>
                                                    <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                                                    <asp:ListItem Text="2026" Value="2026" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <%--<div class="col-lg-2">
                                                <div class="form-group">
                                                    <input type="text" runat="server" id="txbFechaIni" class="form-control input-sm datepicker" />
                                                </div>
                                            </div>
                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                    <input type="text" runat="server" id="txbFechaFin" class="form-control input-sm datepicker" />
                                                </div>
                                            </div>--%>
                                            <div class="col-lg-1">
                                                <div class="form-group">
                                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary btn-sm" OnClick="btnBuscar_Click" />
                                                </div>
                                            </div>
                                            <div class="col-lg-1">
                                                <asp:LinkButton ID="lbExportarExcel" runat="server"
                                                    CausesValidation="false"
                                                    CssClass="btn btn-info pull-right dim m-l-md" Style="font-size: 12px;"
                                                    OnClick="lbExportarExcel_Click">
                                                    <i class="fa fa-file-excel m-r-xs"></i>EXCEL
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                        <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                            data-filter-min="3" data-filter-placeholder="Buscar"
                                            data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}" data-paging-limit="10"
                                            data-filtering="true" data-filter-container="#filter-form-container" data-filter-delay="300"
                                            data-filter-dropdown-title="Buscar en:" data-filter-position="left" data-empty="Sin resultados">
                                            <thead>
                                                <tr>
                                                    <th data-sortable="false" data-breakpoints="xs" style="width: 80px;">Id Pago</th>
                                                    <th>Documento</th>
                                                    <th>Afiliado</th>
                                                    <th data-breakpoints="xs sm md">Valor</th>
                                                    <th data-breakpoints="xs sm md">Tipo Pago</th>
                                                    <th data-breakpoints="xs sm md">Plan</th>
                                                    <th data-breakpoints="xs sm md">Referencia</th>
                                                    <th data-breakpoints="xs sm md">Fecha</th>
                                                    <th data-breakpoints="xs sm md">Estado</th>
                                                    <th data-breakpoints="xs sm md">Usuario</th>
                                                    <th data-breakpoints="xs sm md">Canal</th>
                                                    <%--<th data-breakpoints="xs sm md">Detalle</th>--%>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpPagos" runat="server">
                                                    <ItemTemplate>
                                                        <tr class="feed-element">
                                                            <td><%# Eval("idAfiliadoPlan") %></td>
                                                            <td><%# Eval("DocumentoAfiliado") %></td>
                                                            <td><%# Eval("NombreAfiliado") %></td>
                                                            <td><%# Eval("Valor", "{0:C0}") %></td>
                                                            <td><%# Eval("NombreMedioPago") %></td>
                                                            <td><%# Eval("NombrePlan") %></td>
                                                            <td><%# Eval("IdReferencia") %></td>
                                                            <td><%# Eval("FechaHoraPago", "{0:dd MMM yyyy HH:mm}") %></td>
                                                            <td><%# Eval("EstadoPago") %></td>
                                                            <td><%# Eval("Usuario") %></td>
                                                            <td><%# Eval("CanalVenta") %></td>
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

                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Gráficos
                                            <asp:Literal ID="ltMes5" runat="server"></asp:Literal></h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <canvas id="miGrafico1"></canvas>
                                            </div>
                                            <div class="col-lg-6">
                                                <canvas id="miGrafico2"></canvas>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <canvas id="miGrafico3"></canvas>
                                            </div>
                                            <div class="col-lg-6">
                                                <canvas id="miGrafico4"></canvas>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <canvas id="miGrafico5"></canvas>
                                            </div>
                                            <div class="col-lg-6">
                                                <canvas id="miGrafico6"></canvas>
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

        //const miTabla = document.getElementById('miTabla');
        //const tbody = miTabla.tBodies[0];
        //const numeroRegistros = tbody.rows.length;

        //console.log("Número total de registros:", numeroRegistros);
    </script>

    <script>
        $.validator.setDefaults({ ignore: ":hidden:not(.chosen-select)" })

        $("#form").validate({
            rules: {
                ddlTipoPago: {
                    required: true,
                },
            },
            messages: {
                ddlTipoPago: "*",
            }
        });

    </script>

    <script>
        function redondearSuperior(valor, base = 1000) {
            return Math.ceil(valor / base) * base;
        }

        // Grafico de Ventas y Cantidad Diaria x mes
        const datos1 = <%= Grafico1 %>;

        const ctx1 = document.getElementById('miGrafico1');

        const maxVentas1 = Math.max(...datos1.ventas_web, ...datos1.ventas_counter);
        const maxCantidad1 = Math.max(...datos1.cantidad_web, ...datos1.cantidad_counter);

        const maxY11 = redondearSuperior(maxVentas1 * 1.1, 100000);
        const maxY12 = Math.ceil(maxCantidad1 * 1.2);

        const data1 = {
            labels: datos1.labels, // nombres de canal
            datasets: [
                {
                    type: 'bar',                // Tipo: Barras
                    label: 'Ventas Web',
                    data: datos1.ventas_web,
                    yAxisID: 'y1',              // Asociado al eje Y izquierdo
                    backgroundColor: 'rgba(54, 162, 235, 0.5)',
                    borderColor: 'rgb(54, 162, 235)',
                    borderWidth: 1
                },
                {
                    type: 'bar',                // Tipo: Barras
                    label: 'Ventas Counter',
                    data: datos1.ventas_counter,
                    yAxisID: 'y1',              // Asociado al eje Y izquierdo
                    backgroundColor: 'rgba(255, 206, 86, 0.6)',
                    borderColor: 'rgb(255, 206, 86)',
                    borderWidth: 1
                },
                {
                    type: 'line',               // Tipo: Línea
                    label: 'Cantidad Web',
                    data: datos1.cantidad_web,
                    yAxisID: 'y2',              // Asociado al eje Y derecho
                    borderColor: 'rgb(255, 99, 132)',
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    tension: 0,
                    fill: false
                },
                {
                    type: 'line',               // Tipo: Línea
                    label: 'Cantidad Counter',
                    data: datos1.cantidad_counter,
                    yAxisID: 'y2',              // Asociado al eje Y derecho
                    borderColor: 'rgb(75, 192, 192)',
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    tension: 0,
                    fill: false
                }
            ]
        };

        new Chart(ctx1, {
            data: data1,
            options: {
                responsive: true,
                interaction: {
                    mode: 'index',
                    intersect: false
                },
                stacked: false,
                scales: {
                    y1: {
                        type: 'linear',
                        position: 'left',
                        min: 0,
                        max: maxY11,
                        title: { display: true, text: 'Ventas' },
                        grid: { drawOnChartArea: true }
                    },
                    y2: {
                        type: 'linear',
                        position: 'right',
                        min: 0,
                        max: maxY12,
                        title: { display: true, text: 'Cantidad' },
                        grid: { drawOnChartArea: false } // Evita duplicar líneas de cuadrícula
                    }
                },
                plugins: {
                    title: {
                        display: true,
                        text: 'Comparativo de Ventas y Cantidad Diario'
                    }
                }
            }
        });


        // Grafico de Ventas y Cantidad por Usuario
        const datos2 = <%= Grafico2 %>;

        const ctx2 = document.getElementById('miGrafico2');

        const maxVentas2 = Math.max(...datos2.ventas);
        const maxCantidad2 = Math.max(...datos2.cantidad);

        const maxY21 = redondearSuperior(maxVentas2 * 1.1, 100000);
        const maxY22 = Math.ceil(maxCantidad2 * 1.2);

        const coloresBarras2 = datos2.labels.map((_, index) => {
            const hue = (index * 360) / datos2.labels.length;
            return `hsla(${hue}, 70%, 55%, 0.7)`;
        });

        const bordesBarras2 = coloresBarras2.map(c => c.replace('0.7', '1'));

        const data2 = {
            labels: datos2.labels, // nombres de canal
            datasets: [
                {
                    type: 'bar',                // Tipo: Barras
                    label: 'Ventas',
                    data: datos2.ventas,
                    yAxisID: 'y1',              // Asociado al eje Y izquierdo
                    backgroundColor: coloresBarras2,
                    borderColor: bordesBarras2,
                    borderWidth: 1
                },
                {
                    type: 'line',               // Tipo: Línea
                    label: 'Cantidad',
                    data: datos2.cantidad,
                    yAxisID: 'y2',              // Asociado al eje Y derecho
                    borderColor: 'rgb(255, 99, 132)',
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    tension: 0,
                    fill: false
                }
            ]
        };

        new Chart(ctx2, {
            data: data2,
            options: {
                responsive: true,
                interaction: {
                    mode: 'index',
                    intersect: false
                },
                stacked: false,
                scales: {
                    y1: {
                        type: 'linear',
                        position: 'left',
                        min: 0,
                        max: maxY21,
                        title: { display: true, text: 'Ventas' },
                        grid: { drawOnChartArea: true }
                    },
                    y2: {
                        type: 'linear',
                        position: 'right',
                        min: 0,
                        max: maxY22,
                        title: { display: true, text: 'Cantidad' },
                        grid: { drawOnChartArea: false } // Evita duplicar líneas de cuadrícula
                    }
                },
                plugins: {
                    title: {
                        display: true,
                        text: 'Comparativo de Ventas y Cantidad por Usuario'
                    }
                }
            }
        });



        // Grafico de Ventas y Cantidad por Canal de Venta
        const datos3 = <%= Grafico3 %>;

        const ctx3 = document.getElementById('miGrafico3');

        const maxVentas3 = Math.max(...datos3.ventas);
        const maxCantidad3 = Math.max(...datos3.cantidad);

        const maxY31 = redondearSuperior(maxVentas3 * 1.1, 100000);
        const maxY32 = Math.ceil(maxCantidad3 * 1.2);

        const data3 = {
            labels: datos3.labels, // nombres de canal
            datasets: [
                {
                    type: 'bar',                // Tipo: Barras
                    label: 'Ventas',
                    data: datos3.ventas,
                    yAxisID: 'y1',              // Asociado al eje Y izquierdo
                    backgroundColor: 'rgba(54, 162, 235, 0.5)',
                    borderColor: 'rgb(54, 162, 235)',
                    borderWidth: 1
                },
                {
                    type: 'line',               // Tipo: Línea
                    label: 'Cantidad',
                    data: datos3.cantidad,
                    yAxisID: 'y2',              // Asociado al eje Y derecho
                    borderColor: 'rgb(255, 99, 132)',
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    tension: 0,
                    fill: false
                }
            ]
        };

        new Chart(ctx3, {
            data: data3,
            options: {
                responsive: true,
                interaction: {
                    mode: 'index',
                    intersect: false
                },
                stacked: false,
                scales: {
                    y1: {
                        type: 'linear',
                        position: 'left',
                        min: 0,
                        max: maxY31,
                        title: { display: true, text: 'Ventas' },
                        grid: { drawOnChartArea: true }
                    },
                    y2: {
                        type: 'linear',
                        position: 'right',
                        min: 0,
                        max: maxY32,
                        title: { display: true, text: 'Cantidad' },
                        grid: { drawOnChartArea: false } // Evita duplicar líneas de cuadrícula
                    }
                },
                plugins: {
                    title: {
                        display: true,
                        text: 'Comparativo de Ventas y Cantidad por Canal de Venta'
                    }
                }
            }
        });


        // Grafico de Ventas y Cantidad por Banco
        const datos4 = <%= Grafico4 %>;

        const ctx4 = document.getElementById('miGrafico4');

        const maxVentas4 = Math.max(...datos4.ventas);
        const maxCantidad4 = Math.max(...datos4.cantidad);

        const maxY41 = redondearSuperior(maxVentas4 * 1.1, 100000);
        const maxY42 = Math.ceil(maxCantidad4 * 1.2);

        const data4 = {
            labels: datos4.labels, // nombres de canal
            datasets: [
                {
                    type: 'bar',                // Tipo: Barras
                    label: 'Ventas',
                    data: datos4.ventas,
                    yAxisID: 'y1',              // Asociado al eje Y izquierdo
                    backgroundColor: 'rgba(54, 162, 235, 0.5)',
                    borderColor: 'rgb(54, 162, 235)',
                    borderWidth: 1
                },
                {
                    type: 'line',               // Tipo: Línea
                    label: 'Cantidad',
                    data: datos4.cantidad,
                    yAxisID: 'y2',              // Asociado al eje Y derecho
                    borderColor: 'rgb(255, 99, 132)',
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    tension: 0,
                    fill: false
                }
            ]
        };

        new Chart(ctx4, {
            data: data4,
            options: {
                responsive: true,
                interaction: {
                    mode: 'index',
                    intersect: false
                },
                stacked: false,
                scales: {
                    y1: {
                        type: 'linear',
                        position: 'left',
                        min: 0,
                        max: maxY41,
                        title: { display: true, text: 'Ventas' },
                        grid: { drawOnChartArea: true }
                    },
                    y2: {
                        type: 'linear',
                        position: 'right',
                        min: 0,
                        max: maxY42,
                        title: { display: true, text: 'Cantidad' },
                        grid: { drawOnChartArea: false } // Evita duplicar líneas de cuadrícula
                    }
                },
                plugins: {
                    title: {
                        display: true,
                        text: 'Comparativo de Ventas y Cantidad por Banco'
                    }
                }
            }
        });


        // Grafico de Ventas y Cantidad por Medio de Pago
        const datos5 = <%= Grafico5 %>;

        const ctx5 = document.getElementById('miGrafico5');

        const maxVentas5 = Math.max(...datos5.ventas);
        const maxCantidad5 = Math.max(...datos5.cantidad);

        const maxY51 = redondearSuperior(maxVentas5 * 1.1, 100000);
        const maxY52 = Math.ceil(maxCantidad5 * 1.2);

        const data5 = {
            labels: datos5.labels, // nombres de canal
            datasets: [
                {
                    type: 'bar',                // Tipo: Barras
                    label: 'Ventas',
                    data: datos5.ventas,
                    yAxisID: 'y1',              // Asociado al eje Y izquierdo
                    backgroundColor: 'rgba(54, 162, 235, 0.5)',
                    borderColor: 'rgb(54, 162, 235)',
                    borderWidth: 1
                },
                {
                    type: 'line',               // Tipo: Línea
                    label: 'Cantidad',
                    data: datos5.cantidad,
                    yAxisID: 'y2',              // Asociado al eje Y derecho
                    borderColor: 'rgb(255, 99, 132)',
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    tension: 0,
                    fill: false
                }
            ]
        };

        new Chart(ctx5, {
            data: data5,
            options: {
                responsive: true,
                interaction: {
                    mode: 'index',
                    intersect: false
                },
                stacked: false,
                scales: {
                    y1: {
                        type: 'linear',
                        position: 'left',
                        min: 0,
                        max: maxY51,
                        title: { display: true, text: 'Ventas' },
                        grid: { drawOnChartArea: true }
                    },
                    y2: {
                        type: 'linear',
                        position: 'right',
                        min: 0,
                        max: maxY52,
                        title: { display: true, text: 'Cantidad' },
                        grid: { drawOnChartArea: false } // Evita duplicar líneas de cuadrícula
                    }
                },
                plugins: {
                    title: {
                        display: true,
                        text: 'Comparativo de Ventas y Cantidad por Medio de Pago'
                    }
                }
            }
        });


        // Grafico de Ventas y Cantidad por Plan
        const datos6 = <%= Grafico6 %>;

        const ctx6 = document.getElementById('miGrafico6');

        const maxVentas6 = Math.max(...datos6.ventas_web, ...datos6.ventas_counter);
        const maxCantidad6 = Math.max(...datos6.cantidad_web, ...datos6.cantidad_counter);

        const maxY61 = redondearSuperior(maxVentas6 * 1.1, 100000);
        const maxY62 = Math.ceil(maxCantidad6 * 1.2);

        const data6 = {
            labels: datos6.labels, // nombres de canal
            datasets: [
                {
                    type: 'bar',                // Tipo: Barras
                    label: 'Ventas Web',
                    data: datos6.ventas_web,
                    yAxisID: 'y1',              // Asociado al eje Y izquierdo
                    backgroundColor: 'rgba(54, 162, 235, 0.5)',
                    borderColor: 'rgb(54, 162, 235)',
                    borderWidth: 1
                },
                {
                    type: 'bar',                // Tipo: Barras
                    label: 'Ventas Counter',
                    data: datos6.ventas_counter,
                    yAxisID: 'y1',              // Asociado al eje Y izquierdo
                    backgroundColor: 'rgba(255, 206, 86, 0.6)',
                    borderColor: 'rgb(255, 206, 86)',
                    borderWidth: 1
                },
                {
                    type: 'line',               // Tipo: Línea
                    label: 'Cantidad Web',
                    data: datos6.cantidad_web,
                    yAxisID: 'y2',              // Asociado al eje Y derecho
                    borderColor: 'rgb(255, 99, 132)',
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    tension: 0,
                    fill: false
                },
                {
                    type: 'line',               // Tipo: Línea
                    label: 'Cantidad Counter',
                    data: datos6.cantidad_counter,
                    yAxisID: 'y2',              // Asociado al eje Y derecho
                    borderColor: 'rgb(75, 192, 192)',
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    tension: 0,
                    fill: false
                }
            ]
        };

        new Chart(ctx6, {
            data: data6,
            options: {
                responsive: true,
                interaction: {
                    mode: 'index',
                    intersect: false
                },
                stacked: false,
                scales: {
                    y1: {
                        type: 'linear',
                        position: 'left',
                        min: 0,
                        max: maxY61,
                        title: { display: true, text: 'Ventas' },
                        grid: { drawOnChartArea: true }
                    },
                    y2: {
                        type: 'linear',
                        position: 'right',
                        min: 0,
                        max: maxY62,
                        title: { display: true, text: 'Cantidad' },
                        grid: { drawOnChartArea: false } // Evita duplicar líneas de cuadrícula
                    }
                },
                plugins: {
                    title: {
                        display: true,
                        text: 'Comparativo de Ventas y Cantidad por Plan'
                    }
                }
            }
        });
    </script>

</body>

</html>
