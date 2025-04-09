<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="olvidoclave.aspx.cs" Inherits="fpWebApp.olvidoclave" %>

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
            <h3 class="text-white">Olvido de Clave</h3>
            <p class="text-white">
                Ingresa los siguientes datos:
            </p>
            <form class="m-t" role="form" id="form1" runat="server">
                <div class="row">
                    <div class="col-lg-6">
                        <div class="form-group">
                            <asp:TextBox ID="txbEmail" CssClass="form-control" runat="server" placeholder="Usuario" required></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-6">
                        <div class="form-group">
                            <asp:DropDownList ID="ddlDominio" runat="server" CssClass="form-control">
                                <asp:ListItem Text="@fitnesspeoplecmd.com" Value="@fitnesspeoplecmd.com"></asp:ListItem>
                                <asp:ListItem Text="@fitnesspeoplecolombia.com" Value="@fitnesspeoplecolombia.com"></asp:ListItem>
                                <asp:ListItem Text="Usuario externo" Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                
                <asp:Button ID="btnRecuperar" runat="server" CssClass="btn btn-warning1 block full-width m-b font-bold" Text="RECUPERAR CLAVE" OnClick="btnRecuperar_Click" />

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
