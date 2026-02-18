<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresDirOpe2.ascx.cs" Inherits="fpWebApp.controles.indicadoresDirOpe2" %>

<div class="ibox-title">
    <h5>
        <asp:Label ID="lblTituloMesAc" runat="server"></asp:Label>
    </h5>
</div>

<div class="row">
    <asp:Repeater ID="rptKpis" runat="server">
        <ItemTemplate>

            <div class="col-xl-3 col-lg-3 col-md-6 mb-4">
                <div class="ibox h-100">

                    <!-- TÍTULO -->
                    <div class="ibox-title text-center">
                        <h5>
                            <i class="fa fa-bolt text-warning"></i>
                            <%# Eval("Kpi") %>
                        </h5>
                    </div>

                    <!-- CONTENIDO -->
                    <div class="ibox-content text-center">

                        <h1 class="no-margins">
                            <%# Eval("ValorFormateado") %>
                        </h1>

                        <small class='<%# Eval("ColorClase") %>'>
                            <i class='<%# Eval("IconoTendencia") %>'></i>
                            <%# Eval("TextoTendencia") %>
                        </small>

                        <!-- AREA ABAJO -->
                        <div style="margin-top:15px;">
                            <small class="text-muted">
                                <%# Eval("Area") %>
                            </small>
                        </div>

                    </div>

                </div>
            </div>

        </ItemTemplate>
    </asp:Repeater>
</div>

