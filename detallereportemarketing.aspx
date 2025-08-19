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
                                <div class="col-lg-9">
                                    <div class="wrapper wrapper-content animated fadeInUp">
                                        <div class="ibox">
                                            <div class="ibox-content">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="m-b-md">
                                                            <a href="#" class="btn btn-white btn-xs pull-right">Editar estrategia</a>
                                                            <h2>Estrategia 1</h2>
                                                        </div>
                                                        <dl class="dl-horizontal">
                                                            <dt>Estado:</dt>
                                                            <dd><span class="label label-primary">Active</span></dd>
                                                        </dl>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-5">
                                                        <dl class="dl-horizontal">

                                                            <dt>Creado por:</dt>
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
                                        <h4>Descripción de la estrategia</h4>
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

</body>

</html>
