<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresconcursogympass.ascx.cs" Inherits="fpWebApp.controles.indicadoresconcursogympass" %>
<%--
    ****************
    Indicadores: 
        Cantidad total de personas registradas
        Embajador con más códigos registrados
        Sedes con más personas registradas
        Fecha con más personas registradas
    ****************
--%>
<div class="row">
    <div class="col-lg-3">
        <div class="widget style1 navy-bg">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-users fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Registrados</span>
                    <h2 class="font-bold">
                        <asp:Literal ID="ltCantidadTotalPersonasRegistradas" runat="server"></asp:Literal>
                    </h2>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <div class="widget style1 bg-success">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-crown fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Mejor Embajador</span>
                    <div>
                        <h3 class="font-bold">
                            <asp:Literal ID="ltNombreEmbajador" runat="server"></asp:Literal>
                        </h3>
                        <h3 class="font-bold">
                            <asp:Literal ID="ltCantidadTotalEmbajador" runat="server"></asp:Literal>
                        </h3>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <div class="widget style1 bg-danger">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-building fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Mejor Sede</span>
                    <div>
                        <h3 class="font-bold">
                            <asp:Literal ID="ltNombreSede" runat="server"></asp:Literal>
                        </h3>
                        <h3 class="font-bold">
                            <asp:Literal ID="ltCantidadTotalSede" runat="server"></asp:Literal>
                        </h3>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-3">
        <div class="widget style1 yellow-bg">
            <div class="row">
                <div class="col-xs-4">
                    <i class="fa fa-calendar-day fa-5x"></i>
                </div>
                <div class="col-xs-8 text-right">
                    <span>Mejor Fecha</span>
                    <div>
                        <h3 class="font-bold">
                            <asp:Literal ID="ltFechaMasRegistros" runat="server"></asp:Literal>
                        </h3>
                        <h3 class="font-bold">
                            <asp:Literal ID="ltCantidadTotalFecha" runat="server"></asp:Literal>
                        </h3>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>