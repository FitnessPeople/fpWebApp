<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="rightsidebar.ascx.cs" Inherits="fpWebApp.controles.rightsidebar" %>
<div id="right-sidebar">
    <div class="sidebar-container">

        <ul class="nav nav-tabs navs-2">
            <li class="active">
                <asp:Literal ID="ltEtiqueta1" runat="server"></asp:Literal>
            </li>
            <li>
                <a data-toggle="tab" href="#tab-1">Cumpleaños</a>
            </li>
            <%--<li>
                <a data-toggle="tab" href="#tab-3"><i class="fa fa-gear"></i></a>
            </li>--%>
        </ul>

        <div class="tab-content">

            <div id="tab-2" class="tab-pane active">

                <%--<div class="sidebar-title">
                    <h3><i class="fa fa-cube m-r-sm"></i>Últimas tareas</h3>
                    <small><i class="fa fa-tim"></i>Tienes 10 tareas, 8 sin completar.</small>
                </div>--%>

                <ul class="sidebar-list list-group-item">
                    <asp:Repeater ID="rpTareas" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href="#">
                                    <%--<div class="small pull-right m-t-xs">Hace x horas/dias</div>--%>
                                    <h4><%# Eval("TituloTarea") %></h4>
                                    <%# Eval("DescripcionTarea") %>
                                    <div class="small"><%# Eval("Completado") %>% completado</div>
                                    <div class="progress progress-mini">
                                        <div style='width: <%# Eval("Completado") %>%;' class="progress-bar progress-bar-warning"></div>
                                    </div>
                                    <div class="small text-muted m-t-xs">Termina: <%# Eval("FechaFinal") %></div>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>

                    <asp:Repeater ID="rpEnlaces" runat="server" OnItemDataBound="rpEnlaces_ItemDataBound">
                        <ItemTemplate>
                            <li style="padding: 1px 20px;">
                                <h6 class="text-info font-bold m-b-xxs"><%# Eval("NombrePlan") %></h6>
                                <asp:Literal ID="ltEnlacePago" runat="server"></asp:Literal>
                                <%--<asp:HiddenField ID="hdEnlacePago" runat="server" ClientIDMode="Static" />--%>
                                <button type="button" class="btn btn-success btn-xs" id="btnPortapapeles"
                                    onclick='copyToClipboard(this)' title="Copiar enlace">
                                    <i class="fa fa-copy"></i>
                                </button>

                            </li>
                        </ItemTemplate>
                    </asp:Repeater>

                    <!-- Enlace para cambio de método de pago -->
                    <li style="padding: 1px 20px;">
                        <h6 class="text-info font-bold m-b-xxs">Cambio de método de pago</h6>
                        <asp:Literal ID="ltEnlaceCambio" runat="server"></asp:Literal>
                        <button type="button" class="btn btn-success btn-xs" id="btnPortapapeles"
                            onclick='copyToClipboard(this)' title="Copiar enlace">
                            <i class="fa fa-copy"></i>
                        </button>
                    </li>
                </ul>

            </div>

            <script>
                function copyToClipboard(btn) {
                    var url = btn.parentElement.querySelector(".enlace").innerText;
                    navigator.clipboard.writeText(url);
                }
            </script>



            <div id="tab-1" class="tab-pane">

                <div class="sidebar-title">
                    <h3><i class="fa fa-birthday-cake m-r-sm text-warning"></i>
                        Cumpleaños del Mes
                    </h3>
                    <small>
                        <asp:Label ID="lblCantidadCumpleSidebar" runat="server"></asp:Label>
                    </small>
                </div>

                <div>
                    <asp:Repeater ID="rptCumpleSidebar" runat="server">
                        <ItemTemplate>
                            <div class="sidebar-message">
                                <a href="#">

                                    <!-- FOTO -->
                                    <div class="pull-left text-center">
                                        <img alt="image"
                                            class="img-circle message-avatar"
                                            src='img/empleados/<%# Eval("FotoEmpleado") %>'
                                            onerror="this.src='img/empleados/nofoto.png';" />
                                    </div>

                                    <!-- CONTENIDO -->
                                    <div class="media-body">
                                        <strong><%# Eval("Nombre") %></strong>
                                        <br />
                                        <small><i class="fa fa-cake text-warning"></i> <%# Eval("Fecha", "{0:dd MMM}") %>
                                            <br />
                                            <%# Eval("Cargo") %> - <%# Eval("Sede") %>
                                        </small>
                                    </div>
                                </a>
                            </div>

                        </ItemTemplate>
                    </asp:Repeater>

                </div>

            </div>

            <div id="tab-3" class="tab-pane">

                <div class="sidebar-title">
                    <h3><i class="fa fa-gears m-r-sm"></i>Configuración</h3>
                    <small><i class="fa fa-tim"></i>Tienes 14 proyectos. 10 sin completar.</small>
                </div>

                <div class="setings-item">
                    <span>Mostrar notificaciones
                    </span>
                    <div class="switch">
                        <div class="onoffswitch">
                            <input type="checkbox" name="collapsemenu" class="onoffswitch-checkbox" id="example">
                            <label class="onoffswitch-label" for="example">
                                <span class="onoffswitch-inner"></span>
                                <span class="onoffswitch-switch"></span>
                            </label>
                        </div>
                    </div>
                </div>
                <div class="setings-item">
                    <span>Deshabilitar chat
                    </span>
                    <div class="switch">
                        <div class="onoffswitch">
                            <input type="checkbox" name="collapsemenu" checked class="onoffswitch-checkbox" id="example2">
                            <label class="onoffswitch-label" for="example2">
                                <span class="onoffswitch-inner"></span>
                                <span class="onoffswitch-switch"></span>
                            </label>
                        </div>
                    </div>
                </div>
                <div class="setings-item">
                    <span>Habilitar historia
                    </span>
                    <div class="switch">
                        <div class="onoffswitch">
                            <input type="checkbox" name="collapsemenu" class="onoffswitch-checkbox" id="example3">
                            <label class="onoffswitch-label" for="example3">
                                <span class="onoffswitch-inner"></span>
                                <span class="onoffswitch-switch"></span>
                            </label>
                        </div>
                    </div>
                </div>
                <div class="setings-item">
                    <span>Mostrar graficos
                    </span>
                    <div class="switch">
                        <div class="onoffswitch">
                            <input type="checkbox" name="collapsemenu" class="onoffswitch-checkbox" id="example4">
                            <label class="onoffswitch-label" for="example4">
                                <span class="onoffswitch-inner"></span>
                                <span class="onoffswitch-switch"></span>
                            </label>
                        </div>
                    </div>
                </div>
                <div class="setings-item">
                    <span>Usuarios fuera de linea
                    </span>
                    <div class="switch">
                        <div class="onoffswitch">
                            <input type="checkbox" checked name="collapsemenu" class="onoffswitch-checkbox" id="example5">
                            <label class="onoffswitch-label" for="example5">
                                <span class="onoffswitch-inner"></span>
                                <span class="onoffswitch-switch"></span>
                            </label>
                        </div>
                    </div>
                </div>
                <div class="setings-item">
                    <span>Busqueda global
                    </span>
                    <div class="switch">
                        <div class="onoffswitch">
                            <input type="checkbox" checked name="collapsemenu" class="onoffswitch-checkbox" id="example6">
                            <label class="onoffswitch-label" for="example6">
                                <span class="onoffswitch-inner"></span>
                                <span class="onoffswitch-switch"></span>
                            </label>
                        </div>
                    </div>
                </div>
                <div class="setings-item">
                    <span>Actualizacion diaria
                    </span>
                    <div class="switch">
                        <div class="onoffswitch">
                            <input type="checkbox" name="collapsemenu" class="onoffswitch-checkbox" id="example7">
                            <label class="onoffswitch-label" for="example7">
                                <span class="onoffswitch-inner"></span>
                                <span class="onoffswitch-switch"></span>
                            </label>
                        </div>
                    </div>
                </div>

                <div class="sidebar-content">
                    <h4>Configuracion</h4>
                    <div class="small">
                        I belive that. Lorem Ipsum is simply dummy text of the printing and typesetting industry.
                        And typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.
                        Over the years, sometimes by accident, sometimes on purpose (injected humour and the like).
                    </div>
                </div>

            </div>
        </div>
    </div>
</div>
