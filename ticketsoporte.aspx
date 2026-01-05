<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ticketsoporte.aspx.cs" Inherits="fpWebApp.ticketsoporte" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>
<%@ Register Src="~/controles/indicadoressoporte.ascx" TagPrefix="uc1" TagName="indicadoressoporte" %>


<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Tickets soporte</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/dataTables/datatables.min.css" rel="stylesheet" />
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />

    <!-- Select2 -->
    <link href="css/plugins/select2/select2.min.css" rel="stylesheet">

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
            var element1 = document.querySelector("#ticketsoporte");
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
                    <i class="fa fa-users modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para visualizar los usuarios registrados</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar el listado de forma rápida y sencilla.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Busca y filtra</b><br />
                        Usa el buscador para encontrar usuarios específicos.<br />
                        <i class="fa-solid fa-magnifying-glass"></i>Filtra por: 
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i><b>Nombre</b>, 
                        <i class="fa-solid fa-user-tie" style="color: #0D6EFD;"></i><b>Empleado</b>, 
                        <i class="fa-solid fa-envelope" style="color: #0D6EFD;"></i><b>Correo</b>, 
                        <i class="fa-solid fa-user-shield" style="color: #0D6EFD;"></i><b>Perfil</b> o
                        <i class="fa-solid fa-circle" style="color: #0D6EFD;"></i><b>Estado</b><br />
                        <i class="fa-solid fa-star" style="color: #FECE32;"></i>Tip: ¡Combina filtros para resultados más precisos!
                    <br />
                        <br />
                        <b>Paso 2: Revisa la tabla de resultados</b><br />
                        La tabla muestra toda la información clave de cada usuario.<br />
                        En la columna "Acciones" encontrarás estas opciones:<br />
                        <i class="fa fa-edit" style="color: #1AB394;"></i><b>Editar:</b> Modifica los datos necesarios.<br />
                        <i class="fa fa-trash" style="color: #DC3545;"></i><b>Eliminar:</b> Borra lo que creas innecesario.
                    <br />
                        <br />
                        <b>Paso 3: Acciones adicionales</b><br />
                        Al lado opuesto del buscador encontrarás dos botones útiles:<br />
                        <i class="fa-solid fa-file-export" style="color: #212529;"></i><b>Exportar a Excel:</b>
                        Genera un archivo Excel con los datos visibles en la tabla.<br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i><b>Crear Nuevo Usuario:</b>
                        Te lleva a un formulario para registrar un nuevo usuario.
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

        <uc1:navbar runat="server" ID="navbar1" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header1" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">
                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fas fa-screwdriver-wrench text-success m-r-sm"></i>Tickets soporte</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Sistema</li>
                        <li class="active"><strong>Tickets soporte</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">

                    <uc1:indicadoressoporte runat="server" id="indicadoressoporte" />

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

                    <div class="row" id="divContenido" runat="server">
                        <form runat="server" id="form1">
                            <div class="col-lg-8">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5><i class="fas fa-clipboard-list text-success m-r-sm"></i>Lista de Tickets</h5>
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
                                                        <div class="col-md-3" style="padding-left: 10px; padding-right: 0px;">
                                                            <div class="form-group" id="filter-form-container"></div>
                                                        </div>
                                                        <div class="col-md-3" style="padding-left: 10px; padding-right: 0px;">
                                                            <div class="form-group form-horizontal">
                                                                <label class="col-lg-3 control-label">Estado</label>
                                                                <div class="col-lg-9">
                                                                    <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged" CssClass="form-control input-sm select3">
                                                                        <asp:ListItem Text="Todos" Value="" />
                                                                        <asp:ListItem Text="Pendiente" Value="Pendiente" />
                                                                        <asp:ListItem Text="En Proceso" Value="En Proceso" />
                                                                        <asp:ListItem Text="Resuelto" Value="Resuelto" />
                                                                        <asp:ListItem Text="Cancelado" Value="Cancelado" />
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3" style="padding-left: 10px; padding-right: 0px;">
                                                            <div class="form-group form-horizontal">
                                                                <label class="col-lg-4 control-label">Prioridad</label>
                                                                <div class="col-lg-8">
                                                                    <asp:DropDownList ID="ddlFiltroPrioridad" runat="server" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlFiltroPrioridad_SelectedIndexChanged" 
                                                                        CssClass="form-control input-sm select2">
                                                                        <asp:ListItem Text="Todas" Value="" />
                                                                        <asp:ListItem Text="Alta" Value="Alta" />
                                                                        <asp:ListItem Text="Media" Value="Media" />
                                                                        <asp:ListItem Text="Baja" Value="Baja" />
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3" style="padding-left: 10px; padding-right: 0px;">
                                                            <div class="form-group form-horizontal">
                                                                <label class="col-lg-4 control-label">Sede</label>
                                                                <div class="col-lg-8">
                                                                    <asp:DropDownList ID="ddlSedes" runat="server" AutoPostBack="true" 
                                                                        OnSelectedIndexChanged="ddlSedes_SelectedIndexChanged" 
                                                                        CssClass="form-control input-sm" AppendDataBoundItems="true">
                                                                        <asp:ListItem Text="Todas" Value="0" />
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

                                        <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                            data-filter-min="3" data-filter-placeholder="Buscar"
                                            data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}"
                                            data-paging-limit="10" data-filtering="true"
                                            data-filter-container="#filter-form-container" data-filter-delay="300"
                                            data-filter-dropdown-title="Buscar en:" data-filter-position="left"
                                            data-empty="Sin resultados">
                                            <thead>
                                                <tr>
                                                    <th data-sortable="false" data-breakpoints="xs"></th>
                                                    <th data-sortable="true" data-breakpoints="xs">Activo</th>
                                                    <%--<th data-sortable="true" data-breakpoints="xs">Categoría</th>--%>
                                                    <th data-breakpoints="all" data-title="Info"></th>
                                                    <th data-breakpoints="xs sm md">Estado</th>
                                                    <th data-breakpoints="xs sm md">Prioridad</th>
                                                    <th class="text-nowrap" data-breakpoints="xs" width="150px">Fecha</th>
                                                    <th class="text-nowrap" data-breakpoints="xs" width="150px">Hace cuánto?</th>
                                                    <th class="text-nowrap" data-breakpoints="xs">Sede</th>
                                                    <th class="text-nowrap" data-breakpoints="xs">Responsable</th>
                                                    <th data-sortable="false" class="text-right" >Acciones</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpTickets" runat="server" OnItemDataBound="rpTickets_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td></td>
                                                            <%--<td class="client-avatar"><img alt="image" src="img/activos/<%# Eval("ImagenActivo") %>"></td>--%>
                                                            <td><%# Eval("NombreActivoFijo") %><br />
                                                                <%# Eval("CodigoInterno") %></td>
                                                            <%--<td><%# Eval("NombreCategoriaActivo") %></td>--%>
                                                            <td>
                                                                <table class="table table-bordered table-striped">
                                                                    <tr>
                                                                        <th width="25%"><i class="fa fa-dumbbell m-r-xs"></i>Activo</th>
                                                                        <th width="50%"><i class="fa fa-message m-r-xs"></i>Descripción</th>
                                                                        <th width="25%"><i class="fa fa-user m-r-xs"></i>Reportado por</th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><img src='img/activos/<%# Eval("ImagenActivo") %>' class="img-responsive" width="100px" /></td>
                                                                        <td><%# Eval("DescripcionTicket") %></td>
                                                                        <td><%# Eval("NombreUsuario") %></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td><span class="badge badge-<%# Eval("badge") %>"><%# Eval("EstadoTicket") %></span></td>
                                                            <td><i class="fa fa-circle m-r-sm text-<%# Eval("badge2") %>"></i><%# Eval("PrioridadTicket") %></td>
                                                            <td><%# Eval("FechaCreacionTicket", "{0:dd MMM yyyy}") %></br>
                                                                    <%# Eval("FechaCreacionTicket", "{0:hh:mm:ss}") %>
                                                            </td>
                                                            <td>
                                                                <asp:Literal ID="ltTiempoTranscurrido" runat="server"></asp:Literal></td>
                                                            <td><%# Eval("NombreSede") %></td>
                                                            <td><%# Eval("Responsable") %></td>
                                                            <td>
                                                                <button type="button" runat="server" id="btnAsignar"
                                                                    class="btn btn-outline btn-warning pull-right m-r-xs"
                                                                    style="padding: 1px 2px 1px 2px; margin-bottom: 0px;"
                                                                    title="Asignar técnico">
                                                                    <i class="fa fa-user-plus"></i>
                                                                </button>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5><i class="fas fa-user-plus text-success m-r-sm"></i>Asignación de responsable</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content" runat="server" id="divAsignacion">

                                        <div class="row">
                                            <div class="col-lg-4">
                                                <h4>
                                                    <asp:Literal ID="ltActivo" runat="server"></asp:Literal><br />
                                                    <asp:Literal ID="ltCodigo" runat="server"></asp:Literal>
                                                </h4>
                                                <img src="img/activos/caminadora_cybex.jpg" width="120px" class="img-responsive">
                                            </div>
                                            <div class="col-lg-8">
                                                <p class="small">
                                                    <b>Descripción:</b><br />
                                                    <asp:Literal ID="ltDescripcion" runat="server"></asp:Literal>
                                                </p>
                                            </div>
                                        </div>

                                        <p class="small font-bold">
                                            Prioridad: 
                                                <span>
                                                    <asp:Literal ID="ltCirculoPrioridad" runat="server"></asp:Literal>
                                                    <%--<i class="fa fa-circle text-warning m-r-sm"></i>--%>
                                                    <asp:Literal ID="ltPrioridad" runat="server"></asp:Literal>
                                                </span>
                                        </p>

                                        <div class="form-group">
                                            <label>Responsable</label>
                                            <asp:DropDownList ID="ddlUsuarios" runat="server" CssClass="form-control input-sm" 
                                                AppendDataBoundItems="true">
                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvUsuarios" runat="server" ErrorMessage="* Campo requerido"
                                                ControlToValidate="ddlUsuarios" ValidationGroup="agregar"
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
                        </form>
                    </div>
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
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Select2 -->
    <script src="js/plugins/select2/select2.full.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();

        function initSelect2() {
            $('.select2').select2({
                width: '100%',
                minimumResultsForSearch: Infinity, // 👈 Quita el buscador
                templateResult: formatPrioridad,
                templateSelection: formatPrioridad,
                escapeMarkup: function (markup) { return markup; }
            });
        }

        function initSelect3() {
            $('.select3').select2({
                width: '100%',
                minimumResultsForSearch: Infinity, // 👈 Quita el buscador
                templateResult: formatPrioridad,
                templateSelection: formatPrioridad,
                escapeMarkup: function (markup) { return markup; }
            });
        }

        function formatPrioridad(option) {
            if (!option.id) {
                return option.text;
            }

            let icon = '';
            switch (option.text) {
                case 'Alta':
                    icon = "<i class='fa fa-circle text-danger'></i>";
                    break;
                case 'Media':
                    icon = "<i class='fa fa-circle text-warning'></i>";
                    break;
                case 'Baja':
                    icon = "<i class='fa fa-circle text-info'></i>";
                    break;
                case 'Pendiente':
                    icon = "<i class='fa fa-circle text-warning'></i>";
                    break;
                case 'En Proceso':
                    icon = "<i class='fa fa-circle text-info'></i>";
                    break;
                case 'Resuelto':
                    icon = "<i class='fa fa-circle text-navy'></i>";
                    break;
                case 'Cancelado':
                    icon = "<i class='fa fa-circle text-success'></i>";
                    break;
                default:
                    icon = "<i class='fa fa-circle text-muted'></i>";
            }

            return "<span>" + icon + " " + option.text + "</span>";
        }

        $(document).ready(function () {
            initSelect2();
            initSelect3();
        });
    </script>

</body>

</html>
