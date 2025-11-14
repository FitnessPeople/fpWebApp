<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detallecorreo.aspx.cs" Inherits="fpWebApp.detallecorreo" %>

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
                    <h2><i class="fa fa-envelope text-success m-r-sm"></i>Correo interno</h2>
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
                                                <asp:Literal ID="ltNroMensajes1" runat="server"></asp:Literal></span></a></li>
                                            <li><a href="#"><i class="fa fa-envelope"></i>Enviados</a></li>
                                            <li><a href="#"><i class="fa fa-certificate"></i>Importantes</a></li>
                                            <li><a href="#"><i class="fa fa-file-text"></i>Documentos <span class="label label-danger pull-right">2</span></a></li>
                                            <li><a href="#"><i class="fa fa-trash"></i>Papelera</a></li>
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
                            <div class="mail-box-header">
                                <div class="pull-right tooltip-demo">
                                    <a href="mail_compose.html" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" title="Responder"><i class="fa fa-reply text-success m-r-xs"></i>Responder</a>
                                    <a href="#" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" title="Imprimir"><i class="fa fa-print text-info"></i></a>
                                    <a href="mailbox.html" class="btn btn-white btn-sm" data-toggle="tooltip" data-placement="top" title="Mover a la papelera"><i class="fa fa-trash text-danger"></i></a>
                                </div>
                                <h2>Ver mensaje
                </h2>
                                <div class="mail-tools tooltip-demo m-t-md">


                                    <h3>
                                        <span class="font-normal">Asunto: </span>
                                        <asp:Literal ID="ltAsunto" runat="server"></asp:Literal>
                                    </h3>
                                    <h5>
                                        <span class="pull-right font-normal">
                                            <asp:Literal ID="ltFechaHora" runat="server"></asp:Literal></span>
                                        <span class="font-normal">De: </span>
                                        <asp:Literal ID="ltRemitente" runat="server"></asp:Literal>
                                    </h5>
                                </div>
                            </div>
                            <div class="mail-box">

                                <div class="mail-body">
                                    <p>
                                        <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                    </p>
                                </div>
                                <%--<div class="mail-attachment">
                                    <p>
                                        <span><i class="fa fa-paperclip"></i>2 adjuntos - </span>
                                        <a href="#">Descargar todos</a>
                                        |
                           
                                        <a href="#">Ver todas las imagenes</a>
                                    </p>

                                    <div class="attachment">
                                        <div class="file-box">
                                            <div class="file">
                                                <a href="#">
                                                    <span class="corner"></span>

                                                    <div class="icon">
                                                        <i class="fa fa-file"></i>
                                                    </div>
                                                    <div class="file-name">
                                                        Document_2014.doc
                                           
                                                        <br />
                                                        <small>Added: Jan 11, 2014</small>
                                                    </div>
                                                </a>
                                            </div>

                                        </div>
                                        <div class="file-box">
                                            <div class="file">
                                                <a href="#">
                                                    <span class="corner"></span>

                                                    <div class="image">
                                                        <img alt="image" class="img-responsive" src="img/p1.jpg">
                                                    </div>
                                                    <div class="file-name">
                                                        Italy street.jpg
                                           
                                                        <br />
                                                        <small>Added: Jan 6, 2014</small>
                                                    </div>
                                                </a>

                                            </div>
                                        </div>
                                        <div class="file-box">
                                            <div class="file">
                                                <a href="#">
                                                    <span class="corner"></span>

                                                    <div class="image">
                                                        <img alt="image" class="img-responsive" src="img/p2.jpg">
                                                    </div>
                                                    <div class="file-name">
                                                        My feel.png
                                           
                                                        <br />
                                                        <small>Added: Jan 7, 2014</small>
                                                    </div>
                                                </a>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>--%>
                                <div class="mail-body text-right tooltip-demo">
                                    <a class="btn btn-sm btn-white" href="#"><i class="fa fa-reply text-success m-r-xs"></i>Responder</a>
                                    <a class="btn btn-sm btn-white" href="#"><i class="fa fa-share text-success m-r-xs"></i>Reenviar</a>
                                    <button title="" data-placement="top" data-toggle="tooltip" type="button" data-original-title="Print" class="btn btn-sm btn-white"><i class="fa fa-print text-info m-r-xs"></i>Imprimir</button>
                                    <button title="" data-placement="top" data-toggle="tooltip" data-original-title="Trash" class="btn btn-sm btn-white"><i class="fa fa-trash text-danger m-r-xs"></i>Eliminar</button>
                                </div>
                                <div class="clearfix"></div>


                            </div>
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
