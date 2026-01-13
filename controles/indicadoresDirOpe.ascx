<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresDirOpe.ascx.cs" Inherits="fpWebApp.controles.indicadoresDirOpe" %>
<%--
    ****************
    Indicadores: 
        Asistencia promedio diaria
        Clases realizadas vs programadas
        Ocupación promedio por sede
        Incidentes operativos abiertos
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class="widget style1 bg-success">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-person-arrow-down-to-line fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Asistencia promedio diaria</span>
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
                    <i class="fa fa-person-running fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Clases realizadas vs programadas</span>
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
                    <i class="fa fa-users-line fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Ocupación promedio por sede</span>
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
                    <i class="fa fa-users-viewfinder fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Incidentes operativos abiertos</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos4" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
</div>