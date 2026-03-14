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

    <%--        Validación postback modal--%>
    <script>
        // Función para reabrir el modal si fue cerrado por un PostBack
        function reopenModal() {
            setTimeout(function () {
                $('#ModalContacto').modal('show');
            }, 100);
        }

        // Detectar cuándo se actualiza el UpdatePanel y reabrir el Modal
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            reopenModal();
        });
    </script>

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

    <%--        Validar botón Agregar --%>
    <script>
        function validarFormulario() {
            const nombre = document.getElementById('txbNombreContacto').value.trim();
            const telefono = document.getElementById('txbTelefonoContacto').value.trim();
            const correo = document.getElementById('txbCorreoContacto').value.trim();
            const fechaPrim = document.getElementById('txbFechaPrim').value.trim();
            const fechaProx = document.getElementById('txbFechaProx').value.trim();
            const valor = document.getElementById('txbValorPropuesta').value.trim();
            const status = document.getElementById('ddlStatusLead').value;

<%--            //const boton = document.getElementById('<%= btnAgregar.ClientID %>');--%>

            const camposCompletos =
                nombre !== "" &&
                telefono !== "" &&
                correo !== "" &&
                fechaPrim !== "" &&
                fechaProx !== "" &&
                valor !== "" &&
                status !== "0";

            if (camposCompletos) {
                boton.disabled = false;
                boton.classList.remove('btn-secondary');
                boton.classList.add('btn-primary');
            } else {
                boton.disabled = true;
                boton.classList.remove('btn-primary');
                boton.classList.add('btn-secondary');
            }
        }

        // Asignar eventos a cada campo
        document.addEventListener("DOMContentLoaded", function () {
            const campos = [
                'txbNombreContacto', 'txbTelefonoContacto', 'txbCorreoContacto',
                'txbFechaPrim', 'txbFechaProx', 'txbValorPropuesta',
                , 'ddlStatusLead'
            ];

            campos.forEach(id => {
                const campo = document.getElementById(id);
                if (campo) {
                    campo.addEventListener('input', validarFormulario);
                    campo.addEventListener('change', validarFormulario);
                }
            });

            validarFormulario(); // Ejecutar al cargar
        });
    </script>

    <%--        Abrir modal--%>
    <script>
        function AbrirModal() {
            $("#ModalContacto").modal("show");
        }
    </script>

    <%--        Formatear telefono --%>
    <script>
        function formatearTelefono(input) {
            let num = input.value.replace(/\D/g, ''); // Eliminar caracteres no numéricos

            // Si el número tiene 10 dígitos, es un celular
            if (num.length === 10) {
                input.value = num.substring(0, 3) + '-' + num.substring(3, 6) + '-' + num.substring(6, 10);
            }
            // Si el número tiene 7 o más dígitos, es un teléfono fijo
            else if (num.length > 6) {
                input.value = '(' + num.substring(0, 3) + ') ' + num.substring(3, 6) + '-' + num.substring(6, 10);
            } else {
                input.value = num;
            }
        }
    </script>

    <%--        Formatear solo letraas --%>
    <script>
        function validarSoloLetras(input) {
            // Eliminar cualquier caracter que no sea letra o espacio
            input.value = input.value.replace(/[^a-zA-ZáéíóúÁÉÍÓÚñÑ\s]/g, '');
        }
    </script>

    <%--        Formatear solo correo --%>
    <script>
        function validarCorreo(input) {
            const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
            if (!emailRegex.test(input.value)) {
                input.setCustomValidity('Por favor ingrese un correo electrónico válido.');
            } else {
                input.setCustomValidity('');
            }
        }
    </script>

    <!--        SweetAlert2 CDN -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <%--         Filtro de buscar--%>
    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $('#buscador').on('keyup', function () {
                var valorBusqueda = $(this).val().toLowerCase();
                $('.table tbody tr').filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(valorBusqueda) > -1);
                });
            });
        });
    </script>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
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

    <%--                                    <div style="display: flex; align-items: center; margin-bottom: 15px;">

                                            <div class="input-group">
                                                <input type="text" id="buscador" placeholder="Buscar" class="input form-control">

                                                <span class="input-group-btn">
                                                    <button type="button" class="btn btn-primary">
                                                        <i class="fa fa-search"></i>Buscar
                                                    </button>
                                                </span>

                                            </div>
                                        </div>--%>


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

                                                <div id="tab-simulador" class="tab-pane active">

                                                    <div class="panel-body">

                                                        <table class="table table-bordered">

                                                            <tr>
                                                                <td><b>Anualidad</b></td>
                                                                <td>
                                                                    <input type="number" id="txtAnualidad"
                                                                        class="form-control"
                                                                        onkeyup="calcular()" />
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td><b>Semestre</b></td>
                                                                <td>
                                                                    <input type="number" id="txtSemestre"
                                                                        class="form-control"
                                                                        onkeyup="calcular()" />
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td><b>Trimestre</b></td>
                                                                <td>
                                                                    <input type="number" id="txtTrimestre"
                                                                        class="form-control"
                                                                        onkeyup="calcular()" />
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td><b>Mensual</b></td>
                                                                <td>
                                                                    <input type="number" id="txtMensual"
                                                                        class="form-control"
                                                                        onkeyup="calcular()" />
                                                                </td>
                                                            </tr>

                                                        </table>

                                                        <hr />

                                                        <h4>Resultado</h4>

                                                        <p>Puntos Mix: <b><span id="lblMix">0</span></b></p>
                                                        <p>Escala: <b><span id="lblEscala">-</span></b></p>
                                                        <p>Comisión: <b><span id="lblComision">$0</span></b></p>

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

                                                                <asp:Button ID="btnGuardarPlan"
                                                                    runat="server"
                                                                    Text="Guardar"
                                                                    CssClass="btn btn-primary" />

                                                            </div>

                                                        </div>

                                                        <hr />

                                                        <asp:GridView ID="gvPlanes"
                                                            runat="server"
                                                            AutoGenerateColumns="False"
                                                            CssClass="table table-striped table-bordered">

                                                            <Columns>

                                                                <asp:BoundField DataField="Nombre" HeaderText="Plan" />
                                                                <asp:BoundField DataField="Valor" HeaderText="Valor" />
                                                                <asp:BoundField DataField="FactorMix" HeaderText="Factor Mix" />
                                                                <asp:BoundField DataField="EsMensual" HeaderText="Mensual" />

                                                            </Columns>

                                                        </asp:GridView>

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

                                                                <asp:Button ID="btnGuardarEscala"
                                                                    runat="server"
                                                                    Text="Guardar"
                                                                    CssClass="btn btn-primary" />

                                                            </div>

                                                        </div>

                                                        <hr />

                                                        <asp:GridView ID="gvEscalas"
                                                            runat="server"
                                                            AutoGenerateColumns="False"
                                                            CssClass="table table-striped table-bordered">

                                                            <Columns>

                                                                <asp:BoundField DataField="Nombre" HeaderText="Escala" />
                                                                <asp:BoundField DataField="PuntosMin" HeaderText="Min" />
                                                                <asp:BoundField DataField="PuntosMax" HeaderText="Max" />

                                                            </Columns>

                                                        </asp:GridView>

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

                                                                <asp:Button ID="btnGuardarObjetivo"
                                                                    runat="server"
                                                                    Text="Guardar"
                                                                    CssClass="btn btn-primary" />

                                                            </div>

                                                        </div>

                                                        <hr />

                                                        <asp:GridView ID="gvObjetivos"
                                                            runat="server"
                                                            AutoGenerateColumns="False"
                                                            CssClass="table table-striped table-bordered">

                                                            <Columns>

                                                                <asp:BoundField DataField="Escala" HeaderText="Escala" />
                                                                <asp:BoundField DataField="Plan" HeaderText="Plan" />
                                                                <asp:BoundField DataField="CantidadObjetivo" HeaderText="Objetivo" />
                                                                <asp:BoundField DataField="ComisionUnidad" HeaderText="Comisión" />

                                                            </Columns>

                                                        </asp:GridView>

                                                    </div>
                                                </div>


                                                <!-- ===================== -->
                                                <!-- TAB REPORTE -->
                                                <!-- ===================== -->

                                                <div id="tab-reporte" class="tab-pane">

                                                    <div class="panel-body">

                                                        <h3>Reporte general</h3>

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
        $(document).ready(function () {
            if (window.location.href.indexOf("empresaId") > -1) {
                // Activa la pestaña 2 manualmente
                $('#tab-2-tab').click(); // Reemplaza esto con el ID real del botón o tab que activa "Empresas"
            }
        });
    </script>

    <%--Contador de filas--%>
    <script type="text/javascript">
        function actualizarContador() {
            var visibles = $('#tablaContactos tbody tr:visible').length;
            $('#contadorFilas').text(visibles + ' contactos');
        }

        Sys.Application.add_load(function () {
            // Evento al escribir en el buscador
            $('#buscador').off('keyup').on('keyup', function () {
                var valorBusqueda = $(this).val().toLowerCase();

                $('#tablaContactos tbody tr').each(function () {
                    var textoFila = $(this).text().toLowerCase();
                    var coincide = textoFila.indexOf(valorBusqueda) > -1;
                    $(this).toggle(coincide);
                });

                actualizarContador();
            });

            // Actualiza el contador al cargar
            actualizarContador();
        });
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

    <script>
        function calcular() {

            var datos = {
                anual: $("#txtAnualidad").val(),
                semestre: $("#txtSemestre").val(),
                trimestre: $("#txtTrimestre").val(),
                mensual: $("#txtMensual").val()
            };

            $.ajax({
                type: "POST",
                url: "Bonificaciones.aspx/CalcularComision",
                data: JSON.stringify(datos),
                contentType: "application/json",
                success: function (r) {

                    $("#lblMix").text(r.d.PuntosMix);
                    $("#lblEscala").text(r.d.Escala);
                    $("#lblComision").text(r.d.Comision);

                }
            });
        }
    </script>



</body>

</html>


