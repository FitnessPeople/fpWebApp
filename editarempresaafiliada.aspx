<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editarempresaafiliada.aspx.cs" Inherits="fpWebApp.editarempresaafiliada" %>

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

    <link href="css/plugins/dropzone/basic.css" rel="stylesheet" />
    <link href="css/plugins/dropzone/dropzone.css" rel="stylesheet" />
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet" />

    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />
    <link href="css/plugins/bootstrapSocial/bootstrap-social.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            // Activa el menú principal
            var element1 = document.querySelector("#empresasafiliadas");
            if (element1) {
                element1.classList.add("active");
            }

            // Despliega el submenú
            var element2 = document.querySelector("#afiliados2");
            if (element2) {
                element2.classList.add("show"); // en Bootstrap el desplegado es con "show"
                element2.classList.remove("collapse");
            }
        }
    </script>
</head>

<body onload="changeClass()">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <i class="fa fa-building modal-icon"></i>
                    <h4 class="modal-title">Guía para modificar información</h4>
                    <small class="font-bold">¡Bienvenido! Sigue estos pasos para actualizar la información de manera rápida y segura.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>1️⃣ Paso 1: Modifica los datos necesarios</b><br />
                        Actualiza solo los campos que requieran cambios:<br />
                        📄 <b>Datos básicos:<br />
                        ✔ Nombre Comercial.<br />
                        ✔ Documento (NIT, RUC, etc.) + DV (dígito de verificación).<br />
                        ✔ Razón Social.<br />
                        ✔ Tipo de Documento.<br />

                        📞 Contacto:<br />
                        ✔ Teléfono Principal y Secundario.<br />
                        ✔ Celular.<br />
                        ✔ Email.<br />
                        ✔ Dirección y Ciudad.<br />

                        📅 Datos del convenio:<br />
                        ✔ Fecha de Convenio.<br />
                        ✔ Número de Empleados.<br />
                        ✔ Tipo de Negociación.<br />
                        ✔ Días de Crédito.<br />
                        ✔ 📂 PDF del Contrato (listo para cargar).</b>
                    <br />
                        <br />
                        <b>2️⃣ Paso 2: Completa el formulario</b><br />
                        🖊️ Llena todos los campos obligatorios (generalmente marcados con *).<br />
                        🔍 Verifica que los datos estén correctos y actualizados.
                    <br />
                        <br />
                        <b>3️⃣ Paso 3: Confirma o cancela</b><br />
                        ✅ <b>Actualizar:</b> Guarda los cambios realizados.<br />
                        ↩️ <b>Cancelar:</b> Si necesitas volver atrás sin guardar modificaciones.
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

    <div class="modal inmodal" id="myModal2" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <%--<i class="fa fa-file-pdf modal-icon"></i>--%>
                    <h4 class="modal-title"><span id="titulo"></span></h4>
                </div>
                <div class="modal-body">
                
                    <div class="text-center m-t-md">
                        <object data="" type="application/pdf" width="450px" height="600px" id="objFile">
                            <embed src="" id="objEmbed">
                                <p>Este navegador no soporta archivos PDFs. Descarge el archivo para verlo: <a href="" id="objHref">Descargar PDF</a>.</p>
                            </embed>
                        </object> 
                    </div>
                    
                
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
                    <h2><i class="fa fa-building text-success m-r-sm"></i>Editar empresa</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Afiliados</li>
                        <li class="active"><strong>Editar empresa</strong></li>
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
                            <h5>Formulario para la edición de una empresa con convenio</h5>
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
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label>Nro. de Documento</label>
                                                    <asp:TextBox ID="txbDocumento" CssClass="form-control input-sm" runat="server" placeholder="Documento"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
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
                                                <a href="#" class="input-group-addon btn btn-danger fileinput-exists input-sm" data-dismiss="fileinput">Quitar</a>
                                            </div>
                                            <asp:Literal ID="ltContrato" runat="server"></asp:Literal>
                                        </div>

                                        <div class="form-group">
                                            <label>Estado</label>
                                            <asp:RadioButtonList ID="rblEstado" runat="server" RepeatDirection="Horizontal" CssClass="form-control input-sm">
                                                <asp:ListItem Text="&nbsp;Activo&nbsp;&nbsp;&nbsp;&nbsp;" Value="Activo"></asp:ListItem>
                                                <asp:ListItem Text="&nbsp;Inactivo" Value="Inactivo"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div>
                                            <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button" onclick="window.location.href='empresasafiliadas'"><strong>Cancelar</strong></button>
                                            <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right" Text="Actualizar" OnClick="btnActualizar_Click" />
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
            },
            messages: {
                ddlCiudadEmpresa: "*",
            }
        });

        $('.chosen-select').chosen({ width: "100%" });
    </script>

    <script id="script">
        //
        // If absolute URL from the remote server is provided, configure the CORS
        // header on that server.
        //

        $(document).on("click", ".dropdown-toggle", function () {
            var url = './docs/contratos/';
            url = url + $(this).data('file');

            document.getElementById('titulo').innerHTML = $(this).data('file');
            document.getElementById('objFile').data = url;
            document.getElementById('objEmbed').src = url;
            document.getElementById('objHref').src = url;
        });

    </script>

</body>

</html>