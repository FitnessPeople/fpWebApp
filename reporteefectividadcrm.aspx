<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteefectividadcrm.aspx.cs" Inherits="fpWebApp.reporteefectividadcrm" %>

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
            var element1 = document.querySelector("#reporteefectividadcrm");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#crm");
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
                    <h2><i class="fas fa-money-bill-trend-up text-success m-r-sm"></i>Efectividad</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>CRM</li>
                        <li class="active"><strong>Efectividad gestión</strong></li>
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

                                <div class="row">
                                    <div class="col-lg-3">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <h5>Indicador por Estado de Venta</h5>
                                            </div>
                                            <div class="ibox-content">
                                                <div>
                                                    <asp:Literal ID="ltCuantosTemp" runat="server"></asp:Literal>
                                                </div>
                                                <div class="stat-percent font-bold text-success">
                                                    <asp:Literal ID="ltRegistrosTemp" runat="server"></asp:Literal>
                                                    registros
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <h5>Planes más consultados</h5>
                                            </div>
                                            <div class="ibox-content">
                                                <div>
                                                    <asp:Literal ID="ltCuantos4" runat="server"></asp:Literal>
                                                </div>
                                                <div class="stat-percent font-bold text-success">
                                                    <asp:Literal ID="ltRegistros4" runat="server"></asp:Literal>
                                                    registros
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <h5>Indicador por Género</h5>
                                            </div>
                                            <div class="ibox-content">
                                                <div>
                                                    <asp:Literal ID="ltCuantos2" runat="server"></asp:Literal>
                                                </div>
                                                <div class="stat-percent font-bold text-success">
                                                    <asp:Literal ID="ltRegistros2" runat="server"></asp:Literal>
                                                    registros
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-3">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-title">
                                                <h5>Indicador por Rango de Edad
                                                   <asp:Literal ID="ltMes3" runat="server"></asp:Literal>
                                                </h5>
                                            </div>
                                            <div class="ibox-content">
                                                <div>
                                                    <asp:Literal ID="ltCuantos3" runat="server"></asp:Literal>
                                                </div>
                                                <div class="stat-percent font-bold text-success">
                                                    <asp:Literal ID="ltRegistros3" runat="server"></asp:Literal>
                                                    registros
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <!-- INDICADORES FINAL -->

                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Reporte de contactos:
                                            <asp:Literal ID="ltCantidadCon" runat="server"></asp:Literal>:</h5>
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
                                                    <asp:DropDownList ID="ddlCanalesVenta"
                                                        runat="server"
                                                        AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlCanalesVenta_SelectedIndexChanged"
                                                        DataTextField="NombreCanalVenta"
                                                        DataValueField="idCanalVenta"
                                                        AppendDataBoundItems="true"
                                                        CssClass="form-control input-sm">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlAsesores"
                                                        runat="server"
                                                        DataTextField="NombreUsuario"
                                                        DataValueField="idUsuario"
                                                        AppendDataBoundItems="true"
                                                        CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Todos los asesores" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                    <input type="text" runat="server" id="txbFechaIni" class="form-control input-sm datepicker" placeholder="Fecha inicial" />
                                                </div>
                                            </div>

                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                    <input type="text" runat="server" id="txbFechaFin" class="form-control input-sm datepicker" placeholder="Fecha final" />
                                                </div>
                                            </div>

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
                                                    <i class="fa fa-file-excel"></i> EXCEL
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                        <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                            data-filter-min="3" data-filter-placeholder="Buscar"
                                            data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}" data-paging-limit="10"
                                            data-filtering="true" data-filter-container="#filter-form-container" data-filter-delay="300"
                                            data-filter-dropdown-title="Buscar en:" data-filter-position="left" data-empty="Sin resultados" id="miTabla">
                                            <thead>
                                                <tr>
                                                    <th data-sortable="false" data-breakpoints="xs" style="width: 80px;">Id</th>
                                                    <th data-breakpoints="xs sm md">Tipo</th>
                                                    <th data-breakpoints="xs sm md">Primer contacto</th>
                                                    <th data-breakpoints="xs sm md">Documento</th>
                                                    <th data-breakpoints="xs sm md">Nombre</th>
                                                    <th data-breakpoints="xs sm md">Teléfono</th>
                                                    <th data-breakpoints="xs sm md">Estado de venta</th>
                                                    <th data-breakpoints="xs sm md">Canal contacto</th>
                                                    <th data-breakpoints="xs sm md">Canal de venta</th>
                                                    <th data-breakpoints="xs sm md">Asesor</th>
                                                    <th data-breakpoints="xs sm md">Fecha Próxima</th>
                                                    <th data-breakpoints="xs sm md">Plan</th>
                                                    <th data-breakpoints="xs sm md">Valor</th>
                                                    <th data-breakpoints="xs sm md">Estado Lead</th>
                                                    <th data-breakpoints="all" data-title="Info"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpContactos" runat="server">
                                                    <ItemTemplate>
                                                        <tr class="feed-element">
                                                            <td><%# Eval("idContacto") %></td>
                                                            <td><%# Eval("NombreTipoAfiliado") %></td>
                                                            <td><%# Eval("FechaPrimerCon", "{0:dd MMM yyyy}") %></td>
                                                            <td><%# Eval("DocumentoAfiliado") %></td>
                                                            <td><%# Eval("Nombre") %></td>
                                                            <td><%# Eval("TelefonoContacto") %></td>
                                                            <td>
                                                                <asp:Literal
                                                                    ID="ltEstadoVenta"
                                                                    runat="server"
                                                                    Mode="PassThrough"
                                                                    Text='<%# GetIconoEstadoVenta(Eval("NombreEstadoVenta"), Eval("DescripciónEstadoVenta")) %>'>
                                                                </asp:Literal>
                                                            </td>

                                                            <td><%# Eval("NombreCanalMarketing") %></td>
                                                            <td><%# Eval("NombreCanalVenta") %></td>
                                                            <td><%# Eval("Asesor") %></td>
                                                            <td><%# Eval("FechaProximoCon", "{0:dd MMM yyyy HH:mm}") %></td>
                                                            <td><%# Eval("NombrePlan") %></td>
                                                            <td><%# Eval("ValorPropuesta", "{0:C0}") %></td>
                                                            <td>
                                                                <span
                                                                    title='<%# Eval("NombreEstadoCRM") %>'
                                                                    style='color: <%# Eval("ColorHexaCRM") %>; font-size: 18px;'>
                                                                    <%# Eval("IconoMinEstadoCRM") %>
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <table class="table table-bordered table-striped">
                                                                    <tr>
                                                                        <%-- <th width="25%"><i class="fa fa-city m-r-xs"></i>Ciudad</th>--%>
                                                                        <th width="25%"><i class="fa fa-mobile m-r-xs"></i>Datos del contacto</th>
                                                                        <th width="50%" class="text-nowrap"><i class="fa fa-clock m-r-xs"></i>Historial</th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Identificación:<%# Eval("DocumentoAfiliado") %> Otros datos:
                                                                                        <asp:Literal ID="ltInfoAfiliado" runat="server"></asp:Literal></td>
                                                                        <td><%# Eval("HistorialHTML2") %></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>

                                        <%--                                        <p>Total registros: <span id="totalRegistros"></span></p>
                                        <p>Registros visibles: <span id="registrosVisibles"></span></p>--%>
                                    </div>
                                </div>
                                <div class="ibox float-e-margins" id="divPagosRechazados" runat="server">
                                    <div class="ibox-title">
                                        <h5>Reporte gestión asesores: (<asp:Literal ID="ltCuantosAse" runat="server"></asp:Literal>) registros</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">

                                        <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                            data-paging="true" data-sorting="true"
                                            data-paging-count-format="{CP} de {TP}" data-paging-limit="10"
                                            data-filtering="true" data-filter-delay="300"
                                            data-empty="Sin resultados" id="miTabla2">
                                            <thead>
                                                <tr>
                                                    <th>Canal de venta</th>
                                                    <th>Asesor</th>
                                                    <th data-type="number">Total contactos</th>
                                                    <th data-breakpoints="xs sm md">Total propuestas</th>
                                                    <th data-breakpoints="xs sm md" data-type="number">Cierres</th>
                                                    <th data-breakpoints="xs sm md" data-type="number">Valor cierres</th>
                                                    <th data-breakpoints="xs sm md" data-type="number">Efectividad</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpGestionAsesores" runat="server">
                                                    <ItemTemplate>
                                                        <tr class="feed-element">
                                                            <td><%# Eval("CanalVenta") %></td>
                                                            <td><%# Eval("Asesor") %></td>
                                                            <td><%# Eval("TotalContactos") %></td>
                                                            <td><%# String.Format("{0:C0}", Eval("TotalPropuestas")) %></td>
                                                            <td><%# Eval("Cierres") %></td>
                                                            <td><%# String.Format("{0:C0}", Eval("ValorPropuestasCerradas")) %></td>
                                                            <td><%# String.Format("{0:P0}", Convert.ToDecimal(Eval("Efectividad")) / 100m) %></td>

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



</body>

</html>
