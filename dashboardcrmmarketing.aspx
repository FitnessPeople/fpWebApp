<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboardcrmmarketing.aspx.cs" Inherits="fpWebApp.dashboardcrmmarketing" %>


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
            var element1 = document.querySelector("#sedes");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#sistema");
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
                        <li class="active"><strong>Reporte estrategia</strong></li>
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
                                            <span class="label label-primary pull-right">NEW</span>
                                            <h5>Team Online</h5>
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
                                                “Enfocados en resultados, el Team Online ha logrado posicionarse en el Top 5, 
                                                impulsando 12 estrategias activas con una excelente gestión de recursos.”
                                            </p>
                                            <div>
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
                                                    <div class="font-bold">PRESUPUESTO</div>
                                                    $200,913 <i class="fa fa-level-up text-navy"></i>
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
                                                            <h4>236</h4>
                                                        </div>

                                                        <div class="col-xs-4">
                                                            <small class="stats-label">% Leads</small>
                                                            <h4>46.11%</h4>
                                                        </div>
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Ventas mañana</small>
                                                            <h4>$432.021</h4>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="ibox-content">
                                                    <div class="row">
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Leads / Tibio</small>
                                                            <h4>64</h4>
                                                        </div>

                                                        <div class="col-xs-4">
                                                            <small class="stats-label">% Leads</small>
                                                            <h4>92.43%</h4>
                                                        </div>
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Última semana</small>
                                                            <h4>$564.554</h4>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="ibox-content">
                                                    <div class="row">
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Leads / Frío</small>
                                                            <h4>36</h4>
                                                        </div>

                                                        <div class="col-xs-4">
                                                            <small class="stats-label">% Leads</small>
                                                            <h4>150.23%</h4>
                                                        </div>
                                                        <div class="col-xs-4">
                                                            <small class="stats-label">Última semana</small>
                                                            <h4>$124.990</h4>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="ibox float-e-margins">
                                                <div class="ibox-title">
                                                    <h5>Ranking de asesores</h5>
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
                                                    <ul class="todo-list m-t small-list">
                                                        <li>
                                                            <a href="#" class="check-link"><i class="fa fa-square-o"></i></a>
                                                            <span class="m-l-xs">1. Sara Uribe.</span>

                                                        </li>
                                                        <li>
                                                            <a href="#" class="check-link"><i class="fa fa-square-o"></i></a>
                                                            <span class="m-l-xs">2. Adrian Cubaque</span>                                                            
                                                        </li>
                                                        <li>
                                                            <a href="#" class="check-link"><i class="fa fa-square-o"></i></a>
                                                            <span class="m-l-xs">3. Kendy Leal</span>
                                                        </li>
                                                        <li>
                                                            <a href="#" class="check-link"><i class="fa fa-check-o"></i></a>
                                                            <span class="m-l-xs">4. Karina Coronel</span>
                                                        </li>
                                                        <li>
                                                            <a href="#" class="check-link"><i class="fa fa-square-o"></i></a>
                                                            <span class="m-l-xs">5. Javier Galvan</span>
                                                        </li>
                                                    </ul>
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
                               Alex Smith
                            </h2>
                                <small> Mejor Asesor del mes</small>
                            </div>
                            <img src="img/a4.jpg" class="img-circle circle-border m-b-md" alt="profile">
                            <div>
                                <span>Equipo online</span> |
                                <span>Cantidad de leads 600</span> |
                                <span>Valor vendido $61.000.000</span>
                            </div>
                        </div>
<%--                        <div class="widget-text-box">
                            <h4 class="media-heading">Alex Smith</h4>
                            <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                        </div>--%>
                </div>
                            </div>
                        </div>
                    </form>

                    <div class="row">
                        <div class="col-lg-9">
                            <div class="wrapper wrapper-content animated fadeInUp">
                                <div class="ibox">
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="m-b-md">
                                                    <a href="#" class="btn btn-white btn-xs pull-right">Edit project</a>
                                                    <h2>Contract with Zender Company</h2>
                                                </div>
                                                <dl class="dl-horizontal">
                                                    <dt>Status:</dt>
                                                    <dd><span class="label label-primary">Active</span></dd>
                                                </dl>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-5">
                                                <dl class="dl-horizontal">

                                                    <dt>Created by:</dt>
                                                    <dd>Alex Smith</dd>
                                                    <dt>Messages:</dt>
                                                    <dd>162</dd>
                                                    <dt>Client:</dt>
                                                    <dd><a href="#" class="text-navy">Zender Company</a> </dd>
                                                    <dt>Version:</dt>
                                                    <dd>v1.4.2 </dd>
                                                </dl>
                                            </div>
                                            <div class="col-lg-7" id="cluster_info">
                                                <dl class="dl-horizontal">

                                                    <dt>Last Updated:</dt>
                                                    <dd>16.08.2014 12:15:57</dd>
                                                    <dt>Created:</dt>
                                                    <dd>10.07.2014 23:36:57 </dd>
                                                    <dt>Participants:</dt>
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
                                                    <dt>Completed:</dt>
                                                    <dd>
                                                        <div class="progress progress-striped active m-b-sm">
                                                            <div style="width: 60%;" class="progress-bar"></div>
                                                        </div>
                                                        <small>Project completed in <strong>60%</strong>. Remaining close the project, sign a contract and invoice.</small>
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
                                                                <li class="active"><a href="#tab-1" data-toggle="tab">Users messages</a></li>
                                                                <li class=""><a href="#tab-2" data-toggle="tab">Last activity</a></li>
                                                            </ul>
                                                        </div>
                                                    </div>

                                                    <div class="panel-body">

                                                        <div class="tab-content">
                                                            <div class="tab-pane active" id="tab-1">
                                                                <div class="feed-activity-list">
                                                                    <div class="feed-element">
                                                                        <a href="#" class="pull-left">
                                                                            <img alt="image" class="img-circle" src="img/a2.jpg">
                                                                        </a>
                                                                        <div class="media-body ">
                                                                            <small class="pull-right">2h ago</small>
                                                                            <strong>Mark Johnson</strong> posted message on <strong>Monica Smith</strong> site.
                                                                            <br>
                                                                            <small class="text-muted">Today 2:10 pm - 12.06.2014</small>
                                                                            <div class="well">
                                                                                Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.
                                                    Over the years, sometimes by accident, sometimes on purpose (injected humour and the like).
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="feed-element">
                                                                        <a href="#" class="pull-left">
                                                                            <img alt="image" class="img-circle" src="img/a3.jpg">
                                                                        </a>
                                                                        <div class="media-body ">
                                                                            <small class="pull-right">2h ago</small>
                                                                            <strong>Janet Rosowski</strong> add 1 photo on <strong>Monica Smith</strong>.
                                                                            <br>
                                                                            <small class="text-muted">2 days ago at 8:30am</small>
                                                                        </div>
                                                                    </div>
                                                                    <div class="feed-element">
                                                                        <a href="#" class="pull-left">
                                                                            <img alt="image" class="img-circle" src="img/a4.jpg">
                                                                        </a>
                                                                        <div class="media-body ">
                                                                            <small class="pull-right text-navy">5h ago</small>
                                                                            <strong>Chris Johnatan Overtunk</strong> started following <strong>Monica Smith</strong>.
                                                                            <br>
                                                                            <small class="text-muted">Yesterday 1:21 pm - 11.06.2014</small>
                                                                            <div class="actions">
                                                                                <a class="btn btn-xs btn-white"><i class="fa fa-thumbs-up"></i>Like </a>
                                                                                <a class="btn btn-xs btn-white"><i class="fa fa-heart"></i>Love</a>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="feed-element">
                                                                        <a href="#" class="pull-left">
                                                                            <img alt="image" class="img-circle" src="img/a5.jpg">
                                                                        </a>
                                                                        <div class="media-body ">
                                                                            <small class="pull-right">2h ago</small>
                                                                            <strong>Kim Smith</strong> posted message on <strong>Monica Smith</strong> site.
                                                                            <br>
                                                                            <small class="text-muted">Yesterday 5:20 pm - 12.06.2014</small>
                                                                            <div class="well">
                                                                                Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.
                                                    Over the years, sometimes by accident, sometimes on purpose (injected humour and the like).
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="feed-element">
                                                                        <a href="#" class="pull-left">
                                                                            <img alt="image" class="img-circle" src="img/profile.jpg">
                                                                        </a>
                                                                        <div class="media-body ">
                                                                            <small class="pull-right">23h ago</small>
                                                                            <strong>Monica Smith</strong> love <strong>Kim Smith</strong>.
                                                                            <br>
                                                                            <small class="text-muted">2 days ago at 2:30 am - 11.06.2014</small>
                                                                        </div>
                                                                    </div>
                                                                    <div class="feed-element">
                                                                        <a href="#" class="pull-left">
                                                                            <img alt="image" class="img-circle" src="img/a7.jpg">
                                                                        </a>
                                                                        <div class="media-body ">
                                                                            <small class="pull-right">46h ago</small>
                                                                            <strong>Mike Loreipsum</strong> started following <strong>Monica Smith</strong>.
                                                                            <br>
                                                                            <small class="text-muted">3 days ago at 7:58 pm - 10.06.2014</small>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="tab-pane" id="tab-2">

                                                                <table class="table table-striped">
                                                                    <thead>
                                                                        <tr>
                                                                            <th>Status</th>
                                                                            <th>Title</th>
                                                                            <th>Start Time</th>
                                                                            <th>End Time</th>
                                                                            <th>Comments</th>
                                                                        </tr>
                                                                    </thead>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="label label-primary"><i class="fa fa-check"></i>Completed</span>
                                                                            </td>
                                                                            <td>Create project in webapp
                                                                            </td>
                                                                            <td>12.07.2014 10:10:1
                                                                            </td>
                                                                            <td>14.07.2014 10:16:36
                                                                            </td>
                                                                            <td>
                                                                                <p class="small">
                                                                                    Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable.
                                                                                </p>
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="label label-primary"><i class="fa fa-check"></i>Accepted</span>
                                                                            </td>
                                                                            <td>Various versions
                                                                            </td>
                                                                            <td>12.07.2014 10:10:1
                                                                            </td>
                                                                            <td>14.07.2014 10:16:36
                                                                            </td>
                                                                            <td>
                                                                                <p class="small">
                                                                                    Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).
                                                                                </p>
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="label label-primary"><i class="fa fa-check"></i>Sent</span>
                                                                            </td>
                                                                            <td>There are many variations
                                                                            </td>
                                                                            <td>12.07.2014 10:10:1
                                                                            </td>
                                                                            <td>14.07.2014 10:16:36
                                                                            </td>
                                                                            <td>
                                                                                <p class="small">
                                                                                    There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which
                                                                                </p>
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="label label-primary"><i class="fa fa-check"></i>Reported</span>
                                                                            </td>
                                                                            <td>Latin words
                                                                            </td>
                                                                            <td>12.07.2014 10:10:1
                                                                            </td>
                                                                            <td>14.07.2014 10:16:36
                                                                            </td>
                                                                            <td>
                                                                                <p class="small">
                                                                                    Latin words, combined with a handful of model sentence structures
                                                                                </p>
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="label label-primary"><i class="fa fa-check"></i>Accepted</span>
                                                                            </td>
                                                                            <td>The generated Lorem
                                                                            </td>
                                                                            <td>12.07.2014 10:10:1
                                                                            </td>
                                                                            <td>14.07.2014 10:16:36
                                                                            </td>
                                                                            <td>
                                                                                <p class="small">
                                                                                    The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.
                                                                                </p>
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="label label-primary"><i class="fa fa-check"></i>Sent</span>
                                                                            </td>
                                                                            <td>The first line
                                                                            </td>
                                                                            <td>12.07.2014 10:10:1
                                                                            </td>
                                                                            <td>14.07.2014 10:16:36
                                                                            </td>
                                                                            <td>
                                                                                <p class="small">
                                                                                    The first line of Lorem Ipsum, "Lorem ipsum dolor sit amet..", comes from a line in section 1.10.32.
                                                                                </p>
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="label label-primary"><i class="fa fa-check"></i>Reported</span>
                                                                            </td>
                                                                            <td>The standard chunk
                                                                            </td>
                                                                            <td>12.07.2014 10:10:1
                                                                            </td>
                                                                            <td>14.07.2014 10:16:36
                                                                            </td>
                                                                            <td>
                                                                                <p class="small">
                                                                                    The standard chunk of Lorem Ipsum used since the 1500s is reproduced below for those interested.
                                                                                </p>
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="label label-primary"><i class="fa fa-check"></i>Completed</span>
                                                                            </td>
                                                                            <td>Lorem Ipsum is that
                                                                            </td>
                                                                            <td>12.07.2014 10:10:1
                                                                            </td>
                                                                            <td>14.07.2014 10:16:36
                                                                            </td>
                                                                            <td>
                                                                                <p class="small">
                                                                                    Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable.
                                                                                </p>
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <span class="label label-primary"><i class="fa fa-check"></i>Sent</span>
                                                                            </td>
                                                                            <td>Contrary to popular
                                                                            </td>
                                                                            <td>12.07.2014 10:10:1
                                                                            </td>
                                                                            <td>14.07.2014 10:16:36
                                                                            </td>
                                                                            <td>
                                                                                <p class="small">
                                                                                    Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical
                                                                                </p>
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
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="wrapper wrapper-content project-manager">
                                <h4>Project description</h4>
                                <img src="img/zender_logo.png" class="img-responsive">
                                <p class="small">
                                    There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look
                        even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing
                                </p>
                                <p class="small font-bold">
                                    <span><i class="fa fa-circle text-warning"></i>High priority</span>
                                </p>
                                <h5>Project tag</h5>
                                <ul class="tag-list" style="padding: 0">
                                    <li><a href=""><i class="fa fa-tag"></i>Zender</a></li>
                                    <li><a href=""><i class="fa fa-tag"></i>Lorem ipsum</a></li>
                                    <li><a href=""><i class="fa fa-tag"></i>Passages</a></li>
                                    <li><a href=""><i class="fa fa-tag"></i>Variations</a></li>
                                </ul>
                                <h5>Project files</h5>
                                <ul class="list-unstyled project-files">
                                    <li><a href=""><i class="fa fa-file"></i>Project_document.docx</a></li>
                                    <li><a href=""><i class="fa fa-file-picture-o"></i>Logo_zender_company.jpg</a></li>
                                    <li><a href=""><i class="fa fa-stack-exchange"></i>Email_from_Alex.mln</a></li>
                                    <li><a href=""><i class="fa fa-file"></i>Contract_20_11_2014.docx</a></li>
                                </ul>
                                <div class="text-center m-t-md">
                                    <a href="#" class="btn btn-xs btn-primary">Add files</a>
                                    <a href="#" class="btn btn-xs btn-primary">Report contact</a>

                                </div>
                            </div>
                        </div>
                    </div>
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

</body>

</html>




