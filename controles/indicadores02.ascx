<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadores02.ascx.cs" Inherits="fpWebApp.controles.indicadores02" %>
<%--
    ****************
    Indicadores:
        Afiliados Activos
        Afiliados Inactivos
        Nro de Sedes
        Nuevos afiliados ultimo mes
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class="widget style1 bg-success1">
            <div class="row">
                <div class="col-xs-4 text-center">
                    <i class="fa fa-id-card fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Afiliados activos</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos1" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <div class="widget style1 bg-info1">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-user-slash fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Afiliados inactivos</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos2" runat="server"></asp:Literal>
                    </h2>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <div class="widget style1 bg-warning1">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-school-flag fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Sedes</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos3" runat="server"></asp:Literal>
                    </h2>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <div class="widget style1 bg-primary1">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-user-plus fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Nuevos último mes</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos4" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
</div>