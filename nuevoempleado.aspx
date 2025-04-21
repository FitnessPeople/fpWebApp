<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nuevoempleado.aspx.cs" Inherits="fpWebApp.nuevoempleado" %>

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

    <title>Fitness People | Nuevo empleado</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet">
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <!-- Sweet Alert -->
    <link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">

    <!-- Sweet alert -->
    <script src="js/plugins/sweetalert/sweetalert.min.js"></script>

    <script>
        function formatCurrency(input) {
            let value = input.value.replace(/\D/g, '');
            if (value === "") {
                input.value = "";
                return;
            }
            let formattedValue = new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP', minimumFractionDigits: 0 }).format(value);
            input.value = formattedValue;
        }

        function keepFormatted(input) {
            if (input.value.trim() === "") {
                input.value = "";
                return;
            }
            formatCurrency(input);
        }

        function getNumericValue(input) {
            return input.value.replace(/[^0-9]/g, '');
        }

        function changeClass() {
            var element1 = document.querySelector("#nuevoempleado");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#sistema");
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
                    <i class="fa fa-id-card modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para crear un empleado</h4>
                    <small class="font-bold">¡Bienvenido! Sigue estos pasos para completar el registro sin errores.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Prepara la información</b><br />
                        Asegúrate de tener estos datos del empleado a mano:<br />
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i> <b>Nombre(s), Apellido(s), Tipo y Número de Documento.</b><br />
                        <i class="fa-solid fa-calendar-days" style="color: #0D6EFD;"></i> <b>Fecha de Nacimiento, Género, Estado Civil.</b><br />
                        <i class="fa-solid fa-phone" style="color: #0D6EFD;"></i> <b>Teléfono, Email, Dirección, Ciudad.</b><br />
                        <i class="fa-solid fa-user-tie" style="color: #0D6EFD;"></i> <b>Profesión, EPS, Empresa Convenio, Sede, Foto (si aplica).</b><br />
                        <i class="fa-solid fa-user-group" style="color: #0D6EFD;"></i> <b>Nro. de Contrato, Tipo, Fecha de Inicio y Fin, Sede</b>
                    <br />
                        <br />
                        <b>Paso 2: Completa el formulario</b><br />
                        <i class="fa-solid fa-pencil"></i> Llena todos los campos obligatorios (generalmente marcados con *).<br />
                        <i class="fa-solid fa-magnifying-glass"></i> Verifica que los datos estén correctos y actualizados.
                    <br />
                        <br />
                        <b>Paso 3: Confirma o cancela</b><br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i> <b>Agregar:</b> Guarda la información y finaliza el registro.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i> <b>Cancelar:</b> Si necesitas volver atrás sin guardar cambios.
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

        <uc1:navbar runat="server" ID="navbar1" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header1" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fas fa-user-plus text-success m-r-sm"></i>Nuevo empleado</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Sistema</li>
                        <li class="active"><strong>Nuevo empleado</strong></li>
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
                                    <h5>Formulario para la creación de un nuevo empleado</h5>
                                    <div class="ibox-tools">
                                        <a class="collapse-link">
                                            <i class="fa fa-chevron-up"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="ibox-content">

                                    <div class="alert alert-danger alert-dismissable" runat="server" id="divMensaje1" visible="false">
                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        Un empleado con este documento ya existe!<br />
                                        <a class="alert-link" href="#">Intente nuevamente</a>.
                                    </div>

                                    <div class="alert alert-danger alert-dismissable" runat="server" id="divMensaje2" visible="false">
                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        Este correo ya existe.<br />
                                        <a class="alert-link" href="#">Intente nuevamente</a>.
                                    </div>

                                    <div class="alert alert-danger alert-dismissable" runat="server" id="divMensaje3" visible="false">
                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        Este teléfono ya existe.<br />
                                        <a class="alert-link" href="#">Intente nuevamente</a>.
                                    </div>

                                    <div class="row">
                                        <form role="form" id="form" enctype="multipart/form-data" runat="server">
                                            <div class="col-sm-6 b-r">
                                                <div class="form-group">
                                                    <label>Nombre Completo</label>
                                                    <asp:TextBox ID="txbNombre" CssClass="form-control input-sm" runat="server" placeholder="Nombre completo"></asp:TextBox>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Nro. de Documento</label>
                                                            <asp:TextBox ID="txbDocumento" CssClass="form-control input-sm" runat="server" placeholder="Documento"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Tipo de Documento</label>
                                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" AppendDataBoundItems="true"
                                                                DataTextField="TipoDocumento" DataValueField="idTipoDoc" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Teléfono</label>
                                                            <asp:TextBox ID="txbTelefono" CssClass="form-control input-sm" runat="server" placeholder="Teléfono"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Email</label>
                                                            <asp:TextBox ID="txbEmail" CssClass="form-control input-sm" runat="server" placeholder="Email" required></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-8">
                                                        <div class="form-group">
                                                            <label>Dirección</label>
                                                            <asp:TextBox ID="txbDireccion" CssClass="form-control input-sm" runat="server" placeholder="Dirección"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Ciudad</label>
                                                            <asp:DropDownList ID="ddlCiudadEmpleado" runat="server" 
                                                                AppendDataBoundItems="true" DataTextField="NombreCiudad" 
                                                                DataValueField="idCiudad" CssClass="chosen-select input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Fecha de Nacimiento</label>
                                                            <asp:TextBox ID="txbFechaNac" CssClass="form-control input-sm" runat="server" placeholder="Fecha nacimiento"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Cargo</label>
                                                            <asp:DropDownList ID="ddlCargo" runat="server" AppendDataBoundItems="true"
                                                                DataTextField="NombreCargo" DataValueField="idCargo" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                 <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Estado civil</label>
                                                            <asp:DropDownList ID="ddlEstadoCivil" runat="server" AppendDataBoundItems="true"
                                                                DataTextField="EstadoCivil" DataValueField="idEstadoCivil" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Género</label>
                                                            <asp:DropDownList ID="ddlGenero" runat="server" AppendDataBoundItems="true"
                                                                DataTextField="Genero" DataValueField="idGenero" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label>Foto:</label>
                                                    <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                                        <div class="form-control input-sm" data-trigger="fileinput">
                                                            <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                            <span class="fileinput-filename"></span>
                                                        </div>
                                                        <span class="input-group-addon btn btn-success btn-file input-sm">
                                                            <span class="fileinput-new input-sm">Seleccionar foto</span>
                                                            <span class="fileinput-exists input-sm">Cambiar</span>
                                                            <input type="file" name="fileFoto" id="fileFoto" accept="image/*">
                                                        </span>
                                                        <a href="#" class="input-group-addon btn btn-danger fileinput-exists input-sm" 
                                                            data-dismiss="fileinput">Quitar</a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Nro. de Contrato</label>
                                                            <asp:TextBox ID="txbContrato" CssClass="form-control input-sm" runat="server" placeholder="Contrato"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Tipo de Contrato</label>
                                                            <asp:DropDownList ID="ddlTipoContrato" runat="server" 
                                                                AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="Fijo" Value="Fijo"></asp:ListItem>
                                                                <asp:ListItem Text="OPS" Value="OPS"></asp:ListItem>
                                                                <asp:ListItem Text="Aprendiz" Value="Aprendiz"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label>Fecha inicio</label>
                                                            <asp:TextBox ID="txbFechaInicio" CssClass="form-control input-sm" runat="server" placeholder="Fecha inicio"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label>Fecha final</label>
                                                            <asp:TextBox ID="txbFechaFinal" CssClass="form-control input-sm" runat="server" placeholder="Fecha final"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Sede</label>
                                                            <asp:DropDownList ID="ddlSedes" runat="server" AppendDataBoundItems="true"
                                                                DataTextField="NombreSede" DataValueField="idSede" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Sueldo</label>
                                                            <asp:TextBox ID="txbSueldo" CssClass="form-control input-sm" runat="server" placeholder="Sueldo" onkeyup="formatCurrency(this)" onblur="keepFormatted(this)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Grupo</label>
                                                            <asp:DropDownList ID="ddlGrupo" runat="server" AppendDataBoundItems="true" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="Administrativos" Value="1 - ADMINISTRATIVOS"></asp:ListItem>
                                                                <asp:ListItem Text="Comerciales" Value="2 - COMERCIALES"></asp:ListItem>
                                                                <asp:ListItem Text="Líderes deportivos" Value="3 - LIDERES DEPORTIVOS"></asp:ListItem>
                                                                <asp:ListItem Text="Marketing Digital" Value="5 - MARKETING DIGITAL"></asp:ListItem>
                                                                <asp:ListItem Text="Fisioterapeuta y nutricionista" Value="6 - FISIOTERAPEUTA Y NUTRICIONISTA"></asp:ListItem>
                                                                <asp:ListItem Text="Profesor planta" Value="7 - PROFESOR PLANTA"></asp:ListItem>
                                                                <asp:ListItem Text="Practicantes" Value="9 - PRACTICANTES"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>EPS</label>
                                                            <asp:DropDownList ID="ddlEps" runat="server" AppendDataBoundItems="true"
                                                                DataTextField="NombreEps" DataValueField="idEps" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Fondo de pensión</label>
                                                            <asp:DropDownList ID="ddlFondoPension" runat="server" AppendDataBoundItems="true"
                                                                DataTextField="NombreFondoPension" DataValueField="idFondoPension" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>ARL</label>
                                                            <asp:DropDownList ID="ddlArl" runat="server" AppendDataBoundItems="true"
                                                                DataTextField="NombreArl" DataValueField="idArl" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Caja de compensación</label>
                                                            <asp:DropDownList ID="ddlCajaComp" runat="server" AppendDataBoundItems="true"
                                                                DataTextField="NombreCajaComp" DataValueField="idCajaComp" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Cesantías</label>
                                                            <asp:DropDownList ID="ddlCesantias" runat="server" AppendDataBoundItems="true"
                                                                DataTextField="NombreCesantias" DataValueField="idCesantias" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                           <div class="form-group">
                                                            <label>Canal de ventas</label>
                                                               <asp:DropDownList ID="ddlCanalVenta" runat="server"
                                                                 DataTextField="NombreCanalVenta" DataValueField="idCanalVenta" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                               </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Empresa FP</label>
                                                            <asp:DropDownList ID="ddlempresasFP" runat="server" AppendDataBoundItems="true"
                                                                DataTextField="NombreEmpresaFP" DataValueField="idEmpresaFP" CssClass="form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                                    <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button" onclick="window.location.href='empleados'"><strong>Cancelar</strong></button>
                                                    <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right" Text="Agregar" OnClick="btnAgregar_Click" />
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>

                            <%--Fin Contenido!!!!--%>
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

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <script>

        $.validator.setDefaults({ ignore: ":hidden:not(.chosen-select)" });

        $("#form").validate({
            rules: {
                txbNombre: {
                    required: true,
                    minlength: 3
                },
                txbDocumento: {
                    required: true,
                    minlength: 5
                },
                ddlTipoDocumento: {
                    required: true
                },
                txbTelefono: {
                    required: true,
                    minlength: 10
                },
                txbDireccion: {
                    required: true,
                    minlength: 10
                },
                ddlCiudadEmpleado: {
                    required: true
                },
                txbFechaNac: {
                    required: true,
                },
                txbCiudad: {
                    required: true,
                    minlength: 4
                },
                txbCargo: {
                    required: true,
                    minlength: 5
                },
                txbContrato: {
                    required: true,
                    minlength: 5
                },
                ddlTipoContrato: {
                    required: true
                },
                txbFechaInicio: {
                    required: true
                },
                txbFechaFinal: {
                    required: true
                },
                ddlSedes: {
                    required: true
                },
                txbSueldo: {
                    required: true
                },
                ddlGrupo: {
                    required: true
                },
                ddlEps: {
                    required: true
                },
                ddlFondoPension: {
                    required: true
                },
                ddlArl: {
                    required: true
                },
                ddlCajaComp: {
                    required: true
                },
                ddlCesantias: {
                    required: true
                },
                ddlCanalVenta: {
                    required: true
                },
            },
            messages: {
                ddlCiudadEmpleado: "*",
            }
        });

        $('.chosen-select').chosen({ width: "100%", disable_search_threshold: 10, no_results_text: "Sin resultados" });
    </script>

</body>

</html>
