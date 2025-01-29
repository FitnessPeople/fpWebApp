<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pruebapagowompi.aspx.cs" Inherits="fpWebApp.pruebapagowompi" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Fitness People - Registro</title>

    <!-- Bootstrap core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- Animation CSS -->
    <link href="css/animate.css" rel="stylesheet">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.3/themes/smoothness/jquery-ui.css">
    <link href="css/plugins/jasny/jasny-bootstrap.min.css" rel="stylesheet" />
    <link href="css/plugins/codemirror/codemirror.css" rel="stylesheet" />
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet" />
    <link href="css/plugins/steps/jquery.steps.css" rel="stylesheet">
    <link href="css/animate.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="css/style.css" rel="stylesheet">
</head>
<body id="page-top" class="landing-page no-skin-config">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <i class="fa fa-person-chalkboard modal-icon"></i>
                    <h4 class="modal-title">Términos y Condiciones</h4>
                    <%--<small class="font-bold">¡Bienvenido! A continuación, te ofrecemos una guía sencilla para ayudarte a completar el formulario de manera correcta y eficiente. Sigue estos pasos para asegurarte de que toda la información se registre de forma adecuada.</small>--%>
                </div>
                <div class="modal-body">
                    <p>
                        ¡Siguiendo estos pasos, estarás listo para diligenciar tu formulario sin problemas! Si tienes dudas, no dudes en consultar con el administrador del sistema.
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="navbar-wrapper">
        <nav class="navbar navbar-default navbar-fixed-top" role="navigation">
            <div class="container">
                <div class="navbar-header page-scroll">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="index.html">FITNESS PEOPLE</a>
                </div>
                <div id="navbar" class="navbar-collapse collapse">
                    <ul class="nav navbar-nav navbar-right">
                        <li><a class="page-scroll" href="#page-top">Inicio</a></li>
                        <li><a class="page-scroll" href="#features">Características</a></li>
                        <li><a class="page-scroll" href="#team">Equipo</a></li>
                        <li><a class="page-scroll" href="#testimonials">Testimonios</a></li>
                        <li><a class="page-scroll" href="#pricing">Precio</a></li>
                        <li><a class="page-scroll" href="#contact">Contactenos</a></li>
                    </ul>
                </div>
            </div>
        </nav>
    </div>
    <div id="inSlider" class="carousel carousel-fade" data-ride="carousel">
        <ol class="carousel-indicators">
            <li data-target="#inSlider" data-slide-to="0" class="active"></li>
            <li data-target="#inSlider" data-slide-to="1"></li>
        </ol>
        <div class="carousel-inner" role="listbox">
            <div class="item active">
                <div class="container">
                    <div class="carousel-caption">
                        <h1>We craft<br />
                            brands, web apps,<br />
                            and user interfaces<br />
                            we are IN+ studio</h1>
                        <p>Lorem Ipsum is simply dummy text of the printing.</p>
                        <p>
                            <a class="btn btn-lg btn-primary" href="#" role="button">READ MORE</a>
                            <a class="caption-link" href="#" role="button">Inspinia Theme</a>
                        </p>
                    </div>
                    <div class="carousel-image wow zoomIn">
                        <img src="img/landing/laptop.png" alt="laptop" />
                    </div>
                </div>
                <!-- Set background for slide in css -->
                <div class="header-back one"></div>

            </div>
            <div class="item">
                <div class="container">
                    <div class="carousel-caption blank">
                        <h1>We create meaningful
                            <br />
                            interfaces that inspire.</h1>
                        <p>Cras justo odio, dapibus ac facilisis in, egestas eget quam.</p>
                        <p><a class="btn btn-lg btn-primary" href="#" role="button">Learn more</a></p>
                    </div>
                </div>
                <!-- Set background for slide in css -->
                <div class="header-back two"></div>
            </div>
        </div>
        <a class="left carousel-control" href="#inSlider" role="button" data-slide="prev">
            <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
        </a>
        <a class="right carousel-control" href="#inSlider" role="button" data-slide="next">
            <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
        </a>
    </div>

    <section id="features" class="container features">
        <div class="row">
            <div class="col-lg-12 text-center">
                <div class="navy-line"></div>
                <h1>Formulario de Registro</h1>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 text-center wow fadeInLeft">
            </div>
            <div class="col-md-6 text-center  wow zoomIn">

                <div class="ibox" id="divIbox" runat="server">
                    <div class="ibox-content">
                        <form id="form" method="post">
                            <h1>Inicio</h1>
                            <fieldset>
                                <h2>Información inicial</h2>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Tipo de Documento *</label>
                                            <select id="ddlTipoDoc" class="form-control" runat="server">
                                                <option value="1">Cédula de ciudadania</option>
                                                <option value="2">Cédula de Extranjería</option>
                                                <option value="3">Tarjeta de Identidad</option>
                                                <option value="4">Pasaporte</option>
                                                <option value="5">Permiso Especial de Permanencia</option>
                                                <option value="6">Número de Identificación Tibutaria</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Nro. de Documento *</label>
                                            <input id="txbDocumento" name="txbDocumento" type="number" class="form-control required" data-msg="" runat="server">
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Nombre *</label>
                                            <input id="txbNombre" name="txbNombre" type="text" class="form-control required" data-msg="" runat="server">
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Apellido *</label>
                                            <input id="txbApellido" name="txbApellido" type="text" class="form-control required" data-msg="" runat="server">
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Correo *</label>
                                            <input id="txbCorreo" name="txbCorreo" type="text" class="form-control required email" data-msg="" runat="server">
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Celular *</label>
                                            <input id="txbCelular" name="txbCelular" type="number" class="form-control required" data-msg="" runat="server">
                                        </div>
                                    </div>
                                    <div class="col-lg-8">
                                        <div class="form-group">
                                            <label>Dirección *</label>
                                            <input id="txbDireccion" name="txbDireccion" type="text" class="form-control required" data-msg="" runat="server">
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            <label>Ciudad *</label>
                                            <input id="txbCiudad" name="txbCiudad" type="text" class="form-control required" data-msg="" runat="server">
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                            <h1>Plan</h1>
                            <fieldset>
                                <h2>Información del plan</h2>
                                <div class="row">
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Ciudad *</label>
                                            <select id="ddlCiudad" onchange="popSedes()" class="form-control required" data-msg="">
                                                <option value="">Seleccione</option>
                                                <option value="Bucaramanga">Bucaramanga</option>
                                                <option value="Cúcuta">Cúcuta</option>
                                                <option value="Floridablanca">Floridablanca</option>
                                                <option value="Piedecuesta">Piedecuesta</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Sede *</label>
                                            <select id="ddlSede" class="form-control" required data-msg="">
                                                <option value="">Seleccione</option>
                                            </select>
                                        </div>
                                    </div>

                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Plan *</label>
                                            <select id="ddlPlan" onchange="valor()" class="form-control" required>
                                                <option value="510000">4 Meses - $510.000</option>
                                                <option value="890000">10 meses - $890.000</option>
                                                <option value="590000">4 meses premium - $590.000</option>
                                                <option value="999000">10 meses premium - $999.000</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <label>Fecha inicial *</label>
                                            <input id="txbFechaInicio" name="txbFechaInicio" type="date" min="2024-10-11" class="form-control required" data-msg="">
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <%--<label>Valor *</label>--%>
                                            <input id="txbPrecio" name="txbPrecio" type="hidden" value="510000" class="form-control" runat="server">
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                            <%--<h1>Warning</h1>
                            <fieldset>
                                <div class="text-center" style="margin-top: 120px">
                                    <h2>You did it Man :-)</h2>
                                </div>
                            </fieldset>--%>

                            <h1>Finalizar</h1>
                            <fieldset>
                                <h2>Términos y condiciones</h2>
                                <p>Ver <a data-toggle="modal" href="#" data-target="#myModal">términos y condiciones</a>.</p>
                                <input id="acceptTerms" name="acceptTerms" type="checkbox" class="required" data-msg="*">
                                <label for="acceptTerms">Estoy de acuerdo con los Términos y Condiciones</label>
                            </fieldset>
                        </form>
                    </div>
                </div>


                <%--<form role="form" id="form1" runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <div class="col-sm-12">
                        <asp:UpdatePanel ID="upBusqueda" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label>Documento</label>
                                            <asp:TextBox ID="txbBuscar" runat="server" CssClass="form-control input-sm"
                                                placeholder="Documento"
                                                OnTextChanged="txbBuscar_TextChanged"
                                                AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label>Nombre</label>
                                            <asp:TextBox ID="txbNombreAfiliado" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label>Direccion</label>
                                            <asp:TextBox ID="txbDireccion" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label>Correo</label>
                                            <asp:TextBox ID="txbCorreo" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                            <asp:HiddenField ID="hfIdAfiliado" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label>Celular</label>
                                            <asp:TextBox ID="txbCelular" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <asp:Literal ID="ltPublicKey" runat="server" Visible="false"></asp:Literal>
                                            <asp:HiddenField ID="hfPublicKey" runat="server" />
                                            <script src="js/plugins/wompi/wompi.js" 
                                    data-render="button"
                                    data-public-key="pub_test_Mp5JzDLXitLu7W0I3Gea5OXotOExpFjv"
                                    data-currency="COP"
                                    data-amount-in-cents="4950000"
                                    data-reference="sk8-438k4-xmxm392-6655"
                                    data-signature:integrity="59cdd8ebbaaef3ff6859cf4826e6a6b576147205424e56af7e0a08efd151a953"
                                    data-redirect-url="https://fitnesspeoplecolombia.com/planes"
                                    data-customer-data:email="lola@perez.com"
                                    data-customer-data:full-name="Lola Perez"
                                    data-customer-data:phone-number="3019777777"
                                    data-customer-data:phone-number-prefix="+57"
                                    data-customer-data:legal-id="123456789"
                                    data-customer-data:legal-id-type="CC"></script>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txbBuscar" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </form>--%>

                <%-- Boton de Pago Wompi --%>
                <%--<form action="https://checkout.wompi.co/p/" method="GET" runat="server" id="form1" visible="false">
                    <!-- OBLIGATORIOS -->
                    <input type="hidden" name="public-key" value="pub_test_Mp5JzDLXitLu7W0I3Gea5OXotOExpFjv" />
                    <input type="hidden" name="currency" value="COP" />
                    <input type="hidden" name="amount-in-cents" runat="server" id="amount_in_cents" />
                    <input type="hidden" name="reference" runat="server" id="reference" />
                    <input type="hidden" name="signature:integrity" runat="server" id="signature_integrity" />
                    <!-- OPCIONALES -->
                    <input type="hidden" name="redirect-url" value="https://fitnesspeoplecolombia.com/planes" />
                    <input type="hidden" name="customer-data:email" id="correopagador" runat="server" />
                    <input type="hidden" name="customer-data:full-name" id="nombrecompletopagador" runat="server" />
                    <input type="hidden" name="customer-data:phone-number" id="celularpagador" runat="server" />
                    <input type="hidden" name="customer-data:legal-id" id="documentopagador" runat="server" />
                    <input type="hidden" name="customer-data:legal-id-type" value="CC" />
                    <button type="submit" class="btn btn-primary">Pagar a traves de Wompi</button>
                </form>--%>
                <form runat="server" id="form1" visible="false">
                    <asp:Literal ID="ltScript" runat="server"></asp:Literal>
                </form>
            </div>
            <div class="col-md-3 text-center wow fadeInRight">
            </div>
        </div>
    </section>

    <section id="contact" class="gray-section contact">
        <div class="container">
            <div class="row m-b-lg">
                <div class="col-lg-12 text-center">
                    <div class="navy-line"></div>
                    <h1>Contact Us</h1>
                    <p>Donec sed odio dui. Etiam porta sem malesuada magna mollis euismod.</p>
                </div>
            </div>
            <div class="row m-b-lg">
                <div class="col-lg-3 col-lg-offset-3">
                    <address>
                        <strong><span class="navy">Company name, Inc.</span></strong><br />
                        795 Folsom Ave, Suite 600<br />
                        San Francisco, CA 94107<br />
                        <abbr title="Phone">P:</abbr>
                        (123) 456-7890
               
                    </address>
                </div>
                <div class="col-lg-4">
                    <p class="text-color">
                        Consectetur adipisicing elit. Aut eaque, totam corporis laboriosam veritatis quis ad perspiciatis, totam corporis laboriosam veritatis, consectetur adipisicing elit quos non quis ad perspiciatis, totam corporis ea,
               
                    </p>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12 text-center">
                    <a href="mailto:test@email.com" class="btn btn-primary">Send us mail</a>
                    <p class="m-t-sm">
                        Or follow us on social platform
               
                    </p>
                    <ul class="list-inline social-icon">
                        <li><a href="#"><i class="fa fa-twitter"></i></a>
                        </li>
                        <li><a href="#"><i class="fa fa-facebook"></i></a>
                        </li>
                        <li><a href="#"><i class="fa fa-linkedin"></i></a>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2 text-center m-t-lg m-b-lg">
                    <p>
                        <strong>&copy; 2015 Company Name</strong><br />
                        consectetur adipisicing elit. Aut eaque, laboriosam veritatis, quos non quis ad perspiciatis, totam corporis ea, alias ut unde.
                    </p>
                </div>
            </div>
        </div>
    </section>

    <!-- Mainly scripts -->
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>
    <script src="js/plugins/wow/wow.min.js"></script>

    <!-- Jasny -->
    <script src="js/plugins/jasny/jasny-bootstrap.min.js"></script>

    <!-- Steps -->
    <script src="js/plugins/steps/jquery.steps.min.js"></script>

    <!-- Jquery Validate -->
    <script src="js/plugins/validate/jquery.validate.min.js"></script>

    <script>
        $(document).ready(function () {
            $("#wizard").steps();
            $("#form").steps({
                bodyTag: "fieldset",
                onStepChanging: function (event, currentIndex, newIndex) {
                    // Always allow going backward even if the current step contains invalid fields!
                    if (currentIndex > newIndex) {
                        return true;
                    }

                    var form = $(this);

                    // Clean up if user went backward before
                    if (currentIndex < newIndex) {
                        // To remove error styles
                        $(".body:eq(" + newIndex + ") label.error", form).remove();
                        $(".body:eq(" + newIndex + ") .error", form).removeClass("error");
                    }

                    // Disable validation on fields that are disabled or hidden.
                    form.validate().settings.ignore = ":hidden";

                    // Start validation; Prevent going forward if false
                    return form.valid();
                },
                onStepChanged: function (event, currentIndex, priorIndex) {
                    // Suppress (skip) "Warning" step if the user is old enough and wants to the previous step.
                    if (currentIndex === 2 && priorIndex === 3) {
                        $(this).steps("previous");
                    }
                },
                onFinishing: function (event, currentIndex) {
                    var form = $(this);

                    // Disable validation on fields that are disabled.
                    // At this point it's recommended to do an overall check (mean ignoring only disabled fields)
                    form.validate().settings.ignore = ":disabled";

                    // Start validation; Prevent form submission if false
                    return form.valid();
                },
                onFinished: function (event, currentIndex) {
                    var form = $(this);

                    // Submit form input
                    form.submit();
                }
            }).validate({
                errorPlacement: function (error, element) {
                    element.before(error);
                },
                rules: {
                    txbCelular: {
                        minlength: 10,
                    }
                }
            });
        });
    </script>

    <script>
        function popSedes() {
            const ddlCiudad = document.getElementById("ddlCiudad");
            const ddlSede = document.getElementById("ddlSede");

            ddlSede.innerHTML = "";
            const selectedCiudad = ddlCiudad.value;

            const sedes = {
                "Bucaramanga": ["Boulevard", "Cabecera", "El Prado", "Provenza", "Ciudadela"],
                "Cúcuta": ["Ceiba II", "Jardin Plaza"],
                "Floridablanca": ["Cañaveral"],
                "Piedecuesta": ["DeLaCuesta", "Parque Central"],
            };

            const opcionesSede = sedes[selectedCiudad];
            for (var i = 0; i < opcionesSede.length; i++) {
                const opcion = document.createElement("option");
                opcion.text = opcionesSede[i];
                ddlSede.add(opcion);
            }
        }

        function valor() {
            const ddlPlan = document.getElementById("ddlPlan");
            const selectedPlan = ddlPlan.value;

            const txbPrecio = document.getElementById("txbPrecio");
            txbPrecio.value = selectedPlan;

            console.log(selectedPlan);
        }

    </script>

    <%--<script type="text/javascript">  
        $(document).ready(function () {
            $("#txbBuscar").autocomplete({
                source: function (request, response) {
                    $.getJSON("/obtenerafiliados?search=" + request.term, function (data) {
                        response($.map(data, function (item) {
                            return {
                                label: item.nombre + " " + item.apellido,
                                value: item.id,
                                correo: item.correo,
                                direccion: item.direccion,
                                celular: item.celular,
                            };
                        }));
                    });
                },
                select: function (event, ui) {
                    //console.log(ui);
                    //var id = ui.item.id;
                    //var nombre = ui.item.label;
                    //store in session
                    document.getElementById("txbNombre").value = ui.item.label;
                    document.getElementById("txbCorreo").value = ui.item.correo;
                },
                minLength: 3,
                delay: 100
            });
        });
    </script>--%>

    <script>

        $(document).ready(function () {

            $('body').scrollspy({
                target: '.navbar-fixed-top',
                offset: 80
            });

            // Page scrolling feature
            $('a.page-scroll').bind('click', function (event) {
                var link = $(this);
                $('html, body').stop().animate({
                    scrollTop: $(link.attr('href')).offset().top - 50
                }, 500);
                event.preventDefault();
                $("#navbar").collapse('hide');
            });
        });

        var cbpAnimatedHeader = (function () {
            var docElem = document.documentElement,
                header = document.querySelector('.navbar-default'),
                didScroll = false,
                changeHeaderOn = 200;
            function init() {
                window.addEventListener('scroll', function (event) {
                    if (!didScroll) {
                        didScroll = true;
                        setTimeout(scrollPage, 250);
                    }
                }, false);
            }
            function scrollPage() {
                var sy = scrollY();
                if (sy >= changeHeaderOn) {
                    $(header).addClass('navbar-scroll')
                }
                else {
                    $(header).removeClass('navbar-scroll')
                }
                didScroll = false;
            }
            function scrollY() {
                return window.pageYOffset || docElem.scrollTop;
            }
            init();

        })();

        // Activate WOW.js plugin for animation on scrol
        new WOW().init();

</script>

</body>
</html>
