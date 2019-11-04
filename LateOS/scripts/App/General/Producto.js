$(document).on("click", "#tblProducto tbody tr td #btnDesplegarProd", function () {
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

$(document).on("click", "#tblProductoImagen tbody tr #btnEliminarProductoImagen", function () {
    debugger;
    var ID = $(this).closest('tr').data('id');
    console.log(ID);
    $.ajax({
        url: "/Producto/_EliminarProductoImagen",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ idProdImg: ID })
    });
    $(this).closest('tr').remove();
})