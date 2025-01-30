<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="respuestaautorizacion.aspx.cs" Inherits="fpWebApp.respuestaautorizacion" %>

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

    <title>Fitness People | Autorizaciones</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
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
            var element1 = document.querySelector("#autorizaciones");
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
                    <h4 class="modal-title">Guía para responder una autorización</h4>
                    <small class="font-bold">¡Bienvenido! A continuación, te ofrecemos una guía sencilla para ayudarte a completar el formulario de manera correcta y eficiente. Sigue estos pasos para asegurarte de que toda la información se registre de forma adecuada.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>1. Revisa los datos de la autorización.</b><br />
                        Antes de comenzar, es importante que leas todos los datos de la autorización. Esto te ayudará a entender qué información se requiere y cómo debe ser respondida.
                        <br />
                        <br />
                        <b>2. Completa los campos requeridos</b><br />
                        Respuesta: Ingresa tu respuesta a la autorización solicitada.<br />
                        Elije <strong>Autorizar</strong> o <strong>No autorizar</strong> la solicitud.
                        <br />
                        <br />
                        <b>3. Envía la respuesta</b><br />
                        Asegúrate de seguir el proceso de envío indicado (hacer clic en "Responder").
                        <br />
                        <br />
                        <b>4. Cancelar</b><br />
                        Puedes regresar a las autorizaciones sin generar una respuesta, dando clic en "Cancelar".
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
                    <h2><i class="fa fa-unlock text-success m-r-sm"></i>Autorizaciones</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Afiliados</li>
                        <li class="active"><strong>Autorizaciones</strong></li>
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

                    <form role="form" id="form" runat="server">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <%--Traspasos--%>
                        <div class="ibox float-e-margins" id="divTraspasos" runat="server">
                            <div class="ibox-title">
                                <h5><i class="fa fa-right-left"></i>Autorización de traspaso</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-sm-7 b-r">
                                        <div class="row">
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label>Nombre del afiliado origen</label>
                                                    <p>
                                                        <asp:Literal ID="ltAfiliadoOrigen" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <p class="text-center"><i class="fa fa-angles-right fa-2x"></i></p>
                                            </div>
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label>Nombre del afiliado destino</label>
                                                    <p>
                                                        <asp:Literal ID="ltAfiliadoDestino" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label>Plan a traspasar</label>
                                            <div class="row m-xs">
                                                <ul class="todo-list small-list">
                                                    <asp:Repeater ID="rpPlanAfiliadoTraspaso" runat="server">
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
                                        <div class="row">
                                            <div class="col-sm-6 b-r">
                                                <div class="form-group">
                                                    <label>Fecha solicitud</label>
                                                    <p>
                                                        <asp:Literal ID="ltFechaTraspaso" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Fecha inicio traspaso</label>
                                                    <p>
                                                        <asp:Literal ID="ltFechaInicioTraspaso" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-sm-6 b-r">
                                                <div class="form-group">
                                                    <label>Documento de respaldo</label>
                                                    <p><asp:Literal ID="ltDocumentoTraspaso" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Usuario</label>
                                                    <p>
                                                        <asp:Literal ID="ltUsuarioTraspaso" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="form-group">
                                            <label>Observaciones</label>
                                            <p>
                                                <asp:Literal ID="ltObservacionesTraspaso" runat="server"></asp:Literal></p>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:UpdatePanel ID="upFormularioTraspasos" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <label>Respuesta</label>
                                                    <asp:TextBox ID="txbRespuestaTraspaso" runat="server" 
                                                        TextMode="MultiLine" Rows="5" required CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:LinkButton ID="btnAutorizarTraspaso" runat="server" OnClick="btnAutorizarTraspaso_Click" 
                                                        CssClass="btn btn-info dim btn-outline"><i class="fa fa-check"></i></asp:LinkButton>
                                                    Autorizar &nbsp;&nbsp;&nbsp;

                                                    <asp:LinkButton ID="btnNoAutorizarTraspaso" runat="server" OnClick="btnNoAutorizarTraspaso_Click" 
                                                        CssClass="btn btn-warning dim btn-outline"><i class="fa fa-times"></i></asp:LinkButton> 
                                                    No autorizar
                                                </div>
                                                <div>
                                                    <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button"
                                                        onclick="window.location.href='autorizaciones'">
                                                        <strong>Cancelar</strong></button>
                                                    <asp:Button ID="btnResponderTraspaso" runat="server" 
                                                        CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right"
                                                        Text="Responder" OnClick="btnResponderTraspaso_Click" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--Cortesias--%>
                        <div class="ibox float-e-margins" id="divCortesias" runat="server">
                            <div class="ibox-title">
                                <h5><i class="fa fa-gift"></i> Autorización de Cortesía</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-sm-6 b-r">
                                        <div class="row">
                                            <div class="col-sm-6 b-r">
                                                <div class="form-group">
                                                    <label>Nombre del afiliado</label>
                                                    <p>
                                                        <asp:Literal ID="ltNombreAfiliadoCortesia" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Días de cortesía</label>
                                                    <p>
                                                        <asp:Literal ID="ltDiasCortesia" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label>Plan actual</label>
                                            <div class="row m-xs">
                                                <ul class="todo-list small-list">
                                                    <asp:Repeater ID="rpPlanAfiliadoCortesia" runat="server">
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
                                        <div class="row">
                                            <div class="col-sm-6 b-r">
                                                <div class="form-group">
                                                    <label>Fecha solicitud</label>
                                                    <p>
                                                        <asp:Literal ID="ltFechaHoraCortesia" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Usuario</label>
                                                    <p>
                                                        <asp:Literal ID="ltUsuarioCortesia" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="form-group">
                                            <label>Observaciones</label>
                                            <p>
                                                <asp:Literal ID="ltObservacionesCortesia" runat="server"></asp:Literal></p>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:UpdatePanel ID="upFormularioCortesias" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <label>Respuesta</label>
                                                    <asp:TextBox ID="txbRespuestaCortesia" runat="server" 
                                                        TextMode="MultiLine" Rows="5" required CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:LinkButton ID="btnAutorizarCortesia" runat="server" OnClick="btnAutorizarCortesia_Click"
                                                        CssClass="btn btn-info dim btn-outline"><i class="fa fa-check"></i></asp:LinkButton>
                                                    Autorizar &nbsp;&nbsp;&nbsp;

                                                    <asp:LinkButton ID="btnNoAutorizarCortesia" runat="server" OnClick="btnNoAutorizarCortesia_Click" 
                                                        CssClass="btn btn-warning dim btn-outline"><i class="fa fa-times"></i></asp:LinkButton> 
                                                    No autorizar
                                                </div>
                                                <div>
                                                    <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button"
                                                        onclick="window.location.href='autorizaciones'">
                                                        <strong>Cancelar</strong></button>
                                                    <asp:Button ID="btnResponderCortesia" runat="server" 
                                                        CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right"
                                                        Text="Responder" OnClick="btnResponderCortesia_Click" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--Congelaciones--%>
                        <div class="ibox float-e-margins" id="divCongelaciones" runat="server">
                            <div class="ibox-title">
                                <h5><i class="fa fa-snowflake"></i> Autorización de Congelaciones</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-sm-6 b-r">
                                        <div class="row">
                                            <div class="col-sm-6 b-r">
                                                <div class="form-group">
                                                    <label>Nombre del afiliado</label>
                                                    <p>
                                                        <asp:Literal ID="ltNombreAfiliadoCongelacion" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Días de congelación</label>
                                                    <p>
                                                        <asp:Literal ID="ltDiasCongelacion" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label>Plan actual</label>
                                            <div class="row m-xs">
                                                <ul class="todo-list small-list">
                                                    <asp:Repeater ID="rpPlanAfiliadoCongelacion" runat="server">
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
                                        <div class="row">
                                            <div class="col-sm-6 b-r">
                                                <div class="form-group">
                                                    <label>Fecha solicitud</label>
                                                    <p>
                                                        <asp:Literal ID="ltFechaHoraCongelacion" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Fecha de inicio de la congelación</label>
                                                    <p>
                                                        <asp:Literal ID="ltFechaInicioCongelacion" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-sm-6 b-r">
                                                <div class="form-group">
                                                    <label>Tipo de congelación</label>
                                                    <p>
                                                        <asp:Literal ID="ltTipoCongelacion" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Usuario</label>
                                                    <p>
                                                        <asp:Literal ID="ltUsuarioCongelacion" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="form-group">
                                            <label>Documento de respaldo</label>
                                            <p>
                                                <asp:Literal ID="ltDocumentoCongelacion" runat="server"></asp:Literal></p>
                                        </div>
                                        <hr />
                                        <div class="form-group">
                                            <label>Observaciones</label>
                                            <p>
                                                <asp:Literal ID="ltObservacionesCongelacion" runat="server"></asp:Literal></p>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:UpdatePanel ID="upFormularioCongelaciones" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <label>Respuesta</label>
                                                    <asp:TextBox ID="txbRespuestaCongelacion" runat="server" 
                                                        TextMode="MultiLine" Rows="5" required CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:LinkButton ID="btnAutorizarCongelacion" runat="server" OnClick="btnAutorizarCongelacion_Click" 
                                                        CssClass="btn btn-info dim btn-outline"><i class="fa fa-check"></i></asp:LinkButton>
                                                    Autorizar &nbsp;&nbsp;&nbsp;

                                                    <asp:LinkButton ID="btnNoAutorizarCongelacion" runat="server" OnClick="btnNoAutorizarCongelacion_Click" 
                                                        CssClass="btn btn-warning dim btn-outline"><i class="fa fa-times"></i></asp:LinkButton> 
                                                    No autorizar
                                                </div>
                                                <div>
                                                    <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button"
                                                        onclick="window.location.href='autorizaciones'">
                                                        <strong>Cancelar</strong></button>
                                                    <asp:Button ID="btnResponderCongelacion" runat="server" 
                                                        CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right"
                                                        Text="Responder" OnClick="btnResponderCongelacion_Click" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%--Incapacidades--%>
                        <div class="ibox float-e-margins" id="divIncapacidades" runat="server">
                            <div class="ibox-title">
                                <h5><i class="fa fa-head-side-mask"></i> Autorización de Incapacidades</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-sm-6 b-r">
                                        <div class="row">
                                            <div class="col-sm-6 b-r">
                                                <div class="form-group">
                                                    <label>Nombre del afiliado</label>
                                                    <p>
                                                        <asp:Literal ID="ltNombreAfiliadoIncapacidad" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Días de incapacidad</label>
                                                    <p>
                                                        <asp:Literal ID="ltDiasIncapacidad" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label>Plan actual</label>
                                            <div class="row m-xs">
                                                <ul class="todo-list small-list">
                                                    <asp:Repeater ID="rpPlanAfiliadoIncapacidad" runat="server">
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
                                        <div class="row">
                                            <div class="col-sm-6 b-r">
                                                <div class="form-group">
                                                    <label>Fecha solicitud</label>
                                                    <p>
                                                        <asp:Literal ID="ltFechaHoraIncapacidad" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Fecha de inicio de la incapacidad</label>
                                                    <p>
                                                        <asp:Literal ID="ltFechaInicioIncapacidad" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-sm-6 b-r">
                                                <div class="form-group">
                                                    <label>Tipo de incapacidad</label>
                                                    <p>
                                                        <asp:Literal ID="ltTipoIncapacidad" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Usuario</label>
                                                    <p>
                                                        <asp:Literal ID="ltUsuarioIncapacidad" runat="server"></asp:Literal></p>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="form-group">
                                            <label>Documento de respaldo</label>
                                            <p>
                                                <asp:Literal ID="ltDocumentoIncapacidad" runat="server"></asp:Literal></p>
                                        </div>
                                        <hr />
                                        <div class="form-group">
                                            <label>Observaciones</label>
                                            <p>
                                                <asp:Literal ID="ltObservacionesIncapacidad" runat="server"></asp:Literal></p>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:UpdatePanel ID="upFormularioIncapacidades" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <label>Respuesta</label>
                                                    <asp:TextBox ID="txbRespuestaIncapacidad" runat="server" 
                                                        TextMode="MultiLine" Rows="5" required CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <asp:LinkButton ID="btnAutorizarIncapacidad" runat="server" OnClick="btnAutorizarIncapacidad_Click" 
                                                        CssClass="btn btn-info dim btn-outline"><i class="fa fa-check"></i></asp:LinkButton>
                                                    Autorizar &nbsp;&nbsp;&nbsp;

                                                    <asp:LinkButton ID="btnNoAutorizarIncapacidad" runat="server" OnClick="btnNoAutorizarIncapacidad_Click" 
                                                        CssClass="btn btn-warning dim btn-outline"><i class="fa fa-times"></i></asp:LinkButton> 
                                                    No autorizar
                                                </div>
                                                <div>
                                                    <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button"
                                                        onclick="window.location.href='autorizaciones'">
                                                        <strong>Cancelar</strong></button>
                                                    <asp:Button ID="btnResponderIncapacidad" runat="server" 
                                                        CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right"
                                                        Text="Responder" OnClick="btnResponderIncapacidad_Click" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
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
    <script id="script">
        //
        // If absolute URL from the remote server is provided, configure the CORS
        // header on that server.
        //

        $(document).on("click", ".dropdown-toggle", function () {
            var url = $(this).data('path') + $(this).data('file');
            console.log(url);

            //var modal = $("myModal2");
            //modal.find('.modal-title').text('Prueba');
            //console.log(modal);

            document.getElementById('titulo').innerHTML = $(this).data('file');
            document.getElementById('objFile').data = url;
            document.getElementById('objEmbed').src = url;
            document.getElementById('objHref').src = url;
        });

</script>

</body>

</html>
