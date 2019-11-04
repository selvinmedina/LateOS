console.log('Cargado');

$(document).on("click", "#addProduct", function () {
    console.log("Click");
    var ID = $(this).data('id');
    console.log('Id ' + ID);
    $.ajax({
        url: "/Carrito/AddProduct",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: ID })
    });
});

$(document).on("click", "#tblCart tbody tr td #SumCart", function () {
    console.log("Click Suma");
    var ID = $(this).data('id');
    console.log('Id ' + ID);
    $.ajax({
        url: "/Carrito/AddProduct",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: ID })
    });
});

$(document).on("click", "#tblCart tbody tr td #RestCart", function () {
    console.log("Click Resta");
    var ID = $(this).data('id');
    console.log('Id ' + ID);
    $.ajax({
        url: "/Carrito/RestProduct",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: ID })
    });
});

$(document).on("click", "#tblCart tbody tr td div #DeleteCart", function () {
    console.log("Click Eliminar");
    var ID = $(this).data('id');
    console.log('Id ' + ID);
    $.ajax({
        url: "/Carrito/DeleteProduct",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: ID })
    });
});

$(document).on("click", "#RealizarPago", function () {
    console.log("Click");
    var ID = $(this).data('id');
    console.log('Id ' + ID);
    $.ajax({
        url: "/Carrito/FacturaCarrito",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ id: ID })
    });
});