﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="autorizaciones.aspx.cs" Inherits="fpWebApp.autorizaciones" %>

<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Autorizaciones</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <!-- FooTable -->
    <%--<link href="css/plugins/footable/footable.core.css" rel="stylesheet" />--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />

    <!-- Morris -->
    <link href="css/plugins/morris/morris-0.4.3.min.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#autorizaciones");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#afiliados2");
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
                    <i class="fa fa-unlock modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para gestionar autorizaciones</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo revisar y aprobar solicitudes de cortesías, traspasos, congelaciones e incapacidades de forma clara y eficiente.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>¿Cómo funciona?</b><br />
                        El sistema muestra <b>4 tablas independientes</b>, una para cada tipo de autorización. En cada una encontrarás:<br />
                        <i class="fa fa-gift" style="color: #0D6EFD;"></i><b>Tabla de Cortesías</b><br />
                        <i class="fa fa-right-left" style="color: #F8AC59;"></i><b>Tabla de Traspasos</b><br />
                        <i class="fa fa-snowflake" style="color: #0D6EFD;"></i><b>Tabla de Congelaciones</b><br />
                        <i class="fa fa-head-side-mask" style="color: #1AB394;"></i><b>Tabla de Incapacidades</b>
                        <br />
                        <br />
                        Cada una de las tablas y registros tienen un apartado llamado <b>Acciones</b>, en donde pódrás:<br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i><b>Aprobar:</b> Autorizar si lo deseas.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i><b>Rechazar:</b> Cancelar si lo precisas.
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
                    <h2><i class="fa fa-unlock text-success m-r-sm"></i>Autorizaciones</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Afiliados</li>
                        <li class="active"><strong>Autorizaciones</strong></li>
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

                    <div class="ibox float-e-margins" runat="server" id="divContenido">
                        <div class="row">

                            <div class="col-lg-6">

                                <div class="ibox">
                                    <div class="ibox-title bg-info">
                                        <h5><i class="fa fa-gift m-r-xs"></i>Cortesías</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up text-white"></i>
                                            </a>
                                            <a class="fullscreen-link">
                                                <i class="fa fa-expand text-white"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                            data-paging="true" data-paging-count-format="{CP} de {TP}"
                                            data-empty="Sin resultados">
                                            <thead>
                                                <tr>
                                                    <th data-sortable="false">ID</th>
                                                    <th data-sortable="false">Afiliado</th>
                                                    <th data-sortable="false">Días de Cortesía</th>
                                                    <%--<th data-sortable="false">Observaciones</th>
                                                    <th data-sortable="false">Usuario</th>
                                                    <th data-sortable="false">Fecha</th>--%>
                                                    <th data-sortable="false">Acciones</th>
                                                    <th data-breakpoints="all" data-title="Info"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpCortesias" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("IdCortesia") %></td>
                                                            <td><%# Eval("NombreAfiliado") %> <%# Eval("ApellidoAfiliado") %></td>
                                                            <td><%# Eval("DiasCortesia") %> días</td>
                                                            <%--<td><%# Eval("ObservacionesCortesia") %></td>
                                                            <td><%# Eval("NombreUsuario") %></td>
                                                            <td style="vertical-align: central;">
                                                                <span class="<%# Eval("badge") %>"><%# Eval("hacecuanto") %>/15</span>
                                                                <%# Eval("FechaHoraCortesia", "{0:dd MMM yyyy}") %>
                                                            </td>--%>
                                                            <%--<td><a href="#" data-toggle="modal" data-target="#myModalRespuesta" 
                                                                data-id="<%# Eval("idCortesia") %>"><span class="label label-primary">Responder</span></a></td>--%>
                                                            <td><a href="respuestaautorizacion?idCortesia=<%# Eval("idCortesia") %>"><span class="label label-primary">Responder</span></a></td>
                                                            <td>
                                                                <table class="table table-bordered table-striped">
                                                                    <tr>
                                                                        <th width="50%"><i class="fa fa-message m-r-xs"></i>Observaciones</th>
                                                                        <th width="25%"><i class="fa fa-user m-r-xs"></i>Usuario</th>
                                                                        <th width="25%"><i class="fa fa-calendar m-r-xs"></i>Fecha</th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><%# Eval("ObservacionesCortesia") %></td>
                                                                        <td><%# Eval("NombreUsuario") %></td>
                                                                        <td style="vertical-align: central;">
                                                                            <span class="<%# Eval("badge") %>"><%# Eval("hacecuanto") %>/15</span>
                                                                            <%# Eval("FechaHoraCortesia", "{0:dd MMM yyyy}") %>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <%--<tr>
                                                    <td><small>Pending...</small> </td>
                                                    <td><i class="fa fa-clock-o"></i>12:08am</td>
                                                    <td>Damian</td>
                                                    <td class="text-navy"><i class="fa fa-level-up"></i>23% </td>
                                                </tr>--%>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-6">

                                <div class="ibox">
                                    <div class="ibox-title bg-success">
                                        <h5><i class="fa fa-right-left m-r-xs"></i>Traspasos</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up text-white"></i>
                                            </a>
                                            <a class="fullscreen-link">
                                                <i class="fa fa-expand text-white"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                            data-paging="true" data-paging-count-format="{CP} de {TP}"
                                            data-empty="Sin resultados">
                                            <thead>
                                                <tr>
                                                    <th data-sortable="false">ID</th>
                                                    <th data-sortable="false">Afiliado Origen</th>
                                                    <th data-sortable="false">Afiliado Destino</th>
                                                    <th data-sortable="false">Observaciones</th>
                                                    <th data-sortable="false">Usuario</th>
                                                    <th data-sortable="false">Fecha</th>
                                                    <th data-sortable="false">Acciones</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpTraspasos" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("idTraspaso") %></td>
                                                            <td><%# Eval("nomAfilOrigen") %></td>
                                                            <td><%# Eval("nomAfilDestino") %></td>
                                                            <td><%# Eval("Observaciones") %></td>
                                                            <td><%# Eval("NombreUsuario") %></td>
                                                            <td style="vertical-align: central;">
                                                                <span class="<%# Eval("badge") %>"><%# Eval("hacecuanto") %>/15</span>
                                                                <%# Eval("FechaTraspaso", "{0:dd MMM yyyy}") %>
                                                            </td>
                                                            <%--<td><a href="#" data-toggle="modal" data-target="#myModalRespuesta" 
                                                                data-id="<%# Eval("idCortesia") %>"><span class="label label-primary">Responder</span></a></td>--%>
                                                            <td><a href="respuestaautorizacion?idTraspaso=<%# Eval("idTraspaso") %>"><span class="label label-primary">Responder</span></a></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <%--<tr>
                                                    <td><small>Pending...</small> </td>
                                                    <td><i class="fa fa-clock-o"></i>12:08am</td>
                                                    <td>Damian</td>
                                                    <td class="text-navy"><i class="fa fa-level-up"></i>23% </td>
                                                </tr>--%>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="row">

                            <div class="col-lg-6">

                                <div class="ibox">
                                    <div class="ibox-title bg-warning">
                                        <h5><i class="fa fa-snowflake m-r-xs"></i>Congelaciones</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up text-white"></i>
                                            </a>
                                            <a class="fullscreen-link">
                                                <i class="fa fa-expand text-white"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                            data-paging="true" data-paging-count-format="{CP} de {TP}"
                                            data-empty="Sin resultados">
                                            <thead>
                                                <tr>
                                                    <th data-sortable="false">ID</th>
                                                    <th data-sortable="false">Afiliado</th>
                                                    <th data-sortable="false">Días de congelación</th>
                                                    <th data-sortable="false">Observaciones</th>
                                                    <th data-sortable="false">Usuario</th>
                                                    <th data-sortable="false">Fecha</th>
                                                    <th data-sortable="false">Acciones</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpCongelaciones" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("idCongelacion") %></td>
                                                            <td><%# Eval("NombreCompletoAfiliado") %></td>
                                                            <td><%# Eval("Dias") %></td>
                                                            <td><%# Eval("Observaciones") %></td>
                                                            <td><%# Eval("NombreUsuario") %></td>
                                                            <td style="vertical-align: central;">
                                                                <span class="<%# Eval("badge") %>"><%# Eval("hacecuanto") %>/15</span>
                                                                <%# Eval("Fecha", "{0:dd MMM yyyy}") %>
                                                            </td>
                                                            <td><a href="respuestaautorizacion?idCongelacion=<%# Eval("idCongelacion") %>"><span class="label label-primary">Responder</span></a></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <%--<tr>
                                                    <td><small>Pending...</small> </td>
                                                    <td><i class="fa fa-clock-o"></i>12:08am</td>
                                                    <td>Damian</td>
                                                    <td class="text-navy"><i class="fa fa-level-up"></i>23% </td>
                                                </tr>--%>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-6">

                                <div class="ibox">
                                    <div class="ibox-title bg-info">
                                        <h5><i class="fa fa-head-side-mask m-r-xs"></i>Incapacidades</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up text-white"></i>
                                            </a>
                                            <a class="fullscreen-link">
                                                <i class="fa fa-expand text-white"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <table class="footable table table-striped list-group-item-text" data-paging-size="10"
                                            data-paging="true" data-paging-count-format="{CP} de {TP}"
                                            data-empty="Sin resultados">
                                            <thead>
                                                <tr>
                                                    <th data-sortable="false">ID</th>
                                                    <th data-sortable="false">Afiliado</th>
                                                    <th data-sortable="false">Días de incapacidad</th>
                                                    <%--<th data-sortable="false">Observaciones</th>
                                                    <th data-sortable="false">Usuario</th>
                                                    <th data-sortable="false">Fecha</th>--%>
                                                    <th data-sortable="false">Acciones</th>
                                                    <th data-breakpoints="all" data-title="Info"></th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpIncapacidades" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("idIncapacidad") %></td>
                                                            <td><%# Eval("NombreCompletoAfiliado") %></td>
                                                            <td><%# Eval("Dias") %></td>
                                                            <%--<td><%# Eval("Observaciones") %></td>
                                                            <td><%# Eval("NombreUsuario") %></td>
                                                            <td style="vertical-align: central;">
                                                                <span class="<%# Eval("badge") %>"><%# Eval("hacecuanto") %>/15</span>
                                                                <%# Eval("Fecha", "{0:dd MMM yyyy}") %>
                                                            </td>--%>
                                                            <td><a href="respuestaautorizacion?idIncapacidad=<%# Eval("idIncapacidad") %>"><span class="label label-primary">Responder</span></a></td>
                                                            <td>
                                                                <table class="table table-bordered table-striped">
                                                                    <tr>
                                                                        <th width="50%"><i class="fa fa-message m-r-xs"></i>Observaciones</th>
                                                                        <th width="25%"><i class="fa fa-user m-r-xs"></i>Usuario</th>
                                                                        <th width="25%"><i class="fa fa-calendar m-r-xs"></i>Fecha</th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><%# Eval("Observaciones") %></td>
                                                                        <td><%# Eval("NombreUsuario") %></td>
                                                                        <td style="vertical-align: central;">
                                                                            <span class="<%# Eval("badge") %>"><%# Eval("hacecuanto") %>/15</span>
                                                                            <%# Eval("Fecha", "{0:dd MMM yyyy}") %>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <%--<tr>
                                                    <td><small>Pending...</small> </td>
                                                    <td><i class="fa fa-clock-o"></i>12:08am</td>
                                                    <td>Damian</td>
                                                    <td class="text-navy"><i class="fa fa-level-up"></i>23% </td>
                                                </tr>--%>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>

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

    <!-- Peity -->
    <script src="js/plugins/peity/jquery.peity.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- jQuery UI -->
    <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();

        $(function () {
            $("span.pie1").peity("pie", {
                fill: ['#1ab394', '#d7d7d7', '#ffffff']
            })
            $("span.pie2").peity("pie", {
                fill: ['#F8AC59', '#d7d7d7', '#ffffff']
            })
            $("span.pie3").peity("pie", {
                fill: ['#ed5565', '#d7d7d7', '#ffffff']
            })
        });

    </script>

</body>

</html>
