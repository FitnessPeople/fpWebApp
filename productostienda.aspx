﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productostienda.aspx.cs" Inherits="fpWebApp.productostienda" %>

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

    <title>Fitness People | Productos tienda</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- Toastr style -->
    <link href="css/plugins/toastr/toastr.min.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

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
                    <h2><i class="fa fa-store text-success m-r-sm"></i>Productos tienda</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Página web</li>
                        <li class="active"><strong>Productos tienda</strong></li>
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

                    <div runat="server" id="divContenido">
                        <div class="row">

                            <asp:Repeater ID="rpProductos" runat="server">
                                <ItemTemplate>
                                    <div class="col-md-3">
                                        <div class="ibox">
                                            <div class="ibox-content product-box">

                                                <div>
                                                    <img src="img/productos/<%# Eval("Imagen1Prod") %>" class="img-responsive" />
                                                </div>
                                                <div class="product-desc" style="padding: 20px;">
                                                    <span class="product-price" style="background-color: #1ab394;">$ <%# Eval("PrecioPublicoProd", "{0:N0}") %>
                                                    </span>
                                                    <small class="text-muted"><%# Eval("NombreCat") %></small>
                                                    <small class="text-muted">(<%# Eval("Stock") %> unidades en inventario)</small>
                                                    <a href="#" class="product-name"><%# Eval("NombreProd") %></a>

                                                    <div class="small m-t-xs">
                                                        <%# Eval("DescripcionProd") %>
                                                    </div>
                                                    <div class="m-t text-righ">
                                                        <a href="#" class="btn btn-xs btn-outline btn-primary">Ver <i class="fa fa-magnifying-glass"></i></a>
                                                        <a href="editarproductotienda?id=<%# Eval("idProducto") %>" class="btn btn-xs btn-outline btn-success">Editar <i class="fa fa-edit"></i></a>
                                                        <a href="#" class="btn btn-xs btn-outline btn-danger">Borrar <i class="fa fa-trash"></i></a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <%--<div class="col-md-3">
                                <div class="ibox">
                                    <div class="ibox-content product-box">

                                        <div>
                                            <img src="img/gallery/11.jpg" class="img-responsive" />
                                        </div>
                                        <div class="product-desc" style="padding: 20px;">
                                            <span class="product-price" style="background-color: #1ab394;">$10
                                            </span>
                                            <small class="text-muted">Categoría</small>
                                            <a href="#" class="product-name">Nombre del producto</a>



                                            <div class="small m-t-xs">
                                                Descripción del producto
                                            </div>
                                            <div class="m-t text-righ">

                                                <a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="ibox">
                                    <div class="ibox-content product-box">

                                        <div>
                                            <img src="img/gallery/2.jpg" class="img-responsive" />
                                        </div>
                                        <div class="product-desc" style="padding: 20px;">
                                            <span class="product-price" style="background-color: #1ab394;">$10
                                            </span>
                                            <small class="text-muted">Categoría</small>
                                            <a href="#" class="product-name">Nombre del producto</a>



                                            <div class="small m-t-xs">
                                                Descripción del producto
                                            </div>
                                            <div class="m-t text-righ">

                                                <a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="ibox">
                                    <div class="ibox-content product-box">

                                        <div>
                                            <img src="img/gallery/3.jpg" class="img-responsive" />
                                        </div>
                                        <div class="product-desc" style="padding: 20px;">
                                            <span class="product-price" style="background-color: #1ab394;">$10
                                            </span>
                                            <small class="text-muted">Categoría</small>
                                            <a href="#" class="product-name">Nombre del producto</a>



                                            <div class="small m-t-xs">
                                                Descripción del producto
                                            </div>
                                            <div class="m-t text-righ">

                                                <a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="ibox">
                                    <div class="ibox-content product-box">

                                        <div>
                                            <img src="img/gallery/5.jpg" class="img-responsive" />
                                        </div>
                                        <div class="product-desc" style="padding: 20px;">
                                            <span class="product-price" style="background-color: #1ab394;">$10
                                            </span>
                                            <small class="text-muted">Categoría</small>
                                            <a href="#" class="product-name">Nombre del producto</a>



                                            <div class="small m-t-xs">
                                                Descripción del producto
                                            </div>
                                            <div class="m-t text-righ">

                                                <a href="#" class="btn btn-xs btn-outline btn-primary">Info <i class="fa fa-long-arrow-right"></i></a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>

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
