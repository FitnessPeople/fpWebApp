<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pantallabloqueo.aspx.cs" Inherits="fpWebApp.pantallabloqueo" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Pantalla de Bloqueo</title>

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
            <form class="m-t" role="form" id="form1" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="form-group">
                            <div class="m-b-md">
                                <asp:Literal ID="ltFoto" runat="server"></asp:Literal>
                            </div>
                            <h3 style="color: #fff;"><asp:Literal ID="ltUsuario" runat="server"></asp:Literal></h3>
                            <p style="color: #fff;">Estás en la pantalla de bloqueo. La aplicación principal se cerró y necesitas ingresar tu contraseña para volver a la aplicación.</p>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group">
					    <asp:TextBox ID="txbPassword" CssClass="form-control" runat="server" placeholder="Password" required TextMode="Password"></asp:TextBox>
					    <span class="input-group-btn">
						    <button type="button" class="btn btn-default"><i class="fa fa-eye" id="togglePassword"></i></button>
					    </span>
                    </div>
				</div>
                <asp:Button ID="btnDesbloquear" runat="server" CssClass="btn btn-warning1 block full-width m-b font-bold" Text="DESBLOQUEAR" OnClick="btnDesbloquear_Click" />

                <div class="alert alert-danger alert-dismissable" runat="server" id="divMensaje" visible="false">
                    <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                    <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                </div>

            </form>
            <p class="m-t text-white"><small>Fitness People &copy; <asp:Label ID="lblAnho" runat="server" /></small> </p>
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
