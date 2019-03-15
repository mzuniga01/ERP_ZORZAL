$("#tent_Id").change(function () {

    var valorid = this.value;
    if (valorid == 1) {
        document.getElementById("compra").style.display = 'block';
        document.getElementById("Devolucion").style.display = 'none';
        document.getElementById("traslado").style.display = 'none';
    }
    else if (valorid == 2) {
        document.getElementById("Devolucion").style.display = 'block';
        document.getElementById("compra").style.display = 'none';
        document.getElementById("traslado").style.display = 'none';
    }
    else if (valorid == 3) {
        document.getElementById("traslado").style.display = 'block';
        document.getElementById("Devolucion").style.display = 'none';
        document.getElementById("compra").style.display = 'none';
    }
    else {
        document.getElementById("traslado").style.display = 'none';
        document.getElementById("Devolucion").style.display = 'none';
        document.getElementById("compra").style.display = 'none';
    }
});
