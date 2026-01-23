<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="histclinutricion04.aspx.cs" Inherits="fpWebApp.histclinutricion04" %>

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

    <title>Fitness People | Nueva historia clínica</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.3/themes/smoothness/jquery-ui.css">

    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet" />

    <link href="css/plugins/clockpicker/clockpicker.css" rel="stylesheet">

    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />

    <!-- SweetAlert2 CDN -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            var element1 = document.querySelector("#nuevahistoria");
            element1.classList.replace("old", "active");
            var element2 = document.querySelector("#medico");
            element2.classList.remove("collapse");
        }

        function calculateIMC() {

            var peso = document.getElementById("txbPeso").value;
            var talla = document.getElementById("txbTalla").value;

            if (peso !== '' && talla !== '') {

                peso = parseFloat(peso);
                talla = parseFloat(talla);

                var peso_esperado = 0.75 * (talla - 150) + 50;

                talla = talla / 100;

                var tallax2 = talla * talla;
                var imc = peso / tallax2;

                imc = imc.toFixed(4);

                document.getElementById("txbIMC").value = imc;
                document.getElementById("txbPesoEsperado").value = peso_esperado;
            }
        }

        function calculatePorcGraso() {

            var peso = document.getElementById("txbPeso").value;
            var talla = document.getElementById("txbTalla").value;

            var tricipital = document.getElementById("txbPliegueTricipital").value;
            var iliocrestal = document.getElementById("txbPliegueIliocrestal").value;
            var abdominal = document.getElementById("txbPliegueAbdominal").value;
            var subescapular = document.getElementById("txbPliegueSubescapular").value;
            var muslo = document.getElementById("txbPliegueMuslo").value;
            var pantorrilla = document.getElementById("txbPlieguePantorrilla").value;

            if (tricipital !== '' && iliocrestal !== '' && abdominal !== '' && subescapular !== '' && muslo !== '' && pantorrilla !== '') {
                var pliegues = (parseFloat(tricipital) + parseFloat(iliocrestal) + parseFloat(abdominal) + parseFloat(subescapular) + parseFloat(muslo) + parseFloat(pantorrilla));
            }

            var edad = document.getElementById("hfEdad").value;
            var genero = document.getElementById("hfGenero").value;

            peso = parseFloat(peso);
            talla = parseFloat(talla);

            talla = talla / 100;

            //Femenino
            if (genero == '2') {
                var densidad_corporal = 0.146 * pliegues; //43.8
                var porc_graso = 4.56 + densidad_corporal; //48.36

                var gastoCalor = ((10 * peso) + (6.25 * talla) - 5 * edad) + 5; 

                //console.log(gastoCalor);
            }

            //Masculino
            if (genero == '1') {
                var densidad_corporal = 0.097 * pliegues;
                var porc_graso = 3.64 + densidad_corporal;

                var gastoCalor = ((10 * peso) + (6.25 * talla) - 5 * edad) + 161;
            }
                
            var peso_oseo = peso * 0.15; //11.805
            var decimal = porc_graso / 100;  //0.4836
            var peso_graso = peso * decimal;  //38.05932
            var porc_muscular = peso - peso_oseo - peso_graso; //28.83568
            var peso_magro = peso - peso_graso; //
            var fce = 208.75 - (0.73 * edad);

            document.getElementById("txbPorcGrasa").value = porc_graso.toFixed(2);
            document.getElementById("txbPorcMuscular").value = porc_muscular.toFixed(2);
            document.getElementById("txbFCETanaka").value = fce.toFixed(2);

            document.getElementById("txbPesoGraso").value = peso_graso.toFixed(2);
            document.getElementById("txbPesoMagro").value = peso_magro.toFixed(2);

            document.getElementById("txbGastoCalorico").value = gastoCalor.toFixed(2);

            const dropdown = document.getElementById('ddlActividadFisica');

            dropdown.onchange = function (event) {
                const actividadFisica = parseFloat(event.target.value);
                var gastoTotal = (actividadFisica * gastoCalor);
                document.getElementById("txbGastoTotal").value = gastoTotal.toFixed(2);
            };

            //console.log(genero);
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
                    <h4 class="modal-title">Guía para crear una nueva historia clínica</h4>
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
        <uc1:navbar runat="server" ID="navbar1" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header1" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa fa-notes-medical text-success m-r-sm"></i>Historia clínica - Detalle nutricional</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Asistencial</li>
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

                    <div class="row m-b-xs m-t-xs">
                        <div class="col-md-6">

                            <div class="profile-image">
                                <asp:Literal ID="ltFoto" runat="server"></asp:Literal>
                            </div>
                            <div class="profile-info">
                                <div class="">
                                    <div>
                                        <h2 class="no-margins">
                                            <asp:Literal ID="ltNombre" runat="server"></asp:Literal>
                                            <asp:Literal ID="ltApellido" runat="server"></asp:Literal>
                                        </h2>
                                        <h4>
                                            <asp:Literal ID="ltEmail" runat="server"></asp:Literal></h4>
                                        <small>
                                            <asp:Literal ID="ltDireccion" runat="server"></asp:Literal>,
                        <asp:Literal ID="ltCiudad" runat="server"></asp:Literal></small>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <table class="table small m-b-xs">
                                <tbody>
                                    <tr>
                                        <td><strong><i class="fab fa-whatsapp"></i></strong>
                                            <asp:Literal ID="ltCelular" runat="server"></asp:Literal></td>
                                        <td><strong><i class="fa fa-shield"></i></strong> Estado: 
                        <asp:Literal ID="ltEstado" runat="server"></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td><strong><i class="fa fa-building"></i></strong> Sede:
                        <asp:Literal ID="ltSede" runat="server"></asp:Literal></td>
                                        <td><strong><i class="fa fa-venus-mars"></i></strong> Género:
                                            <asp:Literal ID="ltGenero" runat="server"></asp:Literal>
                                            <input id="hfGenero" type="hidden" runat="server" />
                                            <%--<asp:HiddenField ID="hfGenero" runat="server" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><strong><i class="fa fa-cake"></i></strong>
                                            <asp:Literal ID="ltCumple" runat="server"></asp:Literal>
                                            <input id="hfEdad" type="hidden" runat="server" />
                                            <%--<asp:HiddenField ID="hfEdad" runat="server" />--%>
                                        </td>
                                        <td><strong><i class="fa fa-house-medical"></i></strong> EPS:
                                            <asp:Literal ID="ltEPS" runat="server"></asp:Literal></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-4">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5>Historias clínicas</h5>
                                    <div class="ibox-tools">
                                        <a class="collapse-link">
                                            <i class="fa fa-chevron-up"></i>
                                        </a>
                                        <a class="fullscreen-link">
                                            <i class="fa fa-expand"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="ibox-content">
                                    <div class="panel-body">
                                        <div class="panel-group" id="accordion">
                                            <asp:Repeater ID="rpHistorias" runat="server">
                                                <ItemTemplate>
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h5 class="panel-title">
                                                                <span class="label label-warning-light pull-right"><i class="fa fa-calendar-day"></i> <%# Eval("FechaHora", "{0:dd MMM yyyy}") %></span>
                                                                <a data-toggle="collapse" data-parent="#accordion" href="#collapse<%# Eval("idHistoria") %>">Historia Clínica #<%# Eval("idHistoria") %></a>
                                                            </h5>
                                                        </div>
                                                        <div id="collapse<%# Eval("idHistoria") %>" class="panel-collapse collapse <%# Eval("clase") %>">
                                                            <div class="panel-body">

                                                                <ul class="sortable-list connectList agile-list">

                                                                    <li class="warning-element"><b>Medicina prepagada</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("MedicinaPrepagada") %>
                                                                        </div>
                                                                    </li>

                                                                    <li class="warning-element"><b>Objetivo del ingreso</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("Objetivo") %>
                                                                        </div>
                                                                    </li>

                                                                    <li class="warning-element"><b>Detalle objetivo del ingreso</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("DescripcionObjetivoIngreso") %>
                                                                        </div>
                                                                    </li>

                                                                    <br />
                                                                    <h4><i class="fa fa-clock-rotate-left text-success"></i> ANTECEDENTES</h4>

                                                                    <li class="info-element"><b>Familiares</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteFamiliar") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Patológicos</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AntePatologico") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Quirúrgicos</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteQuirurgico") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Traumatológicos</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteTraumatologico") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Farmacológico</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteFarmacologico") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Actividad física</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteActividadFisica") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Toxicológicos alérgicos</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteFamiliar") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Hospitalarios</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteHospitalario") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>Gineco-obstétricos</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteGineco") %>
                                                                        </div>
                                                                    </li>
                                                                    <li class="info-element"><b>F.U.M.</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("AnteFUM") %>
                                                                        </div>
                                                                    </li>

                                                                    <br />
                                                                    <h4><i class="fa fa-heart-circle-exclamation text-navy"></i> FACTORES DE RIESGO CARDIOVASCULAR</h4>

                                                                    <div class="row">
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Fuma?</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("fuma") %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Cigarrillos x día</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("Cigarrillos") %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Toma?</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("toma") %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Bebidas x mes</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("Bebidas") %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Sedentarismo</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("sedentario") %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Diabetes</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("diabetico") %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                    </div>

                                                                    <div class="row">
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Colesterol</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("colesterado") %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                        <div class="col-lg-6">
                                                                            <li class="success-element"><b>Triglicéridos</b>
                                                                                <div class="agile-detail">
                                                                                    <%# Eval("triglicerado") %>
                                                                                </div>
                                                                            </li>
                                                                        </div>
                                                                    </div>

                                                                    <li class="success-element"><b>H.T.A.</b>
                                                                        <div class="agile-detail">
                                                                            <%# Eval("hipertenso") %>
                                                                        </div>
                                                                    </li>
                                                                    
                                                                </ul>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Literal ID="ltMensaje" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-8">
                            <div class="ibox float-e-margins" runat="server" id="divContenido">
                                <div class="ibox-title">
                                    <h5>Formulario para agregar detalles nutricionales</h5>
                                    <div class="ibox-tools">
                                        <a class="collapse-link">
                                            <i class="fa fa-chevron-up"></i>
                                        </a>
                                        <a class="fullscreen-link">
                                            <i class="fa fa-expand"></i>
                                        </a>
                                    </div>
                                </div>
                                <div class="ibox-content">

                                    <div class="row">
                                        <form role="form" id="form" runat="server">
                                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                            <div class="col-sm-12">
                                                <div class="widget style1 bg-success">
                                                    <div class="row vertical-align">
                                                        <div class="col-xs-3">
                                                            <i class="fa fa-person-arrow-up-from-line fa-2x"></i>
                                                        </div>
                                                        <div class="col-xs-9 text-right">
                                                            <h3 class="font-bold">Antropometría</h3>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div class="row">

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Peso en Kg</label>
                                                            <asp:TextBox ID="txbPeso" CssClass="form-control input-sm" runat="server" onkeyup="calculateIMC(this)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Talla en cms</label>
                                                            <asp:TextBox ID="txbTalla" CssClass="form-control input-sm" runat="server" onkeyup="calculateIMC(this)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Indice de Masa Corporal IMC</label>
                                                            <asp:TextBox ID="txbIMC" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">

                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label>Perímetro de Cintura (cms)</label>
                                                            <asp:TextBox ID="txbPerimCintura" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label>Perímetro de Cadera (cms)</label>
                                                            <asp:TextBox ID="txbPerimCadera" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label>Perímetro de Abdomen (cms)</label>
                                                            <asp:TextBox ID="txbPerimAbdomen" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <div class="form-group">
                                                            <label>Perímetro de Pecho (cms)</label>
                                                            <asp:TextBox ID="txbPerimPecho" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Perímetro de Muslo (cms)</label>
                                                            <asp:TextBox ID="txbPerimMuslo" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Perímetro de Pantorrilla (cms)</label>
                                                            <asp:TextBox ID="txbPerimPantorrilla" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Perímetro de Brazo (cms)</label>
                                                            <asp:TextBox ID="txbPerimBrazo" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Pliegue Tricipital</label>
                                                            <asp:TextBox ID="txbPliegueTricipital" CssClass="form-control input-sm" runat="server" onkeyup="calculatePorcGraso(this)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Pliegue IlioCrestal</label>
                                                            <asp:TextBox ID="txbPliegueIliocrestal" CssClass="form-control input-sm" runat="server" onkeyup="calculatePorcGraso(this)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Pliegue Abdominal</label>
                                                            <asp:TextBox ID="txbPliegueAbdominal" CssClass="form-control input-sm" runat="server" onkeyup="calculatePorcGraso(this)"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Pliegue Subescapular</label>
                                                            <asp:TextBox ID="txbPliegueSubescapular" CssClass="form-control input-sm" runat="server" onkeyup="calculatePorcGraso(this)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Pliegue Muslo</label>
                                                            <asp:TextBox ID="txbPliegueMuslo" CssClass="form-control input-sm" runat="server" onkeyup="calculatePorcGraso(this)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Pliegue Pantorrilla</label>
                                                            <asp:TextBox ID="txbPlieguePantorrilla" CssClass="form-control input-sm" runat="server" onkeyup="calculatePorcGraso(this)"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Porcentaje Graso (%)</label>
                                                            <asp:TextBox ID="txbPorcGrasa" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Porcentaje Muscular (%)</label>
                                                            <asp:TextBox ID="txbPorcMuscular" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>FCE (Tanaka)</label>
                                                            <asp:TextBox ID="txbFCETanaka" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Peso esperado (Kg)</label>
                                                            <asp:TextBox ID="txbPesoEsperado" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Peso graso (Kg)</label>
                                                            <asp:TextBox ID="txbPesoGraso" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Peso magro (Kg)</label>
                                                            <asp:TextBox ID="txbPesoMagro" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Gasto calórico</label>
                                                            <asp:TextBox ID="txbGastoCalorico" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Actividad física</label>
                                                            <asp:DropDownList ID="ddlActividadFisica" runat="server" AppendDataBoundItems="true"
                                                                CssClass="form-control m-b">
                                                                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                                                                <asp:ListItem Text="Sedentario" Value="1.2"></asp:ListItem>
                                                                <asp:ListItem Text="Leve" Value="1.37"></asp:ListItem>
                                                                <asp:ListItem Text="Moderada" Value="1.55"></asp:ListItem>
                                                                <asp:ListItem Text="Activo" Value="1.72"></asp:ListItem>
                                                                <asp:ListItem Text="Muy activo" Value="1.9"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Gasto total</label>
                                                            <asp:TextBox ID="txbGastoTotal" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row">

                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Diagnóstico</label>
                                                            <asp:TextBox ID="txbDiagnostico" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Plan de manejo</label>
                                                            <asp:TextBox ID="txbPlanManejo" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Recomendaciones</label>
                                                            <asp:TextBox ID="txbRecomendaciones" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div class="form-group">
                                                            <label>Observaciones</label>
                                                            <asp:TextBox ID="txbObservaciones" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="col-sm-12">
                                                <div>
                                                    <button class="btn btn-sm btn-danger pull-right m-t-n-xs" type="button">
                                                        <strong>Cancelar</strong></button>
                                                    <asp:Button ID="btnAgregar" runat="server"
                                                        CssClass="btn btn-sm btn-primary m-t-n-xs m-r-md pull-right"
                                                        Text="Guardar y finalizar" Visible="false"
                                                        ValidationGroup="agregar" OnClick="btnAgregar_Click" />
                                                </div>
                                            </div>
                                        </form>
                                    </div>

                                    <%--Fin Contenido!!!!--%>
                                </div>
                            </div>
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
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Clock picker -->
    <script src="js/plugins/clockpicker/clockpicker.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <script>
        $("#form").validate({
            rules: {
                txbPeso: {
                    required: true,
                },
                txbTalla: {
                    required: true,
                },
                txbIMC: {
                    required: true,
                },
                txbPerimCintura: {
                    required: true
                },
                txbPerimCadera: {
                    required: true,
                },
                txbPerimAbdomen: {
                    required: true,
                },
                txbPerimPecho: {
                    required: true,
                },
                txbPerimMuslo: {
                    required: true,
                },
                txbPerimPantorrilla: {
                    required: true,
                },
                txbPerimBrazo: {
                    required: true,
                },
                txbPliegueTricipital: {
                    required: true,
                },
                txbPliegueIliocrestal: {
                    required: true
                },
                txbPliegueAbdominal: {
                    required: true
                },
                txbPliegueSubescapular: {
                    required: true
                },
                txbPliegueMuslo: {
                    required: true
                },
                txbPlieguePantorrilla: {
                    required: true
                },
                txbPorcGrasa: {
                    required: true
                },
                txbPorcMuscular: {
                    required: true
                },
                txbFCETanaka: {
                    required: true
                },
                txbPesoEsperado: {
                    required: true
                },
                txbPesoGraso: {
                    required: true
                },
                txbPesoMagro: {
                    required: true
                },
                txbGastoCalorico: {
                    required: true
                },
                ddlActividadFisica: {
                    required: true
                },
                txbGastoTotal: {
                    required: true
                },
                txbDiagnostico: {
                    required: true
                },
                txbPlanManejo: {
                    required: true
                },
                txbRecomendaciones: {
                    required: true
                },
                txbObservaciones: {
                    required: true
                },
            },
            messages: {
                txbIMC: "*",
                txbPorcGrasa: "*",
                txbPorcMuscular: "*",
                txbFCETanaka: "*",
                txbPesoEsperado: "*",
                txbPesoGraso: "*",
                txbPesoMagro: "*",
                txbGastoCalorico: "*",
                txbGastoTotal: "*",
            }
        });
    </script>

</body>

</html>
