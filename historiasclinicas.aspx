<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="historiasclinicas.aspx.cs" Inherits="fpWebApp.historiasclinicas" %>

<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>
<%@ Register Src="~/controles/indicadoresmedico.ascx" TagPrefix="uc1" TagName="indicadoresmedico" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Historias Clínicas</title>

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
            var element1 = document.querySelector("#historias");
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
                    <h4 class="modal-title">Guía para ver Historias Clínicas</h4>
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

        <uc1:navbar runat="server" ID="navbar" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa fa-notes-medical text-success m-r-sm"></i>Historias Clínicas</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Médico</li>
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
                            <uc1:indicadoresmedico runat="server" id="indicadoresmedico" />

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
                                    <h5>Lista de historias clínicas</h5>
                                    <div class="ibox-tools">
                                        <a class="collapse-link">
                                            <i class="fa fa-chevron-up"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="ibox-content">

                                    <div class="row">
                                        <div class="col-lg-4 form-horizontal">
                                            <div class="form-group">
                                                <label class="col-lg-4 control-label" style="text-align: left;">Buscador:</label>
                                                <div class="col-lg-8">
                                                    <input type="text" placeholder="Buscar..." class="form-control input-sm m-b-xs" id="filter">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-4 form-horizontal" style="text-align: center;">
                                            <label class="control-label">Mostrar </label>

                                            <a href="#" class="data-page-size" data-page-size="10">10</a> | 
                                            <a href="#" class="data-page-size" data-page-size="20">20</a> | 
                                            <a href="#" class="data-page-size" data-page-size="50">50</a> | 
                                            <a href="#" class="data-page-size" data-page-size="100">100</a>

                                            <label class="control-label">registros</label>
                                        </div>

                                        <div class="col-lg-4 form-horizontal">
                                            <label class="control-label">&nbsp;</label>
                                            <a class="btn btn-success pull-right dim m-l-md" style="font-size: 12px;" href="nuevahistoriaclinica" title="Agregar historia clínica" runat="server" id="btnAgregar" visible="false"><i class="fa fa-square-plus"></i> NUEVO</a>
                                            <a class="btn btn-info pull-right dim m-l-md" style="font-size: 12px;" target="_blank" runat="server" id="btnExportar" href="imprimirhistorias" visible="false" title="Exportar"><i class="fa fa-print"></i> IMPRIMIR</a>
                                            <a data-trigger="footable_expand_all" style="font-size: 12px;" class="toggle btn btn-primary pull-right dim" href="#collapse" title="Expandir todo"><i class="fa fa-square-caret-down"></i> EXPANDIR</a>
                                            <a data-trigger="footable_collapse_all" class="toggle btn btn-primary pull-right dim" style="display: none; font-size: 12px;" href="#collapse" title="Contraer todo"><i class="fa fa-square-caret-up"></i> CONTRAER</a>
                                        </div>
                                    </div>

                                    <table class="footable table toggle-arrow-small list-group-item-text" data-page-size="10" data-filter="#filter" data-filter-minimum="3">
                                        <thead>
                                            <tr>
                                                <th data-sort-ignore="true">ID</th>
                                                <th>Fecha de creación</th>
                                                <th data-sort-ignore="true">Documento</th>
                                                <th>Afiliado</th>
                                                <th>Género</th>
                                                <th>Fecha último control</th>
                                                <th data-hide="all"></th>
                                                <th data-sort-ignore="true" data-toggle="false" class="text-right">Acciones</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rpHistoriasClinicas" runat="server" OnItemDataBound="rpHistoriasClinicas_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr class="feed-element">
                                                        <td><%# Eval("idHistoria") %></td>
                                                        <td><%# Eval("FechaHora", "{0:dd MMM yyyy HH:mm}") %></td>
                                                        <td><%# Eval("DocumentoAfiliado") %></td>
                                                        <td><%# Eval("NombreAfiliado") %> <%# Eval("ApellidoAfiliado") %></td>
                                                        <td><%# Eval("Genero") %></td>
                                                        <td><%# Eval("FechaHora", "{0:dd MMM yyyy HH:mm}") %></td>
                                                        <td>
                                                            <h3>Antecedentes</h3>
                                                            <table class="table table-bordered">
                                                                <thead>
                                                                <tr>
                                                                    <th width="20%"><i class="fa fa-people-roof m-r-sm"></i>Familiares</th>
                                                                    <th width="20%"><i class="fa fa-virus m-r-sm"></i>Patológicos</th>
                                                                    <th width="20%"><i class="fa fa-syringe m-r-sm"></i>Quirúrgicos</th>
                                                                    <th width="20%"><i class="fa fa-biohazard m-r-sm"></i>Toxicológico</th>
                                                                    <th width="20%"><i class="fa fa-hospital m-r-sm"></i>Hospitalario</th>
                                                                </tr>
                                                                </thead>
                                                                <tbody>
                                                                <tr>
                                                                    <td><%# Eval("AnteFamiliar") %></td>
                                                                    <td><%# Eval("AntePatologico") %></td>
                                                                    <td><%# Eval("AnteQuirurgico") %></td>
                                                                    <td><%# Eval("AnteToxicologico") %></td>
                                                                    <td><%# Eval("AnteHospitalario") %></td>
                                                                </tr>
                                                                </tbody>
                                                                <thead>
                                                                <tr>
                                                                    <th width="20%"><i class="fa fa-crutch m-r-sm"></i>Traumatológico</th>
                                                                    <th width="20%"><i class="fa fa-capsules m-r-sm"></i>Farmacológico</th>
                                                                    <th width="20%"><i class="fa fa-droplet m-r-sm"></i>F.U.M.</th>
                                                                    <th width="20%"><i class="fa fa-person-running m-r-sm"></i>Actividad Física</th>
                                                                    <th width="20%"><i class="fa fa-person-pregnant m-r-sm"></i>Gineco-Obstetricia</th>
                                                                </tr>
                                                                </thead>
                                                                <tbody>
                                                                <tr>
                                                                    <td><%# Eval("AnteTraumatologico") %></td>
                                                                    <td><%# Eval("AnteFarmacologico") %></td>
                                                                    <td><%# Eval("AnteFUM") %></td>
                                                                    <td><%# Eval("AnteActividadFisica") %></td>
                                                                    <td><%# Eval("AnteGineco") %></td>
                                                                </tr>
                                                                </tbody>
                                                            </table>
                                                            <table class="table table-bordered">
                                                                <thead>
                                                                <tr>
                                                                    <th width="10%"><i class="fa fa-smoking m-r-sm"></i>Tabaco</th>
                                                                    <th width="13%"><i class="fa fa-smoking m-r-sm"></i>Cigarrillos/día</th>
                                                                    <th width="10%"><i class="fa fa-wine-bottle m-r-sm"></i>Alcohol</th>
                                                                    <th width="12%"><i class="fa fa-wine-bottle m-r-sm"></i>Bebidas/mes</th>
                                                                    <th width="13%"><i class="fa fa-chair m-r-sm"></i>Sedentarismo</th>
                                                                    <th width="10%"><i class="fa fa-vial m-r-sm"></i>Diabetes</th>
                                                                    <th width="10%"><i class="fa fa-heart-pulse m-r-sm"></i>Colesterol</th>
                                                                    <th width="12%"><i class="fa fa-heart-circle-bolt m-r-sm"></i>Triglicéridos</th>
                                                                    <th width="10%"><i class="fa fa-stethoscope m-r-sm"></i>H.T.A.</th>
                                                                </tr>
                                                                </thead>
                                                                <tbody>
                                                                <tr>
                                                                    <td><%# Eval("fuma") %></td>
                                                                    <td><%# Eval("Cigarrillos") %></td>
                                                                    <td><%# Eval("toma") %></td>
                                                                    <td><%# Eval("Bebidas") %></td>
                                                                    <td><%# Eval("sedentario") %></td>
                                                                    <td><%# Eval("diabetico") %></td>
                                                                    <td><%# Eval("colesterado") %></td>
                                                                    <td><%# Eval("triglicerado") %></td>
                                                                    <td><%# Eval("hipertenso") %></td>
                                                                </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <button runat="server" id="btnAgregar" class="btn btn-outline btn-success pull-right"
                                                                style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false" title="Agregar control">
                                                                <i class="fa fa-notes-medical"></i>
                                                            </button>
                                                            <button runat="server" id="btnEliminar" class="btn btn-outline btn-danger pull-right m-r-xs"
                                                                style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false" title="Eliminar historia">
                                                                <i class="fa fa-trash"></i>
                                                            </button>
                                                            <button runat="server" id="btnEditar" class="btn btn-outline btn-primary pull-right m-r-xs"
                                                                style="padding: 1px 2px 1px 2px; margin-bottom: 0px;" visible="false" title="Editar historia">
                                                                <i class="fa fa-edit"></i>
                                                            </button>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="6">
                                                    <ul class="pagination"></ul>
                                                </td>
                                            </tr>
                                        </tfoot>
                                    </table>

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
    <script>
        $('.footable').footable();

        $('.data-page-size').on('click', function (e) {
            e.preventDefault();
            var newSize = $(this).data('pageSize');
            $('.footable').data('page-size', newSize);
            $('.footable').trigger('footable_initialized');
        });

        $('.toggle').click(function (e) {
            e.preventDefault();
            $('.toggle').toggle();
            $('.footable').trigger($(this).data('trigger')).trigger('footable_redraw');
        });

        $('.chosen-select').chosen({ width: "100%" });

    </script>

</body>

</html>
