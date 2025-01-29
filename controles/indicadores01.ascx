<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadores01.ascx.cs" Inherits="fpWebApp.controles.indicadores01" %>
<%--
    ****************
    Indicadores: 
        Empleados Activos
        Empleados Fijos
        Empleados OPS
        Empleados Aprendices
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class="widget style1 bg-success">
            <div class="row">
                <div class="col-xs-4 text-center">
                    <i class="fa fa-user-tie fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Empleados activos</span>
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
                    <i class="fa fa-user-check fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Fijos</span>
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
                    <span>OPS</span>
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
                    <span>Aprendiz </span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos4" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
</div>
