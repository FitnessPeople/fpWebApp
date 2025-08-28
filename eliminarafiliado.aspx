<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="eliminarafiliado.aspx.cs" Inherits="fpWebApp.eliminarafiliado" %>

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

    <title>Fitness People | Eliminar afiliado</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.3/themes/smoothness/jquery-ui.css">

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            // Activa el menú principal
            var element1 = document.querySelector("#afiliados");
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
                    <h4 class="modal-title">Guía para eliminar al afiliado</h4>
                    <small class="font-bold">¡Bienvenido! Sigue estos pasos para borrar la información de manera segura y verificada.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Verifica los datos</b><br />
                        Antes de eliminar, confirma que es el afiliado <b>correcto</b> revisando:<br />
                        <i class="fa-solid fa-user-tie" style="color: #0D6EFD;"></i> <b>Foto (si aplica).</b><br />
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i> <b>Nombre(s) y Apellido(s).</b><br />
                        <i class="fa-solid fa-phone" style="color: #0D6EFD;"></i> <b>Correo, Teléfono y Ciudad.</b><br />
                        <i class="fa-solid fa-calendar-days" style="color: #0D6EFD;"></i> <b>Fecha de Nacimiento.</b><br />
                        <i class="fa-solid fa-ticket" style="color: #0D6EFD;"></i> <b>Planes.</b>
                    <br />
                        <br />
                        <b>Paso 2: Confirmación requerida</b><br />
                        <i class="fa-solid fa-robot" style="color: #4f4f4f;"></i> El sistema hará un <b>pregunta de verificación</b> para confirmar que no eres un robot.
                    <br />
                        <br />
                        <b>Paso 3: Confirma o cancela</b><br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i> <b>Eliminar Afiliado:</b> Borra al afiliado del <b>sistema</b>.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i> <b>Cancelar:</b> Si necesitas volver <b>atrás</b> sin guardar cambios.
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
                    <h2><i class="fa fa-id-card text-success m-r-sm"></i>Eliminar afiliado</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Afiliados</li>
                        <li class="active"><strong>Afiliados</strong></li>
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
                            <h5>Confirmación de eliminación de afiliado.</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">

                            <div class="row">
                                <form id="form" runat="server">
                                    <div class="col-sm-5 b-r">
                                        <div class="row" runat="server">
                                            <div class="col-md-12">
                                                <div class="contact-box bg-danger">
                                                    <div class="col-sm-4">
                                                        <div class="text-center">
                                                            <asp:Literal ID="ltFotoAfiliado" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <h3><strong><asp:Literal ID="ltNombreAfiliado" runat="server"></asp:Literal> <asp:Literal ID="ltApellidoAfiliado" runat="server"></asp:Literal></strong></h3>
                                                        <p><i class="fa fa-envelope"></i> <asp:Literal ID="ltEmailAfiliado" runat="server"></asp:Literal></p>
                                                        <address>
                                                            <i class="fa fa-mobile"></i>
                                                            <asp:Literal ID="ltCelularAfiliado" runat="server"></asp:Literal><br />
                                                            <i class="fa fa-building"></i>
                                                            <asp:Literal ID="ltSedeAfiliado" runat="server"></asp:Literal><br />
                                                            <i class="fa fa-cake"></i>
                                                            <asp:Literal ID="ltCumpleAfiliado" runat="server"></asp:Literal>
                                                        </address>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="row m-xs">
                                                    <h4>Planes</h4>
                                                    <asp:Literal ID="ltNoPlanes" runat="server"></asp:Literal>
                                                    <ul class="todo-list small-list">
                                                        <asp:Repeater ID="rpPlanesAfiliado" runat="server">
                                                            <ItemTemplate>
                                                                <li>
                                                                    <small class="label label-primary pull-right"><%# Eval("DiasQueFaltan") %> días disponibles</small>
                                                                    <label>
                                                                        <%# Eval("NombrePlan") %>, <%# Eval("Meses") %> mes(es)
                                                                    </label>
                                                                    <br />
                                                                    <div class="progress progress-striped active">
                                                                        <div style='width: <%# Eval("Porcentaje1") %>%' class="progress-bar progress-bar-success"></div>
                                                                        <div style='width: <%# Eval("Porcentaje2") %>%' class="progress-bar progress-bar-warning"></div>
                                                                    </div>
                                                                    <small class="text-muted"><%# Eval("FechaInicioPlan", "{0:dd MMM yyyy}") %> - <%# Eval("FechaFinalPlan", "{0:dd MMM yyyy}") %></small>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-7 border-left">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label>Pregunta de confirmación <asp:Literal ID="ltPregunta" runat="server"></asp:Literal>:</label>
                                                    <asp:TextBox ID="txbConfirmacion" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                        <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button"
                                            onclick="window.location.href='afiliados'">
                                            <strong>Cancelar</strong></button>
                                        <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click"
                                            CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right"
                                            Text="Eliminar afiliado" />
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

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <script>

        $("#form").validate({
            rules: {
                txbConfirmar: {
                    required: true,
                },
            }
        });
    </script>

</body>

</html>