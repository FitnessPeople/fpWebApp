<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresDirCom.ascx.cs" Inherits="fpWebApp.controles.indicadoresDirCom" %>
<%--
    ****************
    Indicadores: 
        Ventas del mes
        Nuevas afiliaciones
        Tasa de conversión (%)
        Meta del mes
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class="widget style1 bg-success">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-money-bill-trend-up fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Ventas del mes</span>
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
                    <i class="fa fa-person-circle-plus fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Nuevas afiliaciones</span>
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
                    <i class="fa fa-people-arrows fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Tasa de conversión (%)</span>
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
                    <i class="fa fa-sack-dollar fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Meta del mes</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos4" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
</div>