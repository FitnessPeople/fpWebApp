<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresCEO.ascx.cs" Inherits="fpWebApp.controles.indicadoresCEO" %>
<%--
    ****************
    Indicadores: 
        Ingresos totales del mes
        Utilidad neta
        Crecimiento vs mes anterior (%)
        Tasa de retención
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class="widget style1 bg-success">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-hand-holding-dollar fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Ingresos totales del mes</span>
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
                    <i class="fa fa-money-bill-trend-up fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Utilidad neta</span>
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
                    <i class="fa fa-arrow-trend-up fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Crecimiento vs mes anterior (%)</span>
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
                    <i class="fa fa-person-walking-arrow-right fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Tasa de retención </span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos4" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
</div>
