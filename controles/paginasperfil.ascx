<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="paginasperfil.ascx.cs" Inherits="fpWebApp.controles.paginasperfil" %>
<div class="ibox float-e-margins" runat="server" id="listaafiliados">
    <div class="ibox-title">
        <h5>Lista de páginas a las que tienes acceso</h5>
        <div class="ibox-tools">
            <a class="collapse-link">
                <i class="fa fa-chevron-up"></i>
            </a>
        </div>
    </div>
    <div class="ibox-content">

        <div class="row" runat="server" id="divBotonesLista">
            <div class="col-lg-6 form-horizontal">
                <%--<form runat="server" id="form1">--%>
                    <div class="input-group">
                        <input type="text" placeholder="Buscar" class="input form-control input-sm" name="txbBuscar" id="txbBuscar" runat="server">
                        <span class="input-group-btn">
                            <button type="button" id="btnBuscar" name="btnBuscar" onserverclick="btnBuscar_Click" class="btn btn btn-primary btn-sm" runat="server"> <i class="fa fa-search"></i> Buscar</button>
                        </span>
                    </div>
                <%--</form>--%>
            </div>
            <div class="col-lg-6 form-horizontal" style="text-align: center;">
                <label class="control-label">Mostrar </label>

                <a href="#" class="data-page-size" data-page-size="10">10</a> | 
                <a href="#" class="data-page-size" data-page-size="20">20</a> | 
                <a href="#" class="data-page-size" data-page-size="50">50</a> | 
                <a href="#" class="data-page-size" data-page-size="100">100</a>

                <label class="control-label">registros</label>
            </div>
        </div>

        <div class="table-responsive" runat="server" id="divTabla">
            <table class="footable table toggle-arrow-small" data-page-size="10">
                <thead>
                    <tr>
                        <th data-sort-ignore="true">Nombre</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rpPaginas" runat="server">
                        <ItemTemplate>
                            <tr class="feed-element">
                                <td><a href="<%# Eval("NombreAspx") %>"><%# Eval("Categoria") %> / <%# Eval("Pagina") %></a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="8">
                            <ul class="pagination pull-right hide-if-no-paging"></ul>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>

    </div>
</div>