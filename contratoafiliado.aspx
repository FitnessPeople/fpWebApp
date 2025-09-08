<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contratoafiliado.aspx.cs" Inherits="fpWebApp.contratoafiliado" ValidateRequest="false" %>

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

    <title>Fitness People | Contrato</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <%--<link href="font-awesome/css/font-awesome.css" rel="stylesheet">--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <!-- CSS de Quill -->
    <link href="https://cdn.jsdelivr.net/npm/quill@2.0.3/dist/quill.snow.css" rel="stylesheet">
    <!-- JS de Quill -->
    <script src="https://cdn.jsdelivr.net/npm/quill@2.0.3/dist/quill.js"></script>

    <script>
        var quill;

        document.addEventListener("DOMContentLoaded", function () {
            quill = new Quill("#editor1", {
                theme: "snow",
                modules: {
                    toolbar: [
                        [{ 'header': [1, 2, 3, false] }],
                        ['bold'], // Negrita y Tachado
                        ['italic', 'underline'],
                        [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                        [{ 'align': [] }],
                    ]
                }
            });

            quill.on('text-change', function (delta, oldDelta, source) {
                var editorContenido = document.getElementById("editor1");
                if (source === 'user') {
                    const scrollTop = editorContenido.scrollTop;
                    requestAnimationFrame(() => {
                        editorContenido.scrollTop = scrollTop;
                    });
                }
            });

            var contenidoGuardado = document.getElementById('<%= hiddenEditor1.ClientID %>').value;
            if (contenidoGuardado.trim() !== "") {
                quill.root.innerHTML = contenidoGuardado;
            }
        });

        function guardarContenidoEditor1() {
            var contenido = quill.root.innerHTML;
            document.getElementById('<%= hiddenEditor1.ClientID %>').value = contenido;
        }

        document.addEventListener("DOMContentLoaded", function () {
            quill = new Quill("#editor2", {
                theme: "snow",
                modules: {
                    toolbar: [
                        [{ 'header': [1, 2, 3, false] }],
                        ['bold'], // Negrita y Tachado
                        ['italic', 'underline'],
                        [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                        [{ 'align': [] }],
                    ]
                }
            });

            quill.on('text-change', function (delta, oldDelta, source) {
                var editorContenido = document.getElementById("editor2");
                if (source === 'user') {
                    const scrollTop = editorContenido.scrollTop;
                    requestAnimationFrame(() => {
                        editorContenido.scrollTop = scrollTop;
                    });
                }
            });

            var contenidoGuardado = document.getElementById('<%= hiddenEditor2.ClientID %>').value;
            if (contenidoGuardado.trim() !== "") {
                quill.root.innerHTML = contenidoGuardado;
            }
        });

        function guardarContenidoEditor2() {
            var contenido = quill.root.innerHTML;
            document.getElementById('<%= hiddenEditor2.ClientID %>').value = contenido;
        }

    </script>

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />

    <script>
        function changeClass() {
            // Activa el menú principal
            var element1 = document.querySelector("#contratoafiliado");
            if (element1) {
                element1.classList.add("active");
            }

            // Despliega el submenú
            var element2 = document.querySelector("#afiliados2");
            if (element2) {
                element2.classList.add("show"); // en Bootstrap el desplegado es con "show"
                element2.classList.remove("collapse");
            }
        }
    </script>
</head>

<body onload="changeClass()">
    <div class="modal inmodal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content animated bounceInRight">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Cerrar</span></button>
                    <i class="fa fa-file-contract modal-icon" style="color: #1C84C6;"></i>
                    <h4 class="modal-title">Guía para generar contrato de afiliado</h4>
                    <small class="font-bold">¡Bienvenido! Te explicamos cómo redactar y guardar un contrato para un afiliado de manera fácil y segura.</small>
                </div>
                <div class="modal-body">
                    <p>
                        <b>Paso 1: Modifica la estructura inicial del contrato</b><br />
                        <b>Escribe en el cuadro de texto</b> toda la información necesaria:<br />
                        <i class="fa-solid fa-user" style="color: #0D6EFD;"></i> Datos del afiliado (nombre, documento, contacto).<br />
                        <i class="fa-solid fa-receipt" style="color: #0D6EFD;"></i> Términos y condiciones del servicio.<br />
                        <i class="fa-solid fa-calendar-days" style="color: #0D6EFD;"></i> Fechas de vigencia y obligaciones.
                    <br />
                        <br />
                        <b>Paso 2: Revisa la previsualización</b><br />
                        En la <b>parte inferior</b>, verás cómo quedará el contrato final.<br />
                        Asegúrate de que:<br />
                        <i class="fa-solid fa-check"></i> Los datos estén completos y correctos.<br />
                        <i class="fa-solid fa-check"></i> El formato sea claro y profesional.
                    <br />
                        <br />
                        <b>Paso 3: Confirma o cancela</b><br />
                        <i class="fa-solid fa-square-check fa-lg" style="color: #18A689;"></i> <b>Agregar:</b> Guarda el contrato en el sistema.<br />
                        <i class="fa-solid fa-square-minus fa-lg" style="color: #EC4758;"></i> <b>Cancelar:</b> Si necesitas volver atrás sin guardar modificaciones.
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

        <uc1:navbar runat="server" ID="navbar" />

        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom">
                <uc1:header runat="server" ID="header" />
            </div>
            <div class="row wrapper border-bottom white-bg page-heading">

                <%--Inicio Breadcrumb!!!--%>
                <div class="col-sm-10">
                    <h2><i class="fa fa-file-contract text-success m-r-sm"></i>Contrato afiliado</h2>
                    <ol class="breadcrumb">
                        <li><a href="inicio">Inicio</a></li>
                        <li>Afiliados</li>
                        <li class="active"><strong>Contrato afiliado</strong></li>
                    </ol>
                </div>
                <div class="col-sm-2">
                </div>
                <%--Fin Breadcrumb!!!--%>
            </div>
            <div class="wrapper wrapper-content animated fadeInRight article">
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

                    <form role="form" id="form1" runat="server">
                        <div class="row">
                            <div class="col-lg-10 col-lg-offset-1">
                                <div class="tabs-container">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a data-toggle="tab" href="#tab-1"> Contrato Mayor de Edad F.P.</a></li>
                                        <li class=""><a data-toggle="tab" href="#tab-2"> Contrato Mayor de Edad I.F.</a></li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="tab-1" class="tab-pane active">
                                            <div class="panel-body">
                                                <div>
                                                    <p>
                                                        <button id="btnNombreAfiliado" data-button="NOMBRE" onclick="putText('btnNombreAfiliado')" class="btn btn-sm btn-info">Nombre</button>
                                                        <button id="btnDocumentoAfiliado" data-button="DOCUMENTO" onclick="putText('btnDocumentoAfiliado')" class="btn btn-sm btn-info">Documento</button>
                                                        <button id="btnDireccionAfiliado" data-button="DIRECCION" onclick="putText('btnDireccionAfiliado')" class="btn btn-sm btn-info">Dirección</button>
                                                        <button id="btnCelularAfiliado" data-button="CELULAR" onclick="putText('btnCelularAfiliado')" class="btn btn-sm btn-info">Celular</button>
                                                        <button id="btnFechaNacAfiliado" data-button="FECHANAC" onclick="putText('btnFechaNacAfiliado')" class="btn btn-sm btn-info">Fecha nacimiento</button>
                                                        <button id="btnEmailAfiliado" data-button="EMAIL" onclick="putText('btnEmailAfiliado')" class="btn btn-sm btn-info">Email</button>
                                                        <button id="btnFechaInicioPlan" data-button="FECHAINICIOPLAN" onclick="putText('btnFechaInicioPlan')" class="btn btn-sm btn-info">Fecha Inicio Plan</button>
                                                        <button id="btnEPS" data-button="EPS" onclick="putText('btnEPS')" class="btn btn-sm btn-info">EPS</button>
                                                        <button id="btnResponsable" data-button="RESPONSABLE" onclick="putText('btnResponsable')" class="btn btn-sm btn-info">Responsable</button>
                                                        <button id="btnParentesco" data-button="PARENTESCO" onclick="putText('btnParentesco')" class="btn btn-sm btn-info">Parentesco</button>
                                                        <button id="btnCelularResponsable" data-button="CELULARRESPONSABLE" onclick="putText('btnCelularResponsable')" class="btn btn-sm btn-info">Celular Responsable</button>
                                                    </p>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <%--<form role="form" id="form1" runat="server">--%>
                                                                <div class="form-group">
                                                                    <div id="editor1" cssclass="form-control input-sm"></div>
                                                                    <asp:HiddenField ID="hiddenEditor1" runat="server" />
                                                                </div>
                                                                <div class="form-group">
                                                                    <a href="contratoafiliado" class="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md">Cancelar</a>
                                                                    <asp:Button ID="btnAgregar1" runat="server" Text="Guardar" Visible="false" 
                                                                        CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" ValidationGroup="agregar"
                                                                        OnClick="btnAgregar1_Click" OnClientClick="guardarContenidoEditor1()" />
                                                                </div>
                                                            <%--</form>--%>
                                                        </div>
                                                    </div>
                                                </div>

                                                <h5>Previsualización</h5>

                                                <p><asp:Literal ID="ltContrato1" runat="server"></asp:Literal></p>

                                            </div>
                                        </div>
                                        <div id="tab-2" class="tab-pane">
                                            <div class="panel-body">
                                            
                                                <div>
                                                    <p>
                                                        <button id="btnNombreAfiliado2" data-button="NOMBRE" onclick="putText('btnNombreAfiliado2')" class="btn btn-sm btn-info">Nombre</button>
                                                        <button id="btnDocumentoAfiliado2" data-button="DOCUMENTO" onclick="putText('btnDocumentoAfiliado2')" class="btn btn-sm btn-info">Documento</button>
                                                        <button id="btnDireccionAfiliado2" data-button="DIRECCION" onclick="putText('btnDireccionAfiliado2')" class="btn btn-sm btn-info">Dirección</button>
                                                        <button id="btnCelularAfiliado2" data-button="CELULAR" onclick="putText('btnCelularAfiliado2')" class="btn btn-sm btn-info">Celular</button>
                                                        <button id="btnFechaNacAfiliado2" data-button="FECHANAC" onclick="putText('btnFechaNacAfiliado2')" class="btn btn-sm btn-info">Fecha nacimiento</button>
                                                        <button id="btnEmailAfiliado2" data-button="EMAIL" onclick="putText('btnEmailAfiliado2')" class="btn btn-sm btn-info">Email</button>
                                                        <button id="btnFechaInicioPlan2" data-button="FECHAINICIOPLAN" onclick="putText('btnFechaInicioPlan2')" class="btn btn-sm btn-info">Fecha Inicio Plan</button>
                                                        <button id="btnEPS2" data-button="EPS" onclick="putText('btnEPS2')" class="btn btn-sm btn-info">EPS</button>
                                                        <button id="btnResponsable2" data-button="RESPONSABLE" onclick="putText('btnResponsable2')" class="btn btn-sm btn-info">Responsable</button>
                                                        <button id="btnParentesco2" data-button="PARENTESCO" onclick="putText('btnParentesco2')" class="btn btn-sm btn-info">Parentesco</button>
                                                        <button id="btnCelularResponsable2" data-button="CELULARRESPONSABLE" onclick="putText('btnCelularResponsable2')" class="btn btn-sm btn-info">Celular Responsable</button>
                                                    </p>
                                                    <div class="row">
                                                        <div class="col-lg-12">
                                                            <%--<form role="form" id="form2" runat="server">--%>
                                                                <div class="form-group">
                                                                    <div id="editor2" cssclass="form-control input-sm"></div>
                                                                    <asp:HiddenField ID="hiddenEditor2" runat="server" />
                                                                </div>
                                                                <div class="form-group">
                                                                    <a href="contratoafiliado" class="btn btn-sm btn-danger pull-right m-t-n-xs m-l-md">Cancelar</a>
                                                                    <asp:Button ID="btnAgregar2" runat="server" Text="Guardar" Visible="false" 
                                                                        CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" ValidationGroup="agregar"
                                                                        OnClick="btnAgregar2_Click" OnClientClick="guardarContenidoEditor2()" />
                                                                </div>
                                                            <%--</form>--%>
                                                        </div>
                                                    </div>
                                                </div>

                                                <h5>Previsualización</h5>

                                                <p><asp:Literal ID="ltContrato2" runat="server"></asp:Literal></p>

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

        function putText(idBtn) // javascript
        {
            const $btn = document.getElementById(idBtn);

            var contenteditable = document.querySelector("[contenteditable]");
            var text = contenteditable.innerHTML;

            var selected = window.getSelection();
            var textoselected = selected.anchorNode.data;
            var textoantes = textoselected.substring(0, selected.anchorOffset);
            var textodespues = textoselected.substring(selected.anchorOffset, textoselected.length);

            var textoboton = "<b>#" + $btn.dataset.button + "#</b>";

            var nuevotexto = textoantes + " " + textoboton + " " + textodespues;
            contenteditable.innerHTML = text.replace(textoselected, nuevotexto);
        }

    </script>

    <style>
        #editor{
            height: 500px;
              max-height: 500px;
              overflow: auto;
        }
    </style>

</body>

</html>
