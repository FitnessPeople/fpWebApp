<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nuevoticketsoporte.aspx.cs" Inherits="fpWebApp.nuevoticketsoporte" %>

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

    <title>Fitness People | Nuevo ticket de soporte</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />

    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#nuevoticketsoporte");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#mantenimiento");
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
                    <i class="fa fa-user-plus modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para crear un usuario</h4>
                    <small class="font-bold">¡Bienvenido! Sigue estos pasos para completar el registro sin errores.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Prepara la información</b><br />
                        Asegúrate de tener estos datos del usuario a mano:<br />
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i><b>Nombre(s) y Apellido(s).</b><br />
                        <i class="fa-solid fa-phone" style="color: #0D6EFD;"></i><b>Email.</b><br />
                        <i class="fa-solid fa-user-tie" style="color: #0D6EFD;"></i><b>Cargo, Pefil y Empleado.</b><br />
                        <i class="fa-solid fa-lock" style="color: #0D6EFD;"></i><b>Contraseña.</b>
                        <br />
                        <br />
                        <b>Paso 2: Completa el formulario</b><br />
                        <i class="fa-solid fa-pencil"></i>Llena todos los campos obligatorios (generalmente marcados con *).<br />
                        <i class="fa-solid fa-magnifying-glass"></i>Verifica que los datos estén correctos y actualizados.
                    <br />
                        <br />
                        <b>Paso 3: Confirma o cancela</b><br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i><b>Agregar:</b> Guarda la información y finaliza el registro.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i><b>Cancelar:</b> Si necesitas volver atrás sin guardar cambios.
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
                    <h2><i class="fas fa-screwdriver-wrench text-success m-r-sm"></i>Nuevo ticket de soporte</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Sistema</li>
                        <li class="active"><strong>Nuevo ticket de soporte</strong></li>
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

                    <form role="form" id="form1" runat="server">
                        <div class="row" id="divContenido" runat="server">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Agregar ticket de soporte</h5>
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
                                                    <label>Sede:</label>
                                                    <asp:DropDownList ID="ddlSedes" CssClass="form-control input-sm" runat="server"
                                                        AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSedes_SelectedIndexChanged" 
                                                        AutoPostBack="true">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSede" runat="server" ErrorMessage="* Campo requerido"
                                                        ControlToValidate="ddlSedes" ValidationGroup="agregar"
                                                        CssClass="font-bold text-danger" InitialValue="">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Activo:</label>
                                                    <asp:DropDownList ID="ddlActivosFijos" CssClass="form-control input-sm" runat="server" />
                                                    <asp:RequiredFieldValidator ID="rfvActivoFijo" runat="server" ErrorMessage="* Campo requerido"
                                                        ControlToValidate="ddlActivosFijos" ValidationGroup="agregar"
                                                        CssClass="font-bold text-danger">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Descripción:</label>
                                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control input-sm" TextMode="MultiLine" />
                                                    <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ErrorMessage="* Campo requerido"
                                                        ControlToValidate="txtDescripcion" ValidationGroup="agregar"
                                                        CssClass="font-bold text-danger" InitialValue="">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Prioridad</label>
                                                    <asp:DropDownList ID="ddlPrioridad" runat="server" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Baja" Value="Baja" />
                                                        <asp:ListItem Text="Media" Value="Media" Selected="True" />
                                                        <asp:ListItem Text="Alta" Value="Alta" />
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvPrioridad" runat="server" ErrorMessage="* Campo requerido"
                                                        ControlToValidate="ddlPrioridad" ValidationGroup="agregar"
                                                        CssClass="font-bold text-danger" InitialValue="">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click"
                                                        CssClass="btn btn-sm btn-primary m-t-n-xs pull-right" ValidationGroup="agregar" />
                                                </div>
                                                <br />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Lista de mis tickets</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">

                                        <div class="row" style="font-size: 12px;" runat="server" id="divBotonesLista">
                                            <div class="col-lg-10">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group" id="filter-form-container" style="margin-left: 18px;"></div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group form-horizontal">
                                                                <label class="col-lg-3 control-label">Estado</label>
                                                                <div class="col-lg-9">
                                                                    <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" CssClass="form-control input-sm">
                                                                        <asp:ListItem Text="Todos" Value="" />
                                                                        <asp:ListItem Text="Pendiente" Value="Pendiente" />
                                                                        <asp:ListItem Text="En Proceso" Value="En Proceso" />
                                                                        <asp:ListItem Text="Resuelto" Value="Resuelto" />
                                                                        <asp:ListItem Text="Cancelado" Value="Cancelado" />
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group form-horizontal">
                                                                <label class="col-lg-4 control-label">Prioridad</label>
                                                                <div class="col-lg-8">
                                                                    <asp:DropDownList ID="ddlFiltroPrioridad" runat="server" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlFiltroPrioridad_SelectedIndexChanged" CssClass="form-control input-sm">
                                                                        <asp:ListItem Text="Todas" Value="" />
                                                                        <asp:ListItem Text="Alta" Value="Alta" />
                                                                        <asp:ListItem Text="Media" Value="Media" />
                                                                        <asp:ListItem Text="Baja" Value="Baja" />
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-2">
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
                                                    <th data-sortable="false" data-breakpoints="xs">Id</th>
                                                    <th data-sortable="true" data-breakpoints="xs">Activo</th>
                                                    <th data-sortable="false" data-breakpoints="xs sm md">Descripción</th>
                                                    <th data-breakpoints="xs sm md">Estado</th>
                                                    <th data-breakpoints="xs sm md">Prioridad</th>
                                                    <th class="text-nowrap" data-breakpoints="xs">Hace cuánto?</th>
                                                    <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpTickets" runat="server" OnItemDataBound="rpTickets_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("idTicketSoporte") %></td>
                                                            <td><%# Eval("NombreActivoFijo") %></td>
                                                            <td><%# Eval("DescripcionTicket") %></td>
                                                            <td><span class="badge badge-<%# Eval("badge") %>"><%# Eval("EstadoTicket") %></span></td>
                                                            <td><i class="fa fa-circle text-<%# Eval("badge2") %>"></i> <%# Eval("PrioridadTicket") %></td>
                                                            <td><%# Eval("FechaCreacionTicket", "{0:dd MMM yyyy hh:mm:ss}") %> (<asp:Literal ID="ltTiempoTranscurrido" runat="server"></asp:Literal>)</td>
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

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- FooTable -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <script>
        $('.footable').footable();
        //$("#form").validate({
        //    rules: {
        //        txbNombre: {
        //            required: true,
        //            minlength: 3
        //        },
        //        txbCargo: {
        //            required: true,
        //            minlength: 5
        //        },
        //        ddlPerfiles: {
        //            required: true
        //        },
        //        txbClave: {
        //            required: true,
        //            minlength: 8
        //        },
        //    }
        //});
    </script>

</body>

</html>
