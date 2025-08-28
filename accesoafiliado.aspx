<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="accesoafiliado.aspx.cs" Inherits="fpWebApp.accesoafiliado" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>
<%@ Register Src="~/controles/indicadoresacceso.ascx" TagPrefix="uc1" TagName="indicadoresacceso" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Acceso afiliado</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.bootstrap.css" rel="stylesheet" />

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
            // Activa el menú principal
            var element1 = document.querySelector("#accesoafiliado");
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
                    <i class="fa fa-id-card modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para crear un afiliado</h4>
                    <small class="font-bold">¡Bienvenido! Sigue estos pasos para completar el registro sin errores.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Prepara la información</b><br />
                        Asegúrate de tener estos datos del afiliado a mano:<br />
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i><b>Nombre(s), Apellido(s), Tipo y Número de Documento.</b><br />
                        <i class="fa-solid fa-calendar-days" style="color: #0D6EFD;"></i><b>Fecha de Nacimiento, Género, Estado Civil.</b><br />
                        <i class="fa-solid fa-phone" style="color: #0D6EFD;"></i><b>Teléfono, Correo, Dirección, Ciudad.</b><br />
                        <i class="fa-solid fa-user-tie" style="color: #0D6EFD;"></i><b>Profesión, EPS, Empresa Convenio, Sede, Foto (si aplica).</b><br />
                        <i class="fa-solid fa-user-group" style="color: #0D6EFD;"></i><b>Nombre, Parentesco y Teléfono de Contacto.</b>
                        <br />
                        <br />
                        <b>Paso 2: Completa el formulario</b><br />
                        <i class="fa-solid fa-pencil"></i>Llena todos los <b>campos obligatorios</b> (generalmente marcados con *).<br />
                        <i class="fa-solid fa-magnifying-glass"></i>Verifica que los datos estén <b>correctos</b>.
                    <br />
                        <br />
                        <b>Paso 3: Confirma o cancela</b><br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i><b>Agregar:</b> Guarda la información y <b>finaliza</b> el registro.<br />
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
                    <h2><i class="fa fa-lock-open text-success m-r-sm"></i>Acceso afiliado</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Afiliados</li>
                        <li class="active"><strong>Acceso afiliado</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">
                    <%--Inicio Contenido!!!!--%>
                    <uc1:indicadoresacceso runat="server" ID="indicadoresacceso" />

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
                            <h5>Autorización de acceso a afiliado, <b>
                                <asp:Literal ID="ltSede" runat="server"></asp:Literal></b> </h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <form role="form" id="form" runat="server">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel ID="upAcceso" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                            <div class="col-sm-4 b-r">

                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="form-group">
                                                            <label>Nro. de Documento:</label>
                                                            <asp:TextBox ID="txbDocumento" CssClass="form-control input-sm" runat="server"
                                                                placeholder="Documento" TabIndex="1"
                                                                OnTextChanged="txbDocumento_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-8">
                                                <div class="row">
                                                    <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                                        data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}" data-paging-limit="10"
                                                        data-empty="Sin resultados" data-toggle-column="first">
                                                        <thead>
                                                            <tr>
                                                                <th data-sortable="false" data-breakpoints="xs">Afiliado</th>
                                                                <th data-sortable="false" data-breakpoints="xs">Sede</th>
                                                                <th class="text-nowrap" data-breakpoints="xs">Fecha Hora</th>
                                                                <th class="text-nowrap" data-breakpoints="xs">Días para terminar el plan</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rpAccesoAfiliados" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Eval("NombreAfiliado") %> <%# Eval("ApellidoAfiliado") %></td>
                                                                        <td><%# Eval("NombreSede") %></td>
                                                                        <td><%# Eval("FechaHoraIngreso","{0:dd MMM yyyy HH:mm:ss}") %></td>
                                                                        <td><%# Eval("diasquefaltan") %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                    
                                                </div>

                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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

    <!-- FooTable -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <script>

        $('.footable').footable();

        $("#txbDocumento").focus();

        $.validator.setDefaults({ ignore: ":hidden:not(.chosen-select)" })

        $("#form").validate({
            rules: {
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
                ddlProfesiones: "*",
            }
        });

        $('.chosen-select').chosen({ width: "100%", disable_search_threshold: 10, no_results_text: "Sin resultados" });
    </script>

</body>

</html>
