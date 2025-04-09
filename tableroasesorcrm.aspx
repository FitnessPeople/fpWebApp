<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tableroasesorcrm.aspx.cs" Inherits="fpWebApp.tableroasesorcrm" %>

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

    <title>Fitness People | Plantilla</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#usuarios");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#configuracion");
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
                    <i class="fa fa-person-chalkboard modal-icon"></i>
                    <h4 class="modal-title">Guía para editar un especialista</h4>
                    <small class="font-bold">¡Bienvenido! A continuación, te ofrecemos una guía sencilla para ayudarte a completar el formulario de manera correcta y eficiente. Sigue estos pasos para asegurarte de que toda la información se registre de forma adecuada.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>1. Lee las Instrucciones</b><br />
                        Antes de comenzar, es importante que leas todas las instrucciones del formulario. Esto te ayudará a entender qué información se requiere y cómo debe ser presentada.
                        <br />
                        <br />
                        <b>2. Reúne la Información Necesaria</b><br />
                        Asegúrate de tener a mano todos los documentos e información que necesitas, como:
                        Datos personales (nombre, dirección, número de teléfono, etc.)
                        Información específica relacionada con el propósito del formulario (por ejemplo, detalles de empleo, historial médico, etc.)
                        <br />
                        <br />
                        <b>3. Completa los Campos Requeridos</b><br />
                        Campos Obligatorios: Identifica cuáles son los campos obligatorios (generalmente marcados con un asterisco *) y asegúrate de completarlos.
                        Campos Opcionales: Si hay campos opcionales, completa solo los que consideres relevantes.
                        <br />
                        <br />
                        <b>4. Confirma la Información</b><br />
                        Asegúrate de que todos los datos ingresados son correctos y actualizados. Una revisión final puede evitar errores que podrían complicar el proceso.
                        <br />
                        <br />
                        <b>5. Envía el Formulario</b><br />
                        Asegúrate de seguir el proceso de envío indicado (hacer clic en "Agregar").
                        <br />
                        <br />
                        ¡Siguiendo estos pasos, estarás listo para diligenciar tu formulario sin problemas! Si tienes dudas, no dudes en consultar con el administrador del sistema.
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
                    <h2><i class="fa fa-user-tie text-success m-r-sm"></i>Dashboard</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li class="active"><strong>Dashboard</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">
                    <%--Inicio Contenido!!!!--%>

                    <div class="row">
                        <div class="col-lg-4">
                            <div class="ibox">
                                <div class="ibox-content">
                                    <h3>To-do</h3>
                                    <p class="small"><i class="fa fa-hand-o-up"></i>Drag task between list</p>

                                    <div class="input-group">
                                        <input type="text" placeholder="Add new task. " class="input input-sm form-control">
                                        <span class="input-group-btn">
                                            <button type="button" class="btn btn-sm btn-white"><i class="fa fa-plus"></i>Add task</button>
                                        </span>
                                    </div>

                                    <ul class="sortable-list connectList agile-list" id="todo">
                                        <li class="warning-element" id="task1">Simply dummy text of the printing and typesetting industry.
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Tag</a>
                                                <i class="fa fa-clock-o"></i>12.10.2015
                                   
                                            </div>
                                        </li>
                                        <li class="success-element" id="task2">Many desktop publishing packages and web page editors now use Lorem Ipsum as their default.
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Mark</a>
                                                <i class="fa fa-clock-o"></i>05.04.2015
                                   
                                            </div>
                                        </li>
                                        <li class="info-element" id="task3">Sometimes by accident, sometimes on purpose (injected humour and the like).
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Mark</a>
                                                <i class="fa fa-clock-o"></i>16.11.2015
                                   
                                            </div>
                                        </li>
                                        <li class="danger-element" id="task4">All the Lorem Ipsum generators
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-primary">Done</a>
                                                <i class="fa fa-clock-o"></i>06.10.2015
                                   
                                            </div>
                                        </li>
                                        <li class="warning-element" id="task5">Which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Tag</a>
                                                <i class="fa fa-clock-o"></i>09.12.2015
                                   
                                            </div>
                                        </li>
                                        <li class="warning-element" id="task6">Packages and web page editors now use Lorem Ipsum as
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-primary">Done</a>
                                                <i class="fa fa-clock-o"></i>08.04.2015
                                   
                                            </div>
                                        </li>
                                        <li class="success-element" id="task7">Many desktop publishing packages and web page editors now use Lorem Ipsum as their default.
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Mark</a>
                                                <i class="fa fa-clock-o"></i>05.04.2015
                                   
                                            </div>
                                        </li>
                                        <li class="info-element" id="task8">Sometimes by accident, sometimes on purpose (injected humour and the like).
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Mark</a>
                                                <i class="fa fa-clock-o"></i>16.11.2015
                                   
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="ibox">
                                <div class="ibox-content">
                                    <h3>In Progress</h3>
                                    <p class="small"><i class="fa fa-hand-o-up"></i>Drag task between list</p>
                                    <ul class="sortable-list connectList agile-list" id="inprogress">
                                        <li class="success-element" id="task9">Quisque venenatis ante in porta suscipit.
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Tag</a>
                                                <i class="fa fa-clock-o"></i>12.10.2015
                                   
                                            </div>
                                        </li>
                                        <li class="success-element" id="task10">Phasellus sit amet tortor sed enim mollis accumsan in consequat orci.
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Mark</a>
                                                <i class="fa fa-clock-o"></i>05.04.2015
                                   
                                            </div>
                                        </li>
                                        <li class="warning-element" id="task11">Nunc sed arcu at ligula faucibus tempus ac id felis. Vestibulum et nulla quis turpis sagittis fringilla.
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Mark</a>
                                                <i class="fa fa-clock-o"></i>16.11.2015
                                   
                                            </div>
                                        </li>
                                        <li class="warning-element" id="task12">Ut porttitor augue non sapien mollis accumsan.
                                    Nulla non elit eget lacus elementum viverra.
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Tag</a>
                                                <i class="fa fa-clock-o"></i>09.12.2015
                                   
                                            </div>
                                        </li>
                                        <li class="info-element" id="task13">Packages and web page editors now use Lorem Ipsum as
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-primary">Done</a>
                                                <i class="fa fa-clock-o"></i>08.04.2015
                                   
                                            </div>
                                        </li>
                                        <li class="success-element" id="task14">Quisque lacinia tellus et odio ornare maximus.
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Mark</a>
                                                <i class="fa fa-clock-o"></i>05.04.2015
                                   
                                            </div>
                                        </li>
                                        <li class="danger-element" id="task15">Enim mollis accumsan in consequat orci.
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Mark</a>
                                                <i class="fa fa-clock-o"></i>11.04.2015
                                   
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="ibox">
                                <div class="ibox-content">
                                    <h3>Completed</h3>
                                    <p class="small"><i class="fa fa-hand-o-up"></i>Drag task between list</p>
                                    <ul class="sortable-list connectList agile-list" id="completed">
                                        <li class="info-element" id="task16">Sometimes by accident, sometimes on purpose (injected humour and the like).
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Mark</a>
                                                <i class="fa fa-clock-o"></i>16.11.2015
                                   
                                            </div>
                                        </li>
                                        <li class="warning-element" id="task17">Ut porttitor augue non sapien mollis accumsan.
                                    Nulla non elit eget lacus elementum viverra.
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Tag</a>
                                                <i class="fa fa-clock-o"></i>09.12.2015
                                   
                                            </div>
                                        </li>
                                        <li class="warning-element" id="task18">Which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Tag</a>
                                                <i class="fa fa-clock-o"></i>09.12.2015
                                   
                                            </div>
                                        </li>
                                        <li class="warning-element" id="task19">Packages and web page editors now use Lorem Ipsum as
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-primary">Done</a>
                                                <i class="fa fa-clock-o"></i>08.04.2015
                                   
                                            </div>
                                        </li>
                                        <li class="success-element" id="task20">Many desktop publishing packages and web page editors now use Lorem Ipsum as their default.
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Mark</a>
                                                <i class="fa fa-clock-o"></i>05.04.2015
                                   
                                            </div>
                                        </li>
                                        <li class="info-element" id="task21">Sometimes by accident, sometimes on purpose (injected humour and the like).
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Mark</a>
                                                <i class="fa fa-clock-o"></i>16.11.2015
                                   
                                            </div>
                                        </li>
                                        <li class="warning-element" id="task22">Simply dummy text of the printing and typesetting industry.
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Tag</a>
                                                <i class="fa fa-clock-o"></i>12.10.2015
                                   
                                            </div>
                                        </li>
                                        <li class="success-element" id="task23">Many desktop publishing packages and web page editors now use Lorem Ipsum as their default.
                                   
                                            <div class="agile-detail">
                                                <a href="#" class="pull-right btn btn-xs btn-white">Mark</a>
                                                <i class="fa fa-clock-o"></i>05.04.2015
                                   
                                            </div>
                                        </li>
                                    </ul>
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
    <script src="js/plugins/footable/footable.all.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <!-- Page-Level Scripts -->

</body>

</html>
