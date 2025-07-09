<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="traspasos.aspx.cs" Inherits="fpWebApp.traspasos" %>

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

    <title>Fitness People | Traspasos</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.3/themes/smoothness/jquery-ui.css">

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet">
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <!-- Sweet Alert -->
    <link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">

    <!-- Sweet alert -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#traspasos");
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
                    <i class="fa fa-right-left modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para traspaso de planes entre afiliados</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo transferir un plan de un afiliado a otro de forma fácil y segura.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Busca al afiliado origen</b><br />
                        Usa el buscador izquierdo para encontrar al afiliado que cede el plan:<br />
                        <i class="fa-solid fa-magnifying-glass"></i> Filtra por: 
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i> <b>Nombre</b>, 
                        <i class="fa-solid fa-address-card" style="color: #0D6EFD;"></i> <b>Cédula</b>, 
                        <i class="fa-solid fa-envelope" style="color: #0D6EFD;"></i> <b>Correo</b> o 
                        <i class="fa-solid fa-mobile" style="color: #0D6EFD;"></i> <b>Celular.</b><br />
                        Selecciona al afiliado para ver sus planes actuales.
                    <br />
                        <br />
                        <b>Paso 2: Busca al afiliado destino</b><br />
                        Usa el buscador derecho para ubicar al afiliado que recibirá el plan:<br />
                        <i class="fa-solid fa-magnifying-glass"></i> Filtra por: 
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i> <b>Nombre</b>, 
                        <i class="fa-solid fa-address-card" style="color: #0D6EFD;"></i> <b>Cédula</b>, 
                        <i class="fa-solid fa-envelope" style="color: #0D6EFD;"></i> <b>Correo</b> o 
                        <i class="fa-solid fa-mobile" style="color: #0D6EFD;"></i> <b>Celular.</b><br />
                        Verifica que sus datos sean correctos.
                    <br />
                        <br />
                        <b>Paso 3: Confirma el traspaso o cancela</b><br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i> <b>Traspasar: </b> Realiza la acción y finaliza el proceso.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i> <b>Cancelar:</b> Si necesitas volver atrás sin guardar modificaciones.
                    <br />
                        <br />
                        <i class="fa-solid fa-triangle-exclamation" style="color: #FFC107;"></i> <b>Importante</b><br />
                        <i class="fa-solid fa-check"></i> El afiliado origen <b>perderá acceso</b> al plan traspasado.<br />
                        <i class="fa-solid fa-check"></i> El afiliado destino <b>debe cumplir</b> con los requisitos del nuevo plan (si aplica).
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
                    <h2><i class="fa fa-right-left text-success m-r-sm"></i>Traspasos</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Afiliados</li>
                        <li class="active"><strong>Traspasos</strong></li>
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
                            <h5>Formulario para el traspaso de planes</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">

                            <div class="row">
                                <form id="form" enctype="multipart/form-data" runat="server">
                                    <asp:ScriptManager ID="sm1" runat="server"></asp:ScriptManager>
                                    <asp:UpdatePanel ID="upBusqueda" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                    <div class="col-sm-5 b-r">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label>Afiliado origen</label>
                                                    <asp:DropDownList ID="ddlAfiliadoOrigen" name="ddlAfiliadoOrigen" runat="server" 
                                                        DataTextField="DocNombreAfiliado" AppendDataBoundItems="true" 
                                                        DataValueField="idAfiliado" CssClass="chosen-select form-control input-sm"  
                                                        OnSelectedIndexChanged="ddlAfiliadoOrigen_SelectedIndexChanged" 
                                                        AutoPostBack="true" >
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">

                                                <div class="contact-box navy-bg" runat="server" id="divAfiliadoOrigen">
                                                    <div class="col-sm-4">
                                                        <div class="text-center">
                                                            <asp:Literal ID="ltFotoAfiliadoOrigen" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <h3><strong><asp:Literal ID="ltNombreAfiliadoOrigen" runat="server"></asp:Literal> <asp:Literal ID="ltApellidoAfiliadoOrigen" runat="server"></asp:Literal></strong></h3>
                                                        <p><i class="fa fa-envelope"></i> <asp:Literal ID="ltEmailAfiliadoOrigen" runat="server"></asp:Literal></p>
                                                        <address>
                                                            <i class="fa fa-mobile"></i>
                                                            <asp:Literal ID="ltCelularAfiliadoOrigen" runat="server"></asp:Literal><br />
                                                            <i class="fa fa-building"></i> Sede: 
                                                            <asp:Literal ID="ltSedeAfiliadoOrigen" runat="server"></asp:Literal><br />
                                                            <i class="fa fa-cake"></i>
                                                            <asp:Literal ID="ltCumpleAfiliadoOrigen" runat="server"></asp:Literal>
                                                        </address>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="row m-xs" runat="server" id="divPlanes">
                                                    <h4>Planes</h4>
                                                    <%--<asp:Literal ID="ltNoPlanes" runat="server"></asp:Literal>--%>
                                                    <ul class="todo-list m-t small-list">
                                                        <asp:Repeater ID="rpPlanesAfiliado" runat="server">
                                                            <ItemTemplate>
                                                                <li>
                                                                    <div class="i-checks">
                                                                        <small class="label label-primary pull-right"><%# Eval("DiasQueFaltan") %> días disponibles</small>
                                                                        <label>
                                                                            <input type="radio" value="<%# Eval("idPlan") %>" name="planes">
                                                                            <i></i><%# Eval("NombrePlan") %>, <%# Eval("Meses") %> mes(es)
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
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2" runat="server" id="divArrow">
                                        <p class="text-center"><i class="fa fa-angles-right big-icon"></i></p>
                                    </div>

                                    <div class="col-sm-5 border-left">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label>Afiliado destino</label>
                                                    <asp:DropDownList ID="ddlAfiliadoDestino" name="ddlAfiliadoDestino" runat="server" 
                                                        DataTextField="DocNombreAfiliado" AppendDataBoundItems="true" 
                                                        DataValueField="idAfiliado" CssClass="chosen-select form-control input-sm"  
                                                        OnSelectedIndexChanged="ddlAfiliadoDestino_SelectedIndexChanged" 
                                                        AutoPostBack="true">
                                                        <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" runat="server" id="divAfiliadoDestino" visible="false">
                                            <div class="col-md-12">

                                                <div class="contact-box bg-success">
                                                    <div class="col-sm-4">
                                                        <div class="text-center">
                                                            <asp:Literal ID="ltFotoAfiliadoDestino" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <h3><strong><asp:Literal ID="ltNombreAfiliadoDestino" runat="server"></asp:Literal> <asp:Literal ID="ltApellidoAfiliadoDestino" runat="server"></asp:Literal></strong></h3>
                                                        <p><i class="fa fa-envelope"></i> <asp:Literal ID="ltEmailAfiliadoDestino" runat="server"></asp:Literal></p>
                                                        <address>
                                                            <i class="fa fa-mobile"></i>
                                                            <asp:Literal ID="ltCelularAfiliadoDestino" runat="server"></asp:Literal><br />
                                                            <i class="fa fa-building"></i> Sede: 
                                                            <asp:Literal ID="ltSedeAfiliadoDestino" runat="server"></asp:Literal><br />
                                                            <i class="fa fa-calendar"></i> Afiliado desde: 
                                                            <asp:Literal ID="ltFechaAfiliacion" runat="server"></asp:Literal>
                                                            <%--<i class="fa fa-ticket"></i> Plan activo: 
                                                            <asp:Literal ID="ltPlanActivo" runat="server"></asp:Literal>--%>
                                                        </address>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Documento de traspaso</label><br />
                                                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                                                <div class="form-control input-sm" data-trigger="fileinput">
                                                                    <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                                    <span class="fileinput-filename"></span>
                                                                </div>
                                                                <span class="input-group-addon btn btn-success btn-file input-sm">
                                                                    <span class="fileinput-new input-sm">Seleccionar archivo</span>
                                                                    <span class="fileinput-exists input-sm">Cambiar</span>
                                                                    <input type="file" name="documento" id="documento" accept="application/pdf">
                                                                </span>
                                                                <a href="#" class="input-group-addon btn btn-danger fileinput-exists input-sm" 
                                                                    data-dismiss="fileinput">Quitar</a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label>Fecha inicio del plan</label>
                                                            <asp:TextBox ID="txbFechaInicio" CssClass="form-control input-sm" runat="server"></asp:TextBox>
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
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div>
                                                    <%--<asp:Literal ID="ltMensaje" runat="server"></asp:Literal>--%>
                                                    <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button" onclick="window.location.href='afiliados'"><strong>Cancelar</strong></button>
                                                    <asp:Button ID="btnTraspasar" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right" Text="Traspasar" OnClick="btnTraspasar_Click"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btnTraspasar" />
                                            <%--<asp:PostBackTrigger ControlID="btnAfiliadoDestino" />
                                            <asp:PostBackTrigger ControlID="btnAfiliadoOrigen" />--%>
                                       </Triggers>
                                    </asp:UpdatePanel>
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

    <script>

        $("#form").validate({
            rules: {
                documento: {
                    required: true
                },
                txbObservaciones: {
                    required: true,
                    minlength: 20
                },
                txbFechaInicio: {
                    required: true
                },
            },
            messages: {
                documento: "*",
            }
        });
    </script>

</body>

</html>
