 
$("#tent_Id").change(function () {

    var valor = this.value;
    if (valor == 1) {
        console.log(valor);
        document.getElementById("CamposCompra").style.display = 'block';
        document.getElementById("CamposDevoluciones").style.display = 'none';
        document.getElementById("CamposTraslado").style.display = 'none';
    }
    else if (valor == 2) {
        console.log(valor);
        document.getElementById("CamposDevoluciones").style.display = 'block';
        document.getElementById("CamposCompra").style.display = 'none';
        document.getElementById("CamposTraslado").style.display = 'none';
    }
    else {
        console.log(valor);
        document.getElementById("CamposTraslado").style.display = 'block';
        document.getElementById("CamposDevoluciones").style.display = 'none';
        document.getElementById("CamposCompra").style.display = 'none';
    }
});