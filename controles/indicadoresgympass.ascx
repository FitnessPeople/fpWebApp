<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresgympass.ascx.cs" Inherits="fpWebApp.controles.indicadoresgympass" %>
<%--
    ****************
    Indicadores: 
        Gym Pass Agendados
        Gym Pass que Asistieron
        Gym Pass que No Asistieron
        Gym Pass Cancelados
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class="widget style1 navy-bg">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-calendar-plus fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Agendados</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCantidadAgendados" runat="server"></asp:Literal>
                    </h2>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <div class="widget style1 bg-success">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-calendar-check fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Asistieron</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCantidadAsistieron" runat="server"></asp:Literal>
                    </h2>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <div class="widget style1 bg-danger">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-calendar-xmark fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>No Asistieron</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCantidadNoAsistieron" runat="server"></asp:Literal>
                    </h2>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <div class="widget style1 yellow-bg">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-calendar-minus fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Cancelados</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCantidadCancelados" runat="server"></asp:Literal>
                    </h2>
                </div>
            </div>
        </div>
    </div>
</div>