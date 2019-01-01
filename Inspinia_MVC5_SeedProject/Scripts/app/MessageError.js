$("#tpi_Id").blur(function () {
    var tpi_Id = $("#tpi_Id").val();
    if (tpi_Id != '') {
        valido = document.getElementById('Identificacion');
        valido.innerText = "";
    }
});

$("#clte_Identificacion").blur(function () {
    var clte_Identificacion = $("#clte_Identificacion").val();
    if (clte_Identificacion != '') {
        valido = document.getElementById('CIdentificacion');
        valido.innerText = "";
    }
});
//Cliente Juridico
$("#clte_NombreComercial").blur(function () {
    var clte_NombreComercial = $("#clte_NombreComercial").val();
    console.log(clte_NombreComercial)
    if (clte_NombreComercial != '') {
        valido = document.getElementById('NombreC');
        valido.innerText = "";
    }
});

$("#clte_RazonSocial").blur(function () {
    var clte_RazonSocial = $("#clte_RazonSocial").val();
    if (clte_RazonSocial != '') {
        valido = document.getElementById('RazonS');
        valido.innerText = "";
    }
});

$("#clte_ContactoNombre").blur(function () {
    var clte_ContactoNombre = $("#clte_ContactoNombre").val();
    if (clte_ContactoNombre != '') {
        valido = document.getElementById('ContactoN');
        valido.innerText = "";
    }
});

$("#clte_ContactoTelefono").blur(function () {
    var clte_ContactoTelefono = $("#clte_ContactoTelefono").val();
    if (clte_ContactoTelefono != '') {
        valido = document.getElementById('ContactoT');
        valido.innerText = "";
    }
});
//Cliente Natural
$("#clte_Nombres").blur(function () {
    var clte_Nombres = $("#clte_Nombres").val();
    if (clte_Nombres != '') {
        valido = document.getElementById('Nombres');
        valido.innerText = "";
    }
});

$("#clte_Apellidos").blur(function () {
    var clte_Apellidos = $("#clte_Apellidos").val();
    if (clte_Apellidos != '') {
        valido = document.getElementById('Apellidos');
        valido.innerText = "";
    }
});
$("#clte_Sexo").blur(function () {
    var clte_Sexo = $("#clte_Sexo").val();
    if (clte_Sexo != '') {
        valido = document.getElementById('Sexo');
        valido.innerText = "";
    }
});
$("#clte_Telefono").blur(function () {
    var clte_Telefono = $("#clte_Telefono").val();
    if (clte_Telefono != '') {
        valido = document.getElementById('TelefonoCN');
        valido.innerText = "";
    }
});


//
$("#clte_Nacionalidad").blur(function () {
    var clte_Nacionalidad = $("#clte_Nacionalidad").val();
    if (clte_Nacionalidad != '') {
        valido = document.getElementById('Nacionalidad');
        valido.innerText = "";
    }
});

$("#dep_Codigo").blur(function () {
    var dep_Codigo = $("#dep_Codigo").val();
    if (dep_Codigo != '') {
        valido = document.getElementById('Departamento');
        valido.innerText = "";
    }
});

$("#mun_Codigo").blur(function () {
    var mun_Codigo = $("#mun_Codigo").val();
    if (mun_Codigo != '') {
        valido = document.getElementById('Municipio');
        valido.innerText = "";
    }
});