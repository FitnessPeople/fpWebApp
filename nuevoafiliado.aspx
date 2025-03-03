<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nuevoafiliado.aspx.cs" Inherits="fpWebApp.nuevoafiliado" %>

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

    <title>Fitness People | Nuevo afiliado</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/dropzone/basic.css" rel="stylesheet" />
    <link href="css/plugins/dropzone/dropzone.css" rel="stylesheet" />
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet" />

    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#nuevoafiliado");
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
                    <h4 class="modal-title">Guía para crear un nuevo afiliado</h4>
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

        <uc1:navbar runat="server" ID="navbar1" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header1" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa fa-id-card text-success m-r-sm"></i>Nuevo afiliado</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Afiliados</li>
                        <li class="active"><strong>Nuevo afiliado</strong></li>
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
                            <h5>Formulario para la creación de un nuevo afiliado</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>

                            <div class="row">
                                <form role="form" id="form" enctype="multipart/form-data" runat="server">
                                    <div class="col-sm-6 b-r">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Nombre(s):</label>
                                                    <asp:TextBox ID="txbNombre" CssClass="form-control input-sm" runat="server" placeholder="Nombre(s)"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Apellido(s):</label>
                                                    <asp:TextBox ID="txbApellido" CssClass="form-control input-sm" runat="server" placeholder="Apellido(s)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Nro. de Documento:</label>
                                                    <asp:TextBox ID="txbDocumento" CssClass="form-control input-sm" runat="server" placeholder="Documento"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Tipo de Documento:</label>
                                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" 
                                                        AppendDataBoundItems="true" DataTextField="TipoDocumento" 
                                                        DataValueField="idTipoDoc" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Teléfono:</label>
                                                    <asp:TextBox ID="txbTelefono" CssClass="form-control input-sm" runat="server" placeholder="Teléfono"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Email:</label>
                                                    <asp:TextBox ID="txbEmail" CssClass="form-control input-sm" runat="server" placeholder="Email" required></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <label>Dirección:</label>
                                                    <asp:TextBox ID="txbDireccion" CssClass="form-control input-sm" runat="server" placeholder="Dirección"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label>Ciudad:</label>
                                                    <asp:DropDownList ID="ddlCiudadAfiliado" runat="server" 
                                                        AppendDataBoundItems="true" DataTextField="NombreCiudad" 
                                                        DataValueField="idCiudad" CssClass="chosen-select input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label>Empresa convenio:</label>
                                                    <asp:DropDownList ID="ddlEmpresaConvenio" runat="server" 
                                                        AppendDataBoundItems="true" DataTextField="RazonSocial" 
                                                        DataValueField="idEmpresaAfiliada" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Ninguna" Value="null"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label>Foto:</label>
                                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                                <div class="form-control input-sm" data-trigger="fileinput">
                                                    <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                    <span class="fileinput-filename"></span>
                                                </div>
                                                <span class="input-group-addon btn btn-success btn-file input-sm">
                                                    <span class="fileinput-new input-sm">Seleccionar foto</span>
                                                    <span class="fileinput-exists input-sm">Cambiar</span>
                                                    <input type="file" name="fileFoto" id="fileFoto" accept="image/*">
                                                </span>
                                                <a href="#" class="input-group-addon btn btn-danger fileinput-exists input-sm" 
                                                    data-dismiss="fileinput">Quitar</a>
                                            </div>
                                            <asp:Image runat="server" CssClass="img-rounded" ID="imgFoto" Width="150px" />
                                        </div>
                                    </div>

                                    <div class="col-sm-6">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Fecha nacimiento:</label>
                                                    <asp:TextBox ID="txbFechaNac" CssClass="form-control input-sm" runat="server" name="txbFechaNac"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Género:</label>
                                                    <asp:DropDownList ID="ddlGenero" runat="server" AppendDataBoundItems="true" DataTextField="Genero" DataValueField="idGenero" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Estado civil:</label>
                                                    <asp:DropDownList ID="ddlEstadoCivil" runat="server" AppendDataBoundItems="true" DataTextField="EstadoCivil" DataValueField="idEstadoCivil" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Profesión:</label>
                                                    <asp:DropDownList ID="ddlProfesiones" runat="server" AppendDataBoundItems="true" DataTextField="Profesion" DataValueField="idProfesion" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>EPS:</label>
                                                    <asp:DropDownList ID="ddlEps" runat="server" AppendDataBoundItems="true" DataTextField="NombreEps" DataValueField="idEps" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Sede:</label>
                                                    <asp:DropDownList ID="ddlSedes" runat="server" AppendDataBoundItems="true" DataTextField="NombreSede" DataValueField="idSede" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label>Persona responsable:</label>
                                            <asp:TextBox ID="txbResponsable" CssClass="form-control input-sm" runat="server" placeholder="Nombre responsable"></asp:TextBox>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Parentesco:</label>
                                                    <asp:DropDownList ID="ddlParentesco" runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="Padre/Madre" Value="Padre/Madre"></asp:ListItem>
                                                        <asp:ListItem Text="Esposo/a" Value="Esposo/a"></asp:ListItem>
                                                        <asp:ListItem Text="Hermano/a" Value="Hermano/a"></asp:ListItem>
                                                        <asp:ListItem Text="Hijo/a" Value="Hijo/a"></asp:ListItem>
                                                        <asp:ListItem Text="Primo/a" Value="Primo/a"></asp:ListItem>
                                                        <asp:ListItem Text="Sobrino/a" Value="Sobrino/a"></asp:ListItem>
                                                        <asp:ListItem Text="Encargado/a" Value="Encargado/a"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Teléfono de contacto:</label>
                                                    <asp:TextBox ID="txbTelefonoContacto" CssClass="form-control input-sm" runat="server" placeholder="Teléfono"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div>
                                            <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button" onclick="window.location.href='afiliados'"><strong>Cancelar</strong></button>
                                            <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right" Text="Agregar" OnClick="btnAgregar_Click" />
                                        </div>
                                    </div>
                                </form>
                            </div>

                            <%--Fin Contenido!!!!--%>
                        </div>
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

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <script>

        $.validator.setDefaults({ ignore: ":hidden:not(.chosen-select)" })

        $("#form").validate({
            rules: {
                txbNombre: {
                    required: true,
                    minlength: 3
                },
                txbApellido: {
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
                txbTelefono: {
                    required: true,
                    minlength: 10
                },
                txbDireccion: {
                    required: true,
                    minlength: 10
                },
                ddlCiudadAfiliado: {
                    required: true
                },
                ddlGenero: {
                    required: true
                },
                txbFechaNac: {
                    required: true
                },
                ddlEstadoCivil: {
                    required: true,
                },
                ddlProfesiones: {
                    required: true,
                },
                ddlEps: {
                    required: true,
                },
                ddlSedes: {
                    required: true,
                },
                txbResponsable: {
                    required: true,
                    minlength: 10
                },
                ddlParentesco: {
                    required: true,
                },
                txbTelefonoContacto: {
                    required: true,
                    minlength: 10
                },
            },
            messages: {
                ddlCiudadAfiliado: "*",
            }
        });

        $('.chosen-select').chosen({ width: "100%", disable_search_threshold: 10, no_results_text: "Sin resultados" });
    </script>

</body>

</html>