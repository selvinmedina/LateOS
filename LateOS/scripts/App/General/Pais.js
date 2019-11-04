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

$("#btnAgregarCiudad").click(function () {
    var Ciudad = $("#ciu_Descripcion").val();
    Contador += 1;
    console.log(Contador);
    var tbCiudad = {
        ciu_Id: Contador,
        ciu_Descripcion: Ciudad
    };
    var tr = "<tr data-id=" + Contador + "><td>" + Ciudad + "</td>"
    tr += "<td><button type 'button' class='btn btn-danger btn-xs' id ='QuitarCiudad'>-</button></td></tr>";

    $.ajax({
        url: "/Pais/saveCiudad",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ Ciudad: tbCiudad })
    })
        .done(function (data) {
            if (data == "Exito") {
                $("#tblCiudad tbody").append(tr);
                $("#ciu_Descripcion").val("");
                $("#ciu_Descripcion").focus();
            }
        });
});

$(document).on("click", "#tblCiudad tbody tr td #QuitarCiudad", function () {
    var ID = $(this).closest('tr').data('id');
    $.ajax({
        url: "/Pais/removeCiudad",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ IDCiu: ID })
    });
    $(this).closest('tr').remove();
});

$(document).on("click", "#tblCiudad tbody tr td #btnDesplegarSubC", function () {
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
});

//
//EDITAR SUBCATEGORIA
//
$(document).on("click", "#tblCiudad tbody tr td #btnEditarCiudad", function () {
    var ID = $(this).closest('tr').data('id');
    $.ajax({
        url: "/Pais/_EditCiudad",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: ID })
    })
    .done(function (data) {
        if (data.length > 0) {
            $.each(data, function (i, val) {
                $("#pais_Id").val(val.pais_Id);
                $("#ciu_Id").val(val.ciu_Id);
                $("#ciu_Descripcion").val(val.ciu_Descripcion);
                $("#ciu_UsuarioCrea").val(val.ciu_UsuarioCrea);
                var FechaC = FechaFormato(val.ciu_FechaCrea);
                $("#ciu_FechaCrea").val(FechaC);
                $("#EditarCiudad").modal();
            });
        }
    });

});

//
//REALIZAR EDICION DE SUBCATEGORIA
//
$("#btnUpdateCiudad").click(function () {
    var data = $("#frmEditarCiudad").serializeArray();
    $.ajax({
        url: "/Pais/UpdateCiudad",
        method: "POST",
        data: data
    })
    .done(function (data) {
        $("#EditarCiudad").modal('hide');
        window.location.reload();
    });
});