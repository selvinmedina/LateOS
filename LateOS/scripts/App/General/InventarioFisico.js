
//----------------//
//ACTION - CREATE 
//----------------//
var Contador = 0;

//
//OBTENER SCRIPT DE FORMATEO DE FECHA
//
$.getScript("../scripts/App/General/SerializeDate.js")
  .done(function (script, textStatus) {
      console.log(textStatus);
  })
  .fail(function (jqxhr, settings, exception) {
      console.log("No se pudo recuperar Script SerializeDate");
  });

//
//Agregar direcciones
//
$("#btnAgregarDirecciones").click(function () {
    var Direcciones = $("#clted_Descripcion").val();
    Contador += 1;
    console.log(Contador);
    var tbClienteDirecciones = {
        clted_Id: Contador,
        clted_Descripcion: Direcciones
    };
    var tr = "<tr data-id=" + Contador + "><td>" + Direcciones + "</td>"
    tr += "<td><button type='button' class='btn btn-danger btn-xs' id='QuitarDirecciones'>-</button></td></tr>";

    $.ajax({
        url: "/Cliente/saveDirecciones",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ ClienteDireccion: tbClienteDirecciones })
    })
        .done(function (data) {
            if (data == "Exito") {
                $("#tblClienteDirecciones tbody").append(tr);
                $("#clted_Descripcion").val("");
                $("#clted_Descripcion").focus();
            }
        });
});

//
//Quitar direcciones
//
$(document).on("click", "#tblClienteDirecciones tbody tr td #QuitarDirecciones", function () {
    var ID = $(this).closest('tr').data('id');
    $.ajax({
        url: "/Cliente/removeDirecciomes",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ IDDireccion: ID })
    });
    $(this).closest('tr').remove();
})


//----------------//
//ACTION - DETALLE 
//----------------//

//Desplegar direcciones
$(document).on("click", "#tblCliente tbody tr td #btnDesplegarDireccion", function () {
    var ID = $(this).closest('tr').data('id');
    var fila = $("#fila-" + ID);
    if (fila.css("display") == "none") {
        fila.css("display", "table-row");
        $(this).text('-');
    }
    else {
        fila.css("display", "none");
        $(this).text('+');
    }
})

//----------------//
//ACTION - EDITAR 
//----------------//



//
//EDITAR CLIENTE
//
$(document).on("click", "#tblinventario tbody tr td #btnEditarInventario", function () {
    console.log("Hola");
    var ID = $(this).closest('tr').data('id');
    $.ajax({
        url: "/InventarioFisico/_EditInventario",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: ID })
    })
    .done(function (data) {
        if (data.length > 0) {
            $.each(data, function (i, val) {
               // $("#EditarInventario").modal('hide');
                $("#invf_total").val(val.invf_total);
                var fechaInventario = FechaFormato(val.invf_FechaInventario);
                $("#invf_FechaInventario").val(fechaInventario);
                $("#prod_Codigo").val(val.prod_Codigo);
                $("#EditarInventario").modal();
            });
        }
    });
});

//
//REALIZAR EDICION DE CLIENTE
//
$("#btnUpdateInv").click(function () {
    var data = $("#frmInventario").serializeArray();
    $.ajax({
        url: "/InventarioFisico/UpdateInventario",
        method: "POST",
        data: data
    })
    .done(function (data) {
        //console.log(data); Debug
        $("#EditarInventario").modal('hide');
        window.location.reload();
    });
});

//
//REDIRECT TO EDIT
//
$(document).on("click", "#RedirectToEdit", function () {
    console.log("Hola");
    var ID = $("#Detalle #clte_Id").val();
    $.ajax({
        url: "/Cliente/_EditCliente",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: ID })
    })
    .done(function (data) {
        if (data.length > 0) {
            $.each(data, function (i, val) {
                $("#DetalleCliente").modal('hide');
                $("#Editar #clte_Id").val(val.clte_Id);
                $("#Editar #clte_Identidad").val(val.clte_Identidad);
                $("#Editar #clte_Nombre").val(val.clte_Nombre);
                $("#Editar #clte_Apellido").val(val.clte_Apellido);
                var fechaNacimiento = FechaFormatoNac(val.clte_FechaNacimiento);
                $("#Editar #clte_FechaNacimiento").val(fechaNacimiento);
                $("#Editar #clte_Sexo").val(val.clte_Sexo);
                $("#Editar #clte_Telefono").val(val.clte_Telefono);
                $("#Editar #clte_Correo").val(val.clte_Correo);
                var fechaCrea = FechaFormato(val.clte_FechaCrea);
                $("#Editar #clte_UsuarioCrea").val(fechaCrea);
                $("#Editar #clte_FechaCrea").val(val.clte_FechaCrea);
                $("#EditarCliente").modal();
            });
        }
    });
});


//----------------//
//ACTION - DETALLE 
//----------------//

//
//REALIZAR EDICION DE CLIENTE
//
$("#btnUpdateClie").click(function () {
    var data = $("#frmEditarClie").serializeArray();
    $.ajax({
        url: "/Cliente/UpdateCliente",
        method: "POST",
        data: data
    })
    .done(function () {
        //console.log(data); Debug
        $("#EditarCliente").modal('hide');
        $.ajax({
            url: "/Cliente/Index"
        })
        .done(function () {
            console.log("Recarga Exitosa");
        })
        .fail(function () {
            console.log("No se pudo recargar la pagina");
        });
    });
});

//
//Detalle CLIENTE
//
$(document).on("click", "#tblCliente tbody tr td #btnDetalleCliente", function () {
    //console.log("Hola");
    var ID = $(this).closest('tr').data('id');
    $.ajax({
        url: "/Cliente/_EditCliente",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: ID })
    })
    .done(function (data) {
        if (data.length > 0) {
            console.log(data);
            $.each(data, function (i, val) {
                $("#Detalle #clte_Id").val(val.clte_Id);
                $("#Detalle #clte_Identidad").val(val.clte_Identidad);
                $("#Detalle #clte_Nombre").val(val.clte_Nombre);
                $("#Detalle #clte_Apellido").val(val.clte_Apellido);
                var fechaNacimiento = FechaFormatoNac(val.clte_FechaNacimiento);
                $("#Detalle #clte_FechaNacimiento").val(fechaNacimiento);
                $("#Detalle #clte_Sexo").val(val.clte_Sexo);
                $("#Detalle #clte_Telefono").val(val.clte_Telefono);
                $("#Detalle #clte_Correo").val(val.clte_Correo);
                var fechaCrea = FechaFormato(val.clte_FechaCrea);
                $("#Detalle #clte_UsuarioCrea").val(val.clte_UsuarioCrea);
                $("#Detalle #clte_FechaCrea").val(fechaCrea);
                $("#DetalleCliente").modal();
            });
        }
    });

});