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

    <link href="css/bootstrap.min.css" rel="stylesheet" />
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
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

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

    <div class="modal inmodal" id="myModal2" tabindex="-1" role="dialog" >
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <h4 class="modal-title"><span id="titulo"></span></h4>
                </div>
                <div class="modal-body">
            
                    <div class="text-center m-t-md">
                        <object data="" type="application/pdf" width="450px" height="600px" id="objFile">
                            <embed src="" id="objEmbed">
                                <p>Puede descargar el archivo aquí: <a href="" id="objHref">Descargar PDF</a>.</p>
                            </embed>
                        </object> 
                    </div>
                
            
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
                    <h2><i class="fa fa-id-card text-success m-r-sm"></i>Detalle afiliado</h2>
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
                                        <h2 class="no-margins"><asp:Literal ID="ltNombre" runat="server"></asp:Literal> <asp:Literal ID="ltApellido" runat="server"></asp:Literal>
                                </h2>
                                        <h4><asp:Literal ID="ltEmail" runat="server"></asp:Literal></h4>
                                        <small><asp:Literal ID="ltDireccion" runat="server"></asp:Literal>, <asp:Literal ID="ltCiudad" runat="server"></asp:Literal></small>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <table class="table small m-b-xs">
                                <tbody>
                                    <tr>
                                        <td><strong><i class="fab fa-whatsapp"></i></strong> <asp:Literal ID="ltCelular" runat="server"></asp:Literal></td>
                                        <td><strong><i class="fa fa-shield"></i></strong> Estado: <span class="label label-warning"><asp:Literal ID="ltEstado" runat="server"></asp:Literal></span></td>
                                    </tr>
                                    <tr>
                                        <td><strong><i class="fa fa-building"></i></strong> Sede: <asp:Literal ID="ltSede" runat="server"></asp:Literal></td>
                                        <td><strong>54</strong> Días asistidos</td>
                                    </tr>
                                    <tr>
                                        <td><strong><i class="fa fa-cake"></i></strong> <asp:Literal ID="ltCumple" runat="server"></asp:Literal></td>
                                        <td><strong>2</strong> Congelaciones</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="col-md-3">
                            <table class="table small m-b-xs">
                                <tbody>
                                    <tr>
                                        <td><strong><i class="fab fa-whatsapp"></i></strong> <asp:Literal ID="Literal1" runat="server"></asp:Literal></td>
                                        <td><strong><i class="fa fa-shield"></i></strong> Estado: <asp:Literal ID="Literal2" runat="server"></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td><strong><i class="fa fa-building"></i></strong> Sede: <asp:Literal ID="Literal3" runat="server"></asp:Literal></td>
                                        <td><strong>54</strong> Días asistidos</td>
                                    </tr>
                                    <tr>
                                        <td><strong><i class="fa fa-cake"></i></strong> <asp:Literal ID="Literal4" runat="server"></asp:Literal></td>
                                        <td><strong>2</strong> Congelaciones</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>

                    </div>
                    <div class="row">

                        <div class="col-lg-4">

                            <div class="ibox">
                                <div class="ibox-title">
                                    <h5>Planes</h5>
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
                                                        <small class="label label-primary pull-right"><%# Eval("DiasQueFaltan") %> días disponibles</small>
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
                                    <asp:Literal runat="server" ID="ltMensaje"></asp:Literal>
                                </div>
                            </div>

                            <div class="ibox">
                                <div class="ibox-title">
                                    <h5>Pagos Online</h5>
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
                                    <table class="table table-hover no-margins">
                                        <thead>
                                            <tr>
                                                <th>Status</th>
                                                <th>Date</th>
                                                <th>User</th>
                                                <th>Value</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td><small>Pending...</small></td>
                                                <td><i class="fa fa-clock-o"></i>11:20pm</td>
                                                <td>Samantha</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>24% </td>
                                            </tr>
                                            <tr>
                                                <td><span class="label label-warning">Canceled</span> </td>
                                                <td><i class="fa fa-clock-o"></i>10:40am</td>
                                                <td>Monica</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>66% </td>
                                            </tr>
                                            <tr>
                                                <td><small>Pending...</small> </td>
                                                <td><i class="fa fa-clock-o"></i>01:30pm</td>
                                                <td>John</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>54% </td>
                                            </tr>
                                            <tr>
                                                <td><small>Pending...</small> </td>
                                                <td><i class="fa fa-clock-o"></i>02:20pm</td>
                                                <td>Agnes</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>12% </td>
                                            </tr>
                                            <tr>
                                                <td><small>Pending...</small> </td>
                                                <td><i class="fa fa-clock-o"></i>09:40pm</td>
                                                <td>Janet</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>22% </td>
                                            </tr>
                                            <tr>
                                                <td><span class="label label-primary">Completed</span> </td>
                                                <td><i class="fa fa-clock-o"></i>04:10am</td>
                                                <td>Amelia</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>66% </td>
                                            </tr>
                                            <tr>
                                                <td><small>Pending...</small> </td>
                                                <td><i class="fa fa-clock-o"></i>12:08am</td>
                                                <td>Damian</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>23% </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            
                            <div class="ibox">
                                <div class="ibox-title">
                                    <h5>Cortesías</h5>
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
                                    <table class="table table-hover no-margins">
                                        <thead>
                                            <tr>
                                                <th>Status</th>
                                                <th>Date</th>
                                                <th>User</th>
                                                <th>Value</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td><small>Pending...</small></td>
                                                <td><i class="fa fa-clock-o"></i>11:20pm</td>
                                                <td>Samantha</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>24% </td>
                                            </tr>
                                            <tr>
                                                <td><span class="label label-warning">Canceled</span> </td>
                                                <td><i class="fa fa-clock-o"></i>10:40am</td>
                                                <td>Monica</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>66% </td>
                                            </tr>
                                            <tr>
                                                <td><small>Pending...</small> </td>
                                                <td><i class="fa fa-clock-o"></i>01:30pm</td>
                                                <td>John</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>54% </td>
                                            </tr>
                                            <tr>
                                                <td><small>Pending...</small> </td>
                                                <td><i class="fa fa-clock-o"></i>02:20pm</td>
                                                <td>Agnes</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>12% </td>
                                            </tr>
                                            <tr>
                                                <td><small>Pending...</small> </td>
                                                <td><i class="fa fa-clock-o"></i>09:40pm</td>
                                                <td>Janet</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>22% </td>
                                            </tr>
                                            <tr>
                                                <td><span class="label label-primary">Completed</span> </td>
                                                <td><i class="fa fa-clock-o"></i>04:10am</td>
                                                <td>Amelia</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>66% </td>
                                            </tr>
                                            <tr>
                                                <td><small>Pending...</small> </td>
                                                <td><i class="fa fa-clock-o"></i>12:08am</td>
                                                <td>Damian</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>23% </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            <div class="ibox">
                                <div class="ibox-title">
                                    <h5>Congelaciones / Incapacidades</h5>
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
                                    <table class="table table-hover no-margins">
                                        <thead>
                                            <tr>
                                                <th>Status</th>
                                                <th>Date</th>
                                                <th>User</th>
                                                <th>Value</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td><small>Pending...</small></td>
                                                <td><i class="fa fa-clock-o"></i>11:20pm</td>
                                                <td>Samantha</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>24% </td>
                                            </tr>
                                            <tr>
                                                <td><span class="label label-warning">Canceled</span> </td>
                                                <td><i class="fa fa-clock-o"></i>10:40am</td>
                                                <td>Monica</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>66% </td>
                                            </tr>
                                            <tr>
                                                <td><small>Pending...</small> </td>
                                                <td><i class="fa fa-clock-o"></i>01:30pm</td>
                                                <td>John</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>54% </td>
                                            </tr>
                                            <tr>
                                                <td><small>Pending...</small> </td>
                                                <td><i class="fa fa-clock-o"></i>02:20pm</td>
                                                <td>Agnes</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>12% </td>
                                            </tr>
                                            <tr>
                                                <td><small>Pending...</small> </td>
                                                <td><i class="fa fa-clock-o"></i>09:40pm</td>
                                                <td>Janet</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>22% </td>
                                            </tr>
                                            <tr>
                                                <td><span class="label label-primary">Completed</span> </td>
                                                <td><i class="fa fa-clock-o"></i>04:10am</td>
                                                <td>Amelia</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>66% </td>
                                            </tr>
                                            <tr>
                                                <td><small>Pending...</small> </td>
                                                <td><i class="fa fa-clock-o"></i>12:08am</td>
                                                <td>Damian</td>
                                                <td class="text-navy"><i class="fa fa-level-up"></i>23% </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>

                        </div>

                        <div class="col-lg-4">

                            <div class="ibox">
                                <div class="ibox-title">
                                    <h5>ParQ</h5>
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
                                    <div class="feed-activity-list">

                                    <div class="feed-element">
                                        <div>
                                            <small class="pull-right text-navy">25 Ene 2024</small>
                                            <strong>1</strong>
                                            <div>¿Le han diagnosticado o sufre usted una afección cardiaca o cualquier diagnóstico en su estado físico o corporal y le ha recomendado realizar actividades físicas solamente con supervisión médica? Otros similares</div>
                                            <small class="text-muted"><span class="label label-primary">No</span></small>
                                        </div>
                                    </div>

                                    <div class="feed-element">
                                        <div>
                                            <small class="pull-right text-navy">25 Ene 2024</small>
                                            <strong>2</strong>
                                            <div>¿Padece Hipertensión arterial, patología cardiaca, (enfermedad del corazón), cirugía de corazón, enfermedad arterial? ¿Su médico le ha prescrito medicamentos para la presión arterial o algún problema cardiaco? Otros similares</div>
                                            <small class="text-muted"><span class="label label-danger">Si</span></small>
                                        </div>
                                    </div>

                                    <div class="feed-element">
                                        <div>
                                            <small class="pull-right text-navy">25 Ene 2024</small>
                                            <strong>3</strong>
                                            <div>¿Sientes dolor en el pecho mientras descansas, durante tus actividades diarias o cuando haces actividad física? ¿Ha perdido la conciencia o el equilibrio después de notar sensación de mareo? Otros similares</div>
                                            <small class="text-muted"><span class="label label-primary">No</span></small>
                                        </div>
                                    </div>

                                    <div class="feed-element">
                                        <div>
                                            <small class="pull-right text-navy">25 Ene 2024</small>
                                            <strong>4</strong>
                                            <div>¿Le han diagnosticado diabetes, toma algún medicamento?</div>
                                            <small class="text-muted"><span class="label label-primary">No</span></small>
                                        </div>
                                    </div>


                                    <div class="feed-element">
                                        <div>
                                            <small class="pull-right text-navy">25 Ene 2024</small>
                                            <strong>5</strong>
                                            <div>¿Sufre de Hiperglicemia, Hipoglicemia, Colesterol alto, Problemas de peso, Otros similares?</div>
                                            <small class="text-muted"><span class="label label-primary">No</span></small>
                                        </div>
                                    </div>
                                    <div class="feed-element">
                                        <div>
                                            <small class="pull-right text-navy">25 Ene 2024</small>
                                            <strong>6</strong>
                                            <div>¿Sufre de alguna enfermedad neurológica, como convulsiones, epilepsia, depresión, ansiedad, Otros similares?</div>
                                            <small class="text-muted"><span class="label label-primary">No</span></small>
                                        </div>
                                    </div>
                                    <div class="feed-element">
                                        <div>
                                            <small class="pull-right text-navy">25 Ene 2024</small>
                                            <strong>7</strong>
                                            <div>¿Padece o le han diagnosticado alguna enfermedad en los huesos, articulaciones o músculos que pudieran afectar o agravarse por un cambio de su actividad física? ¿Tiene alguna lesión ortopédica, esta discapacitado? ¿Otros similares?</div>
                                            <small class="text-muted"><span class="label label-primary">No</span></small>
                                        </div>
                                    </div>
                                    <div class="feed-element">
                                        <div>
                                            <small class="pull-right text-navy">25 Ene 2024</small>
                                            <strong>8</strong>
                                            <div>¿Actualmente está embarazada?</div>
                                            <small class="text-muted"><span class="label label-primary">No</span></small>
                                        </div>
                                    </div>
                                    <div class="feed-element">
                                        <div>
                                            <small class="pull-right text-navy">25 Ene 2024</small>
                                            <strong>9</strong>
                                            <div>Es mayor de 59 años, tiene algún diagnóstico médico, cuál?</div>
                                            <small class="text-muted"><span class="label label-primary">No</span></small>
                                        </div>
                                    </div>
                                    <div class="feed-element">
                                        <div>
                                            <small class="pull-right text-navy">25 Ene 2024</small>
                                            <strong>10</strong>
                                            <div>¿En los últimos 6 meses, se ha realizado cirugías o tratamientos estéticos, implantes, otros y mantiene prescripciones médicas posteriores de estos tratamientos? ¿Otros similares?</div>
                                            <small class="text-muted"><span class="label label-primary">No</span></small>
                                        </div>
                                    </div>

                                </div>

                                </div>
                            </div>


                        </div>
                        <div class="col-lg-4">
                            
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5>Información adicional</h5>
                                    <div class="ibox-tools">
                                        <a class="collapse-link">
                                            <i class="fa fa-chevron-up"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="ibox-content inspinia-timeline">

                                    <div class="timeline-item">
                                        <div class="row">
                                            <div class="col-xs-3 date">
                                                <i class="fa fa-briefcase"></i>
                                                6:00 am
                                                <br/>
                                                <small class="text-navy">2 hour ago</small>
                                            </div>
                                            <div class="col-xs-7 content no-top-border">
                                                <p class="m-b-xs"><strong>Citas</strong></p>

                                                <p>Conference on the sales results for the previous year. Monica please examine sales trends in marketing and products. Below please find the current status of the
                                                    sale.</p>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="timeline-item">
                                        <div class="row">
                                            <div class="col-xs-3 date">
                                                <i class="fa fa-file-text"></i>
                                                7:00 am
                                                <br/>
                                                <small class="text-navy">3 hour ago</small>
                                            </div>
                                            <div class="col-xs-7 content">
                                                <p class="m-b-xs"><strong>Rutinas</strong></p>
                                                <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since.</p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="timeline-item">
                                        <div class="row">
                                            <div class="col-xs-3 date">
                                                <i class="fa fa-coffee"></i>
                                                8:00 am
                                                <br/>
                                            </div>
                                            <div class="col-xs-7 content">
                                                <p class="m-b-xs"><strong>Clases</strong></p>
                                                <p>
                                                    Go to shop and find some products.
                                                    Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's.
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="timeline-item">
                                        <div class="row">
                                            <div class="col-xs-3 date">
                                                <i class="fa fa-phone"></i>
                                                11:00 am
                                                <br/>
                                                <small class="text-navy">21 hour ago</small>
                                            </div>
                                            <div class="col-xs-7 content">
                                                <p class="m-b-xs"><strong>Productos</strong></p>
                                                <p>
                                                    Lorem Ipsum has been the industry's standard dummy text ever since.
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="timeline-item">
                                        <div class="row">
                                            <div class="col-xs-3 date">
                                                <i class="fa fa-user-md"></i>
                                                09:00 pm
                                                <br/>
                                                <small>21 hour ago</small>
                                            </div>
                                            <div class="col-xs-7 content">
                                                <p class="m-b-xs"><strong>Go to the doctor dr Smith</strong></p>
                                                <p>
                                                    Find some issue and go to doctor.
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="timeline-item">
                                        <div class="row">
                                            <div class="col-xs-3 date">
                                                <i class="fa fa-comments"></i>
                                                12:50 pm
                                                <br/>
                                                <small class="text-navy">48 hour ago</small>
                                            </div>
                                            <div class="col-xs-7 content">
                                                <p class="m-b-xs"><strong>Chat with Monica and Sandra</strong></p>
                                                <p>
                                                    Web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).
                                                </p>
                                            </div>
                                        </div>
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

    <!-- Page-Level Scripts -->

</body>

</html>
