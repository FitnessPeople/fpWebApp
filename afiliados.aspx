<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="afiliados.aspx.cs" Inherits="fpWebApp.afiliados" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/indicadores02.ascx" TagPrefix="uc1" TagName="indicadores02" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Afiliados</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />--%>
    <link href="css/plugins/footable/footable.bootstrap.css" rel="stylesheet" />

    <!-- Morris -->
    <link href="css/plugins/morris/morris-0.4.3.min.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <style type="text/css" media="print">
        body {
            visibility: hidden;
            display: none
        }
    </style>

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
                    <i class="fa fa-id-card modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para visualizar los afiliados registrados</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar el listado de forma rápida y sencilla.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Busca y filtra afiliados</b><br />
                        Usa el buscador para <b>encontrar</b> afiliados específicos.<br />
                        <i class="fa-solid fa-magnifying-glass"></i> Filtra por: 
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i> <b>Nombre</b>, 
                        <i class="fa-solid fa-address-card" style="color: #0D6EFD;"></i> <b>Cédula</b>, 
                        <i class="fa-solid fa-envelope" style="color: #0D6EFD;"></i> <b>Correo</b> o 
                        <i class="fa-solid fa-mobile" style="color: #0D6EFD;"></i> <b>Celular.</b><br />
                        Además, selecciona una <i class="fa-solid fa-map-pin" style="color: #DC3545;"></i> Sede en el menú desplegable para <b>filtrar</b> por ubicación.<br />
                        <i class="fa-solid fa-star" style="color: #FECE32;"></i> Tip: ¡<b>Combina</b> filtros para resultados más precisos!
                    <br />
                        <br />
                        <b>Paso 2: Revisa la tabla de resultados</b><br />
                        La tabla muestra toda la <b>información clave</b> de cada afiliado.<br />
                        En la columna <b>Acciones</b> encontrarás estas opciones:<br />
                        <i class="fa fa-edit" style="color: #1AB394;"></i> <b>Editar:</b> Modifica los <b>datos</b> del afiliado.<br />
                        <i class="fa fa-trash" style="color: #DC3545;"></i> <b>Eliminar:</b> Da de <b>baja</b> al afiliado.<br />
                        <i class="fa fa-ticket" style="color: #23C6C8;"></i> <b>Asignar Plan:</b> Selecciona o <b>cambia</b> su plan actual.<br />
                        <i class="fa fa-right-left" style="color: #F8AC59;"></i> <b>Traspaso:</b> Transfiere el plan <b>de un afiliado a otro</b>.<br />
                        <i class="fa fa-gift" style="color: #0D6EFD;"></i> <b>Cortesía:</b> Asigna <b>beneficios</b> especiales.<br />
                        <i class="fa fa-person-walking" style="color: #0D6EFD;"></i> <b>FreePass:</b> Otorga <b>acceso</b> temporal.<br />
                        <i class="fa fa-head-side-mask" style="color: #1AB394;"></i> <b>Incapacidad:</b> Registra una incapacidad <b>médica</b>.<br />
                        <i class="fa fa-snowflake" style="color: #0D6EFD;"></i> <b>Congelación:</b> Pausa <b>temporalmente</b> su membresía.
                    <br />
                        <br />
                        <b>Paso 3: Acciones adicionales</b><br />
                        Al lado opuesto del buscador <b>encontrarás</b> dos botones útiles:<br />
                        <i class="fa-solid fa-file-excel fa-lg" style="color: #23C6C8;"></i> <b>Excel:</b> 
                        Genera un archivo Excel con los <b>datos</b> visibles en la tabla.<br />
                        <i class="fa-solid fa-square-plus fa-lg" style="color: #1A7BB9;"></i> <b>Nuevo:</b> 
                        Te lleva a un formulario para <b>registrar</b> un nuevo afiliado.
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
                    <h2><i class="fa fa-id-card text-success m-r-sm"></i>Afiliados</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Afiliados</li>
                        <li class="active"><strong>Afiliados</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">
                    
                    <%--Inicio Contenido!!!!--%>
                    <uc1:indicadores02 runat="server" ID="indicadores02" />

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
                            <h5>Lista de afiliados recientes</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">

                            <div class="row">
                                <form runat="server" id="form1">
                                    <div class="col-lg-4 form-horizontal">
                                        <div class="input-group">
                                            <input type="text" placeholder="Nombre / Cédula / Email / Móvil" class="input form-control input-sm" name="txbBuscar" id="txbBuscar" runat="server">
                                            <span class="input-group-btn">
                                                <button type="button" id="btnBuscar" name="btnBuscar" onserverclick="btnBuscar_Click" class="btn btn btn-primary btn-sm" runat="server"><i class="fa fa-search m-r-sm"></i>Buscar</button>
                                            </span>
                                        </div>
                                    </div>

                                    <div class="col-lg-4 form-horizontal">
                                        <div class="form-group">
                                        <label class="col-lg-2 control-label">Sede:</label>
                                        <div class="col-lg-10">
                                        <asp:DropDownList ID="ddlSedes" runat="server" AppendDataBoundItems="true" 
                                            DataTextField="NombreSede" DataValueField="idSede" 
                                            CssClass="form-control input-sm m-b" OnSelectedIndexChanged="ddlSedes_SelectedIndexChanged" 
                                            AutoPostBack="true">
                                            <asp:ListItem Text="Todas" Value="todas"></asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-4 form-horizontal">
                                        <label class="control-label">&nbsp;</label>
                                        <a class="btn btn-success pull-right dim m-l-md" style="font-size: 12px;" 
                                            href="nuevoafiliado" title="Agregar afiliado" 
                                            runat="server" id="btnAgregar" visible="false">
                                                <i class="fa fa-square-plus"></i> NUEVO
                                        </a>
                                        <a class="btn btn-info pull-right dim m-l-md" style="font-size: 12px;" 
                                            target="_blank" runat="server" id="btnExportar" 
                                            href="imprimirafiliados" visible="false" title="Exportar">
                                                <i class="fa fa-file-excel"></i> EXCEL
                                        </a>
                                    </div>
                                </form>
                            </div>

                            <%--<table class="footable table toggle-arrow-small list-group-item-text" data-page-size="10">--%>
                            <table class="footable table table-striped list-group-item-text" data-paging-size="10" 
                                data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}" data-paging-limit="10" 
                                data-empty="Sin resultados" data-toggle-column="first">
                                <thead>
                                    <tr>
                                        <%--<th data-sort-ignore="true">ID</th>--%>
                                        <th data-sortable="false" data-breakpoints="xs" style="width: 110px;">Documento</th>
                                        <th data-sortable="false" data-breakpoints="xs">Nombre</th>
                                        <th data-breakpoints="xs sm md" style="width: 110px;">Télefono</th>
                                        <th data-breakpoints="xs sm md">Correo</th>
                                        <th data-type="date" data-breakpoints="xs sm md">Fecha nacimiento</th>
                                        <th class="text-nowrap" data-breakpoints="xs">Estado Afiliado</th>
                                        <th class="text-nowrap" data-breakpoints="xs">Estado Plan</th>
                                        <th data-breakpoints="all" data-title="Info"></th>
                                        <th data-sortable="false" class="text-right" style="width: 206px;">Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rpAfiliados" runat="server" OnItemDataBound="rpAfiliados_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <%--<td class="text-nowrap"><b>ID:</b> <%# Eval("idAfiliado") %></td>--%>
                                                <td><a href="detalleafiliado?search=<%# Eval("DocumentoAfiliado") %>"><%# Eval("DocumentoAfiliado") %></a></td>
                                                <%--<td><%# Eval("DocumentoAfiliado") %></td>--%>
                                                <td><%# Eval("NombreAfiliado") %> <%# Eval("ApellidoAfiliado") %></td>
                                                <td><i class="fab fa-whatsapp m-r-xs font-bold"></i><a href="https://wa.me/57<%# Eval("CelularAfiliado") %>" target="_blank"><%# Eval("CelularAfiliado") %></a></td>
                                                <td style="white-space: nowrap;"><i class="fa fa-envelope m-r-xs font-bold"></i><%# Eval("EmailAfiliado") %></td>
                                                <td style="white-space: nowrap;"><i class="fa fa-cake m-r-xs font-bold"></i><span class="text-<%# Eval("badge") %> font-bold"><%# Eval("FechaNacAfiliado", "{0:dd MMM yyyy}") %> <%# Eval("edad") %> <i class="fa fa-<%# Eval("age") %>"></i></span></td>
                                                <td><span class="badge badge-<%# Eval("badge2") %>"><%# Eval("EstadoAfiliado") %></span></td>
                                                <td><span class="badge badge-<%# Eval("badge3") %>"><%# Eval("EstadoPlan") %></span></td>
                                                <td>
                                                    <table class="table table-bordered table-striped">
                                                        <tr>
                                                            <th width="34%" colspan="2"><i class="fa fa-map-location-dot m-r-xs"></i>Dirección</th>
                                                            <th width="33%"><i class="fa fa-city m-r-xs"></i>Ciudad</th>
                                                            <th width="33%" class="text-nowrap"><i class="fa fa-venus-mars m-r-xs"></i>Genero</th>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2"><%# Eval("DireccionAfiliado") %></td>
                                                            <td><%# Eval("NombreCiudad") %> - <%# Eval("NombreEstado") %></td>
                                                            <td><%# Eval("Genero") %></td>
                                                        </tr>
                                                        <tr>
                                                            <th width="25%"><i class="fa fa-ring m-r-xs"></i>Estado Civil</th>
                                                            <th width="25%"><i class="fa fa-school-flag m-r-xs"></i>Sede</th>
                                                            <th width="25%"><i class="fa fa-user-tie m-r-xs"></i>Profesión</th>
                                                            <th width="25%"><i class="fa fa-house-medical m-r-xs"></i>EPS</th>
                                                        </tr>
                                                        <tr>
                                                            <td><%# Eval("EstadoCivil") %></td>
                                                            <td><%# Eval("NombreSede") %> - <%# Eval("NombreCiudadSede") %></td>
                                                            <td><%# Eval("Profesion") %></td>
                                                            <td><%# Eval("NombreEps") %></td>
                                                        </tr>
                                                        <tr>
                                                            <th width="33%"><i class="fa fa-person m-r-xs"></i>Responsable</th>
                                                            <th width="33%"><i class="fa fa-user-group m-r-xs"></i>Parentesco</th>
                                                            <th width="34%" colspan="2"><i class="fa fa-mobile m-r-xs"></i>Contacto responsable</th>
                                                        </tr>
                                                        <tr>
                                                            <td><%# Eval("ResponsableAfiliado") %></td>
                                                            <td><%# Eval("Parentesco") %></td>
                                                            <td><a href="https://wa.me/57<%# Eval("ContactoAfiliado") %>" target="_blank"><%# Eval("ContactoAfiliado") %></a></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <button runat="server" id="btnEditar" class="btn btn-outline btn-primary pull-left m-r-xs"
                                                        style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false"
                                                        title="Editar afiliado">
                                                        <i class="fa fa-edit"></i>
                                                    </button>
                                                    <button runat="server" id="btnEliminar" class="btn btn-outline btn-danger pull-left m-r-xs"
                                                        style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false"
                                                        title="Eliminar afiliado">
                                                        <i class="fa fa-trash"></i>
                                                    </button>
                                                    <button runat="server" id="btnPlan" class="btn btn-outline btn-info pull-left m-r-xs"
                                                        style="padding: 1px 2px 1px 2px; margin-bottom: 0px;"
                                                        title="Planes" visible="false">
                                                        <i class="fa fa-ticket"></i>
                                                    </button>
                                                    <button runat="server" id="btnTraspaso" class="btn btn-outline btn-warning pull-left m-r-xs"
                                                        style="padding: 1px 2px 1px 2px; margin-bottom: 0px;"
                                                        title="Traspaso" visible="false">
                                                        <i class="fa fa-right-left"></i>
                                                    </button>
                                                    <button runat="server" id="btnCortesia" class="btn btn-outline btn-success pull-left m-r-xs"
                                                        style="padding: 1px 2px 1px 2px; margin-bottom: 0px;"
                                                        title="Cortesía" visible="false">
                                                        <i class="fa fa-gift"></i>
                                                    </button>
                                                    <button runat="server" id="btnFreePass" class="btn btn-outline btn-success pull-left m-r-xs"
                                                        style="padding: 1px 2px 1px 2px; margin-bottom: 0px;"
                                                        title="FreePass" visible="false">
                                                        <i class="fa fa-person-walking"></i>
                                                    </button>
                                                    <button runat="server" id="btnIncapacidad" class="btn btn-outline btn-primary pull-left m-r-xs"
                                                        style="padding: 1px 2px 1px 2px; margin-bottom: 0px;"
                                                        title="Incapacidad" visible="false">
                                                        <i class="fa fa-head-side-mask"></i>
                                                    </button>
                                                    <button runat="server" id="btnCongelacion" class="btn btn-outline btn-success pull-left"
                                                        style="padding: 1px 2px 1px 2px; margin-bottom: 0px;"
                                                        title="Congelación" visible="false">
                                                        <i class="fa fa-snowflake"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>

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

    <!-- FooTable -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

    <!-- Peity -->
    <script src="js/plugins/peity/jquery.peity.min.js"></script>
    <script src="js/demo/peity-demo.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- jQuery UI -->
    <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();

        document.querySelector("input#txbBuscar").addEventListener("input", function () {
            const allowedCharacters = "0123456789azertyuiopqsdfghjklmwxcvbnAZERTYUIOPQSDFGHJKLMWXCVBNzáéíóúñÁÉÍÓÚÑ@. "; // You can add any other character in the same way

            this.value = this.value.split('').filter(char => allowedCharacters.includes(char)).join('')
        });

        $(document).ready(function () {
            $('#txbBuscar').keypress(function (e) {
                if (e.keyCode == 13)
                    $('#btnBuscar').click();
            });
        });

    </script>

</body>

</html>