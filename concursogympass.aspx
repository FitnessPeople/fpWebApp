<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="concursogympass.aspx.cs" Inherits="fpWebApp.concursogympass" %>

<%@ Register Src="~/controles/navbar.ascx" TagPrefix="uc1" TagName="navbar" %>
<%@ Register Src="~/controles/header.ascx" TagPrefix="uc1" TagName="header" %>
<%@ Register Src="~/controles/footer.ascx" TagPrefix="uc1" TagName="footer" %>
<%@ Register Src="~/controles/rightsidebar.ascx" TagPrefix="uc1" TagName="rightsidebar" %>
<%@ Register Src="~/controles/indicadoresconcursogympass.ascx" TagPrefix="uc1" TagName="indicadoresconcursogympass" %>
<%@ Register Src="~/controles/paginasperfil.ascx" TagPrefix="uc1" TagName="paginasperfil" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Fitness People | Concurso GymPass</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- FooTable -->
    <%--<link href="css/plugins/footable/footable.core.css" rel="stylesheet" />--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.bootstrap.min.css" rel="stylesheet" />

    <!-- Morris -->
    <link href="css/plugins/morris/morris-0.4.3.min.css" rel="stylesheet" />
    
    <!-- c3 Charts -->
    <link href="css/plugins/c3/c3.min.css" rel="stylesheet">

    <link href="css/plugins/clockpicker/clockpicker.css" rel="stylesheet">

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <style type="text/css" media="print">
        body {
            visibility: hidden;
            display: none;
        }
    </style>

    <script>
        function changeClass() {
            var element1 = document.querySelector("#concursogympass");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#sistema+");
            element2.classList.remove("collapse");
        }
    </script>

    <style>
        .clockpicker-popover {
            z-index: 9999 !important;
        }

        .float-e-margins .btn {
            margin-bottom: 0;
        }
    </style>
</head>
<body onload="changeClass()">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <i class="fa fa-id-card modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para visualizar los usuarios inscritos en Gym Pass</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo gestionar el listado de forma rápida y sencilla.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Busca y filtra afiliados</b><br />
                        Usa el buscador para encontrar afiliados específicos.<br />
                        <i class="fa-solid fa-magnifying-glass"></i> Filtra por: 
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i> <b>Nombre</b>, 
                        <i class="fa-solid fa-address-card" style="color: #0D6EFD;"></i> <b>Cédula</b>, 
                        <i class="fa-solid fa-envelope" style="color: #0D6EFD;"></i> <b>Correo</b>, 
                        <i class="fa-solid fa-mobile" style="color: #0D6EFD;"></i> <b>Celular</b>,
                        <i class="fa-solid fa-user-tie" style="color: #0D6EFD;"></i> <b>Cargo</b> o 
                        <i class="fa-solid fa-circle" style="color: #0D6EFD;"></i> <b>Estado</b><br />
                        <i class="fa-solid fa-star" style="color: #FECE32;"></i> Tip: ¡Combina filtros para resultados más precisos!
                    <br />
                        <br />
                        <b>Paso 2: Revisa la tabla de resultados</b><br />
                        La tabla muestra toda la información clave de cada afiliado.<br />
                        En la columna "Acciones" encontrarás estas opciones:<br />
                        <i class="fa fa-edit" style="color: #1AB394;"></i> <b>Editar:</b> Modifica los datos del afiliado.<br />
                        <i class="fa fa-trash" style="color: #DC3545;"></i> <b>Eliminar:</b> Da de baja al afiliado.
                    <br />
                        <br />
                        <b>Paso 3: Acciones adicionales</b><br />
                        Al lado opuesto del buscador encontrarás dos botones útiles:<br />
                        <i class="fa-solid fa-file-export" style="color: #212529;"></i> <b>Exportar a Excel:</b>
                        Genera un archivo Excel con los datos visibles en la tabla.<br />
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
                    <h2><i class="fa fa-trophy text-success m-r-sm"></i>Concurso GymPass</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Sistema</li>
                        <li class="active"><strong>Concurso GymPass</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight">
                <div class="row animated fadeInDown">
                    <%--Inicio Contenido!!!!--%>

                    <uc1:indicadoresconcursogympass runat="server" ID="indicadoresconcursogympass" />

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
                            <h5>Lista de inscritos</h5>
                            <div class="ibox-tools">
                                <a class="collapse-link">
                                    <i class="fa fa-chevron-up"></i>
                                </a>
                                <a class="close-link">
                                    <i class="fa fa-times"></i>
                                </a>
                            </div>
                        </div>
                        <div class="ibox-content">
                            <form runat="server" id="form1">
                                <div class="row" style="font-size: 12px;" runat="server" id="divBotonesLista">
                                    <div class="col-lg-6 form-horizontal">
                                        <div class="form-group">
                                            <div class="form-group" id="filter-form-container" style="margin-left: 28px;"></div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 form-horizontal">
                                        <asp:LinkButton ID="lbExportarExcel" runat="server" CausesValidation="false"
                                            CssClass="btn btn-info pull-right dim m-l-md" style="font-size: 12px;"
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
                                            <th data-sortable="false" data-breakpoints="xs">Documento</th>
                                            <th data-breakpoints="xs">Nombres</th>
                                            <th data-breakpoints="xs">Apellidos</th>
                                            <th data-breakpoints="xs">Correo</th>
                                            <th data-breakpoints="xs">Teléfono</th>
                                            <th data-breakpoints="xs">Fecha Registro</th>
                                            <th data-breakpoints="xs">Fecha Inicio</th>
                                            <th data-breakpoints="xs">Sede</th>
                                            <th data-breakpoints="xs">Código Embajador</th>
                                            <th data-breakpoints="xs">Ver Captura</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="rpConcursoGymPass" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Eval("documento") %></td>
                                                    <td><%# Eval("nombres") %></td>
                                                    <td><%# Eval("apellidos") %></td>
                                                    <td><%# Eval("correo") %></td>
                                                    <td><%# Eval("telefono") %></td>
                                                    <td><%# Eval("fechaRegistro", "{0:dd/MM/yyyy}") %></td>
                                                    <td><%# Eval("fechaInicio", "{0:dd/MM/yyyy}") %></td>
                                                    <td><%# Eval("sede") %></td>
                                                    <td><%# Eval("CodigoEmb") %></td>
                                                    <td style="text-align: center; vertical-align: middle;">
                                                        <a href='<%# "https://fitnesspeoplecmdcolombia.com/img/estudiafit/concurso-gympass/" + Eval("capturaArchivo") %>' 
                                                            target="_blank" class="btn btn-danger btn-outline m-r-xs"
                                                            style="padding: 1px 2px 1px 2px; margin-bottom: 0px;"> 
                                                            <i class="fa-solid fa-camera"></i>
                                                        </a>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>

                                <div class="wrapper wrapper-content animated fadeInRight">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="ibox float-e-margins">
                                                <div class="ibox-title">
                                                    <h5>Embajador con más códigos registrados</h5>
                                                </div>
                                                <div class="ibox-content">
                                                    <div id="barras"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                            <div class="ibox float-e-margins">
                                                <div class="ibox-title">
                                                    <h5>Cantidad de personas registradas por Embajador</h5>
                                                </div>
                                                <div class="ibox-content">
                                                    <div>
                                                        <canvas id="doughnutChart" height="140"></canvas>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="ibox float-e-margins">
                                                <div class="ibox-title">
                                                    <h5>Porcentaje de personas registradas por Embajador</h5>
                                                </div>
                                                <div class="ibox-content">
                                                    <div>
                                                        <div id="pie"></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
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
    <%--<script src="js/plugins/footable/footable.all.min.js"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/3.1.6/footable.min.js"></script>

    <!-- Peity -->
    <script src="js/plugins/peity/jquery.peity.min.js"></script>
    <script src="js/demo/peity-demo.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- jQuery UI -->
    <script src="js/plugins/jquery-ui/jquery-ui.min.js"></script>

    <!-- Data picker -->
    <script src="js/plugins/datapicker/bootstrap-datepicker.js"></script>

    <!-- Clock picker -->
    <script src="js/plugins/clockpicker/clockpicker.js"></script>
    

    <!-- Gráficas -->
    <!-- ChartJS-->
    <script src="js/plugins/chartJs/Chart.min.js"></script>
    <script src="js/demo/chartjs-demo.js"></script>

    <!-- d3 and c3 charts -->
    <script src="js/plugins/d3/d3.min.js"></script>
    <script src="js/plugins/c3/c3.min.js"></script>
    

    <!-- Page-Level Scripts -->
    <script>
        $('.footable').footable();
    </script>

    <script>
        $(document).ready(function () {

            // --- c3: barras categóricas ---
            c3.generate({
                bindto: '#barras',
                data: {
                    columns: columnasJS, // remplazado por el server: [["Emb1",10],["Emb2",5],...]
                    type: 'bar',
                    colors: coloresJS     // objeto { "Emb1":"#...", "Emb2":"#..." }
                },
                axis: {
                    x: {
                        type: 'category',
                        categories: categoriasJS, // [""] tal como antes
                        tick: {
                            rotate: 45,
                            multiline: false
                        }
                    },
                    y: {
                        tick: {
                            format: d3.format("d")
                        }
                    }
                },
                tooltip: {
                    format: {
                        title: function () { return 'Embajadores'; }
                    }
                },
                legend: {
                    show: true
                },
                bar: {
                    width: {
                        ratio: 0.7
                    }
                }
            });

            // --- c3: pie ---
            // Extraemos datos para Chart.js
            var nombres = columnasJS.map(function (c) { return c[0]; });  // ["Emb1","Emb2",...]
            var valores = columnasJS.map(function (c) { return c[1]; });  // [10,20,...]
            var colores = nombres.map(function (n) { return coloresJS[n]; });

            var ctx = document.getElementById("doughnutChart").getContext("2d");

            new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: nombres,
                    datasets: [{
                        data: valores,
                        backgroundColor: colores
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            position: 'bottom'
                        },
                        tooltip: {
                            callbacks: {
                                label: function (context) {
                                    let label = context.label || '';
                                    let value = context.raw || 0;
                                    return `${label}: ${value}`;
                                }
                            }
                        }
                    }
                }
            });

            // --- Chart.js: doughnut/pie ---
            c3.generate({
                bindto: '#pie',
                data: {
                    columns: columnasJS,  // [["Emb1",10],["Emb2",5],...]
                    type: 'pie',
                    colors: coloresJS
                },
                legend: {
                    position: 'right'
                },
                tooltip: {
                    format: {
                        value: function (value, ratio, id) {
                            return value; // muestra la cantidad
                        }
                    }
                }
            });

        });
    </script>

</body>

</html>
