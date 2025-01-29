<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="grafico01.ascx.cs" Inherits="fpWebApp.controles.grafico01" %>
<div class="row">
    <div class="col-lg-12">
        <div class="ibox float-e-margins">
            <div class="ibox-title">
                <h5>Afiliados</h5>
                <div class="pull-right">
                    <div class="btn-group">
                        <button type="button" class="btn btn-xs btn-white active">Hoy</button>
                        <button type="button" class="btn btn-xs btn-white">Mensual</button>
                        <button type="button" class="btn btn-xs btn-white">Anual</button>
                    </div>
                </div>
            </div>
            <div class="ibox-content">
                <div class="row">
                    <div class="col-lg-9">
                        <div class="flot-chart">
                            <div class="flot-chart-content" id="flot-dashboard-chart"></div>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <ul class="stat-list">
                            <li>
                                <h2 class="no-margins">2,346</h2>
                                <small>Solicitudes de afiliación mes actual</small>
                                <div class="stat-percent">48% <i class="fa fa-level-up text-navy"></i></div>
                                <div class="progress progress-mini">
                                    <div style="width: 48%;" class="progress-bar"></div>
                                </div>
                            </li>
                            <li>
                                <h2 class="no-margins">4,422</h2>
                                <small>Solicitudes del mes anterior</small>
                                <div class="stat-percent">60% <i class="fa fa-level-down text-navy"></i></div>
                                <div class="progress progress-mini">
                                    <div style="width: 60%;" class="progress-bar"></div>
                                </div>
                            </li>
                            <li>
                                <h2 class="no-margins">19,180</h2>
                                <small>Afiliaciones acumuladas del año</small>
                                <div class="stat-percent">22% <i class="fa fa-bolt text-navy"></i></div>
                                <div class="progress progress-mini">
                                    <div style="width: 22%;" class="progress-bar"></div>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script>
    $(document).ready(function () {
        var data2 = [
            [gd(2012, 9, 1), 7], [gd(2012, 9, 2), 6], [gd(2012, 9, 3), 4], [gd(2012, 9, 4), 8],
            [gd(2012, 9, 5), 9], [gd(2012, 9, 6), 7], [gd(2012, 9, 7), 5], [gd(2012, 9, 8), 4],
            [gd(2012, 9, 9), 7], [gd(2012, 9, 10), 8], [gd(2012, 9, 11), 9], [gd(2012, 9, 12), 6],
            [gd(2012, 9, 13), 4], [gd(2012, 9, 14), 5], [gd(2012, 9, 15), 11], [gd(2012, 9, 16), 8],
            [gd(2012, 9, 17), 8], [gd(2012, 9, 18), 11], [gd(2012, 9, 19), 11], [gd(2012, 9, 20), 6],
            [gd(2012, 9, 21), 6], [gd(2012, 9, 22), 8], [gd(2012, 9, 23), 11], [gd(2012, 9, 24), 13],
            [gd(2012, 9, 25), 7], [gd(2012, 9, 26), 9], [gd(2012, 9, 27), 9], [gd(2012, 9, 28), 8],
            [gd(2012, 9, 29), 5], [gd(2012, 9, 30), 8], [gd(2012, 9, 31), 25]
        ];

        var data3 = [
            [gd(2012, 9, 1), 800], [gd(2012, 9, 2), 500], [gd(2012, 9, 3), 600], [gd(2012, 9, 4), 700],
            [gd(2012, 9, 5), 500], [gd(2012, 9, 6), 456], [gd(2012, 9, 7), 800], [gd(2012, 9, 8), 589],
            [gd(2012, 9, 9), 467], [gd(2012, 9, 10), 876], [gd(2012, 9, 11), 689], [gd(2012, 9, 12), 700],
            [gd(2012, 9, 13), 500], [gd(2012, 9, 14), 600], [gd(2012, 9, 15), 700], [gd(2012, 9, 16), 786],
            [gd(2012, 9, 17), 345], [gd(2012, 9, 18), 888], [gd(2012, 9, 19), 888], [gd(2012, 9, 20), 888],
            [gd(2012, 9, 21), 987], [gd(2012, 9, 22), 444], [gd(2012, 9, 23), 999], [gd(2012, 9, 24), 567],
            [gd(2012, 9, 25), 786], [gd(2012, 9, 26), 666], [gd(2012, 9, 27), 888], [gd(2012, 9, 28), 900],
            [gd(2012, 9, 29), 178], [gd(2012, 9, 30), 555], [gd(2012, 9, 31), 993]
        ];


        var dataset = [
            {
                label: "Afiliados inscritos",
                data: data3,
                color: "#1ab394",
                bars: {
                    show: true,
                    align: "center",
                    barWidth: 24 * 60 * 60 * 600,
                    lineWidth: 0
                }

            }, {
                label: "Pagos",
                data: data2,
                yaxis: 2,
                color: "#1C84C6",
                lines: {
                    lineWidth: 1,
                    show: true,
                    fill: true,
                    fillColor: {
                        colors: [{
                            opacity: 0.2
                        }, {
                            opacity: 0.4
                        }]
                    }
                },
                splines: {
                    show: false,
                    tension: 0.6,
                    lineWidth: 1,
                    fill: 0.1
                },
            }
        ];


        var options = {
            xaxis: {
                mode: "time",
                tickSize: [3, "day"],
                tickLength: 0,
                axisLabel: "Date",
                axisLabelUseCanvas: true,
                axisLabelFontSizePixels: 12,
                axisLabelFontFamily: 'Arial',
                axisLabelPadding: 10,
                color: "#d5d5d5"
            },
            yaxes: [{
                position: "left",
                max: 1070,
                color: "#d5d5d5",
                axisLabelUseCanvas: true,
                axisLabelFontSizePixels: 12,
                axisLabelFontFamily: 'Arial',
                axisLabelPadding: 3
            }, {
                position: "right",
                clolor: "#d5d5d5",
                axisLabelUseCanvas: true,
                axisLabelFontSizePixels: 12,
                axisLabelFontFamily: ' Arial',
                axisLabelPadding: 67
            }
            ],
            legend: {
                noColumns: 1,
                labelBoxBorderColor: "#000000",
                position: "nw"
            },
            grid: {
                hoverable: false,
                borderWidth: 0
            }
        };

        function gd(year, month, day) {
            return new Date(year, month - 1, day).getTime();
        }

        var previousPoint = null, previousLabel = null;

        $.plot($("#flot-dashboard-chart"), dataset, options);
    });
</script>