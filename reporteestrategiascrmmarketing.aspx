<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reporteestrategiascrmmarketing.aspx.cs" Inherits="fpWebApp.reporteestrategiascrmmarketing" %>

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

    <title>Fitness People | Reporte estrategias marketing</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- CSS de Quill -->
    <link href="https://cdn.quilljs.com/1.3.6/quill.snow.css" rel="stylesheet">
    <!-- JS de Quill -->
    <script src="https://cdn.quilljs.com/1.3.6/quill.min.js"></script>


    <%--<link href="css/plugins/footable/footable.bootstrap.css" rel="stylesheet" />--%>
    <!-- FooTable -->
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
            var element2 = document.querySelector("#CRM");
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
                    <i class="fa fa-school-flag modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para visualizar sedes</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar las sedes de manera clara y eficiente.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Crea una nueva sede</b><br />
                        Usa el formulario que está a la <b>izquierda</b> para digitar la información necesaria de la sede.<br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i><b>Agregar:</b> Guarda la información y finaliza el registro.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i><b>Cancelar:</b> Si necesitas volver atrás sin guardar cambios.
                    <br />
                        <br />
                        <b>Paso 2: Visualiza las sedes existentes</b><br />
                        Usa el buscador que está a la <b>derecha</b> para encontrar lo que buscas.<br />
                        <i class="fa-solid fa-magnifying-glass"></i>Filtra por 
                        <i class="fa-solid fa-school-flag" style="color: #0D6EFD;"></i><b>Sede</b>,
                        <i class="fa-solid fa-location-dot" style="color: #0D6EFD;"></i><b>Dirección</b> o
                        <i class="fa-solid fa-school-flag" style="color: #0D6EFD;"></i><b>Tipo de Sede</b>
                        <br />
                        <br />
                        <b>Paso 3: Gestiona las sedes</b><br />
                        En la columna "Acciones" encontrarás estas opciones:<br />
                        <i class="fa fa-edit" style="color: #1AB394;"></i><b>Editar:</b> Modifica los datos necesarios.<br />
                        <i class="fa fa-trash" style="color: #DC3545;"></i><b>Eliminar:</b> Borra lo que creas innecesario.
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

        <uc1:navbar runat="server" ID="navbar1" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header1" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa fa-school-flag text-success m-r-sm"></i>Reporte estrategias marketing</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>CRM</li>
                        <li class="Activa"><strong>Reporte estrategia</strong></li>
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

                    <form role="form" id="form" runat="server">
                        <div class="wrapper wrapper-content animated fadeInRight">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="ibox">
                                        <div class="ibox-title">
                                            <span class="label label-primary pull-right">Nuevo</span>
                                            <h5>
                                                <asp:Literal ID="ltTeam" runat="server"></asp:Literal></h5>
                                        </div>
                                        <div class="ibox-content">
                                            <div class="team-members">
                                                <a href="#">
                                                    <img alt="member" class="img-circle" src="img/a1.jpg"></a>
                                                <a href="#">
                                                    <img alt="member" class="img-circle" src="img/a2.jpg"></a>
                                                <a href="#">
                                                    <img alt="member" class="img-circle" src="img/a3.jpg"></a>
                                                <a href="#">
                                                    <img alt="member" class="img-circle" src="img/a5.jpg"></a>
                                                <a href="#">
                                                    <img alt="member" class="img-circle" src="img/a6.jpg"></a>
                                            </div>
                                            <h4>Resumen de desempeño y métricas clave del equipo</h4>
                                            <p>
                                                <asp:Literal ID="ltDescripcion" runat="server"></asp:Literal>
                                            </p>
                                            <%--                                            <div>
                                                <span>Estado actual de ventas:</span>
                                                <div class="stat-percent">48%</div>
                                                <div class="progress progress-mini">
                                                    <div style="width: 48%;" class="progress-bar"></div>
                                                </div>
                                            </div>
                                            <div class="row  m-t-sm">
                                                <div class="col-sm-4">
                                                    <div class="font-bold">ESTRATEGIAS</div>
                                                    12
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="font-bold">RANKING</div>
                                                    4th
                                                </div>
                                                <div class="col-sm-4 text-right">
                                                    <div class="font-bold">VENTAS</div>
                                                    $200,913 <i class="fa fa-level-up text-navy"></i>
                                                </div>
                                            </div>--%>

                                            <div>
                                                <span>Estado actual de ventas:</span>
                                                <div id="lblEstadoVentas" runat="server" class="stat-percent"></div>
                                                <div class="progress progress-mini">
                                                    <div id="progressBar" runat="server" class="progress-bar"></div>
                                                </div>
                                            </div>
                                            <div class="row  m-t-sm">
                                                <div class="col-sm-4">
                                                    <div class="font-bold">ESTRATEGIAS</div>
                                                    <asp:Label ID="lblEstrategias" runat="server" />
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="font-bold">RANKING</div>
                                                    <asp:Label ID="lblRanking" runat="server" />
                                                </div>
                                                <div class="col-sm-4 text-right">
                                                    <div class="font-bold">VENTAS</div>
                                                    <asp:Label ID="lblVentas" runat="server" />
                                                    <i class="fa fa-level-up text-navy"></i>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-8">
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="ibox float-e-margins">
                                                <div class="ibox-title">
                                                    <span class="label label-warning pull-right">Datos actualizados</span>
                                                    <h5>Estados de la venta</h5>
                                                </div>

                                                <div class="ibox-content">
                                                    <div class="row">
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Leads / Caliente</small>
                                                            <h4>
                                                                <asp:Label ID="lblCalienteLeads" runat="server" /></h4>
                                                        </div>
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">% Leads</small>
                                                            <h4>
                                                                <asp:Label ID="lblCalientePorcentaje" runat="server" /></h4>
                                                        </div>
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Ventas mañana</small>
                                                            <h4>
                                                                <asp:Label ID="lblCalienteVentas" runat="server" /></h4>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="ibox-content">
                                                    <div class="row">
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Leads / Tibio</small>
                                                            <h4>
                                                                <asp:Label ID="lblTibioLeads" runat="server" /></h4>
                                                        </div>
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">% Leads</small>
                                                            <h4>
                                                                <asp:Label ID="lblTibioPorcentaje" runat="server" /></h4>
                                                        </div>
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Última semana</small>
                                                            <h4>
                                                                <asp:Label ID="lblTibioVentas" runat="server" /></h4>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="ibox-content">
                                                    <div class="row">
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Leads / Frío</small>
                                                            <h4>
                                                                <asp:Label ID="lblFrioLeads" runat="server" /></h4>
                                                        </div>
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">% Leads</small>
                                                            <h4>
                                                                <asp:Label ID="lblFrioPorcentaje" runat="server" /></h4>
                                                        </div>
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Última semana</small>
                                                            <h4>
                                                                <asp:Label ID="lblFrioVentas" runat="server" /></h4>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="ibox float-e-margins">
                                                <div class="ibox-title">
                                                    <h5>TOP 5 asesores <asp:Literal ID="ltMesActual" runat="server"></asp:Literal> </h5>
                                                    <div class="ibox-tools">
                                                        <a class="collapse-link">
                                                            <i class="fa fa-chevron-up"></i>
                                                        </a>
                                                        <a class="close-link">
                                                            <i class="fa fa-times"></i>
                                                        </a>
                                                    </div>
                                                </div>
                                                <div class="ibox-content">
                                                    <asp:Repeater ID="rptRankingAsesores" runat="server">
                                                        <HeaderTemplate>
                                                            <ul class="todo-list m-t small-list">
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <li>
                                                                <a href="#" class="check-link"><i class="fa fa-square-o"></i></a>
                                                                <span class="m-l-xs">
                                                                    <%# Container.ItemIndex + 1 %>. <%# Eval("Asesor") %> - 
                                                                    $<%# String.Format("{0:N0}", Eval("TotalVendido")) %> - 
                                                                    <%# Eval("CanalVenta") %>  (<%# Eval("CantidadPlanesVendidos") %>) Planes
                                                                </span>
                                                            </li>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </ul>
                                                        </FooterTemplate>
                                                    </asp:Repeater>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                            </div>

                            <div class="row">
                                <div class="col-lg-8">
                                    <div class="ibox float-e-margins">
                                        <div class="ibox-content">
                                            <div>
                                                <span class="pull-right text-right">
                                                    <small>Valor promedio de las ventas del ultimo mes: <strong>FP</strong></small>
                                                    <br />
                                                    Ventas totales: 162,862
                                                </span>
                                                <h3 class="font-bold no-margins">Margen de ingresos semestral
                                                </h3>
                                                <small>Marketing de ventas.</small>
                                            </div>

                                            <div class="m-t-sm">

                                                <div class="row">
                                                    <div class="col-md-8">
                                                        <div>
                                                            <canvas id="lineChart" height="114"></canvas>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <ul class="stat-list m-t-lg">
                                                            <li>
                                                                <h2 class="no-margins">2,346</h2>
                                                                <small>Solicitudes del periodo</small>
                                                                <div class="progress progress-mini">
                                                                    <div class="progress-bar" style="width: 48%;"></div>
                                                                </div>
                                                            </li>
                                                            <li>
                                                                <h2 class="no-margins ">4,422</h2>
                                                                <small>Solicitudes ultimo mes</small>
                                                                <div class="progress progress-mini">
                                                                    <div class="progress-bar" style="width: 60%;"></div>
                                                                </div>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="m-t-md">
                                                <small class="pull-right">
                                                    <i class="fa fa-clock-o"></i>
                                                    Actualizado el 03.10.2024
                                                </small>
                                                <small>
                                                    <strong>Analisis de venta:</strong> The value has been changed over time, and last month reached a level over $50,000.
                                                </small>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="widget-head-color-box navy-bg p-lg text-center">
                                        <div class="m-b-md">
                                            <h2 class="font-bold no-margins">
                                                <asp:Literal ID="ltNomAsesorMesPasado" runat="server"></asp:Literal>
                                            </h2>
                                            <small>Mejor Asesor del mes <asp:Literal ID="ltMes" runat="server"></asp:Literal></small>
                                        </div>
                                        <img src="img/a4.jpg" class="img-circle circle-border m-b-md" alt="profile">
                                        <div>
                                            <span>
                                                <asp:Literal ID="ltCanalVenta" runat="server"></asp:Literal></span> |
                                            <span>
                                                <asp:Literal ID="ltCantidadPlanes" runat="server"></asp:Literal></span> |
                                            <span>
                                                <asp:Literal ID="ltValorVendido" runat="server"></asp:Literal></span>
                                        </div>
                                    </div>
                                    <%--                        <div class="widget-text-box">
                            <h4 class="media-heading">Alex Smith</h4>
                            <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                        </div>--%>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="wrapper wrapper-content animated fadeInUp">

                                        <div class="ibox">
                                            <div class="ibox-title">
                                                <h5>All projects assigned to this account</h5>
                                                <div class="ibox-tools">
                                                    <a href="estrategiasmarketing" class="btn btn-primary btn-xs">Crear nueva estrategia</a>
                                                </div>
                                            </div>
                                            <div class="ibox-content">
                                                <div class="row m-b-sm m-t-sm">
                                                    <div class="col-md-1">
                                                        <button type="button" id="loading-example-btn" class="btn btn-white btn-sm"><i class="fa fa-refresh"></i>Refresh</button>
                                                    </div>
                                                    <div class="col-md-11">
                                                        <div class="input-group">
                                                            <input type="text" placeholder="Search" class="input-sm form-control">
                                                            <span class="input-group-btn">
                                                                <button type="button" class="btn btn-sm btn-primary">Go!</button>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="project-list">

                                                    <table class="table table-hover">
                                                        <tbody>
                                                            <tr>
                                                                <td class="project-status">
                                                                    <span class="label label-primary">Activa</span>
                                                                </td>
                                                                <td class="project-title">
                                                                    <a href="detallereportemarketing">Estrategia 1</a>
                                                                    <br />
                                                                    <small>Created 14.08.2014</small>
                                                                </td>
                                                                <td class="project-completion">
                                                                    <small><strong>Eficiencia</strong> :48%</small>
                                                                    <div class="progress progress-mini">
                                                                        <div style="width: 48%;" class="progress-bar"></div>
                                                                    </div>
                                                                </td>
                                                                <td class="project-people">
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
                                                                </td>
                                                                <td class="project-actions">
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-folder"></i>View </a>
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i>Edit </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="project-status">
                                                                    <span class="label label-primary">Activa</span>
                                                                </td>
                                                                <td class="project-title">
                                                                    <a href="project_detail.html">Estrategia 2</a>
                                                                    <br />
                                                                    <small>Created 11.08.2014</small>
                                                                </td>
                                                                <td class="project-completion">
                                                                    <small>Completion with: 28%</small>
                                                                    <div class="progress progress-mini">
                                                                        <div style="width: 28%;" class="progress-bar"></div>
                                                                    </div>
                                                                </td>
                                                                <td class="project-people">
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a7.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a6.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a3.jpg"></a>
                                                                </td>
                                                                <td class="project-actions">
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-folder"></i>View </a>
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i>Edit </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="project-status">
                                                                    <span class="label label-default">Inactiva</span>
                                                                </td>
                                                                <td class="project-title">
                                                                    <a href="project_detail.html">Estrategia 3</a>
                                                                    <br />
                                                                    <small>Created 10.08.2014</small>
                                                                </td>
                                                                <td class="project-completion">
                                                                    <small>Completion with: 8%</small>
                                                                    <div class="progress progress-mini">
                                                                        <div style="width: 8%;" class="progress-bar"></div>
                                                                    </div>
                                                                </td>
                                                                <td class="project-people">
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a5.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a3.jpg"></a>
                                                                </td>
                                                                <td class="project-actions">
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-folder"></i>View </a>
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i>Edit </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="project-status">
                                                                    <span class="label label-primary">Active</span>
                                                                </td>
                                                                <td class="project-title">
                                                                    <a href="project_detail.html">Letraset sheets containing</a>
                                                                    <br />
                                                                    <small>Created 22.07.2014</small>
                                                                </td>
                                                                <td class="project-completion">
                                                                    <small>Completion with: 83%</small>
                                                                    <div class="progress progress-mini">
                                                                        <div style="width: 83%;" class="progress-bar"></div>
                                                                    </div>
                                                                </td>
                                                                <td class="project-people">
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a2.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a3.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a1.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a7.jpg"></a>
                                                                </td>
                                                                <td class="project-actions">
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-folder"></i>View </a>
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i>Edit </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="project-status">
                                                                    <span class="label label-primary">Active</span>
                                                                </td>
                                                                <td class="project-title">
                                                                    <a href="project_detail.html">Contrary to popular belief</a>
                                                                    <br />
                                                                    <small>Created 14.07.2014</small>
                                                                </td>
                                                                <td class="project-completion">
                                                                    <small>Completion with: 97%</small>
                                                                    <div class="progress progress-mini">
                                                                        <div style="width: 97%;" class="progress-bar"></div>
                                                                    </div>
                                                                </td>
                                                                <td class="project-people">
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a4.jpg"></a>
                                                                </td>
                                                                <td class="project-actions">
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-folder"></i>View </a>
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i>Edit </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="project-status">
                                                                    <span class="label label-primary">Active</span>
                                                                </td>
                                                                <td class="project-title">
                                                                    <a href="project_detail.html">Contract with Zender Company</a>
                                                                    <br />
                                                                    <small>Created 14.08.2014</small>
                                                                </td>
                                                                <td class="project-completion">
                                                                    <small>leads comprados vs presupuesto estimado (eficiencia): 48%</small>
                                                                    <div class="progress progress-mini">
                                                                        <div style="width: 48%;" class="progress-bar"></div>
                                                                    </div>
                                                                </td>
                                                                <td class="project-people">
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a1.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a2.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a4.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a5.jpg"></a>
                                                                </td>
                                                                <td class="project-actions">
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-folder"></i>View </a>
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i>Edit </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="project-status">
                                                                    <span class="label label-primary">Active</span>
                                                                </td>
                                                                <td class="project-title">
                                                                    <a href="project_detail.html">There are many variations of passages</a>
                                                                    <br />
                                                                    <small>Created 11.08.2014</small>
                                                                </td>
                                                                <td class="project-completion">
                                                                    <small>Completion with: 28%</small>
                                                                    <div class="progress progress-mini">
                                                                        <div style="width: 28%;" class="progress-bar"></div>
                                                                    </div>
                                                                </td>
                                                                <td class="project-people">
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a7.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a6.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a3.jpg"></a>
                                                                </td>
                                                                <td class="project-actions">
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-folder"></i>View </a>
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i>Edit </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="project-status">
                                                                    <span class="label label-default">Inactiva</span>
                                                                </td>
                                                                <td class="project-title">
                                                                    <a href="project_detail.html">Many desktop publishing packages and web</a>
                                                                    <br />
                                                                    <small>Created 10.08.2014</small>
                                                                </td>
                                                                <td class="project-completion">
                                                                    <small>Completion with: 8%</small>
                                                                    <div class="progress progress-mini">
                                                                        <div style="width: 8%;" class="progress-bar"></div>
                                                                    </div>
                                                                </td>
                                                                <td class="project-people">
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a5.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a3.jpg"></a>
                                                                </td>
                                                                <td class="project-actions">
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-folder"></i>View </a>
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i>Edit </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="project-status">
                                                                    <span class="label label-primary">Active</span>
                                                                </td>
                                                                <td class="project-title">
                                                                    <a href="project_detail.html">Letraset sheets containing</a>
                                                                    <br />
                                                                    <small>Created 22.07.2014</small>
                                                                </td>
                                                                <td class="project-completion">
                                                                    <small>Completion with: 83%</small>
                                                                    <div class="progress progress-mini">
                                                                        <div style="width: 83%;" class="progress-bar"></div>
                                                                    </div>
                                                                </td>
                                                                <td class="project-people">
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a2.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a3.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a1.jpg"></a>
                                                                </td>
                                                                <td class="project-actions">
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-folder"></i>View </a>
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i>Edit </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="project-status">
                                                                    <span class="label label-primary">Active</span>
                                                                </td>
                                                                <td class="project-title">
                                                                    <a href="project_detail.html">Contrary to popular belief</a>
                                                                    <br />
                                                                    <small>Created 14.07.2014</small>
                                                                </td>
                                                                <td class="project-completion">
                                                                    <small>Completion with: 97%</small>
                                                                    <div class="progress progress-mini">
                                                                        <div style="width: 97%;" class="progress-bar"></div>
                                                                    </div>
                                                                </td>
                                                                <td class="project-people">
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a4.jpg"></a>
                                                                </td>
                                                                <td class="project-actions">
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-folder"></i>View </a>
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i>Edit </a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="project-status">
                                                                    <span class="label label-primary">Active</span>
                                                                </td>
                                                                <td class="project-title">
                                                                    <a href="project_detail.html">There are many variations of passages</a>
                                                                    <br />
                                                                    <small>Created 11.08.2014</small>
                                                                </td>
                                                                <td class="project-completion">
                                                                    <small>Completion with: 28%</small>
                                                                    <div class="progress progress-mini">
                                                                        <div style="width: 28%;" class="progress-bar"></div>
                                                                    </div>
                                                                </td>
                                                                <td class="project-people">
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a7.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a6.jpg"></a>
                                                                    <a href="">
                                                                        <img alt="image" class="img-circle" src="img/a3.jpg"></a>
                                                                </td>
                                                                <td class="project-actions">
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-folder"></i>View </a>
                                                                    <a href="#" class="btn btn-white btn-sm"><i class="fa fa-pencil"></i>Edit </a>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
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

        </div>
        <uc1:rightsidebar runat="server" ID="rightsidebar1" />
    </div>

    <!-- Mainly scripts -->
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- FooTable -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();
    </script>

    <!-- EayPIE -->
    <script src="js/plugins/easypiechart/jquery.easypiechart.js"></script>

    <!-- Sparkline -->
    <script src="js/plugins/sparkline/jquery.sparkline.min.js"></script>

    <!-- Sparkline demo data  -->
    <script src="js/demo/sparkline-demo.js"></script>

    <!-- ChartJS-->
    <script src="js/plugins/chartJs/Chart.min.js"></script>

    <!-- Toastr -->
    <script src="js/plugins/toastr/toastr.min.js"></script>

    <script>
        $(document).ready(function () {

            $('#loading-example-btn').click(function () {
                btn = $(this);
                simpleLoad(btn, true)

                // Ajax example
                //                $.ajax().always(function () {
                //                    simpleLoad($(this), false)
                //                });

                simpleLoad(btn, false)
            });
        });

        function simpleLoad(btn, state) {
            if (state) {
                btn.children().addClass('fa-spin');
                btn.contents().last().replaceWith(" Loading");
            } else {
                setTimeout(function () {
                    btn.children().removeClass('fa-spin');
                    btn.contents().last().replaceWith(" Refresh");
                }, 2000);
            }
        }
    </script>

    <script>
        $('.chart').easyPieChart({
            barColor: '#f8ac59',
            //scaleColor: false,
            scaleLength: 5,
            liNuevasidth: 4,
            size: 80
        });

        $('.chart2').easyPieChart({
            barColor: '#1c84c6',
            //scaleColor: false,
            scaleLength: 5,
            liNuevasidth: 4,
            size: 80
        });

        $(document).ready(function () {
            var lineData = {
                labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio"],
                datasets: [
                    {
                        label: "Afiliados",
                        backgroundColor: "rgba(26,179,148,0.5)",
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: [48, 48, 60, 39, 56, 37, 30]
                    },
                    {
                        label: "Empresas",
                        backgroundColor: "rgba(220,220,220,0.5)",
                        borderColor: "rgba(220,220,220,1)",
                        pointBackgroundColor: "rgba(220,220,220,1)",
                        pointBorderColor: "#fff",
                        data: [65, 59, 40, 51, 36, 25, 40]
                    }
                ]
            };

            var lineOptions = {
                responsive: true
            };


            var ctx = document.getElementById("lineChart").getContext("2d");
            new Chart(ctx, { type: 'line', data: lineData, options: lineOptions });

            setTimeout(function () {
                toastr.options = {
                    closeButton: true,
                    progressBar: true,
                    showMethod: 'slideDown',
                    timeOut: 4000
                };
                toastr.error('25 días y contando...', 'DIA ZERO');
            }, 1300);
        });
    </script>

    <!-- EayPIE -->
    <script src="js/plugins/easypiechart/jquery.easypiechart.js"></script>

    <!-- Sparkline -->
    <script src="js/plugins/sparkline/jquery.sparkline.min.js"></script>

    <!-- Sparkline demo data  -->
    <script src="js/demo/sparkline-demo.js"></script>

    <!-- ChartJS-->
    <script src="js/plugins/chartJs/Chart.min.js"></script>

    <!-- Toastr -->
    <script src="js/plugins/toastr/toastr.min.js"></script>

    <script>
        $(document).ready(function () {

            $('#loading-example-btn').click(function () {
                btn = $(this);
                simpleLoad(btn, true)

                // Ajax example
                //                $.ajax().always(function () {
                //                    simpleLoad($(this), false)
                //                });

                simpleLoad(btn, false)
            });
        });

        function simpleLoad(btn, state) {
            if (state) {
                btn.children().addClass('fa-spin');
                btn.contents().last().replaceWith(" Loading");
            } else {
                setTimeout(function () {
                    btn.children().removeClass('fa-spin');
                    btn.contents().last().replaceWith(" Refresh");
                }, 2000);
            }
        }
    </script>

    <script>
        $('.chart').easyPieChart({
            barColor: '#f8ac59',
            //scaleColor: false,
            scaleLength: 5,
            liNuevasidth: 4,
            size: 80
        });

        $('.chart2').easyPieChart({
            barColor: '#1c84c6',
            //scaleColor: false,
            scaleLength: 5,
            liNuevasidth: 4,
            size: 80
        });

        $(document).ready(function () {
            var lineData = {
                labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio"],
                datasets: [
                    {
                        label: "Afiliados",
                        backgroundColor: "rgba(26,179,148,0.5)",
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: [48, 48, 60, 39, 56, 37, 30]
                    },
                    {
                        label: "Empresas",
                        backgroundColor: "rgba(220,220,220,0.5)",
                        borderColor: "rgba(220,220,220,1)",
                        pointBackgroundColor: "rgba(220,220,220,1)",
                        pointBorderColor: "#fff",
                        data: [65, 59, 40, 51, 36, 25, 40]
                    }
                ]
            };

            var lineOptions = {
                responsive: true
            };


            var ctx = document.getElementById("lineChart").getContext("2d");
            new Chart(ctx, { type: 'line', data: lineData, options: lineOptions });

            setTimeout(function () {
                toastr.options = {
                    closeButton: true,
                    progressBar: true,
                    showMethod: 'slideDown',
                    timeOut: 4000
                };
                toastr.error('25 días y contando...', 'DIA ZERO');
            }, 1300);
        });
    </script>

</body>

</html>


