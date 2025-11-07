<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="header.ascx.cs" Inherits="fpWebApp.controles.header" %>
<nav class="navbar navbar-static-top white-bg" role="navigation" style="margin-bottom: 0">
    <div class="navbar-header">
        <a class="navbar-minimalize minimalize-styl-2 btn btn-default " href="#"><i class="fa fa-bars"></i></a>
        <%--<form role="search" class="navbar-form-custom" action="search_results.html">
            <div class="form-group">
                <input type="text" placeholder="Buscar" class="form-control" name="top-search" id="top-search">
            </div>
        </form>--%>
    </div>
    <ul class="nav navbar-top-links navbar-right">
        <li>
            <span class="m-r-sm text-muted welcome-message font-bold" id="demo"></span>
        </li>
        <%--<li class="dropdown">
            <a class="dropdown-toggle count-info" data-toggle="dropdown" href="#" runat="server" id="divCambioPerfil">
                <i class="fa fa-people-arrows"></i>
            </a>
            <ul class="dropdown-menu dropdown-messages">
                <li>
                    <div class="dropdown-messages-box">
                        <a href="inicio?idPerfil=1&idUsuario=147">
                            <div>
                                Cambiar a perfil <strong>Gerencial</strong>.
                            </div>
                        </a>
                    </div>
                </li>
                <li class="divider"></li>
                <li>
                    <div class="dropdown-messages-box">
                        <a href="inicio?idPerfil=8&idUsuario=37">
                            <div>
                                Cambiar a perfil <strong>Fisioterapeuta</strong>.
                            </div>
                        </a>
                    </div>
                </li>
                <li class="divider"></li>
                <li>
                    <div class="dropdown-messages-box">
                        <a href="#">
                            <div>
                                Cambiar a perfil <strong>Deportologo</strong>.
                            </div>
                        </a>
                    </div>
                </li>
                <li class="divider"></li>
                <li>
                    <div class="dropdown-messages-box">
                        <a href="#">
                            <div>
                                Cambiar a perfil <strong>Nutricionista</strong>.
                            </div>
                        </a>
                    </div>
                </li>
                <li>
                    <div class="dropdown-messages-box">
                        <a href="inicio?idPerfil=4&idUsuario=14">
                            <div>
                                Cambiar a perfil <strong>Asesor comercial</strong>.
                            </div>
                        </a>
                    </div>
                </li>
                <li>
                    <div class="dropdown-messages-box">
                        <a href="inicio?idPerfil=2&idUsuario=150">
                            <div>
                                Cambiar a perfil <strong>Administrador Sede</strong>.
                            </div>
                        </a>
                    </div>
                </li>
            </ul>
        </li>--%>
        <li class="dropdown">
            <a class="dropdown-toggle count-info" data-toggle="dropdown" href="#">
                <i class="fa fa-envelope"></i><span class="label label-warning1">0</span>
            </a>
            <ul class="dropdown-menu dropdown-messages">
                <li>
                    <div class="dropdown-messages-box">
                        <a href="#" class="pull-left">
                            <img alt="image" class="img-circle" src="img/a7.jpg">
                        </a>
                        <div>
                            <small class="pull-right text-warning">Hace 40 min</small>
                            Tienes un nuevo mensaje de <strong>Silvia Pardo</strong>.
                            <br>
                            <small class="text-muted">Asunto: No enviaron comprobantes</small>
                        </div>
                    </div>
                </li>
                <li class="divider"></li>
                <li>
                    <div class="dropdown-messages-box">
                        <a href="#" class="pull-left">
                            <img alt="image" class="img-circle" src="img/a4.jpg">
                        </a>
                        <div>
                            <small class="pull-right text-navy">Hace 5 horas</small>
                            Tienes un nuevo mensaje de <strong>Mónica Suarez</strong>.
                            <br>
                            <small class="text-muted">Asunto: Agregar los elementos a la tienda</small>
                        </div>
                    </div>
                </li>
                <li class="divider"></li>
                <li>
                    <div class="dropdown-messages-box">
                        <a href="#" class="pull-left">
                            <img alt="image" class="img-circle" src="img/profile.jpg">
                        </a>
                        <div>
                            <small class="pull-right">Hace 2 días</small>
                            Tienes un nuevo mensaje de <strong>Yerson Suarez</strong>.
                            <br>
                            <small class="text-muted">Asunto: Enviar informes de ventas</small>
                        </div>
                    </div>
                </li>
                <li class="divider"></li>
                <li>
                    <div class="text-center link-block">
                        <%--<a href="correointerno">--%>
                        <a href="#">
                            <i class="fa fa-envelope m-r-sm"></i><strong>Leer todos los mensajes</strong>
                        </a>
                    </div>
                </li>
            </ul>
        </li>
        <li class="dropdown">
            <a class="dropdown-toggle count-info" data-toggle="dropdown" href="#">
                <i class="fa fa-bell"></i><span class="label label-warning1">0</span>
            </a>
            <ul class="dropdown-menu dropdown-alerts">
                <li>
                    <a href="#">
                        <div>
                            <i class="fa fa-envelope fa-fw m-r-sm"></i>Tienes 16 nuevos mensajes
                    <span class="pull-right text-muted small">2 minutos</span>
                        </div>
                    </a>
                </li>
                <li class="divider"></li>
                <li>
                    <a href="#">
                        <div>
                            <i class="fa fa-users fa-fw m-r-sm"></i>3 nuevos amigos
                    <span class="pull-right text-muted small">15 minutos</span>
                        </div>
                    </a>
                </li>
                <li class="divider"></li>
                <li>
                    <a href="#">
                        <div>
                            <i class="fa fa-upload fa-fw m-r-sm"></i>Reinicio del sistema
                    <span class="pull-right text-muted small">10 horas</span>
                        </div>
                    </a>
                </li>
                <li class="divider"></li>
                <li>
                    <div class="text-center link-block">
                        <a href="#">
                            <strong>Ver todas las  alertas</strong>
                            <i class="fa fa-angle-right"></i>
                        </a>
                    </div>
                </li>
            </ul>
        </li>

        <li class="dropdown">
            <a class="dropdown-toggle count-info" data-toggle="dropdown" href="#">
                <i class="fa fa-user-tie"></i><span class="label label-warning1">
                    <asp:Literal ID="ltNroUsuarios" runat="server"></asp:Literal></span>
            </a>
        </li>

        <li class="dropdown">
            <a class="dropdown-toggle count-info" data-toggle="modal" href="#" data-target="#myModal">
                <i class="fa fa-circle-question text-success"></i>
            </a>
        </li>

        <li class="pull-right m-r-md">
            <a class="right-sidebar-toggle">
                <i class="fa fa-tasks"></i>
            </a>
        </li>
        <li class="pull-right">
            <a href="logout">
                <i class="fa fa-sign-out"></i>Salir
            </a>
        </li>
    </ul>

</nav>

<script type="text/javascript">
    var i = 0;
    var txt = 'Bienvenid@ a FP+'; /* The text */
    var speed = 50; /* The speed/duration of the effect in milliseconds */
    function typeWriter() {
        if (i < txt.length) {
            document.getElementById("demo").innerHTML += txt.charAt(i);
            i++;
            setTimeout(typeWriter, speed);
        }
    }
    typeWriter();

    const url = window.location.pathname.replace("/","");
    //console.log(url);

    let teclasPresionadas = {}; // Objeto para guardar las teclas presionadas

    // Evento keydown
    document.addEventListener('keydown', (event) => {
        teclasPresionadas[event.key] = true; // Guardar que la tecla está presionada
        if (teclasPresionadas['Control'] && event.key === 'l') { // Detectar Ctrl+L
            console.log('Ctrl+l detectado');
            window.location.href = "pantallabloqueo?page=" + url;
        }
    });

    // Evento keyup
    document.addEventListener('keyup', (event) => {
        delete teclasPresionadas[event.key]; // Eliminar la tecla del registro
    });
</script>