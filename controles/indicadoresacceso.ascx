<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresacceso.ascx.cs" Inherits="fpWebApp.controles.indicadoresacceso" %>
<%--
    ****************
    Indicadores:
        Acceso hoy
        Acceso semana
        Acceso mes
        Total Activos en Sede
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class="widget style1 bg-success">
            <div class="row">
                <div class="col-xs-4 text-center">
                    <i class="fa fa-calendar-day fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Accesos hoy</span>
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
                    <i class="fa fa-calendar-week fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Accesos esta semana</span>
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
                    <i class="fa fa-calendar-days fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Accesos este mes</span>
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
                    <i class="fa fa-id-card fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Total activos en sede</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos4" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
</div>