<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prospectoscrm.aspx.cs" Inherits="fpWebApp.prospectoscrm" %>

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

    <title>Fitness People | Prospectos CRM</title>

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

    <script>
        function changeClass() {
            var element1 = document.querySelector("#prospectoscrm");
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
                    <h2><i class="fa fa-person-running text-success m-r-sm"></i>Prospectos CRM</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>CRM</li>
                        <li class="active"><strong>Prospectos</strong></li>
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
                            <div class="col-lg-5">
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
                                                            <asp:TextBox ID="txbDocumento" ClientIDMode="Static" CssClass="form-control input-sm" runat="server" placeholder="#" autocomplete="off" spellcheck="false"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <label>Tipo de Documento:</label>
                                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server"
                                                                AppendDataBoundItems="true" DataTextField="TipoDocumento"
                                                                DataValueField="idTipoDoc" CssClass="form-control input-sm m-b">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-lg-6">
                                                            <label>Nombre(s):</label>
                                                            <asp:TextBox ID="txbNombreContacto" runat="server"
                                                                CssClass="form-control input-sm" placeholder="Nombre(s)"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-6">
                                                            <label>Apellidos(s):</label>
                                                            <asp:TextBox ID="txbApellidoContacto" runat="server"
                                                                CssClass="form-control input-sm" placeholder="Apellido(s)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <label>Celular:</label>
                                                            <asp:TextBox ID="txbCelular" CssClass="form-control input-sm"
                                                                runat="server" placeholder="Celular"
                                                                autocomplete="off" spellcheck="false"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <a href="prospectoscrm" class="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md">Cancelar</a>
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

                            <div class="col-lg-7">
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

                                                    <div class="col-lg-6 form-horizontal">
                                                        <%--<div class="form-group">
                                                            <label class="col-lg-2 control-label">Canal de venta:</label>
                                                            <div class="col-lg-10">
                                                                
                                                            </div>
                                                        </div>--%>
                                                        
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
                                                                    CausesValidation="true" ValidationGroup="asignar">
                        <i class="fa fa-user-plus m-r-xs"></i>ASIGNAR
                                                                </asp:LinkButton>
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

                                                </div>

                                                <asp:GridView ID="gvAfiliados" runat="server" AutoGenerateColumns="False"
                                                    AllowPaging="True" PageSize="10" 
                                                    OnPageIndexChanging="gvAfiliados_PageIndexChanging1"
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
    <%--<script src="js/plugins/footable/footable.all.min.js"></script>--%>
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

                        // Campos restantes (omitimos nombre y apellidos)
                        //    var observaciones = `
                        //    Nro. Documento: ${data.numero_doc}
                        //    Sexo: ${data.sexo == 1 ? 'Masculino' : 'Femenino'}
                        //    Correo: ${data.correo || '-'}
                        //    Municipio ID: ${data.municipio_id}
                        //    Departamento ID: ${data.departamento_id}
                        //    EPS: ${data.eps}
                        //    Tipo de Afiliado: ${data.tipo_de_afiliado}
                        //    Estado Afiliación: ${data.estado_afiliacion}
                        //`   .trim();

                        //    $('#txaObservaciones').val(observaciones);
                    },
                    error: function (xhr, status, error) {
                        $('#txaObservaciones').val('Error al consultar la información.');
                        console.error("Error:", error);
                    }
                });
            });
        });
    </script>

</body>

</html>
