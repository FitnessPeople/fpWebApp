<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="procesarfechas.aspx.cs" Inherits="fpWebApp.procesarfechas" %>

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
            <asp:Button ID="btnProcesar" runat="server" Text="Procesar" OnClick="btnProcesar_Click" />

        </div>
    </form>
</body>
</html>
