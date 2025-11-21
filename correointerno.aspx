<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="correointerno.aspx.cs" Inherits="fpWebApp.correointerno" %>

<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Correo interno</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
</head>

<body onload="changeClass()">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <i class="fa fa-person-chalkboard modal-icon"></i>
                    <h4 class="modal-title">Guía para editar un especialista</h4>
                    <small class="font-bold">¡Bienvenido! A continuación, te ofrecemos una guía sencilla para ayudarte a completar el formulario de manera correcta y eficiente. Sigue estos pasos para asegurarte de que toda la información se registre de forma adecuada.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>1. Lee las Instrucciones</b><br />
                        Antes de comenzar, es importante que leas todas las instrucciones del formulario. Esto te ayudará a entender qué información se requiere y cómo debe ser presentada.
                        <br />
                        <br />
                        <b>2. Reúne la Información Necesaria</b><br />
                        Asegúrate de tener a mano todos los documentos e información que necesitas, como:
                        Datos personales (nombre, dirección, número de teléfono, etc.)
                        Información específica relacionada con el propósito del formulario (por ejemplo, detalles de empleo, historial médico, etc.)
                        <br />
                        <br />
                        <b>3. Completa los Campos Requeridos</b><br />
                        Campos Obligatorios: Identifica cuáles son los campos obligatorios (generalmente marcados con un asterisco *) y asegúrate de completarlos.
                        Campos Opcionales: Si hay campos opcionales, completa solo los que consideres relevantes.
                        <br />
                        <br />
                        <b>4. Confirma la Información</b><br />
                        Asegúrate de que todos los datos ingresados son correctos y actualizados. Una revisión final puede evitar errores que podrían complicar el proceso.
                        <br />
                        <br />
                        <b>5. Envía el Formulario</b><br />
                        Asegúrate de seguir el proceso de envío indicado (hacer clic en "Agregar").
                        <br />
                        <br />
                        ¡Siguiendo estos pasos, estarás listo para diligenciar tu formulario sin problemas! Si tienes dudas, no dudes en consultar con el administrador del sistema.
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
                    <h2><i class="fa fa-inbox text-success m-r-sm"></i>Bandeja de entrada</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Menu principal</li>
                        <li class="active"><strong>Correo interno</strong></li>
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

                    <div runat="server" id="divContenido">
                        <div class="col-lg-3">
                            <div class="ibox float-e-margins">
                                <div class="ibox-content mailbox-content">
                                    <div class="file-manager">
                                        <a class="btn btn-block btn-primary compose-mail" href="redactarcorreo">Redactar</a>
                                        <div class="space-25"></div>
                                        <h5>Carpetas</h5>
                                        <ul class="folder-list m-b-md" style="padding: 0">
                                            <li><a href="correointerno"><i class="fa fa-inbox "></i>Bandeja de entrada
                                                <span class="label label-warning pull-right">
                                                <asp:Literal ID="ltNroMensajesSinLeer" runat="server"></asp:Literal>/<asp:Literal ID="ltNroMensajesTotal" runat="server"></asp:Literal></span></a></li>
                                            <li><a href="correoenviado"><i class="fa fa-paper-plane"></i>Enviados
                                                <span class="label label-default pull-right">
                                                <asp:Literal ID="ltNroMensajesEnviados" runat="server"></asp:Literal></span></a></li>
                                            <li><a href="correoeliminado"><i class="fa fa-trash"></i>Papelera
                                                <span class="label label-default pull-right">
                                                <asp:Literal ID="ltNroMensajesPapelera" runat="server"></asp:Literal></span></a></li>
                                        </ul>
                                        <h5>Categorías</h5>
                                        <ul class="category-list" style="padding: 0">
                                            <asp:Repeater ID="rpCategorias" runat="server">
                                                <ItemTemplate>
                                                    <li><a href="#"><i class="fa fa-circle text-<%# Eval("ColorCategoria") %>"></i><%# Eval("NombreCategoria") %></a></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                        <h5 class="tag-title">Etiquetas</h5>
                                        <ul class="tag-list" style="padding: 0">
                                            <li><a href=""><i class="fa fa-tag m-r-xs"></i>Urgente</a></li>
                                            <li><a href=""><i class="fa fa-tag m-r-xs"></i>Alto impacto</a></li>
                                            <li><a href=""><i class="fa fa-tag m-r-xs"></i>Confidencial</a></li>
                                            <li><a href=""><i class="fa fa-tag m-r-xs"></i>Requiere aprobación</a></li>
                                            <li><a href=""><i class="fa fa-tag m-r-xs"></i>Seguimiento</a></li>
                                        </ul>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-9 animated fadeInRight">
                            <form runat="server" id="form1">
                                <div class="mail-box-header">

                                    <div class="pull-right mail-search">
                                        <div class="input-group">
                                            <input type="text" class="form-control input-sm" name="search" placeholder="Buscar correo">
                                            <div class="input-group-btn">
                                                <button type="submit" class="btn btn-sm btn-primary">
                                                    Buscar
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <h2><i class="fa fa-inbox"></i> Bandeja de entrada (<asp:Literal ID="ltNroMensajesSinLeer2" runat="server"></asp:Literal>)
                                    </h2>
                                    <div class="mail-tools tooltip-demo m-t-md">
                                        <div class="btn-group pull-right">
                                            <%--<button class="btn btn-white btn-sm"><i class="fa fa-arrow-left"></i></button>
                                        <button class="btn btn-white btn-sm"><i class="fa fa-arrow-right"></i></button>--%>
                                            <asp:LinkButton ID="btnAnterior" runat="server" CssClass="btn btn-white btn-sm"
                                                OnClick="btnAnterior_Click" ToolTip="Anterior">
                                                <i class="fa fa-arrow-left"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnSiguiente" runat="server" CssClass="btn btn-white btn-sm"
                                                OnClick="btnSiguiente_Click">
                                                <i class="fa fa-arrow-right"></i>
                                            </asp:LinkButton>
                                        </div>
                                        <button class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="left" title="Refrescar"><i class="fa fa-refresh m-r-xs"></i>Refrescar</button>
                                        <button class="btn btn-white btn-sm text-info" data-toggle="tooltip" data-placement="top" title="Marcar como leído"><i class="fa fa-eye"></i></button>
                                        <button class="btn btn-white btn-sm text-warning" data-toggle="tooltip" data-placement="top" title="Marcar como importante"><i class="fa fa-exclamation"></i></button>
                                        <button class="btn btn-white btn-sm text-danger" data-toggle="tooltip" data-placement="top" title="Mover a la papelera"><i class="fa fa-trash"></i></button>

                                    </div>

                                </div>
                                <div class="mail-box">
                                    <table class="table table-hover table-mail">
                                        <tbody>
                                            <asp:Repeater ID="rpMensajes" runat="server" OnItemDataBound="rpMensajes_ItemDataBound">
                                                <ItemTemplate>

                                                    <%--<tr class="read">--%>
                                                    <tr id="fila" runat="server">
                                                        <td class="check-mail">
                                                            <input type="checkbox" class="i-checks">
                                                        </td>
                                                        <td class="mail-ontact"><a href="detallecorreo?idCorreo=<%# Eval("idCorreo") %>">De: <%# Eval("Remitente") %></a>
                                                            <span class="label label-<%# Eval("ColorCategoria") %> pull-right"><%# Eval("NombreCategoria") %></span>
                                                        </td>
                                                        <td class="mail-subject"><a href="detallecorreo?idCorreo=<%# Eval("idCorreo") %>"><%# Eval("Asunto") %></a></td>
                                                        <td class=""></td>
                                                        <td class="text-right mail-date">
                                                            <asp:Literal ID="ltTiempoTranscurrido" runat="server"></asp:Literal>
                                                        </td>
                                                    </tr>

                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </tbody>
                                    </table>
                                </div>
                            </form>
                        </div>
                    </div>
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
    <script src="js/plugins/footable/footable.all.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <!-- Page-Level Scripts -->

</body>

</html>
