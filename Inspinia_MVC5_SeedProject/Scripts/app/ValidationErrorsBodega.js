$("#bod_Nombre").change(function () {
    var nom = $("#bod_Nombre").val();
    if (nom != '') {
        valido = document.getElementById('nombre_error');
        valido.innerText = "";
    }
});

$("#bod_ResponsableBodega").change(function () {
    var res = $("#bod_ResponsableBodega").val();
    if (res != '') {
        valido = document.getElementById('responsable_error');
        valido.innerText = "";
    }
});
$("#dep_Codigo").change(function () {
    var dep = $("#dep_Codigo").val();
    if (dep != '') {
        valido = document.getElementById('depto_error');
        valido.innerText = "";
    }
});
$("#mun_Codigo").change(function () {
    var mun = $("#mun_Codigo").val();
    if (mun != '') {
        valido = document.getElementById('municipio_error');
        valido.innerText = "";
    }
});
//$("#bod_Correo").change(function () {
//    var corr = $("#bod_Correo").val();
//    if (corr != '') {
//        valido = document.getElementById('correo_error');
//        valido.innerText = "";
//    }
//});
$("#bod_Telefono").change(function () {
    var tel = $("#bod_Telefono").val();
    if (tel != '') {
        valido = document.getElementById('telefono_error');
        valido.innerText = "";
    }
});
$("#bod_Direccion").change(function () {
    var tel = $("#bod_Direccion").val();
    if (tel != '') {
        valido = document.getElementById('direccion_error');
        valido.innerText = "";
    }
});