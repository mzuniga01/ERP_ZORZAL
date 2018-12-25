//Edit View CheckBox
$("#fact_AlCredito").change(function () {
    if (this.checked) {
        $('#Credito').show();
    }
    else {

        $('#Credito').hide();
    }
});

$("#fact_AlCredito").ready(function () {
    if (this.checked) {
        $('#Credito').show();
    }
    else {

        $('#Credito').hide();
    }
});

$("#fact_AutorizarDescuento").change(function () {
    if (this.checked) {
        $('#Credito2').show();
    }
    else {

        $('#Credito2').hide();
    }
});

$("#fact_AutorizarDescuento").ready(function () {
    if (this.checked) {
        $('#Credito2').show();
    }
    else {

        $('#Credito2').hide();
    }
});

$(document).ready(function () {
    if (fact_AlCredito.checked) {
        $('#Credito').show();
    } else {
        $('#Credito').hide();
    }
});

$(document).ready(function () {
    if (fact_AutorizarDescuento.checked) {
        $('#Credito2').show();
    } else {
        $('#Credito2').hide();
    }
});


//Create View CheckBox
$("#fact_AlCredito").change(function () {
    if (this.checked) {
        $('#Cred1').show();
    }
    else {

        $('#Cred1').hide();
    }
});

$("#fact_AlCredito").ready(function () {
    if (this.checked) {
        $('#Cred1').show();
    }
    else {

        $('#Cred1').hide();
    }
});

$("#fact_AutorizarDescuento").change(function () {
    if (this.checked) {
        $('#Cred2').show();
    }
    else {

        $('#Cred2').hide();
    }
});

$("#fact_AutorizarDescuento").ready(function () {
    if (this.checked) {
        $('#Cred2').show();
    }
    else {

        $('#Cred2').hide();
    }
});


// Default Value
$(document).ready(function () {
    var isChecked = document.getElementById('fact_AlCredito').checked;
    if (isChecked == false) {
        $('#fact_DiasCredito').val(0);
    }
    else {
    }
});

$(document).change(function () {
    var isChecked = document.getElementById('fact_AlCredito').checked;
    if (isChecked == false) {
        $('#fact_DiasCredito').val(0);
    }
    else {
    }
});

$(document).ready(function () {
    var isChecked = document.getElementById('fact_AutorizarDescuento').checked;
    if (isChecked == false) {
        $('#fact_PorcentajeDescuento').val(0);
    }
    else {
    }
});

$(document).change(function () {
    var isChecked = document.getElementById('fact_AutorizarDescuento').checked;
    if (isChecked == false) {
        $('#fact_PorcentajeDescuento').val(0);
    }
    else {
    }
});

$("#fact_AlCredito").click(function () {
    var x = $('#fact_DiasCredito').val();
    console.log(x);
    if (x == '') {
        $('#fact_DiasCredito').val(0);
    }
    else {
        $('#fact_DiasCredito').val('');
    }
});

$("#fact_AutorizarDescuento").click(function () {
    var x = $('#fact_PorcentajeDescuento').val();
    console.log(x);
    if (x == '') {
        $('#fact_PorcentajeDescuento').val(0);
    }
    else {

        $('#fact_PorcentajeDescuento').val('');
    }
});






