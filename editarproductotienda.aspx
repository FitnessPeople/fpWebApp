<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editarproductotienda.aspx.cs" Inherits="fpWebApp.editarproductotienda" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Editar producto</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="css/animate.css" rel="stylesheet">
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet">
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">

    <link href="css/plugins/slick/slick.css" rel="stylesheet">
    <link href="css/plugins/slick/slick-theme.css" rel="stylesheet">

    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#productos");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#webpage");
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
                    <h4 class="modal-title">Guía para crear un nuevo afiliado</h4>
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

        <uc1:navbar runat="server" ID="navbar1" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header1" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa fa-tag text-success m-r-sm"></i>Editar producto</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Página web</li>
                        <li class="active"><strong>Nuevo producto</strong></li>
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

                    <div class="row">
                        <div class="col-lg-12">

                            <div class="ibox product-detail">
                                <div class="ibox-content">

                                    <div class="row">
                                        <div class="col-md-5">


                                            <div class="product-images">

                                                <div>
                                                    <div class="image-imitation" style="padding: initial">
                                                        <img src="img/productos/course_1.jpg" width="100%" />
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="image-imitation" style="padding: initial">
                                                        <img src="img/productos/course_2.jpg" width="100%" />
                                                    </div>
                                                </div>
                                                <div>
                                                    <div class="image-imitation" style="padding: initial">
                                                        <img src="img/productos/course_3.jpg" width="100%" />
                                                    </div>
                                                </div>


                                            </div>

                                        </div>
                                        <div class="col-md-7">

                                            <h2 class="font-bold m-b-xs">Desktop publishing software
                                    </h2>
                                            <small>Many desktop publishing packages and web page editors now.</small>
                                            <div class="m-t-md">
                                                <h2 class="product-main-price">$406,602 <small class="text-muted">Exclude Tax</small> </h2>
                                            </div>
                                            <hr>

                                            <h4>Product description</h4>

                                            <div class="small text-muted">
                                                It is a long established fact that a reader will be distracted by the readable
                                        content of a page when looking at its layout. The point of using Lorem Ipsum is

                                       

                                                <br />
                                                <br />
                                                There are many variations of passages of Lorem Ipsum available, but the majority
                                        have suffered alteration in some form, by injected humour, or randomised words
                                        which don't look even slightly believable.
                                   
                                            </div>
                                            <dl class="small m-t-md">
                                                <dt>Description lists</dt>
                                                <dd>A description list is perfect for defining terms.</dd>
                                                <dt>Euismod</dt>
                                                <dd>Vestibulum id ligula porta felis euismod semper eget lacinia odio sem nec elit.</dd>
                                                <dd>Donec id elit non mi porta gravida at eget metus.</dd>
                                                <dt>Malesuada porta</dt>
                                                <dd>Etiam porta sem malesuada magna mollis euismod.</dd>
                                            </dl>
                                            <hr>

                                            <div>
                                                <div class="btn-group">
                                                    <button class="btn btn-primary btn-sm"><i class="fa fa-cart-plus"></i>Add to cart</button>
                                                    <button class="btn btn-white btn-sm"><i class="fa fa-star"></i>Add to wishlist </button>
                                                    <button class="btn btn-white btn-sm"><i class="fa fa-envelope"></i>Contact with author </button>
                                                </div>
                                            </div>



                                        </div>
                                    </div>

                                </div>
                                <div class="ibox-footer">
                                    <span class="pull-right">Full stock - <i class="fa fa-clock-o"></i>14.04.2016 10:04 pm
                            </span>
                                    The generated Lorem Ipsum is therefore always free
                       
                                </div>
                            </div>

                        </div>
                    </div>

                    <div class="ibox float-e-margins" runat="server" id="divContenido">
                        <div class="ibox-title">
                            <h5>Formulario para la creación de un nuevo producto</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>

                            <div class="row">
                                <form role="form" enctype="multipart/form-data" runat="server" id="form1">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <div class="col-sm-6 b-r">
                                        <div class="row">
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <label>Nombre del producto:</label>
                                                    <asp:TextBox ID="txbNombre" CssClass="form-control input-sm" runat="server"
                                                        placeholder="Nombre del producto" required></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label>Código:</label>
                                                    <asp:TextBox ID="txbCodigo" CssClass="form-control input-sm" runat="server"
                                                        placeholder="Código"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <label>Categoría:</label>
                                                    <asp:DropDownList ID="ddlCategorias" runat="server"
                                                        AppendDataBoundItems="true" DataTextField="NombreCat"
                                                        DataValueField="idCategoria" CssClass="form-control input-sm m-b">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label>Precio público:</label>
                                                    <asp:TextBox ID="txbPrecio" CssClass="form-control input-sm" runat="server" placeholder="Precio"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label>Detalle:</label>
                                                    <asp:TextBox ID="txbDetalle" CssClass="form-control input-sm" runat="server" placeholder="Detalle" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label>Descripción:</label>
                                                    <asp:TextBox ID="txbDescripcion" CssClass="form-control input-sm" runat="server" placeholder="Descripción" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label>Beneficios:</label>
                                                    <asp:TextBox ID="txbBeneficios" CssClass="form-control input-sm" runat="server" placeholder="Beneficios" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label>Características:</label>
                                                    <asp:TextBox ID="txbCaracteristicas" CssClass="form-control input-sm" runat="server" placeholder="Características" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-6">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Imagen 1:</label>
                                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 150px; max-height: 50px;"></div>
                                                                <div class="form-control input-sm" data-trigger="fileinput">
                                                                    <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                                    <span class="fileinput-filename"></span>
                                                                </div>
                                                                <span class="input-group-addon btn btn-success btn-file input-sm">
                                                                    <span class="fileinput-new input-sm">Seleccionar imagen 1</span>
                                                                    <span class="fileinput-exists input-sm">Cambiar</span>
                                                                    <asp:FileUpload ID="imgInp1" runat="server" />
                                                                </span>
                                                                <span class="input-group-addon btn btn-danger fileinput-exists input-sm" data-dismiss="fileinput">Quitar</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Imagen 2:</label>
                                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 150px; max-height: 50px;"></div>
                                                                <div class="form-control input-sm" data-trigger="fileinput">
                                                                    <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                                    <span class="fileinput-filename"></span>
                                                                </div>
                                                                <span class="input-group-addon btn btn-success btn-file input-sm">
                                                                    <span class="fileinput-new input-sm">Seleccionar imagen 2</span>
                                                                    <span class="fileinput-exists input-sm">Cambiar</span>
                                                                    <asp:FileUpload ID="imgInp2" runat="server" />
                                                                </span>
                                                                <span class="input-group-addon btn btn-danger fileinput-exists input-sm" data-dismiss="fileinput">Quitar</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Imagen 3:</label>
                                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 150px; max-height: 50px;"></div>
                                                                <div class="form-control input-sm" data-trigger="fileinput">
                                                                    <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                                    <span class="fileinput-filename"></span>
                                                                </div>
                                                                <span class="input-group-addon btn btn-success btn-file input-sm">
                                                                    <span class="fileinput-new input-sm">Seleccionar imagen 3</span>
                                                                    <span class="fileinput-exists input-sm">Cambiar</span>
                                                                    <asp:FileUpload ID="imgInp3" runat="server" />
                                                                </span>
                                                                <span class="input-group-addon btn btn-danger fileinput-exists input-sm" data-dismiss="fileinput">Quitar</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Imagen 4:</label>
                                                            <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 150px; max-height: 50px;"></div>
                                                                <div class="form-control input-sm" data-trigger="fileinput">
                                                                    <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                                    <span class="fileinput-filename"></span>
                                                                </div>
                                                                <span class="input-group-addon btn btn-success btn-file input-sm">
                                                                    <span class="fileinput-new input-sm">Seleccionar imagen 4</span>
                                                                    <span class="fileinput-exists input-sm">Cambiar</span>
                                                                    <asp:FileUpload ID="imgInp4" runat="server" />
                                                                </span>
                                                                <span class="input-group-addon btn btn-danger fileinput-exists input-sm" data-dismiss="fileinput">Quitar</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div>
                                                    <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button" onclick="window.location.href='afiliados'"><strong>Cancelar</strong></button>
                                                    <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right" Text="Agregar" OnClick="btnAgregar_Click" />
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnAgregar" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </form>
                            </div>

                            <%--Fin Contenido!!!!--%>
                        </div>
                    </div>

                </div>
            </div>

            <uc1:footer runat="server" ID="footer1" />

        </div>
        <uc1:rightsidebar runat="server" ID="rightsidebar1" />
    </div>

    <!-- Mainly scripts -->
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>


    <!-- slick carousel-->
    <script src="js/plugins/slick/slick.min.js"></script>

    <script>

        $.validator.setDefaults({ ignore: ":hidden:not(.chosen-select)" })

        $("#form1").validate({
            rules: {
                txbNombre: {
                    required: true,
                    minlength: 3
                },
                txbCodigo: {
                    required: true,
                    minlength: 3
                },
                txbPrecio: {
                    required: true
                },
                txbDetalle: {
                    required: true,
                    minlength: 3
                },
                txbDescripcion: {
                    required: true,
                    minlength: 3
                },
                txbBeneficios: {
                    required: true,
                    minlength: 3
                },
                txbCaracteristicas: {
                    required: true,
                    minlength: 3
                },
            },
            messages: {
                ddlCategorias: "*",
            }
        });

        $('.chosen-select').chosen({ width: "100%", disable_search_threshold: 10, no_results_text: "Sin resultados" });

        $(document).ready(function () {


            $('.product-images').slick({
                dots: true
            });

        });
    </script>

</body>

</html>
