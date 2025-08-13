<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="asignacionescrm.aspx.cs" Inherits="fpWebApp.asignacionescrm" %>

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

    <title>Fitness People | Asignaciones CRM</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

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

    <style type="text/css">
        .paginador {
            background-color: #f1f1f1;
            text-align: center;
            padding: 10px;
            /*font-weight: bold;*/
        }

        /* Enlaces normales */
        .paginador a {
            /*margin: 0 5px;
        padding: 5px 10px;
        color: #007bff;
        text-decoration: none;
        border: 1px solid #ccc;
        border-radius: 5px;*/
            background-color: #FFFFFF;
            border: 1px solid #DDDDDD;
            color: inherit;
            float: left;
            line-height: 1.42857;
            margin-left: -1px;
            padding: 4px 7px;
            position: relative;
            text-decoration: none;
        }

        /* Enlace activo (página actual) */
        .paginador span {
            /*margin: 0 5px;
        padding: 5px 10px;
        color: white;
        background-color: #007bff;
        border-radius: 5px;*/
            background-color: #FFFFFF;
            border: 1px solid #DDDDDD;
            color: inherit;
            float: left;
            line-height: 1.42857;
            margin-left: -1px;
            padding: 4px 7px;
            position: relative;
            text-decoration: none;
        }

        /* Hover en los enlaces */
        .paginador a:hover {
            background-color: #e0e0e0;
            border-color: #999;
        }
    </style>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#asignacionescrm");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#crm");
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
                        <i class="fa-solid fa-magnifying-glass"></i>Filtra por: 
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i><b>Nombre</b>, 
                        <i class="fa-solid fa-address-card" style="color: #0D6EFD;"></i><b>Cédula</b>, 
                        <i class="fa-solid fa-envelope" style="color: #0D6EFD;"></i><b>Correo</b> o 
                        <i class="fa-solid fa-mobile" style="color: #0D6EFD;"></i><b>Celular.</b><br />
                        Además, selecciona una <i class="fa-solid fa-map-pin" style="color: #DC3545;"></i>Sede en el menú desplegable para <b>filtrar</b> por ubicación.<br />
                        <i class="fa-solid fa-star" style="color: #FECE32;"></i>Tip: ¡<b>Combina</b> filtros para resultados más precisos!
                    <br />
                        <br />
                        <b>Paso 2: Revisa la tabla de resultados</b><br />
                        La tabla muestra toda la <b>información clave</b> de cada afiliado.<br />
                        En la columna <b>Acciones</b> encontrarás estas opciones:<br />
                        <i class="fa fa-edit" style="color: #1AB394;"></i><b>Editar:</b> Modifica los <b>datos</b> del afiliado.<br />
                        <i class="fa fa-trash" style="color: #DC3545;"></i><b>Eliminar:</b> Da de <b>baja</b> al afiliado.<br />
                        <i class="fa fa-ticket" style="color: #23C6C8;"></i><b>Asignar Plan:</b> Selecciona o <b>cambia</b> su plan actual.<br />
                        <i class="fa fa-right-left" style="color: #F8AC59;"></i><b>Traspaso:</b> Transfiere el plan <b>de un afiliado a otro</b>.<br />
                        <i class="fa fa-gift" style="color: #0D6EFD;"></i><b>Cortesía:</b> Asigna <b>beneficios</b> especiales.<br />
                        <i class="fa fa-head-side-mask" style="color: #1AB394;"></i><b>Incapacidad:</b> Registra una incapacidad <b>médica</b>.<br />
                        <i class="fa fa-snowflake" style="color: #0D6EFD;"></i><b>Congelación:</b> Pausa <b>temporalmente</b> su membresía.
                    <br />
                        <br />
                        <b>Paso 3: Acciones adicionales</b><br />
                        Al lado opuesto del buscador <b>encontrarás</b> dos botones útiles:<br />
                        <i class="fa-solid fa-file-excel fa-lg" style="color: #23C6C8;"></i><b>Excel:</b>
                        Genera un archivo Excel con los <b>datos</b> visibles en la tabla.<br />
                        <i class="fa-solid fa-square-plus fa-lg" style="color: #1A7BB9;"></i><b>Nuevo:</b>
                        Te lleva a un formulario para <b>registrar</b> un nuevo afiliado.
                   <br />
                        <br />
                        <i class="fa fa-exclamation-circle mr-2"></i>Si tienes dudas, no dudes en consultar con el administrador del sistema.
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
                    <h2><i class="fa fa-id-card text-success m-r-sm"></i>Asignaciones CRM</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>CRM</li>
                        <li class="active"><strong>Asignaciones</strong></li>
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
                            <h5>Lista de afiliados</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <form runat="server" id="form1">
                                <asp:ScriptManager ID="sm1" runat="server">
                                    <Scripts>
                                        <asp:ScriptReference Path="https://cdn.jsdelivr.net/npm/sweetalert2@11"></asp:ScriptReference>
                                    </Scripts>
                                </asp:ScriptManager>

                                <asp:UpdatePanel ID="upContactos" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <div class="row">

                                            <div class="col-lg-6 form-horizontal">
                                                <div class="form-group">
                                                    <label class="col-lg-2 control-label">Canal de venta:</label>
                                                    <div class="col-lg-10">
                                                        <asp:DropDownList ID="ddlCanalVenta" runat="server" 
                                                            AppendDataBoundItems="true"
                                                            DataTextField="NombreCanalVenta" 
                                                            CssClass="form-control input-sm m-b" 
                                                            DataValueField="idCanalVenta" 
                                                            OnSelectedIndexChanged="ddlCanalVenta_SelectedIndexChanged" 
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-lg-2 control-label">Días:</label>
                                                    <div class="col-lg-10">
                                                        <asp:DropDownList ID="ddlDias" runat="server" AppendDataBoundItems="true"
                                                            CssClass="form-control input-sm m-b" OnSelectedIndexChanged="ddlDias_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                            <asp:ListItem Text="Todos" Value=""></asp:ListItem>
                                                            <asp:ListItem Text="Más de 30 días vencido" Value="-30"></asp:ListItem>
                                                            <asp:ListItem Text="Menos de 30 días vencido" Value="30"></asp:ListItem>
                                                            <asp:ListItem Text="Más de 30 días por vencer" Value="31"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-lg-2 control-label">Mostrar</label>
                                                    <div class="col-lg-10">
                                                        <asp:RadioButtonList ID="rblPageSize" runat="server" AutoPostBack="true"
                                                            RepeatDirection="Horizontal" 
                                                            OnSelectedIndexChanged="rblPageSize_SelectedIndexChanged">
                                                            <asp:ListItem Text="&nbsp;10" Value="10" Selected="True" />
                                                            <asp:ListItem Text="&nbsp;50" Value="50" />
                                                            <asp:ListItem Text="&nbsp;100" Value="100" />
                                                            <asp:ListItem Text="&nbsp;Todos" Value="0" />
                                                        </asp:RadioButtonList>
                                                        <asp:Label ID="lblTotalRegistros" runat="server" CssClass="total-registros" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-6 form-horizontal">
                                                <div class="form-group">
                                                    <label class="col-lg-4 control-label">Asesor comercial:</label>
                                                    <div class="col-lg-8">
                                                        <asp:DropDownList ID="ddlAsesores" runat="server" AppendDataBoundItems="true"
                                                            DataTextField="NombreUsuario" DataValueField="idUsuario"
                                                            CssClass="form-control input-sm m-b">
                                                            <asp:ListItem Text="Seleccione" Value="" Selected="True" />
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvAsesor" runat="server" 
                                                            ControlToValidate="ddlAsesores" ErrorMessage="Campo requerido." InitialValue="" 
                                                            EnableClientScript="true" CssClass="text-danger font-bold" ValidationGroup="asignar">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-lg-2 control-label">&nbsp;</label>
                                                    <div class="col-lg-10">
                                                        <a class="btn btn-info pull-right dim m-l-md" style="font-size: 12px;"
                                                            target="_blank" runat="server" id="btnExportar"
                                                            href="imprimirafiliados" visible="false" title="Exportar">
                                                            <i class="fa fa-file-excel m-r-xs"></i>EXCEL
                                                        </a>
                                                        <asp:LinkButton ID="lnkAsignar" runat="server" Style="font-size: 12px;"
                                                            CssClass="btn btn-primary pull-right dim m-l-md" Visible="true"
                                                            OnClick="lnkAsignar_Click" CausesValidation="true" ValidationGroup="asignar">
                                                        <i class="fa fa-user-plus m-r-xs"></i>ASIGNAR
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <!-- Imagen de carga -->
                                                    <div id="divCargando" style="display:none; text-align:center;">
                                                        <div class="sk-spinner sk-spinner-cube-grid">
                                                            <div class="sk-cube"></div>
                                                            <div class="sk-cube"></div>
                                                            <div class="sk-cube"></div>
                                                            <div class="sk-cube"></div>
                                                            <div class="sk-cube"></div>
                                                            <div class="sk-cube"></div>
                                                            <div class="sk-cube"></div>
                                                            <div class="sk-cube"></div>
                                                            <div class="sk-cube"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <asp:GridView ID="gvAfiliados" runat="server" AutoGenerateColumns="False" 
                                            AllowPaging="True" PageSize="10" OnPageIndexChanging="gvAfiliados_PageIndexChanging" 
                                            AllowSorting="true" OnSorting="gvAfiliados_Sorting" 
                                            OnRowCreated="gvAfiliados_RowCreated" 
                                            OnRowDataBound="gvAfiliados_RowDataBound" 
                                            CssClass="table table-striped list-group-item-text" 
                                            DataKeyNames="IdAfiliado,NombreAfiliado,ApellidoAfiliado,DocumentoAfiliado,idTipoDocumento,CelularAfiliado,diasquefaltan"
                                            BorderStyle="None" GridLines="None"
                                            PagerSettings-Mode="NumericFirstLast"
                                            PagerSettings-FirstPageText="«"
                                            PagerSettings-LastPageText="»"
                                            PagerStyle-CssClass="paginador">
                                            <Columns>
                                                <%--Columna de CheckBox--%>
                                                <asp:TemplateField HeaderText="Seleccionar">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSeleccionarTodo" runat="server" />
                                                        Todos
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--Otras columnas--%>
                                                <asp:BoundField DataField="IdAfiliado" HeaderText="ID" Visible="false" />
                                                <asp:BoundField DataField="NombreAfiliado" HeaderText="Nombres" 
                                                    SortExpression="NombreAfiliado" />
                                                <asp:BoundField DataField="ApellidoAfiliado" HeaderText="Apellidos" 
                                                    SortExpression="ApellidoAfiliado" />
                                                <asp:BoundField DataField="DocumentoAfiliado" HeaderText="Documento" 
                                                    SortExpression="DocumentoAfiliado" />
                                                <asp:BoundField DataField="idTipoDocumento" HeaderText="TipoDocumento" 
                                                    SortExpression="idTipoDocumento" Visible="False" />
                                                <asp:BoundField DataField="CelularAfiliado" HeaderText="Celular" 
                                                    SortExpression="CelularAfiliado" />
                                                <asp:BoundField DataField="diasquefaltan" HeaderText="Días plan" 
                                                    SortExpression="diasquefaltan" />
                                                <asp:TemplateField HeaderText="Estado" SortExpression="EstadoPlan">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEstado" runat="server" Text='<%# Eval("EstadoPlan") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </form>
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
        $('.footable').footable({
            "paging": {
                "size": 10
            }
        });

        function seleccionarTodos(chkHeader) {
            var grid = document.getElementById('<%= gvAfiliados.ClientID %>');
            var inputs = grid.getElementsByTagName("input");

            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type === "checkbox" && inputs[i] !== chkHeader) {
                    inputs[i].checked = chkHeader.checked;
                }
            }
        }

        function seleccionarCheckbox(fila, event) {
            if (event.target.type !== 'checkbox') {
                var chk = fila.querySelector("input[type='checkbox']");
                if (chk) {
                    chk.checked = !chk.checked;
                }
            }
        }

        var table = FooTable.get(".footable"); // Reemplaza con el ID o selector de tu tabla

        if (table) {
            console.log(table);
            table.options.paging.limit = 20; // Cambia el valor a lo que necesites
            table.draw(); // Redibuja la tabla con la nueva configuración
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_beginRequest(function () {
            document.getElementById("divCargando").style.display = "block";
        });

        prm.add_endRequest(function () {
            document.getElementById("divCargando").style.display = "none";
        });

    </script>

</body>

</html>
