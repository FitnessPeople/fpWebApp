<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nuevousuario.aspx.cs" Inherits="fpWebApp.nuevousuario" %>

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

    <title>Fitness People | Nuevo usuario</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#nuevousuario");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#sistema");
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
                    <i class="fa fa-user-plus modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para crear un usuario</h4>
                    <small class="font-bold">¡Bienvenido! Sigue estos pasos para completar el registro sin errores.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Prepara la información</b><br />
                        Asegúrate de tener estos datos del usuario a mano:<br />
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i> <b>Nombre(s) y Apellido(s).</b><br />
                        <i class="fa-solid fa-phone" style="color: #0D6EFD;"></i> <b>Email.</b><br />
                        <i class="fa-solid fa-user-tie" style="color: #0D6EFD;"></i> <b>Cargo, Pefil y Empleado.</b><br />
                        <i class="fa-solid fa-lock" style="color: #0D6EFD;"></i> <b>Contraseña.</b>
                    <br />
                        <br />
                        <b>Paso 2: Completa el formulario</b><br />
                        <i class="fa-solid fa-pencil"></i> Llena todos los campos obligatorios (generalmente marcados con *).<br />
                        <i class="fa-solid fa-magnifying-glass"></i> Verifica que los datos estén correctos y actualizados.
                    <br />
                        <br />
                        <b>Paso 3: Confirma o cancela</b><br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i> <b>Agregar:</b> Guarda la información y finaliza el registro.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i> <b>Cancelar:</b> Si necesitas volver atrás sin guardar cambios.
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
                    <h2><i class="fas fa-user-plus text-success m-r-sm"></i>Nuevo usuario</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Sistema</li>
                        <li class="active"><strong>Nuevo usuario</strong></li>
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
                            <h5>Formulario para la creación de un nuevo usuario</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">

                            <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>

                            <div class="row">
                                <form role="form" id="form" runat="server">
                                    <div class="col-sm-6 b-r">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Nombre Completo</label>
                                                    <asp:TextBox ID="txbNombre" CssClass="form-control input-sm" runat="server" placeholder="Nombre(s)"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Cargo</label>
                                                    <asp:TextBox ID="txbCargo" CssClass="form-control input-sm" runat="server" placeholder="Cargo"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Email</label>
                                                    <asp:TextBox ID="txbEmail" CssClass="form-control input-sm" runat="server" placeholder="Email" required></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Clave</label>
                                                    <asp:TextBox ID="txbClave" CssClass="form-control input-sm" runat="server" placeholder="Clave"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Perfil</label>
                                                    <asp:DropDownList ID="ddlPerfiles" runat="server" AppendDataBoundItems="true" DataTextField="Perfil" DataValueField="idPerfil" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Empleado</label>
                                                    <asp:DropDownList ID="ddlEmpleados" runat="server" AppendDataBoundItems="true" DataTextField="NombreEmpleado" DataValueField="DocumentoEmpleado" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Ninguno" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div>
                                            <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button" onclick="window.location.href='usuarios'"><strong>Cancelar</strong></button>
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

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <script>
        $("#form").validate({
            rules: {
                txbNombre: {
                    required: true,
                    minlength: 3
                },
                txbCargo: {
                    required: true,
                    minlength: 5
                },
                ddlPerfiles: {
                    required: true
                },
                txbClave: {
                    required: true,
                    minlength: 8
                },
            }
        });
    </script>

</body>

</html>
