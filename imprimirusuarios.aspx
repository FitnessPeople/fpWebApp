<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="imprimirusuarios.aspx.cs" Inherits="fpWebApp.imprimirusuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Lista de Usuarios</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <style type="text/css" media="print">
        .botones { visibility: hidden; display: none }
    </style>
</head>
<body class="gray-bg">
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="ibox float-e-margins">
            <div class="ibox-title">
                <h5>Lista de Usuarios</h5>
            </div>
            <div class="ibox-content">
                <form id="form1" runat="server">
                    <div class="row botones">
                        <div class="col-lg-8 form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label" style="text-align: left;">Buscador:</label>
                                <div class="col-lg-8">
                                    <input type="text" placeholder="Buscar..." class="form-control input-sm m-b-xs" id="filter">
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 form-horizontal">
                            <div class="form-group">
                                <button onclick="window.print()" class="btn btn-info btn-sm pull-right"><i class="fa fa-print m-r-sm"></i>Imprimir</button>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <table class="footable table toggle-arrow-small" data-page-size="100" data-filter="#filter" data-filter-minimum="3">
                            <thead>
                                <tr>
                                    <th data-sort-initial="true">Nombre</th>
                                    <th>Correo</th>
                                    <th data-sort-ignore="true">Clave</th>
                                    <th data-sort-ignore="true">Cargo</th>
                                    <th data-sort-ignore="true">Empleado</th>
                                    <th>Perfil</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rpUsuarios" runat="server">
                                    <ItemTemplate>
                                        <tr class="feed-element">
                                            <td><%# Eval("NombreUsuario") %></td>
                                            <td><%# Eval("EmailUsuario") %></td>
                                            <td><%# Eval("ClaveUsuario") %></td>
                                            <td><%# Eval("CargoUsuario") %></td>
                                            <td><%# Eval("Empleado") %></td>
                                            <td><%# Eval("Perfil") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <!-- Mainly scripts -->
    <script src="js/jquery-3.1.1.min.js"></script>

    <!-- FooTable -->
    <script src="js/plugins/footable/footable.all.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();
    </script>
</body>
</html>
