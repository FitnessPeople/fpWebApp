<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadores05.ascx.cs" Inherits="fpWebApp.controles.indicadores05" %>
<%--
    ****************
    Indicadores:
        Tablas
        Procedimientos Almacenados
        Vistas
        Eventos
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class="widget style1 bg-success">
            <div class="row">
                <div class="col-xs-4 text-center">
                    <i class="fa fa-table fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Tablas</span>
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
                    <i class="fa fa-database fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Procedimientos almacenados</span>
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
                    <i class="fa fa-server fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Vistas</span>
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
                    <span>Eventos</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos4" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
</div>