<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="navbar.ascx.cs" Inherits="fpWebApp.controles.navbar" %>
<nav class="navbar-default navbar-static-side" role="navigation">
    <div class="sidebar-collapse">
        <ul class="nav metismenu" id="side-menu">
            <li class="nav-header">
                <div class="dropdown profile-element">
                    <span>
                        <asp:Literal ID="ltFoto" runat="server"></asp:Literal>
                    </span>
                    <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                        <span class="clear"><span class="block m-t-xs"><strong class="font-bold">
                            <asp:Literal ID="ltNombreUsuario" runat="server"></asp:Literal></strong>
                        </span><span class="text-muted text-xs block">
                            <asp:Literal ID="ltCargo" runat="server"></asp:Literal><b class="caret"></b></span></span></a>
                    <ul class="dropdown-menu animated fadeInRight m-t-xs">
                        <li><a href="micuenta">Mi cuenta</a></li>
                        <li><a href="contactos">Contactos</a></li>
                        <li><a href="mensajes">Mensajes</a></li>
                        <li class="divider"></li>
                        <li><a href="logout">Salir</a></li>
                    </ul>
                </div>
                <div class="logo-element">
                    FP+
                </div>
            </li>
            <%--<li>
                <a href="#"><i class="fa fa-house"></i><span class="nav-label">Dashboards</span></a>
                <ul class="nav nav-second-level">
                    <li class="active"><a href="inicio">Gerencia</a></li>
                    <li><a href="dashboard_5.html">Administrativo</a></li>
                    <li><a href="dashboard_4_1.html">Comercial</a></li>
                    <li><a href="dashboard_3.html">Líder deportivo</a></li>
                    <li><a href="dashboard_2.html">Marketing digital </a></li>
                    <li><a href="dashboard_4.html">Fisioterapia y nutrición</a></li>
                    <li><a href="dashboard_5.html">Profesor</a></li>
                </ul>
            </li>--%>
            <li id="inicio" class="old">
                <a href="inicio"><i class="fa fa-house"></i><span class="nav-label">Inicio</span></a>
            </li>
            <li>
                <a href="#"><i class="fa fa-id-card"></i><span class="nav-label">Afiliados</span><span class="fa arrow"></span></a>
                <ul id="afiliados2" class="nav nav-second-level collapse">
                    <li id="afiliados1" class="old">
                        <a href="afiliados"><i class="fa fa-id-card"></i>Afiliados<span class="label label-warning1 pull-right"><asp:Literal ID="ltTotalAfiliados" runat="server"></asp:Literal></span></a>
                    </li>
                    <li id="nuevoafiliado" class="old"><a href="nuevoafiliado"><i class="fa fa-id-card"></i>Crear afiliado</a></li>
                    <li id="empresasafiliadas" class="old"><a href="empresasafiliadas"><i class="fa fa-building"></i>Empresas convenios</a></li>
                    <li id="nuevaempresa" class="old"><a href="nuevaempresaafiliada"><i class="fa fa-building"></i>Crear empresa</a></li>
                    <li id="traspasos" class="old"><a href="traspasos"><i class="fa fa-right-left"></i>Traspasos</a></li>
                    <li id="cortesias" class="old"><a href="cortesias"><i class="fa fa-gift"></i>Cortesías</a></li>
                    <li id="incapacidades" class="old"><a href="incapacidades"><i class="fa fa-head-side-mask"></i>Incapacidades</a></li>
                    <li id="congelaciones" class="old"><a href="congelaciones"><i class="fa fa-snowflake"></i>Congelaciones</a></li>
                    <li id="autorizaciones" class="old"><a href="autorizaciones"><i class="fa fa-unlock"></i>Autorizaciones</a></li>
                    <li id="contratoafiliado" class="old"><a href="contratoafiliado"><i class="fa fa-file-contract"></i>Contrato afiliado</a></li>
                </ul>
            </li>
            <li>
                <a href="#"><i class="fa fa-user-doctor"></i><span class="nav-label">Asistencial</span><span class="fa arrow"></span></a>
                <ul id="medico" class="nav nav-second-level collapse">
                    <li id="agenda" class="old"><a href="agenda"><i class="fa fa-calendar-days"></i>Administrar agenda</a></li>
                    <li id="agendarcita" class="old"><a href="agendarcita"><i class="fa fa-calendar-days"></i>Agendar cita</a></li>
                    <li id="agendaespecialista" class="old"><a href="agendaespecialista"><i class="fa fa-calendar-days"></i>Agenda especialista</a></li>
                    <li id="historias" class="old"><a href="historiasclinicas"><i class="fa fa-notes-medical"></i>Historias clínicas</a></li>
                    <li id="nuevahistoria" class="old"><a href="nuevahistoriaclinica"><i class="fa fa-notes-medical"></i>Crear historia clínica</a></li>
                    <li id="especialistas" class="old"><a href="especialistas"><i class="fa fa-user-doctor"></i>Especialistas</a></li>
                    <li id="nuevoespecialista" class="old"><a href="nuevoespecialista"><i class="fa fa-user-doctor"></i>Nuevo especialista</a></li>
                </ul>
            </li>
            <li>
                <a href="#"><i class="fa fa-file-lines"></i><span class="nav-label">Reportes</span><span class="fa arrow"></span></a>
                <ul id="reportes" class="nav nav-second-level collapse">
                    <li id="reportepagos" class="old"><a href="reportepagos" ><i class="fas fa-hand-holding-usd"></i>Pagos</a></li>
                    <li id="reportepagosmulticanal" class="old"><a href="reportepagosmulticanal" ><i class="fa-solid fa-coins"></i>Pagos multicanal</a></li>
                    <li><a href="#">Dashboard</a></li>
                    <li><a href="#">Informe de ventas</a></li>
                    <li><a href="#">Ventas por producto</a></li>
                    <li><a href="#">Ventas por agente</a></li>
                    <li><a href="#">Ventas por entrenador</a></li>
                    <li><a href="#">Vencimientos</a></li>
                    <li><a href="#">Consulta de facturas</a></li>
                    <li><a href="#">Auditoria ingreso sedes</a></li>
                    <li><a href="#">Demografico afiliados</a></li>
                    <li><a href="#">Free Pass</a></li>                    
                </ul>
            </li>
            <li>
                <a href="#"><i class="fa fa-users-rectangle"></i><span class="nav-label">CRM</span><span class="fa arrow"></span></a>
                <ul id="crm" class="nav nav-second-level collapse">
                    <li id="crmnuevocontacto" class="old"><a href="crmnuevocontacto"><i class="fas fa-user-plus"></i>Nuevo</a></li>
<%--                    <li id="nuevocontactocrm" class="old"><a href="nuevocontactocrm"><i class="fas fa-user-plus"></i>Nuevo Contacto</a></li>--%>
                    <li id="listacontactoscrm" class="old"><a href="listacontactoscrm"><i class="fa fa-desktop"></i>Contactos / Empresas</a></li>
                    <li id="agendacrm" class="old"><a href="agendacrm"><i class="fa fa-book"></i>Agenda</a></li>
                    <li><a href="#">Efectividad en actividades</a></li>
                    <li><a href="#">Efectividad en gestion</a></li>
                    <li><a href="#">Traspasos de agenda</a></li>
                    <li><a href="#">Seguimiento asesores</a></li>
                </ul>
            </li>
            <li>
                <a href="#"><i class="fa fa-calculator"></i><span class="nav-label">Contabilidad</span><span class="fa arrow"></span></a>
                <ul class="nav nav-second-level collapse">
                    <li><a href="#">Reporte de caja</a></li>
                    <li><a href="#">Cartera</a></li>
                </ul>
            </li>
            <li>
                <a href="#"><i class="fa fa-desktop"></i><span class="nav-label">Sistema</span><span class="fa arrow"></span></a>
                <ul id="sistema" class="nav nav-second-level collapse">
                    <li id="empleados" class="old"><a href="empleados"><i class="fa fa-user-tie"></i>Empleados<span class="label label-warning1 pull-right"><asp:Literal ID="ltTotalEmpleados" runat="server"></asp:Literal></span></a></li>
                    <li id="nuevoempleado" class="old"><a href="nuevoempleado"><i class="fas fa-user-plus"></i>Nuevo empleado</a></li>
                    <li id="cargos" class="old"><a href="cargos"><i class="fa-solid fas fa-address-card"></i>Cargos empleado</a></li>
                    <li id="usuarios" class="old"><a href="usuarios"><i class="fas fa-users"></i>Usuarios<span class="label label-warning1 pull-right"><asp:Literal ID="ltTotalUsuarios" runat="server"></asp:Literal></span></a></li>
                    <li id="nuevousuario" class="old"><a href="nuevousuario"><i class="fas fa-user-plus"></i>Nuevo usuario</a></li>
                    <li id="sedes" class="old"><a href="sedes"><i class="fa fa-school-flag"></i>Sedes</a></li>
                    <li id="planes" class="old"><a href="planes"><i class="fa fa-ticket"></i>Planes</a></li>
                    <li id="gympass" class="old"><a href="gympass"><i class="fa fa-user-tag"></i>Gym Pass<span class="label label-warning1 pull-right"><asp:Literal ID="ltTotalInscritos" runat="server"></asp:Literal></span></a></li>
                    <li><a href="#">Resolución de facturas</a></li>
                    <li><a href="#">Productos</a></li>
                    <li><a href="#">Unificar afiliados</a></li>
                    <li id="logactividades" class="old"><a href="logactividades"><i class="fa fa-location-crosshairs"></i>Flujo de Actividades</a></li>
                </ul>
            </li>
            <li>
                <a href="#"><i class="fa fa-cog"></i><span class="nav-label">Configuración</span><span class="fa arrow"></span></a>
                <ul id="configuracion" class="nav nav-second-level collapse">
                    <li id="paginas" class="old"><a href="paginas"><i class="fa fa-file-code"></i>Páginas</a></li>
                    <li id="perfiles" class="old"><a href="perfiles"><i class="fa fa-id-badge"></i>Perfiles</a></li>
                    <li id="profesiones" class="old"><a href="profesiones"><i class="fa fa-briefcase"></i>Profesiones</a></li>
                    <li id="eps" class="old"><a href="eps"><i class="fa fa-house-medical"></i>EPS's</a></li>
                    <li id="pension" class="old"><a href="pension"><i class="fa fa-sack-dollar"></i>Fondos de pensión</a></li>
                    <li id="cajas" class="old"><a href="cajascomp"><i class="fa fa-piggy-bank"></i>Cajas de compensación</a></li>
                    <li id="arl" class="old"><a href="arl"><i class="fa fa-person-falling-burst"></i>ARL's</a></li>
                    <li id="cesantias" class="old"><a href="cesantias"><i class="fa fa-hand-holding-dollar"></i>Fondos de cesantías</a></li>
                    <li id="ciudades" class="old"><a href="ciudades"><i class="fa fa-city"></i>Ciudades</a></li>
                    <li id="ciudadessedes" class="old"><a href="ciudadessedes"><i class="fa fa-building-user"></i>Ciudades sedes</a></li>
                    <li id="tiposdocumento" class="old"><a href="tiposdocumento"><i class="fa fa-id-card"></i>Tipos de documento</a></li>
                    <li id="genero" class="old"><a href="genero"><i class="fa fa-children"></i>Género</a></li>
                    <li id="estadocivil" class="old"><a href="estadocivil"><i class="fa fa-people-pulling"></i>Estado civil</a></li>
                    <li id="objetivosafiliado" class="old"><a href="objetivosafiliado"><i class="fa fa-bullseye"></i>Objetivos afiliado</a></li>
                    <li id="parq" class="old"><a href="parq"><i class="fa fa-person-circle-question"></i>ParQ</a></li>
                    <li id="tiposincapacidades" class="old"><a href="tiposincapacidades"><i class="fa fa-hospital-user"></i>Tipos de incapacidad</a></li>
                    <li id="canalesventa" class="old"><a href="canalesventa"><i class="fa-solid fa-list-ul"></i>Canales de venta</a></li>
                    <li id="tablasbd" class="old"><a href="tablasbd"><i class="fa-solid fa-table"></i>Tablas BD</a></li>
                    <li id="procedimientosalmacenados" class="old"><a href="procedimientosalmacenados"><i class="fa-solid fa-database"></i>Procedimientos almacenados</a></li>
                </ul>
            </li>
            <li>
                <a href="#"><i class="fa fa-fingerprint"></i><span class="nav-label">Biométricos</span><span class="fa arrow"></span></a>
                <ul id="biometricos" class="nav nav-second-level collapse">
                    <li id="biodispositivos" class="old"><a href="biodispositivos"><i class="fa-solid fa-address-card"></i>Dispositivos</a></li>
                    <li id="biopersonas" class="old"><a href="biopersonas"><i class="fa-solid fa-person"></i>Personas</a></li>
                </ul>
            </li>
            <li>
                <a href="#"><i class="fa fa-globe"></i><span class="nav-label">Página Web</span><span class="fa arrow"></span></a>
                <ul id="webpage" class="nav nav-second-level collapse">
                    <li id="categorias" class="old"><a href="categoriastienda"><i class="fa fa-cart-shopping"></i>Categorías tienda</a></li>
                    <li id="productos" class="old"><a href="productostienda"><i class="fa fa-store"></i>Productos tienda</a></li>
                    <li id="nuevoproducto" class="old"><a href="nuevoproductotienda"><i class="fa fa-tag"></i>Nuevo producto</a></li>
                    <li id="blog" class="old"><a href="blogs"><i class="fab fa-blogger"></i>Blog</a></li>
                    <li id="embajadores" class="old"><a href="embajadores"><i class="fa fa-person-rays"></i>Embajadores</a></li>
                </ul>
            </li>
            <li class="special_link">
                <a href="calendariofpadmin"><i class="fa fa-calendar"></i><span class="nav-label">Calendario FP+ Admin</span></a>
            </li>
            <li class="special_link">
                <a href="https://fitnesspeoplecolombia.com" target="_blank"><i class="fa fa-star"></i><span class="nav-label">Fitness People WebPage</span></a>
            </li>
        </ul>

    </div>
</nav>