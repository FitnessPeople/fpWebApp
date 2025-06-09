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

        <div class="row" style="font-size: 12px;" runat="server" id="divBotonesLista">
            <div class="col-lg-6 form-horizontal">
                <div class="form-group">
                    <div class="form-group" id="filter-form-container" style="margin-left: 28px;"></div>
                </div>
            </div>
 
            <div class="col-lg-6 form-horizontal">
                <%--<asp:LinkButton ID="lbExportarExcel" runat="server" CausesValidation="false" 
                    CssClass="btn btn-info pull-right dim m-l-md" style="font-size: 12px;" 
                    OnClick="lbExportarExcel_Click">
                    <i class="fa fa-file-excel"></i> EXCEL
                </asp:LinkButton>--%>
            </div>
        </div>

        <div class="table-responsive" runat="server" id="divTabla">
            <%--<table class="footable table toggle-arrow-small" data-page-size="10">--%>
            <table class="footable table table-striped" data-paging-size="10" 
                data-filter-min="3" data-filter-placeholder="Buscar" 
                data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}" 
                data-paging-limit="10" data-filtering="true" 
                data-filter-container="#filter-form-container" data-filter-delay="300" 
                data-filter-dropdown-title="Buscar en:" data-filter-position="left" 
                data-empty="Sin resultados">
                <thead>
                    <tr>
                        <th data-breakpoints="xs">Nombre</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rpPaginas" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><a href="<%# Eval("NombreAspx") %>"><%# Eval("Nombre") %> / <%# Eval("Pagina") %></a></td>
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