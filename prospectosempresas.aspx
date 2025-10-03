<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prospectosempresas.aspx.cs" Inherits="fpWebApp.prospectosempresas" %>

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

    <title>Fitness People | Prospectos empresas</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />



    <link href="css/plugins/dataTables/datatables.min.css" rel="stylesheet" />
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <!-- Sweet Alert -->
    <link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">
    <link href="css/plugins/clockpicker/clockpicker.css" rel="stylesheet">
    <!-- Sweet alert -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <!-- FooTable -->
    <%--<link href="css/plugins/footable/footable.core.css" rel="stylesheet" />--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <!-- Select2 -->
    <link href="css/plugins/select2/select2.min.css" rel="stylesheet">

    <style type="text/css" media="print">
        body {
            visibility: hidden;
            display: none
        }
    </style>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#prospectosempresas");
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
                    <i class="fa fa-city modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para visualizar estados crm</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar los prospectos de emepresas clara y eficiente.</small>
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
                        <i class="fa-solid fa-hashtag" style="color: #0D6EFD;"></i><b>Id</b> o
                        <i class="fa-solid fa-city" style="color: #0D6EFD;"></i><b>Ciudad Sede</b>
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
                    <h2><i class="fa fa-industry text-success m-r-sm"></i>Prospectos empresas</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Configuración</li>
                        <li class="active"><strong>Prospectos empresas CRM</strong></li>
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
                        <div class="row" id="divContenido" runat="server">
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

                                                <div class="row">

                                                    <div class="row">
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                                <label>Tipo Doc.</label>
                                                                <asp:DropDownList ID="ddlTipoDocumento" runat="server" AppendDataBoundItems="true"
                                                                    DataTextField="SiglaDocumento" DataValueField="idTipoDoc" CssClass="form-control input-sm">
                                                                    <asp:ListItem Text="Seleccione" Value="" InitialValue=""></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvTipoDoc" runat="server" ControlToValidate="ddlTipoDocumento"
                                                                    ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" ValidationGroup="agregar">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                                <label>Número</label>
                                                                <asp:TextBox ID="txbDocumento" TextMode="Number" MaxLength="12" ClientIDMode="Static" CssClass="form-control input-sm" runat="server" placeholder="#"
                                                                    spellcheck="false" autocomplete="new-password" autocorrect="off" autocapitalize="off"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvNumDoc" runat="server" ControlToValidate="txbDocumento"
                                                                    ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" ValidationGroup="agregar">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="form-group">
                                                                <label>DV</label>
                                                                <asp:TextBox ID="txbDigitoVerificacion" ClientIDMode="Static" CssClass="form-control input-sm" runat="server" placeholder="#"
                                                                    MaxLength="1"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDigitoVerfica" runat="server" ControlToValidate="txbDigitoVerificacion"
                                                                    ErrorMessage="*" CssClass="font-bold text-danger" Display="Dynamic" ValidationGroup="agregar">
                                                                </asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="form-group">
                                                                <i class="fa fa-industry text-info"></i>
                                                                <label for="txbRazonSocial" class="col-form-label">Razon social:</label>
                                                                <input type="text" runat="server" id="txbRazonSocial" class="form-control"
                                                                    style="text-transform: uppercase;" spellcheck="false" autocomplete="off" />
                                                                <asp:RequiredFieldValidator ID="rfvNombreEmpresa" runat="server" ControlToValidate="txbRazonSocial"
                                                                    ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" ValidationGroup="agregar" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <i class="fa fa-building text-info"></i>
                                                                <label for="NombreComercialEmpresa" class="col-form-label">Nombre comercial:</label>
                                                                <input type="text" runat="server" id="txbNombreComercialEmpresa" class="form-control"
                                                                    style="text-transform: uppercase;" spellcheck="false" autocomplete="off" />
                                                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txbNombreComercialEmpresa"
                                                                    ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" ValidationGroup="agregar" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <i class="fa fa-person text-info"></i>
                                                                <label for="NombreContacto" class="col-form-label">Nombre del contacto:</label>
                                                                <input type="text" runat="server" id="txbNombreContacto" class="form-control"
                                                                    oninput="validarSoloLetras(this)" style="text-transform: uppercase;" spellcheck="false" autocomplete="off" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txbNombreContacto"
                                                                    ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" ValidationGroup="agregar" />
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <i class="fa fa-building text-info"></i>
                                                                <label for="CargoContacto" class="col-form-label">Cargo del contacto:</label>
                                                                <input type="text" runat="server" id="txbCargoContacto" class="form-control"
                                                                    style="text-transform: uppercase;" spellcheck="false" autocomplete="off"
                                                                    placeholder="Ej: Secretario/a…" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txbCargoContacto"
                                                                    ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" ValidationGroup="agregar" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <i class="fa fa-phone text-info"></i>
                                                                <label for="txbCelularEmpresa" class="col-form-label">Celular:</label>
                                                                <input type="text" runat="server" id="txbCelularEmpresa" class="form-control"
                                                                    spellcheck="false" autocorrect="off" autocapitalize="off" />
                                                                <asp:RequiredFieldValidator ID="rfvCelularEmpresa" runat="server" ControlToValidate="txbCelularEmpresa"
                                                                    ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" ValidationGroup="agregar" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <i class="fa fa-envelope text-info"></i>
                                                                <label for="correoEmpresa" class="col-form-label">Correo:</label>
                                                                <input type="email" runat="server" id="txbCorreoEmpresa" class="form-control"
                                                                    style="text-transform: lowercase;"
                                                                    spellcheck="false" autocorrect="off" autocapitalize="off" />
                                                                <asp:RequiredFieldValidator ID="rfvCorreoEmpresa" runat="server" ControlToValidate="txbCorreoEmpresa"
                                                                    ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" ValidationGroup="agregar" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label>Ciudad:</label>
                                                                <asp:DropDownList ID="ddlCiudades" runat="server" AppendDataBoundItems="true" DataTextField="NombreCiudad"
                                                                    DataValueField="idCiudad" CssClass="form-control input-sm m-b" InitialValue="">
                                                                    <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCiudad" runat="server" ControlToValidate="ddlCiudades"
                                                                    ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" ValidationGroup="agregar" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="form-group">
                                                                <i class="fas fa-pen text-info"></i>
                                                                <label for="message-text" class="col-form-label">Oferta:</label>
                                                                <textarea id="txaObservaciones" runat="server" rows="3"
                                                                    cssclass="form-control input-sm" class="form-control" placeholder="Escribe tu comentario…"></textarea>
                                                                <asp:RequiredFieldValidator ID="rfvObservaciones" runat="server" ControlToValidate="txaObservaciones"
                                                                    ErrorMessage="* Campo requerido" CssClass="font-bold text-danger" Display="Dynamic" ValidationGroup="agregar" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <a href="prospectosempresas" class="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md">Cancelar</a>
                                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-sm btn-primary pull-right m-t-n-xs"
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
                            </div>
                            <div class="col-lg-8">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Lista de prospectos</h5>
                                        <div class="ibox-tools">
                                            <a class="collapse-link">
                                                <i class="fa fa-chevron-up"></i>
                                            </a>
                                        </div>
                                    </div>
                                    <div class="ibox-content">

                                        <div class="row" style="font-size: 12px;" runat="server" id="divBotonesLista">
                                            <div class="col-lg-6 form-horizontal">
                                                <div class="form-group">
                                                    <div class="form-group" id="filter-form-container" style="margin-left: 28px;"></div>
                                                </div>
                                            </div>

                                            <div class="col-lg-6 form-horizontal">
                                                <asp:LinkButton ID="lbExportarExcel" runat="server"
                                                    CausesValidation="false"
                                                    CssClass="btn btn-info pull-right dim m-l-md" Style="font-size: 12px;"
                                                    OnClick="lbExportarExcel_Click">
                                                    <i class="fa fa-file-excel"></i> EXCEL
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                        <table class="footable table table-striped" data-paging-size="10"
                                            data-filter-min="3" data-filter-placeholder="Buscar"
                                            data-paging="true" data-sorting="true" data-paging-count-format="{CP} de {TP}"
                                            data-paging-limit="10" data-filtering="true"
                                            data-filter-container="#filter-form-container" data-filter-delay="300"
                                            data-filter-dropdown-title="Buscar en:" data-filter-position="left"
                                            data-empty="Sin resultados">
                                            <thead>
                                                <tr>
                                                    <th>Identificación</th>
                                                    <th>Nombre empresa</th>
                                                    <th>Teléfono</th>
                                                    <th>Ciudad</th>
                                                    <th data-breakpoints="xs">HaceCuanto</th>

                                                    <asp:PlaceHolder ID="phAsesorHeader" runat="server" Visible="false">
                                                        <th data-breakpoints="xs">Asesor</th>
                                                    </asp:PlaceHolder>

                                                    <th data-breakpoints="all" data-title="Más Información"></th>
                                                    <th data-sortable="false" data-filterable="false" class="text-right">Acciones</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="rpEmpresasCRM" runat="server" OnItemDataBound="rpEmpresasCRM_ItemDataBound">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Eval("DocumentoEmpresa") %> - <%# Eval("digitoverificacion") %></td>
                                                            <td><%# Eval("NombreComercial") %></td>
                                                            <td><%# Eval("CelularEmpresa") %></td>
                                                            <td><%# Eval("NombreCiudad") %></td>
                                                            <td>
                                                                <asp:Literal ID="ltTiempoTranscurrido" runat="server"></asp:Literal></td>

                                                            <asp:PlaceHolder ID="phAsesorCol" runat="server" Visible="false">
                                                                <td><%# Eval("NombreUsuario") %></td>
                                                            </asp:PlaceHolder>

                                                            <td>
                                                                <table class="table table-bordered table-striped">
                                                                    <tr>
                                                                        <th width="25%"><i class="fa fa-mobile m-r-xs"></i>Razon social</th>
                                                                        <th width="25%"><i class="fa fa-mobile m-r-xs"></i>Correo</th>
                                                                        <th width="50%" class="text-nowrap"><i class="fa fa-handshake m-r-xs"></i>Oferta</th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><%# Eval("NombreEmpresaCRM") %></td>
                                                                        <td><%# Eval("CorreoEmpresa") %></td>
                                                                        <td><%# Eval("ObservacionesEmp") %></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td>
                                                                <a runat="server" id="btnEliminar" href="#" class="btn btn-outline btn-danger pull-right m-r-xs"
                                                                    style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" ><i class="fa fa-trash"></i></a>
                                                                <a runat="server" id="btnEditar" href="#" class="btn btn-outline btn-primary pull-right m-r-xs"
                                                                    style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" ><i class="fa fa-edit"></i></a></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>

                                        </table>
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
    <%--    <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-beta.1/dist/js/select2.min.js"></script>--%>


    <script>
        document.getElementById("btnConsultar").addEventListener("click", async () => {
            const nit = document.getElementById("txbDocumento").value.trim();
            if (!nit) {
                alert("Ingresa un NIT");
                return;
            }

            const recurso = "f9nk-qw9u"; // dataset
            const url = `https://www.datos.gov.co/resource/${recurso}.json?identificacion=${encodeURIComponent(nit)}`;
            console.log("Consultando:", url);

            try {
                const response = await fetch(url);
                if (!response.ok) {
                    throw new Error("HTTP error: " + response.status);
                }
                const datos = await response.json();
                console.log("Datos obtenidos:", datos);

                if (datos.length === 0) {
                    document.getElementById("salida").textContent = "No se encontró información para ese NIT.";
                } else {
                    // Mostrar en formato bonito
                    document.getElementById("salida").textContent = JSON.stringify(datos, null, 2);
                }
            } catch (err) {
                console.error("Error al consultar:", err);
                document.getElementById("salida").textContent = "Error: " + err;
            }
        });
    </script>
    <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const input = document.getElementById("txxbDigitoVerificacion");
            input.addEventListener("input", function () {
                this.value = this.value.replace(/[^0-9]/g, '').slice(0, 1);
            });
        });
    </script>

</body>

</html>
