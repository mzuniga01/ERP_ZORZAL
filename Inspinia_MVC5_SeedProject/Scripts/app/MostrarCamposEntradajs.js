$("#tent_Id").change(function () {

    var valor = this.value;
    if (valor == 1) {
        console.log(valor);
        document.getElementById("compra").style.display = 'block';
        document.getElementById("Devolucion").style.display = 'none';
        document.getElementById("traslado").style.display = 'none';
    }
    else if (valor == 2) {
        console.log(valor);
        document.getElementById("Devolucion").style.display = 'block';
        document.getElementById("compra").style.display = 'none';
        document.getElementById("traslado").style.display = 'none';
    }
    else {
        console.log(valor);
        document.getElementById("traslado").style.display = 'block';
        document.getElementById("Devolucion").style.display = 'none';
        document.getElementById("compra").style.display = 'none';
    }
});
