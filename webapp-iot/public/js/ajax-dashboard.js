var urlGetDashboard = "/api/ajax/dashboard";

var lastData = { tint : "", text: "", viento: "", humedad: "",  modo: "", iluminacion: "", ventana: "", toldo: ""};

/* Ajax function - Get information about sensor */
function getDashboard () {
    $.get(urlGetDashboard, function (data) {

        if(  checkChange(data)){
            updateData(data);
            var today = new Date();
            var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
            $('#info-actualizado').html( time);
            $('#div-alert-update').removeClass('hidden');
        }


    });
}

function checkChange (data) {
    var result = false;
    $.each(lastData, function(index, value) {
        console.log( data[index] !== value );
        if(  data[index] !== value   ){
            result = true;
        }
    });

    return result;
}

function updateData(data) {
    lastData.tint = data.tint;
    $('#Tint').html(data.tint);

    lastData.text = data.text;
    $('#Text').html(data.text);

    lastData.viento = data.viento;
    $('#Viento').html(data.viento);

    lastData.humedad = data.humedad;
    $('#Humedad').html(data.humedad);

    lastData.modo = data.modo;
    switch (data.modo){
        case "0":
            $('#div-auto').removeClass('hidden');
            $('#div-man').addClass('hidden');
            break;
        case "1":
            $('#div-auto').addClass('hidden');
            $('#div-man').removeClass('hidden');
            break;
        default:
            break;
    }

    lastData.iluminacion = data.iluminacion;
    $('#Ilum').html(data.iluminacion);

    lastData.ventana = data.ventana;
    $('#Ventana').html(data.ventana);

    lastData.toldo = data.toldo;
    $('#Toldo').html(data.toldo);
}

$(document).ready(function () {

    setInterval(function() {
        getDashboard();
    }, 1000 * 5);
});