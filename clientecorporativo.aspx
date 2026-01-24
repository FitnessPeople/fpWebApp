<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="clientecorporativo.aspx.cs" Inherits="fpWebApp.clientecorporativo" %>

<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/indicadores02.ascx" TagPrefix="uc1" TagName="indicadores02" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Cliente corporativo</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />

    <link href="css/plugins/dropzone/basic.css" rel="stylesheet" />
    <link href="css/plugins/dropzone/dropzone.css" rel="stylesheet" />
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <style type="text/css" media="print">
        body {
            visibility: hidden;
            display: none
        }
    </style>

    <!-- SweetAlert2 CDN -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#clientecorporativo");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#corporativo");
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
                    <i class="fa fa-house-medical modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para visualizar las EPS</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar las EPS de manera clara y eficiente.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Crea una nueva</b><br />
                        Usa el campo que está a la <b>izquierda</b> para digitar el nombre que quieres registrar.<br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i><b>Agregar:</b> Guarda la información y finaliza el registro.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i><b>Cancelar:</b> Si necesitas volver atrás sin guardar cambios.
                    <br />
                        <br />
                        <b>Paso 2: Visualiza</b><br />
                        Usa el buscador que está a la <b>derecha</b> para encontrar lo que buscas.<br />
                        <i class="fa-solid fa-magnifying-glass"></i>Filtra por 
                        <i class="fa-solid fa-house-medical" style="color: #0D6EFD;"></i><b>EPS</b>
                        <br />
                        <br />
                        <b>Paso 3: Gestiona</b><br />
                        En la columna <b>Acciones</b> encontrarás estas opciones:<br />
                        <i class="fa fa-edit" style="color: #1AB394;"></i><b>Editar:</b> Modifica los datos necesarios.<br />
                        <i class="fa fa-trash" style="color: #DC3545;"></i><b>Eliminar:</b> Borra lo que creas innecesario.
                   <br />
                        <br />
                        <b>Paso 4: Acción adicional</b><br />
                        Al lado opuesto del buscador encontrarás un botón útil:<br />
                        <i class="fa-solid fa-file-export" style="color: #212529;"></i><b>Exportar a Excel:</b>
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

        <uc1:navbar runat="server" ID="navbar" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa fa-person-running text-success m-r-sm"></i>Cliente corporativo</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Corporativo</li>
                        <li class="active"><strong>Cliente corporativo</strong></li>
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

                    <form role="form" id="form" runat="server">
                        <asp:ScriptManager ID="sm1" runat="server">
                            <Scripts>
                                <asp:ScriptReference Path="https://cdn.jsdelivr.net/npm/sweetalert2@11"></asp:ScriptReference>
                            </Scripts>
                        </asp:ScriptManager>
                        <div class="row" id="divContenido" runat="server">

                            <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                            <div class="col-lg-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>
                                            <asp:Literal ID="ltTitulo" runat="server"></asp:Literal></h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <label>Documento:</label>
                                                            <asp:TextBox ID="txbDocumento" ClientIDMode="Static" CssClass="form-control input-sm"
                                                                runat="server" placeholder="#" autocomplete="off" spellcheck="false" required></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvDocumento" runat="server"
                                                                ErrorMessage="* Campo requerido" ControlToValidate="txbDocumento"
                                                                CssClass="text-danger font-bold" ValidationGroup="agregar"></asp:RequiredFieldValidator>

                                                        </div>
                                                        <div class="col-lg-6">
                                                            <label>Tipo de Documento:</label>
                                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server"
                                                                AppendDataBoundItems="true" DataTextField="TipoDocumento"
                                                                DataValueField="idTipoDoc" CssClass="form-control input-sm m-b">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvTipoDoc" runat="server"
                                                                ErrorMessage="* Campo requerido" ControlToValidate="ddlTipoDocumento"
                                                                CssClass="text-danger font-bold" ValidationGroup="agregar"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <label>Nombre(s):</label>
                                                            <asp:TextBox ID="txbNombreContacto" runat="server"
                                                                CssClass="form-control input-sm" placeholder="Nombre(s)" required></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                                                                ErrorMessage="* Campo requerido" ControlToValidate="txbNombreContacto"
                                                                CssClass="text-danger font-bold" ValidationGroup="agregar"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <label>Apellidos(s):</label>
                                                            <asp:TextBox ID="txbApellidoContacto" runat="server"
                                                                CssClass="form-control input-sm" placeholder="Apellido(s)" required></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvApellido" runat="server"
                                                                ErrorMessage="* Campo requerido" ControlToValidate="txbApellidoContacto"
                                                                CssClass="text-danger font-bold" ValidationGroup="agregar"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-lg-6 form-group">
                                                            <label>Celular:</label>
                                                            <asp:TextBox ID="txbCelular" CssClass="form-control input-sm"
                                                                runat="server" placeholder="Celular"
                                                                autocomplete="off" spellcheck="false" required></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-6 form-group">
                                                            <label>Empresa:</label>
                                                            <asp:DropDownList CssClass="form-control input-sm required" ID="ddlEmpresas" runat="server"
                                                                DataValueField="idEmpresa" DataTextField="NombreEmpresa"
                                                                AppendDataBoundItems="true">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <a href="clientecorporativo" class="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md">Cancelar</a>
                                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar"
                                                        CssClass="btn btn-sm btn-primary pull-right m-t-n-xs"
                                                        OnClick="btnAgregar_Click" Visible="false" ValidationGroup="agregar" />
                                                </div>
                                                <br />
                                                <br />
                                                <div class="form-group">
                                                    <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Subir prospectos a través de archivo plano</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <label>Archivo:</label>
                                                            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                                                                <div class="form-control input-sm" data-trigger="fileinput">
                                                                    <i class="glyphicon glyphicon-file fileinput-exists"></i>
                                                                    <span class="fileinput-filename"></span>
                                                                </div>
                                                                <span class="input-group-addon btn btn-success btn-file input-sm">
                                                                    <span class="fileinput-new input-sm">Seleccionar archivo plano</span>
                                                                    <span class="fileinput-exists input-sm">Cambiar</span>
                                                                    <input type="file" name="fileFoto" id="fileFoto" accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel">
                                                                </span>
                                                                <a href="#" class="input-group-addon btn btn-danger fileinput-exists input-sm"
                                                                    data-dismiss="fileinput">Quitar</a>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <a href="prospectoscrm" class="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md">Cancelar</a>
                                                    <asp:Button ID="btnProcesar" runat="server" Text="Procesar"
                                                        CssClass="btn btn-sm btn-primary pull-right m-t-n-xs m-l-md"
                                                        OnClick="btnProcesar_Click" Visible="true" ValidationGroup="procesar" />
                                                    <asp:Button ID="btnAgregarDatos" runat="server" Text="Agregar datos"
                                                        CssClass="btn btn-sm btn-info pull-right m-t-n-xs"
                                                        OnClick="btnAgregarDatos_Click" Visible="true" ValidationGroup="procesar" />
                                                </div>
                                                <br />
                                                <br />
                                                <div class="form-group">
                                                    <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-8">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Lista de Prospectos</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">

                                        <asp:UpdatePanel ID="upContactos" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <div class="row">

                                                    <div class="col-lg-4 form-horizontal">
                                                        <div class="form-group">
                                                            <label class="col-lg-2 control-label">Mostrar</label>
                                                            <div class="col-lg-10">
                                                                <asp:RadioButtonList ID="rblPageSize" runat="server" AutoPostBack="true"
                                                                    RepeatDirection="Horizontal" CssClass="form-control input-sm" RepeatLayout="Flow"
                                                                    OnSelectedIndexChanged="rblPageSize_SelectedIndexChanged">
                                                                    <asp:ListItem Text="&nbsp;10" Value="10" style="margin-right: 5px; font-size: 10px;" Selected="True" />
                                                                    <asp:ListItem Text="&nbsp;50" Value="50" style="margin-right: 5px; font-size: 10px;" />
                                                                    <asp:ListItem Text="&nbsp;100" Value="100" style="margin-right: 5px; font-size: 10px;" />
                                                                    <asp:ListItem Text="&nbsp;Todos" Value="0" style="margin-right: 5px; font-size: 10px;" />
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
                                                            <!-- Imagen de carga -->
                                                            <div id="divCargando" style="display: none; text-align: center;">
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

                                                    <div class="col-lg-2 form-horizontal">
                                                        <div class="form-group">
                                                            <%--<label class="col-lg-2 control-label"></label>--%>
                                                            <div class="col-lg-10">
                                                                <a class="btn btn-info pull-right dim m-l-md" style="font-size: 12px;"
                                                                    target="_blank" runat="server" id="btnExportar"
                                                                    href="imprimirafiliados" visible="false" title="Exportar">
                                                                    <i class="fa fa-file-excel m-r-xs"></i>EXCEL
                                                                </a>
                                                                <asp:LinkButton ID="lnkAsignar" runat="server" Style="font-size: 12px;"
                                                                    CssClass="btn btn-primary pull-right dim m-l-md" Visible="false"
                                                                    OnClick="lnkAsignar_Click" CausesValidation="true" ValidationGroup="asignar">
                                                                    <i class="fa fa-user-plus m-r-xs"></i>ASIGNAR
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <asp:GridView ID="gvProspectos" runat="server" AutoGenerateColumns="False"
                                                    AllowPaging="True" PageSize="10"
                                                    OnPageIndexChanging="gvProspectos_PageIndexChanging"
                                                    AllowSorting="true" OnSorting="gvProspectos_Sorting"
                                                    OnRowCreated="gvProspectos_RowCreated"
                                                    OnRowDataBound="gvProspectos_RowDataBound"
                                                    CssClass="table table-striped list-group-item-text"
                                                    DataKeyNames="idPregestion,NombreContacto,ApellidoContacto,DocumentoContacto,idTipoDocumentoContacto,CelularContacto,hacecuanto,EstadoNegociacion"
                                                    BorderStyle="None" GridLines="None"
                                                    PagerSettings-Mode="NumericFirstLast"
                                                    PagerSettings-FirstPageText="«"
                                                    PagerSettings-LastPageText="»"
                                                    PagerStyle-CssClass="paginador"
                                                    OnRowCommand="gvProspectos_RowCommand">
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
                                                        <asp:BoundField DataField="idPregestion" HeaderText="ID" Visible="false" />
                                                        <asp:BoundField DataField="DocumentoEmpresa" HeaderText="DocumentoEmpresa" Visible="false" />
                                                        <%--<asp:BoundField DataField="NombreEmpresa" HeaderText="Empresa"
                                                            SortExpression="NombreEmpresa" />--%>
                                                        <asp:BoundField DataField="NombreContacto" HeaderText="Nombres"
                                                            SortExpression="NombreContacto" />
                                                        <asp:BoundField DataField="ApellidoContacto" HeaderText="Apellidos"
                                                            SortExpression="ApellidoContacto" />
                                                        <asp:BoundField DataField="DocumentoContacto" HeaderText="Documento"
                                                            SortExpression="DocumentoContacto" />
                                                        <asp:BoundField DataField="idTipoDocumentoContacto" HeaderText="TipoDocumento"
                                                            SortExpression="idTipoDocumentoContacto" Visible="False" />
                                                        <asp:BoundField DataField="CelularContacto" HeaderText="Celular"
                                                            SortExpression="CelularContacto" />
                                                        <asp:BoundField DataField="EstadoNegociacion" HeaderText="Estado Negociación"
                                                            SortExpression="EstadoNegociacion" />
                                                        <asp:BoundField DataField="NombreEmpresa" HeaderText="Empresa"
                                                            SortExpression="NombreEmpresa" />

                                                        <asp:TemplateField HeaderText="Editar">
                                                            <ItemTemplate>
                                                                <asp:LinkButton
                                                                    ID="btnEditar"
                                                                    runat="server"
                                                                    ToolTip="Editar"
                                                                    CssClass="btn btn-sm btn-primary pull-right m-t-n-xs m-l-md"
                                                                    CommandName="Editar"
                                                                    CommandArgument='<%# Eval("idPregestion") %>'>
                                                                    <i class="fa fa-edit"></i> 
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Eliminar">
                                                            <ItemTemplate>
                                                                <asp:LinkButton
                                                                    ID="btnEliminar"
                                                                    runat="server"
                                                                    ToolTip="Eliminar"
                                                                    CssClass="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md"
                                                                    CommandName="Eliminar"
                                                                    CommandArgument='<%# Eval("idPregestion") %>'
                                                                    OnClientClick="return confirm('¿Está seguro de eliminar este registro?');">
                                                                    <i class="fa fa-trash"></i> 
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                                <%--Asignados--%>
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Prospectos asignados</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">

                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <div class="row">

                                                    <div class="col-lg-4 form-horizontal">
                                                        <div class="form-group">
                                                            <label class="col-lg-2 control-label">Mostrar</label>
                                                            <div class="col-lg-10">
                                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true"
                                                                    RepeatDirection="Horizontal" CssClass="form-control input-sm" RepeatLayout="Flow"
                                                                    OnSelectedIndexChanged="rblPageSize_SelectedIndexChanged">
                                                                    <asp:ListItem Text="&nbsp;10" Value="10" style="margin-right: 5px; font-size: 10px;" Selected="True" />
                                                                    <asp:ListItem Text="&nbsp;50" Value="50" style="margin-right: 5px; font-size: 10px;" />
                                                                    <asp:ListItem Text="&nbsp;100" Value="100" style="margin-right: 5px; font-size: 10px;" />
                                                                    <asp:ListItem Text="&nbsp;Todos" Value="0" style="margin-right: 5px; font-size: 10px;" />
                                                                </asp:RadioButtonList>
                                                                <asp:Label ID="Label1" runat="server" CssClass="total-registros" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-lg-6 form-horizontal">
<%--                                                        <div class="form-group">
                                                            <label class="col-lg-4 control-label">Asesor comercial:</label>
                                                            <div class="col-lg-8">
                                                                <asp:DropDownList ID="ddlAsesores2" runat="server" AppendDataBoundItems="true"
                                                                    DataTextField="NombreUsuario" DataValueField="idUsuario"
                                                                    CssClass="form-control input-sm m-b">
                                                                    <asp:ListItem Text="Seleccione" Value="" Selected="True" />
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>--%>
                                                        <div class="form-group">
                                                            <!-- Imagen de carga -->
                                                            <div id="divCargando" style="display: none; text-align: center;">
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

                                                    <div class="col-lg-2 form-horizontal">
                                                        <div class="form-group">
                                                            <%--<label class="col-lg-2 control-label"></label>--%>
                                                            <div class="col-lg-10">
                                                                <a class="btn btn-info pull-right dim m-l-md" style="font-size: 12px;"
                                                                    target="_blank" runat="server" id="A1"
                                                                    href="imprimirafiliados" visible="false" title="Exportar">
                                                                    <i class="fa fa-file-excel m-r-xs"></i>EXCEL
                                                                </a>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" Style="font-size: 12px;"
                                                                    CssClass="btn btn-primary pull-right dim m-l-md" Visible="false"
                                                                    OnClick="lnkAsignar_Click" CausesValidation="true" ValidationGroup="asignar">
                                                                    <i class="fa fa-user-plus m-r-xs"></i>ASIGNAR
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <asp:GridView ID="gvAsignados" runat="server" AutoGenerateColumns="False"
                                                    AllowPaging="True" PageSize="10"
                                                    OnPageIndexChanging="gvAsignados_PageIndexChanging"
                                                    AllowSorting="true" OnSorting="gvProspectos_Sorting"
                                                    OnRowCreated="gvAsignados_RowCreated"
                                                    OnRowDataBound="gvAsignados_RowDataBound"
                                                    CssClass="table table-striped list-group-item-text"
                                                    DataKeyNames="idPregestion,NombreContacto,ApellidoContacto,DocumentoContacto,idTipoDocumentoContacto,CelularContacto,hacecuanto,EstadoNegociacion,idUsuarioAsigna"
                                                    BorderStyle="None" GridLines="None"
                                                    PagerSettings-Mode="NumericFirstLast"
                                                    PagerSettings-FirstPageText="«"
                                                    PagerSettings-LastPageText="»"
                                                    PagerStyle-CssClass="paginador"
                                                    OnRowCommand="gvAsignados_RowCommand">
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
                                                        <asp:BoundField DataField="idPregestion" HeaderText="ID" Visible="false" />
                                                        <asp:BoundField DataField="DocumentoEmpresa" HeaderText="DocumentoEmpresa" Visible="false" />
                                                        <%--<asp:BoundField DataField="NombreEmpresa" HeaderText="Empresa"
                                                            SortExpression="NombreEmpresa" />--%>
                                                        <asp:BoundField DataField="NombreContacto" HeaderText="Nombres"
                                                            SortExpression="NombreContacto" />
                                                        <asp:BoundField DataField="ApellidoContacto" HeaderText="Apellidos"
                                                            SortExpression="ApellidoContacto" />
                                                        <asp:BoundField DataField="DocumentoContacto" HeaderText="Documento"
                                                            SortExpression="DocumentoContacto" />
                                                        <asp:BoundField DataField="idTipoDocumentoContacto" HeaderText="TipoDocumento"
                                                            SortExpression="idTipoDocumentoContacto" Visible="False" />
                                                        <asp:BoundField DataField="CelularContacto" HeaderText="Celular"
                                                            SortExpression="CelularContacto" />
                                                        <asp:BoundField DataField="EstadoNegociacion" HeaderText="Estado Negociación"
                                                            SortExpression="EstadoNegociacion" />
                                                        <asp:BoundField DataField="NombreEmpresa" HeaderText="Empresa"
                                                            SortExpression="NombreEmpresa" />
                                                        <asp:BoundField DataField="Asesor" HeaderText="Asignadao a:"
                                                            SortExpression="Asesor" />

                                                        <asp:TemplateField HeaderText="Deshacer">
                                                            <ItemTemplate>
                                                                <asp:LinkButton
                                                                    ID="btnDeshacer"
                                                                    runat="server"
                                                                    ToolTip="Deshacer"
                                                                    CssClass="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md"
                                                                    CommandName="Deshacer"
                                                                    CommandArgument='<%# Eval("idPregestion") %>'
                                                                    OnClientClick="return confirm('¿Está seguro de deshacer esta asignación??');">
                                                                    <i class="fa fa-trash"></i> 
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>

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
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();
    </script>

    <script>
        $(document).ready(function () {
            $('#txbDocumento').on('change blur', function () {
                var documento = $(this).val().trim();
                if (documento.length === 0) return;

                var url = 'https://pqrdsuperargo.supersalud.gov.co/api/api/adres/0/' + documento;

                // Limpia primero los campos
                $('#txbNombreContacto').val('');
                $('#txbApellidoContacto').val('');
                // $('#txaObservaciones').val('Consultando...');

                $.ajax({
                    url: url,
                    method: 'GET',
                    success: function (data) {
                        // Nombres
                        var nombreCompleto = [data.nombre, data.s_nombre].filter(Boolean).join(' ').toUpperCase();
                        var apellidoCompleto = [data.apellido, data.s_apellido].filter(Boolean).join(' ').toUpperCase();

                        $('#txbNombreContacto').val(nombreCompleto);
                        $('#txbApellidoContacto').val(apellidoCompleto);

                        //$('#txbEdad').val((data.edad != null ? data.edad + ' años' : ''));
                        //$('#txbFecNac').val((data.fecha_nacimiento));
                        //$('#ddlGenero').val(data.sexo);

                    },
                    error: function (xhr, status, error) {
                        $('#txaObservaciones').val('Error al consultar la información.');
                        console.error("Error:", error);
                    }
                });
            });
        });


        function seleccionarTodos(chkHeader) {
            var grid = document.getElementById('<%= gvProspectos.ClientID %>');
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
    </script>

</body>

</html>

