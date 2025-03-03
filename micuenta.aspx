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
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

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
                                        <h2 class="no-margins"><asp:Literal ID="ltNombreUsuario" runat="server"></asp:Literal></h2>
                                        <h4><asp:Literal ID="ltCargo" runat="server"></asp:Literal></h4>
                                        <small>There are many variations of passages of Lorem Ipsum available, but the majority
                                    have suffered alteration in some form Ipsum available.
                                </small>
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
                    <div class="row">

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
                                        <span><i class="fa fa-circle text-navy"></i> Online status</span>
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
                                        <li><a href=""><i class="fa fa-file"></i> Project_document.docx</a></li>
                                        <li><a href=""><i class="fa fa-file-image"></i> Logo_zender_company.jpg</a></li>
                                        <li><a href=""><i class="fab fa-stack-exchange"></i> Email_from_Alex.mln</a></li>
                                        <li><a href=""><i class="fa fa-file"></i> Contract_20_11_2014.docx</a></li>
                                        <li><a href=""><i class="fa fa-file-powerpoint"></i> Presentation.pptx</a></li>
                                        <li><a href=""><i class="fa fa-file"></i> 10_08_2015.docx</a></li>
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