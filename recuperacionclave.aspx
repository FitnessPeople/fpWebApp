<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recuperacionclave.aspx.cs" Inherits="fpWebApp.recuperacionclave" %>

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
            <h3 class="text-white">Restaurar Clave</h3>
            <form class="m-t" role="form" id="form1" runat="server">
                <p class="text-white">
                    Restaurar la clave para el usuario: <asp:Literal ID="ltUsuario" runat="server"></asp:Literal><br /><br />
                    Escriba la nueva clave y haga clic en "RESTAURAR CLAVE".<br />
                </p>
            
                <div class="row">
                    <div class="col-lg-12">
                        <div class="form-group">
                            <asp:TextBox ID="txbNuevaClave" CssClass="form-control" runat="server" placeholder="Nueva clave" required></asp:TextBox>
                        </div>
                    </div>
                </div>
                
                <asp:Button ID="btnRestaurar" runat="server" CssClass="btn btn-warning1 block full-width m-b font-bold" Text="RESTAURAR CLAVE" OnClick="btnRestaurar_Click" />

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
