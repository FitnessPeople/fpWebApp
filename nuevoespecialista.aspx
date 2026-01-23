<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nuevoespecialista.aspx.cs" Inherits="fpWebApp.nuevoespecialista" %>

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

    <title>Fitness People | Nuevo especialista</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/dropzone/basic.css" rel="stylesheet" />
    <link href="css/plugins/dropzone/dropzone.css" rel="stylesheet" />
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet" />

    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />
    <link href="css/plugins/colorpicker/bootstrap-colorpicker.min.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#nuevoespecialista");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#medico");
            element2.classList.remove("collapse");
        }
    </script>
</head>

<body onload="changeClass()">
    <div id="wrapper">

        <uc1:navbar runat="server" ID="navbar1" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header1" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa fa-user-doctor text-success m-r-sm"></i>Nuevo especialista</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Asistencial</li>
                        <li class="active"><strong>Especialistas</strong></li>
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
                                    <h5>Formulario para la creación de un nuevo especialista</h5>
                                    <div class="ibox-tools">
                                        <a class="collapse-link">
                                            <i class="fa fa-chevron-up"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="ibox-content">

                                    <div class="alert alert-danger alert-dismissable" runat="server" id="divMensaje1" visible="false">
                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        Un especialista con este documento ya existe!<br />
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

                                    <div class="alert alert-danger alert-dismissable" runat="server" id="divMensaje4" visible="false">
                                        <button aria-hidden="true" data-dismiss="alert" class="close" type="button">×</button>
                                        La persona debe tener 14 años o mas.<br />
                                        <a class="alert-link" href="#">Intente nuevamente</a>.
                                    </div>

                                    <div class="row">
                                        <form role="form" id="form" enctype="multipart/form-data" runat="server">
                                            <div class="col-sm-6 b-r">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Nombre(s)</label>
                                                            <asp:TextBox ID="txbNombre" CssClass="form-control input-sm" runat="server" placeholder="Nombre(s)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Apellido(s)</label>
                                                            <asp:TextBox ID="txbApellido" CssClass="form-control input-sm" runat="server" placeholder="Apellido(s)"></asp:TextBox>
                                                        </div>
                                                    </div>
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
                                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" AppendDataBoundItems="true" DataTextField="TipoDocumento" DataValueField="idTipoDoc" CssClass="form-control input-sm m-b">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Teléfono</label>
                                                            <asp:TextBox ID="txbTelefono" CssClass="form-control input-sm" runat="server" placeholder="Teléfono"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Email</label>
                                                            <asp:TextBox ID="txbEmail" CssClass="form-control input-sm" runat="server" placeholder="Email" required></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Color para agenda</label>
                                                            <a data-color="rgb(255, 255, 255)" id="demo_apidemo" 
                                                                class="btn btn-white btn-block colorpicker-element back-change input-sm" 
                                                                style="font-size: 12px;" href="#">Color</a>
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
                                                            <span class="fileinput-new input-sm">Seleccionar archivo</span>
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
                                                    <div class="col-sm-8">
                                                        <div class="form-group">
                                                            <label>Dirección</label>
                                                            <asp:TextBox ID="txbDireccion" CssClass="form-control input-sm" runat="server" placeholder="Dirección"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Ciudad</label>
                                                            <asp:DropDownList ID="ddlCiudadEspecialista" runat="server" 
                                                                AppendDataBoundItems="true" DataTextField="NombreCiudad" 
                                                                DataValueField="idCiudad" CssClass="chosen-select form-control input-sm">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Fecha nacimiento</label>
                                                            <asp:TextBox ID="txbFechaNac" CssClass="form-control input-sm" runat="server" name="txbFechaNac"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Genero</label>
                                                            <asp:DropDownList ID="ddlGenero" runat="server" AppendDataBoundItems="true" DataTextField="Genero" DataValueField="idGenero" CssClass="form-control input-sm m-b">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Estado civil</label>
                                                            <asp:DropDownList ID="ddlEstadoCivil" runat="server" AppendDataBoundItems="true" DataTextField="EstadoCivil" DataValueField="idEstadoCivil" CssClass="form-control input-sm m-b">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Profesión</label>
                                                            <asp:DropDownList ID="ddlProfesiones" runat="server" AppendDataBoundItems="true" DataTextField="Profesion" DataValueField="idProfesion" CssClass="form-control input-sm m-b">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>EPS</label>
                                                            <asp:DropDownList ID="ddlEps" runat="server" AppendDataBoundItems="true" DataTextField="NombreEps" DataValueField="idEps" CssClass="form-control input-sm m-b">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Sede</label>
                                                            <asp:DropDownList ID="ddlSedes" runat="server" AppendDataBoundItems="true" DataTextField="NombreSede" DataValueField="idSede" CssClass="form-control input-sm m-b">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div>
                                                    <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button" onclick="window.location.href='especialistas'"><strong>Cancelar</strong></button>
                                                    <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right" Text="Agregar" OnClick="btnAgregar_Click" />
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

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <!-- Color picker -->
    <script src="js/plugins/colorpicker/bootstrap-colorpicker.min.js"></script>

    <script>
        var divStyle = $('.back-change')[0].style;
        $('#demo_apidemo').colorpicker({
            color: divStyle.backgroundColor
        }).on('changeColor', function (ev) {
            divStyle.backgroundColor = ev.color.toHex();
        });

        $.validator.setDefaults({ ignore: ":hidden:not(.chosen-select)" })

        $("#form").validate({
            rules: {
                txbNombre: {
                    required: true,
                    minlength: 3
                },
                txbApellido: {
                    required: true,
                    minlength: 3
                },
                txbDocumento: {
                    required: true,
                    minlength: 7
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
                ddlCiudadEspecialista: {
                    required: true
                },
                ddlGenero: {
                    required: true
                },
                txbFechaNac: {
                    required: true
                },
                txbCiudad: {
                    required: true,
                    minlength: 4
                },
                ddlEstadoCivil: {
                    required: true,
                },
                ddlProfesiones: {
                    required: true,
                },
                ddlEps: {
                    required: true,
                },
                ddlSedes: {
                    required: true,
                },
            },
            messages: {
                ddlCiudadEspecialista: "*",
                ddlTipoDocumento
            }
        });

        $('.chosen-select').chosen({ width: "100%", disable_search_threshold: 10, no_results_text: "Sin resultados" });
    </script>

</body>

</html>
