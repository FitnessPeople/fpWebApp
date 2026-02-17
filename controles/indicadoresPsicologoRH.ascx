<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="indicadoresPsicologoRH.ascx.cs" Inherits="fpWebApp.controles.indicadoresPsicologoRH" %>

<!-- Sweet Alert -->
<link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">
<!-- Sweet alert -->
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

<div class="row">

    <!-- 1. Empleados Activos -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-primary">
                <h5>Empleados Activos</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblTotalActivos" runat="server"></asp:Literal>
                </h1>
                <small>Base activa actual</small>
            </div>
        </div>
    </div>

    <!-- 2. Ingresos Mes Actual -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-info">
                <h5>Ingresos Mes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblIngresosMes" runat="server"></asp:Literal>
                </h1>
                <small>Nuevos ingresos del mes</small>
            </div>
        </div>
    </div>

    <!-- 3. % Digitalización -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-success">
                <h5>% Digitalización</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblDigitalizacion" runat="server"></asp:Literal>%
                </h1>
                <small>Ingresos con usuario activo</small>
            </div>
        </div>
    </div>

    <!-- 4. Sin Usuario -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-danger">
                <h5>Sin Usuario</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblSinUsuario" runat="server"></asp:Literal>
                </h1>
                <small>Ingresos pendientes digitalizar</small>
            </div>
        </div>
    </div>

</div>


<div class="row">

    <!-- 5. Con Usuario -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-navy">
                <h5>Con Usuario</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblConUsuario" runat="server"></asp:Literal>
                </h1>
                <small>Usuarios activos creados</small>
            </div>
        </div>
    </div>

    <!-- 6. Cargos Ocupados -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-warning">
                <h5>Cargos Ocupados (Mes)</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblCargos" runat="server"></asp:Literal>
                </h1>
                <small>Diversidad de cargos ingresados</small>
            </div>
        </div>
    </div>

    <!-- 7. Cumpleaños del Mes -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-muted">
                <h5>Cumpleaños Mes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblCumpleanios" runat="server"></asp:Literal>
                </h1>
                <small>Empleados activos</small>
            </div>
        </div>
    </div>

    <!-- 8. Retiros del Mes -->
    <div class="col-lg-3">
        <div class="ibox float-e-margins">
            <div class="ibox-title text-danger">
                <h5>Retiros Mes</h5>
            </div>
            <div class="ibox-content">
                <h1 class="no-margins">
                    <asp:Literal ID="lblRetirosMes" runat="server"></asp:Literal>
                </h1>
                <small>Empleados con fecha final este mes</small>
            </div>
        </div>
    </div>
</div>


<div class="ibox-title">
    <h5>
        <asp:Label ID="lblTituloCumple" runat="server"></asp:Label>
    </h5>
</div>

<table class="footable table table-striped list-group-item-text"
       data-paging="true"
       data-filtering="true"
       data-sorting="true">

    <thead>
        <tr>
            <th data-breakpoints="xs"></th>
            <th data-type="date">Fecha</th>
            <th data-sortable="true">Nombre</th>
            <th data-breakpoints="xs sm">Cargo</th>
            <th data-breakpoints="xs sm">Sede</th>
            <th data-breakpoints="xs sm">Antigüedad</th>
        </tr>
    </thead>

    <tbody>
        <asp:Repeater ID="rptCumpleanos" runat="server">
            <ItemTemplate>
                <tr>

                    <!-- FOTO estilo corporativo -->
                    <td class="client-avatar">
                        <img alt="image"
                             src="img/empleados/<%# Eval("FotoEmpleado") %>"
                             onerror="this.src='img/user-default.png';" />
                    </td>

                    <!-- FECHA -->
                    <td>
                    <%--    <%# Eval("icono") %>--%>
                        <%# Eval("Fecha", "{0:dd MMM}") %>
                    </td>

                    <!-- NOMBRE -->
                    <td>
                        <strong><%# Eval("Nombre") %></strong>
                    </td>

                    <!-- CARGO -->
                    <td>
                        <%# Eval("Cargo") %>
                    </td>

                    <!-- SEDE -->
                    <td>
                        <%# Eval("Sede") %>
                    </td>

                    <!-- ANTIGÜEDAD -->
                    <td>
                        <span class="badge badge-info">
                            <%# Eval("Antiguedad") %>
                        </span>
                    </td>

                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>


