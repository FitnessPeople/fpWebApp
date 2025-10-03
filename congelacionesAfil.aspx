<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="congelacionesAfil.aspx.cs" Inherits="fpWebApp.congelacionesAfil" %>

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

    <title>Fitness People | Congelaciones</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.3/themes/smoothness/jquery-ui.css" />

    <link href="css/plugins/dropzone/basic.css" rel="stylesheet" />
    <link href="css/plugins/dropzone/dropzone.css" rel="stylesheet" />
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet" />
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />
    <link href="css/plugins/ionRangeSlider/ion.rangeSlider.css" rel="stylesheet" />
    <link href="css/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css" rel="stylesheet" />
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet">

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#congelaciones");
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
                    <h4 class="modal-title">Guía para crear una congelacion</h4>
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
                    <h2><i class="fa fa-head-side-mask text-success m-r-sm"></i>Congelaciones</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Afiliados</li>
                        <li class="active"><strong>Congelaciones</strong></li>
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
                        <div class="ibox-title">
                            <h5>Congelación para afiliado</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">

                            <div class="row">
                                <form role="form" id="form" runat="server">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <div class="col-md-5">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="contact-box navy-bg">
                                                    <div class="col-sm-4">
                                                        <div class="text-center">
                                                            <asp:Literal ID="ltFoto" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <h3><strong>
                                                            <asp:Literal ID="ltNombre" runat="server"></asp:Literal>
                                                            <asp:Literal ID="ltApellido" runat="server"></asp:Literal></strong></h3>
                                                        <p>
                                                            <i class="fa fa-envelope"></i>
                                                            <asp:Literal ID="ltEmail" runat="server"></asp:Literal>
                                                        </p>
                                                        <address>
                                                            <i class="fa fa-mobile"></i>
                                                            <asp:Literal ID="ltCelular" runat="server"></asp:Literal><br />
                                                            <i class="fa fa-building"></i>
                                                            <asp:Literal ID="ltSede" runat="server"></asp:Literal><br />
                                                            <i class="fa fa-cake"></i>
                                                            <asp:Literal ID="ltCumple" runat="server"></asp:Literal>
                                                        </address>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="row m-xs" runat="server" id="divPlanes">
                                                    <h4>Planes</h4>
                                                    <asp:Literal ID="ltNoPlanes" runat="server"></asp:Literal>
                                                    <ul class="todo-list small-list">
                                                        <asp:Repeater ID="rpPlanesAfiliado" runat="server">
                                                            <ItemTemplate>
                                                                <li>
                                                                    <small class="label label-primary pull-right"><%# Eval("DiasQueFaltan") %> días disponibles</small>
                                                                    <label>
                                                                        <i></i><%# Eval("NombrePlan") %>, <%# Eval("Meses") %> mes(es)
                                                                    </label>
                                                                    <br />
                                                                    <div class="progress progress-striped active">
                                                                        <div style='width: <%# Eval("Porcentaje1") %>%' class="progress-bar progress-bar-success"></div>
                                                                        <div style='width: <%# Eval("Porcentaje2") %>%' class="progress-bar progress-bar-warning"></div>
                                                                    </div>
                                                                    <small class="text-muted"><%# Eval("FechaInicioPlan", "{0:dd MMM yyyy}") %> - <%# Eval("FechaFinalPlan", "{0:dd MMM yyyy}") %></small>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-7 m-t-xs">
                                        <asp:UpdatePanel ID="upCongelaciones" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Días de congelación:</label>
                                                                    <div class="form-group">
                                                                        <div id="ionrange_2"></div>
                                                                        <asp:HiddenField ID="hfDiasAfiliado" runat="server" />
                                                                        <input id="hfDias" type="hidden" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Tipo de congelación:</label>
                                                                    <div class="form-group">
                                                                        <asp:DropDownList ID="ddlTipoCongelacion" runat="server"
                                                                            CssClass="form-control input-sm" DataTextField="TipoIncapacidad"
                                                                            DataValueField="idTipoIncapacidad" AppendDataBoundItems="true">
                                                                            <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Fecha inicio de la congelación</label>
                                                                    <asp:TextBox ID="txbFechaInicio" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Documento de soporte</label><br />
                                                                    <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                                                        <div class="form-control input-sm" data-trigger="fileinput">
                                                                            <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                                            <span class="fileinput-filename"></span>
                                                                        </div>
                                                                        <span class="input-group-addon btn btn-success btn-file input-sm">
                                                                            <span class="fileinput-new input-sm">Seleccionar archivo</span>
                                                                            <span class="fileinput-exists input-sm">Cambiar</span>
                                                                            <input type="file" name="documento" id="documento" accept="application/pdf" />
                                                                        </span>
                                                                        <a href="#" class="input-group-addon btn btn-danger fileinput-exists input-sm" 
                                                                            data-dismiss="fileinput">Quitar</a>
                                                                    </div>
                                                                    <div class="error-message" style="color: red;"></div>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Observaciones</label>
                                                                    <asp:TextBox ID="txbObservaciones" CssClass="form-control input-sm" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--<asp:AsyncPostBackTrigger ControlID="check15" EventName="CheckedChanged" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                                    <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button"
                                                        onclick="window.location.href='afiliados'">
                                                        <strong>Cancelar</strong></button>
                                                    <asp:Button ID="btnSolicitarCongelacion" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right"
                                                        Text="Solicitar congelación" OnClick="btnSolicitarCongelacion_Click" />
                                                </div>
                                            </div>
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

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- IonRangeSlider -->
    <script src="js/plugins/ionRangeSlider/ion.rangeSlider.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>

        $("#form").validate({
            rules: {
                txbAfiliado: {
                    required: true,
                    minlength: 3
                },
                ddlTipoCongelacion: {
                    required: true
                },
                txbFechaInicio: {
                    required: true
                },
                txbObservaciones: {
                    required: true,
                    minlength: 20
                },
                documento: {
                    required: true
                }
            },
            errorPlacement: function (error, element) {
                if (element.attr("name") === "documento") {
                    // Coloca el mensaje de error en el contenedor personalizado
                    error.appendTo(element.closest(".form-group").find(".error-message"));
                } else {
                    // Comportamiento por defecto para otros campos
                    error.insertAfter(element);
                }
            }
        });

    </script>

    <script>

        var hfieldDias = document.getElementById("hfDias");

        $("#ionrange_2").ionRangeSlider({
            min: 1,
            max: 10,
            type: 'single',
            step: 1,
            postfix: " día(s)",
            prettify: false,
            hasGrid: true,
            onChange: function (data) {
                console.dir(data.fromNumber);
                hfieldDias.value = data.fromNumber;
            }
        });

    </script>

</body>

</html>
