$(document).ready(function () {
    $("#exo_FechaIFinalVigencia").focus(function () {
        $('#ccc').html('');
    });
    $("#exo_FechaIFinalVigencia").change(function () {
        var hoy = $('#exo_FechaInicialVigencia').val();
        var fecha = $('#exo_FechaIFinalVigencia').val();
        //var fechaFormulario = Date.parse(fecha);

        if (hoy < fecha) {
            //$('#ccc').html("");
            valido = document.getElementById('ccc');
            valido.innerText = "";
            return true;
        } else {
            //$('#ccc').html("La fecha final debe ser mayor a la fecha inicial");
            valido = document.getElementById('ccc');
            valido.innerText = "La fecha final debe ser mayor a la fecha inicial";
            return false;
        }
    });
});

$(document).ready(function () {
    $("#btnGuardar").click(function () {
        var hoy = $('#exo_FechaInicialVigencia').val();
        var fecha = $('#exo_FechaIFinalVigencia').val();
        if (hoy < fecha) {
            valido = document.getElementById('ccc');
            valido.innerText = "";
        }
        else {
            valido = document.getElementById('ccc');
            valido.innerText = "La fecha final debe ser mayor a la fecha inicial";
            return false;
        }

    });
});

$(document).on("click", "#tbCliente tbody tr td button#seleccionar", function () {
    idItem = $(this).closest('tr').data('id');
    rtnItem = $(this).closest('tr').data('rtn');
    NombreCliente = $(this).closest('tr').data('name');
    $("#clte_Id").val(idItem);
    $("#tbCliente_clte_Identificacion").val(rtnItem);
    $('#ModalAgregarClientes').modal('hide');
    $("#tbCliente_clte_Nombres").val(NombreCliente)
    $("#tbCliente_clte_NombreComercial").val(NombreCliente);
});




//Validacion campos Caracteres especiales


//Exoneracion
$('#exo_Documento').on('input', function (e) {
    if (!/^[ a-z0-9áéíóúüñ]*$/i.test(this.value)) {
        this.value = this.value.replace(/[^ a-z0-9áéíóúüñ]+/ig, "");
    }
});


//Documento fiscal


function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode;
    return ((key >= 48 && key <= 57) || (key == 8))
}

//$('#dfisc_Descripcion').on('input', function (e) {
//    if (!/^[ a-z0-9áéíóúüñ]*$/i.test(this.value)) {
//        this.value = this.value.replace(/[^ a-z0-9áéíóúüñ]+/ig, "");
//    }
//});

//Documento fiscal

$('#dfisc_Id').on('input', function (e) {
    if (!/^[ a-z0-9áéíóúüñ]*$/i.test(this.value)) {
        this.value = this.value.replace(/[^ a-z0-9áéíóúüñ]+/ig, "");
    }
});


$('#dfisc_Descripcion').on('input', function (e) {
    if (!/^[ a-z0-9áéíóúüñ]*$/i.test(this.value)) {
        this.value = this.value.replace(/[^ a-z0-9áéíóúüñ]+/ig, "");
    }
});




onload = function () {
    var ele = document.querySelectorAll('.validanumericos')[0];
    ele.onkeypress = function (e) {
        if (isNaN(this.value + String.fromCharCode(e.charCode)))
            return false;
    }
    ele.onpaste = function (e) {
        e.preventDefault();
    }
}

//correo

$("#suc_Correo").blur(function () {
    campo = event.target;
    valido = document.getElementById('emailOK');

    var reg = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    var regOficial = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (reg.test(campo.value) && regOficial.test(campo.value)) {
        valido.innerText = "";
    } else if (reg.test(campo.value)) {
        valido.innerText = "";

    } else {
        valido.innerText = "Direccion de Correo Electronico Incorrecta";

    }
});

$("#suc_Correo").blur(function () {
    campo = event.target;
    valido = document.getElementById('emailOK1');

    var reg = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    var regOficial = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (reg.test(campo.value) && regOficial.test(campo.value)) {
        valido.innerText = "";
    } else if (reg.test(campo.value)) {
        valido.innerText = "";

    } else {
        valido.innerText = "Direccion de Correo Electronico Incorrecta";

    }
});

//Validacion de letras//
function soloLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    tecla = String.fromCharCode(tecla)
    return /^[a-zA-ZáéíóúñÁÉÍÓÚÑ ]+$/.test(tecla);
}
//////
$(document).ready(function () {
    $("#btnGuardar").click(function () {
        var monto = $('#exo_Documento').val();
        //var montoint = parseInt(monto);

        if (monto == '') {
            valido = document.getElementById('doc');
            valido.innerText = "El campo Documento es Requerido";
            return false;
        }
        else {
            valido = document.getElementById('doc');
            valido.innerText = "";
        }

    });
});




////////////////////////Sucursal///////////////////////////////


$(document).ready(function () {
    $("#btnGuardar").click(function () {
        var monto = $('#dep_Codigo').val();
        //var montoint = parseInt(monto);

        if (monto == '') {
            valido = document.getElementById('Departamento');
            valido.innerText = "El campo Documento es Requerido";
            return false;
        }
        else {
            valido = document.getElementById('Departamento');
            valido.innerText = "";
        }

    });
});




$(document).ready(function () {
    $("#btnGuardar").click(function () {
        var monto = $('#mun_Codigo').val();
        //var montoint = parseInt(monto);

        if (monto == '') {
            valido = document.getElementById('Municipio');
            valido.innerText = "El campo Documento es Requerido";
            return false;
        }
        else {
            valido = document.getElementById('Municipio');
            valido.innerText = "";
        }

    });
});


$(document).ready(function () {
    $("#btnGuardar").click(function () {
        var monto = $('#suc_Direccion').val();
        //var montoint = parseInt(monto);

        if (monto == '') {
            valido = document.getElementById('direccion');
            valido.innerText = "El campo Documento es Requerido";
            return false;
        }
        else {
            valido = document.getElementById('direccion');
            valido.innerText = "";
        }

    });
});


$(document).ready(function () {
    $("#btnGuardar").click(function () {
        var monto = $('#bod_Id').val();
        //var montoint = parseInt(monto);

        if (monto == '') {
            valido = document.getElementById('bod');
            valido.innerText = "El campo Documento es Requerido";
            return false;
        }
        else {
            valido = document.getElementById('bod');
            valido.innerText = "";
        }

    });
});



$(document).ready(function () {
    $("#btnGuardar").click(function () {
        var monto = $('#pemi_Id').val();
        //var montoint = parseInt(monto);

        if (monto == '') {
            valido = document.getElementById('pemi');
            valido.innerText = "El campo Documento es Requerido";
            return false;
        }
        else {
            valido = document.getElementById('pemi');
            valido.innerText = "";
        }

    });
});



$(document).ready(function () {
    $("#btnGuardar").click(function () {
        var monto = $('#suc_Telefono').val();
        //var montoint = parseInt(monto);

        if (monto == '') {
            valido = document.getElementById('telefono');
            valido.innerText = "El campo Documento es Requerido";
            return false;
        }
        else {
            valido = document.getElementById('telefono');
            valido.innerText = "";
        }

    });
});




$(document).ready(function () {
    $("#btnGuardar").click(function () {
        var monto = $('#suc_Correo').val();
        //var montoint = parseInt(monto);

        if (monto == '') {
            valido = document.getElementById('correo');
            valido.innerText = "El campo Documento es Requerido";
            return false;
        }
        else {
            valido = document.getElementById('correo');
            valido.innerText = "";
        }

    });
});


$('#suc_Descripcion').on('input', function (e) {
    if (!/^[ a-z0-9áéíóúüñ]*$/i.test(this.value)) {
        this.value = this.value.replace(/[^ a-z0-9áéíóúüñ]+/ig, "");
    }
});







