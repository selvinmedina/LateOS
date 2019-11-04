
//DESPLEIGA LA VISTA PARCIAL
$(document).on("click", "#tblListadoPrecio tbody tr td #btnDesplegarLisPreDet", function () {
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

//DESPLEGAR MODAL EDITAR SUBCATEGORIA
$(document).on("click", "#tblListadoPrecioDetalle tbody tr td #btnEditarListadoPrecioDetalle", function () {
    var ID = $(this).closest('tr').data('id');
    $.ajax({
        url: "/ListadoPrecio/_EditListadoPrecioDetalle",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: ID })
    })
         .done(function (data) {
             if (data.length > 0) {
                 $.each(data, function (i, val) {
                     
                     $("#lipd_Id").val(val.lipd_Id);
                     $("#lip_Id").val(val.lip_Id);
                     $("#prod_Id").val(val.prod_Id);
                     $("#tipd_Id").val(val.tipd_Id);
                     $("#lipd_Precio").val(val.lipd_Precio);
                     $("#lipd_ISV").val(val.lipd_ISV);
                     $("#lipd_UsuarioCrea").val(val.lipd_UsuarioCrea);

                     var Fecha = FechaFormato(val.lipd_FechaCrea);
                     $("#lipd_FechaCrea").val(Fecha);

                     $("#EditarListadoPrecioDetalle").modal();
                 })
             }
         });
});

//GUARDAR CAMBIOS LISTADO PRECIO DETALLE

$("#btnUpdateListPrecDet").click(function () {
    var data = $("#frmEditarListadoPrecioDetalle").serializeArray();
    $.ajax({
        url: "/ListadoPrecio/UpdateListadoPrecioDetalle",
        method: "POST",
        data: data
       // ,
       // success: function (result) {
       //    $("#partial").html(result);
       //}
    })
         .done(function (data) {
             $("#EditarListadoPrecioDetalle").modal('hide');
             window.location.reload();
         });
});