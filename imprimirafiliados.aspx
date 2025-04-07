<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="imprimirafiliados.aspx.cs" Inherits="fpWebApp.imprimirafiliados" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Lista de Afiliados</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.bootstrap.css" rel="stylesheet" />

    <link href="css/plugins/datapicker/datepicker3.css" rel="stylesheet" />

    <link href="css/plugins/daterangepicker/daterangepicker-bs3.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <style type="text/css" media="print">
        .botones {
            visibility: hidden;
            display: none
        }
    </style>
</head>
<body class="gray-bg">
    <div class="wrapper wrapper-content animated fadeInRight">
        <div class="ibox float-e-margins">
            <div class="ibox-title">
                <h5>Lista de Afiliados</h5>
            </div>
            <div class="ibox-content">
                <form id="form1" runat="server">
                    <div class="row botones">
                        <div class="col-lg-4 form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Buscador:</label>
                                <div class="col-lg-8">
                                    <input type="text" placeholder="Buscar..." class="form-control input-sm m-b-xs" id="filter" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 form-horizontal">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlSedes" runat="server" CssClass="form-control input-sm" DataTextField="NombreSede" DataValueField="idSede" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlSedes_SelectedIndexChanged">
                                    <asp:ListItem Text="Elija una sede" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4 form-horizontal">
                            <div class="form-group" id="data_5">
                                <label class="col-lg-4 control-label">Fecha afiliación:</label>
                                <div class="col-lg-8">
                                    <div class="input-daterange input-group" id="datepicker">
                                        <asp:TextBox ID="txbInicio" runat="server" CssClass="input-sm form-control" OnTextChanged="txbInicio_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <span class="input-group-addon">a</span>
                                        <asp:TextBox ID="txbFinal" runat="server" CssClass="input-sm form-control" OnTextChanged="txbFinal_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2 form-horizontal">
                            <div class="form-group">
                                <asp:LinkButton ID="lbExportarExcel" runat="server" CausesValidation="false" 
                                    CssClass="btn btn-info pull-right dim m-l-md" style="font-size: 12px;" 
                                    OnClick="lbExportarExcel_Click" >
                                    <i class="fa fa-file-excel"></i> EXCEL
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <table class="footable table table-striped list-group-item-text" data-paging-size="10" 
                            data-paging="true" data-paging-count-format="{CP} de {TP}" data-paging-limit="10" 
                            data-empty="Sin resultados" data-toggle-column="first">
                            <thead>
                                <tr>
                                    <%--<th data-sort-ignore="true">ID</th>--%>
                                    <th data-sort-ignore="true">Documento</th>
                                    <th data-sort-ignore="true">Nombre</th>
                                    <th data-sort-ignore="true">Telefono</th>
                                    <th data-sort-ignore="true">Correo</th>
                                    <th data-sort-ignore="true">Direccion</th>
                                    <th data-sort-ignore="true">Fecha nacimiento</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rpAfiliados" runat="server">
                                    <ItemTemplate>
                                        <tr class="feed-element">
                                            <%--<td><%# Eval("idAfiliado") %></td>--%>
                                            <td><%# Eval("DocumentoAfiliado") %></td>
                                            <td><%# Eval("NombreAfiliado") %> <%# Eval("ApellidoAfiliado") %></td>
                                            <td><%# Eval("CelularAfiliado") %></td>
                                            <td><%# Eval("EmailAfiliado") %></td>
                                            <td><%# Eval("DireccionAfiliado") %></td>
                                            <td><%# Eval("FechaNacAfiliado", "{0:dd MMM yyyy}") %> (<%# Eval("edad") %> años) <i class="fa fa-<%# Eval("age") %>"></i></td>
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
    <script src="js/bootstrap.min.js"></script>

    <!-- FooTable -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

   <!-- Data picker -->
   <script src="js/plugins/datapicker/bootstrap-datepicker.js"></script>

    <!-- Date range picker -->
    <script src="js/plugins/daterangepicker/daterangepicker.js"></script>


    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();

        $(document).ready(function () {
        $('#data_5 .input-daterange').datepicker({
            keyboardNavigation: false,
            forceParse: false,
            autoclose: true
        });
        });
    </script>
</body>
</html>
