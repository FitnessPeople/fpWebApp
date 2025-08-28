<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="especialistas.aspx.cs" Inherits="fpWebApp.especialistas" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>
<%@ Register Src="~/controles/indicadoresmedico.ascx" TagPrefix="uc1" TagName="indicadoresmedico" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Especialistas</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />--%>
    <link href="css/plugins/footable/footable.bootstrap.css" rel="stylesheet" />

    <!-- Morris -->
    <link href="css/plugins/morris/morris-0.4.3.min.css" rel="stylesheet">

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
            // Activa el menú principal
            var element1 = document.querySelector("#especialistas");
            if (element1) {
                element1.classList.add("active");
            }

            // Despliega el submenú
            var element2 = document.querySelector("#medico");
            if (element2) {
                element2.classList.add("show"); // en Bootstrap el desplegado es con "show"
                element2.classList.remove("collapse");
            }
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
                    <h2><i class="fa fa-user-doctor text-success m-r-sm"></i>Especialistas</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Asistencial</li>
                        <li class="active"><strong>Especialistas</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">

                    <%--Inicio Contenido!!!!--%>
                    <uc1:indicadoresmedico runat="server" ID="indicadoresmedico" />

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
                        <div class="ibox-title">
                            <h5>Lista de especialistas</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">

                            <div class="row">
                                <form id="form1" runat="server">
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
                                        <a class="btn btn-success pull-right dim m-l-md" style="font-size: 12px;" 
                                            href="nuevoespecialista" title="Agregar especialista" runat="server" 
                                            id="btnAgregar" visible="false"><i class="fa fa-square-plus"></i> NUEVO</a>
                                    </div>
                                </form>
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
                                        <%--<th data-sort-ignore="true">ID</th>--%>
                                        <th data-sortable="false" data-breakpoints="xs" style="width: 110px;">Documento</th>
                                        <th data-breakpoints="xs">Nombre</th>
                                        <th data-breakpoints="xs sm md" style="width: 110px;">Télefono</th>
                                        <th data-breakpoints="xs sm md">Correo</th>
                                        <th data-breakpoints="xs sm md">Profesión</th>
                                        <th data-filterable="false" data-type="date" data-breakpoints="xs sm md">Fecha nacimiento</th>
                                        <th class="text-nowrap" data-breakpoints="xs">Estado</th>
                                        <th data-breakpoints="all" data-title="Info"></th>
                                        <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rpEspecialistas" runat="server" OnItemDataBound="rpEspecialistas_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <%--<td><%# Eval("idEspecialista") %></td>--%>
                                                <td style="white-space: nowrap;"><%# Eval("DocumentoEmpleado") %></td>
                                                <td><%# Eval("NombreEmpleado") %></td>
                                                <td style="white-space: nowrap;"><i class="fab fa-whatsapp m-r-xs font-bold"></i><a href="https://wa.me/57<%# Eval("TelefonoEmpleado") %>" target="_blank"><%# Eval("TelefonoEmpleado") %></a></td>
                                                <td style="white-space: nowrap;"><i class="fa fa-envelope m-r-xs font-bold"></i><%# Eval("EmailEmpleado") %></td>
                                                <td style="white-space: nowrap;"><i class="fa fa-user-tie m-r-xs font-bold"></i><%# Eval("NombreCargo") %></td>
                                                <td style="white-space: nowrap;"><i class="fa fa-cake m-r-xs font-bold"></i><span class="text-<%# Eval("badge") %> font-bold"><%# Eval("FechaNacEmpleado", "{0:dd MMM yyyy}") %> <%# Eval("edad") %></span></td>
                                                <td><span class="badge badge-<%# Eval("badge2") %>"><%# Eval("Estado") %></span></td>
                                                <td>
                                                    <table class="table table-bordered table-striped">
                                                        <tr>
                                                            <th width="40%"><i class="fa fa-map-location-dot m-r-xs"></i>Dirección</th>
                                                            <th width="30%"><i class="fa fa-city m-r-xs"></i>Ciudad</th>
                                                            <th width="30%" class="text-nowrap"><i class="fa fa-venus-mars m-r-xs"></i>Genero</th>
                                                        </tr>
                                                        <tr>
                                                            <td><%# Eval("DireccionEmpleado") %></td>
                                                            <td><%# Eval("NombreCiudad") %> (<%# Eval("NombreEstado") %>)</td>
                                                            <td><%# Eval("Genero") %></td>
                                                        </tr>
                                                        <tr>
                                                            <th width="40%"><i class="fa fa-ring m-r-xs"></i>Estado Civil</th>
                                                            <th width="30%"><i class="fa fa-school-flag m-r-xs"></i>Sede</th>
                                                            <th width="30%"><i class="fa fa-house-medical m-r-xs"></i>EPS</th>
                                                        </tr>
                                                        <tr>
                                                            <td><%# Eval("EstadoCivil") %></td>
                                                            <td><%# Eval("NombreSede") %></td>
                                                            <td><%# Eval("NombreEps") %></td>
                                                        </tr>
                                                    </table>
                                                </td>
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
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

    <!-- Peity -->
    <script src="js/plugins/peity/jquery.peity.min.js"></script>
    <script src="js/demo/peity-demo.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- jQuery UI -->
    <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();
    </script>

</body>

</html>
