<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresreportespagos.ascx.cs" Inherits="fpWebApp.controles.indicadoresreportespagos" %>
<%--
    ****************
    Indicadores: 
        Total ventas
        Total ventas por web
        Total ventas por counter
        Total ventas por plan
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title">
                <span class="label label-success pull-right">Mes actual</span>
                <h5>Ventas totales</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins"><asp:Literal ID="ltCuantos1" runat="server"></asp:Literal></h1>
                <div class="stat-percent font-bold text-success">15 registros <%--<i class="fa fa-bolt"></i>--%></div>
                <small>&nbsp;</small>
            </div>
        </div>
        <%--<div class="widget style1 bg-success">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-user-tie fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Ventas totales</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos1" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>--%>
    </div>
    <div class="col-lg-3">
        <div class="widget style1 lazur-bg">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-user-check fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Ventas por Web</span>
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
                    <i class="fa fa-user-tag fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Ventas por counter</span>
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
                    <i class="fa fa-user-ninja fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Ventas Nuevos Plan Easy</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos4" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
</div>
