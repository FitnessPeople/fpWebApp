<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="imprimirhistoriaclinica.aspx.cs" Inherits="fpWebApp.imprimirhistoriaclinica" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Imprimir</title>

    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.6.0/css/all.min.css" rel="stylesheet" />

    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
</head>
<body onload="window.print();" class="gray-bg">

    <form id="form1" runat="server">
        <div class="table-responsive">
            <asp:Repeater ID="rpHistoriasClinicas" runat="server">
                <ItemTemplate>
                    <h3 class="text-info"><i class="fa fa-notes-medical"></i> Historia Clínica</h3>
                    <table class="table table-bordered table-striped">
                        <tr>
                            <th>Fecha de creación</th>
                            <th>Documento</th>
                            <th>Afiliado</th>
                            <th>Género</th>
                            <th>Edad</th>
                        </tr>
                        <tr>
                            <td><i class="fa fa-calendar-day m-r-xs"></i><%# Eval("FechaHora", "{0:dd MMM yyyy}") %> <i class="fa fa-clock m-r-xs"></i><%# Eval("FechaHora", "{0:HH:mm}") %></td>
                            <td><%# Eval("DocumentoAfiliado") %></td>
                            <td><%# Eval("NombreAfiliado") %> <%# Eval("ApellidoAfiliado") %></td>
                            <%--<td><%# Eval("iconGenero") %> <%# Eval("Genero") %></td>--%>
                            <td><%# Eval("idGenero").ToString() == "1"
                                ? "<i class='fa fa fa-mars'></i> Si"
                                : "<i class='fa fa fa-venus'></i> No" %> <%# Eval("Genero") %></td>
                            <td><%# Eval("Edad") %> años</td>
                        </tr>
                    </table>
                    <h3 class="text-info"><i class="fa fa-clock-rotate-left"></i> Antecedentes</h3>
                    <table class="table table-bordered table-striped">
                        <tr>
                            <th width="20%"><i class="fa fa-people-roof m-r-sm"></i>Familiares</th>
                            <th width="20%"><i class="fa fa-virus m-r-sm"></i>Patológicos</th>
                            <th width="20%"><i class="fa fa-syringe m-r-sm"></i>Quirúrgicos</th>
                            <th width="20%"><i class="fa fa-biohazard m-r-sm"></i>Toxicológico</th>
                            <th width="20%"><i class="fa fa-hospital m-r-sm"></i>Hospitalario</th>
                        </tr>
                        <tr>
                            <td><%# Eval("AnteFamiliar") %></td>
                            <td><%# Eval("AntePatologico") %></td>
                            <td><%# Eval("AnteQuirurgico") %></td>
                            <td><%# Eval("AnteToxicologico") %></td>
                            <td><%# Eval("AnteHospitalario") %></td>
                        </tr>
                        <tr>
                            <th width="20%"><i class="fa fa-crutch m-r-sm"></i>Traumatológico</th>
                            <th width="20%"><i class="fa fa-capsules m-r-sm"></i>Farmacológico</th>
                            <th width="20%"><i class="fa fa-person-running m-r-sm"></i>Actividad Física</th>
                            <th width="20%"><i class="fa fa-person-pregnant m-r-sm"></i>Gineco-Obstetricia</th>
                            <th width="20%"><i class="fa fa-droplet m-r-sm"></i>F.U.M.</th>
                        </tr>
                        <tr>
                            <td><%# Eval("AnteTraumatologico") %></td>
                            <td><%# Eval("AnteFarmacologico") %></td>
                            <td><%# Eval("AnteActividadFisica") %></td>
                            <td><%# Eval("AnteGineco") %></td>
                            <td><%# Eval("AnteFUM", "{0:dd MMM yyyy}") %></td>
                        </tr>
                    </table>
                    <h3 class="text-info"><i class="fa fa-heart-circle-exclamation"></i> Factores de Riesgo Cardiovascular</h3>
                    <table class="table table-bordered table-striped" style="margin-bottom: 0px;">
                        <tr>
                            <th width="25%"><i class="fa fa-smoking m-r-sm"></i>Tabaco</th>
                            <th width="25%"><i class="fa fa-smoking m-r-sm"></i>Cigarrillos/día</th>
                            <th width="25%"><i class="fa fa-wine-bottle m-r-sm"></i>Alcohol</th>
                            <th width="25%"><i class="fa fa-wine-bottle m-r-sm"></i>Bebidas/mes</th>
                        </tr>
                        <tr>
                            <td> <%# Eval("Tabaquismo").ToString() == "1"
                                 ? "<i class='fa fa-square-check text-danger'></i> Si"
                                 : "<i class='fa fa-square text-navy'></i> No" %></td>
                            <td><%# Eval("Cigarrillos") %></td>
                            <td><%# Eval("Alcoholismo").ToString() == "1"
                                ? "<i class='fa fa-square-check text-danger'></i> Si"
                                : "<i class='fa fa-square text-navy'></i> No" %></td>
                            <td><%# Eval("Bebidas") %></td>
                        </tr>
                    </table>
                    <table class="table table-bordered table-striped">
                        <tr>
                            <th width="20%"><i class="fa fa-chair m-r-sm"></i>Sedentarismo</th>
                            <th width="20%"><i class="fa fa-vial m-r-sm"></i>Diabetes</th>
                            <th width="20%"><i class="fa fa-heart-pulse m-r-sm"></i>Colesterol</th>
                            <th width="20%"><i class="fa fa-heart-circle-bolt m-r-sm"></i>Triglicéridos</th>
                            <th width="20%"><i class="fa fa-stethoscope m-r-sm"></i>H.T.A.</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Sedentarismo").ToString() == "1"
                                ? "<i class='fa fa-square-check text-danger'></i> Si"
                                : "<i class='fa fa-square text-navy'></i> No" %></td>
                            <td><%# Eval("Diabetes").ToString() == "1"
                                ? "<i class='fa fa-square-check text-danger'></i> Si"
                                : Eval("Diabetes").ToString() == "2"
                                    ? "<i class='fa fa-comment-slash text-warning'></i> NS/NR"
                                    : "<i class='fa fa-square text-navy'></i> No" %></td>
                            <td><%# Eval("Colesterol").ToString() == "1"
                                ? "<i class='fa fa-square-check text-danger'></i> Si"
                                : Eval("Colesterol").ToString() == "2"
                                    ? "<i class='fa fa-comment-slash text-warning'></i> NS/NR"
                                    : "<i class='fa fa-square text-navy'></i> No" %></td>
                            <td><%# Eval("Trigliceridos").ToString() == "1"
                                ? "<i class='fa fa-square-check text-danger'></i> Si"
                                : Eval("Trigliceridos").ToString() == "2"
                                    ? "<i class='fa fa-comment-slash text-warning'></i> NS/NR"
                                    : "<i class='fa fa-square text-navy'></i> No" %></td>
                            <td><%# Eval("HTA").ToString() == "1"
                                ? "<i class='fa fa-square-check text-danger'></i> Si"
                                : Eval("HTA").ToString() == "2"
                                    ? "<i class='fa fa-comment-slash text-warning'></i> NS/NR"
                                    : "<i class='fa fa-square text-navy'></i> No" %></td>
                        </tr>
                    </table>
                    <h3 class="text-info"><i class="fa fa-utensils"></i> Historia alimentaria (Nutricionista)</h3>
                    <table class="table table-bordered table-striped" style="margin-bottom: 0px;">
                        <tr>
                            <th width="33%">Gastritis</th>
                            <th width="33%">Colon</th>
                            <th width="34%">Estreñimiento</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Gastritis").ToString() == "1"
                                ? "<i class='fa fa-square-check text-danger'></i> Si"
                                : "<i class='fa fa-square text-navy'></i> No" %></td>
                            <td><%# Eval("Colon").ToString() == "1"
                                ? "<i class='fa fa-square-check text-danger'></i> Si"
                                : "<i class='fa fa-square text-navy'></i> No" %></td>
                            <td><%# Eval("Estrenimiento").ToString() == "1"
                                ? "<i class='fa fa-square-check text-danger'></i> Si"
                                : "<i class='fa fa-square text-navy'></i> No" %></td>
                        </tr>
                    </table>
                    <table class="table table-bordered table-striped">
                        <tr>
                            <th width="50%">Cafeína</th>
                            <th width="50%">Alimentos no tolerados</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Cafeina") %></td>
                            <td><%# Eval("AlimNoTolerados") %></td>
                        </tr>
                        <tr>
                            <th width="50%">Complementos</th>
                            <th width="50%">Nutrición anterior</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Complementos") %></td>
                            <td><%# Eval("NutriAnterior") %></td>
                        </tr>
                        <tr>
                            <th width="50%">Paraclínicos</th>
                            <th width="50%">Apetito</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Paraclinicos") %></td>
                            <td><%# Eval("Apetito") %></td>
                        </tr>
                        <tr>
                            <th width="50%">Masticación</th>
                            <th width="50%">Hábito intestinal</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Masticacion") %></td>
                            <td><%# Eval("HabitoIntestinal") %></td>
                        </tr>
                        <tr>
                            <th width="50%">Síntomas gastrointestinales</th>
                            <th width="50%">Alimentos preferidos</th>
                        </tr>
                        <tr>
                            <td><%# Eval("SintGastrointestinales") %></td>
                            <td><%# Eval("AlimPreferidos") %></td>
                        </tr>
                    </table>
                    <h3 class="text-info"><i class="fa fa-carrot"></i> Anamnesis alimentaria</h3>
                    <table class="table table-bordered table-striped" style="margin-bottom: 0px;">
                        <tr>
                            <th width="50%">Desayuno</th>
                            <th width="50%">Nueves</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Desayuno") %></td>
                            <td><%# Eval("Nueves") %></td>
                        </tr>
                        <tr>
                            <th width="50%">Almuerzo</th>
                            <th width="50%">Onces</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Almuerzo") %></td>
                            <td><%# Eval("Onces") %></td>
                        </tr>
                        <tr>
                            <th width="50%">Cena</th>
                            <th width="50%">Merienda</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Cena") %></td>
                            <td><%# Eval("Merienda") %></td>
                        </tr>
                        <tr>
                            <th width="50%">Datos bioquímicos</th>
                            <th width="50%">Medicamentos</th>
                        </tr>
                        <tr>
                            <td><%# Eval("DatosBioquimicos") %></td>
                            <td><%# Eval("Medicamentos") %></td>
                        </tr>
                        <tr>
                            <th width="50%">Alergias</th>
                            <th width="50%">Proteínas</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Alergias") %></td>
                            <td><%# Eval("Proteinas") %></td>
                        </tr>
                        <tr>
                            <th width="50%">Carbohidratos</th>
                            <th width="50%">Somatotipo</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Carbohidratos") %></td>
                            <td><%# Eval("Somatotipo") %></td>
                        </tr>
                    </table>
                    <table class="table table-bordered table-striped">
                        <tr>
                            <th width="33%">¿A qué hora se levanta?</th>
                            <th width="33%">¿A qué hora desayuna?</th>
                            <th width="34%">¿A qué hora se acuesta?</th>
                        </tr>
                        <tr>
                            <td><%# Eval("HoraLevanta") %></td>
                            <td><%# Eval("HoraDesayuno") %></td>
                            <td><%# Eval("HoraAcuesta") %></td>
                        </tr>
                    </table>
                    <h3 class="text-info"><i class="fa fa-burger"></i> Frecuencia de consumo</h3>
                    <table class="table table-bordered table-striped">
                        <tr>
                            <th width="33%">Lacteos</th>
                            <th width="33%">Azúcares</th>
                            <th width="34%">Gaseosas</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Lacteos") %></td>
                            <td><%# Eval("Azucares") %></td>
                            <td><%# Eval("Gaseosa") %></td>
                        </tr>
                        <tr>
                            <th width="33%">Verduras</th>
                            <th width="33%">Salsamentaria</th>
                            <th width="34%">Agua</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Verduras") %></td>
                            <td><%# Eval("Salsamentaria") %></td>
                            <td><%# Eval("Agua") %></td>
                        </tr>
                        <tr>
                            <th width="33%">Frutas</th>
                            <th width="33%">Carnes</th>
                            <th width="34%">Comidas rápidas</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Frutas") %></td>
                            <td><%# Eval("Carnes") %></td>
                            <td><%# Eval("ComidasRapidas") %></td>
                        </tr>
                        <tr>
                            <th width="33%">Cigarrillos</th>
                            <th width="33%">Psicoactivos</th>
                            <th width="34%">Huevos</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Cigarrillos") %></td>
                            <td><%# Eval("Psicoactivos") %></td>
                            <td><%# Eval("Huevos") %></td>
                        </tr>
                        <tr>
                            <th width="33%">Visceras</th>
                            <th width="33%">Sopas</th>
                            <th width="34%">Paquetes</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Visceras") %></td>
                            <td><%# Eval("Sopas") %></td>
                            <td><%# Eval("Paquetes") %></td>
                        </tr>
                        <tr>
                            <th width="33%">Cereales</th>
                            <th width="33%">Raíces</th>
                            <th width="34%">Pan</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Cereales") %></td>
                            <td><%# Eval("Raices") %></td>
                            <td><%# Eval("Pan") %></td>
                        </tr>
                        <tr>
                            <th width="33%">Grasas</th>
                            <th width="33%">Alcohol</th>
                            <th width="34%">Bebida hidratante</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Grasas") %></td>
                            <td><%# Eval("Alcohol") %></td>
                            <td><%# Eval("BebidaHidratante") %></td>
                        </tr>
                    </table>
                    <h3 class="text-info"><i class="fa fa-person-arrow-up-from-line"></i> Antropometría</h3>
                    <table class="table table-bordered table-striped" style="margin-bottom: 0px;">
                        <tr>
                            <th width="33%">Peso</th>
                            <th width="33%">Talla</th>
                            <th width="34%">Indice de Masa Corporal IMC</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Peso") %> Kg</td>
                            <td><%# Eval("Talla") %> cms</td>
                            <td><%# Eval("IMC") %></td>
                        </tr>
                    </table>
                    <table class="table table-bordered table-striped" style="margin-bottom: 0px;">
                        <tr>
                            <th width="25%">Perímetro de Cintura</th>
                            <th width="25%">Perímetro de Cadera</th>
                            <th width="25%">Perímetro de Abdomen</th>
                            <th width="25%">Perímetro de Pecho</th>
                        </tr>
                        <tr>
                            <td><%# Eval("PerimCintura") %> cms</td>
                            <td><%# Eval("PerimCadera") %> cms</td>
                            <td><%# Eval("PerimAbdomen") %> cms</td>
                            <td><%# Eval("PerimPecho") %> cms</td>
                        </tr>
                    </table>
                    <table class="table table-bordered table-striped" style="margin-bottom: 0px;">
                        <tr>
                            <th width="33%">Perímetro de Muslo</th>
                            <th width="33%">Perímetro de Pantorrilla</th>
                            <th width="34%">Perímetro de Brazo</th>
                        </tr>
                        <tr>
                            <td><%# Eval("PerimMuslo") %></td>
                            <td><%# Eval("PerimPantorrilla") %></td>
                            <td><%# Eval("PerimBrazo") %></td>
                        </tr>
                        <tr>
                            <th width="33%">Pliegue Tricipital</th>
                            <th width="33%">Pliegue IlioCrestal</th>
                            <th width="34%">Pliegue Abdominal</th>
                        </tr>
                        <tr>
                            <td><%# Eval("PliegueTricipital") %></td>
                            <td><%# Eval("PliegueIliocrestal") %></td>
                            <td><%# Eval("PliegueAbdominal") %></td>
                        </tr>
                        <tr>
                            <th width="33%">Pliegue Subescapular</th>
                            <th width="33%">Pliegue Muslo</th>
                            <th width="34%">Pliegue Pantorrilla</th>
                        </tr>
                        <tr>
                            <td><%# Eval("PliegueSubescapular") %></td>
                            <td><%# Eval("PliegueMuslo") %></td>
                            <td><%# Eval("PlieguePantorrilla") %></td>
                        </tr>
                        <tr>
                            <th width="33%">Porcentaje Graso</th>
                            <th width="33%">Porcentaje Muscular</th>
                            <th width="34%">FCE (Tanaka)</th>
                        </tr>
                        <tr>
                            <td><%# Eval("PorcGrasa") %> %</td>
                            <td><%# Eval("PorcMuscular") %> %</td>
                            <td><%# Eval("FCETanaka") %></td>
                        </tr>
                        <tr>
                            <th width="33%">Peso esperado</th>
                            <th width="33%">Peso graso</th>
                            <th width="34%">Peso magro</th>
                        </tr>
                        <tr>
                            <td><%# Eval("PorcGrasa") %> Kg</td>
                            <td><%# Eval("PorcMuscular") %> Kg</td>
                            <td><%# Eval("FCETanaka") %> Kg</td>
                        </tr>
                        <tr>
                            <th width="33%">Gasto calórico</th>
                            <th width="33%">Actividad física</th>
                            <th width="34%">Gasto total</th>
                        </tr>
                        <tr>
                            <td><%# Eval("GastoCalorico") %></td>
                            <td><%# Eval("ActividadFisica") %></td>
                            <td><%# Eval("GastoTotal") %></td>
                        </tr>
                    </table>
                    <table class="table table-bordered table-striped">
                        <tr>
                            <th width="50%">Diagnóstico</th>
                            <th width="50%">Plan de manejo</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Diagnostico") %></td>
                            <td><%# Eval("PlanManejo") %></td>
                        </tr>
                        <tr>
                            <th width="50%">Recomendaciones</th>
                            <th width="50%">Observaciones</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Recomendaciones") %></td>
                            <td><%# Eval("Observaciones") %></td>
                        </tr>
                    </table>

                    <hr />

                    <h3 class="text-info"><i class="fa fa-person-arrow-up-from-line"></i> Historia Fisioterapéutica</h3>
                    <table class="table table-bordered table-striped" style="margin-bottom: 0px;">
                        <tr>
                            <th width="33%">FC Reposo</th>
                            <th width="33%">TA Reposo</th>
                            <th width="34%">FC Max</th>
                        </tr>
                        <tr>
                            <td><%# Eval("FCReposo") %></td>
                            <td><%# Eval("TAReposo") %></td>
                            <td><%# Eval("FCMax") %></td>
                        </tr>
                        <tr>
                            <th width="33%">Peso</th>
                            <th width="33%">Talla</th>
                            <th width="34%">IMC</th>
                        </tr>
                        <tr>
                            <td><%# Eval("Peso") %> Kg</td>
                            <td><%# Eval("Talla") %> cms</td>
                            <td><%# Eval("IMC") %></td>
                        </tr>
                    </table>
                    <table class="table table-bordered table-striped" style="margin-bottom: 0px;">
                        <tr>
                            <th width="25%">Perímetro de Cintura</th>
                            <th width="25%">Perímetro de Cadera</th>
                            <th width="25%">Perímetro de Abdomen</th>
                            <th width="25%">Perímetro de Pecho</th>
                        </tr>
                        <tr>
                            <td><%# Eval("PerimCintura") %> cms</td>
                            <td><%# Eval("PerimCadera") %> cms</td>
                            <td><%# Eval("PerimAbdomen") %> cms</td>
                            <td><%# Eval("PerimPecho") %> cms</td>
                        </tr>
                    </table>
                    <table class="table table-bordered table-striped" style="margin-bottom: 0px;">
                        <tr>
                            <th width="33%">Perímetro de Muslo</th>
                            <th width="33%">Perímetro de Pantorrilla</th>
                            <th width="34%">Perímetro de Brazo</th>
                        </tr>
                        <tr>
                            <td><%# Eval("PerimMuslo") %></td>
                            <td><%# Eval("PerimPantorrilla") %></td>
                            <td><%# Eval("PerimBrazo") %></td>
                        </tr>
                        <tr>
                            <th width="33%">Pliegue Tricipital</th>
                            <th width="33%">Pliegue IlioCrestal</th>
                            <th width="34%">Pliegue Abdominal</th>
                        </tr>
                        <tr>
                            <td><%# Eval("PliegueTricipital") %></td>
                            <td><%# Eval("PliegueIliocrestal") %></td>
                            <td><%# Eval("PliegueAbdominal") %></td>
                        </tr>
                        <tr>
                            <th width="33%">Pliegue Subescapular</th>
                            <th width="33%">Pliegue Muslo</th>
                            <th width="34%">Pliegue Pantorrilla</th>
                        </tr>
                        <tr>
                            <td><%# Eval("PliegueSubescapular") %></td>
                            <td><%# Eval("PliegueMuslo") %></td>
                            <td><%# Eval("PlieguePantorrilla") %></td>
                        </tr>
                        <tr>
                            <th width="33%">Porcentaje Graso</th>
                            <th width="33%">Porcentaje Muscular</th>
                            <th width="34%">FCE (Tanaka)</th>
                        </tr>
                        <tr>
                            <td><%# Eval("PorcGrasa") %> %</td>
                            <td><%# Eval("PorcMuscular") %> %</td>
                            <td><%# Eval("FCETanaka") %></td>
                        </tr>
                        <tr>
                            <th width="33%">Peso esperado</th>
                            <th width="33%">Peso graso</th>
                            <th width="34%">Peso magro</th>
                        </tr>
                        <tr>
                            <td><%# Eval("PorcGrasa") %> Kg</td>
                            <td><%# Eval("PorcMuscular") %> Kg</td>
                            <td><%# Eval("FCETanaka") %> Kg</td>
                        </tr>
                        <tr>
                            <th width="33%">Gasto calórico</th>
                            <th width="33%">Actividad física</th>
                            <th width="34%">Gasto total</th>
                        </tr>
                        <tr>
                            <td><%# Eval("GastoCalorico") %></td>
                            <td><%# Eval("ActividadFisica") %></td>
                            <td><%# Eval("GastoTotal") %></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
