<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadores04.ascx.cs" Inherits="fpWebApp.controles.indicadores04" %>
<%--
    ****************
    Indicadores:
        Usuarios Activos
        Usuarios Inactivos
        Perfiles
        Paginas
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class="widget style1 bg-success">
            <div class="row">
                <div class="col-xs-4 text-center">
                    <i class="fa fa-circle-user fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Usuarios activos</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos1" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <div class="widget style1 lazur-bg">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-user-slash fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Usuarios inactivos</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos2" runat="server"></asp:Literal>
                    </h2>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <div class="widget style1 yellow-bg">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-id-badge fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Perfiles</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos3" runat="server"></asp:Literal>
                    </h2>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <div class="widget style1 navy-bg">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-file-code fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Páginas</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos4" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
</div>