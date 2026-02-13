<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="graficosDirRRHH.ascx.cs" Inherits="fpWebApp.controles.graficosDirRRHH" %>


                    <div class="wrapper wrapper-content animated fadeInRight" style="padding: 20px 10px 0px;">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Información Sociodemográfica</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row text-center">
                                            <div class="col-lg-3">
                                                <canvas id="barChart1" height="150"></canvas>
                                                <h5><i class="fa fa-venus-mars fa-2x text-navy m-r-xs"></i>Géneros</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="barChart2" height="150"></canvas>
                                                <h5><i class="fa fa-city text-navy fa-2x m-r-xs"></i>Ciudades</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="barChart3" height="150"></canvas>
                                                <h5><i class="fa fa-ring text-navy fa-2x m-r-xs"></i>Estado civil</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="barChart4" height="150"></canvas>
                                                <h5><i class="fa fa-file-lines fa-2x text-navy m-r-xs"></i>Tipo de contrato</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="wrapper wrapper-content animated fadeInRight" style="padding: 20px 10px 0px;">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Perfil Socioeconómico y Hábitos</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row text-center">
                                            <div class="col-lg-3">
                                                <canvas id="barChart" height="150"></canvas>
                                                <h5><i class="fa fa-graduation-cap fa-2x text-navy m-r-xs"></i>Nivel de estudio</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="doughnutChart6" height="150"></canvas>
                                                <h5><i class="fa fa-house fa-2x text-navy m-r-xs"></i>Tipo de vivienda</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="doughnutChart7" height="150"></canvas>
                                                <h5><i class="fa fa-person-rays fa-2x text-navy m-r-xs"></i>Actividad extra</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="doughnutChart8" height="150"></canvas>
                                                <h5><i class="fa fa-martini-glass fa-2x text-navy m-r-xs"></i>Consumo de licor</h5>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="wrapper wrapper-content animated fadeInRight" style="padding: 20px 10px 0px;">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <h5>Salud y Movilidad del Personal</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <div class="row text-center">
                                            <div class="col-lg-3">
                                                <canvas id="barChart9" height="150"></canvas>
                                                <h5><i class="fa fa-person-cane fa-2x text-navy m-r-xs"></i>Edades</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="barChart10" height="150"></canvas>
                                                <h5><i class="fa fa-car fa-2x text-navy m-r-xs"></i>Medio de transporte</h5>
                                            </div>
                                            <div class="col-lg-3">
                                                <canvas id="barChart11" height="150"></canvas>
                                                <h5><i class="fa fa-droplet fa-2x text-navy m-r-xs"></i>Tipo de sangre</h5>
                                            </div>
                                            <%--<div class="col-lg-3">
                                                <canvas id="doughnutChart8" height="150"></canvas>
                                                <h5>Consumo licor</h5>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



    <script>
        // Gráfico de Géneros

        $(function () {

            const colores1 = cantidades1.map((_, index) => {
                const hue = (index * 360) / cantidades1.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres1,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores1,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades1
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
                    onComplete: function () {
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

            var ctx4 = document.getElementById("barChart1").getContext("2d");
            new Chart(ctx4, { type: 'bar', data: barData, options: barOptions });


            // Grafica Ciudades

            const colores2 = cantidades2.map((_, index) => {
                const hue = (index * 360) / cantidades2.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres2,
                datasets: [
                    {
                        label: "Empleados",
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
                    onComplete: function () {
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
                                ctx.fillText(value, bar._model.x + 10, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("barChart2").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });



            // Grafica Estado civil

            const colores3 = cantidades3.map((_, index) => {
                const hue = (index * 360) / cantidades3.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres3,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores3,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades3
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
                    onComplete: function () {
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
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("barChart3").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Grafica TipoContrato

            const colores4 = cantidades4.map((_, index) => {
                const hue = (index * 360) / cantidades4.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres4,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores4,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades4
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
                    onComplete: function () {
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
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("barChart4").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Gráfica Nivel de Estudio

            const colores5 = cantidades5.map((_, index) => {
                const hue = (index * 360) / cantidades5.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres5,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores5,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades5
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
                    onComplete: function () {
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
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("barChart").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Gráfica Tipo de Vivienda

            const colores6 = cantidades6.map((_, index) => {
                const hue = (index * 360) / cantidades6.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });
            
            var barData = {
                labels: nombres6,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores6,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades6
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
                    onComplete: function () {
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
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("doughnutChart6").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Gráfica Actividad Extra

            const colores7 = cantidades7.map((_, index) => {
                const hue = (index * 360) / cantidades7.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });
            
            var barData = {
                labels: nombres7,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores7,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades7
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
                    onComplete: function () {
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
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("doughnutChart7").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Gráfica Consume Licor

            const colores8 = cantidades8.map((_, index) => {
                const hue = (index * 360) / cantidades8.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });
            
            var barData = {
                labels: nombres8,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores8,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades8
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
                    onComplete: function () {
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
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("doughnutChart8").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Gráfico de edades

            const colores9 = cantidades9.map((_, index) => {
                const hue = (index * 360) / cantidades9.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres9,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores9,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades9
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
                    onComplete: function () {
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
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("barChart9").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Gráfico de Medio de Transporte

            const colores10 = cantidades10.map((_, index) => {
                const hue = (index * 360) / cantidades10.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres10,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores10,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades10
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
                    onComplete: function () {
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
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("barChart10").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });


            // Gráfico de Tipo de Sangre

            const colores11 = cantidades11.map((_, index) => {
                const hue = (index * 360) / cantidades11.length;
                return `hsla(${hue}, 70%, 55%, 0.7)`;

            });

            var barData = {
                labels: nombres11,
                datasets: [
                    {
                        label: "Empleados",
                        backgroundColor: colores11,
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: cantidades11
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
                    onComplete: function () {
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
                                ctx.fillText(value, bar._model.x + 7, bar._model.y + 8);
                            });
                        });
                    }
                }
            };

            var ctx4 = document.getElementById("barChart11").getContext("2d");
            new Chart(ctx4, { type: 'horizontalBar', data: barData, options: barOptions });
        });

    </script>