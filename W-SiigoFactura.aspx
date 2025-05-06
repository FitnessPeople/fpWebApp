<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="W-SiigoFactura.aspx.cs" Inherits="fpWebApp.W_SiigoFactura" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8">
    <title>Ejemplo - Creación de Factura - Siigo Nube API</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnCrearFactura" runat="server" Text="Crear Factura" OnClick="btnCrearFactura_Click" />
        </div>
    </form>
</body>
</html>
