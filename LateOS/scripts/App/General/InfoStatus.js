//
//Cargar Graficos
//

//Obtener Usuarios Por genero
var TotalRegistrados = "";
var Mujeres = "";
var Hombres = "";
$.ajax({
    url: "/Home/GET_Gender",
    method: "POST",
    dataType: "json",
    contentType: "application/json; charset=utf-8",
    data: JSON.stringify({ Cantidad: 1 })
})
.done(function (data) {
    var array = data.split("%");
    //debugger;
    TotalRegistrados = array[0];
    Mujeres = array[1];
    Hombres = array[2];

    //Generacion del chart Donnut
    //
    google.charts.load("current", { packages: ["corechart"] });
    google.charts.setOnLoadCallback(drawChart);
    function drawChart() {
        var data = google.visualization.arrayToDataTable([
          ['Task', 'Generos'],
          [' Mujeres', (Mujeres * 1.0)],
          ['Hombres', (Hombres * 1.0)]
        ]);

        var options = {
            title: 'Usuarios de la plataforma',
            pieHole: 0.4,
        };
        var chart = new google.visualization.PieChart(document.getElementById('donutchart'));
        chart.draw(data, options);
    }
});

//
//Variables contenedoras de los valores

var Total_Usuarios = Math.random(1, 20);
var Online = Math.random(2, 4);
var Offline = Math.random(5, 9);
//Generacion del chart Donnut
//
google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {

    var data = google.visualization.arrayToDataTable([
      ['Task', 'Estado'],
      [' Conectados', (Online * 1.0)],
      ['Desconectados', (Offline * 1.0)]
    ]);
    var options = {
        title: 'Usuarios de LateOS'
    };
    var chart = new google.visualization.PieChart(document.getElementById('piechart'));
    chart.draw(data, options);
}


setInterval(function () { 

    Total_Usuarios = Math.random(1, 20);
    Online = Math.random(2,4);
    Offline = Math.random(5,9);
        //Generacion del chart Donnut
        //
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {

            var data = google.visualization.arrayToDataTable([
              ['Task', 'Estado'],
              [' Conectados', (Online * 1.0)],
              ['Desconectados', (Offline * 1.0)]
            ]);
            var options = {
                title: 'Usuarios de LateOS'
            };
            var chart = new google.visualization.PieChart(document.getElementById('piechart'));
            chart.draw(data, options);
        }


    //Obtener Usuarios Por genero
    var TotalRegistrados = "";
    var Mujeres = "";
    var Hombres = "";
    $.ajax({
        url: "/Home/GET_Gender",
        method: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ Cantidad: 1 })
    })
    .done(function (data) {
        var array = data.split("%");
        //debugger;
        TotalRegistrados = array[0];
        Mujeres = array[1];
        Hombres = array[2];

        //Generacion del chart Donnut
        //
        google.charts.load("current", { packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([
              ['Task', 'Generos'],
              [' Mujeres', (Mujeres * 1.0)],
              ['Hombres', (Hombres * 1.0)]
            ]);

            var options = {
                title: 'Usuarios de la plataforma',
                pieHole: 0.4,
            };
            var chart = new google.visualization.PieChart(document.getElementById('donutchart'));
            chart.draw(data, options);
        }
    });

}, 20000);