<%@ Page Title="Fitness Peple | Login" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="fpWebApp._Default" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Login</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
</head>

<body class="darkgray-bg">

    <div class="middle-box text-center loginscreen animated fadeInDown">
        <div>
            <div>
                <h1 class="logo-name">FP+</h1>

            </div>
            <h3 class="text-white"><i class="fa fa-dumbbell m-r-sm"></i>Bienvenido a FP+</h3>
            <p class="text-white">
                ¡Nuestra app está lista, reconstruida desde cero!<br />
                Se avecinan muchas funciones y mejoras.
            </p>
            <form class="m-t" role="form" id="form1" runat="server">
                <div class="row" runat="server" id="divUsuario">
                    <div class="col-lg-12">
                        <div class="form-group">
                            <%--<asp:TextBox ID="txbEmail" CssClass="form-control" runat="server" placeholder="Usuario" required></asp:TextBox>--%>
                            <asp:TextBox ID="txbEmail" CssClass="form-control" runat="server" placeholder="Identificacion" required></asp:TextBox>
                        </div>
                    </div>
                    <%--<div class="col-lg-7">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlDominio" runat="server" CssClass="form-control">
                                <asp:ListItem Text="@fitnesspeoplecmd.com" Value="@fitnesspeoplecmd.com"></asp:ListItem>
                                <asp:ListItem Text="@fitnesspeoplecolombia.com" Value="@fitnesspeoplecolombia.com"></asp:ListItem>
                                <asp:ListItem Text="Usuario externo" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>--%>
                </div>

                <%--<div class="form-group">
                    <asp:TextBox ID="txbPassword" CssClass="form-control" runat="server" placeholder="Password" required TextMode="Password"></asp:TextBox>
                </div>--%>
                <div class="form-group" runat="server" id="divPassword">
                    <div class="input-group">
                        <asp:TextBox ID="txbPassword" CssClass="form-control" runat="server" placeholder="Password" required TextMode="Password"></asp:TextBox>
                        <span class="input-group-btn">
                            <button type="button" class="btn btn-default"><i class="fa fa-eye" id="togglePassword"></i></button>
                        </span>
                    </div>
                </div>
                <asp:Button ID="btnIngresar" runat="server" CssClass="btn btn-warning1 block full-width m-b font-bold" Text="INGRESAR" OnClick="btnIngresar_Click" />

                <div class="alert alert-danger alert-dismissable" runat="server" id="divMensaje" visible="false">
                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                    <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                </div>

                <div class="alert alert-danger alert-dismissable" runat="server" id="divCodigo" visible="false">
                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                    <asp:Literal ID="ltCodigo" runat="server"></asp:Literal>
                    <asp:TextBox ID="txbCodigo" CssClass="form-control" runat="server" required></asp:TextBox>
                    <asp:Button ID="btnIngresarCodigo" runat="server" CssClass="btn btn-warning1 block full-width m-b font-bold"
                        Text="INGRESAR CON CODIGO" OnClick="btnIngresarCodigo_Click" />
                </div>

            <p><a href="olvidoclave"><small style="color: #fff; text-decoration: underline;">Olvidaste la contraseña?</small></a></p>
            <%--<p class="text-muted text-center text-white"><small>No tienes una cuenta?</small></p>--%>
            <a class="btn btn-warning1 block full-width m-b font-bold" href="soporte">No tengo cuenta</a>

            </form>
            <p class="m-t text-white"><small>Fitness People &copy;
                <asp:Label ID="lblAnho" runat="server" /></small> </p>
        </div>
    </div>

    <!-- Mainly scripts -->
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <script>
        const icon = document.getElementById('togglePassword');
        let password = document.getElementById('txbPassword');

        /* Event fired when <i> is clicked */
        icon.addEventListener('click', function () {
            if (password.type === "password") {
                password.type = "text";
                icon.classList.add("fa-eye-slash");
                icon.classList.remove("fa-eye");
            }
            else {
                password.type = "password";
                icon.classList.add("fa-eye");
                icon.classList.remove("fa-eye-slash");
            }
        });

        $(document).ready(function () {

            $("#form").validate({
                rules: {
                    txbPassword: {
                        required: true,
                        minlength: 3
                    },
                }
            });
        });
    </script>

</body>

</html>
