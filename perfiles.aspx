﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="perfiles.aspx.cs" Inherits="fpWebApp.perfiles" %>

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

    <title>Fitness People | Perfiles</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />

    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

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
            var element1 = document.querySelector("#perfiles");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#gestiontecnica");
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
                    <i class="fa fa-user-shield modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para visualizar perfiles</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar los perfiles de manera clara y eficiente.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Crea uno nuevo</b><br />
                        Usa el campo que está a la <b>izquierda</b> para digitar el nombre que quieres registrar.<br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i> <b>Agregar:</b> Guarda la información y finaliza el registro.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i> <b>Cancelar:</b> Si necesitas volver atrás sin guardar cambios.
                    <br />
                        <br />
                        <b>Paso 2: Visualiza</b><br />
                        Justo debajo se encuentran todos los diferentes perfiles que se han creado.
                    <br />
                        <br />
                        <b>Paso 3: Gestiona perfiles</b><br />
                        En la columna <b>Acciones</b> encontrarás estas opciones:<br />
                        <i class="fa fa-edit" style="color: #1AB394;"></i> <b>Editar:</b> Modifica los datos necesarios.<br />
                        <i class="fa fa-trash" style="color: #DC3545;"></i> <b>Eliminar:</b> Borra lo que creas innecesario.
                     <br />
                        <br />
                        <b>Paso 4: Gestiona permisos</b><br />
                        Usa la tabla que está a la <b>derecha</b> para gestionar lo que desees.<br />
                        Selecciona el perfil.<br />
                        Da click en <i class="fa-solid fa-thumbs-up" style="color: #1AB394;"></i> para modificar sus permisos.
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

        <uc1:navbar runat="server" ID="navbar" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa fa-user-shield text-success m-r-sm"></i>Perfiles</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Gestión técnica</li>
                        <li class="active"><strong>Perfiles</strong></li>
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

                    <form runat="server" id="form">
                        <div class="row" runat="server" id="divContenido">
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

                                        <div class="form-group">
                                            <label>Nombre del perfil:</label>
                                            <asp:TextBox ID="txbPerfil" runat="server" CssClass="form-control input-sm" placeholder="Perfil"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPerfil" runat="server" 
                                                ErrorMessage="* Campo requerido" ControlToValidate="txbPerfil" 
                                                CssClass="text-danger font-bold" ValidationGroup="agregar"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <a href="perfiles" class="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md">Cancelar</a>
                                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" OnClick="btnAgregar_Click" Visible="false" ValidationGroup="agregar" />
                                        </div>
                                        <br />
                                        <br />
                                        <div class="form-group">
                                            <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                        </div>

                                        <table class="footable1 table table-striped" data-paging-size="100" 
                                            data-paging="false" data-sorting="true" data-empty="-">
                                            <thead>
                                                <tr>
                                                    <th>Perfil</th>
                                                    <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpPerfiles" runat="server" OnItemDataBound="rpPerfiles_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr class="feed-element">
                                                            <td><%# Eval("Perfil") %></td>
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

                            <div class="col-lg-8">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Permisos perfiles</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                        <asp:UpdatePanel ID="upPerfiles" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="form-group">
                                                            <label>Perfil</label>
                                                            <asp:DropDownList CssClass="chosen-select form-control input-sm m-b required" 
                                                                ID="ddlPerfiles" runat="server"
                                                                OnSelectedIndexChanged="ddlPerfiles_SelectedIndexChanged" 
                                                                DataValueField="idPerfil" DataTextField="Perfil"
                                                                AutoPostBack="true" AppendDataBoundItems="true">
                                                            </asp:DropDownList>
                                                        </div>

                                                        <table class="footable2 table table-striped" data-paging-size="100" 
                                                            data-paging="false" data-sorting="true" >
                                                            <thead>
                                                                <tr>
                                                                    <th>Página</th>
                                                                    <th data-sortable="false">Tiene acceso?</th>
                                                                    <th data-sortable="false">Puede consultar?</th>
                                                                    <th data-sortable="false">Puede exportar?</th>
                                                                    <th data-sortable="false">Puede crear y modificar?</th>
                                                                    <th data-sortable="false">Puede borrar?</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Repeater ID="rpPaginasPermisos" runat="server" OnItemDataBound="rpPaginasPermisos_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <tr class="feed-element">
                                                                            <td style="vertical-align: middle;"><%# Eval("NombreCategoriaPagina") %> / <%# Eval("Pagina") %></td>
                                                                            <td class="text-center">
                                                                                <asp:LinkButton ID="lb1" runat="server" OnClick="lb1_Click" ClientIDMode="AutoID"><%# Eval("SinPermiso") %></asp:LinkButton></td>
                                                                            <td class="text-center">
                                                                                <asp:LinkButton ID="lb2" runat="server" OnClick="lb2_Click" ClientIDMode="AutoID"><%# Eval("Consulta") %></asp:LinkButton></td>
                                                                            <td class="text-center">
                                                                                <asp:LinkButton ID="lb3" runat="server" OnClick="lb3_Click" ClientIDMode="AutoID"><%# Eval("Exportar") %></asp:LinkButton></td>
                                                                            <td class="text-center">
                                                                                <asp:LinkButton ID="lb4" runat="server" OnClick="lb4_Click" ClientIDMode="AutoID"><%# Eval("CrearModificar") %></asp:LinkButton></td>
                                                                            <td class="text-center">
                                                                                <asp:LinkButton ID="lb5" runat="server" OnClick="lb5_Click" ClientIDMode="AutoID"><%# Eval("Borrar") %></asp:LinkButton></td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:Repeater>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlPerfiles" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
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

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable1').footable();
        $('.footable2').footable();
        $.validator.setDefaults({ ignore: ":hidden:not(.chosen-select)" });
        $('.chosen-select').chosen({ width: "100%", disable_search_threshold: 10, no_results_text: "Sin resultados" });
    </script>

</body>

</html>
