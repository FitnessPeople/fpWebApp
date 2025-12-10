<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="confirmarcodigo.aspx.cs" Inherits="fpWebApp.confirmarcodigo" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Confirmar código</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <!-- Sweet Alert -->
    <link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">

    <!-- Sweet alert -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</head>

<body class="darkgray-bg">

    <div class="middle-box text-center loginscreen animated fadeInDown">
        <div>
            <div>
                <h1 class="logo-name">FP+</h1>

            </div>
            <h3 class="text-white"><i class="fa fa-dumbbell m-r-sm"></i>Bienvenido a FP+</h3>
            <p class="text-white">
                ¡Nuestra app está lista, construida desde cero!<br />
                Se avecinan muchas funciones y mejoras.
                <br />(Servidor: Localhost)
            </p>
            <form class="m-t" role="form" id="form1" runat="server">
                <div class="alert alert-danger alert-dismissable" runat="server">
                    <%--<button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>--%>
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
        $(document).ready(function () {

            $("#form").validate({
                rules: {
                    txbCodigo: {
                        required: true,
                        minlength: 6
                    },
                }
            });
        });
    </script>

</body>

</html>