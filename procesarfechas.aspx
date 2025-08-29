<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="procesarfechas.aspx.cs" Inherits="fpWebApp.procesarfechas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <div class="col-md-4">
                <div class="form-group form-horizontal">
                    <label class="col-lg-3 control-label">Sede</label>

                    <asp:DropDownList ID="ddlSede" runat="server" AutoPostBack="true"
                        CssClass="form-control input-sm" AppendDataBoundItems="true">
                        <asp:ListItem Text="Todas" Value="" />
                    </asp:DropDownList>

                </div>
            </div>
            <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
            <asp:Button ID="btnProcesar" runat="server" Text="Procesar y Actualizar" CssClass="btn btn-success" OnClick="btnProcesar_Click" />
            <asp:Literal ID="ltCantidadRegistros" runat="server"></asp:Literal>

            <hr />

            <!-- Aquí va el grid -->
            <asp:GridView ID="gvAfiliados" runat="server" CssClass="table table-striped table-bordered"
                AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="IdAfiliado" HeaderText="ID" />
                    <asp:BoundField DataField="DocumentoAfiliado" HeaderText="Documento" />
                    <asp:BoundField DataField="NombreAfiliado" HeaderText="Nombre" />
                    <asp:BoundField DataField="ApellidoAfiliado" HeaderText="Apellido" />
                    <asp:BoundField DataField="idGenero" HeaderText="Genero" />
                    <asp:BoundField DataField="FechaNacAfiliado" HeaderText="Fec Nacimiento" />
                    <asp:BoundField DataField="idSede" HeaderText="Sede" />
                </Columns>
            </asp:GridView>

        </div>
    </form>
</body>
</html>
