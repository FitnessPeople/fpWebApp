<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="planes.aspx.cs" Inherits="fpWebApp.planes" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Planes</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />--%>
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
            var element1 = document.querySelector("#planes");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#sistema");
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
                    <h4 class="modal-title">Guía para visualizar planes</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar los planes de manera clara y eficiente.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Crea un nuevo plan</b><br />
                        Usa el formulario que está a la <b>izquierda</b> para digitar la información necesaria del plan.<br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i> <b>Agregar:</b> Guarda la información y finaliza el registro.<br />
                        <i class="fa-solid fa-chart-simple" style="color: #1A7BB9;"></i> <b>Simular:</b> Recrea en un gráfico el comportamiento que tendrá el plan en una cantidad de tiempo.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i> <b>Cancelar:</b> Si necesitas volver atrás sin guardar cambios.
                    <br />
                        <br />
                        <b>Paso 2: Visualiza las sedes existentes</b><br />
                        Usa el buscador que está a la <b>derecha</b> para encontrar lo que buscas.<br />
                        <i class="fa-solid fa-magnifying-glass"></i> Filtra por 
                        <i class="fa-solid fa-ticket" style="color: #0D6EFD;"></i> <b>Nombre</b>,
                        <i class="fa-solid fa-note-sticky" style="color: #0D6EFD;"></i> <b>Descripción</b>,
                        <i class="fa-solid fa-money-bill-wave" style="color: #0D6EFD;"></i> <b>Precio</b>,
                        <i class="fa-solid fa-circle-user" style="color: #0D6EFD;"></i> <b>Creado por</b> o
                        <i class="fa-solid fa-circle" style="color: #0D6EFD;"></i> <b>Estado</b><br />
                        <i class="fa-solid fa-star" style="color: #FECE32;"></i> Tip: ¡Combina filtros para resultados más precisos!
                    <br />
                        <br />
                        <b>Paso 3: Gestiona las sedes</b><br />
                        En la columna "Acciones" encontrarás estas opciones:<br />
                        <i class="fa fa-edit" style="color: #1AB394;"></i> <b>Editar:</b> Modifica los datos necesarios.<br />
                        <i class="fa fa-trash" style="color: #DC3545;"></i> <b>Eliminar:</b> Borra lo que creas innecesario.
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
    <div id="wrapper">

        <uc1:navbar runat="server" ID="navbar1" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header1" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa fa-ticket text-success m-r-sm"></i>Planes</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Sistema</li>
                        <li class="active"><strong>Planes</strong></li>
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
                                            <div class="col-lg-12">
                                                <div class="form-group m-b-n-xs">
                                                    <label>Nombre del plan:</label>
                                                    <asp:TextBox ID="txbPlan" runat="server" CssClass="form-control input-sm"
                                                        placeholder="Nombre del plan"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvPlan" runat="server" ErrorMessage="* Campo requerido"
                                                        ControlToValidate="txbPlan" ValidationGroup="agregar"
                                                        CssClass="font-bold text-danger"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group m-b-n-xs">
                                                    <label>Descripción del plan:</label>
                                                    <asp:TextBox ID="txbDescripcion" runat="server" CssClass="form-control input-sm" TextMode="MultiLine"
                                                        placeholder="Descripción"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="* Campo requerido"
                                                        ControlToValidate="txbDescripcion" ValidationGroup="agregar"
                                                        CssClass="font-bold text-danger"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="row m-b-n-xs">
                                                    <div class="col-sm-5">
                                                        <label>Precio base del mes</label>
                                                        <asp:TextBox ID="txbPrecio" CssClass="form-control input-sm" runat="server"
                                                            Text="0"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ErrorMessage="* Campo requerido"
                                                            ControlToValidate="txbPrecio" ValidationGroup="agregar"
                                                            CssClass="font-bold text-danger"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <label>Días de congelamiento x mes</label>
                                                        <asp:TextBox ID="txbDiasCongelamiento" CssClass="form-control input-sm" runat="server"
                                                            Text="1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvDiasCongelamiento" runat="server" ErrorMessage="* Campo requerido"
                                                            ControlToValidate="txbDiasCongelamiento" ValidationGroup="agregar"
                                                            CssClass="font-bold text-danger"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row m-b-n-xs">
                                                    <div class="col-sm-5">
                                                        <label>% Descuento x mes</label>
                                                        <asp:TextBox ID="txbDescuentoMensual" CssClass="form-control input-sm" runat="server"
                                                            Text="0"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvDescuentoMensual" runat="server" ErrorMessage="* Campo requerido"
                                                            ControlToValidate="txbDescuentoMensual" ValidationGroup="agregar"
                                                            CssClass="font-bold text-danger"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <label>Nro. de meses máximo del plan</label>
                                                        <asp:TextBox ID="txbMesesMaximo" CssClass="form-control input-sm" runat="server"
                                                            Text="1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvMesesMaximo" runat="server" ErrorMessage="* Campo requerido"
                                                            ControlToValidate="txbMesesMaximo" ValidationGroup="agregar"
                                                            CssClass="font-bold text-danger"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group m-b-md">
                                                    <label>Color del botón:</label>
                                                    <asp:RadioButtonList ID="rblColor" runat="server" RepeatLayout="Flow"
                                                        CssClass="form-control input-sm" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="primary" style="margin-right: 5px; font-size: 10px; color: blue;">&nbsp;Azul</asp:ListItem>
                                                        <asp:ListItem Value="success" style="margin-right: 5px; font-size: 10px; color: green;">&nbsp;Verde</asp:ListItem>
                                                        <asp:ListItem Value="info" style="margin-right: 5px; font-size: 10px; color: cyan;">&nbsp;Celeste</asp:ListItem>
                                                        <asp:ListItem Value="warning" style="margin-right: 5px; font-size: 10px; color: darkgoldenrod;">&nbsp;Amarillo</asp:ListItem>
                                                        <asp:ListItem Value="danger" style="margin-right: 5px; font-size: 10px; color: red;">&nbsp;Rojo</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="row m-b-n-xs">
                                                    <div class="col-sm-12">
                                                        <h3>Período del plan</h3>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Fecha de inicio</label>
                                                            <asp:TextBox ID="txbFechaInicio" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvFechaInicio" runat="server" ErrorMessage="* Campo requerido"
                                                                ControlToValidate="txbFechaInicio" ValidationGroup="agregar"
                                                                CssClass="font-bold text-danger"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Fecha final</label>
                                                            <asp:TextBox ID="txbFechaFinal" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvFechaFinal" runat="server" ErrorMessage="* Campo requerido"
                                                                ControlToValidate="txbFechaFinal" ValidationGroup="agregar"
                                                                CssClass="font-bold text-danger"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row m-b-n-xs">
                                                    <div class="col-sm-12">
                                                        <div class="form-group">
                                                            <asp:CheckBox runat="server" ID="cbPermanente" Text="&nbsp;Permanente" CssClass="form-control" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <a href="planes" class="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md">Cancelar</a>
                                                    <asp:Button ID="btnSimular" runat="server" Text="Simular"
                                                        CssClass="btn btn-sm btn-success pull-right m-t-n-xs m-l-md" OnClick="btnSimular_Click"
                                                        Visible="true" ValidationGroup="agregar" />
                                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar"
                                                        CssClass="btn btn-sm btn-primary pull-right m-t-n-xs"
                                                        OnClick="btnAgregar_Click" Visible="false" ValidationGroup="agregar" />
                                                </div>
                                                <div class="form-group">
                                                    <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-8">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Lista de planes</h5>
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

                                            <div class="col-lg-6 form-horizontal">
                                                <asp:LinkButton ID="lbExportarExcel" runat="server" CausesValidation="false"
                                                    CssClass="btn btn-info pull-right dim m-l-md" Style="font-size: 12px;"
                                                    OnClick="lbExportarExcel_Click">
                                                    <i class="fa fa-file-excel"></i> EXCEL
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                        <table class="footable table table-striped" data-paging-size="10"
                                            data-filter-min="3" data-filter-placeholder="Buscar"
                                            data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}"
                                            data-paging-limit="10" data-filtering="true"
                                            data-filter-container="#filter-form-container" data-filter-delay="300"
                                            data-filter-dropdown-title="Buscar en:" data-filter-position="left"
                                            data-empty="Sin resultados">
                                            <thead>
                                                <tr>
                                                    <th>Nombre</th>
                                                    <th data-breakpoints="xs">Descripción</th>
                                                    <th data-breakpoints="xs" class="text-right">Precio</th>
                                                    <th data-breakpoints="xs sm md">Creado por</th>
                                                    <th data-breakpoints="xs sm md" data-sortable="false">Estado</th>
                                                    <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpPlanes" runat="server" OnItemDataBound="rpPlanes_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr class="feed-element">
                                                            <td><span class="btn btn-<%# Eval("NombreColorPlan") %> btn-outline" style="font-size: 12px;"><%# Eval("NombrePlan") %></span></td>
                                                            <td><i class="fa fa-note-sticky m-r-xs font-bold"></i><%# Eval("DescripcionPlan") %></td>
                                                            <td style="text-align: right;">$<%# Eval("Precio","{0:N0}") %></td>
                                                            <td style="white-space: nowrap;"><i class="fa fa-circle-user m-r-xs font-bold"></i><%# Eval("NombreUsuario") %></td>
                                                            <td><span class="badge badge-<%# Eval("label") %>"><%# Eval("EstadoPlan") %></span></td>
                                                            <td>
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

                                <%-- Gráfico --%>
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Comparativa de planes</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">

                                        <div>
                                            <canvas id="lineChart" height="140"></canvas>
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>

                    <%--Fin Contenido!!!!--%>
                </div>
            </div>

            <uc1:footer runat="server" ID="footer1" />

        </div>
        <uc1:rightsidebar runat="server" ID="rightsidebar1" />
    </div>

    <!-- Mainly scripts -->
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- FooTable -->
    <%--<script src="js/plugins/footable/footable.all.min.js"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- ChartJS-->
    <script src="js/plugins/chartJs/Chart.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>

        var lineData = {
            labels: ["Mes 1", "Mes 2", "Mes 3", "Mes 4", "Mes 5", "Mes 6", "Mes 7", "Mes 8", "Mes 9", "Mes 10", "Mes 11", "Mes 12"],
            datasets: [
                <%=strData%>
            ]
        };

        var lineOptions = {
            responsive: true
        };

        var ctx = document.getElementById("lineChart").getContext("2d");
        new Chart(ctx, { type: 'line', data: lineData, options: lineOptions });

        $('.footable').footable();
    </script>

</body>

</html>
