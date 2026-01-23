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
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.3/themes/smoothness/jquery-ui.css">

    <!-- Estilos para FileUpload -->
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />

    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <!-- ClockPicker -->
    <link href="css/plugins/clockpicker/clockpicker.css" rel="stylesheet">

    <!-- Sweet alert -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#empresasafiliadas");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#corporativo");
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
                    <i class="fa fa-building modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para registrar una empresa</h4>
                    <small class="font-bold">¡Bienvenido! Sigue estos pasos para completar el registro sin errores.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Prepara la información</b><br />
                        Antes de empezar, asegúrate de tener estos datos a mano:<br />
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i><b>Nombre Comercial, Documento (NIT, RUC, etc.) + DV (dígito de verificación), Razón Social y Tipo de Documento.</b><br />
                        <i class="fa-solid fa-phone" style="color: #0D6EFD;"></i><b>Teléfono Principal y Secundario, Celular, Email, Dirección y Ciudad.</b><br />
                        <i class="fa-solid fa-calendar-days" style="color: #0D6EFD;"></i><b>Fecha de Convenio, Número de Empleados, Tipo de Negociación, Días de Crédito y PDF del Contrato.</b>
                        <br />
                        <br />
                        <b>Paso 2: Completa el formulario</b><br />
                        <i class="fa-solid fa-pencil"></i>Llena todos los campos obligatorios (generalmente marcados con *).<br />
                        <i class="fa-solid fa-magnifying-glass"></i>Verifica que los datos estén correctos y actualizados.
                    <br />
                        <br />
                        <b>Paso 3: Confirma o cancela</b><br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i><b>Agregar:</b> Guarda la información y finaliza el registro.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i><b>Cancelar:</b> Si necesitas volver atrás sin guardar cambios.
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
                                                    <asp:TextBox ID="txbNombreCcial" CssClass="form-control input-sm" runat="server" placeholder="Nombre comercial"
                                                        Style="text-transform: uppercase;" SpellCheck="false" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Razón social</label>
                                                    <asp:TextBox ID="txbRazonSocial" CssClass="form-control input-sm" runat="server" placeholder="Razón social"
                                                        Style="text-transform: uppercase;" SpellCheck="false" autocomplete="off"></asp:TextBox>
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
                                                    <label>Nombre del Contacto</label>
                                                    <asp:TextBox ID="txbNombreContacto" CssClass="form-control input-sm" runat="server" placeholder="Nombre Contacto"
                                                        Style="text-transform: uppercase;" SpellCheck="false" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Cargo del Contacto</label>
                                                    <asp:TextBox ID="txbCargoContacto" CssClass="form-control input-sm" runat="server" placeholder="Cargo"
                                                        Style="text-transform: uppercase;" SpellCheck="false" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Teléfono Principal</label>
                                                    <asp:TextBox ID="txbTelefonoPpal" CssClass="form-control input-sm" runat="server" placeholder="Teléfono principal"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Correo Empresa</label>
                                                    <asp:TextBox ID="txbCorreo" CssClass="form-control input-sm" runat="server" placeholder="Correo" required></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Nombre del Pagador</label>
                                                    <asp:TextBox ID="txbNombrepagador" CssClass="form-control input-sm" runat="server" placeholder="Pagador"
                                                        Style="text-transform: uppercase;" SpellCheck="false" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Celular</label>
                                                    <asp:TextBox ID="txbCelularPagador" CssClass="form-control input-sm" runat="server" placeholder="Celular del Pagador"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Correo del pagador</label>
                                                    <asp:TextBox ID="txbCorreoPagador" CssClass="form-control input-sm" runat="server" placeholder="Correo Pagador"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label for="Retorno" class="col-form-label">Retorno administrativo?:</label>
                                                    <div class="col-sm-10">
                                                        <asp:RadioButtonList
                                                            ID="rblActivo"
                                                            runat="server"
                                                            RepeatDirection="Horizontal"
                                                            CssClass="i-checks">
                                                            <asp:ListItem Text="Sí" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                        </asp:RadioButtonList>

                                                    </div>
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
                                                        DataValueField="idCiudad" CssClass="chosen-select form-control input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label>Fecha inicio convenio</label>
                                                    <asp:TextBox ID="txbFechaConvenio" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label>Fecha fin convenio</label>
                                                    <asp:TextBox ID="txbFechaFinConvenio" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
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
                                                    <span class="fileinput-new input-sm">Seleccionar archivo *.Pdf</span>
                                                    <span class="fileinput-exists input-sm">Cambiar</span>
                                                    <input type="file" name="fileConvenio" id="fileConvenio" accept="application/pdf">
                                                </span>
                                                <a href="#" class="input-group-addon btn btn-danger fileinput-exists input-sm"
                                                    data-dismiss="fileinput">Quitar</a>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label>Camara de comercio:</label>
                                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                                <div class="form-control input-sm" data-trigger="fileinput">
                                                    <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                    <span class="fileinput-filename"></span>
                                                </div>
                                                <span class="input-group-addon btn btn-success btn-file input-sm">
                                                    <span class="fileinput-new input-sm">Seleccionar archivo *.Pdf</span>
                                                    <span class="fileinput-exists input-sm">Cambiar</span>
                                                    <input type="file" name="fileCamara" id="fileCamara" accept="application/pdf">
                                                </span>
                                                <a href="#" class="input-group-addon btn btn-danger fileinput-exists input-sm"
                                                    data-dismiss="fileinput">Quitar</a>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label>RUT:</label>
                                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                                <div class="form-control input-sm" data-trigger="fileinput">
                                                    <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                    <span class="fileinput-filename"></span>
                                                </div>
                                                <span class="input-group-addon btn btn-success btn-file input-sm">
                                                    <span class="fileinput-new input-sm">Seleccionar archivo *.Pdf</span>
                                                    <span class="fileinput-exists input-sm">Cambiar</span>
                                                    <input type="file" name="fileRut" id="fileRut" accept="application/pdf">
                                                </span>
                                                <a href="#" class="input-group-addon btn btn-danger fileinput-exists input-sm"
                                                    data-dismiss="fileinput">Quitar</a>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label>Cédula Representante Legal:</label>
                                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                                <div class="form-control input-sm" data-trigger="fileinput">
                                                    <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                    <span class="fileinput-filename"></span>
                                                </div>
                                                <span class="input-group-addon btn btn-success btn-file input-sm">
                                                    <span class="fileinput-new input-sm">Seleccionar archivo *.Pdf</span>
                                                    <span class="fileinput-exists input-sm">Cambiar</span>
                                                    <input type="file" name="fileCedulaRepLeg" id="fileCedulaRepLeg" accept="application/pdf">
                                                </span>
                                                <a href="#" class="input-group-addon btn btn-danger fileinput-exists input-sm"
                                                    data-dismiss="fileinput">Quitar</a>
                                            </div>
                                        </div>

                                        <div>
                                            <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button" onclick="window.location.href='empresasafiliadas'"><strong>Cancelar</strong></button>
                                            <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right" Text="Agregar" OnClick="btnAgregar_Click" />
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
                txbFechaFinConvenio: {
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
                fileCamara: {
                    required: true
                },
                fileRut: {
                    required: true
                },
                fileCedulaRepLeg: {
                    required: true
                },
            },
            messages: {
                fileConvenio: "*",
                fileCamara: "*",
                fileRut: "*",
                fileCedulaRepLeg: "*",
                ddlCiudadEmpresa: "*",
            }
        });

        $('.chosen-select').chosen({ width: "100%" });
    </script>

</body>

</html>