<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nuevahistoriaclinica.aspx.cs" Inherits="fpWebApp.nuevahistoriaclinica" %>

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

    <title>Fitness People | Nueva historia clínica</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.3/themes/smoothness/jquery-ui.css">

    <link href="css/plugins/dropzone/basic.css" rel="stylesheet" />
    <link href="css/plugins/dropzone/dropzone.css" rel="stylesheet" />
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet" />

    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#nuevahistoria");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#medico");
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
                    <h4 class="modal-title">Guía para crear una nueva historia clínica</h4>
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
                    <h2><i class="fa fa-notes-medical text-success m-r-sm"></i>Nueva historia clínica</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Médico</li>
                        <li class="active"><strong>Historias clínicas</strong></li>
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
                                    <h5>Formulario para la creación de una nueva historia clínica</h5>
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
                                            <div class="col-sm-12">
                                                <%--<asp:UpdatePanel ID="upBusqueda" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                                    <ContentTemplate>--%>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label>Afiliado</label>
                                                            <asp:TextBox ID="txbAfiliado" CssClass="form-control input-sm" runat="server" 
                                                                placeholder="Nombre / Cédula / Email / Celular"></asp:TextBox>
                                                             <asp:Button ID="btnAfiliado" runat="server" Text="" 
                                                                style="display:none;" OnClick="btnAfiliado_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label>Nombre</label>
                                                            <asp:TextBox ID="txbNombreAfiliado" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label>Profesión</label>
                                                            <asp:TextBox ID="txbProfesion" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label>EPS</label>
                                                            <asp:TextBox ID="txbEps" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                                            <asp:HiddenField ID="hfGenero" runat="server" />
                                                            <asp:HiddenField ID="hfIdAfiliado" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                                    <%--</ContentTemplate>
                                                    <Triggers>
                                                      <asp:AsyncPostBackTrigger ControlID="txbBuscar" EventName="TextChanged" />
                                                   </Triggers>
                                                </asp:UpdatePanel>--%>
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Medicina prepagada</label>
                                                            <asp:TextBox ID="txbMedicinaPrepagada" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Objetivo del ingreso</label>
                                                            <asp:DropDownList ID="ddlObjetivo" runat="server" AppendDataBoundItems="true" 
                                                                DataTextField="Objetivo" DataValueField="idObjetivo" CssClass="form-control m-b">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Descripción objetivo del ingreso</label>
                                                            <asp:TextBox ID="txbDescripcionObjetivo" CssClass="form-control" runat="server" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="widget style1 lazur-bg">
                                                    <div class="row vertical-align">
                                                        <div class="col-xs-3">
                                                            <i class="fa fa-clock-rotate-left fa-2x"></i>
                                                        </div>
                                                        <div class="col-xs-9 text-right">
                                                            <h3 class="font-bold">Antecedentes</h3>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-7">
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Familiares</label>
                                                            <asp:TextBox ID="txbAnteFamiliares" CssClass="form-control" runat="server" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvAnteFamiliares" 
                                                                ControlToValidate="txbAnteFamiliares" runat="server" 
                                                                ErrorMessage="* Campo requerido" ValidationGroup="agregar">
                                                            </asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Patológicos</label>
                                                            <asp:TextBox ID="txbAntePatologico" CssClass="form-control" runat="server" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Quirúrgicos</label>
                                                            <asp:TextBox ID="txbAnteQuirurgico" CssClass="form-control" runat="server" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Traumatológicos </label>
                                                            <asp:TextBox ID="txbAnteTraumatologico" CssClass="form-control" runat="server" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Farmacológico</label>
                                                            <asp:TextBox ID="txbAnteFarmacologico" CssClass="form-control" runat="server" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Actividad física</label>
                                                            <asp:TextBox ID="txbAnteActividadFisica" CssClass="form-control" runat="server" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-5">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Toxicológicos alérgicos</label>
                                                            <asp:TextBox ID="txbAnteToxicologico" CssClass="form-control" runat="server" name="txbFechaNac" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Hospitalarios</label>
                                                            <asp:TextBox ID="txbAnteHospitalario" CssClass="form-control" runat="server" name="txbFechaNac" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Gineco-obstétricos</label>
                                                            <asp:TextBox ID="txbAnteGinecoObstetricio" CssClass="form-control" runat="server" name="txbFechaNac" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>F.U.M.</label>
                                                            <asp:TextBox ID="txbFum" CssClass="form-control" runat="server" name="txbFum"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="widget style1 lazur-bg">
                                                    <div class="row vertical-align">
                                                        <div class="col-xs-3">
                                                            <i class="fa fa-heart-circle-exclamation fa-2x"></i>
                                                        </div>
                                                        <div class="col-xs-9 text-right">
                                                            <h3 class="font-bold">Factores de Riesgo Cardiovascular</h3>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Fuma?</label>
                                                            <asp:RadioButtonList ID="rblFuma" runat="server" CssClass="i-checks" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="&nbsp;Si" Value="1" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;No" Value="0" style="margin-right: 10px;"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Cigarrilos x día</label>
                                                            <asp:TextBox ID="txbCigarrillos" CssClass="form-control" runat="server" Text="0"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Toma?</label>
                                                            <asp:RadioButtonList ID="rblToma" runat="server" CssClass="i-checks" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="&nbsp;Si" Value="1" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;No" Value="0" style="margin-right: 10px;"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Bebidas x mes</label>
                                                            <asp:TextBox ID="txbBebidas" CssClass="form-control" runat="server" Text="0"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4">
                                                <div class="row">
                                                    <div class="col-sm-6 b-r">
                                                        <div class="form-group">
                                                            <label>Sedentarismo</label>
                                                            <asp:RadioButtonList ID="rblSedentarismo" runat="server" CssClass="i-checks" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="&nbsp;Si" Value="1" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;No" Value="0" style="margin-right: 10px;"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 b-r">
                                                        <div class="form-group">
                                                            <label>Diabetes</label>
                                                            <asp:RadioButtonList ID="rblDiabetes" runat="server" CssClass="i-checks" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="&nbsp;Si" Value="1" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;No" Value="0" style="margin-right: 10px;"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-8">
                                                <div class="row">
                                                    <div class="col-sm-4 b-r">
                                                        <div class="form-group">
                                                            <label>Colesterol</label>
                                                            <asp:RadioButtonList ID="rblColesterol" runat="server" CssClass="i-checks" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="&nbsp;Si" Value="1" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;No" Value="0" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;NS/NR" Value="2" style="margin-right: 10px;"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 b-r">
                                                        <div class="form-group">
                                                            <label>Triglicéridos</label>
                                                            <asp:RadioButtonList ID="rblTrigliceridos" runat="server" CssClass="i-checks" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="&nbsp;Si" Value="1" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;No" Value="0" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;NS/NR" Value="2" style="margin-right: 10px;"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>H.T.A.</label>
                                                            <asp:RadioButtonList ID="rblHTA" runat="server" CssClass="i-checks" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="&nbsp;Si" Value="1" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;No" Value="0" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;NS/NR" Value="2" style="margin-right: 10px;"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div>
                                                    <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button" 
                                                        onclick="window.location.href='historiasclinicas'"><strong>Cancelar</strong></button>
                                                    <asp:Button ID="btnAgregar" runat="server" 
                                                        CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right" 
                                                        Text="Agregar" Visible="false" OnClick="btnAgregar_Click" 
                                                        ValidationGroup="agregar" />
                                                </div>
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

    <script>
        $("#form").validate({
            rules: {
                txbAfiliado: {
                    required: true,
                    minlength: 3
                },
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
                delay: 100,
            });
        });
    </script>

</body>

</html>
