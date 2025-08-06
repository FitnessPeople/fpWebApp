<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="detalleafiliado.aspx.cs" Inherits="fpWebApp.detalleafiliado" %>

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

    <title>Fitness People | Detalle afiliado</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.3/themes/smoothness/jquery-ui.css">

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />
    <link href="css/plugins/dropzone/basic.css" rel="stylesheet">
    <link href="css/plugins/dropzone/dropzone.css" rel="stylesheet">
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet">
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet">

    <!-- FooTable -->
    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />--%>
    <link href="css/plugins/footable/footable.bootstrap.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#afiliados1");
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
                    <i class="fa fa-person-chalkboard modal-icon"></i>
                    <h4 class="modal-title">Guía para realizar autorizaciones</h4>
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
                    <h2><i class="fa fa-id-card text-success m-r-xs"></i>Detalle afiliado</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Afiliados</li>
                        <li class="active"><strong>Detalle afiliado</strong></li>
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
                        <div class="row m-b-lg m-t-lg">
                            <div class="col-md-6">

                                <div class="profile-image">
                                    <asp:Literal ID="ltFoto" runat="server"></asp:Literal>
                                </div>
                                <div class="profile-info">
                                    <div class="">
                                        <div>
                                            <h2 class="no-margins">
                                                <asp:Literal ID="ltNombre" runat="server"></asp:Literal>
                                                <asp:Literal ID="ltApellido" runat="server"></asp:Literal>
                                            </h2>
                                            <h4>
                                                <asp:Literal ID="ltTipoDoc" runat="server"></asp:Literal>
                                                <asp:Literal ID="ltDocumento" runat="server"></asp:Literal></h4>
                                            <h4>
                                                <asp:Literal ID="ltEmail" runat="server"></asp:Literal></h4>
                                            <small>
                                                <asp:Literal ID="ltDireccion" runat="server"></asp:Literal>,
                                                <asp:Literal ID="ltCiudad" runat="server"></asp:Literal></small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <table class="table small m-b-xs">
                                    <tbody>
                                        <tr>
                                            <td><strong><i class="fab fa-whatsapp m-r-xs"></i></strong>
                                                <asp:Literal ID="ltCelular" runat="server"></asp:Literal></td>
                                            <td><strong><i class="fa fa-shield m-r-xs"></i></strong>Estado: 
                                                <asp:Literal ID="ltEstado" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td><strong><i class="fa fa-building m-r-xs"></i></strong>Sede:
                                                <asp:Literal ID="ltSede" runat="server"></asp:Literal></td>
                                            <td><strong>54</strong> Días asistidos</td>
                                        </tr>
                                        <tr>
                                            <td><strong><i class="fa fa-cake m-r-xs"></i></strong>
                                                <asp:Literal ID="ltCumple" runat="server"></asp:Literal></td>
                                            <td><strong>2</strong> Congelaciones</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <%--<div class="col-md-3">
                                <table class="table small m-b-xs">
                                    <tbody>
                                        <tr>
                                            <td><strong><i class="fab fa-whatsapp"></i></strong>
                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal></td>
                                            <td><strong><i class="fa fa-shield"></i></strong> Estado:
                                                <asp:Literal ID="Literal2" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td><strong><i class="fa fa-building"></i></strong> Sede:
                                                <asp:Literal ID="Literal3" runat="server"></asp:Literal></td>
                                            <td><strong>54</strong> Días asistidos</td>
                                        </tr>
                                        <tr>
                                            <td><strong><i class="fa fa-cake"></i></strong>
                                                <asp:Literal ID="Literal4" runat="server"></asp:Literal></td>
                                            <td><strong>2</strong> Congelaciones</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>--%>
                        </div>
                        <div class="row">

                            <div class="col-lg-5">

                                <div class="ibox">
                                    <div class="ibox-title bg-primary">
                                        <h5><i class="fa fa-ticket m-r-xs"></i>Planes</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                            <a class="fullscreen-link">
                                                <i class="fa fa-expand"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <ul class="todo-list small-list">
                                            <asp:Repeater ID="rpPlanesAfiliado" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <div class="i-checks">
                                                            <small class="label label-<%# Eval("label1") %> pull-right"><%# Eval("DiasQueFaltan") %></small>
                                                            <label>
                                                                <%# Eval("NombrePlan") %>, <%# Eval("Meses") %> mes(es)
                                                            </label>
                                                            <br />
                                                            <div class="progress progress-striped active">
                                                                <div style='width: <%# Eval("Porcentaje1") %>%' class="progress-bar progress-bar-success"></div>
                                                                <div style='width: <%# Eval("Porcentaje2") %>%' class="progress-bar progress-bar-warning"></div>
                                                            </div>
                                                            <small class="text-muted"><%# Eval("FechaInicioPlan", "{0:dd MMM yyyy}") %> - <%# Eval("FechaFinalPlan", "{0:dd MMM yyyy}") %></small>
                                                        </div>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </div>

                                    <%-- Acceso a Armatura --%>
                                    <form runat="server" id="form1">
                                        <div class="ibox-content media-body">
                                            <div class="row">
                                                <div class="col-lg-3">
                                                    <asp:Literal runat="server" ID="ltImagen"></asp:Literal>
                                                </div>
                                                <div class="col-lg-9">
                                                    <div class="well">
                                                        <asp:Literal runat="server" ID="ltMensaje"></asp:Literal>
                                                    </div>
                                                    <div class="pull-right" runat="server" visible="false" id="divAcceso">
                                                        <asp:LinkButton ID="lbDarAcceso" CssClass="btn btn-xs btn-primary"
                                                            runat="server" OnClick="lbDarAcceso_Click"><i class="fa fa-building-lock"></i> Dar acceso
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </form>
                                </div>

                                <div class="ibox">
                                    <div class="ibox-title bg-info">
                                        <h5><i class="fa fa-credit-card m-r-xs"></i>Pagos</h5>
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
                                        <asp:Literal ID="ltDetalle" runat="server"></asp:Literal>
                                    </div>
                                </div>

                                <div class="ibox">
                                    <div class="ibox-title bg-success">
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
                                            data-empty="Sin registros">
                                            <thead>
                                                <tr>
                                                    <th data-sortable="false">Días de Cortesía</th>
                                                    <th data-sortable="false">Observaciones</th>
                                                    <th data-sortable="false">Fecha</th>
                                                    <th data-sortable="false">Estado</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpCortesias" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("DiasCortesia") %> días</td>
                                                            <td><%# Eval("ObservacionesCortesia") %></td>
                                                            <td style="vertical-align: central;">
                                                                <%# Eval("FechaHoraCortesia", "{0:dd MMM yyyy}") %>
                                                            </td>
                                                            <td><%# Eval("EstadoCortesia") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

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
                                            data-empty="Sin registros">
                                            <thead>
                                                <tr>
                                                    <th data-sortable="false">Días de congelación</th>
                                                    <th data-sortable="false">Observaciones</th>
                                                    <th data-sortable="true">Fecha</th>
                                                    <th data-sortable="true">Estado</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpCongelaciones" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("Dias") %></td>
                                                            <td><%# Eval("Observaciones") %></td>
                                                            <td style="vertical-align: central;">
                                                                <%# Eval("Fecha", "{0:dd MMM yyyy}") %>
                                                            </td>
                                                            <td><%# Eval("Estado") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

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
                                            data-empty="Sin registros">
                                            <thead>
                                                <tr>
                                                    <th data-sortable="false">Días de incapacidad</th>
                                                    <th data-sortable="false">Observaciones</th>
                                                    <th data-sortable="false">Fecha</th>
                                                    <th data-sortable="false">Estado</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpIncapacidades" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("Dias") %></td>
                                                            <td><%# Eval("Observaciones") %></td>
                                                            <td style="vertical-align: central;">
                                                                <%# Eval("Fecha", "{0:dd MMM yyyy}") %>
                                                            </td>
                                                            <td><%# Eval("Estado") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                            </div>

                            <div class="col-lg-4">

                                <div class="ibox">
                                    <div class="ibox-title bg-warning">
                                        <h5><i class="fa fa-person-circle-question m-r-xs"></i>ParQ</h5>
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
                                        <div class="feed-activity-list">

                                            <asp:Repeater ID="rpParq" runat="server">
                                                <ItemTemplate>
                                                    <div class="feed-element">
                                                        <div>
                                                            <small class="pull-right text-navy"><%# Eval("FechaRespParQ", "{0:dd MMM yyyy}") %></small>
                                                            <strong><%# Eval("Orden") %></strong>
                                                            <div><%# Eval("PreguntaParq") %></div>
                                                            <small class="text-muted"><span class="label label-<%# Eval("label") %>"><%# Eval("Respuesta1") %></span> <%# Eval("Argumento") %></small>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </div>

                                    </div>
                                </div>

                            </div>

                            <div class="col-lg-3">

                                <div class="ibox float-e-margins">
                                    <div class="ibox-title bg-info">
                                        <h5><i class="fa fa-circle-info m-r-xs"></i>Información CRM</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content inspinia-timeline">

                                        <div class="timeline-item">
                                            <div class="row">
                                                <asp:Literal ID="ltCRM" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="ibox">
                                    <div class="ibox-content">
                                        <h3><i class="fa fa-folder-open m-r-xs"></i>Documentos</h3>
                                        <ul class="list-unstyled file-list">
                                            <li><a href=""><i class="fa fa-file m-r-xs"></i>Project_document.docx</a></li>
                                            <li><a href=""><i class="fa fa-file-image m-r-xs"></i>Logo_zender_company.jpg</a></li>
                                            <li><a href=""><i class="fab fa-stack-exchange m-r-xs"></i>Email_from_Alex.mln</a></li>
                                            <li><a href=""><i class="fa fa-file m-r-xs"></i>Contract_20_11_2014.docx</a></li>
                                            <li><a href=""><i class="fa fa-file-powerpoint m-r-xs"></i>Presentation.pptx</a></li>
                                            <li><a href=""><i class="fa fa-file m-r-xs"></i>10_08_2015.docx</a></li>
                                        </ul>
                                    </div>
                                </div>
                                <button id="btnVolver" runat="server" type="button" class="btn btn-sm btn-info pull-right m-t-n-xs" onclick="window.location.href='agendacrm.aspx';">
                                    Regresar a Agenda CRM</button>
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
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <!-- FooTable -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();
    </script>
</body>

</html>
