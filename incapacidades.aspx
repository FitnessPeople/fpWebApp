<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="incapacidades.aspx.cs" Inherits="fpWebApp.incapacidades" %>

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

    <title>Fitness People | Incapacidades</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.3/themes/smoothness/jquery-ui.css">

    <link href="css/plugins/dropzone/basic.css" rel="stylesheet" />
    <link href="css/plugins/dropzone/dropzone.css" rel="stylesheet" />
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet" />
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />
    <link href="css/plugins/ionRangeSlider/ion.rangeSlider.css" rel="stylesheet" />
    <link href="css/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#incapacidades");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#afiliados2");
            element2.classList.remove("collapse");
        }
    </script>
    <script src="js/jquery-3.1.1.min.js"></script>
</head>

<body onload="changeClass()">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <i class="fa fa-head-side-mask modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para registrar incapacidades médicas</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar una incapacidad para un afiliado de manera clara y eficiente.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Busca al afiliado</b><br />
                        Usa el buscador para encontrar al afiliado:<br />
                        <i class="fa-solid fa-magnifying-glass"></i> Filtra por: 
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i> <b>Nombre</b>, 
                        <i class="fa-solid fa-address-card" style="color: #0D6EFD;"></i> <b>Cédula</b>, 
                        <i class="fa-solid fa-envelope" style="color: #0D6EFD;"></i> <b>Correo</b> o 
                        <i class="fa-solid fa-mobile" style="color: #0D6EFD;"></i> <b>Celular.</b><br />
                        Selecciona al afiliado correcto.
                    <br />
                        <br />
                        <b>Paso 2: Completa los datos de la incapacidad</b><br />
                        Días de incapacidad:<br />
                        <i class="fa-solid fa-calendar-day" style="color: #0D6EFD;"></i> Selecciona un valor entre <b>1 y 100 días.</b><br />
                        Tipo de incapacidad:<br />
                        <i class="fa-solid fa-tag" style="color: #0D6EFD;"></i> Selecciona una opción del menú (ej: "Enfermedad general", "Accidente laboral").<br />
                        Fecha de inicio:<br />
                        <i class="fa-solid fa-calendar-days" style="color: #0D6EFD;"></i> Elige el día en que inicia la incapacidad.<br />
                        Documento de soporte:<br />
                        <i class="fa-solid fa-folder-open" style="color: #0D6EFD;"></i> Sube el archivo (PDF/imagen) del certificado médico.<br />
                        <i class="fa-solid fa-check"></i> Asegúrate de que esté legible y sea válido.<br />
                        Motivo:<br />
                        <i class="fa-solid fa-pencil" style="color: #0D6EFD;"></i> Describe brevemente la causa (ej: "Fractura de brazo", "Cirugía programada").
                    <br />
                        <br />
                        <i class="fa-solid fa-triangle-exclamation" style="color: #FFC107;"></i> <b>Importante:</b><br />
                        Antes de finalizar, verifica en la <b>sección inferior</b> que:<br />
                        <i class="fa-solid fa-check"></i> Los datos del afiliado sean correctos.<br />
                        <i class="fa-solid fa-check"></i> La fecha y días coincidan con el certificado médico.<br />
                        <i class="fa-solid fa-check"></i> El documento esté cargado correctamente.
                    <br />
                        <br />
                        <b>Paso 3: Confirma o cancela</b><br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i> <b>Solicitar Incapacidad:</b> Envía la solicitud al sistema.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i> <b>Cancelar:</b> Si necesitas volver atrás sin guardar modificaciones.
                   <br />
                        <br />
                        <i class="fa fa-exclamation-circle mr-2"></i> Si tienes dudas, no dudes en consultar con el administrador del sistema.
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
                    <h2><i class="fa fa-head-side-mask text-success m-r-sm"></i>Incapacidades</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Afiliados</li>
                        <li class="active"><strong>Incapacidades</strong></li>
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
                            <h5>Incapacidad para afiliado</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">

                            <div class="row">
                                <form id="form" runat="server">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                    <div class="col-md-5">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label>Afiliado para aplicar incapacidad:</label>
                                                    <asp:TextBox ID="txbAfiliado" CssClass="form-control input-sm" runat="server" 
                                                        placeholder="Nombre / Cédula / Email / Celular"></asp:TextBox>
                                                    <asp:Button ID="btnAfiliado" runat="server" Text="" 
                                                        style="display:none;" OnClick="btnAfiliado_Click" />
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="contact-box navy-bg" runat="server" id="divAfiliado">
                                                    <div class="col-sm-4">
                                                        <div class="text-center">
                                                            <asp:Literal ID="ltFoto" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <h3><strong>
                                                            <asp:Literal ID="ltNombre" runat="server"></asp:Literal>
                                                            <asp:Literal ID="ltApellido" runat="server"></asp:Literal></strong></h3>
                                                        <p><i class="fa fa-envelope"></i> <asp:Literal ID="ltEmail" runat="server"></asp:Literal></p>
                                                        <address>
                                                            <i class="fa fa-mobile"></i>
                                                            <asp:Literal ID="ltCelular" runat="server"></asp:Literal><br />
                                                            <i class="fa fa-building"></i> Sede: 
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
                                                    <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
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
                                        <asp:UpdatePanel ID="upIncapacidades" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Días de incapacidad:</label>
                                                                    <div class="form-group">
                                                                        <div id="ionrange_2"></div>
                                                                        <asp:HiddenField ID="hfDiasAfiliado" runat="server" />
                                                                        <input id="hfDias" type="hidden" runat="server" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Tipo de incapacidad:</label>
                                                                    <div class="form-group">
                                                                        <asp:DropDownList ID="ddlTipoIncapacidad" runat="server"
                                                                            CssClass="form-control input-sm" DataTextField="TipoIncapacidad"
                                                                            DataValueField="idTipoIncapacidad" AppendDataBoundItems="true">
                                                                            <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Fecha inicio de la incapacidad</label>
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
                                                                            <input type="file" name="documento" id="documento" 
                                                                                accept="application/pdf" runat="server">
                                                                        </span>
                                                                        <a href="#" class="input-group-addon btn btn-danger fileinput-exists input-sm" 
                                                                            data-dismiss="fileinput">Quitar</a>
                                                                    </div>
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

                                        <div>
                                            <asp:Literal ID="ltValidacion" runat="server"></asp:Literal>
                                            <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button"
                                                onclick="window.location.href='afiliados'">
                                                <strong>Cancelar</strong></button>
                                            <asp:Button ID="btnSolicitarIncapacidad" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right"
                                                Text="Solicitar incapacidad" OnClick="btnSolicitarIncapacidad_Click" />
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

    <!-- IonRangeSlider -->
    <script src="js/plugins/ionRangeSlider/ion.rangeSlider.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>

        var hfieldDias = document.getElementById("hfDias");
        var hfieldDiasAfiliado = document.getElementById("hfDiasAfiliado");

        console.log(hfieldDiasAfiliado.value);

        $("#ionrange_2").ionRangeSlider({
            min: 1,
            max: hfieldDiasAfiliado.value,
            type: 'single',
            step: 1,
            postfix: " días",
            prettify: false,
            hasGrid: true,
            onChange: function (data) {
                console.dir(data.fromNumber);
                hfieldDias.value = data.fromNumber;
            }
        });

    </script>

    <script>

        $("#form").validate({
            rules: {
                txbAfiliado: {
                    required: true,
                    minlength: 3
                },
                //ddlTipoIncapacidad: {
                //    required: true,
                //},
                //txbFechaInicial: {
                //    required: true,
                //},
                //documento: {
                //    required: true,
                //},
                //txbObservaciones: {
                //    required: true,
                //    minlength: 20
                //}
            },
            messages: {
                documento: "*",
            }
        });
    </script>

    <script type="text/javascript">  
        $(document).ready(function () {
            $("#txbAfiliado").autocomplete({
                source: function (request, response) {
                    $.getJSON("/obtenerafiliados?search=" + request.term, function (data) {
                        response($.map(data, function (item) {
                            return {
                                label: item.nombre + " " + item.apellido + " - " + item.id + ", " + item.correo,
                                value: item.id + " - " + item.nombre + " " + item.apellido,
                            };
                        }));
                    });
                },
                select: function (event, ui) {
                    if (ui.item) {
                        console.log(ui.item.value);
                        document.getElementById("txbAfiliado").value = ui.item.value;
                        var btn = document.getElementById("btnAfiliado");
                        btn.click();
                    }
                },
                minLength: 3,
                delay: 100
            });
        });
    </script>

</body>

</html>
