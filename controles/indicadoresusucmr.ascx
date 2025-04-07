<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresusucmr.ascx.cs" Inherits="fpWebApp.controles.indicadoresusucmr" %>


<%--
    ****************
    Indicadores: 
        Primer contacto
        Propuesta enviada
        Negociación propuesta
        Negociación aceptada
        Negociación rechazada
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class="widget style1 lazur-bg ">
            <div class="row">
                <div class="col-xs-4 text-center">
                <i class="fa-solid fa-hand-point-up fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Primer contacto</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos1" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <div class="widget style1 bg-success">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa-solid fa-paper-plane fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Propuesta enviada</span>
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
                    <i class="fa-regular fa-file-lines fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Negociación propuesta</span>
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
                    <i class="fa-solid fa-handshake fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Negociación aceptada </span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos4" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
</div>
