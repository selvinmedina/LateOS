var Contador = 0;
function pad2(number) {
    return (number < 10 ? '0' : '') + number
}

function FechaFormato(pFecha) {
    var fechaString = pFecha.substr(6);
    var fechaActual = new Date(parseInt(fechaString));
    var mes = fechaActual.getMonth() + 1;
    var dia = pad2(fechaActual.getDate());
    var anio = fechaActual.getFullYear();
    var hora = pad2(fechaActual.getHours());
    var minutos = pad2(fechaActual.getMinutes());
    var segundos = pad2(fechaActual.getSeconds().toString());
    var FechaFinal = dia + "/" + mes + "/" + anio + " " + hora + ":" + minutos + ":" + segundos;
    return FechaFinal;
}



$(document).on("click", "#tblFactura tbody tr td #btnDesplegarSubC", function () {
    console.log("hola")
    var ID = $(this).closest('tr').data('id');
    var fila = $("#fila-" + ID);
    if (fila.css("display") == "none") {
        $(this).closest('tr').after('<tr style="display:table-row" id="fila-' + ID + '">' + $("#fila-" + ID).html() + '</tr>');
        $(fila).remove();
        $(this).text('-');
    }
    else {
        fila = $("#fila-" + ID);
        fila.hide();
        $(this).text('+');
    }
    fila = $("#fila-" + ID);
})