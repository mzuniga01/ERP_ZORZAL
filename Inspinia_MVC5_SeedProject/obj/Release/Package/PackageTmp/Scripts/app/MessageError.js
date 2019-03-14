$("#tpi_Id").change(function () {
    var tpi_Id = $("#tpi_Id").val();
    if (tpi_Id != '') {
        valido = document.getElementById('Identificacion');
        valido.innerText = "";
    }
});

$("#clte_Identificacion").change(function () {
    var clte_Identificacion = $("#clte_Identificacion").val();
    if (clte_Identificacion != '') {
        valido = document.getElementById('CIdentificacion');
        valido.innerText = "";
    }


    if (tpi_Id == 'RTN' && Identificacion.length == 14) {
        valido = document.getElementById('CIdentificacion');
        valido.innerText = "RTN debe tener 14 dígitos";
        document.getElementById("clte_Identificacion").focus();

    }
    else if (tpi_Id == 'IDENTIDAD' && Identificacion.length == 13) {
        valido = document.getElementById('CIdentificacion');
        valido.innerText = "Identidad debe tener 13 dígitos";
        document.getElementById("#clte_Identificacion").focus();
    }
});

//Cliente Juridico
$("#clte_NombreComercial").change(function () {
    var clte_NombreComercial = $("#clte_NombreComercial").val();
    console.log(clte_NombreComercial)
    if (clte_NombreComercial != '') {
        valido = document.getElementById('NombreC');
        valido.innerText = "";
    }
});

$("#clte_RazonSocial").change(function () {
    var clte_RazonSocial = $("#clte_RazonSocial").val();
    if (clte_RazonSocial != '') {
        valido = document.getElementById('RazonS');
        valido.innerText = "";
    }
});
$("#clte_Nacionalidad").on("click", function () {
    var clte_FechaConstitucion = $("#clte_FechaConstitucion").val();
    if (clte_FechaConstitucion != '') {
        valido = document.getElementById('fechaJ');
        valido.innerText = "";
    }
});
$("#clte_ContactoNombre").change(function () {
    var clte_ContactoNombre = $("#clte_ContactoNombre").val();
    if (clte_ContactoNombre != '') {
        valido = document.getElementById('ContactoN');
        valido.innerText = "";
    }
}); 

$("#clte_ContactoEmail").change(function () {
    var clte_ContactoEmail = $("#clte_ContactoEmail").val();
    if (clte_ContactoEmail != '') {
        valido = document.getElementById('ContactoN');
        valido.innerText = "";
    }
});

$("#clte_ContactoTelefono").change(function () {
    var clte_ContactoTelefono = $("#clte_ContactoTelefono").val();
    if (clte_ContactoTelefono != '') {
        valido = document.getElementById('ContactoT');
        valido.innerText = "";
    }
});
//Cliente Natural
$("#clte_Nombres").change(function () {
    var clte_Nombres = $("#clte_Nombres").val();
    if (clte_Nombres != '') {
        valido = document.getElementById('Nombres');
        valido.innerText = "";
    }
});
$("#clte_CorreoElectronico").change(function () {
    var clte_CorreoElectronico = $("#clte_CorreoElectronico").val();
    if (clte_CorreoElectronico != '') {
        valido = document.getElementById('emailOK1');
        valido.innerText = "";
    }
});
$("#clte_Sexo").click(function () {
    var clte_FechaNacimiento = $("#clte_FechaNacimiento").val();
    if (clte_FechaNacimiento != '') {
        valido = document.getElementById('fechaN');
        valido.innerText = "";
    }
});
$("#clte_Apellidos").change(function () {
    var clte_Apellidos = $("#clte_Apellidos").val();
    if (clte_Apellidos != '') {
        valido = document.getElementById('Apellidos');
        valido.innerText = "";
    }
});
$("#clte_Sexo").change(function () {
    var clte_Sexo = $("#clte_Sexo").val();
    if (clte_Sexo != '') {
        valido = document.getElementById('Sexo');
        valido.innerText = "";
    }
});
$("#clte_Telefono").change(function () {
    var clte_Telefono = $("#clte_Telefono").val();
    if (clte_Telefono != '') {
        valido = document.getElementById('TelefonoCN');
        valido.innerText = "";
    }
});


//
$("#clte_Nacionalidad").change(function () {
    var clte_Nacionalidad = $("#clte_Nacionalidad").val();
    if (clte_Nacionalidad != '') {
        valido = document.getElementById('Nacionalidad');
        valido.innerText = "";
    }
});

$("#dep_Codigo").change(function () {
    var dep_Codigo = $("#dep_Codigo").val();
    if (dep_Codigo != '') {
        valido = document.getElementById('Departamento');
        valido.innerText = "";
    }
});

$("#mun_Codigo").change(function () {
    var mun_Codigo = $("#mun_Codigo").val();
    if (mun_Codigo != '') {
        valido = document.getElementById('Municipio');
        valido.innerText = "";
    }
});



$("#guardar").click(function () {
    emailJ = $("#clte_ContactoEmail").val();
    fechaJ = $("#clte_FechaConstitucion").val();
    console.log(fechaJ)
    emailN = $("#clte_CorreoElectronico").val();
    fechaN = $("#clte_FechaNacimiento").val();
    departamento = $("#dep_Codigo").val();
    Sexos = $("#clte_Sexo").val();
    telefono = $("#clte_Telefono").val();
    telefonoJ = $("#clte_ContactoTelefono").val();

    if (clte_EsPersonaNatural.checked) {
        if (fechaN == "") {
            valido = document.getElementById('fechaN');
            valido.innerText = "El campo Fecha Nacimiento es requerido";
            return false;
        }
        if (Sexos == null) {
            valido = document.getElementById('Sexo');
            valido.innerText = "El campo Sexo es requerido";
            return false;
        }
        if (telefono == "+") {
            valido = document.getElementById('TelefonoCN');
            valido.innerText = "El campo Teléfono es requerido";
            return false;
        }
        if (emailN == "") {
            valido = document.getElementById('emailOK1');
            valido.innerText = "El campo Correo Electronico es requerido";
            return false;
        }
        if (departamento == "") {
            valido = document.getElementById('Departamento');
            valido.innerText = "El campo Departamento es requerido";
            return false;
        }
    } else {
        if (emailJ == "") {
            valido = document.getElementById('emailOK');
            valido.innerText = "El campo Correo Electronico es requerido";
            return false;
        }
        if (telefonoJ == "+") {
            valido = document.getElementById('ContactoT');
            valido.innerText = "El campo Contacto Teléfono es requerido";
            return false;
        }
        if (fechaJ == "") {
            valido = document.getElementById('fechaJ');
            valido.innerText = "El campo Fecha Constitución es requerido";
            return false;
        }
        if (departamento == "") {
            valido = document.getElementById('Departamento');
            valido.innerText = "El campo Departamento es requerido";
            return false;
        }
    }

})
