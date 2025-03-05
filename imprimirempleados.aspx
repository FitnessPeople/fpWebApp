<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="imprimirempleados.aspx.cs" Inherits="fpWebApp.imprimirempleados" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Imprimir</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
</head>
<body onload="window.print()" class="gray-bg">
    <div class="lock-word animated fadeInDown">
        
    </div>
    <form id="form1" runat="server">
        <div class="table-responsive">
            <table class="footable table toggle-arrow-small" data-page-size="10" data-filter="#filter" data-filter-minimum="3">
                <thead>
                    <tr>
                        <th data-sort-ignore="true">Documento</th>
                        <th data-sort-initial="true">Nombre</th>
                        <th data-sort-ignore="true" data-hide="phone,tablet">Celular</th>
                        <th data-sort-ignore="true" data-hide="phone,tablet">Correo</th>
                        <th data-hide="phone,tablet">Cargo</th>
                        <th data-hide="phone,tablet">Cumpleaños</th>
                        <th data-hide="all">Nro Contrato</th>
                        <th data-hide="all">Tipo Contrato</th>
                        <th data-hide="all">Fecha Inicio</th>
                        <th data-hide="all">Fecha Final</th>
                        <th data-toggle="false">Estado</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rpEmpleados" runat="server">
                        <ItemTemplate>
                            <tr class="feed-element">
                                <td><%# Eval("DocumentoEmpleado") %></td>
                                <td><%# Eval("NombreEmpleado") %></td>
                                <td><%# Eval("TelefonoEmpleado") %></td>
                                <td><%# Eval("EmailEmpleado") %></td>
                                <td><%# Eval("CargoEmpleado") %></td>
                                <td><%# Eval("FechaNacEmpleado", "{0:dd MMM}") %></td>
                                <td><%# Eval("NroContrato") %></td>
                                <td><%# Eval("TipoContrato") %></td>
                                <td><%# Eval("FechaInicio", "{0:dd MMM yyyy}") %></td>
                                <td><%# Eval("FechaFinal", "{0:dd MMM yyyy}") %></td>
                                <td><%# Eval("Estado") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
