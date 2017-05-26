//Flot Line Chart
$(document).ready(function() {
    getDashboard();

    function getDashboard () {
        $.get('/api/ajax/windchart', function (data) {
            // success data

            plot(data);

            console.log(data);


        });
    }


    var offset = 0;
    function plot(viento) {
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
                data: viento,
                label: "Vel. viento (Km/h)"
            }],
            options);
    }
});
