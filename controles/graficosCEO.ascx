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
    $(function () {

        // Gráfico de Ventas y Cantidades últimos 3 meses
        const colores1 = sumatoria1.map((_, index) => {
            const hue = (index * 360) / sumatoria1.length;
            return `hsla(${hue}, 70%, 55%, 0.7)`;

        });

        var barData = {
            labels: nombres1,
            datasets: [
                {
                    yAxisID: 'y-bar',
                    type: 'bar',
                    label: "Ventas",
                    backgroundColor: colores1,
                    borderColor: "rgba(26,179,148,0.7)",
                    pointBackgroundColor: "rgba(26,179,148,1)",
                    pointBorderColor: "#fff",
                    data: sumatoria1
                }
            ]
        };

        var barOptions = {
            responsive: true,
            legend: {
                display: true,
                labels: {
                    usePointStyle: true
                }
            },
            tooltips: {
                enabled: true,
                mode: 'index',
                intersect: false,
                callbacks: {
                    label: function (tooltipItem, data) {
                        var dataset = data.datasets[tooltipItem.datasetIndex];
                        var value = tooltipItem.yLabel;

                        if (dataset.type === 'bar') {
                            return 'Ventas: $ ' + value.toLocaleString('es-CO');
                        }
                    }
                }
            },
            scales: {
                yAxes: [
                    {
                        id: 'y-bar',
                        position: 'left',
                        ticks: {
                            beginAtZero: true,
                            suggestedMax: Math.max(...sumatoria1) * 1.2,
                            callback: function (value) {
                                return '$ ' + value.toLocaleString('es-CO');
                            }
                        },
                        scaleLabel: {
                            display: true,
                            labelString: 'Ventas'
                        }
                    }
                ]
            },
            animation: {
                duration: 1,
                onProgress: function () {
                    var chartInstance = this.chart;
                    var ctx = chartInstance.ctx;

                    ctx.font = "10px Arial";
                    ctx.fillStyle = "#000";
                    ctx.textAlign = "center";
                    ctx.textBaseline = "bottom";

                    this.data.datasets.forEach(function (dataset, i) {
                        var meta = chartInstance.controller.getDatasetMeta(i);

                        // 🔵 BARRAS (Ventas)
                        if (dataset.type === 'bar') {
                            meta.data.forEach(function (bar, index) {
                                var value = dataset.data[index];
                                var yPos = bar._model.y - 5;
                                if (yPos < 15) yPos = 15;

                                var texto = '$ ' + value.toLocaleString('es-CO');
                                ctx.fillText(texto, bar._model.x, yPos);
                            });
                        }
                    });
                }
            }
        };

        var ctx1 = document.getElementById("miGrafico1").getContext("2d");
        new Chart(ctx1, { type: 'bar', data: barData, options: barOptions });


        // Gráfico de Afiliados Activos por Sede
        const colores2 = cantidades2.map((_, index) => {
            const hue = (index * 360) / cantidades2.length;
            return `hsla(${hue}, 70%, 55%, 0.7)`;

        });

        var barData = {
            labels: nombres2,
            datasets: [
                {
                    label: "Afiliados",
                    backgroundColor: colores2,
                    borderColor: "rgba(26,179,148,0.7)",
                    pointBackgroundColor: "rgba(26,179,148,1)",
                    pointBorderColor: "#fff",
                    data: cantidades2
                }
            ]
        };

        var barOptions = {
            responsive: true,
            legend: {
                display: false
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            },
            animation: {
                onProgress: function () {
                    var chartInstance = this.chart;
                    var ctx = chartInstance.ctx;

                    ctx.font = "10px Arial";
                    ctx.fillStyle = "#000";
                    ctx.textAlign = "center";
                    ctx.textBaseline = "bottom";

                    this.data.datasets.forEach(function (dataset, i) {
                        var meta = chartInstance.controller.getDatasetMeta(i);
                        meta.data.forEach(function (bar, index) {
                            var value = dataset.data[index];
                            ctx.fillText(value, bar._model.x, bar._model.y - 5);
                        });
                    });
                }
            }
        };

        var ctx1 = document.getElementById("miGrafico2").getContext("2d");
        new Chart(ctx1, { type: 'bar', data: barData, options: barOptions });
    });
</script>