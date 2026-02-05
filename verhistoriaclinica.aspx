<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="verhistoriaclinica.aspx.cs" Inherits="fpWebApp.verhistoriaclinica" %>

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

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.3/themes/smoothness/jquery-ui.css">

    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet" />

    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <!-- SweetAlert2 CDN -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

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
                        <li>Asistencial</li>
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

                    <div class="row m-b-xs m-t-xs">
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
                                        <td><strong><i class="fa fa-venus-mars m-r-xs"></i></strong>Género:
                                            <asp:Literal ID="ltGenero" runat="server"></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td><strong><i class="fa fa-cake"></i></strong>
                                            <asp:Literal ID="ltCumple" runat="server"></asp:Literal></td>
                                        <td><strong><i class="fa fa-house-medical m-r-xs"></i></strong>EPS:
                                            <asp:Literal ID="ltEPS" runat="server"></asp:Literal></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-4">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5>Historias clínicas</h5>
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
                                    <div class="panel-body">
                                        <div class="panel-group" id="accordion">
                                            <asp:Repeater ID="rpHistorias" runat="server">
                                                <ItemTemplate>
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h5 class="panel-title">
                                                                <span class="label label-warning-light pull-right"><i class="fa fa-calendar-day m-r-xs"></i><%# Eval("FechaHora", "{0:dd MMM yyyy}") %> <i class="fa fa-clock m-r-xs"></i><%# Eval("FechaHora", "{0:HH:mm}") %></span>
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse<%# Eval("idHistoria") %>">Historia Clínica #<%# Eval("idHistoria") %></a>
                                                            </h5>
                                                        </div>
                                                        <div id="collapse<%# Eval("idHistoria") %>" class="panel-collapse collapse <%# Eval("clase") %>">
                                                            <div class="panel-body">

                                                                <ul class="sortable-list connectList agile-list">

                                                                    <li class="warning-element"><b>Medicina prepagada</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("MedicinaPrepagada") %>
                                                                        </div>
                                                                    </li>

                                                                    <li class="warning-element"><b>Objetivo del ingreso</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("Objetivo") %>
                                                                        </div>
                                                                    </li>

                                                                    <li class="warning-element"><b>Detalle objetivo del ingreso</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("DescripcionObjetivoIngreso") %>
                                                                        </div>
                                                                    </li>

                                                                    <li class="warning-element"><b>Remisión</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("Remision") %>
                                                                        </div>
                                                                    </li>

                                                                    <li class="warning-element"><b>Tipo de consulta</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("TipoConsulta") %>
                                                                        </div>
                                                                    </li>

                                                                    <li class="warning-element"><b>Motivo de consulta</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("MotivoConsulta") %>
                                                                        </div>
                                                                    </li>

                                                                    <br />
                                                                    <h4><i class="fa fa-clock-rotate-left text-success m-r-xs"></i>ANTECEDENTES</h4>

                                                                    <li class="info-element"><b>Familiares</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteFamiliar") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Patológicos</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AntePatologico") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Quirúrgicos</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteQuirurgico") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Traumatológicos</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteTraumatologico") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Farmacológico</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteFarmacologico") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Actividad física</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteActividadFisica") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Toxicológicos alérgicos</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteFamiliar") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Hospitalarios</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteHospitalario") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Gineco-obstétricos</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteGineco") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>F.U.M.</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteFUM") %>
                                                                        </div>
                                                                    </li>

                                                                    <br />
                                                                    <h4><i class="fa fa-heart-circle-exclamation text-navy m-r-xs"></i>FACTORES DE RIESGO CARDIOVASCULAR</h4>

                                                                    <div class="row">
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Fuma?</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("Tabaquismo").ToString() == "1"
                                                                                        ? "<i class='fa fa-square-check text-danger'></i> Si"
                                                                                        : "<i class='fa fa-square text-navy'></i> No" %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Cigarrillos x día</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("Cigarrillos") %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Toma?</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("Alcoholismo").ToString() == "1"
                                                                                        ? "<i class='fa fa-square-check text-danger'></i> Si"
                                                                                        : "<i class='fa fa-square text-navy'></i> No" %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Frecuencia de bebida</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("Bebidas") %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Sedentarismo</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("Sedentarismo").ToString() == "1"
                                                                                        ? "<i class='fa fa-square-check text-danger'></i> Si"
                                                                                        : "<i class='fa fa-square text-navy'></i> No" %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Diabetes</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("Diabetes").ToString() == "1"
                                                                                        ? "<i class='fa fa-square-check text-danger'></i> Si"
                                                                                        : Eval("Diabetes").ToString() == "2"
                                                                                            ? "<i class='fa fa-comment-slash text-warning'></i> NS/NR"
                                                                                            : "<i class='fa fa-square text-navy'></i> No" %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Colesterol</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("Colesterol").ToString() == "1"
                                                                                        ? "<i class='fa fa-square-check text-danger'></i> Si"
                                                                                        : Eval("Colesterol").ToString() == "2"
                                                                                            ? "<i class='fa fa-comment-slash text-warning'></i> NS/NR"
                                                                                            : "<i class='fa fa-square text-navy'></i> No" %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Triglicéridos</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("Trigliceridos").ToString() == "1"
                                                                                        ? "<i class='fa fa-square-check text-danger'></i> Si"
                                                                                        : Eval("Trigliceridos").ToString() == "2"
                                                                                            ? "<i class='fa fa-comment-slash text-warning'></i> NS/NR"
                                                                                            : "<i class='fa fa-square text-navy'></i> No" %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                    </div>

                                                                    <li class="success-element"><b>H.T.A.</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("HTA").ToString() == "1"
                                                                                ? "<i class='fa fa-square-check text-danger'></i> Si"
                                                                                : Eval("HTA").ToString() == "2"
                                                                                    ? "<i class='fa fa-comment-slash text-warning'></i> NS/NR"
                                                                                    : "<i class='fa fa-square text-navy'></i> No" %>
                                                                        </div>
                                                                    </li>

                                                                </ul>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-8">
                            <div class="ibox float-e-margins" runat="server" id="divContenido">
                                <div class="ibox-title">
                                    <h5>Formulario para la creación de una nueva historia clínica</h5>
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

                                    <div class="row">
                                        <form role="form" id="form" runat="server">
                                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                            <div class="col-sm-12">

                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Medicina prepagada</label>
                                                            <asp:TextBox ID="txbMedicinaPrepagada" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Objetivo del ingreso</label>
                                                            <asp:DropDownList ID="ddlObjetivo" runat="server" AppendDataBoundItems="true"
                                                                DataTextField="Objetivo" DataValueField="idObjetivo" CssClass="form-control input-sm m-b">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Detalle objetivo del ingreso</label>
                                                            <asp:TextBox ID="txbDescripcionObjetivo" CssClass="form-control input-sm" runat="server"
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Remisión</label>
                                                            <asp:TextBox ID="txbRemision" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Tipo de consulta</label>
                                                            <asp:DropDownList ID="ddlTipoConsulta" runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm m-b">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="Consulta de control" Value="Consulta de control"></asp:ListItem>
                                                                <asp:ListItem Text="Primera vez" Value="Primera vez"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Motivo de consulta</label>
                                                            <asp:TextBox ID="txbMotivoConsulta" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="widget style1 bg-success">
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

                                            <div class="col-sm-12">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Familiares</label>
                                                            <asp:TextBox ID="txbAnteFamiliares" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Patológicos</label>
                                                            <asp:TextBox ID="txbAntePatologico" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Quirúrgicos</label>
                                                            <asp:TextBox ID="txbAnteQuirurgico" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Traumatológicos </label>
                                                            <asp:TextBox ID="txbAnteTraumatologico" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Farmacológico</label>
                                                            <asp:TextBox ID="txbAnteFarmacologico" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Actividad física</label>
                                                            <asp:TextBox ID="txbAnteActividadFisica" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Toxicológicos alérgicos</label>
                                                            <asp:TextBox ID="txbAnteToxicologico" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Hospitalarios</label>
                                                            <asp:TextBox ID="txbAnteHospitalario" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Gineco-obstétricos</label>
                                                            <asp:TextBox ID="txbAnteGinecoObstetricio" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>F.U.M.</label>
                                                            <asp:TextBox ID="txbFum" CssClass="form-control input-sm" runat="server" name="txbFum"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="widget style1 navy-bg">
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
                                                            <asp:RadioButtonList ID="rblFuma" runat="server" CssClass="i-checks input-sm" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="&nbsp;Si" Value="1" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;No" Value="0" style="margin-right: 10px;"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Cigarrilos x día</label>
                                                            <asp:TextBox ID="txbCigarrillos" CssClass="form-control input-sm" runat="server" Text="0"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Toma?</label>
                                                            <asp:RadioButtonList ID="rblToma" runat="server" CssClass="i-checks input-sm" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="&nbsp;Si" Value="1" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;No" Value="0" style="margin-right: 10px;"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Frecuencia de bebida</label>
                                                            <asp:DropDownList ID="ddlBebidas" runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm m-b" Enabled="false">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="Diario" Value="Diario"></asp:ListItem>
                                                                <asp:ListItem Text="Semanal" Value="Semanal"></asp:ListItem>
                                                                <asp:ListItem Text="Mensual" Value="Mensual"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-4">
                                                <div class="row">
                                                    <div class="col-sm-6 b-r">
                                                        <div class="form-group">
                                                            <label>Sedentarismo</label>
                                                            <asp:RadioButtonList ID="rblSedentarismo" runat="server" CssClass="i-checks input-sm" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="&nbsp;Si" Value="1" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;No" Value="0" style="margin-right: 10px;"></asp:ListItem>
                                                                <%--<asp:ListItem Text="&nbsp;NS/NR" Value="2" style="margin-right: 10px;"></asp:ListItem>--%>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6 b-r">
                                                        <div class="form-group">
                                                            <label>Diabetes</label>
                                                            <asp:RadioButtonList ID="rblDiabetes" runat="server" CssClass="i-checks input-sm" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="&nbsp;Si" Value="1" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;No" Value="0" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;NS/NR" Value="2" style="margin-right: 10px;"></asp:ListItem>
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
                                                            <asp:RadioButtonList ID="rblColesterol" runat="server" CssClass="i-checks input-sm" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="&nbsp;Si" Value="1" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;No" Value="0" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;NS/NR" Value="2" style="margin-right: 10px;"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 b-r">
                                                        <div class="form-group">
                                                            <label>Triglicéridos</label>
                                                            <asp:RadioButtonList ID="rblTrigliceridos" runat="server" CssClass="i-checks input-sm" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="&nbsp;Si" Value="1" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;No" Value="0" style="margin-right: 10px;"></asp:ListItem>
                                                                <asp:ListItem Text="&nbsp;NS/NR" Value="2" style="margin-right: 10px;"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>H.T.A.</label>
                                                            <asp:RadioButtonList ID="rblHTA" runat="server" CssClass="i-checks input-sm" RepeatDirection="Horizontal">
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
                                                    <br />
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div>
                                                    <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button"
                                                        onclick="window.location.href='historiasclinicas'">
                                                        <strong>Cancelar</strong></button>
                                                    <asp:Button ID="btnAgregar" runat="server"
                                                        CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right"
                                                        Text="Guardar y continuar" Visible="false"
                                                        ValidationGroup="agregar" OnClick="btnAgregar_Click" />
                                                    <asp:Button ID="btnContinuar" runat="server"
                                                        CssClass="btn btn-sm btn-success m-t-n-xs m-r-md pull-right"
                                                        Text="Continuar sin guardar" Visible="false" OnClick="btnContinuar_Click" />
                                                </div>
                                            </div>
                                        </form>
                                    </div>

                                    <%--Fin Contenido!!!!--%>
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
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <script>

        $.validator.setDefaults({ ignore: ":hidden:not(.chosen-select)" })

        $("#form").validate({
            rules: {
                txbMedicinaPrepagada: {
                    required: true,
                },
                ddlObjetivo: {
                    required: true,
                },
                txbDescripcionObjetivo: {
                    required: true,
                },
                ddlTipoConsulta: {
                    required: true,
                },
                txbMotivoConsulta: {
                    required: true,
                },
                txbAnteFamiliares: {
                    required: true
                },
                txbAntePatologico: {
                    required: true,
                },
                txbAnteQuirurgico: {
                    required: true,
                },
                txbAnteTraumatologico: {
                    required: true,
                },
                txbAnteFarmacologico: {
                    required: true,
                },
                txbAnteActividadFisica: {
                    required: true,
                },
                txbAnteToxicologico: {
                    required: true,
                },
                txbAnteHospitalario: {
                    required: true,
                },
                rblFuma: {
                    required: true
                },
                txbCigarrillos: {
                    required: true
                },
                rblToma: {
                    required: true
                },
                ddlBebidas: {
                    required: true
                },
                rblSedentarismo: {
                    required: true
                },
                rblDiabetes: {
                    required: true
                },
                rblColesterol: {
                    required: true
                },
                rblTrigliceridos: {
                    required: true
                },
                rblHTA: {
                    required: true
                },
            },
            messages: {
                rblFuma: "*",
                rblToma: "*",
                rblSedentarismo: "*",
                rblDiabetes: "*",
                rblColesterol: "*",
                rblTrigliceridos: "*",
                rblHTA: "*",
            }
        });

        $('.chosen-select').chosen({ width: "100%" });
    </script>

    <script>
        document.addEventListener("DOMContentLoaded", function () {

            function configurarRadio(controlRadioID, controlObjetivoID, valorSi) {

                var radio = document.getElementById(controlRadioID);
                var objetivo = document.getElementById(controlObjetivoID);

                function actualizar() {
                    var seleccionado = radio.querySelector("input[type='radio']:checked");

                    if (seleccionado && seleccionado.value === valorSi) {
                        objetivo.disabled = false;
                    } else {
                        objetivo.disabled = true;

                        // Reset según tipo
                        if (objetivo.tagName === "INPUT") objetivo.value = "0";
                        if (objetivo.tagName === "SELECT") objetivo.value = "";
                    }
                }

                radio.addEventListener("change", actualizar);
                actualizar();
            }

            configurarRadio("<%= rblFuma.ClientID %>", "<%= txbCigarrillos.ClientID %>", "1");
            configurarRadio("<%= rblToma.ClientID %>", "<%= ddlBebidas.ClientID %>", "1");

        });
    </script>

</body>

</html>
