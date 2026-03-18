<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bonificaciones.aspx.cs" Inherits="fpWebApp.bonificaciones" %>

<!DOCTYPE html>

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

    <title>Fitness People | Bonificaciones</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/plugins/iCheck/custom.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <!-- FooTable -->
    <link href="css/plugins/footable/footable.core.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />


    <%--        formato de moneda--%>
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
    </script>

    <%--        formato de posición en el menú--%>
    <script>
        function changeClass() {
            var element1 = document.querySelector("#listacontactoscrm");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#gestioncomercial");
            element2.classList.remove("collapse");
        }
    </script>




    <%--        Formatear solo letraas --%>
    <script>
        function validarSoloLetras(input) {
            // Eliminar cualquier caracter que no sea letra o espacio
            input.value = input.value.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
        }
    </script>



    <!--        SweetAlert2 CDN -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>



    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <style>
        .table {
            width: 100%;
        }

        #tablaDetalleComision {
            background: white;
        }

        .tab-content {
            overflow-x: hidden;
        }

        .panel-body {
            overflow-x: auto;
        }

        .row {
            margin-left: 0;
            margin-right: 0;
        }
    </style>
    }

</head>

<body onload="changeClass()">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <i class="fa fa-person-chalkboard modal-icon"></i>
                    <h4 class="modal-title">Guía para editar un especialista</h4>
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
                    <h2><i class="fa fa-user-tie text-success m-r-sm"></i>Bonificaciones</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li class="active"><strong>Gestión Comercial / Bonificaciones</strong></li>
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

                    <form id="form" runat="server">
                        <asp:ScriptManager ID="ScriptManager1" runat="server" />

                        <div class="row">
                            <div class="col-sm-12">
                                <div class="ibox">

                                    <div class="ibox-content">

                                        <span class="text-muted small pull-right">Fitness People: <i class="fa fa-clock-o"></i>
                                            <asp:Literal ID="ltFechaHoy" runat="server"></asp:Literal>
                                        </span>

                                        <h2>Gestión de Bonificaciones de Ventas</h2>

                                        <p>
                                            Configura planes, escalas y objetivos. Usa el simulador para calcular la comisión.
                                        </p>




                                        <div class="clients-list">

                                            <ul class="nav nav-tabs">

                                                <li class="active">
                                                    <a data-toggle="tab" href="#tab-simulador">
                                                        <i class="fa fa-calculator"></i>Simulador
                                                    </a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#tab-planes">
                                                        <i class="fa fa-tags"></i>Planes
                                                    </a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#tab-escalas">
                                                        <i class="fa fa-signal"></i>Escalas
                                                    </a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#tab-objetivos">
                                                        <i class="fa fa-bullseye"></i>Objetivos
                                                    </a>
                                                </li>

                                                <li>
                                                    <a data-toggle="tab" href="#tab-reporte">
                                                        <i class="fa fa-bar-chart"></i>Reporte
                                                    </a>
                                                </li>

                                            </ul>


                                            <div class="tab-content">

                                                <!-- ===================== -->
                                                <!-- TAB SIMULADOR -->
                                                <!-- ===================== -->

                                                <%--<div id="tab-simulador" class="tab-pane active">

                                                    <div class="panel-body">

                                                        <div class="table-responsive">
                                                            <table class="table table-bordered">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Plan</th>
                                                                        <th>Cantidad</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody id="tablaPlanesSimulador">
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <hr />

                                                        <h4>Resultado</h4>

                                                        <p>Puntos Mix: <b><span id="lblMix">0</span></b></p>
                                                        <p>Escala: <b><span id="lblEscala">-</span></b></p>
                                                        <p>Comisión: <b><span id="lblComision">$0</span></b></p>

                                                        <div class="alert alert-success mt-3" style="margin-top: 15px;">
                                                            <span id="lblRecomendacion"></span>
                                                        </div>


                                                        <h4>Detalle de Comisión</h4>

                                                        <div class="table-responsive">
                                                            <table class="table table-bordered" id="tablaDetalleComision">

                                                                <thead>
                                                                    <tr>
                                                                        <th>Plan</th>
                                                                        <th>Cantidad</th>
                                                                        <th>Objetivo</th>
                                                                        <th>Comisión Unidad</th>
                                                                        <th>Comisión Total</th>
                                                                    </tr>
                                                                </thead>

                                                                <tbody></tbody>

                                                            </table>
                                                        </div>


                                                        <div class="alert alert-success mt-3">

                                                            <b>Cómo subir de escala:</b>

                                                            <div id="lblSimulacion"></div>

                                                        </div>

                                                    </div>

                                                </div>--%>

                                                <div id="tab-simulador" class="tab-pane active">

                                                    <div class="panel-body">

                                                        <div class="row">

                                                            <!-- ========================= -->
                                                            <!-- PLANES (4 COLUMNAS) -->
                                                            <!-- ========================= -->
                                                            <div class="col-md-4">

                                                                <h4>Ventas</h4>

                                                                <div class="table-responsive">
                                                                    <table class="table table-bordered table-striped">
                                                                        <thead>
                                                                            <tr>
                                                                                <th>Plan</th>
                                                                                <th>Cantidad</th>
                                                                            </tr>
                                                                        </thead>

                                                                        <tbody id="tablaPlanesSimulador"></tbody>

                                                                    </table>
                                                                </div>

                                                            </div>


                                                            <!-- ========================= -->
                                                            <!-- RESULTADOS (8 COLUMNAS) -->
                                                            <!-- ========================= -->
                                                            <div class="col-md-8">

                                                                <h4>Resultado</h4>

                                                                <div class="row">

                                                                    <div class="col-md-4">
                                                                        <div class="well text-center">
                                                                            <b>Puntos Mix</b>
                                                                            <h3><span id="lblMix">0</span></h3>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <div class="well text-center">
                                                                            <b>Escala</b>
                                                                            <h3><span id="lblEscala">-</span></h3>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-4">
                                                                        <div class="well text-center">
                                                                            <b>Comisión</b>
                                                                            <h3><span id="lblComision">$0</span></h3>
                                                                        </div>
                                                                    </div>

                                                                </div>


                                                                <!-- RECOMENDACION -->

                                                                <div class="alert alert-success" style="margin-top: 10px;">
                                                                    <span id="lblRecomendacion"></span>
                                                                </div>


                                                                <!-- DETALLE -->

                                                                <h4>Detalle de Comisión</h4>

                                                                <div class="table-responsive">
                                                                    <table class="table table-bordered table-striped" id="tablaDetalleComision">

                                                                        <thead>
                                                                            <tr>
                                                                                <th>Plan</th>
                                                                                <th>Cantidad</th>
                                                                                <th>Objetivo</th>
                                                                                <th>Comisión Unidad</th>
                                                                                <th>Comisión Total</th>
                                                                            </tr>
                                                                        </thead>

                                                                        <tbody></tbody>

                                                                    </table>
                                                                </div>


                                                                <!-- SIMULACION -->

                                                                <div class="alert alert-info" style="margin-top: 10px;">

                                                                    <b>Cómo subir de escala</b>

                                                                    <div id="lblSimulacion"></div>

                                                                </div>

                                                            </div>

                                                        </div>

                                                    </div>

                                                </div>


                                                <!-- ===================== -->
                                                <!-- TAB PLANES -->
                                                <!-- ===================== -->

                                                <div id="tab-planes" class="tab-pane">

                                                    <div class="panel-body">

                                                        <h3>Configuración de Planes</h3>

                                                        <div class="row">

                                                            <div class="col-md-3">
                                                                <label>Nombre</label>
                                                                <asp:TextBox ID="txtNombrePlan" runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <label>Valor</label>
                                                                <asp:TextBox ID="txtValorPlan" runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <label>Factor Mix</label>
                                                                <asp:TextBox ID="txtFactorMix" runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <label>Es Mensual</label>
                                                                <asp:DropDownList ID="ddlEsMensual"
                                                                    runat="server"
                                                                    CssClass="form-control">

                                                                    <asp:ListItem Text="No" Value="0" />
                                                                    <asp:ListItem Text="Sí" Value="1" />

                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="col-md-2" style="margin-top: 25px">
                                                                <button type="button" class="btn btn-primary" onclick="guardarPlan()">
                                                                    Guardar Plan
                                                                </button>
                                                            </div>
                                                        </div>
                                                        <hr />


                                                        <div class="table-responsive">
                                                            <table class="table table-striped table-bordered" id="tablaPlanes">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Plan</th>
                                                                        <th>Valor</th>
                                                                        <th>Factor Mix</th>
                                                                        <th>Mensual</th>
                                                                        <th>Acciones</th>
                                                                    </tr>
                                                                </thead>

                                                                <tbody>
                                                                </tbody>

                                                            </table>
                                                        </div>



                                                        <input type="hidden" id="idPlanEditar" value="0" />

                                                    </div>
                                                </div>


                                                <!-- ===================== -->
                                                <!-- TAB ESCALAS -->
                                                <!-- ===================== -->

                                                <div id="tab-escalas" class="tab-pane">

                                                    <div class="panel-body">

                                                        <h3>Configuración de Escalas</h3>

                                                        <div class="row">

                                                            <div class="col-md-3">
                                                                <label>Nombre Escala</label>
                                                                <asp:TextBox ID="txtNombreEscala"
                                                                    runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <label>Puntos mínimos</label>
                                                                <asp:TextBox ID="txtPuntosMin"
                                                                    runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <label>Puntos máximos</label>
                                                                <asp:TextBox ID="txtPuntosMax"
                                                                    runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-3" style="margin-top: 25px">
                                                                <button type="button"
                                                                    id="btnGuardarEscala"
                                                                    class="btn btn-primary"
                                                                    onclick="guardarEscala()">
                                                                    Guardar
                                                                </button>

                                                            </div>

                                                        </div>

                                                        <hr />

                                                        <div class="table-responsive">
                                                            <table id="tablaEscalas" class="table table-striped table-bordered">

                                                                <thead>

                                                                    <tr>
                                                                        <th>Escala</th>
                                                                        <th>Puntos Min</th>
                                                                        <th>Puntos Max</th>
                                                                        <th>Acciones</th>
                                                                    </tr>

                                                                </thead>

                                                                <tbody></tbody>

                                                            </table>
                                                        </div>


                                                        <input type="hidden" id="idEscalaEditar" value="0" />

                                                    </div>
                                                </div>


                                                <!-- ===================== -->
                                                <!-- TAB OBJETIVOS -->
                                                <!-- ===================== -->

                                                <div id="tab-objetivos" class="tab-pane">

                                                    <div class="panel-body">

                                                        <h3>Configuración de Objetivos</h3>


                                                        <div class="row">

                                                            <div class="col-md-3">

                                                                <label>Escala</label>

                                                                <asp:DropDownList ID="ddlEscala"
                                                                    runat="server"
                                                                    CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="col-md-3">

                                                                <label>Plan</label>

                                                                <asp:DropDownList ID="ddlPlan"
                                                                    runat="server"
                                                                    CssClass="form-control">
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="col-md-2">

                                                                <label>Cantidad Objetivo</label>

                                                                <asp:TextBox ID="txtCantidadObjetivo"
                                                                    runat="server"
                                                                    CssClass="form-control"></asp:TextBox>

                                                            </div>

                                                            <div class="col-md-2">

                                                                <label>Comisión Unidad</label>

                                                                <asp:TextBox ID="txtComisionUnidad"
                                                                    runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                            <div class="col-md-2" style="margin-top: 25px">

                                                                <button type="button"
                                                                    onclick="guardarObjetivo()"
                                                                    id="btnGuardarObjetivo"
                                                                    class="btn btn-primary">
                                                                    Guardar
                                                                </button>
                                                            </div>
                                                        </div>

                                                        <hr />

                                                        <div class="table-responsive">
                                                            <table id="tablaObjetivos"
                                                                class="table table-striped table-bordered">

                                                                <thead>
                                                                    <tr>
                                                                        <th>Escala</th>
                                                                        <th>Plan</th>
                                                                        <th>Objetivo</th>
                                                                        <th>Comisión</th>
                                                                        <th></th>

                                                                    </tr>

                                                                </thead>

                                                                <tbody></tbody>

                                                            </table>
                                                        </div>

                                                        <input type="hidden" id="idObjetivoEditar" value="0" />
                                                    </div>
                                                </div>

                                                <!-- ===================== -->
                                                <!-- TAB REPORTE -->
                                                <!-- ===================== -->

                                                <div id="tab-reporte" class="tab-pane">

                                                    <div class="panel-body">

                                                        <h3>Reporte general</h3>

                                                        <div class="table-responsive">
                                                            <asp:GridView ID="gvReporte"
                                                                runat="server"
                                                                CssClass="table table-striped table-bordered">
                                                            </asp:GridView>
                                                        </div>

                                                    </div>
                                                </div>


                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </form>

                    <%--Fin Contenido!!!!--%>
                </div>
            </div>

            <%--         <uc1:footer runat="server" ID="footer" />--%>
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

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Validación de campos de entrada -->
    <script>

        $("#form").validate({
            rules: {
                txbNombreContacto: {
                    required: true,
                    minlength: 3
                },
                txbTelefonoContacto: {
                    required: true,
                    minlength: 10
                },
                txbCorreoContacto: {
                    required: true,
                    maxlength: 150
                },
                ddlEmpresa: {
                    required: true
                },
                ddlStatusLead: {
                    required: true
                },
                txbFechaPrim: {
                    required: true
                },
                txbFechaProx: {
                    required: true,
                },
                txbValorPropuesta: {
                    required: true,
                },
                txaObservaciones: {
                    required: true,
                },
            }
        });

        $('.chosen-select').chosen({ width: "100%", disable_search_threshold: 10, no_results_text: "Sin resultados" });

    </script>


    <script type="text/javascript">
        Sys.Application.add_load(function () {
            console.log("Script cargado");

            $('#buscador').on('keyup', function () {
                var valorBusqueda = $(this).val().toLowerCase();
                console.log("Buscando: " + valorBusqueda); // Esto debería aparecer en consola

                var filas = $('.table tbody tr');
                console.log("Cantidad de filas: " + filas.length); // ¿Cuántas encuentra?

                filas.filter(function () {
                    var texto = $(this).text().toLowerCase();
                    console.log("Fila: " + texto); // Verifica que tenga contenido
                    $(this).toggle(texto.indexOf(valorBusqueda) > -1);
                });
            });
        });
    </script>
    <script type="text/javascript">
        function confirmarGestion() {
            return confirm("¿Está seguro de gestionar el contacto seleccionado?");
        }

        // Inicializa tooltips (si usas Bootstrap)
        $(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>


    <%--ZONA PLANES--%>

    <script>
        function cargarPlanes() {

            $.ajax({
                type: "POST",
                url: "bonificaciones.aspx/ObtenerPlanes",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    console.log("respuesta:", response);

                    var datos = response.d;

                    if (!datos || datos.length == 0) {
                        $("#tablaPlanes tbody").html("<tr><td colspan='5'>Sin datos</td></tr>");
                        return;
                    }

                    var filas = "";

                    datos.forEach(function (p) {

                        filas += "<tr>";

                        filas += "<td>" + p.Nombre + "</td>";
                        filas += "<td>" + formatoMoneda(p.Valor) + "</td>";
                        filas += "<td>" + p.FactorMix + "</td>";
                        filas += "<td>" + (p.EsMensual ? "Sí" : "No") + "</td>";

                        filas += "<td>";

                        filas += "<a href='#' onclick='editarPlan(" + p.Id + ")' class='btn btn-outline btn-primary m-r-xs' style='padding:1px 2px' title='Editar'>";
                        filas += "<i class='fa fa-edit'></i>";
                        filas += "</a>";

                        filas += "<a href='#' onclick='eliminarPlan(" + p.Id + ")' class='btn btn-outline btn-danger m-r-xs' style='padding:1px 2px' title='Eliminar'>";
                        filas += "<i class='fa fa-trash'></i>";
                        filas += "</a>";

                        filas += "</td>";

                        filas += "</tr>";

                    });

                    $("#tablaPlanes tbody").html(filas);

                },

                error: function (err) {
                    console.log("ERROR:", err);
                }

            });

        }
    </script>

    <script>
        function guardarPlan() {

            var id = $("#idPlanEditar").val();
            var nombre = $("#<%=txtNombrePlan.ClientID%>").val();
            var valor = $("#<%=txtValorPlan.ClientID%>").val();
            var factorMix = $("#<%=txtFactorMix.ClientID%>").val();
            var esMensual = $("#<%=ddlEsMensual.ClientID%>").val() == "1";

            var accion = 1;

            if (id != 0)
                accion = 2;

            $.ajax({
                type: "POST",
                url: "bonificaciones.aspx/GuardarPlan",
                data: JSON.stringify({
                    accion: accion,
                    id: parseInt(id),
                    nombre: nombre,
                    valor: parseFloat(valor),
                    factorMix: parseFloat(factorMix),
                    esMensual: esMensual
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    console.log("Guardado:", response);

                    cargarPlanes();
                    cargarPlanesCombo();

                    $("#idPlanEditar").val(0);
                    $("#<%=txtNombrePlan.ClientID%>").val("");
                    $("#<%=txtValorPlan.ClientID%>").val("");
                    $("#<%=txtFactorMix.ClientID%>").val("");

                    $("#btnGuardarPlan").text("Guardar Plan");
                },

                error: function (err) {
                    console.log("Error:", err);
                }
            });
        }
    </script>

    <script>
        function editarPlan(id) {

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/ObtenerPlanPorId",

                data: JSON.stringify({
                    id: id
                }),

                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    var p = response.d;

                    $("#<%=txtNombrePlan.ClientID%>").val(p.Nombre);
                    $("#<%=txtValorPlan.ClientID%>").val(p.Valor);
                    $("#<%=txtFactorMix.ClientID%>").val(p.FactorMix);
                    $("#<%=ddlEsMensual.ClientID%>").val(p.EsMensual ? "1" : "0");

                    $("#idPlanEditar").val(p.Id);
                    $("#btnGuardarPlan").text("Actualizar Plan");
                }
            });
        }
    </script>

    <script>
        function eliminarPlan(id) {

            if (!confirm("¿Desea eliminar este plan?"))
                return;

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/EliminarPlan",

                data: JSON.stringify({
                    id: id
                }),

                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function () {

                    cargarPlanes();

                },
                error: function (err) {
                    console.log("Error:", err);
                }
            });
        }
    </script>

    <script>
        $(document).ready(function () {
            cargarPlanes();
        });
    </script>

    <script>
        function cargarPlanesCombo() {

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/ObtenerPlanes",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    console.log("Planes:", response);

                    var datos = response.d;
                    var combo = $("#ddlPlan");

                    combo.empty();
                    combo.append("<option value='0'>Seleccione</option>");

                    if (datos && datos.length > 0) {

                        datos.forEach(function (p) {

                            combo.append(
                                "<option value='" + p.Id + "'>" + p.Nombre + "</option>"
                            );

                        });

                    }

                }

            });

        }

    </script>

    <%--ZONA ESCALAS--%>
    <script>

        $(document).ready(function () {

            console.log("Página lista");

            cargarEscalas();

        });

        function cargarEscalas() {

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/ObtenerEscalas",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    var datos = response.d;

                    var filas = "";

                    if (!datos || datos.length == 0) {

                        filas = "<tr><td colspan='4'>Sin datos</td></tr>";

                    } else {

                        datos.forEach(function (e) {

                            filas += "<tr>";

                            filas += "<td>" + e.Nombre + "</td>";
                            filas += "<td>" + e.PuntosMin + "</td>";
                            filas += "<td>" + e.PuntosMax + "</td>";

                            filas += "<td>";

                            filas += "<a href='#' onclick='editarEscala(" + e.IdEscala + ")' class='btn btn-outline btn-primary m-r-xs' style='padding:1px 2px' title='Editar'>";
                            filas += "<i class='fa fa-edit'></i>";
                            filas += "</a>";

                            filas += "<a href='#' onclick='eliminarEscala(" + e.IdEscala + ")' class='btn btn-outline btn-danger m-r-xs' style='padding:1px 2px' title='Eliminar'>";
                            filas += "<i class='fa fa-trash'></i>";
                            filas += "</a>";

                            filas += "</td>";

                            filas += "</tr>";

                        });

                    }

                    $("#tablaEscalas tbody").html(filas);

                }

            });

        }

    </script>

    <script>
        function guardarEscala() {

            var nombre = $("#txtNombreEscala").val().trim();
            var min = $("#txtPuntosMin").val();
            var max = $("#txtPuntosMax").val();

            if (nombre == "" || min == "" || max == "") {

                alert("Debe completar todos los campos");
                return;

            }

            var idEscalaEditar = $("#idEscalaEditar").val();

            var accion = (idEscalaEditar == 0) ? 1 : 2;

            console.log("Acción:", accion);
            console.log("IdEscalaEditar:", idEscalaEditar);

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/GuardarEscala",

                data: JSON.stringify({

                    accion: accion,
                    id: idEscalaEditar,
                    nombre: nombre,
                    puntosMin: min,
                    puntosMax: max

                }),

                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    if (response.d.ok) {

                        limpiarEscala();

                        cargarEscalas();

                        cargarEscalasCombo();

                        idEscalaEditar = 0;

                        $("#btnGuardarEscala").text("Guardar");

                    }
                    else {

                        alert("Error código: " + response.d.errorId);

                    }

                },

                error: function (err) {

                    console.log("ERROR:", err);

                }

            });

        }
    </script>

    <script>
        function editarEscala(id) {

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/ObtenerEscalaPorId",

                data: JSON.stringify({
                    id: id
                }),

                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    var e = response.d;

                    $("#txtNombreEscala").val(e.Nombre);
                    $("#txtPuntosMin").val(e.PuntosMin);
                    $("#txtPuntosMax").val(e.PuntosMax);

                    $("#idEscalaEditar").val(e.Id);
                    $("#btnGuardarEscala").text("Actualizar Escala");
                }

            });

        }
    </script>

    <script>
        function limpiarEscala() {

            $("#txtNombreEscala").val("");
            $("#txtPuntosMin").val("");
            $("#txtPuntosMax").val("");

        }
    </script>

    <script>
        function eliminarEscala(id) {

            if (!confirm("¿Eliminar escala?"))
                return;

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/EliminarEscala",

                data: JSON.stringify({ id: id }),

                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    if (response.d.ok) {

                        cargarEscalas();
                    }
                }
            });
        }
    </script>

    <script>
        function cargarEscalasCombo() {

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/ObtenerEscalas",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    console.log("Escalas:", response);

                    var datos = response.d;
                    var combo = $("#ddlEscala");

                    combo.empty();

                    combo.append("<option value='0'>Seleccione</option>");

                    if (datos && datos.length > 0) {

                        datos.forEach(function (e) {

                            combo.append(
                                "<option value='" + e.IdEscala + "'>" + e.Nombre + "</option>"
                            );

                        });

                    }

                },

                error: function (err) {

                    console.log("Error escalas:", err);

                }

            });

        }


    </script>

    <%--    OBJETIVOS PLAN--%>

    <script>

        $(document).ready(function () {

            cargarObjetivos();

        });

        function cargarObjetivos() {

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/ObtenerObjetivos",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    var datos = response.d;

                    var filas = "";

                    if (!datos || datos.length == 0) {

                        filas = "<tr><td colspan='5'>Sin datos</td></tr>";

                    }
                    else {

                        datos.forEach(function (o) {

                            filas += "<tr>";

                            filas += "<td>" + o.Escala + "</td>";
                            filas += "<td>" + o.Plan + "</td>";
                            filas += "<td>" + o.CantidadObjetivo + "</td>";
                            filas += "<td>" + o.ValorUnitarioComision + "</td>";

                            filas += "<td>";

                            filas += "<a href='#' onclick='editarObjetivo(" + o.IdObjetivo + ")' class='btn btn-outline btn-primary m-r-xs' style='padding:1px 2px'>";
                            filas += "<i class='fa fa-edit'></i>";
                            filas += "</a>";

                            filas += "<a href='#' onclick='eliminarObjetivo(" + o.IdObjetivo + ")' class='btn btn-outline btn-danger m-r-xs' style='padding:1px 2px'>";
                            filas += "<i class='fa fa-trash'></i>";
                            filas += "</a>";

                            filas += "</td>";

                            filas += "</tr>";

                        });

                    }

                    $("#tablaObjetivos tbody").html(filas);

                }

            });

        }

    </script>

    <script>

        function guardarObjetivo() {

            var escala = $("#ddlEscala").val();
            var plan = $("#ddlPlan").val();
            var cantidad = $("#txtCantidadObjetivo").val();
            var comision = $("#txtComisionUnidad").val();

            if (escala == "" || plan == "" || cantidad == "" || comision == "") {

                alert("Debe completar todos los campos");
                return;

            }

            var idEditar = $("#idObjetivoEditar").val();

            if (!idEditar) {
                idEditar = 0;
            }

            var accion = (parseInt(idEditar) === 0) ? 1 : 2;

            var datos = {

                accion: parseInt(accion),
                id: parseInt(idEditar),
                idEscala: parseInt(escala),
                idPlan: parseInt(plan),
                cantidadObjetivo: parseInt(cantidad),
                valorUnitarioComision: parseFloat(comision)

            };

            console.log("Datos enviados:", datos);

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/GuardarObjetivo",
                data: JSON.stringify(datos),

                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    console.log("Respuesta:", response);

                    if (response.d.ok) {

                        limpiarObjetivo();
                        cargarObjetivos();

                        $("#btnGuardarObjetivo").text("Guardar");
                        $("#idObjetivoEditar").val(0);

                    }
                    else {

                        alert("Error código: " + response.d.errorId);

                    }

                },

                error: function (err) {

                    console.log("ERROR AJAX:", err.responseText);

                }

            });

        }


    </script>

    <script>

        function editarObjetivo(id) {

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/ObtenerObjetivoPorId",

                data: JSON.stringify({ id: id }),

                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    var o = response.d;

                    $("#ddlEscala").val(o.IdEscala);
                    $("#ddlPlan").val(o.IdPlanSimulador);
                    $("#txtCantidadObjetivo").val(o.CantidadObjetivo);
                    $("#txtComisionUnidad").val(o.ValorUnitarioComision);

                    $("#idObjetivoEditar").val(o.Id);

                    $("#btnGuardarObjetivo").text("Actualizar Objetivo");

                }

            });

        }

    </script>

    <script>

        function eliminarObjetivo(id) {

            if (!confirm("¿Eliminar objetivo?"))
                return;

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/EliminarObjetivo",

                data: JSON.stringify({ id: id }),

                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    if (response.d.ok) {

                        cargarObjetivos();

                    }

                }

            });

        }

    </script>

    <script>
        function limpiarObjetivo() {

            $("#ddlEscala").val("");
            $("#ddlPlan").val("");
            $("#txtCantidadObjetivo").val("");
            $("#txtComisionUnidad").val("");

        }
    </script>


    <script>
        function formatoMoneda(valor) {
            return new Intl.NumberFormat('es-CO', {
                style: 'currency',
                currency: 'COP',
                minimumFractionDigits: 0
            }).format(valor);
        }
    </script>

    <script>
        function limpiarEscala() {

            $("#txtNombreEscala").val("");
            $("#txtPuntosMin").val("");
            $("#txtPuntosMax").val("");
        }
    </script>

    <script>

        $(document).ready(function () {
            cargarPlanesSimulador();
            cargarPlanes();
            cargarPlanesCombo();
            cargarEscalasCombo();
            cargarObjetivos();

            // Cuando el usuario escriba en cualquier input
            $("#txtAnualidad,#txtSemestre,#txtTrimestre,#txtMensual").on("input", function () {
                calcular();
            });

        });
    </script>


    <script>

        $(document).ready(function () {

            cargarPlanesSimulador();

            $(document).on("input", ".plan-cantidad", function () {
                console.log("evento input detectado");
                calcular();
            });

        });


        function calcular() {

            console.log("calcular ejecutado");

            var ventas = [];

            $(".plan-cantidad").each(function () {

                var idPlan = $(this).attr("planid");
                var cantidad = parseInt($(this).val()) || 0;

                ventas.push({
                    idPlan: parseInt(idPlan),
                    cantidad: cantidad
                });

            });

            console.log("ventas:", ventas);

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/CalcularSimulador",

                data: JSON.stringify({ ventas: ventas }),

                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    console.log("respuesta servidor:", response);

                    var r = response.d;

                    $("#lblMix").text(r.Mix);
                    $("#lblEscala").text(r.Escala);
                    var comision = parseFloat(r.Comision) || 0;

                    $("#lblComision").text(
                        comision.toLocaleString('es-CO', {
                            style: 'currency',
                            currency: 'COP',
                            minimumFractionDigits: 0
                        })
                    );
                    console.log("Mix recibido:", r.Mix);

                    obtenerRecomendacion(r.Mix);
                    obtenerSimulacionEscala(r.Mix);
                    obtenerDetalleComision(ventas, parseInt(r.Escala) || 0);
                },

                error: function (xhr) {

                    console.log("ERROR:", xhr.responseText);

                }

            });

        }


        function cargarPlanesSimulador() {

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/ObtenerPlanes",
                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    console.log("Planes:", response);

                    var planes = response.d;

                    $("#tablaPlanesSimulador").html("");

                    $.each(planes, function (i, p) {

                        var fila = `
                <tr>
                    <td>${p.Nombre}</td>
                    <td>
                        <input type="number"
                               class="form-control plan-cantidad"
                               planid="${p.Id}"
                               value="0">
                    </td>
                </tr>`;

                        $("#tablaPlanesSimulador").append(fila);

                    });

                    calcular();

                }

            });

        }

    </script>

    <script>
        success: function (response) {

            var r = response.d;

            $("#lblMix").text(r.Mix);
            $("#lblEscala").text(r.Escala);

            var comision = parseFloat(r.Comision) || 0;

            $("#lblComision").text(
                comision.toLocaleString('es-CO', {
                    style: 'currency',
                    currency: 'COP',
                    minimumFractionDigits: 0
                })
            );

            var mix = parseFloat(r.Mix.toString().replace(",", "."));
            console.log("Mix convertido:", mix);
            obtenerRecomendacion(mix);

        }
    </script>

    <script>
        function obtenerRecomendacion(mix) {

            var mixEnviar = mix.toString().replace(",", ".");

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/ObtenerRecomendacion",

                data: JSON.stringify({
                    mix: mixEnviar
                }),

                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    var r = response.d;

                    $("#lblRecomendacion").html(
                        "🚀 Te faltan <b>" + r.MixFaltante +
                        "</b> puntos mix para la escala <b>" +
                        r.Escala + "</b>"
                    );

                }

            });

        }
    </script>

    <script>

        function obtenerSimulacionEscala(mix) {

            console.log("Simulando escala con mix:", mix);

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/ObtenerSimulacionEscala",

                data: JSON.stringify({
                    mix: mix
                }),

                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    console.log("respuesta simulacion:", response);

                    var datos = response.d;

                    var html = "";

                    datos.forEach(function (p) {

                        if (p.CantidadNecesaria > 0) {

                            html += "✔ " + p.CantidadNecesaria + " " + p.Plan + "<br>";

                        }

                    });

                    $("#lblSimulacion").html(html);

                },

                error: function (xhr) {

                    console.log("ERROR simulacion:", xhr.responseText);

                }

            });

        }

    </script>

    <script>

        function obtenerDetalleComision(ventas) {

            $.ajax({

                type: "POST",
                url: "bonificaciones.aspx/ObtenerDetalleComision",

                data: JSON.stringify({
                    ventas: ventas
                }),

                contentType: "application/json; charset=utf-8",
                dataType: "json",

                success: function (response) {

                    var datos = response.d;

                    var filas = "";

                    datos.forEach(function (d) {

                        filas += "<tr>";
                        filas += "<td>" + d.Plan + "</td>";
                        filas += "<td>" + d.Cantidad + "</td>";
                        filas += "<td>" + d.Objetivo + "</td>";
                        filas += "<td>$" + d.ComisionUnidad + "</td>";
                        filas += "<td>$" + d.ComisionTotal + "</td>";
                        filas += "</tr>";

                    });

                    $("#tablaDetalleComision tbody").html(filas);

                }

            });

        }

    </script>




</body>

</html>


