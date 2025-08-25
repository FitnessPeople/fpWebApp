<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detallereportemarketing.aspx.cs" Inherits="fpWebApp.detallereportemarketing" %>

<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/indicadores01.ascx" TagPrefix="uc1" TagName="indicadores01" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Detalle Reporte Estrategia Marketing</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <%--<link href="css/plugins/footable/footable.core.css" rel="stylesheet" />--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <style type="text/css" media="print">
        body {
            visibility: hidden;
            display: none
        }
    </style>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#reporteestrategiascrmmarketing");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#crm");
            element2.classList.remove("collapse");
        }
    </script>
</head>

<body onload="changeClass()">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <i class="fa fa-address-card modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para visualizar cargos de empleados</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar los cargos de empleados de manera clara y eficiente.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Crea uno nuevo</b><br />
                        Usa el campo que está a la <b>izquierda</b> para digitar el nombre que quieres registrar.<br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i><b>Agregar:</b> Guarda la información y finaliza el registro.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i><b>Cancelar:</b> Si necesitas volver atrás sin guardar cambios.
                    <br />
                        <br />
                        <b>Paso 2: Visualiza</b><br />
                        Usa el buscador que está a la <b>derecha</b> para encontrar lo que buscas.<br />
                        <i class="fa-solid fa-magnifying-glass"></i>Filtra por 
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i><b>Nombre</b>
                        <br />
                        <br />
                        <b>Paso 3: Gestiona</b><br />
                        En la columna <b>Acciones</b> encontrarás estas opciones:<br />
                        <i class="fa fa-edit" style="color: #1AB394;"></i><b>Editar:</b> Modifica los datos necesarios.<br />
                        <i class="fa fa-trash" style="color: #DC3545;"></i><b>Eliminar:</b> Borra lo que creas innecesario.
                    <br />
                        <br />
                        <b>Paso 4: Acción adicional</b><br />
                        Al lado opuesto del buscador encontrarás un botón útil:<br />
                        <i class="fa-solid fa-file-export" style="color: #212529;"></i><b>Exportar a Excel:</b>
                        <br />
                        <br />
                        <i class="fa fa-exclamation-circle mr-2"></i>Si tienes dudas, no dudes en consultar con el administrador del sistema.
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div id="wrapper">

        <uc1:navbar runat="server" ID="navbar" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa-solid fa-address-card text-success m-r-sm"></i>Detalle estrategia marketing</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>CRM</li>
                        <li class="active"><strong>Detalle estrategia marketing</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">
                    <%--Inicio Contenido!!!!--%>

                    <div class="ibox-content m-b-sm border-bottom" runat="server" id="divMensaje" visible="false">
                        <div class="p-xs">
                            <div class="pull-left m-r-md">
                                <i class="fa fa-triangle-exclamation text-danger mid-icon"></i>
                            </div>
                            <h2>Acceso Denegado</h2>
                            <span>Lamentablemente, no tienes permiso para acceder a esta página. Por favor, verifica que estás usando una cuenta con los permisos adecuados o contacta a nuestro soporte técnico para más información. Si crees que esto es un error, no dudes en ponerte en contacto con nosotros para resolver cualquier problema. Gracias por tu comprensión.</span>
                        </div>
                    </div>

                    <uc1:paginasperfil runat="server" ID="paginasperfil" Visible="false" />

                    <form id="form1" runat="server">
                        <div class="row" id="divContenido" runat="server">

                            <div class="row">
                                <div class="col-lg-8">
                                    <div class="wrapper wrapper-content animated fadeInUp">
                                        <div class="ibox">
                                            <div class="ibox-content">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="m-b-md">
                                                            <a href="#" class="btn btn-white btn-xs pull-right">Editar estrategia</a>
                                                            <h2>
                                                                <asp:Literal ID="ltNombreEstrategia" runat="server"></asp:Literal></h2>
                                                        </div>
                                                        <dl class="dl-horizontal">
                                                            <dt>Estado:</dt>
                                                            <dd><span class="label label-primary">
                                                                <asp:Literal ID="ltEstadoEstrategia" runat="server"></asp:Literal>
                                                            </span></dd>
                                                        </dl>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-5">
                                                        <dl class="dl-horizontal">

                                                            <dt>Creado por:</dt>
                                                            <dd>
                                                                <asp:Literal ID="ltNombreUsuario" runat="server"></asp:Literal>
                                                            </dd>
                                                            <dt>Fecha Creación:</dt>
                                                            <dd>
                                                                <asp:Literal ID="ltFechaCreacion" runat="server"></asp:Literal>
                                                            </dd>
                                                            <dt>Total Leads:</dt>
                                                            <dd>
                                                                <span class="label label-success">
                                                                    <asp:Literal ID="ltCantidadLeadsEstrategia" runat="server"></asp:Literal>
                                                                </span>
                                                            </dd>
                                                            <dt>Total Leads con pago:</dt>
                                                            <dd>
                                                                <span class="label label-primary">
                                                                    <asp:Literal ID="ltCantidadLeadsAprobados" runat="server"></asp:Literal>
                                                                </span>
                                                            </dd>
                                                            <dt>Descripción de la estrategia:</dt>
                                                            <dd>
                                                                <asp:Literal ID="ltDescripcionEstrategia" runat="server"></asp:Literal>
                                                            </dd>
                                                        </dl>
                                                    </div>
                                                    <div class="col-lg-7" id="cluster_info">
                                                        <dl class="dl-horizontal">
                                                            <dt>Fecha Inicio:</dt>
                                                            <dd>
                                                                <asp:Literal ID="ltFechaIni" runat="server"></asp:Literal>
                                                            </dd>
                                                            <dt>Fecha Fin:</dt>
                                                            <dd>
                                                                <asp:Literal ID="ltFechaFin" runat="server"></asp:Literal>
                                                            </dd>
                                                            <dt>Tipo estrategia:</dt>
                                                            <dd>
                                                                <asp:Literal ID="ltTipoEstrategia" runat="server"></asp:Literal>
                                                            </dd>
                                                            <dt>Mejores asesores:</dt>
                                                            <dd class="project-people">
                                                                <a href="">
                                                                    <img alt="image" class="img-circle" src="img/a3.jpg"></a>
                                                                <a href="">
                                                                    <img alt="image" class="img-circle" src="img/a1.jpg"></a>
                                                                <a href="">
                                                                    <img alt="image" class="img-circle" src="img/a2.jpg"></a>
                                                                <a href="">
                                                                    <img alt="image" class="img-circle" src="img/a4.jpg"></a>
                                                                <a href="">
                                                                    <img alt="image" class="img-circle" src="img/a5.jpg"></a>
                                                            </dd>
                                                        </dl>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <dl class="dl-horizontal">
                                                            <dt>Eficiencia:</dt>
                                                            <dd>
                                                                <div class="progress progress-striped active m-b-sm">
                                                                    <div id="progressEficiencia" runat="server" class="progress-bar progress-bar-primary"></div>
                                                                </div>
                                                                <small>La estrategia completó con un 
                                                                <strong>
                                                                    <asp:Literal ID="ltEficiencia" runat="server"></asp:Literal>%</strong>
                                                                </small>
                                                            </dd>
                                                        </dl>
                                                    </div>

                                                </div>
                                                <div class="row m-t-sm">
                                                    <div class="col-lg-12">
                                                        <div class="panel blank-panel">
                                                            <div class="panel-heading">
                                                                <div class="panel-options">
                                                                    <ul class="nav nav-tabs">
                                                                        <li class="active"><a href="#tab-1" data-toggle="tab">Leads gestionados</a></li>
                                                                        <%--<li class=""><a href="#tab-2" data-toggle="tab">Last activity</a></li>--%>
                                                                    </ul>
                                                                </div>
                                                            </div>

                                                            <div class="panel-body">

                                                                <table id="tablaContactos" class="footable table table-striped list-group-item-text" data-paging-size="10"
                                                                    data-filter-min="3" data-filter-placeholder="Buscar"
                                                                    data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}"
                                                                    data-paging-limit="10" data-filtering="true"
                                                                    data-filter-container="#filter-form-container1" data-filter-delay="300"
                                                                    data-filter-dropdown-title="Buscar en:" data-filter-position="left"
                                                                    data-empty="Sin resultados">
                                                                    <thead>
                                                                        <tr>
                                                                            <th data-sortable="false" data-breakpoints="xs" style="width: 300px;">Nombre</th>
                                                                            <th data-breakpoints="xs">Teléfono</th>
                                                                            <th data-breakpoints="xs">Estado</th>
                                                                            <th data-breakpoints="xs">Plan</th>
                                                                            <th data-breakpoints="xs">HaceCuanto</th>
                                                                            <th data-breakpoints="xs">Lead</th>
                                                                            <th data-breakpoints="all" data-title="Info"></th>
                                                                            <th data-sortable="false" data-filterable="false" class="text-left" style="width: 120px;">Acciones</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <asp:Repeater ID="rpContactosCRM" runat="server" OnItemDataBound="rpContactosCRM_ItemDataBound">
                                                                            <ItemTemplate>
                                                                                <tr class="feed-element">
                                                                                    <td><%# Eval("NombreContacto") %> <%# Eval("ApellidoContacto") %></td>
                                                                                    <td><%# Eval("TelefonoContacto") %></td>
                                                                                    <td>Caliente</td>
                                                                                    <td><%# Eval("NombrePlan") %></td>
                                                                                    <td>
                                                                                        <asp:Literal ID="ltTiempoTranscurrido" runat="server"></asp:Literal></td>
                                                                                    <td>
                                                                                        <span title='<%# Eval("NombreEstadoCRM") %>' style='color: <%# Eval("ColorHexaCRM") %>'>
                                                                                            <%# Eval("IconoMinEstadoCRM") %>
                                                                                        </span><%# Eval("NombreEstadoCRM") %>
                                                                                    </td>

                                                                                    <td>
                                                                                        <table class="table table-bordered table-striped">
                                                                                            <tr>
                                                                                                <%-- <th width="25%"><i class="fa fa-city m-r-xs"></i>Ciudad</th>--%>
                                                                                                <th width="25%"><i class="fa fa-mobile m-r-xs"></i>Archivo propuesta</th>
                                                                                                <th width="50%" class="text-nowrap"><i class="fa fa-clock m-r-xs"></i>Historial</th>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td><%# Eval("ArchivoPropuesta") %></td>
                                                                                                <td><%# Eval("HistorialHTML2") %></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                    <td style="display: flex; flex-wrap: nowrap;">

                                                                                        <a runat="server" id="btnEditar" href="#" class="btn btn-outline btn-primary pull-left m-r-xs"
                                                                                            style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false" onclientclick="ocultarContador(); return true;">
                                                                                            <i class="fa fa-edit"></i></a>
                                                                                        <a runat="server" id="btnEliminar" href="#" class="btn btn-outline btn-danger pull-left m-r-xs"
                                                                                            style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false"><i class="fa fa-trash"></i></a>
                                                                                        <a runat="server" id="btnNuevoAfiliado" href="#" class="btn btn-outline btn-success pull-left"
                                                                                            style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" data-idcrm='<%# Eval("idContacto") %>'
                                                                                            data-documento='<%# Eval("DocumentoAfiliado") %>' onclick="redirigirNuevoAfiliado(this, event)">
                                                                                            <i class="fa fa-id-card"></i></a>
                                                                                    </td>
                                                                                </tr>
                                                                            </ItemTemplate>
                                                                        </asp:Repeater>
                                                                    </tbody>
                                                                </table>

                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">

                                    <div class="wrapper wrapper-content animated fadeInRight">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="ibox float-e-margins">
                                                    <div class="ibox-title">
                                                        <h5>Resultados canales de venta<small></small></h5>
                                                        <div class="ibox-tools">
                                                            <a class="collapse-link">
                                                                <i class="fa fa-chevron-up"></i>
                                                            </a>
                                                            <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                                                                <i class="fa fa-wrench"></i>
                                                            </a>
                                                            <ul class="dropdown-menu dropdown-user">
                                                                <li><a href="#">Config option 1</a>
                                                                </li>
                                                                <li><a href="#">Config option 2</a>
                                                                </li>
                                                            </ul>
                                                            <a class="close-link">
                                                                <i class="fa fa-times"></i>
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="flot-chart">
                                                            <div class="flot-chart-content" id="flot-bar-chart"></div>
                                                            <asp:HiddenField ID="hiddenGrafica" runat="server" />

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="ibox float-e-margins">
                                                    <div class="ibox-title">
                                                        <h5>Resultados planes comerciales</h5>
                                                        <div class="ibox-tools">
                                                            <a class="collapse-link">
                                                                <i class="fa fa-chevron-up"></i>
                                                            </a>
                                                            <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                                                                <i class="fa fa-wrench"></i>
                                                            </a>
                                                            <ul class="dropdown-menu dropdown-user">
                                                                <li><a href="#">Config option 1</a>
                                                                </li>
                                                                <li><a href="#">Config option 2</a>
                                                                </li>
                                                            </ul>
                                                            <a class="close-link">
                                                                <i class="fa fa-times"></i>
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="flot-chart">
                                                            <div class="flot-chart-pie-content" id="flot-pie-chart"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                           <div class="row">
                                            <div class="col-lg-12">
                                                <table class="table table-hover margin bottom">
                                                    <thead>
                                                    <tr>
                                                        <th style="width: 1%" class="text-center">Identifiación.</th>
                                                        <th>Asesor/a</th>
                                                        <th class="text-center">Día</th>
                                                        <th class="text-center">Cantidad Leads</th>
                                                    </tr>
                                                    </thead>
                                                    <tbody>
                                                    <tr>
                                                        <td class="text-center">1</td>
                                                        <td> Security doors
                                                            </td>
                                                        <td class="text-center small">16 Jun 2014</td>
                                                        <td class="text-center"><span class="label label-primary">$483.00</span></td>

                                                    </tr>
                                                    <tr>
                                                        <td class="text-center">2</td>
                                                        <td> Wardrobes
                                                        </td>
                                                        <td class="text-center small">10 Jun 2014</td>
                                                        <td class="text-center"><span class="label label-primary">$327.00</span></td>

                                                    </tr>
                                                    <tr>
                                                        <td class="text-center">3</td>
                                                        <td> Set of tools
                                                        </td>
                                                        <td class="text-center small">12 Jun 2014</td>
                                                        <td class="text-center"><span class="label label-warning">$125.00</span></td>

                                                    </tr>
                                                    <tr>
                                                        <td class="text-center">4</td>
                                                        <td> Panoramic pictures</td>
                                                        <td class="text-center small">22 Jun 2013</td>
                                                        <td class="text-center"><span class="label label-primary">$344.00</span></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-center">5</td>
                                                        <td>Phones</td>
                                                        <td class="text-center small">24 Jun 2013</td>
                                                        <td class="text-center"><span class="label label-primary">$235.00</span></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="text-center">6</td>
                                                        <td>Monitors</td>
                                                        <td class="text-center small">26 Jun 2013</td>
                                                        <td class="text-center"><span class="label label-primary">$100.00</span></td>
                                                    </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                          </div>
                                    </div>




                                </div>
                            </div>
                        </div>

                    </form>
                    <%--Fin Contenido!!!!--%>
                </div>
            </div>
            <uc1:footer runat="server" ID="footer" />
        </div>
        <uc1:rightsidebar runat="server" ID="rightsidebar" />
    </div>

    <!-- Mainly scripts -->
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- FooTable -->
    <%--<script src="js/plugins/footable/footable.all.min.js"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();
    </script>

    <!-- Flot -->
    <script src="js/plugins/flot/jquery.flot.js"></script>
    <script src="js/plugins/flot/jquery.flot.tooltip.min.js"></script>
    <script src="js/plugins/flot/jquery.flot.resize.js"></script>
    <script src="js/plugins/flot/jquery.flot.pie.js"></script>
    <script src="js/plugins/flot/jquery.flot.time.js"></script>

    <!-- Flot demo data -->
    <script src="js/demo/flot-demo.js"></script>

    <script>
        $(function () {
            var rawData = $('#<%= hiddenGrafica.ClientID %>').val();

            if (!rawData || rawData.trim() === "") {
                console.warn("⚠️ No hay datos en hiddenGrafica");
                return;
            }

            var parsedData = JSON.parse(rawData);

            var barData = {
                label: "Ventas",
                data: parsedData.map(function (d) { return [d[0], d[1]]; })
            };

            // 👉 Función para formatear números en K/M
            function formatNumber(num) {
                if (num >= 1000000) {
                    return (num / 1000000).toFixed(1).replace(".0", "") + "M";
                } else if (num >= 1000) {
                    return (num / 1000).toFixed(1).replace(".0", "") + "K";
                }
                return num;
            }

            var barOptions = {
                series: {
                    bars: {
                        show: true,
                        barWidth: 0.6,
                        align: "center",
                        fill: true,
                        fillColor: { colors: [{ opacity: 0.8 }, { opacity: 0.8 }] }
                    }
                },
                xaxis: {
                    ticks: parsedData.map(function (d) { return [d[0], d[2]]; }),
                    tickDecimals: 0,
                    tickLength: 0
                },
                colors: ["#1ab394"],
                grid: {
                    color: "#999999",
                    hoverable: true,
                    clickable: true,
                    tickColor: "#D4D4D4",
                    borderWidth: 0
                },
                legend: { show: false },
                tooltip: true,
                tooltipOpts: {
                    content: function (label, x, y) {
                        var canal = parsedData.find(function (d) { return d[0] === x; });
                        return canal ? canal[2] + "<br>Ventas: " + formatNumber(y) : y;
                    }
                }
            };

            $.plot($("#flot-bar-chart"), [barData], barOptions);

            // 👇 Rotar etiquetas del eje X
            $("#flot-bar-chart .flot-x-axis div").css("transform", "rotate(45deg)")
                .css("transform-origin", "top right")
                .css("white-space", "nowrap");
        });

    </script>

    <script>
        $(function () {
            // Métrica del área del pie: "cantidad" (número de planes) o "valor" (monto)
            var metric = "cantidad"; // ← como pediste, por número de planes vendidos

            var raw = (window.planesRanking || []);
            // 1) Construir data → 2) quitar ceros → 3) ordenar desc (ranking visual)
            var data = raw
                .map(function (p) {
                    return {
                        label: p.label,
                        data: metric === "cantidad" ? (Number(p.cantidad) || 0) : (Number(p.valor) || 0)
                    };
                })
                .filter(function (it) { return it.data > 0; })   // ← no mostrar ceros
                .sort(function (a, b) { return b.data - a.data; });

            if (!data.length) {
                $("#flot-pie-chart").text("Sin datos para mostrar.");
                return;
            }

            $.plot($("#flot-pie-chart"), data, {
                series: {
                    pie: {
                        show: true,
                        radius: 1,
                        label: { show: false } // evitamos ruido visual, usa tooltip/leyenda
                        // si quisieras agrupar por porcentaje pequeño:
                        // ,combine: { threshold: 0.03, label: "Otros" }
                    }
                },
                legend: { show: true, position: "ne" },
                grid: { hoverable: true },
                tooltip: true,
                tooltipOpts: {
                    // %p = porcentaje, %s = label, %y = valor
                    content: metric === "cantidad"
                        ? "%p.1% | %s: %y planes"
                        : "%p.1% | %s: %y"
                },
                colors: ["#1ab394", "#79d2c0", "#bababa", "#d3d3d3", "#f8ac59", "#ed5565", "#23c6c8"]
            });
        });

    </script>


</body>

</html>
