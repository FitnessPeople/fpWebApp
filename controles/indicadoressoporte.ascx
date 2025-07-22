<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoressoporte.ascx.cs" Inherits="fpWebApp.controles.indicadoressoporte" %>
<%--
    ****************
    Indicadores:
        Tickets Totales
        Tickets Pendientes
        Tickets Resueltos
        Tickets En proceso
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class="widget style1 bg-success">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-screwdriver-wrench fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Tickets totales</span>
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
                    <i class="fa fa-paint-roller fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Tickets en proceso</span>
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
                    <i class="fa fa-trowel fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Tickets pendientes</span>
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
                    <i class="fa fa-wrench fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Tickets resueltos</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos4" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
</div>