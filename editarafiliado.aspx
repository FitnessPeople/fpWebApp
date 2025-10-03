<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editarafiliado.aspx.cs" Inherits="fpWebApp.editarafiliado"  %>

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

    <title>Fitness People | Editar afiliado</title>

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

    <!-- SweetAlert2 CDN -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#afiliados");
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
                    <i class="fa fa-id-card modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para modificar información de un afiliado</h4>
                    <small class="font-bold">¡Bienvenido! Sigue estos pasos para actualizar la información de manera rápida y segura.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Modifica los datos necesarios</b><br />
                        Actualiza solo los campos que <b>requieran</b> cambios:<br />
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i><b>Nombre(s), Apellido(s), Tipo y Número de Documento.</b><br />
                        <i class="fa-solid fa-calendar-days" style="color: #0D6EFD;"></i><b>Fecha de Nacimiento, Género, Estado Civil.</b><br />
                        <i class="fa-solid fa-phone" style="color: #0D6EFD;"></i><b>Teléfono, Correo, Dirección, Ciudad.</b><br />
                        <i class="fa-solid fa-user-tie" style="color: #0D6EFD;"></i><b>Profesión, EPS, Empresa Convenio, Sede, Foto (si aplica).</b><br />
                        <i class="fa-solid fa-user-group" style="color: #0D6EFD;"></i><b>Nombre, Parentesco y Teléfono de Contacto.</b>
                        <br />
                        <br />
                        <b>Paso 2: Completa el formulario</b><br />
                        <i class="fa-solid fa-pencil"></i>Llena todos los <b>campos obligatorios</b> (generalmente marcados con *).<br />
                        <i class="fa-solid fa-magnifying-glass"></i>Verifica que los datos estén <b>correctos y actualizados</b>.
                    <br />
                        <br />
                        <b>Paso 3: Confirma o cancela</b><br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i><b>Actualizar:</b> Guarda los <b>cambios</b> realizados.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i><b>Cancelar:</b> Si necesitas volver <b>atrás</b> sin guardar cambios.
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
                    <h2><i class="fa fa-id-card text-success m-r-sm"></i>Editar afiliado</h2>
                    <ol class="breadcrumb">
                        <li>
                            <a href="inicio.aspx">Inicio</a>
                        </li>
                        <li class="active">
                            <strong>Afiliados</strong>
                        </li>
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
                            <h5>Formulario para la modificación de un afiliado</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">

                            <div class="alert alert-danger alert-dismissable" runat="server" id="divMensaje1" visible="false">
                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                No existe el afiliado para editar.<br />
                                <a class="alert-link" href="#">Intente nuevamente</a>.
                            </div>

                            <div class="row">
                                <form role="form" id="form" enctype="multipart/form-data" runat="server">
                                    <div class="col-sm-6 b-r">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Nombre(s):</label>
                                                    <asp:TextBox ID="txbNombre" CssClass="form-control input-sm" runat="server" placeholder="Nombre(s)"
                                                        autocomplete="off" spellcheck="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Apellido(s):</label>
                                                    <asp:TextBox ID="txbApellido" CssClass="form-control input-sm" runat="server" placeholder="Apellido(s)"
                                                        autocomplete="off" spellcheck="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Nro. de Documento:</label>
                                                    <asp:TextBox ID="txbDocumento" CssClass="form-control input-sm" runat="server" Enabled="false"
                                                        autocomplete="off" spellcheck="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Tipo de Documento:</label>
                                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" AppendDataBoundItems="true" DataTextField="TipoDocumento" 
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
                                                    <asp:TextBox ID="txbEmail" CssClass="form-control input-sm" runat="server" placeholder="Email" required="*">
                                                        autocomplete="off" spellcheck="false"
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-7">
                                                <div class="form-group">
                                                    <label>Dirección:</label>
                                                    <asp:TextBox ID="txbDireccion" CssClass="form-control input-sm" runat="server" placeholder="Dirección"
                                                        autocomplete="off" spellcheck="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label>Ciudad:</label>
                                                    <asp:DropDownList ID="ddlCiudadAfiliado" runat="server"
                                                        AppendDataBoundItems="true" DataTextField="NombreCiudad"
                                                        DataValueField="idCiudad" CssClass="chosen-select form-control input-sm m-b">
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
                                                        <asp:ListItem Text="Ninguna" Value="0"></asp:ListItem>
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
                                                    <asp:DropDownList ID="ddlEstadoCivil" runat="server" AppendDataBoundItems="true"
                                                        DataTextField="EstadoCivil" DataValueField="idEstadoCivil" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Profesión/Oficio:</label>
                                                    <asp:DropDownList ID="ddlProfesiones" runat="server" AppendDataBoundItems="true"
                                                        DataTextField="Profesion" DataValueField="idProfesion" CssClass="chosen-select form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>EPS:</label>
                                                    <asp:DropDownList ID="ddlEps" runat="server" AppendDataBoundItems="true"
                                                        DataTextField="NombreEps" DataValueField="idEps" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Sede:</label>
                                                    <asp:DropDownList ID="ddlSedes" runat="server" AppendDataBoundItems="true"
                                                        DataTextField="NombreSede" DataValueField="idSede" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label>Persona responsable:</label>
                                            <asp:TextBox ID="txbResponsable" CssClass="form-control input-sm" runat="server" placeholder="Nombre responsable"
                                                autocomplete="off" spellcheck="false"></asp:TextBox>
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
                                                    <asp:TextBox ID="txbTelefonoContacto" CssClass="form-control input-sm" runat="server" placeholder="Teléfono"
                                                        spellcheck="false" autocomplete="new-password" autocorrect="off"   autocapitalize="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label>Estado:</label>
                                            <asp:RadioButtonList ID="rblEstado" runat="server" RepeatDirection="Horizontal"
                                                CssClass="form-control input-sm" Enabled="false">
                                                <asp:ListItem Text="&nbsp;Activo&nbsp;&nbsp;&nbsp;&nbsp;" Value="Activo"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;Inactivo&nbsp;&nbsp;&nbsp;&nbsp;" Value="Inactivo"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;Pendiente&nbsp;&nbsp;&nbsp;&nbsp;" Value="Pendiente"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;Verificado" Value="Verificado"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div>
                                            <button id="btnCancelar" class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button" runat="server" onclick="window.location.href='afiliados'"><strong>Cancelar</strong></button>
                                            <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right" Text="Actualizar" OnClick="btnActualizar_Click" />
                                            <button id="btnVolver" runat="server" type="button" class="btn btn-sm btn-info pull-right m-t-n-xs" onclick="window.location.href='agendacrm.aspx';">
                                               Regresar a Agenda CRM</button>
                                            <asp:Button ID="btnActualizaryVenderPlan" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right"
                                                Text="Actualizar y Vender Plan" Visible="false" OnClick="btnActualizaryVenderPlan_Click" />
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

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <!-- DROPZONE -->
    <script src="js/plugins/dropzone/dropzone.js"></script>

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <script>
        Dropzone.options.dropzoneForm = {
            paramName: "file", // The name that will be used to transfer the file
            maxFilesize: 2, // MB
            dictDefaultMessage: "<strong>Arrastre hasta aquí o haga clic para cargar la foto.</strong>",
            maxFiles: 1,
            acceptedFiles: "image/*"
        };

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
                ddlProfesiones: "*",
            }
        });

        $('.chosen-select').chosen({ width: "100%", disable_search_threshold: 10, no_results_text: "Sin resultados" });
    </script>

</body>

</html>
