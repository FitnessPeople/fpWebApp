<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="redactarcorreo.aspx.cs" Inherits="fpWebApp.redactarcorreo" %>

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

    <link href="css/plugins/dropzone/basic.css" rel="stylesheet" />
    <link href="css/plugins/dropzone/dropzone.css" rel="stylesheet" />
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet" />

    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />
    <link href="css/plugins/select2/select2.min.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <!-- CSS de Quill -->
    <link href="https://cdn.jsdelivr.net/npm/quill@2.0.3/dist/quill.snow.css" rel="stylesheet">
    <!-- JS de Quill -->
    <script src="https://cdn.jsdelivr.net/npm/quill@2.0.3/dist/quill.js"></script>

    <script>
        var editorContenido = document.querySelector(".ql-editor");
        console.log(editorContenido);
        //editorContenido.style.height = "600";
        //editorContenido.style.height = editorContenido.scrollHeight + "px";

        var quill;
        document.addEventListener("DOMContentLoaded", function () {
            quill = new Quill("#editor", {
                theme: "snow",
                modules: {
                    toolbar: [
                        [{ 'header': [1, 2, 3, false] }],
                        ['bold', 'strike'], // Negrita y Tachado
                        ['italic', 'underline'],
                        [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                        [{ 'align': [] }],
                        ['link', 'image', 'video'],
                    ]
                }
            });
            //function ajustarAlturaEditor() {
            //    var editorContenido = document.querySelector(".ql-editor");
            //    editorContenido.style.height = "auto";
            //    editorContenido.style.height = editorContenido.scrollHeight + "px";
            //}
            //quill.on("text-change", ajustarAlturaEditor);

            var contenidoGuardado = document.getElementById('<%= hiddenEditor.ClientID %>').value;
        if (contenidoGuardado.trim() !== "") {
            quill.root.innerHTML = contenidoGuardado;
        }
    });
    function guardarContenidoEditor() {
        var contenido = quill.root.innerHTML;
        document.getElementById('<%= hiddenEditor.ClientID %>').value = contenido;
        }
    </script>

    <style type="text/css" media="print">
        body {
            visibility: hidden;
            display: none
        }
    </style>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#usuarios");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#configuracion");
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
                                            <li><a href="correointerno"><i class="fa fa-inbox "></i>Bandeja de entrada<span class="label label-warning pull-right">
                                                <asp:Literal ID="ltNroMensajes1" runat="server"></asp:Literal></span> </a></li>
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
                                <h2>Redactar mensaje</h2>
                            </div>
                            <form runat="server" id="form1">
                                <div class="mail-box">
                                    <div class="mail-body">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Para:</label>
                                                <div class="col-sm-10">
                                                    <%--<asp:DropDownList ID="ddlUsuarios" runat="server"
                                                        CssClass="form-control select2_demo_2"
                                                        multiple="multiple">
                                                    </asp:DropDownList>--%>
                                                    <asp:ListBox ID="ddlUsuarios" runat="server"
                                                        CssClass="form-control select2_demo_2"
                                                        SelectionMode="Multiple">
                                                    </asp:ListBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Asunto:</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txbAsunto" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Categoría:</label>
                                                <div class="col-sm-10">
                                                    <asp:DropDownList ID="ddlCategorias" runat="server" 
                                                        DataTextField="NombreCategoria" DataValueField="idCategoriaCorreo" 
                                                        CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-2 control-label">Mensaje:</label>
                                                <div class="col-sm-10">
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                        <div id="editor" cssclass="form-control input-sm">
                                                            <p>&nbsp;</p>
                                                            <p>&nbsp;</p>
                                                            <p>&nbsp;</p>
                                                            <p>&nbsp;</p>
                                                            <p>&nbsp;</p>
                                                        </div>
                                                        <asp:HiddenField ID="hiddenEditor" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="mail-body">
                                        <div class="form-horizontal">
                                            <div class="form-group m-b-n-xs">
                                                <label>Adjunto:</label>
                                                <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                                    <div class="form-control input-sm" data-trigger="fileinput">
                                                        <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                        <span class="fileinput-filename"></span>
                                                    </div>
                                                    <span class="input-group-addon btn btn-success btn-file input-sm">
                                                        <span class="fileinput-new input-sm">Seleccionar banner</span>
                                                        <span class="fileinput-exists input-sm">Cambiar</span>
                                                        <input type="file" name="fileBanner" id="fileBanner" accept="image/*">
                                                    </span>
                                                    <a href="#" class="input-group-addon btn btn-danger fileinput-exists input-sm" 
                                                        data-dismiss="fileinput">Quitar</a>
                                                </div>
                                                <asp:Literal ID="ltBanner" runat="server"></asp:Literal>
                                                <%--<asp:RequiredFieldValidator ID="rfvBanner" runat="server" ErrorMessage="* Campo requerido"
                                                    ControlToValidate="txbDescripcion" ValidationGroup="agregar"
                                                    CssClass="font-bold text-danger"></asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="mail-body text-right tooltip-demo">
                                        <asp:LinkButton ID="lbEnviar" runat="server" 
                                            CssClass="btn btn-sm btn-primary" data-toggle="tooltip" 
                                            data-placement="top" title="Enviar" OnClick="lbEnviar_Click"><i class="fa fa-reply"></i> Enviar</asp:LinkButton>
                                        <a href="correointerno" class="btn btn-danger btn-sm" data-toggle="tooltip" data-placement="top" title="Descartar"><i class="fa fa-times"></i> Descartar</a>
                                    </div>
                                    <div class="clearfix"></div>
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

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Select2 -->
    <script src="js/plugins/select2/select2.full.min.js"></script>

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.select2_demo_2').select2({
            language: {
                noResults: function () {
                    return "No se encontraron usuarios";
                }
            }
        });
    </script>

</body>

</html>
