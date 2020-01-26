$(document).ready(function () {
    $(tbCliente_clte_EsPersonaNatural).hide();

    if (tbCliente_clte_EsPersonaNatural.checked) {
        $('#natural').show();
        $('#juridica').hide();
        console.log(1);
    } else {
        $('#natural').hide();
        $('#juridica').show();

        console.log(2);

    }
});