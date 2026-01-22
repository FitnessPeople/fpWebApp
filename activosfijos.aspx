<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="activosfijos.aspx.cs" Inherits="fpWebApp.activosfijos" %>

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

    <title>Fitness People | Tickets soporte</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <style type="text/css" media="print">
        body {
            visibility: hidden;
            display: none
        }
    </style>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#activosfijos");
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
                    <h2><i class="fas fa-dumbbell text-success m-r-sm"></i>Activos fijos</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Sistema</li>
                        <li class="active"><strong>Activos fijos</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">

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

                    <form role="form" id="form" runat="server" enctype="multipart/form-data" >
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
                                                <div class="form-group">
                                                    <label>Nombre Activo:</label>
                                                    <asp:TextBox ID="txbActivo" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvActivo" runat="server" ErrorMessage="* Campo requerido"
                                                        ControlToValidate="txbActivo" ValidationGroup="agregar"
                                                        CssClass="font-bold text-danger">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <%--<div class="form-group">
                                                    <label>Dirección:</label>
                                                    <asp:TextBox ID="txbDireccion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDirSede" runat="server" ErrorMessage="* Campo requerido"
                                                        ControlToValidate="txbDireccion" ValidationGroup="agregar"
                                                        CssClass="font-bold text-danger" InitialValue="">
                                                    </asp:RequiredFieldValidator>
                                                </div>--%>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Categoría:</label>
                                                            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control input-sm"
                                                                DataTextField="NombreCiudadSede" DataValueField="idCiudadSede" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" ErrorMessage="* Campo requerido"
                                                                ControlToValidate="ddlCategoria" ValidationGroup="agregar"
                                                                CssClass="font-bold text-danger" InitialValue="">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Codigo Interno:</label>
                                                            <asp:TextBox ID="txbCodigoInterno" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvCodigoInterno" runat="server" ErrorMessage="* Campo requerido"
                                                                ControlToValidate="txbCodigoInterno" ValidationGroup="agregar"
                                                                CssClass="font-bold text-danger">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Marca:</label>
                                                            <asp:TextBox ID="txbMarca" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvMarca" runat="server" ErrorMessage="* Campo requerido"
                                                                ControlToValidate="txbMarca" ValidationGroup="agregar"
                                                                CssClass="font-bold text-danger">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Proveedor:</label>
                                                            <asp:TextBox ID="txbProveedor" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvProveedor" runat="server" ErrorMessage="* Campo requerido"
                                                                ControlToValidate="txbProveedor" ValidationGroup="agregar"
                                                                CssClass="font-bold text-danger">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Sede:</label>
                                                            <asp:DropDownList ID="ddlSede" runat="server" 
                                                                CssClass="form-control input-sm" AppendDataBoundItems="true">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvSede" runat="server" ErrorMessage="* Campo requerido"
                                                                ControlToValidate="ddlSede" ValidationGroup="agregar"
                                                                CssClass="font-bold text-danger" InitialValue="">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Fecha de ingreso:</label>
                                                            <asp:TextBox ID="txbFechaIngreso" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Imagen:</label>
                                                    <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                                        <div class="form-control input-sm" data-trigger="fileinput">
                                                            <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                            <span class="fileinput-filename"></span>
                                                        </div>
                                                        <span class="input-group-addon btn btn-success btn-file input-sm">
                                                            <span class="fileinput-new input-sm">Seleccionar imagen</span>
                                                            <span class="fileinput-exists input-sm">Cambiar</span>
                                                            <input type="file" name="fileFoto" id="fileFoto" accept="image/*">
                                                        </span>
                                                        <a href="#" class="input-group-addon btn btn-danger fileinput-exists input-sm"
                                                            data-dismiss="fileinput">Quitar</a>
                                                    </div>
                                                    <asp:Image runat="server" CssClass="img-rounded" ID="imgFoto" Width="150px" />
                                                </div>
                                                
                                                <div class="form-group">
                                                    <a href="activosfijos" class="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md">Cancelar</a>
                                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click"
                                                        CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" ValidationGroup="agregar"/>
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
                                        <h5>Lista de Activos Fijos</h5>
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
                                                                <label class="col-lg-3 control-label">Sede</label>
                                                                <div class="col-lg-9">
                                                                    <asp:DropDownList ID="ddlSedes" runat="server" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlSedes_SelectedIndexChanged"
                                                                        CssClass="form-control input-sm" AppendDataBoundItems="true">
                                                                        <asp:ListItem Text="Todas" Value="" />
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group form-horizontal">
                                                                <label class="col-lg-4 control-label">Categoría</label>
                                                                <div class="col-lg-8">
                                                                    <asp:DropDownList ID="ddlCategorias" runat="server" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged"
                                                                        CssClass="form-control input-sm" AppendDataBoundItems="true">
                                                                        <asp:ListItem Text="Todas" Value="" />
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
                                                    <i class="fa fa-file-excel m-r-xs"></i>EXCEL
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
                                                    <th data-sortable="false" data-breakpoints="xs"></th>
                                                    <th data-sortable="true" data-breakpoints="xs">Activo</th>
                                                    <th data-sortable="true" data-breakpoints="xs">Categoría</th>
                                                    <th data-sortable="false" data-breakpoints="xs sm md">Estado</th>
                                                    <th data-breakpoints="xs sm md">Código interno</th>
                                                    <th data-breakpoints="xs sm md">Marca</th>
                                                    <th class="text-nowrap" data-breakpoints="xs">Proveedor</th>
                                                    <th class="text-nowrap" data-breakpoints="xs" width="150px">Fecha ingreso</th>
                                                    <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpActivosFijos" runat="server" OnItemDataBound="rpActivosFijos_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <img src='img/activos/<%# Eval("ImagenActivo") %>' alt="imagen" class="img-responsive" width="120px" /></td>
                                                            <td><%# Eval("NombreActivoFijo") %></td>
                                                            <td><%# Eval("NombreCategoriaActivo") %></td>
                                                            <td><%# Eval("EstadoActivoFijo") %></td>
                                                            <td><%# Eval("CodigoInterno") %></td>
                                                            <td><%# Eval("Marca") %></td>
                                                            <td><%# Eval("Proveedor") %></td>
                                                            <td><%# Eval("FechaIngreso", "{0:dd MMM yyyy}") %></td>
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

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();
    </script>

</body>

</html>
