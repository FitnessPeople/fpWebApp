<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="micuenta.aspx.cs" Inherits="fpWebApp.micuenta" %>

<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Mi cuenta</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet">
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

    <!-- Sweet Alert -->
    <link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">

    <!-- Sweet alert -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element = document.querySelector("#inicio");
            element.classList.replace("old", "active");
        }
    </script>
</head>

<body onload="changeClass()">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <i class="fa fa-person-chalkboard modal-icon"></i>
                    <h4 class="modal-title">Guía para administrar mi cuenta</h4>
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
                    <h2><i class="fa fa-id-badge text-success m-r-sm"></i>Mi cuenta</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li class="active"><strong>Mi cuenta</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">
                    <%--Inicio Contenido!!!!--%>

                    <div class="row m-b-lg m-t-lg">
                        <div class="col-md-6">

                            <div class="profile-image">
                                <asp:Literal ID="ltFoto" runat="server"></asp:Literal>
                            </div>
                            <div class="profile-info">
                                <div class="">
                                    <div>
                                        <h2 class="no-margins">
                                            <asp:Literal ID="ltNombreUsuario" runat="server"></asp:Literal></h2>
                                        <h4>
                                            <asp:Literal ID="ltCargo" runat="server"></asp:Literal></h4>
                                        <small></small>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <table class="table small m-b-xs">
                                <tbody>
                                    <tr>
                                        <td>
                                            <strong>142</strong> Projects
                                        </td>
                                        <td>
                                            <strong>22</strong> Followers
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>61</strong> Comments
                                        </td>
                                        <td>
                                            <strong>54</strong> Articles
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>154</strong> Tags
                                        </td>
                                        <td>
                                            <strong>32</strong> Friends
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="col-md-3">
                            <small>Sales in last 24h</small>
                            <h2 class="no-margins">206 480</h2>
                            <div id="sparkline1"></div>
                        </div>


                    </div>

                    <%--<div class="row">

                        <div class="col-lg-3">

                            <div class="ibox">
                                <div class="ibox-content">
                                    <h3>Acerca de mi</h3>

                                    <p class="small">
                                        There are many variations of passages of Lorem Ipsum available, but the majority have
                                suffered alteration in some form, by injected humour, or randomised words which don't.
                               
                                        <br />
                                        <br />
                                        If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't
                                anything embarrassing
                           
                                    </p>

                                    <p class="small font-bold">
                                        <span><i class="fa fa-circle text-navy"></i>Online status</span>
                                    </p>

                                </div>
                            </div>

                            <div class="ibox">
                                <div class="ibox-content">
                                    <h3>Compañeros</h3>
                                    <p class="small">
                                        If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't
                                anything embarrassing
                           
                                    </p>
                                    <div class="user-friends">
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
                                        <a href="">
                                            <img alt="image" class="img-circle" src="img/a6.jpg"></a>
                                        <a href="">
                                            <img alt="image" class="img-circle" src="img/a7.jpg"></a>
                                        <a href="">
                                            <img alt="image" class="img-circle" src="img/a8.jpg"></a>
                                        <a href="">
                                            <img alt="image" class="img-circle" src="img/a2.jpg"></a>
                                        <a href="">
                                            <img alt="image" class="img-circle" src="img/a1.jpg"></a>
                                    </div>
                                </div>
                            </div>

                            <div class="ibox">
                                <div class="ibox-content">
                                    <h3>Documentos</h3>
                                    <ul class="list-unstyled file-list">
                                        <li><a href=""><i class="fa fa-file"></i>Project_document.docx</a></li>
                                        <li><a href=""><i class="fa fa-file-image"></i>Logo_zender_company.jpg</a></li>
                                        <li><a href=""><i class="fab fa-stack-exchange"></i>Email_from_Alex.mln</a></li>
                                        <li><a href=""><i class="fa fa-file"></i>Contract_20_11_2014.docx</a></li>
                                        <li><a href=""><i class="fa fa-file-powerpoint"></i>Presentation.pptx</a></li>
                                        <li><a href=""><i class="fa fa-file"></i>10_08_2015.docx</a></li>
                                    </ul>
                                </div>
                            </div>

                            <div class="ibox">
                                <div class="ibox-content">
                                    <h3>Private message</h3>

                                    <p class="small">
                                        Send private message to Alex Smith
                           
                                    </p>

                                    <div class="form-group">
                                        <label>Subject</label>
                                        <input type="email" class="form-control" placeholder="Message subject">
                                    </div>
                                    <div class="form-group">
                                        <label>Message</label>
                                        <textarea class="form-control" placeholder="Your message" rows="3"></textarea>
                                    </div>
                                    <button class="btn btn-primary btn-block">Send</button>

                                </div>
                            </div>

                        </div>

                        <div class="col-lg-5">

                            <div class="social-feed-box">

                                <div class="pull-right social-action dropdown">
                                    <button data-toggle="dropdown" class="dropdown-toggle btn-white">
                                        <i class="fa fa-angle-down"></i>
                                    </button>
                                    <ul class="dropdown-menu m-t-xs">
                                        <li><a href="#">Config</a></li>
                                    </ul>
                                </div>
                                <div class="social-avatar">
                                    <a href="" class="pull-left">
                                        <img alt="image" src="img/a1.jpg">
                                    </a>
                                    <div class="media-body">
                                        <a href="#">Andrew Williams
                                        </a>
                                        <small class="text-muted">Today 4:21 pm - 12.06.2014</small>
                                    </div>
                                </div>
                                <div class="social-body">
                                    <p>
                                        Many desktop publishing packages and web page editors now use Lorem Ipsum as their
                                default model text, and a search for 'lorem ipsum' will uncover many web sites still
                                in their infancy. Packages and web page editors now use Lorem Ipsum as their
                                default model text.
                           
                                    </p>

                                    <div class="btn-group">
                                        <button class="btn btn-white btn-xs"><i class="fa fa-thumbs-up"></i>Like this!</button>
                                        <button class="btn btn-white btn-xs"><i class="fa fa-comments"></i>Comment</button>
                                        <button class="btn btn-white btn-xs"><i class="fa fa-share"></i>Share</button>
                                    </div>
                                </div>
                                <div class="social-footer">
                                    <div class="social-comment">
                                        <a href="" class="pull-left">
                                            <img alt="image" src="img/a1.jpg">
                                        </a>
                                        <div class="media-body">
                                            <a href="#">Andrew Williams
                                            </a>
                                            Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words.
                                   
                                            <br />
                                            <a href="#" class="small"><i class="fa fa-thumbs-up"></i>26 Like this!</a> -
                                   
                                            <small class="text-muted">12.06.2014</small>
                                        </div>
                                    </div>

                                    <div class="social-comment">
                                        <a href="" class="pull-left">
                                            <img alt="image" src="img/a2.jpg">
                                        </a>
                                        <div class="media-body">
                                            <a href="#">Andrew Williams
                                            </a>
                                            Making this the first true generator on the Internet. It uses a dictionary of.
                                   
                                            <br />
                                            <a href="#" class="small"><i class="fa fa-thumbs-up"></i>11 Like this!</a> -
                                   
                                            <small class="text-muted">10.07.2014</small>
                                        </div>
                                    </div>

                                    <div class="social-comment">
                                        <a href="" class="pull-left">
                                            <img alt="image" src="img/a3.jpg">
                                        </a>
                                        <div class="media-body">
                                            <textarea class="form-control" placeholder="Write comment..."></textarea>
                                        </div>
                                    </div>

                                </div>

                            </div>

                            <div class="social-feed-box">

                                <div class="pull-right social-action dropdown">
                                    <button data-toggle="dropdown" class="dropdown-toggle btn-white">
                                        <i class="fa fa-angle-down"></i>
                                    </button>
                                    <ul class="dropdown-menu m-t-xs">
                                        <li><a href="#">Config</a></li>
                                    </ul>
                                </div>
                                <div class="social-avatar">
                                    <a href="" class="pull-left">
                                        <img alt="image" src="img/a6.jpg">
                                    </a>
                                    <div class="media-body">
                                        <a href="#">Andrew Williams
                                        </a>
                                        <small class="text-muted">Today 4:21 pm - 12.06.2014</small>
                                    </div>
                                </div>
                                <div class="social-body">
                                    <p>
                                        Many desktop publishing packages and web page editors now use Lorem Ipsum as their
                                default model text, and a search for 'lorem ipsum' will uncover many web sites still
                                in their infancy. Packages and web page editors now use Lorem Ipsum as their
                                default model text.
                           
                                    </p>
                                    <p>
                                        Lorem Ipsum as their
                                default model text, and a search for 'lorem ipsum' will uncover many web sites still
                                in their infancy. Packages and web page editors now use Lorem Ipsum as their
                                default model text.
                           
                                    </p>
                                    <img src="img/gallery/3.jpg" class="img-responsive">
                                    <div class="btn-group">
                                        <button class="btn btn-white btn-xs"><i class="fa fa-thumbs-up"></i>Like this!</button>
                                        <button class="btn btn-white btn-xs"><i class="fa fa-comments"></i>Comment</button>
                                        <button class="btn btn-white btn-xs"><i class="fa fa-share"></i>Share</button>
                                    </div>
                                </div>
                                <div class="social-footer">
                                    <div class="social-comment">
                                        <a href="" class="pull-left">
                                            <img alt="image" src="img/a1.jpg">
                                        </a>
                                        <div class="media-body">
                                            <a href="#">Andrew Williams
                                            </a>
                                            Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words.
                                   
                                            <br />
                                            <a href="#" class="small"><i class="fa fa-thumbs-up"></i>26 Like this!</a> -
                                   
                                            <small class="text-muted">12.06.2014</small>
                                        </div>
                                    </div>

                                    <div class="social-comment">
                                        <a href="" class="pull-left">
                                            <img alt="image" src="img/a2.jpg">
                                        </a>
                                        <div class="media-body">
                                            <a href="#">Andrew Williams
                                            </a>
                                            Making this the first true generator on the Internet. It uses a dictionary of.
                                   
                                            <br />
                                            <a href="#" class="small"><i class="fa fa-thumbs-up"></i>11 Like this!</a> -
                                   
                                            <small class="text-muted">10.07.2014</small>
                                        </div>
                                    </div>

                                    <div class="social-comment">
                                        <a href="" class="pull-left">
                                            <img alt="image" src="img/a8.jpg">
                                        </a>
                                        <div class="media-body">
                                            <a href="#">Andrew Williams
                                            </a>
                                            Making this the first true generator on the Internet. It uses a dictionary of.
                                   
                                            <br />
                                            <a href="#" class="small"><i class="fa fa-thumbs-up"></i>11 Like this!</a> -
                                   
                                            <small class="text-muted">10.07.2014</small>
                                        </div>
                                    </div>

                                    <div class="social-comment">
                                        <a href="" class="pull-left">
                                            <img alt="image" src="img/a3.jpg">
                                        </a>
                                        <div class="media-body">
                                            <textarea class="form-control" placeholder="Write comment..."></textarea>
                                        </div>
                                    </div>

                                </div>

                            </div>




                        </div>
                        <div class="col-lg-4 m-b-lg">
                            <div id="vertical-timeline" class="vertical-container light-timeline no-margins">
                                <div class="vertical-timeline-block">
                                    <div class="vertical-timeline-icon navy-bg">
                                        <i class="fa fa-briefcase"></i>
                                    </div>

                                    <div class="vertical-timeline-content">
                                        <h2>Meeting</h2>
                                        <p>
                                            Conference on the sales results for the previous year. Monica please examine sales trends in marketing and products. Below please find the current status of the sale.
                               
                                        </p>
                                        <a href="#" class="btn btn-sm btn-primary">More info</a>
                                        <span class="vertical-date">Today
                                            <br>
                                            <small>Dec 24</small>
                                        </span>
                                    </div>
                                </div>

                                <div class="vertical-timeline-block">
                                    <div class="vertical-timeline-icon blue-bg">
                                        <i class="fa fa-file-text"></i>
                                    </div>

                                    <div class="vertical-timeline-content">
                                        <h2>Send documents to Mike</h2>
                                        <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since.</p>
                                        <a href="#" class="btn btn-sm btn-success">Download document </a>
                                        <span class="vertical-date">Today
                                            <br>
                                            <small>Dec 24</small>
                                        </span>
                                    </div>
                                </div>

                                <div class="vertical-timeline-block">
                                    <div class="vertical-timeline-icon lazur-bg">
                                        <i class="fa fa-coffee"></i>
                                    </div>

                                    <div class="vertical-timeline-content">
                                        <h2>Coffee Break</h2>
                                        <p>Go to shop and find some products. Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's. </p>
                                        <a href="#" class="btn btn-sm btn-info">Read more</a>
                                        <span class="vertical-date">Yesterday
                                            <br>
                                            <small>Dec 23</small></span>
                                    </div>
                                </div>

                                <div class="vertical-timeline-block">
                                    <div class="vertical-timeline-icon yellow-bg">
                                        <i class="fa fa-phone"></i>
                                    </div>

                                    <div class="vertical-timeline-content">
                                        <h2>Phone with Jeronimo</h2>
                                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Iusto, optio, dolorum provident rerum aut hic quasi placeat iure tempora laudantium ipsa ad debitis unde? Iste voluptatibus minus veritatis qui ut.</p>
                                        <span class="vertical-date">Yesterday
                                            <br>
                                            <small>Dec 23</small></span>
                                    </div>
                                </div>

                                <div class="vertical-timeline-block">
                                    <div class="vertical-timeline-icon navy-bg">
                                        <i class="fa fa-comments"></i>
                                    </div>

                                    <div class="vertical-timeline-content">
                                        <h2>Chat with Monica and Sandra</h2>
                                        <p>Web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like). </p>
                                        <span class="vertical-date">Yesterday
                                            <br>
                                            <small>Dec 23</small></span>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>--%>

                    <div class="ibox float-e-margins" runat="server">
                        <div class="ibox-title">
                            <h5>Formulario para la actualización de datos
                            </h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">

                            <div class="alert alert-danger alert-dismissable" runat="server" id="divMensaje1" visible="false">
                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                Un empleado con este documento ya existe!<br />
                                <a class="alert-link" href="#">Intente nuevamente</a>.
                            </div>

                            <div class="alert alert-danger alert-dismissable" runat="server" id="divMensaje2" visible="false">
                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                Este correo ya existe.<br />
                                <a class="alert-link" href="#">Intente nuevamente</a>.
                            </div>

                            <div class="alert alert-danger alert-dismissable" runat="server" id="divMensaje3" visible="false">
                                <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                Este teléfono ya existe.<br />
                                <a class="alert-link" href="#">Intente nuevamente</a>.
                            </div>

                            <div class="row">
                                <form role="form" id="form" enctype="multipart/form-data" runat="server">
                                    <div class="col-sm-6 b-r">
                                        <div class="form-group">
                                            <label>Nombre Completo</label>
                                            <asp:TextBox ID="txbNombre" CssClass="form-control input-sm" runat="server" placeholder="Nombre completo"></asp:TextBox>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Nro. de Documento</label>
                                                    <asp:TextBox ID="txbDocumento" CssClass="form-control input-sm" runat="server" placeholder="Documento"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Tipo de Documento</label>
                                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" AppendDataBoundItems="true"
                                                        DataTextField="TipoDocumento" DataValueField="idTipoDoc" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Teléfono personal</label>
                                                    <asp:TextBox ID="txbTelefono" CssClass="form-control input-sm" runat="server" placeholder="Teléfono"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Email personal</label>
                                                    <asp:TextBox ID="txbEmail" CssClass="form-control input-sm" runat="server" placeholder="Email"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Teléfono corporativo</label>
                                                    <asp:TextBox ID="txbTelefonoCorp" CssClass="form-control input-sm" runat="server" placeholder="Teléfono corp"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Email corporativo</label>
                                                    <asp:TextBox ID="txbEmailCorp" CssClass="form-control input-sm" runat="server" placeholder="Email corp"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <label>Dirección</label>
                                                    <asp:TextBox ID="txbDireccion" CssClass="form-control input-sm" runat="server" placeholder="Dirección"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label>Ciudad</label>
                                                    <asp:DropDownList ID="ddlCiudadEmpleado" runat="server" 
                                                        AppendDataBoundItems="true" DataTextField="NombreCiudad" 
                                                        DataValueField="idCiudad" CssClass="chosen-select input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                           <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Fecha de Nacimiento</label>
                                                    <asp:TextBox ID="txbFechaNac" CssClass="form-control input-sm" runat="server" placeholder="Fecha nacimiento"></asp:TextBox>
                                                </div>
                                            </div>
                                             <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Cargo</label>
                                                    <asp:DropDownList ID="ddlCargo" runat="server" AppendDataBoundItems="true"
                                                        DataTextField="NombreCargo" DataValueField="idCargo" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                       <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label>Estado civil</label>
                                                <asp:DropDownList ID="ddlEstadoCivil" runat="server" AppendDataBoundItems="true"
                                                    DataTextField="EstadoCivil" DataValueField="idEstadoCivil" CssClass="form-control input-sm">
                                                    <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label>Género</label>
                                                <asp:DropDownList ID="ddlGenero" runat="server" AppendDataBoundItems="true"
                                                    DataTextField="Genero" DataValueField="idGenero" CssClass="form-control input-sm">
                                                    <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                        <div class="form-group">
                                            <label>Foto:</label>
                                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                                <div class="form-control input-sm" data-trigger="fileinput">
                                                    <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                    <span class="fileinput-filename"></span>
                                                </div>
                                                <span class="input-group-addon btn btn-success btn-file input-sm">
                                                    <span class="fileinput-new input-sm">Seleccionar foto</span>
                                                    <span class="fileinput-exists input-sm">Cambiar</span>
                                                    <input type="file" name="fileFoto" id="fileFoto" accept="image/*">
                                                </span>
                                                <a href="#" class="input-group-addon btn btn-danger fileinput-exists input-sm" 
                                                    data-dismiss="fileinput">Quitar</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <%--<div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Nro. de Contrato</label>
                                                    <asp:TextBox ID="txbContrato" CssClass="form-control input-sm" runat="server" placeholder="Contrato"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Tipo de Contrato</label>
                                                    <asp:DropDownList ID="ddlTipoContrato" runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="Fijo" Value="Fijo"></asp:ListItem>
                                                        <asp:ListItem Text="OPS" Value="OPS"></asp:ListItem>
                                                        <asp:ListItem Text="Aprendiz" Value="Aprendiz"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>--%>

                                        <%--<div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Fecha inicio</label>
                                                    <asp:TextBox ID="txbFechaInicio" CssClass="form-control input-sm" runat="server" placeholder="Fecha inicio"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Fecha final</label>
                                                    <asp:TextBox ID="txbFechaFinal" CssClass="form-control input-sm" runat="server" placeholder="Fecha final"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    
                                                </div>
                                            </div>
                                        </div>--%>

                                        <%--<div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Sueldo</label>
                                                    <asp:TextBox ID="txbSueldo" CssClass="form-control input-sm" runat="server" placeholder="Sueldo" onkeyup="formatCurrency(this)" onblur="keepFormatted(this)"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Grupo</label>
                                                    <asp:DropDownList ID="ddlGrupo" runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                        <asp:ListItem Text="Administrativos" Value="1 - ADMINISTRATIVOS"></asp:ListItem>
                                                        <asp:ListItem Text="Comerciales" Value="2 - COMERCIALES"></asp:ListItem>
                                                        <asp:ListItem Text="Líderes deportivos" Value="3 - LIDERES DEPORTIVOS"></asp:ListItem>
                                                        <asp:ListItem Text="Marketing Digital" Value="5 - MARKETING DIGITAL"></asp:ListItem>
                                                        <asp:ListItem Text="Fisioterapeuta y nutricionista" Value="6 - FISIOTERAPEUTA Y NUTRICIONISTA"></asp:ListItem>
                                                        <asp:ListItem Text="Profesor planta" Value="7 - PROFESOR PLANTA"></asp:ListItem>
                                                        <asp:ListItem Text="Practicantes" Value="9 - PRACTICANTES"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>--%>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>EPS</label>
                                                    <asp:DropDownList ID="ddlEps" runat="server" AppendDataBoundItems="true"
                                                        DataTextField="NombreEps" DataValueField="idEps" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Fondo de pensión</label>
                                                    <asp:DropDownList ID="ddlFondoPension" runat="server" AppendDataBoundItems="true"
                                                        DataTextField="NombreFondoPension" DataValueField="idFondoPension" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>ARL</label>
                                                    <asp:DropDownList ID="ddlArl" runat="server" AppendDataBoundItems="true"
                                                        DataTextField="NombreArl" DataValueField="idArl" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Caja de compensación</label>
                                                    <asp:DropDownList ID="ddlCajaComp" runat="server" AppendDataBoundItems="true"
                                                        DataTextField="NombreCajaComp" DataValueField="idCajaComp" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Cesantías</label>
                                                    <asp:DropDownList ID="ddlCesantias" runat="server" AppendDataBoundItems="true"
                                                        DataTextField="NombreCesantias" DataValueField="idCesantias" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <%--<div class="form-group">
                                                    <label>Estado</label>
                                                    <asp:RadioButtonList ID="rblEstado" runat="server" RepeatDirection="Horizontal" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="&nbsp;Activo&nbsp;&nbsp;&nbsp;&nbsp;" Value="Activo"></asp:ListItem>
                                                        <asp:ListItem Text="&nbsp;En pausa&nbsp;&nbsp;&nbsp;&nbsp;" Value="En pausa"></asp:ListItem>
                                                        <asp:ListItem Text="&nbsp;Inactivo" Value="Inactivo"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>--%>
                                                <label>Sede</label>
                                                <asp:DropDownList ID="ddlSedes" runat="server" AppendDataBoundItems="true"
                                                    DataTextField="NombreSede" DataValueField="idSede" CssClass="form-control input-sm">
                                                    <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <%--<div class="row">
                                              <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Canal de ventas</label>
                                                    <asp:DropDownList ID="ddlCanalVenta" runat="server" AppendDataBoundItems="true" 
                                                        DataTextField="NombreCanalVenta" DataValueField="idCanalVenta" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Empresa FP</label>
                                                    <asp:DropDownList ID="ddlEmpresasFP" runat="server" AppendDataBoundItems="true"
                                                        DataTextField="NombreEmpresaFP" DataValueField="idEmpresaFP" CssClass="form-control input-sm">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div>
                                            <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                            <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button" onclick="window.location.href='micuenta'"><strong>Cancelar</strong></button>
                                            <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right" Text="Actualizar" OnClick="btnActualizar_Click" />
                                        </div>
                                    </div>
                                </form>
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

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <script>

        $.validator.setDefaults({ ignore: ":hidden:not(.chosen-select)" });

        $("#form").validate({
            rules: {
                txbNombre: {
                    required: true,
                    minlength: 3
                },
                txbDocumento: {
                    required: true,
                    minlength: 3
                },
                ddlTipoDocumento: {
                    required: true
                },
                txbTelefono: {
                    required: true,
                    minlength: 10
                },
                txbTelefonoCorp: {
                    required: true,
                    minlength: 10
                },
                txbEmail: {
                    required: true,
                },
                txbEmailCorp: {
                    required: true,
                },
                txbDireccion: {
                    required: true,
                    minlength: 10
                },
                ddlCiudadEmpleado: {
                    required: true
                },
                txbFechaNac: {
                    required: true
                },
                ddlCargo: {
                    required: true
                },
                ddlEstadoCivil: {
                    required: true
                },
                ddlGenero: {
                    required: true
                },
                txbContrato: {
                    required: true,
                    minlength: 5
                },
                ddlTipoContrato: {
                    required: true
                },
                ddlSedes: {
                    required: true
                },
                txbFechaInicio: {
                    required: true
                },
                txbFechaFinal: {
                    required: true
                },
                txbSueldo: {
                    required: true
                },
                ddlGrupo: {
                    required: true
                },
                ddlEps: {
                    required: true
                },
                ddlFondoPension: {
                    required: true
                },
                ddlArl: {
                    required: true
                },
                ddlCajaComp: {
                    required: true
                },
                ddlCesantias: {
                    required: true
                },
                ddlEmpresasFP: {
                    required: true
                },
            },
            messages: {
                ddlCiudadEmpleado: "*",
            }
        });

        $('.chosen-select').chosen({ width: "100%", disable_search_threshold: 10, no_results_text: "Sin resultados" });
    </script>

</body>

</html>
