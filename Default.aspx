<%@ Page Title="Fitness Peple | Login" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="fpWebApp._Default" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Login</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
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

                <div class="form-group">
                    <asp:TextBox ID="txbEmail" CssClass="form-control" runat="server" placeholder="Email" required></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txbPassword" CssClass="form-control" runat="server" placeholder="Password" required TextMode="Password"></asp:TextBox>
                </div>
                <asp:Button ID="btnIngresar" runat="server" CssClass="btn btn-warning1 block full-width m-b font-bold" Text="INGRESAR" OnClick="btnIngresar_Click" />

                <div class="alert alert-danger alert-dismissable" runat="server" id="divMensaje" visible="false">
                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                    Email o contraseña errada.<br />
                    <a class="alert-link" href="#">Intente nuevamente</a>.
                </div>

                <a href="#"><small>Olvidaste la contraseña?</small></a>
                <p class="text-muted text-center text-white"><small>No tienes una cuenta?</small></p>
                <a class="btn btn-sm btn-white btn-block" href="registro.aspx">Crear una cuenta</a>

            </form>
            <p class="m-t text-white"><small>Fitness People &copy; 2024</small> </p>
        </div>
    </div>

    <!-- Mainly scripts -->
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <script>
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
