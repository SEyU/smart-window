//Flot Line Chart
$(document).ready(function() {
    getDashboard();

    function getDashboard () {
        $.get('/api/ajax/tempchart', function (data) {
            // success data
            plot(data[0], data[1]);

        });
    }


    var offset = 0;
    function plot(text, tint) {
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
