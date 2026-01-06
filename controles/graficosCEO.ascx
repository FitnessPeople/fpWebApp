<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="graficosCEO.ascx.cs" Inherits="fpWebApp.controles.graficosCEO" %>
<div class="ibox float-e-margins">
    <div class="ibox-title">
        <h5><i class="fa fa-chart-simple"></i> Información gráfica</h5>
        <div class="ibox-tools">
            <a class="collapse-link">
                <i class="fa fa-chevron-up"></i>
            </a>
        </div>
    </div>
    <div class="ibox-content">
        <div class="row">
            <div class="col-lg-4">
                <canvas id="miGrafico1"></canvas>
            </div>
            <div class="col-lg-4">
                <canvas id="miGrafico2"></canvas>
            </div>
            <div class="col-lg-4">
                <canvas id="miGrafico3"></canvas>
            </div>
        </div>
    </div>
</div>

<script>
    function redondearSuperior(valor, base = 1000) {
        return Math.ceil(valor / base) * base;
    }

    // Grafico de Ventas y Cantidad Diaria x mes
    const datos1 = <%= Grafico1 %>;

    const ctx1 = document.getElementById('miGrafico1');

    const maxVentas1 = Math.max(...datos1.ventas_web, ...datos1.ventas_counter);
    const maxCantidad1 = Math.max(...datos1.cantidad_web, ...datos1.cantidad_counter);

    const maxY11 = redondearSuperior(maxVentas1 * 1.1, 100000);
    const maxY12 = Math.ceil(maxCantidad1 * 1.2);

    const data1 = {
        labels: datos1.labels, 
        datasets: [
            {
                type: 'bar',                // Tipo: Barras
                label: 'Ventas Web',
                data: datos1.ventas_web,
                yAxisID: 'y1',              // Asociado al eje Y izquierdo
                backgroundColor: 'rgba(54, 162, 235, 0.5)',
                borderColor: 'rgb(54, 162, 235)',
                borderWidth: 1
            },
            {
                type: 'bar',                // Tipo: Barras
                label: 'Ventas Counter',
                data: datos1.ventas_counter,
                yAxisID: 'y1',              // Asociado al eje Y izquierdo
                backgroundColor: 'rgba(255, 206, 86, 0.6)',
                borderColor: 'rgb(255, 206, 86)',
                borderWidth: 1
            },
            {
                type: 'line',               // Tipo: Línea
                label: 'Cantidad Web',
                data: datos1.cantidad_web,
                yAxisID: 'y2',              // Asociado al eje Y derecho
                borderColor: 'rgb(255, 99, 132)',
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                tension: 0,
                fill: false
            },
            {
                type: 'line',               // Tipo: Línea
                label: 'Cantidad Counter',
                data: datos1.cantidad_counter,
                yAxisID: 'y2',              // Asociado al eje Y derecho
                borderColor: 'rgb(75, 192, 192)',
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                tension: 0,
                fill: false
            }
        ]
    };

    new Chart(ctx1, {
        data: data1,
        options: {
            responsive: true,
            interaction: {
                mode: 'index',
                intersect: false
            },
            stacked: false,
            scales: {
                y1: {
                    type: 'linear',
                    position: 'left',
                    min: 0,
                    max: maxY11,
                    title: { display: true, text: 'Ventas' },
                    grid: { drawOnChartArea: true }
                },
                y2: {
                    type: 'linear',
                    position: 'right',
                    min: 0,
                    max: maxY12,
                    title: { display: true, text: 'Cantidad' },
                    grid: { drawOnChartArea: false } // Evita duplicar líneas de cuadrícula
                }
            },
            plugins: {
                title: {
                    display: true,
                    text: 'Ventas totales últimos 3 meses'
                }
            }
        }
    });


    // Grafico de Ventas y Cantidad por Usuario
    const datos2 = <%= Grafico2 %>;

    const ctx2 = document.getElementById('miGrafico2');

    const maxVentas2 = Math.max(...datos2.ventas);
    const maxCantidad2 = Math.max(...datos2.cantidad);

    const maxY21 = redondearSuperior(maxVentas2 * 1.1, 100000);
    const maxY22 = Math.ceil(maxCantidad2 * 1.2);

    const data2 = {
        labels: datos2.labels, // nombres de canal
        datasets: [
            {
                type: 'bar',                // Tipo: Barras
                label: 'Ventas',
                data: datos2.ventas,
                yAxisID: 'y1',              // Asociado al eje Y izquierdo
                backgroundColor: 'rgba(54, 162, 235, 0.5)',
                borderColor: 'rgb(54, 162, 235)',
                borderWidth: 1
            },
            {
                type: 'line',               // Tipo: Línea
                label: 'Cantidad',
                data: datos2.cantidad,
                yAxisID: 'y2',              // Asociado al eje Y derecho
                borderColor: 'rgb(255, 99, 132)',
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                tension: 0,
                fill: false
            }
        ]
    };

    new Chart(ctx2, {
        data: data2,
        options: {
            responsive: true,
            interaction: {
                mode: 'index',
                intersect: false
            },
            stacked: false,
            scales: {
                y1: {
                    type: 'linear',
                    position: 'left',
                    min: 0,
                    max: maxY21,
                    title: { display: true, text: 'Ventas' },
                    grid: { drawOnChartArea: true }
                },
                y2: {
                    type: 'linear',
                    position: 'right',
                    min: 0,
                    max: maxY22,
                    title: { display: true, text: 'Cantidad' },
                    grid: { drawOnChartArea: false } // Evita duplicar líneas de cuadrícula
                }
            },
            plugins: {
                title: {
                    display: true,
                    text: 'Comparativo de Ventas y Cantidad por Usuario'
                }
            }
        }
    });



    // Grafico de Ventas y Cantidad por Canal de Venta
    const datos3 = <%= Grafico3 %>;

    const ctx3 = document.getElementById('miGrafico3');

    const maxVentas3 = Math.max(...datos3.ventas);
    const maxCantidad3 = Math.max(...datos3.cantidad);

    const maxY31 = redondearSuperior(maxVentas3 * 1.1, 100000);
    const maxY32 = Math.ceil(maxCantidad3 * 1.2);

    const data3 = {
        labels: datos3.labels, // nombres de canal
        datasets: [
            {
                type: 'bar',                // Tipo: Barras
                label: 'Ventas',
                data: datos3.ventas,
                yAxisID: 'y1',              // Asociado al eje Y izquierdo
                backgroundColor: 'rgba(54, 162, 235, 0.5)',
                borderColor: 'rgb(54, 162, 235)',
                borderWidth: 1
            },
            {
                type: 'line',               // Tipo: Línea
                label: 'Cantidad',
                data: datos3.cantidad,
                yAxisID: 'y2',              // Asociado al eje Y derecho
                borderColor: 'rgb(255, 99, 132)',
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                tension: 0,
                fill: false
            }
        ]
    };

    new Chart(ctx3, {
        data: data3,
        options: {
            responsive: true,
            interaction: {
                mode: 'index',
                intersect: false
            },
            stacked: false,
            scales: {
                y1: {
                    type: 'linear',
                    position: 'left',
                    min: 0,
                    max: maxY31,
                    title: { display: true, text: 'Ventas' },
                    grid: { drawOnChartArea: true }
                },
                y2: {
                    type: 'linear',
                    position: 'right',
                    min: 0,
                    max: maxY32,
                    title: { display: true, text: 'Cantidad' },
                    grid: { drawOnChartArea: false } // Evita duplicar líneas de cuadrícula
                }
            },
            plugins: {
                title: {
                    display: true,
                    text: 'Comparativo de Ventas y Cantidad por Canal de Venta'
                }
            }
        }
    });
</script>