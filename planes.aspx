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

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <style type="text/css" media="print">
        body { visibility: hidden; display: none }
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

    <div id="wrapper">

        <uc1:navbar runat="server" ID="navbar1" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header1" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa fa-file-code text-success m-r-sm"></i>Planes</h2>
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
                                                <h5><asp:Literal ID="ltTitulo" runat="server"></asp:Literal></h5>
                                                <div class="ibox-tools">
                                                    <a class="collapse-link">
                                                        <i class="fa fa-chevron-up"></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class="ibox-content">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <label>Nombre del plan:</label>
                                                            <asp:TextBox ID="txbPlan" runat="server" CssClass="form-control input-sm" placeholder="Nombre del plan"></asp:TextBox>
                                                        </div>
                                                        <div class="form-group">
                                                            <label>Descripción del plan:</label>
                                                            <asp:TextBox ID="txbDescripcion" runat="server" CssClass="form-control input-sm" TextMode="MultiLine" 
                                                                placeholder="Descripción"></asp:TextBox>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label>Precio base del mes</label>
                                                                    <asp:TextBox ID="txbPrecio" CssClass="form-control input-sm" runat="server" placeholder="Precio" Text="0"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <div class="form-group">
                                                                    <label>Días de congelamiento por mes</label>
                                                                    <asp:TextBox ID="txbDiasCongelamiento" CssClass="form-control input-sm" runat="server" placeholder="Días" Text="1"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md" OnClick="btnCancelar_Click" formnovalidate />
                                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" OnClick="btnAgregar_Click" />
                                                        </div>
                                                        <br />
                                                        <br />
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
                                                            <label class="col-lg-4 control-label" style="text-align: left;">Buscador:</label>
                                                            <div class="col-lg-8">
                                                                <input type="text" placeholder="Buscar..." class="form-control input-sm m-b-xs" id="filter">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--<div class="col-lg-6 form-horizontal" style="text-align: center;">
                                                        <label class="control-label">Mostrar </label>

                                                        <a href="#" class="data-page-size" data-page-size="10">10</a> | 
                                                        <a href="#" class="data-page-size" data-page-size="20">20</a> | 
                                                        <a href="#" class="data-page-size" data-page-size="50">50</a> | 
                                                        <a href="#" class="data-page-size" data-page-size="100">100</a>

                                                        <label class="control-label">registros</label>
                                                    </div>--%>
                                                    <div class="col-lg-6 form-horizontal">
                                                        <label class="control-label">&nbsp;</label>
                                                        <a class="btn btn-info pull-right dim m-l-md" style="font-size: 12px;" target="_blank" runat="server" id="btnExportar" href="imprimirplanes" title="Exportar"><i class="fa fa-print"></i> IMPRIMIR</a>
                                                        <a data-trigger="footable_expand_all" style="font-size: 12px;" class="toggle btn btn-primary pull-right dim" href="#collapse" title="Expandir todo"><i class="fa fa-square-caret-down"></i> EXPANDIR</a>
                                                        <a data-trigger="footable_collapse_all" class="toggle btn btn-primary pull-right dim" style="display: none; font-size: 12px;" href="#collapse" title="Contraer todo"><i class="fa fa-square-caret-up"></i> CONTRAER</a>
                                                    </div>
                                                </div>


                                                <table class="footable table toggle-arrow-small" data-page-size="10" data-filter="#filter" data-filter-minimum="3">
                                                    <thead>
                                                        <tr>
                                                            <th data-sort-initial="true">ID</th>
                                                            <th>Nombre</th>
                                                            <th data-sort-ignore="true" data-hide="all">Descripción</th>
                                                            <th class="text-right">Precio</th>
                                                            <th data-hide="phone,tablet">Creado por</th>
                                                            <th data-toggle="false" data-sort-ignore="true">Estado</th>
                                                            <th data-sort-ignore="true" data-toggle="false" class="text-right" 
                                                                style="display: flex; flex-wrap: nowrap; width: 100%;">Acciones</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="rpPlanes" runat="server" OnItemDataBound="rpPlanes_ItemDataBound">
                                                            <ItemTemplate>
                                                                <tr class="feed-element">
                                                                    <td><%# Eval("idPlan") %></td>
                                                                    <td><%# Eval("NombrePlan") %></td>
                                                                    <td><i class="fa fa-note-sticky m-r-xs font-bold"></i><%# Eval("DescripcionPlan") %></td>
                                                                    <td style="text-align: right;">$<%# Eval("PrecioBase") %></td>
                                                                    <td><i class="fa fa-circle-user m-r-xs font-bold"></i><%# Eval("NombreUsuario") %></td>
                                                                    <td><span class="badge badge-<%# Eval("label") %>"><%# Eval("EstadoPlan") %></span></td>
                                                                    <td style="display: flex; flex-wrap: nowrap; width: 100%;">
                                                                        <button runat="server" id="btnEditar" class="btn btn-outline btn-primary pull-left m-r-xs" 
                                                                            style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false"><i class="fa fa-edit"></i></button>
                                                                        <button runat="server" id="btnEliminar" class="btn btn-outline btn-danger pull-right" 
                                                                            style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false"><i class="fa fa-trash"></i></button>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                    <tfoot>
                                                        <tr>
                                                            <td colspan="8">
                                                                <ul class="pagination"></ul>
                                                            </td>
                                                        </tr>
                                                    </tfoot>
                                                </table>

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
    <script src="js/plugins/footable/footable.all.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $(document).ready(function () {
            $("#form").validate({
                rules: {
                    txbPlan: {
                        required: true,
                        minlength: 5
                    },
                    txbDescripcion: {
                        required: true,
                        minlength: 10
                    },
                    txbPrecio: {
                        required: true,
                        minlength: 1
                    },
                    txbTiempo: {
                        required: true,
                    },
                    ddlTipoPlan: {
                        required: true,
                    },
                }
            });
        });

        $('.footable').footable();

        $('.data-page-size').on('click', function (e) {
            e.preventDefault();
            var newSize = $(this).data('pageSize');
            $('.footable').data('page-size', newSize);
            $('.footable').trigger('footable_initialized');
        });

        $('.toggle').click(function (e) {
            e.preventDefault();
            $('.toggle').toggle();
            $('.footable').trigger($(this).data('trigger')).trigger('footable_redraw');
        });

        $('.chosen-select').chosen({ width: "100%" });

    </script>

</body>

</html>