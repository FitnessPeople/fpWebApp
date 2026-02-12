<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresAdmSede.ascx.cs" Inherits="fpWebApp.controles.indicadoresAdmSede" %>

<!-- Sweet Alert -->
<link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">
<!-- Sweet alert -->
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

<div class="row">
    <!-- CARD 1: Cumplimiento -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-success">
                <h5>Cumplimiento del mes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblCumplimientoMes" runat="server"></asp:Literal>%
                </h1>
                <small>Ventas vs Meta</small>
            </div>
        </div>
    </div>

    <!-- CARD 2: Ventas -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-primary">
                <h5>Ventas del mes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">$<asp:Literal ID="lblVentasMes" runat="server"></asp:Literal>
                </h1>
                <small>Acumulado a la fecha</small>
            </div>
        </div>
    </div>

    <!-- CARD 3: Meta -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-warning">
                <h5>Meta del mes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">$<asp:Literal ID="lblMetaMes" runat="server"></asp:Literal>
                </h1>
                <small>Meta asignada a la sede</small>
            </div>
        </div>
    </div>

    <!-- CARD 4: Ticket promedio -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-info">
                <h5>Ticket promedio</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">$<asp:Literal ID="lblTicketPromedio" runat="server"></asp:Literal>
                </h1>
                <small>Promedio por venta</small>
            </div>
        </div>
    </div>

</div>

<div class="row">
    <div class="col-lg-12">
        <asp:Repeater ID="rptKPIAsesores" runat="server">
            <HeaderTemplate>
                <table class="table table-striped table-bordered table-hover">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Asesor</th>
                            <th>Ventas</th>
                            <th>Productividad Diaria</th>
                            <th>Ticket Promedio</th>
                            <th>Meta Esperada</th>
                            <th>Cumplimiento</th>
                            <th>Nivel Ventas</th>
                            <th>Nivel Productividad</th>
                            <th>Nivel Ticket</th>
                            <th>Estado</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>

            <ItemTemplate>
                <tr>
                    <td><%# Eval("idUsuario") %></td>
                    <td><%# Eval("Asesor") %></td>
                    <td><%# String.Format("{0:C0}", Eval("Ventas")) %></td>
                    <td><%# String.Format("{0:N2}", Eval("ProductividadDiaria")) %></td>
                    <td><%# String.Format("{0:C0}", Eval("TicketPromedio")) %></td>
                    <td><%# String.Format("{0:C0}", Eval("MetaEsperada")) %></td>
                    <td><%# String.Format("{0:P2}", Eval("Cumplimiento")) %></td>
                    <td><%# Eval("NivelVentas") %></td>
                    <td><%# Eval("NivelProductividad") %></td>
                    <td><%# Eval("NivelTicket") %></td>
                    <td>
                        <span class="label label-danger">
                            <%# Eval("Estado") %>
                        </span>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
        </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>
