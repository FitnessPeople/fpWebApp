<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nuevaempresaafiliada.aspx.cs" Inherits="fpWebApp.nuevaempresaafiliada" %>

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

    <title>Fitness People | Nueva Empresa Convenio</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.3/themes/smoothness/jquery-ui.css">

    <link href="css/plugins/dropzone/basic.css" rel="stylesheet" />
    <link href="css/plugins/dropzone/dropzone.css" rel="stylesheet" />
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet" />

    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#nuevaempresa");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#afiliados2");
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
                    <h4 class="modal-title">Guía para crear una empresa con convenio</h4>
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
                    <h2><i class="fa fa-building text-success m-r-sm"></i>Nueva empresa convenio</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Afiliados</li>
                        <li class="active"><strong>Nueva empresa convenio</strong></li>
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

                    <div class="ibox float-e-margins" runat="server" id="divContenido">
                        <div class="ibox-title">
                            <h5>Formulario para la creación de una nueva empresa con convenio</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <form role="form" id="form" enctype="multipart/form-data" runat="server">
                                    <div class="col-sm-6 b-r">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Nombre comercial</label>
                                                    <asp:TextBox ID="txbNombreCcial" CssClass="form-control input-sm" runat="server" placeholder="Nombre comercial"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Razón social</label>
                                                    <asp:TextBox ID="txbRazonSocial" CssClass="form-control input-sm" runat="server" placeholder="Razón social"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Documento</label>
                                                    <asp:TextBox ID="txbDocumento" CssClass="form-control input-sm" runat="server" placeholder="Documento"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>DV</label>
                                                    <asp:TextBox ID="txbDV" CssClass="form-control input-sm" runat="server" placeholder="DV"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Tipo de Documento</label>
                                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" AppendDataBoundItems="true" DataTextField="TipoDocumento" DataValueField="idTipoDoc" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Teléfono principal</label>
                                                    <asp:TextBox ID="txbTelefonoPpal" CssClass="form-control input-sm" runat="server" placeholder="Teléfono principal"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Teléfono secundario</label>
                                                    <asp:TextBox ID="txbTelefonoSrio" CssClass="form-control input-sm" runat="server" placeholder="Teléfono secundario" ></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Celular</label>
                                                    <asp:TextBox ID="txbCelular" CssClass="form-control input-sm" runat="server" placeholder="Celular"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Correo</label>
                                                    <asp:TextBox ID="txbCorreo" CssClass="form-control input-sm" runat="server" placeholder="Correo" required></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Dirección</label>
                                                    <asp:TextBox ID="txbDireccion" CssClass="form-control input-sm" runat="server" placeholder="Dirección"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Ciudad</label>
                                                    <asp:DropDownList ID="ddlCiudadEmpresa" runat="server" 
                                                        AppendDataBoundItems="true" DataTextField="NombreCiudad" 
                                                        DataValueField="idCiudad" CssClass="chosen-select input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Fecha convenio</label>
                                                    <asp:TextBox ID="txbFechaConvenio" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Nro de empleados</label>
                                                    <asp:TextBox ID="txbNroEmpleados" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Tipo negociación</label>
                                                    <asp:DropDownList ID="ddlTipoNegociacion" runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="Alianza" Value="Alianza"></asp:ListItem>
                                                        <asp:ListItem Text="Convenio" Value="Convenio"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Días de crédito</label>
                                                    <asp:DropDownList ID="ddlDiasCredito" runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                        <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                                        <asp:ListItem Text="90" Value="90"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label>Contrato:</label>
                                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                                <div class="form-control input-sm" data-trigger="fileinput">
                                                    <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                    <span class="fileinput-filename"></span>
                                                </div>
                                                <span class="input-group-addon btn btn-success btn-file input-sm">
                                                    <span class="fileinput-new input-sm">Seleccionar archivo</span>
                                                    <span class="fileinput-exists input-sm">Cambiar</span>
                                                    <input type="file" name="fileConvenio" id="fileConvenio" accept="application/pdf">
                                                </span>
                                                <a href="#" class="input-group-addon btn btn-danger fileinput-exists input-sm" 
                                                    data-dismiss="fileinput">Quitar</a>
                                            </div>
                                        </div>

                                        <div>
                                            <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button" onclick="window.location.href='empresasafiliadas'"><strong>Cancelar</strong></button>
                                            <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right" Text="Agregar" OnClick="btnAgregar_Click"/>
                                        </div>
                                    </div>
                                </form>
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
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <!-- DROPZONE -->
    <script src="js/plugins/dropzone/dropzone.js"></script>

    <script>

        $.validator.setDefaults({ ignore: ":hidden:not(.chosen-select)" })

        $("#form").validate({
            rules: {
                txbNombreCcial: {
                    required: true,
                    minlength: 3
                },
                txbRazonSocial: {
                    required: true,
                    minlength: 3
                },
                txbDocumento: {
                    required: true,
                    minlength: 7
                },
                ddlTipoDocumento: {
                    required: true
                },
                txbTelefonoPpal: {
                    required: true,
                    minlength: 10
                },
                txbTelefonoSrio: {
                    required: true,
                    minlength: 10
                },
                txbCelular: {
                    required: true,
                    minlength: 10
                },
                txbDireccion: {
                    required: true,
                    minlength: 10
                },
                ddlCiudadEmpresa: {
                    required: true,
                },
                txbFechaConvenio: {
                    required: true
                },
                txbNroEmpleados: {
                    required: true
                },
                ddlTipoNegociacion: {
                    required: true,
                },
                ddlDiasCredito: {
                    required: true,
                },
                fileConvenio: {
                    required: true
                },
            },
            messages: {
                fileConvenio: "*",
                ddlCiudadEmpresa: "*",
            }
        });

        $('.chosen-select').chosen({ width: "100%" });
    </script>

</body>

</html>