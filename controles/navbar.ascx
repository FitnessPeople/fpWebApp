<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="navbar.ascx.cs" Inherits="fpWebApp.controles.navbar" %>
<nav class="navbar-default navbar-static-side" role="navigation">
    <div class="sidebar-collapse">
        <ul class="nav metismenu" id="side-menu">
            <li class="nav-header">
                <div class="dropdown profile-element">
                    <span>
                        <asp:Literal ID="ltFoto" runat="server"></asp:Literal>
                    </span>
                   <asp:Literal ID="ltCargo" runat="server" Visible="false"></asp:Literal>
                    <a data-toggle="dropdown" class="dropdown-toggle" href="#" title="<%= ltCargo.Text %>">
                        <span class="clear"><span class="block m-t-xs"><strong class="font-bold">
                            <asp:Literal ID="ltNombreUsuario" runat="server"></asp:Literal></strong>
                        </span><span class="text-muted text-xs block">
                            <asp:Label ID="lblNombrePerfil" runat="server" Text="Label"></asp:Label><br/>
                            <asp:Label ID="lblNombreSede" runat="server" Text="Label"></asp:Label>
                            <b class="caret"></b></span></span></a>
                    <ul class="dropdown-menu animated fadeInRight m-t-xs">
                        <li><a href="micuenta?token=qu3rty">Mi cuenta</a></li>
                        <li><a href="contactos">Contactos</a></li>
                        <li><a href="mensajes">Mensajes</a></li>
                        <li class="divider"></li>
                        <li><a href="logout">Salir</a></li>
                    </ul>
                </div>
                <div class="logo-element">
                    FP+
                </div>
            </li>
            <li id="inicio" class="old">
                <a href="inicio"><i class="fa fa-house"></i><span class="nav-label">Inicio</span></a>
            </li>
            <asp:Literal ID="ltMenu" runat="server"></asp:Literal>
            <asp:Literal ID="ltMenuCalendario" runat="server"></asp:Literal>
            <li class="special_link">
                <a href="https://fitnesspeoplecmdcolombia.com" target="_blank"><i class="fa fa-star"></i><span class="nav-label">Fitness People WebPage</span></a>
            </li>
        </ul>

    </div>
</nav>