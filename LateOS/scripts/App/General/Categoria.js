var Contador = 0;

//
//OBTENER SCRIPT DE FORMATEO DE FECHA
//
//$.getScript("../scripts/App/General/SerializeDate.js")
//  .done(function (script, textStatus) {
//      console.log(textStatus);
//  })
//  .fail(function (jqxhr, settings, exception) {
//      console.log("No se pudo recuperar Script SerializeDate");
//  });

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


//----------------//
//ACTION - CREAR 
//----------------//

//
//AGREGAR SUBCATEGORIA
//
$("#btnAgregarSubcategoria").click(function () {
    var SubCategoria = $("#subc_Descripcion").val();
    Contador += 1;
    console.log(Contador);
    var tbSubCategoria = {
        subc_Id: Contador,
        subc_Descripcion: SubCategoria
    };
    var tr = "<tr data-id=" + Contador + "><td>" + SubCategoria + "</td>"
        tr+= "<td><button type='button' class='btn btn-danger btn-xs' id='QuitarSubCategoria'>-</button></td></tr>";

    $.ajax({
        url: "/Categorias/saveSubCategoria",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ Subcategoria: tbSubCategoria })
    })
        .done(function (data) {
            if (data == "Exito") {
                $("#tblSubcategoria tbody").append(tr);
                $("#subc_Descripcion").val("");
                $("#subc_Descripcion").focus();
            }
        });
});

//
//REMOVER SUBCATEGORIA
//
$(document).on("click", "#tblSubcategoria tbody tr td #QuitarSubCategoria", function ()
{
    var ID = $(this).closest('tr').data('id');
    $.ajax({
        url: "/Categorias/removeSubCategoria",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ IDSub: ID })
    });
    $(this).closest('tr').remove();
})

//
//DESPLEGAR SUBCATEGORIAS
//
$(document).on("click", "#tblCategoria tbody tr td #btnDesplegarSubC", function () {
    var ID = $(this).closest('tr').data('id');
    var fila = $("#fila-" + ID);
    if(fila.css("display")== "none")
    {
        $(this).closest('tr').after('<tr style="display:table-row" id="fila-' + ID + '">' + $("#fila-" + ID).html() + '</tr>');
        $(fila).remove();     
        $(this).text('-');
    }
    else
    {
        fila = $("#fila-" + ID);        
        fila.hide();
        $(this).text('+');
    }
    fila = $("#fila-" + ID);
})

//----------------//
//ACTION - EDITAR 
//----------------//



//
//EDITAR SUBCATEGORIA
//
$(document).on("click", "#tblSubCategoria tbody tr td #btnEditarSubCategoria", function () {
    var ID = $(this).closest('tr').data('id');
    $.ajax({
        url: "/Categorias/_EditSubCategoria",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: ID })
    })
    .done(function (data) {
        if(data.length>0)
        {
            $.each(data, function (i, val) {
                $("#cat_Id").val(val.cat_Id);
                $("#subc_Id").val(val.subc_Id);
                $("#subc_Descripcion").val(val.subc_Descripcion);
                $("#subc_UsuarioCrea").val(val.subc_UsuarioCrea);
                var FechaC = FechaFormato(val.subc_FechaCrea);                
                $("#subc_FechaCrea").val(FechaC);
                $("#EditarSubCategoria").modal();
            });
        }
    });
    
});

//
//REALIZAR EDICION DE SUBCATEGORIA
//
$("#btnUpdateSubCat").click(function () {
    var data = $("#frmEditarSubCat").serializeArray();
    $.ajax({
        url: "/Categorias/UpdateSubCategoria",
        method: "POST",
        data: data
    })
    .done(function (data) {
        console.log(data);
        $("#EditarSubCategoria").modal('hide');
        window.location = "index";
    });
});

