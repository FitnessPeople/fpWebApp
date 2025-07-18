<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="activosfijos.aspx.cs" Inherits="fpWebApp.activosfijos" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>
<%@ Register Src="~/controles/indicadores04.ascx" TagPrefix="uc1" TagName="indicadores04" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Tickets soporte</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/dataTables/datatables.min.css" rel="stylesheet" />
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />

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
            var element1 = document.querySelector("#activosfijos");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#mantenimiento");
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
                    <i class="fa fa-users modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para visualizar los usuarios registrados</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar el listado de forma rápida y sencilla.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Busca y filtra</b><br />
                        Usa el buscador para encontrar usuarios específicos.<br />
                        <i class="fa-solid fa-magnifying-glass"></i>Filtra por: 
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i><b>Nombre</b>, 
                        <i class="fa-solid fa-user-tie" style="color: #0D6EFD;"></i><b>Empleado</b>, 
                        <i class="fa-solid fa-envelope" style="color: #0D6EFD;"></i><b>Correo</b>, 
                        <i class="fa-solid fa-user-shield" style="color: #0D6EFD;"></i><b>Perfil</b> o
                        <i class="fa-solid fa-circle" style="color: #0D6EFD;"></i><b>Estado</b><br />
                        <i class="fa-solid fa-star" style="color: #FECE32;"></i>Tip: ¡Combina filtros para resultados más precisos!
                    <br />
                        <br />
                        <b>Paso 2: Revisa la tabla de resultados</b><br />
                        La tabla muestra toda la información clave de cada usuario.<br />
                        En la columna "Acciones" encontrarás estas opciones:<br />
                        <i class="fa fa-edit" style="color: #1AB394;"></i><b>Editar:</b> Modifica los datos necesarios.<br />
                        <i class="fa fa-trash" style="color: #DC3545;"></i><b>Eliminar:</b> Borra lo que creas innecesario.
                    <br />
                        <br />
                        <b>Paso 3: Acciones adicionales</b><br />
                        Al lado opuesto del buscador encontrarás dos botones útiles:<br />
                        <i class="fa-solid fa-file-export" style="color: #212529;"></i><b>Exportar a Excel:</b>
                        Genera un archivo Excel con los datos visibles en la tabla.<br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i><b>Crear Nuevo Usuario:</b>
                        Te lleva a un formulario para registrar un nuevo usuario.
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
                    <h2><i class="fas fa-dumbbell text-success m-r-sm"></i>Activos fijos</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Sistema</li>
                        <li class="active"><strong>Activos fijos</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">

                    <uc1:indicadores04 runat="server" ID="indicadores04" />

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

                    <div class="row" id="divContenido" runat="server">
                        <div class="col-sm-8">
                            <div class="ibox">
                                <div class="ibox-content">
                                    <span class="text-muted small pull-right">Last modification: <i class="fa fa-clock-o"></i>2:10 pm - 12.06.2014</span>
                                    <h2>Activos fijos</h2>
                                    <p>
                                        All clients need to be verified before you can send email and set a project.
                           
                                    </p>
                                    <div class="input-group">
                                        <input type="text" placeholder="Search client " class="input form-control">
                                        <span class="input-group-btn">
                                            <button type="button" class="btn btn btn-primary"><i class="fa fa-search"></i>Search</button>
                                        </span>
                                    </div>
                                    <div class="clients-list">
                                        <ul class="nav nav-tabs">
                                            <span class="pull-right small text-muted">1406 Elements</span>
                                            <li class="active"><a data-toggle="tab" href="#tab-1"><i class="fa fa-user"></i>Contacts</a></li>
                                            <li class=""><a data-toggle="tab" href="#tab-2"><i class="fa fa-briefcase"></i>Companies</a></li>
                                        </ul>
                                        <div class="tab-content">
                                            <div id="tab-1" class="tab-pane active">
                                                <div class="full-height-scroll">
                                                    <div class="table-responsive">
                                                        <table class="table table-striped table-hover">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="client-avatar">
                                                                        <img alt="image" src="img/a2.jpg">
                                                                    </td>
                                                                    <td><a data-toggle="tab" href="#contact-1" class="client-link">Anthony Jackson</a></td>
                                                                    <td>Tellus Institute</td>
                                                                    <td class="contact-type"><i class="fa fa-envelope"></i></td>
                                                                    <td>gravida@rbisit.com</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar">
                                                                        <img alt="image" src="img/a3.jpg">
                                                                    </td>
                                                                    <td><a data-toggle="tab" href="#contact-2" class="client-link">Rooney Lindsay</a></td>
                                                                    <td>Proin Limited</td>
                                                                    <td class="contact-type"><i class="fa fa-envelope"></i></td>
                                                                    <td>rooney@proin.com</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar">
                                                                        <img alt="image" src="img/a4.jpg">
                                                                    </td>
                                                                    <td><a data-toggle="tab" href="#contact-3" class="client-link">Lionel Mcmillan</a></td>
                                                                    <td>Et Industries</td>
                                                                    <td class="contact-type"><i class="fa fa-phone"></i></td>
                                                                    <td>+432 955 908</td>
                                                                    <td class="client-status"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a5.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-4" class="client-link">Edan Randall</a></td>
                                                                    <td>Integer Sem Corp.</td>
                                                                    <td class="contact-type"><i class="fa fa-phone"></i></td>
                                                                    <td>+422 600 213</td>
                                                                    <td class="client-status"><span class="label label-warning">Waiting</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a6.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-2" class="client-link">Jasper Carson</a></td>
                                                                    <td>Mone Industries</td>
                                                                    <td class="contact-type"><i class="fa fa-phone"></i></td>
                                                                    <td>+400 468 921</td>
                                                                    <td class="client-status"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a7.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-3" class="client-link">Reuben Pacheco</a></td>
                                                                    <td>Magna Associates</td>
                                                                    <td class="contact-type"><i class="fa fa-envelope"></i></td>
                                                                    <td>pacheco@manga.com</td>
                                                                    <td class="client-status"><span class="label label-info">Phoned</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a1.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-1" class="client-link">Simon Carson</a></td>
                                                                    <td>Erat Corp.</td>
                                                                    <td class="contact-type"><i class="fa fa-envelope"></i></td>
                                                                    <td>Simon@erta.com</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a3.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-2" class="client-link">Rooney Lindsay</a></td>
                                                                    <td>Proin Limited</td>
                                                                    <td class="contact-type"><i class="fa fa-envelope"></i></td>
                                                                    <td>rooney@proin.com</td>
                                                                    <td class="client-status"><span class="label label-warning">Waiting</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a4.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-3" class="client-link">Lionel Mcmillan</a></td>
                                                                    <td>Et Industries</td>
                                                                    <td class="contact-type"><i class="fa fa-phone"></i></td>
                                                                    <td>+432 955 908</td>
                                                                    <td class="client-status"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a5.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-4" class="client-link">Edan Randall</a></td>
                                                                    <td>Integer Sem Corp.</td>
                                                                    <td class="contact-type"><i class="fa fa-phone"></i></td>
                                                                    <td>+422 600 213</td>
                                                                    <td class="client-status"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a2.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-1" class="client-link">Anthony Jackson</a></td>
                                                                    <td>Tellus Institute</td>
                                                                    <td class="contact-type"><i class="fa fa-envelope"></i></td>
                                                                    <td>gravida@rbisit.com</td>
                                                                    <td class="client-status"><span class="label label-danger">Deleted</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a7.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-2" class="client-link">Reuben Pacheco</a></td>
                                                                    <td>Magna Associates</td>
                                                                    <td class="contact-type"><i class="fa fa-envelope"></i></td>
                                                                    <td>pacheco@manga.com</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a5.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-3" class="client-link">Edan Randall</a></td>
                                                                    <td>Integer Sem Corp.</td>
                                                                    <td class="contact-type"><i class="fa fa-phone"></i></td>
                                                                    <td>+422 600 213</td>
                                                                    <td class="client-status"><span class="label label-info">Phoned</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a6.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-4" class="client-link">Jasper Carson</a></td>
                                                                    <td>Mone Industries</td>
                                                                    <td class="contact-type"><i class="fa fa-phone"></i></td>
                                                                    <td>+400 468 921</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a7.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-2" class="client-link">Reuben Pacheco</a></td>
                                                                    <td>Magna Associates</td>
                                                                    <td class="contact-type"><i class="fa fa-envelope"></i></td>
                                                                    <td>pacheco@manga.com</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a1.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-1" class="client-link">Simon Carson</a></td>
                                                                    <td>Erat Corp.</td>
                                                                    <td class="contact-type"><i class="fa fa-envelope"></i></td>
                                                                    <td>Simon@erta.com</td>
                                                                    <td class="client-status"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a3.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-3" class="client-link">Rooney Lindsay</a></td>
                                                                    <td>Proin Limited</td>
                                                                    <td class="contact-type"><i class="fa fa-envelope"></i></td>
                                                                    <td>rooney@proin.com</td>
                                                                    <td class="client-status"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a4.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-4" class="client-link">Lionel Mcmillan</a></td>
                                                                    <td>Et Industries</td>
                                                                    <td class="contact-type"><i class="fa fa-phone"></i></td>
                                                                    <td>+432 955 908</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a5.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-1" class="client-link">Edan Randall</a></td>
                                                                    <td>Integer Sem Corp.</td>
                                                                    <td class="contact-type"><i class="fa fa-phone"></i></td>
                                                                    <td>+422 600 213</td>
                                                                    <td class="client-status"><span class="label label-info">Phoned</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a2.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-2" class="client-link">Anthony Jackson</a></td>
                                                                    <td>Tellus Institute</td>
                                                                    <td class="contact-type"><i class="fa fa-envelope"></i></td>
                                                                    <td>gravida@rbisit.com</td>
                                                                    <td class="client-status"><span class="label label-warning">Waiting</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="client-avatar"><a href="">
                                                                        <img alt="image" src="img/a7.jpg"></a> </td>
                                                                    <td><a data-toggle="tab" href="#contact-4" class="client-link">Reuben Pacheco</a></td>
                                                                    <td>Magna Associates</td>
                                                                    <td class="contact-type"><i class="fa fa-envelope"></i></td>
                                                                    <td>pacheco@manga.com</td>
                                                                    <td class="client-status"></td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="tab-2" class="tab-pane">
                                                <div class="full-height-scroll">
                                                    <div class="table-responsive">
                                                        <table class="table table-striped table-hover">
                                                            <tbody>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-1" class="client-link">Tellus Institute</a></td>
                                                                    <td>Rexton</td>
                                                                    <td><i class="fa fa-flag"></i>Angola</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-2" class="client-link">Velit Industries</a></td>
                                                                    <td>Maglie</td>
                                                                    <td><i class="fa fa-flag"></i>Luxembourg</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-3" class="client-link">Art Limited</a></td>
                                                                    <td>Sooke</td>
                                                                    <td><i class="fa fa-flag"></i>Philippines</td>
                                                                    <td class="client-status"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-1" class="client-link">Tempor Arcu Corp.</a></td>
                                                                    <td>Eisden</td>
                                                                    <td><i class="fa fa-flag"></i>Korea, North</td>
                                                                    <td class="client-status"><span class="label label-warning">Waiting</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-2" class="client-link">Penatibus Consulting</a></td>
                                                                    <td>Tribogna</td>
                                                                    <td><i class="fa fa-flag"></i>Montserrat</td>
                                                                    <td class="client-status"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-3" class="client-link">Ultrices Incorporated</a></td>
                                                                    <td>Basingstoke</td>
                                                                    <td><i class="fa fa-flag"></i>Tunisia</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-2" class="client-link">Et Arcu Inc.</a></td>
                                                                    <td>Sioux City</td>
                                                                    <td><i class="fa fa-flag"></i>Burundi</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-1" class="client-link">Tellus Institute</a></td>
                                                                    <td>Rexton</td>
                                                                    <td><i class="fa fa-flag"></i>Angola</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-2" class="client-link">Velit Industries</a></td>
                                                                    <td>Maglie</td>
                                                                    <td><i class="fa fa-flag"></i>Luxembourg</td>
                                                                    <td class="client-status"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-3" class="client-link">Art Limited</a></td>
                                                                    <td>Sooke</td>
                                                                    <td><i class="fa fa-flag"></i>Philippines</td>
                                                                    <td class="client-status"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-1" class="client-link">Tempor Arcu Corp.</a></td>
                                                                    <td>Eisden</td>
                                                                    <td><i class="fa fa-flag"></i>Korea, North</td>
                                                                    <td class="client-status"><span class="label label-warning">Waiting</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-2" class="client-link">Penatibus Consulting</a></td>
                                                                    <td>Tribogna</td>
                                                                    <td><i class="fa fa-flag"></i>Montserrat</td>
                                                                    <td class="client-status"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-3" class="client-link">Ultrices Incorporated</a></td>
                                                                    <td>Basingstoke</td>
                                                                    <td><i class="fa fa-flag"></i>Tunisia</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-2" class="client-link">Et Arcu Inc.</a></td>
                                                                    <td>Sioux City</td>
                                                                    <td><i class="fa fa-flag"></i>Burundi</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-1" class="client-link">Tellus Institute</a></td>
                                                                    <td>Rexton</td>
                                                                    <td><i class="fa fa-flag"></i>Angola</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-2" class="client-link">Velit Industries</a></td>
                                                                    <td>Maglie</td>
                                                                    <td><i class="fa fa-flag"></i>Luxembourg</td>
                                                                    <td class="client-status"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-3" class="client-link">Art Limited</a></td>
                                                                    <td>Sooke</td>
                                                                    <td><i class="fa fa-flag"></i>Philippines</td>
                                                                    <td class="client-status"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-1" class="client-link">Tempor Arcu Corp.</a></td>
                                                                    <td>Eisden</td>
                                                                    <td><i class="fa fa-flag"></i>Korea, North</td>
                                                                    <td class="client-status"><span class="label label-warning">Waiting</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-2" class="client-link">Penatibus Consulting</a></td>
                                                                    <td>Tribogna</td>
                                                                    <td><i class="fa fa-flag"></i>Montserrat</td>
                                                                    <td class="client-status"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-3" class="client-link">Ultrices Incorporated</a></td>
                                                                    <td>Basingstoke</td>
                                                                    <td><i class="fa fa-flag"></i>Tunisia</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
                                                                </tr>
                                                                <tr>
                                                                    <td><a data-toggle="tab" href="#company-2" class="client-link">Et Arcu Inc.</a></td>
                                                                    <td>Sioux City</td>
                                                                    <td><i class="fa fa-flag"></i>Burundi</td>
                                                                    <td class="client-status"><span class="label label-primary">Active</span></td>
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
                        <div class="col-sm-4">
                            <div class="ibox ">

                                <div class="ibox-content">
                                    <div class="tab-content">
                                        <div id="contact-1" class="tab-pane active">
                                            <div class="row m-b-lg">
                                                <div class="col-lg-4 text-center">
                                                    <h2>Caminadora</h2>

                                                    <div class="m-b-sm">
                                                        <img alt="image" class="img-circle" src="img/activos/caminadora_cybex.jpg"
                                                            style="width: 62px">
                                                    </div>
                                                </div>
                                                <div class="col-lg-8">
                                                    <strong>FPCB-C-01
                                            </strong>

                                                    <p>
                                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod
                                                tempor incididunt ut labore et dolore magna aliqua.
                                           
                                                    </p>
                                                    <button type="button" class="btn btn-primary btn-sm btn-block">
                                                        <i
                                                            class="fa fa-layer-group"></i> Boton
                                           
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="client-detail">
                                                <div class="full-height-scroll">

                                                    <strong>Last activity</strong>

                                                    <ul class="list-group clear-list">
                                                        <li class="list-group-item fist-item">
                                                            <span class="pull-right">09:00 pm </span>
                                                            Please contact me
                                            </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">10:16 am </span>
                                                            Sign a contract
                                            </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">08:22 pm </span>
                                                            Open new shop
                                            </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">11:06 pm </span>
                                                            Call back to Sylvia
                                            </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">12:00 am </span>
                                                            Write a letter to Sandra
                                            </li>
                                                    </ul>
                                                    <strong>Notes</strong>
                                                    <p>
                                                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod
                                            tempor incididunt ut labore et dolore magna aliqua.
                                       
                                                    </p>
                                                    <hr />
                                                    <strong>Timeline activity</strong>
                                                    <div id="vertical-timeline" class="vertical-container dark-timeline">
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                   
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                   
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-bolt"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    There are many variations of passages of Lorem Ipsum available.
                                                   
                                                                </p>
                                                                <span class="vertical-date small text-muted">06:10 pm - 11.03.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon navy-bg">
                                                                <i class="fa fa-warning"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    The generated Lorem Ipsum is therefore.
                                                   
                                                                </p>
                                                                <span class="vertical-date small text-muted">02:50 pm - 03.10.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                   
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                   
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="contact-2" class="tab-pane">
                                            <div class="row m-b-lg">
                                                <div class="col-lg-4 text-center">
                                                    <h2>Edan Randall</h2>

                                                    <div class="m-b-sm">
                                                        <img alt="image" class="img-circle" src="img/a3.jpg"
                                                            style="width: 62px">
                                                    </div>
                                                </div>
                                                <div class="col-lg-8">
                                                    <strong>About me
                                            </strong>

                                                    <p>
                                                        Many desktop publishing packages and web page editors now use Lorem Ipsum as their default tempor incididunt model text.
                                           
                                                    </p>
                                                    <button type="button" class="btn btn-primary btn-sm btn-block">
                                                        <i
                                                            class="fa fa-envelope"></i>Send Message
                                           
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="client-detail">
                                                <div class="full-height-scroll">

                                                    <strong>Last activity</strong>

                                                    <ul class="list-group clear-list">
                                                        <li class="list-group-item fist-item">
                                                            <span class="pull-right">09:00 pm </span>
                                                            Lorem Ipsum available
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">10:16 am </span>
                                                            Latin words, combined
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">08:22 pm </span>
                                                            Open new shop
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">11:06 pm </span>
                                                            The generated Lorem Ipsum
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">12:00 am </span>
                                                            Content here, content here
                                                </li>
                                                    </ul>
                                                    <strong>Notes</strong>
                                                    <p>
                                                        There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words.
                                           
                                                    </p>
                                                    <hr />
                                                    <strong>Timeline activity</strong>
                                                    <div id="vertical-timeline" class="vertical-container dark-timeline">
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-bolt"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    There are many variations of passages of Lorem Ipsum available.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">06:10 pm - 11.03.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon navy-bg">
                                                                <i class="fa fa-warning"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    The generated Lorem Ipsum is therefore.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">02:50 pm - 03.10.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="contact-3" class="tab-pane">
                                            <div class="row m-b-lg">
                                                <div class="col-lg-4 text-center">
                                                    <h2>Jasper Carson</h2>

                                                    <div class="m-b-sm">
                                                        <img alt="image" class="img-circle" src="img/a4.jpg"
                                                            style="width: 62px">
                                                    </div>
                                                </div>
                                                <div class="col-lg-8">
                                                    <strong>About me
                                            </strong>

                                                    <p>
                                                        Latin professor at Hampden-Sydney College in Virginia, looked  embarrassing hidden in the middle.
                                           
                                                    </p>
                                                    <button type="button" class="btn btn-primary btn-sm btn-block">
                                                        <i
                                                            class="fa fa-envelope"></i>Send Message
                                           
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="client-detail">
                                                <div class="full-height-scroll">

                                                    <strong>Last activity</strong>

                                                    <ul class="list-group clear-list">
                                                        <li class="list-group-item fist-item">
                                                            <span class="pull-right">09:00 pm </span>
                                                            Aldus PageMaker including
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">10:16 am </span>
                                                            Finibus Bonorum et Malorum
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">08:22 pm </span>
                                                            Write a letter to Sandra
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">11:06 pm </span>
                                                            Standard chunk of Lorem
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">12:00 am </span>
                                                            Open new shop
                                                </li>
                                                    </ul>
                                                    <strong>Notes</strong>
                                                    <p>
                                                        Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source.
                                           
                                                    </p>
                                                    <hr />
                                                    <strong>Timeline activity</strong>
                                                    <div id="vertical-timeline" class="vertical-container dark-timeline">
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-bolt"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    There are many variations of passages of Lorem Ipsum available.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">06:10 pm - 11.03.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon navy-bg">
                                                                <i class="fa fa-warning"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    The generated Lorem Ipsum is therefore.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">02:50 pm - 03.10.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="contact-4" class="tab-pane">
                                            <div class="row m-b-lg">
                                                <div class="col-lg-4 text-center">
                                                    <h2>Reuben Pacheco</h2>

                                                    <div class="m-b-sm">
                                                        <img alt="image" class="img-circle" src="img/a5.jpg"
                                                            style="width: 62px">
                                                    </div>
                                                </div>
                                                <div class="col-lg-8">
                                                    <strong>About me
                                            </strong>

                                                    <p>
                                                        Finibus Bonorum et Malorum" (The Extremes of Good and Evil) by Cicero,written in 45 BC. This book is a treatise on.
                                           
                                                    </p>
                                                    <button type="button" class="btn btn-primary btn-sm btn-block">
                                                        <i
                                                            class="fa fa-envelope"></i>Send Message
                                           
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="client-detail">
                                                <div class="full-height-scroll">

                                                    <strong>Last activity</strong>

                                                    <ul class="list-group clear-list">
                                                        <li class="list-group-item fist-item">
                                                            <span class="pull-right">09:00 pm </span>
                                                            The point of using
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">10:16 am </span>
                                                            Lorem Ipsum is that it has
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">08:22 pm </span>
                                                            Text, and a search for 'lorem ipsum'
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">11:06 pm </span>
                                                            Passages of Lorem Ipsum
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right">12:00 am </span>
                                                            If you are going
                                                </li>
                                                    </ul>
                                                    <strong>Notes</strong>
                                                    <p>
                                                        Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.
                                           
                                                    </p>
                                                    <hr />
                                                    <strong>Timeline activity</strong>
                                                    <div id="vertical-timeline" class="vertical-container dark-timeline">
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-bolt"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    There are many variations of passages of Lorem Ipsum available.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">06:10 pm - 11.03.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon navy-bg">
                                                                <i class="fa fa-warning"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    The generated Lorem Ipsum is therefore.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">02:50 pm - 03.10.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="company-1" class="tab-pane">
                                            <div class="m-b-lg">
                                                <h2>Tellus Institute</h2>

                                                <p>
                                                    Finibus Bonorum et Malorum" (The Extremes of Good and Evil) by Cicero,written in 45 BC. This book is a treatise on.
                                           
                                                </p>
                                                <div>
                                                    <small>Active project completion with: 48%</small>
                                                    <div class="progress progress-mini">
                                                        <div style="width: 48%;" class="progress-bar"></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="client-detail">
                                                <div class="full-height-scroll">

                                                    <strong>Last activity</strong>

                                                    <ul class="list-group clear-list">
                                                        <li class="list-group-item fist-item">
                                                            <span class="pull-right"><span class="label label-primary">NEW</span> </span>
                                                            The point of using
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right"><span class="label label-warning">WAITING</span></span>
                                                            Lorem Ipsum is that it has
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right"><span class="label label-danger">BLOCKED</span> </span>
                                                            If you are going
                                                </li>
                                                    </ul>
                                                    <strong>Notes</strong>
                                                    <p>
                                                        Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.
                                           
                                                    </p>
                                                    <hr />
                                                    <strong>Timeline activity</strong>
                                                    <div id="vertical-timeline" class="vertical-container dark-timeline">
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-bolt"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    There are many variations of passages of Lorem Ipsum available.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">06:10 pm - 11.03.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon navy-bg">
                                                                <i class="fa fa-warning"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    The generated Lorem Ipsum is therefore.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">02:50 pm - 03.10.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="company-2" class="tab-pane">
                                            <div class="m-b-lg">
                                                <h2>Penatibus Consulting</h2>

                                                <p>
                                                    There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some.
                                       
                                                </p>
                                                <div>
                                                    <small>Active project completion with: 22%</small>
                                                    <div class="progress progress-mini">
                                                        <div style="width: 22%;" class="progress-bar"></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="client-detail">
                                                <div class="full-height-scroll">

                                                    <strong>Last activity</strong>

                                                    <ul class="list-group clear-list">
                                                        <li class="list-group-item fist-item">
                                                            <span class="pull-right"><span class="label label-warning">WAITING</span> </span>
                                                            Aldus PageMaker
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right"><span class="label label-primary">NEW</span> </span>
                                                            Lorem Ipsum, you need to be sure
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right"><span class="label label-danger">BLOCKED</span> </span>
                                                            The generated Lorem Ipsum
                                                </li>
                                                    </ul>
                                                    <strong>Notes</strong>
                                                    <p>
                                                        Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.
                                           
                                                    </p>
                                                    <hr />
                                                    <strong>Timeline activity</strong>
                                                    <div id="vertical-timeline" class="vertical-container dark-timeline">
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-bolt"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    There are many variations of passages of Lorem Ipsum available.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">06:10 pm - 11.03.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon navy-bg">
                                                                <i class="fa fa-warning"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    The generated Lorem Ipsum is therefore.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">02:50 pm - 03.10.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="company-3" class="tab-pane">
                                            <div class="m-b-lg">
                                                <h2>Ultrices Incorporated</h2>

                                                <p>
                                                    Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text.
                                       
                                                </p>
                                                <div>
                                                    <small>Active project completion with: 72%</small>
                                                    <div class="progress progress-mini">
                                                        <div style="width: 72%;" class="progress-bar"></div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="client-detail">
                                                <div class="full-height-scroll">

                                                    <strong>Last activity</strong>

                                                    <ul class="list-group clear-list">
                                                        <li class="list-group-item fist-item">
                                                            <span class="pull-right"><span class="label label-danger">BLOCKED</span> </span>
                                                            Hidden in the middle of text
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right"><span class="label label-primary">NEW</span> </span>
                                                            Non-characteristic words etc.
                                                </li>
                                                        <li class="list-group-item">
                                                            <span class="pull-right"><span class="label label-warning">WAITING</span> </span>
                                                            Bonorum et Malorum
                                                </li>
                                                    </ul>
                                                    <strong>Notes</strong>
                                                    <p>
                                                        There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour.
                                           
                                                    </p>
                                                    <hr />
                                                    <strong>Timeline activity</strong>
                                                    <div id="vertical-timeline" class="vertical-container dark-timeline">
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-bolt"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    There are many variations of passages of Lorem Ipsum available.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">06:10 pm - 11.03.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon navy-bg">
                                                                <i class="fa fa-warning"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    The generated Lorem Ipsum is therefore.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">02:50 pm - 03.10.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-coffee"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Conference on the sales results for the previous year.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">2:10 pm - 12.06.2014 </span>
                                                            </div>
                                                        </div>
                                                        <div class="vertical-timeline-block">
                                                            <div class="vertical-timeline-icon gray-bg">
                                                                <i class="fa fa-briefcase"></i>
                                                            </div>
                                                            <div class="vertical-timeline-content">
                                                                <p>
                                                                    Many desktop publishing packages and web page editors now use Lorem.
                                                       
                                                                </p>
                                                                <span class="vertical-date small text-muted">4:20 pm - 10.05.2014 </span>
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
                    </div>
            </div>
        </div>

        <uc1:footer runat="server" ID="footer1" />

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

</body>

</html>
