<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="histclinutricion03.aspx.cs" Inherits="fpWebApp.histclinutricion03" %>

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

    <link href="css/plugins/clockpicker/clockpicker.css" rel="stylesheet">

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
                    <h2><i class="fa fa-notes-medical text-success m-r-sm"></i>Historia clínica - Detalle nutricional</h2>
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
                                        <td><strong><i class="fab fa-whatsapp"></i></strong>
                                            <asp:Literal ID="ltCelular" runat="server"></asp:Literal></td>
                                        <td><strong><i class="fa fa-shield"></i></strong> Estado: 
                        <asp:Literal ID="ltEstado" runat="server"></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td><strong><i class="fa fa-building"></i></strong> Sede:
                        <asp:Literal ID="ltSede" runat="server"></asp:Literal></td>
                                        <td><strong><i class="fa fa-venus-mars"></i></strong> Género:
                                            <asp:Literal ID="ltGenero" runat="server"></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td><strong><i class="fa fa-cake"></i></strong>
                                            <asp:Literal ID="ltCumple" runat="server"></asp:Literal></td>
                                        <td><strong><i class="fa fa-house-medical"></i></strong> EPS:
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
                                                                <span class="label label-warning-light pull-right"><i class="fa fa-calendar-day"></i> <%# Eval("FechaHora", "{0:dd MMM yyyy}") %></span>
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

                                                                    <br />
                                                                    <h4><i class="fa fa-clock-rotate-left text-success"></i> ANTECEDENTES</h4>

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
                                                                    <h4><i class="fa fa-heart-circle-exclamation text-navy"></i> FACTORES DE RIESGO CARDIOVASCULAR</h4>

                                                                    <div class="row">
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Fuma?</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("fuma") %>
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
                                                                                    <%# Eval("toma") %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Bebidas x mes</b>
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
                                                                                    <%# Eval("sedentario") %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Diabetes</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("diabetico") %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Colesterol</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("colesterado") %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Triglicéridos</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("triglicerado") %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                    </div>

                                                                    <li class="success-element"><b>H.T.A.</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("hipertenso") %>
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
                                    <h5>Formulario para agregar detalles nutricionales</h5>
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
                                                <div class="widget style1 bg-success">
                                                    <div class="row vertical-align">
                                                        <div class="col-xs-3">
                                                            <i class="fa fa-burger fa-2x"></i>
                                                        </div>
                                                        <div class="col-xs-9 text-right">
                                                            <h3 class="font-bold">Frecuencia de consumo</h3>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Lacteos </label>
                                                            <asp:TextBox ID="txbLacteos" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Azúcares</label>
                                                            <asp:TextBox ID="txbAzucares" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Gaseosas</label>
                                                            <asp:TextBox ID="txbGaseosa" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Verduras</label>
                                                            <asp:TextBox ID="txbVerduras" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Salsamentaria</label>
                                                            <asp:TextBox ID="txbSalsamentaria" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Agua</label>
                                                            <asp:TextBox ID="txbAgua" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Frutas</label>
                                                            <asp:TextBox ID="txbFrutas" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Carnes</label>
                                                            <asp:TextBox ID="txbCarnes" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Comidas rápidas</label>
                                                            <asp:TextBox ID="txbComidasRapidas" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Cigarrillos</label>
                                                            <asp:TextBox ID="txbCigarrillos" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Psicoactivos</label>
                                                            <asp:TextBox ID="txbPsicoactivos" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Huevos </label>
                                                            <asp:TextBox ID="txbHuevos" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Vísceras</label>
                                                            <asp:TextBox ID="txbVisceras" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Sopas</label>
                                                            <asp:TextBox ID="txbSopas" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Paquetes</label>
                                                            <asp:TextBox ID="txbPaquetes" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Cereales</label>
                                                            <asp:TextBox ID="txbCereales" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Raíces</label>
                                                            <asp:TextBox ID="txbRaices" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Pan</label>
                                                            <asp:TextBox ID="txbPan" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Grasas</label>
                                                            <asp:TextBox ID="txbGrasas" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Alcohol</label>
                                                            <asp:TextBox ID="txbAlcohol" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Bebida hidratante</label>
                                                            <asp:TextBox ID="txbBebidaHidratante" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div>
                                                    <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button">
                                                        <strong>Cancelar</strong></button>
                                                    <asp:Button ID="btnAgregar" runat="server"
                                                        CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right"
                                                        Text="Siguiente" Visible="false"
                                                        ValidationGroup="agregar" OnClick="btnAgregar_Click" />
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

    <!-- Clock picker -->
    <script src="js/plugins/clockpicker/clockpicker.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <script>
        $.validator.setDefaults({ ignore: ":hidden:not(.chosen-select)" })

        $("#form").validate({
            rules: {
                txbLacteos: {
                    required: true,
                },
                txbAzucares: {
                    required: true,
                },
                txbGaseosa: {
                    required: true,
                },
                txbVerduras: {
                    required: true
                },
                txbSalsamentaria: {
                    required: true,
                },
                txbAgua: {
                    required: true,
                },
                txbFrutas: {
                    required: true,
                },
                txbCarnes: {
                    required: true,
                },
                txbComidasRapidas: {
                    required: true,
                },
                txbCigarrillos: {
                    required: true,
                },
                txbPsicoactivos: {
                    required: true,
                },
                txbHuevos: {
                    required: true
                },
                txbVisceras: {
                    required: true
                },
                txbSopas: {
                    required: true
                },
                txbPaquetes: {
                    required: true
                },
                txbCereales: {
                    required: true
                },
                txbRaices: {
                    required: true
                },
                txbPan: {
                    required: true
                },
                txbGrasas: {
                    required: true
                },
                txbAlcohol: {
                    required: true
                },
                txbBebidaHidratante: {
                    required: true
                },
            },
        });

        $('.chosen-select').chosen({ width: "100%" });
    </script>

</body>

</html>
