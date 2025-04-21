<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresusucrm.ascx.cs" Inherits="fpWebApp.controles.indicadoresusucrm" %>

<%--
    ****************
    Indicadores: 
        Primer contacto
        Propuesta en gestión
        Negociación aceptada
        Negociación rechazada
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class='widget style1 lazur-bg'>
            <div class="row">
                <div class="col-xs-4 text-center">
                <i class="fa-solid fa-hand-point-up fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>
                        <asp:Literal ID="ltEstado1" runat="server"></asp:Literal></span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos1" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <div class="widget style1 yellow-bg">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa-solid fa-paper-plane fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span><asp:Literal ID="ltEstado2" runat="server"></asp:Literal></span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos2" runat="server"></asp:Literal>
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
                    <span><asp:Literal ID="ltEstado3" runat="server"></asp:Literal></span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos3" runat="server"></asp:Literal>
                    </h2>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <asp:Literal ID="ltWidget4" runat="server"></asp:Literal>
        <div class="widget style1 red-bg">
            <div class="row">
                <div class="col-xs-4">
                   <i class="fa-solid fa-handshake-slash fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span><asp:Literal ID="ltEstado4" runat="server"></asp:Literal></span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCuantos4" runat="server"></asp:Literal></h2>
                </div>
            </div>
        </div>
    </div>
</div>
