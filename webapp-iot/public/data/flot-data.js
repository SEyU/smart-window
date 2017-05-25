//Flot Line Chart
$(document).ready(function() {

    var offset = 0;
    plot();

    function plot() {
        var tint = [],
            text = [];
            var hoy = 1495304109840;
        for (var x = 0; x < 50; x++) {
            hoy+=5000000;
            tint.push([ hoy, Math.floor((Math.random() * 25) + 10)]);
            text.push([ hoy, Math.floor((Math.random() * 25) + 10)]);
        }

        var options = {
            series: {
                lines: {
                    show: true
                },
                points: {
                    show: true
                }
            },
            grid: {
                hoverable: true //IMPORTANT! this is needed for tooltip to work
            },
            xaxes: [{
                mode: 'time'
            }],
            tooltip: true,
            tooltipOpts: {
                content: "'%s' of %x.1 is %y.4",
                xDateFormat: "%y-%0m-%0d",
                shifts: {
                    x: -60,
                    y: 25
                }
            }
        };

        var plotObj = $.plot($("#flot-line-chart"), [{
                data: tint,
                label: "Tª Interior"
            }, {
                data: text,
                label: "Tª Exterior"
            }],
            options);
    }
});
