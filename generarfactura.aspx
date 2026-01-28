<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="generarfactura.aspx.cs" Inherits="fpWebApp.generarfactura" %>

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

    <title>Fitness People | Mis ventas</title>

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
            var element1 = document.querySelector("#generarfactura");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#corporativo");
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
                    <h2><i class="fas fa-sheet-plastic text-success m-r-sm"></i>Generar factura</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Corporativo</li>
                        <li class="active"><strong>Generar factura</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>


            <form id="form" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" />

                <!-- Modal de Ver Detalle -->
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

                        <div class="form-group">
                            <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                        </div>

                        <uc1:paginasperfil runat="server" ID="paginasperfil" Visible="false" />

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
                                </div>

                                <!-- INDICADORES FINAL -->

                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Reporte de ventas
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
                                                    <input type="text" id="txtBuscarGrid" class="form-control input-sm" placeholder="Buscar"
                                                        onkeyup="filtrarGridView()" />
                                                </div>
                                            </div>

                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <asp:DropDownList ID="ddlEmpresa" DataTextField="NombreComercial" DataValueField="DocumentoEmpresa"
                                                        runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvEmpresa" runat="server"
                                                        ErrorMessage="* Campo requerido" ControlToValidate="ddlEmpresa"
                                                        CssClass="text-danger font-bold" ValidationGroup="agregar"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <%--                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                    <input type="text" runat="server" id="txbFechaIni" class="form-control input-sm datepicker" placeholder="Fecha inicial" />
                                                </div>
                                            </div>

                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                    <input type="text" runat="server" id="txbFechaFin" class="form-control input-sm datepicker" placeholder="Fecha final" />
                                                </div>
                                            </div>--%>
                                            <div class="col-lg-1">
                                                <div class="form-group">
                                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary btn-sm"
                                                        OnClick="btnBuscar_Click" />
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
                                            <div class="col-lg-1">
                                                <asp:LinkButton
                                                    ID="lbExportarPdf"
                                                    runat="server"
                                                    CausesValidation="false"
                                                    CssClass="btn btn-success pull-right dim m-l-md"
                                                    Style="font-size: 12px;"
                                                    OnClick="lbExportarPdf_Click"
                                                    ValidationGroup="agregar">
                                                    <i class="fa fa-file-pdf m-r-xs"></i>PDF
                                                </asp:LinkButton>

                                            </div>
                                        </div>
                                        <%--<asp:GridView
                                            ID="gvCartera"
                                            runat="server"
                                            AutoGenerateColumns="false"
                                            CssClass="table table-bordered">

                                            <Columns>
                                                <asp:BoundField
                                                    DataField="IdAfiliadoPlan"
                                                    HeaderText="IdAfiliadoPlan" />

                                                <asp:BoundField
                                                    DataField="DocumentoAfiliado"
                                                    HeaderText="Documento" />

                                                <asp:BoundField
                                                    DataField="NombreAfiliado"
                                                    HeaderText="Afiliado" />

                                                <asp:BoundField
                                                    DataField="NombrePlan"
                                                    HeaderText="Plan" />

                                                <asp:BoundField
                                                    DataField="FechaElaboracion"
                                                    HeaderText="Fecha Elaboración"
                                                    DataFormatString="{0:dd/MM/yyyy}" />

                                                <asp:BoundField
                                                    DataField="DiasTranscurridos"
                                                    HeaderText="Días" />

                                                <asp:BoundField
                                                    DataField="ValorFacturar"
                                                    HeaderText="Valor"
                                                    DataFormatString="{0:C0}" />

                                                <asp:BoundField
                                                    DataField="EstadoVencimiento"
                                                    HeaderText="Estado" />

                                            </Columns>
                                        </asp:GridView>--%>

                                        <asp:GridView
                                            ID="gvCartera"
                                            runat="server"
                                            AutoGenerateColumns="false"
                                            CssClass="table table-bordered">

                                            <Columns>


                                                <asp:TemplateField HeaderStyle-Width="40px">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox
                                                            ID="chkTodos"
                                                            runat="server"
                                                            onclick="SeleccionarTodos(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox
                                                            ID="chkItem"
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="IdAfiliadoPlan" HeaderText="IdAfiliadoPlan" />
                                                <asp:BoundField DataField="DocumentoAfiliado" HeaderText="Documento" />
                                                <asp:BoundField DataField="NombreAfiliado" HeaderText="Afiliado" />
                                                <asp:BoundField DataField="NombrePlan" HeaderText="Plan" />
                                                <asp:BoundField DataField="FechaElaboracion" HeaderText="Fecha Elaboración"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="DiasTranscurridos" HeaderText="Días" />
                                                <asp:BoundField DataField="ValorFacturar" HeaderText="Valor"
                                                    DataFormatString="{0:C0}" />
                                                <asp:BoundField DataField="EstadoVencimiento" HeaderText="Estado" />

                                            </Columns>
                                        </asp:GridView>
                                        <asp:Button
                                            ID="btnGenerarLiquidacion"
                                            runat="server"
                                            Text="Generar liquidación"
                                            CssClass="btn btn-success btn-sm"
                                            OnClick="btnGenerarLiquidacion_Click"
                                            Visible="false" />
                                    </div>

                                </div>



                                <div class="ibox float-e-margins" id="divPagosRechazados" runat="server" visible="false">
                                    <div class="ibox-title">
                                        <h5>Reporte de pagos rechazados (<asp:Literal ID="ltCuantos" runat="server"></asp:Literal>):</h5>
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
                                            data-filtering="false" data-filter-delay="300"
                                            data-empty="Sin resultados" id="miTabla2">
                                            <thead>
                                                <tr>
                                                    <th data-sortable="false" data-breakpoints="xs" style="width: 80px;">ID</th>
                                                    <th>Documento</th>
                                                    <th>Afiliado</th>
                                                    <th data-breakpoints="xs sm md">Intentos</th>
                                                    <th data-breakpoints="xs sm md">Último intento</th>
                                                    <th data-breakpoints="xs sm md">Mensaje</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpHistorialCobrosRechazados" runat="server">
                                                    <ItemTemplate>
                                                        <tr class="feed-element">
                                                            <td><%# Eval("idAfiliadoPlan") %></td>
                                                            <td><%# Eval("DocumentoAfiliado") %></td>
                                                            <td><%# Eval("NombreCompletoAfiliado") %></td>
                                                            <td><%# Eval("Intentos") %></td>
                                                            <td><%# Eval("UltimoIntento") %></td>
                                                            <td><%# Eval("Mensaje") %></td>
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
    <uc1:footer runat="server" ID="footer" />

    <uc1:rightsidebar runat="server" ID="rightsidebar" />




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

        const miTabla = document.getElementById('miTabla');
        const tbody = miTabla.tBodies[0];
        const numeroRegistros = tbody.rows.length;

        console.log("Número total de registros:", numeroRegistros);
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

    <script type="text/javascript">
        function filtrarGridView() {
            var input = document.getElementById("txtBuscarGrid");
            var filtro = input.value.toLowerCase();

            var grid = document.getElementById("<%= gvCartera.ClientID %>");
            if (!grid) return;

            var filas = grid.getElementsByTagName("tr");

            // Recorre filas (saltando encabezado)
            for (var i = 1; i < filas.length; i++) {
                var celdas = filas[i].getElementsByTagName("td");
                var mostrar = false;

                for (var j = 0; j < celdas.length; j++) {
                    var texto = celdas[j].textContent || celdas[j].innerText;
                    if (texto.toLowerCase().indexOf(filtro) > -1) {
                        mostrar = true;
                        break;
                    }
                }

                filas[i].style.display = mostrar ? "" : "none";
            }
        }
    </script>

    <script type="text/javascript">
        function SeleccionarTodos(chk) {
            var grid = document.getElementById('<%= gvCartera.ClientID %>');
            var checkboxes = grid.getElementsByTagName("input");

            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].type === "checkbox" && checkboxes[i] !== chk) {
                    checkboxes[i].checked = chk.checked;
                }
            }
        }
    </script>


</body>

</html>



