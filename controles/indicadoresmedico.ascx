<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresmedico.ascx.cs" Inherits="fpWebApp.controles.indicadoresmedico" %>
<%--
    ****************
    Indicadores: 
        Especialistas Activos
        Especialistas Inactivos
        Especialidades
        Agenda hoy
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class="widget style1 bg-success">
            <div class="row">
                <div class="col-xs-4 text-center">
                    <i class="fa fa-user-doctor fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Especialistas activos</span>
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
                    <span>Especialistas inactivos</span>
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
                    <i class="fa fa-briefcase-medical fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Especialidades</span>
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
                    <i class="fa fa-calendar-days fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Agenda hoy </span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos4" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
</div>